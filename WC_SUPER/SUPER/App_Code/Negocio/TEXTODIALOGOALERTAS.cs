using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public class TEXTODIALOGOALERTAS
    {
        #region Propiedades y Atributos

		private int _t832_idtextodialogoalertas;
		public int t832_idtextodialogoalertas
		{
			get {return _t832_idtextodialogoalertas;}
			set { _t832_idtextodialogoalertas = value ;}
		}

		private int _t831_iddialogoalerta;
		public int t831_iddialogoalerta
		{
			get {return _t831_iddialogoalerta;}
			set { _t831_iddialogoalerta = value ;}
		}

		private int? _t314_idusuario_redactor;
		public int? t314_idusuario_redactor
		{
			get {return _t314_idusuario_redactor;}
			set { _t314_idusuario_redactor = value ;}
		}

		private string _t832_texto;
		public string t832_texto
		{
			get {return _t832_texto;}
			set { _t832_texto = value ;}
		}

		private string _t832_posicion;
		public string t832_posicion
		{
			get {return _t832_posicion;}
			set { _t832_posicion = value ;}
		}

		private DateTime _t832_fechacreacion;
		public DateTime t832_fechacreacion
		{
			get {return _t832_fechacreacion;}
			set { _t832_fechacreacion = value ;}
		}

		private DateTime? _t832_fechaleido;
		public DateTime? t832_fechaleido
		{
			get {return _t832_fechaleido;}
			set { _t832_fechaleido = value ;}
		}
		#endregion

		#region Constructor

		public TEXTODIALOGOALERTAS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static void InsertarMensaje(SqlTransaction tr,
                    int t831_iddialogoalerta, 
                    int t314_idusuario_redactor, 
                    string t832_texto, 
                    string t832_posicion,
                    bool bUsuarioResponsableProy)
        {

            SUPER.Capa_Datos.TEXTODIALOGOALERTAS.Insertar(tr, t831_iddialogoalerta, t314_idusuario_redactor, t832_texto, t832_posicion, bUsuarioResponsableProy);
        }

        #endregion
    }
}