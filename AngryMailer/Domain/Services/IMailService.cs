using AngryMailer.Domain.Entities;

namespace AngryMailer.Domain.Services
{
    /// <summary>
    ///     Represents an object that can send emails to recipients.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        ///     Asychronously sends the given email.
        /// </summary>
        /// <param name="email">Email to send.</param>
        void Send(Email email);
    }
}
