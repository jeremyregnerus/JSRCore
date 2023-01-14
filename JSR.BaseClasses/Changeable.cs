namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="INotifyChanged"/>.
    /// </summary>
    public abstract class Changeable : INotifyChanged
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
                    OnChanged?.Invoke(this, value);
                }
            }
        }

        /// <inheritdoc/>
        public void AcceptChanges()
        {
            IsChanged = false;
        }

        /// <summary>
        /// Sets the value property and changes the <see cref="IsChanged"/> if the value changes.
        /// </summary>
        /// <typeparam name="T">Type of property value to set.</typeparam>
        /// <param name="field">Field containing the property value.</param>
        /// <param name="value">Value to assign to the property.</param>
        /// <returns>True if the value was changed, otherwise false.</returns>
        protected virtual bool SetProperty<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            RemoveChildChangeTracking(field);
            field = value;
            AddChildChangeTracking(field);

            return true;
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
        private void OnChildChanged(object sender, bool wasChanged)
        {
            if (wasChanged)
            {
                IsChanged = true;
            }
        }
    }
}
