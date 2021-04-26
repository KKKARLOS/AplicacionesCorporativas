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
using System.Data.SqlClient;
using CR2I.Capa_Negocio;

public partial class CONEXION_WIFI : System.Web.UI.Page
{
    public string strControl = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
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

            string strconexion = Utilidades.CadenaConexion.ToLower();
            int_pfin = strconexion.IndexOf(";database=", 0);
            strServer = strconexion.Substring(7, int_pfin - 7);

            int_pinicio = int_pfin + 10;
            int_pfin = strconexion.IndexOf(";uid=", int_pinicio);
            strDataBase = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";pwd=", int_pinicio);
            strUid = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            int_pinicio = int_pfin + 5;
            int_pfin = strconexion.IndexOf(";trusted_connection=", int_pinicio);
            strPwd = strconexion.Substring(int_pinicio, int_pfin - int_pinicio);

            //creo un objeto ReportDocument
            ReportDocument rdInforme = new ReportDocument();
            
            try
            {
                rdInforme.Load(Server.MapPath(".") + @"\cr2_conexionwifi.rpt");
            }
            catch (Exception ex)
            {
                rdInforme.Close();
                rdInforme.Dispose();
                Response.Write("Error al abrir el report: " + ex.Message);
            }

            try
            {
                rdInforme.SetDatabaseLogon(strUid, strPwd, strServer, strDataBase);
            }
            catch (Exception ex)
            {
                rdInforme.Close();
                rdInforme.Dispose();
                Response.Write("Error al logarse al report: " + ex.Message);
            }

            //creo un objeto logon .

            CrystalDecisions.Shared.TableLogOnInfo tliCurrent;
            try
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table tbCurrent in rdInforme.Database.Tables)
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
                rdInforme.Close();
                rdInforme.Dispose();
                Response.Write("Error al actualizar la localización: " + ex.Message);
            }

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();
           
            try
            {
                rdInforme.SetParameterValue("@t085_idreserva", int.Parse(Request.Form["ctl00$CPHC$hdnIDReserva"].ToString()));
            }
            catch (Exception ex)
            {
                rdInforme.Close();
                rdInforme.Dispose();
                Response.Write("Error al actualizar los parámetros del report: " + ex.Message);
            }
            try
            {
                System.IO.Stream oStream;
                byte[] byteArray = null;

                oStream = rdInforme.ExportToStream(ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.Clear();
                string strTipoFormato = "PDF";
                string nav = HttpContext.Current.Request.Browser.Browser.ToString();
                if (nav.IndexOf("IE") == -1)
                {
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
                }
                Response.BinaryWrite(byteArray);

                Response.Flush();
                Response.Close();
                //HttpContext.Current.Response.End();
                Response.End();
            }            
            catch (Exception ex)
            {
                rdInforme.Close();
                rdInforme.Dispose();
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
    ~CONEXION_WIFI()
    {
        // Simply call Dispose(false).
        this.Dispose();
    }
}
