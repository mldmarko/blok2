using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    enum AuthenticationType { Windows, Certificate}

    class Program
    {
        static void Main(string[] args)
        {
            if (Meni() == AuthenticationType.Windows)
            {
                Alarm a = new Alarm() { Message = "ovo je neki alarm", Risk = 4, TimeGenerated = DateTime.Now };

                var clientIndentity = WindowsIdentity.GetCurrent();
                Console.WriteLine(String.Format("Authentificated User: {0}",clientIndentity.Name.ToString()));

                NetTcpBinding binding = new NetTcpBinding();
                string address = "net.tcp://localhost:4000/IServer";

                using (WinAuthClient proxy = new WinAuthClient(binding, new EndpointAddress(new Uri(address))))
                {
                    proxy.SetAlarm(2, 2, 2, a);
                }
            }
            else
            {
                //certificate auth
                //kada se zavrse obe, onda se moze videti koji kod je zajednicki  a koji ne, i podeliti u metode
            }

            Console.Write("Press any key to exit: ");
            Console.ReadKey();
        }

        static AuthenticationType Meni()
        {
            while (true)
            {
                Console.WriteLine("Choose type of authentication:");
                Console.WriteLine("  1. Windows");
                Console.WriteLine("  2. Certificate");
                Console.Write("  >> ");

                char choise = Console.ReadKey().KeyChar;

                switch (choise)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Windows authentication:\n\n");
                        return AuthenticationType.Windows;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Certificate authentication:\n\n");
                        return AuthenticationType.Certificate;
                    default:
                        Console.Clear();
                        Console.WriteLine("Error, not valid choise. Try again:");
                        break;
                }
            }
        }
    }
}
