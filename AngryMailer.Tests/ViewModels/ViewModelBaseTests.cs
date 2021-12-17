using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngryMailer.ViewModels;

namespace AngryMailer.Tests.ViewModels
{
    [TestClass]
    public class ViewModelBaseTests
    {
        [TestMethod]
        public void NotifyPropertyChanged_WhenInvoked_ItNotifiesTheNotifyingPropertyHasChanged()
        {
            // Given
            bool notificationArrived = false;

            var subject = new ViewModel();
            subject.PropertyChanged += (_, args) => notificationArrived = args.PropertyName == nameof(ViewModel.Name);

            // When
            subject.Name = "Some value";

            // Then
            Assert.IsTrue(notificationArrived);
        }


        private class ViewModel : ViewModelBase
        {
            private string? _name;

            public string? Name  
            {
                get => _name;
                set
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
