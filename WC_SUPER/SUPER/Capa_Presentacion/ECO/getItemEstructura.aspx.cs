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
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        int nNivel = int.Parse(Request.QueryString["nNivel"].ToString());
        int nID = int.Parse(Request.QueryString["nIDPadre"].ToString());

        try
        {
            obtenerEstructura(nNivel, nID);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void obtenerEstructura(int nNivel, int nID)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        try
        {
            switch (nNivel)
            {
                case 1: //SUPERNODO4
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4);
                    dr = SUPERNODO4.Catalogo(null, "", true, null, null, null, null, 5, 0);
                    break;
                case 2: //SUPERNODO3
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3);
                    dr = SUPERNODO3.Catalogo(null, "", nID, true, null, null, null, null, 6, 0);
                    break;
                case 3: //SUPERNODO2
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2);
                    dr = SUPERNODO2.Catalogo(null, "", true, nID, null, null, null, null, 6, 0);
                    break;
                case 4: //SUPERNODO1
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1);
                    dr = SUPERNODO1.Catalogo(null, "", nID, true, null, null, null, null, 6, 0);
                    break;
                case 5: //NODO
                    this.Title = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    dr = NODO.CatalogoBySuperNodo1(nID);
                    break;
                case 11: //TIPOLOGIA DE PROYECTO
                    this.Title = "Tipologías de proyecto";
                    dr = TIPOLOGIAPROY.Catalogo(null, "", null, null, null, null, null, 7, 0);
                    break;
                case 12: //GRUPO DE NATURALEZA
                    this.Title = "Grupos de naturaleza";
                    dr = GRUPONAT.Catalogo(null, "", (byte)nID, null, true, 4, 0);
                    break;
                case 13: //SUBGRUPO DE NATURALEZA
                    this.Title = "Subgrupos de naturaleza";
                    dr = SUBGRUPONAT.Catalogo(null, "", nID, null, true, 4, 0);
                    break;
                case 21: //UNIDADES DE PREVENTA
                    this.Title = "Unidades de preventa";
                    dr = SUPER.DAL.PreventaUnidad.Catalogo(null, "", true);
                    break;
                case 22: //AREAS DE PREVENTA
                    this.Title = "Áreas de preventa";
                    dr = SUPER.DAL.PreventaArea.Catalogo((short)nID, null, "", true, null);
                    break;
            }

            sb.Append("<div style='background-image:url(../../Images/imgFT18.gif);width:396px'>");
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
            sb.Append("<colgroup><col style='width:396px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                switch (nNivel)
                {
                    case 1: //SUPERNODO4
                        sb.Append("<tr id='" + dr["t394_idsupernodo4"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t394_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 2: //SUPERNODO3
                        sb.Append("<tr id='" + dr["t393_idsupernodo3"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t393_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 3: //SUPERNODO2
                        sb.Append("<tr id='" + dr["t392_idsupernodo2"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t392_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 4: //SUPERNODO1
                        sb.Append("<tr id='" + dr["t391_idsupernodo1"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t391_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 5: //NODO
                        sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 11: //TIPOLOGIA DE PROYECTO
                        sb.Append("<tr id='" + dr["t320_idtipologiaproy"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t320_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 12: //GRUPO DE NATURALEZA
                        sb.Append("<tr id='" + dr["t321_idgruponat"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t321_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 13: //SUBGRUPO DE NATURALEZA
                        sb.Append("<tr id='" + dr["t322_idsubgruponat"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["t322_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 21: //UNIDAD DE PREVENTA
                        sb.Append("<tr id='" + dr["ta199_idunidadpreventa"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["ta199_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                    case 22: //AREA DE PREVENTA
                        sb.Append("<tr id='" + dr["ta200_idareapreventa"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                        sb.Append("<td>" + dr["ta200_denominacion"].ToString() + "</td>");
                        sb.Append("</tr>");
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");
            divCatalogo.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener los elementos de la estructura.", ex);
        }
    }

}
