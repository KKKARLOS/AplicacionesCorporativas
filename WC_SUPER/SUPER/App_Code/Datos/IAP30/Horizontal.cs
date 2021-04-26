using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Horizontal
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Horizontal 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t307_idhorizontal = 1,
			t307_denominacion = 2,
            nOrden = 3,
            nAscDesc = 4
        }

        internal Horizontal(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los Horizontal
        /// </summary>
        internal List<Models.Horizontal> Catalogo(Nullable<int> t307_idhorizontal, string t307_denominacion, byte nOrden, byte nAscDesc)
		{
			Models.Horizontal oHorizontal = null;
            List<Models.Horizontal> lst = new List<Models.Horizontal>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.t307_idhorizontal, t307_idhorizontal),
					Param(enumDBFields.t307_denominacion, t307_denominacion),
                    Param(enumDBFields.nOrden, nOrden),
					Param(enumDBFields.nAscDesc, nAscDesc)
				};

                dr = cDblib.DataReader("SUP_HORIZONTAL_C", dbparams);
				while (dr.Read())
				{
					oHorizontal = new Models.Horizontal();
					oHorizontal.identificador=Convert.ToInt32(dr["t307_idhorizontal"]);
					oHorizontal.denominacion=Convert.ToString(dr["t307_denominacion"]);

                    lst.Add(oHorizontal);

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
				case enumDBFields.t307_idhorizontal:
					paramName = "@t307_idhorizontal";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t307_denominacion:
					paramName = "@t307_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
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
