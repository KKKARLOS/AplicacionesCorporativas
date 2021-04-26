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

public partial class INV_COSTES_PROF : System.Web.UI.Page
{
    public string strControl = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }
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

            string pf = "ctl00$CPHC$";
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
            ReportDocument rdInventario = new ReportDocument();
            
            try
            {
                rdInventario.Load(Server.MapPath(".") + @"\inv_costes_profe.rpt");             
            }
            catch (Exception ex)
            {
                rdInventario.Close();
                rdInventario.Dispose();
                Response.Write("Error al abrir el report: " + ex.Message);
            }

            try
            {
                rdInventario.SetDatabaseLogon(strUid, strPwd, strServer, strDataBase);
            }
            catch (Exception ex)
            {
                rdInventario.Close();
                rdInventario.Dispose();
                Response.Write("Error al logarse al report: " + ex.Message);
            }

            //creo un objeto logon .

            CrystalDecisions.Shared.TableLogOnInfo tliCurrent;
            try
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table tbCurrent in rdInventario.Database.Tables)
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
                rdInventario.Close();
                rdInventario.Dispose();
                Response.Write("Error al actualizar la localización: " + ex.Message);
            }

            string strTipoFormato = "PDF"; // Request.QueryString[pf + "FORMATO"]; // ESTO LO RECOGERE COMO PARAMETRO
            strControl = strTipoFormato;

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();

            try
            {
                rdInventario.SetParameterValue("@t001_idficepi", Request.Form[pf + "txtID"]);
                rdInventario.SetParameterValue("path", Server.MapPath(".") + "//");
            }
            catch (Exception ex)
            {
                rdInventario.Close();
                rdInventario.Dispose();
                Response.Write("Error al actualizar los parámetros del report: " + ex.Message);
            }
            try
            {
                System.IO.Stream oStream;
                byte[] byteArray = null;

                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        oStream = rdInventario.ExportToStream(ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                    case "EXC":
                        oStream = rdInventario.ExportToStream(ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                }
                

                // FIN

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                //Response.Buffer = false;

                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        Response.ContentType = "application/pdf";
                        break;
                    case "EXC":
                        //		EXCEL
                        Response.ContentType = "application/vnd.ms-excel";
                        //Response.ContentType = "Application/x-msexcel";

                        break;
                }

                //String nav = HttpContext.Current.Request.Browser.Browser.ToString();
                //if (nav.IndexOf("IE") == -1)
                //{
                switch (strTipoFormato)
                {
                    case "PDF":
                        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "Filename.pdf"));
                        break;
                    case "EXC":
                        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "Filename.xls"));
                        break;
                }
                //}
                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        Response.ContentType = "application/pdf";
                        break;
                    case "EXC":
                        //		EXCEL
                        Response.ContentType = "application/xls";
                        //Response.ContentType = "Application/vnd.ms-excel";

                        break;
                }              
                Response.BinaryWrite(byteArray);

                Response.Flush();
                Response.Close();
                //HttpContext.Current.Response.End();
                Response.End();
            }            
            catch (Exception ex)
            {
                rdInventario.Close();
                rdInventario.Dispose();
                Response.Write("Error al exportar el report: " + ex.Message);
            }  
        }
        //handle any exceptions
        catch (Exception ex)
        {
            Response.Write("No se puede crear el report: " + ex.Message);
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
    ~INV_COSTES_PROF()
    {
        // Simply call Dispose(false).
        this.Dispose();
    }
}
