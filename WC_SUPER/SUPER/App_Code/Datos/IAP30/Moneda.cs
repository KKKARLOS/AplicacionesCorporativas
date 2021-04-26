using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Moneda
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Moneda 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            bSoloActivas = 1
        }

        internal Moneda(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un Moneda
        ///// </summary>
        //internal int Insert(Models.Moneda oMoneda)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t422_idmoneda, oMoneda.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oMoneda.t422_denominacion),
        //            Param(enumDBFields.t422_estado, oMoneda.t422_estado)
        //        };

        //        return (int)cDblib.Execute("_Moneda_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un Moneda a partir del id
        ///// </summary>
        //internal Models.Moneda Select()
        //{
        //    Models.Moneda oMoneda = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("_Moneda_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oMoneda = new Models.Moneda();
        //            oMoneda.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oMoneda.t422_denominacion=Convert.ToString(dr["t422_denominacion"]);
        //            oMoneda.t422_estado=Convert.ToBoolean(dr["t422_estado"]);

        //        }
        //        return oMoneda;
				
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
        ///// Actualiza un Moneda a partir del id
        ///// </summary>
        //internal int Update(Models.Moneda oMoneda)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t422_idmoneda, oMoneda.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oMoneda.t422_denominacion),
        //            Param(enumDBFields.t422_estado, oMoneda.t422_estado)
        //        };
                           
        //        return (int)cDblib.Execute("_Moneda_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un Moneda a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("_Moneda_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los Moneda
        ///// </summary>
        //internal List<Models.Moneda> Catalogo(Models.Moneda oMonedaFilter)
        //{
        //    Models.Moneda oMoneda = null;
        //    List<Models.Moneda> lst = new List<Models.Moneda>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(enumDBFields.t422_idmoneda, oTEMP_MonedaFilter.t422_idmoneda),
        //            Param(enumDBFields.t422_denominacion, oTEMP_MonedaFilter.t422_denominacion),
        //            Param(enumDBFields.t422_estado, oTEMP_MonedaFilter.t422_estado)
        //        };

        //        dr = cDblib.DataReader("_Moneda_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oMoneda = new Models.Moneda();
        //            oMoneda.t422_idmoneda=Convert.ToString(dr["t422_idmoneda"]);
        //            oMoneda.t422_denominacion=Convert.ToString(dr["t422_denominacion"]);
        //            oMoneda.t422_estado=Convert.ToBoolean(dr["t422_estado"]);

        //            lst.Add(oMoneda);

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
        public List<Models.Moneda> Catalogo()
        {
            Models.Moneda oMoneda = null;
            List<Models.Moneda> lst = new List<Models.Moneda>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                            Param(enumDBFields.bSoloActivas, true),
                        };

                dr = cDblib.DataReader("GVT_MONEDA_CAT", dbparams);
                while (dr.Read())
                {
                    oMoneda = new Models.Moneda();
                    oMoneda.t422_idmoneda = Convert.ToString(dr["t422_idmoneda"]);
                    oMoneda.t422_denominacion = Convert.ToString(dr["t422_denominacion"]);
                    oMoneda.t422_estado = Convert.ToBoolean(dr["t422_estado"]);
                    lst.Add(oMoneda);
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
                case enumDBFields.bSoloActivas:
                    paramName = "@bSoloActivas";
					paramType = SqlDbType.Bit;
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
