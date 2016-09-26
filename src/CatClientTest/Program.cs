using Com.Dianping.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatClientTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileMap = "CatConfig.xml";

            Cat.Initialize(fileMap);

            for (int j = 0; j < 500; j++)
            {
                var a = Cat.GetProducer().NewTransaction("test", "test");
                for (int i = 0; i < 7; i++)
                {
                    Cat.GetProducer().NewTransaction("test", "test");
                    Cat.GetProducer().LogError(new Exception());
                    Cat.GetProducer().LogEvent("test", "test", "0", null);
                    Cat.GetProducer().LogHeartbeat("test", "test", "0", null);
                }
                for (int i = 0; i < 7; i++)
                {
                    var c = Cat.GetManager().PeekTransaction;
                    c.Status = "0";
                    c.Complete();
                }
                a.Status = "0";
                a.Complete();
                Cat.GetProducer().LogError(new Exception());
                Cat.GetProducer().LogEvent("test", "test", "0", null);
                Cat.GetProducer().LogHeartbeat("test", "test", "0", null);
            }
            Console.ReadLine();
        }
    }
}
