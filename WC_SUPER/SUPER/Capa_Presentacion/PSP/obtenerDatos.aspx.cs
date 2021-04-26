using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;


namespace SUPER.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerDatos.
	/// </summary>
	public partial class obtenerDatos : System.Web.UI.Page
	{
		protected string strResultado;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            if(!Page.IsPostBack)
			{
				int nOp = int.Parse(Request.QueryString["nOpcion"].ToString());

				switch (nOp)
				{
                    case 1:  //Comprobar si un recurso tiene consumos en una tarea.
                        int nTarea = int.Parse(Request.QueryString["nTarea"].ToString());
                        int nUsuario = int.Parse(Request.QueryString["nRecurso"].ToString());
                        strResultado = bTieneconsumos(nTarea, nUsuario);
                        break;
                    case 2:  //Comprobar si un proyecto económico tiene consumos
                        //short nCodCr = short.Parse(Request.QueryString["sCodCr"].ToString());
                        int nCodPE = int.Parse(Request.QueryString["sCodPE"].ToString());
                        //strResultado = bTieneConsumosPE(nCodCr, nCodPE);
                        strResultado = bTieneConsumosPE(nCodPE);
                        break;
                    case 3:  //Comprobar si un proyecto Técnico tiene consumos
                        int nCodPT = int.Parse(Request.QueryString["sCodPT"].ToString());
                        strResultado = bTieneConsumosPT(nCodPT);
                        break;
                }

				Response.Write(strResultado);
			}
		}

        public string bTieneconsumos(int nTarea, int nUsuario)
        {
            string strReturn = "0";

            try
            {
                bool bConsumos = TareaRecurso.ExistenConsumos(null, nTarea, nUsuario);
                if (bConsumos) strReturn = "1";
            }
            catch (Exception)
            {
                //string s = ex.Message;
                strReturn = "Error";
            }

            return strReturn;
        }
        //public string bTieneConsumosPE(short nCodCr, int nCodPE)
        public string bTieneConsumosPE(int nCodPE)
        {
            string strReturn = "0";

            try
            {
                //bool bConsumos = EstrProy.ExistenConsumosPE(null, nCodCr, nCodPE);
                bool bConsumos = EstrProy.ExistenConsumosPE(null, nCodPE);
                if (bConsumos) strReturn = "1";
            }
            catch (Exception)
            {
                strReturn = "Error";
            }

            return strReturn;
        }
        public string bTieneConsumosPT(int nCodPT)
        {
            string strReturn = "0";

            try
            {
                bool bConsumos = EstrProy.ExistenConsumosPT(null, nCodPT);
                if (bConsumos) strReturn = "1";
            }
            catch (Exception)
            {
                strReturn = "Error";
            }

            return strReturn;
        }


		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
