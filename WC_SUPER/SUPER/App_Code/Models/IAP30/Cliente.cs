using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IB.SUPER.IAP30.Models
{
   
   public class Cliente
    {

        /// <summary>
        /// Summary description for Cliente
        /// </summary>
		#region Private Variables
		private String _tipo;
		private Int32 _t302_idcliente;
		private String _t302_denominacion;
		private Boolean _t302_estado;
		private Int32 _t302_idcliente_matriz;
		private String _t302_denominacion_matriz;
		private Boolean _t302_estado_matriz;
		private String _t302_codigoexterno;

		#endregion

		#region Public Properties
		public String tipo
		{
			get{return _tipo;}
			set{_tipo = value;}
		}

		public Int32 t302_idcliente
		{
			get{return _t302_idcliente;}
			set{_t302_idcliente = value;}
		}

		public String t302_denominacion
		{
			get{return _t302_denominacion;}
			set{_t302_denominacion = value;}
		}

		public Boolean t302_estado
		{
			get{return _t302_estado;}
			set{_t302_estado = value;}
		}

		public Int32 t302_idcliente_matriz
		{
			get{return _t302_idcliente_matriz;}
			set{_t302_idcliente_matriz = value;}
		}

		public String t302_denominacion_matriz
		{
			get{return _t302_denominacion_matriz;}
			set{_t302_denominacion_matriz = value;}
		}

		public Boolean t302_estado_matriz
		{
			get{return _t302_estado_matriz;}
			set{_t302_estado_matriz = value;}
		}

		public String t302_codigoexterno
		{
			get{return _t302_codigoexterno;}
			set{_t302_codigoexterno = value;}
		}


        #endregion

	}
}
