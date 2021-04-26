using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using GASVI.DAL;
using System.Text.RegularExpressions;

namespace GASVI.BLL
{
    public class DIALIMITECONTABILIZACIONGV
    {
        #region Propiedades

        private byte _t670_dialimitecontanoanterior;
        public byte t670_dialimitecontanoanterior
        {
            get { return _t670_dialimitecontanoanterior; }
            set { _t670_dialimitecontanoanterior = value; }
        }

        private byte _t670_dialimitecontmesanterior;
        public byte t670_dialimitecontmesanterior
        {
            get { return _t670_dialimitecontmesanterior; }
            set { _t670_dialimitecontmesanterior = value; }
        }

        private short _t670_diapago;
        public short t670_diapago
        {
            get { return _t670_diapago; }
            set { _t670_diapago = value; }
        }

        private DateTime? _t670_diaavisado;
        public DateTime? t670_diaavisado
        {
            get { return _t670_diaavisado; }
            set { _t670_diaavisado = value; }
        }

        private DateTime? _t670_diapagado;
        public DateTime? t670_diapagado
        {
            get { return _t670_diapagado; }
            set { _t670_diapagado = value; }
        }

        private byte _t670_vigenciaaparcadas;
        public byte t670_vigenciaaparcadas
        {
            get { return _t670_vigenciaaparcadas; }
            set { _t670_vigenciaaparcadas = value; }
        }

        private byte _t670_avisoaparcadas;
        public byte t670_avisoaparcadas
        {
            get { return _t670_avisoaparcadas; }
            set { _t670_avisoaparcadas = value; }
        }


        #endregion

        public DIALIMITECONTABILIZACIONGV()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DIALIMITECONTABILIZACIONGV Obtener()
        {
            DIALIMITECONTABILIZACIONGV o = new DIALIMITECONTABILIZACIONGV();

            SqlDataReader dr = DAL.DIALIMITECONTABILIZACIONGV.Obtener(null);
            if (dr.Read()){
                if (dr["t670_dialimitecontanoanterior"] != DBNull.Value)
                    o.t670_dialimitecontanoanterior = (byte)dr["t670_dialimitecontanoanterior"];
                if (dr["t670_dialimitecontmesanterior"] != DBNull.Value)
                    o.t670_dialimitecontmesanterior = (byte)dr["t670_dialimitecontmesanterior"];
                if (dr["t670_diapago"] != DBNull.Value)
                    o.t670_diapago = short.Parse(dr["t670_diapago"].ToString());
                if (dr["t670_diaavisado"] != DBNull.Value)
                    o.t670_diaavisado = DateTime.Parse(dr["t670_diaavisado"].ToString());
                if (dr["t670_diapagado"] != DBNull.Value)
                    o.t670_diapagado = DateTime.Parse(dr["t670_diapagado"].ToString());
                if (dr["t670_vigenciaaparcadas"] != DBNull.Value)
                    o.t670_vigenciaaparcadas = (byte)dr["t670_vigenciaaparcadas"];
                if (dr["t670_avisoaparcadas"] != DBNull.Value)
                    o.t670_avisoaparcadas = (byte)dr["t670_avisoaparcadas"];

            }
            dr.Close();
            dr.Dispose();

            return o;
        }

        public static void UpdateParametrizacion(string sDatos)
        {
            string[] aDatos = Regex.Split(sDatos, "#sCad#");
            DAL.DIALIMITECONTABILIZACIONGV.UpdateParametrizacion(null,
                                                                 byte.Parse(aDatos[0]),
                                                                 byte.Parse(aDatos[1]),
                                                                 byte.Parse(aDatos[2]),
                                                                 byte.Parse(aDatos[3]),
                                                                 byte.Parse(aDatos[4]));
        }
    }

}