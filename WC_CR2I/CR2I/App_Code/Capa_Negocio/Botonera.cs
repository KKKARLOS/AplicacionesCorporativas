using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
//using Microsoft.Web.UI.WebControls;
using EO.Web;
using CR2I.Capa_Datos;
using System.Collections;
using System.Web;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Botonera.
	/// </summary>
	public class CBotonera
	{
        private string botonesOpcionOn = "";
        private string botonesOpcionOff = "";
        private Hashtable htBotoneras = new Hashtable();
        private Hashtable htBotones = new Hashtable();
        
        public CBotonera()
		{
            #region Relación de botones
            htBotones.Add(0, new Boton("Buscar", "Buscar", "Buscar"));
            htBotones.Add(1, new Boton("Mantenimiento", "Mantenimiento", "Mantenim."));
            htBotones.Add(2, new Boton("Añadir", "Añadir", "Añadir"));
            htBotones.Add(3, new Boton("Salir", "Salir", "Salir"));
            htBotones.Add(4, new Boton("Grabar", "Grabar", "Grabar"));
            htBotones.Add(5, new Boton("Integrantes", "Integrantes", "Integrantes"));
            htBotones.Add(6, new Boton("Regresar", "Regresar", "Regresar"));
            htBotones.Add(7, new Boton("Eliminar", "Eliminar", "Eliminar"));
            htBotones.Add(8, new Boton("Administradores", "Mantenimiento de administradores", "Admin."));
            htBotones.Add(9, new Boton("Tramitar", "Tramita la reserva", "Tramitar"));
            htBotones.Add(10, new Boton("Anular", "Anula la reserva", "Anular"));
            htBotones.Add(11, new Boton("Inicio", "Regresa al inicio de la aplicación", "Inicio"));
            htBotones.Add(12, new Boton("Salas", "Mantenimiento de salas", "Salas"));
            htBotones.Add(13, new Boton("Usuarios", "Mantenimiento de usuarios", "Usuarios"));
            htBotones.Add(14, new Boton("Cancelar", "Regresa sin realizar cambios", "Cancelar"));
            htBotones.Add(15, new Boton("Nuevo", "Crear nueva reserva", "Nueva"));
            htBotones.Add(16, new Boton("Imprimir", "Imprime la reserva", "Imprimir"));
            htBotones.Add(17, new Boton("Procesar", "Procesa la petición de conexión wifi", "Procesar"));
            #endregion

            #region Relación de Botoneras
            htBotoneras.Add(1, new string[] { "6", "" });
            htBotoneras.Add(2, new string[] { "9,10,14", "" });
            htBotoneras.Add(3, new string[] { "11", "" });
            htBotoneras.Add(4, new string[] { "6", "" });
            htBotoneras.Add(5, new string[] { "6", "" });
            htBotoneras.Add(6, new string[] { "12,13,3", "" });
            htBotoneras.Add(7, new string[] { "3", "" });
            htBotoneras.Add(8, new string[] { "15,11", "" });
            htBotoneras.Add(9, new string[] { "17,10,14,6,16", "" });
            #endregion
        }

        public void CargarBotoneraEO(ToolBar objBot, int nIdBotonera, int nWidthBoton, string sbotonesOpcionOn, string sbotonesOpcionOff)
        {
            if ((nIdBotonera != 0) || (sbotonesOpcionOn != ""))
            {
                objBot.Items.Clear();

                if (nIdBotonera != 0)
                {
                    string[] oRelBotones = (string[])htBotoneras[nIdBotonera]; //cod_botonera --> botones on y off
                    if (oRelBotones != null)
                    {
                        botonesOpcionOn = oRelBotones[0];
                        botonesOpcionOff = oRelBotones[1];
                    }
                }
                else
                {
                    botonesOpcionOn = sbotonesOpcionOn;
                    botonesOpcionOff = sbotonesOpcionOff;
                }

                Regex r = new Regex(",");
                string[] OpcionOn = r.Split(botonesOpcionOn);
                string[] OpcionOff = r.Split(botonesOpcionOff);

                objBot.ID = "Botonera";
                objBot.Height = Unit.Pixel(25);
                objBot.Width = Unit.Percentage(101);

                objBot.AutoPostBack = true;
                objBot.Width = Unit.Percentage(100);
                //objBot.BackgroundImageLeft = "~/ToolBar/Style2/left.gif";
                //objBot.BackgroundImage = "~/ToolBar/Style2/bg.gif";
                //objBot.BackgroundImageRight = "~/ToolBar/Style2/right.gif";
                //objBot.SeparatorImage = "~/ToolBar/Style2/separator.gif";
                objBot.SeparatorImage = "~/Images/imgBtnSeparator.gif";
                objBot.ClientSideOnLoad = "CrearBotoneraCliente";


                ToolBarItem btn;
                ToolBarItem btnSep;

                for (int i = 0; i < OpcionOn.Length; i++)
                {
                    btn = new ToolBarItem();
                    btn.Type = ToolBarItemType.Button;
                    Boton oBoton = (Boton)htBotones[int.Parse(OpcionOn[i])];
                    if (oBoton != null)
                    {
                        btn.CommandName = oBoton.ID;
                        //btn.Text = "<nobr>&nbsp;&nbsp;" + oBoton.Nombre + "</nobr>";
                        btn.Text = oBoton.Nombre;
                        btn.ImageUrl = HttpContext.Current.Session["strServer"].ToString() + "Images/Botones/img" + oBoton.ID + ".gif";
                        //btn.AccessKey = oBoton.TeclaAcceso;
                        btn.ToolTip = oBoton.ToolTip;
                        if (i == 0)
                        {
                            btn.NormalStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#3e6779;font-weight:normal;text-decoration:none;cursor:pointer;width:" + nWidthBoton.ToString() + "px;height:18px;padding-top:3px;padding-left:5px;";
                            btn.HoverStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#C1D2EE;width:" + (nWidthBoton - 2).ToString() + "px;height:19px;border-style:solid;border-width:1px;border-color:navy;padding-top:3px;padding-left:5px;";
                            btn.DownStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#97B2DE;width:" + (nWidthBoton - 2).ToString() + "px;height:19px;border-style:solid;border-width:1px;border-color:navy;padding-top:3px;padding-left:5px;";
                        }
                        else
                        {
                            btn.NormalStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#3e6779;font-weight:normal;text-decoration:none;cursor:pointer;width:" + nWidthBoton.ToString() + "px;height:18px;padding-top:3px;padding-left:2px;";
                            btn.HoverStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#C1D2EE;width:" + (nWidthBoton - 2).ToString() + "px;height:19px;border-style:solid;border-width:1px;border-color:navy;padding-top:3px;padding-left:2px;";
                            btn.DownStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#97B2DE;width:" + (nWidthBoton - 2).ToString() + "px;height:19px;border-style:solid;border-width:1px;border-color:navy;padding-top:3px;padding-left:2px;";
                        }
                    }

                    for (int j = 0; j < OpcionOff.Length; j++)
                    {
                        if (OpcionOn[i] == OpcionOff[j])
                        {
                            btn.Disabled = true;
                            break;
                        }
                    }

                    btnSep = new ToolBarItem();
                    btnSep.Type = ToolBarItemType.Separator;

                    objBot.Items.Add(btn);
                    objBot.Items.Add(btnSep);
                }
            }
        }

	}
    public class Boton
    {
        private string _ID = "";
        private string _ToolTip = "";
        private string _Nombre = "";
        //private string _TeclaAcceso = "";

        public string ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }
        public string ToolTip
        {
            get { return this._ToolTip; }
            set { this._ToolTip = value; }
        }
        public string Nombre
        {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }
        //public string TeclaAcceso
        //{
        //    get { return this._TeclaAcceso; }
        //    set { this._TeclaAcceso = value; }
        //}

        /// -----------------------------------------------------------------------------
        /// Project	 : CR2I
        /// Class	 : Boton
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Construnctor de la clase Boton:
        /// sID: -> string con el ID que se le va a asignar al botón.
        /// sToolTip: -> string con el ToolTip que tendrá el botón por defecto.
        /// sNombre: -> string con el nombre que se mostrará en el botón.
        /// sTeclaAcceso: -> string con la tecla de acceso rápido.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public Boton(string sID, string sToolTip, string sNombre)//, string sTeclaAcceso
        {
            this.ID = sID;
            this.ToolTip = sToolTip;
            this.Nombre = sNombre;
            //this.TeclaAcceso = sTeclaAcceso;
        }
    }
}
