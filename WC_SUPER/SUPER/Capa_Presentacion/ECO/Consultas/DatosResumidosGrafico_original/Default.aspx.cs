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
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Resumen Gráfico";
                Master.FuncionesJavaScript.Add("Javascript/FusionCharts.js");

                try
                {
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    this.txtAnno.Text = DateTime.Now.Year.ToString();
                    cargarNodos();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getDatos"):
                sResultado += getDatos(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private void cargarNodos()
    {
        try
        {
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"], true);
            while (dr.Read())
            {
                oLI = new ListItem(dr["DENOMINACION"].ToString(), dr["IDENTIFICADOR"].ToString());
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private string getDatos(int nNodo, int nAnno)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = ConsultasPGE.ObtenerDatosResumidosGraficosNodo(nNodo, nAnno);
            while (dr.Read())
            {
                sb.Append(dr["T325_ANOMES"].ToString() +"##");
                sb.Append(dr["Ingresos_Netos"].ToString().Replace(",",".") + "##");
                sb.Append(dr["Margen"].ToString().Replace(",",".") + "##");
                sb.Append(dr["Obra_en_curso"].ToString().Replace(",",".") + "##");
                sb.Append(dr["Saldo_de_Clientes"].ToString().Replace(",",".") + "##");
                sb.Append(dr["Total_Cobros"].ToString().Replace(",",".") + "##");
                sb.Append(dr["Total_Ingresos"].ToString().Replace(",",".") + "##");
                sb.Append(dr["Volumen_de_Negocio"].ToString().Replace(",",".") + "##");
                sb.Append(dr["Total_consumos"].ToString().Replace(",", ".") + "##");
                sb.Append(dr["Ratio"].ToString().Replace(",",".") + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos", ex);
        }
    }


}
