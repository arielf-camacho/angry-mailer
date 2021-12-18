using AngryMailer.Tests.Utils;
using AngryMailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace AngryMailer.Tests.ViewModels
{
    [TestClass]
    public class SendMailViewModelTests
    {
        private static readonly Random _random = new(Environment.TickCount);

        private SendMailViewModel? _subject;


        [TestInitialize]
        public void Initialize()
        {
            _subject = new SendMailViewModel();
        }


        [TestMethod]
        public void SendCommand_CanAlwaysExecute()
        {
            // When
            var canExecute = _subject!.SendCommand.CanExecute(null);

            // Then
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void To_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.ToEmail));

            // When
            _subject!.ToEmail = newValue;

            // When
            Assert.AreEqual(newValue, _subject!.ToEmail);
            Assert.IsTrue(watcher.NotifiedChange);
        }

        [TestMethod]
        public void Subject_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Subject));

            // When
            _subject!.Subject = newValue;

            // When
            Assert.AreEqual(newValue, _subject!.Subject);
            Assert.IsTrue(watcher.NotifiedChange);
        }

        [TestMethod]
        public void Content_IfChanged_NotifiesItDidChange()
        {
            // Given
            var newValue = $"Value: #{_random.Next(10)}";
            var watcher = new PropertyChangeWatcher(_subject!, nameof(SendMailViewModel.Content));

            // When
            _subject!.Content = newValue;

            // When
            Assert.AreEqual(newValue, _subject!.Content);
            Assert.IsTrue(watcher.NotifiedChange);
        }
    }
}
