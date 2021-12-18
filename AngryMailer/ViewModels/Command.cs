using System;
using System.Windows.Input;

namespace AngryMailer.ViewModels
{
    /// <summary>
    ///     Defines the default implementation of a command as view models should provide them.
    /// </summary>
    public class Command : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;


        /// <summary>
        ///     Initializes a new instance of <see cref="Command"/> given a "execute" and (optionally) an 
        ///     "can execute" method.
        /// </summary>
        /// <param name="execute">Method to invoke when the command instance gets executed.</param>
        /// <param name="canExecute">
        ///     Method to invoke to determine whether the command instance can or cannot be executed.
        /// </param>
        public Command(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (o => true);
        }


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
        public bool CanExecute(object? parameter) => _canExecute(parameter);

        /// <summary>
        ///     Invokes the logic of this command given a (nullable) parameter.
        /// </summary>
        /// <param name="parameter">
        ///     The parameters to invoke the command with. Can be null.
        /// </param>
        public void Execute(object? parameter) => _execute(parameter);
    }
}