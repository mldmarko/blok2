using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class WinAuthServer : IServer
    {
        public bool SetAlarm(uint blockIndex, uint vectorIndex, uint alarmKey, Alarm alarm)
        {
            return Database.InternModel.SetAlarm(blockIndex, vectorIndex, alarmKey, alarm);
        }
    }
}
