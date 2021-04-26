using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.IAP30.Models;

/// <summary>
/// Descripción breve de ConsultaFacturabilidad
/// </summary>
/// 
namespace IB.SUPER.IAP30.DAL
{
    internal class ConsultaFacturabilidad
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t001_idficepi = 1,
            dDesde = 2,
            dHasta = 3
        }

        internal ConsultaFacturabilidad(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los datos de la tabla principal de la facturabilidad de consumos IAP
        /// </summary>
        internal List<Models.ConsultaFacturabilidad> Catalogo(int t001_idficepi, DateTime dDesde, DateTime dHasta)
		{
            Models.ConsultaFacturabilidad oConsultaFacturabilidad = null;

            List<Models.ConsultaFacturabilidad> lst = new List<Models.ConsultaFacturabilidad>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[3] {
					Param(enumDBFields.t001_idficepi,t001_idficepi),
					Param(enumDBFields.dDesde, dDesde),
					Param(enumDBFields.dHasta, dHasta)
				};

                dr = cDblib.DataReader("SUP_CONSUMOIAP_FACT", dbparams);
				while (dr.Read())
				{
                    oConsultaFacturabilidad = new Models.ConsultaFacturabilidad();
                    /*
                    oConsultaFacturabilidad.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    oConsultaFacturabilidad.t305_seudonimo = Convert.ToString(dr["t305_seudonimo"]);
                    oConsultaFacturabilidad.t331_despt = Convert.ToString(dr["T331_despt"]);
                    oConsultaFacturabilidad.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oConsultaFacturabilidad.t332_destarea = Convert.ToString(dr["t332_destarea"]);
                    oConsultaFacturabilidad.t332_facturable = (bool)dr["t332_facturable"];
                    oConsultaFacturabilidad.t332_orden = Convert.ToInt32(dr["t332_orden"]);
                    
                    oConsultaFacturabilidad.t334_desfase = Convert.ToString(dr["t334_desfase"]);
                    oConsultaFacturabilidad.t335_desactividad = Convert.ToString(dr["t335_desactividad"]);

                    */
                    oConsultaFacturabilidad.Proyecto = Convert.ToString(dr["Proyecto"]);
                    oConsultaFacturabilidad.Tarea = Convert.ToString(dr["Tarea"]);
                    oConsultaFacturabilidad.Facturable = (bool)dr["Facturable"];
                    

                    oConsultaFacturabilidad.t320_idtipologiaproy = Convert.ToByte(dr["t320_idtipologiaproy"]);
                    oConsultaFacturabilidad.t320_denominacion = Convert.ToString(dr["t320_denominacion"]);
                    oConsultaFacturabilidad.t323_idnaturaleza = Convert.ToInt32(dr["t323_idnaturaleza"]);
                    oConsultaFacturabilidad.t323_denominacion = Convert.ToString(dr["t323_denominacion"]);

                    if (!Convert.IsDBNull(dr["t332_etpl"]))
                        oConsultaFacturabilidad.t332_etpl = Convert.ToDouble(dr["t332_etpl"]);
                    if (!Convert.IsDBNull(dr["t336_etp"]))
                        oConsultaFacturabilidad.t336_etp = Convert.ToDouble(dr["t336_etp"]);
                    if (!Convert.IsDBNull(dr["horas_planificadas_periodo"]))
                        oConsultaFacturabilidad.horas_planificadas_periodo = Convert.ToDouble(dr["horas_planificadas_periodo"]);
                    if (!Convert.IsDBNull(dr["horas_tecnico_periodo"]))
                        oConsultaFacturabilidad.horas_tecnico_periodo = Convert.ToDouble(dr["horas_tecnico_periodo"]);
                    if (!Convert.IsDBNull(dr["horas_otros_periodo"]))
                        oConsultaFacturabilidad.horas_otros_periodo = Convert.ToDouble(dr["horas_otros_periodo"]);
                    if (!Convert.IsDBNull(dr["horas_total_periodo"]))
                        oConsultaFacturabilidad.horas_total_periodo = Convert.ToDouble(dr["horas_total_periodo"]);
                    if (!Convert.IsDBNull(dr["horas_planificadas_finperiodo"]))
                        oConsultaFacturabilidad.horas_planificadas_finperiodo = Convert.ToDouble(dr["horas_planificadas_finperiodo"]);
                    if (!Convert.IsDBNull(dr["horas_tecnico_finperiodo"]))
                        oConsultaFacturabilidad.horas_tecnico_finperiodo = Convert.ToDouble(dr["horas_tecnico_finperiodo"]);
                    if (!Convert.IsDBNull(dr["horas_otros_finperiodo"]))
                        oConsultaFacturabilidad.horas_otros_finperiodo = Convert.ToDouble(dr["horas_otros_finperiodo"]);
                    if (!Convert.IsDBNull(dr["horas_total_finperiodo"]))
                        oConsultaFacturabilidad.horas_total_finperiodo = Convert.ToDouble(dr["horas_total_finperiodo"]);

                    lst.Add(oConsultaFacturabilidad);

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
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
					paramType = SqlDbType.DateTime;
					paramSize = 4;
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