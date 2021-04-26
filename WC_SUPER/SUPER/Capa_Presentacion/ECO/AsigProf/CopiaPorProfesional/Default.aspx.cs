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
    protected string strInicial = "", sErrores;
    public string gsAcceso, sNodo = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Copia de profesionales asignados";
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
                    sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.txtNumPE.Focus();
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("recursos"):
                sResultado += ObtenerRecursos(aArgs[1], aArgs[2]);
                break;
            case ("recuperarPSN"):
            case ("recuperarPSN2"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
            case ("buscarPE2"):
                sResultado += buscarPE(aArgs[1]);
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

    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {//Busco entre los proyectos contratantes (cualidad=C)
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, "C", false);
            while (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al recuperar los datos del proyecto", ex);
        }
        return sResul;
    }
    private string ObtenerRecursos(string nPSN, string sCodUne)
    {// Devuelve el código HTML del catalogo de recursos asociados al proyecto
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;
        string sDeriva;
        try
        {
            //sb.Append("<div style='background-image:url(../../../../Images/imgFT20.gif); width:460px; height:auto'>");
            sb.Append("<table id='tblOpciones3' class='texto' style='WIDTH: 460px;'>");
            sb.Append("<colgroup><col style='width:20px;'/><col style='width:420px'/><col style='width:20px'/></colgroup>");//style='padding-left:5px' 
            sb.Append("<tbody>");
            if (nPSN != "")
            {
                dr = USUARIOPROYECTOSUBNODO.CatalogoUsuarios(int.Parse(nPSN), "C", false);
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px; noWrap:true;' bd='' id='" + dr["t314_idusuario"].ToString() + "' ");
                    //sb.Append("estado='" + dr["t336_estado"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    //sb.Append("baja='" + dr["baja"].ToString() + "' ");

                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else if (dr["t303_idnodo"].ToString() == sCodUne) sb.Append("tipo='P' ");
                    //else sb.Append("tipo='N' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    sb.Append("nodo='" + dr["t303_idnodo"].ToString() + "' ");
                    sb.Append("cierre='" + dr["t303_ultcierreeco"].ToString() + "' ");

                    if ((bool)dr["t330_deriva"]) sDeriva = "T";
                    else sDeriva = "F";
                    sb.Append("deriva='" + sDeriva + "' ");

                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'> ");
                    sb.Append("Información] body=[<label style='width:60px'>Profesional&nbsp;:</label>");
                    sb.Append(dr["profesional"].ToString().Replace((char)34, (char)39));

                    sb.Append("<br><label style='width:60px'>Usuario&nbsp;:</label>");
                    sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));

                    sb.Append("<br><label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;:</label>");
                    //sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px'>Empresa&nbsp;:</label>");
                    //sb.Append(dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append("><td style='vertical-align:top;'></td><td><nobr class='NBR' style='width:410px;'>" + dr["Profesional"].ToString() + "</nobr></td>");
                    sb.Append("<td style='vertical-align:middle;'><input type='checkbox' style='width:15px; height:15px;' class='checkTabla' checked='true'></td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");
                sb.Append("</table></div>");
                dr.Close(); dr.Dispose();
            }
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los profesionales ", ex);
        }
    }
    private string recuperarPSN(string nPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nPSN), (int)Session["UsuarioActual"], "PGE");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");// + "@#@");  //4
                //sb.Append(dr["estado"].ToString());  //5
                if ((bool)dr["t305_admiterecursospst"]) sb.Append("S");//5
                else sb.Append("N");//5
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }
    private string Grabar(string sPSN, string sRecursos, string t305_avisorecursopst)
    {
        string sResul = "", sItem;
        int idPSN = int.Parse(sPSN), idUser;
        bool bDeriva, bNotif;
        decimal costecon = 0, costerep = 0;
        try
        {
            if (sPSN == "" || sRecursos == "")
            {//Tenemos lista vacía. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aRecursos = Regex.Split(sRecursos, @"##");
                for (int i = 0; i < aRecursos.Length; i++)
                {
                    sItem = aRecursos[i];
                    string[] aIt = Regex.Split(sItem, @",");

                    idUser = int.Parse(aIt[0]);
                    if (aIt[3] == "T")
                        bDeriva = true;
                    else
                        bDeriva = false;

                    if (t305_avisorecursopst == "S") bNotif = true;
                    else bNotif = false;

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
                    sResul += PonerRecurso(idUser, int.Parse(aIt[2]), idPSN, bDeriva, costecon, costerep, bNotif);
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
    private string PonerRecurso(int IdRecurso, int iUltCierreEco, int IdPsn, bool bDeriva, decimal costecon, decimal costerep, bool bNotif)
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
                if (!USUARIOPROYECTOSUBNODO.AsociadoDeAltaProyecto(tr,IdPsn,IdRecurso))
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
