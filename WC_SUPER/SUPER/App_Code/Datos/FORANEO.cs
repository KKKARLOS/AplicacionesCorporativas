using System;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// FORANEOS son profesionales en T001_FICEPI cuyo tipo de recurso es F
    /// y solo serán visibles desde la aplicación SUPER
    /// </summary>
    public class FORANEO
    {
        //public FORANEO()
        //{
        //    //
        //    // TODO: Agregar aquí la lógica del constructor
        //    //
        //}

        public static int Insertar(SqlTransaction tr, string sCIP, string sApe1, string sApe2, string sNombre, string sAlias,
                                   string sSexo, string sEmail, int idCalendario, DateTime dtAlta)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            int i = 0;

            aParam[i++] = ParametroSql.add("@t001_cip", SqlDbType.Text, 15, sCIP.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.Text, 25, sApe1.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.Text, 25, sApe2.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.Text, 20, sNombre.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_alias", SqlDbType.Text, 30, sAlias.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_fecalta", SqlDbType.SmallDateTime, 10, dtAlta);
            aParam[i++] = ParametroSql.add("@t001_sexo", SqlDbType.Char, 1, sSexo);
            aParam[i++] = ParametroSql.add("@t001_email", SqlDbType.Text, 50, sEmail);
            aParam[i++] = ParametroSql.add("@t066_idcal", SqlDbType.Int, 4, idCalendario);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FORANEO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FORANEO_INS", aParam));
        }
        public static void Updatear(SqlTransaction tr, int t001_idficepi, string sCIP, string sApe1, string sApe2, string sNombre,
                                    string sAlias, DateTime dtAlta, Nullable<DateTime> dtBaja, string sSexo,
                                    string sEmail, string sPassw, int idCalendario)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            int i = 0;

            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_cip", SqlDbType.Text, 12, sCIP.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.Text, 25, sApe1.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.Text, 25, sApe2.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.Text, 20, sNombre.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_alias", SqlDbType.Text, 30, sAlias.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_fecalta", SqlDbType.SmallDateTime, 10, dtAlta);
            aParam[i++] = ParametroSql.add("@t001_fecbaja", SqlDbType.SmallDateTime, 10, dtBaja);
            aParam[i++] = ParametroSql.add("@t001_cip", SqlDbType.Char, 1, sSexo);
            aParam[i++] = ParametroSql.add("@t001_email", SqlDbType.Text, 50, sEmail);
            aParam[i++] = ParametroSql.add("@t001_passw", SqlDbType.Text, 12, sPassw);
            aParam[i++] = ParametroSql.add("@t066_idcal", SqlDbType.Int, 4, idCalendario);

            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_FORANEO_UPD", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FORANEO_UPD", aParam);
        }
        public static int ModificarUsuario(SqlTransaction tr,
                                    int t314_idusuario,
                                    string t314_alias,
                                    DateTime t314_falta,
                                    Nullable<DateTime> t314_fbaja,
                                    bool t314_controlhuecos,
                                    bool t314_mailiap,
                                    bool t314_accesohabilitado,
                                    decimal t314_costehora,
                                    decimal t314_costejornada,
                                    bool t314_calculoJA,
                                    string t422_idmoneda
            )
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t314_alias", SqlDbType.VarChar, 30, t314_alias.ToUpper()),
                ParametroSql.add("@t314_falta", SqlDbType.SmallDateTime, 10, t314_falta),
                ParametroSql.add("@t314_fbaja", SqlDbType.SmallDateTime, 10, t314_fbaja),
                ParametroSql.add("@t314_controlhuecos", SqlDbType.Bit, 1, t314_controlhuecos),
                ParametroSql.add("@t314_mailiap", SqlDbType.Bit, 1, t314_mailiap),
                ParametroSql.add("@t314_accesohabilitado", SqlDbType.Bit, 1, t314_accesohabilitado),
                ParametroSql.add("@t314_costehora", SqlDbType.Money, 8, t314_costehora),
                ParametroSql.add("@t314_costejornada", SqlDbType.Money, 8, t314_costejornada),
                ParametroSql.add("@t314_calculoJA", SqlDbType.Bit, 1, t314_calculoJA),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIO_UPD_F", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIO_UPD_F", aParam);
        }
        /// <summary>
        /// Permite poner de baja a un profesional FICEPI o revivirlo (quiténdole la fecha de baja)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="dtBaja"></param>
        /// 
        public static void ModificarBaja(SqlTransaction tr, int t001_idficepi, Nullable<DateTime> dtBaja)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;

            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_fecbaja", SqlDbType.SmallDateTime, 10, dtBaja);

            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_FORANEO_BAJA_U", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FORANEO_BAJA_U", aParam);
        }

        /// <summary>
        /// Busca un profesional en FICEPI por NIF y devuelve sus datos
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_cip"></param>
        /// <returns></returns>
        public static SqlDataReader GetByNif(SqlTransaction tr, string t001_cip)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_cip", SqlDbType.Text, 15, t001_cip.ToUpper());

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FORANEO_NIF_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FORANEO_NIF_S", aParam);
        }
        /// <summary>
        /// Busca u  profesional en FICEPI por nombre y devuelve sus datos
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_apellido1"></param>
        /// <param name="t001_apellido2"></param>
        /// <param name="t001_nombre"></param>
        /// <returns></returns>
        public static SqlDataReader GetByNombre(SqlTransaction tr, string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.Text, 25, t001_apellido1.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.Text, 25, t001_apellido2.ToUpper());
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.Text, 20, t001_nombre.ToUpper());

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FORANEO_NOMBRE_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FORANEO_NOMBRE_S", aParam);
        }
        /// <summary>
        /// Dado un idFicepi obtiene el código de usuario de alta que le corresponde
        /// Si hay más de uno coge el IdUsuario más alto
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static int GetUsuario(SqlTransaction tr, int t001_idficepi)
        {
            int iRes = -1;
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_FORANEO_FIC_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FORANEO_FIC_S", aParam);
            if (dr.Read())
            {
                iRes = int.Parse(dr["t314_idusuario"].ToString());
            }
            dr.Close();
            dr.Dispose();
            return iRes;
        }

        public static SqlDataReader GetUsuariosSuper(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FORANEO_USER_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FORANEO_USER_C", aParam);
        }
        /// <summary>
        /// Dado un usuario y password busca si existe como foráneo
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="sUser"></param>
        /// <param name="sPasww"></param>
        /// <returns></returns>
        public static SqlDataReader Validar(SqlTransaction tr, string sUser, string sPassw)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_cip", SqlDbType.Text, 15, sUser.ToUpper());
            aParam[i++] = ParametroSql.add("@t080_passw", SqlDbType.Text, 50, sPassw);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FORANEO_VALIDAR", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FORANEO_VALIDAR", aParam);
        }

        #region CONSULTAS FORANEOS
        public static SqlDataReader CatalogoConsulta(SqlTransaction tr, string apellido1, string apellido2, string nombre, 
                                                     Nullable<int> promotor, int verBloqueados)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_apellido1", SqlDbType.VarChar, 25, apellido1);
            aParam[i++] = ParametroSql.add("@t001_apellido2", SqlDbType.VarChar, 25, apellido2);
            aParam[i++] = ParametroSql.add("@t001_nombre", SqlDbType.VarChar, 20, nombre);
            aParam[i++] = ParametroSql.add("@t001_idficepiProm", SqlDbType.Int, 4, promotor);
            aParam[i++] = ParametroSql.add("@verBloqueados", SqlDbType.Int, 4, verBloqueados);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FORANEOCONSULTA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FORANEOCONSULTA_CAT", aParam);
        }

        public static SqlDataReader ConsultaSelect(SqlTransaction tr, int idFicepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, idFicepi);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FORANEOCONSULTA_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FORANEOCONSULTA_SEL", aParam);
        }       

        #endregion
    }
}
