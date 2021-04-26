using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{

    public class MIEQUIPO
    {
        #region profesional
        public class profesional
        {
            private int _idficepi;
            private string _nombre;
            private string _nombreapellidosprofesional;
            private string _nombreevaluadordestino;
            private string _correoevaluadordestino;    
            
            private string _correo;
            private string _prof;
            private string _nombreevaluado;
            private int _idficepievaluadordestino;
            private string _motivo;

            public string Motivo
            {
                get { return _motivo; }
                set { _motivo = value; }
            }

            public string nombre
            {
                get { return _nombre; }
                set { _nombre = value; }
            }

            public string nombreapellidosprofesional
            {
                get { return _nombreapellidosprofesional; }
                set { _nombreapellidosprofesional = value; }
            }

            public string nombreevaluadordestino
            {
                get { return _nombreevaluadordestino; }
                set { _nombreevaluadordestino = value; }
            }

            public string correoevaluadordestino
            {
                get { return _correoevaluadordestino; }
                set { _correoevaluadordestino = value; }
            }

            public string Nombreevaluado
            {
                get { return _nombreevaluado; }
                set { _nombreevaluado = value; }
            }
            private string _nombreevaluador;

            public string Nombreevaluador
            {
                get { return _nombreevaluador; }
                set { _nombreevaluador = value; }
            }
            public int idficepievaluadordestino
            {
                get { return _idficepievaluadordestino; }
                set { _idficepievaluadordestino = value;  }
            }

            private string _rol;
            private Nullable<byte> _estado;
            private bool _evaluacionAbierta;
            private bool _evaluacionEnCurso;
            private bool _enviarcorreo;
            private string _textocorreo;
            private string _resporigen;
            private string _respdestino;
            private string _t937_fechainipeticion;
            private string _t937_comentario_resporigen;
            private string _t937_comentario_respdestino;
            private Nullable <int> _t937_estadopeticion;
            private Nullable<int> _t937_idpetcambioresp;
            private int _incorporacionentramite;
            private int _idRespOrigen;
            private string _sexo;

            public string Sexo
            {
                get { return _sexo; }
                set { _sexo = value; }
            }

            public int idficepi
            {
                get { return _idficepi; }
                set { _idficepi = value; }
            }

            public string Correo
            {
                get { return _correo; }
                set { _correo = value; }
            }

            public Nullable<int> t937_idpetcambioresp
            {
                get { return _t937_idpetcambioresp; }
                set { _t937_idpetcambioresp = value; }
            }

            public string prof
            {
                get { return _prof; }
                set { _prof = value; }
            }

            public string rol
            {
                get { return _rol; }
                set { _rol = value; }
            }

            public Nullable<byte> estado
            {
                get { return _estado; }
                set { _estado = value; }
            }

            public bool evaluacionAbierta
            {
                get { return _evaluacionAbierta; }
                set { _evaluacionAbierta = value; }
            }

            public bool evaluacionEnCurso
            {
                get { return _evaluacionEnCurso; }
                set { _evaluacionEnCurso = value; }
            }

            public bool enviarcorreo
            {
                get { return _enviarcorreo; }
                set { _enviarcorreo = value; }
            }

            public string textocorreo {
                get { return _textocorreo; }
                set { _textocorreo = value; }
            }

            public string resporigen
            {
                get { return _resporigen; }
                set { _resporigen = value; }
            }

            public string respdestino
            {
                get { return _respdestino; }
                set { _respdestino = value; }
            }

            public string t937_fechainipeticion
            {
                get { return _t937_fechainipeticion; }
                set { _t937_fechainipeticion = value; }
            }


            public string t937_comentario_resporigen
            {
                get { return _t937_comentario_resporigen; }
                set { _t937_comentario_resporigen = value; }
            }

            public string t937_comentario_respdestino
            {
                get { return _t937_comentario_respdestino; }
                set { _t937_comentario_respdestino = value; }
            }

            public Nullable <int> t937_estadopeticion
            {
                get { return _t937_estadopeticion; }
                set { _t937_estadopeticion = value; }
            }

            public int incorporacionentramite
            {
                get { return _incorporacionentramite; }
                set { _incorporacionentramite = value; }
            }

            public int idRespOrigen
            {
                get { return _idRespOrigen; }
                set { _idRespOrigen = value; }
            }

            //Tiene que existir un constructor vacío para que funcione el mapeo Javascript/.NET de clases heredadas.
            public profesional() { }

            public profesional(int idfic, string nombre)
            {
                _idficepi = idfic;
                _prof = nombre;
                
            }

            //Constructor Confirmar mi equipo
            public profesional(int idfic, string nombre, string rol, Nullable<byte> estadopeticion)
            {
                _idficepi = idfic;
                _prof = nombre;
                _rol = rol;
                _estado = estadopeticion;

            }


            public profesional(int idfic, string nombre, string rol, Nullable<byte> est, bool estEva)
            {
                _idficepi = idfic;
                _prof = nombre;
                _rol = rol;
                _estado = est;
                _evaluacionEnCurso = estEva;
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto();               
            }




          //DAVID
            public profesional(int idfic, string sexo, string correo, string nombreevaluado, string nombreevaluador,  bool estEva,  Nullable<byte> t937_estadopeticion, string evaluado)
            {
                _idficepi = idfic;                
                _sexo = sexo;
                _correo = correo;
                _nombreevaluado = nombreevaluado;
                _nombreevaluador = nombreevaluador;               
                _evaluacionEnCurso = estEva;
                _estado = t937_estadopeticion;
                _prof = evaluado;                                          
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto(nombreevaluado);               
            }

            public profesional(int idfic, string sexo, string correo, string nombreevaluado, string nombreevaluador, bool estEvaAbierta, bool estEvaEnCurso, Nullable<byte> t937_estadopeticion, string evaluado)
            {
                _idficepi = idfic;                
                _sexo = sexo;
                _correo = correo;
                _nombreevaluado = nombreevaluado;
                _nombreevaluador = nombreevaluador;
                _evaluacionAbierta = estEvaAbierta;
                _evaluacionEnCurso = estEvaEnCurso;
                _estado = t937_estadopeticion;
                _prof = evaluado;                                          
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto(nombreevaluado);               
            }


          
            //public profesional(int idfic, string sexo, string correo, string nombreevaluado, string nombreevaluador, bool estEvaAbierta, bool estEvaEnCurso, Nullable<byte> t937_estadopeticion, string evaluado)
            //{
            //    _idficepi = idfic;
            //    _sexo = sexo;
            //    _correo = correo;
            //    _nombreevaluado = nombreevaluado;
            //    _nombreevaluador = nombreevaluador;
            //    _evaluacionAbierta = estEvaAbierta;
            //    _evaluacionEnCurso = estEvaEnCurso;
            //    _estado = t937_estadopeticion;
            //    _prof = evaluado;
            //    _enviarcorreo = true;
            //    _textocorreo = textoPorDefecto(nombreevaluado);
            //}



            public profesional(int idfic,  Nullable<int> t937_idpetcambioresp, string nombre, string rol, Nullable<byte> est, bool estEva, string respdestino, string t937_fechainipeticion, string t937_comentario_resporigen, string t937_comentario_respdestino, Nullable<int> t937_estadopeticion)
            {
                _idficepi = idfic;                
                _t937_idpetcambioresp = t937_idpetcambioresp;
                _prof = nombre;
                _rol = rol;
                _estado = est;
                _evaluacionEnCurso = estEva;
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto();
                _respdestino = respdestino;
                _t937_fechainipeticion = t937_fechainipeticion;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _t937_comentario_respdestino = t937_comentario_respdestino;
                _t937_estadopeticion = t937_estadopeticion;
            }

            //CONSTRUCTOR DAVID
            public profesional(int idfic, string sexo,  string correo, Nullable<int> t937_idpetcambioresp, string nombre, string rol, Nullable<byte> est, bool estEva, string respdestino, string t937_fechainipeticion, string t937_comentario_resporigen, string t937_comentario_respdestino, Nullable<int> t937_estadopeticion)
            {
                _idficepi = idfic;                
                _sexo = sexo;
                _correo = correo;
                _t937_idpetcambioresp = t937_idpetcambioresp;
                _prof = nombre;
                _rol = rol;
                _estado = est;
                _evaluacionEnCurso = estEva;
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto();
                _respdestino = respdestino;
                _t937_fechainipeticion = t937_fechainipeticion;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _t937_comentario_respdestino = t937_comentario_respdestino;
                _t937_estadopeticion = t937_estadopeticion;
            }


            public profesional(int idfic, string nombreapellidosprofesional, string nombreevaluadordestino, string correoevaluadordestino,  string sexo, string correo, Nullable<int> t937_idpetcambioresp, string nombre,  Nullable<byte> est, bool estEva, string respdestino, string t937_fechainipeticion, string t937_comentario_resporigen, string t937_comentario_respdestino, Nullable<int> t937_estadopeticion)
            {
                _idficepi = idfic;
                _nombreapellidosprofesional = nombreapellidosprofesional;
                _nombreevaluadordestino = nombreevaluadordestino;
                 _correoevaluadordestino = correoevaluadordestino;
                _sexo = sexo;
                _correo = correo;
                _t937_idpetcambioresp = t937_idpetcambioresp;
                _prof = nombre;                
                _estado = est;
                _evaluacionEnCurso = estEva;
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto();
                _respdestino = respdestino;
                _t937_fechainipeticion = t937_fechainipeticion;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _t937_comentario_respdestino = t937_comentario_respdestino;
                _t937_estadopeticion = t937_estadopeticion;
            }


            //11 FEBRERO 2016
            public profesional(int idfic, string nombreapellidosprofesional, string nombreevaluadordestino, string correoevaluadordestino, string sexo, string correo, Nullable<int> t937_idpetcambioresp, string nombre, Nullable<byte> est, bool estEvaAbierta, bool estEvaEnCurso, string respdestino, string t937_fechainipeticion, string t937_comentario_resporigen, string t937_comentario_respdestino, Nullable<int> t937_estadopeticion)
            {
                _idficepi = idfic;
                _nombreapellidosprofesional = nombreapellidosprofesional;
                _nombreevaluadordestino = nombreevaluadordestino;
                _correoevaluadordestino = correoevaluadordestino;
                _sexo = sexo;
                _correo = correo;
                _t937_idpetcambioresp = t937_idpetcambioresp;
                _prof = nombre;
                _estado = est;
                _evaluacionAbierta = estEvaAbierta;
                _evaluacionEnCurso = estEvaEnCurso;
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto();
                _respdestino = respdestino;
                _t937_fechainipeticion = t937_fechainipeticion;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _t937_comentario_respdestino = t937_comentario_respdestino;
                _t937_estadopeticion = t937_estadopeticion;
            }

            //15 febrero 2016
            public profesional(int idfic, int idficepievaluadordestino, string Solonombre, string nombreapellidosprofesional, string nombreevaluadordestino, string correoevaluadordestino, string sexo, string correo, Nullable<int> t937_idpetcambioresp, string nombre, Nullable<byte> est, bool estEvaAbierta, bool estEvaEnCurso, string respdestino, string t937_fechainipeticion, string t937_comentario_resporigen, string t937_comentario_respdestino, Nullable<int> t937_estadopeticion)
            {
                _idficepi = idfic;
                _idficepievaluadordestino = idficepievaluadordestino;
                _nombre = Solonombre;
                _nombreapellidosprofesional = nombreapellidosprofesional;
                _nombreevaluadordestino = nombreevaluadordestino;
                _correoevaluadordestino = correoevaluadordestino;
                _sexo = sexo;
                _correo = correo;
                _t937_idpetcambioresp = t937_idpetcambioresp;
                _prof = nombre;
                _estado = est;
                _evaluacionAbierta = estEvaAbierta;
                _evaluacionEnCurso = estEvaEnCurso;
                _enviarcorreo = true;
                _textocorreo = textoPorDefecto();
                _respdestino = respdestino;
                _t937_fechainipeticion = t937_fechainipeticion;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _t937_comentario_respdestino = t937_comentario_respdestino;
                _t937_estadopeticion = t937_estadopeticion;
            }



            public profesional(int idfic, Nullable<int> t937_idpetcambioresp, string nombre, int incorporacionentramite, string t937_fechainipeticion, string resporigen, string t937_comentario_resporigen, int idRespOrigen)
            {
                _idficepi = idfic;
                _t937_idpetcambioresp = t937_idpetcambioresp;
                _prof = nombre;
                _incorporacionentramite = incorporacionentramite;                                                
                _t937_fechainipeticion = t937_fechainipeticion;
                _resporigen = resporigen;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _idRespOrigen = idRespOrigen;
                
            }


           

            //Plantilla por defecto para avisar a un profesional de la evaluación progress            
            private string textoPorDefecto()
            {
                string textoSexo = String.Empty;
                    if (((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Sexo.ToString()  == "V")textoSexo = "evaluador";
                    else textoSexo = "evaluadora";
                
                return "Tu " + textoSexo + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrecorto + ", te ha abierto una evaluación. En los próximos días se pondrá en contacto contigo para fijar la fecha de la reunión.";

            }


             private string textoPorDefecto(string nombreevaluado)
            {
                string textoSexo = String.Empty;
                    if (((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).Sexo.ToString()  == "V")textoSexo = "evaluador";
                    else textoSexo = "evaluadora";

                    return nombreevaluado + ", tu " + textoSexo + " " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrecorto + " te ha abierto una evaluación. En los próximos días se pondrá en contacto contigo para fijar la fecha de la reunión.";

            }


        }
        #endregion


        #region Profesional con entradas en trámite
        public class profEntradasTramite
        {

            
            private int _idficepi;
            private int _idficepievaluadordestino;            
            private string _prof;

            private string _resporigen;
            private string _respdestino;
            private string _t937_fechainipeticion;
            private string _t937_comentario_resporigen;
            private string _t937_comentario_respdestino;
            private Nullable <int> _t937_estadopeticion;
            private Nullable<int> _t937_idpetcambioresp;
            private string _nombreresporigen;
            private string _nombreinteresado;
            private string _nombreapellidosinteresado;
            private string _correointeresado;
            private string _correoresporigen;
            private int _idRespOrigen;
            
             //Tiene que existir un constructor vacío para que funcione el mapeo Javascript/.NET de clases heredadas.
            public profEntradasTramite() { }
            
        #endregion

            #region Public Properties
           
            public int idficepi
            {
                get { return _idficepi; }
                set { _idficepi = value; }
            }

            public int Idficepievaluadordestino
            {
                get { return _idficepievaluadordestino; }
                set { _idficepievaluadordestino = value; }
            }

            public Nullable<int> t937_idpetcambioresp
            {
                get { return _t937_idpetcambioresp; }
                set { _t937_idpetcambioresp = value; }
            }

            public string nombreresporigen
            {
                get { return _nombreresporigen; }
                set { _nombreresporigen = value; }
            }
            public string nombreinteresado
            {
                get { return _nombreinteresado; }
                set { _nombreinteresado = value; }
            }

            public string nombreapellidosinteresado
            {
                get { return _nombreapellidosinteresado; }
                set { _nombreapellidosinteresado = value; }
            }

            public string correointeresado
            {
                get { return _correointeresado; }
                set { _correointeresado = value; }
            }

            public string correoresporigen
            {
                get { return _correoresporigen; }
                set { _correoresporigen = value; }
            }

            public string prof
            {
                get { return _prof; }
                set { _prof = value; }
            }

          
            public string resporigen
            {
                get { return _resporigen; }
                set { _resporigen = value; }
            }

            public string respdestino
            {
                get { return _respdestino; }
                set { _respdestino = value; }
            }

            public string t937_fechainipeticion
            {
                get { return _t937_fechainipeticion; }
                set { _t937_fechainipeticion = value; }
            }


            public string t937_comentario_resporigen
            {
                get { return _t937_comentario_resporigen; }
                set { _t937_comentario_resporigen = value; }
            }

            public string t937_comentario_respdestino
            {
                get { return _t937_comentario_respdestino; }
                set { _t937_comentario_respdestino = value; }
            }

            public Nullable <int> t937_estadopeticion
            {
                get { return _t937_estadopeticion; }
                set { _t937_estadopeticion = value; }
            }

           

            public int idRespOrigen
            {
                get { return _idRespOrigen; }
                set { _idRespOrigen = value; }
            }

            public profEntradasTramite(int idfic, int idpeticion, string nombre, string t937_fechainipeticion, string resporigen, string t937_comentario_resporigen, int idRespOrigen)
            {
                _idficepi = idfic;
                _t937_idpetcambioresp = idpeticion;
                _prof = nombre;                
                _t937_fechainipeticion = t937_fechainipeticion;
                _resporigen = resporigen;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _idRespOrigen = idRespOrigen;

            }

            //CONSTRUCTOR PARA CORREOS
            public profEntradasTramite(int idfic, int idficepievaluadordestino, int idpeticion, string nombreresporigen, string nombreinteresado, string nombreapellidosinteresado, string correointeresado, string correoresporigen, string nombre, string t937_fechainipeticion, string resporigen, string t937_comentario_resporigen, int idRespOrigen)
            {
                _idficepi = idfic;
                _idficepievaluadordestino = idficepievaluadordestino;
                _t937_idpetcambioresp = idpeticion;
                _nombreresporigen = nombreresporigen;
                _nombreinteresado = nombreinteresado;
                _nombreapellidosinteresado = nombreapellidosinteresado;
                _correointeresado = correointeresado;
                _correoresporigen = correoresporigen;
                _prof = nombre;
                _t937_fechainipeticion = t937_fechainipeticion;
                _resporigen = resporigen;
                _t937_comentario_resporigen = t937_comentario_resporigen;
                _idRespOrigen = idRespOrigen;

            }

        }
        #endregion


        #region Profesional con evaluacion en curso
        public class profPendEval {
            private int _idvaloracion;
            private string _prof;
            private DateTime _fecapertura;
            private int _idformulario;
            private string _correoprofesional;
            private string _nombreprofesional;

            public int idvaloracion
            {
                get { return _idvaloracion; }
                set { _idvaloracion = value; }
            }

            public string prof
            {
                get { return _prof; }
                set { _prof = value; }
            }

            public DateTime fecapertura {
                get { return _fecapertura; }
                set { _fecapertura = value; }
            }

            public int idformulario
            {
                get { return _idformulario; }
                set { _idformulario = value; }
            }

            public string correoprofesional
            {
                get { return _correoprofesional; }
                set { _correoprofesional = value; }
            }

            public string nombreprofesional
            {
                get { return _nombreprofesional; }
                set { _nombreprofesional = value; }
            }

            public profPendEval(int idval, string nombre, DateTime fecap, int idfor)
            {
                _idvaloracion = idval;
                _prof = nombre;
                _fecapertura = fecap;
                _idformulario = idfor;
            }

            public profPendEval(int idval, string nombre, DateTime fecap, int idfor, string correoprofesional, string nombreprofesional)
            {
                _idvaloracion = idval;
                _prof = nombre;
                _fecapertura = fecap;
                _idformulario = idfor;
                _correoprofesional = correoprofesional;
                _nombreprofesional = nombreprofesional;
            }



            
        }
        #endregion

        #region profesional cambio rol
        public class profesional_CRol
        {
            private int _t940_idtramitacambiorol;
            private int _idficepi;
            private string _nombreapellidosprofesional;
            private string _prof;
            private string _nombreprofesional;
            private string _sexo;
            private string _correo;
            private string _promotor;
            private string _rol;
            private string _rol_prop;
            private Nullable<byte> _estado;
            private string _t940_motivopropuesto;
            private string _nombreaprobador;
            private string _correoaprobador;
            private string _t940_resolucion;
            
            private string _t940_fechaproposicion;
            private string _nombreapellidosaprobador;

            public int t940_idtramitacambiorol
            {
                get { return _t940_idtramitacambiorol; }
                set { _t940_idtramitacambiorol = value; }
            }
            
            public int idficepi
            {
                get { return _idficepi; }
                set { _idficepi = value; }
            }

            public string nombreapellidosprofesional
            {
                get { return _nombreapellidosprofesional; }
                set { _nombreapellidosprofesional = value; }
            }

            public string prof
            {
                get { return _prof; }
                set { _prof = value; }
            }

            public string nombreprofesional
            {
                get { return _nombreprofesional; }
                set { _nombreprofesional = value; }
            }

            public string sexo
            {
                get { return _sexo; }
                set { _sexo = value; }
            }

            public string correo
            {
                get { return _correo; }
                set { _correo = value; }
            }

            public string promotor
            {
                get { return _promotor; }
                set { _promotor = value; }
            }

            public string rol
            {
                get { return _rol; }
                set { _rol = value; }
            }

            public string rol_prop
            {
                get { return _rol_prop; }
                set { _rol_prop = value; }
            }

            public Nullable<byte> estado
            {
                get { return _estado; }
                set { _estado = value; }
            }

            public string t940_motivopropuesto
            {
                get { return _t940_motivopropuesto; }
                set { _t940_motivopropuesto = value; }
            }


            public string t940_fechaproposicion
            {
                get { return _t940_fechaproposicion; }
                set { _t940_fechaproposicion = value; }
            }

            public string nombreaprobador
            {
                get { return _nombreaprobador; }
                set { _nombreaprobador = value; }
            }

            public string correoaprobador
            {
                get { return _correoaprobador; }
                set { _correoaprobador = value; }
            }

            public string t940_resolucion
            {
                get { return _t940_resolucion; }
                set { _t940_resolucion = value; }
            }

            public string nombreapellidosaprobador
            {
                get { return _nombreapellidosaprobador; }
                set { _nombreapellidosaprobador = value; }
            }


            public profesional_CRol(int t940_idtramitacambiorol, int idfic, string nombre, string rol, string rol_p, Nullable<byte> est, string t940_motivopropuesto, string nombreapellidosaprobador)
            {
                _t940_idtramitacambiorol = t940_idtramitacambiorol;
                _idficepi = idfic;
                _prof = nombre;
                _rol = rol;
                _rol_prop = rol_p;
                _estado = est;
                _t940_motivopropuesto = t940_motivopropuesto;
                _nombreapellidosaprobador = nombreapellidosaprobador;
            }

            ///Solicitud cambio de Rol
            public profesional_CRol(int t940_idtramitacambiorol, int idfic, string nombreapellidosprofesional, string sexo, string correo, string nombre, string nombreprofesional, string rol, string rol_p, string promotor, string t940_fechaproposicion, Nullable<byte> estadopeticion, string t940_motivopropuesto)
            {
                _t940_idtramitacambiorol = t940_idtramitacambiorol;
                _idficepi = idfic;
                _nombreapellidosprofesional = nombreapellidosprofesional;
                _sexo = sexo;
                _correo = correo;
                _prof = nombre;
                _nombreprofesional = nombreprofesional;
                _rol = rol;
                _rol_prop = rol_p;
                _promotor = promotor;
                _t940_fechaproposicion = t940_fechaproposicion;
                _estado = estadopeticion;
                _t940_motivopropuesto = t940_motivopropuesto;
            }

            ///Solicitud cambio de Rol
            public profesional_CRol(int t940_idtramitacambiorol, int idfic, string nombreapellidosprofesional, string sexo, string correo, string nombre, string nombreprofesional, string rol, string rol_p, string promotor, string t940_fechaproposicion, Nullable<byte> estadopeticion, string t940_motivopropuesto, string t940_resolucion)
            {
                _t940_idtramitacambiorol = t940_idtramitacambiorol;
                _idficepi = idfic;
                _nombreapellidosprofesional = nombreapellidosprofesional;
                _sexo = sexo;
                _correo = correo;
                _prof = nombre;
                _nombreprofesional = nombreprofesional;
                _rol = rol;
                _rol_prop = rol_p;
                _promotor = promotor;
                _t940_fechaproposicion = t940_fechaproposicion;
                _estado = estadopeticion;
                _t940_motivopropuesto = t940_motivopropuesto;
                _t940_resolucion = t940_resolucion;
            }

            public profesional_CRol(string nombreaprobador, string correoaprobador, string nombreapellidosaprobador)
            {
                _nombreaprobador = nombreaprobador;
                _correoaprobador = correoaprobador;
                _nombreapellidosaprobador = nombreapellidosaprobador;
            }


            
        }
        #endregion

        /// <summary>
        /// Summary description for MIEQUIPO
        /// </summary>
        #region Private Variables
        private int _idficepi;
        private List<profesional> _profesionales;
        private List<profEntradasTramite> _profesionalesEnTramite;
        private bool _entradasentramite;
        private Nullable<DateTime> _confirmequipo;
        #endregion

        #region Public Properties
        public int idficepi
        {
            get { return _idficepi; }
            set { _idficepi = value; }
        }

        public List<profesional> profesionales
        {
            get { return _profesionales; }
            set { _profesionales = value; }
        }

        public List<profEntradasTramite> profesionalesEnTramite
        {
            get { return _profesionalesEnTramite; }
            set { _profesionalesEnTramite = value; }
        }



        public bool entradasentramite
        {
            get { return _entradasentramite; }
            set { _entradasentramite = value; }
        }

        public Nullable<DateTime> confirmequipo
        {
            get { return _confirmequipo; }
            set { _confirmequipo = value; }
        }

        public profesional findById(int idficepi) {
            foreach (profesional prof in this.profesionales) {
                if (idficepi == prof.idficepi)
                    return prof;
            }
            return null;
        }

        public static profesional findById(List<profesional> listProf, int idficepi)
        {
            foreach (profesional prof in listProf)
            {
                if (idficepi == prof.idficepi)
                    return prof;
            }
            return null;
        }

        #endregion

    }


}
