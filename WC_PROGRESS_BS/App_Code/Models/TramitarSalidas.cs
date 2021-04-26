using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{
    /// <summary>
    /// Descripción breve de TramitarSalidas
    /// </summary>
    public class TramitarSalidas
    {
        public TramitarSalidas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        #region Private Variables
        private int _t001_idficepi_interesado;
        private int _t001_idficepi_resporigen;
        private int _t001_idficepi_respdestino;
        private string _t937_comentario_resporigen;
        private byte _t937_estadopeticion;
        

        #endregion

        #region Public Properties
        public int t001_idficepi_respdestino
        {
            get { return _t001_idficepi_respdestino; }
            set { _t001_idficepi_respdestino = value; }
        }

        public int t001_idficepi_resporigen
        {
            get { return _t001_idficepi_resporigen; }
            set { _t001_idficepi_resporigen = value; }
        }

        public string t937_comentario_resporigen
        {
            get { return _t937_comentario_resporigen; }
            set { _t937_comentario_resporigen = value; }
        }

        public int T001_idficepi_interesado
        {
            get { return _t001_idficepi_interesado; }
            set { _t001_idficepi_interesado = value; }
        }

        public byte t937_estadopeticion
        {
            get { return _t937_estadopeticion; }
            set { _t937_estadopeticion = value; }
        }

       
        #endregion
    }
}