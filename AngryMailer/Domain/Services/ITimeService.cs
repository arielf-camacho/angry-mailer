using System;

namespace AngryMailer.Domain.Services
{
    /// <summary>
    ///     A service used to mark time intervals elapsed.
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        ///     Returns how long have elapsed since the last <see cref="Mark"/> call.
        /// </summary>
        TimeSpan Elapsed { get; }

        /// <summary>
        ///     Marks the current time as the next to take into account when comparing how long has elapsed next time 
        ///     <see cref="Elapsed"/> is called.
        /// </summary>
        void Mark();
    }
}