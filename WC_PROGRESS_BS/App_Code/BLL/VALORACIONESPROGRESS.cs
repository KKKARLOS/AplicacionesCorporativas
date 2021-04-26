using IB.Progress.Shared;
using System;
using System.Collections.Generic;


/// <summary>
/// Summary description for VALORACIONESPROGRESS
/// </summary>
namespace IB.Progress.BLL 
{
    public class VALORACIONESPROGRESS : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("cb451084-2fb7-4b35-aaab-6cc87cfe45a3");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public VALORACIONESPROGRESS()
			: base()
        {
			//OpenDbConn();
        }
		
		public VALORACIONESPROGRESS(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
		
		internal List<Models.VALORACIONESPROGRESS.formulario_id1> Catalogo(Int16 t934_idmodeloformulario,DateTime t930_fechaapertura,DateTime t930_fechacierre,Int32 t001_idficepi_evaluado,Int32 t001_idficepi_evaluador,DateTime t930_fecfirmaevaluado,DateTime t930_fecfirmaevaluador,String t930_denominacionROL,String t930_denominacionCR,Int16 t930_objetoevaluacion,String t930_denominacionPROYECTO,int t930_anomes_ini,int t930_anomes_fin,Int16 t930_actividad,String t930_areconocer,String t930_amejorar,Int16 t930_gescli,Int16 t930_liderazgo,Int16 t930_planorga,Int16 t930_exptecnico,Int16 t930_cooperacion,Int16 t930_iniciativa,Int16 t930_perseverancia,Int16 t930_interesescar,String t930_formacion,String t930_autoevaluacion,String t930_especificar,Int16 t930_atclientes,Int16 t930_prespuesta,Int16 t930_crespuesta,Int16 t930_respdificil,Int16 t930_valactividad,Boolean t930_forofichk,String t930_forofitxt,Boolean t930_fortecchk,String t930_fortectxt,Boolean t930_foratcchk,String t930_foratctxt,Boolean t930_forcomchk,String t930_forcomtxt,Boolean t930_forvenchk,String t930_forventxt,Boolean t930_forespchk,String t930_foresptxt)
        {
            OpenDbConn();

			DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);
            return cVALORACIONESPROGRESS.Catalogo(t934_idmodeloformulario,t930_fechaapertura,t930_fechacierre,t001_idficepi_evaluado,t001_idficepi_evaluador,t930_fecfirmaevaluado,t930_fecfirmaevaluador,t930_denominacionROL,t930_denominacionCR,t930_objetoevaluacion,t930_denominacionPROYECTO,t930_anomes_ini,t930_anomes_fin,t930_actividad,t930_areconocer,t930_amejorar,t930_gescli,t930_liderazgo,t930_planorga,t930_exptecnico,t930_cooperacion,t930_iniciativa,t930_perseverancia,t930_interesescar,t930_formacion,t930_autoevaluacion,t930_especificar,t930_atclientes,t930_prespuesta,t930_crespuesta,t930_respdificil,t930_valactividad,t930_forofichk,t930_forofitxt,t930_fortecchk,t930_fortectxt,t930_foratcchk,t930_foratctxt,t930_forcomchk,t930_forcomtxt,t930_forvenchk,t930_forventxt,t930_forespchk,t930_foresptxt);

        }

        public List<Models.VALORACIONESPROGRESS.ImprimirInsertados> Insert(List<int> listaProfesionales)
        {
            //Guid methodOwnerID = new Guid("5a1728b8-da76-48a1-82c5-3c7e599c4ce1");

            OpenDbConn();

            //Transacción Serializable (System.Data.IsolationLevel.Serializable en el método begintransaction)
            //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
                
				DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);

                List<Models.VALORACIONESPROGRESS.ImprimirInsertados> idVALORACIONESPROGRESS = cVALORACIONESPROGRESS.Insert(string.Join(",", listaProfesionales));

