﻿using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Collection of objects that can track <see cref="OnChanged"/> and <see cref="OnMessage"/>.
    /// </summary>
    /// <typeparam name="T">Type of object within the Collection.</typeparam>
    public class BaseCollection<T> : ObservableCollection<T>, IList<T>, IList, INotifyPropertyChanged, IChangeTracking, INotifyChanged, IMessenger
    {
        private bool isChanged;
        private string message = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCollection{T}"/> class.
        /// </summary>
        public BaseCollection() : base()
        {
            AddListItemNotification();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCollection{T}"/> class.
        /// </summary>
        /// <param name="collection"><see cref="IEnumerable"/> of the list type.</param>
        public BaseCollection(IEnumerable<T> collection) : base(collection)
        {
            AddListItemNotification();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCollection{T}"/> class.
        /// </summary>
        /// <param name="list"><see cref="IList"/> of the list type.</param>
        public BaseCollection(List<T> list) : base(list)
        {
            AddListItemNotification();
        }

        /// <inheritdoc/>
        public event OnChangedEventHandler? OnChanged;

        /// <inheritdoc/>
        public event OnMessageEventHandler? OnMessage;

        /// <inheritdoc/>
        public bool IsChanged
        {
            get => isChanged;
            private set
            {
                if (value != isChanged)
                {
                    isChanged = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsChanged)));
                    OnChanged?.Invoke(this, isChanged);
                }
            }
        }

        /// <inheritdoc/>
        public string Message
        {
            get => message;
            private set
            {
                if (value != message)
                {
                    message = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Message)));
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        /// <inheritdoc/>
        public void AcceptChanges()
        {
            foreach (IChangeTracking item in Items.OfType<IChangeTracking>())
            {
                item.AcceptChanges();
            }

            IsChanged = false;
        }

        /// <summary>
        /// Adds <see cref="INotifyChanged"/> and <see cref="IMessenger"/> tracking to list items.
        /// </summary>
        protected void AddListItemNotification()
        {
            CollectionChanged += CollectionListChanged;

            foreach (var item in Items)
            {
                if (item is INotifyChanged notify)
                {
                    AddChangable(notify);
                }

                if (item is IMessenger messenger)
                {
                    AddMessenger(messenger);
                }
            }
        }

        private void AddChangable(INotifyChanged changable)
        {
            if (changable != null)
            {
                changable.OnChanged += OnChildChanged;
            }
        }

        private void AddMessenger(IMessenger messenger)
        {
            if (messenger != null)
            {
                messenger.OnMessage += OnChildMessage;
            }
        }

        private void CollectionListChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems != null)
            {
                foreach (var item in args.OldItems.OfType<INotifyChanged>())
                {
                    RemoveChangable(item);
                }

                foreach (var item in args.OldItems.OfType<IMessenger>())
                {
                    RemoveMessenger(item);
                }
            }

            if (args.NewItems != null)
            {
                foreach (var item in args.NewItems.OfType<INotifyChanged>())
                {
                    AddChangable(item);
                }

                foreach (var item in args.NewItems.OfType<IMessenger>())
                {
                    AddMessenger(item);
                }
            }

            IsChanged = true;
        }

        private void OnChildChanged(object sender, bool wasChanged)
        {
            IsChanged = true;
        }

        private void OnChildMessage(object sender, string message)
        {
            Message = message;
        }

        private void RemoveChangable(INotifyChanged changable)
        {
            if (changable != null)
            {
                changable.OnChanged -= OnChildChanged;
            }
        }

        private void RemoveMessenger(IMessenger messenger)
        {
            if (messenger != null)
            {
                messenger.OnMessage -= OnChildMessage;
            }
        }
    }
}
