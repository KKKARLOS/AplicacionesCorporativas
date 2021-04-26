using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SUPER.BLL;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class Capa_Presentacion_Administracion_Foraneos_Consultas_Detalle_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string sIdMoneda = "";
        try
        {
            string sIdMoneda = "";
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
                Utilidades.SetEventosFecha(this.txtFAlta);
                Utilidades.SetEventosFecha(this.txtFecBaja);

                if (Request.QueryString["idF"] != null)
                    hdnIdFicepi.Value = Utilidades.decodpar(Request.QueryString["idF"]);
                FORANEO o = FORANEO.ConsultaSelect(int.Parse(hdnIdFicepi.Value));
                //txtProfesional.Text = o.NombreCompleto;
                this.hdnIdUser.Value = o.t314_idusuario.ToString();
                this.txtApe1.Text = o.t001_apellido1;
                this.txtApe2.Text = o.t001_apellido2;
                this.txtNombre.Text = o.t001_nombre;
                this.hdnIdCalendario.Value = o.t066_idcal.ToString();
                this.txtCal.Text = o.t066_descal;
                this.txtNJornLab.Text = o.Njorlabcal.ToString();
                this.txtMail.Text = o.t001_email;

                txtCip.Text = o.t001_cip;
                txtTel.Text = o.t001_exttel;
                txtProm.Text = o.NombreCompletoProm;
                txtCal.Text = o.t066_descal;
                txtCal.Attributes.Add("idCal", o.t066_idcal.ToString());
                txtAltaForaneo.Text = (o.t080_falta == null) ? "" : ((DateTime)o.t080_falta).ToShortDateString();
                txtfultacc.Text = (o.t080_fultacc == null) ? "" : ((DateTime)o.t080_fultacc).ToShortDateString();
                chkBloqueado.Checked = !o.t314_accesohabilitado;
                txtPass.Text = DesEncriptar(o.t080_passw);
                txtPreg.Text = DesEncriptar(o.t080_pregunta);
                txtResp.Text = DesEncriptar(o.t080_respuesta);
                txtFCrea.Text = (o.t080_facep == null) ? "" : ((DateTime)o.t080_facep).ToShortDateString();
                rdbSexo.SelectedValue = o.t001_sexo;
                //ModoLectura.Poner(this.Controls);
                chkBloqueado.Enabled = true;
                txtFAlta.Text = (o.t314_falta == null) ? "" : ((DateTime)o.t314_falta).ToShortDateString();
                this.txtFecBaja.Text = (o.t314_fbaja == null) ? "" : ((DateTime)o.t314_fbaja).ToShortDateString();
                this.txtUltImp.Text = (o.t314_fbaja == null) ? "" : ((DateTime)o.fultImpIAP).ToShortDateString();
                this.txtAlias.Text = o.t314_alias;
                this.txtUsuario.Text = o.t314_idusuario.ToString("#,###");
                if (o.t314_calculoJA)
                    this.cboCJA.SelectedValue = "1";
                else
                    this.cboCJA.SelectedValue = "0";
                this.chkHuecos.Checked = o.t314_controlhuecos;
                this.chkMailIAP.Checked = o.t314_mailiap;
                sIdMoneda = o.t422_idmoneda;
                this.txtCosteHora.Text = o.t314_costehora.ToString("#,##0.0000");
                this.txtCosteJornada.Text = o.t314_costejornada.ToString("#,##0.0000");

                List<ElementoLista> oLista = MONEDA.ListaMonedasCosteUsu();
                ListItem oLI = null;
                foreach (ElementoLista oMoneda in oLista)
                {
                    oLI = new ListItem(oMoneda.sDenominacion, oMoneda.sValor);
                    if (oMoneda.sValor == sIdMoneda) oLI.Selected = true;
                    cboMoneda.Items.Add(oLI);
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
            sErrores += Errores.mostrarError("Error al obtener los datos del profesional foráneo", ex);
        }

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@"); 
        try
        {
            sResultado = aArgs[0] + @"@#@"; 
            if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += Grabar(eventArg);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case "obtener":
                    sResultado += "Errores@#@" + Errores.mostrarError("Error al obtener los foráneos.", ex);
                    break;
            }
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
    protected string DesEncriptar(string cadenaAdesencriptar)
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(cadenaAdesencriptar);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }
    private string Grabar(string sParams)
    {
        string sResul = "";
        try
        {
            string[] aArgs = Regex.Split(sParams, "@#@"); 
            FORANEO.Update(int.Parse(aArgs[1]), (aArgs[2] == "1") ? true : false, int.Parse(hdnIdFicepi.Value),
                           Utilidades.unescape(aArgs[3]), Utilidades.unescape(aArgs[4]), Utilidades.unescape(aArgs[5]),
                           aArgs[6], Utilidades.unescape(aArgs[7]), Utilidades.unescape(aArgs[8]), aArgs[9]);
            
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del foráneo. Error ", ex);
        }
        return sResul;
    }
}
