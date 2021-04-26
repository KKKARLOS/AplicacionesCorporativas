using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores;
    public string sIdPeticionario;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2� Se "registra" la funci�n que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

            if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            sErrores = "";

            if (Request.QueryString["p"] != null)
            {
                sIdPeticionario = Utilidades.decodpar(Request.QueryString["p"].ToString());

                if (sIdPeticionario != "")
                {
                    try
                    {
                        this.nIdPeticionario.Value = sIdPeticionario;
                        GetPeticionario(sIdPeticionario);
                    }
                    catch (Exception ex)
                    {
                        sErrores += Errores.mostrarError("Error al obtener los datos de la petici�n", ex);
                    }
                }
            }
            if (Request.QueryString["a"] != null)
                this.txtAptdo.Text = Utilidades.decodpar(Request.QueryString["a"].ToString());
            if (Request.QueryString["e"] != null)
                this.txtElem.Text = Utilidades.decodpar(Request.QueryString["e"].ToString());
            if (Request.QueryString["n"] != null)
                this.txtProf.Text = Utilidades.decodpar(Request.QueryString["n"].ToString());

            if (Request.QueryString["t"] != null)
                this.hdnTipo.Value = Request.QueryString["t"].ToString();
            if (Request.QueryString["k"] != null)
                this.hdnKey.Value = Utilidades.decodpar(Request.QueryString["k"].ToString());
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
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
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

    private bool GetPeticionario(string sIdPeticionario)
    {
        this.txtSolic.Text = USUARIO.GetNombreProfesional(int.Parse(sIdPeticionario));

        return true;
    }

    protected string Grabar(int idPeticionario, string sTipo, string sKey, string sMotivo, string sDatosCorreo)
    {
        string sResul = "";

        #region abrir conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
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
            switch (sTipo)
            {
                case "FR"://formaci�n recibida
                    string[] aKey = Regex.Split(sKey, "##");
                    SUPER.BLL.Curso.PedirBorradoRecibido(int.Parse(aKey[0]), int.Parse(aKey[1]), idPeticionario, Utilidades.unescape(sMotivo), Utilidades.unescape(sDatosCorreo));
                    break;
                case "FI"://formaci�n impartida
                    SUPER.BLL.Curso.PedirBorradoImpartido(int.Parse(sKey), idPeticionario, Utilidades.unescape(sMotivo), Utilidades.unescape(sDatosCorreo));
                    break;
                case "EI"://Experiencia profesional en Iberm�tica
                    SUPER.BLL.EXPPROFFICEPI.PedirBorrado(int.Parse(sKey), idPeticionario, Utilidades.unescape(sMotivo), Utilidades.unescape(sDatosCorreo), false);
                    break;
                case "PE"://Perfil de Experiencia profesional
                    //Si estamos solicitando eliminar un perfil es porque �ste viene de plantilla, por tanto la experiencia de ese profesional
                    //tendr� un �nico perfil, lo que implica que la petici�n se traduce en una petici�n de borrado de la experiencia de ese profesional
                    SUPER.BLL.EXPPROFFICEPI.PedirBorrado(int.Parse(sKey), idPeticionario, Utilidades.unescape(sMotivo), Utilidades.unescape(sDatosCorreo), true);
                    break;
                case "EX"://Examen
                    string[] aKey2 = Regex.Split(sKey, "##");
                    SUPER.BLL.Examen.PedirBorrado(int.Parse(aKey2[0]), int.Parse(aKey2[1]), idPeticionario, Utilidades.unescape(sMotivo), Utilidades.unescape(sDatosCorreo));
                    break;
                case "CE"://Certificados
                    string[] aKey3 = Regex.Split(sKey, "##");
                    SUPER.BLL.Certificado.PedirBorrado(int.Parse(aKey3[0]), int.Parse(aKey3[1]), idPeticionario, Utilidades.unescape(sMotivo), Utilidades.unescape(sDatosCorreo));
                    break;
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    //protected string EnviarCorreoApertura(string strDatosTarea, string sIdTarea)
    //{
    //    string sResul = "";
    //    ArrayList aListCorreo = new ArrayList();
    //    StringBuilder sbuilder = new StringBuilder();
    //    string sAsunto = "";
    //    string sTexto = "";
    //    string sTO = "";

    //    try
    //    {
    //        sAsunto = "Apertura de tarea.";

    //        string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
    //        //Hay que notificar el fin de la tarea al origen de la misma.
    //        sbuilder.Append(@"<BR>SUPER le informa de la apertura de la siguiente tarea:<BR><BR>");
    //        sbuilder.Append("<label style='width:120px'>Proyecto econ�mico: </label>" + aDatosTarea[2] + @" - " + Utilidades.unescape(aDatosTarea[3]) + "<br>");
    //        sbuilder.Append("<label style='width:120px'>Proyecto T�cnico: </label>" + Utilidades.unescape(aDatosTarea[4]) + "<br>");

    //        if (aDatosTarea[5] != "") sbuilder.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(aDatosTarea[5]) + "<br>");
    //        if (aDatosTarea[6] != "") sbuilder.Append("<label style='width:120px'>Actividad: " + Utilidades.unescape(aDatosTarea[6]) + "<br>");

    //        //sbuilder.Append("<label style='width:120px'>Tarea: </label><b>" + aDatosTarea[0] + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
    //        sbuilder.Append("<label style='width:120px'>Tarea: </label><b>" + int.Parse(sIdTarea.Replace(".", "")).ToString("#,###") + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
    //        sbuilder.Append("<b>Informaci�n de la tarea:</b><br><br>");

    //        if (aDatosTarea[7] != "")
    //            sbuilder.Append("<label style='width:120px'>OTC: </label>" + Utilidades.unescape(aDatosTarea[7]) + " - " + Utilidades.unescape(aDatosTarea[8]) + "<br>");
    //        if (aDatosTarea[9] != "")
    //            sbuilder.Append("<label style='width:120px'>OTL: </label>" + Utilidades.unescape(aDatosTarea[9]) + "<br>");
    //        if (aDatosTarea[10] != "")
    //            sbuilder.Append("<label style='width:120px'>Incidencia/Petici�n: </label>" + Utilidades.unescape(aDatosTarea[10]) + "<br>");

    //        sTO = Utilidades.unescape(aDatosTarea[11]);
    //        sTO = sTO.Replace(";", @"/");
    //        sTexto = sbuilder.ToString();

    //        string[] aMail = { sAsunto, sTexto, sTO };
    //        aListCorreo.Add(aMail);

    //        Correo.EnviarCorreos(aListCorreo);

    //        sResul = "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de apertura de tarea.", ex);
    //    }
    //    return sResul;
    //}

}
