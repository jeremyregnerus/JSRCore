using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace JSR.BaseClasses
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> that implements <see cref="INotifyChanged"/> on the collection and items.
    /// </summary>
    /// <typeparam name="T">Type within the collection.</typeparam>
    public class ObservableChangeableCollection<T> : ObservableCollection<T>, INotifyChanged
    {
        private bool isChanged = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableChangeableCollection{T}"/> class.
        /// </summary>
        public ObservableChangeableCollection()
        {
            AddListItemChangeTracking();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableChangeableCollection{T}"/> class.
        /// </summary>
        /// <param name="collection"><see cref="IEnumerable{T}"/> of the list type.</param>
        public ObservableChangeableCollection(IEnumerable<T> collection) : base(collection)
        {
            AddListItemChangeTracking();
        }

        /// <inheritdoc/>
        public event OnChangedEventHandler? OnChanged;

        /// <inheritdoc/>
        public bool IsChanged
        {
            get => isChanged;
            set
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
        public void AcceptChanges()
        {
            foreach (var item in Items.OfType<IChangeTracking>())
            {
                item.AcceptChanges();
            }

            IsChanged = false;
        }

        private void CollectionListChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems != null)
            {
                foreach (var item in args.OldItems.OfType<INotifyChanged>())
                {
                    RemoveItemChangeTracking(item);
                }
            }

            if (args.NewItems != null)
            {
                foreach (var item in args.NewItems.OfType<INotifyChanged>())
                {
                    AddItemChangeTracking(item);
                }
            }

            IsChanged = true;
        }

        private void AddListItemChangeTracking()
        {
            CollectionChanged += CollectionListChanged;

            foreach (var item in Items)
            {
                if (item is INotifyChanged changable)
                {
                    AddItemChangeTracking(changable);
                }
            }
        }

        private void AddItemChangeTracking(INotifyChanged item)
        {
            item.OnChanged += OnChildChanged;
        }

        private void RemoveItemChangeTracking(INotifyChanged item)
        {
            item.OnChanged -= OnChildChanged;
        }

        private void OnChildChanged(object sender, bool wasChanged)
        {
            IsChanged = true;
        }
    }
}
