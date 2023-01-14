﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace JSR.BaseClasses
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> that implements <see cref="IMessenger"/> on the collection and items.
    /// </summary>
    /// <typeparam name="T">Type within the collection.</typeparam>
    public class ObservableMessengerCollection<T> : ObservableCollection<T>, IMessenger
    {
        private string message = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableMessengerCollection{T}"/> class.
        /// </summary>
        public ObservableMessengerCollection()
        {
            AddListItemMessaging();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableMessengerCollection{T}"/> class.
        /// </summary>
        /// <param name="collection"><see cref="IEnumerable{T}"/> of the list type.</param>
        public ObservableMessengerCollection(IEnumerable<T> collection) : base(collection)
        {
            AddListItemMessaging();
        }

        /// <inheritdoc/>
        public event OnMessageEventHandler? OnMessage;

        /// <inheritdoc/>
        public string Message
        {
            get => message;

            set
            {
                if (value != message)
                {
                    message = value;
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(Message)));
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        private void CollectionListChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems != null)
            {
                foreach (var item in args.OldItems.OfType<IMessenger>())
                {
                    RemoveMessenger(item);
                }
            }

            if (args.NewItems != null)
            {
                foreach (var item in args.NewItems.OfType<IMessenger>())
                {
                    AddMessenger(item);
                }
            }
        }

        private void AddListItemMessaging()
        {
            CollectionChanged += CollectionListChanged;

            foreach (var item in Items)
            {
                if (item is IMessenger messenger)
                {
                    AddMessenger(messenger);
                }
            }
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

        private void OnChildMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
