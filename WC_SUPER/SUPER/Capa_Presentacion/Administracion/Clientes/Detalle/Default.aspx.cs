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
using EO.Web;
using SUPER.Capa_Negocio;
using System.Collections.Generic;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sNodo = "";

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

                cargarCombosBaseCache();
                sNodo = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                // Leer cliente  
                hdnID.Text = Utilidades.decodpar(Request.QueryString["ID"].ToString());
                hdnOrigen.Value = Utilidades.decodpar(Request.QueryString["origen"].ToString());
                CLIENTE oCLIENTE = CLIENTE.Select(null, int.Parse(hdnID.Text));
                txtCodigoExterno.Text = oCLIENTE.t302_codigoexterno;
                txtDenominacion.Text = oCLIENTE.t302_denominacion;
                hdnIDResponsable.Text = oCLIENTE.t314_idusuario_responsable.ToString();
                txtDesResponsable.Text = oCLIENTE.RESPONSABLE;
                cboPaisGes.SelectedValue = (oCLIENTE.pais_gest.ToString()=="0")? "" : oCLIENTE.pais_gest.ToString();
                if (oCLIENTE.pais_gest.ToString() != "0")
                {
                    cargarCombosProvinciasGtonPais((int)oCLIENTE.pais_gest);
                    cboProvGes.SelectedValue = oCLIENTE.prov_gest.ToString();
                }

                cboPaisFis.SelectedValue = (oCLIENTE.pais_fiscal.ToString()=="0")? "" : oCLIENTE.pais_fiscal.ToString();
                if (oCLIENTE.pais_fiscal.ToString() != "0")
                {
                    cargarCombosProvinciasFisPais((int)oCLIENTE.pais_fiscal);
                    cboProvFis.SelectedValue = oCLIENTE.prov_fiscal.ToString();
                }

                cboSector.SelectedValue = oCLIENTE.cod_sector.ToString();
                cargarCombosSegmentosSector((int)oCLIENTE.cod_sector);
                cboSegmento.SelectedValue = oCLIENTE.cod_segmento.ToString();

                txtAmbito.Text = oCLIENTE.Ambito;
                txtZona.Text = oCLIENTE.Zona;
                chkAlertas.Checked = oCLIENTE.t302_noalertas;
                chkCualiCVT.Checked = oCLIENTE.t302_cualificacionCVT;

                //ObtenerNodosInvitados(hdnID.Text);

                if (hdnOrigen.Value == "MantFiguras")
                {
                    tsPestanas.SelectedIndex = 1;                
                    tsPestanas.Items[0].Disabled = true;
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de los clientes", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    private void cargarCombosProvinciasGtonPais(int iID)
    {
        ListItem oLI = null;
        SqlDataReader dr = SUPER.DAL.PAIS.Provincias(iID);
        while (dr.Read())
        {
            oLI = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
            oLI.Attributes.Add("ambito", dr["ambito"].ToString());
            oLI.Attributes.Add("zona", dr["zona"].ToString());
            cboProvGes.Items.Add(oLI);
        }
        cboProvGes.Items.Insert(0, new ListItem("", ""));
        dr.Close();
        dr.Dispose();

    }
    private void cargarCombosProvinciasFisPais(int iID)
    {
        cboProvFis.DataValueField = "identificador";
        cboProvFis.DataTextField = "denominacion";
        cboProvFis.DataSource = SUPER.DAL.PAIS.Provincias(iID);
        cboProvFis.DataBind();
        cboProvFis.Items.Insert(0, new ListItem("", ""));
    }
    private void cargarCombosSegmentosSector(int iID)
    {
        cboSegmento.DataValueField = "identificador";
        cboSegmento.DataTextField = "denominacion";
        cboSegmento.DataSource = SUPER.DAL.SECTOR.Segmentos(iID);
        cboSegmento.DataBind();
    }

    private void cargarCombosBaseCache()
    {
        //PAIS.CatalogoPaisesGesCache(cboPaisGes, "");
        //PAIS.CatalogoPaisesFisCache(cboPaisFis, "");
        //SECTOR.CatalogoSectoresCache(cboSector);
        cargarCboPaisesGes();
        cargarCboPaisesFis();
        cargarCboSectores();
    }
    private void cargarCboPaisesGes()
    {
        ListItem Elemento;
        List<PAIS> ListaPaisesGes = PAIS.ListaPaisesGes();

        foreach (PAIS oPais in ListaPaisesGes)
        {
            Elemento = new ListItem(oPais.t172_denominacion, oPais.t172_idpais.ToString());
            this.cboPaisGes.Items.Add(Elemento);
        }
        cboPaisGes.Items.Insert(0, new ListItem("", ""));
    }
    private void cargarCboPaisesFis()
    {
        ListItem Elemento;
        List<PAIS> ListaPaisesFis = PAIS.ListaPaisesFis();

        foreach (PAIS oPais in ListaPaisesFis)
        {
            Elemento = new ListItem(oPais.t172_denominacion, oPais.t172_idpais.ToString());
            this.cboPaisFis.Items.Add(Elemento);
        }
        cboPaisFis.Items.Insert(0, new ListItem("", ""));
    }
    private void cargarCboSectores()
    {
        ListItem Elemento;

        List<SECTOR> ListaSectores = SECTOR.ListaSectores();

        foreach (SECTOR oSector in ListaSectores)
        {
            Elemento = new ListItem(oSector.t483_denominacion, oSector.t483_idsector.ToString());
            this.cboSector.Items.Add(Elemento);
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Figuras
                        sResultado += obtenerFigurasItem(aArgs[1], aArgs[2]);
                        break;
                    case 2://Invitados
                        sResultado += obtenerInvitados(aArgs[1], aArgs[2]) + "///" + ObtenerNodosInvitados(aArgs[2]);
                        break;
                }
                break;
            case ("tecnicos"):
                sResultado += obtenerProfesionalesFigura(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("provinciasGtonPais"):
                sResultado += cargarProvinciasGtonPais(aArgs[1]);
                break;
            case ("provinciasFisPais"):
                sResultado += cargarProvinciasFisPais(aArgs[1]);
                break;
            case ("segmentosSector"):
                sResultado += cargarSegmentosSector(aArgs[1]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    private string cargarProvinciasGtonPais(string sID)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUPER.DAL.PAIS.Provincias(int.Parse(sID)); //Mostrar todos todos las provincias relacionadas a un país determinado

            while (dr.Read())
            {
                sb.Append(dr["identificador"].ToString() + "##" + dr["denominacion"].ToString() + "##" + dr["zona"].ToString() + "##" + dr["ambito"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener las provincias de gestión de un determinado país", ex);
        }
    }
    private string cargarProvinciasFisPais(string sID)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUPER.DAL.PAIS.Provincias(int.Parse(sID)); //Mostrar todos todos las provincias relacionadas a un país determinado

            while (dr.Read())
            {
                sb.Append(dr["identificador"].ToString() + "##" + dr["denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener las provincias fiscales de un determinado país", ex);
        }
    } 
    private string cargarSegmentosSector(string sID)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = SUPER.DAL.SECTOR.Segmentos(int.Parse(sID)); //Mostrar todos todos los segmentos relacionados a un determinado sector

            while (dr.Read())
            {
                sb.Append(dr["identificador"].ToString() + "##" + dr["denominacion"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "N@#@" + Errores.mostrarError("Error al obtener los segmentos de un determinado sector", ex);
        }
    } 
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string obtenerFigurasItem(string sPestana, string sIdCliente)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append(" aFigIni = new Array();");//\n
            int i = 0;

            SqlDataReader dr = FIGURACLIENTE.CatalogoFiguras(int.Parse(sIdCliente));
            sb.Append("<table id='tblFiguras2' class='MM' style='width:420px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width: 20px;' /><col style='width: 280px;' /><col style='width: 110px;' /></colgroup>");
            int nUsuario = 0;
            bool bHayFilas = false;
            while (dr.Read())
            {
                bHayFilas = true;
                sbuilder.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "sFig:\"" + dr["figura"].ToString() + "\"};");//\n
                i++;
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:20px;' onclick='mm(event)' onmousedown='DD(event);' ");

                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append("><td><img src='../../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //sb.Append("<img src='../../../../images/imgUsuIV.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../../images/imgUsuPV.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        //sb.Append("<img src='../../../../images/imgUsuIM.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../../images/imgUsuPM.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../../images/imgUsuEM.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFM.gif'>");
                                break;
                        }
                    }
                    sb.Append("</td><td><nobr class='NBR W280'>" + dr["Profesional"].ToString() + "</nobr></td>");

                    //Figuras
                    sb.Append("<td><div style='height:20px;'><ul id='box-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            if (bHayFilas)
            {
                sb.Append("</ul></div></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "///" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras.", ex);
        }
    }
    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1' class='texto' style='WIDTH: 400px;CURSOR: url(../../../../images/imgManoAzul2Move.cur),pointer;' cellSpacing='0' cellPadding='0' border='0' >");
            sb.Append("<colgroup><col style='width:20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:22px;' ");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");
                sb.Append("<td></td>");

                sb.Append("<td style='padding-left:5px;'>");
                sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string Grabar(string strDatosBasicos, string strFiguras, string strNodos, string sInvActual)
    {
        string sResul = "", sIdUser;
        int nID = -1;
        string[] aDatosBasicos = null;

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
            #region Datos Generales
            if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
            {
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = IDResponsable
                ///aDatosBasicos[1] = NoAlertas
                ///aDatosBasicos[2] = cualificacionCVT
                ///aDatosBasicos[3] = prov_gest
                ///aDatosBasicos[4] = prov_fiscal
                ///aDatosBasicos[5] = cod_segmento
                ///
                nID = int.Parse(aDatosBasicos[0]);

                CLIENTE.Update(tr,
                            int.Parse(hdnID.Text),
                            (aDatosBasicos[0] == "0") ? null : (int?)int.Parse(aDatosBasicos[0]),
                            (aDatosBasicos[1] == "1") ? true : false,
                            (aDatosBasicos[2] == "1") ? true : false,
                            (aDatosBasicos[3] == "") ? null : (int?)int.Parse(aDatosBasicos[3]),
                            (aDatosBasicos[4] == "") ? null : (int?)int.Parse(aDatosBasicos[4]),
                            int.Parse(aDatosBasicos[5])
                            );
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
                    {
                        FIGURACLIENTE.Delete(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]));
                    }
                    else
                    {
                        string[] aFiguras = Regex.Split(aFig[2], ",");
                        foreach (string oFigura in aFiguras)
                        {
                            if (oFigura == "") continue;
                            string[] aFig2 = Regex.Split(oFigura, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            if (aFig2[0] == "D")
                                FIGURACLIENTE.Delete(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]));
                            else
                                FIGURACLIENTE.Insert(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]), aFig2[1]);
                        }
                    }
                }
            }

            #endregion
            #region Datos Nodos de invitados
            if (strNodos != "")//No se ha modificado nada de la pestaña de nodos
            {
                string[] aElems = Regex.Split(strNodos, "/");
                foreach (string oElem in aElems)
                {
                    if (oElem == "") continue;
                    string[] aElem = Regex.Split(oElem, "#");
                    ///aElem[0] = bd
                    ///aElem[1] = idUsuario
                    ///aElem[2] = idNodo
                    sIdUser = aElem[1];
                    if (sIdUser != "")
                    {
                        if (aElem[0] == "D")
                            RESTRICCIONNODOFIGURACLIENTE.Delete(tr, int.Parse(hdnID.Text), int.Parse(sIdUser), int.Parse(aElem[2]));
                        else
                            RESTRICCIONNODOFIGURACLIENTE.Insert(tr, int.Parse(hdnID.Text), int.Parse(sIdUser), int.Parse(aElem[2]));
                    }
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            //sResul = "OK@#@" + nID.ToString("#,###");
            sResul = obtenerInvitados("1", hdnID.Text) + "@#@" + ObtenerNodosInvitados(hdnID.Text) + "@#@" + sInvActual;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del cliente", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string obtenerInvitados(string sPestana, string sIdCliente)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";

        sb.Append("<table id='tblInvitados' class='texto MM' style='WIDTH: 400px;'>");
        sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
        sb.Append("<tbody>");
        try
        {
            if (sIdCliente != "")
            {
                int nIdCliente = int.Parse(sIdCliente);
                SqlDataReader dr = FIGURACLIENTE.CatalogoInvitados(nIdCliente);
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px' id='" + dr["t314_idusuario"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_idnodo"].ToString() == "") sb.Append("tipo='E' ");
                    //else sb.Append("tipo='I' ");

                    sb.Append(" onclick='mm(event);RefrescarNodos(this.id);' ");
                    //sb.Append(" onmousedown='eventos(this);'");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append("><td></td><td style='padding-left:5px;'><nobr class='NBR W380'>" + dr["Profesional"].ToString() + "</nobr></td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody></table>");
            //strTablaInvitados = sb.ToString();
            sResul = "OK@#@" + sPestana + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de invitados.", ex);
        }
        return sResul;
    }
    private string ObtenerNodosInvitados(string sIdCliente)
    {
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aNODOS = new Array();\n");
        if (sIdCliente != "")
        {
            //SqlDataReader dr = VAE.CatalogoByUne(int.Parse(sNodo), sAmbito, null);
            SqlDataReader dr = FIGURACLIENTE.CatalogoNodos(int.Parse(sIdCliente));
            int i = 0;
            while (dr.Read())
            {
                sbuilder.Append("\taNODOS[" + i.ToString() + "] = {bd:\"\", " +
                                "idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "idNODO:\"" + dr["t303_idnodo"].ToString() + "\"," +
                                "nombre:\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\"};\n");
                i++;
            }
            dr.Close();
            dr.Dispose();
        }
        return sbuilder.ToString();
    }

}
