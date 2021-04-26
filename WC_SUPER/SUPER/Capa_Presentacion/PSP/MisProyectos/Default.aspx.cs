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
    public string sErrores, strTablaProys, strTablaFig;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            #region Inicializacion
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            this.lblCR.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            sErrores = "";
            //sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            #endregion
            try
            {
                string sIdUser = "", sIdFicepi="";
                #region Combo tipos de item
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
                #endregion
                #region Recojo parámetros de entrada
                if (Request.QueryString["f"] != null)//IdFicepi
                {
                    sIdFicepi = Utilidades.decodpar(Request.QueryString["f"].ToString());
                    this.hdnIdFicepi.Value = sIdFicepi;
                }
                if (Request.QueryString["u"] != null)//Idusuario
                {
                    sIdUser = Utilidades.decodpar(Request.QueryString["u"].ToString());
                    this.hdnIdUser.Value = sIdUser;
                }
                #endregion
                if (sIdUser != "" && sIdFicepi!="")
                {
                    ObtenerProfesional(int.Parse(sIdFicepi));
                    this.strTablaProys = ObtenerProyectos(sIdFicepi+"##1##1##0##0");
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de proyectos", ex);
            }
            #region registrar CallBack
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            #endregion
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; 
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("proyectos"):
                sResultado += "OK@#@" + ObtenerProyectos(aArgs[1]);
                break;
            case ("figuras"):
                sResultado += ObtenerFiguras(aArgs[1]);
                break;
            case ("getDatosPestana"):
                //sResultado += "OK@#@" + aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0://PROYECTOS
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://FIGURAS
                        sCad = ObtenerFiguras(aArgs[2]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                }
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

    private string ObtenerProyectos(string sParametros)
    {
        StringBuilder sb = new StringBuilder();
        int idFicepi;
        string sPresupuestado = "", sAbierto = "", sCerrado = "", sHistorico = "";
        try
        {
            string[] aParam = Regex.Split(sParametros, "##");
            idFicepi = int.Parse(aParam[0]);
            sPresupuestado = aParam[1];
            sAbierto = aParam[2];
            sCerrado = aParam[3];
            sHistorico = aParam[4];

            string sEstadosProy = "";
            if (sPresupuestado == "1") sEstadosProy = "P,";
            if (sAbierto == "1") sEstadosProy += "A,";
            if (sCerrado == "1") sEstadosProy += "C,";
            if (sHistorico == "1") sEstadosProy += "H,";

            sb.Append("<table id='tblProy' class='texto' style='width:760px;' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:60px' />");
            sb.Append("<col style='width:300px;' />");
            sb.Append("<col style='width:200px' />");
            sb.Append("<col style='width:70px' />");
            sb.Append("<col style='width:70px' />");//
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = SUPER.DAL.Profesional.GetProyectos(null, idFicepi, sEstadosProy);

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px' categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("moneda_proyecto='" + dr["t422_idmoneda_proyecto"].ToString() + "'>");

                sb.Append("<td></td><td></td><td></td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W300' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W200'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + Fechas.SinHora(dr["t330_falta"].ToString()) + "</td>");
                sb.Append("<td>" + Fechas.SinHora(dr["t330_fbaja"].ToString()) + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos económicos", ex);
            return "Error@#@Error al obtener los proyectos económicos";
        }
    }
    private string ObtenerFiguras(string sParametros)
    {
        StringBuilder sb = new StringBuilder();
        string sCualidad = "";
        string nUsuario = "", nFicepi = "", sTipoItem = "";
        string sPresupuestado = "", sAbierto = "", sCerrado = "", sHistorico = "";
        try
        {
            string[] aParam = Regex.Split(sParametros, "##");
            nUsuario = aParam[0];
            nFicepi = aParam[1];
            sTipoItem = aParam[2];
            sPresupuestado = aParam[3];
            sAbierto = aParam[4];
            sCerrado = aParam[5];
            sHistorico = aParam[6];

            sb.Append("<table id='tblDatos' style='width: 765px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:25px;' />");
            sb.Append("<col style='width:530px;' />");//Profesional
            sb.Append("<col style='width:210px;' />");//
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            string sEstadosProy = "";
            if (sPresupuestado == "1") sEstadosProy = "P,";
            if (sAbierto == "1") sEstadosProy += "A,";
            if (sCerrado == "1") sEstadosProy += "C,";
            if (sHistorico == "1") sEstadosProy += "H,";
            if (sEstadosProy.Length != 0) sEstadosProy = sEstadosProy.Substring(0, sEstadosProy.Length - 1);

            SqlDataReader dr = USUARIO.ObtenerFigurasUsuario(int.Parse(nUsuario), int.Parse(nFicepi), 
                                                             (sTipoItem == "") ? null : (int?)int.Parse(sTipoItem), 
                                                             sEstadosProy);
            int iPos;

            while (dr.Read())
            {
                if (
                    (!Utilidades.EstructuraActiva("SN1") && (int)dr["Item"] == 14)
                    || (!Utilidades.EstructuraActiva("SN2") && (int)dr["Item"] == 15)
                    || (!Utilidades.EstructuraActiva("SN3") && (int)dr["Item"] == 16)
                    || (!Utilidades.EstructuraActiva("SN4") && (int)dr["Item"] == 17)
                    ) continue;

                sb.Append("<tr ");
                sb.Append("item=" + dr["Item"].ToString() + " ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");

                string[] aFigurasIn = Regex.Split(dr["Figuras"].ToString(), ",");
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

                if ((int)dr["Item"] == 7)
                {
                    switch (dr["t305_cualidad"].ToString())
                    {
                        case "C": sCualidad = "Contratante"; break;
                        case "J": sCualidad = "Replicado sin gestión"; break;
                        case "P": sCualidad = "Replicado con gestión"; break;
                    }
                    string sProyecto = dr["denominacion"].ToString().Replace((char)34, (char)39);

                    iPos = sProyecto.IndexOf(" - ");

                    sb.Append("idProy='" + sProyecto.Substring(0, iPos) + "' ");
                    sb.Append("denominacion='" + sProyecto.Substring(iPos + 3) + "' ");
                    sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                    sb.Append("responsable='" + dr["responsable"].ToString().Replace((char)34, (char)39) + "' ");
                    sb.Append("nodo='" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "' ");
                    sb.Append("cliente='" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "' ");
                }

                sb.Append("style='height:20px;'>");

                sb.Append("<td style='padding-left:2px;'></td>");
                if ((int)dr["Item"] == 7)
                    sb.Append("<td style='text-align:left; padding-left:5px;'><nobr class='NBR W520' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cualidad:</label>" + sCualidad + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Denominacion"].ToString() + "</nobr></td>");
                else sb.Append("<td style='text-align:left; padding-left:5px;'><nobr class='NBR W520'>" + dr["Denominacion"].ToString() + "</nobr></td>");
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
    private void ObtenerProfesional(int idFicepi)
    {
        Recurso oRec = new Recurso();
        oRec = SUPER.Capa_Negocio.Recurso.GetDatos(idFicepi);

        string sCRP = (oRec.CRP) ? "1" : "0";
        //return "OK@#@" + sb.ToString() + "@#@" + oRec.sSexo + "@#@" + oRec.Admin + "@#@" + sCRP + "@#@" + oRec.AdminPer;
        if (oRec.Nif != "")
        {
            this.txtProfesional.Text = oRec.Nombre;
            //this.txtDenCR.Text = oRec.DesNodo;

            imgTec.Src = "~/Images/imgTecnico" + oRec.sSexo + ".gif";

            if (oRec.AdminPC != "" || oRec.AdminPer != "")
            {
                imgAdm.Src = "~/Images/imgAdministrador" + oRec.sSexo + ".gif";
                lblAdm.InnerText = (oRec.AdminPC == "A") ? "Administrador" : "Superadministrador";
                if (oRec.AdminPer == "P") lblAdm.InnerText = "Administrador de personal";
            }
            else cldAdm.Style.Add("visibility", "hidden");
            if (sCRP == "1")
            {
                imgCRP.Src = "~/Images/imgCRP" + oRec.sSexo + ".gif";
                lblCRP.InnerText = "Candidato a responsable de proyecto";
            }
            else cldCRP.Style.Add("visibility", "hidden");
        }
    }
}
