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
    /// Class	 : INTERFACTSAP
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: INTERFACTSAP
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	24/09/2008 9:18:09	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class INTERFACTSAP
    {
        #region Metodos

        public static DataSet EmpresasProyectos()
        {
            return SqlHelper.ExecuteDataset("SUP_SAP_EMPyPROY");
        }
        public static void Borrar(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_INTERFACTSAP_D_ALL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_INTERFACTSAP_D_ALL", aParam);
        }
        public static int numFacturas(SqlTransaction tr)
        {
            //return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_INTERFACTSAP_Count"));
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_INTERFACTSAP_Count", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_INTERFACTSAP_Count", aParam));
        }
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_INTERFACTSAP_C1", aParam);
        }

        #endregion
    }
    public class FactSAP
    {
        #region Propiedades y Atributos

        private string _cod_empresa;
        public string cod_empresa
        {
            get { return _cod_empresa; }
            set { _cod_empresa = value; }
        }
        private int _iCodEmpresa;
        public int iCodEmpresa
        {
            get { return _iCodEmpresa; }
            set { _iCodEmpresa = value; }
        }

        private string _cod_cliente;
        public string cod_cliente
        {
            get { return _cod_cliente; }
            set { _cod_cliente = value; }
        }
        private int _iCodCliente;
        public int iCodCliente
        {
            get { return _iCodCliente; }
            set { _iCodCliente = value; }
        }

        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private string _num_proyecto;
        public string num_proyecto
        {
            get { return _num_proyecto; }
            set { _num_proyecto = value; }
        }
        private int _iNumProyecto;
        public int iNumProyecto
        {
            get { return _iNumProyecto; }
            set { _iNumProyecto = value; }
        }

        private bool _grupo;
        public bool grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }

        private string _serie;
        public string serie
        {
            get { return _serie; }
            set { _serie = value; }
        }

        private string _numero;
        public string numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        private int _iNumero;
        public int iNumero
        {
            get { return _iNumero; }
            set { _iNumero = value; }
        }

        private string _fec_fact;
        public string fec_fact
        {
            get { return _fec_fact; }
            set { _fec_fact = value; }
        }
        private DateTime _dtFecFact;
        public DateTime dtFecFact
        {
            get { return _dtFecFact; }
            set { _dtFecFact = value; }
        }

        private string _imp_fact;
        public string imp_fact
        {
            get { return _imp_fact; }
            set { _imp_fact = value; }
        }
        private decimal _dImpFfact;
        public decimal dImpFfact
        {
            get { return _dImpFfact; }
            set { _dImpFfact = value; }
        }

        private string _moneda;
        public string moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }

        private string _descri;
        public string descri
        {
            get { return _descri; }
            set { _descri = value; }
        }

        public string refCliente { get; set; }
        #endregion

        #region Constructor

        public FactSAP()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos
        public static FactSAP getFactura(string sLinea)
        {
            FactSAP oFact = new FactSAP();
            //sLinea = Utilidades.unescape(sLinea);
            try
            {
                oFact.cod_empresa = sLinea.Substring(0, 4).Trim();
                oFact.cod_cliente = sLinea.Substring(4, 10).Trim();
                oFact.num_proyecto = sLinea.Substring(14, 8).Trim();
                oFact.serie = sLinea.Substring(22, 5).Trim();
                oFact.numero = sLinea.Substring(27, 5).Trim();
                oFact.fec_fact = sLinea.Substring(32, 8).Trim();
                //oFact.imp_fact = sLinea.Substring(40, 15).Trim();
                oFact.imp_fact = sLinea.Substring(40, 13).Trim() + "," + sLinea.Substring(53, 2).Trim();
                oFact.moneda = sLinea.Substring(55, 3).Trim();
                //oFact.descri = sLinea.Substring(58, sLinea.Length - 61).Trim();
                oFact.descri = sLinea.Substring(58, 40).Trim();
                if (sLinea.Length > 102)
                    oFact.refCliente = sLinea.Substring(103, sLinea.Length - 103).Trim();
                else
                    oFact.refCliente = "";
                oFact.iCodEmpresa = 0;
                oFact.iCodCliente = 0;
                oFact.iNumProyecto = 0;
                oFact.grupo = false;
            }
            catch (Exception e)
            {
                Errores.mostrarError("La línea de factura no tiene un formato correcto", e);
            }
            return oFact;
        }
        #endregion
    }

    public class EmpFactSAP
    {
        #region Propiedades y Atributos

        public int t313_idempresa;
        public string t313_denominacion;
        public string t302_codigoexterno;
        //public bool t313_ute;

        #endregion

        #region Constructor

        public EmpFactSAP(int idempresa, string denominacion, string codigoexterno)//, bool ute
        {
            this.t313_idempresa = idempresa;
            this.t313_denominacion = denominacion;
            this.t302_codigoexterno = codigoexterno;
            //this.t313_ute = ute;
        }

        #endregion
    }
    public class ProyFactSAP
    {
        #region Propiedades y Atributos
        public int t301_idproyecto;
        public int t305_idproyectosubnodo;
        #endregion

        #region Constructor

        public ProyFactSAP(int idproyecto, int idproyectosubnodo)
        {
            this.t301_idproyecto = idproyecto;
            this.t305_idproyectosubnodo = idproyectosubnodo;
        }

        #endregion
    }
    public class CliFactSAP
    {
        #region Propiedades y Atributos
        public int t302_idcliente;
        public string t302_codigoexterno;
        public bool t302_interno;
        #endregion

        #region Constructor

        public CliFactSAP(int idcliente, string codigoexterno, bool interno)
        {
            this.t302_idcliente = idcliente;
            this.t302_codigoexterno = codigoexterno;
            this.t302_interno = interno;
        }

        #endregion
    }
}
