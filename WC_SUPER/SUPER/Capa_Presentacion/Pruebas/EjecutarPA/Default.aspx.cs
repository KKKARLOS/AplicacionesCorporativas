using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Capa_Presentacion_Pruebas_EjecutarPA_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sError = "";
        try
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@anomes", SqlDbType.Int, 4);
            aParam[0].Value = 201407;

            IB.EjecutarPA.BLL.Procedimiento.Ejecutar("ZZKORO_ENVIO_MES", aParam);
        }
        catch (EjecutarPAException ex)
        {
            sError = ex.Message;
        }
        catch (Exception ex)
        {
            sError = ex.Message;
        }
    }
}
