using System;

namespace AngryMailer.Infrastructure.Time
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

        void Mark();
    }
}