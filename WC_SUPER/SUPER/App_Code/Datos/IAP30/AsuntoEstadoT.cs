using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoEstadoT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoEstadoT 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            t600_idasunto = 1,
            t606_estado = 2,
            t314_idusuario = 3
        }

        internal AsuntoEstadoT(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion
	
        #region funciones publicas
        ///// <summary>
        ///// Inserta un AsuntoEstadoT
        ///// </summary>
        internal int Insert(Models.AsuntoEstadoT oAsuntoEstadoT)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t600_idasunto, oAsuntoEstadoT.t600_idasunto),
                    Param(enumDBFields.t606_estado, oAsuntoEstadoT.t606_estado),
                    Param(enumDBFields.t314_idusuario, oAsuntoEstadoT.t314_idusuario),
                };

                return (int)cDblib.Execute("SUP_ASUNTOESTADO_T_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AsuntoEstadoT a partir del id
        ///// </summary>
        //internal Models.AsuntoEstadoT Select()
        //{
        //    Models.AsuntoEstadoT oAsuntoEstadoT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AsuntoEstadoT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAsuntoEstadoT = new Models.AsuntoEstadoT();
        //            oAsuntoEstadoT.t600_idasunto=Convert.ToInt32(dr["t600_idasunto"]);
        //            oAsuntoEstadoT.t606_estado=Convert.ToByte(dr["t606_estado"]);
        //            if(!Convert.IsDBNull(dr["Estado"]))
        //                oAsuntoEstadoT.Estado=Convert.ToString(dr["Estado"]);
        //            oAsuntoEstadoT.t606_fecha=Convert.ToDateTime(dr["t606_fecha"]);
        //            oAsuntoEstadoT.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAsuntoEstadoT.nomRecurso=Convert.ToString(dr["nomRecurso"]);

        //        }
        //        return oAsuntoEstadoT;
				
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
        ///// Actualiza un AsuntoEstadoT a partir del id
        ///// </summary>
        //internal int Update(Models.AsuntoEstadoT oAsuntoEstadoT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[6] {
        //            Param(enumDBFields.t600_idasunto, oAsuntoEstadoT.t600_idasunto),
        //            Param(enumDBFields.t606_estado, oAsuntoEstadoT.t606_estado),
        //            Param(enumDBFields.Estado, oAsuntoEstadoT.Estado),
        //            Param(enumDBFields.t606_fecha, oAsuntoEstadoT.t606_fecha),
        //            Param(enumDBFields.t314_idusuario, oAsuntoEstadoT.t314_idusuario),
        //            Param(enumDBFields.nomRecurso, oAsuntoEstadoT.nomRecurso)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_AsuntoEstadoT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un AsuntoEstadoT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_AsuntoEstadoT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los AsuntoEstadoT
        ///// </summary>
        internal List<Models.AsuntoEstadoT> Catalogo(int t600_idasunto)
        {
            Models.AsuntoEstadoT oAsuntoEstadoT = null;
            List<Models.AsuntoEstadoT> lst = new List<Models.AsuntoEstadoT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t600_idasunto, t600_idasunto),
                };

                dr = cDblib.DataReader("SUP_ASUNTOESTADO_T_SByt600_idasunto", dbparams);
                while (dr.Read())
                {
                    oAsuntoEstadoT = new Models.AsuntoEstadoT();
                    oAsuntoEstadoT.t600_idasunto=Convert.ToInt32(dr["t600_idasunto"]);
                    oAsuntoEstadoT.t606_estado=Convert.ToByte(dr["t606_estado"]);
                    if(!Convert.IsDBNull(dr["Estado"]))
                        oAsuntoEstadoT.Estado=Convert.ToString(dr["Estado"]);
                    oAsuntoEstadoT.t606_fecha=Convert.ToDateTime(dr["t606_fecha"]);
                    oAsuntoEstadoT.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
                    if(!Convert.IsDBNull(dr["nomRecurso"]))
                        oAsuntoEstadoT.nomRecurso=Convert.ToString(dr["nomRecurso"]);

                    lst.Add(oAsuntoEstadoT);

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
                case enumDBFields.t600_idasunto:
                    paramName = "@t600_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t606_estado:
                    paramName = "@t606_codestado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                //case enumDBFields.Estado:
                //    paramName = "@Estado";
                //    paramType = SqlDbType.VarChar;
                //    paramSize = 10;
                //    break;
                //case enumDBFields.t606_fecha:
                //    paramName = "@t606_fecha";
                //    paramType = SqlDbType.DateTime;
                //    paramSize = 8;
                //    break;
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                //case enumDBFields.nomRecurso:
                //    paramName = "@nomRecurso";
                //    paramType = SqlDbType.VarChar;
                //    paramSize = 73;
                //    break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion
    
    }

}
