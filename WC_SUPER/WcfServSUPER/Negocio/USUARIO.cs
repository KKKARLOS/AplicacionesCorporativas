using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace IB.Services.Super.Negocio
{
    public class USUARIO
    {
        #region Propiedades y Atributos 

        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private DateTime _t001_fecalta;
        public DateTime t001_fecalta
        {
            get { return _t001_fecalta; }
            set { _t001_fecalta = value; }
        }

        private DateTime? _t001_fecbaja;
        public DateTime? t001_fecbaja
        {
            get { return _t001_fecbaja; }
            set { _t001_fecbaja = value; }
        }

        private DateTime _t314_falta;
        public DateTime t314_falta
        {
            get { return _t314_falta; }
            set { _t314_falta = value; }
        }

        private DateTime? _t314_fbaja;
        public DateTime? t314_fbaja
        {
            get { return _t314_fbaja; }
            set { _t314_fbaja = value; }
        }

        private string _t314_password;
        public string t314_password
        {
            get { return _t314_password; }
            set { _t314_password = value; }
        }

        #endregion
        public USUARIO()
        {
        }
        /// <summary>
        /// Obtiene codigo ficepi, nombre y apellidos para un codigo de red (la persona debe estar activa en ficepi)
        /// </summary>
        public static USUARIO GetDatos(SqlTransaction tr, int t314_idusuario)
        {
            USUARIO o = new USUARIO();
            SqlDataReader dr = IB.Services.Super.DAL.USUARIO.Select(tr, t314_idusuario);
            if (!dr.Read())
                o.t314_idusuario = -1;
            else
            {
                o.t314_idusuario = t314_idusuario;
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                if (dr["t001_fecalta"] != DBNull.Value)
                    o.t001_fecalta = (DateTime)dr["t001_fecalta"];
                if (dr["t001_fecbaja"] != DBNull.Value)
                    o.t001_fecbaja = (DateTime)dr["t001_fecbaja"];
                if (dr["t314_falta"] != DBNull.Value)
                    o.t314_falta = (DateTime)dr["t314_falta"];
                if (dr["t314_fbaja"] != DBNull.Value)
                    o.t314_fbaja = (DateTime)dr["t314_fbaja"];
                if (dr["t314_password"] != DBNull.Value)
                    o.t314_password = dr["t314_password"].ToString();
            }

            return o;
        }
    }
}
