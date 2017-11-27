using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class CertAuthServer : CertAuth
    {
        public void printSmth()
        {
            Console.WriteLine("SmTH");
        }

        public bool SetAlarm(Message message, byte[] signature)
        {
            Audit.AuthorizationSuccess("Test user", "SetAlarm method");

            return Database.InternModel.SetAlarm(message.BlockIndex, message.VectorIndex, message.AlarmKey, message.Alarm);
            
        }
    }
}
