using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoRecursosPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoRecursosPT 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            nomRecurso = 2,
            T409_idasunto = 3,
            t413_notificar = 4,
            MAIL = 5,
            t001_sexo = 6,
            t303_idnodo = 7,
            baja = 8,
            tipo = 9
        }

        internal AsuntoRecursosPT(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un AsuntoRecursosPT
        /// </summary>
        /// 

        internal int Insert(Models.AsuntoRecursosPT oAsuntoRecursosPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursosPT.t314_idusuario),
                    Param(enumDBFields.T409_idasunto, oAsuntoRecursosPT.T409_idasunto),
                    Param(enumDBFields.t413_notificar, oAsuntoRecursosPT.t413_notificar)
                };

                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_PT_I_SNE", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AsuntoRecursosPT a partir del id
        ///// </summary>
        //internal Models.AsuntoRecursosPT Select()
        //{
        //    Models.AsuntoRecursosPT oAsuntoRecursosPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AsuntoRecursosPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAsuntoRecursosPT = new Models.AsuntoRecursosPT();
        //            oAsuntoRecursosPT.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAsuntoRecursosPT.nomRecurso=Convert.ToString(dr["nomRecurso"]);
        //            oAsuntoRecursosPT.T409_idasunto=Convert.ToInt32(dr["T409_idasunto"]);
        //            oAsuntoRecursosPT.t413_notificar=Convert.ToBoolean(dr["t413_notificar"]);
        //            oAsuntoRecursosPT.MAIL=Convert.ToString(dr["MAIL"]);
        //            oAsuntoRecursosPT.t001_sexo=Convert.ToString(dr["t001_sexo"]);
        //            if(!Convert.IsDBNull(dr["t303_idnodo"]))
        //                oAsuntoRecursosPT.t303_idnodo=Convert.ToInt32(dr["t303_idnodo"]);
        //            oAsuntoRecursosPT.baja=Convert.ToInt32(dr["baja"]);
        //            if(!Convert.IsDBNull(dr["tipo"]))
        //                oAsuntoRecursosPT.tipo=Convert.ToString(dr["tipo"]);

        //        }
        //        return oAsuntoRecursosPT;
				
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
        /// Actualiza un AsuntoRecursosPT a partir del id
        /// </summary>
        internal int Update(Models.AsuntoRecursosPT oAsuntoRecursosPT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursosPT.t314_idusuario),
                    Param(enumDBFields.T409_idasunto, oAsuntoRecursosPT.T409_idasunto),
                    Param(enumDBFields.t413_notificar, oAsuntoRecursosPT.t413_notificar)
                };

                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_PT_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un AsuntoRecursosPT a partir del id
        /// </summary>
        internal int Delete(Models.AsuntoRecursosPT oAsuntoRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.T409_idasunto, oAsuntoRecursos.T409_idasunto),
                    Param(enumDBFields.t314_idusuario, oAsuntoRecursos.t314_idusuario)
                };

                return (int)cDblib.Execute("SUP_ASUNTORECURSOS_PT_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los AsuntoRecursosPT
        /// </summary>
        internal List<Models.AsuntoRecursosPT> Catalogo(Models.AsuntoRecursosPT oAsuntoRecursosPTFilter)
        {
            Models.AsuntoRecursosPT oAsuntoRecursosPT = null;
            List<Models.AsuntoRecursosPT> lst = new List<Models.AsuntoRecursosPT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T409_idasunto, oAsuntoRecursosPTFilter.T409_idasunto)
                };

                dr = cDblib.DataReader("SUP_ASUNTORECURSOS_PT_SByT409_idasunto", dbparams);
                while (dr.Read())
                {
                    oAsuntoRecursosPT = new Models.AsuntoRecursosPT();
                    oAsuntoRecursosPT.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["nomRecurso"]))
                        oAsuntoRecursosPT.nomRecurso = Convert.ToString(dr["nomRecurso"]);
                    oAsuntoRecursosPT.T409_idasunto = Convert.ToInt32(dr["T409_idasunto"]);
                    oAsuntoRecursosPT.t413_notificar = Convert.ToBoolean(dr["t413_notificar"]);
                    oAsuntoRecursosPT.MAIL = Convert.ToString(dr["MAIL"]);
                    oAsuntoRecursosPT.t001_sexo = Convert.ToString(dr["t001_sexo"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oAsuntoRecursosPT.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    oAsuntoRecursosPT.baja = Convert.ToInt32(dr["baja"]);
                    if (!Convert.IsDBNull(dr["tipo"]))
                        oAsuntoRecursosPT.tipo = Convert.ToString(dr["tipo"]);

                    lst.Add(oAsuntoRecursosPT);

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
                case enumDBFields.T409_idasunto:
                    paramName = "@T409_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t413_notificar:
                    paramName = "@t413_notificar";
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
