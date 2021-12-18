using AngryMailer.Domain;
using AngryMailer.Domain.Entities;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace AngryMailer.ViewModels
{
    /// <summary>
    ///     Represents the view models.
    /// </summary>
    public class SendMailViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _to = string.Empty;
        private string _subject = string.Empty;
        private string _content = string.Empty;
        private Email _email;

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
            _email = new Email(_to, _subject, _content);

            SendEmailCommand = new Command(ExecuteSendMailCommand, CanSendEmailCommand);
        }


        /// <summary>
        ///     Gets the command used to Send emails.
        /// </summary>
        public ICommand SendEmailCommand { get; }

        /// <summary>
        ///     Gets or sets the email address to send the email to.
        /// </summary>
        public string ToAddress
        {
            get => _to;
            set
            {
                if (_to == value) return;

                _to = value;
                _email = _email with { ToAddress = _to };
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
                _email = _email with { Subject = _subject };
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
                _email = _email with { Content = _content };
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets an error that describes overall what can be wrong with the email data.
        /// </summary>
        public string Error => _email.IsValid ? string.Empty : "Email data is invalid";

        /// <summary>
        ///     Gets the validation error that can exist for the specified property.
        /// </summary>
        /// <param name="columnName">Name of the field to validate.</param>
        /// <returns>
        ///     A string representing the validation error that can exist in the property name as 
        ///     <paramref name="columnName"/>; or empty string if property is valid.
        /// </returns>
        public string this[string columnName] => _email.Validate(columnName) ?? string.Empty;

        private bool CanSendEmailCommand(object? arg) => _email.IsValid;

        private void ExecuteSendMailCommand(object? obj) => _mailSender.Send(_email);
    }
}
