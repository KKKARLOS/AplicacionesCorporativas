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
//Para usar List<>
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;
using SUPER.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public short idUnidad = 0;
    public int idArea = 0, nIDItem = 0;
    public byte nNivel = 2;
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
                if (Request.QueryString["unidad"] != "")
                {
                    idUnidad = short.Parse(Utilidades.decodpar(Request.QueryString["unidad"].ToString()));
                    CargarDatosEstructura(idUnidad);
                }
                if (Request.QueryString["area"] != "")
                {
                    idArea = int.Parse(Utilidades.decodpar(Request.QueryString["area"].ToString()));
                    CargarDatosItem(idArea);
                }

                if (Utilidades.decodpar(Request.QueryString["origen"]) == "MantFiguras")
                {
                    tsPestanas.SelectedIndex = 1;
                    tsPestanas.Items[0].Disabled = true;
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos del ára de preventa", ex);
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
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("buscarPT"):
                sResultado += buscarPT(aArgs[1]);
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
    private void CargarDatosEstructura(short idUnidad)
    {
        IB.SUPER.ADM.SIC.Models.UnidadPreventa oUnidad = new IB.SUPER.ADM.SIC.Models.UnidadPreventa();
        IB.SUPER.ADM.SIC.BLL.UnidadPreventa oElem = new IB.SUPER.ADM.SIC.BLL.UnidadPreventa();
        oUnidad = oElem.Select(idUnidad);
        //PreventaUnidad oUnidad = PreventaUnidad.Select(tr, idUnidad);
        txtUnidad.Text = oUnidad.ta199_denominacion;
        hdnIdUnidad.Text = oUnidad.ta199_idunidadpreventa.ToString();

        oElem.Dispose();
    }
    private void CargarDatosItem(int idArea)
    {
        IB.SUPER.ADM.SIC.Models.AreaPreventa oArea = new IB.SUPER.ADM.SIC.Models.AreaPreventa();
        IB.SUPER.ADM.SIC.BLL.AreaPreventa oElem = new IB.SUPER.ADM.SIC.BLL.AreaPreventa();

        oArea = oElem.Select2(idArea);
        //PreventaArea oArea = PreventaArea.Select(null, idArea);
        txtID.Text = idArea.ToString();
        txtDenominacion.Text = oArea.ta200_denominacion;
        hdnIDResponsable.Text = oArea.t001_idficepi_responsable.ToString();
        txtDesResponsable.Text = oArea.Responsable;
        if (oArea.ta200_estadoactiva) chkActivo.Checked = true;
        else chkActivo.Checked = false;

        oElem.Dispose();

        #region Datos del PT
        if (oArea.t331_idpt != null)
        {
            IB.SUPER.SIC.Models.ProyectoTecnico oPT = new IB.SUPER.SIC.Models.ProyectoTecnico();
            IB.SUPER.SIC.BLL.ProyectoTecnico oElemPT = new IB.SUPER.SIC.BLL.ProyectoTecnico();
            oPT = oElemPT.Select((int)oArea.t331_idpt);
            this.hdnIDPT.Value = oPT.t331_idpt.ToString();
            this.txtNumPT.Text = oPT.t331_idpt.ToString("#,###");
            this.txtPT.Text = oPT.t331_despt;
            this.hdnT305IdProy.Value = oPT.t305_idproyectosubnodo.ToString();
            this.txtNumPE.Text = oPT.t301_idproyecto.ToString("#,###");
            this.txtPE.Text = oPT.t301_denominacion;

            oElemPT.Dispose();
        }
        #endregion
    }

    private string obtenerFigurasItem(string sPestana, string sNivel, string sIDItem)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        IB.SUPER.ADM.SIC.BLL.FiguraAreaPreventa oFigura = new IB.SUPER.ADM.SIC.BLL.FiguraAreaPreventa();
        try
        {
            //SqlDataReader dr = null;
            //switch (int.Parse(sNivel))
            //{
            //    case 2: //Area de Preventa
            //        dr = PreventaAreaFiguras.CatalogoFiguras(int.Parse(sIDItem));
            //        break;
            //    default:
            //        dr = null;
            //        break;
            //}

            List<IB.SUPER.ADM.SIC.Models.FiguraAreaPreventa> oLista = oFigura.Catalogo(int.Parse(sIDItem));

            sb.Append("<table id='tblFiguras2' class='texto MM' style='width: 420px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 20px' /><col style='width: 280px;' /><col style='width: 100px;' /></colgroup>");
            sb.Append("<tbody id='tbodyFiguras2'>");
            int nUsuario = 0;
            bool bHayFilas = false;
            foreach (IB.SUPER.ADM.SIC.Models.FiguraAreaPreventa oElem in oLista)
            {
                bHayFilas = true;
                sbuilder.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + oElem.t001_idficepi + "\"," +
                                "sFig:\"" + oElem.ta202_figura + "\"};");
                i++;
                if (oElem.t001_idficepi != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr id='" + oElem.t001_idficepi + "' bd='' style='height:22px;' onclick='mm(event)' onmousedown='DD(event);' ");

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
                    sb.Append("<td><div><ul id='box-" + oElem.t001_idficepi + "'>");

                    switch (oElem.ta202_figura)
                    {
                        case "D": sb.Append("<li id='D' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = oElem.t001_idficepi;
                }
                else
                {
                    switch (oElem.ta202_figura)
                    {
                        case "D": sb.Append("<li id='D' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='C' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + oElem.orden.ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
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
        string[] aDatosBasicos = null;
        IB.SUPER.ADM.SIC.Models.AreaPreventa oArea = new IB.SUPER.ADM.SIC.Models.AreaPreventa();
        IB.SUPER.ADM.SIC.Models.AreaPreventa oAreaD = new IB.SUPER.ADM.SIC.Models.AreaPreventa();
        IB.SUPER.ADM.SIC.BLL.AreaPreventa oElem = new IB.SUPER.ADM.SIC.BLL.AreaPreventa();

        IB.SUPER.ADM.SIC.Models.FiguraAreaPreventa oFigura = new IB.SUPER.ADM.SIC.Models.FiguraAreaPreventa();
        IB.SUPER.ADM.SIC.BLL.FiguraAreaPreventa oElemFig = new IB.SUPER.ADM.SIC.BLL.FiguraAreaPreventa();
        try
        {
            #region Datos Generales

            if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
            {
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = ID
                ///aDatosBasicos[1] = Denominacion
                ///aDatosBasicos[2] = IDResponsable
                ///aDatosBasicos[3] = Activo
                ///aDatosBasicos[4] = IdPadre
                ///aDatosBasicos[5] = IdPT
                oArea.ta199_idunidadpreventa = short.Parse(aDatosBasicos[4]);
                oArea.ta200_denominacion = aDatosBasicos[1];
                oArea.ta200_estadoactiva = (aDatosBasicos[3] == "1") ? true : false;
                oArea.t001_idficepi_responsable = int.Parse(aDatosBasicos[2]);
                if (aDatosBasicos[5] != "")
                    oArea.t331_idpt = int.Parse(aDatosBasicos[5]);

                oAreaD = oElem.SelectPorDenominacion(oArea.ta200_denominacion);                              

                if (aDatosBasicos[0] == "") //insert          
                {            
                    if(oAreaD != null) return "AVISO2@#@Ya existe un área con la misma denominación"; //throw new Exception("Ya existe un área con la misma denominación");
                    nID = oElem.Insert(oArea);
                }
                else //update
                {
                    nID = int.Parse(aDatosBasicos[0]);
                    if (oAreaD != null && oAreaD.ta200_idareapreventa != nID) return "AVISO2@#@Ya existe un área con la misma denominación";
                    oArea.ta200_idareapreventa = nID;
                    oElem.Update(oArea);
                }
            }

            #endregion

            #region Datos Figuras
            if (strFiguras != "")//No se ha modificado nada de la pestaña de Figuras
            {
                string[] aUsuarios = Regex.Split(strFiguras, "///");
                foreach (string oUsuario in aUsuarios)
                {
                    if (oUsuario == "") continue;
                    string[] aFig = Regex.Split(oUsuario, "##");
                    ///aFig[0] = bd
                    ///aFig[1] = idUsuario
                    ///aFig[2] = Figuras

                    if (aFig[0] == "D")
                        //PreventaAreaFiguras.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                        oElemFig.Delete(nID, int.Parse(aFig[1]));
                    else
                    {
                        string[] aFiguras = Regex.Split(aFig[2], ",");
                        foreach (string oFig in aFiguras)
                        {
                            if (oFig == "") continue;
                            string[] aFig2 = Regex.Split(oFig, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            oFigura.ta200_idareapreventa = nID;
                            oFigura.t001_idficepi = int.Parse(aFig[1]);
                            oFigura.ta202_figura=aFig2[1];
                            if (aFig2[0] == "D")
                                //PreventaAreaFiguras.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                oElemFig.DeleteFigura(nID, int.Parse(aFig[1]), aFig2[1]);
                            else
                                //PreventaAreaFiguras.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                oElemFig.Insert(oFigura);
                        }
                    }
                }
            }
            #endregion

            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos.", ex, false);
        }
        finally
        {
            oElem.Dispose();
            oElemFig.Dispose();
        }
        return sResul;
    }

    private string buscarPE(string t301_idproyecto)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.GetInstanciaContratante(null, int.Parse(t301_idproyecto));
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");
                sb.Append(dr["t301_denominacion"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }
    private string buscarPT(string t331_idpt)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            ProyTec o = ProyTec.Obtener(int.Parse(t331_idpt));
            if (o.t331_idpt.ToString() != "")
            {
                sb.Append(o.t305_idproyectosubnodo.ToString() + "@#@");
                sb.Append(o.num_proyecto.ToString() + "@#@");
                sb.Append(o.nom_proyecto + "@#@");
                sb.Append(o.t331_despt);
            }

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "AVISO@#@El proyecto técnico no existe o no está bajo tu ámbito de visión";
        }
    }

}
