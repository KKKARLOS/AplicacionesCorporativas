using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GESTAR.Capa_Negocio;
using EO.Web;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    protected System.Web.UI.WebControls.TextBox hdnInicial;
    private string _callbackResultado = null;
    SqlDataReader dr_resultado = null;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;
            Master.nBotonera = 0;
            Session["PANT_AVANZADO"] = null;
            Session["PANT_VENCIMIENTO"] = null;

            try
            {
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                txtFechaInicio.Attributes.Add("readonly", "readonly");
                txtFechaFin.Attributes.Add("readonly", "readonly");

                //this.divCal1.Controls.Add(Fechas.InsertarCalendario("txtFechaInicio"));
                //this.divCal2.Controls.Add(Fechas.InsertarCalendario("txtFechaFin"));

                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                hdnVerCaja.Text = Session["ADMIN"].ToString();
                hdnIDFICEPI.Text = Session["IDFICEPI"].ToString();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context");
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@@");

        //2º Aquí realizaríamos el acceso a BD, etc,...
        System.Text.StringBuilder strbTabla = new System.Text.StringBuilder();
        strbTabla.Length = 0;

        switch (aArgs[0])
        {
            case "1":   // Catálogo de áreas
                try
                {
                    dr_resultado = null;

                    dr_resultado = Areas.Catalogo(int.Parse(aArgs[1].ToString()), int.Parse(aArgs[2].ToString()), int.Parse(Session["IDFICEPI"].ToString()), aArgs[3].ToString());
                    int i = 0;

                    strbTabla.Append("<table id='tblCatalogo' style='cursor:pointer;width:950px;text-align:left' cellpadding='0' cellspacing='0' border='0'>" + (char)13);
                    strbTabla.Append("<colgroup>");
                    strbTabla.Append(" <col style='width:58%' />");
                    strbTabla.Append(" <col style='width:42%' />");

                    strbTabla.Append("</colgroup>");

                    while (dr_resultado.Read())
                    {
                        strbTabla.Append("<tr id='" + dr_resultado["Id"].ToString() + "' ");
                        strbTabla.Append(" style='height:16px'");
                        strbTabla.Append(" ondblclick=Det_Area(this); ");
                        strbTabla.Append(" onclick=ms(this);btnElimArea(this,'" + dr_resultado["PROPIETARIO"].ToString() + "','" + dr_resultado["NOMBRE"].ToString().Replace(" ", "+") + "','" + dr_resultado["COORDINADOR"].ToString() + "','" + dr_resultado["SOLICITANTE"].ToString() + "','" + dr_resultado["RESPONSABLE"].ToString() + "','" + dr_resultado["CATEGORIA"].ToString() + "'); ");
                        strbTabla.Append(">");

                        strbTabla.Append("<td>&nbsp;&nbsp;" + dr_resultado["NOMBRE"].ToString() + "</td>");
                        strbTabla.Append("<td>" + dr_resultado["RECURSO"].ToString() + "</td>");
                        strbTabla.Append("</tr>" + (char)13);
                        i = i + 1;
                    }

                    dr_resultado.Close();
                    dr_resultado.Dispose();

                    strbTabla.Append("</table>");
                    break;
                }
                catch (System.Exception objError)
                {
                    strbTabla.Append("N@@" + Errores.mostrarError("Error al leer catálogo de areas.", objError));
                }
                break;
            case "2":   // Catálogo de deficiencias asociadas a un área
                try
                {
                    dr_resultado = null;

                    dr_resultado = Deficiencias.LeerCatalogoDeficiencias(int.Parse(Session["IDFICEPI"].ToString()), aArgs[1].ToString(), int.Parse(aArgs[2].ToString()), int.Parse(aArgs[3].ToString()), aArgs[4].ToString(), aArgs[5].ToString(), aArgs[6].ToString(), aArgs[7].ToString(), aArgs[8].ToString(), aArgs[9].ToString(), aArgs[10].ToString(), int.Parse(aArgs[11].ToString()));
                    int i = 0;
                    strbTabla.Append("<table id='tblCatalogoDefi' style='cursor:pointer;width:950px;text-align:left' cellpadding='0' cellspacing='0' border='0'>" + (char)13);
                    strbTabla.Append("<colgroup>");
                    strbTabla.Append(" <col style='width:12%' />");
                    strbTabla.Append(" <col style='width:33%' />");
                    strbTabla.Append(" <col style='width:10%' />");
                    strbTabla.Append(" <col style='width:9%' />");
                    strbTabla.Append(" <col style='width:12%' />");
                    strbTabla.Append(" <col style='width:8%' />");
                    strbTabla.Append(" <col style='width:7%' />");
                    strbTabla.Append(" <col style='width:9%' />");
                    strbTabla.Append("</colgroup>");

                    while (dr_resultado.Read())
                    {
                        strbTabla.Append("<tr id='" + dr_resultado["ID"].ToString() + "/" + dr_resultado["ELIMINAR"].ToString() + "/" + dr_resultado["NOTIFICADOR"].ToString() + "'");
                        strbTabla.Append(" ondblclick='Det_Defi(this)'");
                        strbTabla.Append(" onclick='ms(this);btnElimDefi(this);'");

                        strbTabla.Append(" style='height:16px'");

                        int iEstado=int.Parse(dr_resultado["T044_ESTADO"].ToString());

                        if (iEstado == 0 || iEstado == 2 || iEstado == 4 || iEstado == 7 || iEstado == 9)
                        {
                            if (dr_resultado["NOTIFICADOR"].ToString() == Session["IDFICEPI"].ToString() && aArgs[7].ToString() != "A")
                                strbTabla.Append("class='verde'");
                            else if (aArgs[7].ToString() != "A")
                                strbTabla.Append("class='black'");
                        }
                        if (iEstado == 1 || iEstado == 3 || iEstado == 5 || iEstado == 6 || iEstado == 8 || iEstado == 13)
                        {
                            if (dr_resultado["COORDINADOR"].ToString() == Session["IDFICEPI"].ToString() && aArgs[7].ToString() != "A")
                                strbTabla.Append("class='verde'");
                            else if (Session["ESPECIALIS"].ToString() == "S" && iEstado == 8)
                                strbTabla.Append("class='verde'");
                            else if (dr_resultado["COORDINADOR"] == System.DBNull.Value && aArgs[8].ToString() == "1" && aArgs[7].ToString() != "A")
                                strbTabla.Append("class='verde'");
                            else if (aArgs[7].ToString() != "A")
                                strbTabla.Append("class='black'");
                        }
 
                        if (iEstado == 10 || iEstado == 11 || iEstado == 12)
                            strbTabla.Append("class='black'");
     
                        strbTabla.Append("'> ");
                        strbTabla.Append("<td align='right' style='padding-right: 35px'>" + int.Parse(dr_resultado["ID"].ToString()).ToString("#,###,##0") + "&nbsp;&nbsp;&nbsp;</td>");

                        strbTabla.Append("<td>&nbsp;&nbsp;<nobr style='width:355px;text-overflow:ellipsis;overflow:hidden;' TITLE='" + dr_resultado["ASUNTO"].ToString().Replace("'", "&#39;").Replace("\"", "&#34;") + "'>" + dr_resultado["ASUNTO"].ToString() + "</nobr></td>");

                        strbTabla.Append("<td>&nbsp;" + dr_resultado["IMPORTANCIA"].ToString() + "</td>");
                        strbTabla.Append("<td>&nbsp;" + dr_resultado["PRIORIDAD"].ToString() + "</td>");
                        strbTabla.Append("<td>&nbsp;" + dr_resultado["ESTADO"].ToString() + "</td>");

                        string strFecha;
                        if (dr_resultado["FECHA_NOTIFICACION"] == System.DBNull.Value)
                            strFecha = "";
                        else
                            strFecha = ((DateTime)dr_resultado["FECHA_NOTIFICACION"]).ToShortDateString();

                        strbTabla.Append("<td>&nbsp;" + strFecha + "</td>");

                        if (dr_resultado["FECHA_LIMITE"] == System.DBNull.Value)
                            strFecha = "";
                        else
                            strFecha = ((DateTime)dr_resultado["FECHA_LIMITE"]).ToShortDateString();

                        strbTabla.Append("<td>" + strFecha + "</td>");

                        if (dr_resultado["FECHA_PACTADA"] == System.DBNull.Value)
                            strFecha = "";
                        else
                            strFecha = ((DateTime)dr_resultado["FECHA_PACTADA"]).ToShortDateString();

                        strbTabla.Append("<td>" + strFecha + "</td>");

                        strbTabla.Append("</tr>" + (char)13);
                        //strbTabla += strFilas;
                        i = i + 1;
                    }

                    dr_resultado.Close();
                    dr_resultado.Dispose();

                    strbTabla.Append("</table>");
                    break;
                }
                catch (System.Exception objError)
                {
                    strbTabla.Append("N@@" + Errores.mostrarError("Error al leer catálogo de órdenes.", objError));
                }
                break;

            case "3":       // Eliminación de un área
                try
                {
                    Areas.Eliminar(int.Parse(aArgs[1].ToString()));
                }
                catch (System.Exception objError)
                {
                    strbTabla.Append("N@@" + Errores.mostrarError("Error al borrar un área.", objError));
                }

                break;
            case "4":       // Eliminación de una deficiencia
                try
                {
                    Deficiencias.Delete(null, int.Parse(aArgs[1].ToString()));
                }
                catch (System.Exception objError)
                {
                    strbTabla.Append("N@@" + Errores.mostrarError("Error al borrar una orden.", objError));
                }

                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        try
        {
            if (strbTabla.ToString().Substring(0, 1) != "N") _callbackResultado = aArgs[0] + "@@OK@@" + strbTabla.ToString();
            else _callbackResultado = aArgs[0] + "@@" + strbTabla.ToString();
        }
        catch
        {
            _callbackResultado = aArgs[0] + "@@OK"; //las actualizaciones insert
        }
    }

    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
}
