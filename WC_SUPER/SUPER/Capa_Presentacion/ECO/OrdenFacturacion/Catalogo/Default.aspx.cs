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
    public SqlConnection oConn;
    public SqlTransaction tr;
    public bool bLecturaProyectoOF = true;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.TituloPagina = "Órdenes de facturación";
            Master.nBotonera = 55;
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

            if (Request.QueryString["nPSN"] != null)
            {
                hdnOrigen.Text = "C";
            }
            else
            {
                hdnOrigen.Text = "A";
            }

            if (!Page.IsCallback)
            {
                try
                {
                    Utilidades.SetEventosFecha(this.txtFechaInicio);
                    Utilidades.SetEventosFecha(this.txtFechaFin);

                    DateTime dHoy = DateTime.Now, dtAux;
                    dtAux = Fechas.primerDiaMes(dHoy.AddMonths(-2));
                    txtFechaInicio.Text = dtAux.ToShortDateString();

                    dtAux = Fechas.primerDiaMes(dHoy.AddMonths(1));
                    dtAux = dtAux.AddDays(-1);
                    txtFechaFin.Text = dtAux.ToShortDateString();

                    bLecturaProyectoOF = (bool)Session["MODOLECTURA_PROYECTOSUBNODO"];
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        bLecturaProyectoOF = false;
                    }
                    else if (Session["ID_PROYECTOSUBNODO"].ToString() != ""){
                        bLecturaProyectoOF = PROYECTO.AccesoEnEscrituraOrdenFacturacion(null, (int)Session["UsuarioActual"], int.Parse(Session["ID_PROYECTOSUBNODO"].ToString()));
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
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("getOrdenes"):
                sResultado += getOrdenes(aArgs[1], aArgs[2], aArgs[3], bool.Parse(aArgs[4]));
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
            case "duplicarOrden":
                sResultado += DuplicarOrden(aArgs[1], aArgs[2]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("hayDocs"):
                sResultado += HayDocs(aArgs[1]);
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

    private string getOrdenes(string sT305IdProy, string sFecIni, string sFecFin, bool erroneas)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblOrdenes' class='texto MA' style='width: 960px;'>");
		    sb.Append("    <colgroup>");
            sb.Append("        <col style='width:20px' />");
		    sb.Append("        <col style='width:80px;' />");
		    sb.Append("        <col style='width:70px;' />");
		    sb.Append("        <col style='width:80px;' />");
		    sb.Append("        <col style='width:300px;' />");
            sb.Append("        <col style='width:190px;' />");
            sb.Append("        <col style='width:130px;' />");
            sb.Append("        <col style='width:90px;' />");
            sb.Append("    </colgroup>");
            sb.Append("<tbody>");

            //SqlDataReader dr = ORDENFAC.CatalogoByPSN(null, int.Parse(sT305IdProy));
            DateTime? dtIni=null;
            DateTime? dtFin=null;
            if (sFecIni != "") dtIni = DateTime.Parse(sFecIni);
            if (sFecFin != "") dtFin = DateTime.Parse(sFecFin);
            SqlDataReader dr = ORDENFAC.CatalogoByPSNyFechas(null, int.Parse(sT305IdProy), dtIni, dtFin);

            while (dr.Read())
            {
                if (!erroneas && dr["t610_estado"].ToString() == "X") continue;
                sb.Append("<tr id='" + dr["t610_idordenfac"].ToString() + "' ");
                sb.Append("dtopor='" + dr["t610_dto_porcen"].ToString() + "' ");
                sb.Append("dtoimp='" + dr["t610_dto_importe"].ToString() + "' ");
                sb.Append("estado='" + dr["t610_estado"].ToString() + "' ");
                sb.Append("moneda='" + dr["t610_moneda"].ToString() + "' ");
                sb.Append("iva='" + dr["t610_ivaincluido"].ToString() + "' ");
                sb.Append("autor='" + dr["autor"].ToString() + "' ");
                sb.Append("onclick='ms(this);getPosiciones(this)' onDblClick='mdOrden(this.id);' style='height:20px'>");
                sb.Append("<td><img src='../../../../images/imgOrden" + dr["t610_estado"].ToString() + ".gif' title='Orden " + dr["estado"].ToString().ToLower() + "' /></td>");
                sb.Append("<td style='text-align:right; padding-right:15px;'><nobr class='NBR W60' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:35px;'>Autor:</label>" + dr["Autor"].ToString() + "] hideselects=[off]\" >");
                sb.Append(int.Parse(dr["t610_idordenfac"].ToString()).ToString("#,###") + "</nobr></td>");
                if (dr["t610_fprevemifact"] != DBNull.Value)
                    sb.Append("<td><nobr class='NBR W60'>" + ((DateTime)dr["t610_fprevemifact"]).ToShortDateString() + "</nobr></td>");
                else
                    sb.Append("<td></td>");
                sb.Append("<td>" + dr["t610_DVSAP"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W320'>(" + dr["NIF"].ToString() + ") " + dr["denominacion SUPER"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W200'>" + dr["des_condicionpago"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W140'>" + dr["OVSAP"].ToString() + "</nobr></td>");
                sb.Append("<td style='padding-left:10px;' title='"+ dr["t610_refcliente"].ToString() + "'><nobr class='NBR W60'>" + dr["t610_refcliente"].ToString() + "</nobr></td>");
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
            sb.Append("<table id='tblPosiciones' class='texto' style='width: 960px;'>");
            sb.Append("    <colgroup>");
		    sb.Append("        <col style='width:25px;' />");
		    sb.Append("        <col style='width:60px;' />");
		    sb.Append("        <col style='width:60px;' />");
		    sb.Append("        <col style='width:435px;' />");
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
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W420'>" + dr["t611_descripcion"].ToString() + "</nobr></td>");

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

    private string recuperarPSN(string sT305IdProy)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.fgGetDatosProy3(null, int.Parse(sT305IdProy));
            if (dr.Read())
            {
                sb.Append(dr["t301_estado"].ToString() + "@#@");  //2
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //3
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "@#@");  //4

                sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO) + ":</label> " + dr["t304_denominacion"].ToString() + "@#@");  //5
                sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["t303_denominacion"].ToString() + "@#@");  //6

                if (Utilidades.EstructuraActiva("SN1")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString() + "@#@");  //7
                else sb.Append("@#@");  //7
                if (Utilidades.EstructuraActiva("SN2")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "@#@");  //8
                else sb.Append("@#@");  //8
                if (Utilidades.EstructuraActiva("SN3")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "@#@");  //9
                else sb.Append("@#@");  //9
                if (Utilidades.EstructuraActiva("SN4")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "@#@");  //10
                else sb.Append("@#@");  //10

                if (dr["t301_categoria"].ToString() == "P")
                    sb.Append("<label style='width:70px'>Categoría:</label> Producto@#@");//11
                else
                    sb.Append("<label style='width:70px'>Categoría:</label> Servicio@#@");//11

                sb.Append("<label style='width:70px'>Cliente:</label> " + dr["t302_denominacion"].ToString() + "@#@");  //12
                sb.Append("<label style='width:70px'>Responsable:</label> " + dr["responsable"].ToString() + "&nbsp;&nbsp;&#123;Ext.: " + dr["t001_exttel"].ToString() + "&#125;" + "@#@");  //13
                
                sb.Append(dr["t302_denominacion"].ToString() + "@#@");  //14
                sb.Append(dr["t302_idcliente_proyecto"].ToString() + "@#@");  //15             
                sb.Append(dr["NifRespPago"].ToString() + "@#@");  //16
                sb.Append(dr["t621_idovsap"].ToString() + "@#@");  //17
                //sb.Append(dr["t314_idusuario_comercialhermes"].ToString() + "@#@");  //18
                if (dr["t314_idusuario_comercialhermes"] != DBNull.Value)
                    sb.Append(dr["t314_idusuario_comercialhermes"].ToString() + "@#@");  //18
                else
                    sb.Append(dr["t314_idusuario_responsable"].ToString() + "@#@");  //18
                sb.Append(dr["t306_idcontrato"].ToString() + "@#@");  //19

                sb.Append(dr["responsableContrato"].ToString() + "@#@");  //20
            }
            dr.Close();
            dr.Dispose();

            bLecturaProyectoOF = (bool)Session["MODOLECTURA_PROYECTOSUBNODO"];
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {
                bLecturaProyectoOF = false;
            }
            else if (Session["ID_PROYECTOSUBNODO"].ToString() != ""){
                bLecturaProyectoOF = PROYECTO.AccesoEnEscrituraOrdenFacturacion(null, (int)Session["UsuarioActual"], int.Parse(sT305IdProy));
            }

            if (sb.ToString() != "")
            {
                if (bLecturaProyectoOF)
                    sb.Append("1"); //modo lectura  //21
                else
                    sb.Append("0"); //modo escritura  //21
            }
            else sb.Append("@#@@#@@#@");  // No existe registro           

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
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
    private string DuplicarOrden(string sOrden, string sAccDoc)
    {
        string sResul = "";
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
        try
        {
            int nDuplicada = ORDENFAC.Duplicar(tr, int.Parse(sOrden), (int)Session["UsuarioActual"], sAccDoc);
            Conexion.CommitTransaccion(tr);

            return "OK@#@" + nDuplicada.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            return "Error@#@" + Errores.mostrarError("Error al duplicar la orden de facturación.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
    }
    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = null;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, true, "C", true);
            else
                dr = PROYECTO.ObtenerProyectosParaFacturar((int)Session["UsuarioActual"], int.Parse(sNumProyecto));
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "///");
            }
            if (sb.ToString() == "") sb.Append("@#@");  // No existe registro    

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto por número", ex);
        }
        return sResul;
    }
    private string HayDocs(string sOrden)
    {
        string sResul = "N";
        try
        {
            SqlDataReader dr = SUPER.Capa_Negocio.DOCUOF.Catalogo(int.Parse(sOrden));
            if (dr.Read())
                sResul = "S";
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los archivos asociados a la orden de facturación.", ex);
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
