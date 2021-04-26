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


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, sLectura;
    public int nPSN;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            sErrores = "";
            sLectura = "false";
            string sEstado = Request.QueryString["Est"].ToString();
            this.hdnEstProy.Value = sEstado;
            nPSN = int.Parse(Request.QueryString["nPSN"].ToString());
            this.hdnIdProyectoSubNodo.Value = nPSN.ToString();
            try
            {
                ////18/10/2016 Por petición de Yolanda los proyectos cerrados no permiten añadir documentos
                //if (sEstado == "C" || sEstado == "H")
                //    this.hdnModoAcceso.Value = "R";
                //else
                    this.hdnModoAcceso.Value = PROYECTOSUBNODO.getAcceso(null, nPSN, int.Parse(Session["UsuarioActual"].ToString()));

                div1.InnerHtml = Utilidades.ObtenerDocumentos("PSN", nPSN, this.hdnModoAcceso.Value, sEstado);

                string strTabla2 = ObtenerListaDocumentos(nPSN, int.Parse(Session["UsuarioActual"].ToString()));
                string[] aTabla2 = Regex.Split(strTabla2, "@#@");
                if (aTabla2[0] == "OK") div2.InnerHtml = aTabla2[1];
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad="";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("documentos"):
                //sResultado += ObtenerDocumentos(aArgs[1]);
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos("PSN", int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    protected string EliminarDocumentos(string strIdsDocs)
    {
        string sResul = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region eliminar documentos
            if (strIdsDocs != "")
            {
                string[] aDocs = Regex.Split(strIdsDocs, "##");

                foreach (string oDoc in aDocs)
                {
                    DOCUPE.Delete(tr, int.Parse(oDoc));
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string ObtenerListaDocumentos(int nPSN, int idUserAct)
    {
        StringBuilder sbuilder = new StringBuilder();
        SqlDataReader dr = DOCUPE.Lista(null, nPSN, idUserAct);

        sbuilder.Append("<table id='tblDocumentos2' class='texto MANO' style='width: 850px;'>");
        sbuilder.Append("<colgroup><col style='width:290px;' /><col style='width:235px' /><col style='width:225px' /><col style='width:100px' /></colgroup>");
        sbuilder.Append("<tbody>");
        while (dr.Read())
        {
            sbuilder.Append("<tr id='" + dr["idDocu"].ToString() + "' style='height:20px;' sTipo='" + dr["TIPO"].ToString() + "' sAutor='" + dr["Num_Autor"].ToString() + "' onmouseover='TTip(event);' >");

            //No se permite modificar.
            string sTTip = "";
            if (dr["des_pt"].ToString() != "") sTTip += "Proy. Téc.: " + dr["des_pt"].ToString();
            if (dr["des_fase"].ToString() != "") sTTip += (char)10 + "Fase:          " + dr["des_fase"].ToString();
            if (dr["des_actividad"].ToString() != "") sTTip += (char)10 + "Actividad:   " + dr["des_actividad"].ToString();
            if (dr["des_tarea"].ToString() != "") sTTip += (char)10 + "Tarea:        " + dr["des_tarea"].ToString();

            sbuilder.Append("<td style='padding-left:5px;'><nobr class='NBR' style='width:280px' title=\"" + sTTip + "\">" + dr["Descripcion"].ToString() + "</nobr></td>");

            if (dr["Nombrearchivo"].ToString() == "")
                sbuilder.Append("<td></td>");
            else
            {
                string sNomArchivo = dr["Nombrearchivo"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                //Si el archivo no es privado, o es privado y la persona que entra es el autor, o es administrador, se permite descargar.
                //if ((!(bool)dr["Privado"]) || ((bool)dr["Privado"] && dr["Num_Autor"].ToString() == Session["UsuarioActual"].ToString()) || Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A")
                    sbuilder.Append("<td><img src=\"../../../../images/imgDescarga.gif\" width='16px' height='16px' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">&nbsp;<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
                //else
                //    sbuilder.Append("<td><img src=\"../../../../images/imgSeparador.gif\" width='16px' height='16px' style='vertical-align:bottom;'>&nbsp;<nobr class='NBR' style='width:205px;'>" + sNomArchivo + "</nobr></td>");
            }

            if (dr["Weblink"].ToString() == "")
                sbuilder.Append("<td></td>");
            else
            {
                string sHTTP = "";
                if (dr["Weblink"].ToString().ToLower().IndexOf("http") == -1) sHTTP = "http://";
                sbuilder.Append("<td><a href='" + sHTTP + dr["Weblink"].ToString() + "'><nobr class='NBR' style='width:215px'>" + dr["Weblink"].ToString() + "</nobr></a></td>");
            }

            sbuilder.Append("<td><nobr class='NBR' style='width:90px;'>" + dr["Autor"].ToString() + "</nobr></td>");
        }
        dr.Close();
        dr.Dispose();
        sbuilder.Append("</tbody>");
        sbuilder.Append("</table>");

        return "OK@#@" + sbuilder.ToString();
    }
}
