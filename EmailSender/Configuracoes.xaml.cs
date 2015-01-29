using System;
using System.Windows;
using System.Windows.Input;

namespace EmailSender
{
    /// <summary>
    /// Interaction logic for Configuracoes.xaml
    /// </summary>
    public partial class Configuracoes : Window
    {
        private MainWindow mainWindow;

        public Configuracoes()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            txtSMTPPort.Text = mainWindow.SmtpPort.ToString();
            txtSMTPServer.Text = mainWindow.SmtpServer;
            txtUsername.Text = mainWindow.UserName;
            passwordBox.Password = mainWindow.Password;
        }

        private void ShowPasswordCharsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void ShowPasswordCharsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SmtpPort = int.Parse(txtSMTPPort.Text);
            mainWindow.SmtpServer = txtSMTPServer.Text;
            mainWindow.UserName = txtUsername.Text;
            mainWindow.Password = passwordBox.Password;
            mainWindow.From = txtUsername.Text;
            mainWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void txtSMTPPort_KeyDown(object sender, KeyEventArgs e)
        {
            var x = (Char)KeyInterop.VirtualKeyFromKey(e.Key);
            if (Char.IsNumber(x))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}