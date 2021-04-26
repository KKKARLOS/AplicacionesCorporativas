using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for SALDOSSAP
/// </summary>

namespace IB.SUPER.APP.DAL
{

    internal class SALDOSSAP
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            MANDT = 1,
            BUKRS = 2,
            KUNNR = 3,
            AUGDT = 4,
            AUGBL = 5,
            AUGGJ = 6,
            GJAHR = 7,
            BELNR = 8,
            UMSKS = 9,
            UMSKZ = 10,
            BUZEI = 11,
            ZUONR = 12,
            POSNR = 13,
            PARVW = 14,
            XBLNR = 15,
            VBELN = 16,
            REBZG = 17,
            LIFNR = 18,
            ZTERM = 19,
            FKDAT = 20,
            SHKZG = 21,
            DMBTR = 22,
            MWSKZ = 23,
            MWSK1 = 24,
            DMBT1 = 25,
            MWSK2 = 26,
            DMBT2 = 27,
            MWSK3 = 28,
            DMBT3 = 29,
            SGTXT = 30,
            HKONT = 31,
            BUDAT = 32,
            ZFBDT = 33,
            ZBD1T = 34,
            ZBD2T = 35,
            ZBD3T = 36,
            ZVENC = 37,
            BUSAB = 38,
            MANSP = 39,
            ZLSCH = 40
        }

        internal SALDOSSAP(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas
        /// <summary>
        /// Inserta un SALDOSSAP
        /// </summary>
        internal int Insert(Models.SALDOSSAP oSALDOSSAP)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[40] {
                    Param(enumDBFields.MANDT, oSALDOSSAP.MANDT),
                    Param(enumDBFields.BUKRS, oSALDOSSAP.BUKRS),
                    Param(enumDBFields.KUNNR, oSALDOSSAP.KUNNR),
                    Param(enumDBFields.AUGDT, oSALDOSSAP.AUGDT),
                    Param(enumDBFields.AUGBL, oSALDOSSAP.AUGBL),
                    Param(enumDBFields.AUGGJ, oSALDOSSAP.AUGGJ),
                    Param(enumDBFields.GJAHR, oSALDOSSAP.GJAHR),
                    Param(enumDBFields.BELNR, oSALDOSSAP.BELNR),
                    Param(enumDBFields.UMSKS, oSALDOSSAP.UMSKS),
                    Param(enumDBFields.UMSKZ, oSALDOSSAP.UMSKZ),
                    Param(enumDBFields.BUZEI, oSALDOSSAP.BUZEI),
                    Param(enumDBFields.ZUONR, oSALDOSSAP.ZUONR),
                    Param(enumDBFields.POSNR, oSALDOSSAP.POSNR),
                    Param(enumDBFields.PARVW, oSALDOSSAP.PARVW),
                    Param(enumDBFields.XBLNR, oSALDOSSAP.XBLNR),
                    Param(enumDBFields.VBELN, oSALDOSSAP.VBELN),
                    Param(enumDBFields.REBZG, oSALDOSSAP.REBZG),
                    Param(enumDBFields.LIFNR, oSALDOSSAP.LIFNR),
                    Param(enumDBFields.ZTERM, oSALDOSSAP.ZTERM),
                    Param(enumDBFields.FKDAT, oSALDOSSAP.FKDAT),
                    Param(enumDBFields.SHKZG, oSALDOSSAP.SHKZG),
                    Param(enumDBFields.DMBTR, oSALDOSSAP.DMBTR),
                    Param(enumDBFields.MWSKZ, oSALDOSSAP.MWSKZ),
                    Param(enumDBFields.MWSK1, oSALDOSSAP.MWSK1),
                    Param(enumDBFields.DMBT1, oSALDOSSAP.DMBT1),
                    Param(enumDBFields.MWSK2, oSALDOSSAP.MWSK2),
                    Param(enumDBFields.DMBT2, oSALDOSSAP.DMBT2),
                    Param(enumDBFields.MWSK3, oSALDOSSAP.MWSK3),
                    Param(enumDBFields.DMBT3, oSALDOSSAP.DMBT3),
                    Param(enumDBFields.SGTXT, oSALDOSSAP.SGTXT),
                    Param(enumDBFields.HKONT, oSALDOSSAP.HKONT),
                    Param(enumDBFields.BUDAT, oSALDOSSAP.BUDAT),
                    Param(enumDBFields.ZFBDT, oSALDOSSAP.ZFBDT),
                    Param(enumDBFields.ZBD1T, oSALDOSSAP.ZBD1T),
                    Param(enumDBFields.ZBD2T, oSALDOSSAP.ZBD2T),
                    Param(enumDBFields.ZBD3T, oSALDOSSAP.ZBD3T),
                    Param(enumDBFields.ZVENC, oSALDOSSAP.ZVENC),
                    Param(enumDBFields.BUSAB, oSALDOSSAP.BUSAB),
                    Param(enumDBFields.MANSP, oSALDOSSAP.MANSP),
                    Param(enumDBFields.ZLSCH, oSALDOSSAP.ZLSCH)
                };

                return (int)cDblib.Execute("SUP_SALDOSSAP_INS", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina todos los registros de SALDOSSAP
        /// </summary>
        internal int Delete()
        {
            try
            {

                SqlParameter[] dbparams = new SqlParameter[0] { };
                return (int)cDblib.Execute("SUP_SALDOSSAP_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los SALDOSSAP
        /// </summary>
        internal List<Models.SALDOSSAP> Catalogo()
        {
            Models.SALDOSSAP oSALDOSSAP = null;
            List<Models.SALDOSSAP> lst = new List<Models.SALDOSSAP>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] { };

                dr = cDblib.DataReader("SUP_SALDOSSAP_CAT", dbparams);
                while (dr.Read())
                {
                    oSALDOSSAP = new Models.SALDOSSAP();
                    if (!Convert.IsDBNull(dr["MANDT"]))
                        oSALDOSSAP.MANDT = Convert.ToString(dr["MANDT"]);
                    if (!Convert.IsDBNull(dr["BUKRS"]))
                        oSALDOSSAP.BUKRS = Convert.ToString(dr["BUKRS"]);
                    if (!Convert.IsDBNull(dr["KUNNR"]))
                        oSALDOSSAP.KUNNR = Convert.ToString(dr["KUNNR"]);
                    if (!Convert.IsDBNull(dr["AUGDT"]))
                        oSALDOSSAP.AUGDT = Convert.ToString(dr["AUGDT"]);
                    if (!Convert.IsDBNull(dr["AUGBL"]))
                        oSALDOSSAP.AUGBL = Convert.ToString(dr["AUGBL"]);
                    if (!Convert.IsDBNull(dr["AUGGJ"]))
                        oSALDOSSAP.AUGGJ = Convert.ToString(dr["AUGGJ"]);
                    if (!Convert.IsDBNull(dr["GJAHR"]))
                        oSALDOSSAP.GJAHR = Convert.ToString(dr["GJAHR"]);
                    if (!Convert.IsDBNull(dr["BELNR"]))
                        oSALDOSSAP.BELNR = Convert.ToString(dr["BELNR"]);
                    if (!Convert.IsDBNull(dr["UMSKS"]))
                        oSALDOSSAP.UMSKS = Convert.ToString(dr["UMSKS"]);
                    if (!Convert.IsDBNull(dr["UMSKZ"]))
                        oSALDOSSAP.UMSKZ = Convert.ToString(dr["UMSKZ"]);
                    if (!Convert.IsDBNull(dr["BUZEI"]))
                        oSALDOSSAP.BUZEI = Convert.ToString(dr["BUZEI"]);
                    if (!Convert.IsDBNull(dr["ZUONR"]))
                        oSALDOSSAP.ZUONR = Convert.ToString(dr["ZUONR"]);
                    if (!Convert.IsDBNull(dr["POSNR"]))
                        oSALDOSSAP.POSNR = Convert.ToString(dr["POSNR"]);
                    if (!Convert.IsDBNull(dr["PARVW"]))
                        oSALDOSSAP.PARVW = Convert.ToString(dr["PARVW"]);
                    if (!Convert.IsDBNull(dr["XBLNR"]))
                        oSALDOSSAP.XBLNR = Convert.ToString(dr["XBLNR"]);
                    if (!Convert.IsDBNull(dr["VBELN"]))
                        oSALDOSSAP.VBELN = Convert.ToString(dr["VBELN"]);
                    if (!Convert.IsDBNull(dr["REBZG"]))
                        oSALDOSSAP.REBZG = Convert.ToString(dr["REBZG"]);
                    if (!Convert.IsDBNull(dr["LIFNR"]))
                        oSALDOSSAP.LIFNR = Convert.ToString(dr["LIFNR"]);
                    if (!Convert.IsDBNull(dr["ZTERM"]))
                        oSALDOSSAP.ZTERM = Convert.ToString(dr["ZTERM"]);
                    if (!Convert.IsDBNull(dr["FKDAT"]))
                        oSALDOSSAP.FKDAT = Convert.ToString(dr["FKDAT"]);
                    if (!Convert.IsDBNull(dr["SHKZG"]))
                        oSALDOSSAP.SHKZG = Convert.ToString(dr["SHKZG"]);
                    if (!Convert.IsDBNull(dr["DMBTR"]))
                        oSALDOSSAP.DMBTR = (decimal)dr["DMBTR"];
                    if (!Convert.IsDBNull(dr["MWSKZ"]))
                        oSALDOSSAP.MWSKZ = Convert.ToString(dr["MWSKZ"]);
                    if (!Convert.IsDBNull(dr["MWSK1"]))
                        oSALDOSSAP.MWSK1 = Convert.ToString(dr["MWSK1"]);
                    if (!Convert.IsDBNull(dr["DMBT1"]))
                        oSALDOSSAP.DMBT1 = (decimal)dr["DMBT1"];
                    if (!Convert.IsDBNull(dr["MWSK2"]))
                        oSALDOSSAP.MWSK2 = Convert.ToString(dr["MWSK2"]);
                    if (!Convert.IsDBNull(dr["DMBT2"]))
                        oSALDOSSAP.DMBT2 = (decimal)dr["DMBT2"];
                    if (!Convert.IsDBNull(dr["MWSK3"]))
                        oSALDOSSAP.MWSK3 = Convert.ToString(dr["MWSK3"]);
                    if (!Convert.IsDBNull(dr["DMBT3"]))
                        oSALDOSSAP.DMBT3 = (decimal)dr["DMBT3"];
                    if (!Convert.IsDBNull(dr["SGTXT"]))
                        oSALDOSSAP.SGTXT = Convert.ToString(dr["SGTXT"]);
                    if (!Convert.IsDBNull(dr["HKONT"]))
                        oSALDOSSAP.HKONT = Convert.ToString(dr["HKONT"]);
                    if (!Convert.IsDBNull(dr["BUDAT"]))
                        oSALDOSSAP.BUDAT = Convert.ToString(dr["BUDAT"]);
                    if (!Convert.IsDBNull(dr["ZFBDT"]))
                        oSALDOSSAP.ZFBDT = Convert.ToString(dr["ZFBDT"]);
                    if (!Convert.IsDBNull(dr["ZBD1T"]))
                        oSALDOSSAP.ZBD1T = (decimal)dr["ZBD1T"];
                    if (!Convert.IsDBNull(dr["ZBD2T"]))
                        oSALDOSSAP.ZBD2T = (decimal)dr["ZBD2T"];
                    if (!Convert.IsDBNull(dr["ZBD3T"]))
                        oSALDOSSAP.ZBD3T = (decimal)dr["ZBD3T"];
                    if (!Convert.IsDBNull(dr["ZVENC"]))
                        oSALDOSSAP.ZVENC = Convert.ToString(dr["ZVENC"]);
                    if (!Convert.IsDBNull(dr["BUSAB"]))
                        oSALDOSSAP.BUSAB = Convert.ToString(dr["BUSAB"]);
                    if (!Convert.IsDBNull(dr["MANSP"]))
                        oSALDOSSAP.MANSP = Convert.ToString(dr["MANSP"]);
                    if (!Convert.IsDBNull(dr["ZLSCH"]))
                        oSALDOSSAP.ZLSCH = Convert.ToString(dr["ZLSCH"]);

                    lst.Add(oSALDOSSAP);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }


        internal int Pasar_a_SUPER(int iFecha)
        {
            try
            {

               
                SqlParameter[] aParam = new SqlParameter[1];                
                aParam[0] = new SqlParameter("@anomes", SqlDbType.Int, 4);
                aParam[0].Value = iFecha;
               
                return (int)cDblib.ExecuteScalar("SUP_GENERACOBROSSUPER", aParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region funciones privadas
        private SqlParameter Param(enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            ParameterDirection paramDirection = ParameterDirection.Input;

            switch (dbField)
            {
                case enumDBFields.MANDT:
                    paramName = "@MANDT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 3;
                    break;
                case enumDBFields.BUKRS:
                    paramName = "@BUKRS";
                    paramType = SqlDbType.VarChar;
                    paramSize = 4;
                    break;
                case enumDBFields.KUNNR:
                    paramName = "@KUNNR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.AUGDT:
                    paramName = "@AUGDT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 8;
                    break;
                case enumDBFields.AUGBL:
                    paramName = "@AUGBL";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.AUGGJ:
                    paramName = "@AUGGJ";
                    paramType = SqlDbType.VarChar;
                    paramSize = 4;
                    break;
                case enumDBFields.GJAHR:
                    paramName = "@GJAHR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 4;
                    break;
                case enumDBFields.BELNR:
                    paramName = "@BELNR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.UMSKS:
                    paramName = "@UMSKS";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;
                case enumDBFields.UMSKZ:
                    paramName = "@UMSKZ";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;
                case enumDBFields.BUZEI:
                    paramName = "@BUZEI";
                    paramType = SqlDbType.VarChar;
                    paramSize = 3;
                    break;
                case enumDBFields.ZUONR:
                    paramName = "@ZUONR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 18;
                    break;
                case enumDBFields.POSNR:
                    paramName = "@POSNR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 6;
                    break;
                case enumDBFields.PARVW:
                    paramName = "@PARVW";
                    paramType = SqlDbType.VarChar;
                    paramSize = 2;
                    break;
                case enumDBFields.XBLNR:
                    paramName = "@XBLNR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 16;
                    break;
                case enumDBFields.VBELN:
                    paramName = "@VBELN";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.REBZG:
                    paramName = "@REBZG";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.LIFNR:
                    paramName = "@LIFNR";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.ZTERM:
                    paramName = "@ZTERM";
                    paramType = SqlDbType.VarChar;
                    paramSize = 4;
                    break;
                case enumDBFields.FKDAT:
                    paramName = "@FKDAT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 8;
                    break;
                case enumDBFields.SHKZG:
                    paramName = "@SHKZG";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;
                case enumDBFields.DMBTR:
                    paramName = "@DMBTR";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.MWSKZ:
                    paramName = "@MWSKZ";
                    paramType = SqlDbType.VarChar;
                    paramSize = 2;
                    break;
                case enumDBFields.MWSK1:
                    paramName = "@MWSK1";
                    paramType = SqlDbType.VarChar;
                    paramSize = 2;
                    break;
                case enumDBFields.DMBT1:
                    paramName = "@DMBT1";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.MWSK2:
                    paramName = "@MWSK2";
                    paramType = SqlDbType.VarChar;
                    paramSize = 2;
                    break;
                case enumDBFields.DMBT2:
                    paramName = "@DMBT2";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.MWSK3:
                    paramName = "@MWSK3";
                    paramType = SqlDbType.VarChar;
                    paramSize = 2;
                    break;
                case enumDBFields.DMBT3:
                    paramName = "@DMBT3";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.SGTXT:
                    paramName = "@SGTXT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;
                case enumDBFields.HKONT:
                    paramName = "@HKONT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 10;
                    break;
                case enumDBFields.BUDAT:
                    paramName = "@BUDAT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 8;
                    break;
                case enumDBFields.ZFBDT:
                    paramName = "@ZFBDT";
                    paramType = SqlDbType.VarChar;
                    paramSize = 8;
                    break;
                case enumDBFields.ZBD1T:
                    paramName = "@ZBD1T";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.ZBD2T:
                    paramName = "@ZBD2T";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.ZBD3T:
                    paramName = "@ZBD3T";
                    paramType = SqlDbType.Money;
                    paramSize = 8;
                    break;
                case enumDBFields.ZVENC:
                    paramName = "@ZVENC";
                    paramType = SqlDbType.VarChar;
                    paramSize = 8;
                    break;
                case enumDBFields.BUSAB:
                    paramName = "@BUSAB";
                    paramType = SqlDbType.VarChar;
                    paramSize = 2;
                    break;
                case enumDBFields.MANSP:
                    paramName = "@MANSP";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;
                case enumDBFields.ZLSCH:
                    paramName = "@ZLSCH";
                    paramType = SqlDbType.VarChar;
                    paramSize = 1;
                    break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion

    }

}
