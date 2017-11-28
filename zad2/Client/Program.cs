using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    enum AuthenticationType { Windows, Certificate }

    class Program
    {
        static void Main(string[] args)
        {
            BigInteger n, e, d;
            SecurityManager.RSA.GenerateKeys(out n, out e, out d);

            using (StreamWriter file = new StreamWriter(@"../../../publicKey.txt"))
            {
                file.WriteLine(n);
                file.WriteLine(d);
            }

            Console.WriteLine("Keys generated, press any key to continue.");
            Console.ReadKey(true);

            string srvCertCN = "client";

            if (Meni() == AuthenticationType.Windows)
            {
                Alarm a = new Alarm() {Risk = 4, TimeGenerated = DateTime.Now };
                a.SetMessageAlarmBlack();

                var clientIndentity = WindowsIdentity.GetCurrent();
                Console.WriteLine(String.Format("Authentificated User: {0}", clientIndentity.Name.ToString()));
                Audit.AuthenticationSuccess(clientIndentity.Name.ToString());

                NetTcpBinding binding = new NetTcpBinding();
                string address = "net.tcp://localhost:4000/IServer";

                using (WinAuthClient proxy = new WinAuthClient(binding, new EndpointAddress(new Uri(address))))
                {
                    Message m = new Message(2, 2, 2, a);
                    proxy.SetAlarm(m, DigitalSignature.SignMessage(m, n, e));
                }
            }
            else
            {
                Alarm a = new Alarm() {Risk = 4, TimeGenerated = DateTime.Now };
                a.SetMessageAlarmBlack();

                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

                X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.CurrentUser, srvCertCN);
                try
                {
                    if (srvCert != null)
                    {
                        Audit.AuthenticationSuccess(WindowsIdentity.GetCurrent().ToString());
                    }
                    else
                    {
                        Audit.AuthenticationFailed(WindowsIdentity.GetCurrent().ToString());
                        throw new Exception("Certificate not found\n");
                    } 
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:4001/IServer"),
                                          new X509CertificateEndpointIdentity(srvCert));
               
                using (CertAuthClient proxy = new CertAuthClient(binding, address))
                {
                    object privateKey = new object();
                    Message m = new Message(2, 2, 2, a);
                    proxy.SetAlarm(m, DigitalSignature.SignMessage(m, n, e));  
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
