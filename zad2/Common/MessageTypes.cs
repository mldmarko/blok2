using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum AlarmMessageTypes
    {
        CodeGreen = 0,
        CodeYellow = 1,
        CodeRed = 2,
        CodeBlack = 3
    }

    public class MessageTypes
    {
        private static ResourceManager alarmResourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if (alarmResourceManager == null)
                    {
                        alarmResourceManager = new ResourceManager(typeof(AlarmMessagesFile).FullName, Assembly.GetExecutingAssembly());
                    }
                    return alarmResourceManager;
                }
            }
        }

        public static string CodeGreen
        {
            get
            {
                return ResourceMgr.GetString(AlarmMessageTypes.CodeGreen.ToString());
            }
        }

        public static string CodeYellow
        {
            get
            {
                return ResourceMgr.GetString(AlarmMessageTypes.CodeYellow.ToString());
            }
        }

        public static string CodeRed
        {
            get
            {
                return ResourceMgr.GetString(AlarmMessageTypes.CodeRed.ToString());
            }
        }

        public static string CodeBlack
        {
            get
            {
                return ResourceMgr.GetString(AlarmMessageTypes.CodeBlack.ToString());
            }
        }
    }
}
