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
    public string strTablaHTML = "", sNodos = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    ArrayList aNodos = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Proyectos con línea base";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/funcionesPestVertical.js");
                Master.nResolucion = 1280;

                lblNodo2.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                /*
                lblCDP.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                lblCSN1P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
                lblCSN2P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
                lblCSN3P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
                lblCSN4P.Attributes.Add("title", "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));
                
                
                if (!Utilidades.EstructuraActiva("SN4")) fstCSN4P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN3")) fstCSN3P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN2")) fstCSN2P.Style.Add("visibility", "hidden");
                if (!Utilidades.EstructuraActiva("SN1")) fstCSN1P.Style.Add("visibility", "hidden");
                */
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
            case ("buscar"):
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9]);
                break;
            //case ("getTablaCriterios"):
            //    sResultado += cargarCriterios();
            //    break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    //private string cargarCriterios()
    //{
    //    StringBuilder sb = new StringBuilder();
    //    int i = 0;
    //    try
    //    {
    //        /*
    //         * t -> tipo
    //         * c -> codigo
    //         * d -> denominacion
    //         * ///datos auxiliares para el catálogo de proyecto (16)
    //         * a -> categoria
    //         * u -> cualidad
    //         * e -> estado
    //         * l -> cliente
    //         * n -> nodo
    //         * r -> responsable
    //         * */
    //        SqlDataReader dr = ConsultasPGE.ObtenerCombosDatosResumidosCriterios((int)Session["UsuarioActual"], 0, 0, Constantes.nNumElementosMaxCriterios);
    //        while (dr.Read())
    //        {
    //            if ((int)dr["codigo"] == -1)
    //                sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"excede\":1};\n");
    //            else
    //            {
    //                if ((int)dr["tipo"] == 16)
    //                    sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\",\"p\":\"" + dr["t301_idproyecto"].ToString() + "\",\"a\":\"" + dr["t301_categoria"].ToString() + "\",\"u\":\"" + dr["t305_cualidad"].ToString() + "\",\"e\":\"" + dr["t301_estado"].ToString() + "\",\"l\":\"" + dr["t302_denominacion"].ToString() + "\",\"n\":\"" + dr["t303_denominacion"].ToString() + "\",\"r\":\"" + dr["Responsable"].ToString() + "\"};\n");
    //                else
    //                    sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\"};\n");
    //            }
    //            i++;
    //        }
    //        dr.Close();
    //        dr.Dispose();

    //        return "OK@#@" + sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
    //    }
    //}

    private string obtenerDatos(string sDenExperiencia, string sCategoria, string sEstado, string sCualidad, string sClientes, string sResponsables,
                                string sIDEstructura, string sComparacionLogica, string sPSN)
    {
        try
        {
            return "OK@#@" + SUPER.BLL.EXPPROF.ObtenerExperienciasProyectos(sDenExperiencia, int.Parse(Session["UsuarioActual"].ToString()), ((sCategoria == "") ? null : sCategoria),
                                ((sEstado == "") ? null : sEstado), ((sCualidad == "") ? null : sCualidad), sClientes,
                                sResponsables, sIDEstructura, (sComparacionLogica == "1") ? true : false, sPSN, true);
            //return "OK@#@" + ObtenerExperienciasProyectos(sDenExperiencia, int.Parse(Session["UsuarioActual"].ToString()), ((sCategoria == "") ? null : sCategoria),
            //                    ((sEstado == "") ? null : sEstado), ((sCualidad == "") ? null : sCualidad), sClientes,
            //                    sResponsables, sIDEstructura, (sComparacionLogica == "1") ? true : false, sPSN, true);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener datos de la consulta de experiencia.", ex);
        }
    }

    //private string setResolucion()
    //{
    //    try
    //    {
    //        Session["DATOSRES1024"] = !(bool)Session["DATOSRES1024"];

    //        USUARIO.UpdateResolucion(4, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["DATOSRES1024"]);

    //        return "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
    //    }
    //}



}