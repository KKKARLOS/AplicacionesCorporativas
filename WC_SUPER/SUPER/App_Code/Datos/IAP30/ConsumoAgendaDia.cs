using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoAgendaDia
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoAgendaDia 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			tot_Dia = 1
        }

        internal ConsumoAgendaDia(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ConsumoAgendaDia
        ///// </summary>
        //internal int Insert(Models.ConsumoAgendaDia oConsumoAgendaDia)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.tot_Dia, oConsumoAgendaDia.tot_Dia)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_ConsumoAgendaDia_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ConsumoAgendaDia a partir del id
        ///// </summary>
        //internal Models.ConsumoAgendaDia Select()
        //{
        //    Models.ConsumoAgendaDia oConsumoAgendaDia = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_ConsumoAgendaDia_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oConsumoAgendaDia = new Models.ConsumoAgendaDia();
        //            oConsumoAgendaDia.tot_Dia=Convert.ToDecimal(dr["tot_Dia"]);

        //        }
        //        return oConsumoAgendaDia;
				
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
        ///// Actualiza un ConsumoAgendaDia a partir del id
        ///// </summary>
        //internal int Update(Models.ConsumoAgendaDia oConsumoAgendaDia)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.tot_Dia, oConsumoAgendaDia.tot_Dia)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_ConsumoAgendaDia_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ConsumoAgendaDia a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_ConsumoAgendaDia_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ConsumoAgendaDia
        ///// </summary>
        //internal List<Models.ConsumoAgendaDia> Catalogo(Models.ConsumoAgendaDia oConsumoAgendaDiaFilter)
        //{
        //    Models.ConsumoAgendaDia oConsumoAgendaDia = null;
        //    List<Models.ConsumoAgendaDia> lst = new List<Models.ConsumoAgendaDia>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[1] {
        //            Param(enumDBFields.tot_Dia, oTEMP_ConsumoAgendaDiaFilter.tot_Dia)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_ConsumoAgendaDia_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oConsumoAgendaDia = new Models.ConsumoAgendaDia();
        //            oConsumoAgendaDia.tot_Dia=Convert.ToDecimal(dr["tot_Dia"]);

        //            lst.Add(oConsumoAgendaDia);

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
				case enumDBFields.tot_Dia:
					paramName = "@tot_Dia";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
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
