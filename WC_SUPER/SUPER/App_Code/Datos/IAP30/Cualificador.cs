using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Cualificador
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Cualificador 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            sTipo = 1,
            t303_idnodo = 2
        }

        internal Cualificador(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los Cualificador
        /// </summary>
        internal List<Models.Cualificador> Catalogo(string sTipo, Int32 t303_idnodo)
		{
			Models.Cualificador oCualificador = null;
            List<Models.Cualificador> lst = new List<Models.Cualificador>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.sTipo, sTipo),
					Param(enumDBFields.t303_idnodo, t303_idnodo)
				};

                dr = cDblib.DataReader("SUP_CUALIFICADORESPROY_C", dbparams);
				while (dr.Read())
				{
					oCualificador = new Models.Cualificador();
					if(!Convert.IsDBNull(dr["identificador"]))
						oCualificador.identificador=Convert.ToInt32(dr["identificador"]);
					if(!Convert.IsDBNull(dr["denominacion"]))
						oCualificador.denominacion=Convert.ToString(dr["denominacion"]);
					if(!Convert.IsDBNull(dr["orden"]))
						oCualificador.orden=Convert.ToByte(dr["orden"]);

                    lst.Add(oCualificador);

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
                case enumDBFields.sTipo:
                    paramName = "@sTipo";
					paramType = SqlDbType.Char;
					paramSize = 2;
					break;
                case enumDBFields.t303_idnodo:
                    paramName = "@t303_idnodo";
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
