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
//Para usar List<>
using System.Collections.Generic;

using SUPER.Capa_Negocio;
using SUPER.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public short idUnidad = 0;
    public int idSubarea=0, idArea = 0, nIDItem = 0;
    public byte nNivel = 3;

    protected void Page_Load(object sender, EventArgs e)
    {
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
                if (Request.QueryString["subarea"] != "")
                {
                    idSubarea = int.Parse(Utilidades.decodpar(Request.QueryString["subarea"].ToString()));
                    CargarDatosItem(idSubarea);
                }
                else
                {
                    if (Request.QueryString["unidad"] != "")
                    {
                        idUnidad = short.Parse(Utilidades.decodpar(Request.QueryString["unidad"].ToString()));
                        CargarUnidad(idUnidad);
                    }
                    if (Request.QueryString["area"] != "")
                    {
                        idArea = int.Parse(Utilidades.decodpar(Request.QueryString["area"].ToString()));
                        CargarArea(idArea);
                    }
                }
                if (Utilidades.decodpar(Request.QueryString["origen"]) == "MantFiguras")
                {
                    tsPestanas.SelectedIndex = 1;
                    tsPestanas.Items[0].Disabled = true;
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del subárea de preventa", ex);
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
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Figuras
                        sResultado += obtenerFigurasItem(aArgs[1], aArgs[2], aArgs[3]);
                        break;
                }
                break;
            case ("tecnicos"):
                sResultado += obtenerProfesionalesFigura(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getResponsableArea"):
                sResultado += GetResponsableArea(int.Parse(aArgs[1]));
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

    private void CargarUnidad(short idUnidad)
    {
        IB.SUPER.ADM.SIC.Models.UnidadPreventa oUnidad = new IB.SUPER.ADM.SIC.Models.UnidadPreventa();
        IB.SUPER.ADM.SIC.BLL.UnidadPreventa oElem = new IB.SUPER.ADM.SIC.BLL.UnidadPreventa();
        oUnidad = oElem.Select(idUnidad);
        //PreventaUnidad oUnidad = PreventaUnidad.Select(tr, idUnidad);
        txtUnidad.Text = oUnidad.ta199_denominacion;
        hdnIdUnidad.Text = oUnidad.ta199_idunidadpreventa.ToString();

        oElem.Dispose();
    }
    private void CargarArea(int idArea)
    {
        //PreventaArea oArea = PreventaArea.Select(tr, idArea);
        IB.SUPER.ADM.SIC.Models.AreaPreventa oArea = new IB.SUPER.ADM.SIC.Models.AreaPreventa();
        IB.SUPER.ADM.SIC.BLL.AreaPreventa oElem = new IB.SUPER.ADM.SIC.BLL.AreaPreventa();
        oArea = oElem.Select2(idArea);
        txtArea.Text = oArea.ta200_denominacion;
        hdnIdArea.Text = oArea.ta200_idareapreventa.ToString();
        //Por defecto el responsable de un subarea es el del área
        if (this.hdnIDResponsable.Text=="")
        {
            this.hdnIDResponsable.Text = oArea.t001_idficepi_responsable.ToString();
            this.txtDesResponsable.Text = oArea.Responsable;
        }

        oElem.Dispose();
    }
    private void CargarDatosItem(int idSubarea)
    {
        //PreventaSubarea oSubarea = PreventaSubarea.Select(null, idSubarea);
        IB.SUPER.ADM.SIC.Models.SubareaPreventa oSubArea = new IB.SUPER.ADM.SIC.Models.SubareaPreventa();
        IB.SUPER.ADM.SIC.BLL.SubareaPreventa oElem = new IB.SUPER.ADM.SIC.BLL.SubareaPreventa();

        oSubArea = oElem.Select2(idSubarea);

        txtID.Text = idSubarea.ToString();
        
        txtUnidad.Text = oSubArea.ta199_denominacion;
        hdnIdUnidad.Text = oSubArea.ta199_idunidadpreventa.ToString();

        txtArea.Text = oSubArea.ta200_denominacion;
        hdnIdArea.Text = oSubArea.ta200_idareapreventa.ToString();


        txtDenominacion.Text = oSubArea.ta201_denominacion;
        hdnIDResponsable.Text = oSubArea.t001_idficepi_responsable.ToString();
        txtDesResponsable.Text = oSubArea.Responsable;

        if (oSubArea.ta201_estadoactiva) chkActivo.Checked = true;
        else chkActivo.Checked = false;

        //if (oSubArea.ta201_permitirautoasignacionlider == "A")
        if (oSubArea.ta201_permitirautoasignacionlider)
            chkLider.Checked = true;
        else
            chkLider.Checked = false;

        oElem.Dispose();
    }

    private string GetResponsableArea(int idArea)
    {
        string sRes = "";
        //PreventaArea oArea = PreventaArea.Select(tr, idArea);
        IB.SUPER.ADM.SIC.Models.AreaPreventa oArea = new IB.SUPER.ADM.SIC.Models.AreaPreventa();
        IB.SUPER.ADM.SIC.BLL.AreaPreventa oElem = new IB.SUPER.ADM.SIC.BLL.AreaPreventa();
        oArea = oElem.Select(idArea);

        sRes= "OK@#@" + oArea.t001_idficepi_responsable.ToString() + "@#@" + oArea.Responsable;

        oElem.Dispose();

        return sRes;
    }

    private string obtenerFigurasItem(string sPestana, string sNivel, string sIDItem)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        IB.SUPER.ADM.SIC.BLL.FiguraSubareaPreventa oFigura = new IB.SUPER.ADM.SIC.BLL.FiguraSubareaPreventa();
        try
        {
            //SqlDataReader dr = null;

            //switch (int.Parse(sNivel))
            //{
            //    case 3: //Subarea de Preventa
            //        dr = PreventaSubareaFiguras.CatalogoFiguras(int.Parse(sIDItem));
            //        break;
            //    default:
            //        dr = null;
            //        break;
            //}
            List<IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa> oLista = oFigura.Catalogo(int.Parse(sIDItem));

            sb.Append("<table id='tblFiguras2' class='texto MM' style='width: 420px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 20px' /><col style='width: 280px;' /><col style='width: 100px;' /></colgroup>");
            sb.Append("<tbody id='tbodyFiguras2'>");
            int nUsuario = 0;
            bool bHayFilas = false;
            foreach (IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa oElem in oLista)
            {
                bHayFilas = true;
                sbuilder.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + oElem.t001_idficepi + "\"," +
                                "sFig:\"" + oElem.ta203_figura + "\"};");
                i++;
                if (oElem.t001_idficepi != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr id='" + oElem.t001_idficepi.ToString() + "' bd='' style='height:22px;' onclick='mm(event)' onmousedown='DD(event);' ");

                    sb.Append("><td><img src='../../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (oElem.sexo == "V")
                    {
                        switch (oElem.tipoProf)
                        {
                            case "I":
                                sb.Append("<img src='../../../../images/imgUsuPV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFV.gif'>");
                                break;
                            default:
                                sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        switch (oElem.tipoProf)
                        {
                            case "I":
                                sb.Append("<img src='../../../../images/imgUsuPV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFV.gif'>");
                                break;
                            default:
                                sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                                break;
                        }
                    }
                    sb.Append("</td><td><span class='NBR W275' ");
                    //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + oElem.profesional.Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append(">" + oElem.profesional + "</span></td>");

                    //Figuras
                    sb.Append("<td><div><ul id='box-" + oElem.t001_idficepi.ToString() + "'>");
                    //sb.Append("<td><div style='height:22px;'><ul id='box-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (oElem.ta203_figura)
                    {
                        case "D": sb.Append("<li id='D' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "L": sb.Append("<li id='L' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgUsuLider.gif' title='Posible líder' /></li>"); break;
                    }

                    nUsuario = oElem.t001_idficepi;
                }
                else
                {
                    switch (oElem.ta203_figura)
                    {
                        case "D": sb.Append("<li id='D' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "L": sb.Append("<li id='L' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgUsuLider.gif' title='Posible líder' /></li>"); break;
                    }
                }
            }
            if (bHayFilas)
            {
                sb.Append("</ul></div></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "///" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras.", ex);
        }
        finally
        {
            oFigura.Dispose();
        }
    }
    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1' class='texto MAM' style='width: 400px;'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' style='height:22px;' ");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");

                sb.Append("<td></td>");

                sb.Append("<td style='padding-left:5px;'>");
                sb.Append("<span ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W375'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string Grabar(string strDatosBasicos, string strFiguras)
    {
        string sResul = "";
        int nID = -1;
        IB.SUPER.ADM.SIC.BLL.SubareaPreventa oElem = new IB.SUPER.ADM.SIC.BLL.SubareaPreventa();

        try
        {
            nID = oElem.Grabar(strDatosBasicos, strFiguras);
            if (nID == -1) sResul = "AVISO@#@Ya existe un subárea con la misma denominación";
            else sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la subárea", ex, false);
        }
        finally
        {
            oElem.Dispose();
        }
        return sResul;
    }
}
