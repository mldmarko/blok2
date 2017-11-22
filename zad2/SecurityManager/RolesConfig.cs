using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public enum Permissions
    {
        SetAlarm = 0,
        Test = 1
    }

    public class RolesConfig
    {
        static string[] Empty = new string[] { };

        public static string[] GetPermissions(string role)
        {
            switch (role)
            {
                case "None": return WorkWithXML.ReadXml(role);
                case "Everyone": return WorkWithXML.ReadXml(role);
                case "Local_account_and_member_of_Administrators_group": return WorkWithXML.ReadXml(role);
                case "HelpLibraryUpdaters": return WorkWithXML.ReadXml(role);
                case "Administrators": return WorkWithXML.ReadXml(role);
                case "Distributed_COM_Users": return WorkWithXML.ReadXml(role);
                case "Performance_Log_Users": return WorkWithXML.ReadXml(role);
                case "Users": return WorkWithXML.ReadXml(role);
                case "INTERACTIVE": return WorkWithXML.ReadXml(role);
                case "CONSOLE_LOGON": return WorkWithXML.ReadXml(role);
                case "Authenticated_Users": return WorkWithXML.ReadXml(role);
                case "This_Organization": return WorkWithXML.ReadXml(role);
                case "Local_account": return WorkWithXML.ReadXml(role);
                case "LOCAL": return WorkWithXML.ReadXml(role);
                case "NTLM_Authentication": return WorkWithXML.ReadXml(role);
                default: return Empty;
            }
        }
    }
}
