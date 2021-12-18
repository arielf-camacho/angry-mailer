using AngryMailer.Domain;
using AngryMailer.Tests.Utils;
using AngryMailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NSubstitute;
using AngryMailer.Domain.Entities;
using AngryMailer.ViewModels.Commands;
using AngryMailer.Domain.Services;

namespace AngryMailer.Tests.ViewModels
{
    [TestClass]
    public class SendMailViewModelTests
    {
        private static readonly Random _random = new(Environment.TickCount);

        private Email _email;
        private SendMailCommand? _sendMailCommand;

        private SendMailViewModel? _subject;


        [TestInitialize]
        public void Initialize()
        {
            _sendMailCommand = new SendMailCommand(
                Substitute.For<IMailService>(), Substitute.For<IAngerDetectionService>());

            _email = new Email("some@mail.com", "Hello", "Hi friend");
            _subject = new SendMailViewModel(_sendMailCommand)
            {
                ToAddress = _email.ToAddress,
                Subject = _email.Subject,
                Content = _email.Content
            };
        }


        [TestMethod]
        public void Content_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Content));
            var emailWatcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Email));

            // When
            _subject!.Content = newValue;

            // When
            Assert.IsTrue(watcher.NotifiedChange);
            Assert.IsTrue(emailWatcher.NotifiedChange);
            Assert.AreEqual(newValue, _subject!.Content);
        }

        [TestMethod]
        public void Error_EmailDataIsInvalid_ReturnsGenericValidationError()
        {
            // Given
            _subject!.ToAddress = string.Empty;

            // When
            var validationError = _subject.Error;

            // Then
            Assert.AreEqual(validationError, "Email data is invalid");
        }

        [TestMethod]
        public void Error_EmailDataIsValid_ReturnsEmptyString()
        {
            // When
            var validationError = _subject!.Error;

            // Then
            Assert.AreEqual(string.Empty, validationError);
        }

        [TestMethod]
        public void Indexer_GivenFieldIsInvalid_ReturnsSpecificFieldValidationError()
        {
            // Given
            _subject!.ToAddress = string.Empty;

            // When
            var validationError = _subject[nameof(SendMailViewModel.ToAddress)];

            // Then
            Assert.AreEqual(validationError, $"'{nameof(SendMailViewModel.ToAddress)}' field must not be null or empty");
        }

        [TestMethod]
        public void Indexer_GivenFieldIsInvalid_ReturnsEmptyString()
        {
            // Given
            _subject!.ToAddress = string.Empty;

            // When
            var validationError = _subject.Error;

            // Then
            Assert.AreEqual("Email data is invalid", validationError);
        }

        [TestMethod]
        public void SendCommand_IsOfCorrectType()
        {
            // When
            var type = _subject!.SendMailCommand.GetType();

            // Then
            Assert.AreEqual(typeof(SendMailCommand).FullName, type.FullName);
        }

        [TestMethod]
        public void Subject_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Subject));
            var emailWatcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Email));

            // When
            _subject!.Subject = newValue;

            // When
            Assert.IsTrue(watcher.NotifiedChange);
            Assert.IsTrue(emailWatcher.NotifiedChange);
            Assert.AreEqual(newValue, _subject!.Subject);
        }

        [TestMethod]
        public void ToAddress_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.ToAddress));
            var emailWatcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Email));

            // When
            _subject!.ToAddress = newValue;

            // When
            Assert.IsTrue(watcher.NotifiedChange);
            Assert.IsTrue(emailWatcher.NotifiedChange);
            Assert.AreEqual(newValue, _subject!.ToAddress);
        }
    }
}
