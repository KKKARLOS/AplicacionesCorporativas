using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Services;
using System.Web.Services;
using SUPER.Capa_Negocio;
using System.Collections.Generic;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 0;
                Master.sbotonesOpcionOn = "4";
                Master.sbotonesOpcionOff = "4";

                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Asignación a provincias de una zona seleccionada";

                cargarCboPaisesGes();
                cargarCboAmbitos();

                //PAIS.CatalogoPaisesGesCache(cboPaisGes, "");
                //AMBITO.CatalogoAmbitosCache(cboAmbito, "");

            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
    }
    private void cargarCboPaisesGes()
    {
		ListItem Elemento;
		List<PAIS> ListaPaisesGes = PAIS.ListaPaisesGes();

		foreach (PAIS oPais in ListaPaisesGes)
		{
            Elemento = new ListItem(oPais.t172_denominacion, oPais.t172_idpais.ToString());
			this.cboPaisGes.Items.Add(Elemento);
		}   
        cboPaisGes.Items.Insert(0, new ListItem("", ""));
    }

    private void cargarCboAmbitos()
    {
        ListItem Elemento;

        List<AMBITO> ListaAmbitos = AMBITO.ListaAmbitos();

        foreach (AMBITO oAmbito in ListaAmbitos)
        {
            Elemento = new ListItem(oAmbito.t481_denominacion, oAmbito.t481_idambito.ToString());
            this.cboAmbito.Items.Add(Elemento);
        }
        cboAmbito.Items.Insert(0, new ListItem("", ""));
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string provinciasGtonPais(string sID)
    {
        try
        {
            return "OK@#@" + PAIS.cargarProvinciasGtonPais(sID);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al cargar las provincias de gtón relacionadas a un país determinado.", ex);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string zonasAmbito(string sID)
    {
        try
        {
            return "OK@#@" + AMBITO.cargarZonasAmbito(sID);
        }            
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al cargar las zonas relacionadas a un ámbito determinado.", ex);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string Grabar(string sProvincias)
    {
        try
        {
            return "OK@#@" + PROVINCIA.Grabar(sProvincias);
        }            
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarErrorAjax("Error al grabar las asignaciones Zonas/Provincias.", ex);
        }
    }
}
