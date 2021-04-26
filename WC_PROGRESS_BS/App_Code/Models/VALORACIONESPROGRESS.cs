using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{
   
   public class VALORACIONESPROGRESS
    {
        #region mi evaluacion
        public class miEval
        {
            private int _idvaloracion;
            private string _evaluador;
            private string _estado;
            private DateTime _fecapertura;
            private Nullable<DateTime> _feccierre;
            private int _idformulario;

            public int idvaloracion
            {
                get { return _idvaloracion; }
                set { _idvaloracion = value; }
            }

            public string evaluador
            {
                get { return _evaluador; }
                set { _evaluador = value; }
            }

            public string estado
            {
                get { return _estado; }
                set { _estado = value; }
            }

            public DateTime fecapertura
            {
                get { return _fecapertura; }
                set { _fecapertura = value; }
            }

            public Nullable<DateTime> feccierre
            {
                get { return _feccierre; }
                set { _feccierre = value; }
            }

            public int idformulario
            {
                get { return _idformulario; }
                set { _idformulario = value; }
            }

            public miEval(int idval, string nombre, string est, DateTime fecap, Nullable<DateTime> feccie, int idfor)
            {
                _idvaloracion = idval;
                _evaluador = nombre;
                _estado = est;
                _fecapertura = fecap;
                _feccierre = feccie;
                _idformulario = idfor;
            }

           
        }

        #endregion
       
       
        public class TemporadaProgress
        {

            public TemporadaProgress()
            {

            }

            private int _temporada;
            private int _desde;
            private int _hasta;
            private DateTime _t936_referenciaantiguedad;
            private int _temporadaprogress;
            private string _periodoprogress;

            public int Temporada
            {
                get { return _temporada; }
                set { _temporada = value; }
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

            public DateTime T936_referenciaantiguedad
            {
                get { return _t936_referenciaantiguedad; }
                set { _t936_referenciaantiguedad = value; }
            }

            public int temporadaprogress
            {
                get { return _temporadaprogress; }
                set { _temporadaprogress = value; }
            }

            public string periodoprogress
            {
                get { return _periodoprogress; }
                set { _periodoprogress = value; }
            }


            public TemporadaProgress(int temporada, int desde, int hasta, DateTime t936_referenciaantiguedad)
            {
                _temporada = temporada;
                _desde = desde;
                _hasta = hasta;
                _t936_referenciaantiguedad = t936_referenciaantiguedad;
            }

            public TemporadaProgress(int temporada, int desde, int hasta, DateTime t936_referenciaantiguedad, string periodoprogress)
            {
                _temporada = temporada;
                _desde = desde;
                _hasta = hasta;
                _t936_referenciaantiguedad = t936_referenciaantiguedad;
                _periodoprogress = periodoprogress;
            }


        }

        #region de mi equipo
        public class EvalMiEquipo
        {
            private int _idvaloracion;
            private string _evaluador;
            private string _evaluado;
            private string _estado;
            private string _estadooriginal;
            private DateTime _fecapertura;
            private Nullable<DateTime> _feccierre;
            private int _idformulario;
            private string _motivo;
            private int _idficepi_evaluador;
            private bool _mostrarcombo;

            public bool Mostrarcombo
            {
                get { return _mostrarcombo; }
                set { _mostrarcombo = value; }
            }



           

            public int idvaloracion
            {
                get { return _idvaloracion; }
                set { _idvaloracion = value; }
            }

            public string evaluador
            {
                get { return _evaluador; }
                set { _evaluador = value; }
            }

            public string evaluado
            {
                get { return _evaluado; }
                set { _evaluado = value; }
            }

            public string estado
            {
                get { return _estado; }
                set { _estado = value; }
            }

            public string estadooriginal
            {
                get { return _estadooriginal; }
                set { _estadooriginal = value; }
            }

            public DateTime fecapertura
            {
                get { return _fecapertura; }
                set { _fecapertura = value; }
            }

            public Nullable<DateTime> feccierre
            {
                get { return _feccierre; }
                set { _feccierre = value; }
            }

            public int idformulario
            {
                get { return _idformulario; }
                set { _idformulario = value; }
            }

            public string Motivo
            {
                get { return _motivo; }
                set { _motivo = value; }
            }

            public int Idficepi_evaluador
            {
              get { return _idficepi_evaluador; }
              set { _idficepi_evaluador = value; }
            }

            /// <summary>
            /// Constructor vacío
            /// </summary>
            public EvalMiEquipo(){}

            public EvalMiEquipo(int idval, string evaluador, string evaluado, string est, string estadooriginal, DateTime fecap, Nullable<DateTime> feccie, int idfor)
            {
                _idvaloracion = idval;                
                _evaluador = evaluador;
                _evaluado = evaluado;
                _estado = est;
                _estadooriginal = estadooriginal;
                _fecapertura = fecap;
                _feccierre = feccie;
                _idformulario = idfor;
            }

            public EvalMiEquipo(int idval, string evaluador, string evaluado, string est, string estadooriginal, DateTime fecap, Nullable<DateTime> feccie, string motivo, int idfor )
            {
                _idvaloracion = idval;
                _evaluador = evaluador;
                _evaluado = evaluado;
                _estado = est;
                _estadooriginal = estadooriginal;
                _fecapertura = fecap;
                _feccierre = feccie;
                _idformulario = idfor;
                _motivo = motivo;
            }


            public EvalMiEquipo(int idval, int idficepi_evaluador, string evaluador, string evaluado, string est, string estadooriginal, DateTime fecap, Nullable<DateTime> feccie, string motivo, int idfor)
            {
                _idvaloracion = idval;
                _idficepi_evaluador = idficepi_evaluador;
                _evaluador = evaluador;
                _evaluado = evaluado;
                _estado = est;
                _estadooriginal = estadooriginal;
                _fecapertura = fecap;
                _feccierre = feccie;
                _idformulario = idfor;
                _motivo = motivo;
            }

            public EvalMiEquipo(int idval, int idficepi_evaluador, string evaluador, string evaluado, string est, string estadooriginal, DateTime fecap, Nullable<DateTime> feccie, string motivo, bool mostrarcombo, int idfor )
            {
                _idvaloracion = idval;
                _idficepi_evaluador = idficepi_evaluador;
                _evaluador = evaluador;
                _evaluado = evaluado;
                _estado = est;
                _estadooriginal = estadooriginal;
                _fecapertura = fecap;
                _feccierre = feccie;
                _idformulario = idfor;
                _mostrarcombo = mostrarcombo;
                _motivo = motivo;
                
            }

        }
        #endregion



        #region Búsqueda evaluaciones
        [Serializable]
        public class busqEval
        {
            private int _idvaloracion;
            private int _idformulario;
            private int _idevaluador;
            private string _nombrecompletoevaluador;

            public string Nombrecompletoevaluador
            {
                get { return _nombrecompletoevaluador; }
                set { _nombrecompletoevaluador = value; }
            }
            private string _nombrecortoevaluador;

            public string Nombrecortoevaluador
            {
                get { return _nombrecortoevaluador; }
                set { _nombrecortoevaluador = value; }
            }            
            private string _evaluador;
            private string _correoevaluador;

            public string Correoevaluador
            {
                get { return _correoevaluador; }
                set { _correoevaluador = value; }
            }

            private int _idevaluado;
            private string _nombrecompletoevaluado;

            public string Nombrecompletoevaluado
            {
                get { return _nombrecompletoevaluado; }
                set { _nombrecompletoevaluado = value; }
            }
            private string _nombrecortoevaluado;

            public string Nombrecortoevaluado
            {
                get { return _nombrecortoevaluado; }
                set { _nombrecortoevaluado = value; }
            }
            private string _evaluado;
            private string _correoevaluado;

            public string Correoevaluado
            {
                get { return _correoevaluado; }
                set { _correoevaluado = value; }
            }

            private bool _permitircambioestado;

            public bool Permitircambioestado
            {
                get { return _permitircambioestado; }
                set { _permitircambioestado = value; }
            }

            private string _codestado;
            private string _estado;
            private DateTime _fecha;
            private Nullable<DateTime> _fechaCierre;
            




            public int idvaloracion
            {
                get { return _idvaloracion; }
                set { _idvaloracion = value; }
            }

            public int idevaluador
            {
                get { return _idevaluador; }
                set { _idevaluador = value; }
            }

            public int idevaluado
            {
                get { return _idevaluado; }
                set { _idevaluado = value; }
            }

            public string evaluador
            {
                get { return _evaluador; }
                set { _evaluador = value; }
            }

            public string evaluado
            {
                get { return _evaluado; }
                set { _evaluado = value; }
            }

            public string codestado
            {
                get { return _codestado; }
                set { _codestado = value; }
            }

            public string estado
            {
                get { return _estado; }
                set { _estado = value; }
            }

            public DateTime fecha
            {
                get { return _fecha; }
                set { _fecha = value; }
            }

            public Nullable<DateTime> FechaCierre
            {
                get { return _fechaCierre; }
                set { _fechaCierre = value; }
            }

            public int idformulario
            {
                get { return _idformulario; }
                set { _idformulario = value; }
            }

            public busqEval(int idvaloracion, int idevaluador, string evaluador, string evaluado, string codestado , string estado, DateTime fecha,  Nullable<DateTime> FechaCierre, int idformulario)
            {
                _idvaloracion   = idvaloracion;
                _idevaluador = idevaluador;
                _evaluador      = evaluador;
                _evaluado       = evaluado;
                _codestado = codestado;
                _estado         = estado;
                _fecha          = fecha;
                _fechaCierre    = FechaCierre;
                _idformulario   = idformulario;
            }

            //Constructor Abrir evaluaciones ya firmadas
            public busqEval( int idformulario, int idvaloracion, int idevaluador, string nombrecompletoevaluador, string nombrecortoevaluador, string nombreyapellidosevaluador, string correoevaluador, int idficepievaluado, string nombrecompletoevaluado, string nombrecortoevaluado, string nombreyapellidosevaluado, string correoevaluado, bool permitircambioestado, string codestado, string estado, DateTime fecha, Nullable<DateTime> FechaCierre)
            {
                _idformulario = idformulario;
                _idvaloracion = idvaloracion;
                _idevaluador = idevaluador;
                _nombrecompletoevaluador = nombrecompletoevaluador; 
                _nombrecortoevaluador = nombrecortoevaluador;
                _correoevaluador = correoevaluador;
                _evaluador = nombreyapellidosevaluador;


                _idevaluado = idficepievaluado;
                _nombrecompletoevaluado = nombrecompletoevaluado; 
                _nombrecortoevaluado = nombrecortoevaluado;
                _evaluado = nombreyapellidosevaluado;
                _correoevaluado = correoevaluado;


                _permitircambioestado = permitircambioestado;
                _codestado = codestado;
                _estado = estado;
                _fecha = fecha;
                _fechaCierre = FechaCierre;
               
            }
        }

        [Serializable]
        public class FiltrosBusqueda
        {
//            private Nullable<DateTime> _fdesde;
//            private Nullable<DateTime> _fhasta;

            private int _idficepi_usuario;

            public int Idficepi_usuario
            {
                get { return _idficepi_usuario; }
                set { _idficepi_usuario = value; }
            }

            private string _origen;

            public string Origen
            {
                get { return _origen; }
                set { _origen = value; }
            }
            private Nullable<int> _desde;
            private Nullable<int> _hasta;
            private Nullable<int> _t001_idficepi;
            private Nullable<int> _t001_idficepi_evaluador;
            //private short _figura;
            private Nullable <short> _profundidad;
            private string _estado;
            private string _t930_denominacionCR;
            private string _t930_denominacionROL;
            private Nullable <short> _t941_idcolectivo;
            private Nullable<Int16> _t930_puntuacion;
            private List<short> _lestt930_gescli;
            private List<short> _lestt930_liderazgo;
            private List<short> _lestt930_planorga;
            private List<short> _lestt930_exptecnico;
            private List<short> _lestt930_cooperacion;
            private List<short> _lestt930_iniciativa;
            private List<short> _lestt930_perseverancia;
            private Nullable<short> _estaspectos;

            private string _txtNombre;

            public string txtNombre
            {
                get { return _txtNombre; }
                set { _txtNombre = value; }
            }
            //private Nullable<short> _estmejorar;

            //public Nullable<short> Estmejorar
            //{
            //    get { return _estmejorar; }
            //    set { _estmejorar = value; }
            //}
            


            //private List<short> _lestaspectos;
//            private string _lestt930_interesescar;
            private List<short> _lestt930_interesescar;
            private Nullable<short> _estreconocer;
            private Nullable<short> _estmejorar;
            private List<short> _lcaut930_gescli;
            private List<short> _lcaut930_liderazgo;
            private List<short> _lcaut930_planorga;
            private List<short> _lcaut930_exptecnico;
            private List<short> _lcaut930_cooperacion;
            private List<short> _lcaut930_iniciativa;
            private List<short> _lcaut930_perseverancia;
            private short _cauaspectos;
            private List<short> _lcauaspectos;
            private List<short> _lcaut930_interesescar;
            private Nullable<short> _caumejorar;


            private Nullable<short> _selectMejorar;

            public Nullable<short> SelectMejorar
            {
                get { return _selectMejorar; }
                set { _selectMejorar = value; }
            }
            private Nullable<short> _selectSuficiente;

            public Nullable<short> SelectSuficiente
            {
                get { return _selectSuficiente; }
                set { _selectSuficiente = value; }
            }
            private Nullable<short> _selectBueno;

            public Nullable<short> SelectBueno
            {
                get { return _selectBueno; }
                set { _selectBueno = value; }
            }
            private Nullable<short> _selectAlto;

            public Nullable<short> SelectAlto
            {
                get { return _selectAlto; }
                set { _selectAlto = value; }
            }


            private Nullable<short> _selectMejorarCAU;

            public Nullable<short> SelectMejorarCAU
            {
                get { return _selectMejorarCAU; }
                set { _selectMejorarCAU = value; }
            }
            private Nullable<short> _selectSuficienteCAU;

            public Nullable<short> SelectSuficienteCAU
            {
                get { return _selectSuficienteCAU; }
                set { _selectSuficienteCAU = value; }
            }
            private Nullable<short> _selectBuenoCAU;

            public Nullable<short> SelectBuenoCAU
            {
                get { return _selectBuenoCAU; }
                set { _selectBuenoCAU = value; }
            }
            private Nullable<short> _selectAltoCAU;

            public Nullable<short> SelectAltoCAU
            {
                get { return _selectAltoCAU; }
                set { _selectAltoCAU = value; }
            }



            public Nullable<int> desde
            {
                get { return _desde; }
                set { _desde = value; }
            }

            public Nullable<int> hasta
            {
                get { return _hasta; }
                set { _hasta = value; }
            }

            public Nullable<int> t001_idficepi
            {
                get { return _t001_idficepi; }
                set { _t001_idficepi = value; }
            }

            public Nullable<int> t001_idficepi_evaluador
            {
                get { return _t001_idficepi_evaluador; }
                set { _t001_idficepi_evaluador = value; }
            }

            //public short figura
            //{
            //    get { return _figura; }
            //    set { _figura = value; }
            //}

            public Nullable <short> profundidad
            {
                get { return _profundidad; }
                set { _profundidad = value; }
            }

            public string estado
            {
                get { return _estado; }
                set { _estado = value; }
            }

            public string t930_denominacionCR
            {
                get { return _t930_denominacionCR; }
                set { _t930_denominacionCR = value; }
            }

            public string t930_denominacionROL
            {
                get { return _t930_denominacionROL; }
                set { _t930_denominacionROL = value; }
            }

            public Nullable <short> t941_idcolectivo
            {
                get { return _t941_idcolectivo; }
                set { _t941_idcolectivo = value; }
            }

            public Nullable<Int16> t930_puntuacion
            {
                get { return _t930_puntuacion; }
                set { _t930_puntuacion = value; }
            }

            public List<short> lestt930_gescli  
            {
                get { return _lestt930_gescli; }
                set { _lestt930_gescli = value; }
            }
            public List<short> lestt930_liderazgo
            {
                get { return _lestt930_liderazgo; }
                set { _lestt930_liderazgo = value; }
            }
            public List<short> lestt930_planorga
            {
                get { return _lestt930_planorga; }
                set { _lestt930_planorga = value; }
            }
            public List<short> lestt930_exptecnico
            {
                get { return _lestt930_exptecnico; }
                set { _lestt930_exptecnico = value; }
            }
            public List<short> lestt930_cooperacion
            {
                get { return _lestt930_cooperacion; }
                set { _lestt930_cooperacion = value; }
            }
            public List<short> lestt930_iniciativa
            {
                get { return _lestt930_iniciativa; }
                set { _lestt930_iniciativa = value; }
            }
            public List<short> lestt930_perseverancia
            {
                get { return _lestt930_perseverancia; }
                set { _lestt930_perseverancia = value; }
            }

            public Nullable<short> estaspectos
            {
                get { return _estaspectos; }
                set { _estaspectos = value; }
            }

            //public List<short> lestaspectos
            //{
            //    get { return _lestaspectos; }
            //    set { _lestaspectos = value; }
            //}

            public List<short> lestt930_interesescar
            {
                get { return _lestt930_interesescar; }
                set { _lestt930_interesescar = value; }
            }

            public Nullable<short> estreconocer
            {
                get { return _estreconocer; }
                set { _estreconocer = value; }
            }

            public Nullable<short> estmejorar
            {
                get { return _estmejorar; }
                set { _estmejorar = value; }
            }

            public List<short> lcaut930_gescli
            {
                get { return _lcaut930_gescli; }
                set { _lcaut930_gescli = value; }
            }
            public List<short> lcaut930_liderazgo
            {
                get { return _lcaut930_liderazgo; }
                set { _lcaut930_liderazgo = value; }
            }
            public List<short> lcaut930_planorga
            {
                get { return _lcaut930_planorga; }
                set { _lcaut930_planorga = value; }
            }
            public List<short> lcaut930_exptecnico
            {
                get { return _lcaut930_exptecnico; }
                set { _lcaut930_exptecnico = value; }
            }
            public List<short> lcaut930_cooperacion
            {
                get { return _lcaut930_cooperacion; }
                set { _lcaut930_cooperacion = value; }
            }
            public List<short> lcaut930_iniciativa
            {
                get { return _lcaut930_iniciativa; }
                set { _lcaut930_iniciativa = value; }
            }
            public List<short> lcaut930_perseverancia
            {
                get { return _lcaut930_perseverancia; }
                set { _lcaut930_perseverancia = value; }
            }
            public short cauaspectos
            {
                get { return _cauaspectos; }
                set { _cauaspectos = value; }
            }

            public List<short> lcauaspectos
            {
                get { return _lcauaspectos; }
                set { _lcauaspectos = value; }
            }
            public List<short> lcaut930_interesescar
            {
                get { return _lcaut930_interesescar; }
                set { _lcaut930_interesescar = value; }
            }

            public Nullable<short> caumejorar
            {
                get { return _caumejorar; }
                set { _caumejorar = value; }
            }

        }

        #endregion

        public class formulario
        {
           #region Private Variables
           private int _t930_idvaloracion;
           private string _estado;
           private short _t934_idmodeloformulario;
           private DateTime _t930_fechaapertura;
           private Nullable<DateTime> _t930_fechacierre;
           private int _t001_idficepi_evaluado;
           private string _evaluado;
           private string _firmaevaluado;
           private int _t001_idficepi_evaluador;
           private string _evaluador;
           private string _firmaevaluador;
           private Nullable<DateTime> _t930_fecfirmaevaluado;
           private Nullable<DateTime> _t930_fecfirmaevaluador;
          

          
           #endregion

           #region Public Properties
           public int t930_idvaloracion
           {
               get { return _t930_idvaloracion; }
               set { _t930_idvaloracion = value; }
           }

           public string estado
           {
               get { return _estado; }
               set { _estado = value; }
           }

           public short t934_idmodeloformulario
           {
               get { return _t934_idmodeloformulario; }
               set { _t934_idmodeloformulario = value; }
           }

           public DateTime t930_fechaapertura
           {
               get { return _t930_fechaapertura; }
               set { _t930_fechaapertura = value; }
           }

           public Nullable<DateTime> t930_fechacierre
           {
               get { return _t930_fechacierre; }
               set { _t930_fechacierre = value; }
           }

           public int t001_idficepi_evaluado
           {
               get { return _t001_idficepi_evaluado; }
               set { _t001_idficepi_evaluado = value; }
           }

           public string evaluado
           {
               get { return _evaluado; }
               set { _evaluado = value; }
           }

           public string firmaevaluado
           {
               get { return _firmaevaluado; }
               set { _firmaevaluado = value; }
           }

           public int t001_idficepi_evaluador
           {
               get { return _t001_idficepi_evaluador; }
               set { _t001_idficepi_evaluador = value; }
           }

           public string evaluador
           {
               get { return _evaluador; }
               set { _evaluador = value; }
           }

           public string firmaevaluador
           {
               get { return _firmaevaluador; }
               set { _firmaevaluador = value; }
           }

           public Nullable<DateTime> t930_fecfirmaevaluado
           {
               get { return _t930_fecfirmaevaluado; }
               set { _t930_fecfirmaevaluado = value; }
           }

           public Nullable<DateTime> t930_fecfirmaevaluador
           {
               get { return _t930_fecfirmaevaluador; }
               set { _t930_fecfirmaevaluador = value; }
           }



           #endregion
       }

       public class formulario_id1 : formulario
       {
           /// <summary>
           /// Summary description for VALORACIONESPROGRESS
           /// </summary>
           #region Private Variables
           private string _t930_denominacionROL;
           private string _t930_denominacionCR;
           private Nullable<byte> _t930_objetoevaluacion;
           private string _t930_denominacionPROYECTO;
           private Nullable<int> _t930_anomes_ini;
           private Nullable<int> _t930_anomes_fin;
           private Nullable<byte> _t930_actividad;
           private string _t930_areconocer;
           private string _t930_amejorar;
           private Nullable<byte> _t930_gescli;
           private Nullable<byte> _t930_liderazgo;
           private Nullable<byte> _t930_planorga;
           private Nullable<byte> _t930_exptecnico;
           private Nullable<byte> _t930_cooperacion;
           private Nullable<byte> _t930_iniciativa;
           private Nullable<byte> _t930_perseverancia;
           private Nullable<byte> _t930_interesescar;
           private string _t930_formacion;
           private string _t930_autoevaluacion;
           private Nullable<bool> _t930_puntuacion;
           private string _nombreevaluado;           
           private string _correoevaluado;
           private string _correoevaluador;
           private string _nombreevaluador;
           private char _sexoEvaluador;
           private char _sexoEvaluado;
           
           #endregion

           #region Public Properties
           public string t930_denominacionROL
           {
               get { return _t930_denominacionROL; }
               set { _t930_denominacionROL = value; }
           }

           public string t930_denominacionCR
           {
               get { return _t930_denominacionCR; }
               set { _t930_denominacionCR = value; }
           }

           public Nullable<byte> t930_objetoevaluacion
           {
               get { return _t930_objetoevaluacion; }
               set { _t930_objetoevaluacion = value; }
           }

           public string t930_denominacionPROYECTO
           {
               get { return _t930_denominacionPROYECTO; }
               set { _t930_denominacionPROYECTO = value; }
           }

           public Nullable<int> t930_anomes_ini
           {
               get { return _t930_anomes_ini; }
               set { _t930_anomes_ini = value; }
           }

           public Nullable<int> t930_anomes_fin
           {
               get { return _t930_anomes_fin; }
               set { _t930_anomes_fin = value; }
           }

           public Nullable<byte> t930_actividad
           {
               get { return _t930_actividad; }
               set { _t930_actividad = value; }
           }

           public string t930_areconocer
           {
               get { return _t930_areconocer; }
               set { _t930_areconocer = value; }
           }

           public string t930_amejorar
           {
               get { return _t930_amejorar; }
               set { _t930_amejorar = value; }
           }

           public Nullable<byte> t930_gescli
           {
               get { return _t930_gescli; }
               set { _t930_gescli = value; }
           }

           public Nullable<byte> t930_liderazgo
           {
               get { return _t930_liderazgo; }
               set { _t930_liderazgo = value; }
           }

           public Nullable<byte> t930_planorga
           {
               get { return _t930_planorga; }
               set { _t930_planorga = value; }
           }

           public Nullable<byte> t930_exptecnico
           {
               get { return _t930_exptecnico; }
               set { _t930_exptecnico = value; }
           }

           public Nullable<byte> t930_cooperacion
           {
               get { return _t930_cooperacion; }
               set { _t930_cooperacion = value; }
           }

           public Nullable<byte> t930_iniciativa
           {
               get { return _t930_iniciativa; }
               set { _t930_iniciativa = value; }
           }

           public Nullable<byte> t930_perseverancia
           {
               get { return _t930_perseverancia; }
               set { _t930_perseverancia = value; }
           }

           public Nullable<byte> t930_interesescar
           {
               get { return _t930_interesescar; }
               set { _t930_interesescar = value; }
           }

           public string t930_formacion
           {
               get { return _t930_formacion; }
               set { _t930_formacion = value; }
           }

           public string t930_autoevaluacion
           {
               get { return _t930_autoevaluacion; }
               set { _t930_autoevaluacion = value; }
           }

           public Nullable<bool> t930_puntuacion
           {
               get { return _t930_puntuacion; }
               set { _t930_puntuacion = value; }
           }

           public string Nombreevaluado
           {
               get { return _nombreevaluado; }
               set { _nombreevaluado = value; }
           }

           public string Correoevaluado
           {
               get { return _correoevaluado; }
               set { _correoevaluado = value; }
           }

           public string Correoevaluador
           {
               get { return _correoevaluador; }
               set { _correoevaluador = value; }
           }

           public string Nombreevaluador
           {
               get { return _nombreevaluador; }
               set { _nombreevaluador = value; }
           }

           public char SexoEvaluador
           {
               get { return _sexoEvaluador; }
               set { _sexoEvaluador = value; }
           }

           public char SexoEvaluado
           {
               get { return _sexoEvaluado; }
               set { _sexoEvaluado = value; }
           }
           #endregion
       }

       public class formulario_id2 : formulario
       {
           /// <summary>
           /// Summary description for VALORACIONESPROGRESS
           /// </summary>
           #region Private Variables
           private string _t930_denominacionROL;
           private string _t930_denominacionCR;
           private Nullable<byte> _t930_atclientes;
           private Nullable<byte> _t930_prespuesta;
           private Nullable<byte> _t930_crespuesta;
           private Nullable<byte> _t930_respdificil;
           private Nullable<byte> _t930_valactividad;
           private string _t930_amejorar;
           private Nullable<byte> _t930_gescli;
           private Nullable<byte> _t930_liderazgo;
           private Nullable<byte> _t930_planorga;
           private Nullable<byte> _t930_exptecnico;
           private Nullable<byte> _t930_cooperacion;
           private Nullable<byte> _t930_iniciativa;
           private Nullable<byte> _t930_perseverancia;
           private Nullable<byte> _t930_interesescar;
           private string _t930_especificar;
           private bool _t930_forofichk;
           private string _t930_forofitxt;
           private bool _t930_fortecchk;
           private string _t930_fortectxt;
           private bool _t930_foratcchk;
           private string _t930_foratctxt;
           private bool _t930_forcomchk;
           private string _t930_forcomtxt;
           private bool _t930_forvenchk;
           private string _t930_forventxt;
           private bool _t930_forespchk;
           private string _t930_foresptxt;
           private string _t930_autoevaluacion;
           private Nullable<bool> _t930_puntuacion;
           private string _nombreevaluado;
           private string _correoevaluado;
           private string _correoevaluador;
           private string _nombreevaluador;
           private char _sexoEvaluador;
           private char _sexoEvaluado;
           #endregion

           #region Public Properties
           public string t930_denominacionROL
           {
               get { return _t930_denominacionROL; }
               set { _t930_denominacionROL = value; }
           }

           public string t930_denominacionCR
           {
               get { return _t930_denominacionCR; }
               set { _t930_denominacionCR = value; }
           }

           public Nullable<byte> t930_atclientes
           {
               get { return _t930_atclientes; }
               set { _t930_atclientes = value; }
           }

           public Nullable<byte> t930_prespuesta
           {
               get { return _t930_prespuesta; }
               set { _t930_prespuesta = value; }
           }

           public Nullable<byte> t930_crespuesta
           {
               get { return _t930_crespuesta; }
               set { _t930_crespuesta = value; }
           }

           public Nullable<byte> t930_respdificil
           {
               get { return _t930_respdificil; }
               set { _t930_respdificil = value; }
           }

           public Nullable<byte> t930_valactividad
           {
               get { return _t930_valactividad; }
               set { _t930_valactividad = value; }
           }

           public string t930_amejorar
           {
               get { return _t930_amejorar; }
               set { _t930_amejorar = value; }
           }

           public Nullable<byte> t930_gescli
           {
               get { return _t930_gescli; }
               set { _t930_gescli = value; }
           }

           public Nullable<byte> t930_liderazgo
           {
               get { return _t930_liderazgo; }
               set { _t930_liderazgo = value; }
           }

           public Nullable<byte> t930_planorga
           {
               get { return _t930_planorga; }
               set { _t930_planorga = value; }
           }

           public Nullable<byte> t930_exptecnico
           {
               get { return _t930_exptecnico; }
               set { _t930_exptecnico = value; }
           }

           public Nullable<byte> t930_cooperacion
           {
               get { return _t930_cooperacion; }
               set { _t930_cooperacion = value; }
           }

           public Nullable<byte> t930_iniciativa
           {
               get { return _t930_iniciativa; }
               set { _t930_iniciativa = value; }
           }

           public Nullable<byte> t930_perseverancia
           {
               get { return _t930_perseverancia; }
               set { _t930_perseverancia = value; }
           }

           public Nullable<byte> t930_interesescar
           {
               get { return _t930_interesescar; }
               set { _t930_interesescar = value; }
           }

           public string t930_especificar
           {
               get { return _t930_especificar; }
               set { _t930_especificar = value; }
           }

           public bool t930_forofichk
           {
               get { return _t930_forofichk; }
               set { _t930_forofichk = value; }
           }

           public string t930_forofitxt
           {
               get { return _t930_forofitxt; }
               set { _t930_forofitxt = value; }
           }

           public bool t930_fortecchk
           {
               get { return _t930_fortecchk; }
               set { _t930_fortecchk = value; }
           }

           public string t930_fortectxt
           {
               get { return _t930_fortectxt; }
               set { _t930_fortectxt = value; }
           }

           public bool t930_forcomchk
           {
               get { return _t930_forcomchk; }
               set { _t930_forcomchk = value; }
           }

           public string t930_forcomtxt
           {
               get { return _t930_forcomtxt; }
               set { _t930_forcomtxt = value; }
           }

           public bool t930_foratcchk
           {
               get { return _t930_foratcchk; }
               set { _t930_foratcchk = value; }
           }

           public string t930_foratctxt
           {
               get { return _t930_foratctxt; }
               set { _t930_foratctxt = value; }
           }

           public bool t930_forvenchk
           {
               get { return _t930_forvenchk; }
               set { _t930_forvenchk = value; }
           }

           public string t930_forventxt
           {
               get { return _t930_forventxt; }
               set { _t930_forventxt = value; }
           }

           public bool t930_forespchk
           {
               get { return _t930_forespchk; }
               set { _t930_forespchk = value; }
           }

           public string t930_foresptxt
           {
               get { return _t930_foresptxt; }
               set { _t930_foresptxt = value; }
           }

           public string t930_autoevaluacion
           {
               get { return _t930_autoevaluacion; }
               set { _t930_autoevaluacion = value; }
           }

           public Nullable<bool> t930_puntuacion
           {
               get { return _t930_puntuacion; }
               set { _t930_puntuacion = value; }
           }

           public string Nombreevaluado
           {
               get { return _nombreevaluado; }
               set { _nombreevaluado = value; }
           }

           public string Correoevaluado
           {
               get { return _correoevaluado; }
               set { _correoevaluado = value; }
           }

           public string Correoevaluador
           {
               get { return _correoevaluador; }
               set { _correoevaluador = value; }
           }

           public string Nombreevaluador
           {
               get { return _nombreevaluador; }
               set { _nombreevaluador = value; }
           }

           public char SexoEvaluador
           {
               get { return _sexoEvaluador; }
               set { _sexoEvaluador = value; }
           }

           public char SexoEvaluado
           {
               get { return _sexoEvaluado; }
               set { _sexoEvaluado = value; }
           }
           #endregion
       }

       public class ImprimirInsertados
       {
           private int _t001_idficepi;

           public int t001_idficepi
           {
               get { return _t001_idficepi; }
               set { _t001_idficepi = value; }
           }
           private int _t930_idvaloracion;

           public int t930_idvaloracion
           {
               get { return _t930_idvaloracion; }
               set { _t930_idvaloracion = value; }
           }

           private int _t934_idmodeloformulario;
           
           public int t934_idmodeloformulario
           {
               get { return _t934_idmodeloformulario; }
               set { _t934_idmodeloformulario = value; }
           }
           
       }

       
	}
}
