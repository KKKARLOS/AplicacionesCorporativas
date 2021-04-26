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
//
using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

//Para el StreamReader
using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores="", sAux="";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public StringBuilder sb = new StringBuilder();
    public StringBuilder sbE = new StringBuilder();
    public Hashtable htEmpEnlaceSAP, htProyEnlaceSAP, htCliEnlaceSAP;
    public EmpFactSAP oEmpFactSAP = null;
    public ProyFactSAP oProyFactSAP = null;
    public CliFactSAP oCliFactSAP = null;
    public int iCont = 0, iNumOk = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            sErrores = "";
            Master.nBotonera = 43;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Importación de facturas de SAP";

            if (!Page.IsPostBack)
            {
                try
                {
                    //this.txtAnno.Text = DateTime.Now.Year.ToString();
                    //Obtengo el nº de registros de la tabla T445_INTERFACTSAP
                    this.hdnNumfacts.Value = INTERFACTSAP.numFacturas(null).ToString();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al cargar el número de filas en INTERFACTSAP.", ex);
                }
            }

            try{
                cldFilasIFS.InnerText = int.Parse(this.hdnNumfacts.Value).ToString("#,##0");
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al recuperar el número de filas en INTERFACTSAP.", ex);
            }


            HttpPostedFile selectedFile = this.uplTheFile.PostedFile;
            if (selectedFile != null)
                Validar(selectedFile);

            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
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
                sResultado += Procesar(aArgs[1], aArgs[2]);
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
    private void Validar(HttpPostedFile selectedFile)
    {
        //StringBuilder sbF = new StringBuilder();
        try
        {
            #region Obtenión de dataset con empresas, proyectos y clientes y creación de HASTABLES
            DataSet ds = INTERFACTSAP.EmpresasProyectos();
            oEmpFactSAP = null;
            oProyFactSAP = null;
            oCliFactSAP = null;
            htEmpEnlaceSAP = new Hashtable();
            foreach (DataRow oEmpresa in ds.Tables[0].Rows)//Recorro tabla de empresas
            {
                try
                {
                    htEmpEnlaceSAP.Add(oEmpresa["t302_codigoexterno"].ToString(), new EmpFactSAP((int)oEmpresa["t313_idempresa"],
                                                                        oEmpresa["t313_denominacion"].ToString(),
                                                                        oEmpresa["t302_codigoexterno"].ToString()
                                                                        //,(bool)oEmpresa["t313_ute"]
                                                                    )
                                                         );
                }
                catch (Exception ex) 
                {
                    sErrores = Errores.mostrarError("Error al existir en la tabla 'T313_EMPRESA' diferentes empresas con el mismo código externo ", ex);                
                };
            }
            //oEmpFactSAP = (EmpFactSAP)htEmpEnlaceSAP["IB01"];

            htProyEnlaceSAP = new Hashtable();
            foreach (DataRow oProy in ds.Tables[1].Rows)//Recorro tabla de proyectos
            {
                htProyEnlaceSAP.Add((int)oProy["t301_idproyecto"], new ProyFactSAP((int)oProy["t301_idproyecto"],
                                                                        (int)oProy["t305_idproyectosubnodo"]));
            }
            //oProyFactSAP = (ProyFactSAP)htProyEnlaceSAP[20801];

            htCliEnlaceSAP = new Hashtable();
            foreach (DataRow oCliente in ds.Tables[2].Rows)//Recorro tabla de clientes
            {
                try
                {
                    htCliEnlaceSAP.Add(oCliente["t302_codigoexterno"], new CliFactSAP((int)oCliente["t302_idcliente"],
                                                                        oCliente["t302_codigoexterno"].ToString(),
                                                                        ((int)oCliente["Interno"] == 1)? true : false)
                                                         );
                }
                catch (Exception ex) 
                {
                    sErrores = Errores.mostrarError("Error al existir en la tabla 'T302_CLIENTE' diferentes clientes con el mismo código externo ", ex);                
                };
            }
            //oCliFactSAP = (CliFactSAP)htCliEnlaceSAP[45946];

            ds.Dispose();
            #endregion

            if (selectedFile.ContentLength != 0)
            {
                string sFichero = selectedFile.FileName;
                //Grabo el archivo en base de datos
                byte[] ArchivoEnBinario = new Byte[0];
                ArchivoEnBinario = new Byte[selectedFile.ContentLength]; //Crear el array de bytes con la longitud del archivo
                selectedFile.InputStream.Read(ArchivoEnBinario, 0, selectedFile.ContentLength); //Forzar al control del archivo a cargar los datos en el array

                int iRows = FICHEROSMANIOBRA.Update(null, Constantes.FicheroFacturasSAP, "Fichero origen de INTERFACTSAP", ArchivoEnBinario);
                if (iRows == 0)
                {
                    sErrores = "No existe entrada asociada a este proceso en el fichero de Maniobra";
                    return;
                }

                selectedFile.InputStream.Position = 0;
                StreamReader r = new StreamReader(selectedFile.InputStream, System.Text.Encoding.UTF7);
                FactSAP oFact = null;
                while (r.Peek() > -1)
                {
                    iCont++;
                    //if (iCont==909)
                    //{
                    //    string sAux = "KK";
                    //}
                    //lin = r.ReadLine();
                    //oFact = FactSAP.getFactura(lin);
                    oFact = validarFactura(FactSAP.getFactura(r.ReadLine()));
                    if (!validarCampos(oFact, this.txtAnioMes.Text, true)) continue;

                    iNumOk++;
                }
            }
            this.divB.InnerHtml = cabErrores() + sbE + "</table>";
            cldFacProc.InnerText = iCont.ToString("#,##0");
            cldFacOK.InnerText = iNumOk.ToString("#,##0");
            cldFacErr.InnerText = (iCont - iNumOk).ToString("#,##0");
            this.hdnIniciado.Value = "T";
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("El fichero no tiene el formato requerido para el proceso.\nRevisa la línea " + iCont.ToString(), ex);
        }
    }
    private string Procesar(string sAnomes, string sCadena)
    {//En sCadena tenemos los nº de linea de facturas erróneas
        string sResul = "", lin = "", sLin = "";
        int iNumLin = 1;
        byte[] linea = new byte[103];
        try
        {
            #region Obtenión de dataset con empresas, proyectos y clientes y creación de HASTABLES
            DataSet ds = INTERFACTSAP.EmpresasProyectos();

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
            foreach (DataRow oProy in ds.Tables[1].Rows)//Recorro tabla de proyectos
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

            #region Abro transaccion
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion
            //Leo el fichero de base de datos
            FICHEROSMANIOBRA oFic = FICHEROSMANIOBRA.Select(tr, Constantes.FicheroFacturasSAP);
            if (oFic.t447_fichero.Length > 0)
            {
                //Borrar contenido de T445_INTERFACTSAP
                INTERFACTSAP.Borrar(tr);

                #region Leer fichero de BBDD
                MemoryStream mstr = new MemoryStream(oFic.t447_fichero);
                mstr.Seek(0, SeekOrigin.Begin);
                int count = 0;
                byte[] byteArray = new byte[mstr.Length];
                while (count < mstr.Length)
                {
                    byteArray[count++] = System.Convert.ToByte(mstr.ReadByte());
                }
                lin = FromASCIIByteArray(byteArray);
                #endregion

                string[] aArgs = Regex.Split(lin, "\r\n");
                FactSAP oFact = null;
                //int nInserts = 0;
                //Recorrer lista de facturas e insertar en T445_INTERFACTSAP las que cuyo nº de línea no está en la lista de errores
                for (int iLinea = 0; iLinea < aArgs.Length - 1; iLinea++)
                {
                    if (aArgs[iLinea] != "")
                    {
                        sLin = aArgs[iLinea];
                        iNumLin = iLinea + 1;
                        if (sCadena.IndexOf("##" + iNumLin.ToString() + "##") == -1)
                        {
                            //oFact = FactSAP.getFactura(sLin);
                            oFact = validarFactura(FactSAP.getFactura(sLin));
                            if (!validarCampos(oFact, sAnomes, false)) continue;
//, oFact.ute
                            INTERFACTSAP.Insert(tr, oFact.iCodEmpresa, oFact.iCodCliente, oFact.t305_idproyectosubnodo,
                                                oFact.grupo, oFact.serie.Replace(".",""), oFact.iNumero, oFact.dtFecFact,
                                                oFact.dImpFfact, oFact.moneda, oFact.descri, oFact.refCliente);
                            //nInserts++;
                        }
                    }
                }
            }
            sResul = "OK@#@" + INTERFACTSAP.numFacturas(tr).ToString("#,##0");
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            //Errores.mostrarError("Error al tramitar el fichero", ex);
            sResul = "Error@#@" + Errores.mostrarError("Error al tramitar el fichero en la línea " + iNumLin.ToString() + " : " + sLin, ex);
        }
        finally
        {
            Conexion.CerrarTransaccion(tr);
        }
        return sResul;
    }

    private string cabFacturas()
    {
        return "<table id='tblFact' style='WIDTH: 780px;'><colgroup><col style='width:20px' /><col style='width:60px' /><col style='width:40px' /><col style='width:60px;text-align:center;' /><col style='width:200px' /><col style='width:300px' /><col style='width:100px;text-align:right;' /></colgroup>";

    }
    private string cabErrores()
    {
        return "<table id='tblErrores' style='WIDTH: 750px;'><colgroup><col style='width:60px' /><col style='width:40px' /><col style='width:60px;' /><col style='width:590px;' /></colgroup>";

    }
    private string ponerFilaError(FactSAP oFact, string sMens)
    {
        try
        {
            sb.Length = 0;
            sb.Append("<tr id=" + iCont.ToString() + "><td>");
            sb.Append(oFact.dtFecFact.ToShortDateString());
            sb.Append("</td><td>");
            sb.Append(oFact.serie);
            sb.Append("</td><td style='text-align:right;'>");
            sb.Append(oFact.iNumero.ToString());
            sb.Append("</td><td style='padding-left:5px;' >");
            sb.Append(sMens);
            sb.Append("</td></tr>");
        }
        catch (Exception ex)
        {
            sb.Append("Error@#@" + Errores.mostrarError("Error al ponerFilaError", ex));
        }
        return sb.ToString();
    }

    private string FromASCIIByteArray(byte[] characters)
    {
        //ASCIIEncoding encoding = new ASCIIEncoding();
        UTF7Encoding encoding = new UTF7Encoding();
        //UTF8Encoding encoding = new UTF8Encoding();
        //UTF32Encoding encoding = new UTF32Encoding();
        //UnicodeEncoding encoding = new UnicodeEncoding();
        string constructedString = encoding.GetString(characters);
        return (constructedString);
    }

    private FactSAP validarFactura(FactSAP oFact)
    {
        oEmpFactSAP = (EmpFactSAP)htEmpEnlaceSAP[oFact.cod_empresa]; //cod_empresa --> código externo
        if (oEmpFactSAP != null)
        {
            oFact.iCodEmpresa = oEmpFactSAP.t313_idempresa;
            //oFact.ute = oEmpFactSAP.t313_ute;
        }
        oCliFactSAP = (CliFactSAP)htCliEnlaceSAP[oFact.cod_cliente];
        if (oCliFactSAP != null)
        {
            oFact.grupo = oCliFactSAP.t302_interno;
            oFact.iCodCliente = oCliFactSAP.t302_idcliente;
        }
        oProyFactSAP = (ProyFactSAP)htProyEnlaceSAP[int.Parse(oFact.num_proyecto)];
        if (oProyFactSAP != null)
        {
            oFact.iNumProyecto = oProyFactSAP.t301_idproyecto;
            oFact.t305_idproyectosubnodo = oProyFactSAP.t305_idproyectosubnodo;
        }
        if (Utilidades.isNumeric(oFact.numero))
        {
            oFact.iNumero = System.Convert.ToInt32(oFact.numero);
        }
        else
        {
            oFact.iNumero = -1;
        }
        if (Utilidades.isNumeric(oFact.num_proyecto))
        {
            oFact.iNumProyecto = System.Convert.ToInt32(oFact.num_proyecto);
        }
        if (Utilidades.isNumeric(oFact.imp_fact))
        {
            oFact.dImpFfact = System.Convert.ToDecimal(oFact.imp_fact);
        }
        if (Utilidades.isNumeric(oFact.fec_fact) && oFact.fec_fact.Length == 8)
        {
            sAux = oFact.fec_fact.Substring(0, 2) + "/" + oFact.fec_fact.Substring(2, 2) + "/" + oFact.fec_fact.Substring(4, 4);
            if (Utilidades.isDate(sAux))
                oFact.dtFecFact = System.Convert.ToDateTime(sAux);
        }
        //if (oFact.serie.Length == 5)
        //{
        //    oFact.serie = oFact.serie.Substring(0, 1) + "." + oFact.serie.Substring(1, 2) + "." + oFact.serie.Substring(3, 2);
        //}
        return oFact;
    }
    private bool validarCampos(FactSAP oFact, string sAnomes, bool bEscribir)
    {
        if ((ProyFactSAP)htProyEnlaceSAP[oFact.iNumProyecto] == null)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Proyecto no existente o histórico (" + oFact.iNumProyecto + ")"));
            return false;
        }
        if (oFact.serie == "")
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Serie de factura vacía"));
            return false;
        }
        if (oFact.iCodEmpresa == 0)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Empresa incorrecta (" + oFact.cod_empresa + ")"));
            return false;
        }
        if (oFact.iCodCliente == 0)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Cliente incorrecto (" + oFact.cod_cliente + ")"));
            return false;
        }
        if (oFact.iNumero == -1)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Número de factura no numérico (" + oFact.numero + ")"));
            return false;
        }
        if (!Utilidades.isDate(oFact.dtFecFact.ToString()))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Fecha de factura incorrecta(" + oFact.fec_fact + ")"));
            return false;
        }
        if (oFact.dtFecFact.Year != int.Parse(sAnomes.Substring(0, 4)))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Año de factura (" + oFact.dtFecFact.Year.ToString() + ") fuera de rango de proceso"));
            return false;
        }
        if (oFact.dtFecFact.Month != int.Parse(sAnomes.Substring(4, 2)))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Mes de factura (" + oFact.dtFecFact.Month.ToString() + ") fuera de rango de proceso"));
            return false;
        }
        if (oFact.moneda != "EUR")
        {
            if (bEscribir) sbE.Append(ponerFilaError(oFact, "Unidad monetaria (" + oFact.moneda + ") desconocida"));
            return false;
        }
        return true;
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
                sb.Append("<td>"+ dr["t313_idempresa"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t302_idcliente"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t305_idproyectosubnodo"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t301_idproyecto"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t445_grupo"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t445_serie"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t445_numero"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t445_fec_fact"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t445_imp_fact"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t445_moneda"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t445_descri"].ToString() +"</td>");
                sb.Append("<td>"+ dr["t301_estado"].ToString() +"</td>");
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

