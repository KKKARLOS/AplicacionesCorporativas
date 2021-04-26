using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class RECALCULOCOSTESSUPER
    {
        #region Propiedades y Atributos

        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        private decimal _t609_costeanual;
        public decimal t609_costeanual
        {
            get { return _t609_costeanual; }
            set { _t609_costeanual = value; }
        }
        #endregion

        #region Constructor

        public RECALCULOCOSTESSUPER()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static void Insert(SqlTransaction tr, int t314_idusuario, decimal t609_costeanual)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t609_costeanual", SqlDbType.Money, 8);
            aParam[1].Value = t609_costeanual;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_RECALCULOCOSTESSUPER_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RECALCULOCOSTESSUPER_I", aParam);
        }


        #endregion
    }
}
