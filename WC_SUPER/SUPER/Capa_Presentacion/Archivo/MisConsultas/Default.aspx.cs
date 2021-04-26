using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML="";
    public int nConsultas=0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Mis consultas";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.Modulo = "NEUTRAL";

                nConsultas = USUARIO_CONSULTAPERSONAL.GetNumConsultas(Utilidades.GetUserActual(), true);
                string strTabla = obtenerConsultas("1");

                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar las consultas", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getConsultas"):
                sResultado += obtenerConsultas(aArgs[1]);
                break;
            //case ("ejecutar"):
            //    //sResultado += ejecutarConsulta(aArgs[1], aArgs[2]);
            //    sResultado += ejecutarConsultaDS(aArgs[1], aArgs[2]);                
            //    break;
            case ("grabar"):
                sResultado += grabar(aArgs[1]);
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string obtenerConsultas(string sEstado)
    {
        try
        {
            string sColor = "black";
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = USUARIO_CONSULTAPERSONAL.ObtenerCatalogo(null, Utilidades.GetUserActual(), (sEstado == "1") ? true : false);

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:480px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t472_idconsulta"].ToString() + "' ");
                if ((bool)dr["t473_estado"])
                {
                    sb.Append("activa='1' ");
                    sColor = "black";
                }
                else
                {
                    sb.Append("activa='0' ");
                    sColor = "#CCCCCC";
                }
                sb.Append("procalm='" + dr["t472_procalm"].ToString() + "' ");
                sb.Append("num_parametros='" + dr["num_parametros"].ToString() + "' ");
                sb.Append("onclick=\"ms(this);\" ondblclick=\"ejecutar(this);\" ");
                sb.Append("style='height:20px;");
                if (dr["t472_descripcion"].ToString() != "")
                    sb.Append("noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Comentario] body=[" + dr["t472_descripcion"].ToString().Replace((char)34, (char)39).Replace(((char)13).ToString(), "<br>") + "] hideselects=[off]\"");
                else
                    sb.Append("' ");

                sb.Append("><td style='text-align:center;' ><img src='../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                sb.Append("<td style='padding-left:3px;color:" + sColor + ";'><nobr class='NBR W450' ");
                sb.Append(">" + dr["t472_denominacion"].ToString() + "</nobr></td>");  
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar las consultas", ex);
        }
    }
    /*
    protected string ejecutarConsulta(string sProdAlm, string sParametros)
    {
        StringBuilder sb = new StringBuilder();
        string sAux = "", sPrimer="";
        try
        {
            string[] aParametros = Regex.Split(sParametros, "///");
            object[] aObjetos = new object[(sParametros=="")? 1:aParametros.Length + 1];
            aObjetos[0] = Utilidades.GetUserActual();
            int i = 1;
            foreach (string oParametro in aParametros)
            {
                if (oParametro == "") continue;
                string[] aDatos = Regex.Split(oParametro, "##");
                switch (aDatos[0])
                {
                    case "A": aObjetos[i] = int.Parse(aDatos[1]); break;
                    case "M": aObjetos[i] = double.Parse(aDatos[1].Replace(".",",")); break;
                    case "B": aObjetos[i] = (aDatos[1] == "1") ? true : false; break;
                    default: aObjetos[i] = aDatos[1]; break;
                }
                
                i++;
            }
            SqlDataReader dr = CONSULTAPERSONAL.EjecutarConsulta(sProdAlm, aObjetos);

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<tbody>");
            bool bTitulos = false;
            while (dr.Read())
            {
                if (!bTitulos)
                {
                    sb.Append("<tr align='center'>");
                    for (int x = 0; x < dr.FieldCount; x++)
                    {
                        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + dr.GetName(x) + "</td>");
                    }
                    sb.Append("</tr>");
                    bTitulos = true;
                }
                sb.Append("<tr>");
                for (int x = 0; x < dr.FieldCount; x++)
                {
                    //sb.Append("<td>"+ dr.GetValue(x) +"</td>");
                    //sAux = dr.GetProviderSpecificFieldType(x).ToString();
                    //sAux = dr.GetFieldType(x).ToString();
                    sAux = dr.GetValue(x).ToString();
                    if (dr.GetDataTypeName(x) == "text" && sAux.Trim() != "")
                    {//Para el contenido de campos de tipo Text hacemos transformaciones para que no falle la exportación a Excel
                        sAux = sAux.Replace("<", " < ");
                        sAux = sAux.Replace(">", " > ");
                        sAux = sAux.Trim();
                        sPrimer = sAux.Substring(0, 1);
                        switch (sPrimer)
                        {
                            case "-":
                            case "+":
                            case "=":
                                sAux = "(" + sPrimer + ")" + sAux.Substring(1);
                                break;
                        }
                    }
                    sb.Append("<td>" + sAux + "</td>");
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la consulta.", ex);
        }
    }
    protected string ejecutarConsultaDS(string sProdAlm, string sParametros)
    {
        StringBuilder sb = new StringBuilder();
        string sAux = "", sPrimer = "";

        try
        {
            string[] aParametros = Regex.Split(sParametros, "///");
            object[] aObjetos = new object[(sParametros == "") ? 1 : aParametros.Length + 1];
            aObjetos[0] = Utilidades.GetUserActual();
            #region Cargo parámetros
            int v = 1;
            foreach (string oParametro in aParametros)
            {
                if (oParametro == "") continue;
                string[] aDatos = Regex.Split(oParametro, "##");
                switch (aDatos[0])
                {
                    case "A": aObjetos[v] = int.Parse(aDatos[1]); break;
                    case "M": aObjetos[v] = double.Parse(aDatos[1].Replace(".", ",")); break;
                    case "B": aObjetos[v] = (aDatos[1]=="1")? true:false; break;
                    default: aObjetos[v] = aDatos[1]; break;
                }
                v++;
            }
            #endregion
            DataSet ds = CONSULTAPERSONAL.EjecutarConsultaDS(sProdAlm, aObjetos);
            #region Creo churro con los datos del DataSet
            string sIdCache = "";
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
                sb.Append("<tbody>");
                bool bTitulos = false;

                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    if (!bTitulos)
                    {
                        sb.Append("<tr align='center'>");
                        for (int x = 0; x < ds.Tables[i].Columns.Count; x++)     
                        {
                            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + ds.Tables[i].Columns[x].ColumnName + "</td>");
                        }
                        sb.Append("</tr>");
                        bTitulos = true;
                    }
                    sb.Append("<tr>");
                    for (int x = 0; x < ds.Tables[i].Columns.Count; x++)     
                    {
                        sAux = ds.Tables[i].Rows[j][x].ToString();

                       if (ds.Tables[i].Columns[x].DataType.Name == "text" && sAux.Trim() != "")
                        {//Para el contenido de campos de tipo Text hacemos transformaciones para que no falle la exportación a Excel
                            sAux = sAux.Replace("<", " < ");
                            sAux = sAux.Replace(">", " > ");
                            sAux = sAux.Trim();
                            sPrimer = sAux.Substring(0, 1);
                            switch (sPrimer)
                            {
                                case "-":
                                case "+":
                                case "=":
                                    sAux = "(" + sPrimer + ")" + sAux.Substring(1);
                                    break;
                            }
                        }
                        sb.Append("<td>" + sAux + "</td>");
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table>{{septabla}}");
            }
            #endregion

            //karlos
            sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString();
            //Session[sIdCache] = Utilidades.escape(sb.ToString());
            //return "OK@#@" + sb.ToString();
            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la consulta.", ex);
        }
    }
    */
    private string grabar(string strConsultas)
    {
        string sResul = "";
        try
        {
            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            string[] aConsultas = Regex.Split(strConsultas, "///");
            foreach (string oConsulta in aConsultas)
            {
                if (oConsulta == "") continue;

                string[] aDatos = Regex.Split(oConsulta, "##");
                USUARIO_CONSULTAPERSONAL.Update(tr, Utilidades.GetUserActual(), int.Parse(aDatos[0]), (aDatos[1] == "1") ? true : false);
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las consultas.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
