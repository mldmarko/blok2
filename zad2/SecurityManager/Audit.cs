using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    //Nudi funkcionalnost logovanja u pozivanjem neke od metoda
    /*
     * Primer poziva loga na serveru:
     * 
     *      Audit.AuthorizationSuccess(principal.Identity.Name, OperationContext.Current.IncomingMessageHeaders.Action);
     * 
     * */
     //Klasa omogucuje upis razlicitih bezbednosnih dogadjaja u WindowsEventLog
     //Skup bezbednosnih dogadjaja definisan je enumeracijom AuditEventTypes a string je dat u resursnom fajlu AuditEventsFile
    public class Audit : IDisposable
    {

        private static EventLog customLog = null;
        const string SourceName = "BLOK2 Projekat - SecurityManager.Audit";
        const string LogName = "BLOK2 Projekat - Custom Log";

        static Audit()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }

                customLog = new EventLog(LogName, Environment.MachineName, SourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }


        public static void AuthenticationSuccess(string userName)
        {
            if (customLog != null)
            {
                string message = String.Format(AuditEventsFile.UserAuthenticationSuccess, userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.UserAuthenticationSuccess));
            }
        }

        public static void AuthorizationSuccess(string userName, string serviceName)
        {
            if (customLog != null)
            {
                string message = String.Format(AuditEventsFile.UserAuthorizationSuccess, userName, serviceName);
                customLog.WriteEntry(message);

            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.UserAuthenticationSuccess));
            }
        }

        public static void AuthorizationFailed(string userName, string serviceName, string reason)
        {
            if (customLog != null)
            {
                string message = String.Format(AuditEventsFile.UserAuthorizationFailed, userName, serviceName, reason);
                customLog.WriteEntry(message);

            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.UserAuthenticationSuccess));
            }
        }

        public static void AuthenticationFailed(string userName)
        {
            if (customLog != null)
            {
                string message = String.Format(AuditEventsFile.UserAuthenticationFailed, userName);
                customLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.", (int)AuditEventTypes.UserAuthenticationSuccess));
            }
        }

        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }
    }
}
