using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Configuration;


class GUIHelper
{
    public static bool IsValid(Control ctlMain)
    {
        string password1 = "";
        string password2 = "";
        foreach (Control ctl in ctlMain.Controls)
        {
            if (ctl.GetType() == typeof(TextBox))
            {
                #region Check Required
                //ctl.Tag =="N"-->Not  Mendatory
                if (ctl.Text.Trim().Length <= 0 & ctl.Tag.ToString() !="N")
                {
                    ctl.Focus();
                    ctl.Select();
                    Messages.ShowErrorMessage(1);
                    return false;
                }
                #endregion

                #region CheckData
                if (ctl.Name.IndexOf("Date",StringComparison.CurrentCultureIgnoreCase) > -1 )
                {
                    DateTime dt;
                    if (!DateTime.TryParse(ctl.Text, GUIHelper.DateFormatInfo, DateTimeStyles.None, out dt))
                    {
                        ctl.Focus();
                        ctl.Select();
                        Messages.ShowErrorMessage(Messages.GetMessage(2) + GUIHelper.DateFormat);
                        return false;
                    }
                }

                else if (ctl.Name.IndexOf("Email", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    if (ctl.Text.IndexOf("@") < 0)
                    {
                        ctl.Focus();
                        ctl.Select();
                        Messages.ShowErrorMessage(3);
                        return false;
                    }
                }

                

                else if (ctl.Name.IndexOf("txtConfirmPassword", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    password2 = ctl.Text;

                    if (password1.Length > 0 && password2.Length > 0)
                    {
                        if (password1 != password2)
                        {
                            ctl.Focus();
                            ctl.Select();
                            Messages.ShowErrorMessage(7);
                            return false;
                        }

                    }
                }
                else if (ctl.Name.IndexOf("txtPassword", StringComparison.CurrentCultureIgnoreCase) > -1)
                {
                    password1 = ctl.Text;

                    if (password1.Length > 0 && password2.Length > 0)
                    {
                        if (password1 != password2)
                        {
                            ctl.Focus();
                            ctl.Select();
                            Messages.ShowErrorMessage(7);
                            return false;
                        }

                    }
                }
                #endregion
            }

        }
        return true;

    }

    public static bool ClearControlData(Control ctlMain)
    {
        foreach (Control ctl in ctlMain.Controls)
        {
            if (ctl.GetType() == typeof(TextBox))
            {
                ctl.Text = "";
            }

        }
        return true;
    }

    public static string DateFormat
    {
        get
        {
            return ConfigurationSettings.AppSettings.Get("DateFormat");
        }
    }


    public static DateTimeFormatInfo DateFormatInfo
    {
        get
        {
            string format= ConfigurationSettings.AppSettings.Get("DateFormat");
            DateTimeFormatInfo dateinfo = new DateTimeFormatInfo();
            dateinfo.ShortDatePattern = DateFormat;
            return dateinfo;

        }
    }
   
}

