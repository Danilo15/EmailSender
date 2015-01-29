using System;
using System.Windows;

namespace EmailSender.Helpers
{
    public class ResourceHelper
    {
        private MainWindow window = (MainWindow)Application.Current.MainWindow;

        public void Init()
        {
            var config = EmailSender.Properties.Resources.config;
            string[] itens = config.Split("\r\n".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
            string root = string.Empty,
                subject = string.Empty,
                email = string.Empty,
                smtpserver = string.Empty,
                smtpport = string.Empty,
                username = string.Empty,
                password = string.Empty;

            foreach (var item in itens)
            {
                if (item.StartsWith("root:"))
                {
                    root = item.Split('"')[1];
                }
                else if (item.StartsWith("subject:"))
                {
                    subject = item.Split('"')[1];
                }
                else if (item.StartsWith("email:"))
                {
                    email = item.Split('"')[1];
                }
                else if (item.StartsWith("smtpserver:"))
                {
                    smtpserver = item.Split('"')[1];
                }
                else if (item.StartsWith("smtpport:"))
                {
                    smtpport = item.Split('"')[1];
                }
                else if (item.StartsWith("username:"))
                {
                    username = item.Split('"')[1];
                }
                else if (item.StartsWith("password:"))
                {
                    password = item.Split('"')[1];
                }
            }

            window.txtEmail.Text = email;
            window.txtDiretorio.Text = root;
            window.txtSubject.Text = subject;
            window.SmtpServer = smtpserver;
            window.SmtpPort = int.Parse(smtpport);
            window.UserName = username;
            window.Password = password;
        }
    }
}