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


public partial class Capa_Presentacion_Administracion_AccesoUsuarios_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaRecursos, strTablaDesactivados;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            //Master.nBotonera = 4;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
            //Poniendo el siguiente atributo a true, se incluye el fichero javascript propio
            //de la carpeta hija "Functions/funciones.js"
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Acceso de usuarios";
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            if (!Page.IsPostBack)
            {
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}
                strTablaRecursos = "<table id='tblRelacion'></table>";
                strTablaDesactivados = "<table id='tblAsignados'></table>";
                ObtenerUsuariosDesactivados();
                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
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
            case "grabar":
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case "grabarMasivo":
                sResultado += GrabarMasivo(aArgs[1]);
                break;
            case ("tecnicos"):
                sResultado += ObtenerTecnicos(aArgs[1], aArgs[2], aArgs[3]);
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
    protected string ObtenerTecnicos(string strValor1, string strValor2, string strValor3)
    {
        string sResul = "", sV1, sV2, sV3;
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr;

        try
        {
            sV1 = Utilidades.unescape(strValor1);
            sV2 = Utilidades.unescape(strValor2);
            sV3 = Utilidades.unescape(strValor3);

            dr = USUARIO.GetProfAdm(sV1, sV2, sV3, false, null);

            sb.Append("<table id='tblRelacion' class='texto' style='WIDTH: 480px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:420px;' /><col style='width:40px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' ");
                //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                //sb.Append("baja='" + dr["baja"].ToString() + "' ");

                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' >" +
                          "<td></td><td><nobr class='NBR W410'>" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><input type='checkbox' style='width:15x' class='check MANO' onclick='insertarRecurso(this);' ");
                if ((bool)dr["t314_accesohabilitado"]) sb.Append("checked=true");
                sb.Append("></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }

        return sResul;
    }
    private string ObtenerUsuariosDesactivados()
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";

        sb.Append("<table id='tblAsignados' style='WIDTH: 430px;'>");
        sb.Append("<colgroup><col style='width:20px' /><col style='width:410px' /></colgroup>");
        try
        {
            SqlDataReader dr = USUARIO.GetProfActivos(false);
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;noWrap:true;' id='" + dr["t314_idusuario"].ToString() + "' onclick='mm(event)' ");
                //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                //sb.Append("baja='" + dr["baja"].ToString() + "' ");
                sb.Append("b='' ");

                //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                //else sb.Append("tipo='P' ");
                sb.Append("tipo='" + dr["tipo"].ToString() + "' ");

                sb.Append("><td></td><td><nobr class='NBR W390'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            strTablaDesactivados = sb.ToString();
            sResul = sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales desactivados.", ex);
        }
        return sResul;
    }

    protected string GrabarMasivo(string strDatos)
    {
        string sResul = "", sUser="";
        try
        {
            if (strDatos != "")
            {
                string[] aUser = Regex.Split(strDatos, "///");
                for (int i = 0; i < aUser.Length - 1; i++)
                {
                    sUser = aUser[i];
                    if (sUser !="")
                        USUARIO.UpdateAcceso(int.Parse(sUser), true);
                }
            }
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
        }
        return sResul;
    }
    protected string Grabar(string sIdUser, string sActivo)
    {
        string sResul = "";

        try
        {
            USUARIO.UpdateAcceso(int.Parse(sIdUser), (sActivo == "T") ? true : false);

            sResul = "OK@#@" + sActivo + "@#@" + sIdUser;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
        }

        return sResul;
    }

}
