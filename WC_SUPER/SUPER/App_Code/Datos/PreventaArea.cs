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
    /// Descripción breve de PreventaArea
    /// </summary>
    public class PreventaArea
    {
        #region Propiedades y Atributos 

        private int _ta200_idareapreventa;
        public int ta200_idareapreventa
        {
            get { return _ta200_idareapreventa; }
            set { _ta200_idareapreventa = value; }
        }

        private short _ta199_idunidadpreventa;
        public short ta199_idunidadpreventa
        {
            get { return _ta199_idunidadpreventa; }
            set { _ta199_idunidadpreventa = value; }
        }

        private string _ta200_denominacion;
        public string ta200_denominacion
        {
            get { return _ta200_denominacion; }
            set { _ta200_denominacion = value; }
        }

        private bool _ta200_estadoactiva;
        public bool ta200_estadoactiva
        {
            get { return _ta200_estadoactiva; }
            set { _ta200_estadoactiva = value; }
        }

        private int _t001_idficepi_responsable;
        public int t001_idficepi_responsable
        {
            get { return _t001_idficepi_responsable; }
            set { _t001_idficepi_responsable = value; }
        }

        private string _Responsable;
        public string Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        private string _denUnidad;
        public string denUnidad
        {
            get { return _denUnidad; }
            set { _denUnidad = value; }
        }

        #endregion
        public PreventaArea()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader Catalogo(short ta199_idunidadpreventa, 
                                             Nullable<int> ta200_idareapreventa, string ta200_denominacion,
                                             Nullable<bool> ta200_estadoactiva,
                                             Nullable<int> t001_idficepi_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@ta199_idunidadpreventa", SqlDbType.SmallInt, 2, ta199_idunidadpreventa),
                ParametroSql.add("@ta200_idareapreventa", SqlDbType.Int, 4, ta200_idareapreventa),
                ParametroSql.add("@ta200_denominacion", SqlDbType.Text, 50, ta200_denominacion),
                ParametroSql.add("@ta200_estadoactiva", SqlDbType.Bit, 1, ta200_estadoactiva),
                ParametroSql.add("@t001_idficepi_responsable", SqlDbType.Int, 4, t001_idficepi_responsable)
            };
            return SqlHelper.ExecuteSqlDataReader("SIC_AREAPREVENTA_C", aParam);
        }
    }
}