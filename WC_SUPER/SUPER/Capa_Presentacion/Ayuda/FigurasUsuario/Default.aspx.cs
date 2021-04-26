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
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Relación de responsabilidades y figuras";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.Modulo = "NEUTRAL";
                try
                {
                    hdnIdSUPER.Text = Session["UsuarioActual"].ToString();
                    if (Session["IDFICEPI_ENTRADA"].ToString() != Session["IDFICEPI_PC_ACTUAL"].ToString() ||
                        Session["IDFICEPI_ENTRADA"].ToString() != Session["IDFICEPI_CVT_ACTUAL"].ToString())
                    {
                        if (Session["IDFICEPI_ENTRADA"].ToString() != Session["IDFICEPI_PC_ACTUAL"].ToString())
                            hdnIdFICEPI.Text = Session["IDFICEPI_PC_ACTUAL"].ToString();
                        else
                            hdnIdFICEPI.Text = Session["IDFICEPI_CVT_ACTUAL"].ToString();
                    }
                    else
                        hdnIdFICEPI.Text = Session["IDFICEPI_ENTRADA"].ToString();

                    txtProfesional.Text = Session["APELLIDO1"].ToString()+" "+Session["APELLIDO2"].ToString()+", "+Session["NOMBRE"].ToString();
                    
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
                    string sIdUser = "0";
                    if (Session["UsuarioActual"].ToString() == "0")
                        sIdUser = Session["UsuarioActual_CVT"].ToString();
                    else
                        sIdUser = Session["UsuarioActual"].ToString();
                    string strTabla = obtenerDatos(sIdUser, hdnIdFICEPI.Text, "", "1", "1", "0", "0");

                    string[] aTabla = Regex.Split(strTabla, "@#@");
                    if (aTabla[0] == "OK"){
                        this.strTablaHTML = aTabla[1];

                        imgTec.Src = "~/Images/imgTecnico" + aTabla[2] + ".gif";

                        if (aTabla[3] != "" || aTabla[5] != "" || aTabla[6] != "")
                        {
                            imgAdm.Src = "~/Images/imgAdministrador" + aTabla[2] + ".gif";
                            
                            if (aTabla[5] == "P") lblAdm.InnerText = "Administrador de personal";
                            else
                            {
                                if (aTabla[3] != "" && aTabla[6] != "")
                                    lblAdm.InnerText = "Administrador PC y CVT";
                                else
                                {
                                    if (aTabla[6] != "")
                                        lblAdm.InnerText = "Administrador CVT";
                                    else
                                        lblAdm.InnerText = (aTabla[3] == "A") ? "Administrador PC" : "Superadministrador PC";
                                }
                            }
                        }
                        else cldAdm.Style.Add("visibility","hidden");
                        if (aTabla[4] == "1")
                        {
                            imgCRP.Src = "~/Images/imgCRP" + aTabla[2] + ".gif";
                            lblCRP.InnerText = "Candidato a responsable de proyecto";
                        }
                        else cldCRP.Style.Add("visibility", "hidden");
                    }
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
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
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
    private string obtenerDatos(string nUsuario, string nFicepi, string sTipoItem, string sPresupuestado, string sAbierto, string sCerrado, string sHistorico)
    {
        StringBuilder sb = new StringBuilder();
        string sCualidad = "";

        try
        {
            sb.Append("<table id='tblDatos' style='width: 820px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:595px;' />");//Profesional
            sb.Append("<col style='width:200px;' />");//
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            string sEstadosProy = "";
            if (sPresupuestado == "1") sEstadosProy="P,";
            if (sAbierto == "1") sEstadosProy += "A,";
            if (sCerrado == "1") sEstadosProy += "C,";
            if (sHistorico == "1") sEstadosProy += "H,";
            if (sEstadosProy.Length != 0) sEstadosProy = sEstadosProy.Substring(0, sEstadosProy.Length - 1);

            SqlDataReader dr = USUARIO.ObtenerFigurasUsuario(int.Parse(nUsuario), int.Parse(nFicepi), (sTipoItem == "") ? null : (int?)int.Parse(sTipoItem), sEstadosProy);
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
                    sb.Append("<td style='text-align:left; padding-left:5px;'><nobr class='NBR W575' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cualidad:</label>" + sCualidad + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Denominacion"].ToString() + "</nobr></td>");
                else sb.Append("<td style='text-align:left; padding-left:5px;'><nobr class='NBR W575'>" + dr["Denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            Recurso oRec = new Recurso();
            //oRec.ObtenerRecurso(Session["IDRED"].ToString(), int.Parse(nUsuario));
            oRec.ObtenerRecurso(null, int.Parse(nUsuario));

            string sCRP = (oRec.CRP)?"1":"0";
            return "OK@#@" + sb.ToString() + "@#@" + oRec.sSexo + "@#@" + oRec.AdminPC + "@#@" + sCRP + "@#@" + oRec.AdminPer + "@#@" + oRec.AdminCVT;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las figuras del usuario.", ex);
        }
    }
    
}
