using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
    /// Class	 : CRONOACTUMA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T961_ACTU_GLOB_TPL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CRONOACTUMA
	{
		#region Propiedades y Atributos

        private int _t714_idcrono;
        public int t714_idcrono
		{
            get { return _t714_idcrono; }
            set { _t714_idcrono = value; }
		}

		private DateTime _t714_fecha;
        public DateTime t714_fecha
		{
            get { return _t714_fecha; }
            set { _t714_fecha = value; }
		}

        private string _t714_fichero;
        public string t714_fichero
		{
            get { return _t714_fichero; }
            set { _t714_fichero = value; }
		}
        private int? _t001_idficepi;
        public int? t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }
		#endregion

		#region Constructor

        public CRONOACTUMA()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_CVT_CRONOACTUMASIVA_C", aParam);
        }

		#endregion
	}
}
