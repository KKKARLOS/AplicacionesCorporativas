using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Actividades.
	/// </summary>
    public class Areas
	{
		public Areas()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
        public static SqlDataReader Catalogo(int intColumna, int intOrden, int intIDFICEPI, string strAdmin)
		{
			string strCaso="S";
			if (strAdmin=="A") strCaso="A";

            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(), 
				"GESTAR_AREA", strCaso,0,"",0,"",intIDFICEPI,0,0,0,0,intColumna, intOrden);

            return drArea;
		}
        public static SqlDataReader DeficienciasArea(int intIDArea)
        {
            SqlDataReader drDeficiencias;

            //if (strFechaInicio == "") strFechaInicio = "01-01-1900";
            //if (strFechaFin=="") strFechaFin= "31-12-2099";

            drDeficiencias = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                    "GESTAR_DEFICI_AREA",intIDArea);
            return drDeficiencias;
        }
        public static SqlDataReader Listado(int intIDFICEPI, string strAdmin)
        {
            string strCaso = "Y";
            if (strAdmin == "A") strCaso = "Z";

            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_AREA", strCaso, 0, "", 0, "", intIDFICEPI, 0, 0, 0, 0, 0);

            return drArea;
        }
        public static SqlDataReader Lista(int intIDFICEPI, string strAdmin)
        {
            string strCaso = "L";
            if (strAdmin == "A") strCaso = "Z";

            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_AREA", strCaso, 0, "", 0, "", intIDFICEPI, 0, 0, 0, 0, 0);

            return drArea;
        }
        public static SqlDataReader ListaEspe(int intIDFICEPI, string strAdmin)
        {
            string strCaso = "F";
            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_AREA", strCaso, 0, "", 0, "", intIDFICEPI, 0, 0, 0, 0, 0);

            return drArea;
        }
        public static SqlDataReader LeerUnRegistro(int intIdContador)
		{
            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(), 
				"GESTAR_AREA", "O", intIdContador);

            return drArea;
		}
        public static void Insertar(SqlTransaction transaccion, int intIdArea, int intIDFICEPI, string strFigura)
        {
            SqlHelper.ExecuteNonQueryTransaccion(transaccion, "GESTAR_INTEGRANTE", "I", intIdArea, intIDFICEPI, strFigura);
        }
        public static int Actualizar(SqlTransaction tr, string strOperacion, int intIdContador, string strNombre, byte byteCorreo, string strDescripcion, int intIDFICEPI, byte byteCategoria, byte byteEstado, byte byteSelCoord, byte byteResuelta, bool bPermitirCambios, bool bAutoaprobacion)
		{
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@CASO", SqlDbType.VarChar, 1);
            if (strOperacion == "I")
                aParam[0].Value ="I";
            else
                aParam[0].Value = "U";

            aParam[1] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[1].Value = intIdContador;
            aParam[2] = new SqlParameter("@T042_DENOMINACION", SqlDbType.VarChar, 50);
            aParam[2].Value = strNombre;
            aParam[3] = new SqlParameter("@T042_CORREO", SqlDbType.Bit, 1);
            aParam[3].Value = byteCorreo;
            aParam[4] = new SqlParameter("@T042_DESCRIPCION", SqlDbType.Text, 0);
            aParam[4].Value = strDescripcion;
            aParam[5] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
            aParam[5].Value = intIDFICEPI;
            aParam[6] = new SqlParameter("@T042_CATEGORIA", SqlDbType.Bit, 1);
            aParam[6].Value = byteCategoria;
            aParam[7] = new SqlParameter("@T042_ESTADO", SqlDbType.Bit, 1);
            aParam[7].Value = byteEstado;
            aParam[8] = new SqlParameter("@T042_SELCOORDI", SqlDbType.Bit, 1);
            aParam[8].Value = byteSelCoord;
            aParam[9] = new SqlParameter("@T042_RESUELTA", SqlDbType.Bit, 1);
            aParam[9].Value = byteResuelta;
            aParam[10] = new SqlParameter("@bPermitirCambios", SqlDbType.Bit, 1);
            aParam[10].Value = bPermitirCambios;
            aParam[11] = new SqlParameter("@bAutoaprobacion", SqlDbType.Bit, 1);
            aParam[11].Value = bAutoaprobacion;
            

            int returnValue;
            object objValue;
            if (tr == null)
                objValue = SqlHelper.ExecuteScalar("GESTAR_AREA", aParam);
            else
                objValue = SqlHelper.ExecuteScalarTransaccion(tr, "GESTAR_AREA", aParam);

            returnValue = int.Parse(objValue.ToString());
            return returnValue;

		}
        public static string Eliminar(int intIdContador)
		{
			SqlCommand sqlCmd = new SqlCommand();
			SqlHelper.AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, null);
			SqlHelper.AddParamToSQLCmd(sqlCmd, "@CASO", SqlDbType.VarChar, 1, ParameterDirection.Input, "D");
			SqlHelper.AddParamToSQLCmd(sqlCmd, "@T042_IDAREA"  , SqlDbType.Int, 4, ParameterDirection.Input, intIdContador);

			SqlHelper.SetCommandType(sqlCmd,CommandType.StoredProcedure,"GESTAR_AREA");
			string mensaje = (string)SqlHelper.ExecuteScalarCmd(sqlCmd);
			if (((int)sqlCmd.Parameters["@ReturnValue"].Value)==0) 
				return "OK";
			else
				return mensaje;
		}

        public static int VerSiEsCoordinadorArea(int intIDArea, int intIDFICEPI)
        {
            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_AREA", "W", intIDArea, "", 0, "", intIDFICEPI);

            drArea.Read();
            int intRetorno = int.Parse(drArea[0].ToString());
            drArea.Close();
            drArea.Dispose();
            return intRetorno;
        }
        public static SqlDataReader SolicitantesArea(int intIDArea)
        {
            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_AREA", "H", intIDArea, "", 0, "", 0);
            return drArea;
        }
        public static SqlDataReader CoordinadoresArea(int intIDArea)
        {
            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_AREA", "R", intIDArea, "", 0, "", 0);
            return drArea;
        }
        public static SqlDataReader PromotorResponsableCoordinador(int intIDArea)
		{
            SqlDataReader drArea = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_AREA", "C", intIDArea);
			return drArea;
		}
        public static SqlDataReader LeerTecnicosArea(Nullable<int> intIdArea)
        {
            SqlParameter[] aParam = new SqlParameter[4];

            aParam[0] = new SqlParameter("@CASO", SqlDbType.Char, 1);
            aParam[0].Value = "A";
            aParam[1] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[1].Value = intIdArea;
            aParam[2] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[2].Value = null;
            aParam[3] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
            aParam[3].Value = null;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_TECNICO", aParam);

            return dr;
        }

        public static SqlDataReader ObtenerOrdenesParaCambios(int t042_idarea)
		{
            return SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_GETORDEN_CAMBIOFECHA", t042_idarea);
		}

        public static void setDatoCambio(string sTipo, int nOpcion, int nIDElemento, string sValor)
        {
            SqlHelper.ExecuteNonQuery(Utilidades.Conexion(), "GESTAR_SETDATO_CAMBIOFECHA", sTipo, nOpcion, nIDElemento, sValor);
        }

	}
}
