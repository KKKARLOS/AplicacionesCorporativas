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
using GASVI.BLL;

public partial class Capa_Presentacion_getExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;

        string sTituloExcel = DateTime.Now.ToString("yyyyMMdd");
        if (Request.Form["hdnTituloExcel"] != null && Utilidades.unescape(Request.Form["hdnTituloExcel"].ToString()) != "")
            sTituloExcel = Utilidades.unescape(Request.Form["hdnTituloExcel"].ToString());

        Response.AppendHeader("Content-Disposition", "inline;filename=" + sTituloExcel + ".xls");
        Response.ContentType = "application/ms-excel";
        this.EnableViewState = false;

        if (Request.Form["hdnInputExcel"] != null)
            Response.Write(Utilidades.unescape(Request.Form["hdnInputExcel"].ToString()));

        Response.Flush();
        Response.Close();
        Response.End();
    }
}
