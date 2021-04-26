using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class PLANTILLAPOSICIONFAC
    {
        #region Metodos

        public static SqlDataReader CatalogoByPlantilla(SqlTransaction tr, int t629_idplantillaof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
            aParam[0].Value = t629_idplantillaof;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLAPOSICIONFAC_CATPL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLAPOSICIONFAC_CATPL", aParam);
        }

        public static int Insert(SqlTransaction tr, int t629_idplantillaof, string t630_denominacion, string t630_descripcion, float t630_unidades, decimal t630_preciounitario, float t630_dto_porcen, decimal t630_dto_importe)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
            aParam[0].Value = t629_idplantillaof;
            aParam[1] = new SqlParameter("@t630_denominacion", SqlDbType.VarChar, 40);
            aParam[1].Value = t630_denominacion;
            aParam[2] = new SqlParameter("@t630_descripcion", SqlDbType.Text, 2147483647);
            aParam[2].Value = t630_descripcion;
            aParam[3] = new SqlParameter("@t630_unidades", SqlDbType.Real, 4);
            aParam[3].Value = t630_unidades;
            aParam[4] = new SqlParameter("@t630_preciounitario", SqlDbType.Money, 8);
            aParam[4].Value = t630_preciounitario;
            aParam[5] = new SqlParameter("@t630_dto_porcen", SqlDbType.Real, 4);
            aParam[5].Value = t630_dto_porcen;
            aParam[6] = new SqlParameter("@t630_dto_importe", SqlDbType.Money, 8);
            aParam[6].Value = t630_dto_importe;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANTILLAPOSICIONFAC_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANTILLAPOSICIONFAC_INS", aParam));

        }
        #endregion
    }
}
