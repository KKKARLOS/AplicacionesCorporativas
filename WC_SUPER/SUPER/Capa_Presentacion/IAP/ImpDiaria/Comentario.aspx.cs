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
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_IAP_ImpDiaria_Comentario : System.Web.UI.Page
{
    public string strTitulo;
    public string nLongitud;
    protected string sEstado, sImputacion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        this.hdnComentario.Value = Utilidades.decodpar(Request.QueryString["strComentario"].ToString());
        if (this.hdnComentario.Value == "@")
        {
            string sIdTarea = Request.QueryString["IDTAREA"].ToString();
            string sFecha = Request.QueryString["FECHA"].ToString();
            string sEmpleado = Request.QueryString["EMPLEADO"].ToString();

            CONSUMOIAP oCONSUMOIAP = CONSUMOIAP.Select(null, int.Parse(sIdTarea), int.Parse(sEmpleado), DateTime.Parse(sFecha));
            this.hdnComentario.Value = oCONSUMOIAP.t337_comentario;
        }

        strTitulo = Utilidades.decodpar(Request.QueryString["strTitulo"].ToString());
        sEstado = Request.QueryString["estado"];
        sImputacion = Request.QueryString["imputacion"];

        this.txtComentario.Text = this.hdnComentario.Value;

        bool bEstadoLectura = false;
        switch (sEstado)//Estado
        {
            case "0"://Paralizada
                bEstadoLectura = true;
                break;
            case "1"://Activo
                break;
            case "2"://Pendiente
                bEstadoLectura = true;
                break;
            case "3"://Finalizada
                if (sImputacion == "0") bEstadoLectura = true;
                break;
            case "4"://Cerrada
                if (sImputacion == "0") bEstadoLectura = true;
                break;
        }
        if (bEstadoLectura) ModoLectura.Poner(this.Controls);
    }
}
