using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Dianping.Cat;
using Com.Dianping.Cat.Message;
using Com.Dianping.Cat.Message.Internals;
using System.Text;

namespace CatASPNETCore
{
    public class CatExtensions
    {
        public static void Init()
        {
            Cat.Initialize("CatConfig.xml");
        }
        public static ITransaction NewTransaction(string type, string name)
        {
            return Cat.GetProducer().NewTransaction(type, name);
        }
        public static void Complete(object transaction)
        {
            if (null != transaction && transaction is ITransaction) ((ITransaction)transaction).Complete();
        }
        public static void LogError(Exception cause)
        {
            Cat.GetProducer().LogError(cause);
        }
        public static void LogHeartbeat(String type, String name, String status, String nameValuePairs)
        {
            Cat.GetProducer().LogHeartbeat(type, name, status, nameValuePairs);
        }
        public static void LogEvent(string type, string name, string status, IDictionary<string, string> nameValuePairs)
        {
            if (null != nameValuePairs && nameValuePairs.Count > 0)
            {
                bool isFirst = true;
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> kvp in nameValuePairs)
                {
                    sb.Append(isFirst ? "" : "&").Append(kvp.Key).Append("=").Append(kvp.Value);
                    isFirst = false;
                }
                Cat.GetProducer().LogEvent(type, name, status, sb.ToString());
            }
            else
            {
                Cat.GetProducer().LogEvent(type, name, status, null);
            }

        }

        public static void LogEvent(string type, string name, string status, string nameValuePairs)
        {
            Cat.GetProducer().LogEvent(type, name, status, nameValuePairs);

        }

        public static void LogEvent(string type, string name, string status)
        {
            Cat.GetProducer().LogEvent(type, name, status, null);
        }

        public static void LogEvent(string type, string name)
        {
            Cat.GetProducer().LogEvent(type, name, "0", null);
        }
    }
}
