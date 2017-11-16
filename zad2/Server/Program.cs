using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:4000/IServer";

            ServiceHost host = new ServiceHost(typeof(WCFServer));
            host.AddServiceEndpoint(typeof(IServer), binding, address);
            host.Open();

            Console.WriteLine("Server started.");


            new Thread(Read).Start();


            Console.ReadKey(true);
            host.Close();
        }

        static void Read()
        {
            while (true)
            {
                Thread.Sleep(30000);
                Database.InternModel.SaveAllToFile();
            }
        }
    }
}
