﻿using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Policy;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding1 = new NetTcpBinding();
            string address1 = "net.tcp://localhost:4000/IServer";

            ServiceHost host1 = new ServiceHost(typeof(WinAuthServer));
            host1.AddServiceEndpoint(typeof(IServer), binding1, address1);
            host1.Authorization.ServiceAuthorizationManager = new CustomAuthorizationManager();
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host1.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            host1.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            host1.Open();

            NetTcpBinding binding2 = new NetTcpBinding();
            string address2 = "net.tcp://localhost:4001/IServer";

            ServiceHost host2 = new ServiceHost(typeof(CertificateServer));
            host2.AddServiceEndpoint(typeof(IServer), binding2, address2);
            host2.Open();

            Console.WriteLine("Server started.");


            new Thread(Read).Start();


            Console.ReadKey(true);
            host1.Close();
            host2.Close();
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
