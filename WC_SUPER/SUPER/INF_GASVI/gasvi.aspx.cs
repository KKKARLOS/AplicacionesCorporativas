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
using SUPER.Capa_Negocio;

public partial class GASVI : System.Web.UI.Page
    {
        //protected HtmlGenericControl HeadHtmlElement;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            //HtmlGenericControl metadata = new HtmlGenericControl("meta");
            //metadata.Attributes.Add("name", "pragma");
            //metadata.Attributes.Add("content", "no-cache");
            //HeadHtmlElement.Controls.Add(metadata); 
            // Put user code to initialize the page here
		    mihead.Title = "Gastos de viaje";
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            try
            {
                bool bMostrar = true;
                int int_pinicio;
                int int_pfin;
                string strServer;
                string strDataBase;
                string strUid;
                string strPwd;
                //				obtengo de la cadena de conexión los parámetros para luego
                //				modificar localizaciones 

                string strconexion = Utilidades.CadenaConexion;
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

                try
                {
                    rdFormulario.Load(Server.MapPath(".") + @"\detalle.rpt");
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

                rdFormulario.SetParameterValue("@nRef", Utilidades.decodpar(Request.QueryString["ing"].ToString()));
                //rdFormulario.SetParameterValue("@nRef", "39273");

                DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();

                //string strTipoFormato = Request.QueryString["FORMATO"]; // ESTO LO RECOGERE COMO PARAMETRO

                string strTipoFormato = "PDF";

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
                            bMostrar = false;
                            break;
                    }

                    rdFormulario.Close();
                    rdFormulario.Dispose();

                    // FIN

                    if (bMostrar)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.Buffer = true;

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
                        Response.Clear();

                        //String nav = HttpContext.Current.Request.Browser.Browser.ToString();
                        //if (nav.IndexOf("IE") == -1)
                        //{
                            switch (strTipoFormato)
                            {
                                case "PDF":

                                    Response.AddHeader
                                    ("Content-Disposition", "attachment;filename=Filename.pdf");
                                    break;
                                case "RTF":
                                    Response.AddHeader
                                    ("Content-Disposition", "attachment;filename=Filename.doc");
                                    break;
                            }
                        //}
                        Response.BinaryWrite(byteArray);

                        Response.Flush();
                        Response.Close();
                        Response.End();
                    }
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
