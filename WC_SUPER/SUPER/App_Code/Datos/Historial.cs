using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de Historial
    /// </summary>
    public class Historial
    {
        #region Propiedades y Atributos

        private string _IdEstado;
        public string IdEstado
        {
            get { return _IdEstado; }
            set { _IdEstado = value; }
        }

        private DateTime _Fecha;
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _Autor;
        public string Autor
        {
            get { return _Autor; }
            set { _Autor = value; }
        }

        private string _Texto;
        public string Texto
        {
            get { return _Texto; }
            set { _Texto = value; }
        }


        #endregion

        public Historial()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader ObtenerHistorial(SqlTransaction tr, string sTabla, int idKey, Nullable<int> idKey2)
        {
            SqlParameter[] aParam;

            if (sTabla == "ASICRONO" || sTabla == "T595_FICEPIEXAMENCRONO")
                aParam = new SqlParameter[2];
            else
                aParam = new SqlParameter[1];
            int i = 0;
            string sProcAlm = "";
            aParam[i++] = ParametroSql.add("@idKey", SqlDbType.Int, 4, idKey);

            switch (sTabla)
            {
                case "T838_EXPFICEPIPERFILCRONO":
                    sProcAlm = "SUP_CVT_EXPFICEPIPERFILCRONO_C2";
                    break;
                case "TITULOFICEPICRONO":
                    sProcAlm = "SUP_CVT_TITULOFICEPICRONO_CAT";
                    break;
                case "ASICRONO":
                    sProcAlm = "SUP_CVT_ASICRONO_CAT";
                    aParam[i++] = ParametroSql.add("@idKey2", SqlDbType.Int, 4, idKey2);
                    break;
                case "TITIDIOMAFICCRONO":
                    sProcAlm = "SUP_CVT_TITIDIOMAFICCRONO_CAT";
                    break;
                case "T595_FICEPIEXAMENCRONO":
                    sProcAlm = "SUP_CVT_CVEXAMENCRONO_CAT";
                    aParam[i++] = ParametroSql.add("@idKey2", SqlDbType.Int, 4, idKey2);
                    break;
                case "CURMONITORCRONO":
                    sProcAlm = "SUP_CVT_CURMONITORCRONO_CAT";
                    break;
            }

            if (sProcAlm != "")
            {
                if (tr == null)
                    return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sProcAlm, aParam);
            }
            else
                throw (new NullReferenceException("La tabla " + sTabla + "no tiene cronología asociada."));
        }
        /// <summary>
        /// Devuelve el último paso registrado en un historial
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="sTabla"></param>
        /// <param name="idKey"></param>
        /// <param name="idKey2"></param>
        /// <returns></returns>
        public static SqlDataReader GetUltimoPaso(SqlTransaction tr, string sTabla, int idKey, Nullable<int> idKey2)
        {
            SqlParameter[] aParam;

            if (sTabla == "ASICRONO" || sTabla == "T595_FICEPIEXAMENCRONO" || sTabla == "CERTIFICADO")
                aParam = new SqlParameter[2];
            else
                aParam = new SqlParameter[1];
            int i = 0;
            string sProcAlm = "";
            aParam[i++] = ParametroSql.add("@idKey", SqlDbType.Int, 4, idKey);

            switch (sTabla)
            {
                case "T838_EXPFICEPIPERFILCRONO"://Perfil en la experiencia profesional
                    sProcAlm = "SUP_CVT_EXPFICEPIPERFILCRONO_ULT_SEL";
                    break;
                case "TITULOFICEPICRONO"://Título académico
                    sProcAlm = "SUP_CVT_TITULOFICEPICRONO_ULT_SEL";
                    break;
                case "ASICRONO"://Cursos asistidos
                    sProcAlm = "SUP_CVT_ASICRONO_ULT_SEL";
                    aParam[i++] = ParametroSql.add("@idKey2", SqlDbType.Int, 4, idKey2);
                    break;
                case "CURMONITORCRONO"://Cursos impartidos
                    sProcAlm = "SUP_CVT_CURMONITORCRONO_ULT_SEL";
                    break;
                case "TITIDIOMAFICCRONO"://Título de idioma
                    sProcAlm = "SUP_CVT_TITIDIOMAFICCRONO_ULT_SEL";
                    break;
                case "T595_FICEPIEXAMENCRONO"://Examen
                    sProcAlm = "SUP_CVT_CVEXAMENCRONO_ULT_SEL";
                    aParam[i++] = ParametroSql.add("@idKey2", SqlDbType.Int, 4, idKey2);
                    break;
                case "CERTIFICADO"://Certificado
                    sProcAlm = "SUP_CVT_CERTIFICADO_ULT_SEL";
                    aParam[i++] = ParametroSql.add("@idKey2", SqlDbType.Int, 4, idKey2);
                    break;
            }

            if (sProcAlm != "")
            {
                if (tr == null)
                    return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
                else
                    return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sProcAlm, aParam);
            }
            else
                throw (new NullReferenceException("La tabla " + sTabla + "no tiene cronología asociada."));
        }
        /// <summary>
        /// Si el último registro del historial es un elemento Pdte de validar, devuelve sus valores,
        /// sino devuelve IdEstado="-1"
        /// </summary>
        /// <param name="sTabla"></param>
        /// <param name="idKey"></param>
        /// <param name="idKey2"></param>
        /// <returns></returns>
        public static Historial GetPdteValidar(SqlTransaction tr, string sTabla, int idKey, Nullable<int> idKey2)
        {
            Historial miHist = new Historial();
            miHist.IdEstado = "-1";
            miHist.Texto = "";
            SqlDataReader dr = SUPER.DAL.Historial.GetUltimoPaso(null, sTabla, idKey, idKey2);
            if (dr.Read())
            {
                if (sTabla == "CERTIFICADO")
                {//Para los certificados no hay tabla con cronología
                    //if (dr["t839_idestado"].ToString() != "V")
                    //{
                        miHist.IdEstado = dr["t839_idestado"].ToString();
                        miHist.Autor = "";
                        miHist.Texto = dr["Motivo"].ToString();
                    //}
                }
                else
                {
                    if (dr["t839_idestado"].ToString() == "S" || dr["t839_idestado"].ToString() == "T")
                    {
                        miHist.IdEstado = dr["t839_idestado"].ToString();
                        miHist.Fecha = DateTime.Parse(dr["Fecha"].ToString());
                        miHist.Autor = dr["Profesional"].ToString();
                        miHist.Texto = dr["Motivo"].ToString();
                    }
                }
            }
            dr.Close();
            dr.Dispose();

            return miHist;
        }
    }
}