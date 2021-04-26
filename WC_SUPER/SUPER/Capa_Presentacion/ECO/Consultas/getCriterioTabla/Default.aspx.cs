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
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_ECO_Consultas_getCriterio_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    public string sErrores = "", sTitulo = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }
        if (Request.QueryString["caso"] != null)
            hdnCaso.Value = Request.QueryString["caso"];

        if (Request.QueryString["TipoRecurso"] != null)
            hdnTipoRecurso.Value = Request.QueryString["TipoRecurso"];

        hdnIdTipo.Value = Request.QueryString["nTipo"].ToString();
        if (!Page.IsCallback)
        {
            try
            {
                strTablaHTML = "<TABLE id='tblDatos' style='width:350px;' class='texto MAM'><colgroup><col style='width:350px;' /></colgroup></TABLE>";
                if (int.Parse(hdnIdTipo.Value) == 2 || int.Parse(hdnIdTipo.Value) == 24 || int.Parse(hdnIdTipo.Value) == 27 || int.Parse(hdnIdTipo.Value) == 32)
                {
                    ambAp.Style.Add("display", "");
                    ambBajas.Style.Add("display", "");
                    if (int.Parse(hdnIdTipo.Value) == 2) ambIconosResp.Style.Add("display", "");
                    else ambIconosResp.Style.Add("display", "none");
                    ambDenominacion.Style.Add("display", "none");
                    rdbTipo.Style.Add("display", "none");
                }
                else
                {
                    ambAp.Style.Add("display", "none");
                    ambBajas.Style.Add("display", "none");
                    ambIconosResp.Style.Add("display", "none");
                    ambDenominacion.Style.Add("display", "");
                    rdbTipo.Style.Add("display", "");
                }

                sTitulo = "Selección de criterio: ";
                switch (int.Parse(hdnIdTipo.Value))
                {
                    case 1: sTitulo += "Ámbito"; break;
                    case 2: sTitulo += "Responsable de proyecto"; break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 22:
                    case 23: 
                    case 25: 
                    case 26:
                    case 37:
                    case 38:
                    case 40:
                    case 41: 
                        switch (int.Parse(hdnIdTipo.Value))
                        {
                            case 3: sTitulo += "Naturaleza"; break;
                            case 4: sTitulo += "Modelo de contratación"; break;
                            case 5: sTitulo += "Horizontal"; break;
                            case 6: sTitulo += "Sector"; break;
                            case 7: sTitulo += "Segmento"; break;
                            case 22: sTitulo += "Empresa de facturación"; break;
                            case 23: sTitulo += "Rol "; break;
                            case 25: sTitulo += "Centro de trabajo "; break;
                            case 26: sTitulo += "Oficina "; break;
                            case 37: sTitulo += "Organización comercial"; break;
                            case 38: sTitulo += "Soporte administrativo"; break;
                            case 40: sTitulo += "Criterios estadísticos económicos empresariales"; break;
                            case 41: sTitulo += "Valores de criterios estadísticos económicos empresariales"; break;
                        }
                        ambDenominacion.Style.Add("display", "none");
                        rdbTipo.Style.Add("display", "none");
                        string[] aTabla = Regex.Split(ObtenerTipoConcepto("I", ""), "@#@");
                        if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                        else sErrores += Errores.mostrarError(aTabla[1]);
                        break;
                    case 8: sTitulo += "Cliente"; break;
                    case 9: sTitulo += "Contrato"; break;
                    case 10: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO); break;
                    case 11: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1); break;
                    case 12: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2); break;
                    case 13: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3); break;
                    case 14: sTitulo += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4); break;
                    case 17: sTitulo += "Proveedor "; break;
                    case 18: sTitulo += "Centro de responsabilidad "; break;
                    case 36: sTitulo += "Centro de responsabilidad "; break;
                    case 24: sTitulo += "Evaluador "; break;
                    case 27: sTitulo += "Profesional "; break;
                    case 32: sTitulo += "Comercial "; break;
                    case 34: sTitulo += "País"; break;
                    case 35: sTitulo += "Provincia"; break;
                    

                }

                this.Title = sTitulo;

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
            }
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
            case "TipoConcepto":
                sResultado += ObtenerTipoConcepto(aArgs[1], Utilidades.unescape(aArgs[2]));
                break;
            case ("responsables"):
                sResultado += getResponsables(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("Supervisores"):
                sResultado += getSupervisores(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("Profesionales"):
                sResultado += getProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("Comerciales"):
                sResultado += getComerciales(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
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
    private string ObtenerTipoConcepto(string sTipoBusqueda, string sCadena)
    {
        string sResul = "";
        string sTootTip = "";
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = null;

            switch (int.Parse(hdnIdTipo.Value))
            {
                case 1: 	// Ambito
                    break;
                case 2:		// Responsable de proyecto 
                    break;
                case 3:		// Naturaleza 
                    dr = NATURALEZA.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 4:		// Modelo de contratación
                    dr = MODALIDADCONTRATO.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 5:		// Horizontal
                    dr = HORIZONTAL.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 6:		// Sector
                    dr = SECTOR.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 7: 	// Segmento
                    dr = SEGMENTO.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 8: 	// Cliente
                    dr = CLIENTE.SelectByNombre(sCadena, sTipoBusqueda, false, false, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 9:		// Contrato
                    dr = CONTRATO.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 10:	// Cualificador de proyectos a nivel de NODO
                    dr = CDP.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 11:	// Cualificador de proyectos a nivel de SUPERNODO1
                    dr = CSN1P.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 12:	// Cualificador de proyectos a nivel de SUPERNODO2
                    dr = CSN2P.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 13:	// Cualificador de proyectos a nivel de SUPERNODO3
                    dr = CSN3P.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 14:	// Cualificador de proyectos a nivel de SUPERNODO4
                    dr = CSN4P.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 17:	// Proveedores
                    //dr = PROVEEDOR.Catalogo(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    dr = PROVEEDOR.SelectByNombre(null, sCadena, 2, 0, sTipoBusqueda, false);
                    break;
                case 18:    // Centros de responsabilidad
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        dr = NODO.CatalogoAdministrador(sCadena, sTipoBusqueda);
                    else
                        if (hdnCaso.Value == "1")
                            dr = NODO.ObtenerNodosUsuarioEsRespDelegColab(null, (int)Session["UsuarioActual"], sCadena, sTipoBusqueda);
                        else
                            dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"], false);
                    break;
                case 36:
                    dr = NODO.CatalogoAdministrador(sCadena, sTipoBusqueda);
                    break;
                case 22:	// Sociedades que facturan
                    //dr = EMPRESA.Catalogo(null, "", "", null, null, null, "", "", null, 2, 0);
                    dr = EMPRESA.Catalogo(null);
                    break;
                case 23:	// Roles
                    dr = ROL.Catalogo();
                    break;
                case 25:	// Centro de trabajo
                    dr = CENTROTRAB.Obtener();
                    break;
                case 26:	// Oficina
                    dr = OFICINA.Catalogo();
                    break;
                case 34:	// Pais
                    dr = SUPER.DAL.PAIS.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 35:	// Provincia
                    dr = SUPER.DAL.PROVINCIA.CatalogoDenominacion(sCadena, sTipoBusqueda, int.Parse(Session["UsuarioActual"].ToString()));
                    break;
                case 37:	// Organización comercial
                    dr = SUPER.BLL.OrganizacionComercial.Catalogo(null, true);
                    break;
                case 38:	// Soporte administrativo
                    dr = SUPER.Capa_Negocio.SOPORTEADM.Catalogo();
                    break;
                case 40:    // Criterios estadísticos económicos empresariales
                    dr = CEC.Catalogo();
                    break;
                case 41:    // Valores de criterios estadísticos económicos empresariales
                    dr = VCEC.Catalogo();
                    break;

            }

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 350px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:350px;' /></colgroup>" + (char)10);
            sb.Append("<tbody>");

            while (dr.Read())
            {
                switch (int.Parse(hdnIdTipo.Value))
                {
                    case 3: 	// Naturalezas
                    case 4:		// Modelo de contratación
                    case 5:		// Horizontal
                    case 6:		// Sector
                    case 7: 	// Segmento
                    case 9:		// Contrato
                    case 17: 	// Proveedores  
                    case 23:	// Roles
                    case 25:	// Centro de trabajo
                    case 26:	// Oficina 
                    case 34:    // País
                    case 35:    // Provincia
                    case 40:    // Criterios estadísticos económicos empresariales
                    case 41:    // Valores de criterios estadísticos económicos empresariales


                        sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' title='" + dr["DENOMINACION"].ToString() + "' ");
                        if (int.Parse(hdnIdTipo.Value)==41) sb.Append("ceec=" + dr["DENOMINACION"].ToString() + " ");
                        sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;'>");
                        sb.Append("<td style='padding-left:5px;'><nobr class='NBR W320'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                        sb.Append("</tr>" + (char)10);
                        break;
                    case 8: 	// Cliente
                        //sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' ");
                        //sb.Append("onclick='mmse(this)' ondblclick='insertarItem(this)' onmousedown='DD(this)' style='height:20px;'>");
                        //sb.Append("<td>" + dr["t302_denominacion"].ToString() + "</td>");
                        //sb.Append("</tr>" + (char)10);

                        sb.Append("<tr id='" + dr["t302_idcliente"].ToString() + "' title='" + dr["t302_denominacion"].ToString() + "' ");

                        if ((bool)dr["t302_estado"]) sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;'");
                        else sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;color:gray;'");
                        //else sb.Append(" onmousedown='eventos(this);' style='height:20px;color:gray;'");

                        sb.Append("><td><img src='../../../../images/img" + dr["tipo"].ToString() + ".gif' ");
                        if (dr["tipo"].ToString() == "M") sb.Append("style='margin-right:5px;'");
                        else sb.Append("style='margin-left:15px;margin-right:5px;'");
                        sb.Append("><nobr class='NBR W310'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                        sb.Append("</tr>" + (char)10);

                        break;
                    case 10:	// Cualificador de proyectos a nivel de NODO
                    case 11:	// Cualificador de proyectos a nivel de SUPERNODO1
                    case 12:	// Cualificador de proyectos a nivel de SUPERNODO2
                    case 13:	// Cualificador de proyectos a nivel de SUPERNODO3
                    case 14:	// Cualificador de proyectos a nivel de SUPERNODO4
                        sTootTip = "";
                        if (Utilidades.EstructuraActiva("SN4") && int.Parse(hdnIdTipo.Value) <= 14) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3") && int.Parse(hdnIdTipo.Value) <= 13) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2") && int.Parse(hdnIdTipo.Value) <= 12) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN1") && int.Parse(hdnIdTipo.Value) <= 11) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString() + "<br>";
                        if (int.Parse(hdnIdTipo.Value) <= 10) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["t303_denominacion"].ToString();

                        sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' title='" + dr["DENOMINACION"].ToString() + "' ");
                        sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' ");
                        sb.Append("style='height:20px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");

                        sb.Append("<td><nobr class='NBR W320'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                        sb.Append("</tr>" + (char)10);
                        break;
                    case 18: 	// Centros de responsabilidad 
                    case 36:
                        sTootTip = "";
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["DENOMINACION"].ToString();

                        sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' title='" + dr["DENOMINACION"].ToString() + "' ");
                        sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' ");
                        sb.Append("style='height:20px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");

                        sb.Append("<td><nobr class='NBR W320'>" + dr["DENOMINACION"].ToString() + "</nobr></td>");
                        sb.Append("</tr>" + (char)10);
                        break;
                    case 22: 	// Empresas que facturan
                        if (bool.Parse(dr["t313_estado"].ToString()))
                        {
                            sb.Append("<tr id='" + dr["t313_idempresa"].ToString() + "' title='" + dr["t313_denominacion"].ToString() + "' ");
                            sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;'>");
                            sb.Append("<td><nobr class='NBR W320'>" + dr["t313_denominacion"].ToString() + "</nobr></td>");
                        }
                        else
                        {
                            sb.Append("<tr id='" + dr["t313_idempresa"].ToString() + "' title='" + dr["t313_denominacion"].ToString() + "' ");
                            sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;'>");
                            sb.Append("<td style='color:gray'><nobr class='NBR W320'>" + dr["t313_denominacion"].ToString() + "</nobr></td>");
                        }

                        sb.Append("</tr>" + (char)10);
                        break;

                    case 37:    // Organización comercial
                        sb.Append("<tr id='" + dr["ta212_idorganizacioncomercial"].ToString() + "' title='" + dr["ta212_denominacion"].ToString() + "' ");
                        if (int.Parse(hdnIdTipo.Value) == 41) sb.Append("ceec=" + dr["ta212_denominacion"].ToString() + " ");
                        sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;'>");
                        sb.Append("<td style='padding-left:5px;'><nobr class='NBR W320'>" + dr["ta212_denominacion"].ToString() + "</nobr></td>");
                        sb.Append("</tr>" + (char)10);
                        break;

                    case 38:    // Soporte administrativo
                        sb.Append("<tr id='" + dr["NUM_EMPLEADO"].ToString() + "' title='" + dr["profesional"].ToString() + "' ");
                        sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' style='height:20px;'>");
                        sb.Append("<td style='padding-left:5px;'><nobr class='NBR W320'>" + dr["profesional"].ToString() + "</nobr></td>");
                        sb.Append("</tr>" + (char)10);
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer : " + sTitulo, objError);
        }
        return sResul;
    }
    private string getResponsables(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto' style='width:350px;'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup><tbody>");
        try
        {
            SqlDataReader dr = USUARIO.ObtenerProfesionalesResponsablesProyecto(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), (sMostrarBajas == "1") ? true : false);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["idusuario"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' ");

                if ((int)dr["es_responsable"] == 0)
                {
                    sb.Append(" respon='0'><td><img src='../../../../images/imgResponsable.gif' width='16px' height='16px' /></td>");
                    sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                }
                else
                {
                    sb.Append(" respon='1' class='MAM' onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'><td><img src='../../../../images/imgResponsable.gif' width='16px' height='16px' /></td>");
                    sb.Append("<td style='noWrap:true; padding-left:3px;' onclick='mm(event)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los responsables.", ex);
        }
        return sResul;
    }
    private string getSupervisores(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto' style='width:350px;'>");
        sb.Append("<colgroup><col style='width:350px;' /></colgroup><tbody>");
        try
        {
            SqlDataReader dr = USUARIO.ObtenerSupervisores(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), (sMostrarBajas == "1") ? true : false);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["idusuario"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' ");


                sb.Append(" class='MAM' onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'>");
                //sb.Append("<td><img src='../../../../images/imgResponsable.gif' width='16px' height='16px' /></td>");
                //sb.Append("<td style='noWrap:true;' onclick='mmse(this.parentNode)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                //sb.Append("<td style='noWrap:true;' onclick='mmse(this.parentNode)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                sb.Append("<td style='noWrap:true; padding-left:3px;' onclick='mm(event)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los responsables.", ex);
        }
        return sResul;
    }
    private string getProfesionales(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto' style='width:350px;'>");
        sb.Append("<colgroup><col style='width:350px;' /></colgroup><tbody>");
        try
        {
            SqlDataReader dr = USUARIO.ObtenerProfesionalesFicepi(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                                                  (sMostrarBajas == "1") ? true : false, true, (hdnTipoRecurso.Value == "") ? null : hdnTipoRecurso.Value);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["idusuario"].ToString() + "' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("cr='" + dr["t303_denominacion"].ToString() + "' ");
                sb.Append("empresa='" + dr["EMPRESA"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' ");


                sb.Append(" class='MAM' onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'>");
                //sb.Append("<td><img src='../../../../images/imgResponsable.gif' width='16px' height='16px' /></td>");
                //sb.Append("<td style='noWrap:true;' onclick='mmse(this.parentNode)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                //sb.Append("<td style='noWrap:true;' onclick='mmse(this.parentNode)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                sb.Append("<td style='noWrap:true; padding-left:3px;' onclick='mm(event)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los responsables.", ex);
        }
        return sResul;
    }
    private string getComerciales(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto' style='width:350px;'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup><tbody>");
        try
        {
            SqlDataReader dr = USUARIO.ObtenerProfesionalesComercialContrato(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), (sMostrarBajas == "1") ? true : false);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["idusuario"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' ");

                if ((int)dr["es_responsable"] == 0)
                {
                    sb.Append(" respon='0'><td><img src='../../../../images/imgResponsable.gif' width='16px' height='16px' /></td>");
                    sb.Append("<td style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                }
                else
                {
                    sb.Append(" respon='1' class='MAM' onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'><td><img src='../../../../images/imgResponsable.gif' width='16px' height='16px' /></td>");
                    sb.Append("<td style='noWrap:true; padding-left:3px;' onclick='mm(event)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los responsables de contrato.", ex);
        }
        return sResul;
    }

}