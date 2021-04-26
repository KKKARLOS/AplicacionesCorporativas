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

public partial class Capa_Presentacion_Administracion_Preventa_ParametrizacionPT_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, sErrores;
    //public SqlConnection oConn;
    //public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.TituloPagina = "Parametrización de Proyectos Técnicos de destino";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    //Catalogo("A", "", "");
                    cargarOC("A");
                    cargarArrayOC();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
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
            case ("oc"):
                sResultado += Catalogo(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("buscarPT"):
                sResultado += buscarPT(aArgs[1]);
                break;
            case ("buscarParametrizaciones"):
                sResultado += buscarParametrizaciones();
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
    private string Catalogo(string sTipo, string sOC, string sIdFicepi, string sMostrarProfBaja)
    {
        StringBuilder sb = new StringBuilder();
        string sRes = "";
        bool? bEstado;
        int? idOC = null;
        int? idFicepi = null;
        bool bMostrarProfBaja = false;
        string bloquear  = "";
        string disabled = "";

        if (sMostrarProfBaja == "S") bMostrarProfBaja = true;
        sb.Append("<table id='tblDatos' class='texto' style='width:990px; text-align:left;' mantenimiento='1' border='0'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:300px;'><col style='width:290px'><col style='width:250px;'><col style='width:70px;'><col style='width:60px;'></colgroup>");
        sb.Append("<tbody>");
        //Si sTipo=T no restrinjo por estado, sino saco solo las activas
        if (sTipo == "T") bEstado = null;
        else bEstado = true;
        //Puede haber restricción por Organizacion Comercial
        if (sOC != "") idOC = int.Parse(sOC);
        //Puede haber restricción por Profesional
        if (sIdFicepi != "") idFicepi = int.Parse(sIdFicepi);

        IB.SUPER.ADM.SIC.BLL.ParametrizacionDestinoPT oP = new IB.SUPER.ADM.SIC.BLL.ParametrizacionDestinoPT();
        try
        {
            List<IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT> oLista = oP.Catalogo(bEstado, idOC, idFicepi,bMostrarProfBaja);
            foreach (IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT oElem in oLista)
            {
                sb.Append("<tr bd='' idOC='" + oElem.ta212_idorganizacioncomercial.ToString() + "'");
                sb.Append(" idP='" + oElem.t001_idficepi_comercial + "' idPT='" + oElem.t331_idpt.ToString() + "'");
                sb.Append(" idPTOri='" + oElem.t331_idpt.ToString() + "'");
                sb.Append(" style='height:20px'>");//onkeydown='activarGrabar()'
                sb.Append("<td></td>");
                sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290'>" + oElem.denOC + "</span></td>");
                if (oElem.baja)
                    sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290' style='color:red;'>" + oElem.denProfesional + "</span></td>");
                else
                    sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290'>" + oElem.denProfesional + "</span></td>");
                sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290'>" + oElem.denPT + "</span></td>");
                sb.Append("<td><input type='checkbox' class='check MANO' style='width:15px;margin-left:25px;' /></td>");
                if (oElem.ta213_nocambioautomatico) bloquear = "checked='checked'";
                else bloquear = "";
                if (oElem.denPT == "")
                {
                    disabled = "disabled = 'disabled'";
                }
                else
                    disabled = "";

                    sb.Append("<td><input onclick='checkUpdated(this)' type='checkbox' " + bloquear +" "+ disabled +" class='check MANO' style='width:15px;margin-left:15px;' /></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtml = sb.ToString();

            sRes = "OK@#@" + strTablaHtml;
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar la parametrización de las organizaciones comerciales", ex);
            sRes = "ERROR@#@" + ex.Message;
            this.hdnMensajeError.Text = ex.Message;
        }
        finally
        {
            oP.Dispose();
            
        }
        return sRes;
    }

    private string buscarParametrizaciones()
    {
        StringBuilder sb = new StringBuilder();
        string sRes = "";
        bool? bEstado;
        int? idOC = null;
        int? idFicepi = null;
        bool bMostrarProfBaja = false;
        string bloquear = "";
        string disabled = "";
        
        sb.Append("<table id='tblDatos' class='texto' style='width:990px; text-align:left;' mantenimiento='1' border='0'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:300px;'><col style='width:290px'><col style='width:250px;'><col style='width:70px;'><col style='width:60px;'></colgroup>");
        sb.Append("<tbody>");        

        IB.SUPER.ADM.SIC.BLL.ParametrizacionDestinoPT oP = new IB.SUPER.ADM.SIC.BLL.ParametrizacionDestinoPT();
        try
        {
            List<IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT> oLista = oP.CatPrametrizaciones();
            foreach (IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT oElem in oLista)
            {
                sb.Append("<tr bd='' idOC='" + oElem.ta212_idorganizacioncomercial.ToString() + "'");
                sb.Append(" idP='" + oElem.t001_idficepi_comercial + "' idPT='" + oElem.t331_idpt.ToString() + "'");
                sb.Append(" idPTOri='" + oElem.t331_idpt.ToString() + "'");
                sb.Append(" style='height:20px'>");//onkeydown='activarGrabar()'
                sb.Append("<td></td>");
                sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290'>" + oElem.denOC + "</span></td>");
                if (oElem.baja)
                    sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290' style='color:red;'>" + oElem.denProfesional + "</span></td>");
                else
                    sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290'>" + oElem.denProfesional + "</span></td>");
                sb.Append("<td onmouseover='TTip(event)' style='width:310px;'><span class='NBR W290'>" + oElem.denPT + "</span></td>");
                sb.Append("<td><input type='checkbox' class='check MANO' style='width:15px;margin-left:25px;' /></td>");
                if (oElem.ta213_nocambioautomatico) bloquear = "checked='checked'";
                else bloquear = "";
                if (oElem.denPT == "")
                {
                    disabled = "disabled = 'disabled'";
                }
                else
                    disabled = "";

                sb.Append("<td><input onclick='checkUpdated(this)' type='checkbox' " + bloquear + " " + disabled + " class='check MANO' style='width:15px;margin-left:15px;' /></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtml = sb.ToString();

            sRes = "OK@#@" + strTablaHtml;
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar la parametrización de las organizaciones comerciales", ex);
            sRes = "ERROR@#@" + ex.Message;
            this.hdnMensajeError.Text = ex.Message;
        }
        finally
        {
            oP.Dispose();

        }
        return sRes;
    }
    private void cargarOC(string sTipo)
    {
        IB.SUPER.SIC.BLL.OrganizacionComercial oP = new IB.SUPER.SIC.BLL.OrganizacionComercial();
        try
        {
            bool bSoloActivos = false;
            if (sTipo == "A") bSoloActivos = true;

            //Cargar el combo de organizaciones comerciales
            ListItem oLI = null;
            oLI = new ListItem("", "-1");
            cboCR.Items.Add(oLI);
            oLI = new ListItem("TODAS", "");
            cboCR.Items.Add(oLI);

            List<IB.SUPER.SIC.Models.OrganizacionComercial> oLista = oP.Catalogo(bSoloActivos);
            foreach (IB.SUPER.SIC.Models.OrganizacionComercial oElem in oLista)
            {
                oLI = new ListItem(oElem.ta212_denominacion, oElem.ta212_idorganizacioncomercial.ToString());
                cboCR.Items.Add(oLI);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar las organizaciones comerciales", ex);
        }
        finally
        {
            oP.Dispose();
        }
    }
    private void cargarArrayOC()
    {
        IB.SUPER.SIC.BLL.OrganizacionComercial oP = new IB.SUPER.SIC.BLL.OrganizacionComercial();
        try
        {
            bool bSoloActivos = false;
            StringBuilder sbT = new StringBuilder();
            StringBuilder sbA = new StringBuilder();
            sbT.Append("@#@-1///TODAS@#@///");

            List<IB.SUPER.SIC.Models.OrganizacionComercial> oLista = oP.Catalogo(bSoloActivos);
            foreach (IB.SUPER.SIC.Models.OrganizacionComercial oElem in oLista)
            {
                sbT.Append(oElem.ta212_denominacion);
                sbT.Append("@#@");
                sbT.Append(oElem.ta212_idorganizacioncomercial.ToString());
                sbT.Append("///");

            }
            this.hdnOCtodas.Value = sbT.ToString();

            bSoloActivos = true;
            oLista = oP.Catalogo(bSoloActivos);
            sbA.Append("@#@-1///TODAS@#@///");
            foreach (IB.SUPER.SIC.Models.OrganizacionComercial oElem in oLista)
            {
                sbA.Append(oElem.ta212_denominacion);
                sbA.Append("@#@");
                sbA.Append(oElem.ta212_idorganizacioncomercial.ToString());
                sbA.Append("///");

            }
            this.hdnOCactivas.Value = sbA.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar las organizaciones comerciales", ex);
        }
        finally
        {
            oP.Dispose();
        }
    }

    protected string Grabar(string strDatos)
    {
        string sResul = "";
        int? idPT;
        IB.SUPER.ADM.SIC.BLL.ParametrizacionDestinoPT oLista = new IB.SUPER.ADM.SIC.BLL.ParametrizacionDestinoPT();
        try
        {
            #region Genero lista de objetos
            List<IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT> Catalogo = new List<IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT>();
            string[] aClase = Regex.Split(strDatos, "///");
            foreach (string oClase in aClase)
            {
                if (oClase == "") continue;
                string[] aValores = Regex.Split(oClase, "##");
                //0. Opcion BD. "I", "U", "D"
                //1. ID OC
                //2. ID Profesional
                //3. ID PT
                //4. Bloquear
                if (aValores[3] == "") idPT = null;
                else idPT = int.Parse(aValores[3]);

                IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT oElem = new IB.SUPER.ADM.SIC.Models.ParametrizacionDestinoPT();
                oElem.bd = aValores[0];
                oElem.ta212_idorganizacioncomercial = int.Parse(aValores[1]);
                oElem.t001_idficepi_comercial = int.Parse(aValores[2]);
                oElem.t331_idpt = idPT;
                oElem.ta213_nocambioautomatico = bool.Parse(aValores[4]);

                Catalogo.Add(oElem);
            }
            #endregion

            oLista.GrabarLista(Catalogo);
            
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar.", ex, false);
        }
        finally
        {
            oLista.Dispose();
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