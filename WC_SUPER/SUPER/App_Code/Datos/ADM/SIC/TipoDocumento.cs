using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for TipoDocumento
/// </summary>

namespace IB.SUPER.ADM.SIC.DAL 
{
    
    internal class TipoDocumento 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			ta211_idtipodocumento = 1,
			ta211_denominacion = 2,
            ta211_estadoactiva = 3,
            ta211_orden = 4,
            tablaTipoDocumento = 5
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
        /// grabar TiposTareaPreventa
        /// </summary>
        internal List<Models.TipoDocumento> GrabarDocumentos(DataTable dtTipoDocumentos)
        {
            Models.TipoDocumento oTipoDocumento = null;
            List<Models.TipoDocumento> lst = new List<Models.TipoDocumento>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.tablaTipoDocumento, dtTipoDocumentos)
                };

                dr = cDblib.DataReader("SIC_TIPODOCUMENTOPREVENTA_IUD", dbparams);
                while (dr.Read())
                {
                    oTipoDocumento = new Models.TipoDocumento();
                    oTipoDocumento.ta211_idtipodocumento = Convert.ToInt16(dr["ta211_idtipodocumento"]);
                    oTipoDocumento.ta211_denominacion = Convert.ToString(dr["ta211_denominacion"]);
                    oTipoDocumento.ta211_estadoactiva = Convert.ToBoolean(dr["ta211_activo"]);

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

        /// <summary>
        /// Obtiene un TipoDocumento a partir del id
        /// </summary>
        internal Models.TipoDocumento Select(Int16 ta211_idtipodocumento)
        {
            Models.TipoDocumento oTipoDocumento = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.ta211_idtipodocumento, ta211_idtipodocumento)
                };

                dr = cDblib.DataReader("SIC_TipoDocumento_C", dbparams);
                if (dr.Read())
                {
                    oTipoDocumento = new Models.TipoDocumento();
                    oTipoDocumento.ta211_idtipodocumento = Convert.ToInt16(dr["ta211_idtipodocumento"]);
                    oTipoDocumento.ta211_denominacion = Convert.ToString(dr["ta211_denominacion"]);
                    oTipoDocumento.ta211_estadoactiva = Convert.ToBoolean(dr["ta211_activo"]);

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
        /// Obtiene todos los TipoDocumento
        /// </summary>
        internal List<Models.TipoDocumento> Catalogo()
        {
            Models.TipoDocumento oTipoDocumento = null;
            List<Models.TipoDocumento> lst = new List<Models.TipoDocumento>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] { };

                dr = cDblib.DataReader("SIC_TipoDocumento_CAT", dbparams);
                while (dr.Read())
                {
                    oTipoDocumento = new Models.TipoDocumento();
                    oTipoDocumento.ta211_idtipodocumento = Convert.ToInt16(dr["ta211_idtipodocumento"]);
                    oTipoDocumento.ta211_denominacion = Convert.ToString(dr["ta211_denominacion"]);
                    oTipoDocumento.ta211_estadoactiva = Convert.ToBoolean(dr["ta211_activo"]);

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
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.ta211_denominacion:
					paramName = "@ta211_denominacion";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
                case enumDBFields.tablaTipoDocumento:
                    paramName = "@TABLA";
                    paramType = SqlDbType.Structured;
                    paramSize = -1;
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
