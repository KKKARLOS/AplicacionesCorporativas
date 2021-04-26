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
using EO.Web; 
using System.Collections.Generic;
using SUPER.Capa_Negocio;
//para unescape


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
private string _callbackResultado = null;
    public string strTablaHTMLTarea, strTablaHTMLHito;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            //Master.nBotonera = 8;
            Master.nBotonera = 23;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Desglose de plantilla";
            if (!(bool)Session["PLANT1024"])
            {
                Master.nResolucion = 1280;
            }
            if (!Page.IsPostBack)
            {
                try
                {
                    int iPlant = 0;
                    string sPlantAux = Request.QueryString["nIDPlant"];
                    if (sPlantAux != null)
                    {
                        Session["IDPlant"] = sPlantAux;
                    }
                    else if (Session["IDPlant"] != null)
                    {
                        sPlantAux = Session["IDPlant"].ToString();
                    }
                    string sTipo = Request.QueryString["sTipo"];

                    if (sTipo != null)
                    {
                        Session["TIPO_PLANT"] = sTipo;
                    }
                    else
                    {
                        if (Session["TIPO_PLANT"] != null) sTipo = Session["TIPO_PLANT"].ToString();
                        else Session["TIPO_PLANT"] = sTipo;
                    }
                    if (sTipo == "E")
                    {
                        this.lblTipo.Text = "Proyecto económico";
                    }
                    else
                    {
                        this.lblTipo.Text = "Proyecto técnico";
                    }

                    if (sPlantAux != null)
                    {
                        iPlant = int.Parse(sPlantAux);
                        this.hdnIDPlantilla.Text = sPlantAux;
                    }
                    sPlantAux = Request.QueryString["sDesPlant"];
                    if (sPlantAux != null)
                    {
                        this.txtDesPlantilla.Text = sPlantAux;
                    }

                    //Establezco la modificabilidad de la plantilla
                    PlantProy objPlant = new PlantProy();
                    objPlant.Obtener(int.Parse(this.hdnIDPlantilla.Text));
                    this.txtModificable.Text = flPlantillaModificable(objPlant.ambito);
                    switch (objPlant.ambito)
                    {
                        case "E":
                            this.txtAmbito.Text = "EMPRESARIAL";
                            break;
                        case "D":
                            this.txtAmbito.Text = "DEPARTAMENTAL";
                            break;
                        case "P":
                            this.txtAmbito.Text = "PERSONAL";
                            break;
                        default:
                            this.txtAmbito.Text = "DESCONOCIDO";
                            break;
                    }

                    ObtenerTareas(iPlant, sTipo);
                    ObtenerHitos(iPlant);
                    //Establezco el perfil del empleado para poder controlar desde el lado cliente el ambito que se usará al GrabarComo
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
                    Master.sErrores = Errores.mostrarError("Error al obtener el catálogo de tareas", ex);
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
        int iPos;
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                string strUrl = HistorialNavegacion.Leer();
                if (Request.QueryString["nIDPlant"].ToString() != "0")
                {
                    iPos = strUrl.IndexOf("?nIDPlant=0");
                    if (iPos != -1)
                    {
                        string sAux = strUrl.Substring(0, iPos);
                        sAux += "?nIDPlant=" + Request.QueryString["nIDPlant"].ToString();
                        sAux += strUrl.Substring(iPos + 11);
                        strUrl = sAux;
                    }
                }
                if (Request.QueryString["rCR"] != null)
                {
                    iPos = strUrl.IndexOf("?sTipo=");
                    if (iPos != -1)
                    {
                        strUrl = strUrl.Substring(0, iPos + 9);
                    }
                    strUrl += "&rCR=" + Request.QueryString["rCR"].ToString();
                    strUrl += "&bE=" + Request.QueryString["bE"].ToString();
                    strUrl += "&bD=" + Request.QueryString["bD"].ToString();
                    strUrl += "&bP=" + Request.QueryString["bP"].ToString();
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
        string sResultado = "", sCodPlant, sDatosItems, sDatosHitos;//, sAmbito, sDesPlant
        string[] aArgs = Regex.Split(eventArg, @"//");
        switch (aArgs[0])
        {
            case ("grabar"):
                sCodPlant = aArgs[1];
                sDatosItems = aArgs[2];
                sDatosHitos = aArgs[3];
                //codigo de plantilla + items plantilla, items hitos
                sResultado = @"grabar@#@";
                sResultado += Grabar(sCodPlant, sDatosItems, sDatosHitos);
                //sResultado += Grabar(sCad);
                break;
            //case ("grabarcomo"):
            //    sDesPlant = aArgs[1];
            //    if (sDesPlant == "") sDesPlant = "SIN NOMBRE";
            //    sAmbito = aArgs[2];
            //    sCodPlant = aArgs[3];
            //    sDatosItems = aArgs[4];
            //    sDatosHitos = aArgs[5];
            //    sResultado = @"grabarcomo@#@";
            //    sResultado += GrabarComo(sCodPlant, sAmbito, sDesPlant, sDatosItems, sDatosHitos);
            //    break;
            case ("plantilla"):
                sResultado += ObtenerTareas(int.Parse(aArgs[1]), aArgs[2]);
                break;
            case ("setResolucion"):
                sResultado = @"setResolucion@#@";
                sResultado += setResolucion();
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
    private string Grabar(string sIdPlant, string sCadena, string sHitosEspeciales)
    {/*En el parametro de entrada tenemos en primer lugar el codigo de la plantilla
    y luego una lista de elementos del tipo sEstado@#@sTipo@#@sDes@#@sCodigo@#@sOrden@#@sMargen@#@sFacturable@#@avance@#@obligaest##
    */
        string sCad, sTipo, sDesc, sResul = "", sEstado = "N", sCadenaBorrado = "", sTipoPlant, sAux;
        int iPos, iTarea = -1, iAux = -1, iCodigo = -1, iHito = -1, iMargen=0;
        short iOrden = 0, iOrdenAnt = 0;
        bool bFacturable, bAvance, bObliga;
        SqlConnection oConn=null;
        SqlTransaction tr=null;

        #region Control para comprobar que la estructura no ha variado desde que se ha leido.
        SqlConnection oConn2 = null;
        SqlTransaction tr2 = null;
        try
        {
            oConn2 = Conexion.Abrir();
            tr2 = Conexion.AbrirTransaccion(oConn2);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }

        SqlDataReader dr2 = PlantTarea.Catalogo(int.Parse(sIdPlant));
        ArrayList aEstrOrig = (ArrayList)Session["OrdenEstructuraPlant"];
        ArrayList aEstrActual = new ArrayList();
        string sTipo2, sIDItem2, sOrden2;

        while (dr2.Read())
        {

            sTipo2 = dr2["Tipo"].ToString();
            sIDItem2 = dr2["t339_iditems"].ToString();
            sOrden2 = dr2["orden"].ToString();

            string[] sEstr = new string[3] { sOrden2, sTipo2, sIDItem2 };
            aEstrActual.Add(sEstr);
        }
        dr2.Close();
        dr2.Dispose();

        string sMsgError = "Error@#@PSP ha detectado que durante su edición, la estructura ha variado por modificación de otro usuario.\n\nPulse \"Aceptar\" para recuperar la estructura actualizada, teniendo en cuenta que perderá los cambios realizados.\nPulsando \"Cancelar\" permanecerá en la pantalla actual sin realizar la grabación.@#@1";
        if (aEstrOrig.Count != aEstrActual.Count)
        {
            Conexion.CerrarTransaccion(tr2);
            Conexion.Cerrar(oConn2);
            return sMsgError;
        }
        else
        {
            for (int i = 0; i < aEstrOrig.Count; i++)
            {
                if (((string[])aEstrOrig[i])[0] != ((string[])aEstrActual[i])[0]
                    || ((string[])aEstrOrig[i])[1] != ((string[])aEstrActual[i])[1]
                    || ((string[])aEstrOrig[i])[2] != ((string[])aEstrActual[i])[2]
                    )
                {
                    Conexion.CerrarTransaccion(tr2);
                    Conexion.Cerrar(oConn2);
                    return sMsgError;
                }
            }
        }

        Conexion.CommitTransaccion(tr2);
        Conexion.Cerrar(oConn2);
        Session["OrdenEstructuraPlant"] = null;
        #endregion
        try
    {
        sCadenaBorrado = sIdPlant + @"//";
        //Abro transaccion
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

        if (lblTipo.Text == "Proyecto económico") sTipoPlant = "E";
        else
        {
            sTipoPlant = "T";
        }
        //Obtengo una cadena solo con la lista de filas a grabar
        //estado@#@tipo@#@descripcion@#@codPT@#@sCodigo@#@sOrden@#@sMargen@#@sFacturable  @#@avance@#@obligaest
        sCad = sCadena;
        if (sCad == "")
        {//Tenemos un desglose vacío. No hacemos nada
        }
        else
        {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
            string[] aTareas = Regex.Split(sCad, @"##");

            for (int i = 0; i < aTareas.Length - 1; i++)
            {
                sCad = aTareas[i];
                string[] aItems = Regex.Split(sCad, @"@#@");

                sEstado = aItems[0];
                sTipo = aItems[1];
                sDesc = Utilidades.unescape(aItems[2]);
                sAux = aItems[3];
                if (sAux != "") iCodigo = int.Parse(sAux);
                else iCodigo = -1;
                sAux = aItems[4];
                if (sAux != "") iOrdenAnt = short.Parse(sAux);
                else iOrdenAnt = 1;
                sCad = aItems[5];
                iPos = sCad.IndexOf(@"px");
                sAux = sCad.Substring(0, iPos);
                if (sAux != "") iMargen = int.Parse(sAux);
                else iMargen = 0;
                sCad = aItems[6];
                if (sCad == "T") bFacturable = true;
                else bFacturable = false;
                sCad = aItems[7];
                if (sCad == "T") bAvance = true;
                else bAvance = false;
                sCad = aItems[8];
                if (sCad == "T") bObliga = true;
                else bObliga = false;

                //Si no ha cambiado la linea pero el orden actual es distinto del original hay que updatear la linea para actualizar el orden
                if (iOrden != iOrdenAnt && sEstado=="N") sEstado = "U";
                if (sEstado != "D") iOrden++;

                if (sEstado == "D") sCadenaBorrado += sTipo + "##" + iCodigo + @"@#@";
                switch (sTipo)
                {
                    case "T":
                        iTarea = iCodigo;
                        break;
                    case "H":
                        iHito = iCodigo;
                        break;
                }
                //Si es una plantilla de proyecto técnico no hay que grabar linea de PT
                if ((sTipoPlant == "T") && (sTipo == "P")) sEstado = "N";
                switch (sEstado)
                {
                    case "U":
                        ITEMSPLANTILLA.Update(tr, iCodigo, sTipo, Utilidades.unescape(sDesc), (byte)iMargen, (short)iOrden,
                                              int.Parse(sIdPlant), bFacturable, bAvance, bObliga);
                        break;
                    case "I":
                        iAux = ITEMSPLANTILLA.Insert(tr, sTipo, Utilidades.unescape(sDesc), (byte)iMargen, (short)iOrden,
                                                     int.Parse(sIdPlant), bFacturable, bAvance, bObliga);
                        break;
                }//switch (sEstado)
            }//for
        }
        //Elimino las filas borradas 
        BorrarDesglose(tr, sCadenaBorrado, sTipoPlant);

        //Grabo los hitos especiales
        //sEstado+"##"+Utilidades.escape(sDes)+"##"+sCodigo+"##"+sOrden+"//"
        sCad = sHitosEspeciales;
        if (sCad == "")
        {//Tenemos un desglose vacío. No hacemos nada
        }
        else
        {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
            string[] aTareas = Regex.Split(sCad, @"@#@");
            iOrden = 0;
            for (int i = 0; i < aTareas.Length - 1; i++)
            {
                sCad = aTareas[i];
                if (sCad != "")
                {
                    string[] aElems = Regex.Split(sCad, @"##");
                    sEstado = aElems[0];
                    sDesc = Utilidades.unescape(aElems[1]);
                    iHito = int.Parse(aElems[2]);
                    iOrdenAnt = short.Parse(aElems[3]);

                    if (sEstado != "D") iOrden++;
                    //Si no ha cambiado la linea pero el orden actual es distinto del original hay que updatear la linea para actualizar el orden
                    if (iOrden != iOrdenAnt && sEstado == "N") sEstado = "U";
                    switch (sEstado)
                    {
                        case "N":
                            break;
                        case "D":
                            HITOE_PLANT.Delete(tr, iHito);
                            break;
                        case "U":
                            HITOE_PLANT.Update(tr, iHito, sDesc, null, null, iOrden);
                            break;
                        case "I":
                            iAux = HITOE_PLANT.Insert(tr, sDesc, "", true, iOrden, int.Parse(sIdPlant));
                            break;
                    }//switch (sEstado)
                }
            }
        }
        //Cierro transaccion
        Conexion.CommitTransaccion(tr);

        //Recargo el desglose
        ObtenerTareas(int.Parse(sIdPlant), sTipoPlant);
        ObtenerHitos(int.Parse(sIdPlant));
        //sResul = "OK@#@" + sIdPlant;
        sResul = "OK@#@" + strTablaHTMLTarea + "@#@" + strTablaHTMLHito;
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
    //private string GrabarComo(string sIdPlant, string sAmbito, string sDesPlant, string sCadena, string sHitosEspeciales)
    //{/*En el primer parametro de entrada tenemos la descripción de la nueva plantilla
    //   y en el segundo parametro en primer lugar el codigo de la plantilla origen
    //   y luego una lista de elementos del tipo grabar@#@estado@#@tipo@#@descripcion@#@sEsHijo@#@sCodigo@#@sOrden@#@sMargen@#@sFacturable @#@avance@#@obliga
    //  */
    //    string sCad, sTipo, sDesc, sResul = "", sTipoPlant, sEstado, sAux, sIdTareaPL;
    //    int iAux = -1, iEstado = 0, idPlant = -1, iMargen = 0, iHito, iHitoNuevo;
    //    int iPromotor = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), iPosAux;
    //    short iOrden = 0;
    //    bool bFacturable, bAvance, bObliga;
    //    SqlConnection oConn=null;
    //    SqlTransaction tr = null;
    //    try
    //    {
    //        //Cargo los datos de la plantilla actual
    //        PlantProy miPlant = new PlantProy();
    //        miPlant.Obtener(int.Parse(sIdPlant));
    //        if (miPlant.activo) iEstado = 1;
    //        else iEstado = 0;
    //        //Abro transaccion
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccion(oConn);

    //        if (sAmbito == "") sAmbito = miPlant.ambito;
    //        if (sAmbito != "D") miPlant.codune = -1;
    //        idPlant = PlantProy.Insertar(tr,miPlant.tipo,Utilidades.unescape(sDesPlant),iEstado,sAmbito,iPromotor,miPlant.codune,miPlant.obs);
    //        sIdPlant = idPlant.ToString();

    //        if (lblTipo.Text == "Proyecto económico") sTipoPlant = "E";
    //        else sTipoPlant = "T";
    //        //Obtengo una cadena solo con la lista de filas a grabar
    //        if (sCadena == "")
    //        {//Tenemos un desglose vacío. No hacemos nada
    //        }
    //        else
    //        {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
    //            string[] aTareas = Regex.Split(sCadena, @"##");
    //            for (int i = 0; i < aTareas.Length - 1; i++)
    //            {
    //                //estado@#@tipo@#@descripcion@#@sCodigo@#@sOrden@#@sMargen@#@avance@#@obliga
    //                sCad = aTareas[i];
    //                if (sCad != "")
    //                {
    //                    string[] aElem = Regex.Split(sCad, @"@#@");
    //                    sEstado = aElem[0];
    //                    if (sEstado != "D")
    //                    {
    //                        sTipo = aElem[1];
    //                        sDesc = aElem[2];
    //                        sCad = aElem[5];
    //                        iPosAux = sCad.IndexOf(@"px");
    //                        sAux = sCad.Substring(0, iPosAux);
    //                        iMargen = int.Parse(sAux);
    //                        sCad = aElem[6];
    //                        if (sCad == "T") bFacturable = true;
    //                        else bFacturable = false;
    //                        sCad = aElem[7];
    //                        if (sCad == "T") bAvance = true;
    //                        else bAvance = false;
    //                        sCad = aElem[8];
    //                        if (sCad == "T") bObliga = true;
    //                        else bObliga = false;
    //                        iOrden++;

    //                        if ((sTipoPlant == "T") && (sTipo == "P"))
    //                            iAux = 0;
    //                        else
    //                            iAux = ITEMSPLANTILLA.Insert(tr, sTipo, Utilidades.unescape(sDesc), (byte)iMargen, iOrden,
    //                                                         int.Parse(sIdPlant), bFacturable, bAvance, bObliga);
    //                    }//if (sEstado != "D")
    //                }
    //            }//FOR
    //        }//if (sCad == "")
    //        //Grabo los hitos especiales
    //        //sEstado+"##"+Utilidades.escape(sDes)+"##"+sCodigo+"##"+sOrden+"//"
    //        sCad = sHitosEspeciales;
    //        if (sCad != "")
    //        {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
    //            string[] aHitos = Regex.Split(sCad, @"@#@");
    //            iOrden = 0;
    //            for (int i = 0; i < aHitos.Length - 1; i++)
    //            {
    //                sCad = aHitos[i];
    //                if (sCad != "")
    //                {
    //                    string[] aElems = Regex.Split(sCad, @"##");
    //                    sEstado = aElems[0];
    //                    sDesc = Utilidades.unescape(aElems[1]);
    //                    iHito = int.Parse(aElems[2]);
    //                    if (sEstado != "D") 
    //                    {
    //                        iOrden++;
    //                        iHitoNuevo = HITOE_PLANT.Insert(tr, sDesc, "", true, iOrden, int.Parse(sIdPlant));
    //                        //recorro las tareas del hito original para copiarselas al nuevo
    //                        sCad = HITOE_PLANT.fgListaTareasPlantilla(tr, iHito);
    //                        string[] aElems2 = Regex.Split(sCad, @"##");
    //                        for (int j = 0; j < aElems2.Length; j++)
    //                        {
    //                            sIdTareaPL = aElems2[j];
    //                            if (sIdTareaPL != "")
    //                            {
    //                                HITOE_PLANT_TAREA.Insert(tr,iHitoNuevo,int.Parse(sIdTareaPL));
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        //Descargo los cambios en la BBDD
    //        Conexion.CommitTransaccion(tr);

    //        //Recargo el desglose
    //        ObtenerTareas(int.Parse(sIdPlant), sTipoPlant);
    //        ObtenerHitos(int.Parse(sIdPlant));
    //        sResul = "OK@#@" + sIdPlant+ "@#@" + strTablaHTMLTarea + "@#@" + strTablaHTMLHito;
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al grabar el desglose de la plantilla", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}
    private void BorrarDesglose(SqlTransaction tr, string sCadena,string sTipoPlant)
    {/*En el parametro sCadena tenemos en primer lugar el codigo de la plantilla
       y luego una lista de elementos del tipo tipo##codigo@#@
       En el parametro sTipoPlant tenemos el tipo de plantilla, ya que si es de Proyecto Técnico no hay que borrar
       la línea inventada de tipo proyecto técnico
      */
        string sIdPlant, sCad, sTipo;//, sResul = ""
        int iPos, iCodigo = -1,iLineaInicial;
        //try
        //{
            //Recojo el código de plantilla
            iPos = sCadena.IndexOf(@"//");
            sIdPlant = sCadena.Substring(0, iPos);
            //Obtengo una cadena solo con la lista de filas a borrar
            //tipo@#@codigo##
            sCad = sCadena.Substring(iPos + 2);
            if (sCad == "")
            {//Tenemos un desglose vacío. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para borrar cada elemento
                string[] aTareas = Regex.Split(sCad, @"@#@");
                //if (sTipoPlant == "T") iLineaInicial = 1;
                //else 
                    iLineaInicial = 0;
                for (int i = iLineaInicial; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    string[] aT2 = Regex.Split(sCad, @"##");
                    sTipo = aT2[0];
                    sCad = aT2[1];
                    iCodigo = int.Parse(sCad.Substring(0));
                    PlantTarea.Borrar(tr, sTipo, iCodigo);
                }//for i
            }
            //sResul = "OK@#@" + sIdPlant;
        //}
        //catch (Exception ex)
        //{
        //    Conexion.CerrarTransaccion(tr);
        //    sResul = "Error@#@" + Errores.mostrarError("Error al borrar lineas del desglose de la plantilla", ex);
        //}
        //return sResul;
    }
    private string ObtenerTareas(int iPlant, string sTipoPlant)
    {/* Devuelve el código HTML del catalogo de tareas de la plantilla que se pasa por parámetro
      * En sTipoPlant nos indica si es un Proyecto Tecnico o un Proyecto Economico
      */
        StringBuilder sb = new StringBuilder();
        string sIdTarea, sDesTipo = "", sDesc, sTipo, sTarea, sOrden, sMargen, sObligaEst;//,sAvance
        int iId = -1;
        bool bModificable,bFacturable,bAux, bAvance;
        //Para comprobar posteriormente modificaciones concurrentes por parte de varios usuarios en la plantilla
        ArrayList aEstr = new ArrayList();
        try
        {
            if (this.txtModificable.Text == "T") bModificable = true;
            else bModificable = false;

            SqlDataReader dr = PlantTarea.Catalogo(iPlant);

            sb.Append("<table id='tblDatos' class='texto' style='WIDTH:700px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:600px' /><col style='width:50px' /><col style='width:50px' /></colgroup>");
            sb.Append("<tbody>");
            //Si es una plantilla de proyecto técnico, inserto una línea inventada de tipo P.T.
            if (sTipoPlant == "T")
            {
                iId++;
                sIdTarea = iId.ToString();
                sb.Append("<tr id='" + sIdTarea + "' tipo='P.T.' sAv='F' sOb='F' est='N' cT='' ord='0' p1='0' p2='0' style='display:none;height:20px;'>");
                sb.Append("<td><img src='../../../../Images/imgProyTecOff.gif' border='0' title='P.T. grabado'>");
                sb.Append("<input type='text' name='txtD" + sIdTarea + "' id='Desc" + sIdTarea + "' class='txtL' style='width:400px;margin-left:3px' MaxLength='50' value=''></td>");
                //Columna 2 y 3
                sb.Append("<td></td><td style='text-align:center;'></td></tr>");
            }

            while (dr.Read())
            {
                //sIdTarea = dr["idTarea"].ToString();
                iId++;
                sIdTarea = iId.ToString();
                sTipo = dr["Tipo"].ToString();
                switch (sTipo)
                {
                    case "P": sDesTipo = "P.T."; break;
                    case "F": sDesTipo = "FASE"; break;
                    case "A": sDesTipo = "ACTI."; break;
                    case "T": sDesTipo = "TAREA"; break;
                    case "H": sDesTipo = "HITO"; break;
                }
                sDesc = dr["Nombre"].ToString();
                sTarea = dr["t339_iditems"].ToString();
                sOrden = dr["orden"].ToString();
                //Para el control de modificaciones concurrentes
                string[] sEstr = new string[3] { sOrden, sTipo, sTarea };
                aEstr.Add(sEstr);

                sMargen = dr["margen"].ToString();
                bFacturable = bool.Parse(dr["t339_facturable"].ToString());
                bAvance = bool.Parse(dr["avance"].ToString());
                //bAux = bool.Parse(dr["avance"].ToString());
                //if (bAux) sAvance = "T";
                //else sAvance = "F";
                bAux = bool.Parse(dr["obliga"].ToString());
                if (bAux) sObligaEst = "T";
                else sObligaEst = "F";
                //sb.Append("<tr id='" + sIdTarea + "' sAv='" + sAvance + "' sOb='" + sObligaEst + "' tipo='" + sDesTipo + "' est='N' cT='" + sTarea + "'");
                sb.Append("<tr id='" + sIdTarea + "' sOb='" + sObligaEst + "' tipo='" + sDesTipo + "' est='N' cT='" + sTarea + "'");
                sb.Append(" style='height:20px;' ord='" + sOrden + "' p1='0' p2='0' onclick='mm(event)' ");

                if (this.txtAmbito.Text == "DEPARTAMENTAL" || sDesTipo == "P.T.")
                    sb.Append(" ondblclick='mostrarDetalle()'");
                if (bModificable)
                    sb.Append(" onkeydown='accionLinea(event)'");
                //Columna 1
                string sTitle = (sDesTipo == "TAREA") ? sDesc : "";
                sb.Append("><td title='" + sTitle + "'>");
                switch (sDesTipo)
                {
                    case "P.T.":
                        sb.Append("<img src='../../../../Images/imgProyTecOff.gif' border='0' title='P.T. grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle;'>");
                        break;
                    case "FASE":
                        sb.Append("<img src='../../../../Images/imgFaseOff.gif' border='0' title='Fase grabada' style='vertical-align:middle;margin-left:20px'>");
                        break;
                    case "ACTI.":
                        sb.Append("<img src='../../../../Images/imgActividadOff.gif' border='0' title='Actividad grabada' style='vertical-align:middle;margin-left:" + sMargen + "px'>");
                        break;
                    case "TAREA":
                        if (this.txtAmbito.Text == "DEPARTAMENTAL")
                            sb.Append("<img src='../../../../Images/imgTareaOff.gif' border='0' title='Tarea grabada' style='CURSOR: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle;margin-left:" + sMargen + "px'>");
                        else
                            sb.Append("<img src='../../../../Images/imgTareaOff.gif' border='0' title='Tarea grabada' style='vertical-align:middle;margin-left:" + sMargen + "px'>");
                        break;
                    case "HITO":
                        //sb.Append("<img src='../../../../Images/imgHitoOff.gif' border='0' title='Hito grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur);vertical-align:middle;margin-left:" + sMargen + "px'>");
                        sb.Append("<img src='../../../../Images/imgHitoOff.gif' border='0' title='Hito grabado' style='vertical-align:middle;margin-left:" + sMargen + "px'>");
                        break;
                }
                //sb.Append("</td>");
                //Columna 2
                //sb.Append("<input type='text' name='txtD" + sIdTarea + "' id='Desc" + sIdTarea + "' class='txtL' style='width:400;margin-left:" + sMargen + "px' MaxLength='50' value='" + sDesc + "'");
                string sML = (sDesTipo == "TAREA") ? "100" : "50";
                sb.Append("<input type='text' name='txtD" + sIdTarea + "' id='Desc" + sIdTarea + "' class='txtL' style='width:465px;margin-left:5px;' MaxLength="+ sML + " value='" + sDesc + "'");
                if (bModificable)
                {
                    sb.Append(" onfocus='this.select()' onkeydown='modificarNombreTarea(event)'>");
                }
                else
                {
                    sb.Append(" readonly=true>");
                }
                sb.Append("</td>");
                //Columna 3
                if (sTipo == "T")
                {
                    if (bModificable)
                    {
                        if (bFacturable) sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' checked='true' onclick='modificarItem(this.parentNode.parentNode.id);'></td>");
                        else sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' onclick='modificarItem(this.parentNode.parentNode.id);'></td>");

                        if (bAvance) sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' checked='true' onclick='modificarItem(this.parentNode.parentNode.id);'></td></tr>");
                        else sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' onclick='modificarItem(this.parentNode.parentNode.id);'></td></tr>");
                    }
                    else
                    {
                        if (bFacturable) sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' checked='true'></td>");
                        else sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla'></td>");

                        if (bAvance) sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla' checked='true'></td></tr>");
                        else sb.Append("<td><input type='checkbox' style='width:15px' class='checkTabla'></td></tr>");
                    }
                }
                else
                {
                    sb.Append("<td>&nbsp;</td><td>&nbsp;</td></tr>");
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            //Para el control de modificaciones concurrentes
            Session["OrdenEstructuraPlant"] = aEstr;

            this.strTablaHTMLTarea = sb.ToString();
            return "plantilla@#@OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener las tareas", ex);
            return sb.ToString();
        }
    }
    private string ObtenerHitos(int iPlant)
    {// Devuelve el código HTML del catalogo de hitos de la plantilla que se pasa por parámetro
        StringBuilder sb = new StringBuilder();
        string sIdTarea, sDesc, sHito, sOrden, sResul;
        int iId = 0;
        try
        {
            sb.Append("<table id='tblDatos2' class='texto' style='width:470px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:20px' /><col style='width:450px' /></colgroup>");
            sb.Append("<tbody>");
            if (iPlant > 0)
            {
                SqlDataReader dr = HITOE_PLANT.CatalogoHitos(iPlant);
                while (dr.Read())
                {
                    sIdTarea = iId.ToString();
                    iId++;
                    sDesc = dr["t369_deshito"].ToString();
                    sHito = dr["t369_idhito"].ToString();
                    sOrden = dr["t369_orden"].ToString();

                    sb.Append("<tr id='" + sIdTarea + "' style='height:20px;' est='N' ord='" + sOrden + "' codH='" + sHito + "'");
                    sb.Append(" onclick='mm(event)' onkeydown='accionLineaHito(event)'>");

                    //Columna 1
                    sb.Append("<td ondblclick='mostrarDetalleHito()' style='vertical-align:middle;'>");
                    sb.Append("<img src='../../../../Images/imgHitoOff.gif' border='0' title='Hito grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle'></td>");
                    //Columna 2
                    sb.Append("<td><input type='text' class='txtL' style='width:440px;' MaxLength='50' value='" + sDesc + "' onfocus='this.select()' onkeydown='modificarNombreHito(event)' >");
                    sb.Append("</td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = sb.ToString();
            this.strTablaHTMLHito = sResul;
            return "@#@" + sResul;
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener los hitos especiales", ex);
            return "error@#@Error al obtener los hitos especiales " + ex.Message;
        }
    }
    private string flPlantillaModificable(string sAmbito)
    {
        //	Si la plantilla es Empresarial solo será modificable si el usuario conectado tiene perfil de Administrador
        //	Si la plantilla es Departamental solo será modificable si el usuario conectado tiene perfil de Oficina Técnica o superior
        //	Si la plantilla es Personal siempre es modificable (se supone que un usuario solo ve las plantillas personales que son suyas)
        string sResul = "F";
        try
        {
            switch (sAmbito)
            {
                case "E"://empresarial
                    if (User.IsInRole("A")) sResul = "T";
                    break;
                case "D"://departamental
                    if (User.IsInRole("A")) sResul = "T";
                    else
                    {
                        if (User.IsInRole("RSN") || User.IsInRole("DSN") || User.IsInRole("ISN") || User.IsInRole("RN")
                            || User.IsInRole("DN") || User.IsInRole("CN") || User.IsInRole("IN") || User.IsInRole("OT"))
                        {
                            sResul = "T";
                        }
                    }
                    break;
                case "P"://privada
                    sResul = "T";
                    break;
                case ""://no tiene ambito -> vamos a crear una plantilla nueva
                    sResul = "T";
                    break;
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al establecer la modificabilidad de la plantilla", ex);
        }

        return sResul;
    }
    private string setResolucion()
    {
        try
        {
            Session["PLANT1024"] = !(bool)Session["PLANT1024"];

            USUARIO.UpdateResolucion(10, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["PLANT1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }

}
