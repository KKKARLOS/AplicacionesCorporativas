using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.SIC.Models
{

    public class ProfesionalSimple
    {
        public int t001_idficepi { get; set; }
        public int t314_idusuario { get; set; }
        public string profesional { get; set; }
        public string t001_sexo { get; set; }
        public string nombreprofesional { get; set; }
        public string nombreapellidosprofesional { get; set; }
        public string correo_profesional { get; set; }
        public string empresa { get; set; }
        public string t303_denominacion { get; set; }


        public ProfesionalSimple()
        {
        }

    }
}