using AngryMailer.Domain.Entities;
using AngryMailer.Domain.Services;
using AngryMailer.ViewModels.Commands;
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

        private readonly SendMailCommand _sendMailCommand;
        private readonly IAngerDetectionService _angerDetectionService;
        
        private Email _email;


        /// <summary>
        ///     Initializes a new instance of <see cref="SendMailViewModel"/>.
        /// </summary>
        /// <param name="sendMailCommand">
        ///     Command that is used to send emails.
        /// </param>
        /// <param name="angerDetectionService">
        ///     Service used to detect whether the user is angry.
        /// </param>
        public SendMailViewModel(SendMailCommand sendMailCommand, IAngerDetectionService angerDetectionService)
        {
            _email = new Email(_to, _subject, _content);

            _sendMailCommand = sendMailCommand;
            _angerDetectionService = angerDetectionService;
        }


        public Email Email => _email;

        /// <summary>
        ///     Gets the command used to Send emails.
        /// </summary>
        public ICommand SendMailCommand => _sendMailCommand;

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
                NotifyPropertyChanged(nameof(Email));

                _angerDetectionService.CountStroke();
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
                NotifyPropertyChanged(nameof(Email));

                _angerDetectionService.CountStroke();
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
                NotifyPropertyChanged(nameof(Email));

                _angerDetectionService.CountStroke();
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
    }
}
