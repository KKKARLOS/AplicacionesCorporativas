using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionRecursosT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AccionRecursosT 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            MAIL = 2,
            nomRecurso = 3,
            t601_idaccion = 4,
            t605_notificar = 5,
            t001_sexo = 6,
            t303_idnodo = 7,
            baja = 8,
            tipo = 9
        }

        internal AccionRecursosT(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AccionRecursosT
        /// </summary>
        internal int Insert(Models.AccionRecursosT oAccionRecursosT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAccionRecursosT.t314_idusuario),
                    Param(enumDBFields.t601_idaccion, oAccionRecursosT.t601_idaccion),
                    Param(enumDBFields.t605_notificar, oAccionRecursosT.t605_notificar)
                };

                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_T_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un AccionRecursosT a partir del id
        /// </summary>
        //internal Models.AccionRecursosT Select()
        //{
        //    Models.AccionRecursosT oAccionRecursosT = null;
        //    IDataReader dr = null;

        //    try
        //    {


        //        dr = cDblib.DataReader("SUPER.IAP30_AccionRecursosT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAccionRecursosT = new Models.AccionRecursosT();
        //            oAccionRecursosT.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
        //            oAccionRecursosT.MAIL = Convert.ToString(dr["MAIL"]);
        //            if (!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAccionRecursosT.nomRecurso = Convert.ToString(dr["nomRecurso"]);
        //            oAccionRecursosT.t601_idaccion = Convert.ToInt32(dr["t601_idaccion"]);
        //            oAccionRecursosT.t605_notificar = Convert.ToBoolean(dr["t605_notificar"]);
        //            oAccionRecursosT.t001_sexo = Convert.ToString(dr["t001_sexo"]);
        //            if (!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oAccionRecursosT.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
        //            oAccionRecursosT.baja = Convert.ToInt32(dr["baja"]);
        //            if (!Convert.IsDBNull(dr["tipo"]))
        //                oAccionRecursosT.tipo = Convert.ToString(dr["tipo"]);

        //        }
        //        return oAccionRecursosT;

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
        /// Actualiza un AccionRecursosT a partir del id
        /// </summary>
        internal int Update(Models.AccionRecursosT oAccionRecursosT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAccionRecursosT.t314_idusuario),
                    Param(enumDBFields.t601_idaccion, oAccionRecursosT.t601_idaccion),
                    Param(enumDBFields.t605_notificar, oAccionRecursosT.t605_notificar)
                };
                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_T_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AccionRecursosT a partir del id
        /// </summary>
        internal int Delete(Models.AccionRecursosT oAccionRecursosT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t601_idaccion, oAccionRecursosT.t601_idaccion),
                    Param(enumDBFields.t314_idusuario, oAccionRecursosT.t314_idusuario)
                };
                return (int)cDblib.Execute("SUP_ACCIONRECURSOS_T_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AccionRecursosT
        /// </summary>
        internal List<Models.AccionRecursosT> Catalogo(Models.AccionRecursosT oAccionRecursosTFilter)
        {
            Models.AccionRecursosT oAccionRecursosT = null;
            List<Models.AccionRecursosT> lst = new List<Models.AccionRecursosT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t601_idaccion, oAccionRecursosTFilter.t601_idaccion)
                };

                dr = cDblib.DataReader("SUP_ACCIONRECURSOS_T_SByT601_idaccion", dbparams);
                while (dr.Read())
                {
                    oAccionRecursosT = new Models.AccionRecursosT();
                    oAccionRecursosT.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    oAccionRecursosT.MAIL = Convert.ToString(dr["MAIL"]);
                    if (!Convert.IsDBNull(dr["nomRecurso"]))
                        oAccionRecursosT.nomRecurso = Convert.ToString(dr["nomRecurso"]);
                    oAccionRecursosT.t601_idaccion = Convert.ToInt32(dr["t601_idaccion"]);
                    oAccionRecursosT.t605_notificar = Convert.ToBoolean(dr["t605_notificar"]);
                    oAccionRecursosT.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oAccionRecursosT.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oAccionRecursosT.baja = Convert.ToInt32(dr["baja"]);
                    if (!Convert.IsDBNull(dr["tipo"]))
                        oAccionRecursosT.tipo = Convert.ToString(dr["tipo"]);

                    lst.Add(oAccionRecursosT);

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
                case enumDBFields.MAIL:
                    paramName = "@MAIL";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.nomRecurso:
                    paramName = "@nomRecurso";
                    paramType = SqlDbType.VarChar;
                    paramSize = 150;
                    break;
                case enumDBFields.t601_idaccion:
                    paramName = "@t601_idaccion";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t605_notificar:
                    paramName = "@t605_notificar";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
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
