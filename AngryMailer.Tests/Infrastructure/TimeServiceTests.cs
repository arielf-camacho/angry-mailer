using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace AngryMailer.Tests.Infrastructure.Time
{
    [TestClass]
    public class TimeServiceTests
    {
        private TimeService _subject;


        [TestInitialize]
        public void Initialize()
        {
            _subject = new TimeService();
        }


        [TestMethod]
        public void Elapsed_ReturnsTimeElapsedBetweenNowAndLastMark()
        {
            // Mark now as the time to count how long has elapsed ahead in the test
            _subject.Mark();

            // Wait some time
            Thread.Sleep(1000);

            // Mark again
            _subject.Mark();

            // Then, Elapsed should be around a second. Note: I'm comparing with 20 milliseconds difference, to give
            // some breathing room on slow machines.
            Console.WriteLine(_subject.Elapsed.TotalMilliseconds - TimeSpan.FromSeconds(1).TotalMilliseconds);
            Assert.IsTrue(_subject.Elapsed.TotalMilliseconds - TimeSpan.FromSeconds(1).TotalMilliseconds < 20);
        }
    }
}
