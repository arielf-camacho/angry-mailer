namespace AngryMailer.Domain.Services
{
    /// <summary>
    ///     Represents a service that determines the Anger state of the user. It counts how many stroke per
    ///     minute the user hits. If above 400, it means the user is tilted.
    /// </summary>
    public interface IAngerDetectionService
    {
        /// <summary>
        ///     Gets a boolean expressing whether the user is angry or not.
        /// </summary>
        bool IsAngry { get; }


        /// <summary>
        ///     Counts a keystroke, and updates the <see cref="IsAngry"/> state.
        /// </summary>
        void CountStroke();
    }
}
