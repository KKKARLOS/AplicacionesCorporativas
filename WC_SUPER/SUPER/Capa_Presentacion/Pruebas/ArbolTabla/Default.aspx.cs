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
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Pruebas_ArbolTabla_Default : System.Web.UI.Page
{
    public int nNE = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
//        Master.nBotonera = 32;
//        Master.nResolucion = 1280;
        Master.bFuncionesLocales = true;
        Master.TituloPagina = "Prueba de árbol de estructura";

        try
        {
            GenerarArbol();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    private void GenerarArbol()
    {
        SqlDataReader dr = Estructura.GetEstructuraOrganizativa(false);
        StringBuilder sb = new StringBuilder();

        string sColor = "black";
        nNE = int.Parse(Request.QueryString["nNE"].ToString());

        sb.Append("<TABLE class=texto id=tblDatos style='WIDTH: 500px; BORDER-COLLAPSE: collapse' cellspacing=0 cellpadding=0 border=0>");
        while (dr.Read())
        {
            sColor = "black";
            if (!(bool)dr["ESTADO"]) sColor = "gray";
            if ((int)dr["INDENTACION"] <= nNE)
            {
                sb.Append("<tr class='MA' id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "' ");
                sb.Append(" style='color:" + sColor + "; DISPLAY:block; HEIGHT:20px; vertical-align:middle;' nivel='" + dr["INDENTACION"].ToString() + "' ondblclick='mdn(this)'>");

                if ((int)dr["INDENTACION"] < 5)
                {
                    if ((int)dr["INDENTACION"] < nNE) sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../images/minus.gif' style='cursor:pointer;'>");
                    else sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                }
                else sb.Append("<td><IMG class='N" + dr["INDENTACION"].ToString() + "' src='../../../images/imgSeparador.gif'>");
            }
            else
            {
                sb.Append("<tr class='MA' id='" + dr["SN4"].ToString() + "-" + dr["SN3"].ToString() + "-" + dr["SN2"].ToString() + "-" + dr["SN1"].ToString() + "-" + dr["NODO"].ToString() + "' ");
                sb.Append(" style='color:" + sColor + ";DISPLAY:none; HEIGHT:20px; vertical-align:middle;' nivel='" + dr["INDENTACION"].ToString() + "' ondblclick='mdn(this)'>");
                sb.Append("<td>");
                if ((int)dr["INDENTACION"] < 5) sb.Append("<IMG class='N" + dr["INDENTACION"].ToString() + "' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                else sb.Append("<IMG class='N" + dr["INDENTACION"].ToString() + "' src='../../../images/imgSeparador.gif'>");
            }
            if ((int)dr["INDENTACION"] < 5) sb.Append("<IMG src='../../../images/imgSN" + dr["INDENTACION"].ToString() + ".gif' style='margin-left:3px;margin-right:3px;'>");
            else sb.Append("<IMG src='../../../images/imgNodo.gif' style='margin-left:3px;margin-right:3px;'>");

            sb.Append(dr["DENOMINACION"].ToString() + "</td>");
            sb.Append("</tr>");

            //if (!(bool)dr["ESTADO"])
            //{
            //    oNodoAux.DefaultStyle = css;
            //    oNodoAux.HoverStyle = css2;
            //}
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</table>");

        divCatalogo.InnerHtml = sb.ToString();
    }
}
