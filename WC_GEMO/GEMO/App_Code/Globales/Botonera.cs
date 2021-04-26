using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using EO.Web;
using GEMO.DAL;
using System.Collections;

using System.Web;
namespace GEMO.BLL
{
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
            htBotones.Add(2, new Boton("Nuevo", "Nuevo", "Nuevo"));
            htBotones.Add(3, new Boton("Salir", "Salir", "Salir"));
            htBotones.Add(4, new Boton("Grabar", "Grabar", "Grabar"));
            htBotones.Add(5, new Boton("Integrantes", "Integrantes", "Integrantes"));
            htBotones.Add(6, new Boton("Regresar", "Regresar", "Regresar"));
            htBotones.Add(7, new Boton("Eliminar", "Eliminar", "Eliminar"));
            htBotones.Add(8, new Boton("Administradores", "Mantenimiento de administradores", "Admin."));
            htBotones.Add(9, new Boton("Tramitar", "Tramita la reasignación del móvil", "Tramitar"));
            htBotones.Add(10, new Boton("Anular", "Anula la reserva", "Anular"));
            htBotones.Add(11, new Boton("Inicio", "Regresa al inicio de la aplicación", "Inicio"));
            htBotones.Add(12, new Boton("Salas", "Mantenimiento de salas", "Salas"));
            htBotones.Add(13, new Boton("Usuarios", "Mantenimiento de usuarios", "Usuarios"));
            htBotones.Add(14, new Boton("Cancelar", "Regresa sin realizar cambios", "Cancelar"));
            htBotones.Add(15, new Boton("Destinatarios", "Destinatarios de las facturas", "Destinatarios"));
            htBotones.Add(16, new Boton("Facturas", "Facturas en formato excel", "Facturas"));
            htBotones.Add(17, new Boton("Reasignar", "Reasigna un movil a otro usuario", "Reasignar"));
            htBotones.Add(18, new Boton("Excel", "Exportar tabla a Excel", "Exportar"));
            htBotones.Add(19, new Boton("Historico", "Muestra la historia de un móvil", "Histórico"));
            htBotones.Add(20, new Boton("Ejecutar", "Ejecutar la reasignación de un móvil", "Ejecutar"));
            htBotones.Add(21, new Boton("Reasignaciones", "Catálogo de reasignaciones pendientes de ejecutar", "Reasignac."));
            htBotones.Add(22, new Boton("Inventario", "Correo a los destinatarios de móviles para actualizar inventario", "Inventario"));
            htBotones.Add(23, new Boton("Adjudicar", "Adjudica la factura a otro destinatario", "Adjudicar"));
            htBotones.Add(24, new Boton("Controladores", "Mantenimiento de controladores", "Control"));
            htBotones.Add(25, new Boton("GrabarComo", "Grabar como...", "Grabar como"));
            htBotones.Add(26, new Boton("Asignar", "Asigna las horas en el rango de fechas indicado", "Asignar"));
            htBotones.Add(27, new Boton("InicioFec", "Establece el inicio del periodo a asignar", "Inicio asig."));
            htBotones.Add(28, new Boton("FinFec", "Establece el fin del periodo a asignar", "Fin asig."));
            htBotones.Add(29, new Boton("Festivo", "Asigna o desasigna el día seleccionado como festivo", "Festivo"));
            htBotones.Add(30, new Boton("Horario", "Horario del calendario", "Horario"));
            htBotones.Add(31, new Boton("Limpiar", "Limpia los datos introducidos en la pantalla", "Limpiar"));
            htBotones.Add(32, new Boton("Borrar", "Borra los datos indicados", "Borrar"));
            htBotones.Add(33, new Boton("Estructura", "Estructura de la plantilla", "Estructura"));
            htBotones.Add(34, new Boton("Comentario", "Comentario", "Comentario"));
            htBotones.Add(35, new Boton("Traspaso", "Traspasa los esfuerzos totales estimados a esfuerzos totales previstos y la fecha de fin estimada a fecha de fin prevista", "Traspaso Plan."));
            htBotones.Add(36, new Boton("Indicaciones", "Indicaciones y observaciones", "Indicaciones"));
            htBotones.Add(37, new Boton("Valores", "Valores de los atributos estadísticos", "Valores"));
            htBotones.Add(38, new Boton("PDF", "Genera un informe en formato PDF", "Exportar"));
            htBotones.Add(39, new Boton("Marcar", "Marca todas las filas del catálogo", "Marcar"));
            htBotones.Add(40, new Boton("Desmarcar", "Desmarca todas las filas del catálogo", "Desmarcar"));
            htBotones.Add(41, new Boton("XML", "Importa al catálogo un fichero de formato XML con las OTC", "Importar"));
            htBotones.Add(42, new Boton("PlantillaPE", "Genera plantilla de proyecto económico con la estructura técnica actual", "Gen. Pla. PE"));
            htBotones.Add(43, new Boton("HorarioOff", "Borrar Horario", "Borrar hor."));
            htBotones.Add(44, new Boton("PlantillaPT", "Genera plantilla de proyecto técnico con la estructura técnica actual", "Gen. Pla. PT"));
            htBotones.Add(45, new Boton("Instantanea", "Genera una 'Instantánea' con los datos del seguimiento de proyecto", "Instantánea"));
            htBotones.Add(46, new Boton("Gantt", "Muestra el diagrama de Gantt del proyecto", "Gantt"));
            htBotones.Add(47, new Boton("InsertarMes", "Inserta meses", "Insertar mes"));
            htBotones.Add(48, new Boton("BorrarMes", "Permite borrar meses", "Borrar mes"));
            htBotones.Add(49, new Boton("Replica", "Replica el proyecto a los destinos correspondientes", "Replicar"));
            htBotones.Add(50, new Boton("Procesar", "Procesar", "Procesar"));
            htBotones.Add(51, new Boton("CerrarMes", "Cierra el mes estrella, replicando si fuera necesario", "Cerrar mes"));
            htBotones.Add(52, new Boton("Generar", "Replica el proyecto a los destinos correspondientes y genera transferencias", "Generar"));
            htBotones.Add(53, new Boton("TraspasoIAP", "Traspasa los consumos a esfuerzos totales previstos y la fecha de última imputación a fecha de fin prevista", "Traspaso IAP"));
            htBotones.Add(54, new Boton("Bitacora", "Muestra la bitácora del proyecto económico seleccionado", "Bitácora PE"));
            htBotones.Add(55, new Boton("Reconexion", "Reconexión", "Reconexión"));
            htBotones.Add(56, new Boton("Jornada", "Imputar jornada completa", "Jornada"));
            htBotones.Add(57, new Boton("Semana", "Imputar semana completa", "Semana"));
            htBotones.Add(58, new Boton("GrabarSS", "Grabar e ir a la semana siguiente", "Grabar sig."));
            htBotones.Add(59, new Boton("GrabarReg", "Grabar y regresar", "Grabar..."));
            htBotones.Add(60, new Boton("TareaBot", "Muestra el detalle de la tarea", "Tarea"));
            htBotones.Add(61, new Boton("ComentarioBot", "Comentario", "Comentario"));
            htBotones.Add(62, new Boton("Documentos", "Documentación asociada", "Documentación"));
            htBotones.Add(63, new Boton("GrabarProy", "Graba y calcula el siguiente proyecto", "GSP"));
            htBotones.Add(64, new Boton("TraspGrabProy", "Traspasa los consumos, graba y calcula el siguiente proyecto", "TGSP"));
            htBotones.Add(65, new Boton("TraspGlobal", "Traspasa y graba los consumos de todos los proyectos bajo su responsabilidad", "Global"));
            htBotones.Add(66, new Boton("Cerrar", "Cerrar", "Cerrar"));
            htBotones.Add(67, new Boton("Aparcar", "Aparcar", "Aparcar"));
            htBotones.Add(68, new Boton("Recuperar", "Recuperar", "Recuperar"));
            htBotones.Add(69, new Boton("Historial", "Historial", "Historial"));
            htBotones.Add(70, new Boton("Parametrizar", "Parametrizar", "Parametrizar"));
            htBotones.Add(71, new Boton("Guia", "Guía", "Guía"));
            htBotones.Add(72, new Boton("ResumenEco", "Acceso al resumen económico", "Resumen"));
            htBotones.Add(73, new Boton("BitacoraPT", "Muestra la bitácora del proyecto técnico seleccionado", "Bitácora PT"));
            htBotones.Add(74, new Boton("Desactivar", "Elimina los profesionales asignados", "Desactivar"));
            htBotones.Add(75, new Boton("Duplicar", "Genera copia de la plantilla actual", "Duplicar"));
            htBotones.Add(76, new Boton("BitacoraT", "Muestra la bitácora de la tarea seleccionada", "Bitácora T"));
            htBotones.Add(77, new Boton("Auditoria", "Muestra la auditoría de datos modificados", "Auditoría"));
            htBotones.Add(78, new Boton("Clonar", "Copia los consumos y la producción de un mes determinado en uno o varios meses", "Clonar mes"));
            htBotones.Add(79, new Boton("Carrusel", "Acceso a la pantalla de Carrusel", "Carrusel"));
            htBotones.Add(80, new Boton("IcoTras", "Acceso a la pantalla de traspaso IAP", "Trasp. IAP"));
            htBotones.Add(81, new Boton("CargarEstr", "Copia la estrucura técnica de otro proyecto, eliminando la existente en el proyecto actual", "Copia Est."));
            htBotones.Add(82, new Boton("OrdenOriginal", "Muestra los datos de una orden de facturación en el momento de su tramitación", "Original"));
            htBotones.Add(83, new Boton("IcoTrasExc", "Realiza el traspaso IAP para los proyectos de los nodos identificados en la columna 'Traspaso IAP Exc.'", "Trasp. Exc"));
            htBotones.Add(84, new Boton("Comunicacion", "Espacio de comunicación para el soporte administrativo", "Comunicación"));
            htBotones.Add(85, new Boton("AgendaUSA", "Agenda del usuario de soporte administrativo", "Agenda USA"));
            htBotones.Add(86, new Boton("AparcarDel", "Elimina la situación aparcada", "Elim. aparcada"));
            htBotones.Add(87, new Boton("Obtener", "Ejecuta la consulta", "Obtener"));
            htBotones.Add(88, new Boton("GraficoVG", "Crea una línea base para la gestión del Valor Ganado", "Crear LB"));
            htBotones.Add(89, new Boton("Exportar", "Exporta a diferentes formatos", "Exportar"));
            htBotones.Add(90, new Boton("PoolFVP", "Pool de figuras virtuales de proyecto", "Pool FVP"));
            htBotones.Add(91, new Boton("Correo", "Envía por correo los curriculums de los profesionales seleccionados en el catálogo a los destinatarios que especifiquemos a continuación.", "Correo"));
            #endregion

