using System.Web.UI;
using System.Web.UI.WebControls;

namespace GASVI.BLL
{
	/// <summary>
	/// Descripción breve de ModoLectura.
	/// </summary>
	public class ModoLectura : System.Web.UI.Page
	{
		public ModoLectura()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
        public static void Poner(ControlCollection ColeccionControles)
        {
            string sName;
            foreach (Control ctrl in ColeccionControles)
            {
                sName = ctrl.GetType().Name;
                switch (sName)
                {
                    case "TextBox":
                        ((TextBox)ctrl).ReadOnly = true;
                        if (((TextBox)ctrl).Attributes["Calendar"] != null){
                            ((TextBox)ctrl).Attributes.Remove("onclick");
                        }
                        //if (((TextBox)ctrl).Attributes["lectura"] != null) ((TextBox)ctrl).Attributes["lectura"] = "1";
                        break;
                    case "CheckBox":
                        ((CheckBox)ctrl).Enabled = false;
                        break;
                    case "RadioButtonList":
                        ((RadioButtonList)ctrl).Enabled = false;
                        break;
                    case "DropDownList":
                        ((DropDownList)ctrl).Enabled = false;
                        break;
                    //case "Image":
                    case "PopCalendar":
                        ctrl.Visible = false;
                        break;
                    case "HtmlGenericControl":
                        if (ctrl.Controls.Count == 0) break;
                        if (ctrl.Controls[0].GetType().Name == "PopCalendar")
                            ctrl.Visible = false;
                        else Poner(ctrl.Controls);
                        break;
                    case "HtmlForm":
                    case "HtmlTable":
                    case "HtmlTableRow":
                    case "HtmlTableCell":
                    case "PageView":
                    case "MultiPage":
                    case "Repeater":
                    case "masterpages_plantilla_master":
                    case "ContentPlaceHolder":
                        Poner(ctrl.Controls);
                        break;
                }
            }
        }
    }
}
