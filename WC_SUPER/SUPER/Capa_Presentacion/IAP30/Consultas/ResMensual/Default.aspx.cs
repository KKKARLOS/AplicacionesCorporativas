using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using IB.SUPER.Shared;

using System.Data;
using System.Data.SqlClient;


public partial class Capa_Presentacion_Consultas_ResMensual_Default : System.Web.UI.Page
{
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
        //Recogida de parámetros y volcado en IB.vars
        try
        {
            string script1 = "IB.vars.idficepi = '" + Session["IDFICEPI_IAP"] + "';";
        
            script1 += "IB.vars.codUsu = '" + Session["NUM_EMPLEADO_IAP"].ToString() + "';";

            if (Request.QueryString["nDesde"] == null)
            {
                DateTime dFechaUMC = Fechas.AnnomesAFecha((int)Session["UMC_IAP"]); //DateTime.Today.AddMonths(-1);
                script1 += "IB.vars.fechaDesde = '" + (dFechaUMC.Year * 100 + dFechaUMC.Month).ToString() + "';";
                //script1 += "IB.vars.fechaDesde = '201601';";
                txtInicio.Value = mes[dFechaUMC.Month - 1] + " " + dFechaUMC.Year.ToString();
                script1 += "IB.vars.fechaHasta = '" + (dFechaUMC.Year * 100 + dFechaUMC.Month).ToString() + "';";
                txtFin.Value = txtInicio.Value;
                txtAno.Value = dFechaUMC.Year.ToString();
                txtAnoMes.Value = txtInicio.Value;
                txtMes.Value = mes[dFechaUMC.Month - 1];
            }
            else
            {
                script1 += "IB.vars.fechaDesde = '" + Request.QueryString["nDesde"].ToString() + "';";
                DateTime dFecha = Fechas.AnnomesAFecha(int.Parse(Request.QueryString["nDesde"].ToString()));
                txtInicio.Value = mes[dFecha.Month - 1] + " " + dFecha.Year.ToString();
                script1 += "IB.vars.fechaHasta = '" + Request.QueryString["nHasta"].ToString() + "';";
                
                dFecha = Fechas.AnnomesAFecha(int.Parse(Request.QueryString["nHasta"].ToString()));
                txtFin.Value = mes[dFecha.Month - 1] + " " + dFecha.Year.ToString();
                txtAno.Value = dFecha.Year.ToString();
                txtAnoMes.Value = mes[dFecha.Month - 1] + " " + dFecha.Year.ToString();
                txtMes.Value = mes[dFecha.Month - 1];
            }
            script1 += "IB.vars.fechaInicioOld = '" + txtInicio.Value + "';";
            script1 += "IB.vars.fechaFinOld = '" + txtFin.Value + "';";
            script1 += "IB.vars.ano = '" + txtAno.Value + "';";

            script1 += "IB.vars.localizacion = ''";
            //registramos en un form runat='server'
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars.error = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.InforImpuMen> getDatos(string sProfesionales, string sDesde, string sHasta, string sTipo)
    {
        IB.SUPER.IAP30.BLL.InforImpuMen cInforImpuMen = new IB.SUPER.IAP30.BLL.InforImpuMen();

        try
        {
            List<Models.InforImpuMen> lInforImpuMen = cInforImpuMen.Catalogo(sProfesionales, int.Parse(sDesde), int.Parse(sHasta), sTipo);
            return lInforImpuMen;
        }
        catch (Exception ex)
        {
            if (cInforImpuMen != null) cInforImpuMen.Dispose();
            throw ex;
        }
        finally
        {
            cInforImpuMen.Dispose();
        }
    }
}