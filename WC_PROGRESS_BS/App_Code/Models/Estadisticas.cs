using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.Progress.Models
{
    /// <summary>
    /// Descripción breve de Estadisticas
    /// </summary>
    /// 
    [Serializable]
    public class Estadisticas
    {
        public Estadisticas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        //PROPIEDADES        
        private Nullable<short> _t932_idfoto;
        private int _desde;
        private int _hasta;
        private DateTime _t001_fecantigu;
        private short _profundidad;
        private int _t001_idficepi;
        private Nullable<short> _t941_idcolectivo;

        private int _profevh;
        private int _profevm;
        private int _profnoevh;
        private int _profnoevm;
        private int _profevhant;
        private int _profevmant;
        private int _profnoevhant;
        private int _profnoevmant;
        private int _evcursoh;
        private int _evcursom;
        private int _evfirmadah;
        private int _evfirmadam;
        private int _evautomaticah;
        private int _evautomaticam;
        private int _evcursohant;
        private int _evcursomant;
        private int _evfirmadahant;
        private int _evfirmadamant;
        private int _evautomaticahant;
        private int _evautomaticamant;
        //Totales calculados como sumas de los anteriores
        private int _profevt;
        private int _profevantt;
        private int _profnoevt;
        private int _profnoevantt;
        private int _profht;
        private int _profmt;
        private int _proft;
        private int _profhantt;
        private int _profmantt;
        private int _profantt;
        private int _evcursot;
        private int _evcursoantt;
        private int _evfirmadat;
        private int _evfirmadaantt;
        private int _evautomaticat;
        private int _evautomaticaantt;
        private int _evht;
        private int _evmt;
        private int _evt;
        private int _evhantt;
        private int _evmantt;
        private int _evantt;
        private int _evabiertah;
        private int _evabiertam;
        private int _evabiertat;
        private int _evabiertahant;
        private int _evabiertamant;
        private int _evabiertaantt;
        private int _evcerradah;
        private int _evcerradam;
        private int _evcerradat;
        private int _evcerradahant;
        private int _evcerradamant;
        private int _evcerradaantt;


        public Nullable<short> t932_idfoto
        {
            get { return _t932_idfoto; }
            set { _t932_idfoto = value; }
        }

        public int Desde
        {
            get { return _desde; }
            set { _desde = value; }
        }

        public int Hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }

        public DateTime t001_fecantigu
        {
            get { return _t001_fecantigu; }
            set { _t001_fecantigu = value; }
        }

        public short Profundidad
        {
            get { return _profundidad; }
            set { _profundidad = value; }
        }

        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public Nullable<short> t941_idcolectivo
        {
            get { return _t941_idcolectivo; }
            set { _t941_idcolectivo = value; }
        }

        public int profevh
        {
            get { return _profevh; }
            set { _profevh = value; }
        }
        public int profevm
        {
            get { return _profevm; }
            set { _profevm = value; }
        }
        public int profnoevh
        {
            get { return _profnoevh; }
            set { _profnoevh = value; }
        }
        public int profnoevm
        {
            get { return _profnoevm; }
            set { _profnoevm = value; }
        }
        public int profevhant
        {
            get { return _profevhant; }
            set { _profevhant = value; }
        }
        public int profevmant
        {
            get { return _profevmant; }
            set { _profevmant = value; }
        }
        public int profnoevhant
        {
            get { return _profnoevhant; }
            set { _profnoevhant = value; }
        }
        public int profnoevmant
        {
            get { return _profnoevmant; }
            set { _profnoevmant = value; }
        }
        public int evcursoh
        {
            get { return _evcursoh; }
            set { _evcursoh = value; }
        }
        public int evcursom
        {
            get { return _evcursom; }
            set { _evcursom = value; }
        }
        public int evfirmadah
        {
            get { return _evfirmadah; }
            set { _evfirmadah = value; }
        }
        public int evfirmadam
        {
            get { return _evfirmadam; }
            set { _evfirmadam = value; }
        }
        public int evautomaticah
        {
            get { return _evautomaticah; }
            set { _evautomaticah = value; }
        }
        public int evautomaticam
        {
            get { return _evautomaticam; }
            set { _evautomaticam = value; }
        }
        public int evcursohant
        {
            get { return _evcursohant; }
            set { _evcursohant = value; }
        }
        public int evcursomant
        {
            get { return _evcursomant; }
            set { _evcursomant = value; }
        }
        public int evfirmadahant
        {
            get { return _evfirmadahant; }
            set { _evfirmadahant = value; }
        }
        public int evfirmadamant
        {
            get { return _evfirmadamant; }
            set { _evfirmadamant = value; }
        }
        public int evautomaticahant
        {
            get { return _evautomaticahant; }
            set { _evautomaticahant = value; }
        }
        public int evautomaticamant
        {
            get { return _evautomaticamant; }
            set { _evautomaticamant = value; }
        }
        public int profevt
        {
            get { return _profevt; }
            set { _profevt = value; }
        }
        public int profevantt
        {
            get { return _profevantt; }
            set { _profevantt = value; }
        }
        public int profnoevt
        {
            get { return _profnoevt; }
            set { _profnoevt = value; }
        }
        public int profnoevantt
        {
            get { return _profnoevantt; }
            set { _profnoevantt = value; }
        }
        public int profht
        {
            get { return _profht; }
            set { _profht = value; }
        }
        public int profmt
        {
            get { return _profmt; }
            set { _profmt = value; }
        }
        public int proft
        {
            get { return _proft; }
            set { _proft = value; }
        }
        public int profhantt
        {
            get { return _profhantt; }
            set { _profhantt = value; }
        }
        public int profmantt
        {
            get { return _profmantt; }
            set { _profmantt = value; }
        }
        public int profantt
        {
            get { return _profantt; }
            set { _profantt = value; }
        }
        public int evcursot
        {
            get { return _evcursot; }
            set { _evcursot = value; }
        }
        public int evcursoantt
        {
            get { return _evcursoantt; }
            set { _evcursoantt = value; }
        }
        public int evfirmadat
        {
            get { return _evfirmadat; }
            set { _evfirmadat = value; }
        }
        public int evfirmadaantt
        {
            get { return _evfirmadaantt; }
            set { _evfirmadaantt = value; }
        }
        public int evautomaticat
        {
            get { return _evautomaticat; }
            set { _evautomaticat = value; }
        }
        public int evautomaticaantt
        {
            get { return _evautomaticaantt; }
            set { _evautomaticaantt = value; }
        }
        public int evht
        {
            get { return _evht; }
            set { _evht = value; }
        }
        public int evmt
        {
            get { return _evmt; }
            set { _evmt = value; }
        }
        public int evt
        {
            get { return _evt; }
            set { _evt = value; }
        }
        public int evhantt
        {
            get { return _evhantt; }
            set { _evhantt = value; }
        }
        public int evmantt
        {
            get { return _evmantt; }
            set { _evmantt = value; }
        }
        public int evantt
        {
            get { return _evantt; }
            set { _evantt = value; }
        }

        public int evabiertah
        {
            get { return _evabiertah; }
            set { _evabiertah = value; }
        }

        public int evabiertam
        {
            get { return _evabiertam; }
            set { _evabiertam = value; }
        }

        public int evabiertat
        {
            get { return _evabiertat; }
            set { _evabiertat = value; }
        }
        public int evabiertahant
        {
            get { return _evabiertahant; }
            set { _evabiertahant = value; }
        }
        public int evabiertamant
        {
            get { return _evabiertamant; }
            set { _evabiertamant = value; }
        }
        public int evabiertaantt
        {
            get { return _evabiertaantt; }
            set { _evabiertaantt = value; }
        }

        public int evcerradah
        {
            get { return _evcerradah; }
            set { _evcerradah = value; }
        }
        public int evcerradam
        {
            get { return _evcerradam; }
            set { _evcerradam = value; }
        }
        public int evcerradat
        {
            get { return _evcerradat; }
            set { _evcerradat = value; }
        }
        public int evcerradahant
        {
            get { return _evcerradahant; }
            set { _evcerradahant = value; }
        }
        public int evcerradamant
        {
            get { return _evcerradamant; }
            set { _evcerradamant = value; }
        }
        public int evcerradaantt
        {
            get { return _evcerradaantt; }
            set { _evcerradaantt = value; }
        }




    }
    public class OtrasEstadisticas
    {
        public OtrasEstadisticas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        //PROPIEDADES  josemi    
        private int _desde;
        private int _hasta;
        private DateTime _t001_fecantigu;
        private short _profundidad;
        private Nullable<int> _t001_idficepi;
        private Nullable<short> _t941_idcolectivo;
        private char _estado;
        private string _t930_denominacionCR;
        private Nullable<int> _t935_idcategoriaprofesional;       
        
        
        private int _evcurso;
        private int _evcursoevaluador;
        private int _evabierta;
        private int _evabiertaevaluador;
        private int _evfirmadaval;
        private int _evfirmadanoval;
        private int _evfirmada;
        private int _evfirmadaevaluador;
        private int _evautomaticaval;
        private int _evautomaticanoval;
        private int _evautomatica;
        private int _evautomaticaevaluador;
        private int _evcerradaval;
        private int _evcerradanoval;
        private int _evcerrada;
        private int _evcerradaevaluador;
        private int _ev;
        private int _evevaluador;
        private int _profevabierta;
        private int _profevcurso;
        private int _profevcerrada;
        private int _profevfirmada;
        private int _profevautomatica;
        private int _prof;
        private int _profsinevaluador;
        private int _evdorsinconfirmar;
        private int _evdorconfirmado;
        private int _evdor;

        public int Desde
        {
            get { return _desde; }
            set { _desde = value; }
        }

        public int Hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }

        public DateTime t001_fecantigu
        {
            get { return _t001_fecantigu; }
            set { _t001_fecantigu = value; }
        }

        public short Profundidad
        {
            get { return _profundidad; }
            set { _profundidad = value; }
        }

        public Nullable<int> t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public Nullable<short> t941_idcolectivo
        {
            get { return _t941_idcolectivo; }
            set { _t941_idcolectivo = value; }
        }

        public char estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string t930_denominacionCR
        {
            get { return _t930_denominacionCR; }
            set { _t930_denominacionCR = value; }
        }

        public Nullable<int> t935_idcategoriaprofesional
        {
            get { return _t935_idcategoriaprofesional; }
            set { _t935_idcategoriaprofesional = value; }
        }

        public int evcurso
        {
            get { return _evcurso; }
            set { _evcurso = value; }
        }

        public int evcursoevaluador
        {
            get { return _evcursoevaluador; }
            set { _evcursoevaluador = value; }
        }

        public int evabierta
        {
            get { return _evabierta; }
            set { _evabierta = value; }
        }

        public int evabiertaevaluador
        {
            get { return _evabiertaevaluador; }
            set { _evabiertaevaluador = value; }
        }

        public int evfirmadaval
        {
            get { return _evfirmadaval; }
            set { _evfirmadaval = value; }
        }

        public int evfirmadanoval
        {
            get { return _evfirmadanoval; }
            set { _evfirmadanoval = value; }
        }

        public int evfirmada
        {
            get { return _evfirmada; }
            set { _evfirmada = value; }
        }

        public int evfirmadaevaluador
        {
            get { return _evfirmadaevaluador; }
            set { _evfirmadaevaluador = value; }
        }

        public int evautomaticaval
        {
            get { return _evautomaticaval; }
            set { _evautomaticaval = value; }
        }

        public int evautomaticanoval
        {
            get { return _evautomaticanoval; }
            set { _evautomaticanoval = value; }
        }

        public int evautomatica
        {
            get { return _evautomatica; }
            set { _evautomatica = value; }
        }

        public int evautomaticaevaluador
        {
            get { return _evautomaticaevaluador; }
            set { _evautomaticaevaluador = value; }
        }

        public int evcerradaval
        {
            get { return _evcerradaval; }
            set { _evcerradaval = value; }
        }

        public int evcerradanoval
        {
            get { return _evcerradanoval; }
            set { _evcerradanoval = value; }
        }

        public int evcerrada
        {
            get { return _evcerrada; }
            set { _evcerrada = value; }
        }

        public int evcerradaevaluador
        {
            get { return _evcerradaevaluador; }
            set { _evcerradaevaluador = value; }
        }

        public int ev
        {
            get { return _ev; }
            set { _ev = value; }
        }

        public int evevaluador
        {
            get { return _evevaluador; }
            set { _evevaluador = value; }
        }

        public int profevabierta
        {
            get { return _profevabierta; }
            set { _profevabierta = value; }
        }

        public int profevcurso
        {
            get { return _profevcurso; }
            set { _profevcurso = value; }
        }

        public int profevcerrada
        {
            get { return _profevcerrada; }
            set { _profevcerrada = value; }
        }

        public int profevfirmada
        {
            get { return _profevfirmada; }
            set { _profevfirmada = value; }
        }

        public int profevautomatica
        {
            get { return _profevautomatica; }
            set { _profevautomatica = value; }
        }

        public int prof
        {
            get { return _prof; }
            set { _prof = value; }
        }

        public int profsinevaluador
        {
            get { return _profsinevaluador; }
            set { _profsinevaluador = value; }
        }

        public int evdorsinconfirmar
        {
            get { return _evdorsinconfirmar; }
            set { _evdorsinconfirmar = value; }
        }

        public int evdorconfirmado
        {
            get { return _evdorconfirmado; }
            set { _evdorconfirmado = value; }
        }

        public int evdor
        {
            get { return _evdor; }
            set { _evdor = value; }
        }
    }


    [Serializable]
    public class OtrasEstadisticasRRHH
    {
        public OtrasEstadisticasRRHH()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        //PROPIEDADES EVALUACIONES
        private int _desde;
        private int _hasta;
        private DateTime _t001_fecantigu;
        private Nullable<short> _t941_idcolectivo;
        private Nullable<char> _estado;
        private string _t930_denominacionCR;
        private Nullable<int> _t001_idficepi;

        public int Desde
        {
            get { return _desde; }
            set { _desde = value; }
        }

        public int Hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }

        public DateTime t001_fecantigu
        {
            get { return _t001_fecantigu; }
            set { _t001_fecantigu = value; }
        }

        public Nullable<int> t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        public Nullable<short> t941_idcolectivo
        {
            get { return _t941_idcolectivo; }
            set { _t941_idcolectivo = value; }
        }

        public Nullable<char> estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string t930_denominacionCR
        {
            get { return _t930_denominacionCR; }
            set { _t930_denominacionCR = value; }
        }

        //PROPIEDADES EVALUAODRES
        private Nullable<int> t303_idnodo_evaluadores;

        public Nullable<int> T303_idnodo_evaluadores
        {
            get { return t303_idnodo_evaluadores; }
            set { t303_idnodo_evaluadores = value; }
        }

        //PROPIEDADES COLECTIVOS
        private int _desdeColectivos;

        public int DesdeColectivos
        {
            get { return _desdeColectivos; }
            set { _desdeColectivos = value; }
        }
        private int _hastaColectivos;

        public int HastaColectivos
        {
            get { return _hastaColectivos; }
            set { _hastaColectivos = value; }
        }
        private DateTime _t001_fecantiguColectivos;

        public DateTime T001_fecantiguColectivos
        {
            get { return _t001_fecantiguColectivos; }
            set { _t001_fecantiguColectivos = value; }
        }
        private Nullable<int> _t303_idnodo_colectivos;

        public Nullable<int> T303_idnodo_colectivos
        {
            get { return _t303_idnodo_colectivos; }
            set { _t303_idnodo_colectivos = value; }
        }
        private Nullable<short> _t941_idcolectivo_colectivos;

        public Nullable<short> T941_idcolectivo_colectivos
        {
            get { return _t941_idcolectivo_colectivos; }
            set { _t941_idcolectivo_colectivos = value; }
        }
       



        //RESULTADO EVALUACIONES
        private int _evaluacionesABI;

        public int EvaluacionesABI
        {
            get { return _evaluacionesABI; }
            set { _evaluacionesABI = value; }
        }
        private int _evaluacionesABIEvaluador;

        public int EvaluacionesABIEvaluador
        {
            get { return _evaluacionesABIEvaluador; }
            set { _evaluacionesABIEvaluador = value; }
        }
        private int _evaluacionesCUR;

        public int EvaluacionesCUR
        {
            get { return _evaluacionesCUR; }
            set { _evaluacionesCUR = value; }
        }
        private int _evaluacionesCUREvaluador;

        public int EvaluacionesCUREvaluador
        {
            get { return _evaluacionesCUREvaluador; }
            set { _evaluacionesCUREvaluador = value; }
        }
        private int _evaluacionesCCF;

        public int EvaluacionesCCF
        {
            get { return _evaluacionesCCF; }
            set { _evaluacionesCCF = value; }
        }
        private int _evaluacionesCCFValida;

        public int EvaluacionesCCFValida
        {
            get { return _evaluacionesCCFValida; }
            set { _evaluacionesCCFValida = value; }
        }
        private int _evaluacionesCCFNoValida;

        public int EvaluacionesCCFNoValida
        {
            get { return _evaluacionesCCFNoValida; }
            set { _evaluacionesCCFNoValida = value; }
        }
        private int _evaluacionesCCFEvaluador;

        public int EvaluacionesCCFEvaluador
        {
            get { return _evaluacionesCCFEvaluador; }
            set { _evaluacionesCCFEvaluador = value; }
        }
        private int _evaluacionesCSF;

        public int EvaluacionesCSF
        {
            get { return _evaluacionesCSF; }
            set { _evaluacionesCSF = value; }
        }
        private int _evaluacionesCSFValida;

        public int EvaluacionesCSFValida
        {
            get { return _evaluacionesCSFValida; }
            set { _evaluacionesCSFValida = value; }
        }
        private int _evaluacionesCSFNoValida;

        public int EvaluacionesCSFNoValida
        {
            get { return _evaluacionesCSFNoValida; }
            set { _evaluacionesCSFNoValida = value; }
        }
        private int _evaluacionesCSFEvaluador;

        public int EvaluacionesCSFEvaluador
        {
            get { return _evaluacionesCSFEvaluador; }
            set { _evaluacionesCSFEvaluador = value; }
        }
        private int _totalevaluacionesvalidas;

        public int Totalevaluacionesvalidas
        {
            get { return _totalevaluacionesvalidas; }
            set { _totalevaluacionesvalidas = value; }
        }
        private int _totalevaluacionesnovalidas;

        public int Totalevaluacionesnovalidas
        {
            get { return _totalevaluacionesnovalidas; }
            set { _totalevaluacionesnovalidas = value; }
        }
        private int _totalevaluacionescerradas;

        public int Totalevaluacionescerradas
        {
            get { return _totalevaluacionescerradas; }
            set { _totalevaluacionescerradas = value; }
        }
        private int _totalevaluacionescerradasevaluador;

        public int Totalevaluacionescerradasevaluador
        {
            get { return _totalevaluacionescerradasevaluador; }
            set { _totalevaluacionescerradasevaluador = value; }
        }
        private int _totalevaluaciones;

        public int Totalevaluaciones
        {
            get { return _totalevaluaciones; }
            set { _totalevaluaciones = value; }
        }
        private int _totalevaluadoresevaluaciones;

        public int Totalevaluadoresevaluaciones
        {
            get { return _totalevaluadoresevaluaciones; }
            set { _totalevaluadoresevaluaciones = value; }
        }

        private int _totalevaluacionesevaluador;

        public int Totalevaluacionesevaluador
        {
            get { return _totalevaluacionesevaluador; }
            set { _totalevaluacionesevaluador = value; }
        }

        //RESULTADO EVALUADORES
        private int _profesionalessinevaluador;

        public int Profesionalessinevaluador
        {
            get { return _profesionalessinevaluador; }
            set { _profesionalessinevaluador = value; }
        }
        private int _evaluadoressinconfirmarequipo;

        public int Evaluadoressinconfirmarequipo
        {
            get { return _evaluadoressinconfirmarequipo; }
            set { _evaluadoressinconfirmarequipo = value; }
        }
        private int _evaluadoresequipoconfirmado;

        public int Evaluadoresequipoconfirmado
        {
            get { return _evaluadoresequipoconfirmado; }
            set { _evaluadoresequipoconfirmado = value; }
        }
        private int _totalevaluadoresevaluadores;

        public int Totalevaluadoresevaluadores
        {
            get { return _totalevaluadoresevaluadores; }
            set { _totalevaluadoresevaluadores = value; }
        }


        //RESULTADOS COLECTIVOS
        private int _profesionalesABI;

        public int ProfesionalesABI
        {
            get { return _profesionalesABI; }
            set { _profesionalesABI = value; }
        }
        private int _profesionalesCUR;

        public int ProfesionalesCUR
        {
            get { return _profesionalesCUR; }
            set { _profesionalesCUR = value; }
        }
        private int _profesionalesCCF;

        public int ProfesionalesCCF
        {
            get { return _profesionalesCCF; }
            set { _profesionalesCCF = value; }
        }
        private int _profesionalesCSF;

        public int ProfesionalesCSF
        {
            get { return _profesionalesCSF; }
            set { _profesionalesCSF = value; }
        }
        private int _profesionalesCER;

        public int ProfesionalesCER
        {
            get { return _profesionalesCER; }
            set { _profesionalesCER = value; }
        }
        private int _totalprofesionales;

        public int Totalprofesionales
        {
            get { return _totalprofesionales; }
            set { _totalprofesionales = value; }
        }
        private int _profesionalessinevaluacion;

        public int Profesionalessinevaluacion
        {
            get { return _profesionalessinevaluacion; }
            set { _profesionalessinevaluacion = value; }
        }


    }

}