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
            Master.sbotonesOpcionOn = "50";
            Master.sbotonesOpcionOff = "50";
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            try
            {
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.TituloPagina = "Cierre de masivo de tareas de un proyecto econ�mico";
                Master.bFuncionesLocales = true;
                Utilidades.SetEventosFecha(this.txtValIni);
                Utilidades.SetEventosFecha(this.txtValFin);
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
                sResultado += ObtenerPEs(aArgs[1], aArgs[2]);
                break;
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
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
    private string ObtenerPEs(string sNumPE, string sNumPSN)
    {
        string sResul = "";

        try
        {
            //Obtengo los datos del proyecto

            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pst", int.Parse(sNumPE), (int)Session["UsuarioActual"], false, false, false);
            while (dr.Read())
            {
                if (int.Parse(dr["t305_idproyectosubnodo"].ToString()) == int.Parse(sNumPSN))
                {
                    sResul = "OK@#@" +
                            Utilidades.escape(dr["t301_denominacion"].ToString()) + "##" +
                            Utilidades.escape(dr["t302_denominacion"].ToString()) + "##" +
                            dr["rtpt"].ToString();
                }
            }
            if (sResul == "") sResul = "error@#@No se ha obtenido ningun dato de PROYECTO.";
            return sResul;
        }
        catch (Exception ex)
        {
            if (ex.Message == "No se ha obtenido ningun dato de PROYECTO")
                return "error@#@Proyecto no encontrado.";
            else
                return "error@#@Error al obtener el Proyecto Econ�mico./n " + ex.Message;
        }
    }
    private string Procesar(string sNumPSN, string sRTPT, string sParalizada, string sActiva, string sPendiente, string sFinalizada, string sFecIniVig, string sFecFinVig)
    {
        bool bParalizada = false, bActiva = false, bPendiente = false, bFinalizada = false, bRTPT = false;
        DateTime? dIniV = null;
        DateTime? dFinV = null;

        string sResul = "";
        int iTareasAfectadas = 0;
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
            if (sRTPT == "1") bRTPT = true;
            if (sParalizada == "1") bParalizada = true;
            if (sActiva == "1") bActiva = true;
            if (sPendiente == "1") bPendiente = true;
            if (sFinalizada == "1") bFinalizada = true;
            if (sFecIniVig != "") dIniV = DateTime.Parse(sFecIniVig);
            if (sFecFinVig != "") dFinV = DateTime.Parse(sFecFinVig);

            iTareasAfectadas = TAREAPSP.Cierre(tr, int.Parse(sNumPSN), (int)Session["UsuarioActual"], bRTPT, bParalizada, bActiva, bPendiente, bFinalizada, dIniV, dFinV);
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + iTareasAfectadas.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "error@#@Error al actualizar los estados de las tareas del proyecto subnodo " + sNumPSN + "\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}