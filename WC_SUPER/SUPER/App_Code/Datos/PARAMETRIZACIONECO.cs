using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
//para gestion de roles
using System.Web.Security;

namespace SUPER.Capa_Datos
{
    public partial class PARAMETRIZACIONECO
    {

        #region Constructor

        public PARAMETRIZACIONECO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static SqlDataReader Obtener(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("ECO_PARAMETRIZACIONECO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "ECO_PARAMETRIZACIONECO_CAT", aParam);
        }


        #endregion
    }
}
