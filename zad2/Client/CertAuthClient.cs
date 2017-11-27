using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class CertAuthClient : ChannelFactory<CertAuth>, CertAuth, IDisposable
    {
        CertAuth factory;

        public CertAuthClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            //string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            string cltCertCN = "testClient";

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.CurrentUser, cltCertCN);

            factory = this.CreateChannel();
        }

        public bool SetAlarm(Message message, byte[] signature)
        {
            bool allowed = false;

            var clientIndentity = WindowsIdentity.GetCurrent();

            try
            {
                factory.SetAlarm(message, signature);
                Console.WriteLine("SetAlarm() successfully executed for user {0}.", clientIndentity.Name.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to SetAlarm(), {0}.", e.Message);
            }

            return allowed;
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        //public void printSmth()
        //{
        //    Console.WriteLine("Does this shit work!!!\n");
        //}
    }
}

