using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoEstado
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoEstado 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            T382_idasunto = 1,
            T385_codestado = 2,
            T385_idautor = 3
        }

        internal AsuntoEstado(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
        #endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un AsuntoEstado
        ///// </summary>
        internal int Insert(Models.AsuntoEstado oAsuntoEstado)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.T382_idasunto, oAsuntoEstado.T382_idasunto),
                    Param(enumDBFields.T385_codestado, oAsuntoEstado.T385_codestado),
                    Param(enumDBFields.T385_idautor, oAsuntoEstado.T385_idautor)
                };

                return (int)cDblib.Execute("SUP_ASUNTOESTADO_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AsuntoEstado a partir del id
        ///// </summary>
        //internal Models.AsuntoEstado Select()
        //{
        //    Models.AsuntoEstado oAsuntoEstado = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AsuntoEstado_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAsuntoEstado = new Models.AsuntoEstado();
        //            oAsuntoEstado.T382_idasunto=Convert.ToInt32(dr["T382_idasunto"]);
        //            oAsuntoEstado.T385_codestado=Convert.ToByte(dr["T385_codestado"]);
        //            if(!Convert.IsDBNull(dr["Estado"]))
        //                oAsuntoEstado.Estado=Convert.ToString(dr["Estado"]);
        //            oAsuntoEstado.T385_fecha=Convert.ToDateTime(dr["T385_fecha"]);
        //            oAsuntoEstado.T385_idautor=Convert.ToInt32(dr["T385_idautor"]);
        //            if(!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAsuntoEstado.nomRecurso=Convert.ToString(dr["nomRecurso"]);

        //        }
        //        return oAsuntoEstado;
				
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
		
        ///// <summary>
        ///// Actualiza un AsuntoEstado a partir del id
        ///// </summary>
        //internal int Update(Models.AsuntoEstado oAsuntoEstado)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[6] {
        //            Param(enumDBFields.T382_idasunto, oAsuntoEstado.T382_idasunto),
        //            Param(enumDBFields.T385_codestado, oAsuntoEstado.T385_codestado),
        //            Param(enumDBFields.Estado, oAsuntoEstado.Estado),
        //            Param(enumDBFields.T385_fecha, oAsuntoEstado.T385_fecha),
        //            Param(enumDBFields.T385_idautor, oAsuntoEstado.T385_idautor),
        //            Param(enumDBFields.nomRecurso, oAsuntoEstado.nomRecurso)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_AsuntoEstado_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un AsuntoEstado a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_AsuntoEstado_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los AsuntoEstado
        ///// </summary>
        internal List<Models.AsuntoEstado> Catalogo(int t382_idasunto)
        {
            Models.AsuntoEstado oAsuntoEstado = null;
            List<Models.AsuntoEstado> lst = new List<Models.AsuntoEstado>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.T382_idasunto, t382_idasunto),
                };

                dr = cDblib.DataReader("SUP_ASUNTOESTADO_SByt382_idasunto", dbparams);
                while (dr.Read())
                {
                    oAsuntoEstado = new Models.AsuntoEstado();
                    oAsuntoEstado.T382_idasunto = Convert.ToInt32(dr["T382_idasunto"]);
                    oAsuntoEstado.T385_codestado = Convert.ToByte(dr["T385_codestado"]);
                    if (!Convert.IsDBNull(dr["Estado"]))
                        oAsuntoEstado.Estado = Convert.ToString(dr["Estado"]);
                    oAsuntoEstado.T385_fecha = Convert.ToDateTime(dr["T385_fecha"]);
                    oAsuntoEstado.T385_idautor = Convert.ToInt32(dr["T385_idautor"]);
                    if (!Convert.IsDBNull(dr["nomRecurso"]))
                        oAsuntoEstado.nomRecurso = Convert.ToString(dr["nomRecurso"]);

                    lst.Add(oAsuntoEstado);
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
		
        //#endregion
		
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
                case enumDBFields.T382_idasunto:
                    paramName = "@T382_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.T385_codestado:
                    paramName = "@T385_codestado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
        //        case enumDBFields.Estado:
        //            paramName = "@Estado";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 10;
        //            break;
        //        case enumDBFields.T385_fecha:
        //            paramName = "@T385_fecha";
        //            paramType = SqlDbType.DateTime;
        //            paramSize = 8;
        //            break;
                case enumDBFields.T385_idautor:
                    paramName = "@T385_idautor";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
        //        case enumDBFields.nomRecurso:
        //            paramName = "@nomRecurso";
        //            paramType = SqlDbType.VarChar;
        //            paramSize = 73;
        //            break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion
    
    }

}
