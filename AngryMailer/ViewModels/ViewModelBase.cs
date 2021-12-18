using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AngryMailer.ViewModels
{
    /// <summary>
    ///     Base class of all the view models in the current project. By view model we reffer to the role with the 
    ///     same name in the MVVM pattern.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     Event that notifies when a property in this view model has changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        ///     When invoked it notifies to <see cref="PropertyChanged"/> event registered listeners that the 
        ///     property calling this method has changed.
        ///     
        ///     NOTE: Please use this method only inside property setters.
        /// </summary>
        /// <param name="propertyName">Name of the changed property's setter method.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handlers = PropertyChanged;

            handlers?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
