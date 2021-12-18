using AngryMailer.Domain.Validation;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AngryMailer.Domain.Entities
{
    /// <summary>
    ///     Represents an email to send to a recipient with a certain content.
    /// </summary>
    /// <param name="ToAddress">Email address to send the email to.</param>
    /// <param name="Subject">Subject for the recipient to glance the email's content.</param>
    /// <param name="Content">Content to include in the email.</param>
    public record Email(string ToAddress, string Subject, string Content) : IValidatable
    {
        /// <inheritdoc cref="IValidatable.IsValid"/>
        public bool IsValid =>
            Validate(nameof(ToAddress)) is null &&
            Validate(nameof(Subject)) is null &&
            Validate(nameof(Content)) is null;


        /// <summary>
        ///     Returns the email content fully formed from the fields in this object. The result of this method is 
        ///     what is to be sent.
        /// </summary>
        /// <returns>The email fully composed and ready to be sent.</returns>
        public override string ToString()
        {
            var emailBuilder = new StringBuilder();

            emailBuilder.AppendLine($"To: {ToAddress}");
            emailBuilder.AppendLine($"Subject: {Subject}");
            emailBuilder.AppendLine($"Content: {Content}");

            return emailBuilder.ToString();
        }

        /// <summary>
        ///     Validates each of the properties in this entity.
        /// </summary>
        /// <param name="field">Name of the property to validate.</param>
        /// <returns>
        ///     Returns:
        ///     - Null if none of the fields is invalid.
        ///     - Or a validation description of the first field in this object that is invalid.
        /// </returns>
        public string? Validate(string field)
        {
            return field switch
            {
                nameof(ToAddress) => ValidateToAddressField(),
                nameof(Subject) => ValidateSubjectField(),
                nameof(Content) => ValidateContentField(),
                _ => throw new ArgumentException("Is not a valid field", nameof(field)),
            };
        }

        private string? ValidateContentField()
        {
            if (string.IsNullOrEmpty(Content))
            {
                return $"'{nameof(Content)}' field must not be null or empty";
            }

            return null;
        }

        private string? ValidateSubjectField()
        {
            if (string.IsNullOrEmpty(Subject))
            {
                return $"'{nameof(Subject)}' field must not be null or empty";
            }

            return null;
        }

        private string? ValidateToAddressField()
        {
            if (string.IsNullOrEmpty(ToAddress))
            {
                return $"'{nameof(ToAddress)}' field must not be null or empty";
            }

            if (!Regex.IsMatch(ToAddress, @"^\w+([\-\.][\w]+)*\@\w+([\-\.][\w]+)*$"))
            {
                return $"'{nameof(ToAddress)} must be a valid email address";
            }

            return null;
        }
    }
}
