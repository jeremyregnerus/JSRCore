using System.Windows.Input;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Provides a default implementation of the <see cref="ICommand"/> interface for use within MVVM and WPF that utilizes a parameter.
    /// </summary>
    public class RelayObjectCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool>? canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayObjectCommand"/> class.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public RelayObjectCommand(Action<object?> execute)
        {
            this.execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayObjectCommand"/> class.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Function specifying if this action can execute when called.</param>
        public RelayObjectCommand(Action<object?> execute, Func<object?, bool>? canExecute) : this(execute)
        {
            this.canExecute = canExecute;

            if (this.canExecute != null)
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <inheritdoc/>
        /// <remarks>
        /// There are many examples where CanExectute is handled by CommandManager using the following
        ///
        /// {
        ///    add { CommandManager.RequerySuggested += value; }
        ///    remove { CommandManager.RequerySuggested -= value; }
        /// }
        ///
        /// This may need to be implemented after, as this class library is not flagged for WPF or Windows.
        /// </remarks>
        public event EventHandler? CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            if (canExecute == null)
            {
                return true;
            }

            return canExecute(parameter);
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
