// <copyright file="DelegateCommand.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Windows.Input;

namespace JSRBaseClassLibrary
{
    /// <summary>
    /// Provides a default implementation of the <see cref="ICommand"/> interface for use within MVVM and WPF.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="exectute">Action to execute.</param>
        public DelegateCommand(Action exectute) : this(exectute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Function specifying if this action can execute when called.</param>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;

            if (this.canExecute != null)
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raised when the ability for the Action to execute changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Checks in the Action can execute.
        /// </summary>
        /// <param name="parameter">Unused in this object, required by the Interface.</param>
        /// <returns>Returns true if the Action can execute.</returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute();
            }
        }

        /// <summary>
        /// Executes the Action of this object.
        /// </summary>
        /// <param name="parameter">Unused in this object, required by the Interface.</param>
        public void Execute(object parameter)
        {
            execute();
        }
    }
}
