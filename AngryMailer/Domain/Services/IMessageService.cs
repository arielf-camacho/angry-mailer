namespace AngryMailer.Domain.Services
{
    /// <summary>
    ///     Represents the contract fulfilled by an object allowing to show messages on the UI for the user to consume.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        ///     Shows the given message in a Message Box.
        /// </summary>
        /// <param name="message">The message to show.</param>
        void ShowMessage(string message);
    }
}
