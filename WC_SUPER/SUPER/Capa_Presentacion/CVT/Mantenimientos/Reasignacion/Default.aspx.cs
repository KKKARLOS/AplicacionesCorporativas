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

using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, strHTMLProfesionales = "", strHTMLDestino="";
    public SqlConnection oConn;
    public SqlTransaction tr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                //E:entornos tecnologicos, T:titulaciones, C:clientes no Ibermatica
                hdnTipo.Value = Request.QueryString["t"].ToString();
                if (hdnTipo.Value != "E")
                {
                    imgPapelera.Visible = false;
                    tblTitulo2.Visible = false;
                    divCatalogo2.Visible = false;
                    tblPie2.Visible = false;
                    divCatalogo.Style.Add("height", "432px");
                }
                hdnOrigen.Value = Utilidades.decodpar(Request.QueryString["k"].ToString());
                this.txtOrigen.Text = Utilidades.decodpar(Request.QueryString["d"].ToString());

                strHTMLProfesionales = GetElementosAsociados(hdnTipo.Value, hdnOrigen.Value);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al cargar la pantalla de copiar proyectos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    private string GetElementosAsociados(string sTipo, string sId)
    {
        string sRes = "";
        try
        {//E:entornos tecnologicos, T:titulaciones, C:clientes no Ibermatica
            switch (sTipo)
            {
                case "E":
                    //sRes = SUPER.BLL.EntornoTecno.ProfesionalesAsociados(int.Parse(sId));
                    sRes = SUPER.BLL.EntornoTecno.ElementosAsociadoAReasignar(int.Parse(sId));
                    break;
                case "T":
                    //sRes = SUPER.BLL.Titulacion.ProfesionalesAsociados(int.Parse(sId));
                    sRes = SUPER.BLL.Titulacion.ElementosAsociadoAReasignar(int.Parse(sId));
                    break;
                case "C":
                    //sRes = SUPER.BLL.CuentasCVT.ProfesionalesAsociados(int.Parse(sId));
                    sRes = SUPER.BLL.CuentasCVT.ElementosAsociadoAReasignar(int.Parse(sId));
                    break;
            }
        }
        catch (Exception e)
        {
            sErrores += Errores.mostrarError("Error al obtener los elementos asociados", e);
        }

        return sRes;
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += getElementos(aArgs[1], Utilidades.unescape(aArgs[2]), aArgs[3]);
                break;
            case ("procesar"):
                sResultado += Procesar(aArgs[1], 			    // Tipo de elemento
                                        int.Parse(aArgs[2]),    // Id Origen
                                        aArgs[3]                // Datos destino
                                        );  
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string getElementos(string sTipo, string sDen, string sTipoBusqueda)
    {
        string sRes = "";
        try
        {//E:entornos tecnologicos, T:titulaciones, C:clientes no Ibermatica
            switch (sTipo)
            {
                case "E":
                    sRes = "OK@#@" + SUPER.BLL.EntornoTecno.CatalogoSimple(sDen, 1, sTipoBusqueda);//OBTENGO SOLO LOS VALIDADOS
                    break;
                case "T":
                    sRes = "OK@#@" + SUPER.BLL.Titulacion.CatalogoSimple(sDen, 1, sTipoBusqueda, true);//OBTENGO SOLO LOS VALIDADOS
                    break;
                case "C":
                    sRes = "OK@#@" + SUPER.BLL.CuentasCVT.CatalogoSimple(sDen, 1, sTipoBusqueda);//OBTENGO SOLO LOS VALIDADOS + clientes
                    break;
            }
        }
        catch (Exception e)
        {
            sErrores += Errores.mostrarError("Error al obtener los profesionales asociados", e);
        }

        return sRes;
    }

    private string Procesar(string sTipo, int idOrigen, string sDatosDestino)
    {
        string sResul = "";
        bool bErrorControlado = false;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            switch (sTipo)
            {
                case "E":
                    SUPER.BLL.EXPFICEPIENTORNO.Reasignar(tr, idOrigen, sDatosDestino);//idDestino
                    break;
                case "T":
                    if (idOrigen != int.Parse(sDatosDestino))
                        SUPER.BLL.TituloFicepi.Reasignar(tr, idOrigen, int.Parse(sDatosDestino));
                    break;
                case "C":
                    string[] aDatos = Regex.Split(sDatosDestino, "{sep}");
                    if (int.Parse(aDatos[0]) != int.Parse(aDatos[1]))
                        SUPER.BLL.EXPPROF.Reasignar(tr, idOrigen, int.Parse(aDatos[0]), int.Parse(aDatos[1]), Utilidades.unescape(aDatos[2]));
                    break;
            }

            //string[] aDatos = Regex.Split(sDatosDestino, "{sepreg}");
            ////E:entornos tecnologicos, T:titulaciones, C:clientes no Ibermatica
            //foreach (string sDato in aDatos)
            //{
            //    if (sDato == "") continue;
            //    string[] aValores = Regex.Split(sDato, "{sep}");
            //}

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar la reasignación", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
