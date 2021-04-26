<%@ WebHandler Language="C#" Class="IB.SUPER.CVT.redir" %>

using System;
using System.Web;
using System.Collections;
using System.Web.SessionState;

namespace IB.SUPER.CVT
{
    public class redir : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            string pantalla = "";
            string qs = "";
            string url = context.Session["strServer"] + "Capa_Presentacion/CVT/";

            try
            {
                Hashtable ht = Shared.Utils.ParseQuerystring(context.Request.QueryString.ToString());
                string caso = ht["caso"].ToString();

                Shared.HistorialNavegacion.Resetear();

                switch (caso)
                {
                    //Acceso a MiCV
                    case "1":
                        //if (!oPE.soyAdministrador && !oPE.soyFiguraSubareaActual && !oPE.soyLider && !oPE.soyFiguraAreaActual && !soyParticipante)
                        //    throw new UnauthorizedAccessException();
                        //Shared.HistorialNavegacion.Insertar(url + "MiCV/Default.aspx", true);
                        pantalla = "./MiCV/Default.aspx";
                        //qs = Shared.Utils.codpar("idTarea=" + ht["ta207_idtareapreventa"].ToString() + "&idAccion=" + ht["ta204_idaccionpreventa"].ToString() + "&modoPantalla=E");
                        qs = "";
                        break;

                    //Acceso a proyectos pendientes de cualificar
                    case "2":
                        //if (!oPE.soyPosibleLider) throw new UnauthorizedAccessException();
                        //Shared.HistorialNavegacion.Insertar(url + "Accion/CatalogoPosibleLider/Default.aspx", true);
                        pantalla = "./Cualificacion/Default.aspx";
                        //qs = Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&modo=E&origenpantalla=SUPER&caller=autoasignacion");
                        qs = "";
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