using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace EmailSender.Helpers
{
    public class ResourceHelper
    {
        private MainWindow window = (MainWindow)Application.Current.MainWindow;

        public string root = string.Empty,
                subject = string.Empty,
                email = string.Empty,
                smtpserver = string.Empty,
                smtpport = string.Empty,
                username = string.Empty,
                password = string.Empty;

        public void Init()
        {
            var config = EmailSender.Properties.Resources.config;
            string[] itens = config.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetType("EmailSender.Helpers.ResourceHelper");
            MemberInfo[] mi = type.GetMembers();
            List<FieldInfo> fi = new List<FieldInfo>();
            foreach (MemberInfo item in mi)
            {
                if (item.MemberType == MemberTypes.Field)
                {
                    fi.Add((FieldInfo)item);
                }
            }
            foreach (var fieldInfo in fi)
            {
                foreach (string item in itens)
                {
                    if (item.StartsWith(string.Format("{0}:",fieldInfo.Name)))
                    {
                        string splitedItem = item.Split('"')[1];
                        fieldInfo.SetValue(this, splitedItem); 
                    }
                }
            }            

            window.txtEmail.Text = email;
            window.txtDiretorio.Text = root;
            window.txtSubject.Text = subject;
            window.SmtpServer = smtpserver;
            window.SmtpPort = int.Parse(smtpport);
            window.UserName = username;
            window.From = username;
            window.Password = password;
        }
    }
}