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


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public StringBuilder sbE = new StringBuilder();
    public Hashtable htEmpEnlaceSAP, htProyEnlaceSAP, htCliEnlaceSAP;
    public EmpFactSAP oEmpFactSAP = null;
    public ProyFactSAP oProyFactSAP = null;
    public CliFactSAP oCliFactSAP = null;
    public int iCont = 0, iNumOk = 0;
    public string sFechaFacIncorrecta = "false", sAnomes = "";
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 43;
            Master.TituloPagina = "Paso de facturas de T445_INTERFACTSAP a datos económicos";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    CargarFacturas();
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("procesar"):
                sResultado += procesar(aArgs[1], aArgs[2]);
                break;
            case ("mostrarIFS"):
                sResultado += mostrarIFS();
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private void CargarFacturas()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblErrores' style='WIDTH: 680px;'>");
        sb.Append("<colgroup><col style='width:60px' /><col style='width:40px' /><col style='width:60px;' /><col style='width:520px;' /></colgroup>");

        #region Obtenión de dataset con empresas, proyectos y clientes y creación de HASTABLES
        DataSet ds = INTERFACTSAP.EmpresasProyectos();
        oEmpFactSAP = null;
        oProyFactSAP = null;
        oCliFactSAP = null;

        htEmpEnlaceSAP = new Hashtable();
        foreach (DataRow oEmpresa in ds.Tables[0].Rows)//Recorro tabla de empresas
        {
            htEmpEnlaceSAP.Add(oEmpresa["t302_codigoexterno"].ToString(), new EmpFactSAP((int)oEmpresa["t313_idempresa"],
                                                                    oEmpresa["t313_denominacion"].ToString(),
                                                                    oEmpresa["t302_codigoexterno"].ToString()
                                                                    //,(bool)oEmpresa["t313_ute"]
                                                                    )
                                                     );
        }
        //oEmpFactSAP = (EmpFactSAP)htEmpEnlaceSAP["IB01"];

        htProyEnlaceSAP = new Hashtable();
        foreach (DataRow oProy in ds.Tables[1].Rows)//Recorro tabla de empresas
        {
            htProyEnlaceSAP.Add((int)oProy["t301_idproyecto"], new ProyFactSAP((int)oProy["t301_idproyecto"],
                                                                    (int)oProy["t305_idproyectosubnodo"])
                                                     );
        }
        //oProyFactSAP = (ProyFactSAP)htProyEnlaceSAP[20801];

        htCliEnlaceSAP = new Hashtable();
        foreach (DataRow oCliente in ds.Tables[2].Rows)//Recorro tabla de clientes
        {
            htCliEnlaceSAP.Add(oCliente["t302_codigoexterno"], new CliFactSAP((int)oCliente["t302_idcliente"],
                                                                    oCliente["t302_codigoexterno"].ToString(),
                                                                    ((int)oCliente["Interno"] == 1) ? true : false)
                                                     );
        }
        //oCliFactSAP = (CliFactSAP)htCliEnlaceSAP[45946];

        ds.Dispose();
        #endregion

        SqlDataReader dr = INTERFACTSAP.Catalogo();
        while (dr.Read())
        {
            if (iCont == 0)
            {
                sAnomes = Fechas.FechaAAnnomes((DateTime)dr["t445_fec_fact"]).ToString();
                cldFecha.InnerText = Fechas.AnnomesAFechaDescLarga(int.Parse(sAnomes));
            }
            iCont++;
            if (validarCampos(dr, sAnomes, true)) iNumOk++;
            if (sFechaFacIncorrecta=="true")
            {
                sbE.Length=0;
                iCont=0;
                iNumOk=0;
                cldFecha.InnerText = "Fecha indefinida";
                break;
            }
        }
        dr.Close();
        dr.Dispose();

        sb.Append("</table>");
        this.divB.InnerHtml = cabErrores() + sbE + "</table>";
        cldTotalFac.InnerText = INTERFACTSAP.numFacturas(null).ToString("#,##0");
        cldFacOK.InnerText = iNumOk.ToString("#,##0");
        cldFacErr.InnerText = (iCont - iNumOk).ToString("#,##0");
    }

    private bool validarCampos(SqlDataReader dr, string sAnomes, bool bEscribir)
    {
        if ((ProyFactSAP)htProyEnlaceSAP[(int)dr["t301_idproyecto"]] == null)
        {
            if (bEscribir) sbE.Append(ponerFilaError(dr, "Proyectosubnodo (" + dr["t305_idproyectosubnodo"].ToString() + ") no contratante, o pertenece a proyecto histórico"));
            return false;
        }
        if (dr["t445_serie"].ToString() == "")
        {
            if (bEscribir) sbE.Append(ponerFilaError(dr, "Serie de factura vacía"));
            return false;
        }
        if (!Utilidades.isDate(dr["t445_fec_fact"].ToString()))
        {
            if (bEscribir) sbE.Append(ponerFilaError(dr, "Fecha de factura incorrecta(" + dr["t445_fec_fact"].ToString() + ")"));
            return false;
        }
        if (((DateTime)dr["t445_fec_fact"]).Year != int.Parse(sAnomes.Substring(0, 4)))
        {
            if (bEscribir) sbE.Append(ponerFilaError(dr, "Año de factura (" + ((DateTime)dr["t445_fec_fact"]).ToString() + ") fuera de rango de proceso"));
            return false;
        }
        if (((DateTime)dr["t445_fec_fact"]).Month != int.Parse(sAnomes.Substring(4, 2)))
        {
            sFechaFacIncorrecta = "true";
            if (bEscribir) sbE.Append(ponerFilaError(dr, "Mes de factura (" + ((DateTime)dr["t445_fec_fact"]).ToString() + ") fuera de rango de proceso"));
            return false;
        }
        if (dr["t445_moneda"].ToString() != "EUR")
        {
            if (bEscribir) sbE.Append(ponerFilaError(dr, "Unidad monetaria (" + dr["t445_moneda"].ToString() + ") desconocida"));
            return false;
        }
        return true;
    }
    protected string procesar(string sAnomesProceso, string sCadena)
    {
        string sResul = "OK@#@";
        string sEstadoMes = "";
        int iNumLin = 1, iAnomes;
        int it325_idsegmesproy, iClaseEco;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
        }
        #endregion
        try
        {
            SqlDataReader dr = INTERFACTSAP.Catalogo();
            iAnomes = int.Parse(sAnomesProceso);
            ArrayList slProyPP = new ArrayList();

            while (dr.Read())
            {
                if (sCadena.IndexOf("##" + iNumLin.ToString() + "##") == -1)
                {
                    it325_idsegmesproy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, (int)dr["t305_idproyectosubnodo"], iAnomes);
                    if (it325_idsegmesproy == 0)
                    {
                        sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, (int)dr["t305_idproyectosubnodo"], iAnomes);
                        it325_idsegmesproy = SEGMESPROYECTOSUBNODO.Insert(tr, (int)dr["t305_idproyectosubnodo"], iAnomes, sEstadoMes, 0, 0, false, 0, 0);
                    }

                    if ((bool)dr["t445_grupo"])
                    {
                        //if ((bool)dr["t445_ute"]) iClaseEco = Constantes.IngExtServProf;
                        //else iClaseEco = Constantes.IngExtServProfGrupo;
                        iClaseEco = Constantes.IngExtServProfGrupo;
                    }
                    else
                        iClaseEco = Constantes.IngExtServProf;

                    DATOECO.InsertFactura(tr, it325_idsegmesproy, iClaseEco,
                                   dr["t445_descri"].ToString(),
                                   decimal.Parse(dr["t445_imp_fact"].ToString()),
                                   null,
                                   null,
                                   DateTime.Parse(dr["t445_fec_fact"].ToString()),
                                   dr["t445_serie"].ToString(),
                                   int.Parse(dr["t445_numero"].ToString()),
                                   int.Parse(dr["t313_idempresa"].ToString()),
                                   int.Parse(dr["t302_idcliente"].ToString()),
                                   Constantes.FicheroFacturasSAP,
                                   dr["t445_refcliente"].ToString());

                    INTERFACTSAP.Delete(tr, (int)dr["t445_id"]);
                    if (dr["t301_estado"].ToString() == "P")
                    {
                        ponerProyPP(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###"), slProyPP);
                    }
                }
                iNumLin++;
            }
            dr.Close();
            dr.Dispose();

            Conexion.CommitTransaccion(tr);
            //sResul = "OK@#@";
            //sResul = ObtenerTiposAsunto("3", "0");
            //genero cadena con los proyectos en estado Presupuestado
            foreach (string sProy in slProyPP)
                sResul += sProy + "##";

            sResul += "@#@" + cabErrores() + "</table>@#@"+ INTERFACTSAP.numFacturas(null).ToString("#,##0");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private void ponerProyPP(string sIdProy, ArrayList slLista)
    {
        try
        {
            bool bEncontrado = false;
            if (slLista != null)
            {
                foreach (string s in slLista)
                {
                    if (s == sIdProy)
                    {
                        bEncontrado = true;
                        break;
                    }
                }
            }
            if (!bEncontrado)
                slLista.Add(sIdProy);
        }
        catch (Exception ex)
        {
            //sResul = "Error@#@" + Errores.mostrarError("Error al poner el proyecto " + sIdProy + " en la lista de proyectos presupuestados", ex);
           Errores.mostrarError("Error al poner el proyecto " + sIdProy + " en la lista de proyectos presupuestados", ex);
        }
    }

    private string ponerFilaError(SqlDataReader dr, string sMens)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<tr style='height:16px' id=" + iCont.ToString() + "><td>");
            sb.Append(int.Parse(dr["t445_id"].ToString()).ToString("#,##0"));
            sb.Append("</td><td>");
            sb.Append(dr["t445_fec_fact"].ToString().Substring(0, 10));
            sb.Append("</td><td>");
            sb.Append(dr["t445_serie"].ToString());
            sb.Append("</td><td align='center'>");
            sb.Append(dr["t445_numero"].ToString());
            sb.Append("</td><td>");
            sb.Append(sMens);
            sb.Append("</td></tr>");
        }
        catch (Exception ex)
        {
            sb.Append("Error@#@" + Errores.mostrarError("Error al ponerFilaError", ex));
        }
        return sb.ToString();
    }
    private string cabErrores()
    {
        return "<table id='tblErrores' class='texto' style='WIDTH: 680px;'><colgroup><col style='width:60px' /><col style='width:40px' /><col style='width:60px;' /><col style='width:520px;' /></colgroup>";

    }
    private string ponerFilaError(DateTime dtFecFact, string sSerie, int iNumero, string sMens)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.Append("<tr><td>");
            sb.Append(dtFecFact.ToShortDateString());
            sb.Append("</td><td>");
            sb.Append(sSerie);
            sb.Append("</td><td style='text-align:right;'>");
            sb.Append(iNumero.ToString("#,###"));
            sb.Append("</td><td>");
            sb.Append(sMens);
            sb.Append("</td></tr>");
        }
        catch (Exception ex)
        {
            sb.Append("Error@#@" + Errores.mostrarError("Error al ponerFilaError", ex));
        }
        return sb.ToString();
    }

    protected string mostrarIFS()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = INTERFACTSAP.Catalogo();

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<tbody>");
            sb.Append("<tr align='center'>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>idEmpresa</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>idCliente</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>idProyectosubnodo</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>idProyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Grupo</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Serie</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Numero</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Fec_fact</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Moneda</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Motivo</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Estado</td>");
            sb.Append("</tr>");

            while (dr.Read())
            {
                sb.Append("<tr>");
                sb.Append("<td>" + dr["t313_idempresa"].ToString() + "</td>");
                sb.Append("<td>" + dr["t302_idcliente"].ToString() + "</td>");
                sb.Append("<td>" + dr["t305_idproyectosubnodo"].ToString() + "</td>");
                sb.Append("<td>" + dr["t301_idproyecto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t445_grupo"].ToString() + "</td>");
                sb.Append("<td>" + dr["t445_serie"].ToString() + "</td>");
                sb.Append("<td>" + dr["t445_numero"].ToString() + "</td>");
                sb.Append("<td>" + dr["t445_fec_fact"].ToString() + "</td>");
                sb.Append("<td>" + dr["t445_imp_fact"].ToString() + "</td>");
                sb.Append("<td>" + dr["t445_moneda"].ToString() + "</td>");
                sb.Append("<td>" + dr["t445_descri"].ToString() + "</td>");
                sb.Append("<td>" + dr["t301_estado"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); 
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de INTERFACTSAP.", ex);
        }
    }
}