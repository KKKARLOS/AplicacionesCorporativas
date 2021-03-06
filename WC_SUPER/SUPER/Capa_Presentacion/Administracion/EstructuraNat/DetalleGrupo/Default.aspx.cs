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
    public string sErrores = "", bSetTipol="T";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public byte SN4 = 0;
    public int SN3 = 0;

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
                CargarDatosEstructura();
                if (SN3 > 0) CargarDatosItem(SN3);

            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la estructura", ex);
            }

            //1? Se indican (por este orden) la funci?n a la que se va a devolver el resultado
            //   y la funci?n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2? Se "registra" la funci?n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1? Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2? Aqu? realizar?amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    private void CargarDatosEstructura()
    {
        TIPOLOGIAPROY oSN4Aux = TIPOLOGIAPROY.Select(tr, (byte)SN4);
        txtDesSN4.Text = oSN4Aux.t320_denominacion;
        hdnIDSN4.Text = oSN4Aux.t320_idtipologiaproy.ToString();
    }
    private void CargarDatosItem(int idGrupo)
    {
        GRUPONAT oGr = GRUPONAT.Select(tr, idGrupo);
        txtDenominacion.Text = oGr.t321_denominacion;
        hdnIDSN3.Text = oGr.t321_idgruponat.ToString();

        if ((bool)oGr.t321_estado) chkActivo.Checked = true;
        else chkActivo.Checked = false;

        txtOrden.Text = oGr.t321_orden.ToString();
        //Un grupo no puede cambiar de tipolog?a si tiene subgrupos
        SqlDataReader dr = SUBGRUPONAT.Catalogo(null, "", idGrupo, null, null, 2, 0);
        if (dr.Read())
        {
            bSetTipol="F";
        }
        dr.Close();
        dr.Dispose();
    }
    private string Grabar(string strDatosBasicos)
    {
        string sResul = "";
        int nID = -1;
        string[] aDatosBasicos = null;
        bool bActualizar = true;
        #region abrir conexi?n y transacci?n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi?n", ex);
            return sResul;
        }
        #endregion
        try
        {
            #region Datos Generales
            if (strDatosBasicos != "")//No se ha modificado nada de la pesta?a general
            {
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = idTipologia
                ///aDatosBasicos[1] = idGrupo
                ///aDatosBasicos[2] = Denominacion
                ///aDatosBasicos[3] = Activo
                ///aDatosBasicos[4] = Orden
                ///aDatosBasicos[5] = idTipologiaAnt

                if (aDatosBasicos[1] == "") //insert
                {
                    nID = GRUPONAT.Insert(tr, Utilidades.unescape(aDatosBasicos[2]), byte.Parse(aDatosBasicos[0]), 
                                          int.Parse(aDatosBasicos[4]), (aDatosBasicos[3] == "1") ? true : false);
                }
                else //update
                {
                    nID = int.Parse(aDatosBasicos[1]);
                    //Un grupo no puede cambiar de tipolog?a si tiene subgrupos
                    if (aDatosBasicos[0] != aDatosBasicos[5])
                    {
                        SqlDataReader dr = SUBGRUPONAT.Catalogo(null, "", byte.Parse(aDatosBasicos[5]), null, null, 2, 0);
                        if (dr.Read())
                        {
                            bActualizar = false;
                        }
                        dr.Close();
                        dr.Dispose();
                    }
                    if (bActualizar)
                        GRUPONAT.Update(tr, nID, Utilidades.unescape(aDatosBasicos[2]), byte.Parse(aDatosBasicos[0]),
                                    int.Parse(aDatosBasicos[4]), (aDatosBasicos[3] == "1") ? true : false);
                }
            }
            #endregion
            if (bActualizar)
            {
                Conexion.CommitTransaccion(tr);
                sResul = "OK@#@" + nID.ToString("#,###");
            }
            else
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@Un grupo de naturaleza no puede cambiar de tipolog?a de proyecto si tiene subgrupos dependientes";
            }
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
