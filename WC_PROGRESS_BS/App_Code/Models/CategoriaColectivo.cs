using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CategoriaColectivo
/// </summary>
/// 
namespace IB.Progress.Models
{
    public class CategoriaColectivo
    {
        public CategoriaColectivo()
        {
            _select1 = new List<Colectivo>();
            _select2 = new List<Categoria>();
        }

        private List<Colectivo> _select1;
        private List<Categoria> _select2;

        public List<Colectivo> Select1
        {
            get { return _select1; }
            set { _select1 = value; }
        }

        public List<Categoria> Select2
        {
            get { return _select2; }
            set { _select2 = value; }
        }
      
    }

   

    public class Categoria
    {
        private int _t935_idcategoriaprofesional;

        public int T935_idcategoriaprofesional
        {
            get { return _t935_idcategoriaprofesional; }
            set { _t935_idcategoriaprofesional = value; }
        }
        private string _t935_denominacion;

        public string T935_denominacion
        {
            get { return _t935_denominacion; }
            set { _t935_denominacion = value; }
        }

        private string _t941_idcolectivoColectivo;

        public string T941_idcolectivoColectivo
        {
            get { return _t941_idcolectivoColectivo; }
            set { _t941_idcolectivoColectivo = value; }
        }

    }


  
}