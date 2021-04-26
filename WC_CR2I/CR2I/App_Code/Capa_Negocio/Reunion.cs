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
	public class Reunion 
	{
		#region Atributos privados 

		private int _nReserva;
		private int _nOficina;
		private int _nRecursoFisico;
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
		public int nOficina
		{
			get { return _nOficina; }
			set { _nOficina = value; }
		}
		public int nRecursoFisico
		{
			get { return _nRecursoFisico; }
			set { _nRecursoFisico = value; }
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

		public Reunion()
		{
			//En el constructor vacío, se inicializan los atributo
			//con los valores predeterminados según el tipo de dato.
			this.dFecHoraIni	= System.DateTime.Parse("01/01/1900");
			this.dFecHoraFin	= System.DateTime.Parse("01/01/1900");
			this.dFecModif		= System.DateTime.Parse("01/01/1900");
		}

		public Reunion(int nReserva)
		{
			this.nReserva	= nReserva;
		}

		public Reunion(int nReserva, 
			int nRecursoFisico,
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
			this.nRecursoFisico = nRecursoFisico;
			this.sMotivo		= sMotivo;
			this.dFecHoraIni	= dFecHoraIni;
			this.dFecHoraFin	= dFecHoraFin;
			this.nEnviaCorreo	= nEnviaCorreo;
			this.sCIP			= sCIP;
			this.sNombreCIP		= sNombreCIP;
			this.dFecModif		= dFecModif;
			this.sAsunto		= sAsunto;
			this.sCentralita	= sCentralita;
			this.sPrivado		= sPrivado;
			this.sCorreoExt		= sCorreoExt;
		}

		#endregion

		#region	Métodos públicos

		public DataSet ObtenerReservasRec(int nRecursoFisico, System.DateTime strFechaIni, System.DateTime strFechaFin)
		{
            return SqlHelper.ExecuteDataset(Utilidades.CadenaConexion, 
				"CR2_REUNIONSREC", nRecursoFisico, strFechaIni, strFechaFin);
		}

		public SqlDataReader ObtenerDisponibilidadRecurso()
		{
            return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_DISPONIBILIDADRES", "R", this.nRecursoFisico, this.dFecHoraIni, this.dFecHoraFin, this.nReserva);
		}
		
        public SqlDataReader ObtenerDisponibilidadRecurso(SqlTransaction tr)
		{
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_DISPONIBILIDADRES", "R", this.nRecursoFisico, this.dFecHoraIni, this.dFecHoraFin, this.nReserva);
		}
		
		public SqlDataReader ObtenerDisponibilidadRecursoAnulacion(SqlTransaction tr)
		{
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_DISPONIBILIDADRES", "A", this.nRecursoFisico, this.dFecHoraIni, this.dFecHoraFin, this.nReserva);
		}

		public void Obtener(int nReserva)
		{

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_REUNIONS", nReserva);

			if (dr.Read())
			{
				this.nReserva		= int.Parse(dr["T047_IDRESERVA"].ToString());
				this.nRecursoFisico	= int.Parse(dr["T046_IDRECURSO"].ToString());
				this.nOficina		= int.Parse(dr["T010_IDOFICINA"].ToString());
				this.sMotivo		= dr["T047_MOTIVO"].ToString();
				
				if (!dr.IsDBNull(dr.GetOrdinal("T047_FECHORAINI")))
					this.dFecHoraIni	= (DateTime)dr["T047_FECHORAINI"];
				if (!dr.IsDBNull(dr.GetOrdinal("T047_FECHORAFIN")))
					this.dFecHoraFin	= (DateTime)dr["T047_FECHORAFIN"];

				if (dr["T047_ENVIACORREO"].ToString() == "True")
					this.nEnviaCorreo = 1;
				else
					this.nEnviaCorreo = 0;
				
				//Datos del interesado
				this.sCIP			= dr["IDCIP_INT"].ToString();
				this.sNombreCIP		= dr["NOMBRE"].ToString();
				//Datos del solicitante
				this.sCIPSol		= dr["IDCIP_SOL"].ToString();

				if (!dr.IsDBNull(dr.GetOrdinal("T047_FECMODIF")))
					this.dFecModif	= (DateTime)dr["T047_FECMODIF"];

				this.sAsunto		= dr["T047_ASUNTO"].ToString();
				this.sCentralita	= dr["T047_CENTRALITA"].ToString();
				this.sPrivado		= dr["T047_PRIVADO"].ToString();
				this.sCorreoExt		= dr["T047_CORREOEXT"].ToString();
			}
            dr.Close();
            dr.Dispose();
        }

		public void Obtener(SqlTransaction tr, int nReserva)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_REUNIONS", nReserva);

			if (dr.Read())
			{
				this.nReserva		= int.Parse(dr["T047_IDRESERVA"].ToString());
				this.nRecursoFisico	= int.Parse(dr["T046_IDRECURSO"].ToString());
				this.nOficina		= int.Parse(dr["T010_IDOFICINA"].ToString());
				this.sMotivo		= dr["T047_MOTIVO"].ToString();
				
				if (!dr.IsDBNull(dr.GetOrdinal("T047_FECHORAINI")))
					this.dFecHoraIni	= (DateTime)dr["T047_FECHORAINI"];
				if (!dr.IsDBNull(dr.GetOrdinal("T047_FECHORAFIN")))
					this.dFecHoraFin	= (DateTime)dr["T047_FECHORAFIN"];

				if (dr["T047_ENVIACORREO"].ToString() == "True")
					this.nEnviaCorreo = 1;
				else
					this.nEnviaCorreo = 0;
				
				//Datos del interesado
				this.sCIP			= dr["IDCIP_INT"].ToString();
				this.sNombreCIP		= dr["NOMBRE"].ToString();
				//Datos del solicitante
				this.sCIPSol		= dr["IDCIP_SOL"].ToString();

				if (!dr.IsDBNull(dr.GetOrdinal("T047_FECMODIF")))
					this.dFecModif	= (DateTime)dr["T047_FECMODIF"];

				this.sAsunto		= dr["T047_ASUNTO"].ToString();
				this.sCentralita	= dr["T047_CENTRALITA"].ToString();
				this.sPrivado		= dr["T047_PRIVADO"].ToString();
				this.sCorreoExt		= dr["T047_CORREOEXT"].ToString();
			}
            dr.Close();
            dr.Dispose();
        }

		
		public int Actualizar()
		{
			int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_REUNIONU", this.nReserva, this.nRecursoFisico, this.sMotivo, this.dFecHoraIni,  
				this.dFecHoraFin, this.nEnviaCorreo, this.sCIP, this.sAsunto, this.sCentralita, this.sPrivado, this.sCorreoExt);
			
			return nResul;
		}

		public int Actualizar(int nReserva, 
			int nRecursoFisico,
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
				"CR2_REUNIONU", nReserva, nRecursoFisico, sMotivo, dFecHoraIni,  
						dFecHoraFin, nEnviaCorreo, sCIP, sAsunto, sCentralita, sPrivado, sCorreoExt);
		}

		public int Actualizar(SqlTransaction tr)
		{
            return SqlHelper.ExecuteNonQueryTransaccion(tr,
				"CR2_REUNIONU", this.nReserva, this.nRecursoFisico, this.sMotivo, this.dFecHoraIni, 
				this.dFecHoraFin, this.nEnviaCorreo, this.sCIP, this.sAsunto, this.sCentralita, this.sPrivado, this.sCorreoExt);
		}


		public int Insertar()
		{
            return Convert.ToInt32(SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
				"CR2_REUNIONI", this.nRecursoFisico, this.sMotivo, this.dFecHoraIni, 
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
			object objResul = SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
				"CR2_REUNIONI", nRecursoFisico, sMotivo, dFecHoraIni, 
				dFecHoraFin, nEnviaCorreo, sCIP, sAsunto, sCentralita, sPrivado, sCorreoExt);
			
			int nResul = int.Parse(objResul.ToString());
			return nResul;
		}

		public int Insertar(SqlTransaction tr)
		{
			return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr,
				"CR2_REUNIONI", this.nRecursoFisico, this.sMotivo, this.dFecHoraIni, 
				this.dFecHoraFin, this.nEnviaCorreo, this.sCIP, this.sAsunto, this.sCentralita, this.sPrivado, this.sCorreoExt));
		}


		public int Eliminar()
		{
            return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_REUNIOND", this.nReserva);
		}

		public int Eliminar(int nReserva)
		{
            return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, 
				"CR2_REUNIOND", nReserva);
		}

		public int Eliminar(SqlTransaction tr)
		{
            return SqlHelper.ExecuteNonQueryTransaccion(tr,
				"CR2_REUNIOND", this.nReserva);
		}


		#endregion

	}
}
