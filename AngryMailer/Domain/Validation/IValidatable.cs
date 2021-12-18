namespace AngryMailer.Domain.Validation
{
    /// <summary>
    ///     Describes the contract of an object that can validate a field of its own.
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        ///     Gets the validation status of the current object.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        ///     Validates the field named as specified and returns the validation error (if any).
        /// </summary>
        /// <param name="field">Name of the field to validate.</param>
        /// <returns>
        ///     A string representing the validation error found for the <paramref name="field"/>; or null if the 
        ///     field is valid.
        /// </returns>
        string? Validate(string field);
    }
}
