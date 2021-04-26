using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for SolicitudPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class SolicitudPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta206_idsolicitudpreventa = 1,
            ta206_denominacion = 2,
            ta206_estado = 3,
            ta206_fechacreacion = 4,
            t001_idficepi_promotor = 5,
            ta206_iditemorigen = 6,
            ta206_itemorigen = 7,
            ta204_idaccionpreventa = 8,
            ta201_idsubareapreventa = 9,
            ta200_idareapreventa = 10,

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
            t001_idficepi = 40,
            ta204_estado = 41,
            ta204_fechafinestipulada_ini = 42,
            ta204_fechafinestipulada_fin = 43,

            actuocomoadministrador = 50,
            ta206_motivoanulacion = 51,
            t001_idficepi_ultmodificador = 52


        }

        internal SolicitudPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un SolicitudPreventa
        /// </summary>
        internal int Insert(Models.SolicitudPreventa oSolicitudPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta206_denominacion, oSolicitudPreventa.ta206_denominacion),
					Param(enumDBFields.t001_idficepi_promotor, oSolicitudPreventa.t001_idficepi_promotor),
					Param(enumDBFields.ta206_iditemorigen, oSolicitudPreventa.ta206_iditemorigen),
					Param(enumDBFields.ta206_itemorigen, oSolicitudPreventa.ta206_itemorigen),
                    Param(enumDBFields.ta200_idareapreventa, oSolicitudPreventa.ta200_idareapreventa)
				};

                return (int)cDblib.ExecuteScalar("SIC_SOLICITUDPREVENTA_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void UpdateDenominacion(int ta206_idsolicitudpreventa, string ta206_denominacion, string ta206_estado, string motivoanulacion, int t001_idficepi_ultmodificador) {

            try
            {
                if (motivoanulacion == "") motivoanulacion = null;

                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.ta206_idsolicitudpreventa, ta206_idsolicitudpreventa),
					Param(enumDBFields.ta206_denominacion, ta206_denominacion),
                    Param(enumDBFields.ta206_estado, ta206_estado),
                    Param(enumDBFields.ta206_motivoanulacion, motivoanulacion),
                    Param(enumDBFields.t001_idficepi_ultmodificador, t001_idficepi_ultmodificador)
                };

                cDblib.ExecuteScalar("SIC_SOLICITUDPREVENTA_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal void EliminarSolicitud(int ta206_idsolicitudpreventa)
        {

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.ta206_idsolicitudpreventa, ta206_idsolicitudpreventa)                    
                };

                cDblib.ExecuteScalar("SIC_SOLICITUDPREVENTA_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal Models.SolicitudPreventa Select(int? ta206_iditemorigen, string ta206_itemorigen)
        {
            Models.SolicitudPreventa oSolicitudPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta206_iditemorigen, ta206_iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, ta206_itemorigen),
                };

                dr = cDblib.DataReader("SIC_SOLICITUDPREVENTA_S", dbparams);
                if (dr.Read())
                {
                    oSolicitudPreventa = new Models.SolicitudPreventa();
                    oSolicitudPreventa.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"]))
                        oSolicitudPreventa.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    oSolicitudPreventa.ta206_estado = Convert.ToString(dr["ta206_estado"]);
                    oSolicitudPreventa.ta206_fechacreacion = Convert.ToDateTime(dr["ta206_fechacreacion"]);
                    oSolicitudPreventa.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    if (!Convert.IsDBNull(dr["ta206_iditemorigen"]))
                        oSolicitudPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    oSolicitudPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oSolicitudPreventa.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["ta200_idareapreventa"]))
                        oSolicitudPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);

                }
                return oSolicitudPreventa;

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

        internal Models.SolicitudPreventa btnAccionesSegunEstadoSolicitud(int? ta206_iditemorigen, string ta206_itemorigen)
        {
            Models.SolicitudPreventa oSolicitudPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta206_iditemorigen, ta206_iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, ta206_itemorigen),
                };

                dr = cDblib.DataReader("SIC_BTNACCIONSEGUNESTADOSOLICITUD", dbparams);
                if (dr.Read())
                {
                    oSolicitudPreventa = new Models.SolicitudPreventa();                   
                    if (!Convert.IsDBNull(dr["botonactivo"]))
                        oSolicitudPreventa.botonactivo = Convert.ToBoolean(dr["botonactivo"]);                   
                }
                return oSolicitudPreventa;

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

        internal Models.ItemCRM SelectOrigen(int ta206_iditemorigen, string ta206_itemorigen)
        {
            Models.ItemCRM o = new Models.ItemCRM() ;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta206_iditemorigen, ta206_iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, ta206_itemorigen),
                };

                dr = cDblib.DataReader("SIC_DATOSHERMES_S", dbparams);
                if (dr.Read())
                {
                    if (!Convert.IsDBNull(dr["tipo_item"])) o.itemorigen = Convert.ToString(dr["tipo_item"]);
                    if (!Convert.IsDBNull(dr["numero_item"])) o.iditemorigen = Convert.ToInt32(dr["numero_item"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.denominacion = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["importe"])) o.importe = Convert.ToString(dr["importe"]);
                    //o.  = Convert.ToString(dr["margen"]);
                    if (!Convert.IsDBNull(dr["estado"])) o.estado = Convert.ToString(dr["estado"]);
                    //o.  = Convert.ToString(dr["cod_cuenta"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.cuenta = Convert.ToString(dr["den_cuenta"]);
                    //o.  = Convert.ToString(dr["codSAP_cuenta"]);
                    if (!Convert.IsDBNull(dr["cod_comercial"])) o.cod_comercial = Convert.ToString(dr["cod_comercial"]);
                    if (!Convert.IsDBNull(dr["etapaventas"])) o.etapaVentas = Convert.ToString(dr["etapaventas"]);
                    if (!Convert.IsDBNull(dr["fechacierre"])) o.fechaCierre = Convert.ToString(dr["fechacierre"]);
                    if (!Convert.IsDBNull(dr["fechalimitepresentacion"])) o.fechaLimitePresentacion = Convert.ToString(dr["fechalimitepresentacion"]);
                    //o.  = Convert.ToString(dr["cod_cr"]);
                    if (!Convert.IsDBNull(dr["den_cr"])) o.centroResponsabilidad = Convert.ToString(dr["den_cr"]);
                    //o.  = Convert.ToString(dr["cod_unidadcomercial"]);
                    if (!Convert.IsDBNull(dr["den_unidadcomercial"])) o.organizacionComercial = Convert.ToString(dr["den_unidadcomercial"]);

                    if (!Convert.IsDBNull(dr["desc_objetivo"]))
                        o.desc_objetivo = Convert.ToString(dr["desc_objetivo"]);

                    if (!Convert.IsDBNull(dr["probabilidadexito"])) o.exito = Convert.ToString(dr["probabilidadexito"]);
                    if (!Convert.IsDBNull(dr["gestorproduccion"])) o.gestorProduccion = Convert.ToString(dr["gestorproduccion"]);
                    if (!Convert.IsDBNull(dr["rentabilidad"])) o.rentabilidad = Convert.ToString(dr["rentabilidad"]);
                    if (!Convert.IsDBNull(dr["areaconocimientotecnico"])) o.areaConTecnologico = Convert.ToString(dr["areaconocimientotecnico"]);
                    if (!Convert.IsDBNull(dr["areaconocimientosectorial"])) o.areaConSectorial = Convert.ToString(dr["areaconocimientosectorial"]);
                    if (!Convert.IsDBNull(dr["duracionproyectoenmeses"])) o.duracionProyecto = Convert.ToString(dr["duracionproyectoenmeses"]);


                    if (!Convert.IsDBNull(dr["fechainicio_objetivo"])) o.fechaInicio = Convert.ToString(dr["fechainicio_objetivo"]);
                    if (!Convert.IsDBNull(dr["fechafin_objetivo"])) o.fechaFin = Convert.ToString(dr["fechafin_objetivo"]);
                    if (!Convert.IsDBNull(dr["oferta_objetivo"])) o.oferta = Convert.ToString(dr["oferta_objetivo"]);
                    if (!Convert.IsDBNull(dr["contratacionprevista_objetivo"])) o.contratacionPrevista = Convert.ToString(dr["contratacionprevista_objetivo"]);
                    if (!Convert.IsDBNull(dr["costeprevisto_objetivo"])) o.costePrevisto = Convert.ToString(dr["costeprevisto_objetivo"]);
                    if (!Convert.IsDBNull(dr["resultado_objetivo"])) o.resultado = Convert.ToString(dr["resultado_objetivo"]);
                    
                    if (!Convert.IsDBNull(dr["moneda"])) o.moneda = Convert.ToString(dr["moneda"]);

                    if (!Convert.IsDBNull(dr["numero_oportunidad"])) o.num_oportunidad = Convert.ToInt32(dr["numero_oportunidad"]);
                    if (!Convert.IsDBNull(dr["den_oportunidad"])) o.den_oportunidad = Convert.ToString(dr["den_oportunidad"]);

                    if (!Convert.IsDBNull(dr["botonactivo"])) o.botonactivo = Convert.ToBoolean(dr["botonactivo"]);

                    if (!Convert.IsDBNull(dr["tipo_negocio"])) o.tipo_negocio = Convert.ToString(dr["tipo_negocio"]);
                    if (!Convert.IsDBNull(dr["oferta_objetivo"])) o.oferta_objetivo = Convert.ToString(dr["oferta_objetivo"]);

                }
                return o;

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

        internal Models.SolicitudPreventa SelectById(int ta206_idsolicitudpreventa)
        {
            Models.SolicitudPreventa oSolicitudPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.ta206_idsolicitudpreventa, ta206_idsolicitudpreventa)
                };

                dr = cDblib.DataReader("SIC_SOLICITUDPREVENTA_S2", dbparams);
                if (dr.Read())
                {
                    oSolicitudPreventa = new Models.SolicitudPreventa();
                    oSolicitudPreventa.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"]))
                        oSolicitudPreventa.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    oSolicitudPreventa.ta206_estado = Convert.ToString(dr["ta206_estado"]);
                    oSolicitudPreventa.ta206_fechacreacion = Convert.ToDateTime(dr["ta206_fechacreacion"]);
                    oSolicitudPreventa.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    oSolicitudPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    oSolicitudPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oSolicitudPreventa.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["ta200_idareapreventa"]))
                        oSolicitudPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);

                     
                }
                return oSolicitudPreventa;

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

        internal Models.SolicitudPreventa getSolicitudbyAccion(int ta204_idaccionpreventa)
        {
            Models.SolicitudPreventa oSolicitudPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa)

                };

                dr = cDblib.DataReader("SIC_SOLICITUDPREVENTA_S1", dbparams);
                if (dr.Read())
                {
                    oSolicitudPreventa = new Models.SolicitudPreventa();
                    oSolicitudPreventa.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"]))
                        oSolicitudPreventa.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    oSolicitudPreventa.ta206_estado = Convert.ToString(dr["ta206_estado"]);
                    oSolicitudPreventa.ta206_fechacreacion = Convert.ToDateTime(dr["ta206_fechacreacion"]);
                    oSolicitudPreventa.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    oSolicitudPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    oSolicitudPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oSolicitudPreventa.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["ta200_idareapreventa"]))
                        oSolicitudPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);

                }
                return oSolicitudPreventa;

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

        internal Models.SolicitudPreventa getSolicitudbyAccion2(int ta204_idaccionpreventa)
        {
            Models.SolicitudPreventa oSolicitudPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa)

                };

                dr = cDblib.DataReader("SIC_SOLICITUDPREVENTA_S3", dbparams);
                if (dr.Read())
                {
                    oSolicitudPreventa = new Models.SolicitudPreventa();
                    oSolicitudPreventa.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    if (!Convert.IsDBNull(dr["denominacion"]))
                        oSolicitudPreventa.ta206_denominacion = Convert.ToString(dr["denominacion"]);
                    oSolicitudPreventa.ta206_estado = Convert.ToString(dr["ta206_estado"]);
                    oSolicitudPreventa.ta206_fechacreacion = Convert.ToDateTime(dr["ta206_fechacreacion"]);
                    oSolicitudPreventa.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    oSolicitudPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    oSolicitudPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oSolicitudPreventa.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["ta200_idareapreventa"]))
                        oSolicitudPreventa.ta200_idareapreventa = Convert.ToInt32(dr["ta200_idareapreventa"]);

                }
                return oSolicitudPreventa;

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

        public List<Models.AccionLider> GetLideresSolicitud(int ta206_iditemorigen, string ta206_itemorigen, int? ta204_idaccionpreventa, int ta201_idsubareapreventa)
        {

            Models.AccionLider o = null;
            List<Models.AccionLider> lst = new List<Models.AccionLider>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.ta206_iditemorigen, ta206_iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, ta206_itemorigen),
                    Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa),
                    Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa)
                };

                dr = cDblib.DataReader("SIC_LIDERESSOLICITUD_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.AccionLider();
                    o.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    o.t001_idficepi_lider = Convert.ToInt32(dr["t001_idficepi_lider"]);
                    o.profesional = Convert.ToString(dr["profesional"]);
                    o.areaPreventa = Convert.ToString(dr["areaPreventa"]);
                    o.subareaPreventa = Convert.ToString(dr["subareaPreventa"]);
                    if (!Convert.IsDBNull(dr["ta203_figura"])) o.posibleLider = true;

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

        public List<Models.SolicCatSuper> CatalogoSolicitudesSUPER(bool admin, int t001_idficepi, SolicCatSuperRF rf)
        {

            Models.SolicCatSuper o = null;
            List<Models.SolicCatSuper> lst = new List<Models.SolicCatSuper>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[19] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.TABUNIDAD,  Shared.Database.ArrayToDataTable(rf.unidades, "numero")),
                    Param(enumDBFields.TABAREA, Shared.Database.ArrayToDataTable(rf.areas, "numero")),
                    Param(enumDBFields.TABSUBAREA, Shared.Database.ArrayToDataTable(rf.subareas, "numero")),
                    Param(enumDBFields.TABTIPOACCION, Shared.Database.ArrayToDataTable(rf.acciones, "numero")),
                    Param(enumDBFields.TABLIDER, Shared.Database.ArrayToDataTable(rf.lideres, "numero")),
                    Param(enumDBFields.t001_idficepi_promotor, rf.promotor),
                    Param(enumDBFields.ta204_estado, rf.estado),
                    Param(enumDBFields.ta206_estado, rf.estadoSolicitud),
                    Param(enumDBFields.ta206_iditemorigen, rf.iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, rf.itemorigen),
                    Param(enumDBFields.ta204_fechafinestipulada_ini, rf.ffinDesde),
                    Param(enumDBFields.ta204_fechafinestipulada_fin, rf.ffinHasta),
                    Param(enumDBFields.importe_desde, rf.importeDesde),
                    Param(enumDBFields.importe_hasta, rf.importeHasta),
                    Param(enumDBFields.t001_idficepi_comercial, rf.comercial),
                    Param(enumDBFields.TABCUENTA, Shared.Database.ArrayToDataTable(rf.clientes, "cadena")),
                    Param(enumDBFields.ta206_idsolicitudpreventa, rf.solicitud),
                    Param(enumDBFields.actuocomoadministrador, admin)
				};


                dr = cDblib.DataReader("SIC_SOLICITUDESPREVENTAMULTIPARAFICEPI_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.SolicCatSuper();
                    if (!Convert.IsDBNull(dr["ta206_idsolicitudpreventa"])) o.ta206_idsolicitudpreventa = Convert.ToInt32(dr["ta206_idsolicitudpreventa"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta206_estado"])) o.ta206_estado = Convert.ToString(dr["ta206_estado"]);
                    if (!Convert.IsDBNull(dr["ta206_itemorigen"])) o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_iditemorigen"])) o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_fechacreacion"])) o.ta206_fechacreacion = Convert.ToDateTime(dr["ta206_fechacreacion"]);
                    if (!Convert.IsDBNull(dr["numeroacciones"])) o.numeroacciones = Convert.ToInt32(dr["numeroacciones"]);
                    if (!Convert.IsDBNull(dr["accionesabiertas"])) o.accionesabiertas = Convert.ToInt32(dr["accionesabiertas"]);

                    if (!Convert.IsDBNull(dr["ta200_denominacion"])) o.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    //if (!Convert.IsDBNull(dr["ta201_denominacion"])) o.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    if (!Convert.IsDBNull(dr["promotor"])) o.promotor = Convert.ToString(dr["promotor"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["ta206_motivoanulacion"])) o.ta206_motivoanulacion = Convert.ToString(dr["ta206_motivoanulacion"]);

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
                case enumDBFields.ta206_idsolicitudpreventa:
                    paramName = "@ta206_idsolicitudpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta206_denominacion:
                    paramName = "@ta206_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.ta206_estado:
                    paramName = "@ta206_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.ta206_fechacreacion:
                    paramName = "@ta206_fechacreacion";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.t001_idficepi_promotor:
                    paramName = "@t001_idficepi_promotor";
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
                case enumDBFields.ta204_idaccionpreventa:
                    paramName = "@ta204_idaccionpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta201_idsubareapreventa:
                    paramName = "@ta201_idsubareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta200_idareapreventa:
                    paramName = "@ta200_idareapreventa ";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
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

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta204_estado:
                    paramName = "@ta204_estado";
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

                case enumDBFields.actuocomoadministrador:
                    paramName = "@actuocomoadministrador";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                case enumDBFields.ta206_motivoanulacion:
                    paramName = "@ta206_motivoanulacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 250;
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
