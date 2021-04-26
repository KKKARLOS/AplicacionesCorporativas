using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ModalidadContrato
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ModalidadContrato 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t316_idmodalidad = 1,
			t316_denominacion = 2,
            bTodos = 3,
            nOrden = 4,
            nAscDesc = 5
        }

        internal ModalidadContrato(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los ModalidadContrato
        /// </summary>
        internal List<Models.ModalidadContrato> Catalogo(Nullable<byte> t316_idmodalidad, string t316_denominacion, bool bTodos,
                                                        byte nOrden, byte nAscDesc)
		{
			Models.ModalidadContrato oModalidadContrato = null;
            List<Models.ModalidadContrato> lst = new List<Models.ModalidadContrato>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.t316_idmodalidad, t316_idmodalidad),
					Param(enumDBFields.t316_denominacion, t316_denominacion),
                    Param(enumDBFields.bTodos, bTodos),
                    Param(enumDBFields.nOrden, nOrden),
                    Param(enumDBFields.nAscDesc, nAscDesc)
				};

                dr = cDblib.DataReader("SUP_MODALIDADCONTRATO_C", dbparams);
				while (dr.Read())
				{
					oModalidadContrato = new Models.ModalidadContrato();
					oModalidadContrato.t316_idmodalidad=Convert.ToByte(dr["t316_idmodalidad"]);
					oModalidadContrato.t316_denominacion=Convert.ToString(dr["t316_denominacion"]);

                    lst.Add(oModalidadContrato);

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
				case enumDBFields.t316_idmodalidad:
					paramName = "@t316_idmodalidad";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.t316_denominacion:
					paramName = "@t316_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 30;
					break;
                case enumDBFields.bTodos:
                    paramName = "@bTodos";
                    paramType = SqlDbType.Bit;
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
