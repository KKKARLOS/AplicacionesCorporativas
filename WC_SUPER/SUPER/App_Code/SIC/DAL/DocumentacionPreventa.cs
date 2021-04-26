using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for DocumentacionPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class DocumentacionPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta210_iddocupreventa = 1,
            t2_iddocumento = 2,
            ta210_descripcion = 3,
            ta210_nombrefichero = 4,
            ta210_kbytes = 5,
            ta210_fechamod = 6,
            ta204_idaccionpreventa = 7,
            ta207_idtareapreventa = 8,
            t001_idficepi_autor = 9,
            ta211_idtipodocumento = 10,
            ta210_destino = 11,
            ta210_guidprovisional = 12
        }

        internal DocumentacionPreventa(sqldblib.SqlServerSP extcDblib)
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
        /// Inserta un DocumentacionPreventa
        /// </summary>
        internal int Insert(Models.DocumentacionPreventa oDocumentacionPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[11] {
					Param(enumDBFields.t2_iddocumento, oDocumentacionPreventa.t2_iddocumento),
                    Param(enumDBFields.ta210_destino, oDocumentacionPreventa.ta210_destino),
					Param(enumDBFields.ta210_descripcion, oDocumentacionPreventa.ta210_descripcion),
					Param(enumDBFields.ta210_nombrefichero, oDocumentacionPreventa.ta210_nombrefichero),
					Param(enumDBFields.ta210_kbytes, oDocumentacionPreventa.ta210_kbytes),
					Param(enumDBFields.ta210_fechamod, oDocumentacionPreventa.ta210_fechamod),
					Param(enumDBFields.ta204_idaccionpreventa, oDocumentacionPreventa.ta204_idaccionpreventa),
					Param(enumDBFields.ta207_idtareapreventa, oDocumentacionPreventa.ta207_idtareapreventa),
					Param(enumDBFields.t001_idficepi_autor, oDocumentacionPreventa.t001_idficepi_autor),
					Param(enumDBFields.ta211_idtipodocumento, oDocumentacionPreventa.ta211_idtipodocumento),
                    Param(enumDBFields.ta210_guidprovisional, oDocumentacionPreventa.ta210_guidprovisional)
				};

                return (int)cDblib.ExecuteScalar("SIC_DOCUMENTACIONPREVENTA_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un DocumentacionPreventa a partir del id
        /// </summary>
        internal Models.DocumentacionPreventa Select(Int32 ta210_iddocupreventa)
        {
            Models.DocumentacionPreventa oDocumentacionPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta210_iddocupreventa, ta210_iddocupreventa)
				};

                dr = cDblib.DataReader("SIC_DOCUMENTACIONPREVENTA_S", dbparams);
                if (dr.Read())
                {
                    oDocumentacionPreventa = new Models.DocumentacionPreventa();
                    oDocumentacionPreventa.ta210_iddocupreventa = Convert.ToInt32(dr["ta210_iddocupreventa"]);
                    oDocumentacionPreventa.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                    oDocumentacionPreventa.ta210_destino = Convert.ToString(dr["ta210_destino"]);
                    oDocumentacionPreventa.ta210_descripcion = Convert.ToString(dr["ta210_descripcion"]);
                    oDocumentacionPreventa.ta210_nombrefichero = Convert.ToString(dr["ta210_nombrefichero"]);
                    oDocumentacionPreventa.ta210_kbytes = Convert.ToInt32(dr["ta210_kbytes"]);
                    oDocumentacionPreventa.ta210_fechamod = Convert.ToDateTime(dr["ta210_fechamod"]);
                    if (!Convert.IsDBNull(dr["ta204_idaccionpreventa"]))
                        oDocumentacionPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta207_idtareapreventa"]))
                        oDocumentacionPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oDocumentacionPreventa.t001_idficepi_autor = Convert.ToInt32(dr["t001_idficepi_autor"]);
                    oDocumentacionPreventa.ta211_idtipodocumento = Convert.ToByte(dr["ta211_idtipodocumento"]);

                    oDocumentacionPreventa.ta211_denominacion = Convert.ToString(dr["ta211_denominacion"]);
                    oDocumentacionPreventa.autor = Convert.ToString(dr["autor"]);
                    if (!Convert.IsDBNull(dr["ta207_denominacion"]))
                        oDocumentacionPreventa.ta207_denominacion = Convert.ToString(dr["ta207_denominacion"]);



                }
                return oDocumentacionPreventa;

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

        /// <summary>
        /// Actualiza un DocumentacionPreventa a partir del id
        /// </summary>
        internal int Update(Models.DocumentacionPreventa oDocumentacionPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[9] {
					Param(enumDBFields.ta210_iddocupreventa, oDocumentacionPreventa.ta210_iddocupreventa),
					Param(enumDBFields.t2_iddocumento, oDocumentacionPreventa.t2_iddocumento),
					Param(enumDBFields.ta210_destino, oDocumentacionPreventa.ta210_destino),
                    Param(enumDBFields.ta210_descripcion, oDocumentacionPreventa.ta210_descripcion),
					Param(enumDBFields.ta210_nombrefichero, oDocumentacionPreventa.ta210_nombrefichero),
					Param(enumDBFields.ta210_kbytes, oDocumentacionPreventa.ta210_kbytes),
					Param(enumDBFields.ta210_fechamod, oDocumentacionPreventa.ta210_fechamod),
					Param(enumDBFields.t001_idficepi_autor, oDocumentacionPreventa.t001_idficepi_autor),
					Param(enumDBFields.ta211_idtipodocumento, oDocumentacionPreventa.ta211_idtipodocumento)
				};

                return (int)cDblib.Execute("SIC_DOCUMENTACIONPREVENTA_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un DocumentacionPreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta210_iddocupreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta210_iddocupreventa, ta210_iddocupreventa)
				};

                return (int)cDblib.Execute("SIC_DOCUMENTACIONPREVENTA_D", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los DocumentacionPreventa
        /// </summary>
        internal List<Models.DocumentacionPreventa> Catalogo(int? ta204_idaccionpreventa, int? ta207_idtareapreventa, BLL.DocumentacionPreventa.enumOrigenEdicion enumProp)
        {
            Models.DocumentacionPreventa oDocumentacionPreventa = null;
            List<Models.DocumentacionPreventa> lst = new List<Models.DocumentacionPreventa>();
            IDataReader dr = null;

            try
            {
                string nomProc = "";
                SqlParameter[] dbparams = null;

                switch (enumProp)
                {
                    case BLL.DocumentacionPreventa.enumOrigenEdicion.accionpreventa:
                        nomProc = "SIC_DocumentacionPreventa_C5";
                        dbparams = new SqlParameter[1] {
					        Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa)
				        };
                        break;
                    case BLL.DocumentacionPreventa.enumOrigenEdicion.tareapreventa:
                        nomProc = "SIC_DocumentacionPreventa_C4";
                        dbparams = new SqlParameter[1] {
					        Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa),
				        };
                        break;

                    case BLL.DocumentacionPreventa.enumOrigenEdicion.tareasaccionpreventa:
                        nomProc = "SIC_DocumentacionPreventa_C2";
                        dbparams = new SqlParameter[1] {
					        Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa)
				        };
                        break;
                    case BLL.DocumentacionPreventa.enumOrigenEdicion.acciontareapreventa:
                        nomProc = "SIC_DocumentacionPreventa_C3";
                        dbparams = new SqlParameter[1] {
					        Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa)
				        };
                        break;
                }

                dr = cDblib.DataReader(nomProc, dbparams);

                while (dr.Read())
                {
                    oDocumentacionPreventa = new Models.DocumentacionPreventa();
                    oDocumentacionPreventa.ta210_iddocupreventa = Convert.ToInt32(dr["ta210_iddocupreventa"]);
                    oDocumentacionPreventa.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                    oDocumentacionPreventa.ta210_destino = Convert.ToString(dr["ta210_destino"]);
                    oDocumentacionPreventa.ta210_descripcion = Convert.ToString(dr["ta210_descripcion"]);
                    oDocumentacionPreventa.ta210_nombrefichero = Convert.ToString(dr["ta210_nombrefichero"]);
                    oDocumentacionPreventa.ta210_kbytes = Convert.ToInt32(dr["ta210_kbytes"]);
                    oDocumentacionPreventa.ta210_fechamod = Convert.ToDateTime(dr["ta210_fechamod"]);
                    if (!Convert.IsDBNull(dr["ta204_idaccionpreventa"]))
                        oDocumentacionPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta207_idtareapreventa"]))
                        oDocumentacionPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oDocumentacionPreventa.t001_idficepi_autor = Convert.ToInt32(dr["t001_idficepi_autor"]);
                    oDocumentacionPreventa.ta211_idtipodocumento = Convert.ToByte(dr["ta211_idtipodocumento"]);
                    oDocumentacionPreventa.ta211_denominacion = Convert.ToString(dr["ta211_denominacion"]);
                    oDocumentacionPreventa.autor = Convert.ToString(dr["autor"]);

                    if (enumProp == BLL.DocumentacionPreventa.enumOrigenEdicion.tareasaccionpreventa) {
                        oDocumentacionPreventa.ta207_denominacion = Convert.ToString(dr["ta207_denominacion"]);
                    }
                    oDocumentacionPreventa.estado = Convert.ToString(dr["estado"]);

                    lst.Add(oDocumentacionPreventa);

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


        /// <summary>
        /// Obtiene todos los DocumentacionPreventa
        /// </summary>
        internal List<Models.DocumentacionPreventa> CatalogoGUID(Guid GUID)
        {
            Models.DocumentacionPreventa oDocumentacionPreventa = null;
            List<Models.DocumentacionPreventa> lst = new List<Models.DocumentacionPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta210_guidprovisional, GUID)
				};

                dr = cDblib.DataReader("SIC_DOCUMENTACIONPREVENTA_C6", dbparams);

                while (dr.Read())
                {
                    oDocumentacionPreventa = new Models.DocumentacionPreventa();
                    oDocumentacionPreventa.ta210_iddocupreventa = Convert.ToInt32(dr["ta210_iddocupreventa"]);
                    oDocumentacionPreventa.t2_iddocumento = Convert.ToInt64(dr["t2_iddocumento"]);
                    oDocumentacionPreventa.ta210_destino = Convert.ToString(dr["ta210_destino"]);
                    oDocumentacionPreventa.ta210_descripcion = Convert.ToString(dr["ta210_descripcion"]);
                    oDocumentacionPreventa.ta210_nombrefichero = Convert.ToString(dr["ta210_nombrefichero"]);
                    oDocumentacionPreventa.ta210_kbytes = Convert.ToInt32(dr["ta210_kbytes"]);
                    oDocumentacionPreventa.ta210_fechamod = Convert.ToDateTime(dr["ta210_fechamod"]);
                    if (!Convert.IsDBNull(dr["ta204_idaccionpreventa"]))
                        oDocumentacionPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    if (!Convert.IsDBNull(dr["ta207_idtareapreventa"]))
                        oDocumentacionPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oDocumentacionPreventa.t001_idficepi_autor = Convert.ToInt32(dr["t001_idficepi_autor"]);
                    oDocumentacionPreventa.ta211_idtipodocumento = Convert.ToByte(dr["ta211_idtipodocumento"]);
                    oDocumentacionPreventa.ta211_denominacion = Convert.ToString(dr["ta211_denominacion"]);
                    oDocumentacionPreventa.autor = Convert.ToString(dr["autor"]);
                    oDocumentacionPreventa.estado = Convert.ToString(dr["estado"]);
                    oDocumentacionPreventa.ta210_guidprovisional = new Guid(Convert.ToString(dr["ta210_guidprovisional"]));

                    lst.Add(oDocumentacionPreventa);
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
                case enumDBFields.ta210_iddocupreventa:
                    paramName = "@ta210_iddocupreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t2_iddocumento:
                    paramName = "@t2_iddocumento";
                    paramType = SqlDbType.BigInt;
                    paramSize = 8;
                    break;
                case enumDBFields.ta210_descripcion:
                    paramName = "@ta210_descripcion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 250;
                    break;
                case enumDBFields.ta210_nombrefichero:
                    paramName = "@ta210_nombrefichero";
                    paramType = SqlDbType.VarChar;
                    paramSize = 100;
                    break;
                case enumDBFields.ta210_kbytes:
                    paramName = "@ta210_kbytes";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta210_fechamod:
                    paramName = "@ta210_fechamod";
                    paramType = SqlDbType.DateTime;
                    paramSize = 4;
                    break;
                case enumDBFields.ta204_idaccionpreventa:
                    paramName = "@ta204_idaccionpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta207_idtareapreventa:
                    paramName = "@ta207_idtareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_autor:
                    paramName = "@t001_idficepi_autor";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta211_idtipodocumento:
                    paramName = "@ta211_idtipodocumento";
                    paramType = SqlDbType.TinyInt;
                    paramSize = 1;
                    break;
                case enumDBFields.ta210_destino:
                    paramName = "@ta210_destino";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.ta210_guidprovisional:
                    paramName = "@ta210_guidprovisional";
                    paramType = SqlDbType.UniqueIdentifier;
                    paramSize = 16;
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
