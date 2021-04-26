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

using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using System.Xml;
using System.IO;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, sErrores, sNodo = "";
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

            if (!Page.IsPostBack)
            {
                try
                {
                    //if (!(bool)Session["FORANEOS"])
                    //{
                    //    this.imgForaneo.Visible = false;
                    //    this.lblForaneo.Visible = false;
                    //    this.imgForaneo2.Visible = false;
                    //    this.lblForaneo2.Visible = false;
                    //    this.imgForaneo3.Visible = false;
                    //    this.lblForaneo3.Visible = false;
                    //}
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    if (Request.QueryString["psn"] != null)
                    {
                        this.hdnPSN.Value = Request.QueryString["psn"].ToString();
                        //bool bLectura = false;
                        //this.hdnEstadoPSN.Value = PROYECTOSUBNODO.getEstado(null, int.Parse(this.hdnPSN.Value));
                        //if (this.hdnEstadoPSN.Value == "C" || this.hdnEstadoPSN.Value == "H")
                        //{
                        //    //ModoLectura.Poner(this.Controls);
                        //    bLectura = true;
                        //}
                        GetProfesionales(int.Parse(this.hdnPSN.Value), true);
                        RellenarCombos(int.Parse(this.hdnPSN.Value));
                    }
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad="";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("grabar"):
            case ("grabar2"):
                sResultado += Grabar(int.Parse(aArgs[1]), aArgs[2]);
                break;
            //case ("profesionales"):
            //    sResultado += GetProfesionales(aArgs[1], int.Parse(aArgs[2]), aArgs[3], aArgs[4]);
            //    break;
            case ("getEstructura"):
                sResultado += "OK@#@" + GetTareas(int.Parse(aArgs[1]), int.Parse(aArgs[2]), aArgs[3]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://PROFESIONALES por defecto no mostramos los de baja
                        sCad = GetProfesionales2(int.Parse(aArgs[2]), false);
                        if (sCad.IndexOf("Error@#@") >= 0)
                            sResultado += sCad;
                        else
                            sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                    case 2://AVANZADO
                        sCad = GetProfesionales3(int.Parse(aArgs[2]), false);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                }
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }
    private string GetProfesionales(int idPSN, bool bConPerfil)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto' style='width:700px;'>");
        //sb.Append(" class='texto MANO' style='width:700px;table-layout:fixed;' cellSpacing='0' border='0'>");
        sb.Append("<colgroup>");
        sb.Append(" <col style='width:20px;' />");
        sb.Append(" <col style='width:450px;' />");
        sb.Append(" <col style='width:230px;' />");
        sb.Append("</colgroup>");

        SqlDataReader dr = USUARIOPROYECTOSUBNODO.CatalogoUsuariosPerfil(idPSN, bConPerfil);
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
            sb.Append("perf='" + dr["t333_idperfilproy"].ToString() + "' ");
            sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
            sb.Append("sexo='" + dr["t001_sexo"].ToString() + "'");
            sb.Append("desnodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
            sb.Append("desempresa=\"" + Utilidades.escape(dr["empresa"].ToString()) + "\" ");
            //sb.Append("desofi=\"" + Utilidades.escape(dr["T010_DESOFICINA"].ToString()) + "\" ");
            sb.Append("style='height:20px' >");
            //sb.Append("style='height:20px' onclick='msse(this);'>");

            sb.Append("<td></td><td>" + dr["profesional"].ToString() + "</td>");
            sb.Append("<td><nobr class='NBR W230' >" + dr["t333_denominacion"].ToString() + "</nobr></td></tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</table>");
        strTablaHtml = sb.ToString();

        return "OK@#@" + sb.ToString();
    }
    private string GetProfesionales2(int idPSN, bool bConPerfil)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblProf2' class='texto' style='width:370px;'>");
        sb.Append("<colgroup><col style='width:20px;'/><col style='width:210px;'/><col style='width:140px;'/></colgroup>");

        SqlDataReader dr = USUARIOPROYECTOSUBNODO.CatalogoUsuariosPerfil(idPSN, bConPerfil);
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
            sb.Append(" perf='" + dr["t333_idperfilproy"].ToString() + "'");
            sb.Append(" tipo='" + dr["tipo"].ToString() + "'");
            sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "'");
            sb.Append(" desnodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\"");
            sb.Append(" desempresa=\"" + Utilidades.escape(dr["empresa"].ToString()) + "\"");
            sb.Append(" style='height:20px' onclick='ms(this);borrarTareas();getEstructura(this.id, this.perf)'>");

            sb.Append("<td></td><td>" + dr["profesional"].ToString() + "</td>");
            sb.Append("<td  title='" + dr["t333_denominacion"].ToString() + "'><nobr class='NBR W140'>" + dr["t333_denominacion"].ToString() + "</nobr></td></tr>");
        }
        sb.Append("</table>");
        dr.Close();
        dr.Dispose();

        return sb.ToString();
    }
    private string GetProfesionales3(int idPSN, bool bConPerfil)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblProf3' class='texto' style='width:440px;'>");
        sb.Append("<colgroup><col style='width:40px;'/><col style='width:20px;'/><col style='width:380px;'/></colgroup>");

        SqlDataReader dr = USUARIOPROYECTOSUBNODO.CatalogoUsuariosPerfil(idPSN, bConPerfil);
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "'");
            sb.Append(" perf='" + dr["t333_idperfilproy"].ToString() + "'");
            sb.Append(" tipo='" + dr["tipo"].ToString() + "'");
            sb.Append(" sexo='" + dr["t001_sexo"].ToString() + "'");
            sb.Append(" desnodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\"");
            sb.Append(" desempresa=\"" + Utilidades.escape(dr["empresa"].ToString()) + "\"");
            sb.Append(" style='height:20px'>");

            sb.Append("<td><input type='checkbox' style='width:30px' class='checkTabla'></td>");
            sb.Append("<td></td><td>" + dr["profesional"].ToString() + "</td></tr>");
        }
        sb.Append("</table>");
        dr.Close();
        dr.Dispose();

        return sb.ToString();
    }

    private string GetTareas(int idPSN, int idUser, string sEstados)
    {
        StringBuilder sb = new StringBuilder();
        string sDesTipo, sDesc, sTarea, sMargen;//sCodPT, sFase, sActiv, sOrden, 
        int iId = -1;

        sb.Append("<table id='tblTareas' class='texto' style='width:540px;'>");
        sb.Append("<colgroup><col style='width:390px;'/><col style='width:30px;'/><col style='width:120px;'/></colgroup>");

        SqlDataReader dr = EstrProy.EstructuraCompleta(idPSN, idUser, sEstados);
        while (dr.Read())
        {
            iId++;
            sDesTipo = dr["Tipo"].ToString();
            sDesc = HttpUtility.HtmlEncode(dr["Nombre"].ToString());
            //sCodPT = dr["codPT"].ToString();
            //sFase = dr["codFase"].ToString();
            //sActiv = dr["codActiv"].ToString();
            sTarea = dr["codTarea"].ToString();
            //sOrden = dr["orden"].ToString();
            sMargen = dr["margen"].ToString();

            sb.Append("<tr style='height:20px;");

            if (sDesTipo == "T") sb.Append("' id='" + sTarea + "' ");
            else sb.Append("' id='" + iId.ToString() + "' ");
            
            sb.Append("des='" + sDesc + "' ");
            sb.Append("mar='" + sMargen + "' ");

            if (sDesTipo == "P")
                sb.Append(" nivel='1' desplegado='1' ");
            else
                sb.Append(" nivel='2' desplegado='1' ");

            //sb.Append("tipo='" + sDesTipo + "' iPT='" + sCodPT + "' iF='" + sFase + "' iA='" + sActiv + "' ");
            sb.Append("tipo='" + sDesTipo + "' ");
            sb.Append("iT='" + sTarea + "'");
            //sb.Append("iT='" + sTarea + "' iOrd='" + iId + "' iLP1='0' iLP2='0'  ");
            //sb.Append("iPTn='" + sCodPT + "' iFn='" + sFase + "' iAn='" + sActiv + "' iTn='" + sTarea + "' ");
            //sb.Append("iOrdn='" + sOrden + "' ");
            sb.Append(" onclick='ms(this);'>");
            sb.Append("<td>");
            switch (sDesTipo)
            {
                case "P":
                case "F":
                case "A":
                    //sb.Append("<img src='../../../../Images/plus.gif' style='margin-left:" + sMargen + "px;'>");
                    sb.Append("<img src='../../../../Images/minus.gif' onclick='mostrar(this);' style='cursor:pointer; margin-left:" + sMargen + "px;'>");
                    break;
                case "T":
                    sb.Append("<img src='../../../../Images/imgTrans9x9.gif' style='margin-left:" + sMargen + "px;'>");
                    break;
            }
            sb.Append("</td>");
            if (sDesTipo=="T")
                sb.Append("<td><input type='checkbox' style='width:30px' class='checkTabla'></td>");//columna para marcar/desmarcar
            else
                sb.Append("<td></td>");
            sb.Append("<td title='" + dr["perfil"].ToString() + "'><nobr class='NBR W120'>" + dr["perfil"].ToString() + "</nobr></td>");//columna para el perfil
            sb.Append("</tr>");
        }
        sb.Append("</tbody></table>");
        dr.Close();
        dr.Dispose();

        return sb.ToString();
    }

    protected string Grabar(int iTipo, string strDatos)
    {
        string sResul = "";

        switch (iTipo)
        {
            case 1:
                sResul = Procesar1(strDatos);
                break;
            case 2:
                sResul = Procesar2(strDatos);
                break;
            case 3:
                sResul = Procesar3(strDatos);
                break;
            default:
                sResul = sResul = "Error@#@Proceso no encontrado.";
                break;
        }
        return sResul;
    }

    private string Procesar1(string strDatos)
    {
        string sResul = "";
        #region apertura de conexión y transacción
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
        #endregion

        try
        {
            string[] aCri = Regex.Split(strDatos, "///");
            //0. id PSN
            //1. tengan
            //2. lista estados
            PERFILPROY.Procesar1(tr, int.Parse(aCri[0]), aCri[1], aCri[2]);
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar opción 1.", ex, false);// +"@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string Procesar2(string strDatos)
    {
        string sResul = "";
        #region apertura de conexión y transacción
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
        #endregion

        try
        {
            string[] aCri = Regex.Split(strDatos, "///");
            //0. id PSN
            //1. id usuario
            //2. tipo
            //3. id perfil en el proyecto
            //4. id perfil Aux
            //5. lista tareas
            int? idPerfil = null;
            int? idPerfilAux = null;
            if (aCri[3] != "-1")
                idPerfil = int.Parse(aCri[3]);
            if (aCri[4] != "-1")
                idPerfilAux = int.Parse(aCri[4]);

            PERFILPROY.Procesar2(tr, int.Parse(aCri[0]), int.Parse(aCri[1]), idPerfil, idPerfilAux, aCri[5], aCri[2]);

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar opción 2.", ex, false);// +"@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string Procesar3(string strDatos)
    {
        string sResul = "";
        #region apertura de conexión y transacción
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
        #endregion

        try
        {
            string[] aCri = Regex.Split(strDatos, "///");
            //0. id PSN
            //1. id perfil en el proyecto
            //2. lista profesionales
            //3. lista estados
            PERFILPROY.Procesar3(tr, int.Parse(aCri[0]), int.Parse(aCri[1]), aCri[2], aCri[3]);
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar opción 3.", ex, false);// +"@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private void RellenarCombos(int t305_idproyectosubnodo)
    {
        obtenerPerfil1(t305_idproyectosubnodo);
        obtenerPerfil2(t305_idproyectosubnodo);
        obtenerPerfil3(t305_idproyectosubnodo);
        obtenerPerfil4(t305_idproyectosubnodo);
    }
    private void obtenerPerfil1(int t305_idproyectosubnodo)
    {
        SqlDataReader dr = PERFILPROY.CatalogoPerfilesProyecto_By_PSN(null, t305_idproyectosubnodo);
        ListItem oItem;
        while (dr.Read())
        {
            if ((bool)dr["t333_estado"])
            {
                oItem = new ListItem(dr["t333_denominacion"].ToString(), dr["t333_idperfilproy"].ToString());
                cboPerfil1.Items.Add(oItem);
            }
        }
        dr.Close();
        dr.Dispose();
    }
    private void obtenerPerfil2(int t305_idproyectosubnodo)
    {
        SqlDataReader dr = PERFILPROY.CatalogoPerfilesProyecto_By_PSN(null, t305_idproyectosubnodo);
        ListItem oItem;
        while (dr.Read())
        {
            if ((bool)dr["t333_estado"])
            {
                oItem = new ListItem(dr["t333_denominacion"].ToString(), dr["t333_idperfilproy"].ToString());
                cboPerfil2.Items.Add(oItem);
            }
        }
        dr.Close();
        dr.Dispose();
    }
    private void obtenerPerfil3(int t305_idproyectosubnodo)
    {
        SqlDataReader dr = PERFILPROY.CatalogoPerfilesProyecto_By_PSN(null, t305_idproyectosubnodo);
        ListItem oItem;
        while (dr.Read())
        {
            if ((bool)dr["t333_estado"])
            {
                oItem = new ListItem(dr["t333_denominacion"].ToString(), dr["t333_idperfilproy"].ToString());
                cboPerfil3.Items.Add(oItem);
            }
        }
        dr.Close();
        dr.Dispose();
    }
    private void obtenerPerfil4(int t305_idproyectosubnodo)
    {
        SqlDataReader dr = PERFILPROY.CatalogoPerfilesProyecto_By_PSN(null, t305_idproyectosubnodo);
        ListItem oItem;
        while (dr.Read())
        {
            if ((bool)dr["t333_estado"])
            {
                oItem = new ListItem(dr["t333_denominacion"].ToString(), dr["t333_idperfilproy"].ToString());
                cboPerfil4.Items.Add(oItem);
            }
        }
        dr.Close();
        dr.Dispose();
    }
}