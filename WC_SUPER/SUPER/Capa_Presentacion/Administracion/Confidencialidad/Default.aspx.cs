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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strTablaHTML="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 52;
            Master.TituloPagina = "Mantenimiento de costes confidenciales";
            Master.bFuncionesLocales = true;

            try
            {
                string[] aTabla = Regex.Split(obtenerCostes(), "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else Master.sErrores += Errores.mostrarError(aTabla[1]);
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    private string obtenerCostes()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CATEGSUPER.Catalogo(null, "", null, null, 2, 0);

            sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 500px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:290px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t450_idcategsuper"].ToString() + "' bd='' onclick='mm(event)' style='height:20px'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'><input type='text' class='txtL' style='width:270px' value=\"" + dr["t450_denominacion"].ToString() + "\" maxlength='25' onKeyUp='fm(event)'></td>");
                sb.Append("<td><input type='text' class='txtNumL' style='width:95px;' value=\"" + decimal.Parse(dr["t450_costemediohora"].ToString()).ToString("N") + "\" onKeyUp='fm(event)' onfocus='fn(this,4,2)'></td>");
                sb.Append("<td><input type='text' class='txtNumL' style='width:95px;' value=\"" + decimal.Parse(dr["t450_costemediojornada"].ToString()).ToString("N") + "\" onKeyUp='fm(event)' onfocus='fn(this,4,2)'></td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener las categorías.", ex);
        }
    }
    protected string Grabar(string strDatos)
    {
        string sResul = "", sDesc = "", sElementosInsertados = "";
        int nAux = 0;

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
            string[] aCategoria = Regex.Split(strDatos, "///");
            foreach (string oCategoria in aCategoria)
            {
                if (oCategoria == "") continue;
                string[] aValores = Regex.Split(oCategoria, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID Categoría
                //2. Descripcion
                //3. Hora
                //4. Jornada

                switch (aValores[0])
                {
                    case "I":
                        nAux = CATEGSUPER.Insert(tr, Utilidades.unescape(aValores[2]), decimal.Parse(aValores[3]), decimal.Parse(aValores[4]));
                        if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                        else sElementosInsertados += "//" + nAux.ToString();
                        break;
                    case "U":
                        CATEGSUPER.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), decimal.Parse(aValores[3]), decimal.Parse(aValores[4]));
                        break;
                    case "D":
                        CATEGSUPER.Delete(tr, int.Parse(aValores[1]));
                        break;
                }
            }
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar las tarifas.", ex, false) + "@#@" + sDesc;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        return sResul;
    }

}
