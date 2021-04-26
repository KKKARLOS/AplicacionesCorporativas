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
    public string strTablaHTML = "";
    public string sErrores = "", sClasesClonables = "";
    public string sLectura = "false", sLecturaInsMes = "false", sModeloImputacionGasvi = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            try
            {
                sClasesClonables = getClasesClonables();
            }
            catch (Exception ex)
            {
                this.sErrores = Errores.mostrarError("Error al obtener las clases clonables", ex);
            }
            try
            {
                string strTabla = getDatos();
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error")
                    this.strTablaHTML = aTabla[1];
                else sErrores = aTabla[1];
            }
            catch (Exception ex)
            {
                this.sErrores = Errores.mostrarError("Error al obtener los meses", ex);
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
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        string sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11]);
                break;
            case ("getMesesProy"):
                sResultado += getMesesProy(aArgs[1]);
                break;
            case ("getDatos"):
                sResultado += getDatos();
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

    public string getClasesClonables()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = CLASEECO.ObtenerClasesClonables(null, SUPER.Capa_Negocio.Utilidades.EsAdminProduccion());
            int i = 0;
            while (dr.Read())
            {
                if (i > 0) sb.Append(",");
                sb.Append(dr["t329_idclaseeco"].ToString());
                i++;
            }
            dr.Close();
            dr.Dispose();

            return sb.ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    public string getDatos()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table class='texto MANO' id='tblDatos' style='width: 350px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:150px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("<col style='width:100px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");


            SqlDataReader dr = SEGMESPROYECTOSUBNODO.ObtenerMesesParaClonado(null, int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()), (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString());

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t325_idsegmesproy"].ToString() + "' anomes='" + dr["t325_anomes"].ToString() + "' estado='" + dr["t325_estado"].ToString() + "' style='height:20px;' ");
                sb.Append("onclick='ms(this);seleccionarMes(this.rowIndex);' >");
                if (dr["t325_estado"].ToString()=="C") sb.Append("<td style='color:red'>");
                else sb.Append("<td style='color:#009900;padding-left:5px;font-weight:bold;'>");
                sb.Append(Fechas.AnnomesAFechaDescLarga((int)dr["t325_anomes"]) + "</td>");
                if (decimal.Parse(dr["Consumos"].ToString()) != 0) sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["Consumos"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right;'></td>");
                if (decimal.Parse(dr["Produccion"].ToString()) != 0) sb.Append("<td style='text-align:right; padding-right:2px;'>" + decimal.Parse(dr["Produccion"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td style='text-align:right; padding-right:2px;'></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses abiertos", ex);
        }
    }
    protected string Procesar(string sSegMesProy, string nPSN, string sConsPersonas, string sConsNivel, string sProdProfesional, string sProdPerfil, string sAvance, string sPeriodCons, string sPeriodProd, string sClasesAClonar, string strMeses)
    {
        string sResul = "", sw="0";
        int nSMPSN_destino = 0;
        int t325_anomes_maxC = 0;
        int t325_anomes_minA = 0;
        int t303_ultcierreeco = 0;

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
            DataSet ds = SEGMESPROYECTOSUBNODO.ObtenerMesesReferenciaParaClonado(tr, int.Parse(nPSN));
            foreach (DataRow oMes in ds.Tables[0].Rows)
            {
                if (oMes["t325_estado"].ToString() == "C") t325_anomes_maxC = (int)oMes["t325_anomes"];
                if (oMes["t325_estado"].ToString() == "A") t325_anomes_minA = (int)oMes["t325_anomes"];
            }
            foreach (DataRow oMes in ds.Tables[1].Rows)
            {
                t303_ultcierreeco = (int)oMes["t303_ultcierreeco"];
            }

            string[] aAnomes = Regex.Split(strMeses, "##");
            foreach (string oAnomes in aAnomes)
            {
                if (oAnomes == "") continue;

                if (int.Parse(oAnomes) <= t303_ultcierreeco
                    || int.Parse(oAnomes) <= t325_anomes_maxC)
                {
                    sw = "1";
                    continue;
                }

                nSMPSN_destino = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, int.Parse(nPSN), int.Parse(oAnomes));
                if (nSMPSN_destino != 0) SEGMESPROYECTOSUBNODO.Delete(tr, nSMPSN_destino);

                nSMPSN_destino = SEGMESPROYECTOSUBNODO.Insert(tr, int.Parse(nPSN), int.Parse(oAnomes), "A", 0, 0, false, 0, 0);

                SEGMESPROYECTOSUBNODO.ClonarMes(tr, int.Parse(sSegMesProy), 
                                                nSMPSN_destino, 
                                                sClasesAClonar, 
                                                (sConsPersonas == "1") ? true : false,
                                                (sConsNivel == "1") ? true : false,
                                                (sProdProfesional == "1") ? true : false,
                                                (sProdPerfil == "1") ? true : false,
                                                (sAvance == "1") ? true : false,
                                                (sPeriodCons == "1") ? true : false,
                                                (sPeriodProd == "1") ? true : false,
                                                 SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()
                                                );
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sw;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al clonar los datos del mes de referencia.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }
    private string getMesesProy(string sIDProySubnodo)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, int.Parse(sIDProySubnodo));

            while (dr.Read())
            {
                sb.Append(dr["t325_idsegmesproy"].ToString() + "##");
                sb.Append(dr["t325_anomes"].ToString() + "##");
                sb.Append(dr["t325_estado"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses del proyectosubnodo", ex);
        }
    }

}
