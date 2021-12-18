using AngryMailer.Domain.Services;
using System;

namespace AngryMailer.Infrastructure.Data
{
    /// <summary>
    ///     Default implementation of <see cref="IAngerDetectionService"/> interface. See that interface's docs for 
    ///     more details.
    /// </summary>
    public class AngerDetectionService : IAngerDetectionService
    {
        /// <inheritdoc cref="IAngerDetectionService.IsAngry"/>
        public bool IsAngry => throw new NotImplementedException();


        /// <inheritdoc cref="IAngerDetectionService.CountStroke"/>
        public void CountStroke()
        {
            throw new NotImplementedException();
        }
    }
}
