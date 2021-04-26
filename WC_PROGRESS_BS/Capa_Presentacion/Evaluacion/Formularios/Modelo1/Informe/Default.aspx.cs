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
using IB.Progress.Shared;

public partial class Default : System.Web.UI.Page
{
    public string strControl = "";
    private void Page_Load(object sender, System.EventArgs e)
    {
      
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

            //IB.Progress.Models.Estadisticas rf = new IB.Progress.Models.Estadisticas();

//            string pf = "ctl00$CPHC$";
//				obtengo de la cadena de conexión los parámetros para luego
//				modificar localizaciones 

            string strconexion = IB.Progress.Shared.Database.GetConStr();
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

            string IdEvaluacion = Utils.decodpar(Request.QueryString["IdEvaluacion"].ToString());
            int iModelo = int.Parse(Request.QueryString["modelo"].ToString());

            try
            {
                if (iModelo == 1)
                   rdFormulario.Load(Server.MapPath(".") + @"\pro_formulario_mod_1_salto.rpt");
                else
                   rdFormulario.Load(Server.MapPath(".") + @"\pro_formulario_mod_2_salto.rpt");
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

            string strTipoFormato = "PDF"; // Request.QueryString[pf + "FORMATO"]; // ESTO LO RECOGERE COMO PARAMETRO
            strControl = strTipoFormato;

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();

            try
            {
                int idficepiConectado = int.Parse(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi.ToString());
                rdFormulario.SetParameterValue("@idvaloraciones", IdEvaluacion);
                rdFormulario.SetParameterValue("@t001_idficepi_encurso", idficepiConectado);
                //rdFormulario.SetParameterValue("@idvaloraciones", "18435");
				rdFormulario.SetParameterValue("path", Server.MapPath(".") + "//");
            }
            catch (Exception ex)
            {
                rdFormulario.Close();
                rdFormulario.Dispose();
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
                        oStream = rdFormulario.ExportToStream(ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                    case "EXC":
                        oStream = rdFormulario.ExportToStream(ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                }
                

                // FIN

                //Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

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
                Response.Clear();

                //String nav = HttpContext.Current.Request.Browser.Browser.ToString();
                //if (nav.IndexOf("IE") == -1)
                //{
                    switch (strTipoFormato)
                    {
                        case "PDF":
                            //Response.AddHeader
                            //("Content-Disposition", "attachment;filename=Filename.pdf");
                            break;
                        case "EXC":
                            Response.AddHeader
                            ("Content-Disposition", "attachment;filename=Filename.xls");
                            break;
                    }
                //}
                
                Response.BinaryWrite(byteArray);

                Response.Flush();
                Response.Close();
                //HttpContext.Current.Response.End();
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
    ~Default()
    {
        // Simply call Dispose(false).
        this.Dispose();
    }
}
