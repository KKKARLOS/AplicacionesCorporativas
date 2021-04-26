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
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML="";
    public int nAnoMes = Fechas.AddAnnomes(Fechas.FechaAAnnomes(DateTime.Now), -1);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 46;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Gastos financieros";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                cargarNodos();
            }
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
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2]);
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
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = NODO.CatalogoGastosFinancieros();
            string sTootTip = "";

            sb.Append("<table id='tblNodos' class='texto MANO' style='width: 970px;'>");
            sb.Append("<colgroup><col style='width:50px;' /><col style='width:70px;' /><col style='width:380px;' /><col style='width:340px;' /><col style='width:40px; text-align:right; padding-right:2px;' /><col style='width:90px;  text-align:center;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                if (Utilidades.EstructuraActiva("SN4")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString();

                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' style='height:20px;' onclick=\"ms(this);AccionBotonera('historial', 'H');\">");
                if ((bool)dr["t303_defectocalcularGF"]) sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' checked onclick='setEstadistica()'></td>");
                else sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='setEstadistica()'></td>");
                sb.Append("<td style='text-align:right;padding-right:3px'>" + dr["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td style='padding-left:8px'><div class='NBR W340' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</div></td>");
                sb.Append("<td><div class='NBR W330'>" + dr["t313_denominacion"].ToString() + "</div></td>");
                sb.Append("<td>" + double.Parse(dr["t303_interesGF"].ToString()).ToString("N") + "</td>");
                if (dr["t469_anomes"].ToString() != "") sb.Append("<td>" + Fechas.AnnomesAFechaDescLarga((int)dr["t469_anomes"]) + "</td>");
                else sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }

    private string Procesar(string sMesValor, string strNodos)
    {
        string sResul = "";
        string sEstadoMes = "";
        try
        {
            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            DataSet ds = PROYECTOSUBNODO.ObtenerSegMesGastosFinancierosDS(tr, int.Parse(sMesValor), strNodos);
            foreach (DataRow oSegMes in ds.Tables[0].Rows)
            {
                try
                {
                    if (oSegMes["t325_idsegmesproy"].ToString() != "")
                    {
                        SEGMESPROYECTOSUBNODO.UpdateGastosFinancieros(tr, (int)oSegMes["t325_idsegmesproy"], decimal.Parse(oSegMes["gasto_financiero"].ToString()));
                    }
                    else
                    {
                        if (decimal.Parse(oSegMes["gasto_financiero"].ToString()) != 0)
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, (int)oSegMes["t305_idproyectosubnodo"], int.Parse(sMesValor));
                            SEGMESPROYECTOSUBNODO.Insert(tr, (int)oSegMes["t305_idproyectosubnodo"], int.Parse(sMesValor), sEstadoMes, 0, decimal.Parse(oSegMes["gasto_financiero"].ToString()), false, 0, 0);
                        }
                    }
                }
                catch (Exception exup)
                {
                    if (((SqlException)exup).Number == 2601)
                    {
                        SEGMESPROYECTOSUBNODO.UpdateGastosFinancierosByPSNAnomes(tr, (int)oSegMes["t305_idproyectosubnodo"], int.Parse(sMesValor), decimal.Parse(oSegMes["gasto_financiero"].ToString()));
                    }
                    else
                    {
                        throw (new Exception("Error al registrar los gastos financieros."));
                    }
                }
            }
            ds.Dispose();

            string[] aNodos = Regex.Split(strNodos, ",");
            foreach (string oNodo in aNodos)
            {
                if (oNodo == "") continue;
                NODO objNodoAux = NODO.Select(tr, int.Parse(oNodo));
                HISTORIALGASTOSFINANCIEROS.Insert(tr, int.Parse(oNodo), int.Parse(sMesValor), DateTime.Now, objNodoAux.t303_interesGF, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al registrar los gastos financieros de un "+ Estructura.getDefLarga(Estructura.sTipoElem.NODO) +".", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
