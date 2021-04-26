using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for ParticipanteTareaPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class ParticipanteTareaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta207_idtareapreventa = 1,
            t001_idficepi_participante = 2,
            ta214_estado = 3,

            t001_idficepi = 4,                                   
            TABUNIDAD = 5,
            TABAREA = 6,
            TABSUBAREA = 7,            
            TABTIPOACCION = 8,
            TABLIDER = 9,
            t001_idficepi_promotor = 10,
            ta207_estado = 11,
            ta206_iditemorigen = 12,
            ta206_itemorigen = 13,
            ta207_fechafinestipulada_ini = 14,
            ta207_fechafinestipulada_fin = 15,
            t001_idficepi_comercial = 16,
            TABCUENTA = 17
            
        }

        internal ParticipanteTareaPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un ParticipanteTareaPreventa
        /// </summary>
        internal int Insert(Models.ParticipanteTareaPreventa oParticipanteTareaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta207_idtareapreventa, oParticipanteTareaPreventa.ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_participante, oParticipanteTareaPreventa.t001_idficepi_participante),
					Param(enumDBFields.ta214_estado, oParticipanteTareaPreventa.ta214_estado)
				};

                return (int)cDblib.Execute("SUPER.SIC_ParticipanteTareaPreventa_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un ParticipanteTareaPreventa a partir del id
        /// </summary>
        internal Models.ParticipanteTareaPreventa Select(Int32 ta207_idtareapreventa, Int32 t001_idficepi_participante)
        {
            Models.ParticipanteTareaPreventa oParticipanteTareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_participante, t001_idficepi_participante)
				};

                dr = cDblib.DataReader("SUPER.SIC_ParticipanteTareaPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oParticipanteTareaPreventa = new Models.ParticipanteTareaPreventa();
                    oParticipanteTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oParticipanteTareaPreventa.t001_idficepi_participante = Convert.ToInt32(dr["t001_idficepi_participante"]);
                    oParticipanteTareaPreventa.ta214_estado = Convert.ToString(dr["ta209_estado"]);

                }
                return oParticipanteTareaPreventa;

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
        /// Actualiza un ParticipanteTareaPreventa a partir del id
        /// </summary>
        internal int Update(Models.ParticipanteTareaPreventa oParticipanteTareaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.ta207_idtareapreventa, oParticipanteTareaPreventa.ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_participante, oParticipanteTareaPreventa.t001_idficepi_participante),
					Param(enumDBFields.ta214_estado, oParticipanteTareaPreventa.ta214_estado)
				};

                return (int)cDblib.Execute("SIC_PARTICIPANTETAREAPREVENTA_ESTADO_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un ParticipanteTareaPreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta207_idtareapreventa, Int32 t001_idficepi_participante)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_participante, t001_idficepi_participante)
				};

                return (int)cDblib.Execute("SUPER.SIC_ParticipanteTareaPreventa_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los ParticipanteTareaPreventa
        /// </summary>
        internal List<Models.ParticipanteTareaPreventa> Catalogo(Models.ParticipanteTareaPreventa oParticipanteTareaPreventaFilter)
        {
            Models.ParticipanteTareaPreventa oParticipanteTareaPreventa = null;
            List<Models.ParticipanteTareaPreventa> lst = new List<Models.ParticipanteTareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta214_estado, oParticipanteTareaPreventaFilter.ta214_estado)
				};

                dr = cDblib.DataReader("SUPER.SIC_ParticipanteTareaPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oParticipanteTareaPreventa = new Models.ParticipanteTareaPreventa();
                    oParticipanteTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oParticipanteTareaPreventa.t001_idficepi_participante = Convert.ToInt32(dr["t001_idficepi_participante"]);
                    oParticipanteTareaPreventa.ta214_estado = Convert.ToString(dr["ta209_estado"]);

                    lst.Add(oParticipanteTareaPreventa);

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

        public List<Models.TareaCatRequestFilter> CatParticipanteTareaPreventa(int t001_idficepi, Models.TareaCatHistoricoFilter rf)
        {

            Models.TareaCatRequestFilter o = null;
            List<Models.TareaCatRequestFilter> lst = new List<Models.TareaCatRequestFilter>();
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
                    Param(enumDBFields.ta207_estado, rf.estado),
                    Param(enumDBFields.ta214_estado, rf.estadoParticipacion),
                    Param(enumDBFields.ta206_iditemorigen, rf.iditemorigen),
                    Param(enumDBFields.ta206_itemorigen, rf.itemorigen),
                    Param(enumDBFields.ta207_fechafinestipulada_ini, rf.ffinDesde),
                    Param(enumDBFields.ta207_fechafinestipulada_fin, rf.ffinHasta),                    
                    Param(enumDBFields.t001_idficepi_comercial, rf.comercial),
                    Param(enumDBFields.TABCUENTA, Shared.Database.ArrayToDataTable(rf.clientes, "cadena")),
                    
				};


                dr = cDblib.DataReader("SIC_HISTAREASPARTICIPANTE_CAT", dbparams);
                while (dr.Read())
                {
                    o = new Models.TareaCatRequestFilter();
                    if (!Convert.IsDBNull(dr["ta204_idaccionpreventa"])) o.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta207_idtareapreventa"])) o.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    if (!Convert.IsDBNull(dr["ta207_fechafinprevista"])) o.ta207_fechafinestipulada = Convert.ToDateTime(dr["ta207_fechafinprevista"]);
                    if (!Convert.IsDBNull(dr["ta207_fechacreacion"])) o.ta207_fechacreacion = Convert.ToDateTime(dr["ta207_fechacreacion"]);
                    if (!Convert.IsDBNull(dr["ta207_fechafinreal"])) o.ta207_fechafinreal = Convert.ToDateTime(dr["ta207_fechafinreal"]);
                    if (!Convert.IsDBNull(dr["ta207_estado"])) o.ta207_estado = Convert.ToString(dr["ta207_estado"]);
                    if (!Convert.IsDBNull(dr["ta207_denominacion"])) o.ta207_denominacion = Convert.ToString(dr["ta207_denominacion"]);
                    if (!Convert.IsDBNull(dr["lider"])) o.lider = Convert.ToString(dr["lider"]);
                    if (!Convert.IsDBNull(dr["ta205_denominacion"])) o.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta201_denominacion"])) o.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta200_denominacion"])) o.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta199_denominacion"])) o.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta206_iditemorigen"])) o.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    if (!Convert.IsDBNull(dr["ta206_itemorigen"])) o.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    if (!Convert.IsDBNull(dr["den_item"])) o.den_item = Convert.ToString(dr["den_item"]);
                    if (!Convert.IsDBNull(dr["ta206_denominacion"])) o.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    if (!Convert.IsDBNull(dr["den_cuenta"])) o.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    if (!Convert.IsDBNull(dr["ta208_negrita"])) o.ta208_negrita = Convert.ToBoolean(dr["ta208_negrita"]);
                    if (!Convert.IsDBNull(dr["promotor"])) o.promotor = Convert.ToString(dr["promotor"]);

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
                case enumDBFields.ta207_idtareapreventa:
                    paramName = "@ta207_idtareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_participante:
                    paramName = "@t001_idficepi_participante";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta214_estado:
                    paramName = "@ta214_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
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
                
                case enumDBFields.t001_idficepi_promotor:
                    paramName = "@t001_idficepi_promotor";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
            
                case enumDBFields.ta207_estado:
                    paramName = "@ta207_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
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

                case enumDBFields.ta207_fechafinestipulada_ini:
                    paramName = "@ta207_fechafinprevista_ini";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;

                case enumDBFields.ta207_fechafinestipulada_fin:
                    paramName = "@ta207_fechafinprevista_fin";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
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
          
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion

    }

}
