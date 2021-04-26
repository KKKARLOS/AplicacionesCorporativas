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


public partial class getNodoAcceso : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Session.Clear();
            //Session.Abandon();

            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            string sTipo = Request.QueryString["t"].ToString();
            string sUsuarioResponsable = "";
            if (Request.QueryString["idUsuariResp"]!= null)
                    sUsuarioResponsable = Request.QueryString["idUsuariResp"].ToString();
            ObtenerNodos(sTipo, sUsuarioResponsable);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerNodos(string sTipo, string idUsuariResp)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        try
        {
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:350px;' /></colgroup>");
            switch (sTipo)
            {
                case "G":
                    dr = NODO.ObtenerNodosGestor(int.Parse(idUsuariResp));
                    //Nodos en los que el usuario puede crear proyectos por ser gerente.
                    while (dr.Read())
                    {
                        sb.Append("<tr id='" + dr["IDNODO"].ToString() + "' ");
                        sb.Append("GSB='" + dr["GSB"].ToString() + "' UMC='" + dr["t303_ultcierreeco"].ToString() + "' ");
                        sb.Append("MC='" + dr["t303_modelocostes"].ToString() + "' MT='" + dr["t303_modelotarifas"].ToString() + "' ");
                        sb.Append("CNP='" + dr["t303_denominacion_CNP"].ToString() + "' ");
                        sb.Append("OBLCNP='" + dr["t303_obligatorio_CNP"].ToString() + "' ");
                        sb.Append("CSN1P='" + dr["t391_denominacion_CSN1P"].ToString() + "' ");
                        sb.Append("OBLCSN1P='" + dr["t391_obligatorio_CSN1P"].ToString() + "' ");
                        sb.Append("CSN2P='" + dr["t392_denominacion_CSN2P"].ToString() + "' ");
                        sb.Append("OBLCSN2P='" + dr["t392_obligatorio_CSN2P"].ToString() + "' ");
                        sb.Append("CSN3P='" + dr["t393_denominacion_CSN3P"].ToString() + "' ");
                        sb.Append("OBLCSN3P='" + dr["t393_obligatorio_CSN3P"].ToString() + "' ");
                        sb.Append("CSN4P='" + dr["t394_denominacion_CSN4P"].ToString() + "' ");
                        sb.Append("OBLCSN4P='" + dr["t394_obligatorio_CSN4P"].ToString() + "' ");
                        sb.Append("tipolinterna='" + dr["t303_tipolinterna"].ToString() + "' ");
                        sb.Append("tipolespecial='" + dr["t303_tipolespecial"].ToString() + "' ");
                        sb.Append("tipolproductivaSC='" + dr["t303_tipolproductivaSC"].ToString() + "' ");
                        sb.Append("idmoneda='" + dr["t422_idmoneda"].ToString() + "' ");
                        //Permitir replica con gestion
                        if ((bool)dr["t303_pgrcg"])
                            sb.Append("prcg='1' ");
                        else
                            sb.Append("prcg='0' ");
                        sb.Append("denominacion_moneda=\"" + Utilidades.escape(dr["t422_denominacion"].ToString()) + "\"");
                        sb.Append(" ondblclick='aceptarClick(this.rowIndex,1)'>");
                        sb.Append("<td>" + dr["DENOMINACION"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    dr.Close();
                    dr.Dispose();
                    break;
                case "A":
                    dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], true);
                    //Nodos administrables por el usuario
                    while (dr.Read())
                    {
                        sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ondblclick='aceptarClick(this.rowIndex,2)'>");
                        sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td></tr>");
                    }
                    dr.Close();
                    dr.Dispose();
                    break;
                case "T":
                    dr = NODO.Catalogo(false);
                    //Nodos administrables por el usuario
                    while (dr.Read())
                    {
                        sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ondblclick='aceptarClick(this.rowIndex,3)'>");
                        sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td></tr>");
                    }
                    dr.Close();
                    dr.Dispose();
                    break;
                case "V":
                    dr = NODO.UsuarioVisibilidad((int)Session["UsuarioActual"]);
                    //Nodos Accesibles por el usuario
                    while (dr.Read())
                    {
                        sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ondblclick='aceptarClick(this.rowIndex,2)'>");
                        sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td></tr>");
                    }
                    dr.Close();
                    dr.Dispose();
                    break;
                case "R"://Solo responsable de grupo funcional
                    dr = GrupoFun.NodosVisibles((int)Session["UsuarioActual"]);
                    //Nodos Accesibles por el usuario
                    while (dr.Read())
                    {
                        sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ondblclick='aceptarClick(this.rowIndex,2)'>");
                        sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td></tr>");
                    }
                    dr.Close();
                    dr.Dispose();
                    break;
            }
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de nodos.", ex);
        }
    }

}
