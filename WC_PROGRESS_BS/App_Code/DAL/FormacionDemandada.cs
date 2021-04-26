using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IB.Progress.DAL
{
    /// <summary>
    /// Descripción breve de GestionarIncorporaciones
    /// </summary>
    internal class FormacionDemandada
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            desde = 1,
            hasta = 2,
            idcolectivo = 3,
            nevaluaciones = 4,
            idficepi = 5
        }

        internal FormacionDemandada(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public FormacionDemandada()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion


        internal Models.FormacionDemandada catFormacionDemandada(Int32 idficepi, Int32 desde, Int32 hasta, Nullable<Int16> t941_idcolectivo)
        {
            Models.FormacionDemandada oFormacionDemandada = new Models.FormacionDemandada();
            
            //Parámetros de salida
            SqlParameter nevaluaciones = null;

            Models.FormacionDemandadaSelect1 oFDS1 = null;

            IDataReader dr = null;
            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(ParameterDirection.Input, enumDBFields.idficepi, desde.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.desde, desde.ToString()),
                    Param(ParameterDirection.Input, enumDBFields.hasta, hasta.ToString()),                                                            
                    Param(ParameterDirection.Input, enumDBFields.idcolectivo, (t941_idcolectivo==null)?null:t941_idcolectivo.ToString()),

                    nevaluaciones = Param(ParameterDirection.Output, enumDBFields.nevaluaciones, "0")
                    
                   
                };
                dr = cDblib.DataReader("PRO_FORMACIONDEMANDADA_GET", dbparams);
                while (dr.Read())
                {

                    oFDS1 = new Models.FormacionDemandadaSelect1();

                    if (!Convert.IsDBNull(dr["evaluador"]))
                        oFDS1.Evaluador  = dr["evaluador"].ToString();

                    if (!Convert.IsDBNull(dr["evaluado"]))
                        oFDS1.Evaluado = dr["evaluado"].ToString();

                    if (!Convert.IsDBNull(dr["t930_idvaloracion"]))
                        oFDS1.T930_idvaloracion = int.Parse(dr["t930_idvaloracion"].ToString());

                    if (!Convert.IsDBNull(dr["t930_formacion"]))
                        oFDS1.Formacion = dr["t930_formacion"].ToString();

                    if (!Convert.IsDBNull(dr["t934_idmodeloformulario"]))
                        oFDS1.idformulario = int.Parse(dr["t934_idmodeloformulario"].ToString());

                    oFormacionDemandada.FormacionDemandadaS1.Add(oFDS1);
                    
                }

                return oFormacionDemandada;
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
                    //Hasta no hacer el dispose, no obtienen valor los parámetros de salida
                    oFormacionDemandada.Nevaluciones = Int32.Parse(nevaluaciones.Value.ToString());
                }
            }
        }



        //internal List<Models.FormacionDemandada> catFormacionDemandadaExcel(Int32 idficepi, Int32 desde, Int32 hasta, Nullable<Int16> t941_idcolectivo)
        //{
        //    Models.FormacionDemandada oFormacionDemandada = new Models.FormacionDemandada();
        //   // List<Models.FormacionDemandada> lista = new List<Models.FormacionDemandada>();

        //    //Parámetros de salida
        //    SqlParameter nevaluaciones = null;

        //    Models.FormacionDemandadaSelect1 oFDS1 = null;

        //    IDataReader dr = null;
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[5] {
        //            Param(ParameterDirection.Input, enumDBFields.idficepi, desde.ToString()),
        //            Param(ParameterDirection.Input, enumDBFields.desde, desde.ToString()),
        //            Param(ParameterDirection.Input, enumDBFields.hasta, hasta.ToString()),                                                            
        //            Param(ParameterDirection.Input, enumDBFields.idcolectivo, (t941_idcolectivo==null)?null:t941_idcolectivo.ToString()),

        //            nevaluaciones = Param(ParameterDirection.Output, enumDBFields.nevaluaciones, "0")
                    
                   
        //        };
        //        dr = cDblib.DataReader("PRO_FORMACIONDEMANDADA_GET", dbparams);
        //        while (dr.Read())
        //        {

        //            oFDS1 = new Models.FormacionDemandadaSelect1();

        //            if (!Convert.IsDBNull(dr["evaluador"]))
        //                oFDS1.Evaluador = dr["evaluador"].ToString();

        //            if (!Convert.IsDBNull(dr["evaluado"]))
        //                oFDS1.Evaluado = dr["evaluado"].ToString();

        //            if (!Convert.IsDBNull(dr["t930_idvaloracion"]))
        //                oFDS1.T930_idvaloracion = int.Parse(dr["t930_idvaloracion"].ToString());

        //            if (!Convert.IsDBNull(dr["t930_formacion"]))
        //                oFDS1.Formacion = dr["t930_formacion"].ToString();

        //            if (!Convert.IsDBNull(dr["t934_idmodeloformulario"]))
        //                oFDS1.idformulario = int.Parse(dr["t934_idmodeloformulario"].ToString());

        //            oFormacionDemandada.FormacionDemandadaS1.Add(oFDS1);

        //        }

        //        return oFormacionDemandada;
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
        //            //Hasta no hacer el dispose, no obtienen valor los parámetros de salida
        //            oFormacionDemandada.Nevaluciones = Int32.Parse(nevaluaciones.Value.ToString());
        //        }
        //    }
        //}


        #region funciones publicas

        #endregion

        #region funciones privadas
        private SqlParameter Param(ParameterDirection paramDirection,enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            
            switch (dbField)
            {

                case enumDBFields.desde:
                    paramName = "@desde";
                    paramType = SqlDbType.Int;
                    paramSize = 3;
                    break;

                case enumDBFields.hasta:
                    paramName = "@hasta";
                    paramType = SqlDbType.Int;
                    paramSize = 3;
                    break;

                case enumDBFields.idcolectivo:
                    paramName = "@idcolectivo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.nevaluaciones:
                    paramName = "@nevaluaciones";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
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