<%@ WebHandler Language="C#" Class="Login" %>

using System;
using System.Web;
using System.Collections;

using IB.SUPER.Shared;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using Shared = IB.SUPER.Shared;

public class Login : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        BLL.Usuario cUsuario = null;
        string url;

        try
        {
            //params --> user=JM_GRACIA&goto=detalleitem&itemorigen=O&iditemorigen=15


            //Hashtable ht = Utils.ParseQuerystring_nocod(context.Request.QueryString.ToString());
            Hashtable ht = Utils.ParseQuerystring(context.Request.QueryString.ToString());

            context.Response.ContentType = "text/plain";

            //TODO control de acceso a la aplicación
            //if (!AccesoApp(context)) context.Response.End();

            //Validación de parámetros
            if (ht["user"] == null) throw new Exception("400");
            if (ht["goto"] == null || ht["goto"].ToString() == "") throw new Exception("400");

            string user = ht["user"].ToString();

            //Durante las pruebas en produccion solo permitir a los siguientes comerciales
            //if (System.Configuration.ConfigurationManager.AppSettings["ENTORNO"] == "E" &&
            //    user != "J_ASENJO" &&
            //    user != "JM_GRACIA")
            //    throw new Exception("402");

            //autenticar usuario
            cUsuario = new BLL.Usuario();
            try
            {
                cUsuario.Autenticar(ht["user"].ToString());
            }
            catch (Exception)
            {
                throw new Exception("401");
            }


            //Establecer variables de sesion (no relacionadas con el usuario)
            string[] sUrlAux = System.Text.RegularExpressions.Regex.Split(context.Request.ServerVariables["URL"], "/");
            if (sUrlAux[1].ToUpper() != "SUPER") context.Session["strServer"] = "/";
            else context.Session["strServer"] = "/SUPER/";

            switch (ht["goto"].ToString().Trim().ToUpper())
            {
                case "DETALLEITEM":
                    if (ht["itemorigen"] == null || (ht["itemorigen"].ToString() != "O" && ht["itemorigen"].ToString() != "E" && ht["itemorigen"].ToString() != "P"))
                        throw new Exception("400");

                    if (ht["iditemorigen"] == null || (string)ht["iditemorigen"] == "")
                    {
                        if (ht["rowid"] == null || (string)ht["rowid"] == "") throw new Exception("400");

                        ht["iditemorigen"] = cUsuario.ObtenerIdItemPorRowId((string)ht["rowid"], (string)ht["itemorigen"]);
                        if ((int)ht["iditemorigen"] == -1) throw new Exception("400");
                    }

                    int iditemorigen;
                    bool isNumeric = int.TryParse(ht["iditemorigen"].ToString(), out iditemorigen);
                    if (!isNumeric) throw new Exception("400");

                    url = "Accion/CatalogoCRM/Default.aspx?";
                    url += Utils.codpar("itemorigen=" + ht["itemorigen"].ToString() + "&iditemorigen=" + ht["iditemorigen"].ToString());
                    break;

                case "MIPREVENTA":
                    url = context.Session["strServer"] + "Capa_Presentacion/SIC/Accion/AmbitoCRM/Default.aspx";
                    break;

                case "ACCIONPREVENTA":
                        //user=JA_TOCINO&goto=ACCIONPREVENTA&ta204_idaccionpreventa=1577 --> dXNlcj1KQV9UT0NJTk8mZ290bz1BQ0NJT05QUkVWRU5UQSZ0YTIwNF9pZGFjY2lvbnByZXZlbnRhPTE1Nzc=
                        BLL.SolicitudPreventa cSolicitud = new BLL.SolicitudPreventa();
                        Models.SolicitudPreventa o;
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

                        string qs = "itemorigen=" + o.ta206_itemorigen + "&iditemorigen=" + o.ta206_iditemorigen;
                        Shared.HistorialNavegacion.Insertar(context.Session["strServer"] + "Capa_Presentacion/SIC/Accion/CatalogoCRM/Default.aspx?" + Shared.Utils.codpar(qs), true); 
                        
                        url = context.Session["strServer"] + "Capa_Presentacion/SIC/Accion/Detalle/Default.aspx?";
                        url += Shared.Utils.codpar("id=" + ht["ta204_idaccionpreventa"].ToString() + "&itemorigen=" + o.ta206_itemorigen + "&iditemorigen=" + o.ta206_iditemorigen + "&modo=E&origenpantalla=CRM");

                        break;

                default:
                    throw new Exception("400");
            }
        }
        catch (Exception ex)
        {
            IB.SUPER.Shared.LogError.LogearError("Error de acceso a la preventa :: querystring=" + context.Request.QueryString.ToString(), ex);
            url = "Error.aspx?error=" + ex.Message;
        }
        finally
        {
            if (cUsuario != null) cUsuario.Dispose();
        }

        //navegar
        string s = "<html><body><script language='Javascript'>window.location.href=\'" + url + "\';</script></body></html>";
        context.Response.Clear();
        context.Response.ContentType = "text/html";
        context.Response.Write(s);
        context.Response.Flush();
        context.ApplicationInstance.CompleteRequest();


    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}