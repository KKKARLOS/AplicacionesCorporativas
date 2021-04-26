using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;
using IB.Progress.Shared;

/// <summary>
/// Summary description for VALORACIONESPROGRESS
/// </summary>

namespace IB.Progress.DAL 
{
    
    internal class VALORACIONESPROGRESS 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t930_idvaloracion = 1,
            t934_idmodeloformulario = 2,
            t930_fechaapertura = 3,
            t930_fechacierre = 4,
            t001_idficepi_evaluado = 5,
            t001_idficepi_evaluador = 6,
            t930_fecfirmaevaluado = 7,
            t930_fecfirmaevaluador = 8,
            t930_denominacionROL = 9,
            t930_denominacionCR = 10,
            t930_objetoevaluacion = 11,
            t930_denominacionPROYECTO = 12,
            t930_anomes_ini = 13,
            t930_anomes_fin = 14,
            t930_actividad = 15,
            t930_areconocer = 16,
            t930_amejorar = 17,
            t930_gescli = 18,
            t930_liderazgo = 19,
            t930_planorga = 20,
            t930_exptecnico = 21,
            t930_cooperacion = 22,
            t930_iniciativa = 23,
            t930_perseverancia = 24,
            t930_interesescar = 25,
            t930_formacion = 26,
            t930_autoevaluacion = 27,
            t930_especificar = 28,
            t930_atclientes = 29,
            t930_prespuesta = 30,
            t930_crespuesta = 31,
            t930_respdificil = 32,
            t930_valactividad = 33,
            t930_forofichk = 34,
            t930_forofitxt = 35,
            t930_fortecchk = 36,
            t930_fortectxt = 37,
            t930_foratcchk = 38,
            t930_foratctxt = 39,
            t930_forcomchk = 40,
            t930_forcomtxt = 41,
            t930_forvenchk = 42,
            t930_forventxt = 43,
            t930_forespchk = 44,
            t930_foresptxt = 45,
            listaprofesionales = 46,
//Para las búsquedas
//@t930_denominacionCR	varchar(50) = '',
//@t930_denominacionROL	varchar(50) = '',
            fdesde = 47,
            fhasta = 48,
            t001_idficepi = 49,
            figura = 50,        //Posibles valores: 0 (Evaluado), 1(Evaluador)
            profundidad = 51,   //Posibles valores: 1, 2, 3 (>2)
            estado = 52,
            t941_idcolectivo = 53,
            t930_puntuacion = 54,
            lestt930_gescli = 55,
            lestt930_liderazgo = 56,
            lestt930_planorga = 57,
            lestt930_exptecnico = 58,
            lestt930_cooperacion = 59,
            lestt930_iniciativa = 60,
            lestt930_perseverancia = 61,
            estaspectos = 62,
            lestaspectos = 63,
            lestt930_interesescar = 64,
            estreconocer = 65,
            estmejorar = 66,
            lcaut930_gescli	= 67,			//CAU (Orientación al cliente)
            lcaut930_liderazgo= 68,			//CAU (Orientación a resultados)
            lcaut930_planorga= 69,			//CAU (Comunicación)
            lcaut930_exptecnico= 70,		//CAU (Compromiso)
            lcaut930_cooperacion= 71,		//CAU 
            lcaut930_iniciativa	= 72,		//CAU 
            lcaut930_perseverancia	= 73,	//CAU 
            cauaspectos			= 74,		//CAU 
            lcauaspectos		= 75,		//CAU 
            lcaut930_interesescar= 76,		//CAU 
            caumejorar			= 77,		//0 (sin rellenar), 100 (100 caracteres)
            desde = 78,
            hasta = 79,
            profundizacion = 80,
            situacion = 81,
            t001_idficepi_usuario = 82, 
            origen= 83,
            t930_calidad = 84, 
            SelectMejorar = 85,
            SelectSuficiente = 86,
            SelectBueno = 87,
            SelectAlto = 88,
            SelectMejorarCAU = 89,
            SelectSuficienteCAU = 90,
            SelectBuenoCAU = 91,
            SelectAltoCAU = 92, 
            evalValida = 93, 
            evalNoValida = 94,
            ListaIdValoracion = 95,
            idficepi_evaluado = 96,
            idficepi_evaluador = 97,
            alcance = 98,
            mostrarcombo= 99,
            fechaantiguedad  = 100,
            idficepiconectado= 101,
            nuevoestado= 102, 
            temporadaprogress= 103,
            periodoprogress= 104

        }

        internal VALORACIONESPROGRESS(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        internal int CualificarEvaluacion(int idvaloracion, Nullable<bool> puntuacion)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, idvaloracion.ToString()), 
                    Param(ParameterDirection.Input,enumDBFields.evalValida, (puntuacion == null) ? null : puntuacion.ToString())	
				    
				};

                return (int)cDblib.Execute("PRO_PUTCUALIFICACION", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		public VALORACIONESPROGRESS()
        {
            
			//lo dejo pero de momento no se usa
        }
		
		#endregion
	
		#region funciones publicas
		/// <summary>
        /// Inserta un VALORACIONESPROGRESS
        /// </summary>
        internal List<Models.VALORACIONESPROGRESS.ImprimirInsertados> Insert(string listaProfesionales)
        {            
            Models.VALORACIONESPROGRESS.ImprimirInsertados oVALORACIONESPROGRESS = null;
            List<Models.VALORACIONESPROGRESS.ImprimirInsertados> listaDevolver = new List<Models.VALORACIONESPROGRESS.ImprimirInsertados>();
            IDataReader dr = null;

			try
			{
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(ParameterDirection.Input,enumDBFields.listaprofesionales, listaProfesionales)
                    //ListaIdValoracion = Param(ParameterDirection.Output,enumDBFields.ListaIdValoracion,null)
				};

                //cDblib.Execute("PRO_VALORACIONESPROGRESS_INS", dbparams);


                dr = cDblib.DataReader("PRO_VALORACIONESPROGRESS_INS", dbparams);

                while (dr.Read())
                {
                    oVALORACIONESPROGRESS = new Models.VALORACIONESPROGRESS.ImprimirInsertados();
                    oVALORACIONESPROGRESS.t001_idficepi = Convert.ToInt32(dr["T001_idficepi"]);
                    oVALORACIONESPROGRESS.t930_idvaloracion = Convert.ToInt32(dr["t930_idvaloracion"]);
                    oVALORACIONESPROGRESS.t934_idmodeloformulario = Convert.ToInt32(dr["t934_idmodeloformulario"]);


                    listaDevolver.Add(oVALORACIONESPROGRESS);
                }

                return listaDevolver;
				//return (int)cDblib.Execute("PRO_VALORACIONESPROGRESS_INS", dbparams);

                
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
        /// Obtiene un VALORACIONESPROGRESS a partir del id
        /// </summary>
        internal Models.VALORACIONESPROGRESS.formulario_id1 Select(Int32 idficepiconectado, Int32 t930_idvaloracion)
        {
            Models.VALORACIONESPROGRESS.formulario_id1 oVALORACIONESPROGRESS = null;
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(ParameterDirection.Input,enumDBFields.idficepiconectado, idficepiconectado.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, t930_idvaloracion.ToString())
				};

				dr = cDblib.DataReader("PRO_VALORACIONESPROGRESS_SEL", dbparams);
				if (dr.Read())
				{
					oVALORACIONESPROGRESS = new Models.VALORACIONESPROGRESS.formulario_id1();
					oVALORACIONESPROGRESS.t930_idvaloracion=Convert.ToInt32(dr["t930_idvaloracion"]);
                    oVALORACIONESPROGRESS.estado = Convert.ToString(dr["estado"]);
					oVALORACIONESPROGRESS.t934_idmodeloformulario=Convert.ToInt16(dr["t934_idmodeloformulario"]);
					oVALORACIONESPROGRESS.t930_fechaapertura=Convert.ToDateTime(dr["t930_fechaapertura"]);
					if(!Convert.IsDBNull(dr["t930_fechacierre"]))
						oVALORACIONESPROGRESS.t930_fechacierre=Convert.ToDateTime(dr["t930_fechacierre"]);
					oVALORACIONESPROGRESS.t001_idficepi_evaluado=Convert.ToInt32(dr["t001_idficepi_evaluado"]);
                    oVALORACIONESPROGRESS.evaluado = Convert.ToString(dr["Evaluado"]);
                    oVALORACIONESPROGRESS.firmaevaluado = Convert.ToString(dr["firmaevaluado"]);
					oVALORACIONESPROGRESS.t001_idficepi_evaluador=Convert.ToInt32(dr["t001_idficepi_evaluador"]);
                    oVALORACIONESPROGRESS.evaluador = Convert.ToString(dr["Evaluador"]);
                    oVALORACIONESPROGRESS.firmaevaluador = Convert.ToString(dr["firmaevaluador"]);
					if(!Convert.IsDBNull(dr["t930_fecfirmaevaluado"]))
						oVALORACIONESPROGRESS.t930_fecfirmaevaluado=Convert.ToDateTime(dr["t930_fecfirmaevaluado"]);
					if(!Convert.IsDBNull(dr["t930_fecfirmaevaluador"]))
						oVALORACIONESPROGRESS.t930_fecfirmaevaluador=Convert.ToDateTime(dr["t930_fecfirmaevaluador"]);
					oVALORACIONESPROGRESS.t930_denominacionROL=Convert.ToString(dr["t930_denominacionROL"]);
					oVALORACIONESPROGRESS.t930_denominacionCR=Convert.ToString(dr["t930_denominacionCR"]);
					if(!Convert.IsDBNull(dr["t930_objetoevaluacion"]))
						oVALORACIONESPROGRESS.t930_objetoevaluacion=Convert.ToByte(dr["t930_objetoevaluacion"]);
					oVALORACIONESPROGRESS.t930_denominacionPROYECTO=Convert.ToString(dr["t930_denominacionPROYECTO"]);
                    if (!Convert.IsDBNull(dr["t930_anomes_ini"]))
                        oVALORACIONESPROGRESS.t930_anomes_ini = Convert.ToInt32(dr["t930_anomes_ini"]);
                    if (!Convert.IsDBNull(dr["t930_anomes_fin"]))
                        oVALORACIONESPROGRESS.t930_anomes_fin = Convert.ToInt32(dr["t930_anomes_fin"]);
					if(!Convert.IsDBNull(dr["t930_actividad"]))
                        oVALORACIONESPROGRESS.t930_actividad = Convert.ToByte(dr["t930_actividad"]);
					oVALORACIONESPROGRESS.t930_areconocer=Convert.ToString(dr["t930_areconocer"]);
					oVALORACIONESPROGRESS.t930_amejorar=Convert.ToString(dr["t930_amejorar"]);
					if(!Convert.IsDBNull(dr["t930_gescli"]))
                        oVALORACIONESPROGRESS.t930_gescli = Convert.ToByte(dr["t930_gescli"]);
					if(!Convert.IsDBNull(dr["t930_liderazgo"]))
                        oVALORACIONESPROGRESS.t930_liderazgo = Convert.ToByte(dr["t930_liderazgo"]);
					if(!Convert.IsDBNull(dr["t930_planorga"]))
                        oVALORACIONESPROGRESS.t930_planorga = Convert.ToByte(dr["t930_planorga"]);
					if(!Convert.IsDBNull(dr["t930_exptecnico"]))
                        oVALORACIONESPROGRESS.t930_exptecnico = Convert.ToByte(dr["t930_exptecnico"]);
					if(!Convert.IsDBNull(dr["t930_cooperacion"]))
                        oVALORACIONESPROGRESS.t930_cooperacion = Convert.ToByte(dr["t930_cooperacion"]);
					if(!Convert.IsDBNull(dr["t930_iniciativa"]))
                        oVALORACIONESPROGRESS.t930_iniciativa = Convert.ToByte(dr["t930_iniciativa"]);
					if(!Convert.IsDBNull(dr["t930_perseverancia"]))
                        oVALORACIONESPROGRESS.t930_perseverancia = Convert.ToByte(dr["t930_perseverancia"]);
					if(!Convert.IsDBNull(dr["t930_interesescar"]))
                        oVALORACIONESPROGRESS.t930_interesescar = Convert.ToByte(dr["t930_interesescar"]);
					oVALORACIONESPROGRESS.t930_formacion=Convert.ToString(dr["t930_formacion"]);
					oVALORACIONESPROGRESS.t930_autoevaluacion=Convert.ToString(dr["t930_autoevaluacion"]);
                    if (!Convert.IsDBNull(dr["t930_puntuacion"]))
                        oVALORACIONESPROGRESS.t930_puntuacion = Convert.ToBoolean(dr["t930_puntuacion"].ToString());

                    if (!Convert.IsDBNull(dr["nombreevaluado"]))
                         oVALORACIONESPROGRESS.Nombreevaluado = Convert.ToString(dr["nombreevaluado"]);

                    if (!Convert.IsDBNull(dr["correoevaluado"]))
                        oVALORACIONESPROGRESS.Correoevaluado = Convert.ToString(dr["correoevaluado"]);

                    if (!Convert.IsDBNull(dr["correoevaluador"]))
                        oVALORACIONESPROGRESS.Correoevaluador = Convert.ToString(dr["correoevaluador"]);

                    if (!Convert.IsDBNull(dr["nombreevaluador"]))
                        oVALORACIONESPROGRESS.Nombreevaluador = Convert.ToString(dr["nombreevaluador"]);

                    if (!Convert.IsDBNull(dr["sexoevaluador"]))
                         oVALORACIONESPROGRESS.SexoEvaluador = Convert.ToChar(dr["sexoevaluador"]);
                    if (!Convert.IsDBNull(dr["sexoevaluado"]))
                         oVALORACIONESPROGRESS.SexoEvaluado = Convert.ToChar(dr["sexoevaluado"]);
				}
				return oVALORACIONESPROGRESS;
				
            }
            catch (Exception ex)
            {
                throw new IBException(109, "Ocurrió un error obteniendo los datos de la evaluación de base de datos.", ex);
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

        internal Models.VALORACIONESPROGRESS.formulario_id2 Select2(Int32 idficepiconectado, Int32 t930_idvaloracion)
        {
            Models.VALORACIONESPROGRESS.formulario_id2 oVALORACIONESPROGRESS = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(ParameterDirection.Input,enumDBFields.idficepiconectado, idficepiconectado.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, t930_idvaloracion.ToString())
				};

                dr = cDblib.DataReader("PRO_VALORACIONESPROGRESS_SEL", dbparams);
                if (dr.Read())
                {
                    oVALORACIONESPROGRESS = new Models.VALORACIONESPROGRESS.formulario_id2();
                    oVALORACIONESPROGRESS.t930_idvaloracion = Convert.ToInt32(dr["t930_idvaloracion"]);
                    oVALORACIONESPROGRESS.estado = Convert.ToString(dr["estado"]);
                    oVALORACIONESPROGRESS.t934_idmodeloformulario = Convert.ToInt16(dr["t934_idmodeloformulario"]);
                    oVALORACIONESPROGRESS.t930_fechaapertura = Convert.ToDateTime(dr["t930_fechaapertura"]);
                    if (!Convert.IsDBNull(dr["t930_fechacierre"]))
                        oVALORACIONESPROGRESS.t930_fechacierre = Convert.ToDateTime(dr["t930_fechacierre"]);
                    oVALORACIONESPROGRESS.t001_idficepi_evaluado = Convert.ToInt32(dr["t001_idficepi_evaluado"]);
                    oVALORACIONESPROGRESS.evaluado = Convert.ToString(dr["Evaluado"]);
                    oVALORACIONESPROGRESS.firmaevaluado = Convert.ToString(dr["firmaevaluado"]);
                    oVALORACIONESPROGRESS.t001_idficepi_evaluador = Convert.ToInt32(dr["t001_idficepi_evaluador"]);
                    oVALORACIONESPROGRESS.evaluador = Convert.ToString(dr["Evaluador"]);
                    oVALORACIONESPROGRESS.firmaevaluador = Convert.ToString(dr["firmaevaluador"]);
                    if (!Convert.IsDBNull(dr["t930_fecfirmaevaluado"]))
                        oVALORACIONESPROGRESS.t930_fecfirmaevaluado = Convert.ToDateTime(dr["t930_fecfirmaevaluado"]);
                    if (!Convert.IsDBNull(dr["t930_fecfirmaevaluador"]))
                        oVALORACIONESPROGRESS.t930_fecfirmaevaluador = Convert.ToDateTime(dr["t930_fecfirmaevaluador"]);
                    oVALORACIONESPROGRESS.t930_denominacionROL = Convert.ToString(dr["t930_denominacionROL"]);
                    oVALORACIONESPROGRESS.t930_denominacionCR = Convert.ToString(dr["t930_denominacionCR"]);
                    if (!Convert.IsDBNull(dr["t930_atclientes"]))
                        oVALORACIONESPROGRESS.t930_atclientes = Convert.ToByte(dr["t930_atclientes"]);
                    if (!Convert.IsDBNull(dr["t930_prespuesta"]))
                        oVALORACIONESPROGRESS.t930_prespuesta = Convert.ToByte(dr["t930_prespuesta"]);
                    if (!Convert.IsDBNull(dr["t930_crespuesta"]))
                        oVALORACIONESPROGRESS.t930_crespuesta = Convert.ToByte(dr["t930_crespuesta"]);
                    if (!Convert.IsDBNull(dr["t930_respdificil"]))
                        oVALORACIONESPROGRESS.t930_respdificil = Convert.ToByte(dr["t930_respdificil"]);
                    if (!Convert.IsDBNull(dr["t930_valactividad"]))
                        oVALORACIONESPROGRESS.t930_valactividad = Convert.ToByte(dr["t930_valactividad"]);
                    oVALORACIONESPROGRESS.t930_amejorar = Convert.ToString(dr["t930_amejorar"]);
                    if (!Convert.IsDBNull(dr["t930_gescli"]))
                        oVALORACIONESPROGRESS.t930_gescli = Convert.ToByte(dr["t930_gescli"]);
                    if (!Convert.IsDBNull(dr["t930_liderazgo"]))
                        oVALORACIONESPROGRESS.t930_liderazgo = Convert.ToByte(dr["t930_liderazgo"]);
                    if (!Convert.IsDBNull(dr["t930_planorga"]))
                        oVALORACIONESPROGRESS.t930_planorga = Convert.ToByte(dr["t930_planorga"]);
                    if (!Convert.IsDBNull(dr["t930_exptecnico"]))
                        oVALORACIONESPROGRESS.t930_exptecnico = Convert.ToByte(dr["t930_exptecnico"]);
                    if (!Convert.IsDBNull(dr["t930_cooperacion"]))
                        oVALORACIONESPROGRESS.t930_cooperacion = Convert.ToByte(dr["t930_cooperacion"]);
                    if (!Convert.IsDBNull(dr["t930_iniciativa"]))
                        oVALORACIONESPROGRESS.t930_iniciativa = Convert.ToByte(dr["t930_iniciativa"]);
                    if (!Convert.IsDBNull(dr["t930_perseverancia"]))
                        oVALORACIONESPROGRESS.t930_perseverancia = Convert.ToByte(dr["t930_perseverancia"]);
                    if (!Convert.IsDBNull(dr["t930_interesescar"]))
                        oVALORACIONESPROGRESS.t930_interesescar = Convert.ToByte(dr["t930_interesescar"]);
                    oVALORACIONESPROGRESS.t930_especificar = Convert.ToString(dr["t930_especificar"]);
                    oVALORACIONESPROGRESS.t930_forofichk = Convert.ToBoolean(dr["t930_forofichk"]);
                    oVALORACIONESPROGRESS.t930_forofitxt = Convert.ToString(dr["t930_forofitxt"]);
                    oVALORACIONESPROGRESS.t930_fortecchk = Convert.ToBoolean(dr["t930_fortecchk"]);
                    oVALORACIONESPROGRESS.t930_fortectxt = Convert.ToString(dr["t930_fortectxt"]);
                    oVALORACIONESPROGRESS.t930_foratcchk = Convert.ToBoolean(dr["t930_foratcchk"]);
                    oVALORACIONESPROGRESS.t930_foratctxt = Convert.ToString(dr["t930_foratctxt"]);
                    oVALORACIONESPROGRESS.t930_forcomchk = Convert.ToBoolean(dr["t930_forcomchk"]);
                    oVALORACIONESPROGRESS.t930_forcomtxt = Convert.ToString(dr["t930_forcomtxt"]);
                    oVALORACIONESPROGRESS.t930_forvenchk = Convert.ToBoolean(dr["t930_forvenchk"]);
                    oVALORACIONESPROGRESS.t930_forventxt = Convert.ToString(dr["t930_forventxt"]);
                    oVALORACIONESPROGRESS.t930_forespchk = Convert.ToBoolean(dr["t930_forespchk"]);
                    oVALORACIONESPROGRESS.t930_foresptxt = Convert.ToString(dr["t930_foresptxt"]);
                    oVALORACIONESPROGRESS.t930_autoevaluacion = Convert.ToString(dr["t930_autoevaluacion"]);
                    

                    if (!Convert.IsDBNull(dr["nombreevaluado"]))
                        oVALORACIONESPROGRESS.Nombreevaluado = Convert.ToString(dr["nombreevaluado"]);

                    if (!Convert.IsDBNull(dr["correoevaluado"]))
                        oVALORACIONESPROGRESS.Correoevaluado = Convert.ToString(dr["correoevaluado"]);

                    if (!Convert.IsDBNull(dr["correoevaluador"]))
                        oVALORACIONESPROGRESS.Correoevaluador = Convert.ToString(dr["correoevaluador"]);

                    if (!Convert.IsDBNull(dr["nombreevaluador"]))
                        oVALORACIONESPROGRESS.Nombreevaluador = Convert.ToString(dr["nombreevaluador"]);

                    if (!Convert.IsDBNull(dr["sexoevaluador"]))
                        oVALORACIONESPROGRESS.SexoEvaluador = Convert.ToChar(dr["sexoevaluador"]);
                    if (!Convert.IsDBNull(dr["sexoevaluado"]))
                        oVALORACIONESPROGRESS.SexoEvaluado = Convert.ToChar(dr["sexoevaluado"]);


                }
                return oVALORACIONESPROGRESS;

            }
            catch (Exception ex)
            {
                throw new IBException(109, "Ocurrió un error obteniendo los datos de la evaluación de base de datos.", ex);
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
        /// Actualiza un VALORACIONESPROGRESS a partir del id
        /// </summary>
		internal int UpdateEvaluador(Models.VALORACIONESPROGRESS.formulario_id1 oVALORACIONESPROGRESS)
        {
			try
			{
                SqlParameter[] dbparams = new SqlParameter[21] {
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, oVALORACIONESPROGRESS.t930_idvaloracion.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t934_idmodeloformulario, oVALORACIONESPROGRESS.t934_idmodeloformulario.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fecfirmaevaluador,(oVALORACIONESPROGRESS.t930_fecfirmaevaluador == null) ? null : oVALORACIONESPROGRESS.t930_fecfirmaevaluador.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionROL, oVALORACIONESPROGRESS.t930_denominacionROL),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionCR, oVALORACIONESPROGRESS.t930_denominacionCR),
					Param(ParameterDirection.Input,enumDBFields.t930_objetoevaluacion, (oVALORACIONESPROGRESS.t930_objetoevaluacion == null) ? null : oVALORACIONESPROGRESS.t930_objetoevaluacion.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionPROYECTO, oVALORACIONESPROGRESS.t930_denominacionPROYECTO),
					Param(ParameterDirection.Input,enumDBFields.t930_anomes_ini, (oVALORACIONESPROGRESS.t930_anomes_ini == null) ? null : oVALORACIONESPROGRESS.t930_anomes_ini.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_anomes_fin, (oVALORACIONESPROGRESS.t930_anomes_fin == null) ? null : oVALORACIONESPROGRESS.t930_anomes_fin.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_actividad, (oVALORACIONESPROGRESS.t930_actividad == null) ? null : oVALORACIONESPROGRESS.t930_actividad.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_areconocer, oVALORACIONESPROGRESS.t930_areconocer),
					Param(ParameterDirection.Input,enumDBFields.t930_amejorar, oVALORACIONESPROGRESS.t930_amejorar),
					Param(ParameterDirection.Input,enumDBFields.t930_gescli, (oVALORACIONESPROGRESS.t930_gescli == null) ? null : oVALORACIONESPROGRESS.t930_gescli.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_liderazgo, (oVALORACIONESPROGRESS.t930_liderazgo == null) ? null : oVALORACIONESPROGRESS.t930_liderazgo.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_planorga, (oVALORACIONESPROGRESS.t930_planorga == null) ? null : oVALORACIONESPROGRESS.t930_planorga.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_exptecnico, (oVALORACIONESPROGRESS.t930_exptecnico == null) ? null : oVALORACIONESPROGRESS.t930_exptecnico.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_cooperacion, (oVALORACIONESPROGRESS.t930_cooperacion == null) ? null : oVALORACIONESPROGRESS.t930_cooperacion.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_iniciativa, (oVALORACIONESPROGRESS.t930_iniciativa == null) ? null : oVALORACIONESPROGRESS.t930_iniciativa.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_perseverancia, (oVALORACIONESPROGRESS.t930_perseverancia == null) ? null : oVALORACIONESPROGRESS.t930_perseverancia.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_interesescar, (oVALORACIONESPROGRESS.t930_interesescar == null) ? null : oVALORACIONESPROGRESS.t930_interesescar.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_formacion, oVALORACIONESPROGRESS.t930_formacion)
				};
                           
				return (int)cDblib.Execute("PRO_VALORACIONESPROGRESS_UPD_EVALUADOR", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int UpdateEvaluador(Models.VALORACIONESPROGRESS.formulario_id2 oVALORACIONESPROGRESS)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[32] {
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, oVALORACIONESPROGRESS.t930_idvaloracion.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t934_idmodeloformulario, oVALORACIONESPROGRESS.t934_idmodeloformulario.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fecfirmaevaluador,(oVALORACIONESPROGRESS.t930_fecfirmaevaluador == null) ? null : oVALORACIONESPROGRESS.t930_fecfirmaevaluador.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionROL, oVALORACIONESPROGRESS.t930_denominacionROL),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionCR, oVALORACIONESPROGRESS.t930_denominacionCR),
					Param(ParameterDirection.Input,enumDBFields.t930_atclientes, (oVALORACIONESPROGRESS.t930_atclientes == null) ? null : oVALORACIONESPROGRESS.t930_atclientes.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_prespuesta, (oVALORACIONESPROGRESS.t930_prespuesta == null) ? null : oVALORACIONESPROGRESS.t930_prespuesta.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_crespuesta, (oVALORACIONESPROGRESS.t930_crespuesta == null) ? null : oVALORACIONESPROGRESS.t930_crespuesta.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_respdificil, (oVALORACIONESPROGRESS.t930_respdificil == null) ? null : oVALORACIONESPROGRESS.t930_respdificil.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_valactividad, (oVALORACIONESPROGRESS.t930_valactividad == null) ? null : oVALORACIONESPROGRESS.t930_valactividad.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_amejorar, oVALORACIONESPROGRESS.t930_amejorar),
					Param(ParameterDirection.Input,enumDBFields.t930_gescli, (oVALORACIONESPROGRESS.t930_gescli == null) ? null : oVALORACIONESPROGRESS.t930_gescli.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_liderazgo, (oVALORACIONESPROGRESS.t930_liderazgo == null) ? null : oVALORACIONESPROGRESS.t930_liderazgo.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_planorga, (oVALORACIONESPROGRESS.t930_planorga == null) ? null : oVALORACIONESPROGRESS.t930_planorga.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_exptecnico, (oVALORACIONESPROGRESS.t930_exptecnico == null) ? null : oVALORACIONESPROGRESS.t930_exptecnico.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_cooperacion, (oVALORACIONESPROGRESS.t930_cooperacion == null) ? null : oVALORACIONESPROGRESS.t930_cooperacion.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_iniciativa, (oVALORACIONESPROGRESS.t930_iniciativa == null) ? null : oVALORACIONESPROGRESS.t930_iniciativa.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_perseverancia, (oVALORACIONESPROGRESS.t930_perseverancia == null) ? null : oVALORACIONESPROGRESS.t930_perseverancia.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_interesescar, (oVALORACIONESPROGRESS.t930_interesescar == null) ? null : oVALORACIONESPROGRESS.t930_interesescar.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_especificar, oVALORACIONESPROGRESS.t930_especificar),
                    Param(ParameterDirection.Input,enumDBFields.t930_forofichk, oVALORACIONESPROGRESS.t930_forofichk.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_forofitxt, oVALORACIONESPROGRESS.t930_forofitxt),
                    Param(ParameterDirection.Input,enumDBFields.t930_fortecchk, oVALORACIONESPROGRESS.t930_fortecchk.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_fortectxt, oVALORACIONESPROGRESS.t930_fortectxt),
                    Param(ParameterDirection.Input,enumDBFields.t930_foratcchk, oVALORACIONESPROGRESS.t930_foratcchk.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_foratctxt, oVALORACIONESPROGRESS.t930_foratctxt),
                    Param(ParameterDirection.Input,enumDBFields.t930_forcomchk, oVALORACIONESPROGRESS.t930_forcomchk.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_forcomtxt, oVALORACIONESPROGRESS.t930_forcomtxt),
                    Param(ParameterDirection.Input,enumDBFields.t930_forvenchk, oVALORACIONESPROGRESS.t930_forvenchk.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_forventxt, oVALORACIONESPROGRESS.t930_forventxt),
                    Param(ParameterDirection.Input,enumDBFields.t930_forespchk, oVALORACIONESPROGRESS.t930_forespchk.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_foresptxt, oVALORACIONESPROGRESS.t930_foresptxt)
				};

                return (int)cDblib.Execute("PRO_VALORACIONESPROGRESS_UPD_EVALUADOR", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int UpdateEvaluado(Models.VALORACIONESPROGRESS.formulario_id1 oVALORACIONESPROGRESS)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, oVALORACIONESPROGRESS.t930_idvaloracion.ToString()),
                    //Param(ParameterDirection.Input,enumDBFields.t934_idmodeloformulario, oVALORACIONESPROGRESS.t934_idmodeloformulario.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fecfirmaevaluado, (oVALORACIONESPROGRESS.t930_fecfirmaevaluado == null) ? null : oVALORACIONESPROGRESS.t930_fecfirmaevaluado.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_autoevaluacion, oVALORACIONESPROGRESS.t930_autoevaluacion.ToString())
				};

                return (int)cDblib.Execute("PRO_VALORACIONESPROGRESS_UPD_EVALUADO", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int UpdateEvaluado(Models.VALORACIONESPROGRESS.formulario_id2 oVALORACIONESPROGRESS)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, oVALORACIONESPROGRESS.t930_idvaloracion.ToString()),
                    //Param(ParameterDirection.Input,enumDBFields.t934_idmodeloformulario, oVALORACIONESPROGRESS.t934_idmodeloformulario.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fecfirmaevaluado, (oVALORACIONESPROGRESS.t930_fecfirmaevaluado == null) ? null : oVALORACIONESPROGRESS.t930_fecfirmaevaluado.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_autoevaluacion, oVALORACIONESPROGRESS.t930_autoevaluacion.ToString())
				};

                return (int)cDblib.Execute("PRO_VALORACIONESPROGRESS_UPD_EVALUADO", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int UpdateFecha(DateTime fecha)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(ParameterDirection.Input,enumDBFields.fechaantiguedad, fecha.ToString())
                   
				};

                return (int)cDblib.Execute("PRO_MODIFICARPARAMETROSPROGRESS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal int updateTemporada(Models.VALORACIONESPROGRESS.TemporadaProgress oDatos)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(ParameterDirection.Input,enumDBFields.temporadaprogress, oDatos.temporadaprogress),                   
                    Param(ParameterDirection.Input,enumDBFields.periodoprogress, oDatos.periodoprogress)
				};

                return (int)cDblib.Execute("PRO_PUTTEMPORADAPROGRESS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal int UpdateEstado(int t930_idvaloracion, string nuevoestado)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, t930_idvaloracion.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.nuevoestado, nuevoestado.ToString()),
                   
				};

                return (int)cDblib.Execute("PRO_VALORACIONESPROGRESS_UPD_ESTADO ", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Elimina un VALORACIONESPROGRESS a partir del id
        /// </summary>
        internal int Delete(Int32 t930_idvaloracion)
        {
			try
			{
				SqlParameter[] dbparams = new SqlParameter[1] {
					Param(ParameterDirection.Input,enumDBFields.t930_idvaloracion, t930_idvaloracion.ToString())
				};

                return (int)cDblib.Execute("PRO_ELIMINARVALORACION", dbparams);
			}
			catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
        /// Obtiene todos los VALORACIONESPROGRESS
        /// </summary>
		internal List<Models.VALORACIONESPROGRESS.formulario_id1> Catalogo(Int16 t934_idmodeloformulario,DateTime t930_fechaapertura,DateTime t930_fechacierre,Int32 t001_idficepi_evaluado,Int32 t001_idficepi_evaluador,DateTime t930_fecfirmaevaluado,DateTime t930_fecfirmaevaluador,String t930_denominacionROL,String t930_denominacionCR,Int16 t930_objetoevaluacion,String t930_denominacionPROYECTO,int t930_anomes_ini,int t930_anomes_fin,Int16 t930_actividad,String t930_areconocer,String t930_amejorar,Int16 t930_gescli,Int16 t930_liderazgo,Int16 t930_planorga,Int16 t930_exptecnico,Int16 t930_cooperacion,Int16 t930_iniciativa,Int16 t930_perseverancia,Int16 t930_interesescar,String t930_formacion,String t930_autoevaluacion,String t930_especificar,Int16 t930_atclientes,Int16 t930_prespuesta,Int16 t930_crespuesta,Int16 t930_respdificil,Int16 t930_valactividad,Boolean t930_forofichk,String t930_forofitxt,Boolean t930_fortecchk,String t930_fortectxt,Boolean t930_foratcchk,String t930_foratctxt,Boolean t930_forcomchk,String t930_forcomtxt,Boolean t930_forvenchk,String t930_forventxt,Boolean t930_forespchk,String t930_foresptxt)
		{
			Models.VALORACIONESPROGRESS.formulario_id1 oVALORACIONESPROGRESS = null;
            List<Models.VALORACIONESPROGRESS.formulario_id1> lst = new List<Models.VALORACIONESPROGRESS.formulario_id1>();
            IDataReader dr = null;

            try
            {
				SqlParameter[] dbparams = new SqlParameter[26] {
					Param(ParameterDirection.Input,enumDBFields.t934_idmodeloformulario, t934_idmodeloformulario.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fechaapertura, t930_fechaapertura.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fechacierre, t930_fechacierre.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t001_idficepi_evaluado, t001_idficepi_evaluado.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t001_idficepi_evaluador, t001_idficepi_evaluador.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fecfirmaevaluado, t930_fecfirmaevaluado.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_fecfirmaevaluador, t930_fecfirmaevaluador.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionROL, t930_denominacionROL),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionCR, t930_denominacionCR),
					Param(ParameterDirection.Input,enumDBFields.t930_objetoevaluacion, t930_objetoevaluacion.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_denominacionPROYECTO, t930_denominacionPROYECTO),
					Param(ParameterDirection.Input,enumDBFields.t930_anomes_ini, t930_anomes_ini.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_anomes_fin, t930_anomes_fin.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_actividad, t930_actividad.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_areconocer, t930_areconocer),
					Param(ParameterDirection.Input,enumDBFields.t930_amejorar, t930_amejorar),
					Param(ParameterDirection.Input,enumDBFields.t930_gescli, t930_gescli.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_liderazgo, t930_liderazgo.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_planorga, t930_planorga.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_exptecnico, t930_exptecnico.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_cooperacion, t930_cooperacion.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_iniciativa, t930_iniciativa.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_perseverancia, t930_perseverancia.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_interesescar, t930_interesescar.ToString()),
					Param(ParameterDirection.Input,enumDBFields.t930_formacion, t930_formacion),
					Param(ParameterDirection.Input,enumDBFields.t930_autoevaluacion, t930_autoevaluacion)
				};

				dr = cDblib.DataReader("PRO_VALORACIONESPROGRESS_CAT", dbparams);
				while (dr.Read())
				{
					oVALORACIONESPROGRESS = new Models.VALORACIONESPROGRESS.formulario_id1();
					oVALORACIONESPROGRESS.t930_idvaloracion=Convert.ToInt32(dr["t930_idvaloracion"]);
					oVALORACIONESPROGRESS.t934_idmodeloformulario=Convert.ToInt16(dr["t934_idmodeloformulario"]);
					oVALORACIONESPROGRESS.t930_fechaapertura=Convert.ToDateTime(dr["t930_fechaapertura"]);
					if(!Convert.IsDBNull(dr["t930_fechacierre"]))
						oVALORACIONESPROGRESS.t930_fechacierre=Convert.ToDateTime(dr["t930_fechacierre"]);
					oVALORACIONESPROGRESS.t001_idficepi_evaluado=Convert.ToInt32(dr["t001_idficepi_evaluado"]);
					oVALORACIONESPROGRESS.t001_idficepi_evaluador=Convert.ToInt32(dr["t001_idficepi_evaluador"]);
					if(!Convert.IsDBNull(dr["t930_fecfirmaevaluado"]))
						oVALORACIONESPROGRESS.t930_fecfirmaevaluado=Convert.ToDateTime(dr["t930_fecfirmaevaluado"]);
					if(!Convert.IsDBNull(dr["t930_fecfirmaevaluador"]))
						oVALORACIONESPROGRESS.t930_fecfirmaevaluador=Convert.ToDateTime(dr["t930_fecfirmaevaluador"]);
					oVALORACIONESPROGRESS.t930_denominacionROL=Convert.ToString(dr["t930_denominacionROL"]);
					oVALORACIONESPROGRESS.t930_denominacionCR=Convert.ToString(dr["t930_denominacionCR"]);
					if(!Convert.IsDBNull(dr["t930_objetoevaluacion"]))
						oVALORACIONESPROGRESS.t930_objetoevaluacion=Convert.ToByte(dr["t930_objetoevaluacion"]);
					oVALORACIONESPROGRESS.t930_denominacionPROYECTO=Convert.ToString(dr["t930_denominacionPROYECTO"]);
                    if (!Convert.IsDBNull(dr["t930_anomes_ini"]))
                        oVALORACIONESPROGRESS.t930_anomes_ini = Convert.ToInt32(dr["t930_anomes_ini"]);
                    if (!Convert.IsDBNull(dr["t930_anomes_fin"]))
                        oVALORACIONESPROGRESS.t930_anomes_fin = Convert.ToInt32(dr["t930_anomes_fin"]);
					if(!Convert.IsDBNull(dr["t930_actividad"]))
                        oVALORACIONESPROGRESS.t930_actividad = Convert.ToByte(dr["t930_actividad"]);
					oVALORACIONESPROGRESS.t930_areconocer=Convert.ToString(dr["t930_areconocer"]);
					oVALORACIONESPROGRESS.t930_amejorar=Convert.ToString(dr["t930_amejorar"]);
					if(!Convert.IsDBNull(dr["t930_gescli"]))
                        oVALORACIONESPROGRESS.t930_gescli = Convert.ToByte(dr["t930_gescli"]);
					if(!Convert.IsDBNull(dr["t930_liderazgo"]))
                        oVALORACIONESPROGRESS.t930_liderazgo = Convert.ToByte(dr["t930_liderazgo"]);
					if(!Convert.IsDBNull(dr["t930_planorga"]))
                        oVALORACIONESPROGRESS.t930_planorga = Convert.ToByte(dr["t930_planorga"]);
					if(!Convert.IsDBNull(dr["t930_exptecnico"]))
                        oVALORACIONESPROGRESS.t930_exptecnico = Convert.ToByte(dr["t930_exptecnico"]);
					if(!Convert.IsDBNull(dr["t930_cooperacion"]))
                        oVALORACIONESPROGRESS.t930_cooperacion = Convert.ToByte(dr["t930_cooperacion"]);
					if(!Convert.IsDBNull(dr["t930_iniciativa"]))
                        oVALORACIONESPROGRESS.t930_iniciativa = Convert.ToByte(dr["t930_iniciativa"]);
					if(!Convert.IsDBNull(dr["t930_perseverancia"]))
                        oVALORACIONESPROGRESS.t930_perseverancia = Convert.ToByte(dr["t930_perseverancia"]);
					if(!Convert.IsDBNull(dr["t930_interesescar"]))
                        oVALORACIONESPROGRESS.t930_interesescar = Convert.ToByte(dr["t930_interesescar"]);
					oVALORACIONESPROGRESS.t930_formacion=Convert.ToString(dr["t930_formacion"]);
					oVALORACIONESPROGRESS.t930_autoevaluacion=Convert.ToString(dr["t930_autoevaluacion"]);
                    lst.Add(oVALORACIONESPROGRESS);

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

        internal List<Models.VALORACIONESPROGRESS.miEval> CatMisEvaluaciones(int t001_idficepi)
        {
            List<Models.VALORACIONESPROGRESS.miEval> misEvaluaciones = new List<Models.VALORACIONESPROGRESS.miEval>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi_evaluado, t001_idficepi.ToString())
                };

                dr = cDblib.DataReader("PRO_MISEVALUACIONES_CAT", dbparams);

                while (dr.Read())
                {
                    misEvaluaciones.Add(new Models.VALORACIONESPROGRESS.miEval(int.Parse(dr["t930_idvaloracion"].ToString()), dr["evaluador"].ToString(), dr["estado"].ToString(), DateTime.Parse(dr["t930_fechaapertura"].ToString()), ((dr["t930_fechacierre"] != DBNull.Value) ? (Nullable<DateTime>)DateTime.Parse(dr["t930_fechacierre"].ToString()) : null), int.Parse(dr["t934_idmodeloformulario"].ToString())));
                }
                return misEvaluaciones;

            }
            catch (Exception ex)
            {
                throw new IBException(108, "Ocurrió un error obteniendo los datos de mis evaluaciones de base de datos.", ex);
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


        internal List<Models.VALORACIONESPROGRESS.EvalMiEquipo> CatEvalMiEquipo(int t001_idficepi, int desde, int hasta, int profundidad, string estado, Nullable<int> idficepi_evaluado, Nullable<int> idficepi_evaluador, string alcance)
        {
            List<Models.VALORACIONESPROGRESS.EvalMiEquipo> misEvaluaciones = new List<Models.VALORACIONESPROGRESS.EvalMiEquipo>();
            
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[8] {
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi, t001_idficepi.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.desde, desde.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.hasta, hasta.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.profundizacion, profundidad.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.situacion, estado.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.idficepi_evaluado, idficepi_evaluado),
                    Param(ParameterDirection.Input,enumDBFields.idficepi_evaluador, idficepi_evaluador),
                    Param(ParameterDirection.Input,enumDBFields.alcance, alcance),

                };

                dr = cDblib.DataReader("PRO_EVALUACIONESDEMISDESCENDIENTES_CONFORZADOS", dbparams);

                while (dr.Read())
                {
                    misEvaluaciones.Add(new Models.VALORACIONESPROGRESS.EvalMiEquipo(
                                        int.Parse(dr["t930_idvaloracion"].ToString()),
                                        int.Parse(dr["t001_idficepi_evaluador"].ToString()), 
                                        dr["evaluador"].ToString(), 
                                        dr["evaluado"].ToString(), 
                                        Utils.getEstado(dr["estado"].ToString()), 
                                        dr["estado"].ToString(),  
                                        DateTime.Parse(dr["t930_fechaapertura"].ToString()), 
                                        ((dr["t930_fechacierre"] != DBNull.Value) ? (Nullable<DateTime>)DateTime.Parse(dr["t930_fechacierre"].ToString()) : null),
                                        dr["motivo"].ToString(), 
                                        bool.Parse(dr["mostrarcombo"].ToString()),
                                        int.Parse(dr["t934_idmodeloformulario"].ToString())));
                }
                return misEvaluaciones;

            }
            catch (Exception ex)
            {
                throw new IBException(108, "Ocurrió un error obteniendo los datos de mis evaluaciones de base de datos.", ex);
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




        internal List<Models.VALORACIONESPROGRESS.busqEval> CatEvaluaciones(string origen, int idficepi_usuario, Nullable<int> desde, Nullable<int> hasta, Nullable<int> t001_idficepi, Nullable<int> t001_idficepi_evaluador, Nullable <short> profundidad,
            string estado, string t930_denominacionCR, string t930_denominacionROL, Nullable<short> t941_idcolectivo, Nullable<Int16> t930_puntuacion, string lestt930_gescli, string lestt930_liderazgo,
            string lestt930_planorga, string lestt930_exptecnico, string lestt930_cooperacion, string lestt930_iniciativa, string lestt930_perseverancia, Nullable<short> estaspectos,
            string lestt930_interesescar,  Nullable<short> estmejorar, string lcaut930_gescli, string lcaut930_liderazgo, string lcaut930_planorga, string lcaut930_exptecnico,
            string lcaut930_cooperacion, string lcaut930_iniciativa, string lcaut930_perseverancia, string lcaut930_interesescar, Nullable<short> caumejorar,
            Nullable<short> SelectMejorar, Nullable<short> SelectSuficiente, Nullable<short> SelectBueno,
            Nullable<short> SelectAlto, Nullable<short> SelectMejorarCAU, Nullable<short> SelectSuficienteCAU, Nullable<short> SelectBuenoCAU, Nullable<short> SelectAltoCAU)
            
        {
            List<Models.VALORACIONESPROGRESS.busqEval> catEvaluaciones = new List<Models.VALORACIONESPROGRESS.busqEval>();

            //((fdesde != DBNull.Value) ? fdesde : null)



            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[39] {
                    Param(ParameterDirection.Input,enumDBFields.origen, origen.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi_usuario, idficepi_usuario.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.desde, ((desde == null) ? null : desde.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.hasta, ((hasta == null) ? null : hasta.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi_evaluador, ((t001_idficepi_evaluador == null) ? null : t001_idficepi_evaluador.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi_evaluado, ((t001_idficepi == null) ? null : t001_idficepi.ToString())),
                    


                    //Param(ParameterDirection.Input,enumDBFields.figura, figura.ToString()),
                    //Param(ParameterDirection.Input,enumDBFields.profundidad, profundidad.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.profundidad, ((profundidad == null) ? null : profundidad.ToString())),

                    //Param(ParameterDirection.Input,enumDBFields.estado, estado),
                    Param(ParameterDirection.Input,enumDBFields.estado, ((estado == "") ? null : estado.ToString())),


                    //Param(ParameterDirection.Input,enumDBFields.t930_denominacionCR, t930_denominacionCR),

                    Param(ParameterDirection.Input,enumDBFields.t930_denominacionCR, ((t930_denominacionCR == "") ? null : t930_denominacionCR.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.t930_denominacionROL, ((t930_denominacionROL == "") ? null : t930_denominacionROL.ToString())),

                    //Param(ParameterDirection.Input,enumDBFields.t930_denominacionROL, t930_denominacionROL),
                    //Param(ParameterDirection.Input,enumDBFields.t941_idcolectivo, t941_idcolectivo.ToString()),

                    Param(ParameterDirection.Input,enumDBFields.t941_idcolectivo, ((t941_idcolectivo == null) ? null : t941_idcolectivo.ToString())),


                    //Param(ParameterDirection.Input,enumDBFields.t930_puntuacion, ((t930_puntuacion == null) ? null : t930_puntuacion.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.t930_calidad, ((t930_puntuacion == null) ? null : t930_puntuacion.ToString())),


                    Param(ParameterDirection.Input,enumDBFields.lestt930_gescli, ((lestt930_gescli == "") ? null : lestt930_gescli.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lestt930_liderazgo, ((lestt930_liderazgo == "") ? null : lestt930_liderazgo.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lestt930_planorga, ((lestt930_planorga == "") ? null : lestt930_planorga.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lestt930_exptecnico, ((lestt930_exptecnico == "") ? null : lestt930_exptecnico.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lestt930_cooperacion, ((lestt930_cooperacion == "") ? null : lestt930_cooperacion.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lestt930_iniciativa, ((lestt930_iniciativa == "") ? null : lestt930_iniciativa.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lestt930_perseverancia, ((lestt930_perseverancia == "") ? null : lestt930_perseverancia.ToString())),



                    Param(ParameterDirection.Input,enumDBFields.SelectMejorar, ((SelectMejorar == null) ? null : SelectMejorar.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.SelectSuficiente, ((SelectSuficiente == null) ? null : SelectSuficiente.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.SelectAlto, ((SelectBueno == null) ? null : SelectBueno.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.SelectBueno, ((SelectAlto == null) ? null : SelectAlto.ToString())),

                    //Param(ParameterDirection.Input,enumDBFields.lestt930_interesescar, lestt930_interesescar),

                    Param(ParameterDirection.Input,enumDBFields.lestt930_interesescar, ((lestt930_interesescar == "") ? null : lestt930_interesescar.ToString())),

                    //Param(ParameterDirection.Input,enumDBFields.estaspectos, estaspectos.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.estaspectos, ((estaspectos == null) ? null : estaspectos.ToString())),
                     Param(ParameterDirection.Input,enumDBFields.estmejorar, ((estmejorar == null) ? null : estmejorar.ToString())),
                    //Param(ParameterDirection.Input,enumDBFields.lestaspectos, lestaspectos.ToString()),


                   
                    //Param(ParameterDirection.Input,enumDBFields.lcaut930_gescli, lcaut930_gescli),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_gescli, ((lcaut930_gescli == "") ? null : lcaut930_gescli.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_liderazgo, ((lcaut930_liderazgo == "") ? null : lcaut930_liderazgo.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_planorga, ((lcaut930_planorga == "") ? null : lcaut930_planorga.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_exptecnico, ((lcaut930_exptecnico == "") ? null : lcaut930_exptecnico.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_cooperacion, ((lcaut930_cooperacion == "") ? null : lcaut930_cooperacion.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_iniciativa, ((lcaut930_iniciativa == "") ? null : lcaut930_iniciativa.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_perseverancia, ((lcaut930_perseverancia == "") ? null : lcaut930_perseverancia.ToString())),


                    //Param(ParameterDirection.Input,enumDBFields.cauaspectos, cauaspectos.ToString()),
                    //Param(ParameterDirection.Input,enumDBFields.lcauaspectos, lcauaspectos),


                    Param(ParameterDirection.Input,enumDBFields.SelectMejorarCAU, ((SelectMejorarCAU == null) ? null : SelectMejorarCAU.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.SelectSuficienteCAU, ((SelectSuficienteCAU == null) ? null : SelectSuficienteCAU.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.SelectAltoCAU, ((SelectBuenoCAU == null) ? null : SelectBuenoCAU.ToString())),
                    Param(ParameterDirection.Input,enumDBFields.SelectBuenoCAU, ((SelectAltoCAU == null) ? null : SelectAltoCAU.ToString())),



                    //Param(ParameterDirection.Input,enumDBFields.lcaut930_interesescar, lcaut930_interesescar),
                    Param(ParameterDirection.Input,enumDBFields.lcaut930_interesescar, ((lcaut930_interesescar == "") ? null : lcaut930_interesescar.ToString())),

                    Param(ParameterDirection.Input,enumDBFields.caumejorar, ((caumejorar == null) ? null : caumejorar.ToString()))

                };

                dr = cDblib.DataReader("PRO_BUSCARVALORACIONES", dbparams);

                while (dr.Read())
                {
                    //misEvaluaciones.Add(new Models.VALORACIONESPROGRESS.miEval(int.Parse(dr["t930_idvaloracion"].ToString()), dr["evaluador"].ToString(), dr["estado"].ToString(), DateTime.Parse(dr["t930_fechaapertura"].ToString()), ((dr["t930_fechacierre"] != DBNull.Value) ? (Nullable<DateTime>)DateTime.Parse(dr["t930_fechacierre"].ToString()) : null), int.Parse(dr["t934_idmodeloformulario"].ToString())));
                    catEvaluaciones.Add(new Models.VALORACIONESPROGRESS.busqEval(
                        int.Parse(dr["t934_idmodeloformulario"].ToString()),
                        int.Parse(dr["t930_idvaloracion"].ToString()), 
                        int.Parse(dr["t001_idficepi_evaluador"].ToString()),
                        dr["nombreevaluador"].ToString(),                        
                        dr["nombrecortoevaluador"].ToString(),                        
                        dr["nombreyapellidoevaluador"].ToString(),
                        dr["correo_evaluador"].ToString(),

                        int.Parse(dr["t001_idficepi_evaluado"].ToString()),
                        dr["nombreevaluado"].ToString(),                        
                        dr["nombrecortoevaluado"].ToString(),                         
                        dr["nombreyapellidoevaluado"].ToString(),

                        dr["correo_evaluado"].ToString(), 

                        bool.Parse(dr["permitircambiarestado"].ToString()),
                        
                        
                        dr["estado"].ToString(), 
                        Utils.getEstado(dr["estado"].ToString()), 
                        DateTime.Parse(dr["t930_fechaapertura"].ToString()), 
                        ((dr["t930_fechacierre"] != DBNull.Value) ? (Nullable<DateTime>)DateTime.Parse(dr["t930_fechacierre"].ToString()) : null)
                        
                        ));
                }

                return catEvaluaciones;
            }
            catch (Exception ex)
            {
                throw new IBException(108, "Ocurrió un error obteniendo los datos de la búsqueda de evaluaciones.", ex);
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


        internal List<string> getProyectos(int t001_idficepi_evaluado,int t930_anomes_ini,int t930_anomes_fin)
        {
            List<string> proyectos = new List<string>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi_evaluado, t001_idficepi_evaluado.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_anomes_ini,t930_anomes_ini.ToString()),
                    Param(ParameterDirection.Input,enumDBFields.t930_anomes_fin,t930_anomes_fin.ToString())
                };

                dr = cDblib.DataReader("PRO_GETPROYECTOS", dbparams);

                while (dr.Read())
                {
                    proyectos.Add(dr["t301_denominacion"].ToString());
                }
                return proyectos;

            }
            catch (Exception ex)
            {
                throw new IBException(110, "Ocurrió un error obteniendo los proyectos asociados al profesional de base de datos.", ex);
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


        internal List<int> buscarValoracionesPrevio(int t001_idficepi)
        {
            List<int> buscarValoracionesPrevio = new List<int>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input,enumDBFields.t001_idficepi_usuario, t001_idficepi.ToString())                      
                };

                dr = cDblib.DataReader("PRO_BUSCARVALORACIONES_PREVIO", dbparams);

                while (dr.Read())
                {
                    buscarValoracionesPrevio.Add(int.Parse(dr["t934_idmodeloformulario"].ToString()));
                }
                return buscarValoracionesPrevio;

            }
            catch (Exception ex)
            {
                throw new IBException(110, "Ocurrió un error obteniendo los modelos de formulario de base de datos.", ex);
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

		
//******************************
        //internal List<Models.VALORACIONESPROGRESS.busqEval> CatEvaluaciones(string origen, Nullable<int> desde, Nullable<int> hasta, Nullable<int> t001_idficepi, short profundidad,
        //    string estado, string t930_denominacionCR, string t930_denominacionROL, short t941_idcolectivo, Nullable<Boolean> t930_puntuacion, string lestt930_gescli, string lestt930_liderazgo,
        //    string lestt930_planorga, string lestt930_exptecnico, string lestt930_cooperacion, string lestt930_iniciativa, string lestt930_perseverancia, short estaspectos, string lestaspectos,
        //    string lestt930_interesescar, Nullable<short> estreconocer, Nullable<short> estmejorar, string lcaut930_gescli, string lcaut930_liderazgo, string lcaut930_planorga, string lcaut930_exptecnico,
        //    string lcaut930_cooperacion, string lcaut930_iniciativa, string lcaut930_perseverancia, short cauaspectos, string lcauaspectos, string lcaut930_interesescar, Nullable<short> caumejorar)
        //{
        //    List<Models.VALORACIONESPROGRESS.busqEval> catEvaluaciones = new List<Models.VALORACIONESPROGRESS.busqEval>();

        //    //((fdesde != DBNull.Value) ? fdesde : null)

        //    IDataReader dr = null;
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[33] {
        //            Param(ParameterDirection.Input,enumDBFields.origen, origen.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.desde, ((desde == null) ? null : desde.ToString())),
        //            Param(ParameterDirection.Input,enumDBFields.hasta, ((hasta == null) ? null : hasta.ToString())),
        //            Param(ParameterDirection.Input,enumDBFields.t001_idficepi, ((t001_idficepi == null) ? null : t001_idficepi.ToString())),
        //            //Param(ParameterDirection.Input,enumDBFields.figura, figura.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.profundidad, profundidad.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.estado, estado),
        //            Param(ParameterDirection.Input,enumDBFields.t930_denominacionCR, t930_denominacionCR),
        //            Param(ParameterDirection.Input,enumDBFields.t930_denominacionROL, t930_denominacionROL),
        //            Param(ParameterDirection.Input,enumDBFields.t941_idcolectivo, t941_idcolectivo.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.t930_puntuacion, ((t930_puntuacion == null) ? null : t930_puntuacion.ToString())),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_gescli, lestt930_gescli),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_liderazgo, lestt930_liderazgo),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_planorga, lestt930_planorga),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_exptecnico, lestt930_exptecnico),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_cooperacion, lestt930_cooperacion),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_iniciativa, lestt930_iniciativa),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_perseverancia, lestt930_perseverancia),
        //            Param(ParameterDirection.Input,enumDBFields.estaspectos, estaspectos.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.lestaspectos, lestaspectos.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.lestt930_interesescar, lestt930_interesescar),
        //            Param(ParameterDirection.Input,enumDBFields.estreconocer, ((estreconocer == null) ? null : estreconocer.ToString())),
        //            Param(ParameterDirection.Input,enumDBFields.estmejorar, ((estmejorar == null) ? null : estmejorar.ToString())),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_gescli, lcaut930_gescli),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_liderazgo, lcaut930_liderazgo),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_planorga, lcaut930_planorga),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_exptecnico, lcaut930_exptecnico),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_cooperacion, lcaut930_cooperacion),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_iniciativa, lcaut930_iniciativa),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_perseverancia, lcaut930_perseverancia),
        //            Param(ParameterDirection.Input,enumDBFields.cauaspectos, cauaspectos.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.lcauaspectos, lcauaspectos),
        //            Param(ParameterDirection.Input,enumDBFields.lcaut930_interesescar, lcaut930_interesescar),
        //            Param(ParameterDirection.Input,enumDBFields.caumejorar, ((caumejorar == null) ? null : caumejorar.ToString()))

        //        };

        //        dr = cDblib.DataReader("PRO_BUSCARVALORACIONES", dbparams);

        //        while (dr.Read())
        //        {
        //            //misEvaluaciones.Add(new Models.VALORACIONESPROGRESS.miEval(int.Parse(dr["t930_idvaloracion"].ToString()), dr["evaluador"].ToString(), dr["estado"].ToString(), DateTime.Parse(dr["t930_fechaapertura"].ToString()), ((dr["t930_fechacierre"] != DBNull.Value) ? (Nullable<DateTime>)DateTime.Parse(dr["t930_fechacierre"].ToString()) : null), int.Parse(dr["t934_idmodeloformulario"].ToString())));
        //            catEvaluaciones.Add(new Models.VALORACIONESPROGRESS.busqEval(int.Parse(dr["t930_idvaloracion"].ToString()), dr["NOMBREEVALUADOR"].ToString(), dr["NOMBREEVALUADO"].ToString(), dr["estado"].ToString(), DateTime.Parse(dr["fecha"].ToString()), int.Parse(dr["t934_idmodeloformulario"].ToString())));
        //        }

        //        return catEvaluaciones;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IBException(108, "Ocurrió un error obteniendo los datos de la búsqueda de evaluaciones.", ex);
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

        //internal List<string> getProyectos(int t001_idficepi_evaluado, int t930_anomes_ini, int t930_anomes_fin)
        //{
        //    List<string> proyectos = new List<string>();

        //    IDataReader dr = null;
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[3] {
        //            Param(ParameterDirection.Input,enumDBFields.t001_idficepi_evaluado, t001_idficepi_evaluado.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.t930_anomes_ini,t930_anomes_ini.ToString()),
        //            Param(ParameterDirection.Input,enumDBFields.t930_anomes_fin,t930_anomes_fin.ToString())
        //        };

        //        dr = cDblib.DataReader("PRO_GETPROYECTOS", dbparams);

        //        while (dr.Read())
        //        {
        //            proyectos.Add(dr["t301_denominacion"].ToString());
        //        }
        //        return proyectos;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IBException(110, "Ocurrió un error obteniendo los proyectos asociados al profesional de base de datos.", ex);
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


        internal List<string> getCR()
        {
            List<string> cr = new List<string>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {
                    
                };

                dr = cDblib.DataReader("PRO_GETCRDEEVALUACIONES", dbparams);

                while (dr.Read())
                {
                    cr.Add(dr["t930_denominacionCR"].ToString());
                }
                return cr;

            }
            catch (Exception ex)
            {
                throw new IBException(110, "Ocurrió un error obteniendo los CR.", ex);
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

        internal List<Models.Profesional> getCRActivos()
        {
            List<string> cr = new List<string>();
            Models.Profesional oColectivo = null;
            List<Models.Profesional> lst = new List<Models.Profesional>();
            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {
                    
                };

                dr = cDblib.DataReader("PRO_GETCRACTIVOS", dbparams);

                while (dr.Read())
                {                    
                    oColectivo = new Models.Profesional();
                    oColectivo.T303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
                    oColectivo.T303_denominacion = dr["t303_denominacion"].ToString();
                    lst.Add(oColectivo);
                    
                }
                return lst;

            }
            catch (Exception ex)
            {
                throw new IBException(110, "Ocurrió un error obteniendo los CR.", ex);
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


        internal List<string> getRol(string t930_denominacionROL)
        {
            List<string> rol = new List<string>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
                    Param(ParameterDirection.Input,enumDBFields.t930_denominacionROL, t930_denominacionROL.ToString())
                };

                dr = cDblib.DataReader("PRO_GETROL", dbparams);

                while (dr.Read())
                {
                    rol.Add(dr["t930_denominacionROL"].ToString());
                }
                return rol;

            }
            catch (Exception ex)
            {
                throw new IBException(110, "Ocurrió un error obteniendo los roles.", ex);
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


        internal List<string> obtenerRol()
        {
            List<string> rol = new List<string>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {
                    
                };

                dr = cDblib.DataReader("PRO_GETROLESDEEVALUCIONES", dbparams);

                while (dr.Read())
                {
                    rol.Add(dr["t930_denominacionROL"].ToString());
                }
                return rol;

            }
            catch (Exception ex)
            {
                throw new IBException(110, "Ocurrió un error obteniendo los roles.", ex);
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


        internal List<Models.VALORACIONESPROGRESS.TemporadaProgress> TemporadaProgress()
        {
            
            List<Models.VALORACIONESPROGRESS.TemporadaProgress> temporada = new List<Models.VALORACIONESPROGRESS.TemporadaProgress>();

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {
                    
                };

                dr = cDblib.DataReader("PRO_GETTEMPORADA", dbparams);

                while (dr.Read())
                {
                    temporada.Add(new Models.VALORACIONESPROGRESS.TemporadaProgress(
                        int.Parse(dr["t936_temporada"].ToString()), 
                        int.Parse(dr["t936_anomesinicio"].ToString()), 
                        int.Parse(dr["t936_anomesfin"].ToString()),
                        DateTime.Parse(dr["t936_referenciaantiguedad"].ToString()),
                        dr["t936_periodoprogress"].ToString()

                        ));                    
                }
                return temporada;

            }
            catch (Exception ex)
            {
                throw new IBException(110, "Ocurrió un error obteniendo la temporada del Progress.", ex);
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


        //*************************


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
				case enumDBFields.t930_idvaloracion:
					paramName = "@t930_idvaloracion";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t934_idmodeloformulario:
					paramName = "@t934_idmodeloformulario";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t930_fechaapertura:
					paramName = "@t930_fechaapertura";
                    paramType = SqlDbType.Date;
					paramSize = 3;
					break;
				case enumDBFields.t930_fechacierre:
					paramName = "@t930_fechacierre";
                    paramType = SqlDbType.Date;
					paramSize = 3;
					break;
				case enumDBFields.t001_idficepi_evaluado:
					paramName = "@t001_idficepi_evaluado";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t001_idficepi_evaluador:
					paramName = "@t001_idficepi_evaluador";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t930_fecfirmaevaluado:
					paramName = "@t930_fecfirmaevaluado";
                    paramType = SqlDbType.Date;
					paramSize = 3;
					break;
				case enumDBFields.t930_fecfirmaevaluador:
					paramName = "@t930_fecfirmaevaluador";
                    paramType = SqlDbType.Date;
					paramSize = 3;
					break;
				case enumDBFields.t930_denominacionROL:
					paramName = "@t930_denominacionROL";
                    paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t930_denominacionCR:
					paramName = "@t930_denominacionCR";
                    paramType = SqlDbType.VarChar;
					paramSize = 50;
					break;
				case enumDBFields.t930_objetoevaluacion:
					paramName = "@t930_objetoevaluacion";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_denominacionPROYECTO:
					paramName = "@t930_denominacionPROYECTO";
                    paramType = SqlDbType.VarChar;
					paramSize = 70;
					break;
				case enumDBFields.t930_anomes_ini:
                    paramName = "@t930_anomes_ini";
                    paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t930_anomes_fin:
                    paramName = "@t930_anomes_fin";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t930_actividad:
					paramName = "@t930_actividad";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_areconocer:
					paramName = "@t930_areconocer";
                    paramType = SqlDbType.VarChar;
					paramSize = -1;
					break;
				case enumDBFields.t930_amejorar:
					paramName = "@t930_amejorar";
                    paramType = SqlDbType.VarChar;
					paramSize = -1;
					break;
				case enumDBFields.t930_gescli:
					paramName = "@t930_gescli";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_liderazgo:
					paramName = "@t930_liderazgo";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_planorga:
					paramName = "@t930_planorga";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_exptecnico:
					paramName = "@t930_exptecnico";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_cooperacion:
					paramName = "@t930_cooperacion";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_iniciativa:
					paramName = "@t930_iniciativa";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_perseverancia:
					paramName = "@t930_perseverancia";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_interesescar:
					paramName = "@t930_interesescar";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_formacion:
					paramName = "@t930_formacion";
                    paramType = SqlDbType.VarChar;
					paramSize = -1;
					break;
				case enumDBFields.t930_autoevaluacion:
					paramName = "@t930_autoevaluacion";
                    paramType = SqlDbType.VarChar;
					paramSize = -1;
					break;
				case enumDBFields.t930_especificar:
					paramName = "@t930_especificar";
                    paramType = SqlDbType.VarChar;
					paramSize = 80;
					break;
				case enumDBFields.t930_atclientes:
					paramName = "@t930_atclientes";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_prespuesta:
					paramName = "@t930_prespuesta";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_crespuesta:
					paramName = "@t930_crespuesta";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_respdificil:
					paramName = "@t930_respdificil";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_valactividad:
					paramName = "@t930_valactividad";
					paramType = SqlDbType.SmallInt;
					paramSize = 1;
					break;
				case enumDBFields.t930_forofichk:
					paramName = "@t930_forofichk";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t930_forofitxt:
					paramName = "@t930_forofitxt";
                    paramType = SqlDbType.VarChar;
					paramSize = 200;
					break;
				case enumDBFields.t930_fortecchk:
					paramName = "@t930_fortecchk";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t930_fortectxt:
					paramName = "@t930_fortectxt";
                    paramType = SqlDbType.VarChar;
					paramSize = 200;
					break;
				case enumDBFields.t930_foratcchk:
					paramName = "@t930_foratcchk";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t930_foratctxt:
					paramName = "@t930_foratctxt";
                    paramType = SqlDbType.VarChar;
					paramSize = 200;
					break;
				case enumDBFields.t930_forcomchk:
					paramName = "@t930_forcomchk";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t930_forcomtxt:
					paramName = "@t930_forcomtxt";
                    paramType = SqlDbType.VarChar;
					paramSize = 200;
					break;
				case enumDBFields.t930_forvenchk:
					paramName = "@t930_forvenchk";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t930_forventxt:
					paramName = "@t930_forventxt";
                    paramType = SqlDbType.VarChar;
					paramSize = 200;
					break;
				case enumDBFields.t930_forespchk:
					paramName = "@t930_forespchk";
					paramType = SqlDbType.Bit;
					paramSize = 1;
					break;
				case enumDBFields.t930_foresptxt:
					paramName = "@t930_foresptxt";
                    paramType = SqlDbType.VarChar;
					paramSize = 200;
					break;
                case enumDBFields.listaprofesionales:
                    paramName = "@listaevaluados";
                    paramType = SqlDbType.VarChar;
                    break;

                case enumDBFields.fdesde:
                    paramName = "@fdesde";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.fhasta:
                    paramName = "@fhasta";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.figura:
                    paramName = "@figura";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;
                case enumDBFields.profundidad:
                    paramName = "@profundidad";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;
                case enumDBFields.estado:
                    paramName = "@estado";
                    paramType = SqlDbType.VarChar;
                    paramSize = 3;
                    break;
                case enumDBFields.t941_idcolectivo:
                    paramName = "@t941_idcolectivo";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;
                case enumDBFields.t930_puntuacion:
                    paramName = "@t930_puntuacion";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;
                case enumDBFields.lestt930_gescli:
                    paramName = "@listaest_t930_gescli";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lestt930_liderazgo:
                    paramName = "@listaestt930_liderazgo";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lestt930_planorga:
                    paramName = "@listaestt930_planorga";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lestt930_exptecnico:
                    paramName = "@listaestt930_exptecnico";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lestt930_cooperacion:
                    paramName = "@listaestt930_cooperacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lestt930_iniciativa:
                    paramName = "@listaestt930_iniciativa";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lestt930_perseverancia:
                    paramName = "@listaestt930_perseverancia";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.estaspectos:
                    paramName = "@longaspectosestareconocer";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;
                case enumDBFields.lestaspectos:
                    paramName = "@lestaspectos";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lestt930_interesescar:
                    paramName = "@listaestt930_interesescar";
                    paramType = SqlDbType.VarChar;
                    paramSize = 13;
                    break;
                case enumDBFields.estreconocer:
                    paramName = "@estreconocer";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;
                case enumDBFields.estmejorar:
                    paramName = "@longaspectosestamejorar";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;
                case enumDBFields.lcaut930_gescli:
                    paramName = "@listacaut930_gescli";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lcaut930_liderazgo:
                    paramName = "@listacaut930_liderazgo";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lcaut930_planorga:
                    paramName = "@listacaut930_planorga";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lcaut930_exptecnico:
                    paramName = "@listacaut930_exptecnico";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lcaut930_cooperacion:
                    paramName = "@listacaut930_cooperacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lcaut930_iniciativa:
                    paramName = "@listacaut930_iniciativa";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.lcaut930_perseverancia:
                    paramName = "@listacaut930_perseverancia";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;

                case enumDBFields.cauaspectos:
                    paramName = "@cauaspectos";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;
                case enumDBFields.lcauaspectos:
                    paramName = "@lcauaspectos";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;

                case enumDBFields.lcaut930_interesescar:
                    paramName = "@listacaut930_interesescar";
                    paramType = SqlDbType.VarChar;
                    paramSize = 13;
                    break;

                case enumDBFields.caumejorar:
                    paramName = "@longaspectoscauamejorar";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                case enumDBFields.desde:
                    paramName = "@desde";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.hasta:
                    paramName = "@hasta";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profundizacion:
                    paramName = "@profundizacion";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.situacion:
                    paramName = "@situacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;


                case enumDBFields.t001_idficepi_usuario:
                    paramName = "@t001_idficepi_usuario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.origen:
                    paramName = "@origen";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;

                case enumDBFields.t930_calidad:
                    paramName = "@t930_calidad";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                 case enumDBFields.SelectMejorar:
                    paramName = "@masdexaspectos1_amejorar";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                     case enumDBFields.SelectSuficiente:
                    paramName = "@masdexaspectos1_suficiente";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                     case enumDBFields.SelectBueno:
                    paramName = "@masdexaspectos1_bueno";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                     case enumDBFields.SelectAlto:
                    paramName = "@masdexaspectos1_alto";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                     case enumDBFields.SelectMejorarCAU:
                    paramName = "@masdexaspectos2_amejorar";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                     case enumDBFields.SelectSuficienteCAU:
                    paramName = "@masdexaspectos2_suficiente";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                     case enumDBFields.SelectBuenoCAU:
                    paramName = "@masdexaspectos2_bueno";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                    case enumDBFields.SelectAltoCAU:
                    paramName = "@masdexaspectos2_alto";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                    case enumDBFields.evalValida:
                    paramName = "@t930_puntuacion ";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                    case enumDBFields.ListaIdValoracion:
                    paramName = "@ListaIdValoracion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1000;
                    break;

                    case enumDBFields.idficepi_evaluado:
                    paramName = "@t001_idficepi_evaluado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                    case enumDBFields.idficepi_evaluador:
                    paramName = "@t001_idficepi_evaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                    case enumDBFields.alcance:
                    paramName = "@alcance";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;

                    case enumDBFields.mostrarcombo:
                    paramName = "@mostrarcombo";
                    paramType = SqlDbType.Bit;
                    paramSize = 1;
                    break;

                    case enumDBFields.fechaantiguedad:
                    paramName = "@fechaantiguedad ";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;

                    case enumDBFields.idficepiconectado:
                    paramName = "@t001_idficepi_encurso";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                                        
                    case enumDBFields.nuevoestado:
                    paramName = "@nuevoestado";
                    paramType = SqlDbType.VarChar;
                    paramSize = 3;
                    break;

                    case enumDBFields.temporadaprogress:
                    paramName = "@temporadaprogress";
                    paramType = SqlDbType.VarChar;
                    paramSize = 4;
                    break;

                    case enumDBFields.periodoprogress:
                    paramName = "@periodoprogress";
                    paramType = SqlDbType.VarChar;
                    paramSize = 100;
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
