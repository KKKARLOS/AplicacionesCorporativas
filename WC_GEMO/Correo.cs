using System;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Collections;
//using ROBOCOR_CLI;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
////PARA ROBOCOR2////////////////////////////////////
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
/////////////////////////////////////////////////////
namespace GEMO.BLL
{
    /// <summary>
    /// Descripción breve de Correo.
    /// </summary>
    public class Correo
    {
        //private cLog mLog;

        public Correo()
        {
        }
        public static void EnviarCorreos(ArrayList aListCorreo)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                EnviarAviso(sAux[0], sAux[1], sAux[2], "", "", "", "ADM_GEMO");
            }
        }
        public static void EnviarCorreosFull(ArrayList aListCorreo, string sFechaHoraEnvio, string sVotOption)
        {
            for (int i = 0; i < aListCorreo.Count; i++)
            {
                string[] sAux = (string[])aListCorreo[i];
                EnviarAvisoFactura(sAux[0], sAux[1], sAux[2], "", "", "", "ADM_GEMO", sFechaHoraEnvio, sVotOption);
            }
        }
        public static void EnviarAvisoFactura(string sAsunto, string sTexto, string sTO, string sCC, string sCCO, string sFich, string sRemitente, string sFechaHoraEnvio, string sVotOption)
        {
            //string webServer = "http://imagenes.ibermatica.com/GEMONET/";
            string webServer = System.Configuration.ConfigurationManager.AppSettings["ImagenesCorreo"];

            string strTexto = @"<html>
                <style type='text/css'>
                            table
                            {
	                            table-layout: fixed;
                                border-collapse: collapse;
                                empty-cells: show;
                                FONT-WEIGHT: normal;
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                TEXT-DECORATION: none;    
                            }
                            tr
                            {
                                display:table-row;
                            }
				            .titulo
				            {
					            FONT-WEIGHT: bold;
					            FONT-SIZE: 11px;
					            FONT-FAMILY: Arial, Helvetica, sans-serif
				            }
                            .TBLINI
                            {
                                FONT-WEIGHT: bold;
                                FONT-SIZE: 11px;
                                BACKGROUND-IMAGE: url(" + webServer + @"fondoEncabezamientoListas.gif);
                                COLOR: #ffffff;
                                FONT-FAMILY: Arial, Helvetica, sans-serif
                            }
                            .textoResultadoTabla 
                            {
                                BACKGROUND-POSITION: left center;
                                FONT-WEIGHT: normal;
                                FONT-SIZE: 11px;
                                BACKGROUND-IMAGE: url(" + webServer + @"fondoTotalResListas.gif);
                                VERTICAL-ALIGN: middle;
                                FONT-FAMILY: Arial, Helvetica, sans-serif
                            }
                            .check
                            {
                                BACKGROUND-POSITION: center;
                                BACKGROUND-REPEAT: no-repeat;
                                BACKGROUND-IMAGE: url(" + webServer + @"imgOK.gif);
                            }
				            .texto
				            {
                                border-collapse: collapse;
					            FONT-WEIGHT: normal;
					            FONT-SIZE: 11px;
					            FONT-FAMILY: Arial, Helvetica, sans-serif;
					            TEXT-DECORATION: none
				            }
				            .texto2
				            {
                                BACKGROUND-IMAGE: url(" + webServer + @"papel.gif);
                                BACKGROUND-COLOR: #EAEAEA;
				            }
                            .FA
                            {
	                            HEIGHT:16px;
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                BACKGROUND-COLOR: #e6eef2;
                                TEXT-DECORATION: none;
                            }
                            .FB
                            {
	                            HEIGHT:16px;	
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                BACKGROUND-COLOR: #ffffff;
                                TEXT-DECORATION: none;
                            }

                            .fondoPapel
                            {
                                BACKGROUND-IMAGE: url(" + webServer + @"papel.gif);
                            }
                            .bordet
                            {
	                            BORDER-TOP: #5894ae 1px solid; 
                            }
                            .bordel
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
                            }
                            .bordelr
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-RIGHT: #5894ae 1px solid; 
                            }
                            .bordelb
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;	
                            }
                            .bordelrb
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-RIGHT: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;	
                            }
                            .bordeltb
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-TOP: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;		
                            }
                            .bordeltb2
                            {
	                            BORDER-TOP: #5894ae 2px solid; 
	                            BORDER-LEFT: #5894ae 2px solid; 
	                            BORDER-BOTTOM: #5894ae 2px solid;		
                            }
                            .bordes
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-TOP: #5894ae 1px solid; 
	                            BORDER-RIGHT: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;	
                            }
                            .bordes2
                            {
	                            BORDER-LEFT: #5894ae 2px solid; 
	                            BORDER-TOP: #5894ae 2px solid; 
	                            BORDER-RIGHT: #5894ae 2px solid; 
	                            BORDER-BOTTOM: #5894ae 2px solid;	
                            }
                            .negri
                            {
                                font-weight: bold; 
                            }

                            @media print and (width: 21cm) and (height: 29.7cm)
                            {
//                                BODY { font-size: 10pt }
//                                @page {margin: 2cm;}
                            }
                            </style>							
            <body bgcolor='#f7fafb' scroll='auto' text='#000000' leftmargin='0' topmargin='0'>
				<table style='FONT-FAMILY: Arial;FONT-SIZE: 11px' width='100%' border='0' cellspacing='0' cellpadding='0'> 
					<tr>
						<td width='132px' bgcolor='#5894AE'><img src='" + webServer + @"imgLogoAplicacion.gif' width='132' height='47'></td> 
						<td bgcolor='#ffffff' align='left'><img src='" + webServer + @"bckSinTrainera.gif' width='500' height='47'></td>
						<td bgcolor='#ffffff' align='right'><img src='" + webServer + @"logoIbermatica2.gif' width='124' height='33'></td>
					</tr> 
					<tr><td colspan='3' background='" + webServer + @"imgLineaAzulada.gif'></td></tr>
					<tr><td colspan='3'><br><blockquote style='margin-left:5px'>" + sTexto + @"</blockquote></td></tr>
					<tr><td colspan='3'><br>
                    <blockquote style='margin-left:10px'><div class='negri' style='FONT-SIZE: 14px;text-decoration:underline;'>Notas:</div><br>
                    1.- El acceso a la información detallada de factura no estará disponible.<br>
                    2.- Las líneas están asociadas a quien las solicitó, en caso de que ya no seas responsable de dicha línea por favor comunícanos el nuevo responsable.<br> 
                    3.- En color <span style='color:blue'>Azul</span>, aquellas líneas de las que soy responsable pero que están asignadas a un departamento/servicio o un colaborador externo (Beneficiario).<br> 
                    4.- Para cualquier consulta, póngase en contacto con <a href='mailto:cau@ibermatica.com' class='enlace'>CAU Ibermática</a><br>
                    5.- Aclaraciones sobre las cabeceras:<br>
                    <table style='FONT-FAMILY: Arial;FONT-SIZE: 11px;margin-top:10px' width='100%' border='0' cellspacing='0' cellpadding='0'> 
                    <tr><td>
                        <label style='width:85px;margin-left:15px;'>- Móv.Nac.</label>->  Llamadas a móviles de cualquier operador nacional.<br>
                        <label style='width:85px;margin-left:15px;'>- Móv.Iber.</label>->  Llamadas a móviles de Ibermática.<br>
                        <label style='width:85px;margin-left:15px;'>- Fij.Nac.</label>->  Llamadas a fijos nacionales de cualquier operador y 90x.<br>
                        <label style='width:85px;margin-left:15px;'>- Men.Nac.</label>->  Mensajes a cualquier operador nacional.<br>
                        <label style='width:85px;margin-left:15px;'>- Intern.</label>->  Llamadas y mensajes a números internacionales, tráfico en Itinerancia y Roaming.<br>
                        <label style='width:85px;margin-left:15px;'>- Datos(Mbytes)</label>->  Tráfico de datos. Tarifa plana que se aplica y entre paréntesis el tráfico cursado.<br>
                        </td>
                    </tr>
                    <tr><td>
                        <ul style='margin:5px'>
                            <li style='margin-left:125px;'>Las líneas que tienen solo datos son las UMTS.</li>
                            <li style='margin-left:125px;'>Líneas con voz y datos (Smartphones).</li>
                        </ul>
                        </td>
                    </tr>
                    <tr><td>                        
                        <label style='width:85px;margin-left:15px;'>- Resto</label>->  Otros conceptos.
                        </td>
                    </tr>
                    </table>

                    </blockquote>
					<div align='right'><img src='" + webServer + @"imgGrupoIbermatica.gif' width='175' height='29' />&nbsp;</div></td>
					</tr>
				</table>
			</body>
			</html>";

            //ROBOCOR_CLI.cMailWHCliente objMail = new ROBOCOR_CLI.cMailWHClienteClass();
            if (sRemitente == "")
            {
                sRemitente = "ADM_GEMO";
            }
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "D")
            {
                sTO = "EDA@ibermatica.com";
                sAsunto += "  ("+ sTO + ")";
                //sTO = "DOPEOTCA";
            }

            //sAsunto += "  (" + sTO + ")";
            //sTO = "DOPEOTCA";

            if (sFechaHoraEnvio == "" && sVotOption == "")
            {
                SendMailRBC2(sAsunto, strTexto, sTO, sFich, sCC, sCCO, "");
                //objMail.EnviaMailNET(sRemitente, sAsunto, strTexto, enumTipoMail.etmHTML, sTO, false, sFich, sCC, sCCO);
            }
            else
            {
                DateTime dtFechaEnvio;
                if (sFechaHoraEnvio.Trim()== "") dtFechaEnvio = DateTime.Now;
                else dtFechaEnvio = DateTime.Parse(sFechaHoraEnvio);

                //objMail.EnviaMailNET2(sRemitente, sAsunto, strTexto, enumTipoMail.etmHTML, sTO, false, sFich, sCC, sCCO, dtFechaEnvio, sVotOption);
                SendMailRBC2(sAsunto, strTexto, sTO, sFich, sCC, sCCO, sFechaHoraEnvio);
            }
            //Explicitly destroy COM type after use.
            //Marshal.ReleaseComObject(objMail);
            //Si se ha enviado algún fichero adjunto, hay que eliminarlo.
            //if (sFich != "") File.Delete(sFich);
        }
        public static void EnviarAviso(string sAsunto, string sTexto, string sTO, string sCC, string sCCO, string sFich, string sRemitente)
        {
            //string webServer = "http://imagenes.intranet.ibermatica/GEMONET/";
            string webServer = System.Configuration.ConfigurationManager.AppSettings["ImagenesCorreo"];

            string strTexto = @"<html>
                <style type='text/css'>
                            table
                            {
	                            table-layout:fixed;
                                border-collapse: collapse;
                                empty-cells: show;
                                FONT-WEIGHT: normal;
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                TEXT-DECORATION: none;    
                            }
                            tr
                            {
                                display:table-row;
                            }
				            .titulo
				            {
					            FONT-WEIGHT: bold;
					            FONT-SIZE: 11px;
					            FONT-FAMILY: Arial, Helvetica, sans-serif
				            }
                            .TBLINI
                            {
                                FONT-WEIGHT: bold;
                                FONT-SIZE: 11px;
                                BACKGROUND-IMAGE: url(" + webServer + @"fondoEncabezamientoListas.gif);
                                COLOR: #ffffff;
                                FONT-FAMILY: Arial, Helvetica, sans-serif
                            }
                            .textoResultadoTabla 
                            {
                                BACKGROUND-POSITION: left center;
                                FONT-WEIGHT: normal;
                                FONT-SIZE: 11px;
                                BACKGROUND-IMAGE: url(" + webServer + @"fondoTotalResListas.gif);
                                VERTICAL-ALIGN: middle;
                                FONT-FAMILY: Arial, Helvetica, sans-serif
                            }
                            .check
                            {
                                BACKGROUND-POSITION: center;
                                BACKGROUND-REPEAT: no-repeat;
                                BACKGROUND-IMAGE: url(" + webServer + @"imgOK.gif);
                            }
				            .texto
				            {
                                border-collapse: collapse;
					            FONT-WEIGHT: normal;
					            FONT-SIZE: 11px;
					            FONT-FAMILY: Arial, Helvetica, sans-serif;
					            TEXT-DECORATION: none
				            }
				            .texto2
				            {
                                BACKGROUND-IMAGE: url(" + webServer + @"papel.gif);
                                BACKGROUND-COLOR: #EAEAEA;
				            }
                            .FA
                            {
	                            HEIGHT:16px;
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                BACKGROUND-COLOR: #e6eef2;
                                TEXT-DECORATION: none;
                            }
                            .FB
                            {
	                            HEIGHT:16px;	
                                FONT-SIZE: 11px;
                                COLOR: #000000;
                                FONT-FAMILY: Arial, Helvetica, sans-serif;
                                BACKGROUND-COLOR: #ffffff;
                                TEXT-DECORATION: none;
                            }

                            .fondoPapel
                            {
                                BACKGROUND-IMAGE: url(" + webServer + @"papel.gif);
                            }
                            .bordet
                            {
	                            BORDER-TOP: #5894ae 1px solid; 
                            }
                            .bordel
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
                            }
                            .bordelr
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-RIGHT: #5894ae 1px solid; 
                            }
                            .bordelb
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;	
                            }
                            .bordelrb
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-RIGHT: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;	
                            }
                            .bordeltb
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-TOP: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;		
                            }
                            .bordeltb2
                            {
	                            BORDER-TOP: #5894ae 2px solid; 
	                            BORDER-LEFT: #5894ae 2px solid; 
	                            BORDER-BOTTOM: #5894ae 2px solid;		
                            }
                            .bordes
                            {
	                            BORDER-LEFT: #5894ae 1px solid; 
	                            BORDER-TOP: #5894ae 1px solid; 
	                            BORDER-RIGHT: #5894ae 1px solid; 
	                            BORDER-BOTTOM: #5894ae 1px solid;	
                            }
                            .bordes2
                            {
	                            BORDER-LEFT: #5894ae 2px solid; 
	                            BORDER-TOP: #5894ae 2px solid; 
	                            BORDER-RIGHT: #5894ae 2px solid; 
	                            BORDER-BOTTOM: #5894ae 2px solid;	
                            }
                            .negri
                            {
                                font-weight: bold; 
                            }

                            @media print and (width: 21cm) and (height: 29.7cm)
                            {
//                                BODY { font-size: 10pt }
//                                @page {margin: 2cm;}
                            }
                            </style>							
            <body bgcolor='#f7fafb' scroll='auto' text='#000000' leftmargin='0' topmargin='0'>
				<table style='FONT-FAMILY: Arial;FONT-SIZE: 11px' width='100%' border='0' cellspacing='0' cellpadding='0'> 
					<tr>
						<td width='132px' bgcolor='#5894AE'><img src='" + webServer + @"imgLogoAplicacion.gif' width='132' height='47'></td> 
						<td bgcolor='#ffffff' align='left'><img src='" + webServer + @"bckSinTrainera.gif' width='500' height='47'></td>
						<td bgcolor='#ffffff' align='right'><img src='" + webServer + @"logoIbermatica2.gif' width='124' height='33'></td>
					</tr> 
					<tr><td colspan='3' background='" + webServer + @"imgLineaAzulada.gif'></td></tr>
					<tr><td colspan='3'><br><blockquote style='margin-left:5px'>" + sTexto + @"</blockquote></td></tr>
					<tr><td colspan='3'><br><blockquote style='margin-left:5px'>Para cualquier consulta, póngase en contacto con <a href='mailto:cau@ibermatica.com' class='enlace'>CAU Ibermática</a></blockquote>
                    <blockquote style='margin-left:5px'>Este mensaje no admite respuesta.</blockquote><br>
					<div align='right'><img src='" + webServer + @"imgGrupoIbermatica.gif' width='175' height='29' />&nbsp;</div></td>
					</tr>
				</table>
			</body>
			</html>";

            //ROBOCOR_CLI.cMailWHCliente objMail = new ROBOCOR_CLI.cMailWHClienteClass();
            if (sRemitente == "")
            {
                sRemitente = "ADM_GEMO";
            }
            if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "D")
            {
                //sTO = "EDA@ibermatica.com";
                sAsunto += "  (" + sTO + ")";
                //sTO = "DOPEOTCA";
            }

            //sTO = "DOPEOTCA";
