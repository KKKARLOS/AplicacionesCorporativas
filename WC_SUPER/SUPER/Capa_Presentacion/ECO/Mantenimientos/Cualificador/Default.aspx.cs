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
using EO.Web;

using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string nNivel = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.bFuncionesLocales = true;
        Master.sbotonesOpcionOn = "4,71";
        Master.sbotonesOpcionOff = "4";
        txtDenominacion.Attributes.Add("readonly", "readonly");
        nNivel = Request.QueryString["nivel"].ToString();
        switch (nNivel)
        {
            case "0":
                Master.TituloPagina = "Cualificador de proyectos a nivel de "+Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                break;
            case "1":
                Master.TituloPagina = "Cualificador de proyectos a nivel de "+Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1);
                break; 
            case "2":
                Master.TituloPagina = "Cualificador de proyectos a nivel de "+Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2);
                break; 
            case "3":
                Master.TituloPagina = "Cualificador de proyectos a nivel de "+Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3);
                break; 
            case "4":
                Master.TituloPagina = "Cualificador de proyectos a nivel de "+Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4);
                break;            
        }

        Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");

        try
        {
            if (!Page.IsCallback)
            {
                switch (nNivel)
                {
                    case "0":
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                        break;
                    case "1":
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
                        break;
                    case "2":
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
                        break;
                    case "3":
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
                        break;
                    case "4":
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));
                        break;
                }

                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                {
                    cboEstructura.Visible = false;
                    hdnIdEstructura.Visible = true;
                    txtDesEstructura.Visible = true;
                }
                else
                {
                    cboEstructura.Visible = true;
                    hdnIdEstructura.Visible = false;
                    txtDesEstructura.Visible = false;
                    cargarEstructura();
                }
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

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
            case "getCualificadores":
                sResultado += getCualificadores(aArgs[1], aArgs[2]);
                break;
            case "grabar":
                sResultado += Grabar(aArgs[1], Utilidades.unescape(aArgs[2]), aArgs[3], aArgs[4]);
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

    private void cargarEstructura()
    {
        try
        {
            //Cargar el combo de nodos accesibles
            //cboEstructura.Items.Clear();
            //cboEstructura.Items.Add(new ListItem("", "0"));

            ListItem oLI = null;

            SqlDataReader dr = null;

            switch (nNivel)
            {
                case "0":
                    dr = NODO.ObtenerNodosUsuarioCualificadores(null, (int)Session["UsuarioActual"]);
                    break;
                case "1":
                    dr = SUPERNODO1.ObtenerSuperNodo1Usuario(null, (int)Session["UsuarioActual"]);
                    break;
                case "2":
                    dr = SUPERNODO2.ObtenerSuperNodo2Usuario(null, (int)Session["UsuarioActual"]);
                    break;
                case "3":
                    dr = SUPERNODO3.ObtenerSuperNodo3Usuario(null, (int)Session["UsuarioActual"]);
                    break;
                case "4":
                    dr = SUPERNODO4.ObtenerSuperNodo4Usuario(null, (int)Session["UsuarioActual"]);
                    break;
            }

            while (dr.Read())
            {
                switch (nNivel)
                {
                    case "0":
                        oLI = new ListItem(dr["t303_denominacion"].ToString(), dr["t303_idnodo"].ToString());
                        oLI.Attributes.Add("Q_DEN", Utilidades.escape(dr["t303_denominacion_CNP"].ToString()));
                        oLI.Attributes.Add("Q_OBL", Utilidades.escape(dr["t303_obligatorio_CNP"].ToString()));
                        break;
                    case "1":
                        oLI = new ListItem(dr["t391_denominacion"].ToString(), dr["t391_idsupernodo1"].ToString());
                        oLI.Attributes.Add("Q_DEN", Utilidades.escape(dr["t391_denominacion_CSN1P"].ToString()));
                        oLI.Attributes.Add("Q_OBL", Utilidades.escape(dr["t391_obligatorio_CSN1P"].ToString()));
                        break;
                    case "2":
                        oLI = new ListItem(dr["t392_denominacion"].ToString(), dr["t392_idsupernodo2"].ToString());
                        oLI.Attributes.Add("Q_DEN", Utilidades.escape(dr["t392_denominacion_CSN2P"].ToString()));
                        oLI.Attributes.Add("Q_OBL", Utilidades.escape(dr["t392_obligatorio_CSN2P"].ToString()));
                        break;
                    case "3":
                        oLI = new ListItem(dr["t393_denominacion"].ToString(), dr["t393_idsupernodo3"].ToString());
                        oLI.Attributes.Add("Q_DEN", Utilidades.escape(dr["t393_denominacion_CSN3P"].ToString()));
                        oLI.Attributes.Add("Q_OBL", Utilidades.escape(dr["t393_obligatorio_CSN3P"].ToString()));
                        break;
                    case "4":
                        oLI = new ListItem(dr["t394_denominacion"].ToString(), dr["t394_idsupernodo4"].ToString());
                        oLI.Attributes.Add("Q_DEN", Utilidades.escape(dr["t394_denominacion_CSN4P"].ToString()));
                        oLI.Attributes.Add("Q_OBL", Utilidades.escape(dr["t394_obligatorio_CSN4P"].ToString()));
                        break;
                }
                oLI.Attributes.Add("EDI", Utilidades.escape(dr["edicion"].ToString()));
                cboEstructura.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private string getCualificadores(string sEstructura, string sActivo)
    {
        StringBuilder sb = new StringBuilder();
        bool bActivo;
        string sId = "";
        string sOrden = "";
        bool sActi = false;
        string sDenominacion = "";
        try
        {
            if (sActivo == "1") bActivo = true;
            else bActivo = false;

            SqlDataReader dr = null;

            switch (nNivel)
            {
                case "0":
                    dr = NODO.ObtenerCualificadores((int)Session["UsuarioActual"], int.Parse(sEstructura), bActivo);
                    
                    break;
                case "1":
                    dr = SUPERNODO1.ObtenerCualificadores((int)Session["UsuarioActual"], int.Parse(sEstructura), bActivo);

                    break;
                case "2":
                    dr = SUPERNODO2.ObtenerCualificadores((int)Session["UsuarioActual"], int.Parse(sEstructura), bActivo);

                    break;
                case "3":
                    dr = SUPERNODO3.ObtenerCualificadores((int)Session["UsuarioActual"], int.Parse(sEstructura), bActivo);

                    break;
                case "4":
                    dr = SUPERNODO4.ObtenerCualificadores((int)Session["UsuarioActual"], int.Parse(sEstructura), bActivo);

                    break;
            }

            sb.Append("<table id='tblDatos' class='texto MA' style='width:400px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:360px;' /></colgroup>");
            sb.Append("<tbody id='tbodyCualificadores'>");
            while (dr.Read())
            {
                switch (nNivel)
                {
                    case "0":
                        sId = dr["t476_idcnp"].ToString();
                        sOrden = dr["t476_orden"].ToString();
                        sActi = (bool)dr["t476_activo"];
                        sDenominacion = dr["t476_denominacion"].ToString();

                        break;
                    case "1":
                        sId = dr["t485_idcsn1p"].ToString();
                        sOrden = dr["t485_orden"].ToString();
                        sActi = (bool)dr["t485_activo"];
                        //sActi = (dr["t485_activo"].ToString() == "1") ? true : false;
                        sDenominacion = dr["t485_denominacion"].ToString();

                        break;
                    case "2":
                        sId = dr["t487_idcsn2p"].ToString();
                        sOrden = dr["t487_orden"].ToString();
                        sActi = (bool)dr["t487_activo"];
                        sDenominacion = dr["t487_denominacion"].ToString();

                        break;
                    case "3":
                        sId = dr["t489_idcsn3p"].ToString();
                        sOrden = dr["t489_orden"].ToString();
                        sActi = (bool)dr["t489_activo"];

                        sDenominacion = dr["t489_denominacion"].ToString();

                        break;
                    case "4":
                        sId = dr["t491_idcsn4p"].ToString();
                        sOrden = dr["t491_orden"].ToString();
                        sActi = (bool)dr["t491_activo"];
                        sDenominacion = dr["t491_denominacion"].ToString();

                        break;
                }
                
                sb.Append("<tr id='" + sId + "' bd='' orden='" + sOrden + "' ");

                sb.Append("onclick='ms(this)' ondblclick='mdn(this)' style='height:20px;'>");
                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                sb.Append("<td><img src='../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");

                if (sActi) sb.Append("<td>");
                else sb.Append("<td style='padding-left:5px; color:gray;'>");
                sb.Append(sDenominacion + "</td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los valores de los cualificadores.", ex);
        }
    }
    private string Grabar(string sIdEstructura, string sDenominacion, string sObligatorio, string strDatos)
    {
        string sResul = "", sValoresInsertados = "";
        SqlConnection oConn=null;
        SqlTransaction tr;

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
            //Datos básicos
                    switch (nNivel)
                    {
                        case "0":
                            NODO.Update(tr,
                                int.Parse(sIdEstructura),
                                Utilidades.unescape(sDenominacion),
                                (sObligatorio == "1") ? true : false
                                );
                            break;
                        case "1":
                            SUPERNODO1.Update(tr,
                                int.Parse(sIdEstructura),
                                Utilidades.unescape(sDenominacion),
                                (sObligatorio == "1") ? true : false
                                );
                            break;
                        case "2":
                            SUPERNODO2.Update(tr,
                                int.Parse(sIdEstructura),
                                Utilidades.unescape(sDenominacion),
                                (sObligatorio == "1") ? true : false
                                );
                            break;
                        case "3":
                            SUPERNODO3.Update(tr,
                                int.Parse(sIdEstructura),
                                Utilidades.unescape(sDenominacion),
                                (sObligatorio == "1") ? true : false
                                );
                            break;
                        case "4":
                            SUPERNODO4.Update(tr,
                                int.Parse(sIdEstructura),
                                Utilidades.unescape(sDenominacion),
                                (sObligatorio == "1") ? true : false
                                );
                            break;
                    }

                    cargarEstructura();

            #region Estructura
            string[] aDatos = Regex.Split(strDatos, "///");

            foreach (string oEstructura in aDatos)
            {
                if (oEstructura == "") continue;
                string[] aEstructura = Regex.Split(oEstructura, "##");
                ///aEstructura[0] = Opcion BD. "I", "U", "D"
                ///aEstructura[1] = ID Estructura
                ///aEstructura[2] = Orden

                switch (aEstructura[0])
                {
                    case "U":                        
                        switch (nNivel)
                        {
                            case "0":
                                CDP.UpdateSimple(tr, int.Parse(aEstructura[1]), byte.Parse(aEstructura[2]));
                                break;
                            case "1":
                                CSN1P.UpdateSimple(tr, int.Parse(aEstructura[1]), byte.Parse(aEstructura[2]));
                                break;
                            case "2":
                                CSN2P.UpdateSimple(tr, int.Parse(aEstructura[1]), byte.Parse(aEstructura[2]));
                                break;
                            case "3":
                                CSN3P.UpdateSimple(tr, int.Parse(aEstructura[1]), byte.Parse(aEstructura[2]));
                                break;
                            case "4":
                                CSN4P.UpdateSimple(tr, int.Parse(aEstructura[1]), byte.Parse(aEstructura[2]));
                                break;
                        }
                        break;
                    case "D":                        
                        switch (nNivel)
                        {
                            case "0":
                                CDP.Delete(tr, int.Parse(aEstructura[1]));
                                break;
                            case "1":
                                CSN1P.Delete(tr, int.Parse(aEstructura[1]));
                                break;
                            case "2":
                                CSN2P.Delete(tr, int.Parse(aEstructura[1]));
                                break;
                            case "3":
                                CSN3P.Delete(tr, int.Parse(aEstructura[1]));
                                break;
                            case "4":
                                CSN4P.Delete(tr, int.Parse(aEstructura[1]));
                                break;
                        }
                        break;
                }
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            
            sResul = "OK@#@" + sValoresInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (Errores.EsErrorIntegridad(ex)) sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al grabar los valores", ex, false); //ex.Message;
            else sResul = "Error@#@" + Errores.mostrarError("Error al grabar los valores", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
