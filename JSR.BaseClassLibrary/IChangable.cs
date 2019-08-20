// <copyright file="IChangable.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// This event handler is raised and an IChangeable Object changes.
    /// </summary>
    /// <param name="sender">the object sending the event.</param>
    /// <param name="isChanged">Boolean value stating if the object is changed or not.</param>
    public delegate void OnChangedEventHandler(object sender, bool isChanged);

    /// <summary>
    /// IChangableObject implements INotifyPropertyChanged and IChangeTracking together.
    /// </summary>
    public interface IChangable : INotifyPropertyChanged, IChangeTracking
    {
        /// <summary>
        /// This event is raised when the object's IsChanged value changes.
        /// </summary>
        event OnChangedEventHandler OnChanged;
    }
}
