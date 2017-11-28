using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    public class WinAuthServer : IServer
    {

        public bool SetAlarm(Message message, BigInteger signature)
        {
            Audit.AuthorizationSuccess(Formatter.ParseName(WindowsIdentity.GetCurrent().Name), "SetAlarm method");
            BigInteger n, d;
            using (StreamReader sr = new StreamReader(@"../../../publicKey.txt"))
            {
                n = BigInteger.Parse(sr.ReadLine());
                d = BigInteger.Parse(sr.ReadLine());
            }

            bool signatureVerified = DigitalSignature.VerifySignature(message, signature, n, d);
           
            return Database.InternModel.SetAlarm(message.BlockIndex, message.VectorIndex, message.AlarmKey, message.Alarm);  
        }
    }
}
