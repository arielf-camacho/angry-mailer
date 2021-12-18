using System;
using System.Windows.Input;

namespace AngryMailer.ViewModels
{
    /// <summary>
    ///     Defines the default implementation of a command as view models should provide them.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        ///     Event that is raised by the <see cref="CommandManager"/> to notify the bound WPF controls
        ///     to check about the availability of the Command, so they can refresh their enabled state.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        /// <summary>
        ///     Determines whether or not the current command can be executed, given a (nullable) parameter.
        /// </summary>
        /// <param name="parameter">
        ///     The future parameter to invoke the command with, so that this function can determine if the command 
        ///     can be executed with it or not.
        /// </param>
        /// <returns>
        ///     True if the command can be executed with the given <paramref name="parameter"/>; false otherwise.
        /// </returns>
        public virtual bool CanExecute(object? parameter) => true;

        /// <summary>
        ///     When overridden in a derived class it contains the actual logic of this command given a (nullable)
        ///     parameter.
        /// </summary>
        /// <param name="parameter">
        ///     The parameters to invoke the command with. Can be null.
        /// </param>
        public abstract void Execute(object? parameter);
    }
}