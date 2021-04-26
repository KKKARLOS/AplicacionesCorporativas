using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoRecursosT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoRecursosT 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            nomRecurso = 2,
            T600_idasunto = 3,
            t604_notificar = 4,
            MAIL = 5,
            t001_sexo = 6,
            t303_idnodo = 7,
            baja = 8,
            tipo = 9
        }

        internal AsuntoRecursosT(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AsuntoRecursosT
        /// </summary>
        internal int Insert(Models.AsuntoRecursosT oAsuntoRecursosT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursosT.t314_idusuario),
                    Param(enumDBFields.T600_idasunto, oAsuntoRecursosT.T600_idasunto),
                    Param(enumDBFields.t604_notificar, oAsuntoRecursosT.t604_notificar)
                };

                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_T_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AsuntoRecursosT a partir del id
        ///// </summary>
        //internal Models.AsuntoRecursosT Select()
        //{
        //    Models.AsuntoRecursosT oAsuntoRecursosT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AsuntoRecursosT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAsuntoRecursosT = new Models.AsuntoRecursosT();
        //            oAsuntoRecursosT.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAsuntoRecursosT.nomRecurso=Convert.ToString(dr["nomRecurso"]);
        //            oAsuntoRecursosT.T600_idasunto=Convert.ToInt32(dr["T600_idasunto"]);
        //            oAsuntoRecursosT.t604_notificar=Convert.ToBoolean(dr["t604_notificar"]);
        //            oAsuntoRecursosT.MAIL=Convert.ToString(dr["MAIL"]);
        //            oAsuntoRecursosT.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oAsuntoRecursosT.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oAsuntoRecursosT.baja=Convert.ToInt32(dr["baja"]);
        //            if(!Convert.IsDBNull(dr["tipo"]))
        //                oAsuntoRecursosT.tipo=Convert.ToString(dr["tipo"]);

        //        }
        //        return oAsuntoRecursosT;
				
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
        /// Actualiza un AsuntoRecursosT a partir del id
        /// </summary>
        internal int Update(Models.AsuntoRecursosT oAsuntoRecursosT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursosT.t314_idusuario),
                    Param(enumDBFields.T600_idasunto, oAsuntoRecursosT.T600_idasunto),
                    Param(enumDBFields.t604_notificar, oAsuntoRecursosT.t604_notificar)
                };

                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_T_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AsuntoRecursosT a partir del id
        /// </summary>
        internal int Delete(Models.AsuntoRecursosT oAsuntoRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.T600_idasunto, oAsuntoRecursos.T600_idasunto),
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursos.t314_idusuario)
                };

                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_T_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AsuntoRecursosT
        /// </summary>
        internal List<Models.AsuntoRecursosT> Catalogo(Models.AsuntoRecursosT oAsuntoRecursosTFilter)
        {
            Models.AsuntoRecursosT oAsuntoRecursosT = null;
            List<Models.AsuntoRecursosT> lst = new List<Models.AsuntoRecursosT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T600_idasunto, oAsuntoRecursosTFilter.T600_idasunto)
                };

                dr = cDblib.DataReader("SUP_ASUNTORECURSOS_T_SByT600_idasunto", dbparams);
                while (dr.Read())
                {
                    oAsuntoRecursosT = new Models.AsuntoRecursosT();
                    oAsuntoRecursosT.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["nomRecurso"]))
                        oAsuntoRecursosT.nomRecurso = Convert.ToString(dr["nomRecurso"]);
                    oAsuntoRecursosT.T600_idasunto = Convert.ToInt32(dr["T600_idasunto"]);
                    oAsuntoRecursosT.t604_notificar = Convert.ToBoolean(dr["t604_notificar"]);
                    oAsuntoRecursosT.MAIL = Convert.ToString(dr["MAIL"]);
                    oAsuntoRecursosT.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oAsuntoRecursosT.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oAsuntoRecursosT.baja = Convert.ToInt32(dr["baja"]);
                    if (!Convert.IsDBNull(dr["tipo"]))
                        oAsuntoRecursosT.tipo = Convert.ToString(dr["tipo"]);

                    lst.Add(oAsuntoRecursosT);

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
                case enumDBFields.T600_idasunto:
                    paramName = "@T600_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t604_notificar:
                    paramName = "@t604_notificar";
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
