using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Actividad SUPER";
                Master.FuncionesJavaScript.Add("Javascript/FusionCharts.js");

                //1? Se indican (por este orden) la funci?n a la que se va a devolver el resultado
                //   y la funci?n que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2? Se "registra" la funci?n que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += obtenerDatos();
                break;
        }
        //3? Damos contenido a la variable que se env?a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env?a el resultado al cliente.
        return _callbackResultado;
    }

    private string obtenerDatos()
    {
        StringBuilder sb = new StringBuilder();
        int nUsuarios = 0;
        string sCODRED = "";
        try
        {
            
            if (HttpContext.Current.Cache.Get("UsuariosConectados") != null)
            {
                Hashtable htUsuarios = (Hashtable)HttpContext.Current.Cache.Get("UsuariosConectados");
                nUsuarios = htUsuarios.Count;
                foreach (string sUsuario in htUsuarios.Keys)
                {
                    sCODRED += (string)htUsuarios[sUsuario] + ",";
                }
            }

            SqlDataReader dr = Recurso.ObtenerUsuariosConectados(sCODRED);
            string sHora = DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
            while (dr.Read())
            {
                sb.Append(dr["Profesional"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sHora + "@#@" + nUsuarios.ToString() + "@#@" + Utilidades.escape(sb.ToString());// +"@#@" + sCODRED;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
        }
    }

}
