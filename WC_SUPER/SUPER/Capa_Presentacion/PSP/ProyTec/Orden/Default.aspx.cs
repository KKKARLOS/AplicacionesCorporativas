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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, strTablaPTs = "<table id='tblPTs'></table>", sLectura;
    public int nPE;
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

            strTablaPTs = "";
            sLectura = "false";
            try
            {
                //ObtenerPTs(nPE);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los proyectos t�cnicos", ex);
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
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);// lista de pares idPT-orden
                break;
            case ("pt")://carga lista de PT�s 
                //sResultado += ObtenerPTs(short.Parse(aArgs[1]), int.Parse(aArgs[2]));
                sResultado += ObtenerPTs(int.Parse(aArgs[1]));
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
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
    protected string Grabar(string strDatosPT)
    {
        string sResul = "", sDatosPT;
        int iCodPT;
        short iOrden; 
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
            string[] aPTs = Regex.Split(strDatosPT, "///");
            for (int i = 0; i < aPTs.Length - 1; i++)
            {
                sDatosPT = aPTs[i];
                string[] aDatosPT = Regex.Split(sDatosPT, "##");
                iCodPT = int.Parse(aDatosPT[0]);
                iOrden = short.Parse(aDatosPT[1]);
                ProyTec.UpdateOrden(tr, iCodPT, iOrden);
            }
            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del proyecto t�cnico", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string ObtenerPTs(int nPSN)
    {
        //Relacion de proyectos t�cnicos asignados al proyecto econ�mico
        string sResul = "";
        StringBuilder sbuilder = new StringBuilder();
        try
        {
            SqlDataReader dr = PROYECTO.ObtenerPTs2(nPSN);

            sbuilder.Append("<table id='tblPTs' class='texto' style='width: 450px;'>");
            sbuilder.Append("<colgroup><col style='width:400px;' /><col style='width:50px' /></colgroup>");
            sbuilder.Append("<tbody>");
            while (dr.Read())
            {
                sbuilder.Append("<tr id='" + dr["t331_idpt"].ToString() + "' ordenAnt='" + dr["t331_orden"].ToString());
                sbuilder.Append("' onclick='mm(event);' style='height:20px'><td>" + dr["t331_despt"].ToString()+"</td>");
                sbuilder.Append("<td><input type='text' MaxLength='4' class='txtNumL' style='width:47px;padding-right:3px;' value='");
                sbuilder.Append(dr["t331_orden"].ToString() + "' onkeypress='vtn2(event);' onblur=\"javascript:this.className='txtNumL'\"");
                sbuilder.Append(" onkeydown='activarGrabar();' onfocus=\"javascript:this.className='txtNumM';this.value=this.value.ToString('N', 4, 0);\"'></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sbuilder.Append("</tbody>");
            sbuilder.Append("</table>");
            strTablaPTs = sbuilder.ToString();
            sResul = "OK@#@" + strTablaPTs;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relaci�n de proyectos t�cnicos asociados al proyecto econ�mico.", ex);
        }
        return sResul;
    }
    private string recuperarPSN(string nPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperado(int.Parse(nPSN), (int)Session["UsuarioActual"], "PST");
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //0
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //1
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //2
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //3
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //4
                sb.Append(dr["estado"].ToString());  //5
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
}
