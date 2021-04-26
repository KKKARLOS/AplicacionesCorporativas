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
    public byte SN4 = 0;
    public int SN3 = 0, SN2 = 0;

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
                SN4 = byte.Parse(Request.QueryString["SN4"].ToString());
                SN3 = int.Parse(Request.QueryString["SN3"].ToString());
                SN2 = int.Parse(Request.QueryString["SN2"].ToString());
                CargarDatosEstructura();
                if (SN2 > 0) CargarDatosItem(SN2);

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

    private void CargarDatosEstructura()
    {
        TIPOLOGIAPROY oSN4Aux = TIPOLOGIAPROY.Select(tr, (byte)SN4);
        txtDesSN4.Text = oSN4Aux.t320_denominacion;
        hdnIDSN4.Text = oSN4Aux.t320_idtipologiaproy.ToString();

        GRUPONAT oGr = GRUPONAT.Select(tr, SN3);
        txtDesSN3.Text = oGr.t321_denominacion;
        hdnIDSN3.Text = oGr.t321_idgruponat.ToString();
    }
    private void CargarDatosItem(int idSubGrupoNat)
    {
        SUBGRUPONAT oGr = SUBGRUPONAT.Select(tr, idSubGrupoNat);
        txtDenominacion.Text = oGr.t322_denominacion;
        hdnIDSN2.Text = oGr.t322_idsubgruponat.ToString();

        if ((bool)oGr.t322_estado) chkActivo.Checked = true;
        else chkActivo.Checked = false;

        txtOrden.Text = oGr.t322_orden.ToString();
    }
    private string Grabar(string strDatosBasicos)
    {
        string sResul = "";
        int nID = -1;
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
            if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
            {
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = idTipologia
                ///aDatosBasicos[1] = idGrupo
                ///aDatosBasicos[2] = idSubGrupo
                ///aDatosBasicos[3] = Denominacion
                ///aDatosBasicos[4] = Activo
                ///aDatosBasicos[5] = Orden
                if (aDatosBasicos[2] == "") //insert
                {
                    nID = SUBGRUPONAT.Insert(tr, Utilidades.unescape(aDatosBasicos[3]), int.Parse(aDatosBasicos[1]),
                                          int.Parse(aDatosBasicos[5]), (aDatosBasicos[4] == "1") ? true : false);
                }
                else //update
                {
                    nID = int.Parse(aDatosBasicos[2]);
                    SUBGRUPONAT.Update(tr, nID, Utilidades.unescape(aDatosBasicos[3]), int.Parse(aDatosBasicos[1]),
                                    int.Parse(aDatosBasicos[5]), (aDatosBasicos[4] == "1") ? true : false);
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
