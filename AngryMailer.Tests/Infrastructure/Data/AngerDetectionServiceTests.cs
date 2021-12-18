using AngryMailer.Infrastructure.Data;
using AngryMailer.Infrastructure.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;

namespace AngryMailer.Tests.Infrastructure.Data
{
    [TestClass]
    public class AngerDetectionServiceTests
    {
        private ITimeService? _timeService;

        private AngerDetectionService? _subject;


        [TestInitialize]
        public void Initialize()
        {
            _timeService = Substitute.For<ITimeService>();

            _subject = new AngerDetectionService(_timeService);
        }


        [TestMethod]
        public void Constructor_SetsTimeServiceToRun()
        {
            // Then
            _timeService!.Received().Mark();
        }

        [TestMethod]
        public void IsAngry_UserOverpasses400KeystrokesPerMinute_ReturnsTrue()
        {
            // Given
            _timeService!.Elapsed.Returns(TimeSpan.FromMilliseconds(1));

            // When, counting a lot of keystrokes very quickly
            for (int i = 0; i < 10; i++)
            {
                _subject!.CountStroke();
            }

            // Then
            Assert.IsTrue(_subject!.IsUserAngry);
        }

        [TestMethod]
        public void IsAngry_UserTypesVerySlowly_ReturnsTrue()
        {
            // Given
            _timeService!.Elapsed.Returns(TimeSpan.FromSeconds(10));

            // When, counting a lot of keystrokes very quickly
            for (int i = 0; i < 10; i++)
            {
                _subject!.CountStroke();
            }

            // Then
            Assert.IsFalse(_subject!.IsUserAngry);
        }
    }
}
