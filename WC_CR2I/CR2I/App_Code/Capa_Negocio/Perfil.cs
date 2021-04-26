using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Perfil.
	/// </summary>
	public class Perfil
	{
		#region Atributos privados 

		private string _sCIP;
		private string _sNombre;
		private string _sPerCR2I;
		private string _sPerReunion;
        private string _sPerVideo;
        private string _sPerWebex;
        private string _sPerWifi;

		#endregion

		#region Propiedades públicas

		public string sCIP
		{
			get { return _sCIP; }
			set { _sCIP = value; }
		}
		public string sNombre
		{
			get { return _sNombre; }
			set { _sNombre = value; }
		}
		public string sPerCR2I
		{
			get { return _sPerCR2I; }
			set { _sPerCR2I = value; }
		}
		public string sPerReunion
		{
			get { return _sPerReunion; }
			set { _sPerReunion = value; }
		}
        public string sPerVideo
        {
            get { return _sPerVideo; }
            set { _sPerVideo = value; }
        }
        public string sPerWebex
        {
            get { return _sPerWebex; }
            set { _sPerWebex = value; }
        }
        public string sPerWifi
        {
            get { return _sPerWifi; }
            set { _sPerWifi = value; }
        }

		#endregion

		#region Constructores

		public Perfil()
		{
			//En el constructor vacío, se inicializan los atributo
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region	Métodos públicos

		public SqlDataReader Obtener(string sInicial)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "CR2_PERFILS", sInicial);

			return dr;
		}

		public int Actualizar()
		{
			int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, "CR2_PERFILU", this.sCIP, 
                                                    this.sPerCR2I, this.sPerReunion, this.sPerVideo,
                                                    this.sPerWebex, this.sPerWifi);
			
			return nResul;
		}

		
		#endregion
	}
}
