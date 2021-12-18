using AngryMailer.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace AngryMailer.Tests.Domain.Entities
{
    [TestClass]
    public class EmailTests
    {
        private Email? _subject;


        [TestInitialize]
        public void Initialize()
        {
            _subject = new("someone@mail.com", "Big thanks", "Some email content");
        }


        [TestMethod]
        public void IsValid_EmailObjectToAddressFieldIsInvalid_ReturnsFalse()
        {
            // Given
            _subject = _subject! with { ToAddress = string.Empty };

            // When
            var result = _subject.IsValid;

            // Then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_EmailObjectSubjectFieldIsInvalid_ReturnsFalse()
        {
            // Given
            _subject = _subject! with { Subject = string.Empty };

            // When
            var result = _subject.IsValid;

            // Then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_EmailObjectContentFieldIsInvalid_ReturnsFalse()
        {
            // Given
            _subject = _subject! with { Content = string.Empty };

            // When
            var result = _subject.IsValid;

            // Then
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToString_ReturnsTheWholeComposedEmailContent()
        {
            // Given
            var emailBuilder = new StringBuilder();
            emailBuilder.AppendLine($"To: {_subject!.ToAddress}");
            emailBuilder.AppendLine($"Subject: {_subject!.Subject}");
            emailBuilder.AppendLine($"Content: {_subject!.Content}");
            var expectedEmailContent = emailBuilder.ToString();

            // When
            var actualEmailContent = _subject.ToString();

            // Then
            Assert.AreEqual(expectedEmailContent, actualEmailContent);
        }

        [TestMethod]
        public void Validate_ToAddressFieldIsEmpty_ReturnsValidationError()
        {
            // Given
            _subject = _subject with { ToAddress = string.Empty };

            // When
            var result = _subject!.Validate(nameof(Email.ToAddress));

            // Then
            Assert.AreEqual($"'{nameof(Email.ToAddress)}' field must not be null or empty", result);
        }

        [TestMethod]
        public void Validate_ToAddressFieldIsNotValidEmailAddress_ReturnsValidationError()
        {
            // Given
            _subject = _subject with { ToAddress = "asd" };

            // When
            var result = _subject!.Validate(nameof(Email.ToAddress));

            // Then
            Assert.AreEqual($"'{nameof(Email.ToAddress)} must be a valid email address", result);
        }

        [TestMethod]
        public void Validate_ToAddressFieldIsValidEmailAddress_ReturnsNull()
        {
            // Given
            _subject = _subject with { ToAddress = "some.person@server98.com" };

            // When
            var result = _subject!.Validate(nameof(Email.ToAddress));

            // Then
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Validate_SubjectFieldIsEmpty_ReturnsValidationError()
        {
            // Given
            _subject = _subject with { Subject = string.Empty };

            // When
            var result = _subject!.Validate(nameof(Email.Subject));

            // Then
            Assert.AreEqual($"'{nameof(Email.Subject)}' field must not be null or empty", result);
        }

        [TestMethod]
        public void Validate_SubjectFieldIsNotEmpty_ReturnsNull()
        {
            // Given
            _subject = _subject with { Subject = "Hello" };

            // When
            var result = _subject!.Validate(nameof(Email.Subject));

            // Then
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Validate_ContentFieldIsEmpty_ReturnsValidationError()
        {
            // Given
            _subject = _subject! with { Content = string.Empty };

            // When
            var result = _subject!.Validate(nameof(Email.Content));

            // Then
            Assert.AreEqual($"'{nameof(Email.Content)}' field must not be null or empty", result);
        }

        [TestMethod]
        public void Validate_ContentFieldIsNotEmpty_ReturnsNull()
        {
            // Given
            _subject = _subject! with { Content = "Hello" };

            // When
            var result = _subject!.Validate(nameof(Email.Content));

            // Then
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Validate_GivenInvalidFieldName_ThrowsException()
        {
            // When
            _subject!.Validate("wrong field");
        }
    }
}
