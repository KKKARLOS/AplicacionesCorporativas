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

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Administradores : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaOrgVtasSAP;
    public string strTablaOrgVtasSuper;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.sbotonesOpcionOn = "4";
            Master.sbotonesOpcionOff = "4";

            Master.TituloPagina = "Mantenimiento de la fecha límite de tramitación de órdenes de facturación";
            Master.bFuncionesLocales = true;
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");


            Utilidades.SetEventosFecha(this.txtFechaMes01);
            Utilidades.SetEventosFecha(this.txtFechaMes02);
            Utilidades.SetEventosFecha(this.txtFechaMes03);
            Utilidades.SetEventosFecha(this.txtFechaMes04);
            Utilidades.SetEventosFecha(this.txtFechaMes05);
            Utilidades.SetEventosFecha(this.txtFechaMes06);
            Utilidades.SetEventosFecha(this.txtFechaMes07);
            Utilidades.SetEventosFecha(this.txtFechaMes08);
            Utilidades.SetEventosFecha(this.txtFechaMes09);
            Utilidades.SetEventosFecha(this.txtFechaMes10);
            Utilidades.SetEventosFecha(this.txtFechaMes11);
            Utilidades.SetEventosFecha(this.txtFechaMes12);

            txtAnno.Text = DateTime.Now.Year.ToString();
            ContentPlaceHolder oCPHC = (ContentPlaceHolder)Master.FindControl("CPHC");

            string sMes="";
            DropDownList cboAux;
            for (int j = 1; j < 13; j++)
            {
                sMes = j.ToString();
                if (j < 10) sMes = "0" + sMes;

                cboAux = (DropDownList)oCPHC.FindControl("cboHora" + sMes);
                cboAux.Items.Add(new ListItem("0:00", "24cache"));
                cboAux.Items.Add(new ListItem("0:30", "0:30"));
                for (int i = 1; i < 24; i++)
                {
                    cboAux.Items.Add(new ListItem(i.ToString() + ":00", i.ToString() + ":00"));
                    cboAux.Items.Add(new ListItem(i.ToString() + ":30", i.ToString() + ":30"));
                    if (i == 18) cboAux.SelectedValue = "18:00";
                }
            }


            try
            {
                //string strTabla0 = cargarLimitesFacturacion(int.Parse(txtAnno.Text));
                //string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                //if (aTabla0[0] == "Error") Master.sErrores = aTabla0[1];
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
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
            case ("cargarLimitesFacturacionCli"):
                sResultado += cargarLimitesFacturacionCli(int.Parse(aArgs[1]));
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
    //private string cargarLimitesFacturacion(int iAnno)
    //{
    //    ContentPlaceHolder oCPHC = (ContentPlaceHolder)Master.FindControl("CPHC");

    //    try
    //    {
    //        DropDownList cboAux;
    //        TextBox txtAux;
    //        SqlDataReader dr = LIMITEFACTURACION.LimitesAnno(iAnno);

    //        while (dr.Read())
    //        {
    //            txtAux = (TextBox)oCPHC.FindControl("txtFechaMes" + dr["mes"].ToString());
    //            txtAux.Text = dr["fecha"].ToString();
    //            txtAux.Attributes.Add("bd", "W");
    //            cboAux = (DropDownList)oCPHC.FindControl("cboHora" + dr["mes"].ToString());
    //            cboAux.SelectedValue = dr["hora"].ToString();
    //        }
    //        dr.Close();
    //        dr.Dispose();

    //        return "OK@#@"; ;
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al obtener los límites de facturación de un año.", ex);
    //    }
    //}
    private string cargarLimitesFacturacionCli(int iAnno)
    {
        string sMeses = "";
        try
        {
            SqlDataReader dr = LIMITEFACTURACION.LimitesAnno(iAnno);

            while (dr.Read())
            {
                if (sMeses == "") sMeses = dr["mes"].ToString() + "||" + ((DateTime)dr["fecha"]).ToShortDateString() + "||" + dr["hora"].ToString();
                else sMeses += "//" + dr["mes"].ToString() + "||" + ((DateTime)dr["fecha"]).ToShortDateString() + "||" + dr["hora"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sMeses; ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los límites de facturación de un año.", ex);
        }
    }
    private string Grabar(string strLimiteFactura)
    {
        string sResul = "" , sElementosInsertados = "";

        #region Abrir conexión y transacción
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
            #region Datos Límites de Facturación
            if (strLimiteFactura != "") 
            {
                string[] aLimiteFactura = Regex.Split(strLimiteFactura, "///");
                foreach (string oLimiteFactura in aLimiteFactura)
                {
                    if (oLimiteFactura == "") continue;
                    string[] aValores = Regex.Split(oLimiteFactura, "##");
                    ///aValores[0] = bd
                    ///aValores[1] = @t637_anomes
                    ///aValores[2] = @t637_fecha
                    ///
                    switch (aValores[0])
                    {
                        case "I":
                            LIMITEFACTURACION.Insert(tr, int.Parse(aValores[1]), DateTime.Parse(aValores[2]));
                            break;
                        case "D":
                            LIMITEFACTURACION.Delete(tr, int.Parse(aValores[1]));
                            break;
                        case "U":
                            LIMITEFACTURACION.Update(tr, int.Parse(aValores[1]), DateTime.Parse(aValores[2]));
                            break;
                    }
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + sElementosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los límites de facturación", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
