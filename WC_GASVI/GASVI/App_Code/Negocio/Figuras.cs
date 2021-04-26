using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security; //para gestion de roles
using GASVI.DAL;
using Microsoft.JScript;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace GASVI.BLL
{
	public partial class Figuras
    {
        #region Propiedades
        
        #endregion

        public Figuras()
		{
			
		}

        public static List<ElementoLista> obtenerFiguras()
        {
            SqlDataReader dr = DAL.Figuras.CatalogoFiguras();
            List<ElementoLista> oLista = new List<ElementoLista>();

            while (dr.Read())
            {
                oLista.Add(new ElementoLista(dr["t418_idfigura"].ToString(), dr["denominacion"].ToString()));
            }
            dr.Close();
            dr.Dispose();
            return oLista;
        }

        public static string mostrarIntegrantes(string sTipo){
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblIntegrantes' class='MM W390' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:339px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.Figuras.ObtenerIntegrantes(sTipo);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("leido='0' ");
                sb.Append("onmousedown='DD(event);' ");
                switch (sTipo){
                    case "P":
                    case "A":
                    case "S":
                        sb.Append("onClick='ms(this);' ");
                        break;
                    case "T":
                        sb.Append("onClick='ms(this); mostrarTramitados(this);' ");
                        break;
                    case "L":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "N":
                        sb.Append("onClick='ms(this); mostrarSubIntegrantes(this,\"" + sTipo + "\");' ");
                        break;              
                }

                sb.Append("style='height:20px' onmouseover='TTip(event)'> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["nombre"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string mostrarIntegrantesNodos(string sIdFicepi, string sTipo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblIntegrantes2' class='W390' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col id='colHidden' style='width:1px;' />");
            sb.Append("    <col style='width:358px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.Figuras.ObtenerIntegrantesNodos(int.Parse(sIdFicepi), sTipo, "V");
            while (dr.Read())
            {
                if(sTipo != "L") sb.Append("<tr id='" + dr["identificativo"].ToString() + "' ");
                else sb.Append("<tr id='" + dr["t010_idoficina"].ToString() + "' ");
                sb.Append("bd='' ");
                if (sTipo == "L") sb.Append("of='" + dr["oficina_ficepi"].ToString() + "' ");
                sb.Append("onmouseover='TTip(event)' style='height:20px; ");
                if ((sTipo == "L" && int.Parse(dr["oficina_ficepi"].ToString()) == 0) || sTipo != "L")
                {
                    sb.Append("' onmousedown='DD(event);' ");
                    sb.Append("onClick='ms(this); PonerMM(this);' ");
                    sb.Append("class='MM' ");
                }
                else sb.Append(" background-color:#e5e5e5' ");
                sb.Append("> ");
                sb.Append("<td><img src='../../../images/imgFN.gif' /></td>");
                sb.Append("<td></td>");
                if(sTipo != "L") sb.Append("<td><nobr class='NBR W340'>" + dr["denominacion"].ToString() + "</nobr></td>");
                else sb.Append("<td><nobr class='NBR W340'>" + dr["t010_desoficina"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string mostrarTramitados(string sIdFicepi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblIntegrantes2' class='MM W390' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:15px; padding-left:2px;' />");
            sb.Append("    <col id='colHidden' style='width:20px; padding-left:2px;' />");
            sb.Append("    <col style='width:339px; padding-left:2px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.Figuras.ObtenerTramitados(int.Parse(sIdFicepi));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi_interesado"].ToString() + "' ");
                sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("onmousedown=\"DD(event);\" onClick='ms(this);' ");
                sb.Append("style='height:20px' onmouseover='TTip(event)'> ");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W340'>" + dr["nombre"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string obtenerPersonas(string sApellido1, string sApellido2, string sNombre, string sExcluidos, string sTipo)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = DAL.Figuras.CatalogoPersonasFiguras(Utilidades.unescape(sApellido1), Utilidades.unescape(sApellido2), Utilidades.unescape(sNombre), sExcluidos, false);

            switch (sTipo) { 
                case "1":
                    sb.Append("<table id='tblPersonas' class='MAM W398' mantenimiento='1'>");
                    break;
                case "2":
                    sb.Append("<table id='tblPersonas2' class='MAM W398' mantenimiento='1'>");
                    break;            
            }
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:378px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                if ((sTipo == "2" && dr["t001_tiporecurso"].ToString() == "I") || sTipo == "1")
                {
                    sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");
                    sb.Append("tipo='" + dr["t001_tiporecurso"].ToString() + "' bd='' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    sb.Append("onClick='ms(this)' ");
                    switch (sTipo)
                    {
                        case "1":
                            sb.Append("onDblClick='anadirConvocados(1);' ");
                            break;
                        case "2":
                            sb.Append("onDblClick='anadirConvocados(2);' ");
                            break;
                    }
                    sb.Append("onmouseover='TTip(event);' ");
                    sb.Append("onmousedown=\"DD(event);\"' style='height:20px'>");
                    sb.Append("<td></td>");
                    sb.Append("<td><nobr class='NBR W350'>" + Utilidades.unescape(dr["nombre"].ToString()) + "</nobr></td>");
                    sb.Append("</tr>");
                }
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string mostrarCatalogoNodos(string sTipo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblPersonas2' class='MAM W398' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:1px; padding-left:2px;' />");
            sb.Append("    <col style='width:397px; padding-left:3px;' />");
            sb.Append("</colgroup>");

            SqlDataReader dr = DAL.Figuras.ObtenerCatalogoNodos(sTipo);
            if (dr != null)
            {
                while (dr.Read())
                {
                    if (sTipo != "L") sb.Append("<tr id='" + dr["identificativo"].ToString() + "' ");
                    else sb.Append("<tr id='" + dr["t010_idoficina"].ToString() + "' ");
                    sb.Append("bd='' ");
                    sb.Append("onmousedown=\"DD(event);\" ");
                    sb.Append("onClick='ms(this);' ");
                    sb.Append("onDblClick='anadirConvocados(3);' ");
                    sb.Append("style='height:20px' onmouseover='TTip(event)'> ");
                    sb.Append("<td></td>");
                    if (sTipo != "L") sb.Append("<td><nobr class='NBR W340'>" + dr["denominacion"].ToString() + "</nobr></td>");
                    else sb.Append("<td><nobr class='NBR W340'>" + dr["t010_desoficina"].ToString() + "</nobr></td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        public static void Grabar(string sDatos)
        {
            string sResul = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                if (sDatos != "")
                {
                    //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                    string[] aDatosGrabar = Regex.Split(sDatos, "#sFin#");
                    for (int i = 0; i <= aDatosGrabar.Length - 1; i++)
                    {
                        string[] aElem = Regex.Split(aDatosGrabar[i], "#sCad#");
                        switch (aElem[1])
                        {
                            case "I":
                                switch (aElem[0]) {
                                    case "P":
                                    case "A":
                                    case "L":
                                    case "S":
                                    //case "T":
                                        DAL.Figuras.InsertIntegranteFigura(tr, aElem[0], int.Parse(aElem[2]));
                                        break;
                                }
                                break;
                            case "D":
                                DAL.Figuras.DeleteIntegranteFigura(tr, aElem[0], int.Parse(aElem[2]));
                                break;
                        }
                        switch (aElem[3])
                        {
                            case "I":
                                DAL.Figuras.InsertElementoFigura(tr, aElem[0], int.Parse(aElem[2]), int.Parse(aElem[4]), "V");
                                break;
                            case "D":
                                DAL.Figuras.DeleteElementoFigura(tr, aElem[0], int.Parse(aElem[2]), int.Parse(aElem[4]), "V");
                                break;
                        }
                    }
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar las figuras.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
        }

	}
}