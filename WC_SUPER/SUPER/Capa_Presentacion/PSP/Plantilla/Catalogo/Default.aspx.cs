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
//para unescape


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string gsTipo = "E", sErrores;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 6;
            Master.bFuncionesLocales = true;

            Master.TituloPagina = "Catálogo de plantillas";
            if (!Page.IsPostBack)
            {
                try
                {
                    this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    this.lblNodo2.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                    //this.lblNodo2.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    this.gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));

                    string sNodo = "";
                    //if (Request.QueryString["nCR"] != null)
                    //    sNodo = Request.QueryString["nCR"].ToString();
                    if (Request.QueryString["rCR"] != null)
                    {
                        sNodo = Request.QueryString["rCR"].ToString();
                        this.hdnIdNodo.Text = sNodo;
                    }
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    {
                        cboCR.Visible = false;
                        //hdnIdNodo.Visible = true;
                        txtDesNodo.Visible = true;
                        gomaNodo.Visible = true;
                        
                        if (sNodo != "")
                        {
                            NODO oNodo = NODO.Select(null, int.Parse(sNodo));
                            txtDesNodo.Text = oNodo.t303_denominacion;
                        }
                    }
                    else
                    {
                        cboCR.Visible = true;
                        if (sNodo != "")
                        {
                            this.cboCR.SelectedValue = sNodo;
                        }
                        //hdnIdNodo.Visible = false;
                        txtDesNodo.Visible = false;
                        gomaNodo.Visible = false;
                    }

                    string sTipo = "", sOrigen = "";
                    string sAux = Request.QueryString["sTipo"];
                    if (sAux != "")
                    {
                        sTipo = sAux.Substring(0, 1);
                        if (sAux.Length > 1)
                            sOrigen = sAux.Substring(1, 1);
                        if (sTipo=="T")//si plantilla de proyecto técnico, deshabilito check de empresarial(no hay plantillas empresariales de PT)
                        {
                            this.chkEmp.Checked = false;
                            this.chkEmp.Enabled = false;
                        }
                    }
                    if (sOrigen == "2")
                        this.txtOrigen.Text = "A";
                    if (sTipo != "")
                    {
                        gsTipo = sTipo;
                        Session["TIPO_PLANT"] = sTipo;
                    }
                    else
                    {
                        if (Session["TIPO_PLANT"] != null) gsTipo = Session["TIPO_PLANT"].ToString();
                        else Session["TIPO_PLANT"] = gsTipo;
                    }
                    string sEmp = "0", sDep = "0", sPer = "0";
                    if (Request.QueryString["bE"] == null)
                    {//Es la primera vez que entramos a la pantalla
                        sPer = "1";
                        this.chkPer.Checked = true;
                    }
                    else
                    {
                        sEmp = Request.QueryString["bE"];
                        sDep = Request.QueryString["bD"];
                        sPer = Request.QueryString["bP"];
                        if (sEmp == "1") this.chkEmp.Checked = true;
                        if (sDep == "1") this.chkDep.Checked = true;
                        if (sPer == "1") this.chkPer.Checked = true;
                    }
                    //obtenerPlantillas(this.txtOrigen.Text);
                    cargarNodos(this.txtOrigen.Text, sNodo, sEmp, sDep, sPer);

                    //Establezco el perfil del empleado para poder controlar desde el lado cliente las plantillas borrables
                    //	Si la plantilla es Empresarial solo será borrable si el usuario conectado tiene perfil de Administrador
                    //	Si la plantilla es Departamental solo será borrable si el usuario conectado tiene perfil de Oficina Técnica o superior
                    //	Si la plantilla es Personal siempre es borrable (se supone que un usuario normal solo ve las plantillas personales que son suyas)
                    string sPerfil = "T";
                    if (User.IsInRole("A")) sPerfil = "A";
                    else
                    {
                        if (User.IsInRole("RSN") || User.IsInRole("DSN") || User.IsInRole("ISN") || User.IsInRole("RN")
                            || User.IsInRole("DN") || User.IsInRole("CN") || User.IsInRole("IN") || User.IsInRole("OT"))
                            sPerfil = "D";
                    }
                    this.txtPerfil.Text = sPerfil;
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener el catálogo de plantillas", ex);
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
                sResultado += EliminarPlantilla(aArgs[1]);
                break;
            case ("buscar"):
                sResultado += Buscar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], true);
                break;
            case ("grabarcomo"):
                sResultado += GrabarComo(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9]);
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
    //public string obtenerPlantillas(int iNodo, string sOrigen, bool bRecarga)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    string sResul = "OK";
    //    try
    //    {//
    //        SqlDataReader dr;
    //        if (iNodo == -1)//Saca todas las plantillas de los nodos administrables
    //            dr = PlantProy.Catalogo(1, 0, gsTipo, -1, int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), sOrigen);
    //        else//Saca todas las plantillas del nodo seleccionado
    //            dr = PlantProy.Catalogo(1, 0, gsTipo, iNodo, int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), sOrigen);
    //        int i = 0;
    //        sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 850px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0'>");
    //        sb.Append("<colgroup><col style='width:27px;padding-left:3px' /><col width='443px' /><col width='330px' /><col align=center width='50px' /></colgroup>");
    //        sb.Append("<tbody>");
    //        while (dr.Read())
    //        {
    //            sb.Append("<tr id='" + dr["t338_idplantilla"].ToString() + "' onclick='mmse(this)' ondblclick='mostrarMaestro(this)'");
    //            sb.Append(" nCR='" + dr["cod_une"].ToString() + "' amb='" + dr["t338_ambito"].ToString() + "' style='height:20px;'>");
    //            //sb.Append("<td>" + dr["des_ambito"].ToString() + "</td>");
    //            sb.Append("<td>");
    //            switch (dr["t338_ambito"].ToString())
    //            {
    //                case "D": sb.Append("<img src='../../../../images/imgIconoDepartamental.gif' style='width:16px;height:16px;border:0px' />"); break;
    //                case "P": sb.Append("<img src='../../../../images/imgIconoPersonal.gif' style='width:16px;height:16px;border:0px' />"); break;
    //                case "E": sb.Append("<img src='../../../../images/imgIconoEmpresarial.gif' style='width:16px;height:16px;border:0px' />"); break;
    //            }
    //            sb.Append("</td>");
    //            sb.Append("<td>" + HttpUtility.HtmlEncode(dr["t338_denominacion"].ToString()) + "</td>"); //Para mostrar caracteres como < y > que haya en el nombre.
    //            sb.Append("<td>" + dr["nom_une"].ToString() + "</td><td>");
    //            if ((int)dr["t338_estado"] == 1) sb.Append("<img src='../../../../Images/imgOk.gif'>");
    //            else sb.Append("<img src='../../../../Images/imgSeparador.gif'>");
    //            sb.Append("</td></tr>");
    //            i++;
    //        }
    //        dr.Close();
    //        dr.Dispose();
    //        sb.Append("</tbody>");
    //        sb.Append("</table>");
    //        if (bRecarga)
    //            sResul = sb.ToString();
    //        else
    //            div1.InnerHtml = sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores = Errores.mostrarError("Error al obtener el catálogo", ex);
    //    }
    //    return sResul;
    //}
    private string EliminarPlantilla(string slIDPlant)
    {
        string sResul = "", sIDPlant;
        try
        {
            //PlantProy objPlant = new PlantProy(int.Parse(sIDPlant));
            //objPlant.Eliminar();
            string[] lstPlant = Regex.Split(slIDPlant, "##");
            for (int i = 0; i < lstPlant.Length; i++)
            {
                sIDPlant = lstPlant[i].ToString();
                if (sIDPlant != "") PlantProy.Eliminar(int.Parse(sIDPlant));
            }
            sResul = "OK";

        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al eliminar la plantilla", ex);
        }

        return sResul;
    }
    private string Buscar(string sOrden, string sAscDesc, string sTipo, string sOrigen, string sNodo, 
                          string sEmp, string sDep, string sPer, bool bRecarga)
    {
        string sResul = "";
        bool bEmp = false, bDep = false, bPer = false;
        int iPromotor = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString());
        if (sNodo == "") sNodo = "-1";
        int iCR = int.Parse(sNodo);
        StringBuilder sb = new StringBuilder();

        try
        {
            if (sEmp == "1") bEmp = true;
            if (sDep == "1") bDep = true;
            if (sPer == "1") bPer = true;
            SqlDataReader dr = PlantProy.Catalogo(int.Parse(sOrden), int.Parse(sAscDesc), sTipo, iCR, iPromotor, sOrigen, bEmp, bDep, bPer);
            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 854px;'>");
            sb.Append("<colgroup><col style='width:27px;' /><col style='width:443px' /><col style='width:330px' /><col style='width:54px' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t338_idplantilla"].ToString() + "' onclick='mm(event)' ondblclick='mostrarMaestro(this)'");
                sb.Append(" nCR='" + dr["cod_une"].ToString() + "' amb='" + dr["t338_ambito"].ToString() + "' style='height:20px;'>");
                sb.Append("<td style='padding-left:3px'>");
                switch (dr["t338_ambito"].ToString())
                {
                    case "D": sb.Append("<img src='../../../../images/imgIconoDepartamental.gif' style='width:16px;height:16px;border:0px' />"); break;
                    case "P": sb.Append("<img src='../../../../images/imgIconoPersonal.gif' style='width:16px;height:16px;border:0px' />"); break;
                    case "E": sb.Append("<img src='../../../../images/imgIconoEmpresarial.gif' style='width:16px;height:16px;border:0px' />"); break;
                }
                sb.Append("</td>");
                sb.Append("<td>" + HttpUtility.HtmlEncode(dr["t338_denominacion"].ToString()) + "</td>"); //Para mostrar caracteres como < y > que haya en el nombre.
                sb.Append("<td>" + dr["nom_une"].ToString() + "</td><td>");
                if (int.Parse(dr["t338_estado"].ToString()) == 1)//Solo mostramos las plantillas en estado activo
                    sb.Append("<img src='../../../../Images/imgOk.gif' border='0' style='margin-left:20px;'>");
                sb.Append("</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            
            if (bRecarga)
                sResul = "OK@#@" + sb.ToString();
            else
                div1.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al ordenar el catálogo", ex);
        }
        return sResul;
    }
    private void cargarNodos(string sOrigen, string sNodo, string sEmp, string sDep, string sPer)
    {
        try
        {
            bool bSeleccionado = false;
            if (sNodo == "-1") sNodo = "";
            //Cargo la denominacion del label Nodo
            //this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            //this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            //Primero miro si solo tengo acceso a un nodo para en su caso ponerlo en el combo como seleccionado
            int iNumNodos = NODO.NumNodosAdministrables((int)Session["UsuarioActual"]);
            if (iNumNodos > 1 || iNumNodos == 0)
            {
                //Primero pongo fila vacía
                oLI = new ListItem("<Todos>", "-1");
                if (sNodo == "")
                {
                    oLI.Selected = true;
                    bSeleccionado = true;
                    //Buscar("1", "0", gsTipo, sOrigen, sNodo, "0", "0", "1", false);
                    Buscar("1", "0", gsTipo, sOrigen, sNodo, sEmp, sDep, sPer, false);
                }
                cboCR.Items.Add(oLI);
            }
            SqlDataReader dr = NODO.CatalogoAdministrables((int)Session["UsuarioActual"], true);
            while (dr.Read())
            {
                oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                if (!bSeleccionado)
                {
                    if (iNumNodos == 1)
                        sNodo = dr["t303_idnodo"].ToString();
                    if (sNodo == dr["t303_idnodo"].ToString())
                    {
                        oLI.Selected = true;
                        bSeleccionado = true;
                        //Buscar("1", "0", gsTipo, sOrigen, sNodo, "0", "0", "1",false);
                        Buscar("1", "0", gsTipo, sOrigen, sNodo, Request.QueryString["bE"], Request.QueryString["bD"], Request.QueryString["bP"], false);
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
    private string GrabarComo(string sDesPlant, string sAmbito, string sIdPlantOrigen, string sOrigen, string sTipo, string sNodo, 
                              string sEmp, string sDep, string sPer)
    {
        string sResul = "";
        int iPromotor = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), idPlantOrigen, idPlantDestino;
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        try
        {
            //Cargo los datos de la plantilla actual
            idPlantOrigen=int.Parse(sIdPlantOrigen);
            PlantProy miPlant = new PlantProy();
            miPlant.Obtener(idPlantOrigen);
            //Abro transaccion serializable
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);

            if (sAmbito == "") sAmbito = miPlant.ambito;
            if (sAmbito != "D") miPlant.codune = -1;
            idPlantDestino = PlantProy.Insertar(tr, miPlant.tipo, Utilidades.unescape(sDesPlant), 1, sAmbito, iPromotor, miPlant.codune, miPlant.obs);
            
            ITEMSPLANTILLA.Duplicar(tr, idPlantOrigen, idPlantDestino);

            Conexion.CommitTransaccion(tr);
            //Recargo el desglose
            //sResul = "OK@#@" + obtenerPlantillas(inodo, sorigen, true);
            sResul = Buscar("1", "0", sTipo, sOrigen, sNodo, sEmp, sDep, sPer, true);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar el desglose de la plantilla", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
