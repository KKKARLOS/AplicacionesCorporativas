using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using GASVI.DAL;
using System.Text;

namespace GASVI.BLL
{
    public partial class NuevoBonoTransporte
    {
        #region Propiedades
        
        #endregion

        public NuevoBonoTransporte()
		{
			
		}

        public static string obtenerDatosIniciales(string sUsuario, string sFecha)
        {
            if (sFecha == "")
            {
                //sFecha = (DateTime.Now.Year * 100 + DateTime.Now.Month - 1).ToString();
                sFecha = Fechas.AddAnnomes(Fechas.FechaAAnnomes(DateTime.Today), -1).ToString();
            }
            SqlDataReader dr = DAL.BonoTransporte.CatalogoBonosUsuarioProyecto(int.Parse(sUsuario),
                                                                               int.Parse(sFecha));
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            while (dr.Read())
            {
                sb.Append(dr["t655_idBono"].ToString() + "#sCad#");
                sb.Append(dr["t655_denominacion"].ToString() + "#sCad#");
                sb.Append(dr["t656_importe"].ToString() + "#sCad#");
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "#sCad#");
                sb.Append(dr["t314_idusuario"].ToString() + "#sCad#");
                sb.Append(dr["t422_idmoneda"].ToString() + "#sCad#");
                sb.Append(dr["t422_denominacion"].ToString() + "#sCad#");
                sb.Append(((int)dr["t301_idproyecto"]).ToString("#,###") + "#sCad#");                
                sb.Append(dr["t301_denominacion"].ToString() + "#sFin#");                

            }
            dr.Close();
            dr.Dispose();
            string sResul = sb.ToString();
            if (sResul != "") sResul = sResul.Substring(0, sResul.Length - 6);
            return sResul;        
        }

