using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using FileUpload;

namespace GASVI
{
    public partial class PorcentajeSubida : System.Web.UI.Page
    {
        private const string uploadIsReadyKey = "UploadIsReady";
        public string sCerrar = "";
        int nResto = 0;

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["GVT_nRefrescos"] == null) Session["GVT_nRefrescos"] = 0;
            else Session["GVT_nRefrescos"] = (int)Session["GVT_nRefrescos"] + 1;

            if (Session["GVT_bArchivoExistente"] == null) Session["GVT_bArchivoExistente"] = false;

            if (Session["GVT_oldLength"] == null) Session["GVT_oldLength"] = 0;

            if (Session[uploadIsReadyKey] == null)
                Session[uploadIsReadyKey] = false;

            if (UploadModule.UploadModule.GetTotalSize(Request.QueryString["guid"]) != null)
            {
                Session["GVT_bArchivoExistente"] = true;
                int totalLength = (int)UploadModule.UploadModule.GetTotalSize(Request.QueryString["guid"]);
                int currentLength = (int)UploadModule.UploadModule.GetCurrentSize(Request.QueryString["guid"]);
                int per = 0;

                if (totalLength > 0)
                    per = (int)(100 * (double)currentLength / totalLength);

                if (per > 0 && (bool)Session[uploadIsReadyKey] == false)
                    Session[uploadIsReadyKey] = true;

                //Tasa de transferencia
                int lastTrans = currentLength - int.Parse(Session["GVT_oldLength"].ToString());
                littasatrans.Text = (lastTrans/1024).ToString() + " KB/Segundo";
                Session["GVT_oldLength"] = currentLength;

                //Tiempo estimado
                if (lastTrans > 0)
                    nResto = (totalLength - currentLength) / lastTrans;

                if (nResto < 60)
                {
                    if (nResto == 0)
                    {
                        littiempoest.Text = "grabando...";
                        per = 100;
                        currentLength = totalLength;
                    }
                    else littiempoest.Text = nResto.ToString() + " seg.";
                }
                else if (nResto > 0)
                {
                    int nMin = nResto / 60;
                    int nSeg = nResto % 60;
                    littiempoest.Text = nMin.ToString() + " min. " + nSeg.ToString() + " seg.";
                }

                objBarraProg.PercentageOfProgress = per;

                littotallen.Text = totalLength / 1024 + " KB";
                litcontent.Text = currentLength / 1024 + " KB" + " (" + per + "%)";

                if (totalLength / 1024 > 25600)
                {
                    Session[uploadIsReadyKey] = null;
                    Session["GVT_oldLength"] = null;
                    Response.Write("<script language='javascript'>window.opener.cerrarVentana();alert('¡Denegado! Se ha seleccionado un archivo mayor del máximo establecido en 25Mb.');window.close();</script>");
                }
            }
            else if ((bool)Session[uploadIsReadyKey] == true || (Session["GVT_bSubido"] != null && (bool)Session["GVT_bSubido"] == true))
            {
                Session[uploadIsReadyKey] = null;
                Session["GVT_oldLength"] = null;
                if (Session["GVT_bSubido"] != null) Session["GVT_bSubido"] = null;
                Response.Write("<script language='javascript'>window.close();</script>");
            }
            else if ((int)Session["GVT_nRefrescos"] > 2 && (bool)Session["GVT_bArchivoExistente"] == false)
            {
                Session[uploadIsReadyKey] = null;
                Session["GVT_oldLength"] = null;
                Session["GVT_nRefrescos"] = null;
                Session["GVT_bArchivoExistente"] = null;
                Response.Write("<script language='javascript'>window.close();</script>");
            }
        }
    }
}
