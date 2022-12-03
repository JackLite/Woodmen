using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Woodman.Analytic
{
    public class AnalyticSenderFacade
    {
        public void SendEvent(string name)
        {
            AppMetrica.Instance.ReportEvent(name);
            LogEvent(name);
        }

        public void SendEvent(string name, string json)
        {
            AppMetrica.Instance.ReportEvent(name, json);
            LogEvent(name, json);
        }

        public void SendEvent(string name, Dictionary<string, string> fields)
        {
            var parameters = fields.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
            AppMetrica.Instance.ReportEvent(name, parameters);
            LogEvent(name, fields);
        }

        private void LogEvent(string name, Dictionary<string, string> fields = null)
        {
            var sb = new StringBuilder("[Analytic] Event: ");
            sb.Append(name);
            sb.Append("\n");
            if (fields != null)
            {
                foreach (var kvp in fields)
                {
                    sb.Append(kvp.Key);
                    sb.Append(": ");
                    sb.Append(kvp.Value);
                    sb.Append("\n");
                }
            }

            Debug.Log(sb.ToString());
        }

        private void LogEvent(string name, string json)
        {
            var sb = new StringBuilder("[Analytic] Event: ");
            sb.Append(name);
            sb.Append("\n");
            sb.Append("Data: ");
            sb.Append(json);

            Debug.Log(sb.ToString());
        }
    }
}