using AngryMailer.Domain.Entities;
using System.Windows;

namespace AngryMailer.Domain
{
    /// <summary>
    ///     Default implementation of <see cref="IMailService"/>. See that interface's docs for more details.
    /// </summary>
    public class MailService : IMailService
    {
        /// <inheritdoc cref="IMailService.Send(Email)"/>
        public void Send(Email email)
        {
            // Simulating the email is being sent.
            MessageBox.Show(email.ToString());
        }
    }
}
