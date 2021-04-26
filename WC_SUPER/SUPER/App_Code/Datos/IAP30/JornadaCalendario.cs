using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for JornadaCalendario
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class JornadaCalendario 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            codUsu = 1,
            idficepi = 2,
            codCal = 3,
            mes = 4,
            anno = 5
        }

        internal JornadaCalendario(sqldblib.SqlServerSP extcDblib)
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
        /// Obtiene todos las JornadaCalendario
        /// </summary>
        internal List<Models.JornadaCalendario> CatalogoJornadas(Int32 codUsu, Int32 codCal, Int32 mes, Int32 anno)
        {
            Models.JornadaCalendario oJornadaCalendario = null;
            List<Models.JornadaCalendario> lst = new List<Models.JornadaCalendario>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
					Param(enumDBFields.codUsu, codUsu),
					Param(enumDBFields.codCal, codCal),
					Param(enumDBFields.mes, mes),
					Param(enumDBFields.anno, anno)
				};

                dr = cDblib.DataReader("SUP_HORASIMPUTADASCALENDARIO_IAP30", dbparams);
                while (dr.Read())
                {
                    oJornadaCalendario = new Models.JornadaCalendario();
                    if (!Convert.IsDBNull(dr["DiaMes"]))
                        oJornadaCalendario.dia = Convert.ToInt32(dr["DiaMes"]);
                    oJornadaCalendario.estilo_festivo = Convert.ToInt32(dr["EsFestivo"]);
                    oJornadaCalendario.esfuerzo = Math.Round(Convert.ToDouble(dr["HorasImputadas"]),2);
                    oJornadaCalendario.horas_estandar = Math.Round(Convert.ToDouble(dr["HorasEstandar"]), 2);
                    oJornadaCalendario.dia_entero = Convert.ToDateTime(dr["Fecha"]);
                    if (!Convert.IsDBNull(dr["HorasPlanificadas"]))
                        oJornadaCalendario.horas_planificadas = Math.Round(Convert.ToDouble(dr["HorasPlanificadas"]), 2);
                    else oJornadaCalendario.horas_planificadas = null;
                    oJornadaCalendario.dia_vacaciones = Convert.ToInt32(dr["EsVacacion"]);
                    lst.Add(oJornadaCalendario);

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
        /// Inserta un JornadaCalendario
        /// </summary>
	/*	internal int Insert(Models.JornadaCalendario oJornadaCalendario)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[8] {
					Param(enumDBFields.dia, oJornadaCalendario.dia),
					Param(enumDBFields.estilo_festivo, oJornadaCalendario.estilo_festivo),
					Param(enumDBFields.esfuerzo, oJornadaCalendario.esfuerzo),
					Param(enumDBFields.horas_estandar, oJornadaCalendario.horas_estandar),
					Param(enumDBFields.dia_entero, oJornadaCalendario.dia_entero),
					Param(enumDBFields.horas_planificadas, oJornadaCalendario.horas_planificadas),
					Param(enumDBFields.dia_festivo, oJornadaCalendario.dia_festivo),
					Param(enumDBFields.dia_vacaciones, oJornadaCalendario.dia_vacaciones)
				};

				return (int)cDblib.Execute("SUPER.IAP30_JornadaCalendario_INS", dbparams);
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Obtiene un JornadaCalendario a partir del id
        /// </summary>
        internal Models.JornadaCalendario Select()
        {
            Models.JornadaCalendario oJornadaCalendario = null;
            IDataReader dr = null;

            try
            {
				

				dr = cDblib.DataReader("SUPER.IAP30_JornadaCalendario_SEL", dbparams);
				if (dr.Read())
				{
					oJornadaCalendario = new Models.JornadaCalendario();
					if(!Convert.IsDBNull(dr["dia"]))
						oJornadaCalendario.dia=Convert.ToInt32(dr["dia"]);
					oJornadaCalendario.estilo_festivo=Convert.ToInt32(dr["estilo_festivo"]);
					oJornadaCalendario.esfuerzo=Convert.ToDouble(dr["esfuerzo"]);
					oJornadaCalendario.horas_estandar=Convert.ToSingle(dr["horas_estandar"]);
					oJornadaCalendario.dia_entero=Convert.ToDateTime(dr["dia_entero"]);
					oJornadaCalendario.horas_planificadas=Convert.ToDouble(dr["horas_planificadas"]);
					oJornadaCalendario.dia_festivo=Convert.ToInt32(dr["dia_festivo"]);
					oJornadaCalendario.dia_vacaciones=Convert.ToInt32(dr["dia_vacaciones"]);

				}
				return oJornadaCalendario;
				
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
        /// Actualiza un JornadaCalendario a partir del id
        /// </summary>
		internal int Update(Models.JornadaCalendario oJornadaCalendario)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[8] {
					Param(enumDBFields.dia, oJornadaCalendario.dia),
					Param(enumDBFields.estilo_festivo, oJornadaCalendario.estilo_festivo),
					Param(enumDBFields.esfuerzo, oJornadaCalendario.esfuerzo),
					Param(enumDBFields.horas_estandar, oJornadaCalendario.horas_estandar),
					Param(enumDBFields.dia_entero, oJornadaCalendario.dia_entero),
					Param(enumDBFields.horas_planificadas, oJornadaCalendario.horas_planificadas),
					Param(enumDBFields.dia_festivo, oJornadaCalendario.dia_festivo),
					Param(enumDBFields.dia_vacaciones, oJornadaCalendario.dia_vacaciones)
				};
                           
				return (int)cDblib.Execute("SUPER.IAP30_JornadaCalendario_UPD", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Elimina un JornadaCalendario a partir del id
        /// </summary>
        internal int Delete()
        {
			try
			{
				
            
				return (int)cDblib.Execute("SUPER.IAP30_JornadaCalendario_DEL", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }
        */
		
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
                              
				case enumDBFields.codUsu:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.idficepi:
					paramName = "@idFicepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.codCal:
					paramName = "@t066_idcal";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.mes:
					paramName = "@nMes";
					paramType = SqlDbType.TinyInt;
					paramSize = 4;
					break;
				case enumDBFields.anno:
					paramName = "@nAnno";
					paramType = SqlDbType.SmallInt;
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
