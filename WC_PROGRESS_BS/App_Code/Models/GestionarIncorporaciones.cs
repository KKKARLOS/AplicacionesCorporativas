using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.Progress.Models
{
    /// <summary>
    /// Descripción breve de TramitarSalidas
    /// </summary>
    public class GestionarIncorporaciones
    {
        public GestionarIncorporaciones()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        #region Private Variables
       
        private string _t937_comentario_respdestino;
        private byte _t937_estadopeticion;


        #endregion

        #region Public Properties
       

        public string t937_comentario_resporigen
        {
            get { return _t937_comentario_respdestino; }
            set { _t937_comentario_respdestino = value; }
        }

      

        public byte t937_estadopeticion
        {
            get { return _t937_estadopeticion; }
            set { _t937_estadopeticion = value; }
        }


        #endregion
    }
}