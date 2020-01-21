using ProgramMonitor.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramMonitor.Tasks
{
    public interface ITask
    {
        int Run();

        TaskModel Config { get; }

        List<INotification> notifications { get; set; }
    }
}
