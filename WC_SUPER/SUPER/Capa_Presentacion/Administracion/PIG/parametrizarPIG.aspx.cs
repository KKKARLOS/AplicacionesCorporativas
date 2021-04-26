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


public partial class getHistoria : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", strTablaHTML = "", strTablaHTML2 = "";
    public string strNN = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                cargarNodos();
                cargarNaturalezas();
                cargarNaturalezasNodosMarcados();
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);//, aArgs[3]
                break;
            case ("borrar"):
                sResultado += Borrar();
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
            SqlDataReader dr = NODO.CatalogoNodosNaturalezas();
            string sTootTip = "";

            sb.Append("<table id='tblNodos' class='texto MANO' style='width: 380px;'>");
            sb.Append("<colgroup><col style='width:50px;' /><col style='width:30px;' /><col style='width:270px;' /><col style='width:30px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sTootTip = "";
                if (Utilidades.EstructuraActiva("SN4")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "<br>";
                if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString();

                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ");
                if ((bool)dr["t303_defectoPIG"]) sb.Append("defectoPIG=1 ");
                else sb.Append("defectoPIG=0 ");
                sb.Append("style='height:20px;' onclick='ms(this);getNaturalezas(this);'>");
                if ((bool)dr["t303_defectoPIG"]) sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='setEstadistica()' checked></td>");
                else sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='setEstadistica()'></td>");
                sb.Append("<td style='text-align:right; padding-right:3px'>" + dr["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td style='padding-left:8px;'><nobr class='NBR W260' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:right; padding-right:4px;'>" + dr["num_naturalezas"].ToString() + "</td>");
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
            sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private void cargarNaturalezas()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            SqlDataReader dr = NATURALEZA.CatalogoPIG();

            sb.Append(" aNat = new Array();\n"); // aNN --> Array de Naturalezas
            /// [0] --> idNaturaleza
            /// [1] --> Denominación
            /// [2] --> meses vigencia
            /// [3] --> idPlantilla
            /// [4] --> replicaPIG
            /// [5] --> hereda nodo
            /// [6] --> imputable GASVI

            sb2.Append("<table id='tblNatMant' class='texto MANO' style='width: 680px;' mantenimiento='1' border='0'>");
            sb2.Append("<colgroup>");
            sb2.Append("<col style='width:420px;' />");
            sb2.Append("<col style='width:20px;' />");
            sb2.Append("<col style='width:60px;' />");
            sb2.Append("<col style='width:60px;' />");
            sb2.Append("<col style='width:60px;' />");
            sb2.Append("<col style='width:60px;' />");
            sb2.Append("</colgroup>");
            sb2.Append("<tbody>");
            int i = 0;
            while (dr.Read())
            {
                sb.Append("\taNat[" + i.ToString() + "] = new Array(" + dr["t323_idnaturaleza"].ToString() + ",");
                sb.Append("\"" + dr["t323_denominacion"].ToString() + "\"," + dr["t323_mesesvigenciaPIG"].ToString() + ",");
                sb.Append(dr["t338_idplantilla"].ToString() + ",");
                if ((bool)dr["t323_replicaPIG"]) sb.Append("1,");
                else sb.Append("0,");
                if ((bool)dr["t323_heredanodo_PIG"]) sb.Append("1,");
                else sb.Append("0,");
                if ((bool)dr["t323_imputableGASVI_PIG"]) sb.Append("1");
                else sb.Append("0");

                sb.Append(");\n");
                i++;

                sb2.Append("<tr id=" + dr["t323_idnaturaleza"].ToString() + " ");
                //sb2.Append("meses='" + dr["t323_mesesvigenciaPIG"].ToString() + "' ");
                sb2.Append("bd='N' ");
                sb2.Append(" style='height:20px;' onclick='ms(this);'>");
                sb2.Append("<td style='padding-left:3px;'>" + dr["t323_denominacion"].ToString() + "</td>");

                if (dr["t338_idplantilla"].ToString() != "0") 
                    sb2.Append("<td><img src='../../../images/imgIconoEmpresarial.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'></td>");
                else 
                    sb2.Append("<td></td>");

                sb2.Append("<td style='text-align:center;'><input type='text' class='txtNumL' style='width:30px;' value='" + dr["t323_mesesvigenciaPIG"].ToString() + "' onfocus='fn(this,2,0)' onchange='setEstadistica();fm(event);' /></td>");

                sb2.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='fm(event)' ");
                if ((bool)dr["t323_replicaPIG"]) sb2.Append("checked");
                sb2.Append(" /></td>");

                sb2.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='fm(event)' ");
                if ((bool)dr["t323_heredanodo_PIG"]) sb2.Append("checked");
                sb2.Append(" /></td>");

                sb2.Append("<td style='text-align:center;'><input type='checkbox' class='check' onclick='fm(event)' ");
                if ((bool)dr["t323_imputableGASVI_PIG"]) sb2.Append("checked");
                sb2.Append(" /></td>");

                sb2.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb2.Append("</tbody>");
            sb2.Append("</table>");

            strTablaHTML2 = sb2.ToString();
            this.hdnArrayNat.Value = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar las naturalezas", ex);
        }
    }
    private void cargarNaturalezasNodosMarcados()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("aNN = new Array();\n"); // aNN --> Array de Nodos Naturalezas
            /// [0] --> idNodo
            /// [1] --> idNaturaleza
            /// [2] --> meses vigencia
            /// [3] --> marcado
            /// [4] --> a grabar
            /// [5] --> replicaPIG
            /// [6] --> heredanodo
            /// [7] --> imputable GASVI
            /// [8] --> Id usuario responsable proyectos
            /// [9] --> responsable proyectos
            /// [10] --> Id ficepi validador GASVI
            /// [11] --> validador GASVI
            /// [12] --> cargado

            SqlDataReader dr = NODO.CatalogoNodosNaturalezasMarcados();
            int i = 0;
            while (dr.Read())
            {
                sb.Append("\taNN[" + i.ToString() + "] = new Array(" + dr["T303_IDNODO"].ToString() + ","
                            + dr["T323_IDNATURALEZA"].ToString() + "," 
                            + dr["mesesvigencia"].ToString() + "," 
                            + dr["marcado"].ToString() 
                            + ",0,"
                            + dr["t471_replicaPIG"].ToString() + ","
                            + dr["t471_heredanodo"].ToString() + ","
                            + dr["t471_imputableGASVI"].ToString() + ","
                            + dr["t314_idusuario_responsable"].ToString() + ",'"
                            + dr["Responsable"].ToString() + "','"
                            + dr["t001_idficepi_visador"].ToString() + "','"
                            + dr["Validador"].ToString() + "',"
                            + "0,"//Indica si el usuario ha clicado sobre el nodo para cargarlo
                            + dr["Parametrizado"].ToString() //Indica si el nodo se había parametrizado previamente
                            + ");\n");
                i++;
            }
            dr.Close();
            dr.Dispose();
            this.hdnArrayNN.Value = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar los nodos y naturalezas marcados", ex);
        }
    }
    private string Borrar()
    {
        string sResul = "";
        try
        {
            NODO_NATURALEZA.DeleteAll(null);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar la parametrización de nodos-naturaleza.", ex, false);
        }
        return sResul;
    }
    private string Grabar(string strNaturalezas, string strDatos)//string strNodos, 
    {
        string sResul = "";
        int? idFicepiValidador = null;
        try
        {
            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            NODO_NATURALEZA.DeleteAll(tr);

            //string[] aNodos = Regex.Split(strNodos, "///");
            //foreach (string oNodo in aNodos)
            //{
            //    if (oNodo == "") continue;
            //    string[] aValoresNodo = Regex.Split(oNodo, "##");
            //    /// aValoresNodo[0] = idNodo
            //    /// aValoresNodo[1] = defectoPIG

            //    NODO.UpdateDefectoPIG(tr, int.Parse(aValoresNodo[0]), (aValoresNodo[1] == "1") ? true : false);
            //}
            string[] aNaturalezas = Regex.Split(strNaturalezas, "///");
            foreach (string oNaturaleza in aNaturalezas)
            {
                if (oNaturaleza == "") continue;
                string[] aValoresNat = Regex.Split(oNaturaleza, "##");
                /// aValoresNat[0] = idNaturaleza
                /// aValoresNat[1] = meses vigencia
                /// aValoresNat[2] = Permite usuarios de otros nodos

                NATURALEZA.UpdateDefectoVIG(tr, int.Parse(aValoresNat[0]), byte.Parse(aValoresNat[1]), (aValoresNat[2]=="1")? true:false);
            }
            string[] aNodoNat = Regex.Split(strDatos, "///");

            foreach (string oNodoNat in aNodoNat)
            {
                if (oNodoNat == "") continue;
                string[] aValores = Regex.Split(oNodoNat, "##");
                /// aValores[0] = idNodo
                /// aValores[1] = idNaturaleza
                /// aValores[2] = Permite usuarios de otros nodos(replica)
                /// aValores[3] = hereda nodo
                /// aValores[4] = imputable GASVI
                /// aValores[5] = IdUsuario responsable
                /// aValores[6] = idficepi validador GASVI
                if (aValores[6] == "" || aValores[6] == "null") 
                    idFicepiValidador = null;
                else 
                    idFicepiValidador = int.Parse(aValores[6]);

                NODO_NATURALEZA.Insert(tr, int.Parse(aValores[0]), int.Parse(aValores[1]), 
                                        (aValores[2] == "1") ? true : false,
                                        (aValores[3] == "1") ? true : false,
                                        (aValores[4] == "1") ? true : false,
                                        int.Parse(aValores[5]),
                                        idFicepiValidador
                                        );
            }
            
            Conexion.CommitTransaccion(tr);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
