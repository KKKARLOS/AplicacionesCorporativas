using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace GESTAR.Capa_Negocio
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
            foreach (Control ctrl in ColeccionControles)
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        if ( ((TextBox)ctrl).ID.IndexOf("hdn") == -1) ((TextBox)ctrl).ReadOnly = true;
                        break;
                    case "CheckBox":
                        ((CheckBox)ctrl).Enabled = false;
                        break;
                    case "HtmlInputCheckBox":
                        ((HtmlInputCheckBox)ctrl).Disabled = true;
                        break;
                    case "RadioButtonList":
                        ((RadioButtonList)ctrl).Enabled = false;
                        break;
                    case "DropDownList":
                        ((DropDownList)ctrl).Enabled = false;
                        break;
                    case "Image":
                        ctrl.Visible = false;
                        break;
                    case "PopCalendar":
                        ctrl.Visible = false;
                        break;
                    //case "HtmlForm":
                    //    Poner(((HtmlForm)ctrl).Controls);
                    //    break;
                    //case "HtmlGenericControl":
                    //    Poner(((HtmlGenericControl)ctrl).Controls);
                    //    break;
                    //case "HtmlTableRow":
                    //    Poner(((HtmlTableRow)ctrl).Controls);
                    //    break;
                    //case "HtmlTableCell":
                    //    Poner(((HtmlTableCell)ctrl).Controls);
                    //    break;

                    case "HtmlGenericControl":
                        if (ctrl.Controls.Count == 0) break;
                        if (ctrl.Controls[0].GetType().Name == "PopCalendar")
                            ctrl.Visible = false;
                        else Poner(ctrl.Controls);
                        break;
                    case "HtmlForm":
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
