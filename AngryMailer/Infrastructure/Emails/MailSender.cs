using AngryMailer.Domain.Entities;
using System.Threading.Tasks;

namespace AngryMailer.Domain
{
    /// <summary>
    ///     Default implementation of <see cref="IMailSender"/>. See that interface's docs for more details.
    /// </summary>
    public class MailSender : IMailSender
    {
        /// <inheritdoc cref="IMailSender.SendMail(Email)"/>
        public Task SendMail(Email email)
        {
            throw new System.NotImplementedException();
        }
    }
}
