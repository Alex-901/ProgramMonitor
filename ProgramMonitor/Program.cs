using ProgramMonitor.Common;
using ProgramMonitor.Notifications;
using ProgramMonitor.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace ProgramMonitor
{
    public class Program
    {
        static void Main(string[] args)
        {
            new TaskRunner().RunTasks();
        }
    }
}
