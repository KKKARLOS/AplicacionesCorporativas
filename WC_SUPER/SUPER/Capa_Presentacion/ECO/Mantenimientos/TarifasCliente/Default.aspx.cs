using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using EO.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    //public string strTablaHTML;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.TituloPagina = "Mantenimiento de tarifas por cliente";
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.bFuncionesLocales = true;

            try
            {
                //if (Session["NodoActivo"].ToString() != "")
                //{
                //    hdnIdNodo.Text = Session["NodoActivo"].ToString();
                //    txtDesNodo.Text = Session["DesNodoActivo"].ToString();
                //    string strTabla = obtenerTarifas(Session["NodoActivo"].ToString(), Session["DesNodoActivo"].ToString());
                //    string[] aTabla = Regex.Split(strTabla, "@#@");
                //    if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                //    else Master.sErrores = aTabla[1];
                //}
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1? Se indican (por este orden) la funci?n a la que se va a devolver el resultado
            //   y la funci?n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2? Se "registra" la funci?n que va a acceder al servidor.
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
        //1? Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2? Aqu? realizar?amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getTarifas"):
                sResultado += obtenerTarifas(aArgs[1], aArgs[2]);
                break;
                
        }

        //3? Damos contenido a la variable que se env?a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env?a el resultado al cliente.
        return _callbackResultado;
    }
        
    private string obtenerTarifas(string sIdCliente, string sTipo)
    {
        try
        {
            bool? bEstado;

            //Si sTipo=T no restrinjo por estado de la PST, sino saco solo las activas
            if (sTipo == "T") bEstado = null;
            else bEstado = true;

            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = PERFILSUPER.SelectByT302_idcliente(null, int.Parse(sIdCliente), bEstado);

            sb.Append("<table id='tblDatos' class='texto MANO' style='width:570px; text-align:left;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:25px;' /><col style='width:270px;' /><col style='width:100px;' /><col style='width:100px;' /><col style='width:60px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t347_idperfilsuper"].ToString() + "' bd='' orden='" + dr["t347_orden"].ToString() + "' onclick='mm(event)' style='height:20px'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td><img src='../../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                sb.Append("<td><input type='text' class='txtL' style='width:260px' value=\"" + dr["t347_denominacion"].ToString() + "\" maxlength='30' onKeyUp='fm(event)'></td>");
                sb.Append("<td style='text-align:right; margin-right:5px;'><input type='text' class='txtNumL' style='width:90px;' value=\"" + decimal.Parse(dr["t347_imptarifahor"].ToString()).ToString("N") + "\" onKeyUp='fm(event)' onfocus='fn(this)'></td>");
                sb.Append("<td style='text-align:right; margin-right:5px;'><input type='text' class='txtNumL' style='width:90px;' value=\"" + decimal.Parse(dr["t347_imptarifajor"].ToString()).ToString("N") + "\" onKeyUp='fm(event)' onfocus='fn(this)'></td>");
                sb.Append("<td><input type='checkbox' style='width:20px; margin-left:23px;' ");
                sb.Append("class='checkTabla' onclick='aG();fm(event);' ");
                if ((bool)dr["t347_estado"]) sb.Append("checked=true");
                sb.Append("></td>");


                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tarifas", ex);
        }
    }
    protected string Grabar(string sIdCliente, string strDatos)
    {
        string sResul = "", sDesc = "", sElementosInsertados = "";
        int nAux = 0;

        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi?n", ex);
            return sResul;
        }

        try
        {
            string[] aTarifa = Regex.Split(strDatos, "///");
            foreach (string oTarifa in aTarifa)
            {
                if (oTarifa == "") continue;
                string[] aValores = Regex.Split(oTarifa, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID Tarifa
                //2. Descripcion
                //3. Hora
                //4. Jornada
                //5. Orden
                //6. Estado
                bool bEstado = false;
                if (aValores[6] == "1") bEstado = true;

                sDesc = Utilidades.unescape(aValores[2]);
                switch (aValores[0])
                {
                    case "I":
                        nAux = PERFILSUPER.Insert(tr, Utilidades.unescape(aValores[2]), decimal.Parse(aValores[3]), decimal.Parse(aValores[4]),
                            null, int.Parse(sIdCliente), short.Parse(aValores[5]), bEstado);
                        if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                        else sElementosInsertados += "//" + nAux.ToString();
                        break;
                    case "U":
                        PERFILSUPER.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), decimal.Parse(aValores[3]),
                            decimal.Parse(aValores[4]), null, int.Parse(sIdCliente), short.Parse(aValores[5]), bEstado);
                        break;
                    case "D":
                        PERFILSUPER.Delete(tr, int.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las tarifas.", ex) + "@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
