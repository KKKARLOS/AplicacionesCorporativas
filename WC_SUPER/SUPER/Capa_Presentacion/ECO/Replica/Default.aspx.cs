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
using EO.Web;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sOrigen = "";
    public string sOpcion = "", sCualidad, sListaProy = "";//, sListaProyRepl = "";
    public string strTablaHTML = "";
    public string strArrayNodos = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.nBotonera = 33;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Generación de réplicas";
            Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/Replica/Functions/NodoReplica.js");
                
            if (!Page.IsPostBack)
            {
                if (!Page.IsCallback)
                {
                    try
                    {
                        sOrigen = Request.QueryString["origen"];
                        sOpcion = Request.QueryString["opcion"];
                        sCualidad = Request.QueryString["sCualidad"];
                        sListaProy = Request.QueryString["lp"];
                        if (sOrigen == "proynocerrados" && sOpcion == "cerrarlista")
                        {
                            #region Cerrar una lista de proyectos
                            SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosAReplicar((int)Session["UsuarioActual"], 
                                                                                         false, sListaProy);
                            bool bHayProyParaReplicar=ponerProyectos(dr);

                            if (!bHayProyParaReplicar)
                            {//Me voy a la pantalla de cerrar.
                                try
                                {
                                    Response.Redirect("../Cierre/Default.aspx?lp=" + sListaProy + 
                                                        "&sAnomes=" + Request.QueryString["sAnomes"] + 
                                                        "&origen=" + Request.QueryString["origen"] + 
                                                        "&opcion=" + sOpcion, false);
                                }
                                catch (System.Threading.ThreadAbortException) { }
                            }
                            #endregion
                        }
                        else
                        {
                            if (sOrigen == "carrusel" || (sOrigen == "proynocerrados" && sOpcion == "cerrarproy"))
                            {
                                #region Cerrar un proyecto
                                switch (Request.QueryString["opcion"])
                                {
                                    case "replicar":
                                        getProyectoCarrusel(Request.QueryString["nProy"], 
                                                            Utilidades.decodpar(Request.QueryString["sProy"]), 
                                                            Request.QueryString["nPSN"], sOrigen);
                                        getNodos(Request.QueryString["nProy"], sOrigen, false);
                                        break;

                                    case "cerrarmes":
                                    case "cerrarproy":
                                        //sOpcion = "cerrarmes";
                                        if (sCualidad == "C" &&
                                            PROYECTOSUBNODO.EsNecesarioReplicar((int)Session["UsuarioActual"],
                                                                                int.Parse(Request.QueryString["nProy"]),
                                                                                SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()))
                                        {
                                            getProyectoCarrusel(Request.QueryString["nProy"], 
                                                                Utilidades.decodpar(Request.QueryString["sProy"]), 
                                                                Request.QueryString["nPSN"], sOrigen);
                                            getNodos(Request.QueryString["nProy"], sOrigen, false);
                                        }
                                        else
                                        {
                                            //Me voy a la pantalla de cerrar.
                                            try
                                            {
                                                Response.Redirect("../Cierre/Default.aspx?nProy=" + Request.QueryString["nProy"] + 
                                                    "&sProy=" + Utilidades.decodpar(Request.QueryString["sProy"]) + 
                                                    "&nPSN=" + Request.QueryString["nPSN"] + 
                                                    "&sAnomes=" + Request.QueryString["sAnomes"] + 
                                                    "&origen=" + Request.QueryString["origen"] + 
                                                    "&opcion=" + Request.QueryString["opcion"], false);
                                            }
                                            catch (System.Threading.ThreadAbortException) { }
                                        }
                                        break;
                                }
                                #endregion
                            }
                            else
                            {
                                #region Cerrar todos los proyectos
                                if (sOrigen == "menucierre") sOpcion = "cerrarmes";
                                if (sOrigen == "menucierresat" || sOrigen == "menucierresatsaa")
                                {
                                    sOpcion = sOrigen;
                                    if (PROYECTOSUBNODO.EsNecesarioReplicarUSA((int)Session["UsuarioActual"], (sOrigen == "menucierresatsaa") ? true : false))
                                    {
                                        getProyectos(true);
                                        getNodos("", "", true);
                                    }
                                    else
                                    {
                                        //Me voy a la pantalla de cerrar.
                                        try
                                        {
                                            Response.Redirect("../Cierre/Default.aspx?origen=" + Request.QueryString["origen"] + "&opcion=" + sOpcion, false);
                                        }
                                        catch (System.Threading.ThreadAbortException) { }
                                    }

                                }
                                else if (PROYECTOSUBNODO.EsNecesarioReplicar((int)Session["UsuarioActual"], null, false))
                                {
                                    getProyectos(false);
                                    getNodos("", "", false);
                                }
                                else if (sOrigen == "menucierre" || sOrigen == "proynocerrados")
                                {
                                    //Me voy a la pantalla de cerrar.
                                    try
                                    {
                                        Response.Redirect("../Cierre/Default.aspx?nProy=" + Request.QueryString["nProy"] + 
                                            "&sProy=" + Utilidades.decodpar(Request.QueryString["sProy"]) + 
                                            "&nPSN=" + Request.QueryString["nPSN"] + 
                                            "&sAnomes=" + Request.QueryString["sAnomes"] + 
                                            "&origen=" + Request.QueryString["origen"] + 
                                            "&opcion=" + sOpcion, false);
                                    }
                                    catch (System.Threading.ThreadAbortException) { }
                                }
                                #endregion
                            }
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
            case ("setReplica"):
                sResultado += procesarReplica(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
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

    private void getProyectoCarrusel(string nProy, string sProy, string nPSN, string sOrigen)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatos' style='width: 700px;'>");
        sb.Append("<colgroup>");
        sb.Append("<col style='width:100px;' />");
        sb.Append("<col style='width:580px;' />");
        sb.Append("<col style='width:20px;' />");
        sb.Append("</colgroup>");
        sb.Append("<tbody>");

        bool bCarrusel = false;
        if (sOrigen == "carrusel")
        {
            bCarrusel = (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) ? true : false;
        }

        SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosAReplicar((int)Session["UsuarioActual"], int.Parse(nProy), bCarrusel);

        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' nPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' onclick='ms(this);getNodos(this)' style='height:20px' procesado=''>");
            sb.Append("<td style='text-align:right; padding-right:8px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
            sb.Append("<td style='padding-left:3px;'>" + dr["t301_denominacion"].ToString() + "</td>");
            if ((int)dr["motivo"] == 0) sb.Append("<td style='padding-left:1px;'><img src='../../../Images/imgRepPrec.gif' /></td>");
            else sb.Append("<td style='padding-left:1px;'><img src='../../../Images/imgRepNO.gif' /></td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");

        strTablaHTML = sb.ToString();
    }
    private void getProyectos(bool bUSA)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table class='texto MANO' id='tblDatos' style='width: 700px;'>");
        sb.Append("<colgroup>");
        sb.Append("<col style='width:100px;' />");
        sb.Append("<col style='width:580px;' />");
        sb.Append("<col style='width:20px;' />");
        sb.Append("</colgroup>");
        sb.Append("<tbody>");

        SqlDataReader dr;
        
        if (bUSA){
            dr = PROYECTOSUBNODO.ObtenerProyectosAReplicarUSA((int)Session["UsuarioActual"], (sOrigen == "menucierresatsaa") ? true : false);
        }
        else
        {
            dr = PROYECTOSUBNODO.ObtenerProyectosAReplicar((int)Session["UsuarioActual"], null, false);
        }
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' nPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' onclick='ms(this);getNodos(this)' style='height:20px' procesado=''>");
            sb.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
            sb.Append("<td style='padding-left:3px;'>" + dr["t301_denominacion"].ToString() + "</td>");
            if ((int)dr["motivo"] == 0) sb.Append("<td style='padding-left:3px;'><img src='../../../Images/imgRepPrec.gif' /></td>");
            else sb.Append("<td style='padding-left:3px;'><img src='../../../Images/imgRepNO.gif' /></td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");

        strTablaHTML = sb.ToString();
    }
    private void getNodos(string nProy, string sOrigen, bool bUSA)
    {
        StringBuilder sb = new StringBuilder();
        int? nProyecto = null;
        string sPropFirme = "0";

        if (nProy != "") nProyecto = int.Parse(nProy);
        bool bCarrusel = false;
        if (sOrigen == "carrusel")
        {
            bCarrusel = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();
        }

        SqlDataReader dr;
        if (bUSA)
            dr = PROYECTOSUBNODO.ObtenerNodosDeProyectosAReplicarUSA((int)Session["UsuarioActual"], (sOrigen == "menucierresatsaa") ? true : false);
        else
            dr = PROYECTOSUBNODO.ObtenerNodosDeProyectosAReplicar((int)Session["UsuarioActual"], nProyecto, bCarrusel); 
        while (dr.Read())
        {
            if (dr["tiporeplica"].ToString() == "J") sPropFirme = "1";
            else sPropFirme = "0";
            sb.Append("insertarNodoEnArray(" + dr["t301_idproyecto"].ToString() + "," + dr["idNodo"].ToString() + ",\"" + dr["t303_denominacion"].ToString() + "\",\"" + dr["tiporeplica"].ToString() + "\"," + sPropFirme + ",\"\",\"\",\"\");" + (char)13);
        }
        dr.Close();
        dr.Dispose();

        strArrayNodos = sb.ToString();
    }
    private string procesarReplica(string nProyecto, string nPSN, string sProy, string strDatos)
    {
        string sResul = "";
        //DataSet ds;
        //int nCount = 0, idNodoAuxDestino=0, idNodoAuxManiobra = 0, idNodoAuxManiobra3=0, nCountSubnodosNoManiobra = 0;
        int idSubNodoGrabar = 0;
        int nAux = 0;
        int idNodo=-1;
        int nResponsablePSN = 0;
        bool bReintentar = true;

        #region apertura de conexión y transacción
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

        try
        {
            string[] aNodo = Regex.Split(strDatos, "///");

            foreach (string oNodo in aNodo)
            {
                if (oNodo == "") continue;

                //nCount = 0;
                //idNodoAuxManiobra = 0;
                //idNodoAuxManiobra3 = 0;
                //nCountSubnodosNoManiobra = 0;
                //idNodoAuxDestino = 0;
                idSubNodoGrabar = 0;

                string[] aValores = Regex.Split(oNodo, "##");
                //0. idNodo
                //1. tiporeplica
                //2. idGestor
                idNodo= int.Parse(aValores[0]);
                if (!PROYECTOSUBNODO.ExisteProyectoSubNodo(tr, int.Parse(nProyecto), idNodo)) 
                {
                    nResponsablePSN = (aValores[2] == "") ? 0 : int.Parse(aValores[2]);
                    #region Cálculo de subnodo destino OLD
                    /*
                    nCount = 0;
                    ds = PROYECTOSUBNODO.ObtenerSubnodosParaReplicar(tr, int.Parse(aValores[0]));
                    foreach (DataRow oFila in ds.Tables[0].Rows)
                    {
                        //Maniobra=3 es el defecto para replicar
                        if ((byte)oFila["t304_maniobra"] == 3)
                        {
                            idNodoAuxManiobra3 = (int)oFila["t304_idsubnodo"];
                        }
                        else
                        {
                            if ((byte)oFila["t304_maniobra"] == 1)
                            {
                                nCount++;
                                idNodoAuxManiobra = (int)oFila["t304_idsubnodo"];
                            }
                            else
                            {
                                nCountSubnodosNoManiobra++;
                                idNodoAuxDestino = (int)oFila["t304_idsubnodo"];
                            }
                        }
                    }

                    if (nCountSubnodosNoManiobra == 1) //si solo hay un subnodo en el nodo, que la réplica se haga a ese subnodo.
                    {
                        idSubNodoGrabar = idNodoAuxDestino;
                    }
                    else
                    {
                        if (idNodoAuxManiobra3 != 0)
                        {
                            idSubNodoGrabar = idNodoAuxManiobra3;
                        }
                        else
                        {
                            if (nCount == 0)
                            {
                                NODO oNodo2 = NODO.SelectEnTransaccion(tr, int.Parse(aValores[0]));
                                nResponsablePSN = oNodo2.t314_idusuario_responsable;
                                //crear subnodo maniobra
                                idSubNodoGrabar = SUBNODO.Insert(tr, "Proyectos a reasignar", int.Parse(aValores[0]), 0, true, 1, oNodo2.t314_idusuario_responsable, null);
                            }
                            else
                            {
                                if (nCount > 1)
                                {
                                    bReintentar = false;
                                    ds.Dispose();
                                    throw (new Exception("El número de subnodos de maniobra es " + nCount.ToString() + " en el nodo " + aValores[0] + ". Por favor avise al administrador."));
                                }

                                if (ds.Tables[0].Rows.Count - 1 > 1 || ds.Tables[0].Rows.Count - 1 == 0)
                                {
                                    idSubNodoGrabar = idNodoAuxManiobra;
                                }
                                else
                                {
                                    idSubNodoGrabar = idNodoAuxDestino;
                                }
                            }
                        }
                    }
                    ds.Dispose();
                    */
                    #endregion
                    idSubNodoGrabar = SUPER.Capa_Negocio.PROYECTOSUBNODO.ObtenerSubnodoDestinoReplica(tr, idNodo);

                    if (nResponsablePSN == 0)
                    {
                        NODO oNodo3 = NODO.SelectEnTransaccion(tr, idNodo);
                        nResponsablePSN = oNodo3.t314_idusuario_responsable;
                    }
                    nAux = PROYECTOSUBNODO.Insert(tr, int.Parse(nProyecto), idSubNodoGrabar, false, aValores[1], false, nResponsablePSN,
                                                sProy, "X", "X", false, false, false, false, false, "", "", "", null, null, null, null, 
                                                null, null, false, 0);
                }
            }

            SEGMESPROYECTOSUBNODO.GenerarMesEnTransaccion(tr, int.Parse(nProyecto));

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar la réplica del proyecto.", ex, bReintentar);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
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

    private bool ponerProyectos(SqlDataReader dr)
    {
        bool bRes = false;
        string sPropFirme = "0";

        StringBuilder sb = new StringBuilder();
        StringBuilder sbNodos = new StringBuilder();

        sb.Append("<table class='texto MANO' id='tblDatos' style='width: 700px;'>");
        sb.Append("<colgroup>");
        sb.Append("<col style='width:100px;' />");
        sb.Append("<col style='width:580px;' />");
        sb.Append("<col style='width:20px;' />");
        sb.Append("</colgroup>");
        sb.Append("<tbody>");

        while (dr.Read())
        {
            bRes = true;
            //sListaProyRepl += dr["t305_idproyectosubnodo"].ToString() + ",";

            sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "' nPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' onclick='ms(this);getNodos(this)' style='height:20px' procesado=''>");
            sb.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
            sb.Append("<td style='padding-left:3px;'>" + dr["t301_denominacion"].ToString() + "</td>");
            if ((int)dr["motivo"] == 0) sb.Append("<td style='padding-left:3px;'><img src='../../../Images/imgRepPrec.gif' /></td>");
            else sb.Append("<td style='padding-left:3px;'><img src='../../../Images/imgRepNO.gif' /></td>");
            sb.Append("</tr>");
            //Para el array de nodos
            if (dr["tiporeplica"].ToString() == "J") sPropFirme = "1";
            else sPropFirme = "0";
            sbNodos.Append("insertarNodoEnArray(" + dr["t301_idproyecto"].ToString() + "," + dr["idnodo"].ToString() + ",\"" +
                    dr["denNodo"].ToString() + "\",\"" + dr["tiporeplica"].ToString() + "\"," + sPropFirme + ",\"\",\"\",\"\");" + (char)13);

        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");

        strTablaHTML = sb.ToString();
        strArrayNodos = sbNodos.ToString();

        return bRes;
    }

}
