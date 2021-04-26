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
    public string sOrigen = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Utilidades.SetEventosFecha(this.txtFecha);
        ObtenerSegmentos();

        if (!Page.IsCallback)
        {
            try
            {
                if (Request.QueryString["bNueva"] == "true" && hdnNueva.Text!="false") hdnNueva.Text = "true";

                // Leer cuenta

                if (hdnNueva.Text != "true")
                {
                    hdnID.Text = Request.QueryString["ID"].ToString();
                    CUENTASCUR oCUENTASCUR = CUENTASCUR.Obtener(null, int.Parse(hdnID.Text));
                    txtDenominacion.Text = oCUENTASCUR.cu_nombre;
                    txtVN.Text = oCUENTASCUR.cu_vn.ToString("N");
                    chkEsCliente.Checked = oCUENTASCUR.cu_escliente;
                    txtFecha.Text = (oCUENTASCUR.cu_fecha.HasValue) ? ((DateTime)oCUENTASCUR.cu_fecha).ToShortDateString() : "";
                    cboSegmento.SelectedValue = oCUENTASCUR.t484_idsegmento.ToString();
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la cuenta", ex);
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
        sResultado = aArgs[0] + @"@#@";

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Figuras
                        sResultado += "OK@#@1@#@"; // "obtenerFigurasItem(aArgs[1]);
                        break;
                }
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
    public void ObtenerSegmentos()
    {
        SqlDataReader dr = SUPER.Capa_Negocio.SEGMENTO.Catalogo(null,1);
        ListItem oLI = null;
        while (dr.Read())
        {
            oLI = new ListItem(dr["T484_DENOMINACION"].ToString(), dr["T484_IDSEGMENTO"].ToString());
            cboSegmento.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();
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
            if (strDatosBasicos != "") //No se ha modificado nada de la pestaña general
            {
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = ID
                ///aDatosBasicos[1] = Denominacion
                ///aDatosBasicos[2] = Valor de Negocio
                ///aDatosBasicos[3] = Es cliente
                ///aDatosBasicos[4] = Fecha
                ///aDatosBasicos[5] = Segmento

                if (aDatosBasicos[0] == "0") //insert
                {
                    nID = SUPER.DAL.CUENTASCUR.Insert
                                    (
                                    tr,
                                    Utilidades.unescape(aDatosBasicos[1]),
                                    Decimal.Parse(aDatosBasicos[2]),
                                    (aDatosBasicos[3]=="1")? true : false,
                                    (aDatosBasicos[4]=="")? null : (DateTime?)DateTime.Parse(aDatosBasicos[4]),
                                    (aDatosBasicos[5] == "") ? null : (int?)int.Parse(aDatosBasicos[5])
                                    );
                }
                else //update
                {
                    nID = int.Parse(aDatosBasicos[0]);
                    SUPER.DAL.CUENTASCUR.Update(tr,
                                int.Parse(aDatosBasicos[0]),
                                    Utilidades.unescape(aDatosBasicos[1]),
                                    Decimal.Parse(aDatosBasicos[2]),
                                    (aDatosBasicos[3] == "1") ? true : false,
                                    (aDatosBasicos[4] == "") ? null : (DateTime?)DateTime.Parse(aDatosBasicos[4]),
                                    (aDatosBasicos[5] == "") ? null : (int?)int.Parse(aDatosBasicos[5])
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
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la división horizontal", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
