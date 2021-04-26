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
    public class LogJS : System.Web.Services.WebService
    {

        public LogJS()
        {
        }

        [WebMethod(EnableSession = true)]
        public void Log(string file, string line, string msg)
        {
            try
            {
                IB.SUPER.Shared.LogError.LogearErrorJS(file, line, msg);
            }
            catch (Exception)
            {
            }
        }

    }
}