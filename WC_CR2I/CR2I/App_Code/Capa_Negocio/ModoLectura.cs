using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CR2I.Capa_Negocio
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
//				foreach (Control ctrl in ctrlp.Controls)
//				{
					switch (ctrl.GetType().Name)
					{
						case "TextBox":
							((TextBox)ctrl).ReadOnly = true;
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
//						case "Image":
//							ctrl.Visible = false;
//							break;
						case "PopCalendar":
							ctrl.Visible = false;
							break;
                        case "HtmlGenericControl":
                            if (ctrl.Controls.Count > 0)
                            {
                                if (ctrl.Controls[0].GetType().Name == "PopCalendar")
                                    ctrl.Visible = false;
                                Poner(ctrl.Controls);
                            }
							break;
                        case "HtmlForm":
                        case "PageView":
                        case "MultiPage":
                        case "Repeater":
                        case "masterpages_plantilla_master":
                        case "ContentPlaceHolder":
                        case "HtmlTable":
                        case "HtmlTableRow":
                        case "HtmlTableCell":
                            Poner(ctrl.Controls);
							break;
//                        case "Repeater":
//                            foreach (RepeaterItem ctrl2 in ((Repeater)ctrl).Items)
//                            {
//                                foreach (Control ctrlf in ((RepeaterItem)ctrl2).Controls)
//                                {
//                                    switch (ctrlf.GetType().Name)
//                                    {
//                                        case "TextBox":
//                                            ((TextBox)ctrlf).ReadOnly = true;
//                                            break;
//                                        case "CheckBox":
//                                            ((CheckBox)ctrlf).Enabled = false;
//                                            break;
//                                        case "RadioButtonList":
//                                            ((RadioButtonList)ctrlf).Enabled = false;
//                                            break;
//                                        case "DropDownList":
//                                            ((DropDownList)ctrlf).Enabled = false;
//                                            break;
////										case "Image":
////											ctrlf.Visible = false;
////											break;
//                                        case "PopCalendar":
//                                            ctrlf.Visible = false;
//                                            break;
//                                    }
//                                }
//                            }
//                            break;
					}
//				}
			}
		}
	}
}
