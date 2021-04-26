using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DESPLAZAMIENTO
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class DESPLAZAMIENTO 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
			t615_fechoraida = 2,
			t615_fechoravuelta = 3,
            t420_idreferencia = 4
        }

        internal DESPLAZAMIENTO(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un DESPLAZAMIENTO
        /// </summary>
        //internal int Insert(Models.DESPLAZAMIENTO oDESPLAZAMIENTO)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[4] {
        //            Param(enumDBFields.t615_fechoraida, oDESPLAZAMIENTO.t615_fechoraida),
        //            Param(enumDBFields.t615_fechoravuelta, oDESPLAZAMIENTO.t615_fechoravuelta),
        //            Param(enumDBFields.numero_usos, oDESPLAZAMIENTO.numero_usos)
        //        };

        //        return (int)cDblib.Execute("IAP_DESPLAZAMIENTO_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
		/// <summary>
        /// Obtiene un DESPLAZAMIENTO a partir del id
        /// </summary>
        //internal Models.DESPLAZAMIENTO Select()
        //{
        //    Models.DESPLAZAMIENTO oDESPLAZAMIENTO = null;
        //    IDataReader dr = null;

        //    try
        //    {
				

        //        dr = cDblib.DataReader("IAP_DESPLAZAMIENTO_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oDESPLAZAMIENTO = new Models.DESPLAZAMIENTO();
        //            oDESPLAZAMIENTO.t615_iddesplazamiento=Convert.ToInt32(dr["t615_iddesplazamiento"]);
        //            oDESPLAZAMIENTO.t615_destino=Convert.ToString(dr["t615_destino"]);
        //            oDESPLAZAMIENTO.t615_observaciones=Convert.ToString(dr["t615_observaciones"]);
        //            oDESPLAZAMIENTO.t615_fechoraida=Convert.ToDateTime(dr["t615_fechoraida"]);
        //            oDESPLAZAMIENTO.t615_fechoravuelta=Convert.ToDateTime(dr["t615_fechoravuelta"]);
        //            oDESPLAZAMIENTO.numero_usos=Convert.ToInt32(dr["numero_usos"]);

        //        }
        //        return oDESPLAZAMIENTO;
				
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}
		
		/// <summary>
        /// Actualiza un DESPLAZAMIENTO a partir del id
        /// </summary>
        //internal int Update(Models.DESPLAZAMIENTO oDESPLAZAMIENTO)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[6] {
        //            Param(enumDBFields.t615_iddesplazamiento, oDESPLAZAMIENTO.t615_iddesplazamiento),
        //            Param(enumDBFields.t615_destino, oDESPLAZAMIENTO.t615_destino),
        //            Param(enumDBFields.t615_observaciones, oDESPLAZAMIENTO.t615_observaciones),
        //            Param(enumDBFields.t615_fechoraida, oDESPLAZAMIENTO.t615_fechoraida),
        //            Param(enumDBFields.t615_fechoravuelta, oDESPLAZAMIENTO.t615_fechoravuelta),
        //            Param(enumDBFields.numero_usos, oDESPLAZAMIENTO.numero_usos)
        //        };
                           
        //        return (int)cDblib.Execute("IAP_DESPLAZAMIENTO_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
		
		/// <summary>
        /// Elimina un DESPLAZAMIENTO a partir del id
        /// </summary>
        //internal int Delete()
        //{
        //    try
        //    {
				
            
        //        return (int)cDblib.Execute("IAP_DESPLAZAMIENTO_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

		/// <summary>
        /// Obtiene todos los DESPLAZAMIENTO
        /// </summary>
        internal List<Models.DESPLAZAMIENTO> Catalogo(int t314_idusuario, DateTime fec_desde, DateTime fec_hasta, int t420_idreferencia)
		{
			Models.DESPLAZAMIENTO oDESPLAZAMIENTO = null;
            List<Models.DESPLAZAMIENTO> lst = new List<Models.DESPLAZAMIENTO>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.t314_idusuario, t314_idusuario),
					Param(enumDBFields.t615_fechoraida, fec_desde),
					Param(enumDBFields.t615_fechoravuelta, fec_hasta),
					Param(enumDBFields.t420_idreferencia, t420_idreferencia)
				};

                dr = cDblib.DataReader("GVT_DESPLAZAMIENTOSECO_CAT", dbparams);
				while (dr.Read())
				{
					oDESPLAZAMIENTO = new Models.DESPLAZAMIENTO();
					oDESPLAZAMIENTO.t615_iddesplazamiento=Convert.ToInt32(dr["t615_iddesplazamiento"]);
					oDESPLAZAMIENTO.t615_destino=Convert.ToString(dr["t615_destino"]);
					oDESPLAZAMIENTO.t615_observaciones=Convert.ToString(dr["t615_observaciones"]);
					oDESPLAZAMIENTO.t615_fechoraida=Convert.ToDateTime(dr["t615_fechoraida"]);
					oDESPLAZAMIENTO.t615_fechoravuelta=Convert.ToDateTime(dr["t615_fechoravuelta"]);
					oDESPLAZAMIENTO.numero_usos=Convert.ToInt32(dr["numero_usos"]);

                    lst.Add(oDESPLAZAMIENTO);

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
                case enumDBFields.t314_idusuario:
                    paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t615_fechoraida:
                    paramName = "@fec_desde";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t615_fechoravuelta:
                    paramName = "@fec_hasta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
                case enumDBFields.t420_idreferencia:
                    paramName = "@t420_idreferencia";
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
