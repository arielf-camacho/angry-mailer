using System.ComponentModel;

namespace AngryMailer.Tests.Utils
{
    public class PropertyChangeWatcher
    {
        private string _propertyName;


        public PropertyChangeWatcher(INotifyPropertyChanged viewModel, string propertyName)
        {
            _propertyName = propertyName;
            viewModel.PropertyChanged += EvaluatePropertyActuallyChanged;
        }


        public bool NotifiedChange { get; private set; }

        private void EvaluatePropertyActuallyChanged(object? sender, PropertyChangedEventArgs e)
        {
            NotifiedChange |= e.PropertyName == _propertyName;
        }
    }
}
