using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Clase para manejar la estructura (desglose) de un proyecto económico)
    /// </summary>
    public class EstrProy
    {
        public EstrProy()
        {
            // TODO: Add constructor logic here
        }
        public static SqlDataReader CatalogoHitos(int iNumProy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            //aParam[0] = new SqlParameter("@nCodUne", SqlDbType.Int);
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int);
            //aParam[0].Value = iCodUne;
            aParam[0].Value = iNumProy;

            return SqlHelper.ExecuteSqlDataReader("SUP_HITOCATA", aParam);
        }
        //<summary>
         
         //Obtiene si hay hitos asociados un proyecto técnico
         //Se utiliza para reasignar una tarea a los hitos que la engloban
         //</summary>
        public static bool bHayHitosPT(SqlTransaction tr, int iPT)
        {
            bool bResul = false;
            int nResul;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPT", SqlDbType.Int);
            aParam[0].Value = iPT;


            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITOCATA3", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_HITOCATA3", aParam));

            if (nResul > 0) bResul = true;
            return bResul;
        }
        /// <summary>
        /// 
        /// Comprueba si un proyecto económico tiene consumos
        ///	(para poder cargar el contenido de una plantilla de PE en la pantalla de desglose de proyecto)
        /// </summary>
        //public static bool ExistenConsumosPE(SqlTransaction tr, short iCodUne, int iNumProy)
        public static bool ExistenConsumosPE(SqlTransaction tr, int iNumProy)
        {
            bool bConsumos = false;
            SqlParameter[] aParam = new SqlParameter[1];
            //aParam[0] = new SqlParameter("@nCodUne", SqlDbType.SmallInt, 2);
            aParam[0] = new SqlParameter("@nCodProy", SqlDbType.Int, 4);
            //aParam[0].Value = iCodUne;
            aParam[0].Value = iNumProy;

            int nResul = 0;
            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTECONSUMO_PE", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EXISTECONSUMO_PE", aParam));

            if (nResul > 0) bConsumos = true;

            return bConsumos;
        }
        /// <summary>
        /// 
        /// Comprueba si un proyecto Técnico tiene consumos
        ///	(para poder cargar el contenido de una plantilla de PT en la pantalla de desglose de proyecto)
        /// </summary>
        public static bool ExistenConsumosPT(SqlTransaction tr, int iNumProyTec)
        {
            bool bConsumos = false;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nCodProy", SqlDbType.Int, 4);

            aParam[0].Value = iNumProyTec;

            int nResul = 0;
            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTECONSUMO_PT", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EXISTECONSUMO_PT", aParam));

            if (nResul > 0) bConsumos = true;

            return bConsumos;
        }
        //public static string estadoProyecto(string sCodUne, string sNumProy)
        public static string estadoProyecto(string sNumProy)
        {
            //string sRes = "C";//cerrado
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int, 4);
            aParam[0].Value = int.Parse(sNumProy);
            object nResul = SqlHelper.ExecuteScalar("SUP_PE_ESTADO", aParam);//.ExecuteSqlDataReader("SUP_PE_ESTADO", aParam);
            return nResul.ToString();
        }
        //Devuelve el nº de orden para el tipo de elemento que se quiere insertar
        //No se permite introducir hitos de proyecto económico
        public static short GetOrdenPT(int iT305IdProy)
        {//Obtiene el orden para la inserción de un PT como el máximo + 1
            short iOrden=0;

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int, 4);
            aParam[0].Value = iT305IdProy;
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_ORDEN_PT", aParam);
            if (dr.Read())
            {
                iOrden=short.Parse(dr[0].ToString());
            }
            else iOrden = 1;
            dr.Close();
            dr.Dispose();
            return iOrden;
        }
        #region Insertar
        public static string Insertar(SqlTransaction tr, int iCodUne, int iNumProy, int it305_IdProy,
                                   string sTipo, string sDesc, int iPT, int iFase, int iActiv, int iMargen, int iOrden,
                                   string sFIniPL, string sFfinPl, double fDuracion, string sFIniV, string sFfinV,
                                   decimal fPresup, bool bFacturable, bool bObligaEst, bool bAvanceAutomatico, 
                                   string sSituacion, string sObs, decimal fAvance)
        {
            int nResul = 0;
            byte iEstado;
            //bool bEstadoTarea = false;
            string sAviso="";
            sDesc = Utilidades.unescape(sDesc);
            sObs = Utilidades.unescape(sObs);
            switch (sTipo)
            {
                case "P":
                    //Compruebo si el CR tiene atributos estadisticos obligatorios
                    if (sSituacion == "") iEstado = 1;
                    else iEstado = byte.Parse(sSituacion);
                    //bEstadoTarea = ProyTec.bFaltanValoresAE(tr, (short)iCodUne, null);
                    //if (bEstadoTarea) iEstado = 2;
                    //else iEstado = 1;
                    //if (bEstadoTarea) sAviso = "Se han insertado proyectos técnicos que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que el proyecto técnico no tiene valores asignados";
                    nResul = ProyTec.Insert(tr, sDesc, it305_IdProy, iEstado, bObligaEst, (short)iOrden, null, sObs, false, false, "X", fPresup, fAvance, bAvanceAutomatico, ""); 
                    break;
                case "F": nResul = InsertarFase(tr, sDesc, iOrden, sObs, fPresup, fAvance, bAvanceAutomatico); break;
                case "A": nResul = InsertarActividad(tr, iFase, sDesc, iOrden, sObs, fPresup, fAvance, bAvanceAutomatico); break;
                case "T": 
                    //Compruebo si el CR tiene atributos estadisticos obligatorios
                    if (sSituacion == "") iEstado = 1;
                    else iEstado = byte.Parse(sSituacion);
                    //bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, (short)iCodUne, null);
                    //if (bEstadoTarea) iEstado = 2;
                    //else iEstado = 1;
                    //if (bEstadoTarea) sAviso = "Se han insertado tareas que quedan en estado Pendiente ya que el C.R. tiene atributos estadísticos\nobligatorios para los que la tarea no tiene valores asignados";
                    nResul = TAREAPSP.Insertar(tr, sDesc, iPT, iActiv, iOrden, sFIniPL, sFfinPl, fDuracion, sFIniV, sFfinV, fPresup,
                                               iEstado, bFacturable, bAvanceAutomatico, sObs);
                    //Asocio los recursos del Proy. Economico a la tarea
                    //el 04/12/2006 comenta Andoni que la herencia se realizará por trigger por lo que quitamos el código
                    //if (nResul > 0)
                    //{
                    //    TAREAPSP.HeredarRecursos(tr, (short)iCodUne, iNumProy, nResul, sFfinPl);
                    //}
                    break;
                case "HM":
                case "HT":
                    nResul = InsertarHito(tr, sDesc, iMargen, iOrden, it305_IdProy);
                    break;
                case "HF":
                    //nResul = InsertarHitoPE(tr, iCodUne, iNumProy, sDesc, sFIniPL, iOrden);
                    nResul = InsertarHitoPE(tr, it305_IdProy, sDesc, sFIniPL, iOrden, sObs);
                    break;
            }
            return nResul.ToString()+"##"+sAviso;
        }
        public static int InsertarFase(SqlTransaction tr, string sDesc, int iOrden, string sObs, decimal fPresupuesto, decimal fAvance, bool bAvanceAuto)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@sDesc", SqlDbType.VarChar, 100);
            aParam[1] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@t334_desfaselong", SqlDbType.Text, 2147483647);
            aParam[3] = new SqlParameter("@t334_presupuesto", SqlDbType.Money, 8);
            aParam[4] = new SqlParameter("@t334_avance", SqlDbType.Float, 8);
            aParam[5] = new SqlParameter("@t334_avanceauto", SqlDbType.Bit, 1);
            aParam[0].Value = sDesc;
            aParam[1].Value = iOrden;
            aParam[2].Value = sObs;
            aParam[3].Value = fPresupuesto;
            aParam[4].Value = fAvance;
            aParam[5].Value = bAvanceAuto;
            object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FASEPSPI", aParam);
            return int.Parse(nResul.ToString());
        }
        public static int InsertarActividad(SqlTransaction tr, int iFase, string sDesc, int iOrden, string sObs, decimal fPresupuesto, decimal fAvance, bool bAvanceAuto)
        {//Si iFase = -1 es una actividad sin fase (se controla en el procedimiento almacenado)
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@nIdFase", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesc", SqlDbType.VarChar, 100);
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@t335_desactividadlong", SqlDbType.Text, 2147483647);
            aParam[4] = new SqlParameter("@t335_presupuesto", SqlDbType.Money, 8);
            aParam[5] = new SqlParameter("@t335_avance", SqlDbType.Float, 8);
            aParam[6] = new SqlParameter("@t335_avanceauto", SqlDbType.Bit, 1);
            aParam[0].Value = iFase;
            aParam[1].Value = sDesc;
            aParam[2].Value = iOrden;
            aParam[3].Value = sObs;
            aParam[4].Value = fPresupuesto;
            aParam[5].Value = fAvance;
            aParam[6].Value = bAvanceAuto;
            object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ACTIVIDADPSPI", aParam);
            return int.Parse(nResul.ToString());
        }
        public static int InsertarHito(SqlTransaction tr, string sDesc, int iMargen, int iOrden, int it305_IdProy)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@sDesHito", SqlDbType.VarChar, 50);
            aParam[1] = new SqlParameter("@nMargen", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.Int, 4);

            aParam[0].Value = sDesc;
            aParam[1].Value = iMargen;
            aParam[2].Value = iOrden;

            object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITOPSPI", aParam);
            return int.Parse(nResul.ToString());
        }
        public static int InsertarHitoPE(SqlTransaction tr, int iT305IdProy, string sDesc, string sFecha, int iOrden, string sObs)
        {//Si iFase = -1 es una tarea sin actividad (se controla en el procedimiento almacenado) 
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nNumProyecto", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesHito", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@dFecha", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@t352_deshitolong", SqlDbType.Text, 2147483647);

            aParam[0].Value = iT305IdProy;
            aParam[1].Value = sDesc;
            //DateTime dFecha= DateTime.Parse("01/01/1900");
            //if (sFecha != "") dFecha = DateTime.Parse(sFecha);
            aParam[2].Value = DateTime.Parse(sFecha).ToShortDateString();
            aParam[3].Value = iOrden;
            aParam[4].Value = sObs;

            object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITOPEI", aParam);
            return int.Parse(nResul.ToString());
        }
        public static void InsertarTareaHito(SqlTransaction tr, int iHito, int iTarea)
        {//
            if (!bExisteTareaHito(tr, iHito, iTarea))
            {
                SqlParameter[] aParam = new SqlParameter[2];
                aParam[0] = new SqlParameter("@nCodHito", SqlDbType.Int, 4);
                aParam[1] = new SqlParameter("@nCodTarea", SqlDbType.Int, 4);

                aParam[0].Value = iHito;
                aParam[1].Value = iTarea;

                //object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "PSP_HITOPSPI", aParam);
                object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITO_TAREAI", aParam);
                //return int.Parse(nResul.ToString());
            }
        }
        private static bool bExisteTareaHito(SqlTransaction tr, int iHito, int iTarea)
        {
            bool bExiste = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nCodHito", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nCodTarea", SqlDbType.Int, 4);
            aParam[0].Value = iHito;
            aParam[1].Value = iTarea;

            int returnValue;
            if (tr == null)
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_HITO_TAREAS", aParam));
            else
                returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITO_TAREAS", aParam));

            if (returnValue > 0)
                bExiste = true;
            return bExiste;
        }

        public static void AsociarTareasHito(SqlTransaction tr, string sTipo, int nHito, int nCodigo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@sTipo", SqlDbType.VarChar, 2);
            aParam[1] = new SqlParameter("@nCodigo", SqlDbType.Int, 4);
            //aParam[2] = new SqlParameter("@nCR", SqlDbType.SmallInt, 2);
            aParam[2] = new SqlParameter("@nIDHito", SqlDbType.Int, 4);

            aParam[0].Value = sTipo;
            aParam[1].Value = nCodigo;
            //aParam[2].Value = nCR;
            aParam[2].Value = nHito;

            int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITO_ASOCTAREA", aParam);
        }

        #endregion

        #region updatear
        public static void Modificar(SqlTransaction tr, int iCodUne, int iNumProy, string sTipo, string sDesc,
                                    int iPT, int iFase, int iActiv, int iTarea, int iHito, int iMargen, int iOrden,
                                    string sFIniPL, string sFfinPl, double fDuracion, string sFIniV, string sFfinV,
                                    decimal fPresup, bool bFacturable, string sSituacion, decimal fAvance, Nullable<byte> iAvanceAuto)
        {
            //bool bEstadoTarea = false;
            byte iEstado;
            sDesc = Utilidades.unescape(sDesc);
            switch (sTipo)
            {
                case "P":
                    if (sSituacion == "") iEstado = 1;
                    else iEstado = byte.Parse(sSituacion);
                    //por peticion de Victor el control de AE obligatorios solo se realizará en el caso de inserción
                    //Compruebo si el CR tiene atributos estadisticos obligatorios
                    //bEstadoTarea = ProyTec.bFaltanValoresAE(tr, (short)iCodUne, iPT);
                    //if (bEstadoTarea)
                    //{
                    //    iEstado = 2;
                    //    ProyTec.Modificar(tr, iPT, sDesc, iOrden,iEstado); 
                    //}
                    //else//el estado lo dejo como estuviera
                    ProyTec.Modificar(tr, iPT, sDesc, iOrden, iEstado, fPresup, fAvance, iAvanceAuto); 
                    break;
                case "F": ModificarFase(tr, iFase, sDesc, iOrden, fPresup, fAvance, iAvanceAuto); break;
                case "A": ModificarActividad(tr, iActiv, iFase, sDesc, iOrden, fPresup, fAvance, iAvanceAuto); break;
                case "T":
                    int iUsuario = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                    if (sSituacion == "") iEstado = 1;
                    else iEstado = byte.Parse(sSituacion);
                    //por peticion de Victor el control de AE obligatorios solo se realizará en el caso de inserción    
                    //Compruebo si el CR tiene atributos estadisticos obligatorios
                    //bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, (short)iCodUne, iTarea);
                    //if (bEstadoTarea)
                    //{
                    //    iEstado = 2;
                    //    TAREAPSP.Modificar(tr, iTarea, sDesc, iPT, iActiv, iOrden, sFIniPL, sFfinPl, fDuracion, sFIniV, sFfinV,
                    //                        iUsuario, fPresup, iEstado);
                    //}
                    //else//el estado lo dejo como estuviera
                        TAREAPSP.Modificar(tr, iTarea, sDesc, iPT, iActiv, iOrden, sFIniPL, sFfinPl, fDuracion, sFIniV, sFfinV,
                                            iUsuario, fPresup, iEstado, bFacturable);
                    break;
                case "HT":
                    ModificarHito(tr, iHito, sDesc, iOrden, iMargen);
                    break;
                case "HF":
                    ModificarHitoPE(tr, iHito, sDesc, iOrden, sFIniPL);
                    break;
                case "HM":
                    ModificarHito(tr, iHito, sDesc, iOrden, iMargen);
                    break;
            }
        }
        private static void ModificarFase(SqlTransaction tr, int iFase, string sDesc, int iOrden, decimal fPresupuesto, decimal fAvance, Nullable<byte> iAvanceAuto)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@nIdFase", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesc", SqlDbType.VarChar, 100);
            aParam[2] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nPresupuesto", SqlDbType.Money, 8);
            aParam[4] = new SqlParameter("@t334_avance", SqlDbType.Float, 8);
            aParam[5] = new SqlParameter("@t334_avanceauto", SqlDbType.Bit, 1);
            aParam[0].Value = iFase;
            aParam[1].Value = sDesc;
            aParam[2].Value = iOrden;
            aParam[3].Value = fPresupuesto;
            aParam[4].Value = fAvance;
            aParam[5].Value = iAvanceAuto;
            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FASEPSPU", aParam);
        }
        private static void ModificarActividad(SqlTransaction tr, int iActiv, int iFase, string sDesc, int iOrden, decimal fPresupuesto, decimal fAvance, Nullable<byte> iAvanceAuto)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@nIdActiv", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdFase", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@sDesc", SqlDbType.VarChar, 100);
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@nPresupuesto", SqlDbType.Money, 8);
            aParam[5] = new SqlParameter("@t335_avance", SqlDbType.Float, 8);
            aParam[6] = new SqlParameter("@t335_avanceauto", SqlDbType.Bit, 1);
            aParam[0].Value = iActiv;
            aParam[1].Value = iFase;
            aParam[2].Value = sDesc;
            aParam[3].Value = iOrden;
            aParam[4].Value = fPresupuesto;
            aParam[5].Value = fAvance;
            aParam[6].Value = iAvanceAuto;
            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACTIVIDADPSPU", aParam);
        }
        private static void ModificarHito(SqlTransaction tr, int iHito, string sDesc, int iOrden, int iMargen)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nIdHito", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesHito", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@nMargen", SqlDbType.TinyInt, 1);
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[0].Value = iHito;
            aParam[1].Value = sDesc;
            aParam[2].Value = iMargen;
            aParam[3].Value = iOrden;
            SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITOPSPU", aParam);
        }
        
        //11/10/2007 pasado método a public para poder utilizarlo desde la pantalla de gantt del proyecto
        public static void ModificarHitoPE(SqlTransaction tr, int iHito, string sDesc, int iOrden, string sFecha)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nIdHito", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesHito", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@dFecha", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.Int, 4);
            aParam[0].Value = iHito;
            aParam[1].Value = sDesc;
            //DateTime dFecha= DateTime.Parse("01/01/1900");
            //if (sFecha != "") dFecha = DateTime.Parse(sFecha);
            aParam[2].Value = DateTime.Parse(sFecha).ToShortDateString();
            aParam[3].Value = iOrden;
            SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HITOPEU", aParam);
        }
        #endregion

        #region Borrar
        /// <summary>
        /// Borra todos los proyetos técnicos de un proyectosubnodo (por delete cascada se borran las tareas)
        /// Además borra los hitos de fecha
        /// El resto de items quedarán huérfanos y se borrarán en el proceso nocturno
        /// </summary>
        public static void BorrarEstructura(SqlTransaction tr, int idPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = idPSN;

            int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTOSUBNODO_BORRAR_ESTRUCTURA", aParam);
            return;
        }
        public static void Borrar(SqlTransaction tr, string sTipo, int iCodigo)
        {
            switch (sTipo)
            {
                case "P": ProyTec.Eliminar(tr,iCodigo); break;
                case "F": BorrarFase(tr, iCodigo); break;
                case "A": BorrarActividad(tr, iCodigo); break;
                case "T": TAREAPSP.Delete(tr,iCodigo); break;
                case "HT": BorrarHito(tr, iCodigo); break;
                case "HM": BorrarHito(tr, iCodigo); break;
                case "HF": BorrarHitoPE(tr, iCodigo); break;
            }
            return;
        }
        private static void BorrarFase(SqlTransaction tr, int iCodigo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t334_idfase", SqlDbType.Int, 4);
            aParam[0].Value = iCodigo;

            int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FASESUP_D", aParam);
            return;
        }
        private static void BorrarActividad(SqlTransaction tr, int iCodigo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            aParam[0].Value = iCodigo;
            //Ademas de borrar la actividad actualizamos a NULL el campo t335_idactividad de las tareas dependientes
            int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACTIVIDADPSP_D", aParam);
            return;
        }
        private static void BorrarHito(SqlTransaction tr, int iCodigo)
        {
            //1º borro sus hijos
            //BorrarTareasHito(tr, iCodigo);
            //Borro el hito
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdHito", SqlDbType.Int, 4);
            aParam[0].Value = iCodigo;

            int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOPSPD", aParam);
            return;
        }
        private static void BorrarHitoPE(SqlTransaction tr, int iCodigo)
        {
            //1º borro sus hijos
            //BorrarTareasHito(tr, iCodigo);
            //Borro el hito
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdHito", SqlDbType.Int, 4);
            aParam[0].Value = iCodigo;

            int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITOPED", aParam);
            return;
        }

        public static void BorrarTareasHito(SqlTransaction tr, int iCodigo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nCodHito", SqlDbType.Int, 4);
            aParam[0].Value = iCodigo;

            int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_HITO_TAREAD", aParam);
            return;
        }
        #endregion

        #region Métodos para la obtención de la estructura bajo demanda.
        //public static DataSet EstructuraPE(int iCodUne, int iNumProy, int idRecurso, string sPerfil)
        public static DataSet EstructuraPE(int iNumProy, int idRecurso, bool bRTPT, bool bMostrarCerradas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nProy", SqlDbType.Int, 4, iNumProy);
            aParam[i++] = ParametroSql.add("@nRecurso", SqlDbType.Int, 4, idRecurso);
            aParam[i++] = ParametroSql.add("@bRTPT", SqlDbType.Bit, 1, bRTPT);
            aParam[i++] = ParametroSql.add("@bCerradas", SqlDbType.Bit, 1, bMostrarCerradas);


            return SqlHelper.ExecuteDataset("SUP_ESTRUCTURA_PE", aParam);
        }
        /// <summary>
        /// Petición de los datos de un Proyecto Técnico
        /// Una vez obtenido el DataSet, hay que tratar los DataTables para colocar
        /// cada hito (Tables[1]) después de la tarea (Tables[0]) a la que pertenece
        /// </summary>
        //public static DataSet EstructuraPT(int iCodUne, int iNumPE, int iNumPT, int idRecurso, string sPerfil, bool bMostrarCerradas)
        public static DataSet EstructuraPT(int iNumPE, int iNumPT, int idRecurso, bool bRTPT, bool bMostrarCerradas)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            //aParam[0] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[0] = new SqlParameter("@nProyE", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nProyT", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[4] = new SqlParameter("@bCerradas", SqlDbType.Bit, 1);
            //aParam[0].Value = iCodUne;
            aParam[0].Value = iNumPE;
            aParam[1].Value = iNumPT;
            aParam[2].Value = idRecurso;
            aParam[3].Value = bRTPT;
            aParam[4].Value = bMostrarCerradas;

            return SqlHelper.ExecuteDataset("SUP_ESTRUCTURA_PT", aParam);
        }
        //public static DataSet EstructuraF(int iCodUne, int iNumPE, int iNumPT, int iNumFase, int idRecurso, string sPerfil)
        public static DataSet EstructuraF(int iNumPE, int iNumPT, int iNumFase, int idRecurso, bool bRTPT, bool bMostrarCerradas)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            //aParam[0] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[0] = new SqlParameter("@nProyE", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nProyT", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nFase", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@bCerradas", SqlDbType.Bit, 1);
            //aParam[0].Value = iCodUne;
            aParam[0].Value = iNumPE;
            aParam[1].Value = iNumPT;
            aParam[2].Value = iNumFase;
            aParam[3].Value = idRecurso;
            aParam[4].Value = bRTPT;
            aParam[5].Value = bMostrarCerradas;

            return SqlHelper.ExecuteDataset("SUP_ESTRUCTURA_F", aParam);
        }
        /// <summary>
        /// Petición de los datos de una actividad
        /// Una vez obtenido el DataSet, hay que tratar los DataTables para colocar
        /// cada hito (Tables[1]) después de la tarea (Tables[0]) a la que pertenece
        /// </summary>
        //public static DataSet EstructuraA(int iCodUne, int iNumPE, int iNumPT, int iNumActiv, int idRecurso, string sPerfil, bool bMostrarCerradas)
        public static DataSet EstructuraA(int iNumPE, int iNumPT, int iNumActiv, int idRecurso, bool bRTPT, bool bMostrarCerradas)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            //aParam[0] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[0] = new SqlParameter("@nProyE", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nProyT", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nActiv", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@bCerradas", SqlDbType.Bit, 1);
            //aParam[0].Value = iCodUne;
            aParam[0].Value = iNumPE;
            aParam[1].Value = iNumPT;
            aParam[2].Value = iNumActiv;
            aParam[3].Value = idRecurso;
            aParam[4].Value = bRTPT;
            aParam[5].Value = bMostrarCerradas;

            return SqlHelper.ExecuteDataset("SUP_ESTRUCTURA_A", aParam);
        }

        public static DataSet EstructuraCompleta(int t305_idproyectosubnodo, int t314_idusuario, bool bRTPT, bool bMostrarCerradas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bRTPT", SqlDbType.Bit, 1, bRTPT);
            aParam[i++] = ParametroSql.add("@bCerradas", SqlDbType.Bit, 1, bMostrarCerradas);

            return SqlHelper.ExecuteDataset("SUP_ESTRUCTURA_COMPLETA", aParam);
        }
        /// <summary>
        /// Devuelve la estructura de un proyecto subnodo correspondientes a las tareas asignadas a un usuario 
        /// que tengan los estados que se pasan como parámetro
        /// </summary>
        /// <param name="t305_idproyectosubnodo"></param>
        /// <param name="sEstados">codigos de estado separados por coma</param>
        /// <returns></returns>
        public static SqlDataReader EstructuraCompleta(int t305_idproyectosubnodo,int t314_idusuario, string sEstados)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@sEstados", SqlDbType.VarChar, 15, sEstados);

            return SqlHelper.ExecuteSqlDataReader("SUP_ESTRUCTURA_COMPLETA2", aParam);
        }

        #endregion

        /// <summary>
        /// Obtiene la estructura de un proyecto económico para crear una
        /// plantilla de dicho nivel.
        /// </summary>
        //public static DataSet EstructuraPlantilla(int iCodUne, int iNumProy, Nullable<int> iNumPT, int idRecurso)
        public static DataSet EstructuraPlantilla(int iNumProy, Nullable<int> iNumPT, int idRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            //aParam[0] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nProyT", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            //aParam[0].Value = iCodUne;
            aParam[0].Value = iNumProy;
            aParam[1].Value = iNumPT;
            aParam[2].Value = idRecurso;

            return SqlHelper.ExecuteDataset("SUP_ESTRUCTURA_PLANT", aParam);
        }

        #region Métodos para la obtención del diagrama de GANTT.
        public static DataSet EstructuraGanttPE(int iT305IdProy, int idRecurso, bool bRTPT)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int, 4);
            aParam[0].Value = iT305IdProy;
            aParam[1] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;
            aParam[2] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[2].Value = bRTPT;

            return SqlHelper.ExecuteDataset("SUP_GANTT_PE", aParam);
        }
        /// <summary>
        /// Petición de los datos de un Proyecto Técnico
        /// Una vez obtenido el DataSet, hay que tratar los DataTables para colocar
        /// cada hito (Tables[1]) después de la tarea (Tables[0]) a la que pertenece
        /// </summary>
        //public static DataSet EstructuraGanttPT(int iCodUne, int iNumPE, int iNumPT, int idRecurso, string sPerfil, bool bMostrarCerradas)
        public static DataSet EstructuraGanttPT(int iNumPT, int idRecurso, bool bMostrarCerradas, bool bRTPT)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nProyT", SqlDbType.Int, 4);
            aParam[0].Value = iNumPT;
            aParam[1] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[1].Value = idRecurso;
            aParam[2] = new SqlParameter("@bCerradas", SqlDbType.Bit, 1);
            aParam[2].Value = bMostrarCerradas;
            aParam[3] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[3].Value = bRTPT;

            return SqlHelper.ExecuteDataset("SUP_GANTT_PT", aParam);
        }
        public static DataSet EstructuraGanttF(int iT305IdProy, int iNumPT, int iNumFase, int idRecurso, bool bRTPT)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nProyE", SqlDbType.Int, 4);
            aParam[0].Value = iT305IdProy;
            aParam[1] = new SqlParameter("@nProyT", SqlDbType.Int, 4);
            aParam[1].Value = iNumPT;
            aParam[2] = new SqlParameter("@nFase", SqlDbType.Int, 4);
            aParam[2].Value = iNumFase;
            aParam[3] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[3].Value = idRecurso;
            aParam[4] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[4].Value = bRTPT;

            return SqlHelper.ExecuteDataset("SUP_GANTT_F", aParam);
        }
        /// <summary>
        /// Petición de los datos de una actividad
        /// Una vez obtenido el DataSet, hay que tratar los DataTables para colocar
        /// cada hito (Tables[1]) después de la tarea (Tables[0]) a la que pertenece
        /// </summary>
        //public static DataSet EstructuraGanttA(int iCodUne, int iNumPE, int iNumPT, int iNumActiv, int idRecurso, string sPerfil, bool bMostrarCerradas)
        public static DataSet EstructuraGanttA(int iT305IdProy, int iNumPT, int iNumActiv, int idRecurso, bool bMostrarCerradas, bool bRTPT)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@nProyE", SqlDbType.Int, 4);
            aParam[0].Value = iT305IdProy;
            aParam[1] = new SqlParameter("@nProyT", SqlDbType.Int, 4);
            aParam[1].Value = iNumPT;
            aParam[2] = new SqlParameter("@nActiv", SqlDbType.Int, 4);
            aParam[2].Value = iNumActiv;
            aParam[3] = new SqlParameter("@nRecurso", SqlDbType.Int, 4);
            aParam[3].Value = idRecurso;
            aParam[4] = new SqlParameter("@bCerradas", SqlDbType.Bit, 1);
            aParam[4].Value = bMostrarCerradas;
            aParam[5] = new SqlParameter("@bRTPT", SqlDbType.Bit, 1);
            aParam[5].Value = bRTPT;

            return SqlHelper.ExecuteDataset("SUP_GANTT_A", aParam);
        }
        #endregion
    }
}