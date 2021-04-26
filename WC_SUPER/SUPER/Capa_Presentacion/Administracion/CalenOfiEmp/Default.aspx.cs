using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EO.Web;   
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Capa_Presentacion_Administracion_CalenOfiEmp_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strTablaHtml = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 9;
            //Poniendo el siguiente atributo a true, se incluye el fichero javascript propio
            //de la carpeta hija "Functions/funciones.js"
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Calendario de empresa oficina";

            try
            {
                ObtenerDatosFiltros();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos de empresas y oficinas", ex);
            }
            try
            {
                strTablaHtml = ObtenerDatos(short.Parse(this.cboEmpresa.SelectedValue), short.Parse(this.cboOficina.SelectedValue), byte.Parse(this.cboEmpleados.SelectedValue));
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos de empresas y oficinas", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }

    public void ObtenerDatosFiltros()
    {
        SqlDataReader dr = EMPRESA.Catalogo(true);
        cboEmpresa.AppendDataBoundItems = true;
        cboEmpresa.DataValueField = "T313_IDEMPRESA";
        cboEmpresa.DataTextField = "T313_DENOMINACION";
        cboEmpresa.DataSource = dr;
        cboEmpresa.DataBind();
        dr.Close();
        dr.Dispose();

        SqlDataReader dr1 = OFICINA.Catalogo(null, "", 2, 0);
        cboOficina.AppendDataBoundItems = true;
        cboOficina.DataValueField = "T010_IDOFICINA";
        cboOficina.DataTextField = "T010_DESOFICINA";
        cboOficina.DataSource = dr1;
        cboOficina.DataBind();
        dr1.Close();
        dr1.Dispose();
    }
    
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "grabar":
                sResultado += Grabar(aArgs[1]);
                break;
            case "buscar":
                sResultado += ObtenerDatosAux(short.Parse(aArgs[1]), short.Parse(aArgs[2]), byte.Parse(aArgs[3]));
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

    private string ObtenerDatos(short nEmpresa, short nOficina, byte nEmpleado)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 950px;'>");
        sb.Append("<colgroup><col style='width:300px;' /><col style='width:300px' /><col style='width:300px' /><col style='width:50px;' /></colgroup>");
        sb.Append("<tbody>");
        SqlDataReader dr = OFIEMPCAL.Catalogo(nEmpresa, nOficina, nEmpleado);
        while (dr.Read())
        {
            sb.Append("<tr id='" + i.ToString() + "' emp='" + dr["T313_IDEMPRESA"].ToString() + "' ofi='" + dr["T010_IDOFICINA"].ToString() + "' cal='" + dr["t066_idcal"].ToString() + "' bd='' onclick='ms(this);' ondblclick='getCal(this);' style='height:16px;'>");
            sb.Append("<td style='padding-left:5px;'>" + dr["t313_denominacion"].ToString() + "</td>");
            sb.Append("<td>" + dr["T010_DESOFICINA"].ToString() + "</td>");
            sb.Append("<td>" + dr["t066_DESCAL"].ToString() + "</td>");
            sb.Append("<td style='text-align:right;padding-right:5px;'>" + dr["NUMERO"].ToString() + "</td></tr>");
            i++;
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");
        return sb.ToString();
    }

    private string ObtenerDatosAux(short nEmpresa, short nOficina, byte nEmpleado)
    {
        string sResul = "";
        try
        {
            sResul = ObtenerDatos(nEmpresa, nOficina, nEmpleado);
            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos", ex);
        }

    }

    protected string Grabar(string strDatos)
    {
        string sResul = "";
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }

        try
        {
            string[] aEIC = Regex.Split(strDatos, "///");

            foreach (string oEIC in aEIC)
            {
                if (oEIC == "") break;
                string[] aDatos = Regex.Split(oEIC, "##");
                ///aDatos[0] = empresa;
                ///aDatos[1] = oficina;
                ///aDatos[2] = calendario;

                OFIEMPCAL.Update(tr, short.Parse(aDatos[1]), short.Parse(aDatos[0]), int.Parse(aDatos[2]));
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + ID.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de los calendarios", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
