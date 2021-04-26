using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Reserva.
	/// </summary>
	public class AsistenteVideo 
	{
		#region Atributos privados 

		private int _nReserva;
		private string _sCIP;
		private string _sFigura;

		#endregion

		#region Propiedades públicas

		public int nReserva
		{
			get { return _nReserva; }
			set { _nReserva = value; }
		}
		public string sCIP
		{
			get { return _sCIP; }
			set { _sCIP = value; }
		}
		public string sFigura
		{
			get { return _sFigura; }
			set { _sFigura = value; }
		}
		
		#endregion

		#region Constructores

		public AsistenteVideo()
		{
			
		}

		public AsistenteVideo(int nReserva, string sCIP, string sFigura)
		{
			this.nReserva	= nReserva;
			this.sCIP		= sCIP;
			this.sFigura	= sFigura;
		}

		#endregion

		#region	Métodos públicos

		public SqlDataReader ObtenerAsistentesReserva(int nReserva, string sAccion)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_ASISVIDEOS", sAccion, nReserva );

			return dr;
		}

		public SqlDataReader ObtenerAsistentesReserva(SqlTransaction tr, int nReserva, string sAccion)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_ASISVIDEOS", sAccion, nReserva );

			return dr;
		}

		public bool AsisteAVideo(int nReserva, int nIdFicepi)
		{
			bool bAsiste = false;
			object oResul = SqlHelper.ExecuteScalar(Utilidades.CadenaConexion, 
				"CR2_ASISTEVIDEO", nReserva, nIdFicepi);

			int nResul = int.Parse(oResul.ToString());
			if (nResul > 0)
				bAsiste = true;

			return bAsiste;
		}

		public int Insertar(int nReserva,
			string sCIP, 
			string sFigura)
		{
			int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_ASISVIDEOI", nReserva, sCIP, sFigura);
			
			return nResul;
		}

		public int Insertar(SqlTransaction tr, 
			int nReserva,
			string sCIP, 
			string sFigura)
		{
			int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, 
				"CR2_ASISVIDEOI", nReserva, sCIP, sFigura);
			
			return nResul;
		}

		public int Insertar(SqlTransaction tr)
		{
			int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, 
				"CR2_ASISVIDEOI", this.nReserva, this.sCIP, this.sFigura);
			
			return nResul;
		}


		public int Eliminar(SqlTransaction tr)
		{
			int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, 
				"CR2_ASISVIDEOD", this.nReserva);
			
			return nResul;
		}

		public int Eliminar(SqlTransaction tr, int nReserva)
		{
			int nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, 
				"CR2_ASISVIDEOD", nReserva);
			
			return nResul;
		}

		
		#endregion

	}
}
