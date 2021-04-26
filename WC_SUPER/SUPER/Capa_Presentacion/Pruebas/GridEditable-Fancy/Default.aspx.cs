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
using SUPER.Capa_Negocio;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text.RegularExpressions;

public partial class GridEditableFancy : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sTreeView = "";
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.sbotonesOpcionOn = "4";
                Master.sbotonesOpcionOff = "4";

                Master.TituloPagina = "Prueba de GridEditable-Fancy";

                Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1.min.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery-ui.min.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.dnd.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.edit.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.menu.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.gridnav.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.fancytree.table.js");
                Master.FuncionesJavaScript.Add("Javascript/jquery.ui-contextmenu.js");
                Master.FicherosCSS.Add("Capa_Presentacion/Pruebas/GridEditable-Fancy/css/ui.fancytree.css");
                //Master.FicherosCSS.Add("App_Themes/src/skin-win7/ui.fancytree.css");
                Master.bFuncionesLocales = true;

                sTreeView = Arbol();
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
     
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static string GrabarAjax(string sDelete, string sInsert, string sUpdate, parametros objeto)
    {
        SqlConnection oConn = null;
        SqlTransaction tr;

        string sResul = "", sInsertados = "";
        int nIDSector = -1;
        int nIDSegmen = -1;
        //string[] aDatosBasicos = null;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            //sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            //return sResul;
            throw new Exception(Errores.mostrarError("Error al abrir la conexión", ex));
        }
        #endregion
        try
        {
            #region Bajas
            if (sDelete != "") //No se han realizado bajas
            {
                string[] aDeletes = Regex.Split(sDelete, "///");
                foreach (string oDelete in aDeletes)
                {
                    if (oDelete == "") continue;
                    string[] aDel = Regex.Split(oDelete, "##");

                    ///aDel[0] = id
                    ///aDel[1] = title
                    ///aDel[2] = nivel
                    ///aDel[2] = parent
                    ///
                    if (aDel[2] == "1") SUPER.DAL.SECTOR.Delete(tr, int.Parse(aDel[0]));
                    else if (aDel[2] == "2") SUPER.DAL.SEGMENTO.Delete(tr, int.Parse(aDel[0]));
                }
            }

            #endregion
            #region Inserciones
            if (sInsert != "") // No se han realizado Inserciones
            {
                string[] aInserts = Regex.Split(sInsert, "///");
                foreach (string oInsert in aInserts)
                {
                    if (oInsert == "") continue;
                    string[] aInsert = Regex.Split(oInsert, "##");

                    ///aInsert[0] = id virtual
                    ///aInsert[1] = title
                    ///aInsert[2] = nivel
                    ///aInsert[3] = parent(sector para el caso de los segmentos)
                    ///aInsert[4] = codext                 
                    // Estoy metiendo el codigo externo con el valor de la denominacion

                    if (aInsert[2] == "1")
                    {
                        nIDSector = SUPER.DAL.SECTOR.Insert(tr, aInsert[1], aInsert[4]);
                        if (sInsertados == "") sInsertados = aInsert[0] + "::" + nIDSector.ToString();
                        else sInsertados += "//" + aInsert[0] + "::" + nIDSector.ToString();
                    }
                    else if (aInsert[2] == "2")
                    {
                        string sID = (int.Parse(aInsert[3]) < 0) ? nIDSector.ToString() : aInsert[3];
                        nIDSegmen = SUPER.DAL.SEGMENTO.Insert(tr, aInsert[1], aInsert[4], int.Parse(sID));
                        if (sInsertados == "") sInsertados = aInsert[0] + "::" + nIDSegmen.ToString();
                        else sInsertados += "//" + aInsert[0] + "::" + nIDSegmen.ToString();
                    }
                }
            }
            #endregion
            #region Modificaciones
            if (sUpdate != "") //No se han realizado modificaciones
            {
                string[] aUpdates = Regex.Split(sUpdate, "///");
                foreach (string oUpdate in aUpdates)
                {
                    if (oUpdate == "") continue;
                    string[] aUpd = Regex.Split(oUpdate, "##");

                    ///aUpd[0] = id
                    ///aUpd[1] = title
                    ///aUpd[2] = nivel
                    ///aUpd[3] = parent(sector para el caso de los segmentos)
                    ///aUpd[4] = codext
                    ///
                    if (aUpd[2] == "1") SUPER.DAL.SECTOR.Update(tr, int.Parse(aUpd[0]), aUpd[1], aUpd[4]);
                    else if (aUpd[2] == "2") SUPER.DAL.SEGMENTO.Update(tr, int.Parse(aUpd[0]), aUpd[1], aUpd[4], int.Parse(aUpd[3]));
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = sInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            string sError = Errores.mostrarError("Error al grabar los datos del árbol. ", ex);
            string[] aError = Regex.Split(sError, "@#@");
            throw new Exception(Utilidades.escape(aError[0]), ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    public class parametros
    {
        public string sDelete;
        public string sInsert;
        public string sUpdate;

    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");

        //Session.Clear();
        //Session.Abandon();

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {

            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
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
    private static string Arbol()
    {
        SqlDataReader dr = SUPER.DAL.SECTOR.Arbol();
        StringBuilder sb = new StringBuilder();
        string sIdSector = "";
        string sIdSegmento = "";
        int indice1 = 0;
        int indice2 = 0;

        sb.Append("[");
        sb.Append("{ title: 'Sector', key:0, folder: true, expanded: false, data:{nivel:'0',codext:''}, children: [");

        while (dr.Read())
        {
            if (sIdSector != dr["identificador1"].ToString())
            {
                indice2 = 0;
                if (indice1 == 1)
                {
                    sb.Append("]},");
                    indice1 = 0;
                }
                //sb.Append("{title: '" + dr["identificador1"].ToString() + "-" + dr["denominacion1"].ToString() + "', key: '" + dr["identificador1"].ToString() + "', folder: true, expanded: false, data:{bd: '', parentIni: '',nivel:'1'}, children: [");
                sb.Append("{title: '" + dr["denominacion1"].ToString() + "', key: '" + dr["identificador1"].ToString() + "', folder: true, expanded: false, data:{bd: '', parentIni: '',nivel:'1', codext:'" + dr["CODEXT1"].ToString() + "'}, children: [");
                sIdSector = dr["identificador1"].ToString();
                if (indice1 == 0) indice1 = 1;
            }
            if (dr["identificador2"].ToString() == "0") continue;
            if (sIdSegmento != dr["identificador2"].ToString())
            {
                if (indice2 == 1) sb.Append(",");
                //sb.Append("{title: '" + dr["identificador2"].ToString() + "-" + dr["denominacion2"].ToString() + "', key: '" + dr["identificador2"].ToString() + "', data:{bd: '', parentIni: '" + dr["identificador1"].ToString() + "',nivel:'2'}}");
                sb.Append("{title: '" + dr["denominacion2"].ToString() + "', key: '" + dr["identificador2"].ToString() + "', data:{bd: '', parentIni: '" + dr["identificador1"].ToString() + "',nivel:'2', codext:'" + dr["CODEXT2"].ToString() + "'}}");
                sIdSegmento = dr["identificador2"].ToString();
                indice2 = 1;
            }
        }
        if (indice1 == 1) sb.Append("]}");

        sb.Append("]}");

        sb.Append("]");

        dr.Close();
        dr.Dispose();
        return sb.ToString();

    }
    private string GenerarArbol()
    {
        SqlDataReader dr = SUPER.DAL.SECTOR.Arbol();
        StringBuilder sb = new StringBuilder();

        string sIdSector = "";
        string sIdSegmento = "";
        int indice1 = 0;

        while (dr.Read())
        {
            if (sIdSector != dr["identificador1"].ToString())
            {
                if (indice1 == 1)
                {
                    sb.Append("</ul>");
                    indice1 = 0;
                }
                sb.Append("<li id='" + dr["identificador1"].ToString() + "' >" + dr["denominacion1"].ToString());
                sIdSector = dr["identificador1"].ToString();
                if (indice1 == 0)
                {
                    sb.Append("<ul>");
                    indice1 = 1;
                }
            }
            if (sIdSegmento != dr["identificador2"].ToString())
            {
                sb.Append("<li id='" + dr["identificador2"].ToString() + "' >" + dr["denominacion2"].ToString());
                sIdSegmento = dr["identificador2"].ToString();
            }
        }
        dr.Close();
        dr.Dispose();
        return sb.ToString();
    }


    private string Grabar(string sDelete, string sInsert, string sUpdate)
    {
        //string pf = "ctl00$CPHC$";
        //string sDelete = Request.Form[pf + "hdnDelete"]; // Borrados
        //string sInsert = Request.Form[pf + "hdnInsert"]; // Insert
        //string sUpdate = Request.Form[pf + "hdnUpdate"]; // Updates

        string sResul = "", sInsertados = "";
        int nIDSector = -1;
        int nIDSegmen = -1;
        //string[] aDatosBasicos = null;


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
            #region Bajas
            if (sDelete != "") //No se han realizado bajas
            {
                string[] aDeletes = Regex.Split(sDelete, "///");
                foreach (string oDelete in aDeletes)
                {
                    if (oDelete == "") continue;
                    string[] aDel = Regex.Split(oDelete, "##");

                    ///aDel[0] = id
                    ///aDel[1] = title
                    ///aDel[2] = nivel
                    ///aDel[2] = parent
                    ///
                    if (aDel[2] == "1") SUPER.DAL.SECTOR.Delete(tr, int.Parse(aDel[0]));
                    else if (aDel[2] == "2") SUPER.DAL.SEGMENTO.Delete(tr, int.Parse(aDel[0]));
                }
            }

            #endregion
            #region Inserciones
            if (sInsert != "") // No se han realizado Inserciones
            {
                string[] aInserts = Regex.Split(sInsert, "///");
                foreach (string oInsert in aInserts)
                {
                    if (oInsert == "") continue;
                    string[] aInsert = Regex.Split(oInsert, "##");

                    ///aInsert[0] = id virtual
                    ///aInsert[1] = title
                    ///aInsert[2] = nivel
                    ///aInsert[3] = parent(sector para el caso de los segmentos)
                    ///aInsert[4] = id real                   
                    // Estoy metiendo el codigo externo con el valor de la denominacion

                    if (aInsert[2] == "1")
                    {
                        nIDSector = SUPER.DAL.SECTOR.Insert(tr, aInsert[1], aInsert[1]);
                        if (sInsertados == "") sInsertados = aInsert[0] + "::" + nIDSector.ToString();
                        else sInsertados += "//" + aInsert[0] + "::" + nIDSector.ToString();
                    }
                    else if (aInsert[2] == "2")
                    {
                        string sID = (int.Parse(aInsert[3]) < 0) ? nIDSector.ToString() : aInsert[3];
                        nIDSegmen = SUPER.DAL.SEGMENTO.Insert(tr, aInsert[1], aInsert[1], int.Parse(sID));
                        if (sInsertados == "") sInsertados = aInsert[0] + "::" + nIDSegmen.ToString();
                        else sInsertados += "//" + aInsert[0] + "::" + nIDSegmen.ToString();
                    }
                }
            }
            #endregion
            #region Modificaciones
            if (sUpdate != "") //No se han realizado modificaciones
            {
                string[] aUpdates = Regex.Split(sUpdate, "///");
                foreach (string oUpdate in aUpdates)
                {
                    if (oUpdate == "") continue;
                    string[] aUpd = Regex.Split(oUpdate, "##");

                    ///aUpd[0] = id
                    ///aUpd[1] = title
                    ///aUpd[2] = nivel
                    ///aUpd[3] = parent(sector para el caso de los segmentos)
                    ///
                    if (aUpd[2] == "1") SUPER.DAL.SECTOR.Update(tr, int.Parse(aUpd[0]), aUpd[1], null);
                    else if (aUpd[2] == "2") SUPER.DAL.SEGMENTO.Update(tr, int.Parse(aUpd[0]), aUpd[1], null, int.Parse(aUpd[3]));
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del árbol", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
