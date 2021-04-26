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
using EO.Web;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial = "", sErrores;//, sLectura = "false";
    public string sNodo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Copia de profesionales por batería";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    //if (!(bool)Session["FORANEOS"])
                    //{
                    //    this.imgForaneo.Visible = false;
                    //    this.lblForaneo.Visible = false;
                    //}
                    //Cargo la denominacion del label Nodo
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    if (sNodo.Trim() != "")
                    {
                        this.lblNodo.InnerText = sNodo;
                        this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                        this.gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    }

                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        hdnIdNodo.Visible = true;
                        txtDesNodo.Visible = true;
                        gomaNodo.Visible = true;
                    }
                    else
                    {
                        cboCR.Visible = true;
                        hdnIdNodo.Visible = false;
                        txtDesNodo.Visible = false;
                        gomaNodo.Visible = false;
                        cargarNodos();
                    }
                    
                    
                    SqlDataReader dr= USUARIO.ObtenerDatosProfUsuario((int)Session["UsuarioActual"]);
                    if (dr.Read())
                        this.hdnCRActual.Value = dr["t303_idnodo"].ToString();
                    dr.Close();
                    dr.Dispose();
                    txtApe1.Focus();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case "PROYECTOS":
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12]);
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
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, true);
            while (dr.Read())
            {
                oLI = new ListItem(dr["DENOMINACION"].ToString(), dr["IDENTIFICADOR"].ToString());
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3, string t305_idProyectoSubnodo, string sTiposProfesional, string sSoloActivos)
    {
        string sResul = "", sV1, sV2, sV3;
        bool bSoloActivos = true;
        StringBuilder sb = new StringBuilder();
        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);

            if (sSoloActivos == "N")
                bSoloActivos = false;

            //SqlDataReader dr = Recurso.ObtenerRelacionProfesionalesTarifa(strOpcion, sV1, sV2, sV3, sCodUne, t305_idProyectoSubnodo, sCualidad, "", true);
            SqlDataReader dr = Recurso.GetProfesionales(strOpcion, sV1, sV2, sV3, t305_idProyectoSubnodo, sTiposProfesional, bSoloActivos);
            sb.Append("<table id='tblOpciones2' class='texto MAM' style='WIDTH: 440px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[<label style='width:60px'>Profesional:&nbsp;</label>");
                sb.Append(dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>");

                sb.Append("<label style='width:60px'>Usuario:&nbsp;</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append("<br><label style='width:60px'>");

                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>Empresa&nbsp;:</label>");
                //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");

                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' onclick='mm(event);' ondblclick='insertarRecurso(this);' onmousedown='DD(event)'>");
                sb.Append("<td></td><td><nobr class='NBR' style='width:410px;'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");

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
    private string ObtenerProyectos(string sNodo, string sEstado, string sCategoria, string sIdCliente, string sIdResponsable, string sNumPE, string sDesPE, string sTipoBusqueda, 
                                    string sCualidad, string sIDContrato, string sIdHorizontal, string sIdNaturaleza)
    {
        string sResul = "", sHayProyectos="F";
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            SqlDataReader dr = PROYECTO.ObtenerProyectos
            (
                "pge",
                (sNodo != "") ? (int?)int.Parse(sNodo) : null,
                sEstado,
                sCategoria,
                (sIdCliente != "") ? (int?)int.Parse(sIdCliente) : null,
                (sIdResponsable != "") ? (int?)int.Parse(sIdResponsable) : null,
                (sNumPE != "" && sNumPE != "0") ? (int?)int.Parse(sNumPE) : null,
                Utilidades.unescape(sDesPE),
                sTipoBusqueda,
                sCualidad,
                (sIDContrato != "") ? (int?)int.Parse(sIDContrato) : null,
                (sIdHorizontal != "") ? (int?)int.Parse(sIdHorizontal) : null,
                (int)Session["UsuarioActual"],
                false, false, null, null, null, null, null, false,
                (sIdNaturaleza != "") ? (int?)int.Parse(sIdNaturaleza) : null,
                null,null,null
            );

            sb.Append("<table id='tblDatos' class='texto MAM' style='width:440px;'>" + (char)10);
            sb.Append("<colgroup>" + (char)10);
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:330px;' />");
            //sb.Append("<col style='width:20px' />");
            sb.Append("</colgroup>" + (char)10);

            while (dr.Read())
            {
                if (dr["t305_cualidad"].ToString() == "J") continue;
                sHayProyectos = "T";
                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "'");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("nodo='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("cierre='" + dr["t303_ultcierreeco"].ToString() + "' ");
                //sb.Append("ondblclick='insertarItem(this)' onclick='mmse(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:22px'><td></td><td></td><td></td><td style='text-align:right; padding-right:10px;'");
                if (ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
                    sb.Append(" title=\"" + dr["desmotivo"].ToString() + "\"");
                sb.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W310' style='noWrap:true; cursor:pointer;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                //sb.Append("<td><input type='checkbox' style='width:15' class='checkTabla' checked='true'></td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString() + "@#@" + sHayProyectos;
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer obtenerProyectos ", objError);
        }
        return sResul;
    }

    private string Grabar(string sRecursos, string sProys)
    {
        string sResul = "", sItem;
        int idPSN, idUser;
        bool bDeriva, bNotif=false;
        decimal costecon = 0, costerep = 0;
        try
        {
            if (sRecursos == "" || sProys=="")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {//Con la cadenas generamos listas y las recorremos para grabar cada elemento
                string[] aRecursos = Regex.Split(sRecursos, @"##");
                for (int i = 0; i < aRecursos.Length; i++)
                {
                    idUser = int.Parse(aRecursos[i]);

                    string[] aItems = Regex.Split(sProys, @"##");
                    for (int j = 0; j < aItems.Length; j++)
                    {
                        sItem = aItems[j];
                        string[] aIt = Regex.Split(sItem, @",");
                        idPSN = int.Parse(aIt[0]);
                        if (aIt[3] == "T")
                            bDeriva = true;
                        else
                            bDeriva = false;
                        costecon = 0;
                        costerep = 0;
                        SqlDataReader dr = USUARIOPROYECTOSUBNODO.GetCoste(idPSN, idUser);
                        if (dr.Read())
                        {
                            costecon = decimal.Parse(dr["coste_con"].ToString());
                            costerep = decimal.Parse(dr["coste_rep"].ToString());
                        }
                        dr.Close();
                        dr.Dispose();
                        //ID item, id nodo, anomes del ultimo cierre economico, id proyectosubnodo
                        sResul += PonerRecurso(idUser, int.Parse(aIt[1]), int.Parse(aIt[2]), idPSN, bDeriva, costecon, costerep, bNotif);
                    }
                }//for
            }
            if (sResul != "") 
                sResul = "Error@#@" + sResul;
            else
                sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
        }
        return sResul;
    }
    private string PonerRecurso(int IdRecurso, int IdNodo, int iUltCierreEco, int IdPsn,
                                bool bDeriva, decimal costecon, decimal costerep, bool bNotif)
    {
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        string sResul = "";
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
            //lA FECHA DE alta en el proyecto será la siguiente al último mes cerrado del nodo
            DateTime dtFechaAlta = Fechas.AnnomesAFecha(Fechas.AddAnnomes(iUltCierreEco, 1));
            if (!TareaRecurso.AsociadoAProyecto(tr, IdPsn, IdRecurso))
            {
                //TareaRecurso.AsociarAProyecto(tr, IdNodo, IdRecurso, IdPsn, null, dtFechaAlta, null);
                //if (costecon == null) costecon = 0;
                USUARIOPROYECTOSUBNODO.Insert(tr, IdPsn, IdRecurso, costecon, costerep, bDeriva, dtFechaAlta, null, null);
            }
            else
            {
                //TareaRecurso.ReAsociarAProyecto(tr, IdRecurso, IdPsn);
                if (!USUARIOPROYECTOSUBNODO.AsociadoDeAltaProyecto(tr, IdPsn, IdRecurso))
                    USUARIOPROYECTOSUBNODO.Update(tr, IdPsn, IdRecurso, costecon, costerep, bDeriva, dtFechaAlta, null, null);
            }
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = Errores.mostrarError("Error al grabar", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
