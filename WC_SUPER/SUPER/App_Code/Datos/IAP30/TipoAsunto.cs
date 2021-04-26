using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TipoAsunto
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TipoAsunto 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			T384_destipo = 1,
			T384_idtipo = 2,
			T384_orden = 3,
            nOrden = 4,
            nAscDesc = 5
        }

        internal TipoAsunto(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        //#region funciones publicas
        ///// <summary>
        ///// Inserta un TipoAsunto
        ///// </summary>
        //internal int Insert(Models.TipoAsunto oTipoAsunto)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.T384_destipo, oTipoAsunto.T384_destipo),
        //            Param(enumDBFields.T384_idtipo, oTipoAsunto.T384_idtipo),
        //            Param(enumDBFields.T384_orden, oTipoAsunto.T384_orden)
        //        };

        //        return (int)cDblib.Execute("_TipoAsunto_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un TipoAsunto a partir del id
        ///// </summary>
        //internal Models.TipoAsunto Select()
        //{
        //    Models.TipoAsunto oTipoAsunto = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_TipoAsunto_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oTipoAsunto = new Models.TipoAsunto();
        //            oTipoAsunto.T384_destipo=Convert.ToString(dr["T384_destipo"]);
        //            oTipoAsunto.T384_idtipo=Convert.ToInt32(dr["T384_idtipo"]);
        //            oTipoAsunto.T384_orden=Convert.ToByte(dr["T384_orden"]);

        //        }
        //        return oTipoAsunto;
				
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
        ///// Actualiza un TipoAsunto a partir del id
        ///// </summary>
        //internal int Update(Models.TipoAsunto oTipoAsunto)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.T384_destipo, oTipoAsunto.T384_destipo),
        //            Param(enumDBFields.T384_idtipo, oTipoAsunto.T384_idtipo),
        //            Param(enumDBFields.T384_orden, oTipoAsunto.T384_orden)
        //        };
                           
        //        return (int)cDblib.Execute("_TipoAsunto_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un TipoAsunto a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_TipoAsunto_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los TipoAsunto
        ///// </summary>
        internal List<Models.TipoAsunto> Catalogo(string t384_destipo, Nullable<int> t384_idtipo, Nullable<byte> t384_orden, byte nOrden, byte nAscDesc)
        {
            Models.TipoAsunto oTipoAsunto = null;
            List<Models.TipoAsunto> lst = new List<Models.TipoAsunto>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.T384_destipo, t384_destipo),
                    Param(enumDBFields.T384_idtipo, t384_idtipo),
                    Param(enumDBFields.T384_orden, t384_orden),
                    Param(enumDBFields.nOrden, nOrden),
                    Param(enumDBFields.nAscDesc, nAscDesc)
                };

                dr = cDblib.DataReader("SUP_TIPOASUNTO_C", dbparams);
                while (dr.Read())
                {
                    oTipoAsunto = new Models.TipoAsunto();
                    oTipoAsunto.T384_destipo = Convert.ToString(dr["T384_destipo"]);
                    oTipoAsunto.T384_idtipo = Convert.ToInt32(dr["T384_idtipo"]);
                    oTipoAsunto.T384_orden = Convert.ToByte(dr["T384_orden"]);

                    lst.Add(oTipoAsunto);

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
				case enumDBFields.T384_destipo:
					paramName = "@T384_destipo";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.T384_idtipo:
					paramName = "@T384_idtipo";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.T384_orden:
					paramName = "@T384_orden";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
                case enumDBFields.nOrden:
                    paramName = "@nOrden";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.nAscDesc:
                    paramName = "@nAscDesc";
                    paramType = SqlDbType.TinyInt;
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
