// <copyright file="DelegateCommandParam.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Windows.Input;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Provides a default implementation of the <see cref="ICommand"/> interface for use within MVVM and WPF that utilizes a parameter.
    /// </summary>
    public class DelegateCommandParam : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommandParam"/> class.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public DelegateCommandParam(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommandParam"/> class.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Function specifying if this action can execute when called.</param>
        public DelegateCommandParam(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;

            if (this.canExecute != null)
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute(parameter);
            }
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
