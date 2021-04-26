using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoEstadoPT
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AsuntoEstadoPT 
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t409_idasunto = 1,
            t416_estado = 2,
            t314_idusuario = 3
        }

        internal AsuntoEstadoPT(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
        #endregion
	
        #region funciones publicas
        ///// <summary>
        ///// Inserta un AsuntoEstadoPT
        ///// </summary>
        internal int Insert(Models.AsuntoEstadoPT oAsuntoEstado)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t409_idasunto, oAsuntoEstado.t409_idasunto),
                    Param(enumDBFields.t416_estado, oAsuntoEstado.t416_estado),
                    Param(enumDBFields.t314_idusuario, oAsuntoEstado.t314_idusuario)
                };

                return (int)cDblib.Execute("SUP_ASUNTOESTADO_PT_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
        ///// <summary>
        ///// Obtiene un AsuntoEstadoPT a partir del id
        ///// </summary>
        //internal Models.AsuntoEstadoPT Select()
        //{
        //    Models.AsuntoEstadoPT oAsuntoEstadoPT = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_AsuntoEstadoPT_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAsuntoEstadoPT = new Models.AsuntoEstadoPT();
        //            oAsuntoEstadoPT.t409_idasunto=Convert.ToInt32(dr["t409_idasunto"]);
        //            oAsuntoEstadoPT.t416_estado=Convert.ToByte(dr["t416_estado"]);
        //            if(!Convert.IsDBNull(dr["Estado"]))
        //                oAsuntoEstadoPT.Estado=Convert.ToString(dr["Estado"]);
        //            oAsuntoEstadoPT.t416_fecha=Convert.ToDateTime(dr["t416_fecha"]);
        //            oAsuntoEstadoPT.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
        //            if(!Convert.IsDBNull(dr["nomRecurso"]))
        //                oAsuntoEstadoPT.nomRecurso=Convert.ToString(dr["nomRecurso"]);

        //        }
        //        return oAsuntoEstadoPT;
				
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
        ///// Actualiza un AsuntoEstadoPT a partir del id
        ///// </summary>
        //internal int Update(Models.AsuntoEstadoPT oAsuntoEstadoPT)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[6] {
        //            Param(enumDBFields.t409_idasunto, oAsuntoEstadoPT.t409_idasunto),
        //            Param(enumDBFields.t416_estado, oAsuntoEstadoPT.t416_estado),
        //            Param(enumDBFields.Estado, oAsuntoEstadoPT.Estado),
        //            Param(enumDBFields.t416_fecha, oAsuntoEstadoPT.t416_fecha),
        //            Param(enumDBFields.t314_idusuario, oAsuntoEstadoPT.t314_idusuario),
        //            Param(enumDBFields.nomRecurso, oAsuntoEstadoPT.nomRecurso)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_AsuntoEstadoPT_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un AsuntoEstadoPT a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_AsuntoEstadoPT_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        ///// <summary>
        ///// Obtiene todos los AsuntoEstado
        ///// </summary>
        internal List<Models.AsuntoEstadoPT> Catalogo(int t409_idasunto)
        {
            Models.AsuntoEstadoPT oAsuntoEstado = null;
            List<Models.AsuntoEstadoPT> lst = new List<Models.AsuntoEstadoPT>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t409_idasunto, t409_idasunto),
                };

                dr = cDblib.DataReader("SUP_ASUNTOESTADO_PT_SByt409_idasunto", dbparams);
                while (dr.Read())
                {
                    oAsuntoEstado = new Models.AsuntoEstadoPT();
                    oAsuntoEstado.t409_idasunto = Convert.ToInt32(dr["t409_idasunto"]);
                    oAsuntoEstado.t416_estado = Convert.ToByte(dr["t416_estado"]);
                    if (!Convert.IsDBNull(dr["Estado"]))
                        oAsuntoEstado.Estado = Convert.ToString(dr["Estado"]);
                    oAsuntoEstado.t416_fecha = Convert.ToDateTime(dr["t416_fecha"]);
                    oAsuntoEstado.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
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
                case enumDBFields.t409_idasunto:
                    paramName = "@t409_idasunto";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t416_estado:
                    paramName = "@t416_codestado";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                //case enumDBFields.Estado:
                //    paramName = "@Estado";
                //    paramType = SqlDbType.VarChar;
                //    paramSize = 10;
                //    break;
                //case enumDBFields.t416_fecha:
                //    paramName = "@t416_fecha";
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