            #region Relación de Botoneras
            htBotoneras.Add(1, new string[] { "3", "" });
            htBotoneras.Add(2, new string[] { "2,30,7,43", "" });
            htBotoneras.Add(3, new string[] { "4,26,27,28,29,31,6", "4" });
            htBotoneras.Add(4, new string[] { "4,25,30,6", "4" });
            htBotoneras.Add(5, new string[] { "0,38,18", "" });
            htBotoneras.Add(6, new string[] { "2,7,33,75,71", "" });
            htBotoneras.Add(7, new string[] { "4,33,6", "4" });
            htBotoneras.Add(8, new string[] { "4,25,6", "4" });
            htBotoneras.Add(9, new string[] { "4", "4" });
            htBotoneras.Add(10, new string[] { "4,35,36,18,45,53", "4,35,36,45,53" });
            htBotoneras.Add(11, new string[] { "2,6", "" });
            htBotoneras.Add(12, new string[] { "4,7,6", "4" });
            htBotoneras.Add(13, new string[] { "2,7,71", "" });
            htBotoneras.Add(14, new string[] { "6", "" });
            htBotoneras.Add(15, new string[] { "2,7,4,71", "4" });
            htBotoneras.Add(16, new string[] { "2,7,6", "" });
            htBotoneras.Add(17, new string[] { "18", "" });
            htBotoneras.Add(18, new string[] { "0,4,6", "4" });
            htBotoneras.Add(19, new string[] { "18", "" });
            htBotoneras.Add(20, new string[] { "9,4,18", "4" });
            htBotoneras.Add(21, new string[] { "4,42,44,46,62,81,18,71", "4,42,44,46,62,81,18" });
            htBotoneras.Add(22, new string[] { "38,18", "" });
            htBotoneras.Add(23, new string[] { "4,6", "4" });
            htBotoneras.Add(24, new string[] { "2,7,4,6", "4" });
            htBotoneras.Add(25, new string[] { "2,4,6", "4" });
            htBotoneras.Add(26, new string[] { "2,32,4", "4" });
            htBotoneras.Add(27, new string[] { "0,31", "" });
            htBotoneras.Add(28, new string[] { "4,18,6", "4" });
            htBotoneras.Add(29, new string[] { "0", "" });
            htBotoneras.Add(30, new string[] { "4,35,36,18,45,54", "4,35,36,45,54" });
            htBotoneras.Add(31, new string[] { "4,2,71", "4,2" });
            htBotoneras.Add(32, new string[] { "47,48,49,51,80,78,88,71", "47,48,49,51,80,78,88" });
            htBotoneras.Add(33, new string[] { "50,71", "50" });
            htBotoneras.Add(34, new string[] { "4", "4" });
            htBotoneras.Add(35, new string[] { "7,71", "7" });
            htBotoneras.Add(36, new string[] { "55", "55" });
            htBotoneras.Add(37, new string[] { "61,60,56,57,4,58,59,71,6", "61,60,56,57,4,58,59" });
            htBotoneras.Add(38, new string[] { "7", "7" });
            htBotoneras.Add(39, new string[] { "4,63,64,65,71", "4,63" });
            htBotoneras.Add(40, new string[] { "18,71", "" });
            htBotoneras.Add(41, new string[] { "51,66,80,83", "" });
            htBotoneras.Add(42, new string[] { "59,7,6,71", "7" });
            htBotoneras.Add(43, new string[] { "50", "" });
            htBotoneras.Add(44, new string[] { "", "" }); //Botonera libre.
            htBotoneras.Add(45, new string[] { "50,67,68,49,86", "" });
            htBotoneras.Add(46, new string[] { "50,69", "69" });
            htBotoneras.Add(47, new string[] { "50,70", "" });
            htBotoneras.Add(48, new string[] { "71", "" });
            htBotoneras.Add(49, new string[] { "4,71", "4" });
            htBotoneras.Add(50, new string[] { "51,71", "" });
            htBotoneras.Add(51, new string[] { "2,7", "" });
            htBotoneras.Add(52, new string[] { "2,7,4", "4" });
            htBotoneras.Add(53, new string[] { "7,6", "" });
            htBotoneras.Add(54, new string[] { "61,4,71", "61,4" });
            htBotoneras.Add(55, new string[] { "82", "82" });
            htBotoneras.Add(56, new string[] { "50", "50" });
            htBotoneras.Add(57, new string[] { "4,47,48", "" });
            htBotoneras.Add(58, new string[] { "6,18,7,71", "18,7" });
            htBotoneras.Add(59, new string[] { "89", "" });//Curvit MiCv
            htBotoneras.Add(60, new string[] { "4,0", "4" });//Curvit MiCv
            htBotoneras.Add(61, new string[] { "89,71", "89" });//Curvit MiCv
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
                            btn.NormalStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#3e6779;font-weight:normal;text-decoration:none;cursor:pointer;width:" + nWidthBoton.ToString() + "px;height:23px;padding-top:4px;padding-left:5px;";
                            btn.HoverStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#C1D2EE;width:" + (nWidthBoton - 2).ToString() + "px;height:22px;border-style:solid;border-width:1px;border-color:navy;padding-left:1px;padding-top:3px;padding-left:5px;";
                            btn.DownStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#97B2DE;width:" + (nWidthBoton - 2).ToString() + "px;height:22px;border-style:solid;border-width:1px;border-color:navy;padding-top:3px;padding-left:5px;";
                        }
                        else
                        {
                            btn.NormalStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#3e6779;font-weight:normal;text-decoration:none;cursor:pointer;width:" + nWidthBoton.ToString() + "px;height:23px;padding-top:4px;";
                            btn.HoverStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#C1D2EE;width:" + (nWidthBoton - 3).ToString() + "px;height:22px;border-style:solid;border-width:1px;border-color:navy;padding-left:1px;padding-top:3px;";
                            btn.DownStyle.CssText = "font-family:Arial, Helvetica, sans-serif;font-size:11px;color:#003333;font-weight:bold;text-decoration:none;cursor:pointer;background-color:#97B2DE;width:" + (nWidthBoton - 2).ToString() + "px;height:22px;border-style:solid;border-width:1px;border-color:navy;padding-top:3px;";
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
        /// Project	 : CGI
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
