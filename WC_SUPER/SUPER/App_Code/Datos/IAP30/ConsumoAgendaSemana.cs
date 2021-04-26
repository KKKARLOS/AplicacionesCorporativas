using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoAgendaSemana
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoAgendaSemana 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			tot_Lunes = 1,
			tot_Martes = 2,
			tot_Miercoles = 3,
			tot_Jueves = 4,
			tot_Viernes = 5,
			tot_Sabado = 6,
			tot_Domingo = 7
        }

        internal ConsumoAgendaSemana(sqldblib.SqlServerSP extcDblib)
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
        ///// Inserta un ConsumoAgendaSemana
        ///// </summary>
        //internal int Insert(Models.ConsumoAgendaSemana oConsumoAgendaSemana)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.tot_Lunes, oConsumoAgendaSemana.tot_Lunes),
        //            Param(enumDBFields.tot_Martes, oConsumoAgendaSemana.tot_Martes),
        //            Param(enumDBFields.tot_Miercoles, oConsumoAgendaSemana.tot_Miercoles),
        //            Param(enumDBFields.tot_Jueves, oConsumoAgendaSemana.tot_Jueves),
        //            Param(enumDBFields.tot_Viernes, oConsumoAgendaSemana.tot_Viernes),
        //            Param(enumDBFields.tot_Sabado, oConsumoAgendaSemana.tot_Sabado),
        //            Param(enumDBFields.tot_Domingo, oConsumoAgendaSemana.tot_Domingo)
        //        };

        //        return (int)cDblib.Execute("SUPER.IAP30_ConsumoAgendaSemana_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Obtiene un ConsumoAgendaSemana a partir del id
        ///// </summary>
        //internal Models.ConsumoAgendaSemana Select()
        //{
        //    Models.ConsumoAgendaSemana oConsumoAgendaSemana = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("SUPER.IAP30_ConsumoAgendaSemana_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oConsumoAgendaSemana = new Models.ConsumoAgendaSemana();
        //            oConsumoAgendaSemana.tot_Lunes=Convert.ToDecimal(dr["tot_Lunes"]);
        //            oConsumoAgendaSemana.tot_Martes=Convert.ToDecimal(dr["tot_Martes"]);
        //            oConsumoAgendaSemana.tot_Miercoles=Convert.ToDecimal(dr["tot_Miercoles"]);
        //            oConsumoAgendaSemana.tot_Jueves=Convert.ToDecimal(dr["tot_Jueves"]);
        //            oConsumoAgendaSemana.tot_Viernes=Convert.ToDecimal(dr["tot_Viernes"]);
        //            oConsumoAgendaSemana.tot_Sabado=Convert.ToDecimal(dr["tot_Sabado"]);
        //            oConsumoAgendaSemana.tot_Domingo=Convert.ToDecimal(dr["tot_Domingo"]);

        //        }
        //        return oConsumoAgendaSemana;
				
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
        ///// Actualiza un ConsumoAgendaSemana a partir del id
        ///// </summary>
        //internal int Update(Models.ConsumoAgendaSemana oConsumoAgendaSemana)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.tot_Lunes, oConsumoAgendaSemana.tot_Lunes),
        //            Param(enumDBFields.tot_Martes, oConsumoAgendaSemana.tot_Martes),
        //            Param(enumDBFields.tot_Miercoles, oConsumoAgendaSemana.tot_Miercoles),
        //            Param(enumDBFields.tot_Jueves, oConsumoAgendaSemana.tot_Jueves),
        //            Param(enumDBFields.tot_Viernes, oConsumoAgendaSemana.tot_Viernes),
        //            Param(enumDBFields.tot_Sabado, oConsumoAgendaSemana.tot_Sabado),
        //            Param(enumDBFields.tot_Domingo, oConsumoAgendaSemana.tot_Domingo)
        //        };
                           
        //        return (int)cDblib.Execute("SUPER.IAP30_ConsumoAgendaSemana_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
        ///// <summary>
        ///// Elimina un ConsumoAgendaSemana a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("SUPER.IAP30_ConsumoAgendaSemana_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene todos los ConsumoAgendaSemana
        ///// </summary>
        //internal List<Models.ConsumoAgendaSemana> Catalogo(Models.ConsumoAgendaSemana oConsumoAgendaSemanaFilter)
        //{
        //    Models.ConsumoAgendaSemana oConsumoAgendaSemana = null;
        //    List<Models.ConsumoAgendaSemana> lst = new List<Models.ConsumoAgendaSemana>();
        //    IDataReader dr = null;

        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[7] {
        //            Param(enumDBFields.tot_Lunes, oTEMP_ConsumoAgendaSemanaFilter.tot_Lunes),
        //            Param(enumDBFields.tot_Martes, oTEMP_ConsumoAgendaSemanaFilter.tot_Martes),
        //            Param(enumDBFields.tot_Miercoles, oTEMP_ConsumoAgendaSemanaFilter.tot_Miercoles),
        //            Param(enumDBFields.tot_Jueves, oTEMP_ConsumoAgendaSemanaFilter.tot_Jueves),
        //            Param(enumDBFields.tot_Viernes, oTEMP_ConsumoAgendaSemanaFilter.tot_Viernes),
        //            Param(enumDBFields.tot_Sabado, oTEMP_ConsumoAgendaSemanaFilter.tot_Sabado),
        //            Param(enumDBFields.tot_Domingo, oTEMP_ConsumoAgendaSemanaFilter.tot_Domingo)
        //        };

        //        dr = cDblib.DataReader("SUPER.IAP30_ConsumoAgendaSemana_CAT", dbparams);
        //        while (dr.Read())
        //        {
        //            oConsumoAgendaSemana = new Models.ConsumoAgendaSemana();
        //            oConsumoAgendaSemana.tot_Lunes=Convert.ToDecimal(dr["tot_Lunes"]);
        //            oConsumoAgendaSemana.tot_Martes=Convert.ToDecimal(dr["tot_Martes"]);
        //            oConsumoAgendaSemana.tot_Miercoles=Convert.ToDecimal(dr["tot_Miercoles"]);
        //            oConsumoAgendaSemana.tot_Jueves=Convert.ToDecimal(dr["tot_Jueves"]);
        //            oConsumoAgendaSemana.tot_Viernes=Convert.ToDecimal(dr["tot_Viernes"]);
        //            oConsumoAgendaSemana.tot_Sabado=Convert.ToDecimal(dr["tot_Sabado"]);
        //            oConsumoAgendaSemana.tot_Domingo=Convert.ToDecimal(dr["tot_Domingo"]);

        //            lst.Add(oConsumoAgendaSemana);

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
				case enumDBFields.tot_Lunes:
					paramName = "@tot_Lunes";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
					break;
				case enumDBFields.tot_Martes:
					paramName = "@tot_Martes";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
					break;
				case enumDBFields.tot_Miercoles:
					paramName = "@tot_Miercoles";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
					break;
				case enumDBFields.tot_Jueves:
					paramName = "@tot_Jueves";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
					break;
				case enumDBFields.tot_Viernes:
					paramName = "@tot_Viernes";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
					break;
				case enumDBFields.tot_Sabado:
					paramName = "@tot_Sabado";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
					break;
				case enumDBFields.tot_Domingo:
					paramName = "@tot_Domingo";
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
