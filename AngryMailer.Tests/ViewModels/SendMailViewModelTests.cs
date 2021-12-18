using AngryMailer.Domain;
using AngryMailer.Domain.Entities;
using AngryMailer.Domain.Services;
using AngryMailer.Tests.Utils;
using AngryMailer.ViewModels;
using AngryMailer.ViewModels.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace AngryMailer.Tests.ViewModels
{
    [TestClass]
    public class SendMailViewModelTests
    {
        private static readonly Random _random = new(Environment.TickCount);

        private IAngerDetectionService _angerDetectionService;

        private Email? _email;
        private SendMailCommand? _sendMailCommand;

        private SendMailViewModel? _subject;


        [TestInitialize]
        public void Initialize()
        {
            _angerDetectionService = Substitute.For<IAngerDetectionService>();
            _sendMailCommand = new SendMailCommand(Substitute.For<IMailService>(), _angerDetectionService);

            _email = new Email("some@mail.com", "Hello", "Hi friend");

            _subject = new SendMailViewModel(_sendMailCommand, _angerDetectionService)
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

            // Then
            Assert.IsTrue(watcher.NotifiedChange);
            Assert.IsTrue(emailWatcher.NotifiedChange);
            Assert.AreEqual(newValue, _subject!.Content);
            _angerDetectionService.Received().CountStroke();
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

            // Then
            Assert.IsTrue(watcher.NotifiedChange);
            Assert.IsTrue(emailWatcher.NotifiedChange);
            Assert.AreEqual(newValue, _subject!.Subject);
            _angerDetectionService.Received().CountStroke();
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

            // Then
            Assert.IsTrue(watcher.NotifiedChange);
            Assert.IsTrue(emailWatcher.NotifiedChange);
            Assert.AreEqual(newValue, _subject!.ToAddress);
            _angerDetectionService.Received().CountStroke();
        }
    }
}
