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
using EO.Web; 
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial;
    protected string sLectura;
    public string  strTablaHtmlGF, strTablaHTMLIntegrantes, sErrores, sNodo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        strInicial = "";
        sLectura = "false";
        if (!Page.IsCallback)
        {            
            Master.nBotonera = 23;// grabar, regresar
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.TituloPagina = "Asignación de profesionales a grupo funcional";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.bFuncionesLocales = true;
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            
            if (!Page.IsPostBack)
            {
                try
                {
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);

                    
                    this.hdnCR.Text = "";
                    this.hdnMasDeUnGF.Text = "T";

                    //Indica si el usuario es solo responsable de Grupo Funcional
                    if (Request.QueryString["rg"] != null)
                    {
                        this.hdnEsSoloRGF.Value = Request.QueryString["rg"].ToString();
                    }
                    if (Request.QueryString["nCR"] != null)
                    {
                        this.hdnCR.Text = Request.QueryString["nCR"].ToString();
                        if (NODO.bMasDeUnGF(int.Parse(this.hdnCR.Text)))
                            this.hdnMasDeUnGF.Text = "T";
                        else
                            this.hdnMasDeUnGF.Text = "F";
                    }                                     
                    
                    string sGrupoAux = Request.QueryString["nIdGrupo"];
                    if (sGrupoAux != null)
                    {
                        this.hdnIdGf.Text = sGrupoAux;
                        Session["nIdGrupo"] = sGrupoAux;
                    }
                    else if (Session["nIdGrupo"] != null)
                    {
                        sGrupoAux = Session["nIdGrupo"].ToString();
                        this.hdnIdGf.Text = sGrupoAux;
                    }
                    if (sGrupoAux != "")
                    {
                        int iCodGF = int.Parse(sGrupoAux);
                        GrupoFun miGF = new GrupoFun();
                        miGF.Obtener(iCodGF);
                        this.txtDesGF.Text = miGF.sDesGF;
                        this.txtDesGF.Focus();
                        //Cargo la lista de integrantes que ya pertenezcan a este Grupo Funcional
                        strTablaHTMLIntegrantes = ObtenerIntegrantes(iCodGF, miGF.nCodUne);
                    }

                                        
                    txtDesNodo.Visible = true;
                    
                    if (Request.QueryString["nCR"] != null)
                    {
                        string sIdNodo = Request.QueryString["nCR"].ToString();
                        NODO oNodo = NODO.Select(null, int.Parse(sIdNodo));
                        this.txtDesNodo.Text = oNodo.t303_denominacion;
                        this.hdnIdNodo.Text = sIdNodo;
                    }

                    if (!SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) txtDesNodo.ReadOnly = true;


                   

                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                string strUrl = @"~/Capa_Presentacion/PSP/Mantenimientos/GF/Catalogo/Default.aspx";
                if (Request.QueryString["nIdGrupo"] != null)
                {
                    strUrl += "?nIdGrupo=" + Request.QueryString["nIdGrupo"].ToString();
                    if (Request.QueryString["nCR"] != null)
                        strUrl += "&nCR=" + Request.QueryString["nCR"].ToString();
                }
                try
                {
                    Response.Redirect(strUrl, true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
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
            case ("buscar"):
                sResultado += ObtenerPersonas(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            //case ("eliminar"):
            //    sResultado += Eliminar(aArgs[1]);
            //    break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    //protected void Regresar()
    //{
    //    string sUrl = HistorialNavegacion.Leer();
    //    Response.Redirect(sUrl, true);
    //}

    private string ObtenerPersonas(string sAP1, string sAP2, string sNom, string sCR)
    {// Devuelve el código HTML para la lista de candidatos
        StringBuilder sb = new StringBuilder();
        string sCod, sDes, sV1, sV2, sV3, sNumGFs;
        short iCodUne = 0;
        try
        {
            sV1 = Utilidades.unescape(sAP1);
            sV2 = Utilidades.unescape(sAP2);
            sV3 = Utilidades.unescape(sNom);

            if (sCR != "") iCodUne = short.Parse(sCR);
            SqlDataReader dr = GrupoFun.Catalogo(sV1, sV2, sV3, iCodUne);

            sb.Append("<table id='tblOpciones' class='texto MAM' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup><tbody id='tbodyOrigen'>");
            while (dr.Read())
            {
                sCod = dr["CODIGO"].ToString();
                sDes = dr["DESCRIPCION"].ToString().Replace((char)34, (char)39);
                sNumGFs = dr["NUMGFS"].ToString();

                sb.Append("<tr id='" + sCod + "' nGF='" + sNumGFs);
                sb.Append("' onClick='mm(event)' onDblClick='anadirConvocados();' onmousedown='DD(event)' style='height:20px' ");

                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[Profesional:&nbsp;");
                sb.Append(int.Parse(dr["CODIGO"].ToString()).ToString("#,###") + " - " + sDes + "<br>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                
                sb.Append("><td></td><td><span class='NBR W325'>" + sDes + "</span></td></tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close(); dr.Dispose();
            //this.strTablaHTMLPersonas = sb.ToString();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@Error al obtener las personas";
        }
    }
    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3, string sCodUne,
                                    string t305_idProyectoSubnodo, string sCualidad, string sCR_GF)
    {
        //Relacion de técnicos candidatos a ser asignados a la actividad
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();
        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);
            //bool bEsAdminProd = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();
            bool bPonerProf = true;
            
            SqlDataReader dr = SUPER.Capa_Negocio.Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "", true);
            sb.Append("<table id='tblOpciones' class='texto MAM' style='WIDTH: 350px;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                //Solo un administrador puede seleccionar miembros de otro CR
                //22/02/2016 Por petición de Yolanda se podrá seleccionar a cualquier profesional independientemente de su CR
                //if (dr["t303_idnodo"].ToString() != sCR_GF)
                //    if (!bEsAdminProd) bPonerProf = false;
                if (bPonerProf)
                {
                    sb.Append("<tr style='height:20px;noWrap:true;' ");

                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'> ");
                    sb.Append("Información] body=[<label style='width:60px'>Profesional:&nbsp;</label>");
                    sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                    sb.Append(" - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>");
                    sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");

                    sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");
                    //El tipo nos indica el tipo del profesional respecto al proyecto, pero necesitamos el tipo de profesional
                    //con respecto al CR del Grupo Funcional
                    //sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    string sTipo=dr["tipo"].ToString();
                    switch (dr["tipo"].ToString()) 
                    {
                        case "P":
                            if (dr["t303_idnodo"].ToString() != sCR_GF)
                                sTipo = "N";
                            break;
                        case "N":
                            if (dr["t303_idnodo"].ToString() == sCR_GF)
                                sTipo = "P";
                            break;
                    }
                    sb.Append("tipo='" + sTipo + "' ");
                    sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' onclick='mm(event);' ondblclick='anadirConvocados();' onmousedown='DD(event)'>");
                    sb.Append("<td></td><td><nobr class='NBR W325'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }

    private string Grabar(string sCR, string sGrupoFuncional, string sNCR, string sIntegrantes)
    {//En el primer parametro de entrada tenemos codGf#descGF 
        //y en el segundo una lista de codigos de personas separados por comas (persona#responsable,)
        // Se ha añadido un nuevo parámetro (sNCR) que representa el id del nuevo centro de responsavilidad al que van apertener los integrantes seleccionados 
        string sCad, sResul = "", sDesGF, sOperacion;
        int iCodGF, iCodCR, iEmpleado,iResponsable,iPos;
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            //Recojo el código de GF 
            iPos = sGrupoFuncional.IndexOf("#");
            iCodGF = int.Parse(sGrupoFuncional.Substring(0, iPos));
            sDesGF = Utilidades.unescape(sGrupoFuncional.Substring(iPos + 1));

            //Recojo el código de CR             
            iCodCR = int.Parse(sNCR);
            
            //Abro transaccion
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
            if (iCodGF != 0)
            {
                GrupoFun.ModificarGrupo(null, iCodGF, sDesGF, iCodCR);
            }
            else
            {
                iCodGF = GrupoFun.InsertarGrupo(null, sDesGF, iCodCR);
                Session["nIdGrupo"] = iCodGF.ToString();
            }
            //Borrar los integrantes existentes
            //GrupoFun.BorrarIntegrantes(tr,iCodGF);
            if (sIntegrantes == "")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aTareas = Regex.Split(sIntegrantes, @",");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    if (sCad != "")
                    {
                        string[] aElem = Regex.Split(sCad, @"##");
                        sOperacion = aElem[0];
                        iEmpleado = System.Convert.ToInt32(aElem[1]);
                        iResponsable = System.Convert.ToInt32(aElem[2]);
                        //GrupoFun.InsertarIntegrante(tr, iCodGF, iEmpleado, iResponsable);
                        switch (sOperacion)
                        {
                            case "D":
                                GrupoFun.BorrarProfesionalGrupo(tr,iCodGF, iEmpleado);
                                break;
                            case "U":
                                GrupoFun.ModificarProfesional(tr, iCodGF, iEmpleado, iResponsable);
                                break;
                            case "I":
                                GrupoFun.InsertarIntegrante(tr, iCodGF, iEmpleado, iResponsable);
                                break;
                        }//switch
                    }
                }//for
            }
            //Cierro transaccion
            Conexion.CommitTransaccion(tr);
            sCad = ObtenerIntegrantes(iCodGF, int.Parse(sCR));
            //sResul = "OK@#@" + strTablaHTMLIntegrantes;
            sResul = "OK@#@" + iCodGF + "@#@" + sCad;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    public string ObtenerIntegrantes(int iCodGF, int iNodo)
    {// Devuelve el código HTML del catalogo de personas que son integrantes del grupo funcional que se pasa como parametro
        StringBuilder sb = new StringBuilder();
        string sCod, sDes;
        bool bResp;
        try
        {
            SqlDataReader dr = GrupoFun.CatalogoProfesionales(iCodGF);

            sb.Append("<table id='tblOpciones2' class='texto MM' style='WIDTH: 390px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:330px;' /><col style='width:30px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sCod = dr["codigo"].ToString();
                sDes = dr["nombre"].ToString();
                bResp = bool.Parse(dr["responsable"].ToString());

                sb.Append("<tr id='" + sCod + "' bd='' onClick='mm(event)' style='height:20px' onmousedown='DD(event)' ");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                if (dr["tipo"].ToString() == "F") sb.Append("tipo='F' ");
                else
                {
                    if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    else if (dr["t303_idnodo"].ToString() == iNodo.ToString()) sb.Append("tipo='P' ");
                    else sb.Append("tipo='N' ");
                }

                //sb.Append("<td><img src='../../../../../images/imgFN.gif'></td>");
                sb.Append("><td></td><td></td>");

                //sb.Append("<td><label class=texto id='lbl" + sCod + "' style='width:325;text-overflow:ellipsis;overflow:hidden'");
                //if (sDes.Length > 80) sb.Append(" title='" + sDes + "'");
                if (sDes.Length > 80) sb.Append("<td title='" + sDes + "'>");
                else sb.Append("<td>");
                sb.Append("<span class='NBR W330'>" + sDes + "</span></label></td>");

                sb.Append("<td><input type='checkbox' style='width:15' name='chk" + sCod + "' id='chk" + sCod + "' class='checkTabla'");
                if (bResp) 
                    sb.Append(" checked='true' ");

                sb.Append(" onclick=\"activarGrabar();mfa(this.parentNode.parentNode,'U')\"></td></tr>");
            }
            sb.Append("</tbody></table>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }
        catch (Exception)
        {
            //Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@";
        }
    }


    //private string Eliminar(string sIDGF)
    //{
    //    string sResul = "";
    //    try
    //    {
    //        GrupoFun.BorrarGrupo(null, int.Parse(sIDGF));
    //        sResul = "OK";

    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores = Errores.mostrarError("Error al eliminar el grupo funcional", ex);
    //    }

    //    return sResul;
    //}
}