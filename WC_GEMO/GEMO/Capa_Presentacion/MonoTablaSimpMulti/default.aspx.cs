using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using GEMO.BLL;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.JScript;

public partial class Default : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "",sOpcion="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["sTS"] == "S")
        {
            this.seleccion_sim.Style.Add("display", "block");
            this.seleccion_mul.Style.Add("display", "none");
        }
        else
        {
            this.seleccion_sim.Style.Add("display", "none");
            this.seleccion_mul.Style.Add("display", "block");
        }


        try
        {
            switch (int.Parse(Request.QueryString["nT"]))
            {
                case 1: 	// EMPRESA
                    sOpcion = "Empresas";
                    break;
                case 2:		//
                    break;
                case 3:		// 
                    break;
                case 4: 	// DEPARTAMENTO
                    sOpcion = "Departamentos";
                    break;
                case 5: 	// ESTADO
                    sOpcion = "Estados";
                    break;
                case 6: 	// MEDIO
                    sOpcion = "Medios";
                    break;

            }
            strTablaHTML = ObtenerDatos(int.Parse(Request.QueryString["nT"]), Request.QueryString["sTS"]);
            //string[] aTabla0 = Regex.Split(strTabla0, "@#@");
            //strTablaHTML = aTabla0[0];
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
    protected string ObtenerDatos(int nOpcion, string sTS)
    {
        string sResul = "";
        try
        {
            switch (nOpcion)
            {
                case 1: 	// EMPRESA
                    sResul += GEMO.BLL.EMPRESA.Obtener2(sTS);
                    break;
                case 2:		//
                    break;
                case 3:		// 
                    break;
                case 4: 	// DEPARTAMENTO
                    sResul += GEMO.BLL.DEPARTAMENTO.Obtener2(sTS);
                    break;
                case 5: 	// ESTADO
                    sResul += GEMO.BLL.ESTADO.Obtener2(sTS);
                    break;
                case 6: 	// MEDIO
                    sResul += GEMO.BLL.MEDIO.Obtener2(sTS);
                    break;

            }
        }
        catch (System.Exception objError)
        {
            sResul = Errores.mostrarError("Error al leer la opción : " + nOpcion.ToString(), objError);
            throw (new Exception(sResul));
        }
        return sResul;
    }
}
