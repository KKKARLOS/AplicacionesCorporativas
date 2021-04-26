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
    public int SN3 = 0, SN2 = 0, SN1 = 0;

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
                SN1 = int.Parse(Request.QueryString["SN1"].ToString());

                CargarDatosEstructura();
                if (SN1 > 0) CargarDatosItem(SN1);

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

        SUBGRUPONAT oSubGr = SUBGRUPONAT.Select(tr, SN2);
        txtDesSN2.Text = oSubGr.t322_denominacion;
        hdnIDSN2.Text = oSubGr.t322_idsubgruponat.ToString();
    }
    private void CargarDatosItem(int idNat)
    {
        NATURALEZA oNat = NATURALEZA.Select(tr, idNat);
        txtDenominacion.Text = oNat.t323_denominacion;
        hdnIDSN1.Text = oNat.t323_idnaturaleza.ToString();

        if ((bool)oNat.t323_regfes) chkRegFes.Checked = true;
        else chkRegFes.Checked = false;

        if ((bool)oNat.t323_regjornocompleta) chkRegJor.Checked = true;
        else chkRegJor.Checked = false;

        if ((bool)oNat.t323_coste) chkCoste.Checked = true;
        else chkCoste.Checked = false;

        if ((bool)oNat.t323_estado) chkActivo.Checked = true;
        else chkActivo.Checked = false;

        if ((bool)oNat.t323_pasaaSAP) chkPasaSAP.Checked = true;
        else chkPasaSAP.Checked = false;

        txtOrden.Text = oNat.t323_orden.ToString();
        txtMesVig.Text = oNat.t323_mesesvigenciaPIG.ToString();
        hdnIDPlantilla.Text = oNat.t338_idplantilla.ToString();
        int idPlant;
        if (oNat.t338_idplantilla != null)
        {
            idPlant = (int)oNat.t338_idplantilla;
            PlantProy oPlant = PlantProy.Select(idPlant);
            txtDesPlantilla.Text = oPlant.descripcion;
        }
    }
    private string Grabar(string strDatosBasicos)
    {
        string sResul = "";
        int nID = -1;
        int? nIdPlant = null;
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
                ///aDatosBasicos[3] = idNaturaleza
                ///aDatosBasicos[4] = Denominacion
                ///aDatosBasicos[5] = registrar festivos
                ///aDatosBasicos[6] = jornada completa
                ///aDatosBasicos[7] = coste
                ///aDatosBasicos[8] = idPlantilla
                ///aDatosBasicos[9] = orden
                ///aDatosBasicos[10] = meses vigencia
                ///aDatosBasicos[11] = activo
                ///aDatosBasicos[12] = pasa a SAP
                
                if (aDatosBasicos[8] != "") nIdPlant = int.Parse(aDatosBasicos[8]);
                if (aDatosBasicos[3] == "") //insert
                {
                    nID = NATURALEZA.Insert(tr, Utilidades.unescape(aDatosBasicos[4]), int.Parse(aDatosBasicos[2]),
                                            (aDatosBasicos[5] == "1") ? true : false,
                                            (aDatosBasicos[6] == "1") ? true : false,
                                            (aDatosBasicos[7] == "1") ? true : false,
                                            nIdPlant, int.Parse(aDatosBasicos[9]), byte.Parse(aDatosBasicos[10]),
                                            (aDatosBasicos[11] == "1") ? true : false,
                                            (aDatosBasicos[12] == "1") ? true : false);
                }
                else //update
                {
                    nID = int.Parse(aDatosBasicos[3]);
                    NATURALEZA.Update(tr, nID, Utilidades.unescape(aDatosBasicos[4]), int.Parse(aDatosBasicos[2]),
                                        (aDatosBasicos[5] == "1") ? true : false,
                                        (aDatosBasicos[6] == "1") ? true : false,
                                        (aDatosBasicos[7] == "1") ? true : false,
                                        nIdPlant, int.Parse(aDatosBasicos[9]), byte.Parse(aDatosBasicos[10]),
                                        (aDatosBasicos[11] == "1") ? true : false,
                                        (aDatosBasicos[12] == "1") ? true : false);
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
