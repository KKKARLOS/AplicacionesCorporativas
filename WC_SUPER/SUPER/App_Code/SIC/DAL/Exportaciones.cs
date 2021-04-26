using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for AccionPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class Exportaciones
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t001_idficepi = 1,
            ta204_estado = 2,
            t001_idficepi_promotor = 3,
            ta206_iditemorigen = 4,
            ta206_itemorigen = 5,
            ta204_fechafinestipulada_ini = 6,
            ta204_fechafinestipulada_fin = 7,
            importe_desde = 8,
            importe_hasta = 9,
            t001_idficepi_comercial = 10,
            TABUNIDAD = 11,
            TABAREA = 12,
            TABSUBAREA = 13,
            TABTIPOACCION = 14,
            TABLIDER = 15,
            TABCUENTA = 16,
            actuocomoadministrador = 17,
            ta207_estado = 18,
            ta207_fechafinprevista_ini = 19,
            ta207_fechafinprevista_fin = 20

        }

        internal Exportaciones(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas

        public DataTable Acciones(bool admin, int t001_idficepi, ExportAccionesFilter rf)
        {
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

                return cDblib.DataTable("SIC_EXPORTACION_ACCIONESPREVENTAMULTIPARAFICEPI_CAT", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable AccionesMiVision(bool admin, int t001_idficepi, ExportAccionesFilter rf)
        {
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

                return cDblib.DataTable("SIC_ACCIONESPREVENTAMULTIPARAFICEPI_CAT", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable Tareas(bool admin, int t001_idficepi, ExportTareasFilter rf)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
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
                    Param(enumDBFields.actuocomoadministrador, admin),
                    Param(enumDBFields.ta207_estado, rf.estado_tarea),
                    Param(enumDBFields.ta207_fechafinprevista_ini, rf.ffinDesde_tarea),
                    Param(enumDBFields.ta207_fechafinprevista_fin, rf.ffinHasta_tarea)

				};

                return cDblib.DataTable("SIC_EXPORTACION_TAREASPREVENTAMULTIPARAFICEPI_CAT", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable AccionesTareas(bool admin, int t001_idficepi, ExportTareasFilter rf)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[20] {
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
                    Param(enumDBFields.actuocomoadministrador, admin),
                    Param(enumDBFields.ta207_estado, rf.estado_tarea),
                    Param(enumDBFields.ta207_fechafinprevista_ini, rf.ffinDesde_tarea),
                    Param(enumDBFields.ta207_fechafinprevista_fin, rf.ffinHasta_tarea)

                }; 

                return cDblib.DataTable("SIC_EXPORTACION_ACCIONESYTAREASPREVENTAMULTIPARAFICEPI_CAT", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable CargaTrabajo(bool admin, int t001_idficepi, ExportCargaTrabajoFilter rf)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[12] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.TABUNIDAD,  Shared.Database.ArrayToDataTable(rf.unidades, "numero")),
                    Param(enumDBFields.TABAREA, Shared.Database.ArrayToDataTable(rf.areas, "numero")),
                    Param(enumDBFields.TABSUBAREA, Shared.Database.ArrayToDataTable(rf.subareas, "numero")),
                    Param(enumDBFields.TABLIDER, Shared.Database.ArrayToDataTable(rf.lideres, "numero")),
                    Param(enumDBFields.ta204_estado, rf.estado),
                    Param(enumDBFields.ta204_fechafinestipulada_ini, rf.ffinDesde),
                    Param(enumDBFields.ta204_fechafinestipulada_fin, rf.ffinHasta),
                    Param(enumDBFields.actuocomoadministrador, admin),
                    Param(enumDBFields.ta207_estado, rf.estado_tarea),
                    Param(enumDBFields.ta207_fechafinprevista_ini, rf.ffinDesde_tarea),
                    Param(enumDBFields.ta207_fechafinprevista_fin, rf.ffinHasta_tarea)

				};

                return cDblib.DataTable("SIC_EXPORTACION_CARGATRABAJOMULTIPARAFICEPI_CAT", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
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
                case enumDBFields.ta204_estado:
                    paramName = "@ta204_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
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

                case enumDBFields.ta207_estado:
                    paramName = "@ta207_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.ta207_fechafinprevista_ini:
                    paramName = "@ta207_fechafinprevista_ini";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.ta207_fechafinprevista_fin:
                    paramName = "@ta207_fechafinprevista_fin";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
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
