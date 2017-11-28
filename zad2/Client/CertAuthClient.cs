using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class CertAuthClient : ChannelFactory<IServer>, IServer, IDisposable
    {
        IServer factory;

        public CertAuthClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            string cltCertCN = "testClient";
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.CurrentUser, cltCertCN);

            factory = this.CreateChannel();
        }

        public bool SetAlarm(Message message, BigInteger signature)
        {
            bool allowed = false;

            var clientIndentity = WindowsIdentity.GetCurrent();

            try
            {
                factory.SetAlarm(message, signature);
                Console.WriteLine("Alarm successfully set for user {0}.", clientIndentity.Name.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to set Alarm, {0}.", e.Message);
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

    }
}

