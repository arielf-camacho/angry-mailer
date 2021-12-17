using AngryMailer.Domain.Entities;
using System.Threading.Tasks;

namespace AngryMailer.Domain
{
    /// <summary>
    ///     Represents an object that can send emails to recipients.
    /// </summary>
    public interface IMailSender
    {
        /// <summary>
        ///     Asychronously sends the given email.
        /// </summary>
        /// <param name="email">Email to send.</param>
        /// <returns>
        ///     A <see cref="Task"/> that eventually performs the action of sending the provided <paramref name="email"/>.
        /// </returns>
        Task SendMail(Email email);
    }
}
