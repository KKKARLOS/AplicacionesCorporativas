using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionRecursos
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{    
    internal class AccionRecursos 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            nomRecurso = 2,
            T383_idaccion = 3,
            T389_notificar = 4,
            MAIL = 5,
            t001_sexo = 6,
            t303_idnodo = 7,
            baja = 8,
            tipo = 9
        }

        internal AccionRecursos(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AccionRecursos
        /// </summary>
        internal int Insert(Models.AccionRecursos oAccionRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAccionRecursos.t314_idusuario),
                    Param(enumDBFields.T383_idaccion, oAccionRecursos.T383_idaccion),
                    Param(enumDBFields.T389_notificar, oAccionRecursos.T389_notificar)
                };

                return (int)cDblib.ExecuteScalar("SUP_ACCIONRECURSOS_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un AccionRecursos a partir del id
        /// </summary>
        //internal Models.AccionRecursos Select()
        //{
        //    Models.AccionRecursos oAccionRecursos = null;
        //    IDataReader dr = null;

        //    try
        //    {


        //        dr = cDblib.DataReader("SUPER.IAP30_AccionRecursos_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAccionRecursos = new Models.AccionRecursos();
        //            oAccionRecursos.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
        //            if (!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAccionRecursos.nomRecurso = Convert.ToString(dr["nomRecurso"]);
        //            oAccionRecursos.T383_idaccion = Convert.ToInt32(dr["T383_idaccion"]);
        //            oAccionRecursos.T389_notificar = Convert.ToBoolean(dr["T389_notificar"]);
        //            oAccionRecursos.MAIL = Convert.ToString(dr["MAIL"]);
        //            oAccionRecursos.t001_sexo = Convert.ToString(dr["t001_sexo"]);
        //            if (!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oAccionRecursos.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
        //            oAccionRecursos.baja = Convert.ToInt32(dr["baja"]);
        //            if (!Convert.IsDBNull(dr["tipo"]))
        //                oAccionRecursos.tipo = Convert.ToString(dr["tipo"]);

        //        }
        //        return oAccionRecursos;

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
        /// Actualiza un AccionRecursos a partir del id
        /// </summary>
        internal int Update(Models.AccionRecursos oAccionRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAccionRecursos.t314_idusuario),
                    Param(enumDBFields.T383_idaccion, oAccionRecursos.T383_idaccion),
                    Param(enumDBFields.T389_notificar, oAccionRecursos.T389_notificar)
                };

                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_U", dbparams);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AccionRecursos a partir del id
        /// </summary>
        internal int Delete(Models.AccionRecursos oAccionRecursos)
        {
            try
            {

                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.T383_idaccion, oAccionRecursos.T383_idaccion),
                    Param(enumDBFields.t314_idusuario, oAccionRecursos.t314_idusuario)
                };

                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AccionRecursos
        /// </summary>
        internal List<Models.AccionRecursos> Catalogo(Models.AccionRecursos oAccionRecursosFilter)
        {
            Models.AccionRecursos oAccionRecursos = null;
            List<Models.AccionRecursos> lst = new List<Models.AccionRecursos>();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T383_idaccion, oAccionRecursosFilter.T383_idaccion)
                };

                dr = cDblib.DataReader("SUP_ACCIONRECURSOS_SByT383_idaccion", dbparams);
                while (dr.Read())
                {
                    oAccionRecursos = new Models.AccionRecursos();
                    oAccionRecursos.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["nomRecurso"]))
                        oAccionRecursos.nomRecurso = Convert.ToString(dr["nomRecurso"]);
                    oAccionRecursos.T383_idaccion = Convert.ToInt32(dr["T383_idaccion"]);
                    oAccionRecursos.T389_notificar = Convert.ToBoolean(dr["T389_notificar"]);
                    oAccionRecursos.mail = Convert.ToString(dr["MAIL"]);
                    oAccionRecursos.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oAccionRecursos.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oAccionRecursos.baja = Convert.ToInt32(dr["baja"]);
                    if (!Convert.IsDBNull(dr["tipo"]))
                        oAccionRecursos.tipo = Convert.ToString(dr["tipo"]);

                    lst.Add(oAccionRecursos);

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
                case enumDBFields.nomRecurso:
                    paramName = "@nomRecurso";
                    paramType = SqlDbType.VarChar;
                    paramSize = 150;
                    break;
                case enumDBFields.T383_idaccion:
                    paramName = "@T383_idaccion";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T389_notificar:
                    paramName = "@T389_notificar";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.MAIL:
                    paramName = "@MAIL";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.t001_sexo:
                    paramName = "@t001_sexo";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.t303_idnodo:
                    paramName = "@t303_idnodo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.baja:
                    paramName = "@baja";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.tipo:
                    paramName = "@tipo";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
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
