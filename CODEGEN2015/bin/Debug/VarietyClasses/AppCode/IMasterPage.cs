using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for IMasterPage
/// </summary>
public interface IMasterPage
{
     void RegisterPostbackTrigger(Control ctl);
     void RegisterAsyncPostBackControl(Control ctl);
}
