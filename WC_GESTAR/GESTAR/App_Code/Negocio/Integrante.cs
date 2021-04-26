using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripci�n breve de integrante.
	/// </summary>
	public class Integrante
	{
        #region Propiedades y Atributos

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        #endregion

		public Integrante()
		{
			//
			// TODO: agregar aqu� la l�gica del constructor
			//
		}
        public Integrante(int intID)
        {
            this.ID = intID;
            //
            // TODO: agregar aqu� la l�gica del constructor
            //
        }

        public static SqlDataReader Catalogo(int intIdArea)
		{
			SqlDataReader drIntegrante = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_INTEGRANTE", "S", intIdArea);

			return drIntegrante;
		}

        public static void Insertar(SqlTransaction transaccion, int intIdArea, int intIDFICEPI, string strFigura)
		{
            SqlHelper.ExecuteNonQueryTransaccion(transaccion, "GESTAR_INTEGRANTE", "I", intIdArea, intIDFICEPI, strFigura);
		}

        public static void Eliminar(SqlTransaction transaccion, int intIdArea)
		{
            SqlHelper.ExecuteNonQueryTransaccion(transaccion, "GESTAR_INTEGRANTE", "D", intIdArea);
		}

	}
}
