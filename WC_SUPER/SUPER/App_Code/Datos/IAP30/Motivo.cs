using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Motivo
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Motivo 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
        private enum enumDBFields : byte
        {
			t423_idmotivo = 1,
			t423_denominacion = 2,
			t423_estado = 3,
			t423_cuenta = 4,
            nOrden = 5,
            nAscDesc = 6
        }

        internal Motivo(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un Motivo
        ///// </summary>
        //internal int Insert(Models.Motivo oMotivo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t423_idmotivo, oMotivo.t423_idmotivo),
        //            Param(enumDBFields.t423_denominacion, oMotivo.t423_denominacion),
        //            Param(enumDBFields.t423_estado, oMotivo.t423_estado),
        //            Param(enumDBFields.t423_cuenta, oMotivo.t423_cuenta)
        //        };

        //        return (int)cDblib.Execute("_Motivo_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un Motivo a partir del id
        ///// </summary>
        internal Models.Motivo Select(byte t423_idmotivo)
        {
            Models.Motivo oMotivo = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
                            Param(enumDBFields.t423_idmotivo, t423_idmotivo),
                            Param(enumDBFields.t423_denominacion, ""),
                            Param(enumDBFields.t423_estado, null),
                            Param(enumDBFields.t423_cuenta, null),
                            Param(enumDBFields.nOrden, 2),
                            Param(enumDBFields.nAscDesc, 0)
                        };
                dr = cDblib.DataReader("GVT_MOTIVO_C", dbparams);
                if (dr.Read())
                {
                    oMotivo = new Models.Motivo();
                    oMotivo.t423_idmotivo = t423_idmotivo;
                    oMotivo.t423_denominacion = Convert.ToString(dr["t423_denominacion"]);
                    oMotivo.t423_estado = Convert.ToBoolean(dr["t423_estado"]);
                    oMotivo.t423_cuenta = Convert.ToInt32(dr["t423_cuenta"]);

                }
                return oMotivo;

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
		
        ///// <summary>
        ///// Actualiza un Motivo a partir del id
        ///// </summary>
        //internal int Update(Models.Motivo oMotivo)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t423_idmotivo, oMotivo.t423_idmotivo),
        //            Param(enumDBFields.t423_denominacion, oMotivo.t423_denominacion),
        //            Param(enumDBFields.t423_estado, oMotivo.t423_estado),
        //            Param(enumDBFields.t423_cuenta, oMotivo.t423_cuenta)
        //        };
                           
        //        return (int)cDblib.Execute("_Motivo_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un Motivo a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_Motivo_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los Motivo
        ///// </summary>
        //internal List<Models.Motivo> Catalogo(Models.Motivo oMotivoFilter)
        //{
        //    Models.Motivo oMotivo = null;
        //    List<Models.Motivo> lst = new List<Models.Motivo>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t423_idmotivo, oTEMP_MotivoFilter.t423_idmotivo),
        //            Param(enumDBFields.t423_denominacion, oTEMP_MotivoFilter.t423_denominacion),
        //            Param(enumDBFields.t423_estado, oTEMP_MotivoFilter.t423_estado),
        //            Param(enumDBFields.t423_cuenta, oTEMP_MotivoFilter.t423_cuenta)
        //        };

        //        dr = cDblib.DataReader("_Motivo_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oMotivo = new Models.Motivo();
        //            oMotivo.t423_idmotivo=Convert.ToByte(dr["t423_idmotivo"]);
        //            oMotivo.t423_denominacion=Convert.ToString(dr["t423_denominacion"]);
        //            oMotivo.t423_estado=Convert.ToBoolean(dr["t423_estado"]);
        //            oMotivo.t423_cuenta=Convert.ToInt32(dr["t423_cuenta"]);

        //            lst.Add(oMotivo);

        //        }
        //        return lst;
			
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
        public List<Models.Motivo> Catalogo()
        {
            Models.Motivo oMotivo = null;
            List<Models.Motivo> lst = new List<Models.Motivo>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
                            Param(enumDBFields.t423_idmotivo, null),
                            Param(enumDBFields.t423_denominacion, ""),
                            Param(enumDBFields.t423_estado, null),
                            Param(enumDBFields.t423_cuenta, null),
                            Param(enumDBFields.nOrden, 2),
                            Param(enumDBFields.nAscDesc, 0)
                        };

                dr = cDblib.DataReader("[GVT_MOTIVO_C]", dbparams);
                while (dr.Read())
                {
                    oMotivo = new Models.Motivo();
                    oMotivo.t423_idmotivo = Convert.ToByte(dr["t423_idmotivo"]);
                    oMotivo.t423_denominacion = Convert.ToString(dr["t423_denominacion"]);
                    oMotivo.t423_estado = Convert.ToBoolean(dr["t423_estado"]);
                    oMotivo.t423_cuenta = Convert.ToInt32(dr["t423_cuenta"]);
                    lst.Add(oMotivo);
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
				case enumDBFields.t423_idmotivo:
					paramName = "@t423_idmotivo";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t423_denominacion:
					paramName = "@t423_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t423_estado:
					paramName = "@t423_estado";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t423_cuenta:
					paramName = "@t423_cuenta";
					paramType = SqlDbType.Int;
					paramSize = 4;
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
