using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{

    public class GestionCambioResponsable
    {

        /// <summary>
        /// Summary description for GestionCambioResponsable
        /// </summary>
        #region Private Variables
        private Int32 _t937_idpetcambioresp;       
        private string _interesado;                
        private Int32 _t001_idficepi_interesado;
        private Int32 _t001_idficepi_resporigen;
        private string _resporigen;   
        private Int32 _t001_idficepi_respdestino;
        private string _respdestino;   
        private string _t937_comentario_resporigen;
        private string _t937_comentario_respdestino;
        private DateTime _t937_fechainipeticion;
        private Nullable<int> _t937_estadopeticion;
        private DateTime _t937_fechacambioestado;
        private DateTime _t937_fecharechazo;
        private string _nombreprofesional;
        private string _correo_interesado;
        private string _nombre_resporigen;
        private string _correo_resporigen;
        private string _nombreapellidos_interesado;
        private string _nombre_respdestino;
        private string _correo_respdestino;
        private string _sexo_respdestino;
        private string _sexo_interesado;
        private string _sexo_resporigen;
        private string _nombreapellidos_respdestino;
        private short _estadebaja_respdestino;
        
       
        #endregion

        #region Public Properties
        public Int32 T937_idpetcambioresp
        {
            get { return _t937_idpetcambioresp; }
            set { _t937_idpetcambioresp = value; }
        }

        public string Interesado
        {
            get { return _interesado; }
            set { _interesado = value; }
        }

        public Int32 T001_idficepi_interesado
        {
            get { return _t001_idficepi_interesado; }
            set { _t001_idficepi_interesado = value; }
        }

        public Int32 T001_idficepi_resporigen
        {
            get { return _t001_idficepi_resporigen; }
            set { _t001_idficepi_resporigen = value; }
        }

        public string Resporigen
        {
            get { return _resporigen; }
            set { _resporigen = value; }
        }

        public Int32 T001_idficepi_respdestino
        {
            get { return _t001_idficepi_respdestino; }
            set { _t001_idficepi_respdestino = value; }
        }

        public string Respdestino
        {
            get { return _respdestino; }
            set { _respdestino = value; }
        }

        public string T937_comentario_resporigen
        {
            get { return _t937_comentario_resporigen; }
            set { _t937_comentario_resporigen = value; }
        }

        public string T937_comentario_respdestino
        {
            get { return _t937_comentario_respdestino; }
            set { _t937_comentario_respdestino = value; }
        }


        public DateTime T937_fechainipeticion
        {
            get { return _t937_fechainipeticion; }
            set { _t937_fechainipeticion = value; }
        }

        public Nullable<int> T937_estadopeticion
        {
            get { return _t937_estadopeticion; }
            set { _t937_estadopeticion = value; }
        }

        public DateTime T937_fechacambioestado
        {
            get { return _t937_fechacambioestado; }
            set { _t937_fechacambioestado = value; }
        }

        public DateTime T937_fecharechazo
        {
            get { return _t937_fecharechazo; }
            set { _t937_fecharechazo = value; }
        }

        public string nombreprofesional
        {
            get { return _nombreprofesional; }
            set { _nombreprofesional = value; }
        }

        public string correo_interesado
        {
            get { return _correo_interesado; }
            set { _correo_interesado = value; }
        }
        
        public string nombre_resporigen
        {
            get { return _nombre_resporigen; }
            set { _nombre_resporigen = value; }
        }

        public string correo_resporigen
        {
            get { return _correo_resporigen; }
            set { _correo_resporigen = value; }
        }

        public string nombreapellidos_interesado
        {
            get { return _nombreapellidos_interesado; }
            set { _nombreapellidos_interesado = value; }
        }

        public string nombre_respdestino
        {
            get { return _nombre_respdestino; }
            set { _nombre_respdestino = value; }
        }

        public string correo_respdestino
        {
            get { return _correo_respdestino; }
            set { _correo_respdestino = value; }
        }

        public string sexo_respdestino
        {
            get { return _sexo_respdestino; }
            set { _sexo_respdestino = value; }
        }

        public string sexo_interesado
        {
            get { return _sexo_interesado; }
            set { _sexo_interesado = value; }
        }

        public string sexo_resporigen
        {
            get { return _sexo_resporigen; }
            set { _sexo_resporigen = value; }
        }

        public string nombreapellidos_respdestino
        {
            get { return _nombreapellidos_respdestino; }
            set { _nombreapellidos_respdestino = value; }
        }
        
        public short estadebaja_respdestino
        {
            get { return _estadebaja_respdestino; }
            set { _estadebaja_respdestino = value; }
        }

        

       
        #endregion

    }

    
}
