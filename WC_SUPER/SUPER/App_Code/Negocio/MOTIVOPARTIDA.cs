using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class MOTIVOPARTIDA
    {
        #region Propiedades y Atributos

        private int _t806_idmotivopartida;
        public int t806_idmotivopartida
        {
            get { return _t806_idmotivopartida; }
            set { _t806_idmotivopartida = value; }
        }

        private int _t790_idescenariopar;
        public int t790_idescenariopar
        {
            get { return _t790_idescenariopar; }
            set { _t790_idescenariopar = value; }
        }

        private int? _t303_idnodo;
        public int? t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        private int? _t315_idproveedor;
        public int? t315_idproveedor
        {
            get { return _t315_idproveedor; }
            set { _t315_idproveedor = value; }
        }

        private string _t806_motivo;
        public string t806_motivo
        {
            get { return _t806_motivo; }
            set { _t806_motivo = value; }
        }
        #endregion

        #region Constructor

        public MOTIVOPARTIDA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos


        public static Hashtable ObtenerCatalogoEscenario(SqlTransaction tr, int t789_idescenario)
        {
            Hashtable oHT = new Hashtable();
            SqlDataReader dr = Capa_Datos.MOTIVOPARTIDA.ObtenerCatalogoEscenario(tr, t789_idescenario);

            while (dr.Read())
            {
                oHT.Add((int)dr["t806_idmotivopartida"], (int)dr["t806_idmotivopartida"]);
            }
            dr.Close();
            dr.Dispose();

            return oHT;
        }

        #endregion
    }
}