                //Finalizar transacción 
                //if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idVALORACIONESPROGRESS;
            }
            catch(Exception ex){
                
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                throw new IBException(105, "Ocurrió un error al intentar abrir evaluaciones en base de datos.", ex);
            }	
		}

        public int UpdateEvaluador(Models.VALORACIONESPROGRESS.formulario_id1 oVALORACIONESPROGRESS)
        {
			//Guid methodOwnerID = new Guid("cf2d3afc-5b23-47fc-a07d-2e69f331384f");
			
			OpenDbConn();
			
			//if(cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

			try{
				DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);
				
				int result = cVALORACIONESPROGRESS.UpdateEvaluador(oVALORACIONESPROGRESS);

                //Finalizar transacción 
                //if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);
				
				return result;
			}
            catch(Exception ex){
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(106, "Ocurrió un error al intentar actualizar la evaluación en base de datos.", ex);
            }
        }

        public int UpdateEvaluador(Models.VALORACIONESPROGRESS.formulario_id2 oVALORACIONESPROGRESS)
        {
            //Guid methodOwnerID = new Guid("F43FBFC5-C42C-429C-83E4-7E1442285C62");

            OpenDbConn();

            //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);

                int result = cVALORACIONESPROGRESS.UpdateEvaluador(oVALORACIONESPROGRESS);

                //Finalizar transacción 
                //if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(106, "Ocurrió un error al intentar actualizar la evaluación en base de datos.", ex);
            }
        }

        public int UpdateEvaluado(Models.VALORACIONESPROGRESS.formulario_id1 oVALORACIONESPROGRESS)
        {
            //Guid methodOwnerID = new Guid("E547390C-6CE3-425A-B963-B302E5950235");

            OpenDbConn();

            //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);

                int result = cVALORACIONESPROGRESS.UpdateEvaluado(oVALORACIONESPROGRESS);

                //Finalizar transacción 
                //if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(107, "Ocurrió un error al intentar actualizar la evaluación en base de datos.", ex);
            }
        }

        public int UpdateEvaluado(Models.VALORACIONESPROGRESS.formulario_id2 oVALORACIONESPROGRESS)
        {
            //Guid methodOwnerID = new Guid("13DBE469-B375-4610-9186-E62B4DA8D2DA");

            OpenDbConn();

            //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);

                int result = cVALORACIONESPROGRESS.UpdateEvaluado(oVALORACIONESPROGRESS);

                //Finalizar transacción 
                //if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(107, "Ocurrió un error al intentar actualizar la evaluación en base de datos.", ex);
            }
        }


        public int UpdateFecha(DateTime fecha)
        {
            Guid methodOwnerID = new Guid("5b8d8077-8746-4fbd-ae74-eb7f5647b5f7");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);

                int result = cVALORACIONESPROGRESS.UpdateFecha(fecha);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(106, "Ocurrió un error al intentar actualizar la fecha.", ex);
            }
        }

        public int updateTemporada(Models.VALORACIONESPROGRESS.TemporadaProgress oDatos)
        {
            Guid methodOwnerID = new Guid("692b7ab9-e45f-4b30-8db9-37f867b0f979");
           
            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);

                int result = cVALORACIONESPROGRESS.updateTemporada(oDatos);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(106, "Ocurrió un error al intentar actualizar la fecha.", ex);
            }
        }


        public int UpdateEstado(int t930_idvaloracion, string nuevoestado)

        {
           
            OpenDbConn();

            try
            {
                DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);

                int result = cVALORACIONESPROGRESS.UpdateEstado(t930_idvaloracion, nuevoestado);

                return result;
            }
            catch (Exception ex)
            {
              

                throw new IBException(106, "Ocurrió un error al intentar actualizar el estado de la evaluación.", ex);
            }
        }


        public int CualificarEvaluacion(int idvaloracion, Nullable<bool> t930_puntuacion)
        {            
            OpenDbConn();
            
            try
            {
                DAL.VALORACIONESPROGRESS cEval = new DAL.VALORACIONESPROGRESS(cDblib);

                int idEval = cEval.CualificarEvaluacion(idvaloracion, t930_puntuacion);

                return idEval;
            }
            catch (Exception ex)
            {               
                throw new IBException(105, "No se ha podido cualificar la evaluación.", ex);
            }
        }

        public int Delete(Int32 t930_idvaloracion)
        {
			
			OpenDbConn();
			
			try{
			
				DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);
				
				int result = cVALORACIONESPROGRESS.Delete(t930_idvaloracion);
	
				return result;
			}
            catch(Exception ex){

                throw ex;
            }
        }

        public Models.VALORACIONESPROGRESS.formulario_id1 Select(Int32 idficepiconectado, Int32 t930_idvaloracion)
        {
			OpenDbConn();
			
            DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);
            return cVALORACIONESPROGRESS.Select(idficepiconectado, t930_idvaloracion);
        }

        public Models.VALORACIONESPROGRESS.formulario_id2 Select2(Int32 idficepiconectado, Int32 t930_idvaloracion)
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS cVALORACIONESPROGRESS = new DAL.VALORACIONESPROGRESS(cDblib);
            return cVALORACIONESPROGRESS.Select2(idficepiconectado, t930_idvaloracion);
        }

        public List<Models.VALORACIONESPROGRESS.miEval> CatMisEvaluaciones(int t001_idficepi)
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.CatMisEvaluaciones(t001_idficepi);
        }

        public List<Models.VALORACIONESPROGRESS.EvalMiEquipo> CatEvalMiEquipo(int t001_idficepi, int desde, int hasta, int profundidad, string estado, Nullable<int> idficepi_evaluado, Nullable<int> idficepi_evaluador, string alcance)
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.CatEvalMiEquipo(t001_idficepi, desde, hasta, profundidad, estado, idficepi_evaluado, idficepi_evaluador, alcance);
        }



        public List<string> getProyectos(int t001_idficepi, int t930_anomes_ini, int t930_anomes_fin)
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.getProyectos(t001_idficepi, t930_anomes_ini, t930_anomes_fin);
        }


        public List<int> buscarValoracionesPrevio(int t001_idficepi)
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.buscarValoracionesPrevio(t001_idficepi);
        }


        //Valorar la posibilidad de pasarle un modelo como parámetro!!!
        public List<Models.VALORACIONESPROGRESS.busqEval> CatEvaluaciones(string origen, int idficepi_usuario, Nullable<int> desde, Nullable<int> hasta, Nullable<int> t001_idficepi, Nullable<int> t001_idficepi_evaluador, Nullable <short> profundidad,
            string estado, string t930_denominacionCR, string t930_denominacionROL, Nullable<short> t941_idcolectivo, Nullable<Int16> t930_puntuacion, List<short> lestt930_gescli, List<short> lestt930_liderazgo,
            List<short> lestt930_planorga, List<short> lestt930_exptecnico, List<short> lestt930_cooperacion, List<short> lestt930_iniciativa, List<short> lestt930_perseverancia, Nullable<short> estaspectos,
            List<short> lestt930_interesescar,  Nullable<short> estmejorar, List<short> lcaut930_gescli, List<short> lcaut930_liderazgo, List<short> lcaut930_planorga, List<short> lcaut930_exptecnico,
            List<short> lcaut930_cooperacion, List<short> lcaut930_iniciativa, List<short> lcaut930_perseverancia, List<short> lcaut930_interesescar, Nullable<short> caumejorar, Nullable<short> SelectMejorar, Nullable<short> SelectSuficiente, Nullable<short> SelectBueno,
            Nullable<short> SelectAlto, Nullable<short> SelectMejorarCAU, Nullable<short> SelectSuficienteCAU, Nullable<short> SelectBuenoCAU, Nullable<short> SelectAltoCAU)
            
        {
            string lestt930_interesescar2 = string.Join(",", lestt930_interesescar);
            string lestt930_gescli2 = string.Join(",", lestt930_gescli);
            string lestt930_liderazgo2 = string.Join(",", lestt930_liderazgo);
            string lestt930_planorga2 = string.Join(",", lestt930_planorga);
            string lestt930_exptecnico2 = string.Join(",", lestt930_exptecnico);
            string lestt930_cooperacion2 = string.Join(",", lestt930_cooperacion);
            string lestt930_iniciativa2 = string.Join(",", lestt930_iniciativa);
            string lestt930_perseverancia2 = string.Join(",", lestt930_perseverancia);
            //string lestaspectos2 = string.Join(",", lestaspectos);

            string lcaut930_interesescar2 = string.Join(",", lcaut930_interesescar);
            string lcaut930_gescli2 = string.Join(",", lcaut930_gescli);
            string lcaut930_liderazgo2 = string.Join(",", lcaut930_liderazgo);
            string lcaut930_planorga2 = string.Join(",", lcaut930_planorga);
            string lcaut930_exptecnico2 = string.Join(",", lcaut930_exptecnico);
            string lcaut930_cooperacion2 = string.Join(",", lcaut930_cooperacion);
            string lcaut930_iniciativa2 = string.Join(",", lcaut930_iniciativa);
            string lcaut930_perseverancia2 = string.Join(",", lcaut930_perseverancia);
            //string lcauaspectos2 = string.Join(",", lcauaspectos);
            

            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.CatEvaluaciones(origen, idficepi_usuario, desde, hasta, t001_idficepi, t001_idficepi_evaluador, profundidad, estado, t930_denominacionCR, t930_denominacionROL,
                    t941_idcolectivo, t930_puntuacion, lestt930_gescli2, lestt930_liderazgo2, lestt930_planorga2, lestt930_exptecnico2, lestt930_cooperacion2,
                    lestt930_iniciativa2, lestt930_perseverancia2, estaspectos, lestt930_interesescar2,  estmejorar,
                    lcaut930_gescli2, lcaut930_liderazgo2, lcaut930_planorga2, lcaut930_exptecnico2, lcaut930_cooperacion2, lcaut930_iniciativa2,
                    lcaut930_perseverancia2, lcaut930_interesescar2, caumejorar, SelectMejorar,SelectSuficiente, SelectBueno, SelectAlto, SelectMejorarCAU, SelectSuficienteCAU, SelectBuenoCAU, SelectAltoCAU );
        }


        public List<string> getCR()
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.getCR();
        }

        public List<Models.Profesional> getCRActivos()
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.getCRActivos();
        }

        public List<string> getRol(string t930_denominacionROL)
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.getRol(t930_denominacionROL);
        }

        public List<string> obtenerRol()
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS valoraciones = new DAL.VALORACIONESPROGRESS(cDblib);
            return valoraciones.obtenerRol();
        }


        public List<Models.VALORACIONESPROGRESS.TemporadaProgress> TemporadaProgress()
        {
            OpenDbConn();

            DAL.VALORACIONESPROGRESS temporada = new DAL.VALORACIONESPROGRESS(cDblib);
            return temporada.TemporadaProgress();
        }

		#endregion          
		
		#region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);
        }
        private void AttachDbConn(sqldblib.SqlServerSP extcDblib)
        {
            cDblib = extcDblib;
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) if (cDblib != null && cDblib.OwnerID.Equals(classOwnerID)) cDblib.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~VALORACIONESPROGRESS()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
