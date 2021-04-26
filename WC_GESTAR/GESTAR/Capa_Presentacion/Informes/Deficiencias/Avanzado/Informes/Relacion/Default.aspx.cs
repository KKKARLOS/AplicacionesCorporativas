using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Text.RegularExpressions;
using GESTAR.Capa_Negocio;

public partial class avanzado_relacion : System.Web.UI.Page
{
    //protected HtmlGenericControl HeadHtmlElement;

    private void Page_Load(object sender, System.EventArgs e)
    {
        //HtmlGenericControl metadata = new HtmlGenericControl("meta");
        //metadata.Attributes.Add("name", "pragma");
        //metadata.Attributes.Add("content", "no-cache");
        //HeadHtmlElement.Controls.Add(metadata); 
        // Put user code to initialize the page here
    }

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);

        try
        {
            int int_pinicio;
            int int_pfin;
            string strServer;
            string strDataBase;
            string strUid;
            string strPwd;
            string strOpcion;

            //				obtengo de la cadena de conexión los parámetros para luego
            //				modificar localizaciones 

            string strconexion = Utilidades.Conexion();
            int_pfin = strconexion.IndexOf(";database=", 0);
            strServer = strconexion.Substring(7, int_pfin - 7);

            int_pinicio = int_pfin + 10;
            int_pfin = strconexion.IndexOf(";uid=", int_pinicio);
            strDataBase = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";pwd=", int_pinicio);
            strUid = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";Trusted_Connection=", int_pinicio);
            strPwd = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            //creo un objeto ReportDocument
            ReportDocument rdFormulario = new ReportDocument();

            strOpcion = Session["ORIGEN"].ToString();

            try
            {
                if (strOpcion == "PANT_AVANZADO")
                {
                    if ((((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString() == "A")
                    {
                        rdFormulario.Load(Server.MapPath(".") + @"\relacion_avan_act.rpt");
                    }
                    else
                    {
                        if (short.Parse((((Hashtable)Session["PANT_AVANZADO"])["rdlCasoCronologia"]).ToString()) == 0)
                            rdFormulario.Load(Server.MapPath(".") + @"\relacion_avan_cro_or.rpt");
                        else
                            rdFormulario.Load(Server.MapPath(".") + @"\relacion_avan_cro_and.rpt");
                    }
                }
                else if (strOpcion == "PANT_VENCIMIENTO") rdFormulario.Load(Server.MapPath(".") + @"\relacion_vto.rpt");

            }
            catch (Exception ex)
            {
                rdFormulario.Close();
                rdFormulario.Dispose();
                Response.Write("Error al abrir el report: " + ex.Message);
            }

            try
            {
                rdFormulario.SetDatabaseLogon(strUid, strPwd, strServer, strDataBase);
            }
            catch (Exception ex)
            {
                rdFormulario.Close();
                rdFormulario.Dispose();
                Response.Write("Error al logarse al report: " + ex.Message);
            }

            //creo un objeto logon
            CrystalDecisions.Shared.TableLogOnInfo tliCurrent;

            try
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table tbCurrent in rdFormulario.Database.Tables)
                {

                    //obtengo el logon por tabla
                    tliCurrent = tbCurrent.LogOnInfo;

                    tliCurrent.ConnectionInfo.DatabaseName = strDataBase;
                    tliCurrent.ConnectionInfo.UserID = strUid;
                    tliCurrent.ConnectionInfo.Password = strPwd;
                    tliCurrent.ConnectionInfo.ServerName = strServer;

                    //aplico los cambios hechos al objeto TableLogonInfo
                    tbCurrent.ApplyLogOnInfo(tliCurrent);
                }
            }
            catch (Exception ex)
            {
                rdFormulario.Close();
                rdFormulario.Dispose();

                Response.Write("Error al actualizar la localización: " + ex.Message);
            }
            string strIDFICEPI = Session["IDFICEPI"].ToString();
            if (Session["ADMIN"].ToString() == "A") strIDFICEPI = "0";

            if (strOpcion == "PANT_AVANZADO")
            {
                if ((((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString() == "C")
                {
                    rdFormulario.SetParameterValue("@FECHA_INICIO", (((Hashtable)Session["PANT_AVANZADO"])["txtFechaInicio"]).ToString());
                    rdFormulario.SetParameterValue("@FECHA_FIN", (((Hashtable)Session["PANT_AVANZADO"])["txtFechaFin"]).ToString());
                }
                rdFormulario.SetParameterValue("@T001_IDFICEPI", strIDFICEPI);
                rdFormulario.SetParameterValue("@IDAREA", (((Hashtable)Session["PANT_AVANZADO"])["hdnIDArea"]).ToString());
                rdFormulario.SetParameterValue("@T044_IMPORTANCIA", (((Hashtable)Session["PANT_AVANZADO"])["cboImportancia"]).ToString());
                rdFormulario.SetParameterValue("@T044_PRIORIDAD", (((Hashtable)Session["PANT_AVANZADO"])["cboPrioridad"]).ToString());
                rdFormulario.SetParameterValue("@T044_RTDO", (((Hashtable)Session["PANT_AVANZADO"])["cboRtado"]).ToString());
                rdFormulario.SetParameterValue("@T074_IDENTRADA", (((Hashtable)Session["PANT_AVANZADO"])["hdnEntrada"]).ToString());
                rdFormulario.SetParameterValue("@T076_IDTIPO", (((Hashtable)Session["PANT_AVANZADO"])["hdnTipo"]).ToString());
                rdFormulario.SetParameterValue("@T077_IDALCANCE", (((Hashtable)Session["PANT_AVANZADO"])["hdnAlcance"]).ToString());
                rdFormulario.SetParameterValue("@T078_IDPROCESO", (((Hashtable)Session["PANT_AVANZADO"])["hdnProceso"]).ToString());
                rdFormulario.SetParameterValue("@T079_IDPRODUCTO", (((Hashtable)Session["PANT_AVANZADO"])["hdnProducto"]).ToString());
                rdFormulario.SetParameterValue("@T081_IDREQUISITO", (((Hashtable)Session["PANT_AVANZADO"])["hdnRequisito"]).ToString());
                rdFormulario.SetParameterValue("@T082_CAUSA_CAT", (((Hashtable)Session["PANT_AVANZADO"])["hdnCausa"]).ToString());
                rdFormulario.SetParameterValue("@T044_COORDINADOR", (((Hashtable)Session["PANT_AVANZADO"])["hdnCoordinador"]).ToString());
                rdFormulario.SetParameterValue("@T044_SOLICITANTE", (((Hashtable)Session["PANT_AVANZADO"])["hdnSolicitante"]).ToString());
                rdFormulario.SetParameterValue("@T044_PROVEEDOR", (((Hashtable)Session["PANT_AVANZADO"])["txtProveedor"]).ToString());
                rdFormulario.SetParameterValue("@T044_CLIENTE", (((Hashtable)Session["PANT_AVANZADO"])["txtCliente"]).ToString());
                rdFormulario.SetParameterValue("@T044_CR", (((Hashtable)Session["PANT_AVANZADO"])["txtCR"]).ToString());
                rdFormulario.SetParameterValue("@COLUMNA", "1");
                rdFormulario.SetParameterValue("@ORDEN", "0");

                string strTramitadas;
                string strPdteAclara;
                string strAclaRta;
                string strAceptadas;
                string strRechazadas;
                string strEnEstudio;
                string strPdtesDeResolucion;
                string strPdtesDeAcepPpta;
                string strEnResolucion;
                string strResueltas;
                string strNoAprobadas;
                string strAprobadas;
                string strAnuladas;

                if ((((Hashtable)Session["PANT_AVANZADO"])["hdnCaso"]).ToString() == "A")
                {
                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkTramitadas"]).ToString())) strTramitadas = "1";
                    else strTramitadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdteAclara"]).ToString())) strPdteAclara = "1";
                    else strPdteAclara = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAclaRta"]).ToString())) strAclaRta = "1";
                    else strAclaRta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAceptadas"]).ToString())) strAceptadas = "1";
                    else strAceptadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkRechazadas"]).ToString())) strRechazadas = "1";
                    else strRechazadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnEstudio"]).ToString())) strEnEstudio = "1";
                    else strEnEstudio = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeResolucion"]).ToString())) strPdtesDeResolucion = "1";
                    else strPdtesDeResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeAcepPpta"]).ToString())) strPdtesDeAcepPpta = "1";
                    else strPdtesDeAcepPpta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnResolucion"]).ToString())) strEnResolucion = "1";
                    else strEnResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkResueltas"]).ToString())) strResueltas = "1";
                    else strResueltas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkNoAprobadas"]).ToString())) strNoAprobadas = "1";
                    else strNoAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAprobadas"]).ToString())) strAprobadas = "1";
                    else strAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAnuladas"]).ToString())) strAnuladas = "1";
                    else strAnuladas = "0";
                }
                else
                {
                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkTramitadas2"]).ToString())) strTramitadas = "1";
                    else strTramitadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdteAclara2"]).ToString())) strPdteAclara = "1";
                    else strPdteAclara = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAclaRta2"]).ToString())) strAclaRta = "1";
                    else strAclaRta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAceptadas2"]).ToString())) strAceptadas = "1";
                    else strAceptadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkRechazadas2"]).ToString())) strRechazadas = "1";
                    else strRechazadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnEstudio2"]).ToString())) strEnEstudio = "1";
                    else strEnEstudio = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeResolucion2"]).ToString())) strPdtesDeResolucion = "1";
                    else strPdtesDeResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkPdtesDeAcepPpta2"]).ToString())) strPdtesDeAcepPpta = "1";
                    else strPdtesDeAcepPpta = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkEnResolucion2"]).ToString())) strEnResolucion = "1";
                    else strEnResolucion = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkResueltas2"]).ToString())) strResueltas = "1";
                    else strResueltas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkNoAprobadas2"]).ToString())) strNoAprobadas = "1";
                    else strNoAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAprobadas2"]).ToString())) strAprobadas = "1";
                    else strAprobadas = "0";

                    if (bool.Parse((((Hashtable)Session["PANT_AVANZADO"])["chkAnuladas2"]).ToString())) strAnuladas = "1";
                    else strAnuladas = "0";
                }

                rdFormulario.SetParameterValue("@TRAMITADAS", strTramitadas);
                rdFormulario.SetParameterValue("@PDTEACLARA", strPdteAclara);
                rdFormulario.SetParameterValue("@ACLARARTA", strAclaRta);
                rdFormulario.SetParameterValue("@ACEPTADAS", strAceptadas);
                rdFormulario.SetParameterValue("@RECHAZADAS", strRechazadas);
                rdFormulario.SetParameterValue("@ENESTUDIO", strEnEstudio);
                rdFormulario.SetParameterValue("@PENDTE_RESOLUCION", strPdtesDeResolucion);
                rdFormulario.SetParameterValue("@PENDTE_ACEPTACION", strPdtesDeAcepPpta);
                rdFormulario.SetParameterValue("@ENRESOLUCION", strEnResolucion);
                rdFormulario.SetParameterValue("@RESUELTAS", strResueltas);
                rdFormulario.SetParameterValue("@NOAPROBADAS", strNoAprobadas);
                rdFormulario.SetParameterValue("@APROBADAS", strAprobadas);
                rdFormulario.SetParameterValue("@ANULADAS", strAnuladas);
            }
            else if (strOpcion == "PANT_VENCIMIENTO")
            {
                rdFormulario.SetParameterValue("@OPCION", (((Hashtable)Session["PANT_VENCIMIENTO"])["rdlOpciones"]).ToString());
                rdFormulario.SetParameterValue("@T001_IDFICEPI", strIDFICEPI);
                rdFormulario.SetParameterValue("@IDAREA", (((Hashtable)Session["PANT_VENCIMIENTO"])["hdnIDArea"]).ToString());
                rdFormulario.SetParameterValue("@FECHA_INI", (((Hashtable)Session["PANT_VENCIMIENTO"])["txtFechaInicio"]).ToString());
                rdFormulario.SetParameterValue("@FECHA_FIN", (((Hashtable)Session["PANT_VENCIMIENTO"])["txtFechaFin"]).ToString());
                rdFormulario.SetParameterValue("@COLUMNA", "1");
                rdFormulario.SetParameterValue("@ORDEN", "0");
            }
            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            string strTipoFormato = "PDF"; // ESTO LO RECOGERE COMO PARAMETRO

            ExportOptions exportOpts = new ExportOptions();

            try
            {
                System.IO.Stream oStream;
                byte[] byteArray = null;

                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        oStream = rdFormulario.ExportToStream(ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                    case "RTF":
                        //			RTF
                        rdFormulario.ExportToHttpResponse(ExportFormatType.EditableRTF, Response, false, "Exportacion");
                        return;
                        break;
                }

                rdFormulario.Close();
                rdFormulario.Dispose();

                // FIN

                Response.ClearContent();
                Response.ClearHeaders();

                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        Response.ContentType = "application/pdf";
                        break;
                    case "RTF":
                        //			RTF
                        Response.ContentType = "application/msword";
                        break;
                }
                //String nav = HttpContext.Current.Request.Browser.Browser.ToString();
                //if (nav.IndexOf("IE") == -1)
                //{
                switch (strTipoFormato)
                {
                    case "PDF":

                        Response.AddHeader
                        ("Content-Disposition", "attachment;filename=Filename.pdf");
                        break;
                    case "EXC":
                        Response.AddHeader
                        ("Content-Disposition", "attachment;filename=Filename.xls");
                        break;
                }
                //}
                Response.Clear();
                Response.BinaryWrite(byteArray);

                Response.Flush();
                Response.Close();
                Response.End();
            }
            catch (Exception ex)
            {
                rdFormulario.Close();
                rdFormulario.Dispose();
                Response.Write("Error al exportar el report: " + ex.Message);
            }
        }
        //handle any exceptions
        catch (Exception ex)
        {
            Response.Write("Report could not be created: " + ex.Message);
        }
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.Load += new System.EventHandler(this.Page_Load);
    }
    #endregion
}
