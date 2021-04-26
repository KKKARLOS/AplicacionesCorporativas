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
    public int nAnno = DateTime.Now.Year-1;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 43;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Paso 20% obra en curso";
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                this.txtAnno.Text = nAnno.ToString();

                string[] aTabla = Regex.Split(cargarNodos(nAnno), "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
                
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
            case ("getNodos"):
                sResultado += cargarNodos(int.Parse(aArgs[1]));
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

    private string cargarNodos(int nAnno)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = NODO.CatalogoObraEnCurso(nAnno);
            string sTootTip = "";

            sb.Append("<table id='tblNodos' class='texto MANO' style='width: 600px;'>");
            sb.Append("<colgroup><col style='width:50px;' /><col style='width:70px;' /><col style='width:400px;' /><col style='width:80px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                if (Utilidades.EstructuraActiva("SN4")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString();

                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' pocar=" + dr["num_proy_con_obra_en_curso"].ToString() + " style='height:20px;'>");
                sb.Append("<td align='center'><input type='checkbox' class='check' onclick='setEstadistica()'></td>");
                sb.Append("<td style='text-align:right; padding-right:3px'>" + dr["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td style='padding-left:8px;'><nobr class='NBR W340' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                if ((int)dr["num_proy_con_obra_en_curso"] > 0) sb.Append("<td style='padding-right:5px;text-align:center' class='MA' ondblclick='getPOC(this);'>" + dr["num_proy_con_obra_en_curso"].ToString() + "</td>");
                else sb.Append("<td style='padding-right:5px;text-align:center'></td>");

                //sb.Append("<td style='text-align:right; padding-right:2px;'>" + int.Parse(dr["num_proy_con_obra_en_curso"].ToString()).ToString("#,###") + "</td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los nodos.", ex);
        }
    }

    private string Procesar(string sAnno, string strNodos)
    {
        string sResul = "";
        string sEstadoMes = "";
        int nSMPSN = 0;

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

            PROYECTOSUBNODO.EliminarObraEnCurso(tr, int.Parse(sAnno), strNodos);
            DataSet ds = PROYECTOSUBNODO.ObtenerProyectosObraEnCurso(tr, int.Parse(sAnno), strNodos);

            foreach (DataRow oPSN in ds.Tables[0].Rows)
            {
                nSMPSN = (int)oPSN["t325_idsegmesproy_dic"];
                if (nSMPSN == 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, (int)oPSN["t305_idproyectosubnodo"], int.Parse(sAnno) * 100 + 12);
                    nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, (int)oPSN["t305_idproyectosubnodo"], int.Parse(sAnno) * 100 + 12, sEstadoMes, 0, 0, false, 0, 0);
                }

                //Insertamos en diciembre el importe del 20% en negativo.
                DATOECO.Insert(tr, nSMPSN, Constantes.nIdClaseObraEnCurso, "20% Obra en Curso Fin de Año", decimal.Parse(oPSN["20_Obra_Curso_Anno"].ToString()) * -1, null, null, null);

                nSMPSN = (int)oPSN["t325_idsegmesproy_ene"];
                if (nSMPSN == 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, (int)oPSN["t305_idproyectosubnodo"], (int.Parse(sAnno) + 1) * 100 + 1);
                    nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, (int)oPSN["t305_idproyectosubnodo"], (int.Parse(sAnno) + 1) * 100 + 1, sEstadoMes, 0, 0, false, 0, 0);
                }
                //Insertamos en diciembre el importe del 20% en positivo.
                DATOECO.Insert(tr, nSMPSN, Constantes.nIdClaseObraEnCurso, "20% Obra en Curso Fin de Año", decimal.Parse(oPSN["20_Obra_Curso_Anno"].ToString()), null, null, null);
            }
            ds.Dispose();

            Conexion.CommitTransaccion(tr);

            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al realizar el paso del 20% de la obra en curso.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
