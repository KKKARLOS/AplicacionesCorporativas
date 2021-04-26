using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for AccionPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class AccionPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta204_idaccionpreventa = 1,
            ta204_fechafinestipulada = 2,
            ta204_estado = 3,
            ta204_descripcion = 4,
            ta204_observaciones = 5,
            ta201_idsubareapreventa = 6,
            t001_idficepi_lider = 7,
            t001_idficepi_promotor = 8,
            ta205_idtipoaccionpreventa = 9,
            ta206_idsolicitudpreventa = 10,
            t331_idpt = 11,
            ta207_idtareapreventa = 12,
            guidprovisional = 13,
            ta204_motivoanulacion = 14,
            ta199_idunidadpreventa = 15,
            ta200_idareapreventa = 16,
            ta206_iditemorigen = 17,
            ta206_itemorigen = 18,
            ta204_fechafinestipulada_ini = 19,
            ta204_fechafinestipulada_fin = 20,
            t001_idficepi = 21,
            ta201_permitirautoasignacionlider = 22,

            TABUNIDAD = 23,
            TABAREA = 24,
            TABSUBAREA = 25,
            TABACCIONPREVENTA = 26,
            TABTIPOACCION = 27,
            TABLIDER = 28,
            importe_desde = 29,
            importe_hasta = 30,
            t001_idficepi_comercial = 31,
            TABCUENTA = 32,

            actuocomoadministrador = 33,
            t001_idficepi_ultmodificador = 34
           

            
        }

        internal AccionPreventa(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas
        /// <summary>
        /// Inserta un AccionPreventa
        /// </summary>
        internal int Insert(Models.AccionPreventa oAccionPreventa, Guid guidprovisional, int t001_idficepi_ultmodificador)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[10] {
                    Param(enumDBFields.ta204_fechafinestipulada, oAccionPreventa.ta204_fechafinestipulada),
					Param(enumDBFields.ta204_descripcion, oAccionPreventa.ta204_descripcion),
					Param(enumDBFields.ta204_observaciones, oAccionPreventa.ta204_observaciones),
					Param(enumDBFields.ta201_idsubareapreventa, oAccionPreventa.ta201_idsubareapreventa),
					Param(enumDBFields.t001_idficepi_lider, oAccionPreventa.t001_idficepi_lider),
					Param(enumDBFields.t001_idficepi_promotor, oAccionPreventa.t001_idficepi_promotor),
					Param(enumDBFields.ta205_idtipoaccionpreventa, oAccionPreventa.ta205_idtipoaccionpreventa),
					Param(enumDBFields.ta206_idsolicitudpreventa, oAccionPreventa.ta206_idsolicitudpreventa),
                    Param(enumDBFields.guidprovisional, guidprovisional),
                    Param(enumDBFields.t001_idficepi_ultmodificador, t001_idficepi_ultmodificador),
                };

                return (int)cDblib.ExecuteScalar("SIC_ACCIONPREVENTA_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Inserta un AccionPreventa
        /// </summary>
        internal int AsignarLider(int ta204_idaccionpreventa, int t001_idficepi_lider)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa),
					Param(enumDBFields.t001_idficepi_lider, t001_idficepi_lider)
				};

                return (int)cDblib.ExecuteScalar("SIC_ASIGNAR_LIDER_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Catálogo de acciones de un líder
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public List<Models.AccionPreventa> CatalogoLíder(int t001_idficepi)
        {
            Models.AccionPreventa oAccionPreventa= null;
            List<Models.AccionPreventa> lst = new List<Models.AccionPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t001_idficepi_lider, t001_idficepi),
				};

                dr = cDblib.DataReader("    ", dbparams);
                while (dr.Read())
                {
                    oAccionPreventa = new Models.AccionPreventa();

                    if (!Convert.IsDBNull(dr["ta204_idaccionpreventa"]))
                        oAccionPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["lider"]))
                        oAccionPreventa.lider = Convert.ToString(dr["lider"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinestipulada"]))
                        oAccionPreventa.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    if (!Convert.IsDBNull(dr["tipoAccion"]))
                        oAccionPreventa.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    if (!Convert.IsDBNull(dr["unidadPreventa"]))
                        oAccionPreventa.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);
                    if (!Convert.IsDBNull(dr["areaPreventa"]))
                        oAccionPreventa.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    if (!Convert.IsDBNull(dr["subareaPreventa"]))
                        oAccionPreventa.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    if (!Convert.IsDBNull(dr["promotor"]))
                        oAccionPreventa.promotor = Convert.ToString(dr["promotor"]);
                    

                    lst.Add(oAccionPreventa);
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


        public Models.AccionPreventa numAcciones(int t001_idficepi)
        {
            Models.AccionPreventa oAccionPreventa = null;
            
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t001_idficepi_lider, t001_idficepi),
				};

                dr = cDblib.DataReader("SIC_NUMACCIONESLIDER_C", dbparams);
                if (dr.Read())
                {
                    oAccionPreventa = new Models.AccionPreventa();
                    oAccionPreventa.numaccionesbylider = Convert.ToInt32(dr["numacciones"].ToString());

                }

                return oAccionPreventa;

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
        /// Obtiene un AccionPreventa a partir del id
        /// </summary>
        internal Models.AccionPreventa Select(Int32 ta204_idaccionpreventa)
        {
            Models.AccionPreventa oAccionPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa)
				};

                dr = cDblib.DataReader("SIC_ACCIONPREVENTA_S", dbparams);
                if (dr.Read())
                {
                    oAccionPreventa = new Models.AccionPreventa();
                    oAccionPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    oAccionPreventa.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinestipulada"]))
                        oAccionPreventa.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinreal"]))
                        oAccionPreventa.ta204_fechafinreal = Convert.ToDateTime(dr["ta204_fechafinreal"]);
                    oAccionPreventa.ta204_estado = Convert.ToString(dr["ta204_estado"]);
                    oAccionPreventa.ta204_descripcion = Convert.ToString(dr["ta204_descripcion"]);
                    if (!Convert.IsDBNull(dr["ta204_observaciones"]))
                        oAccionPreventa.ta204_observaciones = Convert.ToString(dr["ta204_observaciones"]);
                    if (!Convert.IsDBNull(dr["ta204_motivoanulacion"]))
                        oAccionPreventa.ta204_motivoanulacion = Convert.ToString(dr["ta204_motivoanulacion"]);
                    oAccionPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_lider"]))
                        oAccionPreventa.t001_idficepi_lider = Convert.ToInt32(dr["t001_idficepi_lider"]);
                    oAccionPreventa.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    oAccionPreventa.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    oAccionPreventa.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    oAccionPreventa.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
   
                    oAccionPreventa.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    oAccionPreventa.ta199_idunidadpreventa = Convert.ToInt32(dr["ta199_idunidadpreventa"]);
                    oAccionPreventa.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);
                    oAccionPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);
                    oAccionPreventa.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    oAccionPreventa.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    if (!Convert.IsDBNull(dr["lider"]))
                        oAccionPreventa.lider = Convert.ToString(dr["lider"]).Trim();
                    oAccionPreventa.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    oAccionPreventa.tareasCount = Convert.ToInt32(dr["tareasCount"]);
                    if (!Convert.IsDBNull(dr["ta206_iditemorigen"]))
                        oAccionPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_itemorigen"]))
                        oAccionPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_estado"]))
                        oAccionPreventa.ta206_estado = Convert.ToString(dr["ta206_estado"]);

                }
                return oAccionPreventa;

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

        internal Models.AccionPreventa accionPreventa_catTareas(Int32 ta204_idaccionpreventa, Nullable<Int32> t001_idficepi)
        {
            Models.AccionPreventa oAccionPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa),
                    Param(enumDBFields.t001_idficepi, t001_idficepi)
				};

                dr = cDblib.DataReader("SIC_ACCIONPREVENTA_CATTAREAS", dbparams);
                if (dr.Read())
                {
                    oAccionPreventa = new Models.AccionPreventa();
                    oAccionPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    oAccionPreventa.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    if (!Convert.IsDBNull(dr["lider"]))
                        oAccionPreventa.lider = Convert.ToString(dr["lider"]).Trim();
                    oAccionPreventa.btnAddTarea = Convert.ToBoolean(dr["btnAddTarea"]);
                    if (!Convert.IsDBNull(dr["ta206_itemorigen"]))
                        oAccionPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_iditemorigen"]))
                        oAccionPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinestipulada"]))
                        oAccionPreventa.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    
                }
                return oAccionPreventa;

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
        /// Actualiza un AccionPreventa a partir del id
        /// </summary>
        internal int Update(Models.AccionPreventa oAccionPreventa, int t001_idficepi_ultmodificador)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[9] {
					Param(enumDBFields.ta204_idaccionpreventa, oAccionPreventa.ta204_idaccionpreventa),
					Param(enumDBFields.ta204_fechafinestipulada, oAccionPreventa.ta204_fechafinestipulada),
					Param(enumDBFields.ta204_estado, oAccionPreventa.ta204_estado),
					Param(enumDBFields.ta204_descripcion, oAccionPreventa.ta204_descripcion),
					Param(enumDBFields.ta204_observaciones, oAccionPreventa.ta204_observaciones),
					Param(enumDBFields.ta201_idsubareapreventa, oAccionPreventa.ta201_idsubareapreventa),
					Param(enumDBFields.t001_idficepi_lider, oAccionPreventa.t001_idficepi_lider),
                    Param(enumDBFields.ta204_motivoanulacion, oAccionPreventa.ta204_motivoanulacion),
                    Param(enumDBFields.t001_idficepi_ultmodificador, t001_idficepi_ultmodificador)
				};

                return (int)cDblib.Execute("SIC_ACCIONPREVENTA_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza un AccionPreventa a partir del id
        /// </summary>
    //    internal int Update_Comercial(Models.AccionPreventa oAccionPreventa)
    //    {
    //        try
    //        {
    //            SqlParameter[] dbparams = new SqlParameter[4] {
				//	Param(enumDBFields.ta204_idaccionpreventa, oAccionPreventa.ta204_idaccionpreventa),
				//	Param(enumDBFields.ta204_fechafinestipulada, oAccionPreventa.ta204_fechafinestipulada),
				//	Param(enumDBFields.ta204_descripcion, oAccionPreventa.ta204_descripcion),
				//	Param(enumDBFields.ta204_observaciones, oAccionPreventa.ta204_observaciones),
				//};

    //            return (int)cDblib.Execute("SIC_ACCIONPREVENTA_U1", dbparams);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }


        /// <summary>
        /// Obtiene catalogo de acciones segúbn los criterios de filtrado
        /// </summary>
        /// <param name="ta206_idsolicitudpreventa"></param>
        /// <returns></returns>
        internal List<Models.AccionPreventaCAT> Catalogo(Models.AccionCatRequestFilter rf)
        {
            Models.AccionPreventaCAT o = null;
            List<Models.AccionPreventaCAT> lst = new List<Models.AccionPreventaCAT>();
            IDataReader dr = null;


            try
            {
                SqlParameter[] dbparams = new SqlParameter[13] {
					Param(enumDBFields.ta206_idsolicitudpreventa, rf.ta206_idsolicitudpreventa),
                    Param(enumDBFields.ta199_idunidadpreventa, rf.ta199_idunidadpreventa),
                    Param(enumDBFields.ta200_idareapreventa, rf.ta200_idareapreventa),
                    Param(enumDBFields.ta201_idsubareapreventa, rf.ta201_idsubareapreventa),
                    Param(enumDBFields.ta204_idaccionpreventa, rf.ta204_idaccionpreventa),
                    Param(enumDBFields.ta205_idtipoaccionpreventa, rf.ta205_idtipoaccionpreventa),
                    Param(enumDBFields.t001_idficepi_lider, rf.t001_idficepilider),
                    Param(enumDBFields.t001_idficepi_promotor, rf.t001_idficepipromotor),
                    Param(enumDBFields.ta204_estado, rf.ta204_estado),
                    Param(enumDBFields.ta206_iditemorigen, rf.ta206_iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, rf.ta206_itemorigen),
                    Param(enumDBFields.ta204_fechafinestipulada_ini, rf.ta204_fechafinestipuladaini),
                    Param(enumDBFields.ta204_fechafinestipulada_fin, rf.ta204_fechafinestipuladafin)
				};


                dr = cDblib.DataReader("SIC_ACCIONESPREVENTA_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionPreventaCAT();
                    o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinreal"]))
                        o.ta204_fechafinreal = Convert.ToDateTime(dr["ta204_fechafinreal"]);

                    if (!Convert.IsDBNull(dr["ta204_fechacreacion"]))
                        o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    
                    o.estadoAccion = Convert.ToString(dr["estadoAccion"]);
                    o.ta204_descripcion = Convert.ToString(dr["ta204_descripcion"]);
                    if (!Convert.IsDBNull(dr["ta204_observaciones"]))
                        o.ta204_observaciones = Convert.ToString(dr["ta204_observaciones"]);
                    o.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_lider"]))
                        o.t001_idficepi_lider = Convert.ToInt32(dr["t001_idficepi_lider"]);
                    o.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    o.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    o.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    o.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    o.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);
                    o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    if (!Convert.IsDBNull(dr["lider"]))
                        o.lider = Convert.ToString(dr["lider"]).Trim();
                    if (!Convert.IsDBNull(dr["promotor"]))
                        o.promotor = Convert.ToString(dr["promotor"]).Trim();
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"]))
                        o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);

                    lst.Add(o);

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

        internal List<Models.AccionPreventaCAT> CatalogoMisAccionescomoLider(int t001_idficepi)
        {
            Models.AccionPreventaCAT o = null;
            List<Models.AccionPreventaCAT> lst = new List<Models.AccionPreventaCAT>();
            IDataReader dr = null;


            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t001_idficepi, t001_idficepi)
				};


                dr = cDblib.DataReader("SIC_MISACCIONESCOMOLIDER_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionPreventaCAT();
                    o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    o.estadoAccion = "A"; //Siempre están abiertas                    
                    o.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    o.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);                    
                    o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    o.promotor = Convert.ToString(dr["promotor"]).Trim();
                    o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["importe"])) o.importe = Convert.ToDouble(dr["importe"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.den_item = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["den_unidadcomercial"])) o.den_unidadcomercial = Convert.ToString(dr["den_unidadcomercial"]);
                    if (!Convert.IsDBNull(dr["moneda"])) o.moneda = Convert.ToString(dr["moneda"]);
                    if (!Convert.IsDBNull(dr["ta208_negrita"])) o.ta208_negrita = Convert.ToBoolean(dr["ta208_negrita"]);
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"])) o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);

                    lst.Add(o);

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


       
        internal List<Models.AccionPreventaCAT> CatalogoPosibleLider(int t001_idficepi)
        {
            Models.AccionPreventaCAT o = null;
            List<Models.AccionPreventaCAT> lst = new List<Models.AccionPreventaCAT>();
            IDataReader dr = null;


            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t001_idficepi, t001_idficepi)
				};


                dr = cDblib.DataReader("SIC_MISACCIONESCOMOPOSIBLELIDER_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionPreventaCAT();
                    o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    o.estadoAccion = "A"; //Siempre están abiertas
                    //o.ta204_descripcion = Convert.ToString(dr["ta204_descripcion"]);
                    //if (!Convert.IsDBNull(dr["ta204_observaciones"]))
                    //    o.ta204_observaciones = Convert.ToString(dr["ta204_observaciones"]);
                    //o.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    //o.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    //o.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    //o.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    o.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    //o.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);
                    o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    o.promotor = Convert.ToString(dr["promotor"]).Trim();
                    o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["importe"])) o.importe = Convert.ToDouble(dr["importe"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.den_item = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["den_unidadcomercial"])) o.den_unidadcomercial = Convert.ToString(dr["den_unidadcomercial"]);
                    if (!Convert.IsDBNull(dr["moneda"])) o.moneda = Convert.ToString(dr["moneda"]);
                    if (!Convert.IsDBNull(dr["ta208_negrita"])) o.ta208_negrita = Convert.ToBoolean(dr["ta208_negrita"]);
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"])) o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);

                    lst.Add(o);

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

        internal List<Models.AccionPreventaCAT> CatalogoPdteLider(int t001_idficepi, bool? ta201_permitirautoasignacionlider)
        {
            Models.AccionPreventaCAT o = null;
            List<Models.AccionPreventaCAT> lst = new List<Models.AccionPreventaCAT>();
            IDataReader dr = null;


            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.ta201_permitirautoasignacionlider, ta201_permitirautoasignacionlider),
                    Param(enumDBFields.actuocomoadministrador, Shared.Utils.EsAdminProduccion())
				};


                dr = cDblib.DataReader("SIC_ACCIONESPENDIENTESDEASIGNARLIDER_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionPreventaCAT();
                    o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    o.estadoAccion = "A"; //Siempre están abiertas
                    //o.ta204_descripcion = Convert.ToString(dr["ta204_descripcion"]);
                    //if (!Convert.IsDBNull(dr["ta204_observaciones"]))
                    //    o.ta204_observaciones = Convert.ToString(dr["ta204_observaciones"]);
                    //o.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    //o.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    //o.ta205_idtipoaccionpreventa = Convert.ToInt16(dr["ta205_idtipoaccionpreventa"]);
                    //o.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    o.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    //o.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);
                    o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    o.ta201_permitirautoasignacionlider = Convert.ToBoolean(dr["ta201_permitirautoasignacionlider"]);
                    o.promotor = Convert.ToString(dr["promotor"]).Trim();
                    o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["importe"])) o.importe = Convert.ToDouble(dr["importe"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.den_item = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["den_unidadcomercial"])) o.den_unidadcomercial = Convert.ToString(dr["den_unidadcomercial"]);
                    if (!Convert.IsDBNull(dr["moneda"])) o.moneda = Convert.ToString(dr["moneda"]);
                    if (!Convert.IsDBNull(dr["ta208_negrita"])) o.ta208_negrita = Convert.ToBoolean(dr["ta208_negrita"]);
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"])) o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);

                    lst.Add(o);

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


        internal List<Models.AccionPreventaCAT> CatalogoPdteAsignarLider(int t001_idficepi)
        {
            Models.AccionPreventaCAT o = null;
            List<Models.AccionPreventaCAT> lst = new List<Models.AccionPreventaCAT>();
            IDataReader dr = null;


            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),                    
                    Param(enumDBFields.actuocomoadministrador, Shared.Utils.EsAdminProduccion())
				};


                dr = cDblib.DataReader("", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionPreventaCAT();
                    o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    o.estadoAccion = "A"; //Siempre están abiertas                    
                    o.tipoAccion = Convert.ToString(dr["tipoAccion"]);                    
                    o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    o.ta201_permitirautoasignacionlider = Convert.ToBoolean(dr["ta201_permitirautoasignacionlider"]);
                    o.promotor = Convert.ToString(dr["promotor"]).Trim();
                    o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["importe"])) o.importe = Convert.ToDouble(dr["importe"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.den_item = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["den_unidadcomercial"])) o.den_unidadcomercial = Convert.ToString(dr["den_unidadcomercial"]);
                    if (!Convert.IsDBNull(dr["moneda"])) o.moneda = Convert.ToString(dr["moneda"]);

                    lst.Add(o);

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

        public List<Models.AccionCatFigAreaSubarea> CatalogoFigAreaSubarea(bool admin, int t001_idficepi, AccionCatFigAreaSubareaFilter rf)
        {

            Models.AccionCatFigAreaSubarea o = null;
            List<Models.AccionCatFigAreaSubarea> lst = new List<Models.AccionCatFigAreaSubarea>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[17] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.TABUNIDAD,  Shared.Database.ArrayToDataTable(rf.unidades, "numero")),
                    Param(enumDBFields.TABAREA, Shared.Database.ArrayToDataTable(rf.areas, "numero")),
                    Param(enumDBFields.TABSUBAREA, Shared.Database.ArrayToDataTable(rf.subareas, "numero")),
                    Param(enumDBFields.TABTIPOACCION, Shared.Database.ArrayToDataTable(rf.acciones, "numero")),
                    Param(enumDBFields.TABLIDER, Shared.Database.ArrayToDataTable(rf.lideres, "numero")),
                    Param(enumDBFields.t001_idficepi_promotor, rf.promotor),
                    Param(enumDBFields.ta204_estado, rf.estado),
                    Param(enumDBFields.ta206_iditemorigen, rf.iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, rf.itemorigen),
                    Param(enumDBFields.ta204_fechafinestipulada_ini, rf.ffinDesde),
                    Param(enumDBFields.ta204_fechafinestipulada_fin, rf.ffinHasta),
                    Param(enumDBFields.importe_desde, rf.importeDesde),
                    Param(enumDBFields.importe_hasta, rf.importeHasta),
                    Param(enumDBFields.t001_idficepi_comercial, rf.comercial),
                    Param(enumDBFields.TABCUENTA, Shared.Database.ArrayToDataTable(rf.clientes, "cadena")),
                    Param(enumDBFields.actuocomoadministrador, admin)
				};


                dr = cDblib.DataReader("SIC_ACCIONESPREVENTAMULTIPARAFICEPI_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionCatFigAreaSubarea();
                    if (!Convert.IsDBNull(dr["ta204_idaccionpreventa"])) o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinestipulada"])) o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    if (!Convert.IsDBNull(dr["ta204_fechacreacion"])) o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinreal"])) o.ta204_fechafinreal = Convert.ToDateTime(dr["ta204_fechafinreal"]);
                    if (!Convert.IsDBNull(dr["ta204_estado"])) o.ta204_estado = Convert.ToString(dr["ta204_estado"]);
                    if (!Convert.IsDBNull(dr["ta204_descripcion"])) o.ta204_descripcion = Convert.ToString(dr["ta204_descripcion"]);
                    if (!Convert.IsDBNull(dr["ta204_observaciones"])) o.ta204_observaciones = Convert.ToString(dr["ta204_observaciones"]);
                    if (!Convert.IsDBNull(dr["ta201_idsubareapreventa"])) o.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_lider"])) o.t001_idficepi_lider = Convert.ToInt32(dr["t001_idficepi_lider"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_promotor"])) o.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    if (!Convert.IsDBNull(dr["ta205_idtipoaccionpreventa"])) o.ta205_idtipoaccionpreventa = Convert.ToInt32(dr["ta205_idtipoaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta206_idsolicitudpreventa"])) o.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    if (!Convert.IsDBNull(dr["tipoAccion"])) o.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    if (!Convert.IsDBNull(dr["unidadPreventa"])) o.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);
                    if (!Convert.IsDBNull(dr["areaPreventa"])) o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    if (!Convert.IsDBNull(dr["subareaPreventa"])) o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    if (!Convert.IsDBNull(dr["lider"])) o.lider = Convert.ToString(dr["lider"]);
                    if (!Convert.IsDBNull(dr["promotor"])) o.promotor = Convert.ToString(dr["promotor"]);
                    if (!Convert.IsDBNull(dr["comercial"])) o.comercial = Convert.ToString(dr["comercial"]);
                    if (!Convert.IsDBNull(dr["ta206_iditemorigen"])) o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_itemorigen"])) o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["importe"])) o.importe = Convert.ToDouble(dr["importe"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.den_item = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["den_unidadcomercial"])) o.den_unidadcomercial = Convert.ToString(dr["den_unidadcomercial"]);
                    if (!Convert.IsDBNull(dr["moneda"])) o.moneda = Convert.ToString(dr["moneda"]);
                    if (!Convert.IsDBNull(dr["ta208_negrita"])) o.ta208_negrita = Convert.ToBoolean(dr["ta208_negrita"]);
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"])) o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);


                    lst.Add(o);

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

        public List<Models.AccionCatAmbitoCRM> CatalogoAmbitoCRM(int t001_idficepi, AccionCatAmbitoCRMFilter rf)
        {

            Models.AccionCatAmbitoCRM o = null;
            List<Models.AccionCatAmbitoCRM> lst = new List<Models.AccionCatAmbitoCRM>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[15] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.TABUNIDAD,  Shared.Database.ArrayToDataTable(rf.unidades, "numero")),
                    Param(enumDBFields.TABAREA, Shared.Database.ArrayToDataTable(rf.areas, "numero")),
                    Param(enumDBFields.TABSUBAREA, Shared.Database.ArrayToDataTable(rf.subareas, "numero")),
                    Param(enumDBFields.TABTIPOACCION, Shared.Database.ArrayToDataTable(rf.acciones, "numero")),
                    Param(enumDBFields.TABLIDER, Shared.Database.ArrayToDataTable(rf.lideres, "numero")),
                    Param(enumDBFields.t001_idficepi_promotor, rf.promotor),
                    Param(enumDBFields.ta204_estado, rf.estado),
                    Param(enumDBFields.ta206_iditemorigen, rf.iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, rf.itemorigen),
                    Param(enumDBFields.ta204_fechafinestipulada_ini, rf.ffinDesde),
                    Param(enumDBFields.ta204_fechafinestipulada_fin, rf.ffinHasta),
                    Param(enumDBFields.importe_desde, rf.importeDesde),
                    Param(enumDBFields.importe_hasta, rf.importeHasta),
                    Param(enumDBFields.TABCUENTA, Shared.Database.ArrayToDataTable(rf.clientes, "cadena"))
				};


                dr = cDblib.DataReader("SIC_ACCIONESPREVENTADEUNCOMERCIAL_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionCatAmbitoCRM();
                    if (!Convert.IsDBNull(dr["ta204_idaccionpreventa"])) o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinestipulada"])) o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    if (!Convert.IsDBNull(dr["ta204_fechacreacion"])) o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);
                    if (!Convert.IsDBNull(dr["ta204_fechafinreal"])) o.ta204_fechafinreal = Convert.ToDateTime(dr["ta204_fechafinreal"]);
                    if (!Convert.IsDBNull(dr["ta204_estado"])) o.ta204_estado = Convert.ToString(dr["ta204_estado"]);
                    if (!Convert.IsDBNull(dr["ta204_descripcion"])) o.ta204_descripcion = Convert.ToString(dr["ta204_descripcion"]);
                    if (!Convert.IsDBNull(dr["ta204_observaciones"])) o.ta204_observaciones = Convert.ToString(dr["ta204_observaciones"]);
                    if (!Convert.IsDBNull(dr["ta201_idsubareapreventa"])) o.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_lider"])) o.t001_idficepi_lider = Convert.ToInt32(dr["t001_idficepi_lider"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_promotor"])) o.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    if (!Convert.IsDBNull(dr["ta205_idtipoaccionpreventa"])) o.ta205_idtipoaccionpreventa = Convert.ToInt32(dr["ta205_idtipoaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta206_idsolicitudpreventa"])) o.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    if (!Convert.IsDBNull(dr["tipoAccion"])) o.tipoAccion = Convert.ToString(dr["tipoAccion"]);
                    if (!Convert.IsDBNull(dr["unidadPreventa"])) o.unidadPreventa = Convert.ToString(dr["unidadPreventa"]);
                    if (!Convert.IsDBNull(dr["areaPreventa"])) o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    if (!Convert.IsDBNull(dr["subareaPreventa"])) o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    if (!Convert.IsDBNull(dr["lider"])) o.lider = Convert.ToString(dr["lider"]);
                    if (!Convert.IsDBNull(dr["promotor"])) o.promotor = Convert.ToString(dr["promotor"]);
                    if (!Convert.IsDBNull(dr["ta206_iditemorigen"])) o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_itemorigen"])) o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["importe"])) o.importe = Convert.ToDouble(dr["importe"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.den_item = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["den_unidadcomercial"])) o.den_unidadcomercial = Convert.ToString(dr["den_unidadcomercial"]);
                    if (!Convert.IsDBNull(dr["moneda"])) o.moneda = Convert.ToString(dr["moneda"]);
                    if (!Convert.IsDBNull(dr["numero_oportunidad"])) o.numero_oportunidad = Convert.ToInt32(dr["numero_oportunidad"]);
                    if (!Convert.IsDBNull(dr["ta205_plazominreq"])) o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);

                    lst.Add(o);

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

        public string EstadoAccion(int ta204_idaccionpreventa)
        {
            IDataReader dr = null;
            string ta204_estado = "X";

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
                    Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa)
				};

                dr = cDblib.DataReader("SIC_ESTADOACCION_S", dbparams);

                if (dr.Read())
                {
                    ta204_estado = Convert.ToString(dr["ta204_estado"]);
                }

                return ta204_estado;

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

        internal List<Models.AccionPreventa> CatalogoImputaciones(string ta206_itemorigen, int ta206_iditemorigen)
        {
            Models.AccionPreventa o = null;
            List<Models.AccionPreventa> lst = new List<Models.AccionPreventa>();
            IDataReader dr = null;


            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta206_itemorigen, ta206_itemorigen),
                    Param(enumDBFields.ta206_iditemorigen, ta206_iditemorigen)                   
                };


                dr = cDblib.DataReader("SIC_IMPUTACIONESPORSOLICITUD", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionPreventa();
                    if (!Convert.IsDBNull(dr["jornadas"]))
                        o.jornadas = decimal.Parse(dr["jornadas"].ToString());

                    if (!Convert.IsDBNull(dr["euros"]))
                        o.euros = decimal.Parse(dr["euros"].ToString());

                    lst.Add(o);

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

        internal List<Models.AccionPreventa> CatalogoAccionesBySolicitud(int ta206_idsolicitudpreventa, int t001_idficepi)
        {
            Models.AccionPreventa o = null;
            List<Models.AccionPreventa> lst = new List<Models.AccionPreventa>();
            IDataReader dr = null;


            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta206_idsolicitudpreventa, ta206_idsolicitudpreventa),
                    Param(enumDBFields.t001_idficepi, t001_idficepi)
                };


                dr = cDblib.DataReader("SIC_ACCIONESPREVENTADEUNASOLICITUDYFICEPI_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionPreventa();
                    o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["tipoAccion"]))
                        o.tipoAccion = Convert.ToString(dr["tipoAccion"]);

                    if (!Convert.IsDBNull(dr["ta201_denominacion"]))
                        o.subareaPreventa = Convert.ToString(dr["ta201_denominacion"]);

                    if (!Convert.IsDBNull(dr["importe"]))
                        o.importe = Convert.ToDouble(dr["importe"]);
                    if (!Convert.IsDBNull(dr["promotor"]))
                        o.promotor = Convert.ToString(dr["promotor"]);

                    if (!Convert.IsDBNull(dr["den_unidadcomercial"]))
                        o.den_unidadcomercial = Convert.ToString(dr["den_unidadcomercial"]);

                    if (!Convert.IsDBNull(dr["lider"]))
                        o.lider = Convert.ToString(dr["lider"]);

                    if(!Convert.IsDBNull(dr["ta204_estado"]))
                        o.ta204_estado = Convert.ToString(dr["ta204_estado"]);

                    if (!Convert.IsDBNull(dr["ta204_fechafinestipulada"]))
                        o.ta204_fechafinestipulada = Convert.ToDateTime(dr["ta204_fechafinestipulada"]);
                    
                    if (!Convert.IsDBNull(dr["ta204_fechafinreal"]))
                        o.ta204_fechafinreal = Convert.ToDateTime(dr["ta204_fechafinreal"]);

                    if (!Convert.IsDBNull(dr["ta204_fechacreacion"]))
                        o.ta204_fechacreacion = Convert.ToDateTime(dr["ta204_fechacreacion"]);

                    if (!Convert.IsDBNull(dr["moneda"]))
                        o.moneda = Convert.ToString(dr["moneda"]);

                    if (!Convert.IsDBNull(dr["linkaccesoaccion"]))
                        o.linkaccesoaccion = Convert.ToBoolean(dr["linkaccesoaccion"]);

                    if (!Convert.IsDBNull(dr["ta205_plazominreq"]))
                        o.ta205_plazominreq = Convert.ToInt16(dr["ta205_plazominreq"]);

                    lst.Add(o);
                    
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
                case enumDBFields.ta204_idaccionpreventa:
                    paramName = "@ta204_idaccionpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta204_fechafinestipulada:
                    paramName = "@ta204_fechafinestipulada";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.ta204_estado:
                    paramName = "@ta204_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.ta204_descripcion:
                    paramName = "@ta204_descripcion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 750;
                    break;
                case enumDBFields.ta204_observaciones:
                    paramName = "@ta204_observaciones";
                    paramType = SqlDbType.VarChar;
                    paramSize = 2147483647;
                    break;
                case enumDBFields.ta201_idsubareapreventa:
                    paramName = "@ta201_idsubareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_lider:
                    paramName = "@t001_idficepi_lider";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_promotor:
                    paramName = "@t001_idficepi_promotor";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta205_idtipoaccionpreventa:
                    paramName = "@ta205_idtipoaccionpreventa";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;
                case enumDBFields.ta206_idsolicitudpreventa:
                    paramName = "@ta206_idsolicitudpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t331_idpt:
                    paramName = "@t331_idpt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.guidprovisional:
                    paramName = "@guidprovisional";
                    paramType = SqlDbType.UniqueIdentifier;
                    paramSize = 16;
                    break;
                case enumDBFields.ta204_motivoanulacion:
                    paramName = "@ta204_motivoanulacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 250;
                    break;
                case enumDBFields.ta199_idunidadpreventa:
                    paramName = "@ta199_idunidadpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta200_idareapreventa:
                    paramName = "@ta200_idareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta206_iditemorigen:
                    paramName = "@ta206_iditemorigen";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta206_itemorigen:
                    paramName = "@ta206_itemorigen";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.ta204_fechafinestipulada_ini:
                    paramName = "@ta204_fechafinestipulada_ini";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.ta204_fechafinestipulada_fin:
                    paramName = "@ta204_fechafinestipulada_fin";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta201_permitirautoasignacionlider:
                    paramName = "@ta201_permitirautoasignacionlider";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                case enumDBFields.TABUNIDAD:
                    paramName = "@TABUNIDAD";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;
                case enumDBFields.TABAREA:
                    paramName = "@TABAREA";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;
                case enumDBFields.TABSUBAREA:
                    paramName = "@TABSUBAREA";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;
                case enumDBFields.TABACCIONPREVENTA:
                    paramName = "@TABACCIONPREVENTA";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;
                case enumDBFields.TABTIPOACCION:
                    paramName = "@TABTIPOACCION";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;
                case enumDBFields.TABLIDER:
                    paramName = "@TABLIDER";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;
                case enumDBFields.importe_desde:
                    paramName = "@importe_desde";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.importe_hasta:
                    paramName = "@importe_hasta";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.t001_idficepi_comercial:
                    paramName = "@t001_idficepi_comercial";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.TABCUENTA:
                    paramName = "@TABCUENTA";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
                    break;

                case enumDBFields.actuocomoadministrador:
                    paramName = "@actuocomoadministrador";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                case enumDBFields.t001_idficepi_ultmodificador:
                    paramName = "@t001_idficepi_ultmodificador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;                                   
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion

    }

}
