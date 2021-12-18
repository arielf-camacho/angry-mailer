using AngryMailer.Domain;
using AngryMailer.ViewModels;
using System.Windows;

namespace AngryMailer.Views
{
    /// <summary>
    /// Interaction logic for SendMailView.xaml
    /// </summary>
    public partial class SendMailView : Window
    {
        public SendMailView()
        {
            InitializeComponent();

            var mailService = new MailService();
            var viewModel = new SendMailViewModel(mailService);
            DataContext = viewModel;
        }
    }
}
