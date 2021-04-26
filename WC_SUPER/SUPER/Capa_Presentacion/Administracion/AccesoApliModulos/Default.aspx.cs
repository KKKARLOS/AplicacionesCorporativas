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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public ACCESOAPLI oACCESOAPLI;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";
            Master.TituloPagina = "Mantenimiento de datos del acceso a la Aplicación/Módulos";

            try
            {
                oACCESOAPLI = ACCESOAPLI.Select(null,byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]));
                if (oACCESOAPLI.T000_ESTADO == false)
                    this.candSUPER.Src = "../../../Images/icoCerradoG.gif";
                else
                    this.candSUPER.Src = "../../../Images/icoAbiertoG.gif";

                this.txtMotivo.Text = oACCESOAPLI.T000_MOTIVO;
                if (oACCESOAPLI.t000_bbdd)
                    this.chkAudit.Checked = true;
                else
                    this.chkAudit.Checked = false;
                //if (SUPER.Capa_Negocio.Utilidades.EsSuperAdminProduccion())
                //{
                //    this.lblAudit.Style.
                //    this.chkAudit.Visible = true;
                //}
                //else
                //{
                //    this.lblAudit.Visible = false;
                //    this.chkAudit.Visible = false;
                //}
                SqlDataReader dr = ACCESOMODULO.Catalogo("", null, 1, 0);
                while (dr.Read())
                {
                    switch ( dr["t434_modulo"].ToString())
                    {
                        case ("IAP"):
                            if ((bool)dr["t434_acceso"])
                                this.candIAP.Src = "../../../Images/icoAbiertoG.gif";
                            else
                                this.candIAP.Src = "../../../Images/icoCerradoG.gif"; 
                            break;
                        case ("PST"):
                            if ((bool)dr["t434_acceso"])
                                this.candPST.Src = "../../../Images/icoAbiertoG.gif";
                            else
                                this.candPST.Src = "../../../Images/icoCerradoG.gif"; 
                            break;
                        case ("PGE"):
                            if ((bool)dr["t434_acceso"])
                                this.candPGE.Src = "../../../Images/icoAbiertoG.gif";
                            else
                                this.candPGE.Src = "../../../Images/icoCerradoG.gif";
                            break;
                        case ("ADP"):
                            if ((bool)dr["t434_acceso"])
                                this.candADP.Src = "../../../Images/icoAbiertoG.gif";
                            else
                                this.candADP.Src = "../../../Images/icoCerradoG.gif";
                            break;
                    }
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
            case "grabar":
                sResultado += Grabar(Utilidades.unescape(aArgs[1]), aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
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
    private string Grabar(string sMotivo, string sBD, string sAccSuper, string sAccIAP, string sAccPST, string sAccPGE, string sAccADP)
    {
        string sResul = "";
        SqlConnection oConn = null;
        SqlTransaction tr;

        #region abrir conexión y transacción
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
        try
        {
            Session["OCULTAR_AUDITORIA"] = sBD;
            ACCESOAPLI.Update(tr, byte.Parse(ConfigurationManager.AppSettings["CODIGO_APLICACION"]), "", (sAccSuper == "1") ? true : false, 
                              sMotivo, null, null, (sBD == "1") ? true : false);
            ACCESOMODULO.Update(tr, "IAP", (sAccIAP == "1") ? true : false);
            ACCESOMODULO.Update(tr, "PST", (sAccPST == "1") ? true : false);
            ACCESOMODULO.Update(tr, "PGE", (sAccPGE == "1") ? true : false);
            ACCESOMODULO.Update(tr, "ADP", (sAccADP == "1") ? true : false);

            Conexion.CommitTransaccion(tr);
            HttpContext.Current.Cache.Remove("ModuloAccesible");
            sResul = "OK@#@";
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
