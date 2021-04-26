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
	public class Videoconferencia 
	{
		#region Atributos privados 

		private int _nReserva;
		private string _sMotivo;
		private DateTime _dFecHoraIni;
		private DateTime _dFecHoraFin;
		private int _nEnviaCorreo;
		private string _sCIP;
		private string _sNombreCIP;
		private string _sCIPSol;  //Solicitante
		private DateTime _dFecModif;
		private string _sAsunto;
		private string _sCentralita;
		private string _sPrivado;
		private string _sCorreoExt;

		#endregion

		#region Propiedades públicas

		public int nReserva
		{
			get { return _nReserva; }
			set { _nReserva = value; }
		}
		public string sMotivo
		{
			get { return _sMotivo; }
			set { _sMotivo = value; }
		}
		public DateTime dFecHoraIni
		{
			get { return _dFecHoraIni; }
			set { _dFecHoraIni = value; }
		}
		public DateTime dFecHoraFin
		{
			get { return _dFecHoraFin; }
			set { _dFecHoraFin = value; }
		}
		public int nEnviaCorreo
		{
			get { return _nEnviaCorreo; }
			set { _nEnviaCorreo = value; }
		}
		public string sCIP
		{
			get { return _sCIP; }
			set { _sCIP = value; }
		}
		public string sNombreCIP
		{
			get { return _sNombreCIP; }
			set { _sNombreCIP = value; }
		}
		public string sCIPSol
		{
			get { return _sCIPSol; }
			set { _sCIPSol = value; }
		}
		public DateTime dFecModif
		{
			get { return _dFecModif; }
			set { _dFecModif = value; }
		}
		public string sAsunto
		{
			get { return _sAsunto; }
			set { _sAsunto = value; }
		}
		public string sCentralita
		{
			get { return _sCentralita; }
			set { _sCentralita = value; }
		}
		public string sPrivado
		{
			get { return _sPrivado; }
			set { _sPrivado = value; }
		}

		public string sCorreoExt
		{
			get { return _sCorreoExt; }
			set { _sCorreoExt = value; }
		}
		
		#endregion

		#region Constructores

		public Videoconferencia()
		{
			//En el constructor vacío, se inicializan los atributo
			//con los valores predeterminados según el tipo de dato.
			this.dFecHoraIni	= System.DateTime.Parse("01/01/1900");
			this.dFecHoraFin	= System.DateTime.Parse("01/01/1900");
			this.dFecModif		= System.DateTime.Parse("01/01/1900");
		}

		public Videoconferencia(int nReserva)
		{
			this.nReserva	= nReserva;
			this.dFecHoraIni	= System.DateTime.Parse("01/01/1900");
			this.dFecHoraFin	= System.DateTime.Parse("01/01/1900");
			this.dFecModif		= System.DateTime.Parse("01/01/1900");
		}

		public Videoconferencia(int nReserva, 
			string sMotivo, 
			DateTime dFecHoraIni,
			DateTime dFecHoraFin,
			int nEnviaCorreo,
			string sCIP, 
			string sNombreCIP, 
			DateTime dFecModif,
			string sAsunto,
			string sCentralita,
			string sPrivado,
			string sCorreoExt)
		{
			this.nReserva		= nReserva;
			this.sMotivo		= sMotivo;
			this.dFecHoraIni	= dFecHoraIni;
			this.dFecHoraFin	= dFecHoraFin;
			this.nEnviaCorreo	= nEnviaCorreo;
			this.sCIP			= sCIP;
			this.sNombreCIP		= sNombreCIP;
			this.dFecModif		= dFecModif;
			this.sAsunto		= sAsunto;
			this.sPrivado		= sPrivado;
			this.sCorreoExt		= sCorreoExt;
		}

		#endregion

		#region	Métodos públicos

		public DataSet ObtenerReservasRec(int nRecursoFisico, System.DateTime strFechaIni, System.DateTime strFechaFin)
		{
            return SqlHelper.ExecuteDataset(Utilidades.CadenaConexion, 
				"CR2_VIDEOSREC", nRecursoFisico, strFechaIni, strFechaFin);
		}

		public SqlDataReader ObtenerDisponibilidadRecurso(int nRecursoFisico)
		{
            return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_DISPONIBILIDADRES", "V", nRecursoFisico, this.dFecHoraIni, this.dFecHoraFin, this.nReserva);
		}
		public SqlDataReader ObtenerDisponibilidadRecurso(SqlTransaction tr, int nRecursoFisico)
		{
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_DISPONIBILIDADRES", "V", nRecursoFisico, this.dFecHoraIni, this.dFecHoraFin, this.nReserva);
		}
		

		public void Obtener(int nReserva)
		{
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_VIDEOS", nReserva);

			if (dr.Read())
			{
				this.nReserva		= int.Parse(dr["T049_IDRESERVA"].ToString());
				this.sMotivo		= dr["T049_MOTIVO"].ToString();
				
				if (!dr.IsDBNull(dr.GetOrdinal("T049_FECHORAINI")))
					this.dFecHoraIni	= (DateTime)dr["T049_FECHORAINI"];
				if (!dr.IsDBNull(dr.GetOrdinal("T049_FECHORAFIN")))
					this.dFecHoraFin	= (DateTime)dr["T049_FECHORAFIN"];

				if (dr["T049_ENVIACORREO"].ToString() == "True")
					this.nEnviaCorreo = 1;
				else
					this.nEnviaCorreo = 0;
				
				//Datos del interesado
				this.sCIP			= dr["IDCIP_INT"].ToString();
				this.sNombreCIP		= dr["NOMBRE"].ToString();
				//Datos del solicitante
				this.sCIPSol		= dr["IDCIP_SOL"].ToString();

				if (!dr.IsDBNull(dr.GetOrdinal("T049_FECMODIF")))
					this.dFecModif	= (DateTime)dr["T049_FECMODIF"];

				this.sAsunto		= dr["T049_ASUNTO"].ToString();
				this.sCentralita	= dr["T049_CENTRALITA"].ToString();
				this.sPrivado		= dr["T049_PRIVADO"].ToString();
				this.sCorreoExt		= dr["T049_CORREOEXT"].ToString();
			}
            dr.Close();
            dr.Dispose();
        }

		public void Obtener(SqlTransaction tr, int nReserva)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_VIDEOS", nReserva);

			if (dr.Read())
			{
				this.nReserva		= int.Parse(dr["T049_IDRESERVA"].ToString());
				this.sMotivo		= dr["T049_MOTIVO"].ToString();
				
				if (!dr.IsDBNull(dr.GetOrdinal("T049_FECHORAINI")))
					this.dFecHoraIni	= (DateTime)dr["T049_FECHORAINI"];
				if (!dr.IsDBNull(dr.GetOrdinal("T049_FECHORAFIN")))
					this.dFecHoraFin	= (DateTime)dr["T049_FECHORAFIN"];

				if (dr["T049_ENVIACORREO"].ToString() == "True")
					this.nEnviaCorreo = 1;
				else
					this.nEnviaCorreo = 0;
				
				//Datos del interesado
				this.sCIP			= dr["IDCIP_INT"].ToString();
				this.sNombreCIP		= dr["NOMBRE"].ToString();
				//Datos del solicitante
				this.sCIPSol		= dr["IDCIP_SOL"].ToString();

				if (!dr.IsDBNull(dr.GetOrdinal("T049_FECMODIF")))
					this.dFecModif	= (DateTime)dr["T049_FECMODIF"];

				this.sAsunto		= dr["T049_ASUNTO"].ToString();
				this.sCentralita	= dr["T049_CENTRALITA"].ToString();
				this.sPrivado		= dr["T049_PRIVADO"].ToString();
				this.sCorreoExt		= dr["T049_CORREOEXT"].ToString();
			}
            dr.Close();
            dr.Dispose();
        }

		
		public int Actualizar()
		{
            return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_VIDEOU", this.nReserva, this.sMotivo, this.dFecHoraIni,  
				this.dFecHoraFin, this.nEnviaCorreo, this.sCIP, this.sAsunto, this.sPrivado, this.sCorreoExt);
		}

		public int Actualizar(int nReserva, 
			string sMotivo, 
			DateTime dFecHoraIni,
			DateTime dFecHoraFin,
			int nEnviaCorreo,
			string sCIP,
			string sAsunto,
			string sCentralita,
			string sPrivado,
			string sCorreoExt)
		{
            return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_VIDEOU", nReserva, sMotivo, dFecHoraIni,  
				dFecHoraFin, nEnviaCorreo, sCIP, sAsunto, sCentralita, sPrivado, sCorreoExt);
		}

		public int Actualizar(SqlTransaction tr)
		{
            return SqlHelper.ExecuteNonQueryTransaccion(tr,
				"CR2_VIDEOU", this.nReserva, this.sMotivo, this.dFecHoraIni, 
				this.dFecHoraFin, this.nEnviaCorreo, this.sCIP, this.sAsunto, this.sCentralita, this.sPrivado, this.sCorreoExt);
		}


		public int Insertar()
		{
            return Convert.ToInt32(SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
				"CR2_VIDEOI", this.sMotivo, this.dFecHoraIni, 
				this.dFecHoraFin, this.nEnviaCorreo, this.sCIP, this.sAsunto, this.sCentralita, this.sPrivado, this.sCorreoExt));
		}

		public int Insertar(int nRecursoFisico,
			string sMotivo, 
			DateTime dFecHoraIni,
			DateTime dFecHoraFin,
			int nEnviaCorreo,
			string sCIP,
			string sAsunto,
			string sCentralita,
			string sPrivado,
			string sCorreoExt)
		{
			return Convert.ToInt32(SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
				"CR2_VIDEOI", nRecursoFisico, sMotivo, dFecHoraIni, 
				dFecHoraFin, nEnviaCorreo, sCIP, sAsunto, sCentralita, sPrivado, sCorreoExt));
		}

		public int Insertar(SqlTransaction tr)
		{
			return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr,
				"CR2_VIDEOI", this.sMotivo, this.dFecHoraIni, 
				this.dFecHoraFin, this.nEnviaCorreo, this.sCIP, this.sAsunto, this.sCentralita, this.sPrivado, this.sCorreoExt));
		}


		public int Eliminar()
		{
            return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_VIDEOD", this.nReserva);
		}

		public int Eliminar(int nReserva)
		{
            return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, 
				"CR2_VIDEOD", nReserva);
		}

		public int Eliminar(SqlTransaction tr)
		{
            return SqlHelper.ExecuteNonQueryTransaccion(tr,
				"CR2_VIDEOD", this.nReserva);
		}


		public SqlDataReader ObtenerRecursos()
		{
            return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_RECVIDEOS", this.nReserva);
		}
		public SqlDataReader ObtenerRecursos(SqlTransaction tr)
		{
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_RECVIDEOS", this.nReserva);
		}
		
		public int InsertarRecurso(SqlTransaction tr, int nRecurso)
		{
            return SqlHelper.ExecuteNonQueryTransaccion(tr,
				"CR2_RECVIDEOI", this.nReserva, nRecurso);
		}
		public int EliminarRecurso(SqlTransaction tr)
		{
            return SqlHelper.ExecuteNonQueryTransaccion(tr,
				"CR2_RECVIDEOD", this.nReserva);
		}

		#endregion

	}
}
