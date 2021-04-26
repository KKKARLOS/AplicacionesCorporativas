using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for TareaPendientePreventa
/// </summary>

namespace IB.SUPER.SIC.DAL 
{
    
    internal class TareaPendientePreventa 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			ta208_idtareapendientepreventa = 1,
			ta209_idconceptotareapendiente = 2,
			ta208_fechaplazo = 3,
			t001_idficepi_interesado = 4,
			ta204_idaccionpreventa = 5,
            ta207_idtareapreventa = 6,
            t001_idficepi = 7,
            tablaconceptos = 8
        }

        internal TareaPendientePreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un TareaPendientePreventa
        /// </summary>
		internal int Insert(Models.TareaPendientePreventa oTareaPendientePreventa)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.ta209_idconceptotareapendiente, oTareaPendientePreventa.ta209_idconceptotareapendiente),
					Param(enumDBFields.ta208_fechaplazo, oTareaPendientePreventa.ta208_fechaplazo),
					Param(enumDBFields.t001_idficepi_interesado, oTareaPendientePreventa.t001_idficepi_interesado),
					Param(enumDBFields.ta204_idaccionpreventa, oTareaPendientePreventa.ta204_idaccionpreventa)
				};

				return (int)cDblib.Execute("SUPER.SIC_TareaPendientePreventa_INS", dbparams);
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Obtiene un TareaPendientePreventa a partir del id
        /// </summary>
        internal Models.TareaPendientePreventa Select(Int32 ta208_idtareapendientepreventa)
        {
            Models.TareaPendientePreventa oTareaPendientePreventa = null;
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta208_idtareapendientepreventa, ta208_idtareapendientepreventa)
				};

				dr = cDblib.DataReader("SUPER.SIC_TareaPendientePreventa_SEL", dbparams);
				if (dr.Read())
				{
					oTareaPendientePreventa = new Models.TareaPendientePreventa();
					oTareaPendientePreventa.ta208_idtareapendientepreventa=Convert.ToInt32(dr["ta208_idtareapendientepreventa"]);
					oTareaPendientePreventa.ta209_idconceptotareapendiente=Convert.ToByte(dr["ta209_idconceptotareapendiente"]);
					oTareaPendientePreventa.ta208_fechaplazo=Convert.ToDateTime(dr["ta208_fechaplazo"]);
					oTareaPendientePreventa.t001_idficepi_interesado=Convert.ToInt32(dr["t001_idficepi_interesado"]);
					if(!Convert.IsDBNull(dr["ta204_idaccionpreventa"]))
						oTareaPendientePreventa.ta204_idaccionpreventa=Convert.ToInt32(dr["ta204_idaccionpreventa"]);

				}
				return oTareaPendientePreventa;
				
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
		
		/// <summary>
        /// Actualiza un TareaPendientePreventa a partir del id
        /// </summary>
		internal int Update(Models.TareaPendientePreventa oTareaPendientePreventa)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[5] {
					Param(enumDBFields.ta208_idtareapendientepreventa, oTareaPendientePreventa.ta208_idtareapendientepreventa),
					Param(enumDBFields.ta209_idconceptotareapendiente, oTareaPendientePreventa.ta209_idconceptotareapendiente),
					Param(enumDBFields.ta208_fechaplazo, oTareaPendientePreventa.ta208_fechaplazo),
					Param(enumDBFields.t001_idficepi_interesado, oTareaPendientePreventa.t001_idficepi_interesado),
					Param(enumDBFields.ta204_idaccionpreventa, oTareaPendientePreventa.ta204_idaccionpreventa)
				};
                           
				return (int)cDblib.Execute("SUPER.SIC_TareaPendientePreventa_UPD", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Elimina un TareaPendientePreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta208_idtareapendientepreventa)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta208_idtareapendientepreventa, ta208_idtareapendientepreventa)
				};
            
				return (int)cDblib.Execute("SUPER.SIC_TareaPendientePreventa_DEL", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
        /// Obtiene todos los TareaPendientePreventa
        /// </summary>
		internal List<Models.TareaPendientePreventa> Catalogo(Models.TareaPendientePreventa oTareaPendientePreventaFilter)
		{
			Models.TareaPendientePreventa oTareaPendientePreventa = null;
            List<Models.TareaPendientePreventa> lst = new List<Models.TareaPendientePreventa>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.ta209_idconceptotareapendiente, oTareaPendientePreventaFilter.ta209_idconceptotareapendiente),
					Param(enumDBFields.ta208_fechaplazo, oTareaPendientePreventaFilter.ta208_fechaplazo),
					Param(enumDBFields.t001_idficepi_interesado, oTareaPendientePreventaFilter.t001_idficepi_interesado),
					Param(enumDBFields.ta204_idaccionpreventa, oTareaPendientePreventaFilter.ta204_idaccionpreventa)
				};

				dr = cDblib.DataReader("SUPER.SIC_TareaPendientePreventa_CAT", dbparams);
				while (dr.Read())
				{
					oTareaPendientePreventa = new Models.TareaPendientePreventa();
					oTareaPendientePreventa.ta208_idtareapendientepreventa=Convert.ToInt32(dr["ta208_idtareapendientepreventa"]);
					oTareaPendientePreventa.ta209_idconceptotareapendiente=Convert.ToByte(dr["ta209_idconceptotareapendiente"]);
					oTareaPendientePreventa.ta208_fechaplazo=Convert.ToDateTime(dr["ta208_fechaplazo"]);
					oTareaPendientePreventa.t001_idficepi_interesado=Convert.ToInt32(dr["t001_idficepi_interesado"]);
					if(!Convert.IsDBNull(dr["ta204_idaccionpreventa"]))
						oTareaPendientePreventa.ta204_idaccionpreventa=Convert.ToInt32(dr["ta204_idaccionpreventa"]);

                    lst.Add(oTareaPendientePreventa);

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

        internal void quitarNegritaTareaPendiente(int t001_idficepi, DataTable tablaconceptos, Nullable<int> ta204_idaccionpreventa, Nullable<int> ta207_idtareapreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.t001_idficepi, t001_idficepi),
					Param(enumDBFields.tablaconceptos, tablaconceptos),
					Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa),
					Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa)
					
				};

                cDblib.Execute("SIC_QUITANEGRITASENTAREASPENDIENTES", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
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
				case enumDBFields.ta208_idtareapendientepreventa:
					paramName = "@ta208_idtareapendientepreventa";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.ta209_idconceptotareapendiente:
					paramName = "@ta209_idconceptotareapendiente";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.ta208_fechaplazo:
					paramName = "@ta208_fechaplazo";
					paramType = SqlDbType.Date;
					paramSize = 3;
					break;
				case enumDBFields.t001_idficepi_interesado:
					paramName = "@t001_idficepi_interesado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.ta204_idaccionpreventa:
					paramName = "@ta204_idaccionpreventa";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.ta207_idtareapreventa:
                    paramName = "@ta207_idtareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.tablaconceptos:
                    paramName = "@tablaconceptos";
                    paramType = SqlDbType.Structured;
                    paramSize = 0;
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
