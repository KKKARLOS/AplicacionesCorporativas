using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de PreventaUnidad
    /// </summary>
    public class PreventaUnidad
    {
        #region Propiedades y Atributos

        private short _ta199_idunidadpreventa;
        public short ta199_idunidadpreventa
        {
            get { return _ta199_idunidadpreventa; }
            set { _ta199_idunidadpreventa = value; }
        }

        private string _ta199_denominacion;
        public string ta199_denominacion
        {
            get { return _ta199_denominacion; }
            set { _ta199_denominacion = value; }
        }

        private bool _ta199_estadoactiva;
        public bool ta199_estadoactiva
        {
            get { return _ta199_estadoactiva; }
            set { _ta199_estadoactiva = value; }
        }

        #endregion
        public PreventaUnidad()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static SqlDataReader Catalogo(Nullable<short> ta199_idunidadpreventa, string ta199_denominacion, Nullable<bool> ta199_estadoactiva)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@ta199_idunidadpreventa", SqlDbType.SmallInt, 2, ta199_idunidadpreventa),
                ParametroSql.add("@ta199_denominacion", SqlDbType.Text, 50, ta199_denominacion),
                ParametroSql.add("@ta199_estadoactiva", SqlDbType.Bit, 1, ta199_estadoactiva)
            };
            return SqlHelper.ExecuteSqlDataReader("SIC_UNIDADPREVENTA_CAT", aParam);
        }
    }
}