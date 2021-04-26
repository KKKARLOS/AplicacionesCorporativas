using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AnnoGasto
/// </summary>

namespace IB.SUPER.IAP30.DAL 
{
    
    internal class AnnoGasto 
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
			t419_anno = 1,
			t419_fdesde = 2,
			t419_fhasta = 3,
			t001_idficepi = 4,
			t419_fmodif = 5
        }

        internal AnnoGasto(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        //#region funciones publicas
        ///// <summary>
        ///// Inserta un AnnoGasto
        ///// </summary>
        //internal int Insert(Models.AnnoGasto oAnnoGasto)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[5] {
        //            Param(enumDBFields.t419_anno, oAnnoGasto.t419_anno),
        //            Param(enumDBFields.t419_fdesde, oAnnoGasto.t419_fdesde),
        //            Param(enumDBFields.t419_fhasta, oAnnoGasto.t419_fhasta),
        //            Param(enumDBFields.t001_idficepi, oAnnoGasto.t001_idficepi),
        //            Param(enumDBFields.t419_fmodif, oAnnoGasto.t419_fmodif)
        //        };

        //        return (int)cDblib.Execute("_AnnoGasto_INS", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Obtiene un AnnoGasto a partir del id
        ///// </summary>
        //internal Models.AnnoGasto Select()
        //{
        //    Models.AnnoGasto oAnnoGasto = null;
        //    IDataReader dr = null;

        //    try
        //    {


        //        dr = cDblib.DataReader("_AnnoGasto_SEL", dbparams);
        //        if (dr.Read())
        //        {
        //            oAnnoGasto = new Models.AnnoGasto();
        //            oAnnoGasto.t419_anno=Convert.ToInt16(dr["t419_anno"]);
        //            oAnnoGasto.t419_fdesde=Convert.ToDateTime(dr["t419_fdesde"]);
        //            oAnnoGasto.t419_fhasta=Convert.ToDateTime(dr["t419_fhasta"]);
        //            oAnnoGasto.t001_idficepi=Convert.ToInt32(dr["t001_idficepi"]);
        //            oAnnoGasto.t419_fmodif=Convert.ToDateTime(dr["t419_fmodif"]);

        //        }
        //        return oAnnoGasto;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (dr != null)
        //        {
        //            if (!dr.IsClosed) dr.Close();
        //            dr.Dispose();
        //        }
        //    }
        //}

        ///// <summary>
        ///// Actualiza un AnnoGasto a partir del id
        ///// </summary>
        //internal int Update(Models.AnnoGasto oAnnoGasto)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[5] {
        //            Param(enumDBFields.t419_anno, oAnnoGasto.t419_anno),
        //            Param(enumDBFields.t419_fdesde, oAnnoGasto.t419_fdesde),
        //            Param(enumDBFields.t419_fhasta, oAnnoGasto.t419_fhasta),
        //            Param(enumDBFields.t001_idficepi, oAnnoGasto.t001_idficepi),
        //            Param(enumDBFields.t419_fmodif, oAnnoGasto.t419_fmodif)
        //        };

        //        return (int)cDblib.Execute("_AnnoGasto_UPD", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Elimina un AnnoGasto a partir del id
        ///// </summary>
        //internal int Delete()
        //{
        //    try
        //    {


        //        return (int)cDblib.Execute("_AnnoGasto_DEL", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Obtiene todos los AnnoGasto
        /// </summary>
        internal List<Models.AnnoGasto> Catalogo()
        {
            Models.AnnoGasto oAnnoGasto = null;
            List<Models.AnnoGasto> lst = new List<Models.AnnoGasto>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {};

                dr = cDblib.DataReader("GVT_ANNOGASTO_CAT", dbparams);
                while (dr.Read())
                {
                    oAnnoGasto = new Models.AnnoGasto();
                    oAnnoGasto.t419_anno = Convert.ToInt16(dr["t419_anno"]);
                    oAnnoGasto.t419_fdesde = Convert.ToDateTime(dr["t419_fdesde"]);
                    oAnnoGasto.t419_fhasta = Convert.ToDateTime(dr["t419_fhasta"]);
                    oAnnoGasto.t001_idficepi = Convert.ToInt32(dr["t001_idficepi"]);
                    oAnnoGasto.t419_fmodif = Convert.ToDateTime(dr["t419_fmodif"]);

                    lst.Add(oAnnoGasto);

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

        //#endregion

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
				case enumDBFields.t419_anno:
					paramName = "@t419_anno";
					paramType = SqlDbType.SmallInt;
					paramSize = 2;
					break;
				case enumDBFields.t419_fdesde:
					paramName = "@t419_fdesde";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t419_fhasta:
					paramName = "@t419_fhasta";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
					break;
				case enumDBFields.t001_idficepi:
					paramName = "@t001_idficepi";
					paramType = SqlDbType.Int;
					paramSize = 4;
					break;
				case enumDBFields.t419_fmodif:
					paramName = "@t419_fmodif";
					paramType = SqlDbType.DateTime;
					paramSize = 8;
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
