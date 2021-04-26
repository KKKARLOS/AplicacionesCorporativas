using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
	public partial class DIETAKM
	{
		#region Propiedades y Atributos

		private byte _t069_iddietakm;
		public byte t069_iddietakm
		{
			get {return _t069_iddietakm;}
			set { _t069_iddietakm = value ;}
		}

		private string _t069_descripcion;
		public string t069_descripcion
		{
			get {return _t069_descripcion;}
			set { _t069_descripcion = value ;}
		}

		private decimal _t069_icdc;
		public decimal t069_icdc
		{
			get {return _t069_icdc;}
			set { _t069_icdc = value ;}
		}

		private decimal _t069_icmd;
		public decimal t069_icmd
		{
			get {return _t069_icmd;}
			set { _t069_icmd = value ;}
		}

		private decimal _t069_icda;
		public decimal t069_icda
		{
			get {return _t069_icda;}
			set { _t069_icda = value ;}
		}

		private decimal _t069_icde;
		public decimal t069_icde
		{
			get {return _t069_icde;}
			set { _t069_icde = value ;}
		}

		private decimal _t069_ick;
		public decimal t069_ick
		{
			get {return _t069_ick;}
			set { _t069_ick = value ;}
		}

		#endregion

		#region Constructor

        public DIETAKM()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}
        public DIETAKM(byte iddietakm, 
                        string descripcion, 
                        decimal icdc,
                        decimal icmd,
                        decimal icda,
                        decimal icde,
                        decimal ick
            )
        {
			this.t069_iddietakm = iddietakm;
            this.t069_descripcion = descripcion; 
            this.t069_icdc = icdc;
            this.t069_icmd = icmd;
            this.t069_icda = icda;
            this.t069_icde = icde;
            this.t069_ick = ick;
		}

		#endregion

	}
}
