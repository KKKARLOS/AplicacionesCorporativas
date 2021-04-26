using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EO.Web; 
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Capa_Presentacion_Administracion_Calendario_Detalle_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strArrayCR;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 4;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            //Poniendo el siguiente atributo a true, se incluye el fichero javascript propio
            //de la carpeta hija "Functions/funciones.js"
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Detalle de calendario";
            cargarCombos();

            if (!Page.IsPostBack)
            {
                //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                //{
                //    lblNJLACV.Visible = true;
                //    txtNJLACV.Visible = true;
                //    lblNHLACV.Visible = true;
                //    txtNHLACV.Visible = true;
                //}
                //else
                //{
                //    lblNJLACV.Visible = false;
                //    txtNJLACV.Attributes.Add("style", "visibility:hidden;");
                //    lblNHLACV.Visible = false;
                //    //txtNHLACV.Visible = false;
                //    txtNHLACV.Attributes.Add("style", "visibility:hidden;");
                //}
                string sCalAux = Request.QueryString["nCalendario"];
                if (sCalAux != null)
                {
                    this.hdnIDCalendario.Text = sCalAux;
                    this.hdnIDCalendarioOriginal.Text = sCalAux;
                    Session["IDCalendario"] = sCalAux;
                }
                else if (Session["IDCalendario"] != null)
                {
                    this.hdnIDCalendario.Text = Session["IDCalendario"].ToString();
                    this.hdnIDCalendarioOriginal.Text = Session["IDCalendario"].ToString();
                }

                //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                //{
                //    CR objCR = new CR();
                //    this.cboCR.DataSource = objCR.ObtenerCatalogo();
                //    this.cboCR.DataTextField = "sDesCR";
                //    this.cboCR.DataValueField = "nIdCR";
                //    this.cboCR.DataBind();
                //}
                //else
                //{
                //    /*
                //    USUARIO oUser = USUARIO.Select(null, (int)Session["UsuarioActual"]);
                //    if (oUser.t303_idnodo != null)
                //    {
                //        int iNodo;
                //        iNodo = (int)oUser.t303_idnodo;
                //        NODO oNodo = NODO.ObtenerNodo(null, iNodo);
                //        ListItem Elemento1 = new ListItem(oNodo.t303_denominacion, iNodo.ToString());
                //        this.cboCR.Items.Insert(0, Elemento1);
                //    }
                //    */
                //    SqlDataReader dr;
                //    ListItem oLI = null;
                //    dr = NODO.ObtenerNodosCalendario((int)Session["UsuarioActual"]);
                //    while (dr.Read())
                //    {
                //        oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                //        cboCR.Items.Add(oLI);
                //    }
                //    dr.Close();
                //    dr.Dispose();
                //}

                //ListItem Elemento2 = new ListItem("", "-1");
                //this.cboCR.Items.Insert(0, Elemento2);
                
                cargarCombosProvinciasPais(66);

                GetArrayCR();

                if (this.hdnIDCalendario.Text != "0")
                {
                    try
                    {
                        Calendario objCal = Calendario.Obtener(int.Parse(this.hdnIDCalendario.Text), DateTime.Now.Year);
                        this.txtDesCalendario.Text = objCal.sDesCal;
                        if (objCal.nEstado == 0) this.chkActivo.Checked = false;
                        //this.cboCR.SelectedValue = objCal.nCodUne.ToString();
                        this.hdnIDCR.Text = objCal.nCodUne.ToString();
                        this.cboTipo.SelectedValue = objCal.sTipo;
                        this.txtObs.Text = objCal.sObs;
                        //this.txtNJLACV.Text = objCal.njlacv.ToString();
                        //this.txtNHLACV.Text = objCal.nhlacv.ToString("#,##0");

                        this.hdnIdFicResp.Value = objCal.nIdFicepiResp.ToString();
                        this.txtResponsable.Text = objCal.sDenResponsable;

                        if (objCal.nCodProvincia.ToString()!="") this.cboProvincia.SelectedValue = objCal.nCodProvincia.ToString();
                        if (objCal.nCodPais.ToString() != "") this.cboPais.SelectedValue = objCal.nCodPais.ToString();
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores = Errores.mostrarError("Error al obtener los datos del calendario", ex);
                    }
                }

                if (!User.IsInRole("A"))
                {
                    this.cboTipo.Items[0].Value = "-1";
                    this.cboTipo.Items[0].Text = "";
                }

                this.txtDesCalendario.Focus();

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
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
            case "grabar":
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], Utilidades.unescape(aArgs[6]), aArgs[7], aArgs[8]);
                break;
            case "grabarcomo":
                sResultado += GrabarComo(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], Utilidades.unescape(aArgs[6]), aArgs[7], aArgs[8]);
                break;
            case ("provinciasPais"):
                sResultado += cargarProvinciasPais(aArgs[1]);
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

    protected string Grabar(string sIDcal, string sDesCal, string sCodUne, string sEstado, string sTipo, string sObs, string sIdFicepiResp, string sCodProvincia)
    {
        string sResul = "";
        int? iCodProvincia = null;
        if (sCodProvincia != "") iCodProvincia = int.Parse(sCodProvincia);
        int? idFicepiResponsable = null;
        if (sIdFicepiResp != "") idFicepiResponsable = int.Parse(sIdFicepiResp);

        if (sCodUne == "") sCodUne = "-1";
        try
        {
            int nResul;
            if (sIDcal == "0")
            {
                nResul = Calendario.Insertar(Utilidades.unescape(sDesCal), int.Parse(sCodUne), int.Parse(sEstado), sTipo, 
                                            int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), sObs, idFicepiResponsable, iCodProvincia);
                sIDcal = nResul.ToString();
                //this.hdnIDCalendario.Text = sIDcal;
                Session["IDCalendario"] = sIDcal;
            }
            else
            {
                nResul = Calendario.Modificar(int.Parse(sIDcal), Utilidades.unescape(sDesCal), int.Parse(sCodUne), int.Parse(sEstado), sTipo, 
                                              int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), sObs, idFicepiResponsable, iCodProvincia);
            }

            sResul = "OK@#@" + sIDcal;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar el detalle del calendario", ex);
        }

        return sResul;
    }
    protected string GrabarComo(string sIDcal, string sDesCal, string sCodUne, string sEstado, string sTipo, string sObs, string sIdFicepiResp, string sCodProvincia)
    {
        string sResul = "";
        int? iCodProvincia = null;
        if (sCodProvincia != "") iCodProvincia = int.Parse(sCodProvincia);
        int? idFicepiResponsable = null;
        if (sIdFicepiResp != "") idFicepiResponsable = int.Parse(sIdFicepiResp);

        try
        {
            int nResul = Calendario.Insertar(Utilidades.unescape(sDesCal), int.Parse(sCodUne), int.Parse(sEstado), sTipo, 
                                            int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), sObs, idFicepiResponsable, iCodProvincia);
            //this.hdnIDCalendario.Text = sIDcalNew.ToString();
            Session["IDCalendario"] = nResul;
            Calendario.InsertarHorasComo(int.Parse(sIDcal), nResul);

            sResul = "OK@#@" + nResul.ToString();
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("IX_T039_CALENDARIO_1"))
                sResul = "Error@#@" + Errores.mostrarError("No puede existir una denominación de calendario repetida dentro del mismo C.R.");
            else
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar el detalle del calendario", ex);
        }

        return sResul;
    }


    private void GetArrayCR()
    {
        StringBuilder sbuilder = new StringBuilder();
        int i = 0;
        sbuilder.Append(" aCR_js = new Array();\n");

        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
        {
            CR objCR = new CR();
            List<CR> lCR = objCR.ObtenerCatalogo();
            foreach (CR oCR in lCR)
            {
                sbuilder.Append("\taCR_js[" + i.ToString() + "] = new Array(\"" + oCR.bEstado.ToString() + "\",\"" +
                                    oCR.nIdCR.ToString() + "\",\"" + oCR.sDesCR.ToString() + "\");\n");
                i++;
            }
            strArrayCR = sbuilder.ToString();
        }
        else
        {
            SqlDataReader dr;

            dr = NODO.ObtenerNodosCalendario((int)Session["UsuarioActual"]);
            while (dr.Read())
            {
                sbuilder.Append("\taCR_js[" + i.ToString() + "] = new Array(\"" + "True" + "\",\"" +
                                    dr["t303_idnodo"].ToString() + "\",\"" + dr["t303_denominacion"].ToString() + "\");\n");
                i++;
            }

            strArrayCR = sbuilder.ToString();

            dr.Close();
            dr.Dispose();
        }
    }
    public void cargarCombos()
    {
        cboPais.DataSource = SUPER.DAL.PAIS.Catalogo();
        cboPais.DataValueField = "identificador";
        cboPais.DataTextField = "denominacion";
        cboPais.DataBind();
        cboPais.SelectedValue = "66"; // Por defecto España
    }
    private void cargarCombosProvinciasPais(int iID)
    {
        cboProvincia.DataValueField = "identificador";
        cboProvincia.DataTextField = "denominacion";
        cboProvincia.DataSource = SUPER.DAL.PAIS.Provincias(iID);
        cboProvincia.DataBind();
        cboProvincia.Items.Insert(0, new ListItem("", ""));
    }
    private string cargarProvinciasPais(string sID)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUPER.DAL.PAIS.Provincias(int.Parse(sID)); //Mostrar todos todos las provincias relacionadas a un país determinado

            while (dr.Read())
            {
                sb.Append(dr["identificador"].ToString() + "##" + dr["denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener las provincias fiscales de un determinado país", ex);
        }
    }
}
