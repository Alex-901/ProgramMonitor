using ProgramMonitor.Common;
using ProgramMonitor.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProgramMonitor.Tasks
{
    public class ServiceTask : ITask
    {
        public TaskModel Config { get; set; }
        public List<INotification> notifications { get; set; } = new List<INotification>();

        public ServiceTask(TaskModel config)
        {
            Config = config;
        }

        public int Run()
        {
            var sw = new Stopwatch();

            //TODO Check for fail
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/xml; charset=utf-8");

                client.DefaultRequestHeaders.Add("SOAPAction", @"""http://tempuri.org/ISiraSystemService/Ping""");
                var content = new StringContent(Config.ServiceBody, Encoding.UTF8, "text/xml");
                sw.Start();
                using (var postResponse = client.PostAsync(Config.ServiceURL, content).Result)
                {
                    string postResult = postResponse.Content.ReadAsStringAsync().Result;
                }
                sw.Stop();
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
