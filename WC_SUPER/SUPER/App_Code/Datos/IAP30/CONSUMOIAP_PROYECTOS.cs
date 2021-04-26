using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for CONSUMOIAP_PROYECTOS
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class CONSUMOIAP_PROYECTOS 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t314_idusuario = 1,
            dDesde = 2,
            dHasta = 3
        }

        internal CONSUMOIAP_PROYECTOS(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
		#region funciones publicas
/*		/// <summary>
        /// Inserta un CONSUMOIAP_PROYECTOS
        /// </summary>
		internal int Insert(Models.CONSUMOIAP_PROYECTOS oCONSUMOIAP)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t305_idproyectosubnodo, oCONSUMOIAP.t305_idproyectosubnodo),
					Param(enumDBFields.t305_seudonimo, oCONSUMOIAP.t305_seudonimo),
					Param(enumDBFields.t301_idproyecto, oCONSUMOIAP.t301_idproyecto)
				};

				return (int)cDblib.Execute("SUPER_CONSUMOIAP_INS", dbparams);
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
		/// <summary>
        /// Obtiene un CONSUMOIAP_PROYECTOS a partir del id
        /// </summary>
        internal Models.CONSUMOIAP_PROYECTOS Select()
        {
            Models.CONSUMOIAP_PROYECTOS oCONSUMOIAP_PROYECTOS = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[0];

				dr = cDblib.DataReader("SUPER_CONSUMOIAP_SEL", dbparams);
				if (dr.Read())
				{
					oCONSUMOIAP_PROYECTOS = new Models.CONSUMOIAP_PROYECTOS();
					oCONSUMOIAP_PROYECTOS.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
					if(!Convert.IsDBNull(dr["t305_seudonimo"]))
						oCONSUMOIAP_PROYECTOS.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
					oCONSUMOIAP_PROYECTOS.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);

				}
				return oCONSUMOIAP_PROYECTOS;
				
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
        /// Actualiza un CONSUMOIAP_PROYECTOS a partir del id
        /// </summary>
        /// 
        /*
		internal int Update(Models.CONSUMOIAP_PROYECTOS oCONSUMOIAP_PROYECTOS)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t305_idproyectosubnodo, oCONSUMOIAP_PROYECTOS.t305_idproyectosubnodo),
					Param(enumDBFields.t305_seudonimo, oCONSUMOIAP_PROYECTOS.t305_seudonimo),
					Param(enumDBFields.t301_idproyecto, oCONSUMOIAP_PROYECTOS.t301_idproyecto)
				};
                           
				return (int)cDblib.Execute("SUPER_CONSUMOIAP_UPD", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Elimina un CONSUMOIAP_PROYECTOS a partir del id
        /// </summary>
        internal int Delete()
        {
			try
			{
				
            
				return (int)cDblib.Execute("SUPER_CONSUMOIAP_DEL", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
        /// Obtiene todos los CONSUMOIAP_PROYECTOS
        /// </summary>
		internal List<Models.CONSUMOIAP_PROYECTOS> Catalogo(Models.CONSUMOIAP_PROYECTOS oCONSUMOIAP_PROYECTOSFilter)
		{
			Models.CONSUMOIAP_PROYECTOS oCONSUMOIAP_PROYECTOS = null;
            List<Models.CONSUMOIAP_PROYECTOS> lst = new List<Models.CONSUMOIAP_PROYECTOS>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t305_idproyectosubnodo, oGETPROYECTOS_CONSUMOIAP_PROYECTOSFilter.t305_idproyectosubnodo),
					Param(enumDBFields.t305_seudonimo, oGETPROYECTOS_CONSUMOIAP_PROYECTOSFilter.t305_seudonimo),
					Param(enumDBFields.t301_idproyecto, oGETPROYECTOS_CONSUMOIAP_PROYECTOSFilter.t301_idproyecto)
				};

				dr = cDblib.DataReader("SUPER_CONSUMOIAP_CAT", dbparams);
				while (dr.Read())
				{
					oCONSUMOIAP_PROYECTOS = new Models.CONSUMOIAP_PROYECTOS();
					oCONSUMOIAP_PROYECTOS.t305_idproyectosubnodo=Convert.ToInt32(dr["t305_idproyectosubnodo"]);
					if(!Convert.IsDBNull(dr["t305_seudonimo"]))
						oCONSUMOIAP_PROYECTOS.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
					oCONSUMOIAP_PROYECTOS.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);

                    lst.Add(oCONSUMOIAP_PROYECTOS);

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
		*/
        public List<Models.CONSUMOIAP_PROYECTOS> Catalogo(int t314_idusuario, DateTime dDesde, DateTime dHasta)
        {
            Models.CONSUMOIAP_PROYECTOS oCONSUMOIAP_PROYECTOS = null;
            List<Models.CONSUMOIAP_PROYECTOS> lst = new List<Models.CONSUMOIAP_PROYECTOS>();
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t314_idusuario, t314_idusuario),
					Param(enumDBFields.dDesde, dDesde),
					Param(enumDBFields.dHasta, dHasta)
				};


                dr = cDblib.DataReader("[SUP_GETPROYECTOS_CONSUMOIAP]", dbparams);
                while (dr.Read())
                {
                    oCONSUMOIAP_PROYECTOS = new Models.CONSUMOIAP_PROYECTOS();
                    oCONSUMOIAP_PROYECTOS.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    if (!Convert.IsDBNull(dr["t305_seudonimo"]))
                        oCONSUMOIAP_PROYECTOS.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oCONSUMOIAP_PROYECTOS.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);

                    lst.Add(oCONSUMOIAP_PROYECTOS);

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
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
					paramType = SqlDbType.DateTime;
					paramSize = 103;
					break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
					paramType = SqlDbType.DateTime;
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
