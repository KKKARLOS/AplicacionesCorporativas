using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using Microsoft.Web.UI.WebControls;
using EO.Web;
using CR2I.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

namespace CR2I.Capa_Presentacion.Wifi.Consulta
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>
    public partial class Default : System.Web.UI.Page
	{
		//protected Toolbar Botonera = new Toolbar();

        public string strTablaHTML="";
        public string sErrores = "";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["CR2I_IDRED"] == null)
            {
                try { Response.Redirect("~/SesionCaducada.aspx", true); }
                catch (System.Threading.ThreadAbortException) { }
            }
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
            // All webpages displaying an ASP.NET menu control must inherit this class.
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                Page.ClientTarget = "uplevel";
        }
        protected void Page_Load(object sender, System.EventArgs e)
		{

            if (!Page.IsCallback)
            {
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click); 

                Master.TituloPagina = "Consulta de Wifi";
                Master.Comportamiento = 8;

                //ArrayList aFuncionesJavaScript = new ArrayList();
                //aFuncionesJavaScript.Add("Capa_Presentacion/Wifi/Consulta/Functions/funciones.js");
                //aFuncionesJavaScript.Add("Javascript/boxover.js");
                //this.FuncionesJavaScript = aFuncionesJavaScript;
                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                string strTabla = obtenerDatos("1");

                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else sErrores += Errores.mostrarError(aTabla[1]);

                //CrearCalendario();
                //if (!Page.IsPostBack)
                //{
                //    string strFecha = Request.Form["ctl00$CPHC$txtFecha"];
                //    if (strFecha != null)
                //    {
                //        this.txtFecha.Text = strFecha.ToString();
                //    }
                //    else
                //    {
                //        if (Session["CR2I_DATOSRESERVAWEBEX"] != null)
                //        {
                //            this.txtFecha.Text = ((string[])Session["CR2I_DATOSRESERVAWEBEX"])[0].ToString();
                //        }
                //        else
                //        {
                //            this.txtFecha.Text = System.DateTime.Today.ToShortDateString();
                //        }
                //    }

                //    CargarDatos();
                //}
            }
		}
        //private void CargarOficinas()
        //{
        //    this.cboOficina.DataValueField	= "strVal";
        //    this.cboOficina.DataTextField	= "strDes";
        //    this.cboOficina.DataSource = Oficina.ListaOficinas(); //Cache["cr2_oficinas"];
        //    this.cboOficina.DataBind();
        //}
        //private void CrearCalendario()
        //{
        //    this.divCal1.Controls.Add(Fechas.InsertarCalendario("txtFecha"));
        //}

        //private void CargarBotonera()
        //{
        //    CBotonera objBot = new CBotonera();
        //    objBot.CargarBotonera(Botonera, this.Comportamiento, Session["strServer"].ToString());
        //    this.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
        //    this.BarraBotones.Controls.Add(Botonera); 
        //}

        //private void Botonera_Click(object sender, System.EventArgs e)
        //{
        //    ToolbarItem btn = (ToolbarItem)sender;
        //    string sMsg = "Clicked on object '" + sender.ToString() +" / " + Botonera.Items.FlatIndexOf(btn).ToString();
			
        //    //Response.Write(sMsg);
        //    //			switch (Botonera.Items.FlatIndexOf(btn))
        //    switch (btn.ID.ToLower())
        //    {
        //        case "inicio":
        //            Response.Redirect("../../Default.aspx");
        //            break;
        //    }
        //}
        public void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
        {
            switch (e.Item.CommandName.ToLower())
            {
                case "regresar":
                    //string sUrl = HistorialNavegacion.Leer();
                    try
                    {
                        Response.Redirect("../../Default.aspx");
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    break;
            }
        }

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion

        private string obtenerDatos(string sSoloActivas)
        {
            string sFecAux="";

            StringBuilder sb = new StringBuilder();

            try
            {
                //sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 850px; table-layout:fixed; HEIGHT: 17px;cursor: url(../../../images/imgManoAzul2.cur)' cellspacing='0' border='0'>");
                sb.Append("<table id='tblDatos' class='texto MA' style='width:850px; table-layout:fixed;' cellspacing='0' border='0'>");
                sb.Append("    <colgroup>");
				sb.Append("        <col style='width:295px;' />");
				sb.Append("        <col style='width:295px' />");
				sb.Append("        <col style='width:100px' />");
                sb.Append("        <col style='width:100px' />");
                sb.Append("        <col style='width:60px' />");
                sb.Append("    </colgroup>");

                sb.Append("<tbody>");

                string sColor = "";
                SqlDataReader dr = WIFI.CatalogoWifi((int)Session["CR2I_IDFICEPI"], (sSoloActivas == "1") ? true : false);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t085_idreserva"].ToString() + "' style='height:16px;'");
                    //sb.Append(" style=\"height:16px; cursor:url(../../../images/imgManoAzul2.cur),pointer\" onclick='msse(this)' ondblclick='mdwifi(this.id)'>");
                    sb.Append(" onclick='msse(this)' ondblclick='mdwifi(this.id)'>");

                    sb.Append("<td><nobr class='NBR W290'>" + dr["Solicitante"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W290'>" + dr["t085_interesado"].ToString() + "</nobr></td>");

                    sFecAux = dr["t085_fechoraini"].ToString().Substring(0, 16);
                    if (sFecAux.Substring(15, 1) == ":")
                        sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);
                    sb.Append("<td>" + sFecAux + "</td>");

                    sFecAux = dr["t085_fechorafin"].ToString().Substring(0, 16);
                    if (sFecAux.Substring(15, 1) == ":")
                        sFecAux = sFecAux.Substring(0, 11) + "0" + sFecAux.Substring(11, 4);
                    sb.Append("<td>" + sFecAux + "</td>");

                    switch (dr["t085_estado"].ToString())
                    {
                        case "1": sColor = "Orange"; break;
                        case "2": sColor = "Green"; break;
                        case "3": sColor = "Gray"; break;
                        case "4": sColor = "Red"; break;
                    }
                    sb.Append("<td style='Color:" + sColor + "'>" + dr["des_estado"].ToString() + "</td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener el catálogo de reservas wifi", ex);
            }
        }

	}
}
