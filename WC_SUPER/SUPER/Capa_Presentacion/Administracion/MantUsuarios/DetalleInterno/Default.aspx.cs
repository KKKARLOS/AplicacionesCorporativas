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
using System.Collections.Generic;
using SUPER.BLL;

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
                Utilidades.SetEventosFecha(this.txtInicioJE);
                Utilidades.SetEventosFecha(this.txtFinJE);

                this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

                nIdUsuario = int.Parse(Request.QueryString["nIdUsuario"].ToString());
                nIdFicepi = int.Parse(Request.QueryString["nIdFicepi"].ToString());
                CargarCategorias();
                CargarDatosItem(nIdUsuario, nIdFicepi);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos. Usuario=" + Request.QueryString["nIdUsuario"].ToString() + " Ficepi=" + Request.QueryString["nIdFicepi"].ToString(), ex);
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

    private void CargarCategorias()
    {
        //cboCategoria.DataValueField = "t450_idcategsuper";
        //cboCategoria.DataTextField = "t450_denominacion";
        //cboCategoria.DataSource = CATEGSUPER.Catalogo(null, "", null, null, 2, 0);
        //cboCategoria.DataBind();

        ListItem oLI = null;
        SqlDataReader dr = CATEGSUPER.Catalogo(null, "", null, null, 2, 0);
        while (dr.Read())
        {
            oLI = new ListItem(dr["t450_denominacion"].ToString(), dr["t450_idcategsuper"].ToString());
            oLI.Attributes.Add("sCosteHora", decimal.Parse(dr["t450_costemediohora"].ToString()).ToString("#,###.0000"));
            oLI.Attributes.Add("sCosteJornada", decimal.Parse(dr["t450_costemediojornada"].ToString()).ToString("#,###.0000"));
            cboCategoria.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();

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
            txtDesProfesional.Text = dr["PROFESIONAL"].ToString().Replace("&nbsp;"," ");
            hdnIdNodo.Text = dr["t303_idnodo"].ToString();
            txtDesNodo.Text = dr["t303_denominacion"].ToString();
            hdnIDEmpresa.Text = dr["idEmpProv"].ToString();
            txtDesEmpresa.Text = dr["desEmpProv"].ToString();
            txtFecAlta.Text = (dr["t314_falta"].ToString() == "") ? DateTime.Today.ToShortDateString() : ((DateTime)dr["t314_falta"]).ToShortDateString();
            //Guardo la fecha de alta inicial para que si cambia pueda hacer comprobaciones adicionales al grabar
            this.hdnFAltaIni.Value = txtFecAlta.Text;

            txtFecBaja.Text = (dr["t314_fbaja"].ToString() == "") ? "" : ((DateTime)dr["t314_fbaja"]).ToShortDateString();
            if ((int)dr["nuevogasvi"]==1) chkNuevoGasvi.Checked = true;
            else chkNuevoGasvi.Checked = false;
            txtLoginHermes.Text = dr["loginhermes"].ToString();
            txtComSAP.Text = dr["codcomercialsap"].ToString();
            txtUltImp.Text = (dr["ult_imputacion"].ToString() == "") ? "" : ((DateTime)dr["ult_imputacion"]).ToShortDateString();
            if ((int)dr["controlhuecos"]==1) chkHuecos.Checked = true;
            else chkHuecos.Checked = false;
            if ((bool)dr["mailiap"]) chkMailIAP.Checked = true;
            else chkMailIAP.Checked = false;
            txtCosteHora.Text = double.Parse(dr["costehora"].ToString()).ToString("#,##0.0000");
            txtCosteJornada.Text = double.Parse(dr["costejornada"].ToString()).ToString("#,##0.0000");
            if ((int)dr["jornadareducida"]==1) chkTieneJE.Checked = true;
            else chkTieneJE.Checked = false;
            txtHorasJE.Text = double.Parse(dr["horasjor_red"].ToString()).ToString("#,###.##");
            txtInicioJE.Text = (dr["fdesde_red"].ToString() == "") ? "" : ((DateTime)dr["fdesde_red"]).ToShortDateString();
            txtFinJE.Text = (dr["fhasta_red"].ToString() == "") ? "" : ((DateTime)dr["fhasta_red"]).ToShortDateString();
            txtUltImp.Text = (dr["ult_imputacion"].ToString() == "") ? "" : ((DateTime)dr["ult_imputacion"]).ToShortDateString();
            cboCategoria.SelectedValue = dr["idcategsuper"].ToString();
            cboCJA.SelectedValue = dr["calculoJA"].ToString();
            if ((int)dr["acs"] == 1) chkACS.Checked = true;
            else chkACS.Checked = false;
            txtMargenCesion.Text = double.Parse(dr["t314_margencesion"].ToString()).ToString("N");
            sIdMoneda = dr["t422_idmoneda"].ToString();

            if ((bool)dr["t314_noalertas"]) chkAlertas.Checked = true;
            else chkAlertas.Checked = false;

            txtNJornLab.Text = double.Parse(dr["Njorlabcal"].ToString()).ToString("#,###.##");
            txtDesCalendario.Text = dr["Calendario"].ToString();
            hdnIdCalendario.Text = dr["IdCalendario"].ToString();
            txtDesOficina.Text = dr["Oficina"].ToString();

            if ((bool)dr["t314_contratorelevo"]) chkRelevo.Checked = true;
            else chkRelevo.Checked = false;

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
        string sMsgError = "";
        try
        {
            string[] aValores = Regex.Split(strDatos, "##");
            #region valores
            ///aValores[0] = nIdUsuario
            ///aValores[1] = nIdFicepi
            ///aValores[2] = txtAlias
            ///aValores[3] = hdnIdNodo
            ///aValores[4] = hdnIDEmpresa
            ///aValores[5] = txtFecAlta
            ///aValores[6] = txtFecBaja
            ///aValores[7] = chkNuevoGasvi
            ///aValores[8] = txtLoginHermes
            ///aValores[9] = txtComSAP
            ///aValores[10] = chkHuecos
            ///aValores[11] = chkMailIAP
            ///aValores[12] = txtCosteHora
            ///aValores[13] = txtCosteJornada
            ///aValores[14] = chkTieneJE
            ///aValores[15] = txtHorasJE
            ///aValores[16] = txtInicioJE
            ///aValores[17] = txtFinJE
            ///aValores[18] = cboCJA
            ///aValores[19] = cboCategoria
            ///aValores[20] = chkACS
            ///aValores[21] = t314_margencesion
            ///aValores[22] = cboMoneda
            ///aValores[23] = chkAlertas
            ///aValores[24] = Fecha Alta Inicial
            ///aValores[25] = Id calendario
            ///aValores[26] = chkRelevo
            #endregion
            #region Datos Generales

            if (aValores[0] == "0") //insert
            {
                nID = USUARIO.InsertarInterno(null,
                            int.Parse(aValores[1]),
                            Utilidades.unescape(aValores[2]),
                            int.Parse(aValores[3]),
                            int.Parse(aValores[4]),
                            DateTime.Parse(aValores[5]),
                            (aValores[6] == "") ? null : (DateTime?)DateTime.Parse(aValores[6]),
                            (aValores[7] == "1") ? true : false,
                            Utilidades.unescape(aValores[8]),
                            Utilidades.unescape(aValores[9]),
                            (aValores[10] == "1") ? true : false,
                            (aValores[11] == "1") ? true : false,
                            decimal.Parse(aValores[12]),
                            decimal.Parse(aValores[13]),
                            (aValores[14] == "1") ? true : false,
                            float.Parse((aValores[15] == "") ? "0" : aValores[15]),
                            (aValores[16] == "") ? null : (DateTime?)DateTime.Parse(aValores[16]),
                            (aValores[17] == "") ? null : (DateTime?)DateTime.Parse(aValores[17]),
                            (aValores[18] == "1") ? true : false,
                            (aValores[19] == "") ? null : (int?)int.Parse(aValores[19]),
                            (aValores[20] == "1") ? true : false,
                            float.Parse((aValores[21] == "") ? "0" : aValores[21]),
                            aValores[22],
                            (aValores[23] == "1") ? true : false,
                            (aValores[26] == "1") ? true : false
                            );
            }
            else //update
            {
                DateTime dtAct = DateTime.Parse(aValores[5]);//Fecha actual
                nID = int.Parse(aValores[0]);
                //Si ha cambiado la fecha de alta compruebo que debe ser menor que el mínimo entre los consumos IAP,
                //los consumos económicos y las asignaciones a proyectos
                #region Comprobar Fecha Alta
                if (aValores[5] != aValores[24])
                {
                    ArrayList aFechas = SUPER.Capa_Negocio.USUARIO.GetMinimoFechaAlta(null, nID);
                    //((string[])aFechas [fila])[columna]
                    string sTipo = "", sFecha = "";
                    DateTime dtAnt = DateTime.Parse(aValores[24]);//Fecha anterior
                    DateTime dtAux;
                    for(int i=0; i<aFechas.Count; i++)
                    {
                        sTipo = ((string[])aFechas[i])[0];
                        sFecha = ((string[])aFechas[i])[1];
                        if (sFecha != "")
                        {
                            dtAux = DateTime.Parse(sFecha);
                            switch (sTipo)
                            {
                                case "IAP":
                                    if (dtAct > dtAux)
                                    {
                                        if (sMsgError != "") sMsgError += "\n";
                                        sMsgError += "\nLa fecha de alta no puede ser superior al primer consumo IAP que corresponde al " + sFecha.Substring(0, 10) + ".";
                                    }
                                    break;
                                case "ECO":
                                    if (dtAct > dtAux)
                                    {
                                        if (sMsgError != "") sMsgError += "\n";
                                        sMsgError += "\nLa fecha de alta no puede ser superior al primer consumo económico que corresponde al " + sFecha.Substring(0, 10) + ".";
                                    }
                                    break;
                                case "PRY":
                                    if (dtAct > dtAux)
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
                USUARIO.ModificarInterno(null,
                            nID,
                            Utilidades.unescape(aValores[2]),
                            int.Parse(aValores[3]),
                            int.Parse(aValores[4]),
                            dtAct,
                            (aValores[6] == "") ? null : (DateTime?)DateTime.Parse(aValores[6]),
                            (aValores[7] == "1") ? true : false,
                            Utilidades.unescape(aValores[8]),
                            Utilidades.unescape(aValores[9]),
                            (aValores[10] == "1") ? true : false,
                            (aValores[11] == "1") ? true : false,
                            decimal.Parse(aValores[12]),
                            decimal.Parse(aValores[13]),
                            (aValores[14] == "1") ? true : false,
                            float.Parse((aValores[15] == "") ? "0" : aValores[15]),
                            (aValores[16] == "") ? null : (DateTime?)DateTime.Parse(aValores[16]),
                            (aValores[17] == "") ? null : (DateTime?)DateTime.Parse(aValores[17]),
                            (aValores[18] == "1") ? true : false,
                            (aValores[19] == "") ? null : (int?)int.Parse(aValores[19]),
                            (aValores[20] == "1") ? true : false,
                            float.Parse((aValores[21] == "") ? "0" : aValores[21]),
                            aValores[22],
                            (aValores[23] == "1") ? true : false,
                            (aValores[26] == "1") ? true : false
                            );                
            }

            if (aValores[25] != "")
                Ficepi.UpdateCal(int.Parse(aValores[1]), int.Parse(aValores[25]));
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

        string sResul = "";

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
            #region Borramos el usuario. Se debe borrar primero las las tareas a las que está ligado el usuario porque en el trigger de borrado hace una insert en la tabla T439_CORREOS. Por otro lado la tabla T314_USUARIO al tener una relación con T439_CORREOS de borrado en cascada se produce la situación de que borra los correos pero al borrar la tabla T336_TAREAPSPUSUARIO vuelve a crear los correos ya con el usuario borrado

                //TAREAPSP.DeleteUsuario(tr, int.Parse(sIdUsuario));
                USUARIO.Delete(tr, int.Parse(sIdUsuario));

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar el usuario", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
