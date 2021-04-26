using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for ConceptoTareaPendiente
/// </summary>

namespace IB.SUPER.SIC.DAL 
{
    
    internal class ConceptoTareaPendiente 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			ta209_idconceptotareapendiente = 1,
			ta209_denominacion = 2
        }

        internal ConceptoTareaPendiente(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un ConceptoTareaPendiente
        /// </summary>
		internal int Insert(Models.ConceptoTareaPendiente oConceptoTareaPendiente)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta209_idconceptotareapendiente, oConceptoTareaPendiente.ta209_idconceptotareapendiente),
					Param(enumDBFields.ta209_denominacion, oConceptoTareaPendiente.ta209_denominacion)
				};

				return (int)cDblib.Execute("SUPER.SIC_ConceptoTareaPendiente_INS", dbparams);
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Obtiene un ConceptoTareaPendiente a partir del id
        /// </summary>
        internal Models.ConceptoTareaPendiente Select(Byte ta209_idconceptotareapendiente)
        {
            Models.ConceptoTareaPendiente oConceptoTareaPendiente = null;
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta209_idconceptotareapendiente, ta209_idconceptotareapendiente)
				};

				dr = cDblib.DataReader("SUPER.SIC_ConceptoTareaPendiente_SEL", dbparams);
				if (dr.Read())
				{
					oConceptoTareaPendiente = new Models.ConceptoTareaPendiente();
					oConceptoTareaPendiente.ta209_idconceptotareapendiente=Convert.ToByte(dr["ta209_idconceptotareapendiente"]);
					oConceptoTareaPendiente.ta209_denominacion=Convert.ToString(dr["ta209_denominacion"]);

				}
				return oConceptoTareaPendiente;
				
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
        /// Actualiza un ConceptoTareaPendiente a partir del id
        /// </summary>
		internal int Update(Models.ConceptoTareaPendiente oConceptoTareaPendiente)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta209_idconceptotareapendiente, oConceptoTareaPendiente.ta209_idconceptotareapendiente),
					Param(enumDBFields.ta209_denominacion, oConceptoTareaPendiente.ta209_denominacion)
				};
                           
				return (int)cDblib.Execute("SUPER.SIC_ConceptoTareaPendiente_UPD", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Elimina un ConceptoTareaPendiente a partir del id
        /// </summary>
        internal int Delete(Byte ta209_idconceptotareapendiente)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta209_idconceptotareapendiente, ta209_idconceptotareapendiente)
				};
            
				return (int)cDblib.Execute("SUPER.SIC_ConceptoTareaPendiente_DEL", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
        /// Obtiene todos los ConceptoTareaPendiente
        /// </summary>
		internal List<Models.ConceptoTareaPendiente> Catalogo(Models.ConceptoTareaPendiente oConceptoTareaPendienteFilter)
		{
			Models.ConceptoTareaPendiente oConceptoTareaPendiente = null;
            List<Models.ConceptoTareaPendiente> lst = new List<Models.ConceptoTareaPendiente>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta209_denominacion, oConceptoTareaPendienteFilter.ta209_denominacion)
				};

				dr = cDblib.DataReader("SUPER.SIC_ConceptoTareaPendiente_CAT", dbparams);
				while (dr.Read())
				{
					oConceptoTareaPendiente = new Models.ConceptoTareaPendiente();
					oConceptoTareaPendiente.ta209_idconceptotareapendiente=Convert.ToByte(dr["ta209_idconceptotareapendiente"]);
					oConceptoTareaPendiente.ta209_denominacion=Convert.ToString(dr["ta209_denominacion"]);

                    lst.Add(oConceptoTareaPendiente);

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
				case enumDBFields.ta209_idconceptotareapendiente:
					paramName = "@ta209_idconceptotareapendiente";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.ta209_denominacion:
					paramName = "@ta209_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
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
