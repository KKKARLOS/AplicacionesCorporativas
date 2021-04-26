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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    public string strTituloMovilHTML = "<table id='tblTituloMovil' style='width:800px; text-align:right;'></table>";
    public string strBodyMovilHTML = "<table id='tblBodyMovil' style='width:800px; text-align:right;' mantenimiento='1'></table>";
    public string strBodyFijoHTML = "<table id='tblBodyFijo' style='width:400px; text-align:right;'></table>";
    public string sNodo = "";
    public string sErrores = "", sEstado = "", sCualidad = "", sMoneda = "";
    public int nPSN = 0;
    public string sLectura = "false";
    public string sLecturaInsMes = "false";
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public SqlConnection oConn;
    public SqlTransaction tr;

    private void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (Session["OCULTAR_AUDITORIA"].ToString() == "1")
                {
                    this.cldAuditoria.Visible = false;
                    this.btnAuditoria.Visible = false;
                }

                Utilidades.RegistrarAcceso(1);
                if ((bool)Session["MODOLECTURA_PROYECTOSUBNODO"]) sLecturaInsMes = "true";

                sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                nPSN = int.Parse(Request.QueryString["nPSN"].ToString());
                this.hdnIdProyectoSubNodo.Text = nPSN.ToString();         
                
                sEstado = Request.QueryString["sEstadoProy"].ToString();
                sMoneda = Request.QueryString["sMoneda"].ToString();

                if (sLectura == "false" && (
                    sEstado == "C" || sEstado == "H")) sLectura = "true";

                string strTabla = ObtenerTabla(nPSN);

                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK")
                {
                    this.strTituloMovilHTML = aTabla[1];
                    this.strBodyMovilHTML = aTabla[2];
                    this.strBodyFijoHTML = aTabla[3];
                }
                else sErrores += Errores.mostrarError(aTabla[1]);
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("obtener"):
                sResultado += ObtenerTabla(int.Parse(aArgs[1]));
                break;
            case ("addMesesProy"):
                sResultado += addMesesProy(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    private string addMesesProy(string nIdProySubNodo, string sDesde, string sHasta)
    {
        return SEGMESPROYECTOSUBNODO.InsertarSegMesProy(nIdProySubNodo, sDesde, sHasta);
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string ObtenerTabla(int nPSN)
    {
        int nWidthTabla = 400;
        int nColumnasACrear = 0;
        int nIndiceColPrimerMes = 13;
        string sComun = "";

        StringBuilder sbA = new StringBuilder(); //body fijo
        StringBuilder sbB = new StringBuilder(); //body móvil

        StringBuilder sbColgroupTitulo = new StringBuilder();
        StringBuilder sbTitulo = new StringBuilder();

        string sTablaTituloMovil = "";
        string sTablaBodyMovil = "";
        DataRow rowTipoDato3;

        //string sTablaResultado = "";
        try
        {
            sbA.Append("<table id='tblBodyFijo' style='width:400px; text-align:left;'>");
            sbA.Append("<colgroup>");
            sbA.Append("<col style='width:20px;'/>");  //Icono
            sbA.Append("<col style='width:280px;'/>"); //Profesional
            sbA.Append("<col style='width:100px;'/>"); //Concepto
            sbA.Append("</colgroup>");
            //sbA.Append("<tbody>");


            DataSet ds = PROYECTOSUBNODO.Disponibilidad(nPSN);

            bool bTitulos = false;
            int i = 0;

            if (ds.Tables[0].Rows.Count != 0)
            {
                rowTipoDato3 = ds.Tables[0].Rows[0];
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    if (oFila["tipodato"].ToString() == "3")
                    {
                        //rowTipoDato3 = oFila;
                        i++;
                        continue;
                    }

                    if (!bTitulos)
                    {
                        sbTitulo.Append("<tr class='TBLINI'>");
                        for (int x = nIndiceColPrimerMes; x < ds.Tables[0].Columns.Count; x++)
                        {
                            if (x < nIndiceColPrimerMes) continue;
                            sbColgroupTitulo.Append("<col style='width:60px;' />");
                            sbTitulo.Append("<td align='center'>" + Fechas.AnnomesAFechaDescCorta(int.Parse(ds.Tables[0].Columns[x].ColumnName)) + "</td>");
                            nColumnasACrear++;
                        }
                        sbTitulo.Append("</tr>");
                        bTitulos = true;
                    }


                    sComun = "<tr id='" + oFila["t314_idusuario"].ToString() + oFila["tipodato"].ToString() + "' ";
                    sComun += " usu=" + oFila["t314_idusuario"].ToString();
                    sComun += " tipo='" + oFila["tipodato"].ToString() + "'";
                    sComun += " caso='" + oFila["caso"].ToString() + "'";
                    sComun += " sexo='" + oFila["sexo"].ToString() + "'";
                    if (oFila["t313_idempresa_nodomes"] == DBNull.Value) sComun += " empnodo=''";
                    else sComun += " empnodo='" + oFila["t313_idempresa_nodomes"].ToString() + "'";

                    if (oFila["t303_idnodo_usuariomes"] == DBNull.Value) sComun += " nodousumes=''";
                    else sComun += " nodousumes='" + oFila["t303_idnodo_usuariomes"].ToString() + "'";
                    sComun += " coste=" + oFila["coste"].ToString();
                    sComun += " costerep=" + oFila["costerep"].ToString();
                    sComun += " falta=" + oFila["falta"].ToString();

                    if (oFila["fbaja"] == DBNull.Value) sComun += " fbaja='000000' ";
                    else sComun += " fbaja='" + oFila["fbaja"].ToString() + "' ";
                    sComun += " style='height: 20px;";
                    if (oFila["tipodato"].ToString() == "1") sComun += "cursor:pointer;";
                    sComun += "' ";

                    sbA.Append(sComun);
                    if (oFila["tipodato"].ToString() == "1") sbA.Append("onclick='setFilaFija(this)'");

                    sbB.Append(sComun);
                    if (oFila["tipodato"].ToString() == "1") sbB.Append("onclick='setFilaMovil(this)'");

                    if (oFila["tipodato"].ToString() == "1")
                    {
                        sbA.Append("><td></td>");
                        sbA.Append("<td align='left' class='tdbr' style='padding-left:3px;'>");
                        sbA.Append("<nobr class='NBR W280' onmouseover='TTip(event)'>" + oFila["Profesional"].ToString() + "</nobr>");
                    }
                    else
                    {
                        sbA.Append("><td></td>");
                        sbA.Append("<td align='left' class='tdbr' style='padding-left:3px;'>");
                    }
                    sbA.Append("</td><td class='tdbl'><nobr class='NBR W90'>" + oFila["descripcion"].ToString() + "</nobr></td>");
                    sbB.Append(">");

                    for (int x = nIndiceColPrimerMes; x < ds.Tables[0].Columns.Count; x++)
                    {
                        if (x < nIndiceColPrimerMes) continue;
                        sbB.Append("<td align='right'");
                        if (x == nIndiceColPrimerMes) sbB.Append(" class='tdbrl'");

                        if (oFila["tipodato"].ToString() == "4") sbB.Append(" jl='" + decimal.Parse(ds.Tables[0].Rows[i - 1].ItemArray[x].ToString()).ToString("#,##0") + "' ");

                        sbB.Append(">");

                        sbB.Append(decimal.Parse(oFila.ItemArray[x].ToString()).ToString("N"));

                        sbB.Append("</td>");
                    }
                    sbA.Append("</tr>");
                    sbB.Append("</tr>");
                    i++;
                }
            }
            sbA.Append("</table>");
            ds.Dispose();

            nWidthTabla = nColumnasACrear * 60;
            sTablaTituloMovil = "<table id='tblTituloMovil' class='texto' style='width:" + nWidthTabla.ToString() + "px;' cellpadding='0'>";
            sTablaTituloMovil += "<colgroup>";
            sTablaTituloMovil += sbColgroupTitulo.ToString();
            sTablaTituloMovil += "</colgroup>";
            sTablaTituloMovil += sbTitulo.ToString();
            sTablaTituloMovil += "</table>";

            sTablaBodyMovil = "<table id='tblBodyMovil' class='texto' style='width:" + nWidthTabla.ToString() + "px; ' mantenimiento='1' cellpadding='0'>";
            sTablaBodyMovil += "<colgroup>";
            sTablaBodyMovil += sbColgroupTitulo.ToString();
            sTablaBodyMovil += "</colgroup>";
            sTablaBodyMovil += sbB.ToString();
            sTablaBodyMovil += "</table>";

            return "OK@#@" + sTablaTituloMovil + "@#@" + sTablaBodyMovil + "@#@" + sbA.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos", ex);
        }
    }
    protected string Grabar(string strDatos)
    {
        string sResul = "";
        int sSegMesProy = 0;
        string sEstadoMes = "";
        bool bErrorControlado = false;
        double dUnidades = 0;
        double? dUnidadesBD = 0;
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
            string[] aConsumo = Regex.Split(strDatos, "///");
            foreach (string oConsumo in aConsumo)
            {
                if (oConsumo == "") continue;
                string[] aValores = Regex.Split(oConsumo, "##");

                //0. Opcion BD. "I", "U", "D"
                //1. AnnoMes 
                //2. ID usuario 
                //3. Coste
                //4. Unidades
                //5. Costerep
                //6. idempresa_nodomes
                //7. Nodo

                nPSN = int.Parse(Request.QueryString["nPSN"].ToString());
                sSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, nPSN, int.Parse(aValores[1]));
                if (sSegMesProy == 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, nPSN, int.Parse(aValores[1]));
                    if (sEstadoMes=="C")
                    {
                            bErrorControlado = true;
                            throw (new Exception("Durante su intervención en la pantalla, otro usuario ha eliminado el año/mes: " + aValores[1]));
                    }
                    sSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, nPSN, int.Parse(aValores[1]), sEstadoMes, 0, 0, false, 0, 0);
                }
                else
                {
                    SEGMESPROYECTOSUBNODO oSegMes = SEGMESPROYECTOSUBNODO.Obtener(tr, nPSN, int.Parse(aValores[1]), Request.QueryString["sMoneda"].ToString());
                    if (oSegMes.t325_estado == "C")
                    {
                        bErrorControlado = true;
                        throw (new Exception("Durante su intervención en la pantalla, otro usuario ha cerrado el año/mes: " + aValores[1]));
                    }
                    else sSegMesProy = oSegMes.t325_idsegmesproy;
                }
                dUnidades = double.Parse(aValores[4]);
                if (dUnidades == 0)
                {
                    CONSPERMES.Delete(tr, sSegMesProy, int.Parse(aValores[2]));
                }
                else
                {//Si existe en BBDD, updateo, sino, inserto
                    dUnidadesBD = CONSPERMES.GetUnidades(tr, sSegMesProy, int.Parse(aValores[2]));
                    if (dUnidadesBD == null)//No existe registro -> lo insertamos
                    {
                        int? nEmpresa = null;
                        if (aValores[6] != "") nEmpresa = int.Parse(aValores[6]);
                        int? nNodo = null;
                        if (aValores[7] != "") nNodo = int.Parse(aValores[7]);
                        CONSPERMES.Insert(tr, sSegMesProy, int.Parse(aValores[2]), dUnidades, decimal.Parse(aValores[3]), decimal.Parse(aValores[5]), nNodo, nEmpresa);
                    }
                    else
                    {//El registro ya existe, solo updateamos si el valor es diferente
                        if (dUnidades != dUnidadesBD)
                            CONSPERMES.UpdateUnidades(tr, sSegMesProy, int.Parse(aValores[2]), dUnidades);
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los consumos de los profesionales.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
