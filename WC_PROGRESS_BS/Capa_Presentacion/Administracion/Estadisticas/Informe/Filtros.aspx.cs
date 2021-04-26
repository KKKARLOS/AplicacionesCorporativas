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
            ReportDocument rdEstadisticas = new ReportDocument();
            try
            {
                rdEstadisticas.Load(Server.MapPath(".") + @"\estadisticas_valores_1.rpt");
            }
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
                Response.Write("Error al abrir el report: " + ex.Message);
            }

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();

            try
            {
                IB.Progress.Models.Estadisticas rf = (IB.Progress.Models.Estadisticas)Session["ValoresRPT"];
                IB.Progress.Models.ParamsRPT rfParamsRpt = (IB.Progress.Models.ParamsRPT)Session["ParamsRPT"];

                //Parámetros Modelo 1
                string Origen = Request.QueryString["origen"].ToString();
                rdEstadisticas.SetParameterValue("Origen", Origen);

                rdEstadisticas.SetParameterValue("FechaDesde", rfParamsRpt.fecDesde.ToString());
                rdEstadisticas.SetParameterValue("FechaHasta", rfParamsRpt.fecHasta.ToString());
                rdEstadisticas.SetParameterValue("Profundizacion", rfParamsRpt.txtProfundizacion.ToString());

                rdEstadisticas.SetParameterValue("Sexo", (rfParamsRpt.sexo == null) ? "" : rfParamsRpt.sexo.ToString());
                rdEstadisticas.SetParameterValue("Evaluador", rfParamsRpt.txtEvaluador.ToString());
                rdEstadisticas.SetParameterValue("Profundizacion", rfParamsRpt.txtProfundizacion.ToString());
                rdEstadisticas.SetParameterValue("AntiguedadReferencia", rf.t001_fecantigu.ToString());
                rdEstadisticas.SetParameterValue("Colectivo", rfParamsRpt.txtColectivo.ToString());
                rdEstadisticas.SetParameterValue("Situacion", rfParamsRpt.txtSituacion.ToString());

                rdEstadisticas.SetParameterValue("profevh", rf.profevh.ToString());
                rdEstadisticas.SetParameterValue("profevm", rf.profevm.ToString());
                rdEstadisticas.SetParameterValue("profevt", rf.profevt.ToString());
                rdEstadisticas.SetParameterValue("profevhant", rf.profevhant.ToString());
                rdEstadisticas.SetParameterValue("profevmant", rf.profevmant.ToString());
                rdEstadisticas.SetParameterValue("profevantt", rf.profevantt.ToString());

                rdEstadisticas.SetParameterValue("profnoevh", rf.profnoevh.ToString());
                rdEstadisticas.SetParameterValue("profnoevm", rf.profnoevm.ToString());
                rdEstadisticas.SetParameterValue("profnoevt", rf.profnoevt.ToString());
                rdEstadisticas.SetParameterValue("profnoevhant", rf.profnoevhant.ToString());
                rdEstadisticas.SetParameterValue("profnoevmant", rf.profnoevmant.ToString());
                rdEstadisticas.SetParameterValue("profnoevantt", rf.profnoevantt.ToString());

                rdEstadisticas.SetParameterValue("profht", rf.profht.ToString());
                rdEstadisticas.SetParameterValue("profmt", rf.profmt.ToString());
                rdEstadisticas.SetParameterValue("proft", rf.proft.ToString());
                rdEstadisticas.SetParameterValue("profhantt", rf.profhantt.ToString());
                rdEstadisticas.SetParameterValue("profmantt", rf.profmantt.ToString());
                rdEstadisticas.SetParameterValue("profantt", rf.profantt.ToString());

                rdEstadisticas.SetParameterValue("evabiertah", rf.evabiertah.ToString());
                rdEstadisticas.SetParameterValue("evabiertam", rf.evabiertam.ToString());
                rdEstadisticas.SetParameterValue("evabiertat", rf.evabiertat.ToString());
                rdEstadisticas.SetParameterValue("evabiertahant", rf.evabiertahant.ToString());
                rdEstadisticas.SetParameterValue("evabiertamant", rf.evabiertamant.ToString());
                rdEstadisticas.SetParameterValue("evabiertaantt", rf.evabiertaantt.ToString());


                rdEstadisticas.SetParameterValue("evcursoh", rf.evcursoh.ToString());
                rdEstadisticas.SetParameterValue("evcursom", rf.evcursom.ToString());
                rdEstadisticas.SetParameterValue("evcursot", rf.evcursot.ToString());
                rdEstadisticas.SetParameterValue("evcursohant", rf.evcursohant.ToString());
                rdEstadisticas.SetParameterValue("evcursomant", rf.evcursomant.ToString());
                rdEstadisticas.SetParameterValue("evcursoantt", rf.evcursoantt.ToString());

                rdEstadisticas.SetParameterValue("evcerradah", rf.evcerradah.ToString());
                rdEstadisticas.SetParameterValue("evcerradam", rf.evcerradam.ToString());
                rdEstadisticas.SetParameterValue("evcerradat", rf.evcursot.ToString());
                rdEstadisticas.SetParameterValue("evcerradahant", rf.evcerradahant.ToString());
                rdEstadisticas.SetParameterValue("evcerradamant", rf.evcerradamant.ToString());
                rdEstadisticas.SetParameterValue("evcerradaantt", rf.evcerradaantt.ToString());

                rdEstadisticas.SetParameterValue("evfirmadah", rf.evfirmadah.ToString());
                rdEstadisticas.SetParameterValue("evfirmadam", rf.evfirmadam.ToString());
                rdEstadisticas.SetParameterValue("evfirmadat", rf.evfirmadat.ToString());
                rdEstadisticas.SetParameterValue("evfirmadahant", rf.evfirmadahant.ToString());
                rdEstadisticas.SetParameterValue("evfirmadamant", rf.evfirmadamant.ToString());
                rdEstadisticas.SetParameterValue("evfirmadaantt", rf.evfirmadaantt.ToString());

                rdEstadisticas.SetParameterValue("evautomaticah", rf.evautomaticah.ToString());
                rdEstadisticas.SetParameterValue("evautomaticam", rf.evautomaticam.ToString());
                rdEstadisticas.SetParameterValue("evautomaticat", rf.evautomaticat.ToString());
                rdEstadisticas.SetParameterValue("evautomaticahant", rf.evautomaticahant.ToString());
                rdEstadisticas.SetParameterValue("evautomaticamant", rf.evautomaticamant.ToString());
                rdEstadisticas.SetParameterValue("evautomaticaantt", rf.evautomaticaantt.ToString());

                rdEstadisticas.SetParameterValue("evht", rf.evht.ToString());
                rdEstadisticas.SetParameterValue("evmt", rf.evmt.ToString());
                rdEstadisticas.SetParameterValue("evt", rf.evt.ToString());
                rdEstadisticas.SetParameterValue("evhantt", rf.evhantt.ToString());
                rdEstadisticas.SetParameterValue("evmantt", rf.evmantt.ToString());
                rdEstadisticas.SetParameterValue("evantt", rf.evantt.ToString());  
            }
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
                Response.Write("Error al actualizar los parámetros del report: " + ex.Message);
            }
            try
            {
                System.IO.Stream oStream;
                byte[] byteArray = null;

                oStream = rdEstadisticas.ExportToStream(ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                

                // FIN

                //Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";

                Response.Clear();
               
                Response.BinaryWrite(byteArray);

                Response.Flush();
                Response.Close();
                //HttpContext.Current.Response.End();
                Response.End();
            }            
            catch (Exception ex)
            {
                rdEstadisticas.Close();
                rdEstadisticas.Dispose();
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
