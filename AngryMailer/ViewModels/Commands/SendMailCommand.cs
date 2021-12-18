using AngryMailer.Domain;
using AngryMailer.Domain.Entities;
using AngryMailer.Domain.Services;
using System;

namespace AngryMailer.ViewModels.Commands
{
    /// <summary>
    ///     Command that is used to send emails given its data.
    /// </summary>
    public class SendMailCommand : CommandBase
    {
        private readonly IMailService _mailService;
        private readonly IAngerDetectionService _angerDetector;


        /// <summary>
        ///     Initializes a new instance of <see cref="SendMailCommand"/> command given the mail service.
        /// </summary>
        /// <param name="mailService">Service used to send emails.</param>
        /// <param name="angerDetector">Service used to determine whether the user is angry.</param>
        public SendMailCommand(IMailService mailService, IAngerDetectionService angerDetector)
        {
            _angerDetector = angerDetector;
            _mailService = mailService;
        }


        /// <summary>
        ///     Determines whether there can executed the current command.
        /// </summary>
        /// <param name="parameter">The <see cref="Email"/> representing the email to send.</param>
        /// <returns>
        ///     A boolean expressing whether or not there can be sent the given email. Returns True if
        ///     <paramref name="parameter"/> is of type <see cref="Email"/>, it's not null or in invalid
        ///     state.
        /// </returns>
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter) && parameter is Email email && email.IsValid && !_angerDetector.IsAngry;
        }

        /// <summary>
        ///     Sends the given email.
        /// </summary>
        /// <param name="parameter">The <see cref="Email"/> representing the email to send.</param>
        /// <exception cref="ArgumentNullException">parameters is null.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameter"/> is not of type <see cref="Email"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="parameter"/> is not a valid <see cref="Email"/> instance.
        /// </exception>
        public override void Execute(object? parameter)
        {
            if (parameter is Email email) _mailService.Send(email);
        }
    }
}
