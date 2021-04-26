using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPDia
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ConsumoIAPDia 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            nUsuario = 1,
            dDia = 2,
            nTarea=3
        }

        internal ConsumoIAPDia(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas
        /// <summary>
        /// Obtiene un ConsumoIAPDia a partir del id
        /// </summary>
        internal Models.ConsumoIAPDia Select(int idUser, DateTime dtFecha, int idTarea)
        {
            Models.ConsumoIAPDia oConsumoIAPDia = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.nUsuario, idUser),
                    Param(enumDBFields.dDia, dtFecha),
                    Param(enumDBFields.nTarea, idTarea)
                };
                dr = cDblib.DataReader("SUP_CONSUMOIAPDIA", dbparams);
                if (dr.Read())
                {
                    oConsumoIAPDia = new Models.ConsumoIAPDia();
                    oConsumoIAPDia.nHorasDiaGlobal = Convert.ToDouble(dr["horas"]);
                    if (dr.Read())
                        oConsumoIAPDia.nHorasDiaTarea = Convert.ToDouble(dr["horas"]);

                }
                return oConsumoIAPDia;

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
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.dDia:
                    paramName = "@dDia";
					paramType = SqlDbType.SmallDateTime;
					paramSize = 4;
					break;
                case enumDBFields.nTarea:
                    paramName = "@nTarea";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
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
