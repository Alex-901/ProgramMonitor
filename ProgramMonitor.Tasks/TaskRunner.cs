using Newtonsoft.Json;
using ProgramMonitor.Common;
using ProgramMonitor.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramMonitor.Tasks
{
    public class TaskRunner
    {
        public bool RunTasks()
        {
            var tasks = GetTasks();
            List<INotification> notifications = new List<INotification>();

            try
            {
                tasks.ForEach(x =>
                {
                    try
                    {
                        x.Run();
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex, ref notifications, x);
                    }
                });
            }
            finally
            {
                tasks.ForEach(y => y.notifications.ForEach(z => z.Send()));
            }

            return true;
        }

        public List<ITask> GetTasks()
        {
            List<ITask> tasks = new List<ITask>();

            var config = File.ReadAllText(Constants.MonitorConfig);

            var taskConfig = (List<TaskModel>)JsonConvert.DeserializeObject(config, typeof(List<TaskModel>));

            foreach (var item in taskConfig)
            {
                if (!item.Enabled)
                {
                    continue;
                }

                switch (item.Type.ToLower())
                {
                    case "service":
                        tasks.Add(new ServiceTask(item));
                        break;
                    case "sql":
                        var script = File.ReadAllText(item.SQLScript);
                        item.SQLScript = script;
                        tasks.Add(new SQLTask(item));
                        break;
                    default:
                        break;
                }
            }

            return tasks;
        }

        public void HandleException(Exception ex, ref List<INotification> notifications, ITask task)
        {
            notifications.Add(new EmailNotification(string.Format(Constants.ServiceError, task.Config.Name), ex.Message + Environment.NewLine + ex.InnerException?.Message));
            notifications.Add(new BallonNotification(string.Format(Constants.ServiceError, task.Config.Name), ex.Message + Environment.NewLine + ex.InnerException?.Message));
        }
    }
}