/*
            objMail.EnviaMailNET(sRemitente, sAsunto, strTexto, enumTipoMail.etmHTML, sTO, false, sFich, sCC, sCCO);

            //Explicitly destroy COM type after use.
            Marshal.ReleaseComObject(objMail);
            //Si se ha enviado algún fichero adjunto, hay que eliminarlo.
            if (sFich != "")
            {
                File.Delete(sFich);
            }
*/
            SendMailRBC2(sAsunto, strTexto, sTO, sFich, sCC, sCCO,"");
        }
        public static string CabeceraHtmlMail(string sEtiqueta)
        {
            StringBuilder sb = new StringBuilder();
            //string webServer = "http://imagenes.intranet.ibermatica/GEMONET/";
            string webServer = System.Configuration.ConfigurationManager.AppSettings["ImagenesCorreo"];
            sb.Append("<table border='0' background='" + webServer + @"imgFondoTitulo.gif' style='width:800px;height:40px;background-color:#e6eef2;BACKGROUND-REPEAT: no-repeat;' cellspacing='0' cellpadding='0'>");
            sb.Append("<tr style='height:30px'><td>&nbsp;&nbsp;" + sEtiqueta + "</td></tr>");
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string CabeceraHtmlPropias()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<table class='texto2' id='tblTitulo' style='width:800px; margin-top:15px; padding-top:10px; padding-bottom:10px; height:17px; table-layout:fixed; border-collapse: collapse;' cellpadding='5px'>
                        <tr>
                            <td style='width:85px;' class='bordeltb' align='center'><label class='negri'>Pref/Nº Línea</label></td>
                            <td style='width:45px;' class='bordeltb' align='center'><label class='negri'>NºExt.</label></td>
                            <td style='width:50px;' class='bordeltb' align='center'><label class='negri'>Total</label></td>
                            <td style='width:60px;' class='bordeltb' align='center'><label class='negri'>Móv.Nac.</label></td>
                            <td style='width:60px;' class='bordeltb' align='center'><label class='negri'>Móv.Iber.</label></td>
                            <td style='width:50px;' class='bordeltb' align='center'><label class='negri'>Fij.Nac.</label></td>
                            <td style='width:60px;' class='bordeltb' align='center'><label class='negri'>Men.Nac.</label></td>
                            <td style='width:50px;' class='bordeltb' align='center'><label class='negri'>Intern.</label></td>
                            <td style='width:95px;' class='bordeltb' align='center'><label class='negri'>Datos (Mbytes)</label></td>
                            <td style='width:45px;' class='bordes' align='center'><label class='negri'>Resto</label></td>
                            <td style='width:200px;' class='bordes' align='center'><label class='negri'>Beneficiario/Dpto</label></td>
                        </tr>
                        </table>");

            sb.Append("<table id='tblDatos' class='fondoPapel' style='width: 800px; table-layout:fixed; border-collapse: collapse;' cellpadding='5px' >");
            //sb.Append("<colgroup>");
            //sb.Append("    <col style='width:65px;text-align:right;' />");
            //sb.Append("    <col style='width:45px;text-align:right;' />");
            //sb.Append("    <col style='width:50px;text-align:right;' />");
            //sb.Append("    <col style='width:60px;text-align:right;' />");
            //sb.Append("    <col style='width:60px;text-align:right;' />");
            //sb.Append("    <col style='width:50px;text-align:right;' />");
            //sb.Append("    <col style='width:60px;text-align:right;' />");
            //sb.Append("    <col style='width:50px;text-align:right;' />");
            //sb.Append("    <col style='width:95px;text-align:right;' />");
            //sb.Append("    <col style='width:45px;text-align:right;' />");
            //sb.Append("    <col style='width:200px;text-align:left;' />");
            //sb.Append("</colgroup>");
            return sb.ToString();
        }
        public static string CabeceraHtmlCola()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<table class='texto2' id='tblTitulo' style='width:800px; margin-top:15px; padding-top:10px; padding-bottom:10px; height:17px; table-layout:fixed; border-collapse: collapse;' cellpadding='5px'>
                        <tr>
                            <td style='width:200px;' align='center' class='bordeltb'><label class='negri'>Beneficiario</label></td>
                            <td style='width:65px;'  align='center' class='bordeltb'><label class='negri'>Pref/Nº Línea</label></td>
                            <td style='width:45px;'  align='center' class='bordeltb'><label class='negri'>NºExt.</label></td>
                            <td style='width:50px;'  align='center' class='bordeltb'><label class='negri'>Total</label></td>
                            <td style='width:60px;'  align='center' class='bordeltb'><label class='negri'>Móv.Nac.</label></td>
                            <td style='width:60px;'  align='center' class='bordeltb'><label class='negri'>Móv.Iber.</label></td>
                            <td style='width:50px;'  align='center' class='bordeltb'><label class='negri'>Fij.Nac.</label></td>
                            <td style='width:60px;'  align='center' class='bordeltb'><label class='negri'>Men.Nac.</label></td>
                            <td style='width:50px;'  align='center' class='bordeltb'><label class='negri'>Intern.</label></td>
                            <td style='width:95px;'  align='center' class='bordeltb'><label class='negri'>Datos (Mbytes)</label></td>
                            <td style='width:45px;'  align='center' class='bordes'><label class='negri'>Resto</label></td>
                        </tr>
                        </table>");

            sb.Append("<table id='tblDatos' class='fondoPapel' style='width: 800px; table-layout:fixed; border-collapse:collapse;' cellpadding='5px' >");
            //sb.Append("<colgroup>");
            //sb.Append("    <col style='width:200px;text-align:left;' />");
            //sb.Append("    <col style='width:65px;text-align:right;' />");
            //sb.Append("    <col style='width:45px;text-align:right;' />");
            //sb.Append("    <col style='width:50px;text-align:right;' />");
            //sb.Append("    <col style='width:60px;text-align:right;' />");
            //sb.Append("    <col style='width:60px;text-align:right;' />");
            //sb.Append("    <col style='width:50px;text-align:right;' />");
            //sb.Append("    <col style='width:60px;text-align:right;' />");
            //sb.Append("    <col style='width:50px;text-align:right;' />");
            //sb.Append("    <col style='width:95px;text-align:right;' />");
            //sb.Append("    <col style='width:45px;text-align:right;' />");
            //sb.Append("</colgroup>");
            return sb.ToString();
        }
        private static void SendMailRBC2(string sAsunto, string sTexto, string sTO, string sFich, string sCC, string sCCO, string sDesde )
        {
            System.Text.StringBuilder strb = new System.Text.StringBuilder();


            //formatear los br (el body debe ser xhtml)
            //sTexto = sTexto.Replace("<br />", "<br />");

            sTexto = sTexto.Replace("-", "&#45;");
            sAsunto = sAsunto.Replace("-", "&#45;");

            strb.Append("<?xml version='1.0' encoding='UTF-8'?>\r\n");
            strb.Append("<Email>\r\n");
            strb.Append("<App>GEMO</App>\r\n");
            strb.Append("<RcptTo>" + sTO + "</RcptTo>\r\n");
            strb.Append("<Cc>" + sCC + "</Cc>\r\n");
            strb.Append("<Cco>" + sCCO + "</Cco>\r\n");
            strb.Append("<Subject><!--" + sAsunto + "--></Subject>\r\n");
            strb.Append("<Body><!--" + sTexto + "--></Body>\r\n");
            strb.Append("<BodyFormat>H</BodyFormat>\r\n");
            strb.Append("<EnviarDesde>" + sDesde + "</EnviarDesde>\r\n");
            strb.Append("<Adjuntos>\r\n");

            if (sFich != "")
            {
                strb.Append("<Adjunto>");
                strb.Append("<Nombre>" + new FileInfo(sFich).Name + "</Nombre>");
                strb.Append("<Fichero>" + EncodeFile(sFich) + "</Fichero>");
                strb.Append("</Adjunto>");
            }

            strb.Append("</Adjuntos>\r\n");
            strb.Append("</Email>\r\n");

            svcEmail.svcEmail MySvcEmail = new svcEmail.svcEmail();

            string certPath = System.Web.HttpContext.Current.Request.MapPath("~/Certificado");
            if (!certPath.EndsWith("\\")) certPath += "\\";
            MySvcEmail.ClientCertificates.Add(new X509Certificate2(certPath + "GEMO.pfx", "igueldo"));


            string xmlretval = "";
            try
            {
                xmlretval = MySvcEmail.SendMessage(strb.ToString());
            }
            catch (Exception ex)
            {
                //el servicio ROBOCOR2 está caido. Avisar a EDA por smtp
                MailMessage objMail = new MailMessage();

                string msgex = ex.Message;
                if (ex.InnerException != null) msgex = msgex + " ::: " + ex.InnerException.Message;

                objMail.From = new MailAddress("gemo@ibermatica.com");
                objMail.To.Add(new MailAddress("EDA@ibermatica.com"));

                objMail.Subject = "No se puede establecer una conexión con ROBOCOR2";
                objMail.IsBodyHtml = false;

                objMail.Body = msgex;

                SmtpClient myClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo.", new Exception(msgex));
            }
            finally
            {
            }


            //Validar la respuesta
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlretval);

            int errnum = int.Parse(xmldoc.SelectSingleNode("/Datos/Error").InnerText);

            //Si error de envío...
            if (errnum > 0)
            {
                string errmotivo = "";
                switch (errnum)
                {
                    case 1:
                        errmotivo = "Error general.";
                        break;
                    case 2:
                        errmotivo = "Aplicación no permitida.";
                        break;
                    case 3:
                        errmotivo = "Mensaje mal formado.";
                        break;
                    case 4:
                        errmotivo = "Destinatarios no existentes.";
                        break;
                }

                //error aplicación no permitidida, mal configurada o certificado erroneo.    
                MailMessage objMail = new MailMessage();

                objMail.From = new MailAddress("gemo@ibermatica.com");
                objMail.To.Add(new MailAddress("EDA@ibermatica.com"));

                objMail.Subject = "GEMO: Error en envío de emails. Error " + errnum + ". " + errmotivo;
                objMail.IsBodyHtml = false;

                objMail.Body = xmldoc.SelectSingleNode("/Datos/Message").InnerText;
                if (errnum == 4)
                    objMail.Body += "\n\nEmailID: " + xmldoc.SelectSingleNode("/Datos/Emailid").InnerText;

                SmtpClient myClient = new SmtpClient(System.Configuration.ConfigurationManager.AppSettings["ServidorSMTP"]);
                myClient.Send(objMail);

                throw new Exception("La operación se realizó correctamente pero no se pudo conectar con el servicio de mensajería para avisar a los destinatarios del correo. Motivo: " + errmotivo, new Exception(xmldoc.SelectSingleNode("/Datos/Message").InnerText));
            }
        }
        private static string EncodeFile(string fichero)
        {
            FileStream fs = new FileStream(fichero, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            string retval = Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);
            fs.Close();

            return retval;

        }    
    }
}
