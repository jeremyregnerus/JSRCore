// <copyright file="IMessenger.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace JSRBaseClassLibrary
{
    /// <summary>
    /// This event handler is raised when a message is raised by an object.
    /// </summary>
    /// <param name="sender">Object raiding the message.</param>
    /// <param name="message">The message text.</param>
    public delegate void OnMessageEventHandler(object sender, string message);

    /// <summary>
    /// IMessenger raises a PropertyChange notification when a Property called Message is changed.
    /// </summary>
    public interface IMessenger : INotifyPropertyChanged
    {
        /// <summary>
        /// Event raised when the Message changes.
        /// </summary>
        event OnMessageEventHandler OnMessage;

        /// <summary>
        /// Gets the Message from this object.
        /// </summary>
        string Message { get; }
    }
}
