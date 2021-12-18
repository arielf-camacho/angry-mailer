using AngryMailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AngryMailer.Tests.ViewModels
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void CanExecute_ProvidedNoMethodOnCommandInstantiation_AlwaysReturnsTrue()
        {
            // Given
            var subject = new Command(o => { });

            // When
            var canExecute = subject.CanExecute(null);

            // Then
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void CanExecute_ProvidedAMethodOnCommandInstantiation_AlwaysInvokesSuchMethodTo()
        {
            // Given
            var result = Environment.TickCount % 2 == 0;
            var subject = new Command(o => { }, o => result);

            // When
            var canExecute = subject.CanExecute(null);

            // Then
            Assert.AreEqual(result, canExecute);
        }

        [TestMethod]
        public void Execute_ExecutesTheProvidedMethod()
        {
            // Given
            var argument = Environment.TickCount;
            
            bool invoked = false;

            void Method(object? parameter) 
            {
                if (parameter is int p && p % 2 == 0) invoked = true;
            }
            
            var subject = new Command(o => { }, o => true);

            // When
            subject.Execute(argument);

            // Then
            var expectedResult = argument % 2 == 0;
            Assert.AreEqual(expectedResult, invoked);
        }
    }
}
