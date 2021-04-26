using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
    public class TELERREUNION
    {
        #region Propiedades y Atributos

        private int _t149_idTL;
        public int t149_idTL
        {
            get { return _t149_idTL; }
            set { _t149_idTL = value; }
        }

        private int _t148_idlicenciawebex;
        public int t148_idlicenciawebex
        {
            get { return _t148_idlicenciawebex; }
            set { _t148_idlicenciawebex = value; }
        }

        private string _t149_motivo;
        public string t149_motivo
        {
            get { return _t149_motivo; }
            set { _t149_motivo = value; }
        }

        private DateTime _t149_fechoraini;
        public DateTime t149_fechoraini
        {
            get { return _t149_fechoraini; }
            set { _t149_fechoraini = value; }
        }

        private DateTime _t149_fechorafin;
        public DateTime t149_fechorafin
        {
            get { return _t149_fechorafin; }
            set { _t149_fechorafin = value; }
        }

        private int _T001_IDFICEPI_INTERESADO;
        public int T001_IDFICEPI_INTERESADO
        {
            get { return _T001_IDFICEPI_INTERESADO; }
            set { _T001_IDFICEPI_INTERESADO = value; }
        }

        private DateTime _t149_fecmodif;
        public DateTime t149_fecmodif
        {
            get { return _t149_fecmodif; }
            set { _t149_fecmodif = value; }
        }

        private string _t149_asunto;
        public string t149_asunto
        {
            get { return _t149_asunto; }
            set { _t149_asunto = value; }
        }

        private string _t149_observaciones;
        public string t149_observaciones
        {
            get { return _t149_observaciones; }
            set { _t149_observaciones = value; }
        }

        private string _t149_privado;
        public string t149_privado
        {
            get { return _t149_privado; }
            set { _t149_privado = value; }
        }

        private string _t149_correoext;
        public string t149_correoext
        {
            get { return _t149_correoext; }
            set { _t149_correoext = value; }
        }

        private bool _t149_vozip;
        public bool t149_vozip
        {
            get { return _t149_vozip; }
            set { _t149_vozip = value; }
        }

        private int _T001_IDFICEPI_SOLICITANTE;
        public int T001_IDFICEPI_SOLICITANTE
        {
            get { return _T001_IDFICEPI_SOLICITANTE; }
            set { _T001_IDFICEPI_SOLICITANTE = value; }
        }

        private string _sProfesional;
        public string sProfesional
        {
            get { return _sProfesional; }
            set { _sProfesional = value; }
        }

        private string _CIP_INTERESADO;
        public string CIP_INTERESADO
        {
            get { return _CIP_INTERESADO; }
            set { _CIP_INTERESADO = value; }
        }

        #endregion

        #region Constructor

        public TELERREUNION()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static int Insertar(SqlTransaction tr, int t148_idlicenciawebex, string t149_motivo, DateTime t149_fechoraini, DateTime t149_fechorafin, int T001_IDFICEPI, DateTime t149_fecmodif, string t149_asunto, string t149_observaciones, string t149_privado, string t149_correoext, bool t149_vozip)
        {
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
                    "CR2_TELERREUNION_I", t148_idlicenciawebex, t149_motivo, t149_fechoraini, t149_fechorafin, T001_IDFICEPI, t149_fecmodif, t149_asunto, t149_observaciones, t149_privado, t149_correoext, t149_vozip));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "CR2_TELERREUNION_I", t148_idlicenciawebex, t149_motivo, t149_fechoraini, t149_fechorafin, T001_IDFICEPI, t149_fecmodif, t149_asunto, t149_observaciones, t149_privado, t149_correoext, t149_vozip));
        }

        public static int Actualizar(SqlTransaction tr, int t149_idTL, int t148_idlicenciawebex, string t149_motivo, DateTime t149_fechoraini, DateTime t149_fechorafin, int T001_IDFICEPI, DateTime t149_fecmodif, string t149_asunto, string t149_observaciones, string t149_privado, string t149_correoext, bool t149_vozip)
        {
            if (tr == null)
                return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
                    "CR2_TELERREUNION_U", t149_idTL, t148_idlicenciawebex, t149_motivo, t149_fechoraini,
                    t149_fechorafin, T001_IDFICEPI, t149_fecmodif, t149_asunto, t149_observaciones, t149_privado, t149_correoext, t149_vozip);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_TELERREUNION_U", t149_idTL, t148_idlicenciawebex, t149_motivo, t149_fechoraini,
                    t149_fechorafin, T001_IDFICEPI, t149_fecmodif, t149_asunto, t149_observaciones, t149_privado, t149_correoext, t149_vozip);
        }

        public static int Eliminar(SqlTransaction tr, int t149_idTL)
        {
            if (tr == null)
                return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
                    "CR2_TELERREUNION_D", t149_idTL);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_TELERREUNION_D", t149_idTL);
        }

        public static TELERREUNION Obtener(SqlTransaction tr, int t149_idTL)
        {
            TELERREUNION o = new TELERREUNION();

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
                    "CR2_TELERREUNION_S", t149_idTL);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "CR2_TELERREUNION_S", t149_idTL);

            if (dr.Read())
            {
                if (dr["t149_idTL"] != DBNull.Value)
                    o.t149_idTL = int.Parse(dr["t149_idTL"].ToString());
                if (dr["t148_idlicenciawebex"] != DBNull.Value)
                    o.t148_idlicenciawebex = int.Parse(dr["t148_idlicenciawebex"].ToString());
                if (dr["t149_motivo"] != DBNull.Value)
                    o.t149_motivo = (string)dr["t149_motivo"];
                if (dr["t149_fechoraini"] != DBNull.Value)
                    o.t149_fechoraini = (DateTime)dr["t149_fechoraini"];
                if (dr["t149_fechorafin"] != DBNull.Value)
                    o.t149_fechorafin = (DateTime)dr["t149_fechorafin"];
                if (dr["IDFICEPI_INTERESADO"] != DBNull.Value)
                    o.T001_IDFICEPI_INTERESADO = int.Parse(dr["IDFICEPI_INTERESADO"].ToString());
                if (dr["CIP_INTERESADO"] != DBNull.Value)
                    o.CIP_INTERESADO = (string)dr["CIP_INTERESADO"];
                if (dr["PROFESIONAL"] != DBNull.Value)
                    o.sProfesional = (string)dr["PROFESIONAL"];
                if (dr["t149_fecmodif"] != DBNull.Value)
                    o.t149_fecmodif = (DateTime)dr["t149_fecmodif"];
                if (dr["IDFICEPI_SOLICITANTE"] != DBNull.Value)
                    o.T001_IDFICEPI_SOLICITANTE = int.Parse(dr["IDFICEPI_SOLICITANTE"].ToString());
                if (dr["t149_asunto"] != DBNull.Value)
                    o.t149_asunto = (string)dr["t149_asunto"];
                if (dr["t149_observaciones"] != DBNull.Value)
                    o.t149_observaciones = (string)dr["t149_observaciones"];
                if (dr["t149_privado"] != DBNull.Value)
                    o.t149_privado = (string)dr["t149_privado"];
                if (dr["t149_correoext"] != DBNull.Value)
                    o.t149_correoext = (string)dr["t149_correoext"];
                if (dr["t149_vozip"] != DBNull.Value)
                    o.t149_vozip = (bool)dr["t149_vozip"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de TELERREUNION"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }




        #endregion

    }
}