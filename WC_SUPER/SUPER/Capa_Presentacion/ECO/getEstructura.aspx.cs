using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class getItemEstructura : System.Web.UI.Page
{
    public string sErrores = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        int nNivel = int.Parse(Request.QueryString["nNivel"].ToString());

        try
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            obtenerEstructura(nNivel);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void obtenerEstructura(int nNivel)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        try
        {
            switch (nNivel)
            {
                case 0: //NODO
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], false);
                    break;
                case 1: //SUPERNODO1
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1);
                    dr = SUPERNODO1.CatalogoAdm();
                    break;
                case 2: //SUPERNODO2
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2);
                    dr = SUPERNODO2.CatalogoAdm();
                    break;
                case 3: //SUPERNODO3
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3);
                    dr = SUPERNODO3.CatalogoAdm();
                    break;
                case 4: //SUPERNODO4
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4);
                    dr = SUPERNODO4.CatalogoAdm();
                    break;
            }

            //sb.Append("<div style='background-image:url(../../Images/imgFT16.gif); width:396px;'>");
            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 400px;'>");
            //sb.Append("<colgroup><col style='width:377px;' /></colgroup>");
            sb.Append("<tbody>");
            string sObligatoriedad="";
            while (dr.Read())
            {
                switch (nNivel)
                {
                    case 0: //NODO
                        sObligatoriedad = ((bool)dr["t303_obligatorio_cnp"]) ? "1" : "0";
                        sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' cualificador='" + dr["t303_denominacion_cnp"].ToString() + "' obligatoriedad='" + sObligatoriedad + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' style='height:16px;'>");
                        sb.Append("<td style='padding-left:3px;'>" + dr["t303_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 1: //SUPERNODO1
                        sObligatoriedad = ((bool)dr["t391_obligatorio_CSN1P"]) ? "1" : "0";
                        sb.Append("<tr id='" + dr["t391_idsupernodo1"].ToString() + "' cualificador='" + dr["t391_denominacion_CSN1P"].ToString() + "' obligatoriedad='" + sObligatoriedad + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' style='height:16px;'>");
                        sb.Append("<td style='padding-left:3px;'>" + dr["t391_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 2: //SUPERNODO2
                        sObligatoriedad = ((bool)dr["t392_obligatorio_CSN2P"]) ? "1" : "0";
                        sb.Append("<tr id='" + dr["t392_idsupernodo2"].ToString() + "' cualificador='" + dr["t392_denominacion_CSN2P"].ToString() + "' obligatoriedad='" + sObligatoriedad + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' style='height:16px;'>");
                        sb.Append("<td style='padding-left:3px;'>" + dr["t392_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 3: //SUPERNODO3
                        sObligatoriedad = ((bool)dr["t393_obligatorio_CSN3P"]) ? "1" : "0";
                        sb.Append("<tr id='" + dr["t393_idsupernodo3"].ToString() + "' cualificador='" + dr["t393_denominacion_CSN3P"].ToString() + "' obligatoriedad='" + sObligatoriedad + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' style='height:16px;'>");
                        sb.Append("<td style='padding-left:3px;'>" + dr["t393_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 4: //SUPERNODO4
                        sObligatoriedad = ((bool)dr["t394_obligatorio_CSN4P"]) ? "1" : "0";
                        sb.Append("<tr id='" + dr["t394_idsupernodo4"].ToString() + "' cualificador='" + dr["t394_denominacion_CSN4P"].ToString() + "' obligatoriedad='" + sObligatoriedad + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' style='height:16px;'>");
                        sb.Append("<td style='padding-left:3px;'>" + dr["t394_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            //sb.Append("</div>");
            divCapa.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener los elementos de la estructura.", ex);
        }
    }

}
