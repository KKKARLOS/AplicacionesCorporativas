using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
    public class OFICINAGV
    {
        #region Propiedades

        private int _t010_idoficina;
        public int t010_idoficina
        {
            get { return _t010_idoficina; }
            set { _t010_idoficina = value; }
        }

        private string _t010_desoficina;
        public string t010_desoficina
        {
            get { return _t010_desoficina; }
            set { _t010_desoficina = value; }
        }

        #endregion

        public OFICINAGV()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public OFICINAGV(int nOficina, string sDenominacion)
        {
            this.t010_idoficina = nOficina;
            this.t010_desoficina = sDenominacion;
        }
    }

}