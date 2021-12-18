using AngryMailer.Domain.Entities;
using System.Threading.Tasks;

namespace AngryMailer.Domain
{
    /// <summary>
    ///     Default implementation of <see cref="IMailService"/>. See that interface's docs for more details.
    /// </summary>
    public class MailSender : IMailService
    {
        /// <inheritdoc cref="IMailService.Send(Email)"/>
        public Task Send(Email email)
        {
            throw new System.NotImplementedException();
        }
    }
}
