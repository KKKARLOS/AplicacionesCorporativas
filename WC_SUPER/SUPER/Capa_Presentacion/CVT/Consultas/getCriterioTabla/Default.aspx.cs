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
using SUPER.Capa_Negocio;
using SUPER.BLL;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_CVT_Consultas_getCriterio_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    public string sErrores = "", sTitulo = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["caso"] != null)
            hdnCaso.Value = Request.QueryString["caso"];

        hdnIdTipo.Value = Request.QueryString["nTipo"].ToString();
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                strTablaHTML = "<TABLE id='tblDatos' style='WIDTH: 350px;' class='texto MAM' cellSpacing='0' cellspacing='0' border='0'><colgroup><col style='width:350px;' /></colgroup></TABLE>";

                if (int.Parse(hdnIdTipo.Value) == 27 || int.Parse(hdnIdTipo.Value) == 28)
                {
                    ambAp.Style.Add("display", "");
                    
                    //if (int.Parse(hdnIdTipo.Value) == 2) ambIconosResp.Style.Add("display", "");
                    //else 
                        ambIconosResp.Style.Add("display", "none");
                    ambDenominacion.Style.Add("display", "none");
                    rdbTipo.Style.Add("display", "none");
                    if (int.Parse(hdnIdTipo.Value) == 28) ambBajas.Style.Add("display", "none");
                    else
                    {
                        if ((bool)Session["CVCONSULTABAJA"])
                            ambBajas.Style.Add("display", "");
                        else
                            ambBajas.Style.Add("display", "none");
                    }
                }
                else
                {
                    ambAp.Style.Add("display", "none");
                    ambBajas.Style.Add("display", "none");
                    ambIconosResp.Style.Add("display", "none");
                    ambDenominacion.Style.Add("display", "");
                    rdbTipo.Style.Add("display", "");
                }


                sTitulo = "Selección de criterio: ";
                switch (int.Parse(hdnIdTipo.Value))
                {
                    case 1: sTitulo += "Unidad de Negocio"; break;
                    case 2: sTitulo += "Centro de Responsabilidad"; break;
                    case 3:
                    case 12: sTitulo += "Perfil"; break;
                    case 4: 
                    case 41: sTitulo += "Título Académico"; break;
                    case 5:
                    case 17:
                    case 171:
                    case 11: sTitulo += "Entorno Tecnológico"; break;
                    case 61:
                    case 6: sTitulo += "Certificado"; break;
                    case 71:
                    case 7: sTitulo += "Idioma"; break;
                    case 8: sTitulo += "Titulación Idiomas"; break;
                    case 9: sTitulo += "Área Conocimiento Sectorial"; break;
                    case 10: sTitulo += "Área Conocimiento Tecnológico"; break;
                    case 13: sTitulo += "Sector"; break;
                    case 141:
                    case 14: sTitulo += "Curso"; break;
                    case 151:
                    case 15: sTitulo += "Entidad certificadora"; break;
                    case 161:
                    case 16: sTitulo += "Perfil en experiencia profesional"; break;
                    case 27:
                    case 28: sTitulo += "Profesional"; break;
                }

                this.Title = sTitulo;

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
            case "TipoConcepto":
                int tipo = int.Parse(aArgs[1]);
                switch (tipo)
                {
                    //case 11:
                    //    tipo = 5; 
                    //    break;
                    //case 12:
                    //    tipo = 3; 
                    //    break;
                    //case 13:
                    //case 14:
                    //case 15:
                    //    tipo = int.Parse(aArgs[1]) - 2;
                    //    break;
                    case 41://Titulación académica
                        tipo = 4;
                        break;
                    case 51://Entornos tecnologicos
                    case 17:
                    case 171:
                        tipo = 5;
                        break;
                    case 61://Certificados
                        tipo = 6;
                        break;
                    case 71://Idiomas
                        tipo = 7;
                        break;
                    case 141://Cursos
                        tipo = 14;
                        break;
                    case 151://Entidades certificadoras
                        tipo = 15;
                        break;
                    case 16://perfil en experiencia profesional
                    case 161:
                        tipo = 3;
                        break;
                }
                sResultado += Curriculum.ObtenerTipoConcepto(tipo, aArgs[2].ToString(), Utilidades.unescape(aArgs[3].ToString()));
                break;
            case ("Profesionales"):
                sResultado += getProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("Evaluados"):
                sResultado += getProfesionalesEvaluados(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
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
    
    private string getProfesionales(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        int idFicepiAnt = -1, idFicepiAct = -1;

        sb.Append("<table id='tblDatos' class='texto' style='width:350px;'>");
        sb.Append("<colgroup><col style='width:350px;' /></colgroup><tbody>");
        try
        {
            //SqlDataReader dr = USUARIO.ObtenerProfesionalesFicepi(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
            //                                                      (sMostrarBajas == "1") ? true : false, true, null);
            SqlDataReader dr = USUARIO.ObtenerProfesionalesCVT(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()),
                                                                Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), 
                                                                Utilidades.unescape(sNombre),
                                                                (sMostrarBajas == "1") ? true : false, true, null);

            while (dr.Read())
            {
                //sb.Append("<tr id='" + dr["idusuario"].ToString() + "' ");
                idFicepiAct = int.Parse(dr["t001_idficepi"].ToString());
                if (idFicepiAct != idFicepiAnt)
                {
                    sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");

                    sb.Append("tipo ='" + dr["tipo"].ToString() + "' ");
                    sb.Append("sexo ='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("baja ='" + dr["baja"].ToString() + "' ");
                    sb.Append("vis ='" + dr["vision"].ToString() + "' ");

                    sb.Append("cr='" + dr["t303_denominacion"].ToString() + "' ");
                    sb.Append("empresa='" + dr["EMPRESA"].ToString() + "' ");
                    sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' ");

                    if (dr["vision"].ToString() == "1")
                    {
                        sb.Append(" class='MAM' onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'>");
                        sb.Append("<td style='noWrap:true; padding-left:3px;' onclick='mm(event)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                    }
                    else
                    {
                        sb.Append(" class='MANO' onclick='msgNoVision()' >");
                        sb.Append("<td style='noWrap:true; padding-left:3px; color:gray;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");
                    }
                    sb.Append("</tr>");
                    idFicepiAnt = idFicepiAct;
                }
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los profesionales.", ex);
        }
        return sResul;
    }

    private string getProfesionalesEvaluados(string sAp1, string sAp2, string sNombre, string sMostrarBajas)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' class='texto' style='width:350px;'>");
        sb.Append("<colgroup><col style='width:350px;' /></colgroup><tbody>");
        try
        {
            SqlDataReader dr = USUARIO.ObtenerProfesionalesEvaluados(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                                                  int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()));

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' ");

                sb.Append("tipo ='" + dr["tipo"].ToString() + "' ");
                sb.Append("sexo ='" + dr["t001_sexo"].ToString() + "' ");
                sb.Append("baja ='" + dr["baja"].ToString() + "' ");

                sb.Append("cr='" + dr["t303_denominacion"].ToString() + "' ");
                sb.Append("empresa='" + dr["EMPRESA"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' style='height:20px;' ");


                sb.Append(" class='MAM' onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)'>");
                sb.Append("<td style='noWrap:true; padding-left:3px;' onclick='mm(event)' ondblclick='insertarItem(this.parentNode)' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["profesional"].ToString() + "</td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody></table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los profesionales.", ex);
        }
        return sResul;
    }
    
    

}
