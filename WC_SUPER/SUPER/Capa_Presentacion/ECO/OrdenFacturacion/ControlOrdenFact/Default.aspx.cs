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
    public string strTablaHTML = "";
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public SqlConnection oConn;
    public SqlTransaction tr;
 	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.TituloPagina = "Órdenes de facturación";
            Master.nBotonera = 55;
            Master.bContienePestanas = true;

            if (!Page.IsCallback)
            {
                try
                {
                    string[] aTabla = Regex.Split(getOrdenes("A,T,E,R,X", "", "", "", "0"), "@#@");
                    if (aTabla[0] == "OK")
                        strTablaHTML = aTabla[1];
                    else
                        Master.sErrores += Errores.mostrarError(aTabla[1]);

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
        string sResultado = "", sCad = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getOrdenes"):
                sResultado += getOrdenes(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
            case ("getPosiciones"):
                sResultado += getPosiciones(aArgs[1]);
                break;
            case "eliminarOrden":
                sResultado += EliminarOrden(aArgs[1]);
                break;
            case "recuperarOrden":
                sResultado += RecuperarOrden(aArgs[1]);
                break;
            case ("getDatosPestana"):
                //sResultado += aArgs[1] + "@#@";
                switch (int.Parse(aArgs[1]))
                {
                    case 0: //Órdenes
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1: //Plantillas
                        //sResultado += getPosiciones(aArgs[2]);
                        sCad = getPlantillas(aArgs[2], aArgs[3]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;

                        break;
                }
                break;
            case "delPlantilla":
                sResultado += delPlantilla(aArgs[1]);
                break;
            case "crearRemesaOF":
                sResultado += crearRemesaOF(aArgs[1], aArgs[2]);
                break;
            case "crearRemesaPL":
                sResultado += crearRemesaPL(aArgs[1], aArgs[2]);
                break;
            case "crearRemesaPLOF":
                sResultado += crearRemesaPLOF(aArgs[1], aArgs[2]);
                break;
            case "addFavorita":
                sResultado += addFavorita(aArgs[1]);
                break;
            case "delFavorita":
                sResultado += delFavorita(aArgs[1]);
                break;
            case "Privatizar":
                sResultado += PrivatizarPlantilla(aArgs[1], aArgs[2]);
                break;
            case "getPrivadas":
                sResultado += getPrivadas(aArgs[1], aArgs[2]);
                break;
            case "duplicarPlantilla":
                sResultado += PrivatizarPlantilla(aArgs[1], aArgs[2]);
                break;
            case ("hayDocs"):
            case ("hayDocsOF"):
                sResultado += HayDocs(aArgs[1]);
                break;
            case ("hayDocsPlant2"):
            case ("hayDocsPlant4"):
                sResultado += HayDocsPlantilla(aArgs[1]);
                break;
            case ("hayDocsPlant3"):
                sResultado += HayDocsPlantilla(aArgs[1], aArgs[2]);
                break;
            case ("cambiarPE"):
                sResultado += cambiarPE(aArgs[1], aArgs[2]);
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

    private string getOrdenes(string sEstados, string sAnnomes, string sPSN, string sCliente, string sMostrarPropias)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblOrdenes' class='texto MA' style='width: 930px;' cellpadding='0'>");
		    sb.Append("    <colgroup>");
		    sb.Append("        <col style='width:25px;' />");
		    sb.Append("        <col style='width:65px;' />");
		    sb.Append("        <col style='width:80px;' />");
		    sb.Append("        <col style='width:330px;' />");
		    sb.Append("        <col style='width:330px;' />");
            sb.Append("        <col style='width:60px;' />");
            sb.Append("        <col style='width:40px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

//            string sToolTip = "";
            SqlDataReader dr=null;
            //2017/01/02 DOARHUMI. Por petición de Oiane, aun siendo Administrador solo se quieren ver las órdenes propias
            //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            //    dr = ORDENFAC.CatalogoADM(null, sEstados, (sAnnomes == "") ? null : (int?)int.Parse(sAnnomes), (sPSN == "") ? null : (int?)int.Parse(sPSN), (sCliente == "") ? null : (int?)int.Parse(sCliente), null);
            //else
                dr = ORDENFAC.Catalogo(null, (int)Session["UsuarioActual"], sEstados, (sAnnomes == "") ? null : (int?)int.Parse(sAnnomes), (sPSN == "") ? null : (int?)int.Parse(sPSN), (sCliente == "") ? null : (int?)int.Parse(sCliente), (sMostrarPropias=="1")? true:false);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t610_idordenfac"].ToString() + "' ");
                sb.Append("dtopor='" + dr["t610_dto_porcen"].ToString() + "' ");
                sb.Append("dtoimp='" + dr["t610_dto_importe"].ToString() + "' ");
                sb.Append("estado='" + dr["t610_estado"].ToString() + "' ");
                sb.Append("moneda='" + dr["t610_moneda"].ToString() + "' ");
                sb.Append("iva='" + dr["t610_ivaincluido"].ToString() + "' ");
                sb.Append("autor='" + dr["autor"].ToString() + "' ");
                sb.Append("onclick='ms(this);getPosiciones(this)' onDblClick='mdOrden(this.id);' style='height:20px' onmouseover='TTip(event)'>");
                sb.Append("<td style='padding-left: 2px;'><img style='vertical-align:middle' src='../../../../images/imgOrden" + dr["t610_estado"].ToString() + ".gif' title='Orden " + dr["estado"].ToString().ToLower() + "' /></td>");
                sb.Append("<td style='style='vertical-align:middle; text-align:right; padding-right:3px;'><nobr class='NBR W60' style='noWrap:true; text-align:right;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:35px;'>Autor:</label>" + dr["Autor"].ToString() + "] hideselects=[off]\" >");
                sb.Append(int.Parse(dr["t610_idordenfac"].ToString()).ToString("#,###") + "</nobr></td>");
                if (dr["t610_fprevemifact"] != DBNull.Value)
                    sb.Append("<td style='padding-left:5px;vertical-align:middle;'>" + ((DateTime)dr["t610_fprevemifact"]).ToShortDateString() + "</td>");
                else
                    sb.Append("<td style='padding-left:5px;vertical-align:bottom'></td>");
                sb.Append("<td><nobr class='NBR W330' style='vertical-align:middle'>(" + dr["NIF"].ToString() + ") " + dr["denominacion SUPER"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W330' style='vertical-align:middle'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td title='" + dr["t610_refcliente"].ToString() + "'><nobr class='NBR W60'>" + dr["t610_refcliente"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center;vertical-align:middle'><input type='checkbox' style='vertical-align:middle;cursor:pointer;' /></td>");
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
            return "Error@#@" +Errores.mostrarError("Error al obtener las órdenes de facturación.", ex);
        }
    }
    private string getPosiciones(string st610_idordenfac)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblPosiciones' class='texto' style='width: 930px;'>");
            sb.Append("    <colgroup>");
		    sb.Append("        <col style='width:25px;' />");
		    sb.Append("        <col style='width:60px;' />");
		    sb.Append("        <col style='width:60px;' />");
		    sb.Append("        <col style='width:405px;' />");
		    sb.Append("        <col style='width:70px;' />");
		    sb.Append("        <col style='width:70px;' />");
		    sb.Append("        <col style='width:70px;' />");
		    sb.Append("        <col style='width:70px;' />");
            sb.Append("        <col style='width:100px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = POSICIONFAC.CatalogoByOrdenFac(null, int.Parse(st610_idordenfac));
            string strDatos = "";

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t611_posicion"].ToString() + "' ");
                sb.Append("estado='" + dr["t611_estado"].ToString() + "' ");
                sb.Append("style='height:20px'>");
                sb.Append("<td style='padding-left: 2px;'><img src='../../../../images/imgPosicion" + dr["t611_estado"].ToString() + ".gif' title='Posición " + dr["estado"].ToString().ToLower() + "' /></td>");
                sb.Append("<td>" + dr["t611_seriefactura"].ToString() + "</td>");
                sb.Append("<td>" + dr["t611_numfactura"].ToString() + "</td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W400'>" + dr["t611_descripcion"].ToString() + "</nobr></td>");

                sb.Append("<td style='text-align:right; padding-right: 2px;'>" + float.Parse(dr["t611_unidades"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right; padding-right: 2px;'>" + decimal.Parse(dr["t611_preciounitario"].ToString()).ToString("N") + "</td>");
                strDatos = "";
                if ((float)dr["t611_dto_porcen"] > 0) strDatos = float.Parse(dr["t611_dto_porcen"].ToString()).ToString("N");
                sb.Append("<td style='text-align:right; padding-right: 2px;'>" + strDatos + "</td>");
                strDatos = "";
                if ((decimal)dr["t611_dto_importe"] > 0) strDatos = decimal.Parse(dr["t611_dto_importe"].ToString()).ToString("N");
                sb.Append("<td style='text-align:right; padding-right: 2px;'>" + strDatos + "</td>");
                sb.Append("<td style='text-align:right; padding-right: 25px;'>" + decimal.Parse(dr["importe"].ToString()).ToString("N") + "</td>");

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
            return "Error@#@" + Errores.mostrarError("Error al obtener las posiciones de una órden de facturación.", ex);
        }
    }

    private string EliminarOrden(string sOrden)
    {
        try
        {
            ORDENFAC.Delete(tr, int.Parse(sOrden));
         
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar la orden de facturación.", ex);
        }
    }
    private string RecuperarOrden(string sOrden)
    {
        try
        {
            int nRecuperada = ORDENFAC.Recuperar(tr, int.Parse(sOrden));

            return "OK@#@" + nRecuperada.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar la orden de facturación.", ex);
        }
    }

    private string crearRemesaOF(string sOrdenes, string sAccDoc)
    {
        string sResul = "";

        #region Abrir conexión y transacción
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

        try
        {
            string[] aOrdenes = Regex.Split(sOrdenes, "///");
            foreach (string sOrden in aOrdenes)
            {
                if (sOrden == "") continue;
                ORDENFAC.Duplicar(tr, int.Parse(sOrden), (int)Session["UsuarioActual"], sAccDoc);
            }

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al crear la remesa de órdenes de facturación", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string crearRemesaPL(string sPlantillas, string sAccDoc)
    {
        string sResul = "";

        #region Abrir conexión y transacción
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

        try
        {
            string[] aPlantilla = Regex.Split(sPlantillas, "///");
            foreach (string sPlantilla in aPlantilla)
            {
                if (sPlantilla == "") continue;
                ORDENFAC.CrearDesdePlantilla(tr, int.Parse(sPlantilla), (int)Session["UsuarioActual"], sAccDoc);
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al crear órdenes desde plantillas", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string crearRemesaPLOF(string sOrdenes, string sAccDoc)
    {
        string sResul = "";

        #region Abrir conexión y transacción
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

        try
        {
            string[] aOrdenes = Regex.Split(sOrdenes, "///");
            foreach (string sOrden in aOrdenes)
            {
                if (sOrden == "") continue;
                PLANTILLAORDENFAC.CrearDesdeOrden(tr, int.Parse(sOrden), (int)Session["UsuarioActual"], sAccDoc);
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al crear plantillas desde órdenes", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string getPlantillas(string sPSN, string sCliente)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;

        try
        {
            #region Plantillas Privadas
            //sb.Append("<table id='tblPlantillasPrivadas' class='texto MA' style='width:420px; table-layout:fixed;' cellSpacing='0' cellpadding='0' border='0'>");
            //sb.Append("    <colgroup>");
            //sb.Append("        <col style='width:20px;' />");
            //sb.Append("        <col style='width:100px; padding-left: 2px;' />");
            //sb.Append("        <col style='width:100px;' />");
            //sb.Append("        <col style='width:100px;' />");
            //sb.Append("        <col style='width:60px;' />");
            //sb.Append("        <col style='width:40px; text-align:center' />");
            //sb.Append("    </colgroup>");
            //sb.Append("<tbody>");

            //dr = PLANTILLAORDENFAC.CatalogoPrivadas(null, (int)Session["UsuarioActual"], (sCliente == "") ? null : (int?)int.Parse(sCliente), (sPSN == "") ? null : (int?)int.Parse(sPSN));

            //while (dr.Read())
            //{
            //    sb.Append("<tr id='" + dr["t629_idplantillaof"].ToString() + "' ");
            ////    sb.Append("bd='' ");
            //    sb.Append("estado='" + dr["t629_estado"].ToString() + "' ");
            ////    sb.Append("valign='top' ");
            //    sb.Append("onclick='msse(this)' ondblclick='mdPlantilla(this.id)' ");
            //    if (dr["t629_estado"].ToString() == "P")
            //        sb.Append("onmousedown='DD(event);' ");
            //    sb.Append("style='height:20px;'>");
            //    sb.Append("<td><img src='../../../../images/imgEstPlan" + dr["t629_estado"].ToString() + ".gif'></td>");
            //    sb.Append("<td><nobr class='NBR W90'>" + dr["t629_denominacion"].ToString() + "</nobr></td>");
            //    sb.Append("<td><nobr class='NBR W90'>" + dr["NIF"].ToString() + "</nobr></td>");
            //    sb.Append("<td><nobr class='NBR W90'>" + dr["t301_idproyecto"].ToString() + "</nobr></td>");
            //    sb.Append("<td><nobr class='NBR W55'>" + dr["Autor"].ToString() + "</nobr></td>");
            //    sb.Append("<td><input type='checkbox' style='vertical-align:middle;cursor:pointer;' ");
            //    if (dr["t629_estado"].ToString() =="B")
            //        sb.Append("disabled ");
            //    sb.Append("/></td>");
            //    sb.Append("</tr>");
            //}
            //dr.Close();
            ////dr.Dispose();
            //sb.Append("</tbody>");
            //sb.Append("</table>");
            #endregion
            sb.Append(getPlantillasPrivadas(sPSN, sCliente));
            sb.Append("|@|@|");
            #region Plantillas Proyectos
            sb.Append("<table id='tblPlantillasProyectos' class='texto MM' style='width:480px;'>");
            sb.Append("    <colgroup>");
            sb.Append("        <col style='width:20px;' />");
            sb.Append("        <col style='width:120px;' />");
            sb.Append("        <col style='width:120px;' />");
            sb.Append("        <col style='width:120px;' />");
            sb.Append("        <col style='width:60px;' />");
            sb.Append("        <col style='width:40px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            dr = PLANTILLAORDENFAC.CatalogoProyectos(null, (int)Session["UsuarioActual"], (sCliente == "") ? null : (int?)int.Parse(sCliente), (sPSN == "") ? null : (int?)int.Parse(sPSN));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t629_idplantillaof"].ToString() + "' ");
                //    sb.Append("bd='' ");
                sb.Append("estado='" + dr["t629_estado"].ToString() + "' ");
                //    sb.Append("valign='top' ");
                sb.Append("onclick='ms(this)' ");
                if (dr["t629_estado"].ToString() == "P")
                    sb.Append("onmousedown='DD(event);' ");
                sb.Append("style='height:22px;' onmouseover='TTip(event)'>");
                sb.Append("<td style='padding-left: 2px;'><img src='../../../../images/imgEstPlan" + dr["t629_estado"].ToString() + ".gif'></td>");
                sb.Append("<td style='padding-left: 2px;'><nobr class='NBR W110'>" + dr["t629_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W110'>" + dr["t302_denominacion"].ToString() + " (" + dr["NIF"].ToString() + ")</nobr></td>");
                sb.Append("<td><nobr class='NBR W110'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W55'>" + dr["Autor"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'><input type='checkbox' style='vertical-align:middle;cursor:pointer;' ");
                if (dr["t629_estado"].ToString() == "B")
                    sb.Append("disabled ");
                sb.Append("/></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            //dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            #endregion
            sb.Append("|@|@|");
            #region Plantillas Favoritas
            sb.Append("<table id='tblPlantillasFavoritas' class='texto MANO' style='width:420px;' mantenimiento='1'>");
            sb.Append("    <colgroup>");
            sb.Append("        <col style='width:20px;' />");
            sb.Append("        <col style='width:100px;' />");
            sb.Append("        <col style='width:100px;' />");
            sb.Append("        <col style='width:100px;' />");
            sb.Append("        <col style='width:60px;' />");
            sb.Append("        <col style='width:40px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            dr = PLANTILLAORDENFAC.CatalogoFavoritas(null, (int)Session["UsuarioActual"], (sCliente == "") ? null : (int?)int.Parse(sCliente), (sPSN == "") ? null : (int?)int.Parse(sPSN));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t629_idplantillaof"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("estado='" + dr["t629_estado"].ToString() + "' ");
                //sb.Append("onmousedown='DD(event);' ");
                sb.Append("onclick='ms(this)' ");
                sb.Append("style='height:22px;' onmouseover='TTip(event)'>");
                sb.Append("<td style='padding-left: 2px;'><img src='../../../../images/imgEstPlan" + dr["t629_estado"].ToString() + ".gif'></td>");
                sb.Append("<td style='padding-left: 2px;'><nobr class='NBR W90'>" + dr["t629_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W90'>" + dr["t302_denominacion"].ToString() + " (" + dr["NIF"].ToString() + ")</nobr></td>");
                if (dr["t301_idproyecto"] != DBNull.Value)
                    sb.Append("<td><nobr class='NBR W90'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td><nobr class='NBR W90'></nobr></td>");
                sb.Append("<td><nobr class='NBR W55'>" + dr["Autor"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:center'><input type='checkbox' style='vertical-align:middle;cursor:pointer;' ");
                if (dr["t629_estado"].ToString() == "B")
                    sb.Append("disabled ");
                sb.Append("/></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            #endregion

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las plantillas.", ex);
        }
    }
    private string delPlantilla(string sPlantillas)
    {
        string sResul = "";

        #region Abrir conexión y transacción
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

        try
        {
            string[] aPlantilla = Regex.Split(sPlantillas, "///");
            foreach (string sPlantilla in aPlantilla)
            {
                if (sPlantilla == "") continue;
                PLANTILLAORDENFAC.Delete(tr, int.Parse(sPlantilla));
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar plantillas", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string addFavorita(string sIdPlantilla)
    {
        try
        {
            PLANTILLAUSUARIO.Insert(null, int.Parse(sIdPlantilla), (int)Session["UsuarioActual"]);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al añadir una plantilla como favorita", ex);
        }
    }

    private string delFavorita(string sIdPlantilla)
    {
        try
        {
            PLANTILLAUSUARIO.Delete(null, (int)Session["UsuarioActual"], int.Parse(sIdPlantilla));
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar una plantilla como favorita", ex);
        }
    }
    private string PrivatizarPlantilla(string sIdPlantilla, string sAccDoc)
    {
        string sResul = "";
        #region Abrir conexión y transacción
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
        try
        {
            //PLANTILLAORDENFAC.Privatizar(int.Parse(sIdPlantilla), (int)Session["UsuarioActual"]);
            SUPER.Capa_Negocio.PLANTILLAORDENFAC.CrearDesdePlantilla(tr, int.Parse(sIdPlantilla), (int)Session["UsuarioActual"], sAccDoc);
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al privatizar la plantilla", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string getPlantillasPrivadas(string sPSN, string sCliente)
    {
        StringBuilder sb = new StringBuilder();

        #region Plantillas Privadas
        sb.Append("<table id='tblPlantillasPrivadas' class='texto MAM' style='width:480px;'>");
        sb.Append("    <colgroup>");
        sb.Append("        <col style='width:20px;' />");
        sb.Append("        <col style='width:120px;' />");
        sb.Append("        <col style='width:120px;' />");
        sb.Append("        <col style='width:120px;' />");
        sb.Append("        <col style='width:60px;' />");
        sb.Append("        <col style='width:40px;' />");
        sb.Append("    </colgroup>");
        sb.Append("<tbody>");

        SqlDataReader dr = PLANTILLAORDENFAC.CatalogoPrivadas(null, (int)Session["UsuarioActual"], (sCliente == "") ? null : (int?)int.Parse(sCliente), (sPSN == "") ? null : (int?)int.Parse(sPSN));

        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t629_idplantillaof"].ToString() + "' ");
            //    sb.Append("bd='' ");
            sb.Append("estado='" + dr["t629_estado"].ToString() + "' ");
            //    sb.Append("valign='top' ");
            sb.Append("onclick='ms(this)' ondblclick='mdPlantilla(this.id)' ");
            if (dr["t629_estado"].ToString() == "P")
                sb.Append("onmousedown='DD(event);' ");
            else
                sb.Append("class='MA' ");
            sb.Append("style='height:22px;' onmouseover='TTip(event)'>");
            sb.Append("<td style='padding-left: 2px;'><img src='../../../../images/imgEstPlan" + dr["t629_estado"].ToString() + ".gif'></td>");
            sb.Append("<td style='padding-left: 2px;'><nobr class='NBR W110'>" + dr["t629_denominacion"].ToString() + "</nobr></td>");
            sb.Append("<td><nobr class='NBR W110'>" + dr["t302_denominacion"].ToString() + " (" + dr["NIF"].ToString() + ")</nobr></td>");
            if (dr["t301_idproyecto"] != DBNull.Value)
                sb.Append("<td><nobr class='NBR W110'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString() + "</nobr></td>");
            else
                sb.Append("<td><nobr class='NBR W110'></nobr></td>");
            sb.Append("<td><nobr class='NBR W55'>" + dr["Autor"].ToString() + "</nobr></td>");
            sb.Append("<td style='text-align:center'><input type='checkbox' style='vertical-align:middle;cursor:pointer;' ");
            if (dr["t629_estado"].ToString() == "B")
                sb.Append("disabled ");
            sb.Append("/></td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");

        #endregion

        return sb.ToString();
    }
    private string getPrivadas(string sPSN, string sCliente)
    {
        StringBuilder sb = new StringBuilder();
//        SqlDataReader dr = null;

        try
        {
            sb.Append(getPlantillasPrivadas(sPSN, sCliente));

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las plantillas.", ex);
        }
    }

    private string HayDocs(string sOrdenes)
    {
        string sResul = "N";
        try
        {
            if (SUPER.Capa_Negocio.ORDENFAC.HayDocs(null, sOrdenes))
                sResul = "S";

            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los archivos asociados a la orden de facturación.", ex);
        }
    }
    private string HayDocsPlantilla(string sOrdenes)
    {
        string sResul = "N";
        try
        {
            if (SUPER.Capa_Negocio.PLANTILLAORDENFAC.HayDocs(null, sOrdenes))
                sResul = "S";

            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los archivos asociados a la palntilla de orden de facturación.", ex);
        }
    }
    private string HayDocsPlantilla(string sOpcion, string sOrdenes)
    {
        string sResul = "N";
        try
        {
            if (SUPER.Capa_Negocio.PLANTILLAORDENFAC.HayDocs(null, sOrdenes))
                sResul = "S";

            return "OK@#@" + sOpcion + "@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los archivos asociados a la palntilla de orden de facturación.", ex);
        }
    }

    private string cambiarPE(string sOrdenes, string sProyecto)
    {
        string sResul = "";

        #region Abrir conexión y transacción
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

        try
        {
            string[] aOrdenes = Regex.Split(sOrdenes, "///");
            foreach (string sOrden in aOrdenes)
            {
                if (sOrden == "") continue;
                ORDENFAC.UpdateProySubnodo(tr, int.Parse(sOrden), int.Parse(sProyecto));
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al cambiar el número de proyecto a la orden", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
