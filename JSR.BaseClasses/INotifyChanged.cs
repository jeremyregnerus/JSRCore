﻿using System.ComponentModel;

namespace JSR.BaseClasses
{
    /// <summary>
    /// This event handler is raised and an IChangeable Object changes.
    /// </summary>
    /// <param name="sender">the object sending the event.</param>
    /// <param name="isChanged">Boolean value stating if the object is changed or not.</param>
    public delegate void OnChangedEventHandler(object sender, bool isChanged);

    /// <summary>
    /// IChangableObject implements <see cref="INotifyPropertyChanged"/> and <see cref="IChangeTracking"/>. Also provides an event that raises when the object is changed.
    /// </summary>
    public interface INotifyChanged : IChangeTracking
    {
        /// <summary>
        /// This event is raised when the object's IsChanged value changes.
        /// </summary>
        event OnChangedEventHandler OnChanged;
    }
}
