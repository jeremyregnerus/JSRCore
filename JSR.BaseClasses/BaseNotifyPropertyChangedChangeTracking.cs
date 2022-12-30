// <copyright file="BaseNotifyPropertyChangedChangeTracking.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Base implementation of <see cref="INotifyPropertyChanged"/> with <see cref="IChangeTracking"/>.
    /// </summary>
    public class BaseNotifyPropertyChangedChangeTracking : BaseNotifyPropertyChanged, IChangeTracking
    {
        private bool isChanged = true;

        /// <inheritdoc/>
        public virtual bool IsChanged
        {
            get
            {
                if (isChanged)
                {
                    return true;
                }

                foreach (PropertyInfo property in GetType().GetProperties())
                {
                    if (property.GetValue(this) is IChangeTracking tracking)
                    {
                        if (tracking.IsChanged)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            protected set
            {
                if (value != isChanged)
                {
                    isChanged = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <inheritdoc/>
        public virtual void AcceptChanges()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.GetValue(this) is IChangeTracking tracking)
                {
                    tracking.AcceptChanges();
                }
            }

            IsChanged = false;
        }

        /// <inheritdoc/>
        protected override bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (base.SetValue(ref field, value, propertyName))
            {
                IsChanged = true;
                return true;
            }

            return false;
        }
    }
}
