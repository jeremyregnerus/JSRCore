// <copyright file="MessagingCollection.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Provides an <see cref="ObservableCollection{T}"/> that also implements <see cref="IMessenger"/>.
    /// </summary>
    /// <typeparam name="T">Type of list objects that implement <see cref="IMessenger"/>.</typeparam>
    public class MessagingCollection<T> : ObservableCollection<T>, IMessenger where T : IMessenger
    {
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingCollection{T}"/> class.
        /// </summary>
        public MessagingCollection() : base()
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">An IEnumberable of the Collection Type implementing <see cref="IMessenger"/>.</param>
        public MessagingCollection(IEnumerable<T> collection) : base(collection)
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagingCollection{T}"/> class.
        /// </summary>
        /// <param name="list">A List of the Collection Type implementing <see cref="IMessenger"/>.</param>
        public MessagingCollection(List<T> list) : base(list)
        {
            OnCreated();
        }

        /// <inheritdoc/>
        public event OnMessageEventHandler OnMessage;

        /// <inheritdoc/>
        public string Message
        {
            get => message;

            protected set
            {
                if (value != message)
                {
                    message = value;
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        /// <summary>
        /// Called when this object is deserialized.
        /// </summary>
        /// <param name="s">Streaming object provided by the deserializer.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext s)
        {
            OnCreated();
        }

        private void OnCreated()
        {
            CollectionChanged += CollectionListChanged;

            foreach (T item in Items)
            {
                item.OnMessage += CollectionItemRaisedMessage;
            }
        }

        private void CollectionListChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems != null)
            {
                foreach (IMessenger item in args.OldItems)
                {
                    item.OnMessage -= CollectionItemRaisedMessage;
                }
            }

            if (args.NewItems != null)
            {
                foreach (IMessenger item in args.NewItems)
                {
                    item.OnMessage += CollectionItemRaisedMessage;
                }
            }
        }

        private void CollectionItemRaisedMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
