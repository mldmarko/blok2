using Common;
using System.Collections.Generic;
using System.IO;

namespace Server.Model
{
    class Vector
    {
        private Dictionary<uint, Alarm> alarms;
        private object locker;

        public Vector()
        {
            alarms = new Dictionary<uint, Alarm>();
            locker = new object();
        }

        public bool GetAlarm(uint alarmKey, out Alarm alarm)
        {
            lock (locker)
            {
                return alarms.TryGetValue(alarmKey, out alarm);
            }
        }

        public void SetAlarm(uint alarmKey, Alarm alarm)
        {
            lock (locker)
            {
                alarms[alarmKey] = alarm;
            }
        }

        public void SaveAllToFile(StreamWriter fileWritter)
        {
            lock (locker)
            {
                foreach (Alarm alarm in alarms.Values)
                {
                    if (alarm.Risk % 2 == 0)
                    {
                        fileWritter.WriteLine(alarm);
                    }
                }
            }
        }

    }
}