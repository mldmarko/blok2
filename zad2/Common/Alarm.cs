using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Alarm
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public byte Risk { get; set; }
        [DataMember]
        public DateTime TimeGenerated { get; set; }

        public override string ToString()
        {
            return $"{TimeGenerated} {Risk} {Message}";
        }
    }
}
