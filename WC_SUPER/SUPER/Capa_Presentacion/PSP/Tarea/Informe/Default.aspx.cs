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

public partial class EXPORTARTAREA: System.Web.UI.Page
{

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
            ReportDocument rdTarea = new ReportDocument();
            
            try
            {
                 rdTarea.Load(Server.MapPath(".") + @"\tarea_super.rpt");
            }
            catch (Exception ex)
            {
                rdTarea.Close();
                rdTarea.Dispose();
                Response.Write("Error al abrir el report: " + ex.Message);
            }

            try
            {
                rdTarea.SetDatabaseLogon(strUid, strPwd, strServer, strDataBase);
            }
            catch (Exception ex)
            {
                rdTarea.Close();
                rdTarea.Dispose();
                Response.Write("Error al logarse al report: " + ex.Message);
            }

            //creo un objeto logon .

            CrystalDecisions.Shared.TableLogOnInfo tliCurrent;
            try
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table tbCurrent in rdTarea.Database.Tables)
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
                rdTarea.Close();
                rdTarea.Dispose();
                Response.Write("Error al actualizar la localización: " + ex.Message);
            }

            string strTipoFormato = Utilidades.decodpar(Request.QueryString["fm"].ToString()); // ESTO LO RECOGERE COMO PARAMETRO

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();

            try
            {
                rdTarea.SetParameterValue("@nIdTarea", Utilidades.decodpar(Request.QueryString["it"].ToString()));
                rdTarea.SetParameterValue("nodo", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                //rdTarea.SetParameterValue("@nIdTarea", Request.QueryString["IDTAREA"],"PROFESIONAL");
                //rdTarea.SetParameterValue("@nIdRecurso", Request.QueryString["IDTAREA"],"PROFESIONAL");
                //rdTarea.SetParameterValue("@t332_idtarea", Request.QueryString["IDTAREA"], "POOL_GFUNCIONAL");
                //rdTarea.SetParameterValue("@t332_idtarea", Request.QueryString["IDTAREA"], "ATRIBUTOS_ESTADISTICOS");
                //rdTarea.SetParameterValue("@t331_idpt", Request.QueryString["IDPT"], "ATRIB_ESTAD_PROY_TEC");
                //rdTarea.SetParameterValue("@t332_idtarea", Request.QueryString["IDTAREA"], "DOCUMENTACION");
            }
            catch (Exception ex)
            {
                rdTarea.Close();
                rdTarea.Dispose();
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
                        oStream = rdTarea.ExportToStream(ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        Response.Clear();
                        Response.ClearContent();
                        Response.ClearHeaders();


                        //String nav = HttpContext.Current.Request.Browser.Browser.ToString();
                        //if (nav.IndexOf("IE") == -1)
                        //{
                        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "Filename.pdf"));
                        Response.ContentType = "application/pdf";

                        //}
                        Response.BinaryWrite(byteArray);

                        //Response.Flush();
                        //Response.Close();
                        Response.End();

                        break;
                    case "EXC":
                        //			RTF
                        rdTarea.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "Exportacion");
                        break;
                }
                

                // FIN

                //Response.ClearContent();
                //Response.ClearHeaders();

                //switch (strTipoFormato)
                //{
                //    //			PDF			
                //    case "PDF":
                //        Response.ContentType = "application/pdf";
                //        break;
                //    case "EXCEL":
                //        //		EXCEL
                //        Response.ContentType = "application/vnd.ms-excel";
                //        break;
                //}
                //Response.Clear();
                //Response.BinaryWrite(byteArray);

                //Response.Flush();
                //Response.Close();
                //Response.End();
            }
            catch (Exception ex)
            {
                rdTarea.Close();
                rdTarea.Dispose();
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
    ~EXPORTARTAREA()
    {
        // Simply call Dispose(false).
        this.Dispose();
    }
}
