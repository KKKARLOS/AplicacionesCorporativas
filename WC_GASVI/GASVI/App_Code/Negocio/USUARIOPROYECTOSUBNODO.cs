using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using GASVI.DAL;

namespace GASVI.BLL
{
    public partial class USUARIOPROYECTOSUBNODO
    {
        #region Metodos

        public static Hashtable ObtenerFechasAsociacionPSN(SqlTransaction tr, int t314_idusuario)
        {
            Hashtable htUsuarioPSN = new Hashtable();
            SqlDataReader dr = DAL.USUARIOPROYECTOSUBNODO.ObtenerFechasAsociacion(tr, t314_idusuario);
            while (dr.Read())
            {
                htUsuarioPSN.Add((int)dr["t305_idproyectosubnodo"], new DateTime?[] {
                                                                (DateTime?)dr["t330_falta"],
                                                                (dr["t330_fbaja"]!=DBNull.Value)?(DateTime?)dr["t330_fbaja"]:null
                                                                });

            }
            dr.Close();
            dr.Dispose();

            return htUsuarioPSN;
        }

        #endregion
    }
}