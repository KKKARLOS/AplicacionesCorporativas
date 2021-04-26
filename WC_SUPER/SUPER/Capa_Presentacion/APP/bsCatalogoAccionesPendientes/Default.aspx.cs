using IB.SUPER.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_bsCatalogoAccionesPendientes_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ObtenerAccionesPendientes()
    {

        IB.SUPER.Negocio.bsUsuario bsUsuariosBLL = null;
        List<IB.SUPER.Models.bsUsuario> lstbsUsuarioModels = null;
        try
        {
            int? idFicepi_cvt = null;
            int? idFicepi_pc = null;
            int? idUser = null;

            HttpContext.Current.Session["BloquearPGEByAcciones"] = false;
            HttpContext.Current.Session["BloquearPSTByAcciones"] = false;
            HttpContext.Current.Session["BloquearIAPByAcciones"] = false;


            if (HttpContext.Current.Session["IDFICEPI_ENTRADA"] != null)
            {
                //Para distinguir usuarios que se pueden reconectar en el mundo cvt y super
                if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString() ||
                    HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString())
                {//Hay reconexión como otro
                    if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString())
                        idFicepi_cvt = int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString());

                    if (HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString() != HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString())
                    {
                        idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                        idFicepi_pc = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());
                    }
                }
                else
                {
                    idFicepi_cvt = int.Parse(HttpContext.Current.Session["IDFICEPI_CVT_ACTUAL"].ToString());
                    idFicepi_pc = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());
                    idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                }
            }

            bsUsuariosBLL = new IB.SUPER.Negocio.bsUsuario();

            lstbsUsuarioModels = bsUsuariosBLL.Catalogo(idUser, idFicepi_cvt, idFicepi_pc);
            bsUsuariosBLL.Dispose();

            string retval = JsonConvert.SerializeObject(lstbsUsuarioModels);

            return retval;

        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener las acciones pendientes", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener las acciones pendientes."));
        }

        finally
        {
            if (bsUsuariosBLL != null) bsUsuariosBLL.Dispose();
        }

    }
}