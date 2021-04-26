using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoRecursos
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoRecursos 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            nomRecurso = 2,
            T382_idasunto = 3,
            T388_notificar = 4,
            mail = 5,
            t001_sexo = 6,
            t303_idnodo = 7,
            baja = 8,
            tipo = 9
        }

        internal AsuntoRecursos(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AsuntoRecursos
        /// </summary>
        internal int Insert(Models.AsuntoRecursos oAsuntoRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursos.t314_idusuario),
                    Param(enumDBFields.T382_idasunto, oAsuntoRecursos.T382_idasunto),
                    Param(enumDBFields.T388_notificar, oAsuntoRecursos.T388_notificar)
                };

                return (int)cDblib.ExecuteScalar("SUP_ASUNTORECURSOS_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }		
        ///// <summary>
        ///// Obtiene un AsuntoRecursos a partir del id
        ///// </summary>
        //internal Models.AsuntoRecursos Select()
        //{
        //    Models.AsuntoRecursos oAsuntoRecursos = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AsuntoRecursos_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAsuntoRecursos = new Models.AsuntoRecursos();
        //            oAsuntoRecursos.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAsuntoRecursos.nomRecurso=Convert.ToString(dr["nomRecurso"]);
        //            oAsuntoRecursos.T382_idasunto=Convert.ToInt32(dr["T382_idasunto"]);
        //            oAsuntoRecursos.T388_notificar=Convert.ToBoolean(dr["T388_notificar"]);
        //            oAsuntoRecursos.mail=Convert.ToString(dr["mail"]);
        //            oAsuntoRecursos.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oAsuntoRecursos.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oAsuntoRecursos.baja=Convert.ToInt32(dr["baja"]);
        //            if(!Convert.IsDBNull(dr["tipo"]))
        //                oAsuntoRecursos.tipo=Convert.ToString(dr["tipo"]);

        //        }
        //        return oAsuntoRecursos;
				
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
        /// Actualiza un AsuntoRecursos a partir del id
        /// </summary>
        internal int Update(Models.AsuntoRecursos oAsuntoRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursos.t314_idusuario),
                    Param(enumDBFields.T382_idasunto, oAsuntoRecursos.T382_idasunto),
                    Param(enumDBFields.T388_notificar, oAsuntoRecursos.T388_notificar)
                };

                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Elimina un AsuntoRecursos a partir del id
        /// </summary>
        internal int Delete(Models.AsuntoRecursos oAsuntoRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.T382_idasunto, oAsuntoRecursos.T382_idasunto),
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursos.t314_idusuario)
                };
                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        ///// <summary>
        ///// Obtiene todos los AsuntoRecursos
        ///// </summary>
        internal List<Models.AsuntoRecursos> Catalogo(Models.AsuntoRecursos oAsuntoRecursosFilter)
        {
            Models.AsuntoRecursos oAsuntoRecursos = null;
            List<Models.AsuntoRecursos> lst = new List<Models.AsuntoRecursos>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T382_idasunto, oAsuntoRecursosFilter.T382_idasunto)
                };

                dr = cDblib.DataReader("SUP_ASUNTORECURSOS_SByt382_idasunto", dbparams);
                while (dr.Read())
                {
                    oAsuntoRecursos = new Models.AsuntoRecursos();
                    oAsuntoRecursos.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["nomRecurso"]))
                        oAsuntoRecursos.nomRecurso = Convert.ToString(dr["nomRecurso"]);
                    oAsuntoRecursos.T382_idasunto = Convert.ToInt32(dr["T382_idasunto"]);
                    oAsuntoRecursos.T388_notificar = Convert.ToBoolean(dr["T388_notificar"]);
                    oAsuntoRecursos.mail = Convert.ToString(dr["mail"]);
                    oAsuntoRecursos.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oAsuntoRecursos.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oAsuntoRecursos.baja = Convert.ToInt32(dr["baja"]);
                    if (!Convert.IsDBNull(dr["tipo"]))
                        oAsuntoRecursos.tipo = Convert.ToString(dr["tipo"]);

                    lst.Add(oAsuntoRecursos);
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
                case enumDBFields.T382_idasunto:
                    paramName = "@T382_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T388_notificar:
                    paramName = "@T388_notificar";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.mail:
                    paramName = "@mail";
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
