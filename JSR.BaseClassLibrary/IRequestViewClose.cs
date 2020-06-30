// <copyright file="IRequestViewClose.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Interface for requesting closing a viewmodel.
    /// </summary>
    public interface IRequestViewClose
    {
        /// <summary>
        /// Event to request closing a ViewModel within an interface.
        /// </summary>
        event EventHandler RequestViewCloseEvent;
    }
}
