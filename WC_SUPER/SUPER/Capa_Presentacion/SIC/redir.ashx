<%@ WebHandler Language="C#" Class="IB.SUPER.SIC.redir" %>

using System;
using System.Web;
using System.Collections;
using System.Web.SessionState;

namespace IB.SUPER.SIC
{
    public class redir : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            string pantalla = "";
            string qs = "";
            string url = context.Session["strServer"] + "Capa_Presentacion/SIC/";

            BLL.SolicitudPreventa cSolicitud = new BLL.SolicitudPreventa();
            BLL.AccionPreventa cAP = null;
            BLL.TareaPreventa cTP = null;
            BLL.Usuario cUsuario = new BLL.Usuario();
            Models.SolicitudPreventa o;
            Models.PerfilesEdicion oPE = null;
            bool soyLider = false;
            bool soyParticipante = false;
            int ta201_idsubareapreventa = 0;

            try
            {
                Hashtable ht = Shared.Utils.ParseQuerystring(context.Request.QueryString.ToString());
                string caso = ht["caso"].ToString();

                Shared.HistorialNavegacion.Resetear();

                #region perfil edicion usuario
                if (caso == "A" || caso == "H" || caso == "F" || caso == "G" || caso == "I")
                {
                    cTP = new BLL.TareaPreventa();
                    soyParticipante = cTP.EsParticipante(Convert.ToInt32(context.Session["IDFICEPI_PC_ACTUAL"]), Convert.ToInt32(ht["ta207_idtareapreventa"]));
                }

                cAP = new BLL.AccionPreventa();
                Models.AccionPreventa oAP = cAP.Select(Convert.ToInt32(ht["ta204_idaccionpreventa"]));
                if (oAP != null)
                {
                    ta201_idsubareapreventa = oAP.ta201_idsubareapreventa;

                    soyLider = (int)context.Session["IDFICEPI_PC_ACTUAL"] == oAP.t001_idficepi_lider;

                    oPE = cUsuario.obtenerPerfilesEdicionUsuario(context.User, soyLider, ta201_idsubareapreventa);
                    oPE.soyComercial = false; //desde super el rol comercial no debe afectar
                }
                else
                {
                    oPE = new Models.PerfilesEdicion();
                    //oPE.soyAdministrador = false;
                    //oPE.soyFiguraSubareaActual = true;
                    //oPE.soyLider = false;
                    //oPE.soyPosibleLider = true;
                    //oPE.soyFiguraAreaActual = false;
                    //oPE.soyComercial = false;
                }
                #endregion


                switch (caso)
                {
                    //Acceso a detalle tarea como participante; G --> L
                    //modulo=SIC&caso=A&ta207_idtareapreventa=1331&ta204_idaccionpreventa=1576 --> bW9kdWxvPVNJQyZjYXNvPUEmdGEyMDdfaWR0YXJlYXByZXZlbnRhPTEzMzEmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc2
                    case "A":

                    //Acceso a detalle tarea que ha cambiado y estoy como participante; G --> L
                    //modulo=SIC&caso=H&ta207_idtareapreventa=1331&ta204_idaccionpreventa=1576 --> bW9kdWxvPVNJQyZjYXNvPUgmdGEyMDdfaWR0YXJlYXByZXZlbnRhPTEzMzEmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc2
                    case "H":
                        if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual && !soyParticipante)
                            throw new UnauthorizedAccessException();

                        Shared.HistorialNavegacion.Insertar(url + "Tarea/CatalogoParticipante/Default.aspx", true);

                        pantalla = "./Tarea/DetalleTarea/Default.aspx";
                        qs = Shared.Utils.codpar("idTarea=" + ht["ta207_idtareapreventa"].ToString() + "&idAccion=" + ht["ta204_idaccionpreventa"].ToString() + "&modoPantalla=E");
                        break;

                    //Acceso a detalle accion para autoasignar lider; E --> K
                    //modulo=SIC&caso=B&ta204_idaccionpreventa=1576 --> bW9kdWxvPVNJQyZjYXNvPUImdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc3
                    case "B":
                        if (!oPE.soyPosibleLider) throw new UnauthorizedAccessException();

                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoPosibleLider/Default.aspx", true);

                        pantalla = "./Accion/Detalle/Default.aspx";
                        qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=autoasignacion");
                        break;

                    //Asignar lider a acción; C --> K
                    //modulo=SIC&caso=C&ta204_idaccionpreventa=1577 --> bW9kdWxvPVNJQyZjYXNvPUMmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc3
                    case "C":

                        if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual)
                            throw new UnauthorizedAccessException();

                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoPdteLider/Default.aspx?b3JpZ2VubWVudT1TSUM=", true); //origenmenu=SIC

