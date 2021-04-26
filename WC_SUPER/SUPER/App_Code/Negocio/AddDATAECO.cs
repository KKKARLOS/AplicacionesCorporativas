using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para HttpUtility.HtmlEncode
//using System.Web;
//Para el RegEx
using System.Text;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : AddDATAECO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: AddDATAECO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	24/09/2008 9:18:09	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class AddDATAECO
    {
        #region Metodos

        public static DataSet ValidarFichero()
        {
            return SqlHelper.ExecuteDataset("SUP_CARGAFICHERO_VALIDAR");
        }
        public static DataSet ValidarTabla()
        {
            return SqlHelper.ExecuteDataset("SUP_CARGATABLA_VALIDAR");
        }
        public static int numFilas(SqlTransaction tr)
        {
            //return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_INTERFACTSAP_Count"));
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DATOECOTABLA_Count", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DATOECOTABLA_Count", aParam));
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_DATOECOTABLA", aParam);
        }

        #endregion
    }
    public class DesdeFichero
    {
 		#region Propiedades y Atributos
        private string _idnodo;
        public string idnodo
        {
            get { return _idnodo; }
            set { _idnodo = value; }
        }

        private int _t303_idnodo;
        public int t303_idnodo
        {
            get { return _t303_idnodo; }
            set { _t303_idnodo = value; }
        }

        private string _idproyecto;
        public string idproyecto
        {
            get { return _idproyecto; }
            set { _idproyecto = value; }
        }

        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }
        
        private string _t305_cualidad;
        public string t305_cualidad
        {
            get { return _t305_cualidad; }
            set { _t305_cualidad = value; }
        }

        private string _annomes;
        public string annomes
        {
            get { return _annomes; }
            set { _annomes = value; }
        }

        private int _t325_annomes;
        public int t325_annomes
        {
            get { return _t325_annomes; }
            set { _t325_annomes = value; }
        }

        private string _idclaseeco;
        public string idclaseeco
        {
            get { return _idclaseeco; }
            set { _idclaseeco = value; }
        }

        private int _t329_idclaseeco;
        public int t329_idclaseeco
        {
            get { return _t329_idclaseeco; }
            set { _t329_idclaseeco = value; }
        }

        private string _t376_motivo;
        public string t376_motivo
        {
            get { return _t376_motivo; }
            set { _t376_motivo = value; }
        }

        private string _importe;
        public string importe
        {
            get { return _importe; }
            set { _importe = value; }
        }

        private decimal _t376_importe;
        public decimal t376_importe
        {
            get { return _t376_importe; }
            set { _t376_importe = value; }
        }

        private string _codigoexterno;
        public string codigoexterno
        {
            get { return _codigoexterno; }
            set { _codigoexterno = value; }
        }

        private int _t315_idproveedor;
        public int t315_idproveedor
        {
            get { return _t315_idproveedor; }
            set { _t315_idproveedor = value; }
        }

        private string _t329_necesidad;
        public string t329_necesidad
        {
            get { return _t329_necesidad; }
            set { _t329_necesidad = value; }
        }

        private bool _t329_visiblecarruselC;
        public bool t329_visiblecarruselC
        {
            get { return _t329_visiblecarruselC; }
            set { _t329_visiblecarruselC = value; }
        }

        private bool _t329_visiblecarruselJ;
        public bool t329_visiblecarruselJ
        {
            get { return _t329_visiblecarruselJ; }
            set { _t329_visiblecarruselJ = value; }
        }

        private bool _t329_visiblecarruselP;
        public bool t329_visiblecarruselP
        {
            get { return _t329_visiblecarruselP; }
            set { _t329_visiblecarruselP = value; }
        }

        private string _idnododestino;
        public string idnododestino
        {
            get { return _idnododestino; }
            set { _idnododestino = value; }
        }

        private int _t303_idnododestino;
        public int t303_idnododestino
        {
            get { return _t303_idnododestino; }
            set { _t303_idnododestino = value; }
        }
        private string _idProveedNodoDestino;
        public string idProveedNodoDestino
        {
            get { return _idProveedNodoDestino; }
            set { _idProveedNodoDestino = value; }
        }
        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }
		#endregion

		#region Constructor

        public DesdeFichero()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos
        public static DesdeFichero getFila(string sLinea)
        {
            DesdeFichero oFila = new DesdeFichero();
            try
            {
                if (sLinea != null)
                {
                    string[] aAtributo = Regex.Split(sLinea, @"\t");

                    oFila.idnodo = aAtributo[0];
                    oFila.idproyecto = aAtributo[1];
                    oFila.annomes = aAtributo[2];
                    oFila.idclaseeco = aAtributo[3];
                    oFila.t376_motivo = aAtributo[4];
                    oFila.idProveedNodoDestino = aAtributo[5];
                    oFila.importe = aAtributo[6];
                    oFila.t422_idmoneda = aAtributo[7].Trim();

                    //oFila.codigoexterno = aAtributo[6];
                    //oFila.idnododestino = aAtributo[7];

                    oFila.codigoexterno = "";
                    oFila.idnododestino = "";
                    oFila.t303_idnodo = 0;
                    oFila.t305_idproyectosubnodo = 0;
                    oFila.t301_idproyecto = 0;
                    oFila.t325_annomes = 0;
                    oFila.t329_idclaseeco = 0;
                    oFila.t376_importe = 0;
                    oFila.t329_necesidad = "";
                    oFila.t329_visiblecarruselC = true;
                    oFila.t329_visiblecarruselJ = true;
                    oFila.t329_visiblecarruselP = true;
                    oFila.t305_cualidad = "";
                }
            }
            catch (Exception e)
            {
                Errores.mostrarError("La línea del fichero de carga no tiene un formato correcto", e);
            }
            return oFila;
        }
		#endregion
   }

    public class Proveedor
    {
       #region Propiedades y Atributos

       public int t315_idproveedor;
       public string t315_codigoexterno;

       #endregion

       #region Constructor

       public Proveedor(int idproveedor, string codigoexterno)
       {
           this.t315_idproveedor = idproveedor;
           this.t315_codigoexterno = codigoexterno;
       }

       #endregion
    }
    public class ProyectoSubNodo
    {
       #region Propiedades y Atributos

       public int t301_idproyecto;
       public int t305_idproyectosubnodo;
       public int t303_idnodo;
       public string t305_cualidad;
       #endregion

       #region Constructor

       public ProyectoSubNodo(int t301_idproyecto, int idproyectosubnodo, int idnodo, string sCualidad)
       {
           this.t301_idproyecto = t301_idproyecto;
           this.t305_idproyectosubnodo = idproyectosubnodo;
           this.t303_idnodo = idnodo;
           this.t305_cualidad = sCualidad;
       }

       #endregion
    }
    public class ClaseEconomica
    {
       #region Propiedades y Atributos
       public int t329_idclaseeco;
       public string t329_necesidad;
       public bool t329_visiblecarruselC;
       public bool t329_visiblecarruselJ;
       public bool t329_visiblecarruselP;
       #endregion

       #region Constructor

       public ClaseEconomica(int idclaseeco, string necesidad, bool visiblecarruselC, bool visiblecarruselJ, bool visiblecarruselP)
       {
           this.t329_idclaseeco = idclaseeco;
           this.t329_necesidad = necesidad;
           this.t329_visiblecarruselC = visiblecarruselC;
           this.t329_visiblecarruselJ = visiblecarruselJ;
           this.t329_visiblecarruselP = visiblecarruselP;
       }

       #endregion
    }
    public class NodoDestino
    {
        #region Propiedades y Atributos
        public int t303_idnododestino;
        #endregion

        #region Constructor

        public NodoDestino(int t303_idnododestino)
        {
            this.t303_idnododestino = t303_idnododestino;
        }

        #endregion
    }
    public class Moneda
    {
        #region Propiedades y Atributos
        public string t422_idmoneda;
        #endregion

        #region Constructor

        public Moneda(string t422_idmoneda)
        {
            this.t422_idmoneda = t422_idmoneda;
        }

        #endregion
    }
}
