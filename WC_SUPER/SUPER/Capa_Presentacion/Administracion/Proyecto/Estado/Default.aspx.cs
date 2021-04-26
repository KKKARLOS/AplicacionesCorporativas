using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Apertura / Cierre de Proyecto Econ�mico";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerPEs(aArgs[1]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
    private string ObtenerPEs(string sNumPE)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            //Obtengo los datos del proyecto
            PROYECTO oProy = PROYECTO.Obtener(tr, int.Parse(sNumPE));

            return "OK@#@" + 
                    Utilidades.escape(oProy.t301_denominacion) + "##" +
                    Utilidades.escape(oProy.t302_denominacion) + "##" +
                    oProy.t301_estado + "##" + oProy.t301_categoria;
        }
        catch (Exception ex)
        {
            if (ex.Message == "No se ha obtenido ningun dato de PROYECTO")
                return "error@#@Proyecto no encontrado.";
            else
                return "error@#@Error al obtener el Proyecto Econ�mico./n " + ex.Message;
        }
    }
    private string Grabar(string sNumPE, string sEstado)
    {
        string sResul = "OK@#@";
        #region abrir conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion

        try
        {
            if (sNumPE != "")
            {
                if (sEstado == "C")
                {
                    int nPE = int.Parse(sNumPE);
                    SUPER.DAL.PROYECTO.CierreTecnico(tr, nPE);
                    //Si hay consumos IAP y no existe mes -> crear mes, traspasar IAP y cerrar mes
                    PROYECTO.TraspasarIAP(tr, nPE, true);
                }

                PROYECTO.Update(tr, int.Parse(sNumPE), sEstado);
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //Master.sErrores = Errores.mostrarError("Error al eliminar el proyecto econ�mico " + sNumPE, ex);
            sResul = "error@#@Error al actualizar el estado del proyecto econ�mico " + sNumPE + "\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}