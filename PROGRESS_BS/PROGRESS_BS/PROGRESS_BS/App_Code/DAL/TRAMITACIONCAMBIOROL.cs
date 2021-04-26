using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TRAMITACIONCAMBIOROL
/// </summary>

namespace IB.Progress.DAL 
{
    
    internal class TramitacionCambioRol 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t940_idtramitacambiorol = 1,
			t001_idficepi_promotor = 2,
			t001_idficepi_interesado = 3,
			t004_idrol_actual = 4,
			t004_idrol_propuesto = 5,
			t940_motivopropuesto = 6,
			t940_fechaproposicion = 7,
            idficepi_entrada = 8,
            t001_idficepi = 9,
            t940_motivorechazo = 10,
            t001_idficepi_aprobador = 11,
            t940_resolucion = 12,
            t001_idficepi_ultmodificador = 13,
            t001_idficepi_conectado = 14,
            t001_idficepi_aprobador_adm = 15,
            n_solicitudes_APA = 16,
            n_solicitudes_DPA = 17,
            n_solicitudes_ASBY = 18,
            n_solicitudes_DSBY = 19
            
        }

        internal TramitacionCambioRol(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
		
		public TramitacionCambioRol()
        {
            
			//lo dejo pero de momento no se usa
        }
		
		#endregion
	
		#region funciones publicas

       

        internal List<Models.TramitacionCambioRol> catalogoSolicitudes(int t001_idficepi)
        {
            Models.TramitacionCambioRol oProfesional = null;
            List<Models.TramitacionCambioRol> returnList = new List<Models.TramitacionCambioRol>();
            //Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input, enumDBFields.idficepi_entrada, t001_idficepi.ToString())                                      
                };
                dr = cDblib.DataReader("PRO_SOLICITUDES_CAMBIOROL_CAT", dbparams);
                while (dr.Read())
                {                    
                    oProfesional = new Models.TramitacionCambioRol();
                    if (!Convert.IsDBNull(dr["t940_idtramitacambiorol"]))
                        oProfesional.t940_idtramitacambiorol = int.Parse(dr["t940_idtramitacambiorol"].ToString());
                    if (!Convert.IsDBNull(dr["t001_idficepi_interesado"]))
                        oProfesional.t001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());

                    if (!Convert.IsDBNull(dr["nombre_promotor"]))
                        oProfesional.nombre_promotor = dr["nombre_promotor"].ToString();

                    if (!Convert.IsDBNull(dr["correo_promotor"]))
                        oProfesional.correoresporigen = dr["correo_promotor"].ToString();

                    if (!Convert.IsDBNull(dr["correo_interesado"]))
                        oProfesional.correointeresado = dr["correo_interesado"].ToString();

                    if (!Convert.IsDBNull(dr["nombreapellidos_interesado"]))
                        oProfesional.nombreapellidos_interesado = dr["nombreapellidos_interesado"].ToString();

                    if (!Convert.IsDBNull(dr["nombre_interesado"]))
                        oProfesional.nombre_interesado = dr["nombre_interesado"].ToString();

                    if (!Convert.IsDBNull(dr["nombreapellidos_promotor"]))
                        oProfesional.nombreapellidos_promotor = dr["nombreapellidos_promotor"].ToString();

                    if (!Convert.IsDBNull(dr["idrol"]))
                        oProfesional.t004_idrol_propuesto = byte.Parse(dr["idrol"].ToString());

                    oProfesional.Nombrecorto = dr["nombre_interesado"].ToString();
                    oProfesional.Profesional = dr["profesional_interesado"].ToString();
                    oProfesional.t940_desrolActual = dr["RolAntiguo"].ToString();
                    oProfesional.t940_desrolPropuesto = dr["RolPropuesto"].ToString();

                    if (!Convert.IsDBNull(dr["t940_fechaproposicion"]))
                        oProfesional.t940_fechaproposicion = DateTime.Parse( dr["t940_fechaproposicion"].ToString());
                    oProfesional.t940_motivopropuesto = dr["t940_motivopropuesto"].ToString();
                    
                    returnList.Add(oProfesional);
                }
                return returnList;
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

        internal List<Models.TramitacionCambioRol> getSolicitudesSegunEstado(char t940_resolucion, int t001_idficepi)
        {
            Models.TramitacionCambioRol oProfesional = null;
            List<Models.TramitacionCambioRol> returnList = new List<Models.TramitacionCambioRol>();
            //Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {                    
                    Param(ParameterDirection.Input, enumDBFields.t940_resolucion, t940_resolucion.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi_conectado, t001_idficepi.ToString())
                                      
                };
                dr = cDblib.DataReader("PRO_GETSOLICITUDESSEGUNESTADO", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.TramitacionCambioRol();

                    if (!Convert.IsDBNull(dr["t940_idtramitacambiorol"]))
                        oProfesional.t940_idtramitacambiorol = int.Parse(dr["t940_idtramitacambiorol"].ToString());

                    if (!Convert.IsDBNull(dr["t001_idficepi_interesado"]))
                        oProfesional.t001_idficepi_interesado = int.Parse(dr["t001_idficepi_interesado"].ToString());

                    if (!Convert.IsDBNull(dr["Interesado"]))
                        oProfesional.nombre_interesado = dr["Interesado"].ToString();

                    if (!Convert.IsDBNull(dr["t001_idficepi_promotor"]))
                        oProfesional.t001_idficepi_promotor = int.Parse(dr["t001_idficepi_promotor"].ToString());

                    if (!Convert.IsDBNull(dr["promotor"]))
                        oProfesional.nombre_promotor = dr["promotor"].ToString();

                    if (!Convert.IsDBNull(dr["Aprobador"]))
                        oProfesional.aprobador = dr["Aprobador"].ToString();

                    if (!Convert.IsDBNull(dr["t001_idficepi_aprobador"]))
                        oProfesional.t001_idficepi_aprobador = Convert.ToInt32(dr["t001_idficepi_aprobador"].ToString());

                    if (!Convert.IsDBNull(dr["t004_idrol_propuesto"]))
                        oProfesional.t004_idrol_propuesto = byte.Parse(dr["t004_idrol_propuesto"].ToString());

                    if (!Convert.IsDBNull(dr["t940_fechaproposicion"]))
                        oProfesional.t940_fechaproposicion = DateTime.Parse(dr["t940_fechaproposicion"].ToString());

                    if (!Convert.IsDBNull(dr["t940_fecharesolucion"]))
                        oProfesional.t940_fecharesolucion = DateTime.Parse(dr["t940_fecharesolucion"].ToString());

                    if (!Convert.IsDBNull(dr["Rolactual"]))
                        oProfesional.t940_desrolActual = dr["Rolactual"].ToString();

                    if (!Convert.IsDBNull(dr["Rolpropuesto"]))
                        oProfesional.t940_desrolPropuesto = dr["Rolpropuesto"].ToString();

                    if (!Convert.IsDBNull(dr["CorreoPromotor"]))
                        oProfesional.CorreoPromotor = dr["CorreoPromotor"].ToString();

                    if (!Convert.IsDBNull(dr["CorreoInteresado"]))
                        oProfesional.correointeresado = dr["CorreoInteresado"].ToString();

                    if (!Convert.IsDBNull(dr["CorreoAprobador"]))
                        oProfesional.CorreoAprobador= dr["CorreoAprobador"].ToString();

                    if (!Convert.IsDBNull(dr["t940_motivopropuesto"]))
                        oProfesional.t940_motivopropuesto = dr["t940_motivopropuesto"].ToString();

                    if (!Convert.IsDBNull(dr["t940_motivorechazo"]))
                        oProfesional.t940_motivorechazo = dr["t940_motivorechazo"].ToString();

                    if (!Convert.IsDBNull(dr["nomCortoPromotor"]))
                        oProfesional.nomCortoPromotor = dr["nomCortoPromotor"].ToString();

                    if (!Convert.IsDBNull(dr["nomCortoAprobador"]))
                        oProfesional.nomCortoAprobador = dr["nomCortoAprobador"].ToString();

                    if (!Convert.IsDBNull(dr["nomCortoInteresado"]))
                        oProfesional.nomCortoInteresado = dr["nomCortoInteresado"].ToString();

                                        
                    returnList.Add(oProfesional);
                }
                return returnList;
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



        internal int CambioEstadoSolicitudCambioRol(int t940_idtramitacambiorol, char t940_resolucion, int t001_idficepi_ultmodificador)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(ParameterDirection.Input, enumDBFields.t940_idtramitacambiorol, t940_idtramitacambiorol.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t940_resolucion, t940_resolucion.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi_ultmodificador, t001_idficepi_ultmodificador.ToString()),
				};

                return (int)cDblib.Execute("PRO_CAMBIOESTADOSOLICITUDCAMBIOROL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Acepta una propuesta de cambio de rol
        /// </summary>
        /// <param name="t940_uid"></param>
        /// <param name="idficepi_interesado"></param>
        /// <param name="t004_idrol_propuesto"></param>
        /// <returns></returns>
        internal void UpdateSolicitudCambioRol(int t940_idtramitacambiorol, int idficepi_interesado, int t004_idrol_propuesto, int t001_idficepi_aprobador)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
					Param(ParameterDirection.Input, enumDBFields.t940_idtramitacambiorol, t940_idtramitacambiorol.ToString())	,
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi_interesado, idficepi_interesado.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t004_idrol_propuesto, t004_idrol_propuesto.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi_aprobador_adm, t001_idficepi_aprobador.ToString()),
				
				};

                cDblib.Execute("PRO_TRAMITACIONCAMBIOROL_UPD_ACEPTAR", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
       
		/// <summary>
        /// Inserta un TRAMITACIONCAMBIOROL
        /// </summary>
		internal string Insert(Models.TRAMITACIONCAMBIOROL_INS oTRAMITACIONCAMBIOROL)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[4] {
					Param(ParameterDirection.Input, enumDBFields.t001_idficepi_promotor, oTRAMITACIONCAMBIOROL.t001_idficepi_promotor.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi_interesado, oTRAMITACIONCAMBIOROL.t001_idficepi_interesado.ToString()),
					Param(ParameterDirection.Input, enumDBFields.t004_idrol_propuesto, oTRAMITACIONCAMBIOROL.t004_idrol_propuesto.ToString()),
					Param(ParameterDirection.Input, enumDBFields.t940_motivopropuesto, oTRAMITACIONCAMBIOROL.t940_motivopropuesto)
				};

                return (string)cDblib.Desc("PRO_TRAMITACIONCAMBIOROL_INS", dbparams);
			}
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Elimina un TRAMITACIONCAMBIOROL a partir del id
        /// </summary>
        internal int NoAceptacion(int t940_idtramitacambiorol, string t940_motivorechazo, int t001_idficepi_aprobador)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[3] {
					Param(ParameterDirection.Input, enumDBFields.t940_idtramitacambiorol, t940_idtramitacambiorol.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t940_motivorechazo, t940_motivorechazo.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi_aprobador, t001_idficepi_aprobador.ToString()),
				};

                return (int)cDblib.Execute("PRO_TRAMITACIONCAMBIOROL_UPD_NOACEPTAR", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int Delete(int t940_idtramitacambiorol)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(ParameterDirection.Input,enumDBFields.t940_idtramitacambiorol, t940_idtramitacambiorol.ToString())
                    
				};

                return (int)cDblib.Execute("PRO_TRAMITACIONCAMBIOROL_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal Models.TramitacionCambioRol getCountTiles(Int32 idficepi)
        {
            Models.TramitacionCambioRol oTramitacion = new Models.TramitacionCambioRol();

            //Parámetros de salida
            SqlParameter n_solicitudes_APA = null;
            SqlParameter n_solicitudes_DPA = null;
            SqlParameter n_solicitudes_ASBY = null;
            SqlParameter n_solicitudes_DSBY = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi_conectado, idficepi.ToString()),
                    n_solicitudes_APA = Param(ParameterDirection.Output, enumDBFields.n_solicitudes_APA, "0"),
                    n_solicitudes_DPA = Param(ParameterDirection.Output, enumDBFields.n_solicitudes_DPA, "0"),
                    n_solicitudes_ASBY = Param(ParameterDirection.Output, enumDBFields.n_solicitudes_ASBY, "0"),
                    n_solicitudes_DSBY = Param(ParameterDirection.Output, enumDBFields.n_solicitudes_DSBY, "0")
                };
                cDblib.Execute("PRO_GETNCAMBIOSROLACTIVOS", dbparams);

                oTramitacion.n_solicitudes_APA = Int32.Parse(n_solicitudes_APA.Value.ToString());
                oTramitacion.n_solicitudes_DPA = Int32.Parse(n_solicitudes_DPA.Value.ToString());
                oTramitacion.n_solicitudes_ASBY = Int32.Parse(n_solicitudes_ASBY.Value.ToString());
                oTramitacion.n_solicitudes_DSBY = Int32.Parse(n_solicitudes_DSBY.Value.ToString());

                return oTramitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }


		
		#endregion
		
		#region funciones privadas
        private SqlParameter Param(ParameterDirection paramDirection, enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;

			switch (dbField)
			{
				case enumDBFields.t940_idtramitacambiorol:
                    paramName = "@t940_idtramitacambiorol";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_idficepi_promotor:
					paramName = "@t001_idficepi_promotor";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_idficepi_interesado:
					paramName = "@t001_idficepi_interesado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t004_idrol_actual:
					paramName = "@t004_idrol_actual";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t004_idrol_propuesto:
					paramName = "@t004_idrol_propuesto";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t940_motivopropuesto:
					paramName = "@t940_motivopropuesto";
                    paramType = SqlDbType.VarChar;
					paramSize = 750;
					break;
				case enumDBFields.t940_fechaproposicion:
					paramName = "@t940_fechaproposicion";
                    paramType = SqlDbType.Date;
					paramSize = 3;
					break;

                case enumDBFields.idficepi_entrada:
                    paramName = "@idficepi_entrada";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 2;
                    break;

                case enumDBFields.t940_motivorechazo:
					paramName = "@t940_motivorechazo";
                    paramType = SqlDbType.VarChar;
					paramSize = 750;
					break;

                case enumDBFields.t001_idficepi_aprobador:
                    paramName = "@t001_idficepi_aprobador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t940_resolucion:
                    paramName = "@t940_resolucion";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;

                case enumDBFields.t001_idficepi_ultmodificador:
                    paramName = "@t001_idficepi_ultmodificador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_conectado:
                    paramName = "@t001_idficepi_conectado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_aprobador_adm:
                    paramName = "@t001_idficepi_aprobador_adm";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.n_solicitudes_APA:
                    paramName = "@n_solicitudes_APA";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.n_solicitudes_ASBY:
                    paramName = "@n_solicitudes_ASBY";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.n_solicitudes_DPA:
                    paramName = "@n_solicitudes_DPA";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.n_solicitudes_DSBY:
                    paramName = "@n_solicitudes_DSBY";
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
