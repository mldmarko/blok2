using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    [DataContract]
    public class Message
    {
        [DataMember]
        public uint BlockIndex { get; set; }
        [DataMember]
        public uint VectorIndex { get; set; }
        [DataMember]
        public uint AlarmKey { get; set; }
        [DataMember]
        public Alarm Alarm { get; set; }

        public Message(uint blockIndex, uint vectorIndex, uint alarmKey, Alarm alarm)
        {
            BlockIndex = blockIndex;
            VectorIndex = vectorIndex;
            AlarmKey = alarmKey;
            Alarm = alarm;
        }
    }
}
