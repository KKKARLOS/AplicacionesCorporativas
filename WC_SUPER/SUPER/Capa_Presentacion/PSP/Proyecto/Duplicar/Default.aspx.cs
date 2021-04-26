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
    public string  sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            try
            {
                hdnNumPE_Destino.Value = Request.QueryString["nPE"].ToString();
                hdnT305IdProy_Destino.Value = Request.QueryString["nT305IdProy"].ToString();
                CRorigen.Value = Request.QueryString["nCR"].ToString();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al cargar la pantalla de copiar proyectos", ex);
            }

            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; 
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("procesar"):
                sResultado += Procesar(
                    int.Parse(aArgs[1]), 			                        // NumPSN_Origen
                    int.Parse(aArgs[2]),                                    // NumPSN_Destino
                    aArgs[3]=="1"? true:false,                                   // Bitacora PT
                    aArgs[4] == "1" ? true : false,                                 // Bitacora Tarea
                    aArgs[5] == "1" ? true : false,                                   // Hitos
                    aArgs[6],                                               // Estados de Tarea
                    aArgs[7],                                               // ID_PROYECTOSUBNODO
                    aArgs[8],                                               // MODOLECTURA_PROYECTOSUBNODO
                    aArgs[9],                                               // RTPT_PROYECTOSUBNODO
                    aArgs[10] ,                                              // MONEDA_PROYECTOSUBNODO
                    aArgs[11] ,                                              // Accion sobre documentod
                    aArgs[12] == "1" ? true : false                        // Copiar atributos estad�sticos
                    );
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

    private string Procesar(int iNumPSN_Origen, int iNumPSN_Destino, bool bBitacoraPT, bool bBitacoraTA, bool bHitos,
                            string sEstadosTarea, string ID_PROYECTOSUBNODO, string MODOLECTURA_PROYECTOSUBNODO,
                            string RTPT_PROYECTOSUBNODO, string MONEDA_PROYECTOSUBNODO, string sAccDoc, bool bCopiaAE)
    {
        string sResul = "";
        string sModelotarif_Origen = "";
        string sModelotarif_Destino = "";
        bool bErrorControlado = false;
        int iNodoOrigen = 0;
        int iNodoDestino = 0;
        int iNumPE_Origen = 0;
        int iNumPE_Destino = 0;

        #region Validaciones previas a la copia de la estructura del proyecto

        try
        {
            
            //  Comprobaciones de servidor

            /*  - Si un proyecto econ�mico destino tiene consumos
                - Los nodos de los proyectos origen y destino deben ser los mismos
                - El proyecto destino debe tener el atributo t305_admiterecursospst con valor a 1
                - El modelo de tarificaci�n de los proyectos origen y destino deben ser los mismos
                - En el proyecto origen debe tener alguna figura en escritura para poder realizarse la copia.
                  Es decir debe ser:
                    - Responsable de Oficina T�cnica del nodo del proyecto
	                - Responsable, Delegado, Colaborador de proyecto
	                - Responsable t�cnico de proyecto econ�mico (RTPE)
	                - Jefe de proyecto
                    - Administrador
            */
            if (bTieneConsumosPE(iNumPSN_Destino))
            {
                bErrorControlado = true;
                throw (new Exception("El proyecto destino tiene consumos.\n\nNo se puede realizar el proceso."));
            }

            SqlDataReader dr = PROYECTO.fgGetDatosProy2(iNumPSN_Origen);
            if (dr.Read())
            {
                iNodoOrigen = int.Parse(dr["t303_idnodo"].ToString());
                iNumPE_Origen = int.Parse(dr["t301_idproyecto"].ToString());
                sModelotarif_Origen = dr["t301_modelotarif"].ToString();                
            }

            dr = PROYECTO.fgGetDatosProy2(iNumPSN_Destino);
            if (dr.Read())
            {
                if ((bool)dr["t305_admiterecursospst"] == false)
                {
                    dr.Close();
                    dr.Dispose();
                    bErrorControlado = true;
                    throw (new Exception("El proyecto destino debe tener el atributo 'Permitir PST' activado.\n\nNo se puede realizar el proceso."));
                }
                iNodoDestino = int.Parse(dr["t303_idnodo"].ToString());
                iNumPE_Destino = int.Parse(dr["t301_idproyecto"].ToString());
                sModelotarif_Destino = dr["t301_modelotarif"].ToString(); 
            }
            dr.Close();
            dr.Dispose();

            //if (iNodoOrigen != iNodoDestino) 
            //{
            //    bErrorControlado = true;
            //    throw (new Exception("Los nodos de los proyectos origen y destino son diferentes.\n\nNo se puede realizar el proceso."));
            //}

            if (sModelotarif_Origen != sModelotarif_Destino)
            {
                bErrorControlado = true;
                throw (new Exception("Los modelos de tarificaci�n de los proyectos origen y destino son diferentes.\n\nNo se puede realizar el proceso."));
            }

            bool bModoEscritura = false;
            dr = PROYECTOSUBNODO.FigurasModoEscritura(null,iNumPSN_Origen, (int)Session["UsuarioActual"]);
            if (dr.HasRows) bModoEscritura = true;
            if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "") bModoEscritura = true;
            dr.Close();
            dr.Dispose();

            if (!bModoEscritura)
            {
                bErrorControlado = true;
                throw (new Exception("Debe tener acceso al proyecto origen en modo escritura.\n\nNo se puede realizar el proceso."));
            }
        }
        catch (Exception ex)
        {
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error en las validaciones previas al proceso", ex);
            else sResul = "Error@#@Operaci�n rechazada.\n\n" + ex.Message;
            return sResul;
        }

        #endregion

        #region abrir conexi�n y transacci�n
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
            return sResul;
        }
        #endregion

        try
        {
            PROYECTOSUBNODO.Duplicar(tr, iNumPSN_Origen, iNumPSN_Destino, iNumPE_Origen, iNumPE_Destino,
                                     bBitacoraPT, bBitacoraTA, bHitos, sEstadosTarea, (int)Session["UsuarioActual"], sAccDoc, bCopiaAE);

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) 
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar datos de la estructura del proyecto", ex);
            else 
                sResul = "Error@#@Operaci�n rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        /* Se restauran los valores que se han modificado al seleccionar el proyecto origen */
        Session["ID_PROYECTOSUBNODO"] = int.Parse(ID_PROYECTOSUBNODO);
        Session["MODOLECTURA_PROYECTOSUBNODO"] = (MODOLECTURA_PROYECTOSUBNODO == "1") ? true : false;
        Session["RTPT_PROYECTOSUBNODO"] = (RTPT_PROYECTOSUBNODO == "1") ? true : false;
        Session["MONEDA_PROYECTOSUBNODO"] = MONEDA_PROYECTOSUBNODO;

        return sResul;
    }

    public bool bTieneConsumosPE(int nCodPE)
    {
        bool bConsumos;
        try
        {
            bConsumos = EstrProy.ExistenConsumosPE(null, nCodPE);
        }
        catch (Exception)
        {
            bConsumos = false;
        }
        return bConsumos;
    }
}
