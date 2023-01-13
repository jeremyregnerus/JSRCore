namespace JSR.BaseClasses
{
    /// <summary>
    /// Base ViewModel object for MVVM environments.
    /// </summary>
    public class ViewModelBase : BaseNotifyPropertyChangedNotifyChangedMessenger, IRequestViewClose
    {
        private DelegateCommand? requestViewCloseCommand;

        /// <inheritdoc/>
        public event EventHandler? RequestViewCloseEvent;

        /// <summary>
        /// Gets a command to request the view to close.
        /// </summary>
        public DelegateCommand RequestCloseCommand { get => requestViewCloseCommand ??= new DelegateCommand(RequestViewClose); }

        /// <summary>
        /// Request closing this object's ViewModel.
        /// </summary>
        protected void RequestViewClose()
        {
            RequestViewCloseEvent?.Invoke(this, new EventArgs());
        }
    }
}
