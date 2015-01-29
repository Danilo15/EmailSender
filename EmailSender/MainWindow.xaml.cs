using EmailSender.Helpers;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace EmailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Configuracoes configuracoes;
        private string _From;

        public string From
        {
            get { return _From; }
            set { _From = value; }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private int _SmtpPort;

        public int SmtpPort
        {
            get { return _SmtpPort; }
            set { _SmtpPort = value; }
        }

        private string _SmtpServer;

        public string SmtpServer
        {
            get { return _SmtpServer; }
            set { _SmtpServer = value; }
        }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            ResourceHelper helper = new ResourceHelper();
            helper.Init();
        }

        private void btConfigurar_Click(object sender, RoutedEventArgs e)
        {
            configuracoes = new Configuracoes();
            configuracoes.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string To = txtEmail.Text;
            EnviarEmail(To);
        }

        private void EnviarEmail(string To)
        {
            string Subject = txtSubject.Text;

            SmtpClient client = new SmtpClient(SmtpServer, SmtpPort);
            MailMessage message = new MailMessage(From, To.Split(';')[0], Subject, (bool)checkHTML.IsChecked ? GetArquivo() : txtBody.Text);
            message = MultiplosEmails(To, message);
            message.IsBodyHtml = (bool)checkHTML.IsChecked;
            client.Credentials = new NetworkCredential(_UserName, _Password);

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string GetArquivo()
        {
            string path = txtDiretorio.Text;
            return File.ReadAllText(path);
        }

        private MailMessage MultiplosEmails(string To, MailMessage message)
        {
            string[] splitedTo = To.Split(';');
            message.To.Clear();
            foreach (var item in splitedTo)
            {
                if (!item.Equals(string.Empty))
                {
                    message.To.Add(item);
                }
            }
            return message;
        }

        private void checkHTML_Checked(object sender, RoutedEventArgs e)
        {
            txtBody.IsEnabled = false;
        }

        private void checkHTML_Unhecked(object sender, RoutedEventArgs e)
        {
            txtBody.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}