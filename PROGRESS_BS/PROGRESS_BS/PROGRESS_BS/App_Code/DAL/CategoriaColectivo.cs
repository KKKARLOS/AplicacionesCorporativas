using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using IB.Progress.Models;
using IB.Progress.Shared;



namespace IB.Progress.DAL
{

    internal class CategoriaColectivo
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            t935_idcategoriaprofesional = 1,
            t941_idcolectivo = 2            
        }

        internal CategoriaColectivo(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        public CategoriaColectivo()
        {

            //lo dejo pero de momento no se usa
        }

        #endregion

        #region funciones publicas

        /// <summary>
        /// Obtiene el catálogo de la pantalla de administración "Perfiles"
        /// </summary>
        /// <returns></returns>
        internal Models.CategoriaColectivo Catalogo()
        {

            Models.CategoriaColectivo oCategoriaColectivo = new Models.CategoriaColectivo();           
            Models.Colectivo oColectivo = null;
            Models.Categoria oCategoria = null;

            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0];

                dr = cDblib.DataReader("PRO_CATEGORIACOLECTIVO_CAT", dbparams);

                //Select 1
                while (dr.Read())
                {
                    oColectivo = new Models.Colectivo();
                    
                    if (!Convert.IsDBNull(dr["t941_idcolectivo"]))
                        oColectivo.t941_idcolectivo = short.Parse(dr["t941_idcolectivo"].ToString());


                    if (!Convert.IsDBNull(dr["t941_denominacion"]))
                        oColectivo.t941_denominacion= dr["t941_denominacion"].ToString();

                    oCategoriaColectivo.Select1.Add(oColectivo);
                }

                //Select 2
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oCategoria = new Models.Categoria();

                        if (!Convert.IsDBNull(dr["t935_idcategoriaprofesional"]))
                            oCategoria.T935_idcategoriaprofesional = short.Parse(dr["t935_idcategoriaprofesional"].ToString());
                        

                        if (!Convert.IsDBNull(dr["t935_denominacion"]))
                            oCategoria.T935_denominacion= dr["t935_denominacion"].ToString();

                        if (!Convert.IsDBNull(dr["t941_idcolectivo"]))
                            oCategoria.T941_idcolectivoColectivo= dr["t941_idcolectivo"].ToString();

                        
                        oCategoriaColectivo.Select2.Add(oCategoria);
                    }

                }

                return oCategoriaColectivo;

            }
            catch (Exception ex)
            {
                throw new IBException(102, "Ocurrió un error obteniendo los datos de las categorías.", ex);
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



        internal int Update(int t935_idcategoriaprofesional, int t941_idcolectivo)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.t935_idcategoriaprofesional, t935_idcategoriaprofesional.ToString()),
                    Param(enumDBFields.t941_idcolectivo, t941_idcolectivo.ToString())	
				
                };

                return (int)cDblib.Execute("PRO_CATEGORIACOLECTIVO_UPD", dbparams);
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
                case enumDBFields.t935_idcategoriaprofesional:
                    paramName = "@t935_idcategoriaprofesional";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t941_idcolectivo:
                    paramName = "@t941_idcolectivo";
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
