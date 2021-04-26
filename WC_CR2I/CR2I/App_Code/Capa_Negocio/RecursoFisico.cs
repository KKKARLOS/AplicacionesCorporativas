using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;
using System.Collections;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de RecursoFisico.
	/// </summary>
	public class RecursoFisico
	{
		#region Atributos privados 

		private int _nRecursoFisico;
		private int _nOficina;
		private string _sNombre;
		private string _sUbicacion;
		private int _nReunion;
		private int _nVideo;
		private string _sCaracteristicas;
        private int _nRequisitos;
        private string _sRequisitos;

		#endregion

		#region Propiedades públicas

		public int nRecursoFisico
		{
			get { return _nRecursoFisico; }
			set { _nRecursoFisico = value; }
		}
		public int nOficina
		{
			get { return _nOficina; }
			set { _nOficina = value; }
		}
		public string sNombre
		{
			get { return _sNombre; }
			set { _sNombre = value; }
		}
		public string sUbicacion
		{
			get { return _sUbicacion; }
			set { _sUbicacion = value; }
		}
		public int nReunion
		{
			get { return _nReunion; }
			set { _nReunion = value; }
		}
		public int nVideo
		{
			get { return _nVideo; }
			set { _nVideo = value; }
		}
		public string sCaracteristicas
		{
			get { return _sCaracteristicas; }
			set { _sCaracteristicas = value; }
		}
        public int nRequisitos
        {
            get { return _nRequisitos; }
            set { _nRequisitos = value; }
        }
        public string sRequisitos
        {
            get { return _sRequisitos; }
            set { _sRequisitos = value; }
        }

		#endregion

		#region Constructores

		public RecursoFisico()
		{
			//En el constructor vacío, se inicializan los atributo
			//con los valores predeterminados según el tipo de dato.
		}

		public RecursoFisico(int nRecursoFisico)
		{
			this.nRecursoFisico	= nRecursoFisico;
		}
		public RecursoFisico(int nRecursoFisico, 
			int nOficina,
			string sNombre, 
			string sUbicacion,
            int nReunion,
            int nVideo,
            string sCaracteristicas,
            int nRequisitos,
            string sRequisitos)
		{
			this.nRecursoFisico = nRecursoFisico;
			this.nOficina		= nOficina;
			this.sNombre		= sNombre;
			this.sUbicacion		= sUbicacion;
			this.nReunion		= nReunion;
            this.nVideo         = nVideo;
            this.sCaracteristicas = sCaracteristicas;
            this.nRequisitos    = nRequisitos;
            this.sRequisitos    = sRequisitos;
        }

		#endregion

		#region	Métodos públicos

		public SqlDataReader ObtenerRecursoOfi(string sOpcion, int nOficina, int intOrden, int intAscDesc)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_RECURSOOFI", nOficina, intOrden, intAscDesc, sOpcion);

			return dr;
		}

		public DataSet ObtenerRecursoOfiDS(int nOficina)
		{	
			DataSet dsRR = SqlHelper.ExecuteDataset(Utilidades.CadenaConexion, 
				"CR2_RECURSOOFI", nOficina );

			return dsRR;
		}

		public SqlDataReader ObtenerRecursoVideo(int nOficina, string sOpcion)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_RECURSOVIDEO", nOficina, sOpcion);

			return dr;
		}
		public SqlDataReader ObtenerRecursoVideo(SqlTransaction tr, int nOficina, string sOpcion)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_RECURSOVIDEO", nOficina, sOpcion);

			return dr;
		}

		public void Obtener(int nRecursoFisico)
		{

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_RECURSOS", nRecursoFisico);

			if (dr.Read())
			{
				this.nRecursoFisico	= int.Parse(dr["T046_IDRECURSO"].ToString());
				this.nOficina		= int.Parse(dr["T010_IDOFICINA"].ToString());
				this.sNombre		= dr["T046_NOMBRE"].ToString();
				this.sUbicacion		= dr["T046_UBICACION"].ToString();
				if ((bool)dr["T046_REUNION"])
					this.nReunion	= 1;
				else
					this.nReunion	= 0;
                if ((bool)dr["T046_VIDEO"])
                    this.nVideo = 1;
                else
                    this.nVideo = 0;
                this.sCaracteristicas = dr["T046_CARAC"].ToString();
                this.nRequisitos = int.Parse(dr["T046_BREQUISITOS"].ToString());

                //if ((bool)dr["T046_BREQUISITOS"])
                //    this.nRequisitos = 1;
                //else
                //    this.nRequisitos = 0;
                this.sRequisitos = dr["T046_SREQUISITOS"].ToString();
            }
            dr.Close();
            dr.Dispose();
        }

		public void Obtener(SqlTransaction tr, int nRecursoFisico)
		{

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_RECURSOS", nRecursoFisico);

			if (dr.Read())
			{
				this.nRecursoFisico	= int.Parse(dr["T046_IDRECURSO"].ToString());
				this.nOficina		= int.Parse(dr["T010_IDOFICINA"].ToString());
				this.sNombre		= dr["T046_NOMBRE"].ToString();
				this.sUbicacion		= dr["T046_UBICACION"].ToString();
				if (dr["T046_REUNION"].ToString() == "True")
					this.nReunion	= 1;
				else
					this.nReunion	= 0;
				if (dr["T046_VIDEO"].ToString() == "True")
					this.nVideo		= 1;
				else
					this.nVideo		= 0;
				this.sCaracteristicas = dr["T046_CARAC"].ToString();
                //this.nRequisitos = int.Parse(dr["T046_BREQUISITOS"].ToString());
                //if ((bool)dr["T046_BREQUISITOS"])
                //    this.nRequisitos = 1;
                //else
                //    this.nRequisitos = 0;
                this.sRequisitos = dr["T046_SREQUISITOS"].ToString();
            }
            dr.Close();
            dr.Dispose();
        }
		
		public int Actualizar()
		{
			int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
                "CR2_RECURSOU", this.nRecursoFisico, this.nOficina, this.sNombre, this.sUbicacion, this.nReunion, this.nVideo, this.sCaracteristicas, this.nRequisitos, this.sRequisitos);
			
			return nResul;
		}
		
		public int Actualizar(int nRecursoFisico, 
			int nOficina, 
			string sNombre, 
			string sUbicacion, 
			int nReunion,
			int nVideo,
            string sCaracteristicas,
            int nRequisitos,
            string sRequisitos)
		{
			int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
                "CR2_RECURSOU", nRecursoFisico, nOficina, sNombre, sUbicacion, nReunion, nVideo, sCaracteristicas, nRequisitos, sRequisitos);
			
			return nResul;
		}

		
		public int Insertar()
		{
			object objResul = SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
                "CR2_RECURSOI", this.nOficina, this.sNombre, this.sUbicacion, this.nReunion, this.nVideo, this.sCaracteristicas, this.nRequisitos, this.sRequisitos);
			
			int nResul = int.Parse(objResul.ToString());
			return nResul;

		}
        
		public int Insertar(int nOficina, 
			string sNombre, 
			string sUbicacion, 
			int nReunion,
			int nVideo,
            string sCaracteristicas,
            int nRequisitos,
            string sRequisitos)
		{
			object objResul = SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
                "CR2_RECURSOI", nOficina, sNombre, sUbicacion, nReunion, nVideo, sCaracteristicas, nRequisitos, sRequisitos);
			
			int nResul = int.Parse(objResul.ToString());
			return nResul;
		}


		public int Eliminar()
		{
			int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_RECURSOD", this.nRecursoFisico);
			
			return nResul;
		}
        
		public int Eliminar(int nRecursoFisico)
		{
			int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
				"CR2_RECURSOD", nRecursoFisico);
			
			return nResul;
		}

        public static ArrayList ListaSalas()
        {
            if (HttpContext.Current.Cache["cr2_salas"] == null)
                {
                ArrayList aTablaSal = new ArrayList();

                RecursoFisico objRec = new RecursoFisico();
                SqlDataReader dr = objRec.ObtenerRecursoOfi("A", -1, 2, 0);
                RecursoFisico objRecAux;
                while (dr.Read())
                {
                    objRecAux = new RecursoFisico(int.Parse(dr["CODIGO"].ToString()), int.Parse(dr["IDOFICINA"].ToString()), dr["DESCRIPCION"].ToString(), dr["UBICACION"].ToString(), int.Parse(dr["T046_REUNION"].ToString()), int.Parse(dr["T046_VIDEO"].ToString()), dr["CARACTERISTICAS"].ToString(), int.Parse(dr["T046_BREQUISITOS"].ToString()), dr["REQUISITOS"].ToString());
                    aTablaSal.Add(objRecAux);
                }
                dr.Close();
                dr.Dispose();

                HttpContext.Current.Cache.Insert("cr2_salas", aTablaSal, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                return aTablaSal;
            }
            else
            {
                return (ArrayList)HttpContext.Current.Cache["cr2_salas"];
            }
        }

		#endregion
	}
}
