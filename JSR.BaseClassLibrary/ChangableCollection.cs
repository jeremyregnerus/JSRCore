// <copyright file="ChangableCollection.cs" company="Jeremy Regnerus">
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
    /// Provides an <see cref="ObservableCollection{T}"/> that also implements <see cref="IChangableObject"/>.
    /// </summary>
    /// <typeparam name="T">Type of List objects that implement <see cref="IChangableObject"/>.</typeparam>
    public class ChangableCollection<T> : ObservableCollection<T>, IChangableObject where T : IChangableObject
    {
        private bool isChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableCollection{T}"/> class.
        /// </summary>
        public ChangableCollection() : base()
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">An IEnumberable of the Collection Type implementing <see cref="IChangableObject"/>.</param>
        public ChangableCollection(IEnumerable<T> collection) : base(collection)
        {
            OnCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangableCollection{T}"/> class.
        /// </summary>
        /// <param name="list">A List of the Collection Type implementing <see cref="IChangableObject"/>.</param>
        public ChangableCollection(List<T> list) : base(list)
        {
            OnCreated();
        }

        /// <inheritdoc/>
        public event OnChangedEventHandler OnChanged;

        /// <summary>
        /// Gets a value indicating whether this object or any items in the collection has changed.
        /// </summary>
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

        /// <summary>
        /// Accepts changes to this object and its collection.
        /// </summary>
        public void AcceptChanges()
        {
            foreach (IChangeTracking item in Items)
            {
                item.AcceptChanges();
            }

            IsChanged = false;
        }

        /// <summary>
        /// This method is called by the serializer when this object is deserialized.
        /// </summary>
        /// <param name="s">Streaming Context to deserialize..</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext s)
        {
            OnCreated();
        }

        /// <summary>
        /// When this object is created this method should be called to register change notification events for all items in the collection.
        /// </summary>
        private void OnCreated()
        {
            CollectionChanged += CollectionItemsChanged;

            foreach (T item in Items)
            {
                item.OnChanged += (x, y) => IsChanged = true;
            }

            AcceptChanges();
        }

        /// <summary>
        /// This method should be called whenever the collection changes
        /// This includes new items being added and existing items being removed.
        /// </summary>
        /// <param name="sender">The object requesting to send the change notification.</param>
        /// <param name="args">Notify Collection Changed Event Arguments that contains lists of items added or removed from the collection.</param>
        private void CollectionItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                foreach (IChangableObject item in args.NewItems)
                {
                    item.OnChanged += (x, y) => IsChanged = true;
                }
            }

            if (args.OldItems != null)
            {
                foreach (IChangableObject item in args.OldItems)
                {
                    item.OnChanged -= (x, y) => IsChanged = true;
                }
            }

            IsChanged = true;
        }
    }
}
