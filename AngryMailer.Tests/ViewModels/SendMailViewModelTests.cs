using AngryMailer.Domain;
using AngryMailer.Tests.Utils;
using AngryMailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NSubstitute;
using System.Threading.Tasks;
using AngryMailer.Domain.Entities;
using System.Text;

namespace AngryMailer.Tests.ViewModels
{
    [TestClass]
    public class SendMailViewModelTests
    {
        private static readonly Random _random = new(Environment.TickCount);

        private IMailService _mailService;

        private SendMailViewModel? _subject;


        [TestInitialize]
        public void Initialize()
        {
            _mailService = Substitute.For<IMailService>();

            _subject = new SendMailViewModel(_mailService);
        }


        [TestMethod]
        public void Content_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Content));

            // When
            _subject!.Content = newValue;

            // When
            Assert.AreEqual(newValue, _subject!.Content);
            Assert.IsTrue(watcher.NotifiedChange);
        }

        [TestMethod]
        public void SendCommand_CanAlwaysExecute()
        {
            // When
            var canExecute = _subject!.SendCommand.CanExecute(null);

            // Then
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void SendCommand_Execute_SendsEmailWithViewModelData()
        {
            const string email = "john@mail.com";
            const string emailSubject = "Happy birthday";
            var contentBuilder = new StringBuilder("Hello John:");
            contentBuilder.AppendLine("Hope you are ok, my friend. I wanted to wish you happy birthday.");
            contentBuilder.AppendLine("Have the best day in the world.");
            contentBuilder.AppendLine("Yours,");
            contentBuilder.AppendLine("James");
            var content = contentBuilder.ToString();

            _subject!.ToEmail = email;
            _subject!.Subject = emailSubject;
            _subject!.Content = content;

            // When
            _subject!.SendCommand.Execute(null);

            // Then
            _mailService
                .Received()
                .Send(Arg.Is<Email>(email => email.Content == content && email.ToEmail == email.ToEmail && email.Subject == emailSubject));
        }

        [TestMethod]
        public void Subject_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Subject));

            // When
            _subject!.Subject = newValue;

            // When
            Assert.AreEqual(newValue, _subject!.Subject);
            Assert.IsTrue(watcher.NotifiedChange);
        }

        [TestMethod]
        public void To_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.ToEmail));

            // When
            _subject!.ToEmail = newValue;

            // When
            Assert.AreEqual(newValue, _subject!.ToEmail);
            Assert.IsTrue(watcher.NotifiedChange);
        }
    }
}
