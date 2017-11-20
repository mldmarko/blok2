using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    public class WinAuthServer : IServer
    {
        public bool SetAlarm(uint blockIndex, uint vectorIndex, uint alarmKey, Alarm alarm)
        {
            //Audit.AuthorizationSuccess("Test user", "SetAlarm method");

            return Database.InternModel.SetAlarm(blockIndex, vectorIndex, alarmKey, alarm);
        }
    }
}
