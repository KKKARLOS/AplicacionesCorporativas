using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPTotalSemana
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoIAPTotalSemana 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            nEmpleado = 1,
            dDesde = 2,
            dHasta = 3

        }

        internal ConsumoIAPTotalSemana(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas

        internal Models.ConsumoIAPTotalSemana ObtenerConsumosTotalesSemanaIAP(int nEmpleado, DateTime dDesde, DateTime dHasta)
        {
            Models.ConsumoIAPTotalSemana oConsumoIAPTotalSemana = null;
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.nEmpleado, nEmpleado),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta),
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAPTOTALSEMANA_IAP30", dbparams);

                if (dr.Read())
                {
                    oConsumoIAPTotalSemana = new Models.ConsumoIAPTotalSemana();
                    oConsumoIAPTotalSemana.tot_Lunes = Convert.ToDouble(dr["tot_Lunes"]);
                    oConsumoIAPTotalSemana.tot_Martes = Convert.ToDouble(dr["tot_Martes"]);
                    oConsumoIAPTotalSemana.tot_Miercoles = Convert.ToDouble(dr["tot_Miercoles"]);
                    oConsumoIAPTotalSemana.tot_Jueves = Convert.ToDouble(dr["tot_Jueves"]);
                    oConsumoIAPTotalSemana.tot_Viernes = Convert.ToDouble(dr["tot_Viernes"]);
                    oConsumoIAPTotalSemana.tot_Sabado = Convert.ToDouble(dr["tot_Sabado"]);
                    oConsumoIAPTotalSemana.tot_Domingo = Convert.ToDouble(dr["tot_Domingo"]);

                }
                return oConsumoIAPTotalSemana;

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
                case enumDBFields.nEmpleado:
                    paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 5;
					break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
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
