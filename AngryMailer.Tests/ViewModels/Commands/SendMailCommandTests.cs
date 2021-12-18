using AngryMailer.Domain;
using AngryMailer.Domain.Entities;
using AngryMailer.Domain.Services;
using AngryMailer.ViewModels.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AngryMailer.Tests.ViewModels.Commands
{
    [TestClass]
    public class SendMailCommandTests
    {
        private Email _email = new("person@mail.com", "Hey!", "Hope you are OK");

        private IAngerDetectionService _angerDetectionService;
        private IMailService _mailService;

        private SendMailCommand? _subject;


        [TestInitialize]
        public void Initialize()
        {
            _angerDetectionService = Substitute.For<IAngerDetectionService>();
            _mailService = Substitute.For<IMailService>();

            _subject = new SendMailCommand(_mailService, _angerDetectionService);
        }


        [TestMethod]
        public void CanExecute_EmailIsNull_ReturnsFalse()
        {
            // When
            var canExecute = _subject!.CanExecute(null);

            // Then
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void CanExecute_ParametersIsNotAnEmail_ReturnsFalse()
        {
            // When
            var canExecute = _subject!.CanExecute(1);

            // Then
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void CanExecute_EmailIsNotValid_ReturnsFalse()
        {
            // Given
            _email = _email with { ToAddress = string.Empty };

            // When
            var canExecute = _subject!.CanExecute(_email);

            // Then
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void CanExecute_UserIsAngry_ReturnsFalse()
        {
            // Given
            _angerDetectionService.IsAngry.Returns(true);

            // When
            var canExecute = _subject!.CanExecute(_email);

            // Then
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void CanExecute_EmailDataIsValid_ReturnsTrue()
        {
            // When
            var canExecute = _subject!.CanExecute(_email);

            // Then
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void Execute_GivenNotEmail_SendsNothing()
        {
            // When
            _subject!.Execute(null);

            // Then
            _mailService.Received(0).Send(Arg.Any<Email>());
        }

        [TestMethod]
        public void Execute_GivenEmail_SendsEmail()
        {
            // When
            _subject!.Execute(_email);

            // Then
            _mailService.Received().Send(_email);
        }
    }
}
