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
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public StringBuilder sb = new StringBuilder();
    public StringBuilder sbE = new StringBuilder();

    public Hashtable htProveedor, htProyectoSubNodo, htClaseEconomica, htNodoDestino, htMoneda;

    public Proveedor oProveedor = null;
    public ProyectoSubNodo oProyectoSubNodo = null;
    public ClaseEconomica oClaseEconomica = null;
    public NodoDestino oNodoDestino = null;
    public Moneda oMoneda = null;

    public int iCont = 0, iNumOk = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            sErrores = "";
            Master.nBotonera = 43;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Carga datos en DATOECO desde fichero";

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
                sResultado += Procesar();
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private void CargarArrayHT()
    {
        #region Obtenión de dataset con empresas, proyectos y clientes y nodos y creación de HASTABLES

        oProveedor = null;
        oProyectoSubNodo = null;
        oClaseEconomica = null;
        oNodoDestino = null;
        oMoneda = null;

        DataSet ds = AddDATAECO.ValidarFichero();
        htProveedor = new Hashtable();
        foreach (DataRow dsProveedor in ds.Tables[0].Rows) //Recorro tabla de proveedores
        {
            htProveedor.Add(dsProveedor["t315_codigoexterno"].ToString(), new Proveedor((int)dsProveedor["t315_idproveedor"],
                            dsProveedor["t315_codigoexterno"].ToString())
                            );
        }

        htProyectoSubNodo = new Hashtable();
        foreach (DataRow dsProyectoSubNodo in ds.Tables[1].Rows)//Recorro tabla de proyectos-subnodos 
        {
            htProyectoSubNodo.Add(dsProyectoSubNodo["t301_idproyecto"].ToString() + @"/" + dsProyectoSubNodo["t303_idnodo"].ToString(), new ProyectoSubNodo((int)dsProyectoSubNodo["t301_idproyecto"], (int)dsProyectoSubNodo["t305_idproyectosubnodo"],
                                 (int)dsProyectoSubNodo["t303_idnodo"], dsProyectoSubNodo["t305_cualidad"].ToString())
                                 );
        }

        htClaseEconomica = new Hashtable();
        foreach (DataRow dsClaseEconomica in ds.Tables[2].Rows)//Recorro tabla de Clases económicas
        {
            htClaseEconomica.Add(dsClaseEconomica["t329_idclaseeco"].ToString(), new ClaseEconomica((int)dsClaseEconomica["t329_idclaseeco"],
                                 dsClaseEconomica["t329_necesidad"].ToString(), bool.Parse(dsClaseEconomica["t329_visiblecarruselC"].ToString()), bool.Parse(dsClaseEconomica["t329_visiblecarruselJ"].ToString()), bool.Parse(dsClaseEconomica["t329_visiblecarruselP"].ToString()))
                                 );
        }

        htNodoDestino = new Hashtable();
        foreach (DataRow dsNodoDestino in ds.Tables[3].Rows)//Recorro tabla de Nodos
        {
            htNodoDestino.Add(dsNodoDestino["t303_idnodo"].ToString(), new NodoDestino((int)dsNodoDestino["t303_idnodo"])
                                 );
        }

        htMoneda = new Hashtable();
        foreach (DataRow dsMoneda in ds.Tables[4].Rows)//Recorro tabla de Monedas
        {
            htMoneda.Add(dsMoneda["t422_idmoneda"].ToString(), new Moneda(dsMoneda["t422_idmoneda"].ToString())
                                 );
        }

        ds.Dispose();
        #endregion
    }
    private void Validar(HttpPostedFile selectedFile)
    {
        //StringBuilder sbF = new StringBuilder();
        try
        {
            CargarArrayHT();

            iCont = 0;
            iNumOk = 0;
            if (selectedFile.ContentLength != 0)
            {
                string sFichero = selectedFile.FileName;
                //Grabo el archivo en base de datos
                byte[] ArchivoEnBinario = new Byte[0];
                ArchivoEnBinario = new Byte[selectedFile.ContentLength]; //Crear el array de bytes con la longitud del archivo
                selectedFile.InputStream.Read(ArchivoEnBinario, 0, selectedFile.ContentLength); //Forzar al control del archivo a cargar los datos en el array

                int iRows = FICHEROSMANIOBRA.Update(null, Constantes.FicheroDatos, "Fichero origen de CARGA DE DATOS", ArchivoEnBinario);
                if (iRows == 0)
                {
                    sErrores = "No existe entrada asociada a este proceso en el fichero de Maniobra";
                    return;
                }
                selectedFile.InputStream.Position = 0;
                StreamReader r = new StreamReader(selectedFile.InputStream, System.Text.Encoding.UTF7);
                //StreamReader r = new StreamReader(selectedFile.InputStream, System.Text.Encoding.Unicode);
                DesdeFichero oDesdeFichero = null;

                String strLinea = null;
                while ((strLinea = r.ReadLine()) != "")
                {
                    if (strLinea == null) break;
                    iCont++;
                    oDesdeFichero = validarLinea(DesdeFichero.getFila(strLinea));
                    if (!validarCampos(oDesdeFichero, true)) continue;
                    iNumOk++;
                }
            }
            this.divB.InnerHtml = cabErrores() + sbE + "</table>";
            cldLinProc.InnerText = iCont.ToString("#,##0");
            cldLinOK.InnerText = iNumOk.ToString("#,##0");
            cldLinErr.InnerText = (iCont - iNumOk).ToString("#,##0");
            this.hdnIniciado.Value = "T";

        }
        catch (Exception)
        {
            sErrores = "El fichero no tiene el formato requerido para el proceso";
        }
    }
    private string Procesar()
    {
        string sResul = "", lin = "", sLin = "";
        int iNumLin = 1;
        try
        {
            CargarArrayHT();

            #region Apertura de conexión y transacción
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
            FICHEROSMANIOBRA oFic = FICHEROSMANIOBRA.Select(tr, Constantes.FicheroDatos);
            if (oFic.t447_fichero.Length > 0)
            {
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
                //lin = FromUnicodeByteArray(byteArray);
                #endregion

                string[] aArgs = Regex.Split(lin, "\r\n");
                DesdeFichero oDesdeFichero = null;

                string sEstadoMes = "";
                int nSegMesProy = 0;

                for (int iLinea = 0; iLinea < aArgs.Length - 1; iLinea++)
                {
                    if (aArgs[iLinea] != "")
                    {
                        sLin = aArgs[iLinea];
                        iNumLin = iLinea + 1;

                        oDesdeFichero = validarLinea(DesdeFichero.getFila(sLin));
                        int? iProveedor = null;
                        if (oDesdeFichero.t315_idproveedor != 0) iProveedor = oDesdeFichero.t315_idproveedor;

                        int? iNodoDestino = null;
                        if (oDesdeFichero.t303_idnododestino != 0) iNodoDestino = oDesdeFichero.t303_idnododestino;

                        nSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, oDesdeFichero.t305_idproyectosubnodo, (int)oDesdeFichero.t325_annomes);
                        if (nSegMesProy == 0)
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, oDesdeFichero.t305_idproyectosubnodo, (int)oDesdeFichero.t325_annomes);
                            nSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, oDesdeFichero.t305_idproyectosubnodo, (int)oDesdeFichero.t325_annomes, sEstadoMes, 0, 0, false, 0, 0);
                        }

                        DATOECO.Insert(tr, nSegMesProy, oDesdeFichero.t329_idclaseeco, oDesdeFichero.t376_motivo, (decimal)oDesdeFichero.t376_importe, (oDesdeFichero.t329_necesidad == "N") ? iNodoDestino : null, (oDesdeFichero.t329_necesidad == "P") ? iProveedor : null, Constantes.FicheroDatos, oDesdeFichero.t422_idmoneda);
                    }
                }
            }
            sResul = "OK@#@";
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            //Errores.mostrarError("Error al tramitar el fichero", ex);
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar el fichero en la línea " + iNumLin.ToString() + " : " + sLin, ex);
            Conexion.CerrarTransaccion(tr);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string cabErrores()
    {
        return @"<table id='tblErrores' class='texto' style='width: 950px;'>
                <colgroup>
                    <col style='width:40px' /><col style='width:65px' /><col style='width:85px' /><col style='width:65px' />
                    <col style='width:200px' /><col style='width:65px' /><col style='width:40px' /><col style='width:65px' />
                    <col style='width:50px' /><col style='width:275px' />
                </colgroup>";
    }
    private string ponerFilaError(DesdeFichero oDesdeFichero, string sMens)
    {
        try
        {
            sb.Length = 0;
            sb.Append("<tr style='cursor:default;height:16px' id=" + iCont.ToString() + "><td>");
            sb.Append(oDesdeFichero.idnodo);
            sb.Append("</td><td>");
            sb.Append(oDesdeFichero.idproyecto);
            sb.Append("</td><td>");
            sb.Append(oDesdeFichero.annomes);
            sb.Append("</td><td>");
            sb.Append(oDesdeFichero.idclaseeco);
            sb.Append("</td><td><nobr style='cursor:default; width:190px;' title='" + oDesdeFichero.t376_motivo + "' class='NBR'>");
            sb.Append(oDesdeFichero.t376_motivo);
            sb.Append("</nobr></td><td>");
            sb.Append(oDesdeFichero.codigoexterno);
            sb.Append("</td><td>");
            sb.Append(oDesdeFichero.idnododestino);
            sb.Append("</td><td>");
            sb.Append(oDesdeFichero.importe);
            sb.Append("</td><td>");
            sb.Append(oDesdeFichero.t422_idmoneda);
            sb.Append("</td><td><nobr style='cursor:default; width:2750px;' title='" + sMens + "' class='NBR'>");
            sb.Append(sMens);
            sb.Append("</nobr></td></tr>");
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
    public static string FromUnicodeByteArray(byte[] characters)
    {
        UnicodeEncoding encoding = new UnicodeEncoding();
        string constructedString = encoding.GetString(characters);

        return (constructedString);
    }
    private DesdeFichero validarLinea(DesdeFichero oDesdeFichero)
    {
        oClaseEconomica = (ClaseEconomica)htClaseEconomica[(int.Parse(oDesdeFichero.idclaseeco)).ToString()];
        if (oClaseEconomica != null)
        {
            oDesdeFichero.t329_necesidad = oClaseEconomica.t329_necesidad;
            oDesdeFichero.t329_visiblecarruselC = oClaseEconomica.t329_visiblecarruselC;
            oDesdeFichero.t329_visiblecarruselJ = oClaseEconomica.t329_visiblecarruselJ;
            oDesdeFichero.t329_visiblecarruselP = oClaseEconomica.t329_visiblecarruselP;
        }
        if (oDesdeFichero.t329_necesidad == "P")
        {
            oDesdeFichero.codigoexterno = oDesdeFichero.idProveedNodoDestino;
            oProveedor = (Proveedor)htProveedor[oDesdeFichero.codigoexterno]; // cod externo Proveedor

            if (oProveedor != null)
                oDesdeFichero.t315_idproveedor = oProveedor.t315_idproveedor;
        }
        else if (oDesdeFichero.t329_necesidad == "N")
        {
            if (Utilidades.isNumeric(oDesdeFichero.idProveedNodoDestino))
            {
                oDesdeFichero.idnododestino = oDesdeFichero.idProveedNodoDestino;
                oNodoDestino = (NodoDestino)htNodoDestino[(int.Parse(oDesdeFichero.idnododestino)).ToString()]; // cod Nodo

                if (oNodoDestino != null)
                    oDesdeFichero.t303_idnododestino = int.Parse(oDesdeFichero.idnododestino);
            }
            else
            {
                oDesdeFichero.t303_idnododestino = -1;
            }
        }
        oProyectoSubNodo = (ProyectoSubNodo)htProyectoSubNodo[oDesdeFichero.idproyecto + "/" + oDesdeFichero.idnodo];
        if (oProyectoSubNodo != null)
        {
            oDesdeFichero.t301_idproyecto = oProyectoSubNodo.t301_idproyecto;
            oDesdeFichero.t305_idproyectosubnodo = oProyectoSubNodo.t305_idproyectosubnodo;
            oDesdeFichero.t303_idnodo = oProyectoSubNodo.t303_idnodo;
            oDesdeFichero.t305_cualidad = oProyectoSubNodo.t305_cualidad;
        }

        if (Utilidades.isNumeric(oDesdeFichero.idnodo))
            oDesdeFichero.t303_idnodo = System.Convert.ToInt32(oDesdeFichero.idnodo);
        else
            oDesdeFichero.t303_idnodo = -1;

        if (Utilidades.isNumeric(oDesdeFichero.idproyecto))
            oDesdeFichero.t301_idproyecto = System.Convert.ToInt32(oDesdeFichero.idproyecto);
        else
            oDesdeFichero.t301_idproyecto = -1;

        if (Utilidades.isNumeric(oDesdeFichero.annomes))
            oDesdeFichero.t325_annomes = System.Convert.ToInt32(oDesdeFichero.annomes);
        else
            oDesdeFichero.t325_annomes = -1;

        if (Utilidades.isNumeric(oDesdeFichero.idclaseeco))
        {
            oDesdeFichero.t329_idclaseeco = System.Convert.ToInt32(oDesdeFichero.idclaseeco);
            if (oDesdeFichero.t329_idclaseeco < 0) oDesdeFichero.t329_idclaseeco = 999999;
        }
        else
        {
            oDesdeFichero.t329_idclaseeco = -1;
        }

        if (oDesdeFichero.t305_cualidad == "C" && oDesdeFichero.t329_visiblecarruselC == false)
        {
            oDesdeFichero.t329_idclaseeco = 888888;
        }
        if (oDesdeFichero.t305_cualidad == "J" && oDesdeFichero.t329_visiblecarruselJ == false)
        {
            oDesdeFichero.t329_idclaseeco = 777777;
        }
        if (oDesdeFichero.t305_cualidad == "P" && oDesdeFichero.t329_visiblecarruselP == false)
        {
            oDesdeFichero.t329_idclaseeco = 666666;
        }

        if (Utilidades.isNumeric(oDesdeFichero.importe))
            oDesdeFichero.t376_importe = System.Convert.ToDecimal(oDesdeFichero.importe);
        else
            oDesdeFichero.t376_importe = -999999999;

        //if (Utilidades.isNumeric(oDesdeFichero.idnododestino))
        //    oDesdeFichero.t303_idnododestino = System.Convert.ToInt32(oDesdeFichero.idnododestino);
        //else
        //    oDesdeFichero.t303_idnododestino = -1;

        return oDesdeFichero;
    }
    private bool validarCampos(DesdeFichero oDesdeFichero, bool bEscribir)
    {
        if ((ProyectoSubNodo)htProyectoSubNodo[oDesdeFichero.idproyecto + "/" + oDesdeFichero.idnodo] == null)
        {
            //if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Proyecto no existente (" + oDesdeFichero.t305_idproyectosubnodo + "/" + oDesdeFichero.idnodo + ")"));
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Proyecto no existente (" + oDesdeFichero.idproyecto + "/" + oDesdeFichero.idnodo + ")"));
            return false;
        }

        if ((oDesdeFichero.codigoexterno != "") && (oDesdeFichero.t329_necesidad == "P") && ((Proveedor)htProveedor[oDesdeFichero.codigoexterno] == null))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "El código de proveedor no existe (" + oDesdeFichero.codigoexterno + ")"));
            return false;
        }

        if ((oDesdeFichero.idnododestino != "") && (oDesdeFichero.t329_necesidad == "N") && ((NodoDestino)htNodoDestino[(int.Parse(oDesdeFichero.idnododestino)).ToString()] == null))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "El código de nodo destino no existe (" + oDesdeFichero.idnododestino + ")"));
            return false;
        }

