using IB.Progress.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de MIEQUIPO
/// </summary>

namespace IB.Progress.DAL
{
    internal class MIEQUIPO
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t001_idficepi = 1,
            entradasentramite = 2,
            confirmequipo = 3
           
        }

        internal MIEQUIPO(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public MIEQUIPO()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas

        /// <summary>
        /// Actualiza la fecha de confirmación de MIEQUIPO
        /// </summary>
        internal int Update(int t001_idficepi)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi, t001_idficepi.ToString())					
                };

                return (int)cDblib.Execute("PRO_PUTCONFIRMACION", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        /// Obtiene todos los integrantes de MIEQUIPO
        /// </summary>
        internal Models.MIEQUIPO CatalogoAbrirEvaluacion(int t001_idficepi)
        {
            //Parámetros de salida
            SqlParameter entradasentramite = null, confirmequipo = null;

            Models.MIEQUIPO miequipo = new Models.MIEQUIPO();
            miequipo.idficepi = t001_idficepi;
            miequipo.profesionales = new List<Models.MIEQUIPO.profesional>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, t001_idficepi.ToString()),
				  
                };

                dr = cDblib.DataReader("PRO_ABRIREVALUACIONES_CAT", dbparams);




                while (dr.Read())
                {
                    miequipo.profesionales.Add(new Models.MIEQUIPO.profesional(int.Parse(dr["t001_idficepi"].ToString()),
                                                                                dr["T001_SEXO"].ToString(),
                                                                                dr["correo"].ToString(),
                                                                                dr["nombreevaluado"].ToString(),
                                                                                dr["nombreevaluador"].ToString(),
                                                                                bool.Parse(dr["evaluacionAbierta"].ToString()), 
                                                                                bool.Parse(dr["evaluacionEnCurso"].ToString()), 
                                                                                ((dr["t937_estadopeticion"] != DBNull.Value) ? (byte?)byte.Parse(dr["t937_estadopeticion"].ToString()) : null), 
                                                                                dr["evaluado"].ToString()));                                                                                                                                                               
                }
                return miequipo;

            }
            catch (Exception ex)
            {
                throw new IBException(100, "Ocurrió un error obteniendo los datos de mi equipo de base de datos.", ex);
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                    //Hasta no hacer el dispose, no obtienen valor los parámetros de salida
                    miequipo.entradasentramite = (entradasentramite != null && entradasentramite.Value != DBNull.Value) ? bool.Parse(entradasentramite.Value.ToString()) : false;
                    miequipo.confirmequipo = (confirmequipo != null && confirmequipo.Value != DBNull.Value) ? (DateTime?)DateTime.Parse(confirmequipo.Value.ToString()) : null;
                }
            }
        }







        /// <summary>
        /// Obtiene  los datos para la pantalla confirmar mi equipo
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        internal Models.MIEQUIPO CatalogoConfirmarMiequipo(int t001_idficepi)
        {
            //Parámetros de salida
            SqlParameter entradasentramite = null, confirmequipo = null;

            Models.MIEQUIPO miequipo = new Models.MIEQUIPO();
            miequipo.idficepi = t001_idficepi;
            miequipo.profesionales = new List<Models.MIEQUIPO.profesional>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, t001_idficepi.ToString()),
				    entradasentramite = Param(ParameterDirection.Output, enumDBFields.entradasentramite, null),
                    confirmequipo = Param(ParameterDirection.Output, enumDBFields.confirmequipo, null)
                };

                dr = cDblib.DataReader("PRO_CONFIRMAREQUIPO", dbparams);




                while (dr.Read())
                {
                    miequipo.profesionales.Add(new Models.MIEQUIPO.profesional(int.Parse(dr["t001_idficepi_evaluado"].ToString()),                                                                                                                                                                                                                                                
                                                                                dr["profesionalevaluado"].ToString(),
                                                                                dr["rolevaluado"].ToString(), 
                                                                                ((dr["t937_estadopeticion"] != DBNull.Value) ? (byte?)byte.Parse(dr["t937_estadopeticion"].ToString()) : null)                                                                                                                                                                
                                                                                ));
                }
                return miequipo;

            }
            catch (Exception ex)
            {
                throw new IBException(100, "Ocurrió un error obteniendo los datos de mi equipo de base de datos.", ex);
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                    //Hasta no hacer el dispose, no obtienen valor los parámetros de salida
                    miequipo.entradasentramite = (entradasentramite != null && entradasentramite.Value != DBNull.Value) ? bool.Parse(entradasentramite.Value.ToString()) : false;
                    miequipo.confirmequipo = (confirmequipo != null && confirmequipo.Value != DBNull.Value) ? (DateTime?)DateTime.Parse(confirmequipo.Value.ToString()) : null;
                }
            }
        }






        /// <summary>
        /// Obtiene el catálogo de tramitación de salidas
        /// </summary>
        internal Models.MIEQUIPO Catalogo(int t001_idficepi)
        {            
            Models.MIEQUIPO miequipo = new Models.MIEQUIPO();
            miequipo.idficepi = t001_idficepi;
            miequipo.profesionales = new List<Models.MIEQUIPO.profesional>();
            IDataReader dr = null;
          
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, t001_idficepi.ToString())				    
                };

                dr = cDblib.DataReader("PRO_TRAMITARSALIDAS_CAT", dbparams);
                
                while (dr.Read())
                {
                    miequipo.profesionales.Add(new Models.MIEQUIPO.profesional(int.Parse(dr["t001_idficepi_evaluado"].ToString()),
                                                                                ((DBNull.Value != dr["idficepievaluadordestino"]) ? int.Parse(dr["idficepievaluadordestino"].ToString()) : 0),
                                                                                dr["nombreprofesional"].ToString(),
                                                                                dr["nombreapellidosprofesional"].ToString(),
                                                                                dr["nombreevaluadordestino"].ToString(),
                                                                                dr["correoevaluadordestino"].ToString(),                                                                                                                                                                
                                                                                dr["t001_sexo_evaluado"].ToString(),
                                                                                dr["correo_evaluado"].ToString(),                                                                                 
                                                                                ((DBNull.Value != dr["t937_idpetcambioresp"]) ? (int?)int.Parse(dr["t937_idpetcambioresp"].ToString()) : null),
                                                                                dr["profesional"].ToString(),                                                                                 
                                                                                ((dr["t937_estadopeticion"] != DBNull.Value) ? (byte?)byte.Parse(dr["t937_estadopeticion"].ToString()) : null),
                                                                                bool.Parse(dr["evaluacionAbierta"].ToString()), 
                                                                                bool.Parse(dr["evaluacionEnCurso"].ToString()), 
                                                                                dr["respdestino"].ToString(), 
                                                                                ((DBNull.Value != dr["t937_fechainipeticion"]) ? DateTime.Parse(dr["t937_fechainipeticion"].ToString()).ToShortDateString() : null),  
                                                                                dr["t937_comentario_resporigen"].ToString(),
                                                                                dr["t937_comentario_respdestino"].ToString(),                                                                                
                                                                                ((DBNull.Value != dr["t937_estadopeticion"]) ? (int?)int.Parse(dr["t937_estadopeticion"].ToString()) : null)));                                                                               
                }
                return miequipo;

            }
            catch (Exception ex)
            {
                throw new IBException(100, "Ocurrió un error obteniendo los datos de mi equipo de base de datos. (Pantalla tramitar salidas)", ex);
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





        internal Models.MIEQUIPO IncoporacionesCAT(int t001_idficepi)
        {
            
            Models.MIEQUIPO miequipo = new Models.MIEQUIPO();
            miequipo.idficepi = t001_idficepi;
            miequipo.profesionales = new List<Models.MIEQUIPO.profesional>();
            miequipo.profesionalesEnTramite = new List<Models.MIEQUIPO.profEntradasTramite>();
            
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, t001_idficepi.ToString()),				    
                };

                dr = cDblib.DataReader("PRO_GESTIONICORPORACIONES_CAT", dbparams);

                while (dr.Read())
                {

                    miequipo.profesionales.Add(new Models.MIEQUIPO.profesional(int.Parse(dr["t001_idficepi"].ToString()),
                                                                                 dr["profesional"].ToString()));                                                                                                                                                                                                                                                                    
                }


                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        miequipo.profesionalesEnTramite.Add(new Models.MIEQUIPO.profEntradasTramite(int.Parse(dr["t001_idficepi"].ToString()),
                                                                                int.Parse(dr["idficepiresporigen"].ToString()),
                                                                                int.Parse(dr["t937_idpetcambioresp"].ToString()),
                                                                                dr["nombreresporigen"].ToString(),
                                                                                dr["nombreinteresado"].ToString(),
                                                                                dr["nombreapellidosinteresado"].ToString(),
                                                                                dr["correointeresado"].ToString(),
                                                                                dr["correoresporigen"].ToString(),
                                                                                dr["interesado"].ToString(),                                                                                
                                                                                ((DBNull.Value != dr["t937_fechainipeticion"]) ? DateTime.Parse(dr["t937_fechainipeticion"].ToString()).ToShortDateString() : null),
                                                                                dr["resporigen"].ToString(),                                                                                                                                                                
                                                                                dr["t937_comentario_resporigen"].ToString(),
                                                                                int.Parse(dr["T001_evalprogress"].ToString())));
                        
                    }


                }

                return miequipo;

            }
            catch (Exception ex)
            {
                throw new IBException(100, "Ocurrió un error obteniendo los datos de mi equipo de base de datos.", ex);
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



        internal List<Models.MIEQUIPO.profPendEval> CatEvalPend(int t001_idficepi)
        {
            List<Models.MIEQUIPO.profPendEval> profPendEval = new List<Models.MIEQUIPO.profPendEval>();
                        
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, t001_idficepi.ToString())
                };

                dr = cDblib.DataReader("PRO_MISEVALUACIONESABIERTASCOMOEVALUADOR", dbparams);

                while (dr.Read())
                {
                    profPendEval.Add(new Models.MIEQUIPO.profPendEval(
                        int.Parse(dr["t930_idvaloracion"].ToString()),
                        dr["profesional"].ToString(),
                        DateTime.Parse(dr["t930_fechaapertura"].ToString()), 
                        int.Parse(dr["t934_idmodeloformulario"].ToString()),
                        dr["correoprofesional"].ToString(), 
                        dr["nombreprofesional"].ToString()
                        ));
                }
                return profPendEval;

            }
            catch (Exception ex)
            {
                throw new IBException(101, "Ocurrió un error obteniendo los datos de mis evaluados con evaluación abierta.", ex);
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

        internal List<Models.MIEQUIPO.profesional_CRol> CatCambioRol(int t001_idficepi)
        {                                    
            List<Models.MIEQUIPO.profesional_CRol> prof_CRol = new List<Models.MIEQUIPO.profesional_CRol>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, t001_idficepi.ToString())				    
                };

                dr = cDblib.DataReader("PRO_TRAMITARCAMBIOROL_CAT", dbparams);

                while (dr.Read())
                {
                    prof_CRol.Add(new Models.MIEQUIPO.profesional_CRol(
                                ((dr["t940_idtramitacambiorol"] != DBNull.Value) ? int.Parse(dr["t940_idtramitacambiorol"].ToString()) : -1),
                                  int.Parse(dr["t001_idficepi_evaluado"].ToString()),
                                  dr["nombreapellidosprofesional"].ToString(),
                                  dr["t001_sexo_evaluado"].ToString(),
                                  dr["correo_evaluado"].ToString(),                                 
                                  dr["profesional"].ToString(),
                                  dr["nombreprofesional"].ToString(),                                  
                                  ((dr["rolactual_evaluado"] != DBNull.Value) ? dr["rolactual_evaluado"].ToString() : ""),                        
                                  ((dr["rolpropuesto_evaluado"] != DBNull.Value) ? dr["rolpropuesto_evaluado"].ToString() : ""),                                  
                                  dr["promotor"].ToString(),
                                  ((DBNull.Value != dr["t940_fechaproposicion"]) ? DateTime.Parse(dr["t940_fechaproposicion"].ToString()).ToShortDateString() : null),
                                  ((dr["t937_estadopeticion"] != DBNull.Value) ? (byte?)byte.Parse(dr["t937_estadopeticion"].ToString()) : null), 
                                  ((dr["t940_motivopropuesto"] != DBNull.Value) ? dr["t940_motivopropuesto"].ToString() : ""),
                                  ((dr["t940_resolucion"] != DBNull.Value) ? dr["t940_resolucion"].ToString() : null)                                  
                                  ));
                }


                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        prof_CRol.Add(new Models.MIEQUIPO.profesional_CRol
                            (
                                dr["nombreaprobador"].ToString(),
                                dr["correoaprobador"].ToString(),
                                dr["nombreapellidosaprobador"].ToString()
                            ));
                                 
                    }


                }




                return prof_CRol;

            }
            catch (Exception ex)
            {
                throw new IBException(101, "Ocurrió un error obteniendo los datos de mis evaluados para el cambio de rol.", ex);
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
        private SqlParameter Param(ParameterDirection paramDirection, enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            

            switch (dbField)
            {
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.entradasentramite:
                    paramName = "@entradasentramite";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;                    
                    break;

                case enumDBFields.confirmequipo:
                    paramName = "@confirmequipo";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
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