                        pantalla = "./Accion/Detalle/Default.aspx";
                        qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=catalogopdtelider");
                        break;

                    //Nuevo documento adjunto a accion. Acceso a detalle de acción desde catalogofigareaasubarea (establecer filtros) D --> K
                    //modulo=SIC&caso=D&ta204_idaccionpreventa=1577 --> bW9kdWxvPVNJQyZjYXNvPUQmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc3
                    case "D":

                        if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual)
                            throw new UnauthorizedAccessException();

                        cSolicitud = new BLL.SolicitudPreventa();
                        try
                        {
                            o = cSolicitud.getSolicitudbyAccion2(int.Parse(ht["ta204_idaccionpreventa"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            cSolicitud.Dispose();
                        }

                        qs = "origenmenu=SIC&filters=ejecutar:true|estado:A|itemorigen:" + o.ta206_itemorigen + "|iditemorigen:" + o.ta206_iditemorigen + "#" + o.ta206_denominacion;
                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoFigAreaSubarea/Default.aspx?" + Shared.Utils.codpar(qs), true); //origenmenu=SIC

                        pantalla = "./Accion/Detalle/Default.aspx";
                        qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=CatalogoFigAreaSubarea");

                        break;

                    //Lideres que han causado baja con las accion afectadas. Acceso a detalle de acción desde catalogofigareaasubarea desde menu ADM (catalogo heterogeneo, no se pueden establecer filtros) D --> K
                    //modulo=SIC&caso=E&ta204_idaccionpreventa=1577 --> bW9kdWxvPVNJQyZjYXNvPUUmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc3
                    case "E":
                        if (!oPE.soyAdministrador) throw new UnauthorizedAccessException();

                        qs = "origenmenu=ADM&filters=ejecutar:true|estado:A";
                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoFigAreaSubarea/Default.aspx?" + Shared.Utils.codpar(qs), true);

                        pantalla = "./Accion/Detalle/Default.aspx";
                        qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=CatalogoFigAreaSubarea");

                        break;

                    //Acceso a tarea preventa que ha finalizado o anulado; D --> K --> I --> L
                    //modulo=SIC&caso=F&ta207_idtareapreventa=1331&ta204_idaccionpreventa=1576 --> bW9kdWxvPVNJQyZjYXNvPUYmdGEyMDdfaWR0YXJlYXByZXZlbnRhPTEzMzEmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc2
                    case "F":

                    //Acceso a tarea preventa que ha cambiado estado de participacion (mismo caso que F); D --> K --> I --> L
                    //modulo=SIC&caso=G&ta207_idtareapreventa=1331&ta204_idaccionpreventa=1576 --> bW9kdWxvPVNJQyZjYXNvPUYmdGEyMDdfaWR0YXJlYXByZXZlbnRhPTEzMzEmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc2
                    case "G":

                    //Acceso a tarea en la que ha causado baja un participante (mismo caso que F); D --> K --> I --> L
                    //modulo=SIC&caso=I&ta207_idtareapreventa=1331&ta204_idaccionpreventa=1576 --> bW9kdWxvPVNJQyZjYXNvPUkmdGEyMDdfaWR0YXJlYXByZXZlbnRhPTEzMzEmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc2
                    case "I":

                        if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual)
                            throw new UnauthorizedAccessException();

                        cSolicitud = new BLL.SolicitudPreventa();
                        try
                        {
                            o = cSolicitud.getSolicitudbyAccion2(int.Parse(ht["ta204_idaccionpreventa"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            cSolicitud.Dispose();
                        }

                        //D
                        qs = "origenmenu=SIC&filters=ejecutar:true|estado:A|itemorigen:" + o.ta206_itemorigen + "|iditemorigen:" + o.ta206_iditemorigen + "#" + o.ta206_denominacion;
                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoFigAreaSubarea/Default.aspx?" + Shared.Utils.codpar(qs), true);

                        //K
                        qs = "id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=CatalogoFigAreaSubarea";
                        Shared.HistorialNavegacion.Insertar(url + "Accion/Detalle/Default.aspx?" + Shared.Utils.codpar(qs), true);

                        //I
                        qs = "id=" + ht["ta204_idaccionpreventa"].ToString() + "&origenpantalla=SUPER";
                        Shared.HistorialNavegacion.Insertar(url + "Tarea/CatalogoPorAccion/Default.aspx?" + Shared.Utils.codpar(qs), true);

                        //L
                        pantalla = "./Tarea/DetalleTarea/Default.aspx";
                        qs = Shared.Utils.codpar("idTarea=" + ht["ta207_idtareapreventa"].ToString() + "&idAccion=" + ht["ta204_idaccionpreventa"].ToString() + "&modoPantalla=E");

                        break;

                    //Accion preventa que ha finalizado o anulado (para el promotor) D --> K
                    //modulo=SIC&caso=JKD&ta204_idaccionpreventa=1577 --> bW9kdWxvPVNJQyZjYXNvPUpLRCZ0YTIwNF9pZGFjY2lvbnByZXZlbnRhPTE1Nzc=
                    case "JKD":
                        if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual)
                            throw new UnauthorizedAccessException();

                        cSolicitud = new BLL.SolicitudPreventa();
                        try
                        {
                            o = cSolicitud.getSolicitudbyAccion2(int.Parse(ht["ta204_idaccionpreventa"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            cSolicitud.Dispose();
                        }

                        qs = "origenmenu=SIC&filters=ejecutar:true|estado:A|itemorigen:" + o.ta206_itemorigen + "|iditemorigen:" + o.ta206_iditemorigen + "#" + o.ta206_denominacion;
                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoFigAreaSubarea/Default.aspx?" + Shared.Utils.codpar(qs), true); //origenmenu=SIC

                        pantalla = "./Accion/Detalle/Default.aspx";
                        qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=CatalogoFigAreaSubarea");

                        break;

                    //Accion preventa que ha finalizado o anulado (para el lider) F --> K
                    //modulo=SIC&caso=JKF&ta204_idaccionpreventa=1577 --> bW9kdWxvPVNJQyZjYXNvPUpLRiZ0YTIwNF9pZGFjY2lvbnByZXZlbnRhPTE1Nzc=
                    case "JKF":
                        if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual)
                            throw new UnauthorizedAccessException();

                        qs = "filters=idaccion:" + ht["ta204_idaccionpreventa"].ToString();
                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoComoLider/Default.aspx?" + Shared.Utils.codpar(qs), true);

                        pantalla = "./Accion/Detalle/Default.aspx";
                        qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=CatalogoComoLider");

                        break;

                    //has sido designado lider de accion. F--> K
                    //modulo=SIC&caso=K&ta204_idaccionpreventa=1577 --> bW9kdWxvPVNJQyZjYXNvPUsmdGEyMDRfaWRhY2Npb25wcmV2ZW50YT0xNTc3
                    case "K":
                        if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual)
                            throw new UnauthorizedAccessException();

                        qs = "filters=idaccion:" + ht["ta204_idaccionpreventa"].ToString();
                        Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoComoLider/Default.aspx?" + Shared.Utils.codpar(qs), true);

                        pantalla = "./Accion/Detalle/Default.aspx";
                        qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=CatalogoComoLider");

                        break;

                }

            }
            catch (UnauthorizedAccessException uaex)
            {
                Shared.LogError.LogearError("Intento de acceso no permitido. qs=" + context.Request.QueryString.ToString(), uaex);
                pantalla = context.Session["strServer"] + "bsNoAutorizado.aspx";
                qs = "";
            }
            catch (Exception ex)
            {
                Shared.LogError.LogearError("Error en redireccionamiento. qs=" + context.Request.QueryString.ToString(), ex);
                pantalla = context.Session["strServer"] + "Capa_Presentacion/bsInicio/Default.aspx";
            }
            finally
            {
                cUsuario.Dispose();
                if (cAP != null) cAP.Dispose();
                if (cTP != null) cTP.Dispose();
            }


            string sScript = "<script language='Javascript'>javascript:location.href=\'" + pantalla + "?" + qs + "\';</script>";
            context.Response.Clear();
            context.Response.ContentType = "text/html";
            context.Response.Write(sScript);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}