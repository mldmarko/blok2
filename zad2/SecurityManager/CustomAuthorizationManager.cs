using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    //Komponenta koja opisuje mehanizam autorizacija WCF servisa
    //Metoda CheckAccessCore proverava autorizaciju korisnika, da li korisnik ima pravo izvrsavanja funkcije 
    public class CustomAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            bool authorized = false;

            IPrincipal principal = operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] as IPrincipal;

            if (principal != null)
            {
                authorized = (principal as CustomPrincipal).IsInRole(Permissions.SetAlarm.ToString());
            }

            return authorized;
        }
    }
}
