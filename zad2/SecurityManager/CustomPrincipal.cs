using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class CustomPrincipal : IPrincipal, IDisposable
    {
        private WindowsIdentity identity = null;
        private Dictionary<string, string[]> roles = new Dictionary<string, string[]>();
        private List<string> rls = new List<string>();

        public CustomPrincipal(WindowsIdentity winIdentity)
        {
            this.identity = winIdentity;

            foreach (IdentityReference group in this.identity.Groups)
            {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                string groupName = Formatter.ParseName(name.ToString());

                if (!roles.ContainsKey(groupName))
                {
                    roles.Add(groupName, RolesConfig.GetPermissions(groupName));
                    rls.Add(groupName);
                }
            }

            //WorkWithXML.Write(rls);
        }

        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
        }

        public void Dispose()
        {
            if (identity != null)
            {
                identity.Dispose();
                identity = null;
            }
        }

        public bool IsInRole(string role)
        {
            bool IsAuthz = false;
            foreach (string[] r in roles.Values)
            {
                if (r.Contains(role))
                {
                    IsAuthz = true;
                    break;
                }
            }

            return IsAuthz;
        }
    }
}
