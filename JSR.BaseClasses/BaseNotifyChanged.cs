﻿// <copyright file="BaseNotifyChanged.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="INotifyChanged"/>.
    /// </summary>
    public abstract class BaseNotifyChanged : BaseChangeTracking, INotifyChanged
    {
        /// <inheritdoc/>
        public event OnChangedEventHandler? OnChanged;

        /// <inheritdoc/>
        public override bool IsChanged
        {
            get => base.IsChanged;

            protected set
            {
                if (value != base.IsChanged)
                {
                    base.IsChanged = value;
                    OnChanged?.Invoke(this, value);
                }
            }
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

        /// <inheritdoc/>
        protected override bool SetValue<T>(ref T field, T value)
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
