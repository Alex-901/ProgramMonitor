using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramMonitor.Notifications
{
    public class BallonNotification : INotification
    {
        private string _header;
        private string _content;
        private bool _error;
        public BallonNotification(string header, string content, bool error = false)
        {
            _header = header;
            _content = content;
            _error = error;
        }
        public void Send()
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = SystemIcons.Information;
            var timeout = 30000;

            if (_error)
            {
                notifyIcon.Icon = SystemIcons.Error;
                _header = _header.ToUpper();
                timeout = 50000;
            }

            if (_header != null)
            {
                notifyIcon.BalloonTipTitle = _header;
            }

            if (_content != null)
            {
                notifyIcon.BalloonTipText = _content;
            }

            notifyIcon.ShowBalloonTip(timeout);
        }
    }
}
