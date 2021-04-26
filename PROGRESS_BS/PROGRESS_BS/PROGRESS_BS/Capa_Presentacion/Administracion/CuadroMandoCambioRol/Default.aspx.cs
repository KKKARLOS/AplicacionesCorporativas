using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_AdmCambioRol_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string getCountTiles()
    {
        IB.Progress.BLL.TramitacionCambioRol pro = null;
        try
        {            
            pro = new IB.Progress.BLL.TramitacionCambioRol();

            IB.Progress.Models.TramitacionCambioRol valores  = pro.getCountTiles(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);

            pro.Dispose();

            string retval = JsonConvert.SerializeObject(valores);
            return retval;
        }

        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            throw ex;
        }
    }
}