//
        if ((oDesdeFichero.t422_idmoneda != "") && ((Moneda)htMoneda[oDesdeFichero.t422_idmoneda] == null))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "El código de moneda no existe (" + oDesdeFichero.t422_idmoneda + ")"));
            return false;
        }

//
        if (oDesdeFichero.t329_necesidad == "P" && oDesdeFichero.codigoexterno == "")
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "La clase económica necesita proveedor y no se ha especificado"));
            return false;
        }

        if (oDesdeFichero.t329_necesidad == "N" && oDesdeFichero.idnododestino == "")
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "La clase económica especificada exige nodo y no se ha especificado"));
            return false;
        }

        if (oDesdeFichero.t329_necesidad != "P" && oDesdeFichero.t329_necesidad != "N" && oDesdeFichero.idProveedNodoDestino != "")
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "La clase económica debe ser de proveedor o de nodo pues se ha especificado un código"));
            return false;
        }

        if (oDesdeFichero.t303_idnodo == -1)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Número de nodo no numérico (" + oDesdeFichero.idnodo + ")"));
            return false;
        }

        if (oDesdeFichero.t301_idproyecto == -1)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Número de proyecto no numérico (" + oDesdeFichero.idproyecto + ")"));
            return false;
        }

        if (oDesdeFichero.t329_idclaseeco == 999999)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "La clase económica para este proceso no puede ser negativa (" + oDesdeFichero.idclaseeco + ")"));
            return false;
        }

        if (oDesdeFichero.t329_idclaseeco == 888888)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "La clase económica (" + oDesdeFichero.idclaseeco + ") para este proyecto/nodo (Contratante) no tiene visibilidad en el carrusel."));
            return false;
        }

        if (oDesdeFichero.t329_idclaseeco == 777777)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "La clase económica (" + oDesdeFichero.idclaseeco + ") para este proyecto/nodo (Replicado sin gestión) no tiene visibilidad en el carrusel."));
            return false;
        }

        if (oDesdeFichero.t329_idclaseeco == 666666)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "La clase económica (" + oDesdeFichero.idclaseeco + ") para este proyecto/nodo (Replicado con gestión propia) no tiene visibilidad en el carrusel."));
            return false;
        }
        if (oDesdeFichero.t329_idclaseeco == -1)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Número de clase económica no numérico (" + oDesdeFichero.idclaseeco + ")"));
            return false;
        }

        if (oDesdeFichero.t376_importe == -999999999)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Importe de clase económica no numérico (" + oDesdeFichero.importe + ")"));
            return false;
        }

        if (oDesdeFichero.annomes.Length != 6)
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Fecha no valida para el formato AAAAMM (" + oDesdeFichero.annomes + ")"));
            return false;
        }
        string sAno = oDesdeFichero.annomes.ToString().Substring(0, 4);
        if (!Utilidades.isNumeric(sAno))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Año de la fecha no es numérico (" + sAno + ")"));
            return false;
        }
        if (Utilidades.isNumeric(sAno))
        {
            if (System.Convert.ToInt32(sAno) < 1950 || System.Convert.ToInt32(sAno) > 2099)
            {
                if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Año de la fecha incorrecto (" + sAno + ")"));
                return false;
            }
        }

        if (!Utilidades.isNumeric(oDesdeFichero.annomes.Substring(4, 2)))
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Mes de la fecha no es numérico (" + oDesdeFichero.annomes.Substring(4, 2) + ")"));
            return false;
        }
        string mes = oDesdeFichero.annomes.ToString().Substring(4, 2);
        if (Utilidades.isNumeric(mes))
        {
            if (System.Convert.ToInt32(mes) < 1 || System.Convert.ToInt32(mes) > 12)
            {
                if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Mes de la fecha incorrecto (" + oDesdeFichero.annomes.Substring(4, 2) + ")"));
                return false;
            }
        }
        if (oDesdeFichero.t303_idnododestino == -1 && oDesdeFichero.t329_necesidad == "N")
        {
            if (bEscribir) sbE.Append(ponerFilaError(oDesdeFichero, "Número de nodo destino no numérico (" + oDesdeFichero.idnododestino + ")"));
            return false;
        }
        return true;
    }
}

