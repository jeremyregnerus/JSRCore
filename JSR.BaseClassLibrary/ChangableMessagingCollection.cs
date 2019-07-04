﻿// <copyright file="ChangableMessagingCollection.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Provides an <see cref="ObservableCollection{T}"/> that also implements <see cref="IChangableObject"/> and <see cref="IMessenger"/>.
    /// </summary>
    /// <typeparam name="T">Type of List objects that implement <see cref="IChangableObject"/> and <see cref="IMessenger"/>.</typeparam>
    public class ChangableMessagingCollection<T> : ObservableCollection<T>, IChangableObject, IMessenger where T : IChangableObject, IMessenger
    {
        private string message;

        private bool isChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableMessagingCollection{T}"/> class.
        /// </summary>
        public ChangableMessagingCollection() : base()
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableMessagingCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">An IEnumberable of the Collection Type implementing both <see cref="IChangableObject"/> and <see cref="IMessenger"/>.</param>
        public ChangableMessagingCollection(IEnumerable<T> collection) : base(collection)
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableMessagingCollection{T}"/> class.
        /// </summary>
        /// <param name="list">A List of the Collection Type implementing both <see cref="IChangableObject"/> and <see cref="IMessenger"/>.</param>
        public ChangableMessagingCollection(List<T> list) : base(list)
        {
            OnCreated();
        }

        /// <inheritdoc/>
        public event OnChangedEventHandler OnChanged;

        /// <inheritdoc/>
        public event OnMessageEventHandler OnMessage;

        /// <inheritdoc/>
        public bool IsChanged
        {
            get => isChanged;

            private set
            {
                if (value != isChanged)
                {
                    isChanged = value;
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(IsChanged)));
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
                    OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(Message)));
                    OnMessage?.Invoke(this, message);
                }
            }
        }

        /// <inheritdoc/>
        public void AcceptChanges()
        {
            foreach (IChangableObject item in Items)
            {
                item.AcceptChanges();
            }

            IsChanged = false;
        }

        /// <summary>
        /// Called when this object is deserialized.
        /// </summary>
        /// <param name="s">Streaming Context provided by the deserializer.</param>
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
                item.OnChanged += CollectionItemChanged;
                item.OnMessage += CollectionItemRaisedMessage;
            }

            AcceptChanges();
        }

        private void CollectionListChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems != null)
            {
                foreach (T item in args.OldItems)
                {
                    item.OnChanged -= CollectionItemChanged;
                    item.OnMessage -= CollectionItemRaisedMessage;
                }
            }

            if (args.NewItems != null)
            {
                foreach (T item in args.NewItems)
                {
                    item.OnChanged += CollectionItemChanged;
                    item.OnMessage += CollectionItemRaisedMessage;
                }
            }

            IsChanged = true;
        }

        private void CollectionItemChanged(object sender, bool isChanged)
        {
            IsChanged = true;
        }

        private void CollectionItemRaisedMessage(object sender, string message)
        {
            Message = message;
        }
    }
}
