using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProgramMonitor.Notifications
{
    public class EmailNotification : INotification
    {
        private string _header;
        private string _content;
        private bool _error;

        public EmailNotification(string header, string content, bool error = false)
        {
            _header = header;
            _content = content;
            _error = error;
        }
        public void Send()
        {
            using (SmtpClient SmtpServer = new SmtpClient("malpha"))
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("noreply@leightonobrien.com", "Service Ping Failure");
                mail.To.Add(Common.Constants.EmailRecipients);

                if (!string.IsNullOrEmpty(_content))
                {
                    mail.Body = _content;
                }

                mail.Subject = string.IsNullOrEmpty(_header) ? "The Neo WCF Service is not responding" : _header;
                SmtpServer.Send(mail);
            }
        }
    }
}
