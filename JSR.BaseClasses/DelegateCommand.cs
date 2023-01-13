using System.Windows.Input;

namespace JSR.BaseClasses
{
    /// <summary>
    /// Provides a default implementation of the <see cref="ICommand"/> interface for use within MVVM and WPF.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool>? canExecute;

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
        public DelegateCommand(Action execute, Func<bool>? canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;

            if (this.canExecute != null)
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            if (canExecute is null)
            {
                return true;
            }

            return canExecute();
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            execute();
        }
    }
}
