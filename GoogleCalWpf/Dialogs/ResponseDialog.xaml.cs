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
using Schuss.GoogleCalWpf;

namespace Schuss.GoogleCalWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ResponseDialog : Window
    {
        int _buttonPressed;

        public ResponseDialog(string message, string header, string[] buttonList)
        {
            InitializeComponent();

            MessageText.Text = message;
            HeaderText.Text = header;

            int buttons = 0;
            if (null != buttonList)
                buttons = buttonList.Length;
            switch (buttons)
            {
                default:
                case 0:
                    button1Text.Text = "OK";
                    button2.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    button1Text.Text = buttonList[0];
                    button2.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    button1Text.Text = buttonList[0];
                    button2Text.Text = buttonList[1];
                    break;
            }
            button1.IsDefault = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _buttonPressed = 1;
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _buttonPressed = 2;
            this.Close();
        }

        public int ButtonPressed
        {
            get { return _buttonPressed; }
        }
    }
}
