using AngryMailer.Domain;
using AngryMailer.Domain.Entities;
using System;
using System.Windows.Input;

namespace AngryMailer.ViewModels
{
    /// <summary>
    ///     Represents the view models.
    /// </summary>
    public class SendMailViewModel : ViewModelBase
    {
        private string _to = string.Empty;
        private string _subject = string.Empty;
        private string _content = string.Empty;

        private IMailService _mailSender;


        /// <summary>
        ///     Initializes a new instance of <see cref="SendMailViewModel"/>.
        /// </summary>
        /// <param name="mailSender">
        ///     Used to send emails, given the data specified in the current view model.
        /// </param>
        public SendMailViewModel(IMailService mailSender)
        {
            _mailSender = mailSender;
            SendCommand = new Command(ExecuteSendCommand);
        }


        /// <summary>
        ///     Gets the command used to Send emails.
        /// </summary>
        public ICommand SendCommand { get; }

        /// <summary>
        ///     Gets or sets the email address to send the email to.
        /// </summary>
        public string ToEmail
        {
            get => _to;
            set
            {
                if (_to == value) return;

                _to = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the subject to send in the email.
        /// </summary>
        public string Subject
        {
            get => _subject;
            set
            {
                if (_subject == value) return;

                _subject = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the content of the email.
        /// </summary>
        public string Content
        {
            get => _content;
            set
            {
                if (_content == value) return;

                _content = value;
                NotifyPropertyChanged();
            }
        }


        private void ExecuteSendCommand(object? obj)
        {
            var email = new Email(ToEmail, Subject, Content);

            _mailSender.Send(email);
        }
    }
}
