// <copyright file="IYesNoCancelMessage.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

namespace JSR.BaseClassLibrary.UI
{
    /// <summary>
    /// Message returns for IYesNoCancelMessage.
    /// </summary>
    public enum YesNoCancelEnum
    {
        /// <summary>
        /// Answer was yes.
        /// </summary>
        Yes,

        /// <summary>
        /// Answer was no.
        /// </summary>
        No,

        /// <summary>
        /// Answer was cancel.
        /// </summary>
        Cancel,
    }

    /// <summary>
    /// An interface for standard message inquiries with yes, no and cancel returns.
    /// </summary>
    public interface IYesNoCancelMessage
    {
        /// <summary>
        /// Request the user to answer a question using Yes, No or Cancel.
        /// </summary>
        /// <param name="message">Message to ask the user.</param>
        /// <returns>Answer to the question.</returns>
        YesNoCancelEnum RequestMessage(string message);
    }
}
