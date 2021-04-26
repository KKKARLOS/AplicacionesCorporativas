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
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sNodoFijo = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Profesionales por dependencia jerárquica";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                try
                {
                    //hdnIdProfesional.Text = Session["UsuarioActual"].ToString();

                    // provisional arriba 

                    hdnUsuarioActual.Value = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();                 
                    cboTipoItem.Items.Add(new ListItem("", ""));
                    cboTipoItem.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4), "1"));
                    cboTipoItem.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3), "2"));
                    cboTipoItem.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2), "3"));
                    cboTipoItem.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1), "4"));
                    cboTipoItem.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.NODO), "5"));
                    cboTipoItem.Items.Add(new ListItem(Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO), "6"));
                    cboTipoItem.Items.Add(new ListItem("Proyecto", "7"));
                    cboTipoItem.Items.Add(new ListItem("Contrato", "8"));
                    cboTipoItem.Items.Add(new ListItem("Horizontal", "9"));
                    cboTipoItem.Items.Add(new ListItem("Cliente", "10"));
                    cboTipoItem.Items.Add(new ListItem("Oficina Técnica", "11"));
                    cboTipoItem.Items.Add(new ListItem("Grupo Funcional", "12"));

                    cboTipoItem.Items.Add(new ListItem("Cualificador Qn", "13"));
                    if (Utilidades.EstructuraActiva("SN1")) cboTipoItem.Items.Add(new ListItem("Cualificador Q1", "14"));
                    if (Utilidades.EstructuraActiva("SN2")) cboTipoItem.Items.Add(new ListItem("Cualificador Q2", "15"));
                    if (Utilidades.EstructuraActiva("SN3")) cboTipoItem.Items.Add(new ListItem("Cualificador Q3", "16"));
                    if (Utilidades.EstructuraActiva("SN4")) cboTipoItem.Items.Add(new ListItem("Cualificador Q4", "17"));

                    //Cargo la denominacion del label Nodo
                    string sAux = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    if (sAux.Trim() != "")
                    {
                        this.lblNodo.InnerText = sAux;
                        this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                        this.gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    }

                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        hdnIdNodo.Visible = true;
                        txtDesNodo.Visible = true;
                        gomaNodo.Visible = true;
                        hdnAdmin.Value = "A" ;
                    }
                    else
                    {
                        cboCR.Visible = true;
                        hdnIdNodo.Visible = false;
                        txtDesNodo.Visible = false;
                        gomaNodo.Visible = false;
                        cargarNodos();
                    }

                    //string strTabla = obtenerDatos(Session["UsuarioActual"].ToString(), "", hdnNodos.Value, "", "");

                    //string[] aTabla = Regex.Split(strTabla, "@#@");
                    //if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                    //else Master.sErrores += Errores.mostrarError(aTabla[1]);
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
            case ("buscar"):
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9]);
                break;
            case "cargarSubnodos":
                sResultado += cargarSubnodos(aArgs[1]);
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
    private string obtenerDatos(string sUsuario, string sTipoItem, string sCRs, string sFigura, string sSn, string sPresupuestado, string sAbierto, string sCerrado, string sHistorico)
    {
        StringBuilder sb = new StringBuilder();
        string sCualidad = "";

        try
        {
            int? nTipoItem = null;
            if (sTipoItem != "") nTipoItem = int.Parse(sTipoItem);

            int? nSn = null;
            if (sSn != "") nSn = int.Parse(sSn);

            sb.Append("<table id='tblDatos' style='width:970px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:25px;' />");
            sb.Append("<col style='width:400px;' />");//
            sb.Append("<col style='width:345px;' />");//Profesional
            sb.Append("<col style='width:200px;' />");//
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            string sEstadosProy = "";
            if (sPresupuestado == "1") sEstadosProy="P,";
            if (sAbierto == "1") sEstadosProy += "A,";
            if (sCerrado == "1") sEstadosProy += "C,";
            if (sHistorico == "1") sEstadosProy += "H,";
            if (sEstadosProy.Length != 0) sEstadosProy = sEstadosProy.Substring(0, sEstadosProy.Length - 1);

            SqlDataReader dr = USUARIO.ObtenerQueEsUsuarios(sUsuario, nTipoItem, sCRs, nSn, sFigura, sEstadosProy);

            while (dr.Read())
            {
                if (
                    (!Utilidades.EstructuraActiva("SN1") && (int)dr["Item"] == 14)
                    || (!Utilidades.EstructuraActiva("SN2") && (int)dr["Item"] == 15)
                    || (!Utilidades.EstructuraActiva("SN3") && (int)dr["Item"] == 16)
                    || (!Utilidades.EstructuraActiva("SN4") && (int)dr["Item"] == 17)
                    ) continue;

                sb.Append("<tr item=" + dr["Item"].ToString() + " estado='" + dr["t301_estado"].ToString() + "' ");
/*
                string[] aFigurasIn = Regex.Split(dr["Figuras"].ToString(), ",");
                string[] aFigurasOut = new string[20]; 
                
                bool bExiste;
                int iCont = 0;

                foreach (string oFigura in aFigurasIn)
                {
                    if (oFigura == "") continue;

                    bExiste = false;
                    for (int i = 0; i < aFigurasOut.Length; i++)
                    {
                        if (oFigura[0].ToString()==aFigurasOut[i]) 
                        {
                            bExiste = true;
                            break;
                        }
                    }
                    if (bExiste == false)
                    {
                        aFigurasOut[iCont] = oFigura[0].ToString();
                        iCont++;
                    }
                }
                sb.Append("figuras='" + String.Join(",", aFigurasOut) + "' ");
 */
                string[] aFigurasIn = Regex.Split(dr["Figuras"].ToString(), ",");
                //List‹string› aFigurasOut = new List‹string›();
                ArrayList aFigurasOut = new ArrayList();

                foreach (string oFiguraIn in aFigurasIn)
                {
                    if (oFiguraIn == "") continue;
					if (!aFigurasOut.Contains(oFiguraIn[0].ToString())) aFigurasOut.Add(oFiguraIn[0].ToString());
                }
                string[] aFiguras = aFigurasOut.ToArray(typeof(string)) as string[];
                sb.Append("figuras='" + String.Join(",", aFiguras) + "' ");
                //sb.Append("figuras='" + dr["Figuras"].ToString() + "' ");
                sb.Append("MaxFiguras='" + dr["MaxFiguras"].ToString() + "' ");
                sb.Append("style='height:20px;cursor:default'>");

                sb.Append("<td style='padding-left:2px;'></td>");
                sb.Append("<td style='text-align:left; padding-left:5px;'>");
                if ((int)dr["Item"] == 7)
                {
                    switch (dr["t305_cualidad"].ToString())
                    {
                        case "C": sCualidad = "Contratante"; break;
                        case "J": sCualidad = "Replicado sin gestión"; break;
                        case "P": sCualidad = "Replicado con gestión"; break;
                    }
                    sb.Append("<nobr class='NBR W400' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cualidad:</label>" + sCualidad + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Denominacion"].ToString() + "</nobr></td>");
                }
                else sb.Append("<nobr class='NBR' style='width:345px;'>" + dr["Denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR' style='width:345px;'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener las figuras del usuario.", ex);
        }
    }
    private void cargarNodos()
    {
        try
        {
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr;
            dr = NODO.ObtenerNodosUsuarioEsRespDelegColab(null, (int)Session["UsuarioActual"]);
            while (dr.Read())
            {
                oLI = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
                cboCR.Items.Add(oLI);
                hdnNodos.Value += dr["identificador"].ToString() + ",";
            }

            if (hdnNodos.Value.Length != 0) hdnNodos.Value = hdnNodos.Value.Substring(0, hdnNodos.Value.Length - 1);

            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private string cargarSubnodos(string sID)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUBNODO.CatalogoPorNodo(null, short.Parse(sID), 0); //Mostrar todos todos los subnodos

            while (dr.Read())
            {
                sb.Append(dr["t304_idsubnodo"].ToString() + "##" + dr["t304_denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener los subnodos", ex);
        }
    }    
}
