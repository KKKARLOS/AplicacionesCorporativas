using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IB.Progress.DAL
{
    /// <summary>
    /// Obtiene los distintos valores de las estadísticas (tanto la situación actual (Valores) como las fotos (Foto))
    /// </summary>
    public class Estadisticas
    {
        private sqldblib.SqlServerSP cDblib;

        private enum enumDBFields : byte
        {
            desde           = 1,
            hasta           = 2,
            t001_fecantigu  = 3,
            profundidad     = 4,
            t001_idficepi   = 5,
            t941_idcolectivo= 6,
            //Parámetros de salida
            profevh         = 7,
            profevm         = 8,
            profnoevh       = 9,
            profnoevm       = 10,
            profevhant      = 11,
            profevmant      = 12,
            profnoevhant    = 13,
            profnoevmant    = 14,
            evcursoh        = 15,
            evcursom        = 16,
            evfirmadah      = 17,
            evfirmadam      = 18,
            evautomaticah   = 19,
            evautomaticam   = 20,
            evcursohant     = 21,
            evcursomant     = 22,
            evfirmadahant   = 23,
            evfirmadamant   = 24,
            evautomaticahant= 25,
	        evautomaticamant= 26,
            //Totales calculados como sumas de los anteriores
	        profevt         = 27,
	        profevantt      = 28,
	        profnoevt       = 29,
	        profnoevantt    = 30,
	        profht          = 31,
	        profmt          = 32,
	        proft           = 33,
	        profhantt       = 34,
	        profmantt       = 35,
	        profantt        = 36,
	        evcursot        = 37,
	        evcursoantt     = 38,
	        evfirmadat      = 39,
	        evfirmadaantt   = 40,
	        evautomaticat   = 41,
	        evautomaticaantt= 42,
	        evht            = 43,
	        evmt            = 44,
	        evt             = 45,
	        evhantt         = 46,
	        evmantt         = 47,
	        evantt          = 48,
            //Evaluaciones abiertas
            evabiertah      = 49,
            evabiertam      = 50, 
            evabiertat      = 51, 
            evabiertahant   = 52, 
            evabiertamant   = 53, 
            evabiertaantt   = 54,
            t932_idfoto     = 55,
            //Evaluaciones cerradas
            evcerradah      = 56,
            evcerradam      = 57,
            evcerradat      = 58,
            evcerradahant   = 59,
            evcerradamant   = 60,
            evcerradaantt   = 61,
            //Otras estadísticas
            estado                      = 62, 
            t930_denominacionCR         = 63, 
            t935_idcategoriaprofesional = 64,
            evcurso                     = 65, 
            evcursoevaluador            = 66, 
            evabierta                   = 67, 
            evabiertaevaluador          = 68, 
            evfirmadaval                = 69, 
            evfirmadanoval              = 70,
            evfirmada                   = 71, 
            evfirmadaevaluador          = 72, 
            evautomaticaval             = 73, 
            evautomaticanoval           = 74, 
            evautomatica                = 75, 
            evautomaticaevaluador       = 76,
            evcerradaval                = 77, 
            evcerradanoval              = 78, 
            evcerrada                   = 79, 
            evcerradaevaluador          = 80, 
            ev                          = 81, 
            evevaluador                 = 82, 
            profevabierta               = 83,
            profevcurso                 = 84, 
            profevcerrada               = 85, 
            profevfirmada               = 86, 
            profevautomatica            = 87, 
            prof                        = 88, 
            profsinevaluador            = 89,
            evdorsinconfirmar           = 90, 
            evdorconfirmado             = 91, 
            evdor                       = 92,


            idcolectivo_evaluaciones    = 93,
            idnodo_evaluadores          = 94,
            desde_colectivos            = 95,
            hasta_colectivos            = 96,
            t001_fecantigu_colectivos   = 97,
            idnodos_colectivos          = 98,
            idcolectivo_colectivos      = 99,
            EvaluacionesABI             = 100,
            EvaluacionesABIEvaluador    = 101,
            EvaluacionesCUR             = 102,
            EvaluacionesCUREvaluador    = 103,
            EvaluacionesCCF             = 104,
            EvaluacionesCCFValida       = 105,
            EvaluacionesCCFNoValida     = 106,
            EvaluacionesCCFEvaluador    = 107,
            EvaluacionesCSF             = 108,
            EvaluacionesCSFValida       = 109,
            EvaluacionesCSFNoValida     = 110,
            EvaluacionesCSFEvaluador    = 111,
            Totalevaluacionesvalidas    = 112,
            Totalevaluacionesnovalidas  = 113,
            Totalevaluacionescerradas   = 114,
            Totalevaluacionescerradasevaluador = 115,
            Totalevaluaciones           = 116,
            Totalevaluadoresevaluaciones = 117,
            Profesionalessinevaluador    = 118,
            Evaluadoressinconfirmarequipo = 119,
            Evaluadoresequipoconfirmado = 120,
            Totalevaluadoresevaluadores = 121, 
            ProfesionalesABI = 122,
            ProfesionalesCUR = 123,
            ProfesionalesCCF = 124,
            ProfesionalesCSF = 125,
            ProfesionalesCER = 126,
            Totalprofesionales = 127,
            Profesionalessinevaluacion = 128,


            desde_evaluaciones = 129,
            hasta_evaluaciones = 130,
            t001_fecantigu_evaluaciones = 131,

            estadoprofesional_evaluaciones = 132,
            t941_idcolectivo_evaluaciones = 133,
            t930_denominacionCR_evaluaciones = 134,

            t303_idnodo_evaluadores = 135,


            //desde_colectivos = 136,
            //hasta_colectivos = 137,
            //t001_fecantigu_colectivos = 138,
            t303_idnodo_colectivos = 139,
            t941_idcolectivo_colectivos = 140,
            t001_idficepievaluador_colectivos = 141,
            Totalevaluacionesevaluador = 142


        }

        public Estadisticas(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public Estadisticas()
        {
            
			//lo dejo pero de momento no se usa
        }

     

        /// <summary>
        /// Obtiene las estadísticas referentes a la situación actual y a una foto determinada (t932_idfoto)
        /// </summary>
        public Models.Estadisticas Foto(Nullable<Int16> t932_idfoto, Int32 desde, Int32 hasta, DateTime t001_fecantigu, Int16 profundidad, Int32 t001_idficepi, Nullable<Int16> t941_idcolectivo)
        {
            //Parámetros de salida
            SqlParameter profevh = null, profevm = null, profnoevh = null, profnoevm = null, profevhant = null, profevmant = null, profnoevhant = null,
            profnoevmant = null, evcursoh = null, evcursom = null, evfirmadah = null, evfirmadam = null, evautomaticah = null, evautomaticam = null,
            evcursohant = null, evcursomant = null, evfirmadahant = null, evfirmadamant = null, evautomaticahant = null, evautomaticamant = null,
            profevt = null, profevantt = null, profnoevt = null, profnoevantt = null, profht = null, profmt = null, proft = null, profhantt = null,
            profmantt = null, profantt = null, evcursot = null, evcursoantt = null, evfirmadat = null, evfirmadaantt = null, evautomaticat = null,
            evautomaticaantt = null, evht = null, evmt = null, evt = null, evhantt = null, evmantt = null, evantt = null,
            evabiertah = null, evabiertam = null, evabiertat = null, evabiertahant = null, evabiertamant = null, evabiertaantt = null,
            evcerradah = null, evcerradam = null, evcerradat = null, evcerradahant = null, evcerradamant = null, evcerradaantt = null;

            Models.Estadisticas oEstadisticas = new Models.Estadisticas();

            try
            {
                SqlParameter[] dbparams = new SqlParameter[61] {
                    Param(ParameterDirection.Input, enumDBFields.t932_idfoto, (t932_idfoto == null) ? null: t932_idfoto.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.desde, desde.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.hasta, hasta.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_fecantigu, t001_fecantigu.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.profundidad, profundidad.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, t001_idficepi.ToString()),                   
                    Param(ParameterDirection.Input, enumDBFields.t941_idcolectivo, (t941_idcolectivo==null)?null:t941_idcolectivo.ToString()),


                    profevh         = Param(ParameterDirection.Output, enumDBFields.profevh, "0"),
                    profevm         = Param(ParameterDirection.Output, enumDBFields.profevm, "0"),
                    profnoevh       = Param(ParameterDirection.Output, enumDBFields.profnoevh, "0"),
                    profnoevm       = Param(ParameterDirection.Output, enumDBFields.profnoevm, "0"),
                    profevhant      = Param(ParameterDirection.Output, enumDBFields.profevhant, "0"),
                    profevmant      = Param(ParameterDirection.Output, enumDBFields.profevmant, "0"),
                    profnoevhant    = Param(ParameterDirection.Output, enumDBFields.profnoevhant, "0"),
                    profnoevmant    = Param(ParameterDirection.Output, enumDBFields.profnoevmant, "0"),
                    evcursoh        = Param(ParameterDirection.Output, enumDBFields.evcursoh, "0"),
                    evcursom        = Param(ParameterDirection.Output, enumDBFields.evcursom, "0"),
                    evfirmadah      = Param(ParameterDirection.Output, enumDBFields.evfirmadah, "0"),
                    evfirmadam      = Param(ParameterDirection.Output, enumDBFields.evfirmadam, "0"),
                    evautomaticah   = Param(ParameterDirection.Output, enumDBFields.evautomaticah, "0"),
                    evautomaticam   = Param(ParameterDirection.Output, enumDBFields.evautomaticam, "0"),
                    evcursohant     = Param(ParameterDirection.Output, enumDBFields.evcursohant, "0"),
                    evcursomant     = Param(ParameterDirection.Output, enumDBFields.evcursomant, "0"),
                    evfirmadahant   = Param(ParameterDirection.Output, enumDBFields.evfirmadahant, "0"),
                    evfirmadamant   = Param(ParameterDirection.Output, enumDBFields.evfirmadamant, "0"),
                    evautomaticahant= Param(ParameterDirection.Output, enumDBFields.evautomaticahant, "0"),
	                evautomaticamant= Param(ParameterDirection.Output, enumDBFields.evautomaticamant, "0"),
                    //Totales calculados como sumas de los anteriores
	                profevt         = Param(ParameterDirection.Output, enumDBFields.profevt, "0"),
	                profevantt      = Param(ParameterDirection.Output, enumDBFields.profevantt, "0"),
	                profnoevt       = Param(ParameterDirection.Output, enumDBFields.profnoevt, "0"),
	                profnoevantt    = Param(ParameterDirection.Output, enumDBFields.profnoevantt, "0"),
	                profht          = Param(ParameterDirection.Output, enumDBFields.profht, "0"),
	                profmt          = Param(ParameterDirection.Output, enumDBFields.profmt, "0"),
	                proft           = Param(ParameterDirection.Output, enumDBFields.proft, "0"),
	                profhantt       = Param(ParameterDirection.Output, enumDBFields.profhantt, "0"),
	                profmantt       = Param(ParameterDirection.Output, enumDBFields.profmantt, "0"),
	                profantt        = Param(ParameterDirection.Output, enumDBFields.profantt, "0"),
	                evcursot        = Param(ParameterDirection.Output, enumDBFields.evcursot, "0"),
	                evcursoantt     = Param(ParameterDirection.Output, enumDBFields.evcursoantt, "0"),
	                evfirmadat      = Param(ParameterDirection.Output, enumDBFields.evfirmadat, "0"),
	                evfirmadaantt   = Param(ParameterDirection.Output, enumDBFields.evfirmadaantt, "0"),
	                evautomaticat   = Param(ParameterDirection.Output, enumDBFields.evautomaticat, "0"),
	                evautomaticaantt= Param(ParameterDirection.Output, enumDBFields.evautomaticaantt, "0"),
	                evht            = Param(ParameterDirection.Output, enumDBFields.evht, "0"),
	                evmt            = Param(ParameterDirection.Output, enumDBFields.evmt, "0"),
	                evt             = Param(ParameterDirection.Output, enumDBFields.evt, "0"),
	                evhantt         = Param(ParameterDirection.Output, enumDBFields.evhantt, "0"),
	                evmantt         = Param(ParameterDirection.Output, enumDBFields.evmantt, "0"),
	                evantt          = Param(ParameterDirection.Output, enumDBFields.evantt, "0"),
                    //Evaluaciones abiertas
                    evabiertah      = Param(ParameterDirection.Output, enumDBFields.evabiertah, "0"),
                    evabiertam      = Param(ParameterDirection.Output, enumDBFields.evabiertam, "0"),
                    evabiertat      = Param(ParameterDirection.Output, enumDBFields.evabiertat, "0"),
                    evabiertahant   = Param(ParameterDirection.Output, enumDBFields.evabiertahant, "0"),
                    evabiertamant   = Param(ParameterDirection.Output, enumDBFields.evabiertamant, "0"),
                    evabiertaantt   = Param(ParameterDirection.Output, enumDBFields.evabiertaantt, "0"),
                    //Evaluaciones cerradas
                    evcerradah      = Param(ParameterDirection.Output, enumDBFields.evcerradah, "0"),
                    evcerradam      = Param(ParameterDirection.Output, enumDBFields.evcerradam, "0"),
                    evcerradat      = Param(ParameterDirection.Output, enumDBFields.evcerradat, "0"),
                    evcerradahant   = Param(ParameterDirection.Output, enumDBFields.evcerradahant, "0"),
                    evcerradamant   = Param(ParameterDirection.Output, enumDBFields.evcerradamant, "0"),
                    evcerradaantt   = Param(ParameterDirection.Output, enumDBFields.evcerradaantt, "0")

                };

                //Modificar para que llame al nuevo sp de foto
                cDblib.Execute("PRO_ESTADISTICAS", dbparams);

                oEstadisticas.t932_idfoto = t932_idfoto;
                oEstadisticas.Desde = desde;
                oEstadisticas.Hasta = hasta;
                oEstadisticas.t001_idficepi = t001_idficepi;
                oEstadisticas.t001_fecantigu = t001_fecantigu;
                oEstadisticas.t941_idcolectivo = t941_idcolectivo;
                oEstadisticas.Profundidad = profundidad;

                oEstadisticas.evantt = Int32.Parse(evantt.Value.ToString());
                oEstadisticas.evautomaticaantt = Int32.Parse(evautomaticaantt.Value.ToString());
                oEstadisticas.evautomaticah = Int32.Parse(evautomaticah.Value.ToString());
                oEstadisticas.evautomaticahant = Int32.Parse(evautomaticahant.Value.ToString());
                oEstadisticas.evautomaticam = Int32.Parse(evautomaticam.Value.ToString());
                oEstadisticas.evautomaticamant = Int32.Parse(evautomaticamant.Value.ToString());
                oEstadisticas.evautomaticat = Int32.Parse(evautomaticat.Value.ToString());
                oEstadisticas.evcursoantt = Int32.Parse(evcursoantt.Value.ToString());
                oEstadisticas.evcursoh = Int32.Parse(evcursoh.Value.ToString());
                oEstadisticas.evcursohant = Int32.Parse(evcursohant.Value.ToString());
                oEstadisticas.evcursom = Int32.Parse(evcursom.Value.ToString());
                oEstadisticas.evcursomant = Int32.Parse(evcursomant.Value.ToString());
                oEstadisticas.evcursot = Int32.Parse(evcursot.Value.ToString());
                oEstadisticas.evfirmadaantt = Int32.Parse(evfirmadaantt.Value.ToString());
                oEstadisticas.evfirmadah = Int32.Parse(evfirmadah.Value.ToString());
                oEstadisticas.evfirmadahant = Int32.Parse(evfirmadahant.Value.ToString());
                oEstadisticas.evfirmadam = Int32.Parse(evfirmadam.Value.ToString());
                oEstadisticas.evfirmadamant = Int32.Parse(evfirmadamant.Value.ToString());
                oEstadisticas.evfirmadat = Int32.Parse(evfirmadat.Value.ToString());
                oEstadisticas.evhantt = Int32.Parse(evhantt.Value.ToString());
                oEstadisticas.evht = Int32.Parse(evht.Value.ToString());
                oEstadisticas.evmantt = Int32.Parse(evmantt.Value.ToString());
                oEstadisticas.evmt = Int32.Parse(evmt.Value.ToString());
                oEstadisticas.evt = Int32.Parse(evt.Value.ToString());
                oEstadisticas.profantt = Int32.Parse(profantt.Value.ToString());
                oEstadisticas.profevantt = Int32.Parse(profevantt.Value.ToString());
                oEstadisticas.profevh = Int32.Parse(profevh.Value.ToString());
                oEstadisticas.profevhant = Int32.Parse(profevhant.Value.ToString());
                oEstadisticas.profevm = Int32.Parse(profevm.Value.ToString());
                oEstadisticas.profevmant = Int32.Parse(profevmant.Value.ToString());
                oEstadisticas.profevt = Int32.Parse(profevt.Value.ToString());
                oEstadisticas.profhantt = Int32.Parse(profhantt.Value.ToString());
                oEstadisticas.profht = Int32.Parse(profht.Value.ToString());
                oEstadisticas.profmantt = Int32.Parse(profmantt.Value.ToString());
                oEstadisticas.profmt = Int32.Parse(profmt.Value.ToString());
                oEstadisticas.profnoevantt = Int32.Parse(profnoevantt.Value.ToString());
                oEstadisticas.profnoevh = Int32.Parse(profnoevh.Value.ToString());
                oEstadisticas.profnoevhant = Int32.Parse(profnoevhant.Value.ToString());
                oEstadisticas.profnoevm = Int32.Parse(profnoevm.Value.ToString());
                oEstadisticas.profnoevmant = Int32.Parse(profnoevmant.Value.ToString());
                oEstadisticas.profnoevt = Int32.Parse(profnoevt.Value.ToString());
                oEstadisticas.proft = Int32.Parse(proft.Value.ToString());
                oEstadisticas.evabiertah = Int32.Parse(evabiertah.Value.ToString());
                oEstadisticas.evabiertam = Int32.Parse(evabiertam.Value.ToString());
                oEstadisticas.evabiertat = Int32.Parse(evabiertat.Value.ToString());
                oEstadisticas.evabiertahant = Int32.Parse(evabiertahant.Value.ToString());
                oEstadisticas.evabiertamant = Int32.Parse(evabiertamant.Value.ToString());
                oEstadisticas.evabiertaantt = Int32.Parse(evabiertaantt.Value.ToString());
                oEstadisticas.evcerradah = Int32.Parse(evcerradah.Value.ToString());
                oEstadisticas.evcerradam = Int32.Parse(evcerradam.Value.ToString());
                oEstadisticas.evcerradat = Int32.Parse(evcerradat.Value.ToString());
                oEstadisticas.evcerradahant = Int32.Parse(evcerradahant.Value.ToString());
                oEstadisticas.evcerradamant = Int32.Parse(evcerradamant.Value.ToString());
                oEstadisticas.evcerradaantt = Int32.Parse(evcerradaantt.Value.ToString());

                return oEstadisticas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Models.OtrasEstadisticas OtrosValores(Int32 desde, Int32 hasta, DateTime t001_fecantigu, Int16 profundidad, Nullable<Int32> t001_idficepi, Nullable<Int16> t941_idcolectivo,
            Char estado, String t930_denominacionCR, Nullable<int> t935_idcategoriaprofesional)
        {
            SqlParameter evcurso = null, evcursoevaluador = null, evabierta = null, evabiertaevaluador = null, evfirmadaval = null, evfirmadanoval = null,
                evfirmada = null, evfirmadaevaluador = null, evautomaticaval = null, evautomaticanoval = null, evautomatica = null, evautomaticaevaluador = null,
                evcerradaval = null, evcerradanoval = null, evcerrada = null, evcerradaevaluador = null, ev = null, evevaluador = null, profevabierta = null,
                profevcurso = null, profevcerrada = null, profevfirmada = null, profevautomatica = null, prof = null, profsinevaluador = null,
                evdorsinconfirmar = null, evdorconfirmado = null, evdor = null;

            Models.OtrasEstadisticas oEstadisticas = new Models.OtrasEstadisticas();

            try
            {
                SqlParameter[] dbparams = new SqlParameter[37] {
                    Param(ParameterDirection.Input, enumDBFields.desde, desde.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.hasta, hasta.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_fecantigu, t001_fecantigu.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.profundidad, profundidad.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_idficepi, (t001_idficepi==null)?null:t001_idficepi.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t941_idcolectivo, (t941_idcolectivo==null)?null:t941_idcolectivo.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.estado, estado.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t930_denominacionCR, t930_denominacionCR),
                    Param(ParameterDirection.Input, enumDBFields.t935_idcategoriaprofesional, (t935_idcategoriaprofesional==null)?null:t935_idcategoriaprofesional.ToString()),

                    evcurso                 = Param(ParameterDirection.Output, enumDBFields.evcurso, "0"),
                    evcursoevaluador        = Param(ParameterDirection.Output, enumDBFields.evcursoevaluador, "0"),
                    evabierta               = Param(ParameterDirection.Output, enumDBFields.evabierta, "0"),
                    evabiertaevaluador      = Param(ParameterDirection.Output, enumDBFields.evabiertaevaluador, "0"),
                    evfirmadaval            = Param(ParameterDirection.Output, enumDBFields.evfirmadaval, "0"),
                    evfirmadanoval          = Param(ParameterDirection.Output, enumDBFields.evfirmadanoval, "0"),
                    evfirmada               = Param(ParameterDirection.Output, enumDBFields.evfirmada, "0"),
                    evfirmadaevaluador      = Param(ParameterDirection.Output, enumDBFields.evfirmadaevaluador, "0"),
                    evautomaticaval         = Param(ParameterDirection.Output, enumDBFields.evautomaticaval, "0"),
                    evautomaticanoval       = Param(ParameterDirection.Output, enumDBFields.evautomaticanoval, "0"),
                    evautomatica            = Param(ParameterDirection.Output, enumDBFields.evautomatica, "0"),
                    evautomaticaevaluador   = Param(ParameterDirection.Output, enumDBFields.evautomaticaevaluador, "0"),
                    evcerradaval            = Param(ParameterDirection.Output, enumDBFields.evcerradaval, "0"),
                    evcerradanoval          = Param(ParameterDirection.Output, enumDBFields.evcerradanoval, "0"),
                    evcerrada               = Param(ParameterDirection.Output, enumDBFields.evcerrada, "0"),
                    evcerradaevaluador      = Param(ParameterDirection.Output, enumDBFields.evcerradaevaluador, "0"),
                    ev                      = Param(ParameterDirection.Output, enumDBFields.ev, "0"),
                    evevaluador             = Param(ParameterDirection.Output, enumDBFields.evevaluador, "0"),
                    profevabierta           = Param(ParameterDirection.Output, enumDBFields.profevabierta, "0"),
	                profevcurso             = Param(ParameterDirection.Output, enumDBFields.profevcurso, "0"),
	                profevcerrada           = Param(ParameterDirection.Output, enumDBFields.profevcerrada, "0"),
	                profevfirmada           = Param(ParameterDirection.Output, enumDBFields.profevfirmada, "0"),
	                profevautomatica        = Param(ParameterDirection.Output, enumDBFields.profevautomatica, "0"),
	                prof                    = Param(ParameterDirection.Output, enumDBFields.prof, "0"),
	                profsinevaluador        = Param(ParameterDirection.Output, enumDBFields.profsinevaluador, "0"),
	                evdorsinconfirmar       = Param(ParameterDirection.Output, enumDBFields.evdorsinconfirmar, "0"),
	                evdorconfirmado         = Param(ParameterDirection.Output, enumDBFields.evdorconfirmado, "0"),
	                evdor                   = Param(ParameterDirection.Output, enumDBFields.evdor, "0"),

                };

                cDblib.Execute("PRO_ADMOTRAESTADISTICA", dbparams);

                oEstadisticas.Desde = desde;
                oEstadisticas.Hasta = hasta;
                oEstadisticas.t001_idficepi = t001_idficepi;
                oEstadisticas.t001_fecantigu = t001_fecantigu;
                oEstadisticas.t941_idcolectivo = t941_idcolectivo;
                //oEstadisticas.Profundidad = profundidad;
                oEstadisticas.estado = estado;
                oEstadisticas.t930_denominacionCR = t930_denominacionCR;
                //oEstadisticas.t935_idcategoriaprofesional = t935_idcategoriaprofesional;
            
                oEstadisticas.evcurso                 = Int32.Parse(evcurso.Value.ToString());
                oEstadisticas.evcursoevaluador        = Int32.Parse(evcursoevaluador.Value.ToString());
                oEstadisticas.evabierta               = Int32.Parse(evabierta.Value.ToString());
                oEstadisticas.evabiertaevaluador      = Int32.Parse(evabiertaevaluador.Value.ToString());
                oEstadisticas.evfirmadaval            = Int32.Parse(evfirmadaval.Value.ToString());
                oEstadisticas.evfirmadanoval          = Int32.Parse(evfirmadanoval.Value.ToString());
                oEstadisticas.evfirmada               = Int32.Parse(evfirmada.Value.ToString());
                oEstadisticas.evfirmadaevaluador      = Int32.Parse(evfirmadaevaluador.Value.ToString());
                oEstadisticas.evautomaticaval         = Int32.Parse(evautomaticaval.Value.ToString());
                oEstadisticas.evautomaticanoval       = Int32.Parse(evautomaticanoval.Value.ToString());
                oEstadisticas.evautomatica            = Int32.Parse(evautomatica.Value.ToString());
                oEstadisticas.evautomaticaevaluador   = Int32.Parse(evautomaticaevaluador.Value.ToString());
                oEstadisticas.evcerradaval            = Int32.Parse(evcerradaval.Value.ToString());
                oEstadisticas.evcerradanoval          = Int32.Parse(evcerradanoval.Value.ToString());
                oEstadisticas.evcerrada               = Int32.Parse(evcerrada.Value.ToString());
                oEstadisticas.evcerradaevaluador      = Int32.Parse(evcerradaevaluador.Value.ToString());
                oEstadisticas.ev                      = Int32.Parse(ev.Value.ToString());
                oEstadisticas.evevaluador             = Int32.Parse(evevaluador.Value.ToString());
                oEstadisticas.profevabierta           = Int32.Parse(profevabierta.Value.ToString());
	            oEstadisticas.profevcurso             = Int32.Parse(profevcurso.Value.ToString());
	            oEstadisticas.profevcerrada           = Int32.Parse(profevcerrada.Value.ToString());
	            oEstadisticas.profevfirmada           = Int32.Parse(profevfirmada.Value.ToString());
	            oEstadisticas.profevautomatica        = Int32.Parse(profevautomatica.Value.ToString());
	            oEstadisticas.prof                    = Int32.Parse(prof.Value.ToString());
	            oEstadisticas.profsinevaluador        = Int32.Parse(profsinevaluador.Value.ToString());
	            oEstadisticas.evdorsinconfirmar       = Int32.Parse(evdorsinconfirmar.Value.ToString());
	            oEstadisticas.evdorconfirmado         = Int32.Parse(evdorconfirmado.Value.ToString());
                oEstadisticas.evdor                   = Int32.Parse(evdor.Value.ToString());

                return oEstadisticas;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public Models.OtrasEstadisticasRRHH OtrosValoresRRHH(Int32 desde, Int32 hasta, DateTime t001_fecantigu, Char? estado, short? idcolectivo_evaluaciones, String t930_denominacionCR, int? idnodo_evaluadores, Int32 desde_colectivos, Int32 hasta_colectivos, DateTime t001_fecantigu_colectivos, int? idnodos_colectivos, int? idcolectivo_colectivos,    int? t001_idficepi)
        {
           
            //EVALUACIONES
            SqlParameter EvaluacionesABI = null, EvaluacionesABIEvaluador = null, EvaluacionesCUR = null, EvaluacionesCUREvaluador = null,
            EvaluacionesCCF = null, EvaluacionesCCFValida = null, EvaluacionesCCFNoValida = null, EvaluacionesCCFEvaluador = null,
            EvaluacionesCSF = null, EvaluacionesCSFValida = null, EvaluacionesCSFNoValida = null, EvaluacionesCSFEvaluador = null,
            Totalevaluacionesvalidas = null, Totalevaluacionesnovalidas = null, Totalevaluacionescerradas = null, Totalevaluaciones = null, Totalevaluadoresevaluaciones = null, Totalevaluacionescerradasevaluador = null, Totalevaluacionesevaluador = null,
            
            //EVALUADORES
            Profesionalessinevaluador = null, Evaluadoressinconfirmarequipo = null, Evaluadoresequipoconfirmado = null, Totalevaluadoresevaluadores = null,

            //COLECTIVOS
            ProfesionalesABI  = null, ProfesionalesCUR = null, ProfesionalesCCF = null, ProfesionalesCSF = null, ProfesionalesCER = null,  Totalprofesionales= null, Profesionalessinevaluacion = null;
        
            Models.OtrasEstadisticasRRHH oEstadisticas = new Models.OtrasEstadisticasRRHH();

            try
            {               
                SqlParameter[] dbparams = new SqlParameter[43] {

                    //PARÁMETROS DE ENTRADA
                    Param(ParameterDirection.Input, enumDBFields.desde_evaluaciones, desde.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.hasta_evaluaciones, hasta.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_fecantigu_evaluaciones, t001_fecantigu.ToString()),                    
                    Param(ParameterDirection.Input, enumDBFields.estadoprofesional_evaluaciones, (estado==null)? null: estado.ToString()),

                    Param(ParameterDirection.Input, enumDBFields.t941_idcolectivo_evaluaciones, (idcolectivo_evaluaciones==null)?null:idcolectivo_evaluaciones.ToString()),
                    
                    Param(ParameterDirection.Input, enumDBFields.t930_denominacionCR_evaluaciones, (t930_denominacionCR == "") ?null:t930_denominacionCR.ToString()),

                    //EVALUADORES
                    Param(ParameterDirection.Input, enumDBFields.idnodo_evaluadores, (idnodo_evaluadores==null)?null:idnodo_evaluadores.ToString()),


                    //COLECTIVOS
                    Param(ParameterDirection.Input, enumDBFields.desde_colectivos, desde_colectivos.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.hasta_colectivos, hasta_colectivos.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.t001_fecantigu_colectivos, t001_fecantigu_colectivos.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.idnodos_colectivos, (idnodos_colectivos==null)?null:idnodos_colectivos.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.idcolectivo_colectivos, (idcolectivo_colectivos==null)?null:idcolectivo_colectivos.ToString()),

                    Param(ParameterDirection.Input, enumDBFields.t001_idficepievaluador_colectivos, (t001_idficepi==null)?null:t001_idficepi.ToString()),
                    

                    //PARÁMETROS DE SALIDA

                    //EVALUACIONES
                    EvaluacionesABI                 = Param(ParameterDirection.Output, enumDBFields.EvaluacionesABI, "0"),
                    EvaluacionesABIEvaluador        = Param(ParameterDirection.Output, enumDBFields.EvaluacionesABIEvaluador, "0"),
                    EvaluacionesCUR               = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCUR, "0"),
                    EvaluacionesCUREvaluador      = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCUREvaluador, "0"),
                    EvaluacionesCCF            = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCCF, "0"),
                    EvaluacionesCCFValida          = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCCFValida, "0"),
                    EvaluacionesCCFNoValida               = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCCFNoValida, "0"),
                    EvaluacionesCCFEvaluador      = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCCFEvaluador, "0"),
                    EvaluacionesCSF         = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCSF, "0"),
                    EvaluacionesCSFValida       = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCSFValida, "0"),
                    EvaluacionesCSFNoValida            = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCSFNoValida, "0"),
                    EvaluacionesCSFEvaluador   = Param(ParameterDirection.Output, enumDBFields.EvaluacionesCSFEvaluador, "0"),
                    Totalevaluacionesvalidas            = Param(ParameterDirection.Output, enumDBFields.Totalevaluacionesvalidas, "0"),
                    Totalevaluacionesnovalidas          = Param(ParameterDirection.Output, enumDBFields.Totalevaluacionesnovalidas, "0"),
                    Totalevaluacionescerradas               = Param(ParameterDirection.Output, enumDBFields.Totalevaluacionescerradas, "0"),
                    Totalevaluacionescerradasevaluador               = Param(ParameterDirection.Output, enumDBFields.Totalevaluacionescerradasevaluador, "0"),
                    Totalevaluaciones      = Param(ParameterDirection.Output, enumDBFields.Totalevaluaciones, "0"),
                    Totalevaluadoresevaluaciones                      = Param(ParameterDirection.Output, enumDBFields.Totalevaluadoresevaluaciones, "0"),
                    Totalevaluacionesevaluador = Param(ParameterDirection.Output, enumDBFields.Totalevaluacionesevaluador, "0"),

                    //EVALUADORES
                    Profesionalessinevaluador             = Param(ParameterDirection.Output, enumDBFields.Profesionalessinevaluador, "0"),
                    Evaluadoressinconfirmarequipo           = Param(ParameterDirection.Output, enumDBFields.Evaluadoressinconfirmarequipo, "0"),
                    Evaluadoresequipoconfirmado             = Param(ParameterDirection.Output, enumDBFields.Evaluadoresequipoconfirmado, "0"),
                    Totalevaluadoresevaluadores           = Param(ParameterDirection.Output, enumDBFields.Totalevaluadoresevaluadores, "0"),

                    //COLECTIVOS
                    ProfesionalesABI           = Param(ParameterDirection.Output, enumDBFields.ProfesionalesABI, "0"),
                    ProfesionalesCUR        = Param(ParameterDirection.Output, enumDBFields.ProfesionalesCUR, "0"),
                    ProfesionalesCCF                    = Param(ParameterDirection.Output, enumDBFields.ProfesionalesCCF, "0"),
                    ProfesionalesCSF        = Param(ParameterDirection.Output, enumDBFields.ProfesionalesCSF, "0"),
                    ProfesionalesCER       = Param(ParameterDirection.Output, enumDBFields.ProfesionalesCER, "0"),
                    Totalprofesionales         = Param(ParameterDirection.Output, enumDBFields.Totalprofesionales, "0"),
                    Profesionalessinevaluacion  = Param(ParameterDirection.Output, enumDBFields.Profesionalessinevaluacion, "0"),

                };

                cDblib.Execute("PRO_ESTADISTICAS_ADM", dbparams);

                oEstadisticas.Desde = desde;
                oEstadisticas.Hasta = hasta;
                oEstadisticas.t001_idficepi = t001_idficepi;
                oEstadisticas.t001_fecantigu = t001_fecantigu;
                oEstadisticas.t941_idcolectivo = idcolectivo_evaluaciones;                
                oEstadisticas.estado = estado;
                oEstadisticas.t930_denominacionCR = t930_denominacionCR;
                oEstadisticas.T303_idnodo_evaluadores = idnodo_evaluadores;
                oEstadisticas.T303_idnodo_colectivos = idnodos_colectivos;
                
                //EVALUACIONES
                oEstadisticas.EvaluacionesABI = Int32.Parse(EvaluacionesABI.Value.ToString());
                oEstadisticas.EvaluacionesABIEvaluador = Int32.Parse(EvaluacionesABIEvaluador.Value.ToString());
                oEstadisticas.EvaluacionesCUR = Int32.Parse(EvaluacionesCUR.Value.ToString());
                oEstadisticas.EvaluacionesCUREvaluador = Int32.Parse(EvaluacionesCUREvaluador.Value.ToString());

                oEstadisticas.EvaluacionesCCF = Int32.Parse(EvaluacionesCCF.Value.ToString());
                oEstadisticas.EvaluacionesCCFValida = Int32.Parse(EvaluacionesCCFValida.Value.ToString());
                oEstadisticas.EvaluacionesCCFNoValida = Int32.Parse(EvaluacionesCCFNoValida.Value.ToString());
                oEstadisticas.EvaluacionesCCFEvaluador = Int32.Parse(EvaluacionesCCFEvaluador.Value.ToString());
                oEstadisticas.EvaluacionesCSF = Int32.Parse(EvaluacionesCSF.Value.ToString());
                oEstadisticas.EvaluacionesCSFValida = Int32.Parse(EvaluacionesCSFValida.Value.ToString());
                oEstadisticas.EvaluacionesCSFNoValida = Int32.Parse(EvaluacionesCSFNoValida.Value.ToString());
                oEstadisticas.EvaluacionesCSFEvaluador = Int32.Parse(EvaluacionesCSFEvaluador.Value.ToString());
                oEstadisticas.Totalevaluacionesvalidas = Int32.Parse(Totalevaluacionesvalidas.Value.ToString());
                oEstadisticas.Totalevaluacionesnovalidas = Int32.Parse(Totalevaluacionesnovalidas.Value.ToString());
                oEstadisticas.Totalevaluacionescerradas = Int32.Parse(Totalevaluacionescerradas.Value.ToString());
                oEstadisticas.Totalevaluacionescerradasevaluador = Int32.Parse(Totalevaluacionescerradasevaluador.Value.ToString());
                oEstadisticas.Totalevaluaciones = Int32.Parse(Totalevaluaciones.Value.ToString());
                oEstadisticas.Totalevaluacionesevaluador = Int32.Parse(Totalevaluacionesevaluador.Value.ToString());

                //EVALUADORES
                oEstadisticas.Profesionalessinevaluador = Int32.Parse(Profesionalessinevaluador.Value.ToString());
                oEstadisticas.Evaluadoresequipoconfirmado = Int32.Parse(Evaluadoresequipoconfirmado.Value.ToString());
                oEstadisticas.Evaluadoressinconfirmarequipo = Int32.Parse(Evaluadoressinconfirmarequipo.Value.ToString());
                oEstadisticas.Totalevaluadoresevaluadores = Int32.Parse(Totalevaluadoresevaluadores.Value.ToString());


                //COLECTIVOS
                oEstadisticas.ProfesionalesABI = Int32.Parse(ProfesionalesABI.Value.ToString());
                oEstadisticas.ProfesionalesCUR = Int32.Parse(ProfesionalesCUR.Value.ToString());
                oEstadisticas.ProfesionalesCCF = Int32.Parse(ProfesionalesCCF.Value.ToString());
                oEstadisticas.ProfesionalesCSF = Int32.Parse(ProfesionalesCSF.Value.ToString());
                oEstadisticas.ProfesionalesCER = Int32.Parse(ProfesionalesCER.Value.ToString());
                oEstadisticas.Totalprofesionales = Int32.Parse(Totalprofesionales.Value.ToString());
                oEstadisticas.Profesionalessinevaluacion = Int32.Parse(Profesionalessinevaluacion.Value.ToString());

                return oEstadisticas;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

















        /// <summary>
        /// Obtiene el año de la valoración más antigua (a partir del cual se dará valores al combo desde)
        /// </summary>
        public int MinAnyoValoracion()
        {
            int anyo;

            anyo = (int)cDblib.ExecuteScalar("PRO_MINANYOVALORACION", null);
            
            return anyo;
        }

        #region funciones privadas
        private SqlParameter Param(ParameterDirection paramDirection, enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            

            switch (dbField)
            {
                case enumDBFields.t932_idfoto:
                    paramName = "@t932_idfoto";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.desde:
                    paramName = "@desde";
                    paramType = SqlDbType.Int;
                    paramSize = 3;
                    break;

                case enumDBFields.hasta:
                    paramName = "@hasta";
                    paramType = SqlDbType.Int;
                    paramSize = 3;
                    break;

                case enumDBFields.t001_fecantigu:
                    paramName = "@t001_fecantigu";
                    paramType = SqlDbType.Date;
                    paramSize = 3;
                    break;

                case enumDBFields.profundidad:
                    paramName = "@profundidad";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 3;
                    
                    break;

                case enumDBFields.t941_idcolectivo:
                    paramName = "@t941_idcolectivo";
                    paramType = SqlDbType.SmallInt;
                    paramSize = 1;
                    break;

                case enumDBFields.profevh:
                    paramName = "@profevh";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profevm:
                    paramName = "@profevm";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profnoevh:
                    paramName = "@profnoevh";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profnoevm:
                    paramName = "@profnoevm";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profevhant:
                    paramName = "@profevhant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profevmant:
                    paramName = "@profevmant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profnoevhant:
                    paramName = "@profnoevhant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profnoevmant:
                    paramName = "@profnoevmant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evcursoh:
                    paramName = "@evcursoh";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evcursom:
                    paramName = "@evcursom";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evfirmadah:
                    paramName = "@evfirmadah";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evfirmadam:
                    paramName = "@evfirmadam";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evautomaticah:
                    paramName = "@evautomaticah";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evautomaticam:
                    paramName = "@evautomaticam";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evcursohant:
                    paramName = "@evcursohant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evcursomant:
                    paramName = "@evcursomant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evfirmadahant:
                    paramName = "@evfirmadahant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evfirmadamant:
                    paramName = "@evfirmadamant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evautomaticahant:
                    paramName = "@evautomaticahant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evautomaticamant:
                    paramName = "@evautomaticamant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profevt:
                    paramName = "@profevt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profevantt:
                    paramName = "@profevantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profnoevt:
                    paramName = "@profnoevt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profnoevantt:
                    paramName = "@profnoevantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profht:
                    paramName = "@profht";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profmt:
                    paramName = "@profmt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.proft:
                    paramName = "@proft";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profhantt:
                    paramName = "@profhantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profmantt:
                    paramName = "@profmantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.profantt:
                    paramName = "@profantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evcursot:
                    paramName = "@evcursot";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evcursoantt:
                    paramName = "@evcursoantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evfirmadat:
                    paramName = "@evfirmadat";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evfirmadaantt:
                    paramName = "@evfirmadaantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evautomaticat:
                    paramName = "@evautomaticat";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evautomaticaantt:
                    paramName = "@evautomaticaantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evht:
                    paramName = "@evht";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evmt:
                    paramName = "@evmt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evt:
                    paramName = "@evt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evhantt:
                    paramName = "@evhantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evmantt:
                    paramName = "@evmantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evantt:
                    paramName = "@evantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evabiertah:
                    paramName = "@evabiertah";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evabiertam:
                    paramName = "@evabiertam";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evabiertat:
                    paramName = "@evabiertat";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evabiertahant:
                    paramName = "@evabiertahant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evabiertamant:
                    paramName = "@evabiertamant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evabiertaantt:
                    paramName = "@evabiertaantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradah:
                    paramName = "@evcerradah";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradam:
                    paramName = "@evcerradam";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradat:
                    paramName = "@evcerradat";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradahant:
                    paramName = "@evcerradahant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradamant:
                    paramName = "@evcerradamant";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradaantt:
                    paramName = "@evcerradaantt";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.estado:
                    paramName = "@estado";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;
                case enumDBFields.t930_denominacionCR:
                    paramName = "@t930_denominacionCR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.t935_idcategoriaprofesional:
                    paramName = "@t935_idcategoriaprofesional";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.evcurso:
                    paramName = "@evcurso";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcursoevaluador:
                    paramName = "@evcursoevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evabierta:
                    paramName = "@evabierta";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evabiertaevaluador:
                    paramName = "@evabiertaevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evfirmadaval:
                    paramName = "@evfirmadaval";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evfirmadanoval:
                    paramName = "@evfirmadanoval";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evfirmada:
                    paramName = "@evfirmada";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evfirmadaevaluador:
                    paramName = "@evfirmadaevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evautomaticaval:
                    paramName = "@evautomaticaval";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evautomaticanoval:
                    paramName = "@evautomaticanoval";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evautomatica:
                    paramName = "@evautomatica";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evautomaticaevaluador:
                    paramName = "@evautomaticaevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradaval:
                    paramName = "@evcerradaval";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradanoval:
                    paramName = "@evcerradanoval";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerrada:
                    paramName = "@evcerrada";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evcerradaevaluador:
                    paramName = "@evcerradaevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ev:
                    paramName = "@ev";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evevaluador:
                    paramName = "@evevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.profevabierta:
                    paramName = "@profevabierta";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.profevcurso:
                    paramName = "@profevcurso";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.profevcerrada:
                    paramName = "@profevcerrada";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.profevfirmada:
                    paramName = "@profevfirmada";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.profevautomatica:
                    paramName = "@profevautomatica";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.prof:
                    paramName = "@prof";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.profsinevaluador:
                    paramName = "@profsinevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evdorsinconfirmar:
                    paramName = "@evdorsinconfirmar";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evdorconfirmado:
                    paramName = "@evdorconfirmado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.evdor:
                    paramName = "@evdor";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

               

                 case enumDBFields.EvaluacionesABI:
                    paramName = "@EvaluacionesABI";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;


                case enumDBFields.EvaluacionesABIEvaluador:
                    paramName = "@EvaluacionesABIEvaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                

                case enumDBFields.EvaluacionesCUR:
                    paramName = "@EvaluacionesCUR";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                 
                  
                case enumDBFields.EvaluacionesCUREvaluador:
                    paramName = "@EvaluacionesCUREvaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.EvaluacionesCCF:
                    paramName = "@EvaluacionesCCF";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.EvaluacionesCCFValida:
                    paramName = "@EvaluacionesCCFValida";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.EvaluacionesCCFNoValida:
                    paramName = "@EvaluacionesCCFNoValida";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.EvaluacionesCCFEvaluador:
                    paramName = "@EvaluacionesCCFEvaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                       
                case enumDBFields.EvaluacionesCSF:
                    paramName = "@EvaluacionesCSF";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break; 
                
                case enumDBFields.EvaluacionesCSFValida:
                    paramName = "@EvaluacionesCSFValida";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;    

                case enumDBFields.EvaluacionesCSFNoValida:
                    paramName = "@EvaluacionesCSFNoValida";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;    

                case enumDBFields.EvaluacionesCSFEvaluador:
                    paramName = "@EvaluacionesCSFEvaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;    

                case enumDBFields.Totalevaluacionesvalidas:
                    paramName = "@Totalevaluacionesvalidas";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;    

                case enumDBFields.Totalevaluacionesnovalidas:
                    paramName = "@Totalevaluacionesnovalidas";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;        
            
                case enumDBFields.Totalevaluacionescerradas:
                    paramName = "@Totalevaluacionescerradas";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.Totalevaluacionescerradasevaluador:
                    paramName = "@Totalevaluacionescerradasevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.Totalevaluaciones:
                    paramName = "@Totalevaluaciones";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      
                
                case enumDBFields.Totalevaluadoresevaluaciones:
                    paramName = "@Totalevaluadoresevaluaciones";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.Profesionalessinevaluador:
                    paramName = "@Profesionalessinevaluador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      
               
                case enumDBFields.Evaluadoressinconfirmarequipo:
                    paramName = "@Evaluadoressinconfirmarequipo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      
               
                case enumDBFields.Evaluadoresequipoconfirmado:
                    paramName = "@Evaluadoresequipoconfirmado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.Totalevaluadoresevaluadores:
                    paramName = "@Totalevaluadoresevaluadores";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.ProfesionalesABI:
                    paramName = "@ProfesionalesABI";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      
             
                case enumDBFields.ProfesionalesCUR:
                    paramName = "@ProfesionalesCUR";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.ProfesionalesCCF:
                    paramName = "@ProfesionalesCCF";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      


                case enumDBFields.ProfesionalesCSF:
                    paramName = "@ProfesionalesCSF";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.ProfesionalesCER:
                    paramName = "@ProfesionalesCER";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.Totalprofesionales:
                    paramName = "@Totalprofesionales";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      


                case enumDBFields.Profesionalessinevaluacion:
                    paramName = "@Profesionalessinevaluacion";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

            

                case enumDBFields.desde_evaluaciones:
                    paramName = "@desde_evaluaciones";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.hasta_evaluaciones:
                    paramName = "@hasta_evaluaciones";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.t001_fecantigu_evaluaciones:
                    paramName = "@t001_fecantigu_evaluaciones";
                    paramType = SqlDbType.Date;
                    paramSize = 4;
                    break;

                case enumDBFields.estadoprofesional_evaluaciones:
                    paramName = "@estadoprofesional_evaluaciones";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;

                case enumDBFields.t941_idcolectivo_evaluaciones:
                    paramName = "@t941_idcolectivo_evaluaciones";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;      

                case enumDBFields.t930_denominacionCR_evaluaciones:
                    paramName = "@t930_denominacionCR_evaluaciones";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break; 

                case enumDBFields.idnodo_evaluadores:
                    paramName = "@t303_idnodo_evaluadores";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.desde_colectivos:
                    paramName = "@desde_colectivos";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
            
                case enumDBFields.hasta_colectivos:
                    paramName = "@hasta_colectivos";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                

                case enumDBFields.t001_fecantigu_colectivos:
                    paramName = "@t001_fecantigu_colectivos";
                    paramType = SqlDbType.Date;
                    paramSize = 4;
                    break;
            
                case enumDBFields.idnodos_colectivos:
                    paramName = "@t303_idnodo_colectivos";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
            
            
                case enumDBFields.idcolectivo_colectivos:
                    paramName = "@t941_idcolectivo_colectivos";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;


                case enumDBFields.t001_idficepievaluador_colectivos:
                    paramName = "@t001_idficepievaluador_colectivos";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.Totalevaluacionesevaluador:
                    paramName = "@Totalevaluacionesevaluador";
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