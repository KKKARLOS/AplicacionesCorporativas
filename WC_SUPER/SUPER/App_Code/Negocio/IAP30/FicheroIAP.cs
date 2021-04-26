using System;
using System.Collections.Generic;
using System.Web;
using IB.SUPER.Shared;

using SUPER.Capa_Negocio;
//Para el ArrayList
using System.Collections;
//Para el DataTable
using System.Data;
//Para el StreamReader
using System.IO;
//para el stringbuilder
using System.Text;
//Para el RegEx
using System.Text.RegularExpressions;

/// <summary>
/// Descripción breve de FicheroIAP
/// </summary>
namespace IB.SUPER.IAP30.BLL
{

    public class FicheroIAP : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("85658DF8-57E7-46D0-AF3A-A8C420B25E8C");
        private bool disposed = false;

        #endregion
        #region Constructor

        public FicheroIAP()
            : base()
        {
            //OpenDbConn();
        }

        public FicheroIAP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion
        #region Funciones públicas

        public List<TAREA> GetTareas()
        {

            OpenDbConn();

            DAL.FicheroIAP cFicheroIAP = new DAL.FicheroIAP(cDblib);
            return cFicheroIAP.GetTareas();

        }
        public List<PROFESIONAL> GetProfesionales()
        {

            OpenDbConn();

            DAL.FicheroIAP cFicheroIAP = new DAL.FicheroIAP(cDblib);
            return cFicheroIAP.GetProfesionales();

        }
        public int Update(byte t722_idtipo, int t001_idficepi, byte[] t722_fichero)
        {
            Guid methodOwnerID = new Guid("28C4A7B3-2633-4B35-ACBB-DDC7A0946273");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.FicheroIAP cFicheroIAP = new DAL.FicheroIAP(cDblib);

                int iRes=cFicheroIAP.Update(t722_idtipo, t001_idficepi, t722_fichero);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return iRes;

            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        private static Hashtable CargarArrayTareas()
        {
            Hashtable htTarea = new Hashtable();
            BLL.FicheroIAP bFicheroIAP = null;
            try
            {
                bFicheroIAP = new BLL.FicheroIAP();

                if (HttpContext.Current.Cache["TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] == null)
                {
                    List<TAREA> oListaTareas = bFicheroIAP.GetTareas();
                    foreach (TAREA OT in oListaTareas) //Recorro tabla de TAREA
                    {
                        htTarea.Add(OT.t332_idtarea.ToString(), new TAREA(OT.t332_idtarea, OT.t332_destarea, OT.t331_idpt,
                                                                            OT.t332_estado, OT.t332_cle, OT.t332_tipocle, OT.t332_impiap,
                                                                            OT.t305_idproyectosubnodo, OT.t332_fiv, OT.t332_ffv,
                                                                            OT.t323_regjornocompleta, OT.t331_obligaest,
                                                                            OT.t331_estado, OT.t323_regfes, OT.t301_estado));
                    }
                    HttpContext.Current.Cache["TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] = htTarea;
                }
                else
                    htTarea = (Hashtable)HttpContext.Current.Cache["TareasFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()];
                return htTarea;
            }
            catch
            {
                throw (new Exception("Error al obtener las consultas para la carga de datos."));
            }
            finally
            {
                bFicheroIAP.Dispose();
            }
        }
        private static Hashtable CargarArrayProfesionales()
        {
            Hashtable htProfesional = new Hashtable();
            BLL.FicheroIAP bFicheroIAP = null;
            try
            {
                bFicheroIAP = new BLL.FicheroIAP();

                if (HttpContext.Current.Cache["ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] == null)
                {
                    List<PROFESIONAL> oListaProf = bFicheroIAP.GetProfesionales();
                    foreach (PROFESIONAL OP in oListaProf) //Recorro tabla de Profesionales
                    {
                        htProfesional.Add(OP.t314_idusuario.ToString(),
                                                    new PROFESIONAL(OP.t001_idficepi, OP.t314_idusuario, OP.Profesional,
                                                                    OP.t303_ultcierreIAP, OP.t314_jornadareducida, OP.t303_idnodo,
                                                                    OP.t314_horasjor_red, OP.t314_fdesde_red, OP.t314_fhasta_red,
                                                                    OP.t314_controlhuecos, OP.fUltImputacion, OP.t066_idcal,
                                                                    OP.t066_descal, OP.SemanaLaboral, OP.t001_codred, OP.fAlta, OP.fBaja));
                    }
                    HttpContext.Current.Cache["ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()] = htProfesional;
                }
                else
                    htProfesional = (Hashtable)HttpContext.Current.Cache["ProfesionalesFicheroIAP_" + HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString()];

                return htProfesional;
            }
            catch
            {
                throw (new Exception("Error al obtener las consultas para la carga de datos."));
            }
            finally
            {
                bFicheroIAP.Dispose();
            }
        }
        private static Models.FicheroIAP_Errores_Linea ponerFilaError(string sMens, int iCont)
        {
            Models.FicheroIAP_Errores_Linea oLin = new Models.FicheroIAP_Errores_Linea();

            oLin.Fila = iCont;
            oLin.Error = sMens;

            return oLin;
        }
        private static bool existenHuecos(Models.Calendario oCal, DateTime dDesde, int iDCalendario, DateTime dUDR)
        {
            bool bResul = false;
            BLL.DesgloseCalendario bDesglose = new DesgloseCalendario();

            try
            {
                List<Models.DesgloseCalendario> lstDias = bDesglose.ObtenerHorasRango(iDCalendario, dUDR, dDesde);

                int nDif =IB.SUPER.Shared.Fechas.DateDiff("day", dUDR, dDesde);
                if (nDif <= 0)
                {
                    ///Si nDif fuera menor o igual a 0, es que se va a imputar a una fecha anterior a
                    ///la fecha de última imputación, por lo que no hay huecos.
                    bResul = false;
                }
                else
                {
                    bool bFestAux = false;
                    for (int i = 1; i < nDif; i++)
                    {
                        bFestAux = false;
                        DateTime dDiaAux = dUDR.AddDays(i);
                        #region laborable y no festivo
                        foreach (Models.DesgloseCalendario oDia in lstDias)
                        {
                            if (oDia.t067_dia == dDiaAux)
                            {
                                //Festivo
                                if (oDia.t067_festivo == 1)
                                {
                                    bFestAux = true;
                                    break;
                                }
                                //No laborable
                                switch (oDia.t067_dia.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                        if (oCal.t066_semlabL == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Tuesday:
                                        if (oCal.t066_semlabM == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Wednesday:
                                        if (oCal.t066_semlabX == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Thursday:
                                        if (oCal.t066_semlabJ == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Friday:
                                        if (oCal.t066_semlabV == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Saturday:
                                        if (oCal.t066_semlabS == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Sunday:
                                        if (oCal.t066_semlabD == 0) bFestAux = true;
                                        break;
                                }
                                if (bFestAux) break;
                            }
                        }
                        #endregion
                        if (!bFestAux)
                        {
                            bResul = true;
                            break;
                        }
                    }
                }

                return bResul;
            }
            catch (Exception e) {
                throw new Exception("Error al obtener el calendario " + iDCalendario.ToString() + ". " + e.Message);
            }
            finally
            {
                bDesglose.Dispose();
            }
        }
        private string ControlLimiteEsfuerzos(int nTarea, double nHoras, Hashtable htTarea, DateTime fecha, Hashtable htTareasSuperanCLE)
        {
            string sResul = "OK", sTipoCle = "", sDesTarea = "";
            double dCle = 0;
            int idPT = -1;//idProy = -1, 
            BLL.TareaPSP bTareaPSP = new TareaPSP();

            try
            {
                TAREA oTarea = (TAREA)htTarea[nTarea.ToString()];

                if (oTarea != null)
                {
                    dCle = oTarea.t332_cle;
                    sTipoCle = oTarea.t332_tipocle;
                    sDesTarea = oTarea.t332_destarea;
                    //idProy = int.Parse(dr["t301_idproyecto"].ToString());
                    idPT = oTarea.t331_idpt;
                }

                if (idPT != -1)
                {
                    
                    sResul = bTareaPSP.ControlLimiteEsfuerzos(nTarea, nHoras, fecha, htTareasSuperanCLE);
                    if (sResul == "") sResul = "OK";
                }                

                return sResul;
            }
            catch (Exception e) { throw new Exception("Error al obtener el control de límite de esfuerzos para la tarea " + nTarea.ToString() + e.Message); }
            finally { bTareaPSP.Dispose(); }
        }
        protected string GenerarCorreoTraspasoIAP(ArrayList aListCorreo, string sProfesional, string sTO, string sProy, string sProyTec, 
                                                  string sFase, string sActiv, string sTarea, string sFecha, string sConsumo)
        {
            string sResul = "", sAsunto = "", sTexto = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                sAsunto = "Imputación en IAP a tarea con el traspaso de dedicaciones al módulo económico ya realizado.";

                sb.Append("<BR>SUPER le informa de que se ha producido una imputación de consumo a tarea en IAP estando el traspaso de dedicaciones al módulo económico realizado.");
                sb.Append("<BR>La imputación ha sido realizada por " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + "<BR><BR>");
                sb.Append("<label style='width:120px'>Profesional: </label><b>" + sProfesional + "</b><br>");
                sb.Append("<label style='width:120px'>Proyecto económico: </label><b>" + sProy + "</b><br>");
                sb.Append("<label style='width:120px'>Proyecto Técnico: </label>" + sProyTec + "<br>");
                if (sFase != "") sb.Append("<label style='width:120px'>Fase: </label>" + sFase + "<br>");
                if (sActiv != "") sb.Append("<label style='width:120px'>Actividad: </label>" + sActiv + "<br>");
                sb.Append("<label style='width:120px'>Tarea: </label>" + sTarea + "<br>");
                sb.Append("<label style='width:120px'>Fecha: </label>" + sFecha.Substring(0, 10) + "<br>");
                sb.Append("<label style='width:120px'>Dedicación: </label>" + sConsumo + "<br><br>");
                sTexto = sb.ToString();

                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);

                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de imputación IAP a tarea con traspaso IAP ya realizado.", ex);
            }
            return sResul;
        }

        public Models.FicheroIAP_Errores Grabar(string tipoFichero, HttpPostedFile selectedFile)
        {
            string sRes = "";
            bool bErrorControlado = false;
            bool bErrorCLE = false;
            string sResul = "";
            int iCont = 0, iNumOk = 0;
            Hashtable htProfesional, htTarea, htTareasSuperanCLE;
            ArrayList aListCorreo = new ArrayList();
            ArrayList aListCorreoCLE = new ArrayList();

            DataSet ds;
            DataSetHelper dsHelper;                        

            Models.FiguraNodo mFiguraNodo = new Models.FiguraNodo();
            Models.FicheroIAP_Errores oRes = new Models.FicheroIAP_Errores();
            Models.Usuario mUser=null;
            Models.TareaRecursos mTareaRecurso = null;
            Models.ConsumoIAPDia mConsumoIAPDia = null;
            Models.Calendario oCal = null;
            Models.UsuarioProyectoSubNodo mUserPSN = null;
            Models.ConsumoIAP mConsumoIAP = new Models.ConsumoIAP();
            //Models.TareaCTIAP mControlTraspasoIAP = null;
            List<Models.FicheroIAP_Errores_Linea> oListaE = new List<Models.FicheroIAP_Errores_Linea>();
            
            #region Apertura de conexión y transacción
            Guid methodOwnerID = new Guid("44A27D4A-279D-4452-8EAE-A2C1A284D162");
            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);
            #endregion

            BLL.FiguraNodo bFiguraNodo = new BLL.FiguraNodo(cDblib);
            BLL.ConsumoIAP bConsumoIAP = new BLL.ConsumoIAP(cDblib);
            BLL.Usuario bUsuario = new Usuario(cDblib);
            BLL.TareaRecursos bTareaRecurso = new TareaRecursos(cDblib);
            BLL.ConsumoIAPDia bConsumoIAPDia = new ConsumoIAPDia(cDblib);
            BLL.Calendario bCal = new Calendario(cDblib);
            BLL.DesgloseCalendario bDesglose = new DesgloseCalendario(cDblib);
            BLL.UsuarioProyectoSubNodo bUserPSN = new UsuarioProyectoSubNodo(cDblib);
            BLL.TareaCTIAP bControlTraspasoIAP = new TareaCTIAP(cDblib);

            try
            {
                #region Inicialización de variables
                iCont = 0;
                iNumOk = 0;
                bool bErrores = false;
                string sAsistente = " Asistente: " + HttpContext.Current.Session["DES_EMPLEADO"].ToString();
                sAsistente += "(IdUser=" + HttpContext.Current.Session["UsuarioActual"].ToString() + ") ";

                mFiguraNodo.t308_figura="S";
                mFiguraNodo.t314_idusuario = (int)HttpContext.Current.Session["UsuarioActual"];
                List<Models.FiguraNodo> lFigNodo = bFiguraNodo.Catalogo(mFiguraNodo);

                ArrayList alNodosUsu = new ArrayList();
                sAsistente += "-> Nodos(";
                foreach(Models.FiguraNodo oFig in lFigNodo)
                {
                    alNodosUsu.Add(oFig.t303_idnodo);
                    sAsistente += oFig.t303_idnodo.ToString() + ", ";
                }
                sAsistente += ")";

                try
                {
                    htTarea = CargarArrayTareas();
                    htProfesional = CargarArrayProfesionales();
                    htTareasSuperanCLE = new Hashtable();
                }
                catch (Exception ex)
                {
                    bErrorControlado = true;
                    throw (new Exception(ex.Message));
                }
                #endregion

                #region Procesar

                #region Crear Datatable principal tomando como base el fichero de entrada así como datatables auxiliares cara a las validaciones

                DataTable table_input = new DataTable("IAP_IN");

                table_input.Columns.Add("t332_idtarea", typeof(int));
                table_input.Columns.Add("t314_idusuario", typeof(int));
                table_input.Columns.Add("t337_fecha", typeof(DateTime));
                table_input.Columns.Add("t337_esfuerzo", typeof(double));
                table_input.Columns.Add("t337_comentario", typeof(string));
                table_input.Columns.Add("festivos", typeof(bool));
                table_input.Columns.Add("fila", typeof(int));

                #region recorrer el archivo para cargar las tablas auxiliares
                byte[] ArchivoEnBinario = new Byte[0];
                ArchivoEnBinario = new Byte[selectedFile.ContentLength]; //Crear el array de bytes con la longitud del archivo
                selectedFile.InputStream.Read(ArchivoEnBinario, 0, selectedFile.ContentLength); //Forzar al control del archivo a cargar los datos en el array
                selectedFile.InputStream.Position = 0;
                StreamReader r = new StreamReader(selectedFile.InputStream, System.Text.Encoding.UTF7);
                String strLinea = null;
                iCont = 0;
                while ((strLinea = r.ReadLine()) != null)
                {
                    iCont++;
                    if (strLinea != null)
                    {
                        string[] aAtributo = Regex.Split(strLinea, @"\t");
                        if (tipoFichero == "D")
                        {
                            table_input.Rows.Add(
                                            int.Parse(aAtributo[0]),            // IdTarea 
                                            int.Parse(aAtributo[1]),            // IdUsuario
                                            DateTime.Parse(aAtributo[2]),       // Fecha
                                            double.Parse(aAtributo[3]),         // Esfuerzo
                                            aAtributo[4],                       // Comentario
                                            true,                               // festivos
                                            iCont                               // Nro Línea erronea
                                            );
                        }
                        else
                        {
                            DateTime dDesde = DateTime.Parse(aAtributo[2]);     //  Fecha desde
                            DateTime dHasta = DateTime.Parse(aAtributo[3]);     //  Fecha hasta
                            int nDifDias = IB.SUPER.Shared.Fechas.DateDiff("day", dDesde, dHasta);
                            DateTime dDiaAux;
                            dDiaAux = DateTime.Parse("01/01/1900");

                            for (int i = 0; i <= nDifDias; i++)
                            {
                                dDiaAux = dDesde.AddDays(i);
                                table_input.Rows.Add(
                                            int.Parse(aAtributo[0]),                // IdTarea 
                                            int.Parse(aAtributo[1]),                // IdUsuario
                                            dDiaAux,                                // Fecha
                                            double.Parse(aAtributo[4]),             // Esfuerzo
                                            aAtributo[6],                           // Comentario
                                            (aAtributo[5] == "1") ? true : false,   // festivos
                                            iCont                                   // Nro Línea erronea
                                            );
                            }
                        }
                    }
                }
                #endregion
                // Crear DataTables auxiliares con información agrupada cara a las validaciones
                ds = new DataSet();
                dsHelper = new DataSetHelper(ref ds);

                // Agrupar en el fichero de entrada los esfuerzos por Usuario-Fecha-Tarea, para ver si existen varias imputaciones de un usuario a la misma tarea y a la misma fecha.
                DataTable table_usufechatarea = new DataTable("USUARIO_FECHA_TAREA");

                table_usufechatarea.Columns.Add("t332_idtarea", typeof(int));
                table_usufechatarea.Columns.Add("t314_idusuario", typeof(int));
                table_usufechatarea.Columns.Add("t337_fecha", typeof(DateTime));
                table_usufechatarea.Columns.Add("contador", typeof(double));

                // Para controlar que no haya más de una imputación a una tarea de un usuario en un día determinado
                DataTable TareaUsuarioFecha = new DataTable("TareaUsuarioFecha");
                TareaUsuarioFecha.Columns.Add("t332_idtarea", typeof(int));
                TareaUsuarioFecha.Columns.Add("t314_idusuario", typeof(int));
                TareaUsuarioFecha.Columns.Add("t337_fecha", typeof(DateTime));
                TareaUsuarioFecha.Columns.Add("Cantidad", typeof(int));

                DataColumn[] myKey1 = new DataColumn[3];
                myKey1[0] = table_usufechatarea.Columns["t332_idtarea"];
                myKey1[1] = table_usufechatarea.Columns["t314_idusuario"];
                myKey1[2] = table_usufechatarea.Columns["t337_fecha"];
                table_usufechatarea.PrimaryKey = myKey1;

                dsHelper.InsertGroupByInto(table_usufechatarea, table_input, "t314_idusuario, t337_fecha, t332_idtarea, count(*) contador", "", " t314_idusuario ASC, t337_fecha ASC, t332_idtarea ASC");
                // Una vez construido el Datatable lo recorro con la ordenación deseada
                DataRow[] oImputaciones = table_input.Select("", "t314_idusuario ASC, t337_fecha ASC, t332_idtarea ASC");
                #endregion
                #region borrado previo
                PROFESIONAL oProfesional;
                TAREA oTarea;

                DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);

                foreach (DataRow oImputacion in oImputaciones)  //Recorro tabla de imputaciones para borrar los consumos
                {
                    try
                    {
                        #region Eliminamos todos los consumos de un profesional en la fecha que tratamos.

                        oProfesional = (PROFESIONAL)htProfesional[oImputacion["t314_idusuario"].ToString()];
                        if (oProfesional == null) continue;

                        oTarea = (TAREA)htTarea[oImputacion["t332_idtarea"].ToString()];
                        if (oTarea == null) continue;

                        DateTime dDiaAux = DateTime.Parse(oImputacion["t337_fecha"].ToString());
                        //Consumo.EliminarRango(tr, oProfesional.t314_idusuario, dDiaAux, dDiaAux);
                        //bConsumoIAP.DeleteRango(oProfesional.t314_idusuario, dDiaAux, dDiaAux);
                        
                        cConsumoIAP.DeleteRango(oProfesional.t314_idusuario, dDiaAux, dDiaAux);
                        #endregion
                    }
                    catch
                    {
                        continue;
                    }
                }
                #endregion
                iCont = 0;
                int nFila = 0;
                foreach (DataRow oImputacion in oImputaciones)
                {
                    #region Recorro tabla de imputaciones
                    try
                    {
                        strLinea = oImputacion["t332_idtarea"].ToString() + ":";
                        strLinea += oImputacion["t314_idusuario"].ToString() + ":";
                        strLinea += oImputacion["t337_fecha"].ToString() + ":";
                        strLinea += oImputacion["t337_esfuerzo"].ToString() + ":";
                        strLinea += oImputacion["t337_comentario"].ToString() + ":";
                        strLinea += (bool.Parse(oImputacion["festivos"].ToString())) ? "1" : "0";

                        strLinea += "|";
                        nFila = int.Parse(oImputacion["fila"].ToString());
                        iCont++;

                        #region Obtención de datos relacionados con el PROFESIONAL y el último día reportado

                        oProfesional = (PROFESIONAL)htProfesional[oImputacion["t314_idusuario"].ToString()];
                        if (oProfesional == null)
                        {
                            bErrorControlado = true;
                            throw (new Exception("No existe el código del profesional."));
                        }

                        int? iUMC_IAP = null;
                        //Recurso oRecurso = new Recurso();

                        //bool bIdentificado = oRecurso.ObtenerRecurso(oProfesional.t001_codred, (int.Parse(oImputacion["t314_idusuario"].ToString()) == 0) ? null : (int?)int.Parse(oImputacion["t314_idusuario"].ToString()));

                        iUMC_IAP = (oProfesional.t303_ultcierreIAP.HasValue) ? oProfesional.t303_ultcierreIAP : DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;
                        if (iUMC_IAP != null)
                        {
                            if (IB.SUPER.Shared.Fechas.FechaAAnnomes(DateTime.Parse(oImputacion["t337_fecha"].ToString())) <= iUMC_IAP)
                            {
                                bErrorControlado = true;
                                throw (new Exception("La fecha de imputación (" + oImputacion["t337_fecha"].ToString() + ") pertenece a un mes IAP cerrado. Último mes cerrado IAP (" + IB.SUPER.Shared.Fechas.AnnomesAFechaDescLarga(int.Parse(iUMC_IAP.ToString())) + ")."));
                            }
                            if (DateTime.Parse(oImputacion["t337_fecha"].ToString()) > IB.SUPER.Shared.Fechas.AnnomesAFecha(int.Parse(iUMC_IAP.ToString())).AddMonths(3).AddDays(-1))
                            {
                                bErrorControlado = true;
                                throw (new Exception("La fecha de imputación (" + oImputacion["t337_fecha"].ToString() + ") debe ser como máximo 2 meses posterior al último cierre IAP y se ha sobrepasado. Último mes cerrado IAP (" + IB.SUPER.Shared.Fechas.AnnomesAFechaDescLarga(int.Parse(iUMC_IAP.ToString())) + ")."));
                            }
                            if (DateTime.Parse(oImputacion["t337_fecha"].ToString()) < oProfesional.fAlta || DateTime.Parse(oImputacion["t337_fecha"].ToString()) > oProfesional.fBaja)
                            {
                                bErrorControlado = true;
                                throw (new Exception("La fecha de imputación (" + oImputacion["t337_fecha"].ToString() + ") debe estar entre la fecha de alta y de baja del profesional.(Fecha Alta:" + ((DateTime)oProfesional.fAlta.Value).ToShortDateString() + " / Fecha Baja:" + ((DateTime)oProfesional.fBaja.Value).ToShortDateString() + ")."));
                            }

                        }

                        // Controlar si es un recurso interno deberá pertenecer a un CR del ámbito 
                        // de visión del asistente, en caso de ser un recurso externo no será necesario.

                        // Si el usuario actual es administrador no realizar comprobaciones

                        if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "")
                        {
                            if (oProfesional.t303_idnodo != null)
                            {
                                bool bEncontrado = false;
                                foreach (int iNodo in alNodosUsu)
                                {
                                    if (oProfesional.t303_idnodo == iNodo)
                                    {
                                        bEncontrado = true;
                                        break;
                                    }
                                }

                                if (bEncontrado == false)
                                {
                                    bErrorControlado = true;
                                    throw (new Exception("Se trata del profesional interno " + oProfesional.Profesional + " (Nodo =" + oProfesional.t303_idnodo.ToString() + ") que no pertenece a ninguno de los CR'S a los que tiene ambito de visión el Asistente."));
                                }
                            }
                        }

                        mUser = bUsuario.GetFechaUltImputacion(int.Parse(oImputacion["t314_idusuario"].ToString()));
                        DateTime? dUDR = mUser.fUltImputacion;

                        #endregion

                        #region Obtención de datos relacionados con la TAREA
                        //Obtener los datos de la tarea a la que se va a imputar.

                        oTarea = (TAREA)htTarea[oImputacion["t332_idtarea"].ToString()];
                        if (oTarea == null)
                        {
                            bErrorControlado = true;
                            throw (new Exception("No existe el código de la tarea."));
                        }
                        //

                        if (oTarea.t301_estado == "C")
                        {
                            bErrorControlado = true;
                            throw (new Exception("El proyecto económico de la tarea está cerrado."));
                        }
                        //
                        DataRow[] rowUsuFechaTarea = table_usufechatarea.Select("t314_idusuario=" + oImputacion["t314_idusuario"].ToString() + " and t337_fecha='" + oImputacion["t337_fecha"].ToString() + "'" + " and t332_idtarea=" + oImputacion["t332_idtarea"].ToString() + "");

                        if (int.Parse(rowUsuFechaTarea[0]["contador"].ToString()) > 1)
                        {
                            bErrorControlado = true;
                            throw (new Exception("El usuario '" + oProfesional.Profesional + "' para el día " + oImputacion["t337_fecha"].ToString() + " no puede tener para la tarea '" + oTarea.t332_destarea + "' más de una imputación"));
                        }

                        #endregion

                        #region Obtención de datos de la IMPUTACION y del PROFESIONAL

                        double nHoras = double.Parse(oImputacion["t337_esfuerzo"].ToString());
                        bool bFestivos = bool.Parse(oImputacion["festivos"].ToString());
                        bool bRegjornocompleta = bool.Parse(oTarea.t323_regjornocompleta.ToString());
                        bool bRegFes = bool.Parse(oTarea.t323_regfes.ToString());

                        bool bObligaEst = bool.Parse(oTarea.t331_obligaest.ToString());

                        string sComentario = oImputacion["t337_comentario"].ToString();

                        bool bFestAux = false;
                        DateTime dDiaAux = DateTime.Parse(oImputacion["t337_fecha"].ToString());
                        float nHorasDia = 0; // ojo
                        double nJornadas = 0;

                        #endregion

                        #region El identificador de usuario debe estar activo para esa tarea

                        mTareaRecurso = bTareaRecurso.ObtenerTareaRecurso(oTarea.t332_idtarea, oProfesional.t314_idusuario);
                        if (mTareaRecurso==null)
                        {
                            bErrorControlado = true;
                            throw (new Exception("La tarea no está asignada al usuario."));
                        }

                        bool bEstadoUsu = true;
                        if (mTareaRecurso.t336_estado == 0) bEstadoUsu = false;

                        if (!bEstadoUsu)
                        {
                            bErrorControlado = true;
                            throw (new Exception("El identificador de usuario debe estar activo para esta tarea."));
                        }
                        #endregion

                        #region Controlar si el proyecto técnico obliga a realizar estimaciones a nivel de tarea

                        if (bObligaEst)
                        {
                            if (mTareaRecurso.t336_ete == 0 || mTareaRecurso.t336_ffe == null)
                            {
                                bErrorControlado = true;
                                throw (new Exception("Es obligatorio realizar estimaciones para esta tarea y no se han hecho. No es posible cargar los esfuerzos de esta tarea desde fichero."));
                            }
                        }
                        #endregion

                        #region Vemos si con esa imputación si se superan las 24 h/diarias
                        //Obtener las imputaciones de otras tareas.
                        DAL.ConsumoIAPDia cConsumoIAPDia = new DAL.ConsumoIAPDia(cDblib);
                        mConsumoIAPDia = cConsumoIAPDia.Select(oProfesional.t314_idusuario, dDiaAux, oTarea.t332_idtarea);
                        double nImpDia = mConsumoIAPDia.nHorasDiaGlobal;         //Consumos totales del día de otras tareas.

                        double nTotalHoras = nHoras + nImpDia; // +nImpDiaTarea;
                        double nTotalTarea = nHoras; // +nImpDiaTarea;

                        if (nTotalHoras > 24)
                        {
                            bErrorControlado = true;
                            throw (new Exception("Las imputaciones del día " + dDiaAux.ToShortDateString() + " superan las 24h."));
                        }
                        #endregion

                        #region Control de huecos
                        oCal = bCal.getCalendario(oProfesional.t066_idcal, dDiaAux.Year);
                        ///Antes de hacer nada, comprobar que no se dejan huecos. 
                        if (oProfesional.t314_controlhuecos)
                        {
                            ///Controlar si entre el último día imputado (f_ult_imputac)
                            ///y el primer día de imputación (dDiaAux) hay días laborables.

                            if (existenHuecos(oCal, dDiaAux, oProfesional.t066_idcal, mUser.fUltImputacion))
                            {
                                bErrorControlado = true;
                                throw (new Exception("Se ha detectado que entre el último día reportado y la fecha inicio imputación existen huecos."));
                            }
                        }
                        #endregion

                        #region Controlar el límite de esfuerzos    
                       
                        string sCLE = ControlLimiteEsfuerzos(oTarea.t332_idtarea, nHoras, htTarea, dDiaAux, htTareasSuperanCLE);

                        if (sCLE != "OK")
                        {
                            bErrorCLE = true;
                            throw (new Exception(sCLE));
                        }
                        
                        #endregion

                        #region Obtención de datos relacionados con la tarea para posteriores validaciones
                        List<Models.DesgloseCalendario> lstDias;
                        //Obtención de las horas estándar y festivos del rango de fechas.
                        try
                        {
                            lstDias = bDesglose.ObtenerHorasRango(oProfesional.t066_idcal, DateTime.Parse(oImputacion["t337_fecha"].ToString()), DateTime.Parse(oImputacion["t337_fecha"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            bErrorControlado = true;
                            throw (new Exception(ex.Message));
                        }

                        try
                        {
                            mUserPSN = bUserPSN.Select(oTarea.t305_idproyectosubnodo, int.Parse(oImputacion["t314_idusuario"].ToString()));
                        }
                        catch (Exception ex)
                        {
                            bErrorControlado = true;
                            throw (new Exception(ex.Message));
                        }

                        //Obtener las fechas de inicio y final de la asociación del recurso al proyecto.
                        DateTime dAltaProy = mUserPSN.t330_falta;
                        DateTime? dBajaProy = (mUserPSN.t330_fbaja.HasValue) ? mUserPSN.t330_fbaja : null;

                        if (dAltaProy == DateTime.Parse("01/01/1900"))
                        {
                            bErrorControlado = true;
                            throw (new Exception("No existe fecha de alta en el proyecto."));
                        }
                        #endregion

                        #region Control día laborable y no festivo
                        foreach (Models.DesgloseCalendario oDia in lstDias)
                        {
                            if (oDia.t067_dia == dDiaAux)
                            {
                                nHorasDia = float.Parse(oDia.t067_horas.ToString());
                                if (nHorasDia == 0) nJornadas = 1;
                                else nJornadas = nHoras / nHorasDia;
                                //Festivo
                                if (oDia.t067_festivo == 1)
                                {
                                    bFestAux = true;
                                    break;
                                }
                                //No laborable
                                switch (oDia.t067_dia.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                        if (oCal.t066_semlabL == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Tuesday:
                                        if (oCal.t066_semlabM == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Wednesday:
                                        if (oCal.t066_semlabX == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Thursday:
                                        if (oCal.t066_semlabJ == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Friday:
                                        if (oCal.t066_semlabV == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Saturday:
                                        if (oCal.t066_semlabS == 0) bFestAux = true;
                                        break;
                                    case DayOfWeek.Sunday:
                                        if (oCal.t066_semlabD == 0) bFestAux = true;
                                        break;
                                }
                                if (bFestAux) break;
                            }
                        }
                        #endregion

                        #region Control de jornada reducida

                        double nHorasRed = 0;
                        DateTime? dDesdeRed = null;
                        DateTime? dHastaRed = null;

                        if (oProfesional.t314_jornadareducida)
                        {
                            nHorasRed = oProfesional.t314_horasjor_red;
                            dDesdeRed = oProfesional.t314_fdesde_red;
                            dHastaRed = oProfesional.t314_fhasta_red;
                        }
                        #endregion

                        #region Controlar vigencia de la tarea

                        ///Control para verificar las fechas de vigencia de la tarea dentro del periodo seleccionado
                        if ((oTarea.t332_fiv == null || dDiaAux >= oTarea.t332_fiv) && (oTarea.t332_ffv == null || dDiaAux <= oTarea.t332_ffv))
                        {
                            #region Imputación
                            ///Control para verificar las fechas de asociación del recurso al proyecto.
                            if (dDiaAux >= dAltaProy && (dBajaProy == null || dDiaAux <= dBajaProy))
                            {
                                //Ahora, si el día es laborable y no festivo, insert de las horas estándar.
                                if (bFestivos || (!bFestivos && !bFestAux))
                                {
                                    #region Control de jornada reducida.
                                    if (oProfesional.t314_jornadareducida)
                                    {
                                        if (dDiaAux >= dDesdeRed && dDiaAux <= dHastaRed)
                                        {
                                            nHorasDia = float.Parse(nHorasRed.ToString());
                                            nJornadas = nHoras / nHorasDia;
                                        }
                                    }
                                    #endregion
                                    #region Controlar segun la naturaleza del proyecto si obliga imputar o no a jornada completa

                                    if (bRegjornocompleta == false)
                                    {
                                        if (float.Parse(nHoras.ToString()) != nHorasDia)
                                        {
                                            bErrorControlado = true;
                                            throw (new Exception("Es obligatorio realizar la imputación de esta tarea a jornada completa."));
                                        }
                                    }
                                    if (!bRegFes && bFestivos && bFestAux)
                                    {
                                        bErrorControlado = true;
                                        throw (new Exception("El proyecto económico de la tarea no permite imputar en festivos."));
                                    }
                                    #endregion

                                    #region Imputación.
                                    try
                                    {
                                        mConsumoIAP.t332_idtarea=int.Parse(oImputacion["t332_idtarea"].ToString());
                                        mConsumoIAP.t314_idusuario=int.Parse(oImputacion["t314_idusuario"].ToString());
                                        mConsumoIAP.t337_fecha=dDiaAux;
                                        mConsumoIAP.t337_esfuerzo=(float)nHoras;
                                        mConsumoIAP.t337_esfuerzoenjor = nJornadas;
                                        mConsumoIAP.t337_comentario = sComentario;
                                        mConsumoIAP.t337_fecmodif = DateTime.Now;
                                        mConsumoIAP.t314_idusuario_modif = (int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"];

                                        cConsumoIAP.Insert(mConsumoIAP);
                                        //bConsumoIAP
                                    }
                                    catch (Exception ex)
                                    {
                                        bErrores = true;
                                        bErrorControlado = true;
                                        throw (new Exception(ex.Message));
                                    }
                                    #endregion
                                    #region  Control de traspaso de IAP realizado
                                    List<Models.TareaCTIAP> lstTraspasoIAP = bControlTraspasoIAP.Catalogo(int.Parse(oImputacion["t332_idtarea"].ToString()), dDiaAux);
                                    foreach(Models.TareaCTIAP oElem in lstTraspasoIAP)
                                    {
                                        string sRe = GenerarCorreoTraspasoIAP(aListCorreo, 
                                                     oProfesional.Profesional, oElem.MAIL,
                                                     oElem.t301_idproyecto.ToString("#,###") + " " + oElem.t301_denominacion,
                                                     oElem.t331_despt, oElem.t334_desfase, oElem.t335_desactividad,
                                                     oElem.t332_idtarea.ToString("#,###")+ " " + oElem.t332_destarea,
                                                     dDiaAux.ToString(), nHoras.ToString("N"));
                                        string[] aRe = Regex.Split(sRe, "@#@");
                                        if (aRe[0] != "OK")
                                        {
                                            bErrorControlado = true;
                                            throw (new Exception(aRe[1]));
                                        }
                                    }
                                    #endregion
                                }
                                iNumOk++;
                            }
                            else
                            {
                                bErrorControlado = true;
                                throw (new Exception("En la fecha de imputación seleccionada, el recurso se encuentra en parte o totalmente fuera de su asignación al proyecto. "));
                            }
                            #endregion
                        }
                        else
                        {
                            bErrorControlado = true;
                            throw (new Exception("La fecha de imputación seleccionada se encuentra en parte o totalmente fuera del periodo de vigencia la tarea. "));
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        if (bErrorCLE)
                        {
                            sResul = "La imputación por fichero no se ha procesado correctamente: " + strLinea + ex.Message + sAsistente;                            
                        }
                        else sResul = "Datos de entrada: " + strLinea + ex.Message + sAsistente;
                        oListaE.Add(ponerFilaError(sResul, nFila));
                        bErrores = true;
                        if (bErrorCLE) break;
                    }
                    #endregion
                }

                if (bErrores)
                {
                    //rollback
                    if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);
                }
                else
                {
                    //Finalizar transacción 
                    if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
                }
                #endregion

                #region Envio de correo a responsables por CLE
                try
                {
                    if (htTareasSuperanCLE.Count > 0)
                    {
                        foreach (int idTarea in htTareasSuperanCLE.Keys)
                        {
                            aListCorreoCLE = (ArrayList)((Models.TareaCLE)htTareasSuperanCLE[idTarea]).destinatariosMail;
                            if (aListCorreoCLE.Count > 0) Correo.EnviarCorreos(aListCorreoCLE);
                        }

                    }

                }
                catch (Exception ex)
                {
                    IB.SUPER.Shared.LogError.LogearError("Error al enviar el mail de control de límite de esfuerzo", ex);
                }
                #endregion

                #region Envio de correo a responsables
                try
                {
                    if ((bErrores == false) && aListCorreo.Count > 0) 
                        Correo.EnviarCorreos(aListCorreo);
                }
                catch (Exception ex)
                {
                    throw new Exception("Imputaciones grabadas pero ha ocurrido un error al enviar el mail a los responsables del proyecto");
                }
                #endregion

                oRes.nFilas = iCont;
                oRes.nFilasC = iNumOk;
                oRes.nFilasE = iCont - iNumOk;
                if (oListaE.Count > 0)
                    oRes.Errores = oListaE;

                return oRes;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);

                if (bErrorControlado)
                    sRes = sResul;
                else
                    sRes = ex.Message;// System.Uri.EscapeDataString(ex.Message);
                oListaE.Add(ponerFilaError(sRes, iCont));
                oRes.Errores = oListaE;
                //throw new Exception(sRes);
                return oRes;
            }
            finally
            {
                bFiguraNodo.Dispose();
                bConsumoIAP.Dispose();
                bUsuario.Dispose();
                bTareaRecurso.Dispose();
                bConsumoIAPDia.Dispose();
                bCal.Dispose();
                bDesglose.Dispose();
                bUserPSN.Dispose();
                bControlTraspasoIAP.Dispose();
            }

        }

        #endregion
        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);
        }
        private void AttachDbConn(sqldblib.SqlServerSP extcDblib)
        {
            cDblib = extcDblib;
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) if (cDblib != null && cDblib.OwnerID.Equals(classOwnerID)) cDblib.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~FicheroIAP()
        {
            Dispose(false);
        }

        #endregion

    }
}