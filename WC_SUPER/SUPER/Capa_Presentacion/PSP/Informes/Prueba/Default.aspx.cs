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

using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;


public partial class Capa_Presentacion_psp_informes_ConsTecnico_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected ArrayList aProyecto;
    public string strTablaHtml;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Informe que utiliza procedimientos almacenados encadenados";
        Master.bFuncionesLocales = true;
        Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
        Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 22;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);


            //Master.FicherosCSS.Add("Capa_Presentacion/Consultas/Seguimiento/Seguimiento.css");
            try
            {
                //cboTecnicos.Items.Add(new ListItem("", "T"));
                //cboTecnicos.Items.Add(new ListItem("Internos", "I"));
                //cboTecnicos.Items.Add(new ListItem("Externos", "E"));

                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                //Obtener los datos necesarios
                DateTime dDesde = DateTime.Parse("01/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                txtFechaInicio.Text = dDesde.ToShortDateString();
                txtFechaFin.Text = dDesde.AddMonths(1).AddDays(-1).ToShortDateString();

                hdnEmpleado.Text = Session["UsuarioActual"].ToString();

                cboConcepto.Items.Add(new ListItem("", "0"));
                cboConcepto.Items.Add(new ListItem("Estructura", "1"));
                cboConcepto.Items.Add(new ListItem("Proyecto", "2"));
                cboConcepto.Items.Add(new ListItem("Responsable", "3"));

                //hdnCR.Text = Session["CRActual"].ToString();
                //hdnDesCR.Text = Session["DesCRActual"].ToString();
                //hdnPerfil.Text = Session["PerfilActual"].ToString();
                //hdnNombre.Text = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();

                //string strPerfil = Session["PerfilActual"].ToString();
                //switch (strPerfil)
                //{
                //        // DIRECTOR Y RESPONSABLE DE OFICINA TÉCNICA
                //    case "D":
                //    case "OT":
                //        cboConcepto.Items.Add(new ListItem("", "0"));
                //        cboConcepto.Items.Add(new ListItem("Centro de responsabilidad", "1"));
                //        cboConcepto.Items.Add(new ListItem("Área de negocio", "2"));
                //        cboConcepto.Items.Add(new ListItem("Gestor", "3"));
                //        cboConcepto.Items.Add(new ListItem("Grupo funcional", "4")); //añadido 25/09/2007 a petición de Koro/Victor
                //        cboConcepto.Items.Add(new ListItem("Proyecto", "6"));
                //        break;
                //    case "GP": // GERENTE	
                //        cboConcepto.Items.Add(new ListItem("", "0"));
                //        cboConcepto.Items.Add(new ListItem("Área de negocio", "2"));
                //        cboConcepto.Items.Add(new ListItem("Gestor", "3"));
                //        cboConcepto.Items.Add(new ListItem("Proyecto", "6"));
                //        break;
                //        // RESPONS.TECNICO DE PROY.ECON Y RESPONS. ECONÓMICO
                //    case "J":
                //    case "K":
                //        cboConcepto.Items.Add(new ListItem("", "0"));
                //        //cboConcepto.Items.Add(new ListItem("Grupo funcional", "4"));
                //        cboConcepto.Items.Add(new ListItem("Proyecto", "6"));
                //        break;
                //    case "N":
                //        // DIRECTOR DE ÁREA NEGOCIO
                //        cboConcepto.Items.Add(new ListItem("", "0"));
                //        cboConcepto.Items.Add(new ListItem("Área de negocio", "2"));
                //        cboConcepto.Items.Add(new ListItem("Gestor", "3"));
                //        cboConcepto.Items.Add(new ListItem("Proyecto", "6"));
                //        break;
                //    case "GF":
                //        // DIRECTOR DE GRUPO FUNCIONAL
                //        cboConcepto.Items.Add(new ListItem("", "0"));
                //        cboConcepto.Items.Add(new ListItem("Grupo funcional", "4"));
                //        cboConcepto.Items.Add(new ListItem("Proyecto", "6"));
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
        /* switch (aArgs[0])
        {
            case ("cargar"):
                break;
        }
        */
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
}