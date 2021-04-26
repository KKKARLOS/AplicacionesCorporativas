using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de REGISTROUSOPANTALLASSUPER
    /// </summary>
    public class REGISTROUSOPANTALLASSUPER
    {
        public REGISTROUSOPANTALLASSUPER()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        #region Metodos

        public static void Registrar(int t198_idpantalla, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t198_idpantalla", SqlDbType.Int, 4, t198_idpantalla),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };

            SqlHelper.ExecuteNonQuery("SUP_REGISTROUSOPANTALLASSUPER_INS", aParam);
        }

        #endregion
    }

}