using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class RECONOCIMIENTOLB
	{
		#region Constructor

        public RECONOCIMIENTOLB()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static string ObtenerCatalogo(int t685_idlineabase, bool bSoloPendiente, string t422_idmoneda)
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
			sb.Append("<table id='tblReconocimiento' style='width:940px;'>");
			sb.Append("<colgroup>");
            sb.Append("     <col style='width:160px;' />");
            sb.Append("     <col style='width:160px;' />");
            sb.Append("     <col style='width:160px;' />");
            sb.Append("     <col style='width:140px;' />");
            sb.Append("     <col style='width:90px;' />");
            sb.Append("     <col style='width:100px;' />");
            sb.Append("     <col style='width:130px;' />");
            sb.Append("</colgroup>");
            #endregion

            bool bLectura = (bool)HttpContext.Current.Session["MODOLECTURA_PROYECTOSUBNODO"];
            bool sw = false;
            SqlDataReader dr = SUPER.Capa_Datos.RECONOCIMIENTOLB.ObtenerDatosReconocimiento(null, t685_idlineabase, bSoloPendiente, t422_idmoneda);
            while (dr.Read())
            {
                if (!sw && !bLectura)
                {
                    sw = true;
                    if (dr["t301_estado"].ToString() == "C" || dr["t301_estado"].ToString() == "H")
                        bLectura = true;
                }
                sb.Append("<tr id='" + dr["t688_idreconocimiento"].ToString() + "' ");
                sb.Append("anomes='" + dr["t688_anomes_recono"].ToString() + "' ");
                if (!bLectura)
                    sb.Append("onclick='ms(this)'");
                sb.Append(">");
                sb.Append("<td><nobr class='NBR W150' onmouseover='TTip(event)'>" + dr["t328_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W150' onmouseover='TTip(event)'>" + dr["t329_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W150' onmouseover='TTip(event)'>" + dr["t688_motivo"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W130' onmouseover='TTip(event)'>" + dr["t303_denominacion_recono"].ToString() + dr["t315_denominacion_recono"].ToString() + "</nobr></td>");
                sb.Append("<td class='num'>" + ((decimal)dr["t688_importe"]).ToString("N") + "</td>");
                sb.Append("<td style='padding-left:5px'>" + dr["Mes_real"].ToString() + "</td>");
                sb.Append("<td>");
                sb.Append("<input type='checkbox' class='check' " + ((dr["t688_anomes_recono"] != DBNull.Value) ? "checked" : "") + " " + ((bLectura) ? "disabled" : " onclick='setReconocimiento(this);' ") + " style='" + ((!bLectura) ? "cursor:pointer;" : "") + "margin-left:10px;margin-right:10px;vertical-align:middle;'>");
                sb.Append("<label " + ((dr["t688_anomes_recono"] != DBNull.Value && !bLectura) ? "style='cursor:pointer;' onclick='getMes(this);'" : "") + " >" + dr["Mes_recono"].ToString() + "</label>");

                sb.Append("</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return sb.ToString();
        }

        public static void ActualizarMesReconocimiento(int t688_idreconocimiento, Nullable<int> t688_anomes_recono){
            Capa_Datos.RECONOCIMIENTOLB.ActualizarMesReconocimiento(null, t688_idreconocimiento, t688_anomes_recono);
        }


        #endregion
	}
}
