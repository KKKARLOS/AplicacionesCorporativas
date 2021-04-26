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
    public string sErrores, sLectura, strTablaRecursos;//, sCRcorto;
    public int sNumEmpleado = 0;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string nIdAv, sCriterios = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session.Clear();
        //Session.Abandon();
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        //if (Session["IDRED"] == null)
        //{
        //    sErrores = "SESIONCADUCADA";
        //    Session["strServer"] = Utilidades.RootAplica();
        //    //variables de servidor y de session (excepto Session.Timeout) que alimentan vbles javascript
              sLectura = "false";
        //    sNumEmpleado = 0;
        //    return;
        //};
        if (!Page.IsCallback)
        {
            //if (!(bool)Session["FORANEOS"])
            //{
            //    this.imgForaneo.Visible = false;
            //    this.lblForaneo.Visible = false;
            //}
            sErrores = "";
            sNumEmpleado = int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString());
            strTablaRecursos = "<table id='tblAsignados'></table>";

            //Cargo la denominacion del label Nodo
            //sCRcorto = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            this.lblNodo.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
            //this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

            Utilidades.SetEventosFecha(this.txtValIni);
            Utilidades.SetEventosFecha(this.txtValFin);

            nIdAv = Request.QueryString["nIdAviso"].ToString();
            if (nIdAv != "0")
            {
                try
                {
                    ObtenerDatosAviso(int.Parse(nIdAv));
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener los datos del aviso", ex);
                }
            }
            else
            {//Hemos entrado a dar de alta un aviso
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
        string sResultado = "", sCad = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            //case ("getRecursos"):
            //    bool bMostrarBajas = false;
            //    if (aArgs[3] == "S") bMostrarBajas = true;
            //    sResultado += "OK@#@" + ObtenerRecursosAsociados(aArgs[1]);
            //    break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://PROFESIONALES
                        sCad = ObtenerRecursosAsociados(aArgs[2]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
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
    private void ObtenerDatosAviso(int nIdAviso)
    {
        TEXTOAVISOS o = TEXTOAVISOS.Select(null,nIdAviso);

        txtDen.Text = o.t448_denominacion;
        txtTit.Text = o.t448_titulo;
        txtDescripcion.Text = o.t448_texto;
        if (o.t448_fiv != null)
            txtValIni.Text = ((DateTime)o.t448_fiv).ToShortDateString();
        else
            txtValIni.Text = "";
        if (o.t448_ffv != null)
            txtValFin.Text = ((DateTime)o.t448_ffv).ToShortDateString();
        else
            txtValFin.Text = "";
        if (o.t448_IAP) chkIAP.Checked = true;
        if (o.t448_PGE) chkPGE.Checked = true;
        if (o.t448_PST) chkPST.Checked = true;
    }
    private string ObtenerRecursosAsociados(string sCodAviso)
    {
        //Relacion de tecnicos asignados al proyecto tecnico
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblAsignados' class='texto MM' style='WIDTH:380px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px' /><col style='width:20px' /><col style='width:345px' /></colgroup>");
            sb.Append("<tbody id='tbodyDestino'>");
            if (sCodAviso != "")
            {
                SqlDataReader dr = TEXTOAVISOS.CatalogoRecursos(int.Parse(sCodAviso));
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;' bd='' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja='" + dr["baja"].ToString() + "' ");

                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else sb.Append("tipo='P' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                    sb.Append(" onclick='mm(event);' onmousedown='DD(event)'>");
                    sb.Append("<td></td><td></td>");
                    sb.Append("<td><nobr class='NBR W340'>" + dr["empleado"].ToString() + "</nobr></td>");
                    sb.Append("</tr>");
                }
                dr.Close(); dr.Dispose();
            }
            sb.Append("</tbody></table>");
            strTablaRecursos = sb.ToString();
            sResul = strTablaRecursos;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales asociados al aviso.", ex);
        }

        return sResul;
    }
    protected string ObtenerTecnicos(string strOpcion, string strValor1, string strValor2, string strValor3)
    {
        //Relacion de técnicos candidatos a ser asignados al proyecto técnico
        string sResul = "", sV1 = "", sV2 = "", sV3 = "";
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr=null;
        try
        {
            if (strOpcion == "F")//Hay que buscar profesionales que tengan determinadas figuras
            {
                DataTable dtFiguras = GetDataTableFiguras(strValor1);
                dr = Recurso.GetUsuariosPorFigura(null, dtFiguras);
            }
            else
            {
                sV1 = Utilidades.unescape(strValor1);
                sV2 = Utilidades.unescape(strValor2);
                sV3 = Utilidades.unescape(strValor3);
                dr = Recurso.CatalogoPerfil(strOpcion, sV1, sV2, sV3);
            }
            
            sb.Append("<table id='tblRelacion' class='texto MAM' style='width: 365px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:345px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px;noWrap:true;' ");
                sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' /> ");
                sb.Append("Información] body=[<label style='width:70px;'>Profesional:</label>");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>");
                //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>");
                //sb.Append(dr["t313_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                //sb.Append("baja='" + dr["baja"].ToString() + "' ");

                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append("onclick='mm(event);' ondblclick='insertarRecurso(this);' onmousedown='DD(event)'");
                sb.Append("><td></td><td><nobr class='NBR W340'>" + dr["tecnico"].ToString() + "</nobr></td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            dr.Close(); dr.Dispose();
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }
    protected string Grabar(string sCodAviso, string strDatosBasicos, string strDatosRecursos)
    {
        string sResul = "", sOpcionBD, sDesc, sTitulo, sDescLong;
        bool bIAP = false, bPGE = false, bPST = false;
        int iCodAviso, iCodRecurso;
        DateTime? dIniV = null;
        DateTime? dFinV = null;
        #region abrir conexión y transacción
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
            if (sCodAviso == "0") iCodAviso = -1;
            else iCodAviso = int.Parse(sCodAviso);
            #region Datos generales
            if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
            {
                string[] aDatosTarea = Regex.Split(strDatosBasicos, "##");
                ///aDatosTarea[0] = Denominacion aviso
                ///aDatosTarea[1] = Titulo aviso
                ///aDatosTarea[2] = Texto libre
                ///aDatosTarea[3] = chkIAP
                ///aDatosTarea[4] = chkPGE
                ///aDatosTarea[5] = chkPST
                ///aDatosTarea[6] = txtValIni
                ///aDatosTarea[7] = txtValFin
                sDesc = Utilidades.unescape(aDatosTarea[0]);
                sTitulo = Utilidades.unescape(aDatosTarea[1]);
                sDescLong = Utilidades.unescape(aDatosTarea[2]);
                if (aDatosTarea[3] == "1") bIAP = true;
                if (aDatosTarea[4] == "1") bPGE = true;
                if (aDatosTarea[5] == "1") bPST = true;
                if (aDatosTarea[6] != "") dIniV = DateTime.Parse(aDatosTarea[6]);
                if (aDatosTarea[7] != "") dFinV = DateTime.Parse(aDatosTarea[7]);

                if (iCodAviso <= 0)
                    iCodAviso = TEXTOAVISOS.Insert(tr, sDesc, sTitulo, sDescLong, bIAP, bPGE, bPST, dIniV, dFinV);
                else
                     TEXTOAVISOS.Update(tr, iCodAviso, sDesc, sTitulo, sDescLong, bIAP, bPGE, bPST, dIniV, dFinV);
            }
            #endregion
            #region Recursos
            if (strDatosRecursos != "")
            {
                string[] aRecursos = Regex.Split(strDatosRecursos, "///");

                foreach (string oRec in aRecursos)
                {
                    string[] aValores = Regex.Split(oRec, "##");
                    ///aValores[0] = opcionBD;
                    ///aValores[1] = idRecurso;
                    if (aValores[0] != "")
                    {
                        sOpcionBD = aValores[0];
                        iCodRecurso = int.Parse(aValores[1]);
                        if (iCodRecurso == -1)
                        {//Queremos operar sobre todos los profesionales
                            USUARIOAVISOS.BorrarTodos(tr, iCodAviso);
                            if (sOpcionBD == "I")//Queremos asignar el aviso a todos los profesionales
                                USUARIOAVISOS.InsertarTodos(tr, iCodAviso);
                            break;
                        }
                        switch (sOpcionBD)
                        {
                            case "I":
                                USUARIOAVISOS.Insert(tr, iCodAviso, iCodRecurso);
                                break;
                            case "D":
                                USUARIOAVISOS.Delete(tr, iCodAviso, iCodRecurso);
                                break;
                        }
                    }
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + DateTime.Now.ToString() + "@#@" + Session["UsuarioActual"].ToString() + "@#@" + 
                     Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString() + "@#@" + 
                     iCodAviso.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del aviso", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private DataTable GetDataTableFiguras(string slFiguras)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("nNivel", typeof(int)));
        dt.Columns.Add(new DataColumn("sFigura", typeof(string)));
        if (slFiguras != "")
        {
            string[] aFigs = Regex.Split(slFiguras, "///");
            for (int i = 0; i < aFigs.Length;i++ )
            {
                string[] aElem = Regex.Split(aFigs[i], "#");
                if (aElem[0] != "")
                {
                    DataRow row = dt.NewRow();
                    row["nNivel"] = aElem[1];
                    row["sFigura"] = aElem[0];

                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

}
