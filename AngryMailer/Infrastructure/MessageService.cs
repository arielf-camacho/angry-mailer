using AngryMailer.Domain.Services;
using System.Windows;

namespace AngryMailer.Infrastructure
{
    /// <summary>
    ///     Default implementation of the <see cref="IMessageService"/> interface. See that interface's docs for more details.
    /// </summary>
    public class MessageService : IMessageService
    {
        /// <inheritdoc cref="IMessageService.ShowMessage(string)"/>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
