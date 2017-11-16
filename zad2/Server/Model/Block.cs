using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class Block
    {
        private Dictionary<uint, Vector> vectors;
        private object locker;

        public Block()
        {
            vectors = new Dictionary<uint, Vector>();
            locker = new object();
        }

        public bool GetAlarm(uint vectorIndex, uint alarmKey, out Alarm alarm)
        {
            Vector vector;
            lock (locker)
            {
                if (!vectors.TryGetValue(vectorIndex, out vector))
                {
                    alarm = null;
                    return false;
                }
            }

            return vector.GetAlarm(alarmKey, out alarm);
            
        }

        public void SetAlarm(uint vectorIndex, uint alarmKey, Alarm alarm)
        {
            Vector vector;
            lock (locker)
            {
                if (!vectors.TryGetValue(vectorIndex, out vector))
                {
                    vector = new Vector();
                    vectors[vectorIndex] = vector;
                }
            }
            vector.SetAlarm(alarmKey, alarm);
        }

        public void SaveAllToFile(StreamWriter fileWritter)
        {
            List<uint> keys = GetKeys();

            foreach (uint vectorKey in keys)
            {
                vectors[vectorKey].SaveAllToFile(fileWritter);
            }
        }

        private List<uint> GetKeys()
        {
            lock (locker)
            {
                return vectors.Keys.ToList();
            }
        }

    }
}
