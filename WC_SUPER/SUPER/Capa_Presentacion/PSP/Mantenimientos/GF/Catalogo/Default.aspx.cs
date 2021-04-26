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
//
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtmlGF, strTablaHTMLIntegrantes, sErrores, sNodo = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            bool bEsSoloResponsableGF = EsSoloResponsableGF();
            //Si solo es Responsable de Grupo Funcional no puede añadir ni borrar Grupos Funcionales
            if (bEsSoloResponsableGF)
                Master.nBotonera = 48;//Solo botón Ayuda
            else
                Master.nBotonera = 13;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Catálogo de grupos funcionales";
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
                    if (!bEsSoloResponsableGF)
                        this.hdnEsSoloRGF.Value = "N";

                    strTablaHtmlGF = "<table id='tblDatos'><tbody id='tbodyDatos'></tbody></table>";
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

                    if (Request.QueryString["nIdGrupo"] != null)
                        this.hdnIdGrupo.Value = Request.QueryString["nIdGrupo"].ToString();
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        //hdnIdNodo.Visible = true;
                        txtDesNodo.Visible = true;
                        
                        if (Request.QueryString["nCR"] != null)
                        {
                            string sIdNodo = Request.QueryString["nCR"].ToString();
                            ObtenerGFs("1", "0", sIdNodo, false);
                            NODO oNodo = NODO.Select(null, int.Parse(sIdNodo));
                            this.txtDesNodo.Text = oNodo.t303_denominacion;
                            this.hdnIdNodo.Text = sIdNodo;
                        }
                    }
                    else
                    {
                        cboCR.Visible = true;
                        //hdnIdNodo.Visible = false;
                        txtDesNodo.Visible = false;
                        string sIdNodo = "";
                        //if (Request.QueryString["A"] != null)
                        //    sIdNodo = Request.QueryString["A"].ToString();
                        if (Request.QueryString["nCR"] != null)
                            sIdNodo = Request.QueryString["nCR"].ToString();
                        cargarNodos(sIdNodo, bEsSoloResponsableGF);
                    }
                    
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
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
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
            case ("buscar"):
                bool bEsSoloResponsableGF=true;
                if (aArgs[4] != "S")bEsSoloResponsableGF=false;
                sResultado += ObtenerGFs(aArgs[1], aArgs[2], aArgs[3], bEsSoloResponsableGF);
                break;
            case ("eliminar"):
                sResultado += EliminarGF(aArgs[1]);
                break;
            case ("integrantes"):
                sResultado += ObtenerIntegrantes(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
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
    private string ObtenerGFs(string sOrden, string sAscDesc, string sCodUne, bool bEsSoloResponsableGF)
    {// Devuelve el código HTML del catalogo de grupos funcionales de la UNE que se pasa por parámetro
        StringBuilder strBuilder = new StringBuilder();
        string sDesc, sCod, sResul;
        try
        {
            //strBuilder.Append("<div style='background-image:url(../../../../../Images/imgFT16.gif); width:0%; height:0%'>");
            strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 430px;'>");
            strBuilder.Append("<colgroup><col style='width:430px;'/></colgroup>");
            //strBuilder.Append("<tbody>");
            if (sCodUne != "")
            {
                SqlDataReader dr;
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                {
                    dr = GrupoFun.CatalogoGrupos(int.Parse(sOrden), int.Parse(sAscDesc), int.Parse(sCodUne));
                }
                else
                {
                    if (bEsSoloResponsableGF)
                        dr = GrupoFun.CatalogoGruposResponsable(int.Parse(sOrden), int.Parse(sAscDesc), int.Parse(sCodUne),
                                                                (int)Session["UsuarioActual"]);
                    else
                    {
                        //Puede que tenga figura de nodo pero en un nodo diferente del que se quiere obtener los GF
                        //Así que no queda más remedio que preguntar si es Responsable, Delegado o Colaborador en ese nodo
                        ArrayList aFig = SUPER.Capa_Negocio.FIGURANODO.Lista(int.Parse(sCodUne), (int)Session["UsuarioActual"]);
                        bEsSoloResponsableGF = true;
                        foreach (string oElem in aFig)
                        {
                            if (oElem == "R" || oElem == "D" || oElem == "C" || oElem == "OT")
                            {
                                bEsSoloResponsableGF = false;
                                break;
                            }
                        }
                        if (bEsSoloResponsableGF)
                            dr = GrupoFun.CatalogoGruposResponsable(int.Parse(sOrden), int.Parse(sAscDesc), int.Parse(sCodUne),
                                                                    (int)Session["UsuarioActual"]);
                        else
                            dr = GrupoFun.CatalogoGrupos(int.Parse(sOrden), int.Parse(sAscDesc), int.Parse(sCodUne));
                    }
                }
                while (dr.Read())
                {
                    sCod = dr["idGrupro"].ToString();
                    sDesc = dr["Nombre"].ToString();

                    strBuilder.Append("<tr id='" + sCod + "' cr=" + sCodUne + " style='height:20px'");
                    //strBuilder.Append(" onclick='mm(event);mostrarIntegrantes(this.id);' ondblclick='mostrarDetalle(this.id)'>");
                    strBuilder.Append(" onclick='mm(event);mostrarIntegrantes(this.id);' ondblclick='mostrarDetalleAux(this)'>");
                    strBuilder.Append("<td>" + sDesc + "</td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            //strBuilder.Append("</tbody>");
            strBuilder.Append("</table>");//</div>

            sResul = strBuilder.ToString();
            this.strTablaHtmlGF = sResul;
            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los Grupos Funcionales", ex);
            return "error@#@Error al obtener los Grupos Funcionales " + ex.Message;
        }
    }
    private string EliminarGF(string strGrupo)
    {
        string sResul = "OK@#@";
        int idGF = -1;
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
            string[] aGF = Regex.Split(strGrupo, "##");
            foreach (string oGF in aGF)
            {
                if (oGF != "")
                {
                    idGF = int.Parse(oGF);
                    GrupoFun.BorrarGrupo(tr, int.Parse(oGF));
                }
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            Master.sErrores = Errores.mostrarError("Error al eliminar el grupo funcional " + idGF.ToString(), ex);
            sResul = "Error@#@No se ha podido eliminar el grupo funcional " + idGF.ToString() + ".\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private void cargarNodos(string sNodo,bool bEsSoloResponsableGF)
    {
        try
        {
            bool bSeleccionado = false;
            //Cargo la denominacion del label Nodo
            this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            //SqlDataReader dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], true);
            SqlDataReader dr = GrupoFun.NodosVisibles((int)Session["UsuarioActual"]);
            while (dr.Read())
            {
                oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                if (!bSeleccionado)
                {
                    if (sNodo == "")
                    {
                        oLI.Selected = true;
                        bSeleccionado = true;
                        ObtenerGFs("1", "0", dr["t303_idnodo"].ToString(), bEsSoloResponsableGF);
                    }
                    else
                    {
                        if (sNodo == dr["t303_idnodo"].ToString())
                        {
                            oLI.Selected = true;
                            bSeleccionado = true;
                            ObtenerGFs("1", "0", sNodo, bEsSoloResponsableGF);
                        }
                    }
                }
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

    public string ObtenerIntegrantes(int iCodGF, int iNodo)
    {// Devuelve el código HTML del catalogo de personas que son integrantes del grupo funcional que se pasa como parametro
        StringBuilder sb = new StringBuilder();
        string sCod, sDes;
        try
        {
            SqlDataReader dr = GrupoFun.CatalogoProfesionales(iCodGF);

            sb.Append("<table id='tblOpciones2' style='width: 430px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:385px;' /><col style='width:25px;' /></colgroup>");
            //sb.Append("<tbody id='tbodyDestino'>");
            while (dr.Read())
            {
                sCod = dr["codigo"].ToString();
                sDes = dr["nombre"].ToString();

                sb.Append("<tr id='" + sCod + "' style='height:20px' sexo='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else if (dr["t303_idnodo"].ToString() == iNodo.ToString()) sb.Append("tipo='P' ");
                //else sb.Append("tipo='N' ");
                sb.Append("><td></td>");//para el icono de la persona
                sb.Append("<td onmouseover='TTip(event)'><span class='NBR W380'>" + sDes + "</span></label></td>");
                if ((bool)dr["responsable"])
                    sb.Append("<td><img style='width:10px; height:10px;' src='../../../../../images/imgOk.gif'></td>");
                else
                    sb.Append("<td></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");//</tbody>
            dr.Close();
            dr.Dispose();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception)
        {
            //Master.sErrores = Errores.mostrarError("Error al obtener las personas", ex);
            return "error@#@";
        }
    }
    /// <summary>
    /// Devuelve si el usuario actual es solo responsable de grupo funcional, en cuyo caso solo podrá añadir/eliminar integrantes
    /// (y que además sean del CR del GF)
    /// </summary>
    /// <returns></returns>
    private bool EsSoloResponsableGF()
    {
        bool bRes = true;
        if (User.IsInRole("A") || User.IsInRole("OT") || User.IsInRole("RN") || User.IsInRole("DN") || User.IsInRole("CN")) 
            bRes = false;
        return bRes;
    }
}