using ProgramMonitor.Common;
using ProgramMonitor.Notifications;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramMonitor.Tasks
{
    public class SQLTask : ITask
    {
        public TaskModel Config { get; set; }
        public List<INotification> notifications { get; set; } = new List<INotification>();

        public SQLTask(TaskModel config)
        {
            Config = config;
        }

        public int Run()
        {
            var sw = new Stopwatch();

            using (SqlConnection conn = new SqlConnection(Config.SQLConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(Config.SQLScript, conn))
                {
                    sw.Start();
                    cmd.ExecuteNonQuery();
                    sw.Stop();
                }
            }

            GenerateNotifications(Convert.ToInt32(sw.ElapsedMilliseconds));

            return Convert.ToInt32(sw.ElapsedMilliseconds);
        }

        public void GenerateNotifications(int timeTaken)
        {
            if (Config.MonitorResponseTime && timeTaken > Config.ResponseTimeLimit)
            {
                notifications.Add(new EmailNotification(string.Format(Constants.LongResponse, Config.Name), string.Format(Constants.ResponseTime, timeTaken)));
                notifications.Add(new BallonNotification(string.Format(Constants.LongResponse, Config.Name), string.Format(Constants.ResponseTime, timeTaken)));
            }
            else
            {
                notifications.Add(new BallonNotification(Config.Name, string.Format(Constants.ResponseTime, timeTaken)));
            }
        }
    }
}
