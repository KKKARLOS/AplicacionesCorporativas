using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Shared;

namespace IB.Progress.DAL
{
    internal class Profesional
    {
        #region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t001_codred = 1,
            t001_idficepi = 2,            
            t001_apellido1 = 3,
            t001_apellido2 = 4,
            t001_nombre = 5,
            t001_evalprogress = 6,
            t001_validoEvalProgress= 7,
            perfilApl = 8,
            evaluadoroevaluado = 9,
            t001_idficepi_visualizador = 10,
            t001_idficepi_entrada = 11,
            t001_idficepi_evaluadoractual = 12,
            t001_idficepi_encurso = 13,
            t001_idficepi_evaluado_actual = 14,
            t001_idficepi_evaluadoract = 15,
            t001_idficepi_interesado = 16
        }

        internal Profesional(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public Profesional()
        {
            
			//lo dejo pero de momento no se usa
        }
		
		#endregion

        #region funciones publicas
        /// <summary>
        /// Obtiene los datos del profesional conectado
        /// </summary>
        /// <param name="t001_codred"></param>
        /// <returns></returns>
        internal Models.Profesional ObtenerDatosLogin(string t001_codred)
        {
            Models.Profesional oProfesional = new Models.Profesional();
            
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.t001_codred, t001_codred.ToString())					
				};
                dr = cDblib.DataReader("PRO_LOGIN", dbparams);

