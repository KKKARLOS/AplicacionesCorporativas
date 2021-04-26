using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;
using SUPER.BLL;

public partial class Capa_Presentacion_Administracion_Absentismo_Default : System.Web.UI.Page
{
    public int nAnoMesActual = DateTime.Now.Year * 100 + DateTime.Now.Month;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.TituloPagina = "Control de absentismo";
            Master.nBotonera = 17;
            Master.bFuncionesLocales = true;
            Master.bEstilosLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/documentos.js");
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ObtenerControlAbsentismo(int annomes, string sCentros, string sEvaluadores, string sPSN)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(@"<table id='tblDatos' border='0' cellspacing='0' cellpadding='0'>
                    <colgroup>
                    <col style='width:325px;' />
                    <col style='width:325px;' />
                    <col style='width:140px;' />
                    <col style='width:60px;' />
                    <col style='width:60px;' />
                    <col style='width:60px;' />
                </colgroup>");

        DataSet ds = SUPER.BLL.Profesional.ObtenerControlAbsentismo(annomes, sCentros, sEvaluadores, sPSN);

        foreach (DataRow oFila in ds.Tables[0].Rows)
        {
            sb.Append(@"<tr>
                        <td>" + oFila["Profesional"].ToString() + @"</td>
                        <td>" + oFila["Evaluador"].ToString() + @"</td>
                        <td>" + oFila["Centro"].ToString() + @"</td>
                        <td class='num' title='" + oFila["dias_baja_iberper"].ToString() + "'>" + ((double)oFila["dias_baja_iberper"]).ToString("N") + @"</td>
                        <td class='num' title='" + oFila["dias_baja_pge"].ToString() + "'>" + ((double)oFila["dias_baja_pge"]).ToString("N") + @"</td>
                        <td class='num' title='" + oFila["dias_baja_iap"].ToString() + "'>" + ((double)oFila["dias_baja_iap"]).ToString("N") + @"</td>
                       </tr>");
        }

        sb.Append("</table>");

        return sb.ToString();
    }

}
