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
    /// Class	 : AddConsumo
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: AddConsumo
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	24/09/2008 9:18:09	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class AddConsumo
    {
        #region Metodos

        public static DataSet ValidarFichero()
        {
            return SqlHelper.ExecuteDataset("SUP_CARGAFICHEROIAP_VALIDAR");
        }
        #endregion
    }
    public class TAREA
    {
       #region Propiedades y Atributos

       public int t332_idtarea;
       public string t332_destarea;
       public int t331_idpt;
       public byte t332_estado; 
       public double t332_cle;
       public string t332_tipocle;
       public bool t332_impiap;
       public int t305_idproyectosubnodo;
       public DateTime? t332_fiv;
       public DateTime? t332_ffv;
       public bool t323_regjornocompleta;
       public bool t331_obligaest;
       public byte t331_estado;
       public bool t323_regfes;
       public string t301_estado;

       #endregion

       #region Constructor

       public TAREA(int idtarea, string destarea, int idpt, byte estado, double cle, string tipocle, bool impiap, int idproyectosubnodo, DateTime? dFIV, DateTime? dFFV, bool regjornocompleta, bool t331_obligaest, byte estado_pt, bool regfes, string estado_pe)
       {
           this.t332_idtarea = idtarea;
           this.t332_destarea = destarea;
           this.t331_idpt = idpt;
           this.t332_estado = estado;
           this.t332_cle = cle;
           this.t332_tipocle = tipocle;
           this.t332_impiap = impiap;
           this.t305_idproyectosubnodo = idproyectosubnodo;
           this.t332_fiv = dFIV;
           this.t332_ffv = dFFV;
           this.t323_regjornocompleta = regjornocompleta;
           this.t331_obligaest = t331_obligaest;
           this.t331_estado = estado_pt;
           this.t323_regfes = regfes;
           this.t301_estado = estado_pe;
       }

       #endregion
    }

    public class PROFESIONAL
    {
        #region Propiedades y Atributos

        public int t001_idficepi;
        public int t314_idusuario;
        public string Profesional;
        public int? t303_ultcierreIAP;
        public bool t314_jornadareducida;
        public int? t303_idnodo;
        public double t314_horasjor_red;
        public DateTime? t314_fdesde_red;
        public DateTime? t314_fhasta_red;
        public bool t314_controlhuecos;
        public DateTime? fUltImputacion;
        public int t066_idcal;
        public string t066_descal;
        public string SemanaLaboral;
        public string t001_codred;
        public DateTime? fAlta;
        public DateTime? fBaja;

        #endregion

        #region Constructor

        public PROFESIONAL(int idficepi, int idusuario, string sProfesional, int? ultcierreIAP, 
                        bool jornadareducida, int? idnodo, double horasjor_red,
                        DateTime? fdesde_red, DateTime? fhasta_red, bool controlhuecos,
                        DateTime? dfUltImputacion, int idcal, string descal, string sSemanaLaboral,
                        string codred, DateTime? dfAlta, DateTime? dfBaja
                        )
        {
            this.t001_idficepi = idficepi;
            this.t314_idusuario = idusuario;
            this.Profesional = sProfesional;
            this.t303_ultcierreIAP = ultcierreIAP;
            this.t314_jornadareducida = jornadareducida;
            this.t303_idnodo= idnodo;
            this.t314_horasjor_red = horasjor_red;
            this.t314_fdesde_red = fdesde_red;
            this.t314_fhasta_red = fhasta_red;
            this.t314_controlhuecos = controlhuecos;
            this.fUltImputacion = dfUltImputacion;
            this.t066_idcal = idcal;
            this.t066_descal = descal;
            this.SemanaLaboral = sSemanaLaboral;
            this.t001_codred = codred;
            this.fAlta = dfAlta;
            this.fBaja = dfBaja;
       }

       #endregion
    }
    public class DesdeFicheroIAP
    {
        #region Propiedades y Atributos
        // Id Usuario

        private string _idusuario;
        public string idusuario
        {
            get { return _idusuario; }
            set { _idusuario = value; }
        }
        private int _t314_idusuario;
        public int t314_idusuario
        {
            get { return _t314_idusuario; }
            set { _t314_idusuario = value; }
        }

        // fecha desde

        private string _fechaDesde;
        public string fechaDesde
        {
            get { return _fechaDesde; }
            set { _fechaDesde = value; }
        }
        private DateTime? _t337_fechaDesde;
        public DateTime? t337_fechaDesde
        {
            get { return _t337_fechaDesde; }
            set { _t337_fechaDesde = value; }
        }

        // fecha hasta

        private string _fechaHasta;
        public string fechaHasta
        {
            get { return _fechaHasta; }
            set { _fechaHasta = value; }
        }
        private DateTime? _t337_fechaHasta;
        public DateTime? t337_fechaHasta
        {
            get { return _t337_fechaHasta; }
            set { _t337_fechaHasta = value; }
        }

        // Id Tarea

        private string _idtarea;
        public string idtarea
        {
            get { return _idtarea; }
            set { _idtarea = value; }
        }
        private int _t332_idtarea;
        public int t332_idtarea
        {
            get { return _t332_idtarea; }
            set { _t332_idtarea = value; }
        }
        
        // Descripción de la tarea

        private string _t332_destarea;
        public string t332_destarea
        {
            get { return _t332_destarea; }
            set { _t332_destarea = value; }
        }

        // Profesional

        private string _Profesional;
        public string Profesional
        {
            get { return _Profesional; }
            set { _Profesional = value; }
        }

        // Id Esfuerzo

        private string _esfuerzo;
        public string esfuerzo
        {
            get { return _esfuerzo; }
            set { _esfuerzo = value; }
        }
        private double _t337_esfuerzo;
        public double t337_esfuerzo
        {
            get { return _t337_esfuerzo; }
            set { _t337_esfuerzo = value; }
        }

        // Comentario

        private string _t337_comentario;
        public string t337_comentario
        {
            get { return _t337_comentario; }
            set { _t337_comentario = value; }
        }

        // Festivo

        private string _festivos;
        public string festivos
        {
            get { return _festivos; }
            set { _festivos = value; }
        }
        private bool? _bfestivos;
        public bool? bfestivos
        {
            get { return _bfestivos; }
            set { _bfestivos = value; }
        }
        #endregion

		#region Constructor

        public DesdeFicheroIAP()
        {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.

            this.idtarea ="";
            this.idusuario = "";
            this.fechaDesde = "";
            this.esfuerzo = "";
            this.t337_comentario = "";
            this.festivos = "1";

            this.t314_idusuario = 0;
            this.t332_idtarea = 0;
            this.t337_esfuerzo = 0;
            this.t337_fechaDesde = null;
            this.t337_fechaHasta = null;
            this.bfestivos = true;
            this.t332_destarea = "";
            this.Profesional = "";
		}

		#endregion

		#region Metodos
        public static DesdeFicheroIAP getFila(string sLinea, string sEstruc)
        {
            DesdeFicheroIAP oFila = new DesdeFicheroIAP();

            if (sLinea != null)
            {
                string[] aAtributo = Regex.Split(sLinea, @"\t");

                if (sEstruc == "D")
                {
                    if (aAtributo.Length != 5)
                    {
                        throw (new Exception("La línea del fichero de carga diario no tiene un formato correcto. Se contabilizan " + aAtributo.Length.ToString() + " campos y el número que tiene que ser es de 5."));
                    }

                    oFila.idtarea = aAtributo[0];                        
                    oFila.idusuario = aAtributo[1];
                    oFila.fechaDesde = aAtributo[2];
                    oFila.esfuerzo = aAtributo[3];
                    oFila.t337_comentario = aAtributo[4];
                    oFila.festivos = "1";

                    oFila.t314_idusuario = 0;
                    oFila.t332_idtarea = 0;
                    oFila.t337_esfuerzo = 0;
                    oFila.t337_fechaDesde = null;
                    oFila.t337_fechaHasta = null;
                    oFila.bfestivos = true;
                    oFila.t332_destarea = "";
                    oFila.Profesional= "";
                }
                else
                {
                    if (aAtributo.Length != 7)
                    {
                        throw (new Exception("La línea del fichero de carga por rango de fecha no tiene un formato correcto. Se contabilizan " + aAtributo.Length.ToString() + " campos y el número que tiene que ser es de 7."));
                    }
                    oFila.idtarea = aAtributo[0];
                    oFila.idusuario = aAtributo[1];                     
                    oFila.fechaDesde = aAtributo[2];
                    oFila.fechaHasta = aAtributo[3];
                    oFila.esfuerzo = aAtributo[4];
                    oFila.festivos = aAtributo[5];
                    oFila.t337_comentario = aAtributo[6];

                    oFila.t314_idusuario = 0;
                    oFila.t332_idtarea = 0;
                    oFila.t337_esfuerzo = 0;
                    oFila.t337_fechaDesde = null;
                    oFila.t337_fechaHasta = null;
                    oFila.bfestivos = null;
                    oFila.t332_destarea = "";
                    oFila.Profesional = "";
                }
            }

            return oFila;
        }
		#endregion

    }

}
