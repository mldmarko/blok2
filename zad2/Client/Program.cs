using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IServer> factory = new ChannelFactory<IServer>( new NetTcpBinding(),
                                                                           new EndpointAddress("net.tcp://localhost:4000/IServer"));
            IServer proxy = factory.CreateChannel();

            Alarm a = new Alarm() { Message = "ovo je neki alarm", Risk = (char)4, TimeGenerated = DateTime.Now };
            proxy.SetAlarm(2, 2, 2, a);
            proxy.SetAlarm(1, 1, 1, a);

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
