using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace SUPER.Capa_Negocio
{
    public partial class PREFERENCIAUSUARIO
    {
        #region Metodos

        public static bool ExistePreferencia(SqlTransaction tr, Nullable<int> t462_idPrefUsuario, Nullable<int> t001_idficepi, 
                                             Nullable<short> t463_idpantalla)
        {
            bool bExistePreferencia = false;
            string sProcAlm = "";

            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t462_idPrefUsuario", SqlDbType.Int, 4, t462_idPrefUsuario);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla);

            switch (t463_idpantalla)
            {
                case 34: //sProcAlm = "SUP_PREFERENCIAUSUARIO_CIERRE_ADM"; break;
                case 3: //sProcAlm = "SUP_PREFERENCIAUSUARIO_AGT"; break;
                case 2: //sProcAlm = "SUP_PREFERENCIAUSUARIO_AGP"; break;
                case 1: sProcAlm = "SUP_PREFERENCIAUSUARIO_REP"; break;
                case 4: sProcAlm = "SUP_PREFERENCIAUSUARIO_GETPROY"; break;
                case 7: //sProcAlm = "SUP_PREFERENCIAUSUARIO_PROF"; break;
                case 8: //sProcAlm = "SUP_PREFERENCIAUSUARIO_PROY"; break;
                case 9: sProcAlm = "SUP_PREFERENCIAUSUARIO_CONS_REPOR"; break;
                case 10: sProcAlm = "SUP_PREFERENCIAUSUARIO_CONS_TAREA"; break;
                case 11: sProcAlm = "SUP_PREFERENCIAUSUARIO_INF_TAREA"; break;
                case 12: sProcAlm = "SUP_PREFERENCIAUSUARIO_DATOSRES"; break;
                case 13: sProcAlm = "SUP_PREFERENCIAUSUARIO_INFPROYASIG"; break;
                case 14: sProcAlm = "SUP_PREFERENCIAUSUARIO_FICHAECO"; break;
                case 15: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROYPROF"; break;
                case 16: sProcAlm = "SUP_PREFERENCIAUSUARIO_TRASPASOIAP"; break;
                case 17: sProcAlm = "SUP_PREFERENCIAUSUARIO_SEGRENTA"; break;
                case 18: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROYNOCERR"; break;
                case 19: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROYPROF_CONSPROVEEJORNOTROS"; break;
                case 20: sProcAlm = "SUP_PREFERENCIAUSUARIO_OBRA_CURSO"; break;
                case 21: sProcAlm = "SUP_PREFERENCIAUSUARIO_CUADRE_CONTRATOS"; break;
                case 22: sProcAlm = "SUP_PREFERENCIAUSUARIO_PRODUCCION"; break;
                case 23:
                case 24:
                case 25: sProcAlm = "SUP_PREFERENCIAUSUARIO_INF_DATOECO"; break;
                case 26: sProcAlm = "SUP_PREFERENCIAUSUARIO_PARTE_ACTIVIDAD"; break;
                case 27: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROY_MASIVO"; break;
                case 28: //Consuconta
                case 29: //SalProv
                    sProcAlm = "SUP_PREFERENCIAUSUARIO_CCSP"; break;
                case 30:
                case 31:
                    sProcAlm = "SUP_PREFERENCIAUSUARIO_CONSPROFGRAF"; break;
                case 32:
                    sProcAlm = "SUP_PREFERENCIAUSUARIO_FIGU_PROF"; break;
                case 33: sProcAlm = "SUP_PREFERENCIAUSUARIO_CUADRE_ECO"; break;
                case 35: sProcAlm = "SUP_PREFERENCIAUSUARIO_LINEABASEVG"; break;
            }
            if (sProcAlm != "")
            {
                SqlDataReader dr;
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sProcAlm, aParam);

                if (dr.Read())
                {
                    bExistePreferencia = true;
                }
                dr.Close();
                dr.Dispose();
            }
            return bExistePreferencia;
        }

        public static SqlDataReader Obtener(SqlTransaction tr, Nullable<int> t462_idPrefUsuario, Nullable<int> t001_idficepi, 
                                            Nullable<short> t463_idpantalla)
        {
            string sProcAlm = "";
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t462_idPrefUsuario", SqlDbType.Int, 4, t462_idPrefUsuario);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla);

            switch (t463_idpantalla)
            {
                case 34: //sProcAlm = "SUP_PREFERENCIAUSUARIO_CIERRE_ADM"; break;
                case 3: //sProcAlm = "SUP_PREFERENCIAUSUARIO_AGT"; break;
                case 2: //sProcAlm = "SUP_PREFERENCIAUSUARIO_AGP"; break;
                case 1: sProcAlm = "SUP_PREFERENCIAUSUARIO_REP"; break;
                case 4: sProcAlm = "SUP_PREFERENCIAUSUARIO_GETPROY"; break;
                case 7: //sProcAlm = "SUP_PREFERENCIAUSUARIO_PROF"; break;
                case 8: //sProcAlm = "SUP_PREFERENCIAUSUARIO_PROY"; break;
                case 9: sProcAlm = "SUP_PREFERENCIAUSUARIO_CONS_REPOR"; break;
                case 10: sProcAlm = "SUP_PREFERENCIAUSUARIO_CONS_TAREA"; break;
                case 11: sProcAlm = "SUP_PREFERENCIAUSUARIO_INF_TAREA"; break;
                case 12: sProcAlm = "SUP_PREFERENCIAUSUARIO_DATOSRES"; break;
                case 13: sProcAlm = "SUP_PREFERENCIAUSUARIO_INFPROYASIG"; break;
                case 14: sProcAlm = "SUP_PREFERENCIAUSUARIO_FICHAECO"; break;
                case 15: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROYPROF"; break;
                case 16: sProcAlm = "SUP_PREFERENCIAUSUARIO_TRASPASOIAP"; break;
                case 17: sProcAlm = "SUP_PREFERENCIAUSUARIO_SEGRENTA"; break;
                case 18: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROYNOCERR"; break;
                case 19: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROYPROF_CONSPROVEEJORNOTROS"; break;
                case 20: sProcAlm = "SUP_PREFERENCIAUSUARIO_OBRA_CURSO"; break;
                case 21: sProcAlm = "SUP_PREFERENCIAUSUARIO_CUADRE_CONTRATOS"; break;
                case 22: sProcAlm = "SUP_PREFERENCIAUSUARIO_PRODUCCION"; break;
                case 23:
                case 24:
                case 25: sProcAlm = "SUP_PREFERENCIAUSUARIO_INF_DATOECO"; break;
                case 26: sProcAlm = "SUP_PREFERENCIAUSUARIO_PARTE_ACTIVIDAD"; break;
                case 27: sProcAlm = "SUP_PREFERENCIAUSUARIO_PROY_MASIVO"; break;
                case 28: //Consuconta
                case 29: //SalProv
                    sProcAlm = "SUP_PREFERENCIAUSUARIO_CCSP"; break;
                case 30:
                case 31:
                    sProcAlm = "SUP_PREFERENCIAUSUARIO_CONSPROFGRAF"; break;
                case 32:
                    sProcAlm = "SUP_PREFERENCIAUSUARIO_FIGU_PROF"; break;
                case 33: sProcAlm = "SUP_PREFERENCIAUSUARIO_CUADRE_ECO"; break;
                case 35: sProcAlm = "SUP_PREFERENCIAUSUARIO_LINEABASEVG"; break;
                case 36:
                case 40:
                    sProcAlm = "SUP_CVT_PREFERENCIAUSUARIO_CONSULTAS_SIMPLE";
                    break;
                case 41:
                    sProcAlm = "SUP_CVT_PREFERENCIAUSUARIO_CONSULTAS_AVANZADA";
                    break;
                case 42:
                    sProcAlm = "SUP_CVT_PREFERENCIAUSUARIO_CONSULTAS_CADENA";
                    break;
                case 37: sProcAlm = "SUP_PREFERENCIAUSUARIO_CONS_DISPONIBILIDAD"; break;
                case 38: sProcAlm = "SUP_PREFERENCIAUSUARIO_SIB"; break;
                case 39: sProcAlm = "SUP_PREFERENCIAUSUARIO_AUDIECO"; break;//Auditoría de datos económicos
                case 43: sProcAlm = "SUP_PREFERENCIAUSUARIO_PRODUCCION_POR_PROFESIONAL"; break;
            }

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sProcAlm, aParam);
        }

        //public static int Insertar(SqlTransaction tr, int t314_idusuario, short t463_idpantalla, string t462_p1, string t462_p2, string t462_p3,
        //                           string t462_p4, string t462_p5, string t462_p6, string t462_p7, string t462_p8, string t462_p9, 
        //                            string t462_p10, string t462_p11, string t462_p12, string t462_p13, string t462_p14, string t462_p15)
        //{
        //    SqlParameter[] aParam = new SqlParameter[17];
        //    aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
        //    aParam[0].Value = t314_idusuario;
        //    aParam[1] = new SqlParameter("@t463_idpantalla", SqlDbType.SmallInt, 2);
        //    aParam[1].Value = t463_idpantalla;
        //    aParam[2] = new SqlParameter("@t462_p1", SqlDbType.Text, 15);
        //    aParam[2].Value = t462_p1;
        //    aParam[3] = new SqlParameter("@t462_p2", SqlDbType.Text, 15);
        //    aParam[3].Value = t462_p2;
        //    aParam[4] = new SqlParameter("@t462_p3", SqlDbType.Text, 15);
        //    aParam[4].Value = t462_p3;
        //    aParam[5] = new SqlParameter("@t462_p4", SqlDbType.Text, 15);
        //    aParam[5].Value = t462_p4;
        //    aParam[6] = new SqlParameter("@t462_p5", SqlDbType.Text, 15);
        //    aParam[6].Value = t462_p5;
        //    aParam[7] = new SqlParameter("@t462_p6", SqlDbType.Text, 15);
        //    aParam[7].Value = t462_p6;
        //    aParam[8] = new SqlParameter("@t462_p7", SqlDbType.Text, 15);
        //    aParam[8].Value = t462_p7;
        //    aParam[9] = new SqlParameter("@t462_p8", SqlDbType.Text, 15);
        //    aParam[9].Value = t462_p8;
        //    aParam[10] = new SqlParameter("@t462_p9", SqlDbType.Text, 15);
        //    aParam[10].Value = t462_p9;
        //    aParam[11] = new SqlParameter("@t462_p10", SqlDbType.Text, 15);
        //    aParam[11].Value = t462_p10;
        //    aParam[12] = new SqlParameter("@t462_p11", SqlDbType.Text, 15);
        //    aParam[12].Value = t462_p11;
        //    aParam[13] = new SqlParameter("@t462_p12", SqlDbType.Text, 15);
        //    aParam[13].Value = t462_p12;
        //    aParam[14] = new SqlParameter("@t462_p13", SqlDbType.Text, 15);
        //    aParam[14].Value = t462_p13;
        //    aParam[15] = new SqlParameter("@t462_p14", SqlDbType.Text, 15);
        //    aParam[15].Value = t462_p14;
        //    aParam[16] = new SqlParameter("@t462_p15", SqlDbType.Text, 15);
        //    aParam[16].Value = t462_p15;

        //    // Ejecuta la query y devuelve el valor del nuevo Identity.
        //    if (tr == null)
        //        return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PREFERENCIAUSUARIO_INS", aParam));
        //    else
        //        return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PREFERENCIAUSUARIO_INS", aParam));
        //}

        public static int Insertar(SqlTransaction tr, int t001_idficepi, short t463_idpantalla, string t462_p1, string t462_p2, string t462_p3,
                                    string t462_p4, string t462_p5, string t462_p6, string t462_p7, string t462_p8, string t462_p9, 
                                    string t462_p10, string t462_p11, string t462_p12, string t462_p13, string t462_p14, string t462_p15, 
                                    string t462_p16, string t462_p17, string t462_p18, string t462_p19, string t462_p20, string t462_p21)
        {
            SqlParameter[] aParam = new SqlParameter[23];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t463_idpantalla", SqlDbType.SmallInt, 2);
            aParam[1].Value = t463_idpantalla;
            aParam[2] = new SqlParameter("@t462_p1", SqlDbType.Text, 15);
            aParam[2].Value = t462_p1;
            aParam[3] = new SqlParameter("@t462_p2", SqlDbType.Text, 15);
            aParam[3].Value = t462_p2;
            aParam[4] = new SqlParameter("@t462_p3", SqlDbType.Text, 15);
            aParam[4].Value = t462_p3;
            aParam[5] = new SqlParameter("@t462_p4", SqlDbType.Text, 15);
            aParam[5].Value = t462_p4;
            aParam[6] = new SqlParameter("@t462_p5", SqlDbType.Text, 15);
            aParam[6].Value = t462_p5;
            aParam[7] = new SqlParameter("@t462_p6", SqlDbType.Text, 15);
            aParam[7].Value = t462_p6;
            aParam[8] = new SqlParameter("@t462_p7", SqlDbType.Text, 15);
            aParam[8].Value = t462_p7;
            aParam[9] = new SqlParameter("@t462_p8", SqlDbType.Text, 15);
            aParam[9].Value = t462_p8;
            aParam[10] = new SqlParameter("@t462_p9", SqlDbType.Text, 15);
            aParam[10].Value = t462_p9;
            aParam[11] = new SqlParameter("@t462_p10", SqlDbType.Text, 15);
            aParam[11].Value = t462_p10;
            aParam[12] = new SqlParameter("@t462_p11", SqlDbType.Text, 15);
            aParam[12].Value = t462_p11;
            aParam[13] = new SqlParameter("@t462_p12", SqlDbType.Text, 15);
            aParam[13].Value = t462_p12;
            aParam[14] = new SqlParameter("@t462_p13", SqlDbType.Text, 15);
            aParam[14].Value = t462_p13;
            aParam[15] = new SqlParameter("@t462_p14", SqlDbType.Text, 15);
            aParam[15].Value = t462_p14;
            aParam[16] = new SqlParameter("@t462_p15", SqlDbType.Text, 15);
            aParam[16].Value = t462_p15;
            aParam[17] = new SqlParameter("@t462_p16", SqlDbType.Text, 15);
            aParam[17].Value = t462_p16;
            aParam[18] = new SqlParameter("@t462_p17", SqlDbType.Text, 15);
            aParam[18].Value = t462_p17;
            aParam[19] = new SqlParameter("@t462_p18", SqlDbType.Text, 15);
            aParam[19].Value = t462_p18;
            aParam[20] = new SqlParameter("@t462_p19", SqlDbType.Text, 15);
            aParam[20].Value = t462_p19;
            aParam[21] = new SqlParameter("@t462_p20", SqlDbType.Text, 15);
            aParam[21].Value = t462_p20;
            aParam[22] = new SqlParameter("@t462_p21", SqlDbType.Text, 15);
            aParam[22].Value = t462_p21;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PREFERENCIAUSUARIO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PREFERENCIAUSUARIO_INS", aParam));
        }

        public static int InsertarCVT(SqlTransaction tr, int t001_idficepi, short t463_idpantalla, string t462_p1, string t462_p2, string t462_p3, 
                                 string t462_p4, string t462_p5, string t462_p6, string t462_p7, string t462_p8, string t462_p9, 
                                 string t462_p10, string t462_p11, string t462_p12, string t462_p13, string t462_p14, string t462_p15, 
                                 string t462_p16, string t462_p17, string t462_p18, string t462_p19, string t462_p20,
                                 string t462_p21, string t462_p22, string t462_p23, string t462_p24, string t462_p25, string t462_p26,
                                 string t462_p27, string t462_p28, string t462_p29, string t462_p30, string t462_p31, string t462_p32,
                                 string t462_p33, string t462_p34, string t462_p35, string t462_p36, string t462_p37, string t462_p38,
                                 string t462_p39
            )
        {
            SqlParameter[] aParam = new SqlParameter[41];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t463_idpantalla", SqlDbType.SmallInt, 2);
            aParam[1].Value = t463_idpantalla;
            aParam[2] = new SqlParameter("@t462_p1", SqlDbType.Text, 200);
            aParam[2].Value = t462_p1;
            aParam[3] = new SqlParameter("@t462_p2", SqlDbType.Text, 200);
            aParam[3].Value = t462_p2;
            aParam[4] = new SqlParameter("@t462_p3", SqlDbType.Text, 200);
            aParam[4].Value = t462_p3;
            aParam[5] = new SqlParameter("@t462_p4", SqlDbType.Text, 200);
            aParam[5].Value = t462_p4;
            aParam[6] = new SqlParameter("@t462_p5", SqlDbType.Text, 200);
            aParam[6].Value = t462_p5;
            aParam[7] = new SqlParameter("@t462_p6", SqlDbType.Text, 200);
            aParam[7].Value = t462_p6;
            aParam[8] = new SqlParameter("@t462_p7", SqlDbType.Text, 200);
            aParam[8].Value = t462_p7;
            aParam[9] = new SqlParameter("@t462_p8", SqlDbType.Text, 200);
            aParam[9].Value = t462_p8;
            aParam[10] = new SqlParameter("@t462_p9", SqlDbType.Text, 200);
            aParam[10].Value = t462_p9;
            aParam[11] = new SqlParameter("@t462_p10", SqlDbType.Text, 200);
            aParam[11].Value = t462_p10;
            aParam[12] = new SqlParameter("@t462_p11", SqlDbType.Text, 200);
            aParam[12].Value = t462_p11;
            aParam[13] = new SqlParameter("@t462_p12", SqlDbType.Text, 200);
            aParam[13].Value = t462_p12;
            aParam[14] = new SqlParameter("@t462_p13", SqlDbType.Text, 200);
            aParam[14].Value = t462_p13;
            aParam[15] = new SqlParameter("@t462_p14", SqlDbType.Text, 200);
            aParam[15].Value = t462_p14;
            aParam[16] = new SqlParameter("@t462_p15", SqlDbType.Text, 200);
            aParam[16].Value = t462_p15;
            aParam[17] = new SqlParameter("@t462_p16", SqlDbType.Text, 200);
            aParam[17].Value = t462_p16;
            aParam[18] = new SqlParameter("@t462_p17", SqlDbType.Text, 200);
            aParam[18].Value = t462_p17;
            aParam[19] = new SqlParameter("@t462_p18", SqlDbType.Text, 200);
            aParam[19].Value = t462_p18;
            aParam[20] = new SqlParameter("@t462_p19", SqlDbType.Text, 200);
            aParam[20].Value = t462_p19;
            aParam[21] = new SqlParameter("@t462_p20", SqlDbType.Text, 200);
            aParam[21].Value = t462_p20;
            aParam[22] = new SqlParameter("@t462_p21", SqlDbType.Text, 200);
            aParam[22].Value = t462_p21;
            aParam[23] = new SqlParameter("@t462_p22", SqlDbType.Text, 200);
            aParam[23].Value = t462_p22;
            aParam[24] = new SqlParameter("@t462_p23", SqlDbType.Text, 200);
            aParam[24].Value = t462_p23;
            aParam[25] = new SqlParameter("@t462_p24", SqlDbType.Text, 200);
            aParam[25].Value = t462_p24;
            aParam[26] = new SqlParameter("@t462_p25", SqlDbType.Text, 200);
            aParam[26].Value = t462_p25;
            aParam[27] = new SqlParameter("@t462_p26", SqlDbType.Text, 200);
            aParam[27].Value = t462_p26;
            aParam[28] = new SqlParameter("@t462_p27", SqlDbType.Text, 200);
            aParam[28].Value = t462_p27;
            aParam[29] = new SqlParameter("@t462_p28", SqlDbType.Text, 200);
            aParam[29].Value = t462_p28;
            aParam[30] = new SqlParameter("@t462_p29", SqlDbType.Text, 200);
            aParam[30].Value = t462_p29;
            aParam[31] = new SqlParameter("@t462_p30", SqlDbType.Text, 200);
            aParam[31].Value = t462_p30;
            aParam[32] = new SqlParameter("@t462_p31", SqlDbType.Text, 200);
            aParam[32].Value = t462_p31;
            aParam[33] = new SqlParameter("@t462_p32", SqlDbType.Text, 200);
            aParam[33].Value = t462_p32;
            aParam[34] = new SqlParameter("@t462_p33", SqlDbType.Text, 200);
            aParam[34].Value = t462_p33;
            aParam[35] = new SqlParameter("@t462_p34", SqlDbType.Text, 200);
            aParam[35].Value = t462_p34;
            aParam[36] = new SqlParameter("@t462_p35", SqlDbType.Text, 200);
            aParam[36].Value = t462_p35;
            aParam[37] = new SqlParameter("@t462_p36", SqlDbType.Text, 200);
            aParam[37].Value = t462_p36;
            aParam[38] = new SqlParameter("@t462_p37", SqlDbType.Text, 200);
            aParam[38].Value = t462_p37;
            aParam[39] = new SqlParameter("@t462_p38", SqlDbType.Text, 200);
            aParam[39].Value = t462_p38;
            aParam[40] = new SqlParameter("@t462_p39", SqlDbType.Text, 200);
            aParam[40].Value = t462_p39;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PREFERENCIAUSUARIO_CVT_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PREFERENCIAUSUARIO_CVT_I", aParam));
        }

        //public static int InsertarConIDFicepi(SqlTransaction tr, int t001_idficepi, short t463_idpantalla, string t462_p1, string t462_p2, 
        //                                        string t462_p3, string t462_p4, string t462_p5, string t462_p6, string t462_p7, string t462_p8, 
        //                                        string t462_p9, string t462_p10, string t462_p11, string t462_p12, string t462_p13, 
        //                                        string t462_p14, string t462_p15)
        //{
        //    SqlParameter[] aParam = new SqlParameter[]{
        //        ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
        //        ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla),
        //        ParametroSql.add("@t462_p1", SqlDbType.Text, 15, t462_p1),
        //        ParametroSql.add("@t462_p2", SqlDbType.Text, 15, t462_p2),
        //        ParametroSql.add("@t462_p3", SqlDbType.Text, 15, t462_p3),
        //        ParametroSql.add("@t462_p4", SqlDbType.Text, 15, t462_p4),
        //        ParametroSql.add("@t462_p5", SqlDbType.Text, 15, t462_p5),
        //        ParametroSql.add("@t462_p6", SqlDbType.Text, 15, t462_p6),
        //        ParametroSql.add("@t462_p7", SqlDbType.Text, 15, t462_p7),
        //        ParametroSql.add("@t462_p8", SqlDbType.Text, 15, t462_p8),
        //        ParametroSql.add("@t462_p9", SqlDbType.Text, 15, t462_p9),
        //        ParametroSql.add("@t462_p10", SqlDbType.Text, 15, t462_p10),
        //        ParametroSql.add("@t462_p11", SqlDbType.Text, 15, t462_p11),
        //        ParametroSql.add("@t462_p12", SqlDbType.Text, 15, t462_p12),
        //        ParametroSql.add("@t462_p13", SqlDbType.Text, 15, t462_p13),
        //        ParametroSql.add("@t462_p14", SqlDbType.Text, 15, t462_p14),
        //        ParametroSql.add("@t462_p15", SqlDbType.Text, 15, t462_p15)
        //        };

        //    if (tr == null)
        //        return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PREFERENCIAUSUARIO_FIC_INS", aParam));
        //    else
        //        return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PREFERENCIAUSUARIO_FIC_INS", aParam));
        //}

        //public static SqlDataReader ObtenerConIDFicepi(SqlTransaction tr, Nullable<int> t462_idPrefUsuario, Nullable<int> t001_idficepi, 
        //                                                Nullable<short> t463_idpantalla)
        //{
        //    string sProcAlm = "";
        //    SqlParameter[] aParam = new SqlParameter[]{
        //        ParametroSql.add("@t462_idPrefUsuario", SqlDbType.Int, 4, t462_idPrefUsuario),
        //        ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
        //        ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla)
        //    };

        //    switch (t463_idpantalla)
        //    {
        //        case 38: sProcAlm = "SUP_PREFERENCIAUSUARIO_SIB"; break;
        //    }

        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sProcAlm, aParam);
        //}
        //public static int DeleteAllConIDFicepi(SqlTransaction tr, int t001_idficepi, short t463_idpantalla)
        //{
        //    SqlParameter[] aParam = new SqlParameter[]{
        //        ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
        //        ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla)
        //    };

        //    if (tr == null)
        //        return SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIO_DEL_FIC_ALL", aParam);
        //    else
        //        return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIO_DEL_FIC_ALL", aParam);
        //}

        public static int DeleteAll(SqlTransaction tr, int t001_idficepi, short t463_idpantalla)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t463_idpantalla", SqlDbType.SmallInt, 2);
            aParam[1].Value = t463_idpantalla;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIO_D_ALL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIO_D_ALL", aParam);
        }
        public static int DeleteAllCVT(SqlTransaction tr, int t001_idficepi, short t463_idpantalla)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@t463_idpantalla", SqlDbType.SmallInt, 2);
            aParam[1].Value = t463_idpantalla;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIO_CVT_D_ALL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIO_CVT_D_ALL", aParam);
        }

        public static SqlDataReader CatalogoPantallaUsuario(SqlTransaction tr, int t001_idficepi, short t463_idpantalla)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla)
            };
            string sProcAlm = "SUP_PREFERENCIAUSUARIO_CAT";
            if (t463_idpantalla==-2)//preferencias totales en la pantalla de consulta de CVs
                sProcAlm = "SUP_PREFERENCIAUSUARIO_CVT_CONSULTA_CAT";
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, sProcAlm, aParam);
        }
        //public static SqlDataReader CatalogoPantallaUsuarioCM(SqlTransaction tr, int t001_idficepi, short t463_idpantalla)
        //{
        //    SqlParameter[] aParam = new SqlParameter[]{
        //        ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
        //        ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla)
        //    };

        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader("SUP_PREFERENCIAUSUARIO_CM_CAT", aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PREFERENCIAUSUARIO_CM_CAT", aParam);
        //}
        public static SqlDataReader CatalogoPantallaUsuarioCVT(SqlTransaction tr, int t001_idficepi, short t463_idpantalla)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t463_idpantalla", SqlDbType.SmallInt, 2, t463_idpantalla)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PREFERENCIAUSUARIO_CVT_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PREFERENCIAUSUARIO_CVT_CAT", aParam);
        }
        public static void setDefecto(SqlTransaction tr, int t462_idPrefUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t462_idPrefUsuario", SqlDbType.Int, 4, t462_idPrefUsuario),
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIO_U_DEF", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIO_U_DEF", aParam);
        }
        public static void UpdateCatalogo(SqlTransaction tr, int t462_idPrefUsuario, string t462_denominacion, bool t462_defecto, byte t462_orden)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t462_idPrefUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t462_idPrefUsuario;
            aParam[1] = new SqlParameter("@t462_denominacion", SqlDbType.VarChar, 50);
            aParam[1].Value = t462_denominacion;
            aParam[2] = new SqlParameter("@t462_defecto", SqlDbType.Bit, 1);
            aParam[2].Value = t462_defecto;
            aParam[3] = new SqlParameter("@t462_orden", SqlDbType.TinyInt, 2);
            aParam[3].Value = t462_orden;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIO_U_CAT", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIO_U_CAT", aParam);
        }
        public static void UpdateCatalogoCVT(SqlTransaction tr, int t462_idPrefUsuario, string t462_denominacion, bool t462_defecto, byte t462_orden)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t462_idPrefUsuario", SqlDbType.Int, 4);
            aParam[0].Value = t462_idPrefUsuario;
            aParam[1] = new SqlParameter("@t462_denominacion", SqlDbType.VarChar, 50);
            aParam[1].Value = t462_denominacion;
            aParam[2] = new SqlParameter("@t462_defecto", SqlDbType.Bit, 1);
            aParam[2].Value = t462_defecto;
            aParam[3] = new SqlParameter("@t462_orden", SqlDbType.TinyInt, 2);
            aParam[3].Value = t462_orden;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PREFERENCIAUSUARIOCVT_U_CAT", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PREFERENCIAUSUARIOCVT_U_CAT", aParam);
        }

        public static int GetTipoPantallaPreferencia(SqlTransaction tr, int t462_idPrefUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t462_idPrefUsuario", SqlDbType.Int, 4, t462_idPrefUsuario)
            };
            int iPant = -1;
            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PREFERENCIAUSUARIO_CVT_GETTIPO", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PREFERENCIAUSUARIO_CVT_GETTIPO", aParam);
            if (dr.Read())
            {
                iPant = int.Parse(dr["t463_idpantalla"].ToString());
            }
            dr.Close();
            dr.Dispose();

            return iPant;
        }
        #region SIB

        public static string delPreferenciaSIB(int t001_idficepi, short t463_idpantalla)
        {
            try
            {
                PREFERENCIAUSUARIO.DeleteAll(null, t001_idficepi, t463_idpantalla);
                return "OK@#@";
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al eliminar la preferencia", ex);
            }
        }

        public static string getPreferenciaSIB(string sIdPrefUsuario)
        {
            ArrayList aSubnodos = new ArrayList();
            string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            StringBuilder sb = new StringBuilder();
            int idPrefUsuario = 0; //, nConceptoEje = 0;
            string sDenominacionPreferencia = "", sMonedaImportesFiltrado="", sDenominacionMonedaImportesFiltrado="";
            string sSubnodos = "";
            bool bHayPreferencia = false;
            try
            {
                SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                             (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], 38);
                if (dr.Read())
                {
                    bHayPreferencia = true;

                    sb.Append(dr["t462_idPrefUsuario"].ToString() + "@#@"); //1
                    sb.Append(dr["categoria"].ToString() + "@#@"); //3
                    sb.Append(dr["cualidad"].ToString() + "@#@"); //4
                    sb.Append(dr["CerrarAuto"].ToString() + "@#@"); //5
                    sb.Append(dr["ActuAuto"].ToString() + "@#@"); //6
                    sb.Append(dr["Vista"].ToString() + "@#@"); //7
                    sb.Append(dr["evoMensual"].ToString() + "@#@"); //8
                    
                    sb.Append(dr["SectorCG"].ToString() + "@#@"); //9
                    sb.Append(dr["SectorCF"].ToString() + "@#@"); //10

                    sb.Append(dr["SegmentoCG"].ToString() + "@#@"); //11
                    sb.Append(dr["SegmentoCF"].ToString() + "@#@"); //12

                    idPrefUsuario = int.Parse(dr["t462_idPrefUsuario"].ToString());
                    sDenominacionPreferencia = dr["t462_denominacion"].ToString();
                    sMonedaImportesFiltrado = dr["monedaImportesFiltrado"].ToString(); 
                    //nUtilidadPeriodo = int.Parse(dr["OpcionPeriodo"].ToString());
                }
                dr.Close();
                //dr.Dispose();

                if (bHayPreferencia == false) return "OK@#@NO@#@";

                if (sMonedaImportesFiltrado == "")
                {
                    sMonedaImportesFiltrado = "EUR";
                }
                sDenominacionMonedaImportesFiltrado = MONEDA.getDenominacionImportes(sMonedaImportesFiltrado);

                #region HTML, IDs
                int nNivelMinimo = 0;
                bool bAmbito = false;
                string[] aID = null;
                dr = PREFUSUMULTIVALOR.Obtener(null, idPrefUsuario);
                string strHTMLAmbito = "", strHTMLResponsable = "", strHTMLNaturaleza = "", strHTMLModeloCon = "", strHTMLSector = "", strHTMLSegmento = "", strHTMLCliente = "", strHTMLContrato = "", strHTMLProyecto = "", strHTMLClienteFact = "", strHTMLSociedad = "", strHTMLAgrupaciones = "", strHTMLComerciales = "";
                string strIDsAmbito = "", strIDsResponsable = "", strIDsNaturaleza = "", strIDsModeloCon = "", strIDsSector = "", strIDsSegmento = "", strIDsCliente = "", strIDsContrato = "", strIDsProyecto = "", strIDsClienteFact = "", strIDsSociedades = "", strIDsComerciales = "";

                while (dr.Read())
                {
                    switch (int.Parse(dr["t441_concepto"].ToString()))
                    {
                        case 1:
                            if (!bAmbito)
                            {
                                bAmbito = true;
                                nNivelMinimo = 6;
                            }
                            aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                            if (int.Parse(aID[0]) < nNivelMinimo) nNivelMinimo = int.Parse(aID[0]);

                            if (strIDsAmbito != "") strIDsAmbito += ",";
                            strIDsAmbito += aID[1];
                            
                            aSubnodos = PREFUSUMULTIVALOR.SelectSubnodosAmbito(null, aSubnodos, int.Parse(aID[0]), int.Parse(aID[1]));
                            strHTMLAmbito += "<tr id='" + aID[1] + "' tipo='" + aID[0] + "' style='height:16px;' idAux='";
                            strHTMLAmbito += SUBNODO.fgGetCadenaID(aID[0], aID[1]);
                            strHTMLAmbito += "'><td>";

                            switch (int.Parse(aID[0]))
                            {
                                case 1: strHTMLAmbito += "<img src='../../../images/imgSN4.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                                case 2: strHTMLAmbito += "<img src='../../../images/imgSN3.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                                case 3: strHTMLAmbito += "<img src='../../../images/imgSN2.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                                case 4: strHTMLAmbito += "<img src='../../../images/imgSN1.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                                case 5: strHTMLAmbito += "<img src='../../../images/imgNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                                case 6: strHTMLAmbito += "<img src='../../../images/imgSubNodo.gif' style='margin-left:2px;margin-right:4px;vertical-align:middle;border: 0px;'>"; break;
                            }

                            strHTMLAmbito += "<nobr class='NBR W230'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 2:
                            if (strIDsResponsable != "") strIDsResponsable += ",";
                            strIDsResponsable += dr["t441_valor"].ToString();
                            strHTMLResponsable += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 3:
                            if (strIDsNaturaleza != "") strIDsNaturaleza += ",";
                            strIDsNaturaleza += dr["t441_valor"].ToString();
                            strHTMLNaturaleza += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 4:
                            if (strIDsModeloCon != "") strIDsModeloCon += ",";
                            strIDsModeloCon += dr["t441_valor"].ToString();
                            strHTMLModeloCon += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 6: if (strIDsSector != "") strIDsSector += ",";
                            strIDsSector += dr["t441_valor"].ToString();
                            strHTMLSector += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 7: if (strIDsSegmento != "") strIDsSegmento += ",";
                            strIDsSegmento += dr["t441_valor"].ToString();
                            strHTMLSegmento += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 8:
                            if (strIDsCliente != "") strIDsCliente += ",";
                            strIDsCliente += dr["t441_valor"].ToString();
                            strHTMLCliente += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 9:
                            if (strIDsContrato != "") strIDsContrato += ",";
                            strIDsContrato += dr["t441_valor"].ToString();
                            strHTMLContrato += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 16:
                            aID = Regex.Split(dr["t441_valor"].ToString(), "-");
                            if (strIDsProyecto != "") strIDsProyecto += ",";
                            strIDsProyecto += aID[0];

                            strHTMLProyecto += "<tr id='" + aID[0] + "' style='height:16px;' ";
                            strHTMLProyecto += "categoria='" + aID[1] + "' ";
                            strHTMLProyecto += "cualidad='" + aID[2] + "' ";
                            strHTMLProyecto += "estado='" + aID[3] + "'><td>";

                            if (aID[1] == "P") strHTMLProyecto += "<img src='../../../images/imgProducto.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";
                            else strHTMLProyecto += "<img src='../../../images/imgServicio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>";

                            switch (aID[2])
                            {
                                case "C": strHTMLProyecto += "<img src='../../../images/imgIconoContratante.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                                case "J": strHTMLProyecto += "<img src='../../../images/imgIconoRepJor.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                                case "P": strHTMLProyecto += "<img src='../../../images/imgIconoRepPrecio.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            }

                            switch (aID[3])
                            {
                                case "A": strHTMLProyecto += "<img src='../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                                case "C": strHTMLProyecto += "<img src='../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                                case "H": strHTMLProyecto += "<img src='../../../images/imgIconoProyHistorico.gif' title='Proyecto histórico' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                                case "P": strHTMLProyecto += "<img src='../../../images/imgIconoProyPresup.gif' title='Proyecto presupuestado' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>"; break;
                            }

                            strHTMLProyecto += "<nobr class='NBR W190' style='margin-left:3px;' onmouseover='TTip(event)'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 17:
                            if (strIDsClienteFact != "") strIDsClienteFact += ",";
                            strIDsClienteFact += dr["t441_valor"].ToString();
                            strHTMLClienteFact += "<tr id='" + dr["t441_valor"].ToString() + "'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 22:
                            if (strIDsSociedades != "") strIDsSociedades += ",";
                            strIDsSociedades += dr["t441_valor"].ToString();
                            strHTMLSociedad += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                            if (strHTMLAgrupaciones != "") strHTMLAgrupaciones += "#/#";
                            strHTMLAgrupaciones += dr["t441_denominacion"].ToString() +"{sep}"+ dr["t441_valor"].ToString();
                            break;
                        case 32:
                            if (strIDsComerciales != "") strIDsComerciales += ",";
                            strIDsComerciales += dr["t441_valor"].ToString();
                            strHTMLComerciales += "<tr id='" + dr["t441_valor"].ToString() + "' style='height:16px;'><td><nobr class='NBR W260'>" + dr["t441_denominacion"].ToString() + "</nobr></td></tr>";
                            break;
                    }
                }
                dr.Close();
                dr.Dispose();
                #endregion

                for (int i = 0; i < aSubnodos.Count; i++)
                {
                    if (i > 0) sSubnodos += ",";
                    sSubnodos += aSubnodos[i];
                }

                sb.Append(sSubnodos + "@#@"); //13
                sb.Append(strHTMLAmbito + "@#@"); //14
                sb.Append(strIDsAmbito + "@#@"); //15
                sb.Append(strHTMLResponsable + "@#@"); //16
                sb.Append(strIDsResponsable + "@#@"); //17
                sb.Append(strHTMLNaturaleza + "@#@"); //18
                sb.Append(strIDsNaturaleza + "@#@"); //19
                sb.Append(strHTMLModeloCon + "@#@"); //20
                sb.Append(strIDsModeloCon + "@#@"); //21
                sb.Append(strHTMLSector + "@#@"); //22
                sb.Append(strIDsSector + "@#@"); //23
                sb.Append(strHTMLSegmento + "@#@"); //24
                sb.Append(strIDsSegmento + "@#@"); //25
                sb.Append(strHTMLCliente + "@#@"); //26
                sb.Append(strIDsCliente + "@#@"); //27
                sb.Append(strHTMLContrato + "@#@"); //28
                sb.Append(strIDsContrato + "@#@"); //29
              
                sb.Append(strHTMLProyecto + "@#@"); //30
                sb.Append(strIDsProyecto + "@#@"); //31
                sb.Append(strHTMLClienteFact + "@#@"); //32
                sb.Append(strIDsClienteFact + "@#@"); //33
                sb.Append(strHTMLSociedad + "@#@"); //34
                sb.Append(strIDsSociedades + "@#@"); //35
                sb.Append(strHTMLAgrupaciones + "@#@"); //36
                sb.Append(sDenominacionPreferencia + "@#@"); //37
                sb.Append(strHTMLComerciales + "@#@"); //38
                sb.Append(strIDsComerciales + "@#@"); //39
                sb.Append(sMonedaImportesFiltrado + "@#@"); //40
                sb.Append(sDenominacionMonedaImportesFiltrado + "@#@"); //41

                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
            }
        }

        public static string setPreferenciaSIB(string sCategoria, string sCualidad, string sCerrarAuto, string sActuAuto, string cboVista, 
                                                string evoMensual, string SectorCG, string SectorCF, string SegmentoCG, string SegmentoCF, 
                                                string sMonedaImportesFiltrado, string sValoresMultiples)
        {
            string sResul = "";
            SqlConnection oConn = new SqlConnection();
            SqlTransaction tr;
            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            try
            {
                int nPref = PREFERENCIAUSUARIO.Insertar(tr,
                                            (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"],
                                            38,
                                            (sCategoria == "") ? null : sCategoria,
                                            (sCualidad == "") ? null : sCualidad,
                                            (sCerrarAuto == "") ? null : sCerrarAuto,
                                            (sActuAuto == "") ? null : sActuAuto,
                                            cboVista,
                                            evoMensual,
                                            SectorCF,
                                            SectorCG, SegmentoCF, SegmentoCG, sMonedaImportesFiltrado,
                                            null, null, null, null, null, null, null, null, null, null);

                #region Valores Múltiples
                if (sValoresMultiples != "")
                {
                    byte nOrden = 0;
                    string[] aValores = Regex.Split(sValoresMultiples, "///");
                    foreach (string oValor in aValores)
                    {
                        if (oValor == "") continue;
                        string[] aDatos = Regex.Split(oValor, "##");
                        ///aDatos[0] = concepto
                        ///aDatos[1] = idValor
                        ///aDatos[2] = denominacion

                        PREFUSUMULTIVALOR.Insertar(tr, nPref, byte.Parse(aDatos[0]), aDatos[1], Utilidades.unescape(aDatos[2]), nOrden);
                        nOrden++;
                    }
                }

                #endregion

                Conexion.CommitTransaccion(tr);

                sResul = "OK@#@" + nPref.ToString();
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        #endregion

        #endregion
    }
}
