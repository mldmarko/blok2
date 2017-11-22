using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WinAuthClient : ChannelFactory<IServer>, IServer, IDisposable
    {
        IServer factory;

        public WinAuthClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
        }


        public bool SetAlarm(uint blockIndex, uint vectorIndex, uint alarmKey, Alarm alarm)
        {
            bool allowed = false;

            var clientIndentity = WindowsIdentity.GetCurrent();

            try
            {
                factory.SetAlarm(blockIndex, vectorIndex, alarmKey, alarm);
                Console.WriteLine("SetAlarm() successfully executed for user {0}.", clientIndentity.Name.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to SetAlarm(), {0}.", e.Message);
            }

            return allowed;
        }
    }
}
