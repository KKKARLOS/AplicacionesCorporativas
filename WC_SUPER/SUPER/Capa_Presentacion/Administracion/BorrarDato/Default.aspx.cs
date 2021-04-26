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
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML = "";
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public StringBuilder sb = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.nBotonera = 43;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Borrado de datos económicos";
                
            if (!Page.IsCallback)
            {
                try
                {
                    hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                    txtDesde.Text = mes[0] + " " + DateTime.Now.Year.ToString();
                    hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                    txtHasta.Text = mes[DateTime.Now.Month - 1] + " " + DateTime.Now.Year.ToString();

                    getArbolClases();
                }
                catch (Exception ex)
                {
                    Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
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
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15]);
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

    protected void getArbolClases()
    {
        sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 560px;'>");
            //sb.Append("<colgroup>");
            //sb.Append("    <col style='padding-left:3px;' />");
            ////sb.Append("    <col style='width:250px;' />");
            ////sb.Append("    <col style='width:300px;' />");
            //sb.Append("</colgroup>");
            sb.Append("<tbody>");

            byte nGrupo = 0, nSubgrupo = 0, nConcepto = 0;
            int nClase = 0;

            SqlDataReader dr = CLASEECO.ObtenerClasesBorrables(null);
            while (dr.Read())
            {
                if (nGrupo != (byte)dr["t326_idgrupoeco"])
                {
                    nGrupo = (byte)dr["t326_idgrupoeco"];
                    nSubgrupo = (byte)dr["t327_idsubgrupoeco"];
                    nConcepto = (byte)dr["t328_idconceptoeco"];
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearGrupo(dr);
                }
                else if (nSubgrupo != (byte)dr["t327_idsubgrupoeco"])
                {
                    nSubgrupo = (byte)dr["t327_idsubgrupoeco"];
                    nConcepto = (byte)dr["t328_idconceptoeco"];
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearSubgrupo(dr);
                }
                else if (nConcepto != (byte)dr["t328_idconceptoeco"])
                {
                    nConcepto = (byte)dr["t328_idconceptoeco"];
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearConcepto(dr);
                }
                else
                {
                    nClase = (int)dr["t329_idclaseeco"];
                    CrearClase(dr);
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener las clases económicas.", ex);
        }
    }
    private void CrearGrupo(IDataReader dr)
    {
        sb.Append("<tr tipo='G' style='height:20px;'>");
        sb.Append("<td style='padding-left:5px;'>" + dr["t326_denominacion"].ToString() + "</td>");
        sb.Append("</tr>");
        CrearSubgrupo(dr);
    }
    private void CrearSubgrupo(IDataReader dr)
    {
        sb.Append("<tr tipo='S' style='height:20px;'>");
        sb.Append("<td style='padding-left:50px;'>" + dr["t327_denominacion"].ToString() + "</td>");
        sb.Append("</tr>");
        CrearConcepto(dr);
    }
    private void CrearConcepto(IDataReader dr)
    {
        sb.Append("<tr tipo='C' style='height:20px;' >");
        sb.Append("<td style='padding-left:100px;'>" + dr["t328_denominacion"].ToString() + "</td>");
        sb.Append("</tr>");
        CrearClase(dr);
    }
    private void CrearClase(IDataReader dr)
    {
        sb.Append("<tr tipo='CL' id='" + dr["t329_idclaseeco"].ToString() + "' style='height:20px;cursor:pointer;' >");
        sb.Append("<td style='padding-left:150px;'><input type='checkbox' class='checkTabla' style='vertical-align:middle;' /><label style='cursor:pointer; margin-left:5px;'  onclick='this.previousSibling.click()'>" + dr["t329_denominacion"].ToString() + "</label></td>");
        sb.Append("</tr>");
    }

    protected string Procesar(string sDesde, string sHasta, string sResponsables, string sSubnodos, string sPSN, string sClasesABorrar, string sConsPersonas, string sConsNivel, string sProdProfesional, string sProdPerfil, string sAvance, string sPeriodCons, string sPeriodProd, string sDeCirculante, string sIncMesesCerrados)
    {
        string sResul = "";

        #region apertura de conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            SEGMESPROYECTOSUBNODO.BorrarDatosMes(tr, int.Parse(sDesde), int.Parse(sHasta),
                                            sResponsables, 
                                            sSubnodos, 
                                            sPSN,
                                            sClasesABorrar,
                                            (sConsPersonas == "1") ? true : false,
                                            (sConsNivel == "1") ? true : false,
                                            (sProdProfesional == "1") ? true : false,
                                            (sProdPerfil == "1") ? true : false,
                                            (sAvance == "1") ? true : false,
                                            (sPeriodCons == "1") ? true : false,
                                            (sPeriodProd == "1") ? true : false,
                                            (sDeCirculante == "1") ? true : false,
                                            (sIncMesesCerrados == "1") ? true : false
                                            );
            Conexion.CommitTransaccion(tr);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar los datos seleccionados.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
}
