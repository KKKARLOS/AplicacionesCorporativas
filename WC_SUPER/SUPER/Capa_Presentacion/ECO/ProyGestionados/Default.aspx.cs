using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EO.Web;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            //Master.nBotonera = 50;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Proyectos gestionados";
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                
            if (!Page.IsPostBack)
            {
                if (!Page.IsCallback)
                {
                    try
                    {
                        string strTabla = getProyectosGestionados();
                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                        else Master.sErrores += Errores.mostrarError(aTabla[1]);
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
                    }

                    //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                    //   y la función que va a acceder al servidor
                    string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                    string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                    //2º Se "registra" la función que va a acceder al servidor.
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                }
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("refrescar"):
                //sResultado += getProyectosNoCerrados(int.Parse(aArgs[1]));
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

    private string getProyectosGestionados()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<TABLE class='texto MANO' id=tblDatos style='width: 700px; text-align:left;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:440px;' />");
            sb.Append("<col style='width:200px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("</colgroup>");
            SqlDataReader dr = PROYECTOSUBNODO.GetGestionados((int)Session["UsuarioActual"]);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["Responsable"].ToString()) + "\" ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("style='height:20px;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:75px'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:75px'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");    
                sb.Append(">");

                sb.Append("<td style='text-align:right;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td style='padding-left:10px;'><nobr class='NBR W430'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                if ((int)dr["externalizado"] == 1) sb.Append("<td style='text-align:center;'><img src='../../../images/imgOK.gif' /></td>");
                else sb.Append("<td style='text-align:center;'></td>");
                if ((bool)dr["t638_facturaSA"]) sb.Append("<td style='text-align:center;'><img src='../../../images/imgOK.gif'/></td>");
                else sb.Append("<td style='text-align:center;'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos.", ex);
        }
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
}
