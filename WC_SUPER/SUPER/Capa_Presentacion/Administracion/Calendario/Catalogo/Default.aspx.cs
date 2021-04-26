using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;

//using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml;
    public SqlConnection oConn;
    public SqlTransaction tr;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 2;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Catálogo de calendarios";
            Master.FuncionesJavaScript.Add("Javascript/jquery.autocomplete.js");
            Master.FicherosCSS.Add("Capa_Presentacion/Administracion/Calendario/Catalogo/Calendario.css");

            try
            {
                ObtenerCalendarios();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener el catálogo de calendarios", ex);
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
            case ("eliminar"):  
                sResultado += EliminarCalendario(aArgs[1]);
                break;
            case ("borrar"):  
                sResultado += Borrar(aArgs[1], aArgs[2]);
                break;
            case ("buscar"):
                sResultado += Buscar(aArgs[1], aArgs[2]);
                break;
            case ("setPantalla"):
                sResultado += setPantalla(aArgs[1], aArgs[2]);
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

    private void ObtenerCalendarios()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 754px;'>");
        sb.Append("<colgroup><col style='width:325px;' /><col style='width:300px' /><col style='width:75px' /><col style='width:54px;' /></colgroup>");
        sb.Append("<tbody>");
        //short? nCR = null;

        SqlDataReader dr = Calendario.Catalogo(null, "", true, null, null, "", null, 2, 0);
        /*SqlDataReader dr;
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) 
            dr = Calendario.Catalogo(null, "", true, null, null, "", null, 2, 0);
        else
            dr = Calendario.CatalogoUsu((int)Session["UsuarioActual"], null, "", true, null, "", 2, 0);
        */
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t066_idcal"].ToString() + "' responsable='" + dr["nombreResponsable"] + "' ");
            sb.Append(" provincia='" + dr["t173_denominacion"] + "' njorlabcal='" + dr["Njorlabcal"] + "' nhlacv='" + dr["t066_nhlacv"] + "' ");
            sb.Append(" observaciones = '" + dr["t066_obs"] + "' estado = '" + (bool)dr["t066_estado"] + "' ");

            sb.Append("onclick ='mm(event)' ondblclick='mostrarDetalle(this.id)' style='height:16px'>");
            sb.Append("<td style='padding-left:5px'>" + dr["t066_descal"].ToString() + "</td>");
            sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");

            string sDesTipo = "";
            switch (dr["t066_tipocal"].ToString())
            {
                case "E":
                    sDesTipo = "Empresarial";
                    break;
                case "D":
                    sDesTipo = "Departamental";
                    break;
                case "P":
                    sDesTipo = "Proyecto";
                    break;
            }
            sb.Append("<td>" + sDesTipo + "</td>");

            if ((bool)dr["t066_estado"])
                sb.Append("<td align='center' ord='1'><img src='../../../../images/imgOk.gif' /></td>");
            else
                sb.Append("<td ord='0'><img src='../../../../images/imgSeparador.gif' /></td>");

            sb.Append("</tr>");
            //i++;
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");
        strTablaHtml = sb.ToString();
    }

    private string EliminarCalendario(string strCal)
    {
        string sResul = "";
        string sCodCal, sDescCal = "";

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
            //Calendario objCal = new Calendario(int.Parse(sIDCal));
            //objCal.Eliminar();
            string[] aCal = Regex.Split(strCal, "##");
            foreach (string oCal in aCal)
            {
                string[] aCal2 = Regex.Split(oCal, @"\\");
                sCodCal = aCal2[0];
                sDescCal = Utilidades.unescape(aCal2[1]);
                Calendario.Eliminar(tr, int.Parse(sCodCal));
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar calendarios ", ex) + "@#@" + sDescCal;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

    private string Borrar(string sAnno, string sCal)
    {
        string sResul = "";
        try
        {
            string[] aDatos = Regex.Split(sCal, "##");
            foreach (string sIdCal in aDatos)
            {
                DiaCal.EliminarAnno(int.Parse(sAnno), int.Parse(sIdCal));
            }

            sResul = "OK";

        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar los datos del año indicado", ex);
        }

        return sResul;
    }

    private string Buscar(string sOrden, string sAscDesc)
    {
        string sResul = "";
        StringBuilder strBuilder = new StringBuilder();

        try
        {
            SqlDataReader dr = Calendario.Catalogo(null, "", null, null, null, "", null, 2, 0);

            /*if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) 
                dr = Calendario.Catalogo(null, "", null, null, null, "", null, 2, 0);
            else
                dr = Calendario.CatalogoUsu((int)Session["UsuarioActual"], null, "", null, null, "", 2, 0);
            */
            strBuilder.Append("<table id='tblDatos' style='width: 754px;'>");
            strBuilder.Append("<colgroup><col style='width:325px;' /><col style='width:300px' /><col style='width:75px' /><col style='width:54px;' /></colgroup>");
            strBuilder.Append("<tbody>");
            while (dr.Read())
            {
                strBuilder.Append("<tr id='" + dr["t066_idcal"].ToString() + "' responsable='" + dr["nombreResponsable"] + "' ");
                strBuilder.Append(" provincia='" + dr["t173_denominacion"] + "' njorlabcal='" + dr["Njorlabcal"] + "' nhlacv='" + dr["t066_nhlacv"] + "' ");
                strBuilder.Append(" observaciones = '" + dr["t066_obs"] + "' estado = '" + (bool)dr["t066_estado"] + "' ");
                strBuilder.Append(" onclick = 'mm(event)' ondblclick='mostrarDetalle(this.id)'  style='height:16px'>");
                strBuilder.Append("<td style='padding-left:5px'>" + dr["t066_descal"].ToString() + "</td>");
                strBuilder.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");

                string sDesTipo = "";
                switch (dr["t066_tipocal"].ToString())
                {
                    case "E":
                        sDesTipo = "Empresarial";
                        break;
                    case "D":
                        sDesTipo = "Departamental";
                        break;
                    case "P":
                        sDesTipo = "Proyecto";
                        break;
                }
                strBuilder.Append("<td>" + sDesTipo + "</td>");

                if ((bool)dr["t066_estado"])
                    strBuilder.Append("<td align='center'><img src='../../../../images/imgOk.gif' /></td>");
                else
                    strBuilder.Append("<td><img src='../../../../images/imgSeparador.gif' /></td>");

                strBuilder.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            strBuilder.Append("</tbody>");
            strBuilder.Append("</table>");

            sResul = "OK@#@" + strBuilder.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al ordenar el catálogo", ex);
        }

        return sResul;
    }
    private string setPantalla(string sActivo, string sIDFicepiResp)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr;
            bool? bActivo;
            int? iIDFicepiResp = null;
            if (sActivo == "0") bActivo = null;
            else bActivo = true;

            if (sIDFicepiResp == "") iIDFicepiResp = null;
            else iIDFicepiResp = int.Parse(sIDFicepiResp);

            dr = Calendario.Catalogo(null, "", bActivo, null, null, "", iIDFicepiResp, 3, 0);

            /*if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) 
                dr = Calendario.Catalogo(null, "", bActivo, null, null, "", iIDFicepiResp, 3, 0);
            else
                dr = Calendario.CatalogoUsu((int)Session["UsuarioActual"], null, "", bActivo, null, "", 3, 0);
            */
            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 750px;'>");
            sb.Append("<colgroup><col style='width:325px;' /><col style='width:300px' /><col style='width:75px' /><col style='width:50px;' /></colgroup>");
            sb.Append("<tbody>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t066_idcal"].ToString() + "'  responsable='" + dr["nombreResponsable"] + "' ");
                sb.Append(" provincia='" + dr["t173_denominacion"] + "' njorlabcal='" + dr["Njorlabcal"] + "' nhlacv='" + dr["t066_nhlacv"] + "' ");
                sb.Append(" observaciones = '" + dr["t066_obs"] + "' estado = '" + (bool)dr["t066_estado"] + "' ");
                sb.Append(" onclick ='mm(event)' ondblclick='mostrarDetalle(this.id)' style='height:16px'>");
                sb.Append("<td style='padding-left:5px'>" + dr["t066_descal"].ToString() + "</td>");
                sb.Append("<td>" + dr["t303_denominacion"].ToString() + "</td>");

                string sDesTipo = "";
                switch (dr["t066_tipocal"].ToString())
                {
                    case "E":
                        sDesTipo = "Empresarial";
                        break;
                    case "D":
                        sDesTipo = "Departamental";
                        break;
                    case "P":
                        sDesTipo = "Proyecto";
                        break;
                }
                sb.Append("<td>" + sDesTipo + "</td>");

                if ((bool)dr["t066_estado"])
                    sb.Append("<td align='center' ord='1'><img src='../../../../images/imgOk.gif' /></td>");
                else
                    sb.Append("<td ord='0'><img src='../../../../images/imgSeparador.gif' /></td>");

                sb.Append("</tr>");
                //i++;
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al ordenar el catálogo", ex);
        }

        return sResul;
    }    
}
