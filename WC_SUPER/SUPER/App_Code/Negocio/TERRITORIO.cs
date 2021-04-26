using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.BLL
{
	public partial class TERRITORIO
	{
		#region Propiedades y Atributos

		private byte _T007_IDTERRFIS;
		public byte T007_IDTERRFIS
		{
			get {return _T007_IDTERRFIS;}
			set { _T007_IDTERRFIS = value ;}
		}

		private string _T007_NOMTERRFIS;
		public string T007_NOMTERRFIS
		{
			get {return _T007_NOMTERRFIS;}
			set { _T007_NOMTERRFIS = value ;}
		}

		private decimal _T007_ITERDC;
		public decimal T007_ITERDC
		{
			get {return _T007_ITERDC;}
			set { _T007_ITERDC = value ;}
		}

		private decimal _T007_ITERMD;
		public decimal T007_ITERMD
		{
			get {return _T007_ITERMD;}
			set { _T007_ITERMD = value ;}
		}

		private decimal _T007_ITERDA;
		public decimal T007_ITERDA
		{
			get {return _T007_ITERDA;}
			set { _T007_ITERDA = value ;}
		}

		private decimal _T007_ITERDE;
		public decimal T007_ITERDE
		{
			get {return _T007_ITERDE;}
			set { _T007_ITERDE = value ;}
		}

		private decimal _T007_ITERK;
		public decimal T007_ITERK
		{
			get {return _T007_ITERK;}
			set { _T007_ITERK = value ;}
		}

		private string _T007_CODSAP;
		public string T007_CODSAP
		{
			get {return _T007_CODSAP;}
			set { _T007_CODSAP = value ;}
		}

		private decimal _T007_ITEATDC;
		public decimal T007_ITEATDC
		{
			get {return _T007_ITEATDC;}
			set { _T007_ITEATDC = value ;}
		}

		private decimal _T007_ITEATMD;
		public decimal T007_ITEATMD
		{
			get {return _T007_ITEATMD;}
			set { _T007_ITEATMD = value ;}
		}

		private decimal _T007_ITEATDA;
		public decimal T007_ITEATDA
		{
			get {return _T007_ITEATDA;}
			set { _T007_ITEATDA = value ;}
		}

		private decimal _T007_ITEATDE;
		public decimal T007_ITEATDE
		{
			get {return _T007_ITEATDE;}
			set { _T007_ITEATDE = value ;}
		}

		private decimal _T007_ITEATKM;
		public decimal T007_ITEATKM
		{
			get {return _T007_ITEATKM;}
			set { _T007_ITEATKM = value ;}
		}
		#endregion

		#region Constructor

		public TERRITORIO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}
        public TERRITORIO(byte IDTERRFIS,
                        string NOMTERRFIS,
                        decimal ITERDC,
                        decimal ITERMD,
                        decimal ITERDA,
                        decimal ITERDE,
                        decimal ITERK
            )
        {
            this.T007_IDTERRFIS = IDTERRFIS;
            this.T007_NOMTERRFIS = NOMTERRFIS;
            this.T007_ITERDC = ITERDC;
            this.T007_ITERMD = ITERMD;
            this.T007_ITERDA = ITERDA;
            this.T007_ITERDE = ITERDE;
            this.T007_ITERK = ITERK;
		}

		#endregion

	}
}
