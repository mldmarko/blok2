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
    public class Alarm
    {
        [DataMember]
        public byte Risk { get; set; }
        [DataMember]
        public DateTime TimeGenerated { get; set; }
        [DataMember]
        private string Message;

        public override string ToString()
        {
            return $"{TimeGenerated} {Risk} {Message}";
        }

        public string GetMessage()
        {
            return Message;
        }

        public void SetMessageAlarmGreen()
        {
            Message = MessageTypes.CodeGreen;
        }

        public void SetMessageAlarmYellow()
        {
            Message = MessageTypes.CodeYellow;
        }

        public void SetMessageAlarmRed()
        {
            Message = MessageTypes.CodeRed;
        }

        public void SetMessageAlarmBlack()
        {
            Message = MessageTypes.CodeBlack;
        }
    }
}
