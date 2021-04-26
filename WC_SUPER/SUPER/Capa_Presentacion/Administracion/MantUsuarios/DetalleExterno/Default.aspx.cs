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
using SUPER.BLL;
using System.Collections.Generic;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores="";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int nIdUsuario = 0, nIdFicepi = 0;

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

                Utilidades.SetEventosFecha(this.txtFecAlta);
                Utilidades.SetEventosFecha(this.txtFecBaja);

                nIdUsuario = int.Parse(Request.QueryString["nIdUsuario"].ToString());
                nIdFicepi = int.Parse(Request.QueryString["nIdFicepi"].ToString());

                CargarDatosItem(nIdUsuario, nIdFicepi);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos.", ex);
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
        //Session.Clear();
        //Session.Abandon();
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("eliminar"):
                sResultado += Eliminar(aArgs[1]);
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

    private void CargarDatosItem(int nIdUsuario, int nIdFicepi)
    {
        SqlDataReader dr = null;
        string sIdMoneda = "";
        if (nIdUsuario > 0)
            dr = USUARIO.ObtenerDatosProfUsuario(nIdUsuario);
        else
            dr = USUARIO.ObtenerDatosProfFicepi(nIdFicepi);

        if (dr.Read())
        {
            txtUsuario.Text = int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###");
            txtAlias.Text = dr["t314_alias"].ToString();
            txtDesProfesional.Text = dr["PROFESIONAL"].ToString().Replace("&nbsp;", " ");
            hdnIDProveedor.Text = dr["idEmpProv"].ToString();
            txtDesProveedor.Text = dr["desEmpProv"].ToString();
            txtFecAlta.Text = (dr["t314_falta"].ToString() == "") ? DateTime.Today.ToShortDateString() : ((DateTime)dr["t314_falta"]).ToShortDateString();
            //Guardo la fecha de alta inicial para que si cambia pueda hacer comprobaciones adicionales al grabar
            this.hdnFAltaIni.Value = txtFecAlta.Text;

            txtFecBaja.Text = (dr["t314_fbaja"].ToString() == "") ? "" : ((DateTime)dr["t314_fbaja"]).ToShortDateString();
            txtLoginHermes.Text = dr["loginhermes"].ToString();
            txtComSAP.Text = dr["codcomercialsap"].ToString();
            txtUltImp.Text = (dr["ult_imputacion"].ToString() == "") ? "" : ((DateTime)dr["ult_imputacion"]).ToShortDateString();
            if ((int)dr["controlhuecos"]==1) chkHuecos.Checked = true;
            else chkHuecos.Checked = false;
            if ((bool)dr["mailiap"]) chkMailIAP.Checked = true;
            else chkMailIAP.Checked = false;
            txtCosteHora.Text = double.Parse(dr["costehora"].ToString()).ToString("#,##0.0000");
            txtCosteJornada.Text = double.Parse(dr["costejornada"].ToString()).ToString("#,##0.0000");
            txtUltImp.Text = (dr["ult_imputacion"].ToString() == "") ? "" : ((DateTime)dr["ult_imputacion"]).ToShortDateString();
            cboCJA.SelectedValue = dr["calculoJA"].ToString();
            if ((int)dr["acs"] == 1) chkACS.Checked = true;
            else chkACS.Checked = false;
            sIdMoneda = dr["t422_idmoneda"].ToString();
            if ((bool)dr["t314_noalertas"]) chkAlertas.Checked = true;
            else chkAlertas.Checked = false;
            this.hdnTiporecurso.Value = dr["t001_tiporecurso"].ToString();

            txtNJornLab.Text = double.Parse(dr["Njorlabcal"].ToString()).ToString("#,###.##");
            txtDesCalendario.Text = dr["Calendario"].ToString();
            hdnIdCalendario.Text = dr["IdCalendario"].ToString();
            txtDesOficina.Text = dr["Oficina"].ToString();
        }
        dr.Close();
        dr.Dispose();

        List<ElementoLista> oLista = MONEDA.ListaMonedasCosteUsu();
        ListItem oLI = null;
        foreach (ElementoLista oMoneda in oLista)
        {
            oLI = new ListItem(oMoneda.sDenominacion, oMoneda.sValor);
            if (oMoneda.sValor == sIdMoneda) oLI.Selected = true;
            cboMoneda.Items.Add(oLI);
        }
    }

    private string Grabar(string strDatos)
    {
        int nID = -1;
        int? nProveedor = null;
        string sMsgError = "";
        try
        {
            #region Datos Generales
            string[] aValores = Regex.Split(strDatos, "##");
            #region valores
            ///aValores[0] = nIdUsuario
            ///aValores[1] = nIdFicepi
            ///aValores[2] = txtAlias
            ///aValores[3] = hdnIDProveedor
            ///aValores[4] = txtFecAlta
            ///aValores[5] = txtFecBaja
            ///aValores[6] = txtLoginHermes
            ///aValores[7] = txtComSAP
            ///aValores[8] = chkHuecos
            ///aValores[9] = chkMailIAP
            ///aValores[10] = txtCosteHora
            ///aValores[11] = txtCosteJornada
            ///aValores[12] = cboCJA
            ///aValores[13] = chkACS
            ///aValores[14] = cboMoneda
            ///aValores[15] = chkAlertas
            ///aValores[16] = Fecha Alta Inicial
            ///aValores[17] = IdCalendario
            #endregion
            if (aValores[3] != "")
                nProveedor = int.Parse(aValores[3]);
            if (aValores[0] == "0") //insert
            {

                nID = USUARIO.InsertarExterno(null,
                            int.Parse(aValores[1]),
                            Utilidades.unescape(aValores[2]),
                            int.Parse(aValores[3]),//Al insertar siempre va a ser un externo y el proveedor es obligatorio
                            DateTime.Parse(aValores[4]),
                            (aValores[5] == "") ? null : (DateTime?)DateTime.Parse(aValores[5]),
                            Utilidades.unescape(aValores[6]),
                            Utilidades.unescape(aValores[7]),
                            (aValores[8] == "1") ? true : false,
                            (aValores[9] == "1") ? true : false,
                            decimal.Parse(aValores[10]),
                            decimal.Parse(aValores[11]),
                            (aValores[12] == "1") ? true : false,
                            (aValores[13] == "1") ? true : false,
                            aValores[14],
                            (aValores[15] == "1") ? true : false
                            );

            }
            else //update
            {
                DateTime dtAltaAct = DateTime.Parse(aValores[4]);//Fecha actual de alta
                nID = int.Parse(aValores[0]);
                //Si ha cambiado la fecha de alta compruebo que debe ser menor que el mínimo entre los consumos IAP,
                //los consumos económicos y las asignaciones a proyectos
                #region Comprobar Fecha Alta
                if (aValores[4] != aValores[16])
                {
                    ArrayList aFechas = SUPER.Capa_Negocio.USUARIO.GetMinimoFechaAlta(null, nID);
                    //((string[])aFechas [fila])[columna]
                    string sTipo = "", sFecha = "";
                    DateTime dtAltaAnt = DateTime.Parse(aValores[16]);//Fecha anterior
                    DateTime dtAux;
                    for (int i = 0; i < aFechas.Count; i++)
                    {
                        sTipo = ((string[])aFechas[i])[0];
                        sFecha = ((string[])aFechas[i])[1];
                        if (sFecha != "")
                        {
                            dtAux = DateTime.Parse(sFecha);
                            switch (sTipo)
                            {
                                case "IAP":
                                    if (dtAltaAct > dtAux)
                                    {
                                        if (sMsgError != "") sMsgError += "\n";
                                        sMsgError += "\nLa fecha de alta no puede ser superior al primer consumo IAP que corresponde al " + sFecha.Substring(0, 10) + ".";
                                    }
                                    break;
                                case "ECO":
                                    if (dtAltaAct > dtAux)
                                    {
                                        if (sMsgError != "") sMsgError += "\n";
                                        sMsgError += "\nLa fecha de alta no puede ser superior al primer consumo económico que corresponde al " + sFecha.Substring(0, 10) + ".";
                                    }
                                    break;
                                case "PRY":
                                    if (dtAltaAct > dtAux)
                                    {
                                        if (sMsgError != "") sMsgError += "\n";
                                        sMsgError += "\nLa fecha de alta no puede ser superior a la primera asignación a proyecto que corresponde al " + sFecha.Substring(0, 10) + ".";
                                    }
                                    break;
                            }
                        }
                    }
                    if (sMsgError != "")
                    {
                        return "Error@#@" + sMsgError + "@#@";
                    }
                }
                #endregion
                USUARIO.ModificarExterno(null,
                            nID,
                            Utilidades.unescape(aValores[2]),
                            nProveedor,//Al modificar puede ser un foráneo con lo que el proveedor es vacío
                            dtAltaAct,
                            (aValores[5] == "") ? null : (DateTime?)DateTime.Parse(aValores[5]),
                            Utilidades.unescape(aValores[6]),
                            Utilidades.unescape(aValores[7]),
                            (aValores[8] == "1") ? true : false,
                            (aValores[9] == "1") ? true : false,
                            decimal.Parse(aValores[10]),
                            decimal.Parse(aValores[11]),
                            (aValores[12] == "1") ? true : false,
                            (aValores[13] == "1") ? true : false,
                            aValores[14],
                            (aValores[15] == "1") ? true : false
                            );              
            }

            if(aValores[17] != "") Ficepi.UpdateCal(int.Parse(aValores[1]), int.Parse(aValores[17]));
            #endregion

            return "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al grabar los datos del usuario.", ex);
        }
    }
    private string Eliminar(string sIdUsuario)
    {
        try
        {
            USUARIO.Delete(null, int.Parse(sIdUsuario));
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar el usuario.", ex, false);
        }
    }
}
