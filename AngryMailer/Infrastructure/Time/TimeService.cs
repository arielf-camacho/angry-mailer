using System;

namespace AngryMailer.Infrastructure.Time
{
    /// <summary>
    ///     Default implementation of <see cref="ITimeService"/> interface. See that interface's documentation for more details.
    /// </summary>
    public class TimeService : ITimeService
    {
        private DateTime _lastMarkTime = DateTime.Now;


        /// <inheritdoc cref="ITimeService.Elapsed"/>
        public TimeSpan Elapsed
        {
            get
            {
                lock (this) return DateTime.Now - _lastMarkTime;
            }
        }


        /// <inheritdoc cref="ITimeService.Mark"/>
        public void Mark()
        {
            lock (this)
            {
                _lastMarkTime = DateTime.Now;
            }
        }
    }
}
