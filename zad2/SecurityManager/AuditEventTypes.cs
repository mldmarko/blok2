using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    //Definise i vraca tipove dogadjaja koje je moguce logovati
    //Tipovi dogadjaja su definisani u AuditEventTypes.resx resursnom fajlu
    public enum AuditEventTypes
    {
        UserAuthenticationSuccess = 0,
        UserAuthorizationSuccess = 1,
        UserAuthorizationFailed = 2,
        UserAuthenticationFailed = 3
    }

    public class AuditEvents
    {
        private static ResourceManager resourceManager = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMgr
        {
            get
            {
                lock (resourceLock)
                {
                    if (resourceManager == null)
                    {
                        resourceManager = new ResourceManager(typeof(AuditEventsFile).FullName, Assembly.GetExecutingAssembly());
                    }
                    return resourceManager;
                }
            }
        }

        public static string UserAuthenticationSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.UserAuthenticationSuccess.ToString());
            }
        }

        public static string UserAuthorizationSuccess
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.UserAuthorizationSuccess.ToString());
            }
        }

        public static string UserAuthorizationFailed
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.UserAuthorizationFailed.ToString());
            }
        }

        public static string UserAuthenticationFailed
        {
            get
            {
                return ResourceMgr.GetString(AuditEventTypes.UserAuthenticationFailed.ToString());
            }
        }
    }
}
