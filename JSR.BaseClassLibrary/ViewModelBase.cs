﻿// <copyright file="ViewModelBase.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Base ViewModel object for MVVM environments.
    /// </summary>
    public class ViewModelBase : BaseClass, IRequestViewClose
    {
        private DelegateCommand requestViewCloseCommand;

        /// <inheritdoc/>
        public event EventHandler RequestViewCloseEvent;

        /// <summary>
        /// Gets a command to request the view to close.
        /// </summary>
        public DelegateCommand RequestCloseCommand { get => requestViewCloseCommand ?? (requestViewCloseCommand = new DelegateCommand(RequestViewClose)); }

        /// <summary>
        /// Request closing this object's ViewModel.
        /// </summary>
        protected void RequestViewClose()
        {
            RequestViewCloseEvent?.Invoke(this, new EventArgs());
        }
    }
}
