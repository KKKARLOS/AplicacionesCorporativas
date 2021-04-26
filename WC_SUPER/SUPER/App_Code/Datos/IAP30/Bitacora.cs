using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.SUPER.IAP30.Models;

namespace IB.SUPER.IAP30.DAL
{
    /// <summary>
    /// Descripción breve de Bitacora
    /// </summary>
    /// 
    internal class Bitacora
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            cod_psn = 1,
            des_asunto = 2,
            estado = 3,
            ffinD = 4,
            flimiteD = 5,
            fnotificacionD = 6,
            ffinH = 7,
            flimiteH = 8,
            fnotificacionH = 9,
            prioridad = 10,
            severidad = 11,
            tipo=12,
            orden=13,
            ascdesc=14,
            t314_idusuario=15,
            cod_pt = 16,
            des_asuntoPT = 17,
            estadoPT = 18,
            prioridadPT = 19,
            severidadPT = 20,
            cod_tarea=21,
            des_asuntoT = 22,
            estadoT = 23,
            prioridadT = 24,
            severidadT = 25

        }

        internal Bitacora(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas

        ///// <summary>
        ///// Actualiza un TareaBitacora a partir del id
        ///// </summary>
        //internal int Update(Models.TareaBitacora oTareaBitacora)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.cod_tarea, oTareaBitacora.cod_tarea),
        //            Param(enumDBFields.nom_tarea, oTareaBitacora.nom_tarea),
        //            Param(enumDBFields.cod_pt, oTareaBitacora.cod_pt),
        //            Param(enumDBFields.nom_pt, oTareaBitacora.nom_pt),
        //            Param(enumDBFields.cod_pe, oTareaBitacora.cod_pe),
        //            Param(enumDBFields.nom_pe, oTareaBitacora.nom_pe),
        //            Param(enumDBFields.t301_estado, oTareaBitacora.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTareaBitacora.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oTareaBitacora.cod_une),
        //            Param(enumDBFields.t332_orden, oTareaBitacora.t332_orden),
        //            Param(enumDBFields.t332_acceso_iap, oTareaBitacora.t332_acceso_iap)
        //        };

        //        return (int)cDblib.Execute("_TareaBitacora_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un TareaBitacora a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_TareaBitacora_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TareaBitacora
        ///// </summary>
        //internal List<Models.TareaBitacora> Catalogo(Models.TareaBitacora oTareaBitacoraFilter)
        //{
        //    Models.TareaBitacora oTareaBitacora = null;
        //    List<Models.TareaBitacora> lst = new List<Models.TareaBitacora>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[11] {
        //            Param(enumDBFields.cod_tarea, oTEMP_TareaBitacoraFilter.cod_tarea),
        //            Param(enumDBFields.nom_tarea, oTEMP_TareaBitacoraFilter.nom_tarea),
        //            Param(enumDBFields.cod_pt, oTEMP_TareaBitacoraFilter.cod_pt),
        //            Param(enumDBFields.nom_pt, oTEMP_TareaBitacoraFilter.nom_pt),
        //            Param(enumDBFields.cod_pe, oTEMP_TareaBitacoraFilter.cod_pe),
        //            Param(enumDBFields.nom_pe, oTEMP_TareaBitacoraFilter.nom_pe),
        //            Param(enumDBFields.t301_estado, oTEMP_TareaBitacoraFilter.t301_estado),
        //            Param(enumDBFields.t305_idproyectosubnodo, oTEMP_TareaBitacoraFilter.t305_idproyectosubnodo),
        //            Param(enumDBFields.cod_une, oTEMP_TareaBitacoraFilter.cod_une),
        //            Param(enumDBFields.t332_orden, oTEMP_TareaBitacoraFilter.t332_orden),
        //            Param(enumDBFields.t332_acceso_iap, oTEMP_TareaBitacoraFilter.t332_acceso_iap)
        //        };

        //        dr = cDblib.DataReader("_TareaBitacora_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oTareaBitacora = new Models.TareaBitacora();
        //            oTareaBitacora.cod_tarea=Convert.ToInt32(dr["cod_tarea"]);
        //            oTareaBitacora.nom_tarea=Convert.ToString(dr["nom_tarea"]);
        //            oTareaBitacora.cod_pt=Convert.ToInt32(dr["cod_pt"]);
        //            oTareaBitacora.nom_pt=Convert.ToString(dr["nom_pt"]);
        //            oTareaBitacora.cod_pe=Convert.ToInt32(dr["cod_pe"]);
        //            oTareaBitacora.nom_pe=Convert.ToString(dr["nom_pe"]);
        //            oTareaBitacora.t301_estado=Convert.ToString(dr["t301_estado"]);
        //            oTareaBitacora.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
        //            oTareaBitacora.cod_une=Convert.ToInt32(dr["cod_une"]);
        //            oTareaBitacora.t332_orden=Convert.ToInt32(dr["t332_orden"]);
        //            oTareaBitacora.t332_acceso_iap=Convert.ToString(dr["t332_acceso_iap"]);

        //            lst.Add(oTareaBitacora);

        //        }
        //        return lst;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}
        /// <summary>
        /// Obtiene los elementos de la bitácora
        /// </summary>
        internal List<Models.Bitacora> Catalogo(int idPSN, bool acciones, string Denominacion,
                                            Nullable<int> TipoAsunto, Nullable<int> Estado, Nullable<int> Severidad, Nullable<int> Prioridad,
                                            Nullable<DateTime> dNotif, Nullable<DateTime> hNotif, Nullable<DateTime> dLimite, Nullable<DateTime> hLimite, Nullable<DateTime> dFin, Nullable<DateTime> hFin)
        {
            Models.Bitacora oElem = null;
            List<Models.Bitacora> lst = new List<Models.Bitacora>();
            IDataReader dr = null;
            string sIdAsuntoAnt="", sIdAsuntoAct="";
            string sProcAlm = "SUP_BIT_AS";
            string t382_ffinD = "01/01/1950", t382_ffinH = "31/12/2050", 
                t382_flimiteD = "01/01/1950", t382_flimiteH = "31/12/2050", 
                t382_fnotificacionD = "01/01/1950", t382_fnotificacionH = "31/12/2050";

            try
            {
                if (acciones) sProcAlm = "SUP_BIT_ASyAC";
                if (dFin.HasValue) t382_ffinD = dFin.ToString();
                if (dLimite.HasValue) t382_flimiteD = dLimite.ToString();
                if (dNotif.HasValue) t382_fnotificacionD = dNotif.ToString();
                if (hFin.HasValue) t382_ffinH = hFin.ToString();
                if (hLimite.HasValue) t382_flimiteH = hLimite.ToString();
                if (hNotif.HasValue) t382_fnotificacionH = hNotif.ToString();


                SqlParameter[] dbparams = new SqlParameter[14] {
                    Param(enumDBFields.cod_psn, idPSN),
                    Param(enumDBFields.des_asunto, Denominacion),
                    Param(enumDBFields.estado, Estado),
                    Param(enumDBFields.ffinD, t382_ffinD),
                    Param(enumDBFields.flimiteD, t382_flimiteD),
                    Param(enumDBFields.fnotificacionD, t382_fnotificacionD),
                    Param(enumDBFields.ffinH, t382_ffinH),
                    Param(enumDBFields.flimiteH, t382_flimiteH),
                    Param(enumDBFields.fnotificacionH, t382_fnotificacionH),
                    Param(enumDBFields.prioridad, Prioridad),
                    Param(enumDBFields.severidad, Severidad),
                    Param(enumDBFields.tipo, TipoAsunto),
                    Param(enumDBFields.orden, 6),
                    Param(enumDBFields.ascdesc, 0),
                };
                sIdAsuntoAnt = "-1";
                dr = cDblib.DataReader(sProcAlm, dbparams);
                while (dr.Read())
                {
                    oElem = new Models.Bitacora();
                    oElem.idPSN = idPSN;
                    if (!acciones)
                    {
                        oElem = ponerAsunto(dr);
                        lst.Add(oElem);
                    }
                    else
                    {
                        sIdAsuntoAct = dr["t382_idasunto"].ToString();
                        if (sIdAsuntoAnt != sIdAsuntoAct)
                        {
                            oElem = ponerAsunto(dr);
                            lst.Add(oElem);
                            if (dr["t383_idaccion"].ToString() != "")
                            {
                                oElem = new Models.Bitacora();
                                oElem = ponerAccion(dr);
                                lst.Add(oElem);
                            }
                        }
                        else
                        {
                            if (dr["t383_idaccion"].ToString() != "")
                            {
                                oElem = ponerAccion(dr);
                                lst.Add(oElem);
                            }
                        }
                    }
                    sIdAsuntoAnt = sIdAsuntoAct;                   
                }
                return lst;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        internal List<Models.Bitacora> CatalogoPT(int idPT, bool acciones, string Denominacion,
                                            Nullable<int> TipoAsunto, Nullable<int> Estado, Nullable<int> Severidad, Nullable<int> Prioridad,
                                            Nullable<DateTime> dNotif, Nullable<DateTime> hNotif, Nullable<DateTime> dLimite, Nullable<DateTime> hLimite, Nullable<DateTime> dFin, Nullable<DateTime> hFin)
        {
            Models.Bitacora oElem = null;
            List<Models.Bitacora> lst = new List<Models.Bitacora>();
            IDataReader dr = null;
            string sIdAsuntoAnt = "", sIdAsuntoAct = "";
            string sProcAlm = "SUP_BIT_AS_PT";
            string ffinD = "01/01/1950", ffinH = "31/12/2050",
                flimiteD = "01/01/1950", flimiteH = "31/12/2050",
                fnotificacionD = "01/01/1950", fnotificacionH = "31/12/2050";

            try
            {
                if (acciones) sProcAlm = "SUP_BIT_AS_PTyAC";
                if (dFin.HasValue) ffinD = dFin.ToString();
                if (dLimite.HasValue) flimiteD = dLimite.ToString();
                if (dNotif.HasValue) fnotificacionD = dNotif.ToString();
                if (hFin.HasValue) ffinH = hFin.ToString();
                if (hLimite.HasValue) flimiteH = hLimite.ToString();
                if (hNotif.HasValue) fnotificacionH = hNotif.ToString();


                SqlParameter[] dbparams = new SqlParameter[14] {
                    Param(enumDBFields.cod_pt, idPT),
                    Param(enumDBFields.des_asuntoPT, Denominacion),
                    Param(enumDBFields.estadoPT, Estado),
                    Param(enumDBFields.ffinD, ffinD),
                    Param(enumDBFields.flimiteD, flimiteD),
                    Param(enumDBFields.fnotificacionD, fnotificacionD),
                    Param(enumDBFields.ffinH, ffinH),
                    Param(enumDBFields.flimiteH, flimiteH),
                    Param(enumDBFields.fnotificacionH, fnotificacionH),
                    Param(enumDBFields.prioridadPT, Prioridad),
                    Param(enumDBFields.severidadPT, Severidad),
                    Param(enumDBFields.tipo, TipoAsunto),
                    Param(enumDBFields.orden, 6),
                    Param(enumDBFields.ascdesc, 0),
                };
                sIdAsuntoAnt = "-1";
                dr = cDblib.DataReader(sProcAlm, dbparams);
                while (dr.Read())
                {
                    oElem = new Models.Bitacora();
                    oElem.idPT = idPT;
                    if (!acciones)
                    {
                        oElem = ponerAsuntoPT(dr);
                        lst.Add(oElem);
                    }
                    else
                    {
                        sIdAsuntoAct = dr["T409_idasunto"].ToString();
                        if (sIdAsuntoAnt != sIdAsuntoAct)
                        {
                            oElem = ponerAsuntoPT(dr);
                            lst.Add(oElem);
                            if (dr["T410_idaccion"].ToString() != "")
                            {
                                oElem = new Models.Bitacora();
                                oElem = ponerAccionPT(dr);
                                lst.Add(oElem);
                            }
                        }
                        else
                        {
                            if (dr["T410_idaccion"].ToString() != "")
                            {
                                oElem = ponerAccionPT(dr);
                                lst.Add(oElem);
                            }
                        }
                    }
                    sIdAsuntoAnt = sIdAsuntoAct;
                }
                return lst;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        internal List<Models.Bitacora> CatalogoTareas(int idTarea, bool acciones, string Denominacion,
                                            Nullable<int> TipoAsunto, Nullable<int> Estado, Nullable<int> Severidad, Nullable<int> Prioridad,
                                            Nullable<DateTime> dNotif, Nullable<DateTime> hNotif, Nullable<DateTime> dLimite, Nullable<DateTime> hLimite, Nullable<DateTime> dFin, Nullable<DateTime> hFin)
        {
            Models.Bitacora oElem = null;
            List<Models.Bitacora> lst = new List<Models.Bitacora>();
            IDataReader dr = null;
            string sIdAsuntoAnt = "", sIdAsuntoAct = "";
            string sProcAlm = "SUP_BIT_AS_T";
            string ffinD = "01/01/1950", ffinH = "31/12/2050",
                flimiteD = "01/01/1950", flimiteH = "31/12/2050",
                fnotificacionD = "01/01/1950", fnotificacionH = "31/12/2050";

            try
            {
                if (acciones) sProcAlm = "SUP_BIT_AS_TyAC";
                if (dFin.HasValue) ffinD = dFin.ToString();
                if (dLimite.HasValue) flimiteD = dLimite.ToString();
                if (dNotif.HasValue) fnotificacionD = dNotif.ToString();
                if (hFin.HasValue) ffinH = hFin.ToString();
                if (hLimite.HasValue) flimiteH = hLimite.ToString();
                if (hNotif.HasValue) fnotificacionH = hNotif.ToString();


                SqlParameter[] dbparams = new SqlParameter[14] {
                    Param(enumDBFields.cod_tarea, idTarea),
                    Param(enumDBFields.des_asuntoT, Denominacion),
                    Param(enumDBFields.estadoT, Estado),
                    Param(enumDBFields.ffinD, ffinD),
                    Param(enumDBFields.flimiteD, flimiteD),
                    Param(enumDBFields.fnotificacionD, fnotificacionD),
                    Param(enumDBFields.ffinH, ffinH),
                    Param(enumDBFields.flimiteH, flimiteH),
                    Param(enumDBFields.fnotificacionH, fnotificacionH),
                    Param(enumDBFields.prioridadT, Prioridad),
                    Param(enumDBFields.severidadT, Severidad),
                    Param(enumDBFields.tipo, TipoAsunto),
                    Param(enumDBFields.orden, 6),
                    Param(enumDBFields.ascdesc, 0),
                };
                sIdAsuntoAnt = "-1";
                dr = cDblib.DataReader(sProcAlm, dbparams);
                while (dr.Read())
                {
                    oElem = new Models.Bitacora();
                    oElem.idTarea = idTarea;
                    if (!acciones)
                    {
                        oElem = ponerAsuntoT(dr);
                        lst.Add(oElem);
                    }
                    else
                    {
                        sIdAsuntoAct = dr["T600_idasunto"].ToString();
                        if (sIdAsuntoAnt != sIdAsuntoAct)
                        {
                            oElem = ponerAsuntoT(dr);
                            lst.Add(oElem);
                            if (dr["T601_idaccion"].ToString() != "")
                            {
                                oElem = new Models.Bitacora();
                                oElem = ponerAccionT(dr);
                                lst.Add(oElem);
                            }
                        }
                        else
                        {
                            if (dr["T601_idaccion"].ToString() != "")
                            {
                                oElem = ponerAccionT(dr);
                                lst.Add(oElem);
                            }
                        }
                    }
                    sIdAsuntoAnt = sIdAsuntoAct;
                }
                return lst;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de proyectos en los que un usuario puede consultar su bitácora (incluyendo proyectos cerrados)
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        internal List<Models.ProyectoNota> Proyectos(int t314_idusuario)
        {
            Models.ProyectoNota oProyectoNota = null;
            List<Models.ProyectoNota> lst = new List<Models.ProyectoNota>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario)
                };

                dr = cDblib.DataReader("SUP_GETPROYECTOS_BITACORA_IAP", dbparams);
                while (dr.Read())
                {
                    oProyectoNota = new Models.ProyectoNota();
                    oProyectoNota.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oProyectoNota.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oProyectoNota.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oProyectoNota.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oProyectoNota.t305_cualidad = Convert.ToString(dr["t305_cualidad"]);
                    oProyectoNota.t301_estado = Convert.ToString(dr["t301_estado"]);
                    if (!Convert.IsDBNull(dr["Responsable_Proyecto"]))
                        oProyectoNota.Responsable_Proyecto = Convert.ToString(dr["Responsable_Proyecto"]);
                    oProyectoNota.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oProyectoNota.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);

                    lst.Add(oProyectoNota);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        internal List<Models.ProyectoNota> ProyectosPT(int t314_idusuario)
        {
            Models.ProyectoNota oProyectoNota = null;
            List<Models.ProyectoNota> lst = new List<Models.ProyectoNota>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario)
                };

                dr = cDblib.DataReader("SUP_GETPROYECTOS_PT_BITACORA_IAP", dbparams);
                while (dr.Read())
                {
                    oProyectoNota = new Models.ProyectoNota();
                    oProyectoNota.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oProyectoNota.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oProyectoNota.t301_denominacion = Convert.ToString(dr["t301_denominacion"]);
                    oProyectoNota.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oProyectoNota.t305_cualidad = Convert.ToString(dr["t305_cualidad"]);
                    oProyectoNota.t301_estado = Convert.ToString(dr["t301_estado"]);
                    if (!Convert.IsDBNull(dr["Responsable_Proyecto"]))
                        oProyectoNota.Responsable_Proyecto = Convert.ToString(dr["Responsable_Proyecto"]);
                    oProyectoNota.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oProyectoNota.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);

                    lst.Add(oProyectoNota);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de proyectos técnicos en los que un usuario puede consultar su bitácora (incluyendo proyectos cerrados)
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        internal List<IB.SUPER.APP.Models.NodoBasico> ProyectosTecnicos(int t314_idusuario, int t305_idproyectosubnodo)
        {
            IB.SUPER.APP.Models.NodoBasico oNodo = null;
            List<IB.SUPER.APP.Models.NodoBasico> lst = new List<IB.SUPER.APP.Models.NodoBasico>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.cod_psn, t305_idproyectosubnodo)
                };

                dr = cDblib.DataReader("SUP_GETPTS_BITACORA_IAP", dbparams);
                while (dr.Read())
                {
                    oNodo = new IB.SUPER.APP.Models.NodoBasico();
                    oNodo.identificador = Convert.ToInt32(dr["t331_idpt"]);
                    oNodo.denominacion = Convert.ToString(dr["t331_despt"]);

                    lst.Add(oNodo);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }


        #endregion

        #region funciones privadas
        private SqlParameter Param(enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            ParameterDirection paramDirection = ParameterDirection.Input;

            switch (dbField)
            {
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.cod_psn:
                    paramName = "@t305_idproyectosubnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.des_asunto:
                    paramName = "@t382_desasunto";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.estado:
                    paramName = "@t382_estado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.ffinD:
                    paramName = "@ffinD";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.flimiteD:
                    paramName = "@flimiteD";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.fnotificacionD:
                    paramName = "@fnotificacionD";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.ffinH:
                    paramName = "@ffinH";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.flimiteH:
                    paramName = "@flimiteH";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.fnotificacionH:
                    paramName = "@fnotificacionH";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.prioridad:
                    paramName = "@t382_prioridad";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.severidad:
                    paramName = "@t382_severidad";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.tipo:
                    paramName = "@t384_idtipo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.orden:
                    paramName = "@nOrden";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.ascdesc:
                    paramName = "@nAscDesc";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.cod_pt:
                    paramName = "@t331_idpt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.des_asuntoPT:
                    paramName = "@T409_desasunto";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.estadoPT:
                    paramName = "@T409_estado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.prioridadPT:
                    paramName = "T409_prioridad";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.severidadPT:
                    paramName = "@T409_severidad";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
               //TAREA
                case enumDBFields.cod_tarea:
                    paramName = "@t332_idtarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.des_asuntoT:
                    paramName = "@T600_desasunto";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.estadoT:
                    paramName = "@T600_estado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.prioridadT:
                    paramName = "T600_prioridad";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.severidadT:
                    paramName = "@T600_severidad";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }
        private Models.Bitacora ponerAsunto(IDataReader dr)
        {
            Models.Bitacora oElem = new Models.Bitacora();

            oElem.ASoACC = "AS";
            oElem.codigo = Convert.ToInt32(dr["t382_idasunto"]);
            oElem.fNotificacion = DateTime.Parse(dr["t382_fnotificacion"].ToString());
            oElem.desTipo = dr["t384_destipo"].ToString();
            oElem.denominacion = dr["t382_desasunto"].ToString();
            oElem.severidad = dr["t382_severidad"].ToString();
            oElem.prioridad = dr["t382_prioridad"].ToString();
            if (dr["t382_flimite"].ToString()!="")
                oElem.fLimite = DateTime.Parse(dr["t382_flimite"].ToString());
            if (dr["t382_ffin"].ToString() != "")
                oElem.fFin = DateTime.Parse(dr["t382_ffin"].ToString());
            //oElem.avance = Convert.ToByte(dr["t383_avance"]);
            oElem.estado = dr["t382_estado"].ToString();
            oElem.descripcion = dr["t382_desasuntolong"].ToString();

            return oElem;
        }
        private Models.Bitacora ponerAccion(IDataReader dr)
        {
            Models.Bitacora oElem = new Models.Bitacora();

            oElem.ASoACC = "AC";
            oElem.codigo = Convert.ToInt32(dr["t383_idaccion"]);
            oElem.fNotificacion = DateTime.Parse(dr["t382_fnotificacion"].ToString());
            oElem.desTipo = dr["t384_destipo"].ToString();
            oElem.denominacion = dr["t383_desaccion"].ToString();
            //oElem.severidad = dr["t382_severidad"].ToString();
            //oElem.prioridad = dr["t382_prioridad"].ToString();
            if (dr["t383_flimite"].ToString() != "")
                oElem.fLimite = DateTime.Parse(dr["t383_flimite"].ToString());
            if (dr["t383_ffin"].ToString() != "")
                oElem.fFin = DateTime.Parse(dr["t383_ffin"].ToString());
            oElem.avance = Convert.ToByte(dr["t383_avance"]);
            //oElem.estado = dr["t382_estado"].ToString();
            oElem.descripcion = dr["t383_desaccionlong"].ToString();

            return oElem;
        }

        private Models.Bitacora ponerAsuntoPT(IDataReader dr)
        {
            Models.Bitacora oElem = new Models.Bitacora();

            oElem.ASoACC = "AS";
            oElem.codigo = Convert.ToInt32(dr["T409_idasunto"]);
            oElem.fNotificacion = DateTime.Parse(dr["T409_fnotificacion"].ToString());
            oElem.desTipo = dr["t384_destipo"].ToString();
            oElem.denominacion = dr["T409_desasunto"].ToString();
            oElem.severidad = dr["T409_severidad"].ToString();
            oElem.prioridad = dr["T409_prioridad"].ToString();
            if (dr["T409_flimite"].ToString() != "")
                oElem.fLimite = DateTime.Parse(dr["T409_flimite"].ToString());
            if (dr["T409_ffin"].ToString() != "")
                oElem.fFin = DateTime.Parse(dr["T409_ffin"].ToString());
            //oElem.avance = Convert.ToByte(dr["t383_avance"]);
            oElem.estado = dr["T409_estado"].ToString();
            oElem.descripcion = dr["T409_desasuntolong"].ToString();

            return oElem;
        }
        private Models.Bitacora ponerAccionPT(IDataReader dr)
        {
            Models.Bitacora oElem = new Models.Bitacora();

            oElem.ASoACC = "AC";
            oElem.codigo = Convert.ToInt32(dr["T410_idaccion"]);
            oElem.fNotificacion = DateTime.Parse(dr["T409_fnotificacion"].ToString());
            oElem.desTipo = dr["t384_destipo"].ToString();
            oElem.denominacion = dr["T410_desaccion"].ToString();
            //oElem.severidad = dr["t382_severidad"].ToString();
            //oElem.prioridad = dr["t382_prioridad"].ToString();
            if (dr["T410_flimite"].ToString() != "")
                oElem.fLimite = DateTime.Parse(dr["T410_flimite"].ToString());
            if (dr["T410_ffin"].ToString() != "")
                oElem.fFin = DateTime.Parse(dr["T410_ffin"].ToString());
            oElem.avance = Convert.ToByte(dr["T410_avance"]);
            //oElem.estado = dr["t382_estado"].ToString();
            oElem.descripcion = dr["T410_desaccionlong"].ToString();

            return oElem;
        }


        private Models.Bitacora ponerAsuntoT(IDataReader dr)
        {
            Models.Bitacora oElem = new Models.Bitacora();

            oElem.ASoACC = "AS";
            oElem.codigo = Convert.ToInt32(dr["T600_idasunto"]);
            oElem.fNotificacion = DateTime.Parse(dr["T600_fnotificacion"].ToString());
            oElem.desTipo = dr["t384_destipo"].ToString();
            oElem.denominacion = dr["T600_desasunto"].ToString();
            oElem.severidad = dr["T600_severidad"].ToString();
            oElem.prioridad = dr["T600_prioridad"].ToString();
            if (dr["T600_flimite"].ToString() != "")
                oElem.fLimite = DateTime.Parse(dr["T600_flimite"].ToString());
            if (dr["T600_ffin"].ToString() != "")
                oElem.fFin = DateTime.Parse(dr["T600_ffin"].ToString());
            //oElem.avance = Convert.ToByte(dr["t383_avance"]);
            oElem.estado = dr["T600_estado"].ToString();
            oElem.descripcion = dr["T600_desasuntolong"].ToString();

            return oElem;
        }
        private Models.Bitacora ponerAccionT(IDataReader dr)
        {
            Models.Bitacora oElem = new Models.Bitacora();

            oElem.ASoACC = "AC";
            oElem.codigo = Convert.ToInt32(dr["T601_idaccion"]);
            oElem.fNotificacion = DateTime.Parse(dr["T600_fnotificacion"].ToString());
            oElem.desTipo = dr["t384_destipo"].ToString();
            oElem.denominacion = dr["T601_desaccion"].ToString();
            //oElem.severidad = dr["t382_severidad"].ToString();
            //oElem.prioridad = dr["t382_prioridad"].ToString();
            if (dr["T601_flimite"].ToString() != "")
                oElem.fLimite = DateTime.Parse(dr["T601_flimite"].ToString());
            if (dr["T601_ffin"].ToString() != "")
                oElem.fFin = DateTime.Parse(dr["T601_ffin"].ToString());
            oElem.avance = Convert.ToByte(dr["T601_avance"]);
            //oElem.estado = dr["t382_estado"].ToString();
            oElem.descripcion = dr["T601_desaccionlong"].ToString();

            return oElem;
        }

        #endregion
    }
}