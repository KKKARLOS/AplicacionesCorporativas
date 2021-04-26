using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;
using IB.Progress.Shared;



namespace IB.Progress.DAL
{

    internal class ColectivoFormulario
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t941_idcolectivo = 1,
            t934_idmodeloformulario = 2 ,
            datatable = 3
            
        }

        internal ColectivoFormulario(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public ColectivoFormulario()
        {

            
        }

        #endregion

        #region funciones publicas

       
        internal Models.ColectivoFormulario Catalogo()
        {

            Models.ColectivoFormulario oColectivoFormulario = new Models.ColectivoFormulario();
            Models.Colectivo oColectivo = null;
            Models.ModeloFormulario oModeloFormulario = null;

            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0];

                dr = cDblib.DataReader("PRO_COLECTIVOMODELO_CAT", dbparams);

                //Select 1
                while (dr.Read())
                {
                    oModeloFormulario = new Models.ModeloFormulario();

                    if (!Convert.IsDBNull(dr["t934_idmodeloformulario"]))
                        oModeloFormulario.T934_idmodeloformulario = short.Parse(dr["t934_idmodeloformulario"].ToString());


                    if (!Convert.IsDBNull(dr["t934_denominacion"]))
                        oModeloFormulario.T934_denominacion = dr["t934_denominacion"].ToString();

                    oColectivoFormulario.Select1.Add(oModeloFormulario);
                }
               
               

                //Select 2
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oColectivo = new Models.Colectivo();

                        if (!Convert.IsDBNull(dr["t941_idcolectivo"]))
                            oColectivo.t941_idcolectivo = short.Parse(dr["t941_idcolectivo"].ToString());


                        if (!Convert.IsDBNull(dr["t941_denominacion"]))
                            oColectivo.t941_denominacion = dr["t941_denominacion"].ToString();

                        if (!Convert.IsDBNull(dr["t934_idmodeloformulario"]))
                            oColectivo.t934_idmodeloformulario = int.Parse(dr["t934_idmodeloformulario"].ToString());


                        oColectivoFormulario.Select2.Add(oColectivo);
                    }

                }

                return oColectivoFormulario;

            }
            catch (Exception ex)
            {
                throw new IBException(102, "Ocurrió un error obteniendo los datos de la pantalla Colectivo/Formulario.", ex);
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


        internal Models.ColectivoFormulario ColectivoForzado()
        {

            Models.ColectivoFormulario oColectivoFormulario = new Models.ColectivoFormulario();
            Models.Colectivo oColectivo = null;
            
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0];

                dr = cDblib.DataReader("PRO_COLECTIVOFORZADO_CAT", dbparams);

                //Select 1
                
                    while (dr.Read())
                    {
                        oColectivo = new Models.Colectivo();

                        if (!Convert.IsDBNull(dr["t001_idficepi"]))
                            oColectivo.t001_idficepi = short.Parse(dr["t001_idficepi"].ToString());


                        if (!Convert.IsDBNull(dr["profesional"]))
                            oColectivo.profesional = dr["profesional"].ToString();

                     
                        oColectivoFormulario.SelectForzadas1.Add(oColectivo);
                    }

                
                //Select 2
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oColectivo = new Models.Colectivo();

                        if (!Convert.IsDBNull(dr["t001_idficepi"]))
                            oColectivo.t001_idficepi = short.Parse(dr["t001_idficepi"].ToString());


                        if (!Convert.IsDBNull(dr["profesional"]))
                            oColectivo.profesional = dr["profesional"].ToString();


                        if (!Convert.IsDBNull(dr["t941_idcolectivo"]))
                            oColectivo.t941_idcolectivo = short.Parse(dr["t941_idcolectivo"].ToString());


                        oColectivoFormulario.SelectForzadas2.Add(oColectivo);
                    }

                }

                //Select 3
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oColectivo = new Models.Colectivo();

                        if (!Convert.IsDBNull(dr["t941_idcolectivo"]))
                            oColectivo.t941_idcolectivo = short.Parse(dr["t941_idcolectivo"].ToString());


                        if (!Convert.IsDBNull(dr["t941_denominacion"]))
                            oColectivo.t941_denominacion = dr["t941_denominacion"].ToString();



                        oColectivoFormulario.SelectForzadas3.Add(oColectivo);
                    }

                }


                

                return oColectivoFormulario;

            }
            catch (Exception ex)
            {
                throw new IBException(102, "Ocurrió un error obteniendo los datos de la pantalla Colectivo/Formulario.", ex);
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





        internal int Update(int t941_idcolectivo, int t934_idmodeloformulario)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t941_idcolectivo, t941_idcolectivo.ToString()),
                    Param(enumDBFields.t934_idmodeloformulario, t934_idmodeloformulario.ToString())	
				
                };

                return (int)cDblib.Execute("PRO_COLECTIVOMODELO_UPD", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal string UpdateColectivoForzado(DataTable colectivoforzado)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
                    Param(enumDBFields.datatable, colectivoforzado)
                    
				};

                return (string)cDblib.Desc("PRO_COLECTIVOFORZADO_UPD", dbparams);
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
                case enumDBFields.t941_idcolectivo:
                    paramName = "@t941_idcolectivo";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t934_idmodeloformulario:
                    paramName = "@t934_idmodeloformulario";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.datatable:
                    paramName = "@TABLALISTA";
                    paramType = SqlDbType.Structured;
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
