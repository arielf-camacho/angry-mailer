using AngryMailer.Domain.Services;
using AngryMailer.Infrastructure.Time;
using System;

namespace AngryMailer.Infrastructure.Data
{
    /// <summary>
    ///     Default implementation of <see cref="IAngerDetectionService"/> interface. See that interface's docs for 
    ///     more details.
    /// </summary>
    public class AngerDetectionService : IAngerDetectionService
    {
        private readonly TimeSpan _angryThreshold = TimeSpan.FromSeconds(400 / 60);

        private readonly ITimeService _timeService;


        /// <summary>
        ///     Initializes a new instance of <see cref="AngerDetectionService"/> with a time service.
        /// </summary>
        /// <param name="timeService">Service used to measure time</param>
        public AngerDetectionService(ITimeService timeService)
        {
            _timeService = timeService;
            _timeService.Mark();
        }


        /// <inheritdoc cref="IAngerDetectionService.IsUserAngry"/>
        public bool IsUserAngry => _timeService.Elapsed <= _angryThreshold;


        /// <inheritdoc cref="IAngerDetectionService.CountStroke"/>
        public void CountStroke() => _timeService.Mark();
    }
}
