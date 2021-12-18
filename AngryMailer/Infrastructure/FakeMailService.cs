using AngryMailer.Domain.Entities;
using AngryMailer.Domain.Services;
using System.Windows;

namespace AngryMailer.Infrastructure
{
    /// <summary>
    ///     Default implementation of <see cref="IMailService"/>. See that interface's docs for more details.
    ///     This is a fake implementation, since it does not send real emails.
    /// </summary>
    public class FakeMailService : IMailService
    {
        private readonly IMessageService _messageService;


        /// <summary>
        ///     Initializes a new instance of <see cref="FakeMailService"/> instance with a message service.
        /// </summary>
        /// <param name="messageService">Use to show dialogs (alerts, notifications) to the user.</param>
        public FakeMailService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <inheritdoc cref="IMailService.Send(Email)"/>
        public void Send(Email email)
        {
            // Simulating the email is being sent.
            MessageBox.Show(email.ToString());
        }
    }
}
