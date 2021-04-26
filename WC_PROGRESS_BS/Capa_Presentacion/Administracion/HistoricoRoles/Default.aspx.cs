using IB.Progress.Models;
using IB.Progress.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_Mantenimientos_HistoricoRoles_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {

        
            //Se obtiene el año mínimo en el que existe una valoración
            int anyomin = obtenerMinAnyoValoracion();

            //Se rellenan los combos en función de ese año y se ponen los defectos (Enero-Año Actual -> mes actual - Año Actual)
            rellenarComboMes(selMesIni);
            rellenarComboMes(selMesFin);
            rellenarComboAnno(selAnoIni, anyomin);
            rellenarComboAnno(selAnoFin, anyomin);


            string mesCombo;

            if (DateTime.Now.Month < 10)
                mesCombo = "0" + (DateTime.Now.Month).ToString();
            else
                mesCombo = (DateTime.Now.Month).ToString();

            selAnoIni.Value = anyomin.ToString();
            selMesIni.Value = "1";
            selAnoFin.Value = (DateTime.Now.Year).ToString();
            selMesFin.Value = mesCombo;
        }
        catch (Exception ex)
        {

            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string historicoRoles(string t001_apellido1, string t001_apellido2, string t001_nombre, int desde, int hasta)
    {

        IB.Progress.BLL.ROLIB rolesBLL = null;
        
        List<ROLIB> miHistorico = new List<ROLIB>();        
        string retval = String.Empty;

        try
        {
            rolesBLL = new IB.Progress.BLL.ROLIB();
            miHistorico = rolesBLL.CatHistoricoRoles(t001_apellido1, t001_apellido2, t001_nombre, desde, hasta);
            rolesBLL.Dispose();

            retval = JsonConvert.SerializeObject(miHistorico);    
            return retval;
        }
        catch (Exception ex)
        {
            if (rolesBLL != null) rolesBLL.Dispose();

            //PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp

            Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
            throw ex;
        }
    }



    //Obtiene el año de la valoración más antigua
    public static int obtenerMinAnyoValoracion()
    {
        IB.Progress.BLL.Estadisticas cEst = null;
        try
        {
            cEst = new IB.Progress.BLL.Estadisticas();

            int anyo = cEst.obtenerMinAnyoValoracion();
            cEst.Dispose();

            string retval = JsonConvert.SerializeObject(anyo);
            return anyo;
        }
        catch (Exception ex)
        {
            if (cEst != null) cEst.Dispose();
            throw ex;
        }

    }

    //Rellena los meses del combo que se le pasa como parámetro
    List<string> meses = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    protected void rellenarComboMes(HtmlSelect combo)
    {
        foreach (string ms in meses)
        {
            ListItem opt = new ListItem();
            opt.Value = (meses.IndexOf(ms) + 1).ToString("00");
            opt.Text = ms;
            combo.Items.Add(opt);
        }
    }

    //Rellenar los años del combo que se le pasa como parámetro desde anno (de la valoración más antigua) hasta el año actual
    protected void rellenarComboAnno(HtmlSelect combo, int anno)
    {
        for (int k = DateTime.Now.Year; k >= anno; k--)
        {
            ListItem opt = new ListItem();
            opt.Value = k.ToString();
            opt.Text = k.ToString();
            combo.Items.Add(opt);
        }
    }
}