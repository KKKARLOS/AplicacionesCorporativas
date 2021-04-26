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
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Consultas_Imputaciones_Default : System.Web.UI.Page
{
    public string idficepi;
    public string strTablaHTML = "";
    protected int nIndice;
    public int i = 0;

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
            //Carga de guia de ayuda
            aspnetUtils.visualizarGuia(Menu);

            string script1 = "IB.vars.idficepi = '" + Session["IDFICEPI_IAP"] + "';";
            script1 += "IB.vars.codUsu = '" + Session["NUM_EMPLEADO_IAP"].ToString() + "';";
            DateTime dHoy = DateTime.Now, dtDesde, dtHasta;
            int nDias = dHoy.Day;
            dtDesde = dHoy.AddDays(-nDias + 1);
            script1 += "IB.vars.fechaDesde = '" + dtDesde.ToShortDateString() + "';";
            dtHasta = dtDesde.AddMonths(1).AddDays(-1);
            script1 += "IB.vars.fechaHasta = '" + dtHasta.ToShortDateString() + "';";

            //Se obtienen la lista de proyectos ligados a tareas con consumos en ese rango temporal y se cargan en el combo
    /*        IB.SUPER.IAP30.BLL.CONSUMOIAP_PROYECTOS cCONSUMOIAP_PROYECTOS = new IB.SUPER.IAP30.BLL.CONSUMOIAP_PROYECTOS();
            List<IB.SUPER.IAP30.Models.CONSUMOIAP_PROYECTOS> lConsumosIAP_Proyectos = cCONSUMOIAP_PROYECTOS.Catalogo((int)Session["UsuarioActual"],
                                                                                            dtDesde,
                                                                                            dtHasta);
            cCONSUMOIAP_PROYECTOS.Dispose();

            foreach (IB.SUPER.IAP30.Models.CONSUMOIAP_PROYECTOS l in lConsumosIAP_Proyectos)
            {
                ListItem option = new ListItem();
                option.Value = l.t305_idproyectosubnodo.ToString();
                option.Text = l.t305_seudonimo;
                cboProyecto.Items.Add(option);
            }

            string strTabla = obtenerDatos("", dtDesde.ToShortDateString(), dtHasta.ToShortDateString());

            string[] aTabla = Regex.Split(strTabla, "@#@");
            if (aTabla[0] == "OK")
            {
                this.strTablaHTML = aTabla[1];
            }else{
                script1 += "IB.vars.error = '" + aTabla[1] + "';";
            }
     */
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
    public static List<Models.CONSUMOIAP_PROYECTOS> getProyectos(string sUsuario, string sFechaDesde, string sFechaHasta)
    {
        IB.SUPER.IAP30.BLL.CONSUMOIAP_PROYECTOS cCONSUMOIAP_PROYECTOS = new IB.SUPER.IAP30.BLL.CONSUMOIAP_PROYECTOS();

        try
        {
            List<Models.CONSUMOIAP_PROYECTOS> lConsumosIAP_Proyectos = cCONSUMOIAP_PROYECTOS.Catalogo(int.Parse(sUsuario), DateTime.Parse(sFechaDesde), DateTime.Parse(sFechaHasta));
            return lConsumosIAP_Proyectos;
        }
        catch (Exception ex)
        {
            if (cCONSUMOIAP_PROYECTOS != null) cCONSUMOIAP_PROYECTOS.Dispose();
            throw ex;
        }
        finally
        {
            cCONSUMOIAP_PROYECTOS.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.ConsumoTecnicoIAP> obtenerDatos(string sUsuario, string sPSN, string sFechaDesde, string sFechaHasta)
    {
        IB.SUPER.IAP30.BLL.ConsumoTecnicoIAP cConsumoTecnicoIAP = new IB.SUPER.IAP30.BLL.ConsumoTecnicoIAP();

        try
        {
            List<Models.ConsumoTecnicoIAP> lConsumoTecnicoIAP = cConsumoTecnicoIAP.Catalogo(int.Parse(sUsuario), (sPSN == "") ? null : (int?)int.Parse(sPSN), DateTime.Parse(sFechaDesde), DateTime.Parse(sFechaHasta));
            return lConsumoTecnicoIAP;
        }
        catch (Exception ex)
        {
            if (cConsumoTecnicoIAP != null) cConsumoTecnicoIAP.Dispose();
            throw ex;
        }
        finally
        {
            cConsumoTecnicoIAP.Dispose();
        }
    }

}