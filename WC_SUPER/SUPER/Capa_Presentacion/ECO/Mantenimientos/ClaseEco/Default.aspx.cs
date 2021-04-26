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
    public string strTablaHTML;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.TituloPagina = "Mantenimiento de clases económicas";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.bFuncionesLocales = true;

            try
            {
                cargarComboGrupos();
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
                sResultado += Grabar(aArgs[1]);
                break;
            case ("getSE"):
                sResultado += getSE(aArgs[1]);
                break;
            case ("getCE"):
                sResultado += getCE(aArgs[1]);
                break;
            case ("getClaseEco"):
                sResultado += getClaseEco(aArgs[1], aArgs[2]);
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

    public void cargarComboGrupos()
    {
        ListItem oLI;
        SqlDataReader dr = GRUPOECO.Catalogo(null, "", null, "", 3, 0);
        while (dr.Read())
        {
            oLI = new ListItem(dr["t326_denominacion"].ToString(),dr["t326_idgrupoeco"].ToString());
            oLI.Attributes.Add("sTipo", dr["t326_tipogrupo"].ToString());
            cboGE.Items.Add(oLI);
        }
        dr.Close();
        dr.Dispose();
    }
    private string getSE(string sGE)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUBGRUPOECO.SelectByT326_idgrupoeco(null, byte.Parse(sGE), true);

            while (dr.Read())
            {
                sb.Append(dr["t327_idsubgrupoeco"].ToString() + "##" + dr["t327_denominacion"].ToString() +"///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los subgrupos económicos", ex);
        }
    }
    private string getCE(string sSE)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CONCEPTOECO.SelectByT327_idsubgrupoeco(null, byte.Parse(sSE), null, true, false, true, true);

            while (dr.Read())
            {
                sb.Append(dr["t328_idconceptoeco"].ToString() + "##" + dr["t328_denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los subgrupos económicos", ex);
        }
    }
    private string getClaseEco(string sCE, string sTipoGrupo)
    {
        string sChecked = "";
        string sSelect = "";
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CLASEECO.SelectByT328_idconceptoeco(tr, byte.Parse(sCE));

            sb.Append("<table id='tblDatos' class='texto MANO' style='width: 980px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' />");
            sb.Append("<col style='width:360px;' />");
            //sb.Append("<col style='width:50px;' />");
            //sb.Append("<col style='width:45px;' />");
            //sb.Append("<col style='width:110px;' />");
            //sb.Append("<col style='width:30px;' />");
            //sb.Append("<col style='width:80px;' />");
            //sb.Append("<col style='width:45px;' />");
            //sb.Append("<col style='width:45px;' />");
            //sb.Append("<col style='width:45px;' />");
            //sb.Append("<col style='width:45px;' />");
            //sb.Append("<col style='width:45px;' />");
            //sb.Append("<col style='width:45px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:115px;' />");
            sb.Append("<col style='width:30px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t329_idclaseeco"].ToString() + "' ");
                sb.Append("bd='' orden='" + dr["t329_orden"].ToString() + "' ");
                if ((bool)dr["t329_noborrable"]) sb.Append("nb='1' ");
                else sb.Append("nb='0'");
                sb.Append(" style='height:22px;' onclick='mm(event)'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td><img src='../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                sb.Append("<td style='padding-left:5px;'><input type='text' class='txtL' style='width:350px' value=\"" + dr["t329_denominacion"].ToString() + "\" maxlength='50' onKeyUp='fm(event)'></td>");
                if ((bool)dr["t329_estado"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + "></td>");
                if ((bool)dr["t329_presentablesoloAdm"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + "></td>");

                sSelect = "<select class='combo' style='width:80px' onChange='fm(event);setReplica(this.value);' >";
	            sSelect += "<option value='' ";
                if (dr["t329_necesidad"].ToString().Trim() == "")
                {
                    sSelect += "selected='selected'";
                }
                sSelect += "></option><option value='N' ";
                if (dr["t329_necesidad"].ToString().Trim() == "N")
                {
                    sSelect += "selected='selected'";
                }
                sSelect += ">" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "</option><option value='P' ";
                if (dr["t329_necesidad"].ToString().Trim() == "P")
                {
                    sSelect += "selected='selected'";
                }
                sSelect += ">Proveedor</option></select>";
                sb.Append("<td>" + sSelect + "</td>");

                if ((bool)dr["t329_disparareplica"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + " ></td>");

                sSelect = "<select class='combo' style='width:70px' onChange='fm(event);' ";

                if (sTipoGrupo != "I") sSelect += "disabled ";
                sSelect += "><option value='' ";
                if (dr["t329_decalajeborrado"].ToString().Trim() == "") sSelect += "selected='selected'";
                sSelect += "></option><option value='F' ";
                if (dr["t329_decalajeborrado"].ToString().Trim() == "F") sSelect += "selected='selected'";
                sSelect += ">Facturación</option><option value='P' ";
                if (dr["t329_decalajeborrado"].ToString().Trim() == "P") sSelect += "selected='selected'";
                sSelect += ">Previsión</option></select>";
                sb.Append("<td>" + sSelect + "</td>");

                if ((bool)dr["t329_calculoGF"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + " ></td>");

                if ((bool)dr["t329_visiblecarruselC"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + " ></td>");
                
                if ((bool)dr["t329_visiblecarruselJ"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + " ></td>");
                
                if ((bool)dr["t329_visiblecarruselP"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + " ></td>");

                if ((bool)dr["t329_clonable"]) sChecked = "checked";
                else sChecked = "";
                sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + " ></td>");

                //if ((bool)dr["t329_factura"]) sChecked = "checked";
                //else sChecked = "";
                //sb.Append("<td><input type='checkbox' class='check' onclick='fm(event)' " + sChecked + " ></td>");

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
            return "Error@#@" + Errores.mostrarError("Error al obtener las clases económicas", ex);
        }
    }

    protected string Grabar(string strDatos)
    {
        string sResul = "", sElementosInsertados = "";
        int nAux = 0;

        #region apertura de conexión y transacción
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
        #endregion

        try
        {
            string[] aClase = Regex.Split(strDatos, "///");
            foreach (string oClase in aClase)
            {
                if (oClase == "") continue;
                string[] aValores = Regex.Split(oClase, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID Clase
                //2. ID Concepto Económico
                //3. Descripcion
                //4. Estado
                //5. PresentableAdm
                //6. Necesidad
                //7. Dispara réplica
                //8. Decalaje y borrado 
                //9. No t329_calculoGF
                //10. VPC
                //11. VPSG
                //12. VPCG
                //13. Orden
                //14. No borrable
                //15. Clonable
                ////16. Factura

                switch (aValores[0])
                {
                    case "I":
                        nAux = CLASEECO.Insert(tr, 
                                            Utilidades.unescape(aValores[3]), 
                                            (aValores[4] == "1")? true:false, 
                                            (aValores[5] == "1")? true:false,
                                            (aValores[6] == "") ? null : aValores[6],
                                            short.Parse(aValores[13]),
                                            (aValores[7] == "1")? true:false, 
                                            byte.Parse(aValores[2]),
                                            (aValores[14] == "1") ? true : false,
                                            (aValores[8] == "") ? null : aValores[8],
                                            (aValores[9] == "1") ? true : false,
                                            (aValores[10] == "1") ? true : false,
                                            (aValores[11] == "1") ? true : false,
                                            (aValores[12] == "1") ? true : false,
                                            (aValores[15] == "1") ? true : false//,(aValores[16] == "1") ? true : false
                                            );

                        if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                        else sElementosInsertados += "//" + nAux.ToString();
                        break;
                    case "U":
                        nAux = CLASEECO.Update(tr, 
                                            int.Parse(aValores[1]),
                                            Utilidades.unescape(aValores[3]),
                                            (aValores[4] == "1") ? true : false,
                                            (aValores[5] == "1") ? true : false,
                                            (aValores[6] == "") ? null : aValores[6],
                                            short.Parse(aValores[13]),
                                            (aValores[7] == "1") ? true : false,
                                            byte.Parse(aValores[2]),
                                            (aValores[14] == "1") ? true : false,
                                            (aValores[8] == "") ? null : aValores[8],
                                            (aValores[9] == "1") ? true : false,
                                            (aValores[10] == "1") ? true : false,
                                            (aValores[11] == "1") ? true : false,
                                            (aValores[12] == "1") ? true : false,
                                            (aValores[15] == "1") ? true : false//, (aValores[16] == "1") ? true : false
                                            );
                        break;
                    case "D":
                        CLASEECO.Delete(tr, int.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las clases.", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
