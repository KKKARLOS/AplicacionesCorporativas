using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text.RegularExpressions;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de PonerValoresFiltros.
	/// </summary>
    public class PonerValoresFiltros : System.Web.UI.Page
    {
        public PonerValoresFiltros()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }
        public static void Actualizar(ControlCollection ColeccionControles, Hashtable FiltrosSeleccionados)
        {
            foreach (Control ctrl in ColeccionControles)
            {
                switch (ctrl.GetType().Name)
                {
                    case "TextBox":
                        ((TextBox)ctrl).Text = (string)FiltrosSeleccionados[((TextBox)ctrl).ID];
                        break;
                    case "ListBox":
                        string aValores = (string)FiltrosSeleccionados[((ListBox)ctrl).ID];
                        string[] aClavesSeleccion = Regex.Split(aValores, ",");
                        foreach (ListItem item in ((ListBox)ctrl).Items)
                        {
                            item.Selected = false;
                            for (int j = 0; j < aClavesSeleccion.Length; j++)
                            {
                                if (item.Value == aClavesSeleccion[j])
                                    item.Selected = true;
                            }
                        }
                        break;
                    case "CheckBox":
                        ((CheckBox)ctrl).Checked = (bool)FiltrosSeleccionados[((CheckBox)ctrl).ID];
                        break;
                    case "HtmlInputCheckBox":
                        ((HtmlInputCheckBox)ctrl).Checked = (bool)FiltrosSeleccionados[((HtmlInputCheckBox)ctrl).ID];
                        break;
                    case "RadioButtonList":
                        ((RadioButtonList)ctrl).SelectedValue = (string)FiltrosSeleccionados[((RadioButtonList)ctrl).ID];
                        break;
                    case "DropDownList":
                        ((DropDownList)ctrl).SelectedValue = (string)FiltrosSeleccionados[((DropDownList)ctrl).ID];
                        break;
                    case "HtmlGenericControl":
                        Actualizar(((HtmlGenericControl)ctrl).Controls,FiltrosSeleccionados);
                        break;
                }
            }
        }
    }
}
