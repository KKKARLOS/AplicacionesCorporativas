using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : EXTENSIONCONTRATO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T401_FIGURACLIENTE
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	19/12/2007 15:07:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EXTENSIONCONTRATO
    {
        #region Constructor

        public EXTENSIONCONTRATO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T377_EXTENSIONCONTRATO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	04/06/2010 12:08:18
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t306_idcontrato, int t377_idextension, decimal t377_importeser, decimal t377_marpreser, decimal t377_importepro, decimal t377_marprepro, DateTime t377_fechacontratacion)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@t377_idextension", SqlDbType.Int, 4);
            aParam[1].Value = t377_idextension;
            aParam[2] = new SqlParameter("@t377_importeser", SqlDbType.Money, 8);
            aParam[2].Value = t377_importeser;
            aParam[3] = new SqlParameter("@t377_marpreser", SqlDbType.Money, 8);
            aParam[3].Value = t377_marpreser;
            aParam[4] = new SqlParameter("@t377_importepro", SqlDbType.Money, 8);
            aParam[4].Value = t377_importepro;
            aParam[5] = new SqlParameter("@t377_marprepro", SqlDbType.Money, 8);
            aParam[5].Value = t377_marprepro;
            aParam[6] = new SqlParameter("@t377_fechacontratacion", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t377_fechacontratacion;
            // Ejecuta la query y devuelve el numero de registros modificados.

            if (tr == null) return SqlHelper.ExecuteNonQuery("SUP_EXTENSIONCONTRATO_U", aParam);
            else return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_EXTENSIONCONTRATO_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T377_EXTENSIONCONTRATO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	04/06/2010 12:08:18
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t306_idcontrato, int t377_idextension)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@t377_idextension", SqlDbType.Int, 4);
            aParam[1].Value = t377_idextension;

            if (tr == null) return SqlHelper.ExecuteNonQuery("SUP_EXTENSIONCONTRATO_D", aParam);
            else return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_EXTENSIONCONTRATO_D", aParam);
        }
        #endregion
    }
}
