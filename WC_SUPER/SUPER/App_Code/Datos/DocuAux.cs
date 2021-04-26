using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;

/// <summary>
/// Descripción breve de DocuAux
/// </summary>

namespace SUPER.DAL
{
    public class DocuAux
    {
        #region Propiedades y Atributos

        private int _t686_iddocuaux;
        public int t686_iddocuaux
        {
            get { return _t686_iddocuaux; }
            set { _t686_iddocuaux = value; }
        }

        private string _t686_usuticks;
        public string t686_usuticks
        {
            get { return _t686_usuticks; }
            set { _t686_usuticks = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private int _t686_clave;
        public int t686_clave
        {
            get { return _t686_clave; }
            set { _t686_clave = value; }
        }

        private string _t686_tipo;
        public string t686_tipo
        {
            get { return _t686_tipo; }
            set { _t686_tipo = value; }
        }

        private string _t686_nombre;
        public string t686_nombre
        {
            get { return _t686_nombre; }
            set { _t686_nombre = value; }
        }

        private DateTime _t686_fecha;
        public DateTime t686_fecha
        {
            get { return _t686_fecha; }
            set { _t686_fecha = value; }
        }

        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        private bool _t686_asignado;
        public bool t686_asignado
        {
            get { return _t686_asignado; }
            set { _t686_asignado = value; }
        }

        #endregion

        public DocuAux()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T686_DOCUAUX.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	22/12/2014 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t001_idficepi, string t686_tipo, string t686_nombre,
                                 string t686_usuticks, long t2_iddocumento, bool t686_asignado)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t686_tipo", SqlDbType.Char, 1, t686_tipo),
                ParametroSql.add("@t686_nombre", SqlDbType.VarChar, 250, t686_nombre),
                ParametroSql.add("@t686_usuticks", SqlDbType.VarChar, 50, t686_usuticks),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento),
                ParametroSql.add("@t686_asignado", SqlDbType.Bit, 1, t686_asignado)
            };
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUAUX_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUAUX_I", aParam));
        }
        public static DocuAux GetDocumento(SqlTransaction tr, string t686_usuticks)
        {
            DocuAux oDoc = new DocuAux();
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t686_usuticks", SqlDbType.VarChar, 50, t686_usuticks)
            };

            if (tr == null)
                dr= SqlHelper.ExecuteSqlDataReader("SUP_DOCUAUX_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUAUX_S", aParam);
            if (dr.Read())
            {
                oDoc.t2_iddocumento=long.Parse(dr["t2_iddocumento"].ToString());
                oDoc.t686_nombre = dr["t686_nombre"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return oDoc;
        }
        //public static long GetDocumento(SqlTransaction tr, string t686_tipo, string t686_usuticks)
        //{
        //    long t2_iddocumento = -1;
        //    SqlParameter[] aParam = new SqlParameter[]{  
        //        ParametroSql.add("@t686_tipo", SqlDbType.Char, 1, t686_tipo),
        //        ParametroSql.add("@t686_usuticks", SqlDbType.Int, 50, t686_usuticks),
        //    };
        //    if (tr == null)
        //        t2_iddocumento = Convert.ToInt64(SqlHelper.ExecuteScalar("SUP_DOCAUX_S", aParam));
        //    else
        //        t2_iddocumento = Convert.ToInt64(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCAUX_S", aParam));

        //    return t2_iddocumento;
        //}
        public static void BorrarDocumento(SqlTransaction tr, string t686_tipo, string t686_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t686_tipo", SqlDbType.Char, 1, t686_tipo),
                ParametroSql.add("@t686_usuticks", SqlDbType.VarChar, 50, t686_usuticks),
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_DOCUAUX_D", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUAUX_D", aParam);
        }
        ///Marca el documento como asignado para que al borrar el registro, el documento no desaparezca de Atenea a través del trigger
        ///sobre la T686_DOCUAUX
        public static void Asignar(SqlTransaction tr, long t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento),
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_DOCUAUX_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUAUX_U", aParam);
        }
    }
}
