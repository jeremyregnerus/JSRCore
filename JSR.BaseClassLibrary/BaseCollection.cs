// <copyright file="BaseCollection.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Collection of objects that can track <see cref="OnChanged"/> and <see cref="OnMessage"/>.
    /// </summary>
    /// <typeparam name="T">Type of object within the Collection.</typeparam>
    public class BaseCollection<T> : ObservableCollection<T>, IList<T>, IList, INotifyChanged, INotifyPropertyChanged, IChangeTracking, IMessenger
    {
        private bool isChanged;
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCollection{T}"/> class.
        /// </summary>
        public BaseCollection() : base()
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCollection{T}"/> class.
        /// </summary>
        /// <param name="collection"><see cref="IEnumerable"/> of the list type.</param>
        public BaseCollection(IEnumerable<T> collection) : base(collection)
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCollection{T}"/> class.
        /// </summary>
        /// <param name="list"><see cref="IList"/> of the list type.</param>
        public BaseCollection(List<T> list) : base(list)
        {
            OnCreated();
        }

        /// <inheritdoc/>
        public event OnMessageEventHandler OnMessage;

        /// <inheritdoc/>
        public event OnChangedEventHandler OnChanged;

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
            foreach (IChangeTracking item in Items)
            {
                item.AcceptChanges();
            }

            IsChanged = false;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext s)
        {
            OnCreated();
        }

        private void OnCreated()
        {
            CollectionChanged += CollectionListChanged;

            foreach (INotifyChanged item in Items)
            {
                AddChangable(item);
            }

            foreach (IMessenger item in Items)
            {
                AddMessenger(item);
            }
        }

        private void CollectionListChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems != null)
            {
                foreach (INotifyChanged item in args.OldItems)
                {
                    RemoveChangable(item);
                }

                foreach (IMessenger item in args.OldItems)
                {
                    RemoveMessenger(item);
                }
            }

            if (args.NewItems != null)
            {
                foreach (INotifyChanged item in args.NewItems)
                {
                    AddChangable(item);
                }

                foreach (IMessenger item in args.NewItems)
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

        private void AddChangable(INotifyChanged changable)
        {
            if (changable != null)
            {
                changable.OnChanged += OnChildChanged;
            }
        }

        private void RemoveChangable(INotifyChanged changable)
        {
            if (changable != null)
            {
                changable.OnChanged -= OnChildChanged;
            }
        }

        private void OnChildMessage(object sender, string message)
        {
            Message = message;
        }

        private void AddMessenger(IMessenger messenger)
        {
            if (messenger != null)
            {
                messenger.OnMessage += OnChildMessage;
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
