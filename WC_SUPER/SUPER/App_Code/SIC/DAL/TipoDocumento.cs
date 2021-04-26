using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for TipoDocumento
/// </summary>

namespace IB.SUPER.SIC.DAL 
{
    
    internal class TipoDocumento 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			ta211_idtipodocumento = 1,
			ta211_denominacion = 2
        }

        internal TipoDocumento(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un TipoDocumento
        /// </summary>
		internal int Insert(Models.TipoDocumento oTipoDocumento)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta211_idtipodocumento, oTipoDocumento.ta211_idtipodocumento),
					Param(enumDBFields.ta211_denominacion, oTipoDocumento.ta211_denominacion)
				};

				return (int)cDblib.Execute("SUPER.SIC_TipoDocumento_INS", dbparams);
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Obtiene un TipoDocumento a partir del id
        /// </summary>
        internal Models.TipoDocumento Select(Byte ta211_idtipodocumento)
        {
            Models.TipoDocumento oTipoDocumento = null;
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta211_idtipodocumento, ta211_idtipodocumento)
				};

				dr = cDblib.DataReader("SUPER.SIC_TipoDocumento_SEL", dbparams);
				if (dr.Read())
				{
					oTipoDocumento = new Models.TipoDocumento();
					oTipoDocumento.ta211_idtipodocumento=Convert.ToByte(dr["ta211_idtipodocumento"]);
					oTipoDocumento.ta211_denominacion=Convert.ToString(dr["ta211_denominacion"]);

				}
				return oTipoDocumento;
				
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
        /// Actualiza un TipoDocumento a partir del id
        /// </summary>
		internal int Update(Models.TipoDocumento oTipoDocumento)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[2] {
					Param(enumDBFields.ta211_idtipodocumento, oTipoDocumento.ta211_idtipodocumento),
					Param(enumDBFields.ta211_denominacion, oTipoDocumento.ta211_denominacion)
				};
                           
				return (int)cDblib.Execute("SUPER.SIC_TipoDocumento_UPD", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Elimina un TipoDocumento a partir del id
        /// </summary>
        internal int Delete(Byte ta211_idtipodocumento)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta211_idtipodocumento, ta211_idtipodocumento)
				};
            
				return (int)cDblib.Execute("SUPER.SIC_TipoDocumento_DEL", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
        /// Obtiene todos los TipoDocumento
        /// </summary>
		internal List<Models.TipoDocumento> Catalogo(Models.TipoDocumento oTipoDocumentoFilter)
		{
			Models.TipoDocumento oTipoDocumento = null;
            List<Models.TipoDocumento> lst = new List<Models.TipoDocumento>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta211_denominacion, oTipoDocumentoFilter.ta211_denominacion)
				};

				dr = cDblib.DataReader("SUPER.SIC_TipoDocumento_CAT", dbparams);
				while (dr.Read())
				{
					oTipoDocumento = new Models.TipoDocumento();
					oTipoDocumento.ta211_idtipodocumento=Convert.ToByte(dr["ta211_idtipodocumento"]);
					oTipoDocumento.ta211_denominacion=Convert.ToString(dr["ta211_denominacion"]);

                    lst.Add(oTipoDocumento);

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
				case enumDBFields.ta211_idtipodocumento:
					paramName = "@ta211_idtipodocumento";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
				case enumDBFields.ta211_denominacion:
					paramName = "@ta211_denominacion";
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
