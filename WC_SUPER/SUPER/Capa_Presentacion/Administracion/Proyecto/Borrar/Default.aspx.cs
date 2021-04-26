using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtmlPE;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 38;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Borrado de Proyecto Econ�mico";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    this.lblNodo2.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerPEs(aArgs[1]);
                break;
            case ("eliminar"):
                sResultado += EliminarPE(aArgs[1], aArgs[2]);
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
    private string ObtenerPEs(string sNumPE)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 920px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");//Cualidad
            sb.Append("<col style='width:400px' />");//Seudonimo
            sb.Append("<col style='width:300px' />");//Nodo
            sb.Append("<col style='width:200px' />");//Responsable
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = PROYECTO.ObtenerProyectosSubNodo(tr, int.Parse(sNumPE));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' style='height:20px' onmouseover='TTip(event)'>");

                //if (dr["t301_categoria"].ToString() == "P") sb.Append("<td><img src='../../../../images/imgProducto.gif' width='16px' height='16px' /></td>");
                //else sb.Append("<td><img src='../../../../images/imgServicio.gif' width='16px' height='16px' /></td>");

                switch (dr["t305_cualidad"].ToString())
                {
                    case "C": sb.Append("<td><img src='../../../../images/imgIconoContratante.gif' width='16px' height='16px' /></td>"); break;
                    case "J": sb.Append("<td><img src='../../../../images/imgIconoRepJor.gif' width='16px' height='16px' /></td>"); break;
                    case "P": sb.Append("<td><img src='../../../../images/imgIconoRepPrecio.gif' width='16px' height='16px' /></td>"); break;
                }

                //switch (dr["t301_estado"].ToString())
                //{
                //    case "A": sb.Append("<td><img src='../../../../images/imgIconoProyAbierto.gif' width='16px' height='16px' /></td>"); break;
                //    case "C": sb.Append("<td><img src='../../../../images/imgIconoProyCerrado.gif' width='16px' height='16px' /></td>"); break;
                //    case "H": sb.Append("<td><img src='../../../../images/imgIconoProyHistorico.gif' width='16px' height='16px' /></td>"); break;
                //    case "P": sb.Append("<td><img src='../../../../images/imgIconoProyPresup.gif' width='16px' height='16px' /></td>"); break;
                //}

                sb.Append("<td><div class='NBR W395'>" + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "</div></td>");
                sb.Append("<td><div class='NBR W290'>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "</div></td>");
                sb.Append("<td><div class='NBR W190'>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "</div></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            //Obtengo los datos del proyecto
            PROYECTO oProy = PROYECTO.Obtener(tr, int.Parse(sNumPE));

            return "OK@#@" + sb.ToString() + "@#@" +
                    Utilidades.escape(oProy.t301_denominacion) + "##" +
                    Utilidades.escape(oProy.t302_denominacion) + "##" +
                    oProy.t301_estado + "##" + oProy.t301_categoria;
        }
        catch (Exception ex)
        {
            if (ex.Message == "No se ha obtenido ningun dato de PROYECTO")
                return "error@#@Proyecto no encontrado.";
            else
                return "error@#@Error al obtener el Proyecto Econ�mico./n " + ex.Message;
        }
    }
    private string EliminarPE(string sNumPE, string sPSNs)
    {
        string sResul = "OK@#@";
        #region abrir conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion

        try
        {
            if (sNumPE != "")
            {
                PROYECTO.Delete(tr, int.Parse(sNumPE));

                string[] aPSN = Regex.Split(sPSNs, ",");
                foreach (string oPSN in aPSN)
                {
                    if (oPSN == Session["ID_PROYECTOSUBNODO"].ToString())
                    {
                        Session["ID_PROYECTOSUBNODO"] = "";
                        Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                        Session["RTPT_PROYECTOSUBNODO"] = false;
                        Session["MONEDA_PROYECTOSUBNODO"] = "";
                        break;
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //Master.sErrores = Errores.mostrarError("Error al eliminar el proyecto econ�mico " + sNumPE, ex);
            sResul = "error@#@Error al eliminar el proyecto econ�mico " + sNumPE + "\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}