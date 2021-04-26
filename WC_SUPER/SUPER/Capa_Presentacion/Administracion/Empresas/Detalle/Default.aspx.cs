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
                // Leer Empresa

                if (Request.QueryString["bNueva"] != "true")
                {
                    hdnID.Text = Request.QueryString["ID"].ToString();
                    EMPRESA oEMPRESA = EMPRESA.Select(null, int.Parse(hdnID.Text));
                    txtCodigoExterno.Text = oEMPRESA.t302_codigoexterno;
                    txtDenominacion.Text = oEMPRESA.t313_denominacion;

                    if ((bool)oEMPRESA.t313_estado) chkActiva.Checked = true;
                    else chkActiva.Checked = false;

                    if ((bool)oEMPRESA.t313_ute) chkUTE.Checked = true;
                    else chkUTE.Checked = false;

                    txtHorasAnu.Text = float.Parse(oEMPRESA.t313_horasanuales.ToString()).ToString("##,###,###");
                    txtInteresesGF.Text = float.Parse(oEMPRESA.t313_interesGF.ToString()).ToString("###,###.##");
                    txtCCIF.Text = oEMPRESA.t313_CCIF;
                    txtCCIE.Text = oEMPRESA.t313_CCICE;
                    hdnIDDieta.Text = oEMPRESA.T069_iddietakm.ToString();
                    txtDesDieta.Text = oEMPRESA.t069_descripcion;
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la empresa", ex);
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
                sResultado += Grabar(
                                    byte.Parse(aArgs[1]),			                        // 0=UPDATE 1=INSERT
                                    int.Parse(aArgs[2]),                                    // Id Empresa
                                    Utilidades.unescape(aArgs[3]),                        // CódigoExterno(SAP) 
                                    Utilidades.unescape(aArgs[4]),                        // Denominanción Empresa
                                    aArgs[5],                                               // UTE
                                    float.Parse((aArgs[6]=="")? "0":aArgs[6]),              // Horas anuales
                                    float.Parse((aArgs[7]=="")? "0":aArgs[7]),              // Intereses gastos fros
                                    Utilidades.unescape(aArgs[8]),                        // CCIF
                                    Utilidades.unescape(aArgs[9]),                        // CCIE 
                                    int.Parse((aArgs[10] == "") ? "0" : aArgs[10]),          // Dieta KM 
                                    aArgs[11]                                               //Activa
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

     private string Grabar  (
                            byte byteNueva,
                            int iID, 
                            string sCodigoExterno, 
                            string sDenominacion, 
                            string sUTE, 
                            float fHorasAnuales,
                            float fIntGtosFros, 
                            string sCCIF, 
                            string sCCIE,
                            int iDieta,
                            string sActiva
         )
    {
        string sResul = "";
        int nID = -1;
        int? intDieta = null;
        if (iDieta != 0) intDieta = iDieta;

        bool bActiva;
        if (sActiva == "1") bActiva = true;
        else bActiva = false;

        bool bUTE;
        if (sUTE == "1") bUTE = true;
        else bUTE = false;
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
            #region Datos Generales


            if (byteNueva == 1)
            {
                nID = EMPRESA.Insert
                                (
                                tr,
                                sDenominacion,
                                sCodigoExterno,
                                bUTE,
                                fHorasAnuales,
                                fIntGtosFros,
                                sCCIF,
                                sCCIE,
                                intDieta,
                                bActiva
                                );
            }
            else //update
            {
                EMPRESA.Update  (
                                tr,
                                iID,
                                sDenominacion,
                                sCodigoExterno,
                                bUTE,
                                fHorasAnuales,
                                fIntGtosFros,
                                sCCIF,
                                sCCIE,
                                intDieta,
                                bActiva
                                );
            }


            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la empresa", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
