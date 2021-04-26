using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


public partial class uc_Session : System.Web.UI.UserControl
{
    protected string strUrl;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["strServer"] != null)
                this.strUrl = Session["strServer"].ToString() + "Imagenes/Session/";
        }
    }

    #region C�digo generado por el Dise�ador de Web Forms
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: llamada requerida por el Dise�ador de Web Forms ASP.NET.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		M�todo necesario para admitir el Dise�ador. No se puede modificar
    ///		el contenido del m�todo con el editor de c�digo.
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion
}

