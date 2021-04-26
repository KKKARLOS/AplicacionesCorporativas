using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ParteActividad
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class ParteActividad 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t314_idusuario = 1,
            t302_idcliente = 2,
            t305_idproyectosubnodo = 3,
            facturable = 4,
            t337_fecha_desde = 5,
			t337_fecha_hasta = 6
        }

        internal ParteActividad(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos los ParteActividad
        /// </summary>
        internal List<Models.ParteActividad> Catalogo(string t314_idusuario, string t302_idcliente, string t305_idproyectosubnodo, Nullable<bool> facturable, DateTime t337_fecha_desde, DateTime t337_fecha_hasta)
		{
			Models.ParteActividad oParteActividad = null;
            List<Models.ParteActividad> lst = new List<Models.ParteActividad>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[6] {
					Param(enumDBFields.t314_idusuario, t314_idusuario),
                    Param(enumDBFields.t302_idcliente, t302_idcliente),
                    Param(enumDBFields.t305_idproyectosubnodo, t305_idproyectosubnodo),
                    Param(enumDBFields.facturable, facturable),
                    Param(enumDBFields.t337_fecha_desde, t337_fecha_desde),
                    Param(enumDBFields.t337_fecha_hasta, t337_fecha_hasta)
				};

                dr = cDblib.DataReader("SUP_PARTEACTIVIDAD_CAT_IAP", dbparams);
				while (dr.Read())
				{
					oParteActividad = new Models.ParteActividad();
					oParteActividad.t332_idtarea=Convert.ToInt32(dr["t332_idtarea"]);
					oParteActividad.t314_idusuario=Convert.ToInt32(dr["t314_idusuario"]);
					oParteActividad.t337_fecha=Convert.ToDateTime(dr["t337_fecha"]);
					oParteActividad.t337_esfuerzo=Convert.ToSingle(dr["t337_esfuerzo"]);
					oParteActividad.t301_idproyecto=Convert.ToInt32(dr["t301_idproyecto"]);
					oParteActividad.t332_destarea=Convert.ToString(dr["t332_destarea"]);
					if(!Convert.IsDBNull(dr["t335_desactividad"]))
						oParteActividad.t335_desactividad=Convert.ToString(dr["t335_desactividad"]);
					if(!Convert.IsDBNull(dr["t334_desfase"]))
						oParteActividad.t334_desfase=Convert.ToString(dr["t334_desfase"]);
					oParteActividad.t331_despt=Convert.ToString(dr["t331_despt"]);
					oParteActividad.t305_seudonimo=Convert.ToString(dr["t305_seudonimo"]);
					oParteActividad.t332_facturable=Convert.ToBoolean(dr["t332_facturable"]);
					if(!Convert.IsDBNull(dr["t324_idmodofact"]))
						oParteActividad.t324_idmodofact=Convert.ToInt32(dr["t324_idmodofact"]);
					oParteActividad.t324_denominacion=Convert.ToString(dr["t324_denominacion"]);
					if(!Convert.IsDBNull(dr["Profesional"]))
						oParteActividad.Profesional=Convert.ToString(dr["Profesional"]);
					oParteActividad.ProfesionalSinAlias=Convert.ToString(dr["ProfesionalSinAlias"]);
					oParteActividad.t302_denominacion=Convert.ToString(dr["t302_denominacion"]);

                    lst.Add(oParteActividad);

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
					paramType = SqlDbType.VarChar;
					paramSize = 8000;
					break;
                case enumDBFields.t302_idcliente:
                    paramName = "@t302_idcliente";
                    paramType = SqlDbType.VarChar;
                    paramSize = 8000;
                    break;
                case enumDBFields.t305_idproyectosubnodo:
                    paramName = "@t305_idproyectosubnodo";
                    paramType = SqlDbType.VarChar;
                    paramSize = 8000;
                    break;
				case enumDBFields.facturable:
					paramName = "@facturable";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
                case enumDBFields.t337_fecha_desde:
                    paramName = "@t337_fecha_desde";
					paramType = SqlDbType.SmallDateTime;
					paramSize = 8;
					break;
                case enumDBFields.t337_fecha_hasta:
                    paramName = "@t337_fecha_hasta";
                    paramType = SqlDbType.SmallDateTime;
                    paramSize = 8;
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
