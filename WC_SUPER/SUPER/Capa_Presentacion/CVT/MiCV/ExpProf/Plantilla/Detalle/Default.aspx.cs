using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.BLL;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, strHTMLEntorno = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e) 
    {
        try
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

                    int idPlant=-1;
                    if (Request.QueryString["ep"] != null)
                        this.hdnEP.Value = Utilidades.decodpar(Request.QueryString["ep"].ToString());
                    if (Request.QueryString["id"] != null && Utilidades.decodpar(Request.QueryString["id"].ToString()) != "-1")
                        idPlant = int.Parse(Utilidades.decodpar(Request.QueryString["id"].ToString()));
                    
                    ObtenerPerfiles();
                    GetIdiomas();
                    if (idPlant != -1)
                    {
                        PLANTILLACVT oPlant = new SUPER.BLL.PLANTILLACVT(idPlant);
                        txtDescripcion.Text = oPlant.t819_denominacion;
                        txtFun.Text = oPlant.t819_funcion;
                        txtObs.Text = oPlant.t819_observa;
                        hdnId.Value = oPlant.t819_idplantillacvt.ToString();
                        if (oPlant.t035_idcodperfil.ToString() != "")
                            cboPerfil.SelectedValue = oPlant.t035_idcodperfil.ToString();
                        if (oPlant.t020_idcodidioma.ToString() != "")
                            cboIdioma.SelectedValue = oPlant.t020_idcodidioma.ToString();
                    }
                    strHTMLEntorno = SUPER.BLL.PLANTILLACVTET.Catalogo(false, idPlant);
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al cargar los datos ", ex);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar la página ", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
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
    public void ObtenerPerfiles()
    {
        SqlDataReader dr = PerfilExper.getPerfiles(null,0);
        ListItem oLI = null;
        while (dr.Read())
        {
            oLI = new ListItem(dr["T035_DESCRIPCION"].ToString(), dr["T035_IDCODPERFIL"].ToString());
            cboPerfil.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();
    }
    private void GetIdiomas()
    {
        ListItem Elemento;
        SqlDataReader dr = SUPER.BLL.Idioma.Catalogo(null);
        while (dr.Read())
        {
            Elemento = new ListItem(dr["T020_DESCRIPCION"].ToString(), dr["T020_IDCODIDIOMA"].ToString());
            this.cboIdioma.Items.Add(Elemento);
        }
        this.cboIdioma.SelectedValue = "34";//CASTELLANO
        dr.Close();
        dr.Dispose();
    }

    protected string Grabar(string strDatos, string strEntornos)
    {
        string sResul = "OK@#@";
        int idPlant = -1;
        short idIdioma = 34;
        int? idPerfil = null;
        string[] aElem = Regex.Split(strDatos, "#/#");

        SqlConnection oConn = null;
        SqlTransaction tr = null;
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
            if (aElem[5] != "")
                idPerfil = int.Parse(aElem[5]);
            if (aElem[6] != "")
                idIdioma = short.Parse(aElem[6]);
            idPlant = SUPER.BLL.PLANTILLACVT.Grabar(tr, int.Parse(aElem[0]),//id experiencia profesional
                                                     int.Parse(aElem[1]),//id plantilla
                                                     Utilidades.unescape(aElem[2]),//denominación
                                                     Utilidades.unescape(aElem[3]),//funciones
                                                     Utilidades.unescape(aElem[4]),//observaciones 
                                                     idPerfil, idIdioma
                                                    );
            SUPER.BLL.PLANTILLACVTET.Grabar(tr, idPlant, strEntornos);
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la plantilla.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul + idPlant.ToString() + "@#@" + DateTime.Now.ToString();
    }
}
