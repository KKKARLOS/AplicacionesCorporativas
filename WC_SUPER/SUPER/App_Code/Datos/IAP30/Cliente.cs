using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Cliente
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class Cliente 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t302_denominacion = 1,
            sTipoBusqueda = 2,
            bSoloActivos = 3,
            bInternos = 4,
            t314_idusuario = 5
        }

        internal Cliente(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los Cliente
        /// </summary>
        internal List<Models.Cliente> Catalogo(string t302_denominacion, string sTipoBusqueda, bool bSoloActivos, bool bInternos, Nullable<int> t314_idusuario)
		{
			Models.Cliente oCliente = null;
            List<Models.Cliente> lst = new List<Models.Cliente>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.t302_denominacion, t302_denominacion),
					Param(enumDBFields.sTipoBusqueda, sTipoBusqueda),
					Param(enumDBFields.bSoloActivos, bSoloActivos),
					Param(enumDBFields.bInternos, bInternos),
					Param(enumDBFields.t314_idusuario, t314_idusuario)
				};

                dr = cDblib.DataReader("SUP_CLIENTE_ByNombre", dbparams);
				while (dr.Read())
				{
					oCliente = new Models.Cliente();
					oCliente.tipo=Convert.ToString(dr["tipo"]);
					oCliente.t302_idcliente=Convert.ToInt32(dr["t302_idcliente"]);
					oCliente.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);
					oCliente.t302_estado=Convert.ToBoolean(dr["t302_estado"]);
					oCliente.t302_idcliente_matriz=Convert.ToInt32(dr["t302_idcliente_matriz"]);
					oCliente.t302_denominacion_matriz=Convert.ToString(dr["t302_denominacion_matriz"]);
					oCliente.t302_estado_matriz=Convert.ToBoolean(dr["t302_estado_matriz"]);
					oCliente.t302_codigoexterno=Convert.ToString(dr["t302_codigoexterno"]);

                    lst.Add(oCliente);

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
                case enumDBFields.t302_denominacion:
                    paramName = "@t302_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 100;
					break;
                case enumDBFields.sTipoBusqueda:
                    paramName = "@sTipoBusqueda";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.bSoloActivos:
                    paramName = "@bSoloActivos";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.bInternos:
                    paramName = "@bInternos";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
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
