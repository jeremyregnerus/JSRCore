namespace JSR.BaseClasses
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
