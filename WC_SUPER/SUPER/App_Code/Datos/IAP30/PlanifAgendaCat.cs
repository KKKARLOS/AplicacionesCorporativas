using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PlanifAgendaCat
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class PlanifAgendaCat 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            IDFicepi = 1,
			StartTime = 2,
			EndTime = 3,
            IDEvento = 4,
            IDFicepiMod = 5,
            Asunto = 6,
            Motivo = 7,
            FechaMod = 8,
            IdTarea = 9,
            Privado= 10,
            Observaciones = 11
        }

        internal PlanifAgendaCat(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un PlanifAgendaCat
        /// </summary>
        internal int Insert(Models.PlanifAgendaCat oPlanifAgendaCat)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[10] {
                    Param(enumDBFields.IDFicepi, oPlanifAgendaCat.Idficepi),
                    Param(enumDBFields.IDFicepiMod, oPlanifAgendaCat.IdficepiMod),
                    Param(enumDBFields.StartTime, oPlanifAgendaCat.StartTime),
                    Param(enumDBFields.EndTime, oPlanifAgendaCat.EndTime),
                    Param(enumDBFields.FechaMod, oPlanifAgendaCat.FechaMod),
                    Param(enumDBFields.IdTarea, oPlanifAgendaCat.IdTarea),
                    Param(enumDBFields.Asunto, oPlanifAgendaCat.Asunto),
                    Param(enumDBFields.Motivo, oPlanifAgendaCat.Motivo),
                    Param(enumDBFields.Privado, oPlanifAgendaCat.Privado),
                    Param(enumDBFields.Observaciones, oPlanifAgendaCat.Observaciones)
                };

                return (int)cDblib.ExecuteScalar("SUP_PLANIFAGENDA_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un PlanifAgendaCat a partir del id
        /// </summary>
        internal Models.PlanifAgendaCat Select(Int32 idEvento)
        {
            Models.PlanifAgendaCat oPlanifAgendaCat = null;
            IDataReader dr = null;

            try
            {


                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.IDEvento, idEvento)
                };

                dr = cDblib.DataReader("SUP_PLANIFAGENDA_O", dbparams);

                if (dr.Read())
                {
                    oPlanifAgendaCat = new Models.PlanifAgendaCat();
                    oPlanifAgendaCat.ID = Convert.ToInt32(dr["t458_idPlanif"]);
                    oPlanifAgendaCat.Motivo = Convert.ToString(dr["t458_motivo"]);
                    oPlanifAgendaCat.Asunto = Convert.ToString(dr["t458_asunto"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"])) oPlanifAgendaCat.IdTarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oPlanifAgendaCat.DesTarea = Convert.ToString(dr["t332_destarea"]);
                    oPlanifAgendaCat.Privado = Convert.ToString(dr["t458_privado"]);
                    oPlanifAgendaCat.Observaciones = Convert.ToString(dr["t458_observaciones"]);
                    oPlanifAgendaCat.Profesional = Convert.ToString(dr["Profesional"]);
                    oPlanifAgendaCat.Promotor = Convert.ToString(dr["Promotor"]);
                    oPlanifAgendaCat.CodRedProfesional = Convert.ToString(dr["codred_profesional"]);
                    oPlanifAgendaCat.CodRedPromotor = Convert.ToString(dr["codred_promotor"]);                    
                    oPlanifAgendaCat.StartTime = Convert.ToDateTime(dr["t458_fechoraini"]);
                    oPlanifAgendaCat.EndTime = Convert.ToDateTime(dr["t458_fechorafin"]);

                }
                return oPlanifAgendaCat;

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
        /// Actualiza un PlanifAgendaCat a partir del id
        /// </summary>
        internal int Update(Models.PlanifAgendaCat oPlanifAgendaCat)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[11] {
                    Param(enumDBFields.IDEvento, oPlanifAgendaCat.ID),
                    Param(enumDBFields.IDFicepi, oPlanifAgendaCat.Idficepi),
                    Param(enumDBFields.IDFicepiMod, oPlanifAgendaCat.IdficepiMod),
                    Param(enumDBFields.StartTime, oPlanifAgendaCat.StartTime),
                    Param(enumDBFields.EndTime, oPlanifAgendaCat.EndTime),
                    Param(enumDBFields.FechaMod, oPlanifAgendaCat.FechaMod),
                    Param(enumDBFields.IdTarea, oPlanifAgendaCat.IdTarea),
                    Param(enumDBFields.Asunto, oPlanifAgendaCat.Asunto),
                    Param(enumDBFields.Motivo, oPlanifAgendaCat.Motivo),
                    Param(enumDBFields.Privado, oPlanifAgendaCat.Privado),
                    Param(enumDBFields.Observaciones, oPlanifAgendaCat.Observaciones)
                };

                return (int)cDblib.Execute("SUP_PLANIFAGENDA_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un PlanifAgendaCat a partir del id
        /// </summary>
        internal int Delete(Int32 idEvento)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.IDEvento, idEvento)
                };
                return (int)cDblib.Execute("SUP_PLANIFAGENDA_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los PlanifAgendaCat
        /// </summary>
        internal List<Models.PlanifAgendaCat> Catalogo(Int32 idficepi, DateTime fechaInicio, DateTime fechaFin)
        {
            Models.PlanifAgendaCat oPlanifAgendaCat = null;
            List<Models.PlanifAgendaCat> lst = new List<Models.PlanifAgendaCat>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.IDFicepi, idficepi),
                    Param(enumDBFields.StartTime, fechaInicio),
                    Param(enumDBFields.EndTime, fechaFin)
                };

                dr = cDblib.DataReader("SUP_PLANIFAGENDA_CAT", dbparams);
                while (dr.Read())
                {
                    oPlanifAgendaCat = new Models.PlanifAgendaCat();
                    oPlanifAgendaCat.ID = Convert.ToInt32(dr["ID"]);
                    oPlanifAgendaCat.Asunto = Convert.ToString(dr["Motivo"]);
                    oPlanifAgendaCat.StartTime = Convert.ToDateTime(dr["StartTime"]);
                    oPlanifAgendaCat.EndTime = Convert.ToDateTime(dr["EndTime"]);

                    lst.Add(oPlanifAgendaCat);

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
        /// Obtiene la disponibilidad de un profesional para un evento planificado
        /// </summary>
        internal bool getDisponibilidad(int t001_idficepi, DateTime t458_fechoraini, DateTime t458_fechorafin, int t458_idPlanif)
        {
            bool disponibilidad = false;
            IDataReader dr = null;

            try
            {


                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.IDFicepi, t001_idficepi),
                    Param(enumDBFields.StartTime, t458_fechoraini),
                    Param(enumDBFields.EndTime, t458_fechorafin),
                    Param(enumDBFields.IDEvento, t458_idPlanif)
                };

                dr = cDblib.DataReader("SUP_DISPONIBILIDADAGENDA", dbparams);

                if (dr.Read())
                {
                    return (Convert.ToInt32(dr["numero"]) == 0) ? true : false;
                }
                return disponibilidad;

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
				case enumDBFields.IDFicepi:
                    paramName = "@t001_idficepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;		
		        case enumDBFields.IDFicepiMod:
                    paramName = "@t001_idficepi_mod";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;	
	            case enumDBFields.FechaMod:
                    paramName = "@t458_fechamod";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.StartTime:
                    paramName = "@t458_fechoraini";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.EndTime:
                    paramName = "@t458_fechorafin";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
                case enumDBFields.IDEvento:
                    paramName = "@t458_idPlanif";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;	
                case enumDBFields.Asunto:
					paramName = "@t458_asunto";
					paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.Motivo:
					paramName = "@t458_motivo";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
                case enumDBFields.IdTarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.Privado:
					paramName = "@t458_privado";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
					break;
				case enumDBFields.Observaciones:
					paramName = "@t458_observaciones";
					paramType = SqlDbType.Text;
					paramSize = 2147483647;
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
    