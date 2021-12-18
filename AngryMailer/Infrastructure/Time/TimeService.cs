using System;

namespace AngryMailer.Infrastructure.Time
{
    /// <summary>
    ///     Default implementation of <see cref="ITimeService"/> interface. See that interface's documentation for more details.
    /// </summary>
    public class TimeService : ITimeService
    {
        private DateTime _currentTime = DateTime.Now;
        private DateTime _lastMarkTime = DateTime.Now;


        /// <inheritdoc cref="ITimeService.Elapsed"/>
        public TimeSpan Elapsed
        {
            get
            {
                lock (this)
                {
                    var elapsed = DateTime.Now - _lastMarkTime;
                    _lastMarkTime = _currentTime;

                    return elapsed;
                }
            }
        }


        /// <inheritdoc cref="ITimeService.Mark"/>
        public void Mark()
        {
            lock (this)
            {
                _currentTime = DateTime.Now;
            }
        }
    }
}
