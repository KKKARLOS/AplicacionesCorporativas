using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using IB.SUPER.IAP30.Models;

using SUPER.Capa_Negocio;
/// <summary>
/// Descripción breve de FicheroIAP
/// </summary>
namespace IB.SUPER.IAP30.DAL 
{
    public class FicheroIAP
    {
    	#region variables privadas y constructor
		private sqldblib.SqlServerSP cDblib;
		
		
        private enum enumDBFields : byte
        {
            t722_idtipo = 1,
            t001_idficepi = 2,
            t722_fichero=3
        }

        internal FicheroIAP(sqldblib.SqlServerSP extcDblib)
        {
            if(extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }
			
		#endregion
	
        #region funciones publicas

        internal List<TAREA> GetTareas()
        {
            IDataReader dr = null;
            List<TAREA> oLista = new List<TAREA>();
            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] { };

                dr = cDblib.DataReader("SUP_CARGAFICHEROIAP_VALIDAR_TAREAS", dbparams);
                while (dr.Read())
                {
                    TAREA oT = new TAREA((int)dr["t332_idtarea"],
                                        dr["t332_destarea"].ToString(),
                                        (int)dr["t331_idpt"],
                                        byte.Parse(dr["t332_estado"].ToString()),
                                        double.Parse(dr["t332_cle"].ToString()),
                                        dr["t332_tipocle"].ToString(),
                                        (dr["t332_impiap"].ToString() == "1") ? true : false,
                                        (int)dr["t305_idproyectosubnodo"],
                                        (dr["t332_fiv"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dr["t332_fiv"].ToString()),
                                        (dr["t332_ffv"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dr["t332_ffv"].ToString()),
                                        (bool)dr["t323_regjornocompleta"],
                                        (bool)dr["t331_obligaest"],
                                        byte.Parse(dr["t331_estado"].ToString()),
                                        (bool)dr["t323_regfes"],
                                        dr["t301_estado"].ToString()
                                        );
                    oLista.Add(oT);

                }
                return oLista;

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
        internal List<PROFESIONAL> GetProfesionales()
        {
            IDataReader dr = null;
            List<PROFESIONAL> oLista = new List<PROFESIONAL>();
            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] { };

                dr = cDblib.DataReader("SUP_CARGAFICHEROIAP_VALIDAR_PROFESIONALES", dbparams);
                while (dr.Read())
                {
                    PROFESIONAL oP = new PROFESIONAL((int)dr["t001_idficepi"],
                                                    (int)dr["t314_idusuario"],
                                                    dr["Profesional"].ToString(),
                                                    (dr["t303_ultcierreIAP"].ToString() == "") ? null : (int?)dr["t303_ultcierreIAP"],
                                                    bool.Parse(dr["t314_jornadareducida"].ToString()),
                                                    (dr["t303_idnodo"].ToString() == "") ? null : (int?)dr["t303_idnodo"],
                                                    double.Parse(dr["t314_horasjor_red"].ToString()),
                                                    (dr["t314_fdesde_red"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dr["t314_fdesde_red"].ToString()),
                                                    (dr["t314_fhasta_red"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dr["t314_fhasta_red"].ToString()),
                                                    bool.Parse(dr["t314_controlhuecos"].ToString()),
                                                    null,
                                                    (int)dr["t066_idcal"],
                                                    dr["t066_descal"].ToString(),
                                                    dr["t066_semlabL"].ToString() + "," + dr["t066_semlabM"].ToString() + "," + dr["t066_semlabX"].ToString() + "," + dr["t066_semlabJ"].ToString() + "," + dr["t066_semlabV"].ToString() + "," + dr["t066_semlabS"].ToString() + "," + dr["t066_semlabD"].ToString(),
                                                    dr["t001_codred"].ToString(),
                                                    (dr["T001_FECALTA"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dr["T001_FECALTA"].ToString()),
                                                    (dr["T001_FECBAJA"].ToString() == "") ? null : (DateTime?)DateTime.Parse(dr["T001_FECBAJA"].ToString())
                                                    );
                    oLista.Add(oP);

                }
                return oLista;

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
        internal int Update(byte t722_idtipo, int t001_idficepi, byte[] t722_fichero)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.t722_idtipo, t722_idtipo),
                    Param(enumDBFields.t001_idficepi, t001_idficepi),
                    Param(enumDBFields.t722_fichero, t722_fichero)
				};

                return (int)cDblib.Execute("SUP_FICHEROSMANIOBRAUSU_U", dbparams);
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
                case enumDBFields.t722_idtipo:
                    paramName = "@t722_idtipo";
					paramType = SqlDbType.TinyInt;
					paramSize = 1;
					break;
                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t722_fichero:
                    paramName = "@t722_fichero";
                    paramType = SqlDbType.VarBinary;
                    paramSize = 2147483647;
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