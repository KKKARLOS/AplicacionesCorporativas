using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class POSICIONFAC
	{
		#region Metodos

        public static SqlDataReader CatalogoByOrdenFac(SqlTransaction tr, int t610_idordenfac) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_POSICIONFAC_CATOF", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_POSICIONFAC_CATOF", aParam);
		}

        public static int Insert(SqlTransaction tr,
                            int t610_idordenfac,
                            string t611_denominacion,
                            string t611_descripcion,
                            float t611_unidades,
                            decimal t611_preciounitario,
                            float t611_dto_porcen,
                            decimal t611_dto_importe,
                            short t611_orden
                            )
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;
            aParam[1] = new SqlParameter("@t611_denominacion", SqlDbType.Text, 40);
            aParam[1].Value = t611_denominacion;
            aParam[2] = new SqlParameter("@t611_descripcion", SqlDbType.Text, 2147483647);
            aParam[2].Value = t611_descripcion;
            aParam[3] = new SqlParameter("@t611_unidades", SqlDbType.Real, 4);
            aParam[3].Value = t611_unidades;
            aParam[4] = new SqlParameter("@t611_preciounitario", SqlDbType.Money, 8);
            aParam[4].Value = t611_preciounitario;
            aParam[5] = new SqlParameter("@t611_dto_porcen", SqlDbType.Real, 4);
            aParam[5].Value = t611_dto_porcen;
            aParam[6] = new SqlParameter("@t611_dto_importe", SqlDbType.Money, 8);
            aParam[6].Value = t611_dto_importe;
            aParam[7] = new SqlParameter("@t611_orden", SqlDbType.SmallInt, 1);
            aParam[7].Value = t611_orden;
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_POSICIONFAC_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_POSICIONFAC_INS", aParam));
        }

        public static void Update(SqlTransaction tr,
                    int t610_idordenfac,
                    int t611_posicion,
                    string t611_denominacion,
                    string t611_descripcion,
                    float t611_unidades,
                    decimal t611_preciounitario,
                    float t611_dto_porcen,
                    decimal t611_dto_importe,
                    short t611_orden
                    )
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;
            aParam[1] = new SqlParameter("@t611_posicion", SqlDbType.Int, 4);
            aParam[1].Value = t611_posicion;
            aParam[2] = new SqlParameter("@t611_denominacion", SqlDbType.Text, 40);
            aParam[2].Value = t611_denominacion;
            aParam[3] = new SqlParameter("@t611_descripcion", SqlDbType.Text, 2147483647);
            aParam[3].Value = t611_descripcion;
            aParam[4] = new SqlParameter("@t611_unidades", SqlDbType.Real, 4);
            aParam[4].Value = t611_unidades;
            aParam[5] = new SqlParameter("@t611_preciounitario", SqlDbType.Money, 8);
            aParam[5].Value = t611_preciounitario;
            aParam[6] = new SqlParameter("@t611_dto_porcen", SqlDbType.Real, 4);
            aParam[6].Value = t611_dto_porcen;
            aParam[7] = new SqlParameter("@t611_dto_importe", SqlDbType.Money, 8);
            aParam[7].Value = t611_dto_importe;
            aParam[8] = new SqlParameter("@t611_orden", SqlDbType.SmallInt, 1);
            aParam[8].Value = t611_orden;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_POSICIONFAC_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POSICIONFAC_UPD", aParam);
        }

        #endregion
	}
}
