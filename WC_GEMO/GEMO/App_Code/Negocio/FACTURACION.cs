using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using GEMO.DAL;

/// <summary>
/// Summary description for FACTURACION
/// </summary>
namespace GEMO.BLL
{
    public class FACTURACION
    {
        // prueba
        
        public static string Control(string sFileName)
        {
            string sResul = "";
            string sFileNamePath = "";

            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
                sFileNamePath = @"\" + ConfigurationManager.AppSettings["REPOSITORIO_FRAS_DES"] + sFileName;
            else
                sFileNamePath = @"\" + ConfigurationManager.AppSettings["REPOSITORIO_FRAS_EXP"] + sFileName;
            try
            {
                SqlDataReader dr;

                dr = GEMO.DAL.FACTURACION.Control
                                  (
                                  null, //tr,
                                  sFileName,
                                  sFileNamePath
                                  );
                dr.Read();
                if (int.Parse(dr["lineas"].ToString())>0) sResul = "Revise la cronología. Este fichero de facturación ya se ha cargado con anterioridad";
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                sResul = Errores.mostrarError("Error al controlar el fichero de carga de facturación.", ex);
            }
            finally
            {
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return sResul;
        }
        public static string Fichero(string sFileName)
        {
            string sResul = "";
            string sFileNamePath = "";

            //SqlConnection oConn = null;
            //SqlTransaction tr = null;

            //#region abrir conexión y transacción
            //try
            //{
            //    oConn = Conexion.Abrir();
            //    tr = Conexion.AbrirTransaccion(oConn);
            //}
            //catch (Exception ex)
            //{
            //    if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            //    throw (new Exception("Error al abrir la conexión."));
            //}
            //#endregion

            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
                sFileNamePath = @"\" + ConfigurationManager.AppSettings["REPOSITORIO_FRAS_DES"] + sFileName;
            else
                sFileNamePath = @"\" + ConfigurationManager.AppSettings["REPOSITORIO_FRAS_EXP"] + sFileName;

            //sFichero = @"\\do15526\FACTURACION\SB100782.MDB";
            try
            {
                GEMO.DAL.FACTURACION.Fichero
                                  (
                                  null, //tr,
                                  sFileName,
                                  sFileNamePath
                                  );
                //Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                //Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("Error al grabar la facturación.", ex);
            }
            finally
            {
                //Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return sResul;
        }
        public static string Mail(DateTime dFecha)
        {
            string sResul = "";
            try
            {
                GEMO.DAL.FACTURACION.Mail
                                  (
                                    null, //tr,
                                    dFecha
                                  );
            }
            catch (Exception ex)
            {
                sResul = Errores.mostrarError("Error al grabar la facturación.", ex);
            }
            finally
            {
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return sResul;
        }
        public static bool   Correo(string sFileName)
        {
            ArrayList aListCorreo = new ArrayList();
            string sTO;
            string sAsunto = "Facturación.";
            string sTexto = "<BR>GEMO le informa que " + HttpContext.Current.Session["NOMBRE"] + ' ' + HttpContext.Current.Session["APELLIDO1"] + ' ' + HttpContext.Current.Session["APELLIDO2"] + ", ha subido al servidor el fichero de facturación: " + sFileName + "</br>";

            SqlDataReader dr = GEMO.DAL.LINEA.Controladores();
            bool bControladores = false;
            while (dr.Read())
            {
                if (dr["T001_FACTURACION"].ToString() != "S") continue;
                sTO = dr["T001_CODRED"].ToString();
                string[] aMail = { sAsunto, sTexto, sTO};
                aListCorreo.Add(aMail);
                bControladores = true;
            }
            if (bControladores)
            {
                GEMO.BLL.Correo.EnviarCorreos(aListCorreo);                        
            }
            return (bControladores);
        }
        public static string ComunicacionesMail(DateTime dFecha)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList aListCorreo = new ArrayList();
            string sTO;
            string sAsunto = "Factura informativa consumo de móviles.";
            int iAnoMes = int.Parse(dFecha.Year.ToString())* 100 + int.Parse(dFecha.Month.ToString());

            SqlDataReader drResp = GEMO.DAL.LINEA.DestinatariosFactura();
            SqlDataReader drLin;

            string sCabecera = "";
            string sCuerpo = "";

            bool bEnviar = false;

            while (drResp.Read())
            {
                sb.Length=0;
                sb.Append("<table id='tblCatalogo' class='texto' align='center' style='WIDTH:800px; table-layout:fixed;  border-collapse:collapse;' border='0' cellspacing='0' cellpadding='0'>");
				sb.Append("<tr>");
				sb.Append("<td>");
                sb.Append("<BR>GEMO le informa sobre el consumo realizado correspondiente a " + Fechas.AnnomesAFechaDescLarga(iAnoMes) + ".</br></br>");

                sb.Append(GEMO.BLL.Correo.CabeceraHtmlMail("MIS LINEAS"));

				sb.Append("</td>");
				sb.Append("</tr>");
				
				sb.Append("<tr>");
                sb.Append("<td></br>");

                drLin = GEMO.DAL.LINEA.ResponsablesLineasFactura(int.Parse(drResp["t001_idficepi"].ToString()), dFecha);
				      
                while (drLin.Read())
                {

                    sCabecera = @"&nbsp;<label style='width:100px' class='negri'>Nº de línea:&nbsp;&nbsp;</label><label>" + drLin["t708_numlinea"].ToString() + "</label><br>";
                    sCabecera += @"&nbsp;<label style='width:100px' class='negri'>Nº de extensión:&nbsp;&nbsp;</label><label>" + drLin["t708_numext"].ToString() + "</label><br>";

                    sCuerpo = ConsumosLineaMesPropiasMail(long.Parse(drLin["t708_numlinea"].ToString()), dFecha);
                    if (sCuerpo=="") continue;
                    else 
                    {
                        sb.Append(sCabecera + sCuerpo);
                        bEnviar = true;
                    }
                }
                if (!bEnviar) continue;

				sb.Append("</td>");
				sb.Append("</tr>");

                sCuerpo = GEMO.BLL.FACTURACION.ConsumosLineaMesColaboradoresMail(int.Parse(drResp["t001_idficepi"].ToString()), dFecha);
                
                if (sCuerpo!="")
                {
                    sb.Append("<tr>");
                    sb.Append("<td></br>");

                    sb.Append(GEMO.BLL.Correo.CabeceraHtmlMail("MI EQUIPO"));
                    sb.Append("</br>");
                    sb.Append(sCuerpo);

                    sb.Append("</br></td>");
                    sb.Append("</tr>");

                    sb.Append(@"<tr><td><br><br>Para acceder a GEMO pinche <a href='http://gemo.intranet.ibermatica:81/default.aspx'>aquí</a> (Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</td></tr>");
                }
             
				sb.Append("</table>");

                if (bEnviar)
                {
                    sTO = drResp["T001_CODRED"].ToString();
                    string[] aMail = { sAsunto, sb.ToString(), sTO };
                    aListCorreo.Add(aMail);
                }
                
                bEnviar = false;

                drLin.Close();
                drLin.Dispose();
            }

            GEMO.BLL.Correo.EnviarCorreos(aListCorreo);                        
            return "";
        }
        public static string ComunicacionesMailV2(DateTime dFecha, string sFechaHoraEnvio)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList aListCorreo = new ArrayList();
            string sTO;
            string sAsunto = "Factura informativa consumo de móviles.";
            int iAnoMes = int.Parse(dFecha.Year.ToString()) * 100 + int.Parse(dFecha.Month.ToString());

            SqlDataReader drResp = GEMO.DAL.LINEA.DestinatariosFactura();
            SqlDataReader drLin;

            //string sCabecera = "";
            string sCuerpo = "";
            string sDetalle = "";

            bool bMisLineas = false;
            bool bMisColaboradores = false;

            while (drResp.Read())
            {
                sb.Length = 0;
                sb.Append("<table id='tblCatalogo' class='texto' align='left' style='WIDTH:800px; table-layout:fixed; border-collapse: collapse;' border='0' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr>");
                sb.Append("<td>");
                //sb.Append("GEMO le informa sobre el consumo realizado correspondiente a " + Fechas.AnnomesAFechaDescLarga(iAnoMes) + ".</br></br>");
                DateTime dFechPeriodo = dFecha.AddMonths(-2);
                int iAnoMesP1 = int.Parse(dFechPeriodo.Year.ToString()) * 100 + int.Parse(dFechPeriodo.Month.ToString());
                dFechPeriodo = dFecha.AddMonths(-1);
                int iAnoMesP2 = int.Parse(dFechPeriodo.Year.ToString()) * 100 + int.Parse(dFechPeriodo.Month.ToString());

                sb.Append(drResp["T001_NOMBRE"].ToString() + " " + drResp["T001_APELLIDO1"].ToString() + " " + drResp["T001_APELLIDO2"].ToString() + ", te informamos sobre el consumo correspondiente al periodo: 18 de " + Fechas.AnnomesAFechaDescLarga(iAnoMesP1).ToLower() + " al 17 de " + Fechas.AnnomesAFechaDescLarga(iAnoMesP2).ToLower() + ".</br></br>");

                sCuerpo = "";
                sDetalle = "";

                //sb.Append(GEMO.BLL.Correo.CabeceraHtmlMail("MIS LINEAS"));
                //sb.Append("</br>");
                //sb.Append(GEMO.BLL.Correo.CabeceraHtmlPropias());

                sCuerpo = GEMO.BLL.Correo.CabeceraHtmlMail("MIS LÍNEAS");
                sCuerpo += "</br>";
                sCuerpo += GEMO.BLL.Correo.CabeceraHtmlPropias();

                bMisLineas = false;
                sDetalle = "";

                drLin = GEMO.DAL.LINEA.ResponsablesLineasFactura(int.Parse(drResp["t001_idficepi"].ToString()), dFecha);
                //drLin = GEMO.DAL.LINEA.ResponsablesLineasFactura(int.Parse(drResp["t001_idficepi"].ToString()));

                while (drLin.Read())
                {
                    string sColor = "";
                    if (drLin["t001_responsable"].ToString() == drResp["t001_idficepi"].ToString() && drLin["t001_responsable"].ToString() != drLin["t001_beneficiario"].ToString()) 
                        sColor = ";color:blue";

                    string sBenefiDpto = "";
                    if (drResp["t001_idficepi"].ToString() == drLin["t001_beneficiario"].ToString())
                    {
                        sBenefiDpto = "";
                    }
                    else
                    {
                        if (drLin["t001_beneficiario"].ToString() == "0") sBenefiDpto = drLin["departamento"].ToString();
                        else sBenefiDpto = drLin["beneficiario"].ToString();
                    }
                    sDetalle = ConsumosLineaMesPropiasMailV2(long.Parse(drLin["t708_numlinea"].ToString()), dFecha, long.Parse(drLin["t708_numlinea"].ToString()), int.Parse(drLin["t708_numext"].ToString()), sColor, sBenefiDpto);
                    if (sDetalle == "") continue;
                    else
                    {
                        //sb.Append(sCuerpo);
                        sCuerpo += sDetalle;
                        bMisLineas = true;
                    }
                }

                //  if (!bEnviar) continue;

                //  Cerrar cuerpo

                //sb.Append("<tr style='height:5px'>");

                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");

                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelb'></td>");
                //sb.Append("<td class='bordelrb'></td>");

                //sb.Append("</tr>");

                //sb.Append("</table>");
                //sb.Append("</td>");
                //sb.Append("</tr>");


                sCuerpo += "<tr style='height:5px'>";

                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                //sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelb'></td>";
                sCuerpo += "<td class='bordelrb'></td>";

                sCuerpo += "</tr>";

                sCuerpo += "</table>";
                sCuerpo += "</td>";
                sCuerpo += "</tr>";

                if (bMisLineas) sb.Append(sCuerpo);
                sCuerpo = GEMO.BLL.FACTURACION.ConsumosLineaMesColaboradoresMailV2(int.Parse(drResp["t001_idficepi"].ToString()), dFecha);

                if (sCuerpo != "")
                {
                    bMisColaboradores = true;
                    sb.Append("<tr>");
                    sb.Append("<td><br>");

                    sb.Append(GEMO.BLL.Correo.CabeceraHtmlMail("MI EQUIPO"));
                    sb.Append("</br>");
                    sb.Append(GEMO.BLL.Correo.CabeceraHtmlCola());
                    sb.Append(sCuerpo);

                    sb.Append("<tr style='height:5px'>");

                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelb'></td>");
                    //sb.Append("<td class='bordelb'></td>");

                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelb'></td>");
                    sb.Append("<td class='bordelrb'></td>");

                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    //sb.Append(@"<tr><td><br><br>Para acceder a GEMO pinche <a href='http://gemo.intranet.ibermatica:81/default.aspx'>aquí</a> (Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</td></tr>");
                }
                else
                {
                    bMisColaboradores = false;
                }
                sb.Append("</table>");

                if (bMisLineas || bMisColaboradores)
                {
                    sTO = drResp["T001_CODRED"].ToString();
                    string[] aMail = { sAsunto, sb.ToString(), sTO };
                    aListCorreo.Add(aMail);
                }


                drLin.Close();
                drLin.Dispose();
            }

            //GEMO.BLL.Correo.EnviarCorreos(aListCorreo);
            GEMO.BLL.Correo.EnviarCorreosFull(aListCorreo, sFechaHoraEnvio,"");
            return "";
        }
        public static string ConsumosLineaMesColaboradoresMailV2(int iIdFicepi, DateTime dFecha)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.FACTURACION.ConsumosLineaMesColaboradoresV2(iIdFicepi, dFecha);

            bool bEnviar = false;
            decimal dTotal = 0;
            decimal dMovilNac = 0;
            decimal dMovilIberm = 0;
            decimal dTraNaFi = 0;
            decimal dMenNac = 0;
            decimal dTraInterna = 0;
            decimal dDatos = 0;
            decimal dResto = 0;
            decimal dMega = 0;

            long inum_linea = 0;
            int inum_ext = 0;
            string sRespon = "";
            string sUsu = "";
            string sColor = "";

            while (dr.Read())
            {
                if (sRespon=="")
                {
                    sRespon = dr["responsable"].ToString();

                    sUsu = (dr["beneficiario"].ToString()=="")? dr["responsable"].ToString():dr["beneficiario"].ToString();
                    sColor = (dr["beneficiario"].ToString() == "") ? ";color:blue" : "";
                    inum_linea = long.Parse(dr["RES_NU_TELEFONO"].ToString());
                    inum_ext = int.Parse(dr["RES_NU_EXTENSION"].ToString());
                }

                if ((inum_linea != long.Parse(dr["RES_NU_TELEFONO"].ToString())) 
                    ||   (inum_ext != int.Parse(dr["RES_NU_EXTENSION"].ToString()))
                    //||   (sRespon != dr["responsable"].ToString())
                    )
                {
                    sb.Append("<tr id='" + inum_linea.ToString() + "' ");
                    sb.Append("style='height:18px" + sColor + "'>");
                    sb.Append("<td style='width:260px;text-align:left;padding-left:4px' class='bordel'>" + sUsu + "</td>");
                    sb.Append("<td style='width:65px;text-align:right;padding-right:3px' class='bordel'>" + inum_linea.ToString() + "</td>");
                    sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordel'>" + inum_ext.ToString() + "</td>");
                    sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'><label style='width:46px;padding-right:4px;' class='negri'>" + dTotal.ToString("N") + "&nbsp;€</label></td>");

                    if (dMovilNac != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMovilNac.ToString("N") + "</td>");
                    else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");
                    // 25/04/2017 Por petición de Víctor quito la columna Móvil Ibermática
                    //if (dMovilIberm != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMovilIberm.ToString("N") + "</td>");
                    //else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");

                    if (dTraNaFi != 0) sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'>" + dTraNaFi.ToString("N") + "</td>");
                    else sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'></td>");

                    if (dMenNac != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMenNac.ToString("N") + "</td>");
                    else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");

                    if (dTraInterna != 0) sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'>" + dTraInterna.ToString("N") + "</td>");
                    else sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'></td>");

                    if (dDatos != 0) sb.Append("<td style='width:95px;text-align:right;padding-right:3px' class='bordel'>" + dDatos.ToString("N") + " (" + dMega.ToString("N") + ")" + "</td>");
                    else sb.Append("<td style='width:95px;text-align:right;padding-right:3px' class='bordel'></td>");

                    if (dResto != 0) sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordelr'>" + dResto.ToString("N") + "</td>");
                    else sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordelr'></td>");

                    sb.Append("</tr>");

                    inum_linea = long.Parse(dr["RES_NU_TELEFONO"].ToString());
                    inum_ext = int.Parse(dr["RES_NU_EXTENSION"].ToString());
                    sRespon = dr["responsable"].ToString();
                    sUsu = (dr["beneficiario"].ToString() == "") ? dr["responsable"].ToString() : dr["beneficiario"].ToString();
                    sColor = (dr["beneficiario"].ToString() == "") ? ";color:blue" : "";
                    dTotal = 0;
                    dTotal = 0;
                    dMovilNac = 0;
                    dMovilIberm = 0;
                    dTraNaFi = 0;
                    dMenNac = 0;
                    dTraInterna = 0;
                    dDatos = 0;
                    dMega = 0;
                    dResto = 0;
                }
                switch (int.Parse(dr["GRUPO"].ToString()))
                {
                    case 1:
                        dMovilNac = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 2:
                        dMovilIberm = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 3:
                        dTraNaFi = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 4:
                        dMenNac = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 5:
                        dTraInterna = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 6:
                        dDatos = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        dMega = decimal.Parse(dr["RES_CANTIDAD_MEDIDA"].ToString());
                        break;
                    case 7:
                        dResto = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                }
                if (int.Parse(dr["GRUPO"].ToString()) != 2)
                    dTotal += decimal.Parse(dr["RES_IMPORTE"].ToString());

                bEnviar = true;
            }
            dr.Close();
            dr.Dispose();

            if (dTotal != 0 && bEnviar == true)
            {
                sb.Append("<tr id='" + inum_linea.ToString() + "' ");
                sb.Append("style='height:18px" + sColor + "'>");
                sb.Append("<td style='width:260px;text-align:left;padding-left:4px' class='bordel'>" + sUsu + "</td>");
                sb.Append("<td style='width:65px;text-align:right;padding-right:3px' class='bordel'>" + inum_linea.ToString() + "</td>");
                sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordel'>" + inum_ext.ToString() + "</td>");
                sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'><label style='width:48px;padding-right:2px;' class='negri'>" + dTotal.ToString("N") + "&nbsp;€</label></td>");

                if (dMovilNac != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMovilNac.ToString("N") + "</td>");
                else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");

                //if (dMovilIberm != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMovilIberm.ToString("N") + "</td>");
                //else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dTraNaFi != 0) sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'>" + dTraNaFi.ToString("N") + "</td>");
                else sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dMenNac != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMenNac.ToString("N") + "</td>");
                else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dTraInterna != 0) sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'>" + dTraInterna.ToString("N") + "</td>");
                else sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dDatos != 0) sb.Append("<td style='width:95px;text-align:right;padding-right:3px' class='bordel'>" + dDatos.ToString("N") + " (" + dMega.ToString("N") + ")" + "</td>");
                else sb.Append("<td style='width:95px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dResto != 0) sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordelr'>" + dResto.ToString("N") + "</td>");
                else sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordelr'></td>"); sb.Append("</tr>");
            }

            if (bEnviar) return sb.ToString();
            else return "";
        }
        public static string ConsumosLineaMesColaboradoresMail(int iIdFicepi, DateTime dFecha)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.FACTURACION.ConsumosLineaMesColaboradores(iIdFicepi, dFecha);

            sb.Append(@"<table class='texto2' id='tblTitulo' style='width:665px; padding-top:10px; padding-bottom:10px; table-layout:fixed; height:17px' cellpadding='5px'>
                    <colgroup>
                        <col style='width:260px;' />
                        <col style='width:135px; text-align:right;' />
                        <col style='width:135px; text-align:right;' />
                        <col style='width:135px; text-align:right;' />
                    </colgroup>
                    <tr>
                        <td class='bordeltb'><label class='negri'>Beneficiario</label></td>
                        <td class='bordeltb'><label class='negri'>Nº de línea</label></td>
                        <td class='bordeltb'><label class='negri'>Nº de extensión</label></td>
                        <td class='bordes'><label class='negri'>Total importe</label></td>
                    </tr>
                    </table>");

            sb.Append("<table class='texto2' id='tblDatos' class='MA' style='WIDTH: 665px; table-layout:fixed;' cellpadding='5px' >");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:260px;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("</colgroup>");

            bool bEnviar = false;

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_beneficiario"].ToString() + "' ");
                sb.Append("style='height:17px'>");
                sb.Append("<td class='bordel'>" + dr["beneficiario"].ToString() + "</td>");
                sb.Append("<td class='bordel'>" + dr["RES_NU_TELEFONO"].ToString() + "</td>");
                sb.Append("<td class='bordel'>" + dr["RES_NU_EXTENSION"].ToString() + "</td>");
                sb.Append("<td class='bordelr'>" + decimal.Parse(dr["RES_IMPORTE"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");

                bEnviar = true;
            }
            dr.Close();
            dr.Dispose();


            //  Cerrar cuerpo

            sb.Append("<tr style='height:5px'>");

            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelrb'></td>");

            sb.Append("</tr>");

            sb.Append("</table>");

            if (bEnviar) return sb.ToString();
            else return "";
        }
        public static string ConsumosLineaMesPropiasMailV2(long iNumlinea, DateTime dFecha, long inum_linea, int inum_ext, string sColor, string sBeneficiario)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.FACTURACION.ConsumosLineaMesPropiasV2(iNumlinea, dFecha);

            decimal dTotal = 0;
            decimal dMovilNac = 0;
            decimal dMovilIberm = 0;
            decimal dTraNaFi = 0;
            decimal dMenNac = 0;
            decimal dTraInterna = 0;
            decimal dDatos = 0;
            decimal dResto = 0;
            decimal dMega = 0;

            bool bEnviar = false;

            while (dr.Read())
            {
                switch (int.Parse(dr["GRUPO"].ToString()))
                {
                    case 1:
                        dMovilNac = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 2:
                        dMovilIberm = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 3:
                        dTraNaFi = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 4:
                        dMenNac = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 5:
                        dTraInterna = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                    case 6:
                        dDatos = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        dMega  = decimal.Parse(dr["RES_CANTIDAD_MEDIDA"].ToString());
                        break;
                    case 7:
                        dResto = decimal.Parse(dr["RES_IMPORTE"].ToString());
                        break;
                }
                if (int.Parse(dr["GRUPO"].ToString()) != 2)
                    dTotal += decimal.Parse(dr["RES_IMPORTE"].ToString());

                bEnviar = true;
            }
            dr.Close();
            dr.Dispose();

            if (bEnviar)
            {
                sb.Append("<tr id='" + inum_linea.ToString() + "' style='height:18px" + sColor + "'>");
                sb.Append("<td style='width:85px;text-align:right;padding-right:3px' class='bordel'>" + inum_linea.ToString() + "</td>");
                sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordel'>" + inum_ext.ToString() + "</td>");
                sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'><label style='width:46px;padding-right:4px;' class='negri'>" + dTotal.ToString("N") + "&nbsp;€</label></td>");

                if (dMovilNac != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMovilNac.ToString("N") + "</td>");
                else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");
                //25/04/2017 Por petición de Víctor se quita la columna de Móvil Ibermática
                //if (dMovilIberm != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMovilIberm.ToString("N") + "</td>");
                //else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dTraNaFi != 0) sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'>" + dTraNaFi.ToString("N") + "</td>");
                else sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dMenNac != 0) sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'>" + dMenNac.ToString("N") + "</td>");
                else sb.Append("<td style='width:60px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dTraInterna != 0) sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'>" + dTraInterna.ToString("N") + "</td>");
                else sb.Append("<td style='width:50px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dDatos != 0) sb.Append("<td style='width:95px;text-align:right;padding-right:3px' class='bordel'>" + dDatos.ToString("N") + " (" + dMega.ToString("N") + ")" + "</td>");
                else sb.Append("<td style='width:95px;text-align:right;padding-right:3px' class='bordel'></td>");

                if (dResto != 0) sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordel'>" + dResto.ToString("N") + "</td>");
                else sb.Append("<td style='width:45px;text-align:right;padding-right:3px' class='bordel'></td>");

                sb.Append("<td style='width:260px;text-align:left;padding-left:4px' class='bordelr'>" + sBeneficiario + "</td>");
                sb.Append("</tr>");
            }
            
            if (bEnviar) return sb.ToString();
            else return "";
        }  
        public static string ConsumosLineaMesPropiasMail(long iNumlinea, DateTime dFecha)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.FACTURACION.ConsumosLineaMesPropias(iNumlinea, dFecha);

            sb.Append(@"<table class='texto2' id='tblTitulo' style='width:800px; margin-top:15px; padding-top:10px; padding-bottom:10px; table-layout:fixed; height:17px' cellpadding='5px'>
                                        <colgroup>
                                            <col style='width:260px;' />
                                            <col style='width:135px; text-align:right;' />
                                            <col style='width:135px; text-align:right;' />
                                            <col style='width:135px; text-align:right;' />
//                                            <col style='width:135px; text-align:right; ' />
                                        </colgroup>
                                        <tr>
                                            <td class='bordeltb'><label class='negri'>Tipo de Tráfico</label></td>
                                            <td class='bordeltb'><label class='negri'>Nº de llamadas</label></td>
                                            <td class='bordeltb'><label class='negri'>Cantidad medida</label></td>
                                            <td class='bordeltb'><label class='negri'>Unidad de medida</label></td>
                                            <td class='bordes'><label class='negri'>Importe</label></td>
                                        </tr>
                                        </table>");

            sb.Append("<table id='tblDatos' class='texto2' style='width: 800px; table-layout:fixed;' cellpadding='5px' >");
            sb.Append("<colgroup>");

            sb.Append("    <col style='width:260px;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("</colgroup>");

            decimal dTotal = 0;
            bool bEnviar = false;

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["RES_TIPO_TRAFICO"].ToString() + "' ");
                sb.Append("style='height:17px'>");
                sb.Append("<td class='bordel'><label style='300px'>" + dr["RES_TIPO_TRAFICO"].ToString() + "</label></td>");
                sb.Append("<td class='bordel'>" + dr["RES_NUM_LLAMADAS"].ToString() + "</td>");
                sb.Append("<td class='bordel'>" + decimal.Parse(dr["RES_CANTIDAD_MEDIDA"].ToString()).ToString("N") + "</td>");
                sb.Append("<td class='bordel'>" + dr["RES_UNIDAD_MEDIDA"].ToString() + "</td>");
                sb.Append("<td class='bordelr'>" + decimal.Parse(dr["RES_IMPORTE"].ToString()).ToString("N") + "</td>");
                dTotal += decimal.Parse(dr["RES_IMPORTE"].ToString());

                sb.Append("</tr>");
                bEnviar = true;
            }
            dr.Close();
            dr.Dispose();

            //  Cerrar cuerpo

            sb.Append("<tr style='height:5px'>");

            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelb'></td>");
            sb.Append("<td class='bordelrb'></td>");

            sb.Append("</tr>");
            sb.Append("</table>");

            //  Total
            sb.Append("<table id='tblDatos' style='WIDTH: 800px; table-layout:fixed;' cellpadding='5px'>");
            sb.Append("<colgroup>");

            sb.Append("    <col style='width:260px;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("    <col style='width:135px;text-align:right;' />");
            sb.Append("</colgroup>");

            sb.Append("<tr>");

            sb.Append("<td colspan='3'>&nbsp;");
            sb.Append("</td>");
            sb.Append("<td><label class='negri'>Total</label>");
            sb.Append("</td>");
            sb.Append("<td class='texto2 negri bordelrb'");
            if (dTotal < 0) sb.Append(" style='color:red'");
            sb.Append(">" + dTotal.ToString("N"));
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("</table></br>");

            if (bEnviar) return sb.ToString();
            else return "";
        }  
        public static string ConsumosMesPropias(int iIdFicepi, DateTime dFecha)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr;

            SqlDataReader drLin = GEMO.DAL.LINEA.ResponsablesLineasFactura(iIdFicepi);

            while (drLin.Read())
            {
                if (drLin["t710_idestado"].ToString().Trim() != "A") continue;
                dr = GEMO.DAL.FACTURACION.ConsumosLineaMesPropias(long.Parse(drLin["t708_numlinea"].ToString()), dFecha);

                sb.Append("<tr>");
                sb.Append("<td></br>");
                sb.Append(@"&nbsp;<label style='width:130px' class='negri'>Nº de línea:&nbsp;&nbsp;</label><label>" + drLin["t708_numlinea"].ToString() + "</label><br>");
                sb.Append(@"&nbsp;<label style='width:130px' class='negri'>Nº de extensión:&nbsp;&nbsp;</label><label>" + drLin["t708_numext"].ToString() + "</label><br>");
                sb.Append(@"<table id='tblTitulo' style='width:960px; margin-top:10px; height:17px;table-layout:fixed;' cellpadding='5px'>
                                    <colgroup>
                                        <col style='width:360px;' />
                                        <col style='width:150px; text-align:right;' />
                                        <col style='width:150px; text-align:right;' />
                                        <col style='width:150px; text-align:right;' />
                                        <col style='width:150px; text-align:right; ' />
                                    </colgroup>
                                    <tr>
                                        <td class='bordeltb'><label class='negri'>Tipo de Tráfico</label></td>
                                        <td class='bordeltb'><label class='negri'>Nº de llamadas</label></td>
                                        <td class='bordeltb'><label class='negri'>Cantidad medida</label></td>
                                        <td class='bordeltb'><label class='negri'>Unidad de medida</label></td>
                                        <td class='bordes'><label class='negri'>Importe</label></td>
                                    </tr>
                                    </table>");

                sb.Append("<table id='tblDatos' class='MA' style='WIDTH: 960px; table-layout:fixed;' cellpadding='5px' >");
                sb.Append("<colgroup>");

                sb.Append("    <col style='width:360px;' />");
                sb.Append("    <col style='width:150px;text-align:right;' />");
                sb.Append("    <col style='width:150px;text-align:right;' />");
                sb.Append("    <col style='width:150px;text-align:right;' />");
                sb.Append("    <col style='width:150px;text-align:right;' />");
                sb.Append("</colgroup>");

                decimal dTotal = 0;

                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["RES_TIPO_TRAFICO"].ToString() + "' ");
                    sb.Append(" onclick=\"ms(this);\" ");
                    sb.Append(" onDblClick='detalle(this.id);'");
                    sb.Append("style='height:20px' >");
                    sb.Append("<td class='bordel'>" + dr["RES_TIPO_TRAFICO"].ToString() + "</td>");
                    sb.Append("<td class='bordel'>" + dr["RES_NUM_LLAMADAS"].ToString() + "</td>");
                    sb.Append("<td class='bordel'>" + decimal.Parse(dr["RES_CANTIDAD_MEDIDA"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td class='bordel'>" + dr["RES_UNIDAD_MEDIDA"].ToString() + "</td>");
                    sb.Append("<td class='bordelr'>" + decimal.Parse(dr["RES_IMPORTE"].ToString()).ToString("N") + "</td>");
                    dTotal += decimal.Parse(dr["RES_IMPORTE"].ToString());

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();


                //  Cerrar cuerpo

                sb.Append("<tr>");
                sb.Append("<td colspan='5' class='bordet'>");
                sb.Append("</td>");

                sb.Append("</tr>");

                //  Total

                sb.Append("<tr>");

                sb.Append("<td colspan='3'>");
                sb.Append("</td>");
                sb.Append("<td><label class='negri'>Total</label>");
                sb.Append("</td>");
                sb.Append("<td class='bordelrb'");
                if (dTotal < 0) sb.Append("color:red");
                sb.Append("'>" + dTotal.ToString("N"));
                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("</table>");


                sb.Append("</br>");

                sb.Append("</td>");
                sb.Append("</tr>");
            }

            drLin.Close();
            drLin.Dispose();

            return sb.ToString();
        }
        public static string ConsumosLineaMesPropias(long iNumlinea, DateTime dFecha)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.FACTURACION.ConsumosLineaMesPropias(iNumlinea, dFecha);

            sb.Append("<table id='tblDatos' class='MA' style='WIDTH: 960px; table-layout:fixed;' cellpadding='5px' >");
            sb.Append("<colgroup>");

            sb.Append("    <col style='width:360px;' />");
            sb.Append("    <col style='width:150px;text-align:right;' />");
            sb.Append("    <col style='width:150px;text-align:right;' />");
            sb.Append("    <col style='width:150px;text-align:right;' />");
            sb.Append("    <col style='width:150px;text-align:right;' />");
            sb.Append("</colgroup>");
            decimal dTotal = 0;

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["RES_TIPO_TRAFICO"].ToString() + "' ");
                sb.Append(" onclick=\"ms(this);\" ");
                sb.Append(" onDblClick='detalle(this.id);'");
                sb.Append("style='height:20px' >");
                sb.Append("<td class='bordel'>" + dr["RES_TIPO_TRAFICO"].ToString() + "</td>");
                sb.Append("<td class='bordel'>" + dr["RES_NUM_LLAMADAS"].ToString() + "</td>");
                sb.Append("<td class='bordel'>" + decimal.Parse(dr["RES_CANTIDAD_MEDIDA"].ToString()).ToString("N") + "</td>");
                sb.Append("<td class='bordel'>" + dr["RES_UNIDAD_MEDIDA"].ToString() + "</td>");
                sb.Append("<td class='bordelr'>" + decimal.Parse(dr["RES_IMPORTE"].ToString()).ToString("N") + "</td>");
                dTotal += decimal.Parse(dr["RES_IMPORTE"].ToString());

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();


              //Cerrar cuerpo

            sb.Append("<tr>");
            sb.Append("<td colspan='5' class='bordet'>");
            sb.Append("</td>");

            sb.Append("</tr>");

              //Total

            sb.Append("<tr>");

            sb.Append("<td colspan='3'>");
            sb.Append("</td>");
            sb.Append("<td><label class='negri'>Total</label>");
            sb.Append("</td>");
            sb.Append("<td class='bordelrb'");
            if (dTotal < 0) sb.Append("color:red");
            sb.Append("'>" + dTotal.ToString("N"));
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("</table>");

            return sb.ToString();
        }
        public static string ConsumosLineaMesColaboradores(int iIdFicepi, DateTime dFecha)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = GEMO.DAL.FACTURACION.ConsumosLineaMesColaboradores(iIdFicepi, dFecha);

            sb.Append("<table id='tblDatos' class='MA' style='WIDTH: 810px; table-layout:fixed;' cellpadding='5px' >");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:360px;' />");
            sb.Append("    <col style='width:150px;text-align:right;' />");
            sb.Append("    <col style='width:150px;text-align:right;' />");
            sb.Append("    <col style='width:150px;text-align:right;' />");
            sb.Append("</colgroup>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_beneficiario"].ToString() + "' ");
                sb.Append(" onclick=\"ms(this);\" ");
                sb.Append(" onDblClick='detalle2(this.id);'");
                sb.Append("style='height:20px' >");
                sb.Append("<td class='bordel'>" + dr["beneficiario"].ToString() + "</td>");
                sb.Append("<td class='bordel'>" + dr["RES_NU_TELEFONO"].ToString() + "</td>");
                sb.Append("<td class='bordel'>" + dr["RES_NU_EXTENSION"].ToString() + "</td>");
                sb.Append("<td class='bordelr'>" + decimal.Parse(dr["RES_IMPORTE"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();


            //  Cerrar cuerpo

            sb.Append("<tr>");
            sb.Append("<td colspan='5' class='bordet'>");
            sb.Append("</td>");

            sb.Append("</tr>");

            sb.Append("</table>");

            return sb.ToString();
        }
     }
}