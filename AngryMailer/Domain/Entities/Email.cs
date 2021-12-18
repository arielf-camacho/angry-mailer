namespace AngryMailer.Domain.Entities
{
    /// <summary>
    ///     Represents an email to send to a recipient with a certain content.
    /// </summary>
    /// <param name="To">Email address to send the email to.</param>
    /// <param name="Subject">Subject for the recipient to glance the email's content.</param>
    /// <param name="Content">Content to include in the email.</param>
    public record Email(string ToEmail, string Subject, string Content);
}
