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
//Para el MemoryStream
using System.IO;

public partial class Capa_Presentacion_Administracion_EjecutarPA_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.TituloPagina = "Ejecución de procedimientos almacenados.";
            Master.bFuncionesLocales = true;
            Master.bEstilosLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1.min.js");
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ObtenerProcedimientos()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblProcedimientos' border='0' cellspacing='0' cellpadding='0'>");

        DataTable dt = PROCALMA.ObtenerCatalogoProcedimientos();
        foreach (DataRow oFila in dt.Rows)
        {
            sb.Append("<tr style='height:20px;' id='" + oFila["t203_idprocalma"].ToString() + @"'>
                        <td>" + oFila["t203_denominacion"].ToString() + @"</td>
                       </tr>");
        }

        sb.Append("</table>");

        return sb.ToString();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ObtenerParametros(string sPA)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(@"<table id='tblParametros' border='0' cellspacing='0' cellpadding='0'>
                    <colgroup>
                        <col style='width:90px;' /> 
                        <col style='width:250px;' />
                    </colgroup>");

        SqlParameter[] aParam = PROCALMA.ObtenerParametrosPA(sPA);
        foreach (SqlParameter oParam in aParam)
        {
            sb.Append("<tr id='" + oParam.ParameterName + @"'>
                        <td>" + oParam.ParameterName + @"</td>
                        <td><input type='text' class='txtM' value='' style='width:80px;' /></td>
                       </tr>");
        }

        sb.Append("</table>");

        return sb.ToString();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void EjecutarProcedimiento(string sDatos)
    {
        string[] aDatos = Regex.Split(sDatos, "{sepdatos}");
        string[] aParamCliente = Regex.Split(aDatos[1], "{sep}");

        SqlParameter[] aParamDestino = PROCALMA.ObtenerParametrosPA(aDatos[0]);
        int i = 0;
        foreach (SqlParameter oParamDestino in aParamDestino)
        {
            //oParamDestino.Value = 
            switch(oParamDestino.SqlDbType){
                case SqlDbType.Int:{
                    oParamDestino.Value = (aParamCliente[i] == "")? null: (int?)int.Parse(aParamCliente[i]);
                    break;
                }
                case SqlDbType.VarChar:{
                    oParamDestino.Value = (aParamCliente[i] == "")? null: aParamCliente[i];
                    break;
                }
            }
            i++;
        }
        //IB.EjecutarPA.BLL.Procedimiento.Ejecutar(aDatos[0], aParamDestino);
        PROCALMA.Ejecutar(aDatos[0], aParamDestino);

    }

    /// <summary>
    /// Lee de la tabla ZZKORO_ALTAGES_BATCH_INPUT y genera un fichero en una carpeta determinada
    /// </summary>
    /// <param name="solicitante"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string EscribirFicheroAltaGestion(string solicitante)
    {
        //string path = @"\\IBDATDO\Area\Temp\Mikel\prueba.txt";
        string path = @"\\IBDATDO\Area\Grupos\SSII\subext\ETT\altages_batch_input.txt";
        string sRes = "Fichero almacenado en " + path;

        StringBuilder result = new StringBuilder();
        DataSet ds = SUPER.DAL.PROCALMA.AltaGestion(null);

        foreach (DataRow oProyecto in ds.Tables[0].Rows)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                result.Append(oProyecto[i].ToString());
                result.Append(i == ds.Tables[0].Columns.Count - 1 ? "\r\n" : "\t");
            }
            //result.AppendLine();
        }

        StreamWriter swProyecto = new StreamWriter(path, false);
        swProyecto.WriteLine(result.ToString());
        swProyecto.Close();

        return sRes;
    }
}