                if (dr.Read()) {
                    oProfesional.bIdentificado = true;
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"].ToString());
                    oProfesional.nombre = dr["profesional"].ToString();
                    oProfesional.Sexo = dr["t001_sexo"].ToString();
                    oProfesional.T001_nombre = dr["t001_nombre"].ToString(); 
                    oProfesional.nombrecorto = dr["nombrecorto"].ToString();
                    oProfesional.nombrelargo = dr["nombrelargo"].ToString();                                                               
                    oProfesional.Correo = dr["direccioncorreo"].ToString();
                    oProfesional.Codred = t001_codred;
                    if (!Convert.IsDBNull(dr["T004_desrol"]))
                        oProfesional.T004_desrol = dr["T004_desrol"].ToString();
                }
                return oProfesional;
            }

            catch (Exception ex) {
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
        /// Obtiene todos los profesionales internos dados de alta
        /// </summary>
        /// <param name="t001_apellido1"></param>
        /// <param name="t001_apellido2"></param>
        /// <param name="t001_nombre"></param>
        /// <returns></returns>
        internal List<Models.Profesional> ObtenerFicepi(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            //Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };
                dr = cDblib.DataReader("PRO_GETFICEPI", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["profesional"]);
                    oProfesional.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProfesional.correo_profesional = Convert.ToString(dr["correo_profesional"]);                    
                    oProfesional.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
                    
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


        internal List<Models.Profesional> getSeleccionarEvaluador(int t001_evaluado_actual, int t001_evaluador_actual, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            //Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t001_idficepi_evaluado_actual, t001_evaluado_actual),
                    Param(enumDBFields.t001_idficepi_evaluadoractual, t001_evaluador_actual),
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };
                dr = cDblib.DataReader("PRO_GETSELECCIONAREVALUADOR", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["profesional"]);
                    oProfesional.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProfesional.correo_profesional = Convert.ToString(dr["correo_profesional"]);
                    oProfesional.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);

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


        internal List<Models.Profesional> getFicepi_Evaluadordestino(int t001_idficepi_interesado, int t001_idficepi_evaluadoractual, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            //Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t001_idficepi_interesado, t001_idficepi_interesado ),
                    Param(enumDBFields.t001_idficepi_evaluadoract, t001_idficepi_evaluadoractual ),
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };
                dr = cDblib.DataReader("PRO_GETFICEPI_EVALUADORDESTINO", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["profesional"]);
                    oProfesional.nombreprofesional = Convert.ToString(dr["nombreprofesional"]);
                    oProfesional.correo_profesional = Convert.ToString(dr["correo_profesional"]);                    
                    oProfesional.nombreapellidosprofesional = Convert.ToString(dr["nombreapellidosprofesional"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
                    
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




        

        /// <summary>
        /// Comprueba si el evaluador destino es correcto
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <param name="t001_evalprogress"></param>
        /// <returns></returns>
        internal Models.Profesional validaProgress(int t001_idficepi, int t001_evalprogress)
        {
            Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString()),
                    Param(enumDBFields.t001_evalprogress, t001_evalprogress.ToString())                    					
                };

                dr = cDblib.DataReader("PRO_VALIDAEVALPROGRESS", dbparams);
                if (dr.Read())
                {
                    oProfesional.validoEvalProgress = bool.Parse(dr["valido"].ToString());
                    
                }
                return oProfesional;


            }
            catch (Exception ex)
            {
                throw new IBException(121, "No se ha podido validar al profesional.", ex);
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
        /// Obtiene los Roles que tiene un FICEPI para PROGRESS
        /// </summary>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        internal List<string> ObtenerRoles(int t001_idficepi)
        {
            List<string> returnList = new List<string>();
            Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString())					
                };
                dr = cDblib.DataReader("PRO_FIGURAS", dbparams);
                while (dr.Read())
                {
                    returnList.Add(dr["figura"].ToString());
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

        /// <summary>
        /// Obtiene los profesionales evaluadores o que figuren como evaluador en alguna valoración
        /// </summary>
        /// <param name="t001_apellido1"></param>
        /// <param name="t001_apellido2"></param>
        /// <param name="t001_nombre"></param>
        /// <returns></returns>
        internal List<Models.Profesional> ObtenerEvaluadores(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            //Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };
                dr = cDblib.DataReader("PRO_FICEVALUADORES", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
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



        internal List<Models.Profesional> ObtenerEvaluadoresEstadisticas(int t001_idficepi, string perfilApl, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString()),
                    Param(enumDBFields.perfilApl, perfilApl.ToString()),
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };
                dr = cDblib.DataReader("PRO_FICEVALUADORES_ESTADISTICAS", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
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


        /// <summary>
        /// Obtiene los profesionales evaluadores o que figuren como evaluador en alguna valoración
        /// </summary>
        /// <param name="t001_apellido1"></param>
        /// <param name="t001_apellido2"></param>
        /// <param name="t001_nombre"></param>
        /// <returns></returns>
        internal List<Models.Profesional> getFic(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };

                dr = cDblib.DataReader("PRO_FICEVALUADORES", dbparams);
                
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
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


        internal List<Models.Profesional> getFicProfesionales(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };

                dr = cDblib.DataReader("PRO_FICPROFESIONALES", dbparams);

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
                    oProfesional.Codred = Convert.ToString(dr["t001_codred"]);
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


        internal List<Models.Profesional> getEvaluadosDeMiEquipo(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t001_idficepi, idficepi),
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };

                dr = cDblib.DataReader("PRO_MIEQUIPO_CONFORZADOS", dbparams);

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["evaluado"]);
                    //oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
                    //oProfesional.Codred = Convert.ToString(dr["t001_codred"]);
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


        internal List<Models.Profesional> getEvaluadoresDeMiEquipo(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t001_idficepi, idficepi),
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };

                dr = cDblib.DataReader("PRO_MIEQUIPODEEVALUADORES_CONFORZADOS", dbparams);

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["evaluador"]);
                    //oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
                    //oProfesional.Codred = Convert.ToString(dr["t001_codred"]);
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



        internal List<Models.Profesional> getFicProfesionales_Reconexion(int idficepi_Entrada, int idficepi_encurso, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.t001_idficepi_entrada, idficepi_Entrada),
                    Param(enumDBFields.t001_idficepi_encurso, idficepi_encurso ),

                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };

                dr = cDblib.DataReader("PRO_FICPROFESIONALES_RECONEXION", dbparams);

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
                    oProfesional.Sexo = Convert.ToString(dr["t001_sexo"]);
                    oProfesional.Codred = Convert.ToString(dr["t001_codred"]);
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


        internal List<Models.Profesional> getProfesionalesVisualizadores(int t001_idficepi_visualizador)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi_visualizador, t001_idficepi_visualizador)                   
                };
                dr = cDblib.DataReader("PRO_GETPROFESIONALES", dbparams);

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["profesional"]);                    
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

        internal List<Models.Profesional> getProfesionalesVisualizadores2(int t001_idficepi_visualizador)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi_visualizador, t001_idficepi_visualizador)                   
                };
                dr = cDblib.DataReader("PRO_GETVISUALIZADORES", dbparams);

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["profesional"]);                    
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

        

        /// <summary>
        /// Obtiene los evaluadores dependientes de un profesional evaluador, a cualquier nivel, más él mismo
        /// </summary>
        /// <param name="idficepi"></param>
        /// <returns></returns>
        internal List<Models.Profesional> FICEVALUADORESDEPENDIENTES(int idficepi)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi, idficepi.ToString())                   
                };

                dr = cDblib.DataReader("PRO_FICEVALUADORESDEPENDIENTES", dbparams);

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
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

        /// <summary>
        /// Obtiene los evaluadores dependientes de un profesional evaluador, a cualquier nivel, más él mismo
        /// </summary>
        /// <param name="idficepi"></param>
        /// <param name="t001_apellido1"></param>
        /// <param name="t001_apellido2"></param>
        /// <param name="t001_nombre"></param>
        /// <returns></returns>
        internal List<Models.Profesional> EvaluadoresArbol(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.t001_idficepi, idficepi.ToString()),
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };

                
                dr = cDblib.DataReader("PRO_FICEVALUADORESDEPENDIENTES", dbparams);
                

                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
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


        internal List<Models.Profesional> getEvaluadoresDescendientes(int idficepi)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi, idficepi.ToString())                    
                };


                dr = cDblib.DataReader("PRO_FICEVALUADORESDEPENDIENTES", dbparams);


                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
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

        internal List<Models.Profesional> ObtenerDescendientes(int t001_idficepi, string perfilApl, string t001_apellido1, string t001_apellido2, string t001_nombre, short evaluadoroevaluado)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            //Models.Profesional oProfesional = new Models.Profesional();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
                    Param(enumDBFields.t001_idficepi, t001_idficepi.ToString()),
                    Param(enumDBFields.perfilApl, perfilApl.ToString()),
                    Param(enumDBFields.evaluadoroevaluado, evaluadoroevaluado.ToString()),                    
                    Param(enumDBFields.t001_apellido1, t001_apellido1.ToString()),
                    Param(enumDBFields.t001_apellido2, t001_apellido2.ToString()),
                    Param(enumDBFields.t001_nombre, t001_nombre.ToString())
                };
                dr = cDblib.DataReader("PRO_BUSCARPROFESIONALES_CONSULTAEVALUACIONES", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.nombre = Convert.ToString(dr["nombre"]);
                    oProfesional.Sexo = Convert.ToString(dr["sexo"]);
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

        internal List<Models.Profesional> getAscendientesHastaAprobador(int t001_idficepi)
        {
            Models.Profesional oProfesional = null;
            List<Models.Profesional> returnList = new List<Models.Profesional>();
            
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(enumDBFields.t001_idficepi_interesado, t001_idficepi.ToString())            
                };
                dr = cDblib.DataReader("PRO_GETASCENDIENTESHASTAAPROBADOR", dbparams);
                while (dr.Read())
                {
                    oProfesional = new Models.Profesional();
                    oProfesional.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oProfesional.t001_apellido1 = Convert.ToString(dr["t001_apellido1"]);
                    oProfesional.t001_apellido2 = Convert.ToString(dr["t001_apellido2"]);
                    oProfesional.nombre = Convert.ToString(dr["t001_nombre"]);
                    oProfesional.Correo = Convert.ToString(dr["correo"]);
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
                case enumDBFields.t001_codred:
                    paramName = "@t001_codred";
                    paramType = SqlDbType.VarChar;
                    paramSize = 15;
                    break;

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_apellido1:
                    paramName = "@t001_apellido1";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;

                case enumDBFields.t001_apellido2:
                    paramName = "@t001_apellido2";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;

                case enumDBFields.t001_nombre:
                    paramName = "@t001_nombre";
                    paramType = SqlDbType.VarChar;
                    paramSize = 25;
                    break;

                case enumDBFields.t001_evalprogress:
                    paramName = "@t001_evalprogress";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.perfilApl:
                    paramName = "perfilApl";
                    paramType = SqlDbType.VarChar;
                    paramSize = 5;
                    break;

                case enumDBFields.evaluadoroevaluado:
                    paramName = "@evaluadooevaluador";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 2;
                    break;

                case enumDBFields.t001_idficepi_visualizador:
                    paramName = "@t001_idficepi_visualizador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_entrada:
                    paramName = "@t001_idficepi_entrada";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_evaluadoractual:
                    paramName = "@t001_idficepi_evaluador_actual";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_encurso:
                    paramName = "@t001_idficepi_encurso";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_evaluado_actual:
                    paramName = "@t001_idficepi_evaluado_actual";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;


                case enumDBFields.t001_idficepi_evaluadoract:
                    paramName = "@t001_idficepi_evaluadoractual";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_interesado:
                    paramName = "@t001_idficepi_interesado";
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