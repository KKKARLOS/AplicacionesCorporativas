using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GASVI.DAL;

namespace GASVI.BLL
{
	/// <summary>
	/// Descripción breve de Idiomas.
	/// </summary>
    public partial class AccesoAplicaciones
    {
        #region Atributos

        private bool _Estado;
        private string _Motivo;
        private bool _Novedades;

        #endregion

        #region Propiedades

        public bool Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
        public string Motivo
        {
            get { return _Motivo; }
            set { _Motivo = value; }
        }
        public bool Novedades
        {
            get { return _Novedades; }
            set { _Novedades = value; }
        }

        #endregion

        #region Contructores

        public AccesoAplicaciones()
		{
            this.Estado = false;
            this.Motivo = "";
            this.Novedades = false;
        }
        #endregion

        #region Métodos

        //public void Leer()
        //{
        //    SqlDataReader dr = DAL.AccesoAplicaciones.ComprobarAcceso();

        //    if (dr.Read())
        //    {
        //        this.Estado = (bool)dr["T000_ESTADO"];
        //        this.Motivo = dr["T000_MOTIVO"].ToString();
        //        this.Novedades = (bool)dr["T000_NOVEDADES"];
        //    }

        //    dr.Close();
        //    dr.Dispose();
        //}

        public static AccesoAplicaciones Comprobar()
        {
            AccesoAplicaciones oAA = new AccesoAplicaciones();

            SqlDataReader dr = DAL.AccesoAplicaciones.ComprobarAcceso();
            if (dr.Read())
            {
                oAA.Estado = (bool)dr["T000_ESTADO"];
                oAA.Motivo = dr["T000_MOTIVO"].ToString();
                oAA.Novedades = (bool)dr["T000_NOVEDADES"];
            }
            dr.Close();
            dr.Dispose();

            return oAA;
        }
        #endregion

    }
}
