using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;
using IB.Progress.Shared;

/// <summary>
/// Summary description for ROLIB
/// </summary>

namespace IB.Progress.DAL 
{
    
    internal class ROLIB 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t004_idrol = 1,
			t004_desrol = 2,
			t004_aprobador = 3,
            tablaaprobadores = 4,
            t001_apellido1 = 5,
            t001_apellido2 = 6,
            t001_nombre = 7,
            desde = 8,
            hasta = 9

        }

        internal ROLIB(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
		
		public ROLIB()
        {
            
			//lo dejo pero de momento no se usa
        }
		
		#endregion
	
		#region funciones publicas
		
        /// <summary>
        /// Actualiza un ROLIB a partir del id
        /// </summary>
        
        //Pendiente de poder pasar datatables a los procedimientos. Limitaciones del dblib.dll
        //internal int Update(DataTable listaRoles)
        internal int Update(string listaRoles)
        {
			try
			{
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.tablaaprobadores, listaRoles.ToString())					
				};

                return (int)cDblib.Execute("PRO_ROLAPROBADOR_UPD", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
        /// Obtiene todos los ROLIB
        /// </summary>
		internal List<Models.ROLIB> Catalogo()
		{
			Models.ROLIB oROLIB = null;
            List<Models.ROLIB> lst = new List<Models.ROLIB>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[0];

                dr = cDblib.DataReader("PRO_GETROLES", dbparams);
				while (dr.Read())
				{
					oROLIB = new Models.ROLIB();
                    oROLIB.t004_idrol = Convert.ToInt16(dr["t004_idrol"]);
                    oROLIB.t004_desrol = Convert.ToString(dr["t004_desrol"]);
                    oROLIB.t004_aprobador = Convert.ToBoolean(dr["t004_aprobador"]);
                    lst.Add(oROLIB);
				}
				return lst;
			
			}
            catch (Exception ex)
            {
                throw new IBException(102, "Ocurrió un error obteniendo todos los roles de base de datos.", ex);
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


        internal List<Models.ROLIB> CatHistoricoRoles(string t001_apellido1, string t001_apellido2, string t001_nombre, int desde, int hasta)
        {
            List<Models.ROLIB> returnList = new List<Models.ROLIB>();

            Models.ROLIB oRol = null;

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString()),
                    Param(enumDBFields.desde, desde),
                    Param(enumDBFields.hasta, hasta)
                  
                };

                dr = cDblib.DataReader("PRO_HISTORICOROLES", dbparams);

                while (dr.Read())
                {
                    oRol = new Models.ROLIB();
                    oRol.Profesional = Convert.ToString(dr["profesional"]);                    
                    oRol.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                    oRol.RolNuevo = Convert.ToString(dr["rolnuevo"]);
                    oRol.FechaCambio = DateTime.Parse(dr["fechacambio"].ToString());


                    returnList.Add(oRol);
                }

                return returnList;
            }

            catch (Exception ex)
            {
                throw new IBException(108, "Ocurrió un error obteniendo los datos de mi histórico de roles de base de datos.", ex);
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
				case enumDBFields.t004_idrol:
					paramName = "@t004_idrol";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				
                case enumDBFields.t004_desrol:
					paramName = "@t004_desrol";
                    paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				
				case enumDBFields.t004_aprobador:
					paramName = "@t004_aprobador";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;

                case enumDBFields.tablaaprobadores:
                    paramName = "@tablaaprobadores";
                    paramType = SqlDbType.VarChar;
                    break;

                case enumDBFields.t001_apellido1:
                    paramName = "@t001_apellido1";
                    paramType = SqlDbType.VarChar;
                    break;


                case enumDBFields.t001_apellido2:
                    paramName = "@t001_apellido2";
                    paramType = SqlDbType.VarChar;
                    break;

                case enumDBFields.t001_nombre:
                    paramName = "@t001_nombre";
                    paramType = SqlDbType.VarChar;
                    break;

                case enumDBFields.desde:
                    paramName = "@desde";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.hasta:
                    paramName = "@hasta";
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
