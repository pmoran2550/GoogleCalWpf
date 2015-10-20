using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Schuss.GoogleCalWpf
{
    /// <summary>
    /// Interaction logic for CredentialDialog.xaml
    /// </summary>
    public partial class CredentialDialog : Window
    {
        CredentialViewModel _viewModel;

        public CredentialDialog()
        {
            InitializeComponent();

            _viewModel = new CredentialViewModel();
            DataContext = _viewModel;
        }


        public CredentialViewModel ViewModel
        {
            get { return _viewModel; }
        }

        private void OKButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
