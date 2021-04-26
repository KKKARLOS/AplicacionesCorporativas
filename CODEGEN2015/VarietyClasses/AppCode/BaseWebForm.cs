using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



public partial class BaseWebForm : System.Web.UI.Page
{
    #region Properties
    public int CurrentAccountID
    {
        get
        {
            General objGeneral = new General();
            return objGeneral.GetCurrentID();
        }
    }
    #endregion

    #region virtual methods
    protected virtual void LoadDate()
    {
          
    }
    protected virtual int GetId()
    {
        return -1;
    }

    protected virtual void FillLists()
    {
        
    }
    protected virtual void AddValidations()
    {
       
    }
    protected virtual bool Validation()
    {
        return true;
    }
    protected virtual bool PerformSave()
    {
        if(Validation())
        {
        return true;
        }
        return false;
    }
    protected virtual bool PerformDelete()
    {
        return true;
    }


    protected bool AfterSave(int result, Label lblMessage,string redirectTo)
    {
        if (result > -1)
        {
           string strScript = "alert('" + Resources.Main.SavedSuccessfully + "');  window.location.href = '" + redirectTo + "';";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "saveSuccessfully", strScript, true);
                    return true;
        }
        else
        {
            string strScript = "alert('" + Resources.Main.SavedFaild + "');" ;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "saveFaild", strScript, true);
            return false;
        }



    }
    protected bool AfterDelete(int result, Label lblMessage, string redirectTo)
    {
        if (result > -1)
        {
           string strScript = "alert('" + Resources.Main.DeletedSuccessfully + "');  window.location.href = '" + redirectTo + "';";
           ScriptManager.RegisterClientScriptBlock(this.Page,this.GetType(), "deleteSuccessfully", strScript, true);
            return true;
        }
        else
        {
            string strScript = "alert('" + Resources.Main.DeletedFaild + "');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deleteFaild", strScript, true);
            return false;
        }
    }


    protected bool LogException(Exception ex, Label lblMessage)
    {
        lblMessage.Text=ex.Message;
        return true;
    }

    protected string GetMessage(Exception ex)
    {
        return "";
    }
    #endregion

    #region Security
    public static Control FindControlRecursive(Control Root, string Id)
    {
        if (Root.ID == Id)
            return Root;

        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = FindControlRecursive(Ctl, Id);
            if (FoundCtl != null)
                return FoundCtl;
        }

        return null;

    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        //#region Get Can Access this page or not
        //bool canAccess = false;
        //string strCurrentPage = this.Page.TemplateControl.AppRelativeVirtualPath;

        //#region Pages without Permission
        //if (strCurrentPage.IndexOf("/" + "Contact" + ".aspx") > -1)
        //{
        //    canAccess = true;
        //}
        //else if (strCurrentPage.IndexOf("/" + "About" + ".aspx") > -1)
        //{
        //    canAccess = true;
        //}
        //else if (strCurrentPage.IndexOf("/" + "HomePage" + ".aspx") > -1)
        //{
        //    canAccess = true;
        //}
        //#endregion

        //#region Check Permission
        //else
        //{
        //    if (Session["Id"] != null)
        //    {

        //        //if (strCurrentPage.IndexOf("/" + objPermissionDL.PageName + ".aspx") > -1)
        //        //{
        //        //    Control ctl = FindControlRecursive(this.Page.Master, objPermissionDL.ControlName);
        //        //    if (ctl != null)
        //        //    {
        //        //        ctl.Visible = true;
        //        //        canAccess = true;
        //        //    }
        //        //}

        //    }
        //}
        //#endregion
        //#endregion

        //if (!canAccess)
        //{
        //    Response.Redirect("HomePage.aspx", false);
        //}

    }
    #endregion
}
