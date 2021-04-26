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
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public byte idTipo = 0;

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
                idTipo = byte.Parse(Request.QueryString["SN4"].ToString());
                if (idTipo > 0) CargarDatos(idTipo);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la estructura", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    private void CargarDatos(byte idTipo)
    {
        TIPOLOGIAPROY oTipo = TIPOLOGIAPROY.Select(tr, idTipo);
        txtDenominacion.Text = oTipo.t320_denominacion;
        hdnIDSN4.Text = oTipo.t320_idtipologiaproy.ToString();

        if ((bool)oTipo.t320_facturable) chkFacturable.Checked = true;
        else chkFacturable.Checked = false;

        if ((bool)oTipo.t320_interno) chkInterno.Checked = true;
        else chkInterno.Checked = false;

        if ((bool)oTipo.t320_especial) chkEspecial.Checked = true;
        else chkEspecial.Checked = false;

        if ((bool)oTipo.t320_requierecontrato) chkReqContr.Checked = true;
        else chkReqContr.Checked = false;

        chkAlertas.Checked = (bool)oTipo.t320_creaalertas;

        txtOrden.Text = oTipo.t320_orden.ToString();
    }
    private string Grabar(string strDatosBasicos)
    {
        string sResul = "";
        byte nID = 0;
        string[] aDatosBasicos = null;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
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
            #region Datos Generales
            if (strDatosBasicos != "")
            {
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = ID
                ///aDatosBasicos[1] = Denominacion
                ///aDatosBasicos[2] = facturable
                ///aDatosBasicos[3] = interno
                ///aDatosBasicos[4] = especial
                ///aDatosBasicos[5] = requiere contrato
                ///aDatosBasicos[6] = Orden
                ///aDatosBasicos[7] = Alertas
                ///
                if (aDatosBasicos[0] == "") //insert
                {
                    nID = TIPOLOGIAPROY.Insert(tr, Utilidades.unescape(aDatosBasicos[1]),
                        (aDatosBasicos[2] == "1") ? true : false,
                        (aDatosBasicos[3] == "1") ? true : false,
                        (aDatosBasicos[4] == "1") ? true : false,
                        (aDatosBasicos[5] == "1") ? true : false,
                        byte.Parse(aDatosBasicos[6]),
                        (aDatosBasicos[7] == "1") ? true : false
                        );
                }
                else //update
                {
                    nID = byte.Parse(aDatosBasicos[0]);
                    TIPOLOGIAPROY.Update(tr, nID, Utilidades.unescape(aDatosBasicos[1]),
                                (aDatosBasicos[2] == "1") ? true : false,
                                (aDatosBasicos[3] == "1") ? true : false,
                                (aDatosBasicos[4] == "1") ? true : false,
                                (aDatosBasicos[5] == "1") ? true : false,
                                byte.Parse(aDatosBasicos[6]),
                                (aDatosBasicos[7] == "1") ? true : false
                                );
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del elemento de estructura", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
