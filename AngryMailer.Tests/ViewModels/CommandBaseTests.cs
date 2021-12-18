using AngryMailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AngryMailer.Tests.ViewModels
{
    [TestClass]
    public class CommandBaseTests
    {
        [TestMethod]
        public void CanExecute_AlwaysReturnsTrue()
        {
            // Given
            var subject = new CommandStub();

            // When
            var canExecute = subject.CanExecute(null);

            // Then
            Assert.IsTrue(canExecute);
        }

        
        private class CommandStub : CommandBase
        {
            public override void Execute(object? parameter)
            {
            }
        }
    }
}
