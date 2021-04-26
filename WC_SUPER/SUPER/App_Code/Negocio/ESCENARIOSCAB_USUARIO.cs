using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class ESCENARIOSCAB_USUARIO
    {
        #region Metodos

        //public static void InsertarUsuario(SqlTransaction tr, int t789_idescenario, int anomes_min, int anomes_max)
        //{
        //    Capa_Datos.ESCENARIOMES.InsertarMeses(tr, t789_idescenario, anomes_min, anomes_max);
        //}

        public static Hashtable ObtenerUsuariosEscenario(SqlTransaction tr, int t789_idescenario)
        {
            Hashtable oHT = new Hashtable();
            SqlDataReader dr = Capa_Datos.ESCENARIOSCAB_USUARIO.ObtenerUsuariosEscenario(tr, t789_idescenario);

            while (dr.Read())
            {
                oHT.Add((int)dr["t314_idusuario"], new int[] { (int)dr["t731_idescenariousuario"], (int)dr["t314_idusuario"] });
            }
            dr.Close();
            dr.Dispose();

            return oHT;
        }

        #endregion
    }
}
