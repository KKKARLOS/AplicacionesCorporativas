using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaRecursos
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class TareaRecursos 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t332_idtarea = 1,
			t314_idusuario = 2,
			t336_ete = 3,
			t336_ffe = 4,
			t336_etp = 5,
			t336_fip = 6,
			t336_ffp = 7,
			t333_idperfilproy = 8,
			t336_estado = 9,
			t336_comentario = 10,
			t336_indicaciones = 11,
			nombreCompleto = 12,
			Pendiente = 13,
			t336_completado = 14,
			t336_notif_exceso = 15,
			Acumulado = 16,
			PrimerConsumo = 17,
			UltimoConsumo = 18,
            nIdRecurso = 19,
            nIdTarea = 20
        }

        internal TareaRecursos(sqldblib.SqlServerSP extcDblib)
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
        /// Establece si una tarea está finalizada por el profesional
        /// </summary>
        internal int SetFinalizacion(Models.TareaRecursos oTareaRecursos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t314_idusuario, oTareaRecursos.t314_idusuario),
                    Param(enumDBFields.t332_idtarea, oTareaRecursos.t332_idtarea),
                    Param(enumDBFields.t336_completado, oTareaRecursos.t336_completado),
                };

                return (int)cDblib.Execute("SUP_FINALIZARIAPU", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un TareaRecursos a partir del id
        /// </summary>
        internal Models.TareaRecursos ObtenerTareaRecurso(Int32 idtarea, int nUsuario)
        {
            Models.TareaRecursos oTareaRecursos = null;
            IDataReader dr = null;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.nIdTarea, idtarea),
                    Param(enumDBFields.nIdRecurso, nUsuario),
                };


                dr = cDblib.DataReader("SUP_TAREARECURSOS", dbparams);
                if (dr.Read())
                {
                    oTareaRecursos = new Models.TareaRecursos();
                    oTareaRecursos.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    oTareaRecursos.t314_idusuario = Convert.ToInt32(dr["t314_idusuario"]);
                    if (!Convert.IsDBNull(dr["t336_ete"]))
                        oTareaRecursos.t336_ete = Convert.ToDouble(dr["t336_ete"]);
                    if (!Convert.IsDBNull(dr["t336_ffe"]))
                        oTareaRecursos.t336_ffe = Convert.ToDateTime(dr["t336_ffe"]);
                    if (!Convert.IsDBNull(dr["t336_etp"]))
                        oTareaRecursos.t336_etp = Convert.ToDouble(dr["t336_etp"]);
                    if (!Convert.IsDBNull(dr["t336_fip"]))
                        oTareaRecursos.t336_fip = Convert.ToDateTime(dr["t336_fip"]);
                    if (!Convert.IsDBNull(dr["t336_ffp"]))
                        oTareaRecursos.t336_ffp = Convert.ToDateTime(dr["t336_ffp"]);
                    if (!Convert.IsDBNull(dr["t333_idperfilproy"]))
                        oTareaRecursos.t333_idperfilproy = Convert.ToInt32(dr["t333_idperfilproy"]);
                    oTareaRecursos.t336_estado = Convert.ToInt32(dr["t336_estado"]);
                    if (!Convert.IsDBNull(dr["t336_comentario"]))
                        oTareaRecursos.t336_comentario = Convert.ToString(dr["t336_comentario"]);
                    if (!Convert.IsDBNull(dr["t336_indicaciones"]))
                        oTareaRecursos.t336_indicaciones = Convert.ToString(dr["t336_indicaciones"]);
                    oTareaRecursos.nombreCompleto = Convert.ToString(dr["nombreCompleto"]);
                    if (!Convert.IsDBNull(dr["Pendiente"]))
                        oTareaRecursos.Pendiente = Convert.ToDouble(dr["Pendiente"]);
                    oTareaRecursos.t336_completado = Convert.ToBoolean(dr["t336_completado"]);
                    oTareaRecursos.t336_notif_exceso = Convert.ToBoolean(dr["t336_notif_exceso"]);
                    if (!Convert.IsDBNull(dr["Acumulado"]))
                        oTareaRecursos.Acumulado = Convert.ToDecimal(dr["Acumulado"]);
                    if (!Convert.IsDBNull(dr["PrimerConsumo"]))
                        oTareaRecursos.PrimerConsumo = Convert.ToDateTime(dr["PrimerConsumo"]);
                    if (!Convert.IsDBNull(dr["UltimoConsumo"]))
                        oTareaRecursos.UltimoConsumo = Convert.ToDateTime(dr["UltimoConsumo"]);

                }
                return oTareaRecursos;

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
				case enumDBFields.t332_idtarea:
					paramName = "@t332_idtarea";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t314_idusuario:
					paramName = "@t314_idusuario";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t336_ete:
					paramName = "@t336_ete";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t336_ffe:
					paramName = "@t336_ffe";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t336_etp:
					paramName = "@t336_etp";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t336_fip:
					paramName = "@t336_fip";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t336_ffp:
					paramName = "@t336_ffp";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t333_idperfilproy:
					paramName = "@t333_idperfilproy";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t336_estado:
					paramName = "@t336_estado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t336_comentario:
					paramName = "@t336_comentario";
					paramType = SqlDbType.VarChar;
					paramSize = 8000;
					break;
				case enumDBFields.t336_indicaciones:
					paramName = "@t336_indicaciones";
					paramType = SqlDbType.VarChar;
					paramSize = 8000;
					break;
				case enumDBFields.nombreCompleto:
					paramName = "@nombreCompleto";
					paramType = SqlDbType.VarChar;
					paramSize = 73;
					break;
				case enumDBFields.Pendiente:
					paramName = "@Pendiente";
					paramType = SqlDbType.Float;
					paramSize = 8;
					break;
				case enumDBFields.t336_completado:
					paramName = "@t336_completado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t336_notif_exceso:
					paramName = "@t336_notif_exceso";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.Acumulado:
					paramName = "@Acumulado";
					paramType = SqlDbType.Decimal;
                    //paramSize = Decimal;
					break;
				case enumDBFields.PrimerConsumo:
					paramName = "@PrimerConsumo";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.UltimoConsumo:
					paramName = "@UltimoConsumo";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
                case enumDBFields.nIdRecurso:
                    paramName = "@nIdRecurso";
                    paramType = SqlDbType.Int;
                    paramSize = 5;
                    break;
                case enumDBFields.nIdTarea:
                    paramName = "@nIdTarea";
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
