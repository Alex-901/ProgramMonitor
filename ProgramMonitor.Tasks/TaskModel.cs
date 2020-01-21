using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramMonitor.Tasks
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string SQLConnectionString { get; set; }
        public string SQLScript { get; set; }
        public string ServiceURL { get; set; }
        public string ServiceBody { get; set; }
        public bool Enabled { get; set; }
        public bool MonitorResponseTime { get; set; }
        public int ResponseTimeLimit { get; set; }
    }
}
