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

public partial class avanzado_detalle: System.Web.UI.Page
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

            try
            {
                rdFormulario.Load(Server.MapPath(".") + @"\detalle_tar.rpt");
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

            //for (int i = 0; i < rdFormulario.ParameterFields.Count; i++)
            //{
            //    if (rdFormulario.ParameterFields[i].ReportParameterType == CrystalDecisions.Shared.ParameterType.ReportParameter) continue;
            //    rdFormulario.SetParameterValue(rdFormulario.ParameterFields[i].Name, null);
            //}
            try
            {
                rdFormulario.SetParameterValue("@Codigo", Request.QueryString["ID"]);
                //rdFormulario.SetParameterValue("@IDDEFICIENCIA", "18", "TAREAS");

                //if (Request.QueryString["ADMIN"] == null)
                //{
                //    if (Session["ADMIN"].ToString() == "A")
                //        rdFormulario.SetParameterValue("@IDFICEPI", "0", "TAREAS");
                //    else
                //        rdFormulario.SetParameterValue("@IDFICEPI", Session["IDFICEPI"].ToString(), "TAREAS");
                //}
                //else
                //{
                //    if (Request.QueryString["ADMIN"] == "A")
                //        rdFormulario.SetParameterValue("@IDFICEPI", "0", "TAREAS");
                //    else
                //        rdFormulario.SetParameterValue("@IDFICEPI", Request.QueryString["IDFICEPI"].ToString(), "TAREAS");

                //}

                //rdFormulario.SetParameterValue("@INTCOLUMNA", "2", "TAREAS");
                //rdFormulario.SetParameterValue("@INTORDEN", "0", "TAREAS");
                //rdFormulario.SetParameterValue("@t042_idarea","1" , "DOCUMENTOS_AREA");
                //rdFormulario.SetParameterValue("@t044_iddeficiencia","1" , "DOCUMENTOS_DEFICIENCIA");
                //rdFormulario.SetParameterValue("@T044_IDDEFICIENCIA","1" , "CRONOLOGIA");
            }
            catch (Exception ex)
            {
                rdFormulario.Close();
                rdFormulario.Dispose();
                Response.Write("Error al actualizar los parámetros del report: " + ex.Message);
            }		
            //try
            //{

            //    string[] aID = Regex.Split(Request.QueryString["ID"], @",");


            //    //filter the data by setting the .RecordSelectionFormula property
            //    //of rdOrders to just return those orders that were shipped via UPS

            //    string strFormula = "";
            //    for (int i = 0; i < aID.Length; i++)
            //    {
            //        strFormula += "{GESTAR_DEFICIENCIA_INF;1.T044_IDDEFICIENCIA} = " + aID[i] + " OR";
            //    }
            //    strFormula = strFormula.Substring(0, strFormula.Length - 3);
            //    rdFormulario.RecordSelectionFormula = strFormula;
            //}
            //catch (Exception ex)
            //{
            //    rdFormulario.Close();
            //    rdFormulario.Dispose();
            //    Response.Write("Error al actualizar los parámetros del report: " + ex.Message);
            //}		
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
                String nav = HttpContext.Current.Request.Browser.Browser.ToString();
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
