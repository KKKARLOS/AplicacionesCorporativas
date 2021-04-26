using System;
using System.Collections.Generic;
using System.Text;

public class General
{

    public int GetCurrentID()
    {
        int id = 0;

        if (System.Web.HttpContext.Current.Session["Id"] != null)
        {
            id = Convert.ToInt32(System.Web.HttpContext.Current.Session["Id"]);
        }
        return id;
    }

   
}