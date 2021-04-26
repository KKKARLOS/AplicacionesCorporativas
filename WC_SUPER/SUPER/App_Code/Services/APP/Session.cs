using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IB.SUPER.Services.APP
{
    [WebService(Namespace = "http://SUPER.ibermatica.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class Session : System.Web.Services.WebService
    {

        public Session()
        {
        }

        [WebMethod(EnableSession = true)]
        public void Resetear()
        {
        }

    }
}