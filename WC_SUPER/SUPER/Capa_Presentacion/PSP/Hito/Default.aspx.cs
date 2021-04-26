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


using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, sTipoHito, strTablaHitos, strTablaTareas, sOrigen;
    public int nIdPE, nIdHito;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            sErrores = "";
            strTablaHitos = "";
            strTablaTareas = "";
            sOrigen = "";

            sTipoHito = Utilidades.decodpar(Request.QueryString["th"].ToString());//sTipoHito
            nIdHito = int.Parse(Utilidades.decodpar(Request.QueryString["h"].ToString()));//nIdHito
            nIdPE = int.Parse(quitaPuntos(Utilidades.decodpar(Request.QueryString["pe"].ToString())));//nPE
            try
            {
                Utilidades.SetEventosFecha(this.txtValFecha);

                ObtenerDatosHito();
                ObtenerTareas(sTipoHito, nIdHito);
                //ObtenerOtrosHitos(nIdPE);
                //Inserto el control calendario para la fecha del hito
                //if (sTipoHito == "HF")
                if (Request.QueryString["o"] != null)//origen
                {
                    sOrigen = Utilidades.decodpar(Request.QueryString["o"].ToString());
                    this.cboModo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del hito", ex);
            }
            //try
            //{
            //    string strTabla = ObtenerDocumentos(nIdHito.ToString(), sTipoHito);
            //    string[] aTabla = Regex.Split(strTabla, "@#@");
            //    if (aTabla[0] == "OK") divCatalogoDoc.InnerHtml = aTabla[1];
            //}
            //catch (Exception ex)
            //{
            //    sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
            //}
            this.hdnAcceso.Text = Utilidades.decodpar(Request.QueryString["pm"].ToString());//Permiso
            if (this.hdnAcceso.Text == "R")
            {
                ModoLectura.Poner(this.Controls);
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
        string sResultado = "", sCad;
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("documentos"):
                //sCad = ObtenerDocumentos(aArgs[1], aArgs[2]);
                //if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                //else
                //{
                //    sResultado += "OK@#@" + sCad;
                //}
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), sModoAcceso, sEstadoProyecto);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1], aArgs[2]);
                break;
            case ("tareas"):
                sResultado += ObtenerTodasTareas(aArgs[1]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://OTROS HITOS
                        sCad = ObtenerOtrosHitos(int.Parse(aArgs[2]), int.Parse(aArgs[3]));
                        if (sCad.IndexOf("Error@#@") >= 0)
                            sResultado += sCad;
                        else
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 2://DOCUMENTACION
                        //sCad = ObtenerDocumentos(aArgs[3], aArgs[4]);
                        sCad = Utilidades.ObtenerDocumentos(aArgs[4], int.Parse(aArgs[3]), aArgs[5], aArgs[6]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else
                        {
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        }
                        break;
                }
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
    private void ObtenerDatosHito()
    {
        try
        {
            HITOPSP o = HITOPSP.Obtener(sTipoHito, nIdHito);

            txtIdHito.Text = o.idhito.ToString();
            txtDesHito.Text = o.deshito;
            txtDescripcion.Text = o.deshitolong;
            cboModo.Text = o.modo;
            cboEstado.Text = o.estado.ToString();
            txtAgrHito.Text = flAgregacion(sTipoHito, o.margen);
            hdnOrden.Text = o.orden.ToString();
            if (o.hitoPE)
            {
                chkPE.Checked = true;
                cboModo.Text = "0";
            }
            else
            {
                chkPE.Checked = false;
            }
            //if (o.hitoPE) cboModo.Text = "3";
            //else cboModo.Text = o.modo;

            if (o.alerta) chkAlerta.Checked = true;
            if (o.ciclico) chkCiclico.Checked = true;
            if (sTipoHito == "HF")
            {
                if (o.fecha.Year > 1900) txtValFecha.Text = o.fecha.ToShortDateString();
            }
            else txtValFecha.Text = "";
            //o.cod_une = nIdCR;
            o.num_proyecto = nIdPE;
            //Estado del proyecto económico
            this.hdnEstProy.Value = o.t301_estado;
            //Modo de acceso
            this.hdnModoAcceso.Value = PROYECTOSUBNODO.getAcceso(null, o.t305_idproyectosubnodo, int.Parse(Session["UsuarioActual"].ToString()));

        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos del hito", ex);
        }
    }
    protected string ObtenerTareas(string sTipo, int iCodHito)
    {
        //Relacion de tareas asignadas al hito
        string sResul = "", sCad, sFecha, sCodTarea;
        double fPrev = 0, fCons = 0, fAvance = 0;
        int i = 0;
        bool bAvanceAutomatico;

        StringBuilder sbuilder = new StringBuilder();
        try
        {
            sbuilder.Append("<table id='tblTareas' class='texto MANO' style='width: 800px;text-align:left;'>");
            //............................ idTarea........... Desc Tarea..........ETPL.......................................separador............FIPL....................FFPL..............ETPR......................................separador...........FFPR................CONSUMO.....................................AVANCE
            sbuilder.Append("<colgroup><col style='width:40px;' /><col style='width:340px;' /><col style='width:50px;'/><col style='width:10px' /><col style='width:60px' /><col style='width:60px' /><col style='width:50px' /><col style='width:10px' /><col style='width:60px' /><col style='width:50px' /><col style='width:50px' /></colgroup>");
            sbuilder.Append("<tbody>");
            if (sTipoHito != "HF")
            {
                SqlDataReader dr = HITOPSP.CatalogoTareas(iCodHito);

                //Tarea 50 Desc 350 ETPL 40+10 FIPL 60 FFPL 60 ETPR 40+10 FFPR 60 CONSUMO 50 AVANCE 50
                while (dr.Read())
                {
                    sCodTarea = dr["t332_idtarea"].ToString();
                    sbuilder.Append("<tr id='" + sCodTarea + "' est='N' onclick='mm(event)' style='height:16px;'>");
                    sbuilder.Append("<td style='text-align:right;padding-rigth:5px;'>");
                    sbuilder.Append(int.Parse(sCodTarea).ToString());
                    sbuilder.Append("</td><td style='padding-left:5px;'>");
                    sbuilder.Append("<NOBR class='NBR W360'>" + dr["t332_destarea"].ToString() + "</NOBR>");
                    sbuilder.Append("</td>");

                    if (dr["t332_etpl"] != DBNull.Value)
                    {
                        sbuilder.Append("<td style='text-align:right'>");
                        sbuilder.Append(double.Parse(dr["t332_etpl"].ToString()).ToString("N"));
                        sbuilder.Append("</td>");
                    }
                    else
                        sbuilder.Append("<td style='text-align:right'></td>");

                    sbuilder.Append("<td >&nbsp;</td>");

                    sFecha = dr["t332_fipl"].ToString();
                    if (sFecha != "")
                        sFecha = DateTime.Parse(dr["t332_fipl"].ToString()).ToShortDateString();

                    sbuilder.Append("<td>");
                    sbuilder.Append(sFecha);
                    sbuilder.Append("</td>");

                    sFecha = dr["t332_ffpl"].ToString();
                    if (sFecha != "")
                        sFecha = DateTime.Parse(dr["t332_ffpl"].ToString()).ToShortDateString();

                    sbuilder.Append("<td>" + sFecha + "</td>");

                    if (dr["t332_etpr"] != DBNull.Value)
                    {
                        sbuilder.Append("<td style='text-align:right'>");
                        sbuilder.Append(double.Parse(dr["t332_etpr"].ToString()).ToString("N"));
                        sbuilder.Append("</td>");
                    }
                    else
                        sbuilder.Append("<td></td>");

                    sbuilder.Append("<td>&nbsp;</td>");

                    sFecha = dr["t332_ffpr"].ToString();
                    if (sFecha != "")
                        sFecha = DateTime.Parse(dr["t332_ffpr"].ToString()).ToShortDateString();

                    sbuilder.Append("<td>");
                    sbuilder.Append(sFecha);
                    sbuilder.Append("</td><td style='text-align:right'>");
                    sbuilder.Append(double.Parse(dr["consumo"].ToString()).ToString("N"));
                    sbuilder.Append("</td>");

                    //sbuilder.Append("<td>&nbsp;</td>");
                    //%Avance
                    bAvanceAutomatico = (bool)dr["t332_avanceauto"];
                    if (!bAvanceAutomatico)
                    {
                        fAvance = double.Parse(dr["t332_AVANCE"].ToString());
                    }
                    else
                    {
                        fPrev = double.Parse(dr["t332_etpr"].ToString());
                        fCons = double.Parse(dr["Consumo"].ToString());
                        if (fPrev == 0) fAvance = 0;
                        else fAvance = (fCons * 100) / fPrev;
                    }
                    sCad = fAvance.ToString("N");
                    sbuilder.Append("<td style='text-align:right'>");
                    sbuilder.Append(sCad);
                    sbuilder.Append("</td></tr>");
                    i++;
                }
                dr.Close(); dr.Dispose();
            }
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table>");
            strTablaTareas = sbuilder.ToString();
            sResul = "OK@#@" + strTablaTareas;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de tareas.", ex);
        }

        return sResul;
    }
    private string ObtenerOtrosHitos(int nIdPE, int iCodHito)
    {
        //Relacion de hitos asignados al proyecto economico
        string sResul = "";
        StringBuilder sbuilder = new StringBuilder();
        try
        {
            SqlDataReader dr = HITOPSP.CatalogoHitos(nIdPE);

            sbuilder.Append("<table id='tblHitos' class='texto' style='width:450px;'>");
            sbuilder.Append("<colgroup><col style='width:400px;' /><col style='width:50px' /></colgroup>");
            sbuilder.Append("<tbody>");
            while (dr.Read())
            {
                //if (dr["idhito"].ToString() == nIdHito.ToString()) continue;
                if (dr["idhito"].ToString() == iCodHito.ToString()) continue;
                sbuilder.Append("<tr id='" + dr["idhito"].ToString() + "' style='height:16px'>");
                sbuilder.Append("<td style='padding-left:5px;'>" + dr["deshito"].ToString() + "</td>");
                string sCad = dr["estado"].ToString();
                string sEstado = "";
                switch (sCad)
                {
                    case "L": sEstado = "Latente"; break;
                    case "C": sEstado = "Cumplido"; break;
                    case "N": sEstado = "Notificado"; break;
                    //case "F": sEstado = "Finalizado"; break;
                    case "F": sEstado = "Inactivo"; break;
                }
                sbuilder.Append("<td>" + sEstado + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table>");
            strTablaHitos = sbuilder.ToString();
            sResul = strTablaHitos;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de hitos del proyecto económico.", ex);
        }

        return sResul;
    }
    protected string Grabar(string strDatosHito, string sDatosBorrado, string sTareas)
    {
        string sResul = "", sTipoHito, sDesHito, sDesHitoLong, sFecha, sEstado, sAlerta, sCiclico, sAccion, sTipoLinea, sCad, sCodTarea, sHitoPE;
        string[] aDatosHito;
        int iCodHito, iCodPE, iMargen, iCodHitoOriginal;
        short iOrden;//iCodCR
        bool bAlerta = false, bCiclico = false, bHitoPE;
        DateTime dtFecha = DateTime.Now;
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        try
        {
            aDatosHito = Regex.Split(strDatosHito, "##");
            iCodHito = int.Parse(aDatosHito[1]);
            //iCodCR = short.Parse(aDatosHito[2]);
            iCodPE = int.Parse(aDatosHito[3]);
            sDesHito = Utilidades.unescape(aDatosHito[4]);
            sDesHitoLong = Utilidades.unescape(aDatosHito[5]);
            sFecha = aDatosHito[6];
            if (sFecha != "") dtFecha = DateTime.Parse(sFecha);
            iOrden = short.Parse(aDatosHito[11]);

            //Al grabar un hito puede ocurrir que se haya cambiado el tipo de hito lo que implica que antes de grabar
            //hay que borrar de una tabla e insertar en otra
            if (sDatosBorrado != "")
            {
                #region Borrado
                string[] aDatosBorrado = Regex.Split(sDatosBorrado, "##");
                sAccion = aDatosBorrado[0];
                if (sAccion == "borrar")
                {
                    sTipoHito = aDatosBorrado[1];
                    iCodHito = int.Parse(aDatosBorrado[2]);
                    iCodHitoOriginal = int.Parse(aDatosBorrado[2]);
                    //Se realiza primero la inserción del nuevo hito y por último el borrado
                    //del viejo, para traspasar la documentación que pudiera tener.

                    //Inserta el hito en la nueva tabla
                    switch (sTipoHito)
                    {
                        case "HF":
                            //iCodHito = EstrProy.InsertarHito(tr, sDesHito, 0, iOrden);
                            iCodHito = EstrProy.InsertarHito(tr, sDesHito, 1, iOrden, iCodPE);
                            break;
                        case "HT":
                        case "HM":
                            //iCodHito = EstrProy.InsertarHitoPE(tr, iCodCR, iCodPE, sDesHito, sFecha, iOrden);
                            iCodHito = EstrProy.InsertarHitoPE(tr, iCodPE, sDesHito, sFecha, iOrden, sDesHitoLong);
                            break;
                    }

                    SqlDataReader dr;
                    //Selecciono los documentos de un tipo de hito, para luego insertarlos en el otro tipo de hitos.
                    if (sTipoHito == "HF")
                        dr = DOCUHE.Catalogo3(iCodHitoOriginal);
                    else
                        dr = DOCUH.Catalogo3(iCodHitoOriginal);

                    int nResul;
                    while (dr.Read())
                    {
                        long? idCS=null;//Id del documento en el Content-Server
                        if (dr["t2_iddocumento"].ToString() != "")
                            idCS = long.Parse(dr["t2_iddocumento"].ToString());
                        if (sTipoHito == "HF")
                            nResul = DOCUH.Insert(tr, iCodHito, dr["t367_descripcion"].ToString(), dr["t367_weblink"].ToString(),
                                                  dr["t367_nombrearchivo"].ToString(), idCS, (bool)dr["t367_privado"], 
                                                  (bool)dr["t367_modolectura"], (bool)dr["t367_tipogestion"], 
                                                  int.Parse(Session["UsuarioActual"].ToString()));
                        else
                            nResul = DOCUHE.Insert(tr, iCodHito, dr["t366_descripcion"].ToString(), dr["t366_weblink"].ToString(),
                                                   dr["t366_nombrearchivo"].ToString(), idCS, (bool)dr["t366_privado"], 
                                                   (bool)dr["t366_modolectura"], (bool)dr["t366_tipogestion"], 
                                                   int.Parse(Session["UsuarioActual"].ToString()));
                    }
                    dr.Close();
                    dr.Dispose();

                    //Borro el hito de la tabla en la que estaba
                    HITOPSP.Delete(tr, sTipoHito, iCodHitoOriginal);
                }
                #endregion
            }

            sTipoHito = aDatosHito[0];
            sEstado = aDatosHito[7];
            sHitoPE = aDatosHito[8];
            if (sHitoPE == "T") bHitoPE = true;
            else bHitoPE = false;
            sAlerta = aDatosHito[9];
            sCiclico = aDatosHito[10];

            if ((sTipoHito == "HT")||(sTipoHito == "HM"))
                iMargen = -1;
            else
                iMargen = 0;
            if (sAlerta == "1") bAlerta = true;
            if (sCiclico == "1") bCiclico = true;
            HITOPSP.Update(tr, sTipoHito, iCodHito, sDesHito, sDesHitoLong, sEstado, iMargen, iOrden,
                           bAlerta, bCiclico, iCodPE, dtFecha, bHitoPE);

            //Grabamos las tareas asociadas al hito (Si es hito de PE no porque ya lo hace el trigger)
            //if (sTareas != "" && !bHitoPE)
            if (sTareas != "")
            {
                string[] aTareas = Regex.Split(sTareas, @"##");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    sTipoLinea = sCad.Substring(0, 1);
                    sCodTarea = quitaPuntos(sCad.Substring(1));
                    if (sTipoLinea == "D")
                    {//Borrar hito-tarea
                        HITOPSP.DeleteTarea(tr, iCodHito, int.Parse(sCodTarea));
                    }
                    else
                    {
                        if (sTipoLinea == "I")
                        {//Insertar hito-tarea
                            HITOPSP.InsertTarea(tr, iCodHito, int.Parse(sCodTarea));
                        }
                    }
                }
            }
            Conexion.CommitTransaccion(tr);
            //sResul = "OK@#@" + DateTime.Now.ToString() + "@#@" + Session["UsuarioActual"].ToString() + "@#@" + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
            sResul = "OK@#@" + iCodHito.ToString() + "@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del hito", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string flAgregacion(string sTipo, byte iMargen)
    {
        string sRes = "";
        try
        {
            if (sTipo != "HF")
            {
                switch (iMargen)
                {
                    case 0: sRes = "Proyecto económico"; break;
                    case 20: sRes = "Proyecto técnico"; break;
                    case 40: sRes = "Fase"; break;
                    case 60: sRes = "Actividad"; break;
                    case 80: sRes = "Tarea"; break;
                    default: sRes = "Desconocido"; break;
                }
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener la agregación del hito", ex);
        }
        return sRes;
    }
    private string quitaPuntos(string sCadena)
    {
        //Finalidad:Elimina los puntos de una cadena
        string sRes;

        sRes = sCadena;
        try
        {
            if (sCadena == "") return "";
            sRes = sRes.Replace(".", "");
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al quitar puntos de la cadena" + sCadena, ex);
        }
        return sRes;
    }
    protected string EliminarDocumentos(string strIdsDocs, string sTipoHito)
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
            #region eliminar documentos

            string[] aDocs = Regex.Split(strIdsDocs, "##");

            foreach (string oDoc in aDocs)
            {
                if (sTipoHito != "HF")
                    DOCUH.Delete(tr, int.Parse(oDoc));
                else
                    DOCUHE.Delete(tr, int.Parse(oDoc));
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string ObtenerTodasTareas(string sPE)
    {
        //Relacion de tareas asignadas del proyecto económico
        string sResul = "", sCad, sFecha, sCodTarea;
        double fPrev = 0, fCons = 0, fAvance = 0;
        bool bAvanceAutomatico;

        StringBuilder sbuilder = new StringBuilder();
        try
        {
            sbuilder.Append("<table id='tblTareas' class='texto MANO' style='width: 800px;'>");
            //............................ idTarea........... Desc Tarea..........ETPL.......................................separador............FIPL....................FFPL..............ETPR......................................separador...........FFPR................CONSUMO.....................................AVANCE
            sbuilder.Append("<colgroup><col style='width:40px;' /><col style='width:340px;' /><col style='width:50px;'/><col style='width:10px' /><col style='width:60px' /><col style='width:60px' /><col style='width:50px' /><col style='width:10px' /><col style='width:60px' /><col style='width:50px'/><col style='width:50px' /></colgroup>");
            sbuilder.Append("<tbody>");
            if (sTipoHito != "HF")
            {
                SqlDataReader dr = HITOPSP.CatalogoTareasPE(int.Parse(sPE));

                //Tarea 50 Desc 350 ETPL 40+10 FIPL 60 FFPL 60 ETPR 40+10 FFPR 60 CONSUMO 50 AVANCE 50
                int i = 0;
                while (dr.Read())
                {
                    sCodTarea = dr["codTarea"].ToString();
                    sbuilder.Append("<tr id='" + sCodTarea + "' est='I' onclick='mm(event)' style='height:16px;'>");
                    sbuilder.Append("<td>" + int.Parse(sCodTarea).ToString() + "</td>");
                    sbuilder.Append("<td>" + dr["desTarea"].ToString() + "</td>");

                    if (dr["ETPL"] != DBNull.Value)
                        sbuilder.Append("<td style='text-align:right'>" + double.Parse(dr["ETPL"].ToString()).ToString("N") + "</td>");
                    else
                        sbuilder.Append("<td style='text-align:right'></td>");

                    sbuilder.Append("<td>&nbsp;</td>");

                    sFecha = dr["FIPL"].ToString();
                    if (sFecha != "")
                        sFecha = DateTime.Parse(dr["FIPL"].ToString()).ToShortDateString();

                    sbuilder.Append("<td>" + sFecha + "</td>");

                    sFecha = dr["FFPL"].ToString();
                    if (sFecha != "")
                        sFecha = DateTime.Parse(dr["FFPL"].ToString()).ToShortDateString();

                    sbuilder.Append("<td>" + sFecha + "</td>");

                    if (dr["ETPR"] != DBNull.Value)
                        sbuilder.Append("<td style='text-align:right'>" + double.Parse(dr["ETPR"].ToString()).ToString("N") + "</td>");
                    else
                        sbuilder.Append("<td style='text-align:right'></td>");

                    sbuilder.Append("<td>&nbsp;</td>");

                    sFecha = dr["FFPR"].ToString();
                    if (sFecha != "")
                        sFecha = DateTime.Parse(dr["FFPR"].ToString()).ToShortDateString();

                    sbuilder.Append("<td>" + sFecha + "</td>");

                    sbuilder.Append("<td style='text-align:right'>" + double.Parse(dr["consumo"].ToString()).ToString("N") + "</td>");

                    //%Avance
                    bAvanceAutomatico = (bool)dr["t332_avanceauto"];
                    if (!bAvanceAutomatico)
                    {
                        fAvance = double.Parse(dr["t332_AVANCE"].ToString());
                    }
                    else
                    {
                        fPrev = double.Parse(dr["ETPR"].ToString());
                        fCons = double.Parse(dr["Consumo"].ToString());
                        if (fPrev == 0) fAvance = 0;
                        else fAvance = (fCons * 100) / fPrev;
                    }
                    sCad = fAvance.ToString("N");
                    sbuilder.Append("<td style='text-align:right'>" + sCad + "</td></tr>");
                    i++;
                }
                dr.Close();
                dr.Dispose();
            }
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table>");
            strTablaTareas = sbuilder.ToString();
            sResul = "OK@#@" + strTablaTareas;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de tareas.", ex);
        }

        return sResul;
    }

}
