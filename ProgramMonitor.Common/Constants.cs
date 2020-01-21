using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramMonitor.Common
{
    public static class Constants
    {
        public const string ServiceError = "{0} is failing";
        public const string ServiceResult = "Neo WCF Service";
        public const string LongResponse = "{0} response time slow";
        public const string ResponseTime = "Response time: {0} milliseconds";

        private static string _EmailRecipients = "alexhodge@leightonobrien.com";
        private static string _MonitorScripts;
        private static string _MonitorConfig;
        public static string EmailRecipients 
        { 
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_EmailRecipients))
                    {
                        _EmailRecipients = ConfigurationManager.AppSettings["EmailRecipients"];
                    }

                    return _EmailRecipients;
                }
                catch
                {
                    return _EmailRecipients;
                }
            }
        }

        public static string MonitorScripts
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_MonitorScripts))
                    {
                        _MonitorScripts = ConfigurationManager.AppSettings["MonitorScripts"];
                    }

                    return _MonitorScripts;
                }
                catch
                {
                    return _MonitorScripts;
                }
            }
        }

        public static string MonitorConfig
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_MonitorConfig))
                    {
                        _MonitorConfig = ConfigurationManager.AppSettings["MonitorConfig"];
                    }

                    return _MonitorConfig;
                }
                catch
                {
                    return _MonitorConfig;
                }
            }
        }
    }
}