        public static string obtenerBonos(string sUsuario, string sFecha)
        {
            if (sFecha == "") {
                //sFecha = (DateTime.Now.Year * 100 + DateTime.Now.Month -1).ToString();
                sFecha = Fechas.AddAnnomes(Fechas.FechaAAnnomes(DateTime.Today), -1).ToString();
            }
            SqlDataReader dr = DAL.BonoTransporte.CatalogoBonosUsuarioProyecto(int.Parse(sUsuario),
                                                                               int.Parse(sFecha));
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='MA' style='width:750px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:60px; text-align:right; padding-right:10px;' />");
            sb.Append("    <col style='width:200px; padding-left:2px;' />");
            sb.Append("    <col style='width:50px; padding-left:2px;' />");
            sb.Append("    <col style='width:70px; text-align:right; padding-right:14px;' />");
            sb.Append("    <col style='width:190px; padding-left:2px;' />");
            sb.Append("    <col style='width:180px; padding-left:2px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t655_idBono"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append("ondblclick='aceptarClick(this)' ");
                sb.Append("idProyecto='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idUsuario='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("idMoneda='" + dr["t422_idmoneda"].ToString() + "' ");
                sb.Append("desMoneda=\"" + Utilidades.escape(dr["t422_denominacion"].ToString()) + "\" ");
                sb.Append(">");
                sb.Append("<td>" + int.Parse(dr["t655_idBono"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W190'>" + dr["t655_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                sb.Append("<td>" + decimal.Parse(dr["t656_importe"].ToString()).ToString("N") + "</td>");
                sb.Append("<td><nobr class='NBR W180'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "<nobr></td>");
                sb.Append("<td><nobr class='NBR W170'>" + dr["t302_denominacion"].ToString() + "<nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();  
        }

        public static string obtenerCabeceraGVBono(string sIdreferencia, string sInteresado, string anomes)
        {
            SqlDataReader dr = DAL.BonoTransporte.CatalogoCabeceraGVBono(int.Parse(sInteresado), int.Parse(sIdreferencia), int.Parse(anomes));
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblCabeceraGVBono' style='width:280px;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:180px; '/>");
            sb.Append("     <col style='width:40px;'/>");
            sb.Append("     <col style='width:60px;'/>");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t655_idBono"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                sb.Append("idProyecto='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                if (dr["t301_denominacion"].ToString() != "")
                {
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle;' />&nbsp;&nbsp;Informaci&oacute;n] ");
                    sb.Append("body=[<label style='width:70px;'>Solicitud:</label>" + ((int)dr["t420_idreferencia"]).ToString("#,###") + "<br>");
                    sb.Append("<label style='width:70px;'>Proyecto:</label>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + " - " + Utilidades.CadenaParaTooltipExtendido(dr["t301_denominacion"].ToString()) + "<br> ");
                    sb.Append("<label style='width:70px;'>Estado:</label>" + Utilidades.CadenaParaTooltipExtendido(dr["t431_denominacion"].ToString()) + "<br>");
                    sb.Append("<label style='width:70px;'>Bono:</label>" + ((int)dr["t655_idBono"]).ToString("#,###") + " - " + Utilidades.CadenaParaTooltipExtendido(dr["t655_denominacion"].ToString()) + "] ");
                    sb.Append("hideselects=[off]\" ");
                }
                sb.Append(">");
                sb.Append("<td style='padding-left:3px;'>" + dr["t420_concepto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t422_idmoneda"].ToString() + "</td>");
                sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["totalviaje"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            return sb.ToString();
        }

        public static string ObtenerHistorial(string sIdreferencia)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatosHistorial' style='width:610px;'>");
            sb.Append("<colgroup>");
            sb.Append(" <col style='width:105px; padding-left:3px;' />");
            sb.Append(" <col style='width:105px;' />");
            sb.Append(" <col style='width:400px; padding-right:3px;' />");
            sb.Append("</colgroup>");

            if (sIdreferencia != "")
            {
                SqlDataReader dr = DAL.CABECERAGV.ObtenerHistorial(null, int.Parse(sIdreferencia));
                int i = 0;
                string sFecha = "";
                while (dr.Read())
                {
                    sb.Append("<tr style='vertical-align: text-top;' ");
                    if (i % 2 == 0) sb.Append("class='FA' ");
                    else sb.Append("class='FB' ");
                    //color pijama
                    sb.Append(">");
                    if (dr["t431_denominacion"] != DBNull.Value)
                        sb.Append("<td>" + dr["t431_denominacion"].ToString() + "</td>");
                    else
                        sb.Append("<td style='vertical-align: text-top;'><img src='../../Images/imgEmail16.png'></td>");
                    sFecha = ((DateTime)dr["t659_fecha"]).ToString();
                    sb.Append("<td>" + sFecha.Substring(0, sFecha.Length - 3) + "</td>");

                    sb.Append("<td>" + dr["Profesional"].ToString());
                    if (dr["t659_motivo"].ToString() != "")
                    {
                        sb.Append("<br><span style='width:350px; margin-left:30px;'>" + dr["t659_motivo"].ToString() + "</span>");
                    }
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    i++;
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        public static string TramitarBono(string sConcepto, string sAnoMes, string sIdBono, string sIdProyecto, string sIdUsuario,
                                            string sComentario, string sAnotaciones, string sImporte, string sIdTerritorio, 
                                            string sIdReferencia, string sMoneda, string sOficina)
        {
            string sResul = "", sValorInsertado = "";

            SqlConnection oConn = null;
            SqlTransaction tr = null;
            int nAux = 0;
            bool bErrorControlado = false;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {
                #region Tramitar Bonos
                if (sIdReferencia == "")
                {
                    nAux = DAL.BonoTransporte.InsertCabeceraGVBono(tr,
                                                                    sConcepto,
                                                                    int.Parse(HttpContext.Current.Session["GVT_IDFICEPI"].ToString()),
                                                                    int.Parse(sIdUsuario),
                                                                    int.Parse(sIdProyecto),
                                                                    Utilidades.unescape(sComentario),
                                                                    Utilidades.unescape(sAnotaciones),
                                                                    int.Parse(HttpContext.Current.Session["GVT_IDEMPRESA"].ToString()),
                                                                    int.Parse(sIdBono),
                                                                    float.Parse(sImporte),
                                                                    int.Parse(sAnoMes),
                                                                    int.Parse(sIdTerritorio),
                                                                    sMoneda,
                                                                    short.Parse(sOficina)
                                                                    );
                    sValorInsertado = nAux.ToString();
                }
                else {
                    DAL.BonoTransporte.UpdateCabeceraGVBono(tr,
                                                                    int.Parse(sIdReferencia),
                                                                    sConcepto,
                                                                    int.Parse(HttpContext.Current.Session["GVT_IDFICEPI"].ToString()),
                                                                    int.Parse(sIdUsuario),
                                                                    int.Parse(sIdProyecto),
                                                                    Utilidades.unescape(sComentario),
                                                                    Utilidades.unescape(sAnotaciones),
                                                                    int.Parse(HttpContext.Current.Session["GVT_IDEMPRESA"].ToString()),
                                                                    int.Parse(sIdBono),
                                                                    float.Parse(sImporte),
                                                                    int.Parse(sAnoMes),
                                                                    int.Parse(sIdTerritorio),
                                                                    sMoneda,
                                                                    short.Parse(sOficina)
                                                                    );
                    sValorInsertado = sIdReferencia;
                }
                #endregion

                //string sCentroCoste = DAL.CABECERAGV.ObtenerCentroCoste(tr, int.Parse(sValorInsertado));
                //if (sCentroCoste == "")
                //{
                //    sResul = "Operación denegada.\n\nNo se ha podido determinar el centro de coste a asociar a la solicitud.";
                //    bErrorControlado = true;
                //    throw (new Exception(sResul));
                //}
                //else
                //{
                //    DAL.CABECERAGV.UpdateCentroCoste(tr, int.Parse(sValorInsertado), sCentroCoste);
                //}

                sValorInsertado += "@#@" + ObtenerHistorial(nAux.ToString());
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                //sResul = Errores.mostrarError("Error al grabar el bono transporte.", ex);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al grabar el bono transporte.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    if (bErrorControlado) sResul = "ErrorControlado##EC##" + sResul;
                    throw (new Exception(sResul));
                }
            }
            return sValorInsertado;
        }

	}
}