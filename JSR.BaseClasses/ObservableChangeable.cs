using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="INotifyPropertyChanged"/> with <see cref="IChangeTracking"/>.
    /// </summary>
    public class ObservableChangeable : Observable, INotifyChanged
    {
        private bool isChanged = true;

        /// <inheritdoc/>
        public event OnChangedEventHandler? OnChanged;

        /// <inheritdoc/>
        public virtual bool IsChanged
        {
            get => isChanged;

            protected set
            {
                if (value != isChanged)
                {
                    isChanged = value;
                    NotifyPropertyChanged();
                    OnChanged?.Invoke(this, isChanged);
                }
            }
        }

        /// <inheritdoc/>
        public virtual void AcceptChanges()
        {
            IsChanged = false;
        }

        /// <inheritdoc/>
        protected override bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (base.SetProperty(ref field, value, propertyName))
            {
                IsChanged = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds <see cref="INotifyChanged"/> notification tracking for an object if it implements <see cref="INotifyChanged"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to track notifications.</typeparam>
        /// <param name="child">Object to track notifications.</param>
        protected void AddChildChangeTracking<T>(T child)
        {
            if (child is INotifyChanged changed)
            {
                changed.OnChanged += OnChildChanged;
            }
        }

        /// <summary>
        /// Removes <see cref="INotifyChanged"/> notification tracking from an object if it implements <see cref="INotifyChanged"/>.
        /// </summary>
        /// <typeparam name="T">Type of object to no longer track notifications.</typeparam>
        /// <param name="child">Object to no longer track notifications.</param>
        protected void RemoveChildChangeTracking<T>(T child)
        {
            if (child is INotifyChanged changed)
            {
                changed.OnChanged -= OnChildChanged;
            }
        }

        /// <summary>
        /// Changes <see cref="IsChanged"/> when a child raises <see cref="OnChanged"/> with a value of true.
        /// </summary>
        /// <param name="sender">Object raising <see cref="OnChangedEventHandler"/>.</param>
        /// <param name="wasChanged">True if the object was changed, otherwise false.</param>
        protected void OnChildChanged(object sender, bool wasChanged)
        {
            if (wasChanged)
            {
                IsChanged = true;
            }
        }
    }
}
