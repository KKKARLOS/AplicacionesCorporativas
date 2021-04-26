using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Recurso.
	/// </summary>
	public class Recurso
	{
		#region Atributos
		private string _CIP;
		private string _CIPOriginal;
		private string _CodRed;
		private string _Nombre;
		private string _Apellido1;
		private string _Apellido2;
		private string _Sexo;
		private string _Domicilio;
		private string _Siglas;
		private string _Numero;
		private string _Piso;
		private string _Letra;
		private string _Poblacion;
		private string _CodPostal;
		private string _Telefono;
		private string _ExtTel;
		private string _Email;
		private string _CodRol;
		private string _DesRol;
		private string _CodCenCosIb;
		private string _DesCenCosIb;
		private int	_CenTrabAdmin;
		private string _DesCenTrabAdmin;
		private int	_CenTrabFisico;
		private string _DesCenTrabFisico;
		private int _Oficina;
		private string _DesOficina;
		private string _TipoRecurso;
		private int _CodIber;
		private int _CodProv;
		private string _DesProv;
		private int _EmpresaIB;
		private string _DesEmpresaIB;
		private int _Proveedor;
		private string _DesProveedor;
		private int _Calendario;
		private string _DesCalendario;
		private int _Automatico;
		private int _Perfil;
		private int _TerrFiscal;
		private string _DesTerrFiscal;
		private DateTime _FecNacim;
		private DateTime _FecAlta;
		private DateTime _FecBaja;
		private byte[] _Foto;
		private int _CodCenResp;
		private string _Nacionalidad;
		private float _Coste;
		private int	_CodCatConvenio;
		private int _QEQ;
		private int _ConoClave;
#endregion
		
		#region Propiedades
		public string CIP
		{
			get { return _CIP; }
			set { _CIP = value; }
		}
		public string CIPOriginal
		{
			get { return _CIPOriginal; }
			set { _CIPOriginal = value; }
		}
		public string CodRed
		{
			get { return _CodRed; }
			set { _CodRed = value; }
		}
		public string Nombre
		{
			get { return _Nombre; }
			set { _Nombre = value; }
		}
		public string Apellido1
		{
			get { return _Apellido1; }
			set { _Apellido1 = value; }
		}
		public string Apellido2
		{
			get { return _Apellido2; }
			set { _Apellido2 = value; }
		}
		public string Domicilio
		{
			get { return _Domicilio; }
			set { _Domicilio = value; }
		}
		public string Sexo
		{
			get { return _Sexo; }
			set { _Sexo = value; }
		}
		public string Siglas
		{
			get { return _Siglas; }
			set { _Siglas = value; }
		}
		public string Numero
		{
			get { return _Numero; }
			set { _Numero = value; }
		}
		public string Piso
		{
		get { return _Piso; }
		set { _Piso = value; }
		}
		public string Letra
		{
			get { return _Letra; }
			set { _Letra = value; }
		}
		public string Poblacion
		{
			get { return _Poblacion; }
			set { _Poblacion = value; }
		}
		public string CodPostal
		{
			get { return _CodPostal; }
			set { _CodPostal = value; }
		}
		public string Telefono
		{
			get { return _Telefono; }
			set { _Telefono = value; }
		}
		public string ExtTel
		{
			get { return _ExtTel; }
			set { _ExtTel = value; }
		}
		public string Email
		{
			get { return _Email; }
			set { _Email = value; }
		}
		public string CodRol
		{
			get { return _CodRol; }
			set { _CodRol = value; }
		}
		public string DesRol
		{
			get { return _DesRol; }
			set { _DesRol = value; }
		}
		public string CodCenCosIb
		{
			get { return _CodCenCosIb; }
			set { _CodCenCosIb = value; }
		}
		public string DesCenCosIb
		{
			get { return _DesCenCosIb; }
			set { _DesCenCosIb = value; }
		}
		public int CenTrabAdmin
		{
			get { return _CenTrabAdmin; }
			set { _CenTrabAdmin = value; }
		}
		public int CenTrabFisico
		{
			get { return _CenTrabFisico; }
			set { _CenTrabFisico = value; }
		}
		public int Oficina
		{
			get { return _Oficina; }
			set { _Oficina = value; }
		}
		public string DesCenTrabAdmin
		{
			get { return _DesCenTrabAdmin; }
			set { _DesCenTrabAdmin = value; }
		}
		public string DesCenTrabFisico
		{
			get { return _DesCenTrabFisico; }
			set { _DesCenTrabFisico = value; }
		}
		public string DesOficina
		{
			get { return _DesOficina; }
			set { _DesOficina = value; }
		}
		public string TipoRecurso
		{
			get { return _TipoRecurso; }
			set { _TipoRecurso = value; }
		}
		public int CodIber
		{
			get { return _CodIber; }
			set { _CodIber = value; }
		}
		public int Calendario
		{
			get { return _Calendario; }
			set { _Calendario = value; }
		}
		public string DesCalendario
		{
			get { return _DesCalendario; }
			set { _DesCalendario = value; }
		}
		public int Automatico
		{
			get { return _Automatico; }
			set { _Automatico = value; }
		}
		public int Perfil
		{
			get { return _Perfil; }
			set { _Perfil = value; }
		}
		public int TerrFiscal
		{
			get { return _TerrFiscal; }
			set { _TerrFiscal = value; }
		}

		public string DesTerrFiscal
		{
			get { return _DesTerrFiscal; }
			set { _DesTerrFiscal = value; }
		}

		public int CodProv
		{
			get { return _CodProv; }
			set { _CodProv = value; }
		}

		public string DesProv
		{
			get { return _DesProv; }
			set { _DesProv = value; }
		}

		public int EmpresaIB
		{
			get { return _EmpresaIB; }
			set { _EmpresaIB = value; }
		}

		public string DesEmpresaIB
		{
			get { return _DesEmpresaIB; }
			set { _DesEmpresaIB = value; }
		}

		public int Proveedor
		{
			get { return _Proveedor; }
			set { _Proveedor = value; }
		}

		public string DesProveedor
		{
			get { return _DesProveedor; }
			set { _DesProveedor = value; }
		}

		public DateTime FecNacim
		{
			get { return _FecNacim; }
			set { _FecNacim = value; }
		}
		public DateTime FecAlta
		{
			get { return _FecAlta; }
			set { _FecAlta = value; }
		}
		public DateTime FecBaja
		{
			get { return _FecBaja; }
			set { _FecBaja = value; }
		}

		public byte[] Foto
		{
			get { return _Foto; }
			set { _Foto = value; }
		}
		public int CodCenResp
		{
			get { return _CodCenResp; }
			set { _CodCenResp = value; }
		}
		public string Nacionalidad
		{
			get { return _Nacionalidad; }
			set { _Nacionalidad = value; }
		}
		public float Coste
		{
			get { return _Coste; }
			set { _Coste = value; }
		}

		public int CodCatConvenio
		{
			get { return _CodCatConvenio; }
			set { _CodCatConvenio = value; }
		}

		public int QEQ
		{
			get { return _QEQ; }
			set { _QEQ = value; }
		}

		public int ConoClave
		{
			get { return _ConoClave; }
			set { _ConoClave = value; }
		}
#endregion

		public Recurso()
		{
			//Constructor de la clase Recurso.
		}

		public SqlDataReader ObtenerUsuario( string IDRED )
		{
            //SqlDataReader dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "FIC_RECURSORED", IDRED);
             return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "CR2_LOGIN", IDRED);
		}
		public SqlDataReader ObtenerRecurso( string IDCIP )
		{
            return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, "CR2_PROFESIONAL", IDCIP);
		}
	}
}
