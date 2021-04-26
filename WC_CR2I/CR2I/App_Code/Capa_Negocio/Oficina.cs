using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using CR2I.Capa_Datos;


namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Oficina.
	/// </summary>
	public class Oficina
	{
		#region Atributos privados 

		private int _nCentro;
		private int _nOficina;
		private string _sNombre;
		private string _sCentro;
		private string _sTelefono;
		private string _sFax;
		private string _sPrefijo;
		private string _sCentralita;

		#endregion

		#region Propiedades públicas

		public int nCentro
		{
			get { return _nCentro; }
			set { _nCentro = value; }
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
		public string sCentro
		{
			get { return _sCentro; }
			set { _sCentro = value; }
		}
		public string sTelefono
		{
			get { return _sTelefono; }
			set { _sTelefono = value; }
		}
		public string sFax
		{
			get { return _sFax; }
			set { _sFax = value; }
		}
		public string sPrefijo
		{
			get { return _sPrefijo; }
			set { _sPrefijo = value; }
		}
		
		public string sCentralita
		{
			get { return _sCentralita; }
			set { _sCentralita = value; }
		}
		
		#endregion

		#region Constructores

		public Oficina()
		{

		}

		public Oficina(int nOficina)
		{
			this.nOficina = nOficina;
		}


		#endregion

		#region	Métodos públicos

		public void Obtener(int nOficina)
		{

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_OFICINAS", nOficina);

			if (dr.Read())
			{
				this.nCentro		= int.Parse(dr["IDCENTRAB"].ToString());
				this.nOficina		= int.Parse(dr["CODIGO"].ToString());
				this.sNombre		= dr["DESCRIPCION"].ToString();
				this.sCentro		= dr["T009_DESCENTRAB"].ToString();
				this.sTelefono		= dr["TELEFONO"].ToString();
				this.sFax			= dr["FAX"].ToString();
				this.sPrefijo		= dr["PREFIJO"].ToString();
				this.sCentralita	= dr["MAILCENTRA"].ToString();
			}
            dr.Close();
            dr.Dispose();
        }

		public void Obtener(SqlTransaction tr, int nOficina)
		{

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_OFICINAS", nOficina);

			if (dr.Read())
			{
				this.nCentro		= int.Parse(dr["IDCENTRAB"].ToString());
				this.nOficina		= int.Parse(dr["CODIGO"].ToString());
				this.sNombre		= dr["DESCRIPCION"].ToString();
				this.sCentro		= dr["T009_DESCENTRAB"].ToString();
				this.sTelefono		= dr["TELEFONO"].ToString();
				this.sFax			= dr["FAX"].ToString();
				this.sPrefijo		= dr["PREFIJO"].ToString();
				this.sCentralita	= dr["MAILCENTRA"].ToString();
			}
            dr.Close();
            dr.Dispose();
        }

		public SqlDataReader obtenerTodas()
		{
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
				"FIC_MANTOFICINA", "SelCR2I");

			return dr;
		}
		public SqlDataReader obtenerTodasSinCentro()
		{
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
				"FIC_MANTOFICINA", "SelCR2I2");

			return dr;
		}
		public DataSet obtenerTodasMantenimiento()
		{
			DataSet ds = SqlHelper.ExecuteDataset(Utilidades.CadenaConexion,
				"FIC_MANTOFICINA", "SelCR2I3");

			return ds;
		}

		public SqlDataReader ObtenerOficinasReserva(int nReserva)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"CR2_OFICINAVIDEOS", nReserva);

			return dr;
		}

		public SqlDataReader ObtenerOficinasReserva(SqlTransaction tr, int nReserva)
		{	
			SqlDataReader dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, 
				"CR2_OFICINAVIDEOS", nReserva);

			return dr;
		}

        public static ArrayList ListaOficinas()
        {
            if (HttpContext.Current.Cache["cr2_oficinas"] == null)
            {
                ArrayList aTablaOfi = new ArrayList();

                Oficina objOfi = new Oficina();
                SqlDataReader dr = objOfi.obtenerTodas();
                ElementoCombo objItem;
                while (dr.Read())
                {
                    objItem = new ElementoCombo(dr["CODIGO"].ToString(), dr["DESCRIPCION"].ToString());
                    aTablaOfi.Add(objItem);
                }
                dr.Close();

                HttpContext.Current.Cache.Insert("cr2_oficinas", aTablaOfi, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                return aTablaOfi;
            }
            else
            {
                return (ArrayList)HttpContext.Current.Cache["cr2_oficinas"];
            }
        }
        public static ArrayList ListaOficinasSC()
        {
            if (HttpContext.Current.Cache.Get("cr2_oficinasSC") == null)
            {
                ArrayList aTablaOfi = new ArrayList();

                Oficina objOfi = new Oficina();
                SqlDataReader dr = objOfi.obtenerTodasSinCentro();
                ElementoCombo objItem;
                while (dr.Read())
                {
                    objItem = new ElementoCombo(dr["CODIGO"].ToString(), dr["DESCRIPCION"].ToString());
                    aTablaOfi.Add(objItem);
                }
                dr.Close();

                HttpContext.Current.Cache.Insert("cr2_oficinasSC", aTablaOfi, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                return aTablaOfi;
            }
            else
            {
                return (ArrayList)HttpContext.Current.Cache.Get("cr2_oficinasSC");
            }
        }
        #endregion
	}
}
