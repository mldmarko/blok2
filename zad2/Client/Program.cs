using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
            //Alarm a1 = new Alarm() { Message = "ovo je neki alarm", Risk = 4, TimeGenerated = DateTime.Now };
            //Message m1 = new Message(2, 2, 2, a1);
            //byte[] e = DigitalSigneture.SignMessage(m1, new object());
            //bool d = DigitalSigneture.VerifySignature(m1, e, new object());
            //Console.WriteLine(d);

            //m1.BlockIndex = 5;
            //d = DigitalSigneture.VerifySignature(m1, e, new object());
            //Console.WriteLine(d);

            //return;

            string srvCertCN = "testService";

            if (Meni() == AuthenticationType.Windows)
            {
                Alarm a = new Alarm() { Message = "ovo je neki alarm", Risk = 4, TimeGenerated = DateTime.Now };

                var clientIndentity = WindowsIdentity.GetCurrent();
                Console.WriteLine(String.Format("Authentificated User: {0}",clientIndentity.Name.ToString()));

                NetTcpBinding binding = new NetTcpBinding();
                string address = "net.tcp://localhost:4000/IServer";

                using (WinAuthClient proxy = new WinAuthClient(binding, new EndpointAddress(new Uri(address))))
                {
                    object privateKey = new object(); //!!!!!!!!!!!!!!!!!!!!!
                    Message m = new Message(2, 2, 2, a);


                    proxy.SetAlarm(m, DigitalSigneture.SignMessage(m, privateKey));
                }
            }
            else
            {
                Alarm a = new Alarm() { Message = "ovo je neki alarm", Risk = 4, TimeGenerated = DateTime.Now };
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

                /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
                X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.CurrentUser, srvCertCN);
                EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:4001/IServer"),
                                          new X509CertificateEndpointIdentity(srvCert));

                using (CertAuthClient proxy = new CertAuthClient(binding, address))
                {
                    object privateKey = new object(); //!!!!!!!!!!!!!!!!!!!!!
                    Message m = new Message(2, 2, 2, a);
                    proxy.SetAlarm(m, DigitalSigneture.SignMessage(m, privateKey));
                    Console.WriteLine("TestCommunication() finished. Press <enter> to continue ...");
                    Console.ReadLine();
                }
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
