using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

public partial class Capa_Presentacion_Pruebas_Excel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.VarChar, 8000, "12157");
            DataSet ds=null;// = Excel.obtenerCVExcel(null, aParam);
            svcEXCEL.IsvcEXCELClient osvcExcel = new svcEXCEL.IsvcEXCELClient();
            byte[] result = osvcExcel.getExcelFromDataSet(ds, ".xls");

            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            String nav = HttpContext.Current.Request.Browser.Browser.ToString();
            if (nav.IndexOf("IE") != -1)
            {

                Response.AddHeader("content-type", "application/excel; charset=utf-8");
                Response.AddHeader("Content-Disposition", "attachment; filename=\"Curriculums.xls\"");
            }
            else
            {
                Response.AddHeader("content-type", "application/excel;charset=utf-8");
                Response.AddHeader("Content-Disposition", "attachment; filename=\"Curriculums.xls\"");
            }
            Response.Clear();
            Response.BinaryWrite(result);

            Response.Flush();
            Response.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }
}
