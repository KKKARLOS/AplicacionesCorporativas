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
using SUPER.Capa_Negocio;

public partial class PROYECTOS_NOCERRADOS : System.Web.UI.Page
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
            bool bMostrar = true;
            int int_pinicio;
            int int_pfin;
            string strServer;
            string strDataBase;
            string strUid;
            string strPwd;

            string pf = "ctl00$CPHC$";

//            pf = "";
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
            ReportDocument rdInforme = new ReportDocument();
            
            try
            {
                //if (Request.Form[pf + "rdbOrdenacion"]=="1")
                //    rdInforme.Load(Server.MapPath(".") + @"\sup_asignproyprof.rpt");
                //else
                //    rdInforme.Load(Server.MapPath(".") + @"\sup_asignprofproy.rpt");
                rdInforme.Load(Server.MapPath(".") + @"\sup_proynocerr.rpt");
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
            string strTipoFormato = Request.Form[pf + "FORMATO"]; // ESTO LO RECOGERE COMO PARAMETRO
            strControl = strTipoFormato;

            DiskFileDestinationOptions diskOpts = ExportOptions.CreateDiskFileDestinationOptions();
            ExportOptions exportOpts = new ExportOptions();
           
            try
            {
                rdInforme.SetParameterValue("CABECERA", "Mes de referencia: " + Request.Form[pf + "txtDesde"].ToString());
                rdInforme.SetParameterValue("path", Server.MapPath(".") + "//");

                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
			        rdInforme.SetParameterValue("@t314_idusuario", 0);
		        else
                    rdInforme.SetParameterValue("@t314_idusuario", Session["UsuarioActual"].ToString());
                string sNivel = Request.Form[pf + "hdnNivelEstructura"];

                //rdInforme.SetParameterValue("@nNivelEstructura", (sNivel=="0")? null:sNivel);    
                rdInforme.SetParameterValue("@nNivelEstructura", Request.Form[pf + "hdnNivelEstructura"]);    
                rdInforme.SetParameterValue("@t301_categoria", Request.Form[pf + "cboCategoria"]);
                rdInforme.SetParameterValue("@t305_cualidad", Request.Form[pf + "cboCualidad"]);
                rdInforme.SetParameterValue("@sClientes", Request.Form[pf + "hdnClientes"]);
                rdInforme.SetParameterValue("@sResponsables", Request.Form[pf + "hdnResponsables"]);
                rdInforme.SetParameterValue("@sNaturalezas", Request.Form[pf + "hdnNaturalezas"]);
                rdInforme.SetParameterValue("@sHorizontal", Request.Form[pf + "hdnHorizontales"]);
                rdInforme.SetParameterValue("@sModeloContrato", Request.Form[pf + "hdnModeloCons"]);
                rdInforme.SetParameterValue("@sContrato", Request.Form[pf + "hdnContratos"]);
                rdInforme.SetParameterValue("@sIDEstructura", Request.Form[pf + "hdnEstrucAmbitos"]);
                rdInforme.SetParameterValue("@sSectores", Request.Form[pf + "hdnSectores"]);
                rdInforme.SetParameterValue("@sSegmentos", Request.Form[pf + "hdnSegmentos"]);
                rdInforme.SetParameterValue("@bComparacionLogica", Request.Form[pf + "rdbOperador"]);
                rdInforme.SetParameterValue("@sProyectos", Request.Form[pf + "hdnProyectos"]);
                rdInforme.SetParameterValue("@sCNP", Request.Form[pf + "hdnCNP"]);
                rdInforme.SetParameterValue("@sCSN1P", Request.Form[pf + "hdnCSN1P"]);
                rdInforme.SetParameterValue("@sCSN2P", Request.Form[pf + "hdnCSN2P"]);
                rdInforme.SetParameterValue("@sCSN3P", Request.Form[pf + "hdnCSN3P"]);
                rdInforme.SetParameterValue("@sCSN4P", Request.Form[pf + "hdnCSN4P"]);
                //rdInforme.SetParameterValue("path", Server.MapPath(".") + "\\..\\..\\Rpts//");  
                //rdInforme.SetParameterValue("CABECERA", "PROYECTOS NO CERRADOS EN : " + Request.Form[pf + "txtDesde"].ToString());
                rdInforme.SetParameterValue("@nAnoMes", Request.Form[pf + "hdnDesde"]);
                rdInforme.SetParameterValue("@NODO", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
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

                switch (strTipoFormato)
                {
                    //			PDF			
                    case "PDF":
                        oStream = rdInforme.ExportToStream(ExportFormatType.PortableDocFormat);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                    case "EXC":
                        oStream = rdInforme.ExportToStream(ExportFormatType.Excel);
                        byteArray = new byte[oStream.Length];
                        oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                        break;
                    case "EXC2":
                        //			
                        rdInforme.ExportToHttpResponse(ExportFormatType.Excel, Response, true, "Exportacion");
                        bMostrar = false;
                        break;
                }
                
                // FIN
                if (bMostrar)
                {
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();

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
                            break;
                    }	
                    Response.BinaryWrite(byteArray);

                    //Response.Flush();
                    //Response.Close();
                    //HttpContext.Current.Response.End();
                    Response.End();
                }
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
    ~PROYECTOS_NOCERRADOS()
    {
        // Simply call Dispose(false).
        this.Dispose();
    }
}
