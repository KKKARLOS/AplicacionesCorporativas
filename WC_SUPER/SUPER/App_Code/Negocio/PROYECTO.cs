using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Datos;
//Para usar el StringBuilder
using System.Text;
using System.Text.RegularExpressions;
//Para el ArrayList
using System.Collections;
//Para el List
using System.Collections.Generic;


namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for Proyecto
    /// </summary>
    public partial class PROYECTO
    {
        #region Propiedades y Atributos

        private string _t377_denominacion;
        public string t377_denominacion
        {
            get { return _t377_denominacion; }
            set { _t377_denominacion = value; }
        }

        private string _t302_denominacion;
        public string t302_denominacion
        {
            get { return _t302_denominacion; }
            set { _t302_denominacion = value; }
        }

        private string _t323_denominacion;
        public string t323_denominacion
        {
            get { return _t323_denominacion; }
            set { _t323_denominacion = value; }
        }

        private string _t307_denominacion;
        public string t307_denominacion
        {
            get { return _t307_denominacion; }
            set { _t307_denominacion = value; }
        }
        private bool _t301_externalizable;
        public bool t301_externalizable
        {
            get { return _t301_externalizable; }
            set { _t301_externalizable = value; }
        }

        private int? _t314_idusuario_SAT;
        public int? t314_idusuario_SAT
        {
            get { return _t314_idusuario_SAT; }
            set { _t314_idusuario_SAT = value; }
        }
        private int? _t314_idusuario_SAA;
        public int? t314_idusuario_SAA
        {
            get { return _t314_idusuario_SAA; }
            set { _t314_idusuario_SAA = value; }
        }
        private string _soporte_titular;
        public string soporte_titular
        {
            get { return _soporte_titular; }
            set { _soporte_titular = value; }
        }
        private string _soporte_alternativo;
        public string soporte_alternativo
        {
            get { return _soporte_alternativo; }
            set { _soporte_alternativo = value; }
        }
        public int t001_idficepi_SAT { get; set; }

        private int _t055_idcalifOCFA;
        public int t055_idcalifOCFA
        {
            get { return _t055_idcalifOCFA; }
            set { _t055_idcalifOCFA = value; }
        }
        private string _t055_denominacion;
        public string t055_denominacion
        {
            get { return _t055_denominacion; }
            set { _t055_denominacion = value; }
        }
        private string _t316_denominacion;
        public string t316_denominacion
        {
            get { return _t316_denominacion; }
            set { _t316_denominacion = value; }
        }
        #endregion

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T301_PROYECTO junto con datos complementarios
        /// y las descripciones de las foreng keys,
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static PROYECTO Obtener(SqlTransaction tr, int t301_idproyecto)
        {
            PROYECTO o = new PROYECTO();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PROYECTO_SD", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTO_SD", aParam);

            if (dr.Read())
            {
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t301_estado"] != DBNull.Value)
                    o.t301_estado = (string)dr["t301_estado"];
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];
                if (dr["t301_descripcion"] != DBNull.Value)
                    o.t301_descripcion = (string)dr["t301_descripcion"];
                if (dr["t302_idcliente_proyecto"] != DBNull.Value)
                    o.t302_idcliente_proyecto = int.Parse(dr["t302_idcliente_proyecto"].ToString());
                if (dr["t301_fcreacion"] != DBNull.Value)
                    o.t301_fcreacion = (DateTime)dr["t301_fcreacion"];
                if (dr["t306_idcontrato"] != DBNull.Value)
                    o.t306_idcontrato = int.Parse(dr["t306_idcontrato"].ToString());
                if (dr["t307_idhorizontal"] != DBNull.Value)
                    o.t307_idhorizontal = int.Parse(dr["t307_idhorizontal"].ToString());
                if (dr["t320_idtipologiaproy"] != DBNull.Value)
                    o.t320_idtipologiaproy = byte.Parse(dr["t320_idtipologiaproy"].ToString());
                if (dr["t323_idnaturaleza"] != DBNull.Value)
                    o.t323_idnaturaleza = int.Parse(dr["t323_idnaturaleza"].ToString());
                if (dr["t316_idmodalidad"] != DBNull.Value)
                    o.t316_idmodalidad = byte.Parse(dr["t316_idmodalidad"].ToString());
                if (dr["t316_denominacion"] != DBNull.Value)
                    o.t316_denominacion = (string)dr["t316_denominacion"];
                if (dr["t301_fiprev"] != DBNull.Value)
                    o.t301_fiprev = (DateTime)dr["t301_fiprev"];
                if (dr["t301_ffprev"] != DBNull.Value)
                    o.t301_ffprev = (DateTime)dr["t301_ffprev"];
                if (dr["t301_categoria"] != DBNull.Value)
                    o.t301_categoria = (string)dr["t301_categoria"];
                if (dr["t301_modelotarif"] != DBNull.Value)
                    o.t301_modelotarif = (string)dr["t301_modelotarif"]; 
                if (dr["t301_modelocoste"] != DBNull.Value)
                    o.t301_modelocoste = (string)dr["t301_modelocoste"];
                if (dr["t377_denominacion"] != DBNull.Value)
                    o.t377_denominacion = (string)dr["t377_denominacion"];
                if (dr["t302_denominacion"] != DBNull.Value)
                    o.t302_denominacion = (string)dr["t302_denominacion"];
                if (dr["t323_denominacion"] != DBNull.Value)
                    o.t323_denominacion = (string)dr["t323_denominacion"];
                if (dr["t307_denominacion"] != DBNull.Value)
                    o.t307_denominacion = (string)dr["t307_denominacion"];
                if (dr["t301_annoPIG"] != DBNull.Value)
                    o.t301_annoPIG = short.Parse(dr["t301_annoPIG"].ToString());
                if (dr["t301_pap"] != DBNull.Value)
                    o.t301_pap = (bool)dr["t301_pap"];
                if (dr["t301_pgrcg"] != DBNull.Value)
                    o.t301_pgrcg = (bool)dr["t301_pgrcg"];
                if (dr["t301_esreplicable"] != DBNull.Value)
                    o.t301_esreplicable = (bool)dr["t301_esreplicable"];
                if (dr["t301_externalizable"] != DBNull.Value)
                    o.t301_externalizable = (bool)dr["t301_externalizable"];
                if (dr["t001_idficepi_SAT"] != DBNull.Value)
                    o.t001_idficepi_SAT = int.Parse(dr["t001_idficepi_SAT"].ToString());
                if (dr["t314_idusuario_SAT"] != DBNull.Value)
                    o.t314_idusuario_SAT = int.Parse(dr["t314_idusuario_SAT"].ToString());
                if (dr["t314_idusuario_SAA"] != DBNull.Value)
                    o.t314_idusuario_SAA = int.Parse(dr["t314_idusuario_SAA"].ToString());
                if (dr["soporte_titular"] != DBNull.Value)
                    o.soporte_titular = (string)dr["soporte_titular"];
                if (dr["soporte_alternativo"] != DBNull.Value)
                    o.soporte_alternativo = (string)dr["soporte_alternativo"];

                if (dr["t301_mesesprevgar"] != DBNull.Value)
                    o.t301_mesesprevgar = short.Parse(dr["t301_mesesprevgar"].ToString());
                if (dr["t301_activagar"] != DBNull.Value)
                    o.t301_activagar = (bool)dr["t301_activagar"];
                if (dr["t301_iniciogar"] != DBNull.Value)
                    o.t301_iniciogar = (DateTime)dr["t301_iniciogar"];
                if (dr["t301_fingar"] != DBNull.Value)
                    o.t301_fingar = (DateTime)dr["t301_fingar"];

                if (dr["t055_idcalifOCFA"] != DBNull.Value)
                    o.t055_idcalifOCFA = int.Parse(dr["t055_idcalifOCFA"].ToString());
                if (dr["t055_denominacion"] != DBNull.Value)
                    o.t055_denominacion = (string)dr["t055_denominacion"];
               
                if (dr["t195_idlineaoferta"] != DBNull.Value)
                    o.t195_idlineaoferta = int.Parse(dr["t195_idlineaoferta"].ToString());
                if (dr["t195_denominacion"] != DBNull.Value)
                    o.t195_denominacion = (string)dr["t195_denominacion"];

                if (dr["t323_coste"] != DBNull.Value)
                    o.t323_coste = (bool)dr["t323_coste"];
                


            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PROYECTO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        private static bool AccesoTotal(string slFiguras)
        {
            bool bRes = false;
            slFiguras += ",";
            if (slFiguras.Contains("R,") || slFiguras.Contains("C,") || slFiguras.Contains("I,") || slFiguras.Contains("OT,") || slFiguras.Contains("A,"))
                bRes = true;

            return bRes;
        }
        private static bool AccesoTotalEscritura(string slFiguras)
        {
            bool bRes = false;
            slFiguras += ",";
            if (slFiguras.Contains("R,") || slFiguras.Contains("C,") || slFiguras.Contains("OT,") || slFiguras.Contains("A,"))
                bRes = true;

            return bRes;
        }
        /// <summary>
        /// Comprueba si alguna de las figuras de la lista da acceso a la estructura del proyecto
        /// Hasta aqui se llega si ninguna figura de nodo, supernodo u oficina técnica da acceso al proyecto
        /// por lo que las figuras son las que se extraen del subnodo y del propio proyecto
        /// Esas figuras son R->responsable, C->consultador, J-> jefe proyecto dotado, X-> jefe proyecto no dotado, A-> administrador
        /// </summary>
        private static bool AccesoTotalProyecto(string slFiguras)
        {
            bool bRes = false;
            slFiguras += ",";
            if (slFiguras.Contains("R,") || slFiguras.Contains("C,") || slFiguras.Contains("J,") || slFiguras.Contains("X,") || slFiguras.Contains("A,"))
                bRes = true;

            return bRes;
        }
          
        public static SqlDataReader ObtenerDatosConsTecnico(int nEmpleado, int nTecnico, DateTime dtFechaFesde, DateTime dtFechaHasta)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);//Usuario que realiza la consulta
            aParam[1] = new SqlParameter("@Tecnico", SqlDbType.Int, 4);//usuario sobre el que se realiza la consulta
            aParam[2] = new SqlParameter("@fDesde", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@fHasta", SqlDbType.SmallDateTime, 4);

            aParam[0].Value = nEmpleado;
            aParam[1].Value = nTecnico;
            aParam[2].Value = dtFechaFesde;
            aParam[3].Value = dtFechaHasta;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO", aParam);
        }
        public static SqlDataReader ObtenerDatosConsTecnicoTotales(int nTecnico, DateTime dtFechaFesde, DateTime dtFechaHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);//usuario sobre el que se realiza la consulta
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.SmallDateTime, 4);
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.SmallDateTime, 4);

            aParam[0].Value = nTecnico;
            aParam[1].Value = dtFechaFesde;
            aParam[2].Value = dtFechaHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_TOTAL", aParam);
        }
        public static SqlDataReader ObtenerDatosConsProyecto(int nEmpleado, int nPSN, string dtFesde, string dtHasta, bool bEsSoloRtpt)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@dtFechaDesde", SqlDbType.DateTime, 8);
            aParam[3] = new SqlParameter("@dtFechaHasta", SqlDbType.DateTime, 8);
            aParam[4] = new SqlParameter("@bEsSoloRtpt", SqlDbType.Bit, 1);

            aParam[0].Value = nEmpleado;
            aParam[1].Value = nPSN;
            aParam[2].Value = dtFesde;
            aParam[3].Value = dtHasta;
            aParam[4].Value = bEsSoloRtpt;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSPROYECTO_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOSPROYECTO", aParam);
        }

        public static DataSet ObtenerDatosConsProyectoDS(int nEmpleado, int nProyecto, int nCodUne, string dtFechaFesde, string dtFechaHasta,
                                                         string sMotivo, string sEstado, string sPreciocerrado)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@num_empleado", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@num_proyecto", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@cod_une", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@FechaDesde", SqlDbType.VarChar, 10);
            aParam[4] = new SqlParameter("@FechaHasta", SqlDbType.VarChar, 10);
            aParam[5] = new SqlParameter("@Motivo", SqlDbType.Char, 1);
            aParam[6] = new SqlParameter("@sEstado", SqlDbType.Char, 2);
            aParam[7] = new SqlParameter("@nPreciocerrado", SqlDbType.Bit, 1);

            aParam[0].Value = nEmpleado;
            aParam[1].Value = nProyecto;
            aParam[2].Value = nCodUne;
            aParam[3].Value = dtFechaFesde;
            aParam[4].Value = dtFechaHasta;
            aParam[5].Value = sMotivo;
            aParam[6].Value = sEstado;
            aParam[7].Value = byte.Parse(sPreciocerrado);

            return SqlHelper.ExecuteDataset("PSP_CONSUMOSPROYECTO", aParam);
        }
        //Para exportaciones masivas a Excel
        //public static SqlDataReader GetConsumosProf(DateTime dtFechaFesde, DateTime dtFechaHasta, string slProfesionales)
        //{
        //    SqlParameter[] aParam = new SqlParameter[3];
        //    aParam[0] = new SqlParameter("@fDesde", SqlDbType.SmallDateTime, 4);
        //    aParam[0].Value = dtFechaFesde;
        //    aParam[1] = new SqlParameter("@fHasta", SqlDbType.SmallDateTime, 4);
        //    aParam[1].Value = dtFechaHasta;
        //    aParam[2] = new SqlParameter("@sProfesionales", SqlDbType.VarChar, 8000);
        //    aParam[2].Value = slProfesionales;
        //    //No hace falta discriminar pues en la lista de profesionales ya se ha aplicado el filtro de cuales son accesibles por el usuario
        //    //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
        //    //    return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO_ADMIN", aParam);
        //    //else
        //    //    return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO_USU", aParam);
        //    return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO", aParam);
        //}
        public static SqlDataReader GetConsumosProy(DateTime dtFechaFesde, DateTime dtFechaHasta, string slProyectos)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@FechaDesde", SqlDbType.SmallDateTime, 4);
            aParam[0].Value = dtFechaFesde;
            aParam[1] = new SqlParameter("@FechaHasta", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dtFechaHasta;
            aParam[2] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[2].Value = slProyectos;
            //No hace falta discriminar pues en la lista de proyectos ya se ha aplicado el filtro de cuales son accesibles por el usuario
            //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            //    return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO_ADMIN", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_MASIVO_USU", aParam);
            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_PROYECTO_MASIVO", aParam);
        }
        public static DataSet GetConsumosProyDS(DateTime dtFechaFesde, DateTime dtFechaHasta, string slProyectos)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@FechaDesde", SqlDbType.SmallDateTime, 4, dtFechaFesde),
                ParametroSql.add("@FechaHasta", SqlDbType.SmallDateTime, 4, dtFechaHasta),
                ParametroSql.add("@sProyectos", SqlDbType.VarChar, 8000, slProyectos)
            };

            return SqlHelper.ExecuteDataset("SUP_CONSUMOS_PROYECTO_MASIVO", aParam);
        }


        public static string ObtenerPTs(int nIdPE)
        {
            StringBuilder strBuilder = new StringBuilder();
            string sCod;
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nIdPE;

            //SqlDataReader rdr = SqlHelper.ExecuteSqlDataReader("PSP_PT_SByCod_une_Num_proyecto", aParam);
            SqlDataReader rdr = SqlHelper.ExecuteSqlDataReader("SUP_PT_SByPSN", aParam);
            while (rdr.Read())
            {
                sCod = rdr["t331_idpt"].ToString();
                strBuilder.Append(sCod);
                strBuilder.Append("@#@");
            }
            rdr.Close();
            rdr.Dispose();
            return strBuilder.ToString();
        }
        /// <summary>
        /// 
        /// Obtiene un datareader de proyecto técnicos del proyecto económico seleccionado.
        /// </summary>
        public static SqlDataReader ObtenerPTs2(int nPSN)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;

            return SqlHelper.ExecuteSqlDataReader("SUP_PT_SByPSN", aParam);
        }

        public static string flEsSoloRtpt(SqlTransaction tr, int iT305IdProy, int iUser)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = iT305IdProy;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = iUser;

            if (tr == null)
                return Convert.ToString(SqlHelper.ExecuteScalar("SUP_PROY_ES_RTPT", aParam));
            else
                return Convert.ToString(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROY_ES_RTPT", aParam));
        }
        public static string flGetNumProy(SqlTransaction tr, int iPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = iPE;

            if (tr == null)
                return Convert.ToString(SqlHelper.ExecuteScalar("SUP_PROY_GET_NUM", aParam));
            else
                return Convert.ToString(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROY_GET_NUM", aParam));
        }
        public static string GetNombre(SqlTransaction tr, int iPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = iPE;

            if (tr == null)
                return Convert.ToString(SqlHelper.ExecuteScalar("SUP_PROY_GETNOMBRE", aParam));
            else
                return Convert.ToString(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROY_GETNOMBRE", aParam));
        }
        public static SqlDataReader GetInstanciaContratante(SqlTransaction tr, int iPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = iPE;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GETCONTRATANTE", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROY_GETCONTRATANTE", aParam);
        }

        public static SqlDataReader fgGetDatosProy(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GET", aParam);
        }
        public static SqlDataReader fgGetDatosProy2(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GET2", aParam);
        }
        //Comprueba si se tiene permiso de acceso al proyecto
        public static SqlDataReader fgGetDatosProy5(int t314_idusuario, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
            
            return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GET5", aParam);
        }
        /// <summary>
        /// Obtiene datos de la instancia contratante y el modo de acceso del usuario
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <param name="t301_idproyecto"></param>
        /// <returns></returns>
        public static SqlDataReader GetDatosContratante(SqlTransaction tr, int t314_idusuario, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[1].Value = t301_idproyecto;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GET6", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROY_GET6", aParam);
        }
        public static SqlDataReader fgGetDatosProy3(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@entorno", SqlDbType.Char, 1);
            aParam[0].Value = Utilidades.Entorno;// System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper();
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GET3", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROY_GET3", aParam);
        }
        public static SqlDataReader fgGetDatosProy4(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GET4", aParam);
        }
        public static SqlDataReader fgGetDatosProyConLineaBase(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROY_GET_LINEABASE", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROY_GET_LINEABASE", aParam);
        }

        public static DataSet GetResumenContratante(int nIdProySubNodo, int nMesM2, int nMesM1, int nMes0, int nMesP1, int nMesP2,
                                    int nInicioAno, int nInicioProy, int nFinProy, string t422_idmoneda_VD, string t422_idmoneda_MP)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@idProyectoSubNodo", SqlDbType.Int, 4);
            aParam[0].Value = nIdProySubNodo;
            aParam[1] = new SqlParameter("@nAnoMesM2", SqlDbType.Int, 4);
            aParam[1].Value = nMesM2;
            aParam[2] = new SqlParameter("@nAnoMesM1", SqlDbType.Int, 4);
            aParam[2].Value = nMesM1;
            aParam[3] = new SqlParameter("@nAnoMes0", SqlDbType.Int, 4);
            aParam[3].Value = nMes0;
            aParam[4] = new SqlParameter("@nAnoMesP1", SqlDbType.Int, 4);
            aParam[4].Value = nMesP1;
            aParam[5] = new SqlParameter("@nAnoMesP2", SqlDbType.Int, 4);
            aParam[5].Value = nMesP2;
            aParam[6] = new SqlParameter("@nAnoMesInicioAno", SqlDbType.Int, 4);
            aParam[6].Value = nInicioAno;
            aParam[7] = new SqlParameter("@nAnoMesInicioProy", SqlDbType.Int, 4);
            aParam[7].Value = nInicioProy;
            aParam[8] = new SqlParameter("@nAnoMesFinProy", SqlDbType.Int, 4);
            aParam[8].Value = nFinProy;
            aParam[9] = new SqlParameter("@t422_idmoneda_VD", SqlDbType.VarChar, 5);//visualizacion de dato
            aParam[9].Value = t422_idmoneda_VD;
            aParam[10] = new SqlParameter("@t422_idmoneda_MP", SqlDbType.VarChar, 5);//moneda del proyecto
            aParam[10].Value = t422_idmoneda_MP;

            return SqlHelper.ExecuteDataset((t422_idmoneda_VD != null && t422_idmoneda_VD != t422_idmoneda_MP) ? "SUP_RESUMENSEGECO_C" : "SUP_RESUMENSEGECO_C_MP", aParam);
        }
        public static DataSet GetResumenRepJornadas(int nIdProySubNodo, int nMesM2, int nMesM1, int nMes0, int nMesP1, int nMesP2,
                                int nInicioAno, int nInicioProy, int nFinProy, string t422_idmoneda_VD, string t422_idmoneda_MP)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@idProyectoSubNodo", SqlDbType.Int, 4);
            aParam[0].Value = nIdProySubNodo;
            aParam[1] = new SqlParameter("@nAnoMesM2", SqlDbType.Int, 4);
            aParam[1].Value = nMesM2;
            aParam[2] = new SqlParameter("@nAnoMesM1", SqlDbType.Int, 4);
            aParam[2].Value = nMesM1;
            aParam[3] = new SqlParameter("@nAnoMes0", SqlDbType.Int, 4);
            aParam[3].Value = nMes0;
            aParam[4] = new SqlParameter("@nAnoMesP1", SqlDbType.Int, 4);
            aParam[4].Value = nMesP1;
            aParam[5] = new SqlParameter("@nAnoMesP2", SqlDbType.Int, 4);
            aParam[5].Value = nMesP2;
            aParam[6] = new SqlParameter("@nAnoMesInicioAno", SqlDbType.Int, 4);
            aParam[6].Value = nInicioAno;
            aParam[7] = new SqlParameter("@nAnoMesInicioProy", SqlDbType.Int, 4);
            aParam[7].Value = nInicioProy;
            aParam[8] = new SqlParameter("@nAnoMesFinProy", SqlDbType.Int, 4);
            aParam[8].Value = nFinProy;
            aParam[9] = new SqlParameter("@t422_idmoneda_VD", SqlDbType.VarChar, 5);//visualizacion de dato
            aParam[9].Value = t422_idmoneda_VD;
            aParam[10] = new SqlParameter("@t422_idmoneda_MP", SqlDbType.VarChar, 5);//moneda del proyecto
            aParam[10].Value = t422_idmoneda_MP;

            return SqlHelper.ExecuteDataset((t422_idmoneda_VD != null && t422_idmoneda_VD != t422_idmoneda_MP) ? "SUP_RESUMENSEGECO_J" : "SUP_RESUMENSEGECO_J_MP", aParam);
        }
        public static DataSet GetResumenRepPrecio(int nIdProySubNodo, int nMesM2, int nMesM1, int nMes0, int nMesP1, int nMesP2,
                                int nInicioAno, int nInicioProy, int nFinProy, string t422_idmoneda_VD, string t422_idmoneda_MP)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@idProyectoSubNodo", SqlDbType.Int, 4);
            aParam[0].Value = nIdProySubNodo;
            aParam[1] = new SqlParameter("@nAnoMesM2", SqlDbType.Int, 4);
            aParam[1].Value = nMesM2;
            aParam[2] = new SqlParameter("@nAnoMesM1", SqlDbType.Int, 4);
            aParam[2].Value = nMesM1;
            aParam[3] = new SqlParameter("@nAnoMes0", SqlDbType.Int, 4);
            aParam[3].Value = nMes0;
            aParam[4] = new SqlParameter("@nAnoMesP1", SqlDbType.Int, 4);
            aParam[4].Value = nMesP1;
            aParam[5] = new SqlParameter("@nAnoMesP2", SqlDbType.Int, 4);
            aParam[5].Value = nMesP2;
            aParam[6] = new SqlParameter("@nAnoMesInicioAno", SqlDbType.Int, 4);
            aParam[6].Value = nInicioAno;
            aParam[7] = new SqlParameter("@nAnoMesInicioProy", SqlDbType.Int, 4);
            aParam[7].Value = nInicioProy;
            aParam[8] = new SqlParameter("@nAnoMesFinProy", SqlDbType.Int, 4);
            aParam[8].Value = nFinProy;
            aParam[9] = new SqlParameter("@t422_idmoneda_VD", SqlDbType.VarChar, 5);//visualizacion de dato
            aParam[9].Value = t422_idmoneda_VD;
            aParam[10] = new SqlParameter("@t422_idmoneda_MP", SqlDbType.VarChar, 5);//moneda del proyecto
            aParam[10].Value = t422_idmoneda_MP;

            return SqlHelper.ExecuteDataset((t422_idmoneda_VD != null && t422_idmoneda_VD != t422_idmoneda_MP) ? "SUP_RESUMENSEGECO_P" : "SUP_RESUMENSEGECO_P_MP", aParam);
        }

        public static SqlDataReader GetResumenArbolTotales(int nIdProySubNodo, int nMesM2, int nMesM1, int nMes0, int nMesP1, int nMesP2,
                                        int nInicioAno, int nInicioProy, int nFinProy, string t422_idmoneda_VD, string t422_idmoneda_MP)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@idProyectoSubNodo", SqlDbType.Int, 4);
            aParam[0].Value = nIdProySubNodo;
            aParam[1] = new SqlParameter("@nAnoMesM2", SqlDbType.Int, 4);
            aParam[1].Value = nMesM2;
            aParam[2] = new SqlParameter("@nAnoMesM1", SqlDbType.Int, 4);
            aParam[2].Value = nMesM1;
            aParam[3] = new SqlParameter("@nAnoMes0", SqlDbType.Int, 4);
            aParam[3].Value = nMes0;
            aParam[4] = new SqlParameter("@nAnoMesP1", SqlDbType.Int, 4);
            aParam[4].Value = nMesP1;
            aParam[5] = new SqlParameter("@nAnoMesP2", SqlDbType.Int, 4);
            aParam[5].Value = nMesP2;
            aParam[6] = new SqlParameter("@nAnoMesInicioAno", SqlDbType.Int, 4);
            aParam[6].Value = nInicioAno;
            aParam[7] = new SqlParameter("@nAnoMesInicioProy", SqlDbType.Int, 4);
            aParam[7].Value = nInicioProy;
            aParam[8] = new SqlParameter("@nAnoMesFinProy", SqlDbType.Int, 4);
            aParam[8].Value = nFinProy;
            aParam[9] = new SqlParameter("@t422_idmoneda_VD", SqlDbType.VarChar, 5);//visualizacion de dato
            aParam[9].Value = t422_idmoneda_VD;
            aParam[10] = new SqlParameter("@t422_idmoneda_MP", SqlDbType.VarChar, 5);//moneda del proyecto
            aParam[10].Value = t422_idmoneda_MP;

            return SqlHelper.ExecuteSqlDataReader((t422_idmoneda_VD != null && t422_idmoneda_VD != t422_idmoneda_MP) ? "SUP_RESUMENSEGECO_TOTALES" : "SUP_RESUMENSEGECO_TOTALES_MP", aParam);
        }

        public static SqlDataReader GetResumenFijosContratante(int nMes0, int nG, Nullable<int> nS, Nullable<int> nC,
                                                               string t422_idmoneda_VD, string t422_idmoneda_MP)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idSegMes0", SqlDbType.Int, 4, nMes0);
            aParam[i++] = ParametroSql.add("@nG", SqlDbType.Int, 4, nG);
            aParam[i++] = ParametroSql.add("@nS", SqlDbType.Int, 4, nS);
            aParam[i++] = ParametroSql.add("@nC", SqlDbType.Int, 4, nC);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_VD", SqlDbType.VarChar, 5, t422_idmoneda_VD);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_MP", SqlDbType.VarChar, 5, t422_idmoneda_MP);

            return SqlHelper.ExecuteSqlDataReader((t422_idmoneda_VD != null && t422_idmoneda_VD != t422_idmoneda_MP) ? "SUP_RESUMENSEGECO_FIJOS_C" : "SUP_RESUMENSEGECO_FIJOS_C_MP", aParam);
        }
        public static SqlDataReader GetResumenFijosRepJornadas(int nMes0, int nG, Nullable<int> nS, Nullable<int> nC, 
                                                               string t422_idmoneda_VD, string t422_idmoneda_MP)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idSegMes0", SqlDbType.Int, 4, nMes0);
            aParam[i++] = ParametroSql.add("@nG", SqlDbType.Int, 4, nG);
            aParam[i++] = ParametroSql.add("@nS", SqlDbType.Int, 4, nS);
            aParam[i++] = ParametroSql.add("@nC", SqlDbType.Int, 4, nC);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_VD", SqlDbType.VarChar, 5, t422_idmoneda_VD);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_MP", SqlDbType.VarChar, 5, t422_idmoneda_MP);

            return SqlHelper.ExecuteSqlDataReader((t422_idmoneda_VD != null && t422_idmoneda_VD != t422_idmoneda_MP) ? "SUP_RESUMENSEGECO_FIJOS_J" : "SUP_RESUMENSEGECO_FIJOS_J_MP", aParam);
        }
        public static SqlDataReader GetResumenFijosRepPrecio(int nMes0, int nG, Nullable<int> nS, Nullable<int> nC,
                                                             string t422_idmoneda_VD, string t422_idmoneda_MP)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idSegMes0", SqlDbType.Int, 4, nMes0);
            aParam[i++] = ParametroSql.add("@nG", SqlDbType.Int, 4, nG);
            aParam[i++] = ParametroSql.add("@nS", SqlDbType.Int, 4, nS);
            aParam[i++] = ParametroSql.add("@nC", SqlDbType.Int, 4, nC);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_VD", SqlDbType.VarChar, 5, t422_idmoneda_VD);
            aParam[i++] = ParametroSql.add("@t422_idmoneda_MP", SqlDbType.VarChar, 5, t422_idmoneda_MP);

            return SqlHelper.ExecuteSqlDataReader((t422_idmoneda_VD != null && t422_idmoneda_VD != t422_idmoneda_MP) ? "SUP_RESUMENSEGECO_FIJOS_P" : "SUP_RESUMENSEGECO_FIJOS_P_MP", aParam);
        }

        /// <summary>
        /// Obtiene los datos de un proyecto económico cuando volvemos a una pantalla y queremos
        /// recuperar los datos del proyecto económico con el que se está trabajando
        /// </summary>
        public static SqlDataReader ObtenerDatosPSNRecuperado(int nPSN, int t314_idusuario, string sModulo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@sModulo", SqlDbType.Char, 3);
            aParam[2].Value = sModulo;
            aParam[3] = new SqlParameter("@bAdmin", SqlDbType.Bit, 1);
            aParam[3].Value = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();

            return SqlHelper.ExecuteSqlDataReader("SUP_RECUPERARPROYSUBNODO", aParam);
        }
        public static SqlDataReader ObtenerDatosPSNRecuperadoContrato(int nPSN, int t314_idusuario, string sModulo, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@sModulo", SqlDbType.Char, 3);
            aParam[2].Value = sModulo;
            aParam[3] = new SqlParameter("@bAdmin", SqlDbType.Bit, 1);
            aParam[3].Value = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();
            aParam[4] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[4].Value = t422_idmoneda;

            return SqlHelper.ExecuteSqlDataReader("SUP_RECUPERARPROYSUBNODO_CONTRATO", aParam);
        }
        public static SqlDataReader ObtenerProyectosModuloEco(int nUsuario, Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, string sCualidad)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = idNodo;
            aParam[2] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[2].Value = sEstado;
            aParam[3] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[3].Value = sCategoria;
            aParam[4] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[4].Value = idCliente;
            aParam[5] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[5].Value = sCualidad;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOECO", aParam);
        }
        public static SqlDataReader ObtenerProyectosModuloEcoAdmin(Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, Nullable<int> idResponsable, Nullable<int> numPE, string sDesPE, string sTipoBusqueda, string sCualidad)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[0].Value = idNodo;
            aParam[1] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[1].Value = sEstado;
            aParam[2] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[2].Value = sCategoria;
            aParam[3] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[3].Value = idCliente;
            aParam[4] = new SqlParameter("@idResponsable", SqlDbType.Int, 4);
            aParam[4].Value = idResponsable;
            aParam[5] = new SqlParameter("@numPE", SqlDbType.Int, 4);
            aParam[5].Value = numPE;
            aParam[6] = new SqlParameter("@sDesPE", SqlDbType.VarChar, 70);
            aParam[6].Value = sDesPE;
            aParam[7] = new SqlParameter("@sTipoBusqueda", SqlDbType.Char, 1);
            aParam[7].Value = sTipoBusqueda;
            aParam[8] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[8].Value = sCualidad;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOECOADMIN", aParam);
        }

        public static SqlDataReader ObtenerProyectosModuloTecnico(int nUsuario, Nullable<int> idNodo, string sEstado, string sCategoria, Nullable<int> idCliente, bool bMostrarBitacoricos, string sCualidad)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[1].Value = idNodo;
            aParam[2] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[2].Value = sEstado;
            aParam[3] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[3].Value = sCategoria;
            aParam[4] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[4].Value = idCliente;
            aParam[5] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
            aParam[5].Value = bMostrarBitacoricos;
            aParam[6] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[6].Value = sCualidad;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {//Si es administrador no merece la pena investigar que figuras tiene
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOTEC_ADMIN", aParam);
            }
            else
            {
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOTEC", aParam);
            }
        }

        public static SqlDataReader ObtenerProyectos(string sModulo, Nullable<int> idNodo, string sEstado, string sCategoria, 
                                            Nullable<int> idCliente, Nullable<int> idResponsable, Nullable<int> numPE, string sDesPE, 
                                            string sTipoBusqueda, string sCualidad, Nullable<int> nContrato, Nullable<int> nHorizontal, 
                                            int nUsuario, bool bMostrarBitacoricos, bool bNoVerPIG, Nullable<int> nCNP, 
                                            Nullable<int> nCSN1P, Nullable<int> nCSN2P, Nullable<int> nCSN3P, Nullable<int> nCSN4P,
                                            bool bSoloFacturables, Nullable<int> nNaturaleza, Nullable<int> nModeloContratacion, Nullable<int> nSoporteAdm, 
                                            Nullable<int> nAnnoPIG)
        {
            SqlParameter[] aParam = null;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion() || sModulo.ToLower() == "cvt_exp")
                aParam = new SqlParameter[22];
            else
                
                if (sModulo.ToLower() == "cm" || sModulo.ToLower() == "pge_of")
                    aParam = new SqlParameter[19];

                else if(sModulo.ToLower() == "pge")
                    aParam = new SqlParameter[20];

                else
                    aParam = new SqlParameter[21];

            aParam[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            aParam[0].Value = idNodo;
            aParam[1] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            aParam[1].Value = sEstado;
            aParam[2] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            aParam[2].Value = sCategoria;
            aParam[3] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            aParam[3].Value = idCliente;
            aParam[4] = new SqlParameter("@idResponsable", SqlDbType.Int, 4);
            aParam[4].Value = idResponsable;
            aParam[5] = new SqlParameter("@numPE", SqlDbType.Int, 4);
            aParam[5].Value = numPE;
            aParam[6] = new SqlParameter("@sDesPE", SqlDbType.VarChar, 70);
            aParam[6].Value = sDesPE;
            aParam[7] = new SqlParameter("@sTipoBusqueda", SqlDbType.Char, 1);
            aParam[7].Value = sTipoBusqueda;
            aParam[8] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[8].Value = sCualidad;
            aParam[9] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            aParam[9].Value = nContrato;
            aParam[10] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            aParam[10].Value = nHorizontal;
            aParam[11] = new SqlParameter("@nCNP", SqlDbType.Int, 4);
            aParam[11].Value = nCNP;
            aParam[12] = new SqlParameter("@nCSN1P", SqlDbType.Int, 4);
            aParam[12].Value = nCSN1P;
            aParam[13] = new SqlParameter("@nCSN2P", SqlDbType.Int, 4);
            aParam[13].Value = nCSN2P;
            aParam[14] = new SqlParameter("@nCSN3P", SqlDbType.Int, 4);
            aParam[14].Value = nCSN3P;
            aParam[15] = new SqlParameter("@nCSN4P", SqlDbType.Int, 4);
            aParam[15].Value = nCSN4P;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion() || sModulo.ToLower() == "cvt_exp")
            {//Si es administrador no merece la pena investigar que figuras tiene
                aParam[16] = new SqlParameter("@bMostrarJ", SqlDbType.Bit, 1);
                aParam[16].Value = (sModulo.ToLower() == "pge" || sModulo.ToLower() == "pge_of") ? true : false;
                aParam[17] = new SqlParameter("@bSoloFacturables", SqlDbType.Bit, 1);
                aParam[17].Value = bSoloFacturables;
                aParam[18] = new SqlParameter("@nNaturaleza", SqlDbType.Int, 4);
                aParam[18].Value = nNaturaleza;
                aParam[19] = new SqlParameter("@nModeloContratacion", SqlDbType.TinyInt, 1);
                aParam[19].Value = nModeloContratacion;
                aParam[20] = new SqlParameter("@nSoporteAdm", SqlDbType.Int, 4);
                aParam[20].Value = nSoporteAdm;
                aParam[21] = new SqlParameter("@nAnnoPIG", SqlDbType.Int, 4);
                aParam[21].Value = nAnnoPIG;
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_ADMIN", aParam);
            }
            else
            {
                if (sModulo.ToLower() == "cm")
                {
                    aParam[16] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[16].Value = nUsuario;
                    aParam[17] = new SqlParameter("@nNaturaleza", SqlDbType.Int, 4);
                    aParam[17].Value = nNaturaleza;
                    aParam[18] = new SqlParameter("@nModeloContratacion", SqlDbType.TinyInt, 1);
                    aParam[18].Value = nModeloContratacion;
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_CUADROMANDO", aParam);
                }
                else if (sModulo.ToLower() == "pge")
                {
                    aParam[16] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[16].Value = nUsuario;
                    aParam[17] = new SqlParameter("@nNaturaleza", SqlDbType.Int, 4);
                    aParam[17].Value = nNaturaleza;
                    aParam[18] = new SqlParameter("@nModeloContratacion", SqlDbType.TinyInt, 1);
                    aParam[18].Value = nModeloContratacion;
                    aParam[19] = new SqlParameter("@nSoporteAdm", SqlDbType.Int, 4);
                    aParam[19].Value = nSoporteAdm;
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOECO", aParam);
                }
                else if (sModulo.ToLower() == "pge_of")
                {
                    aParam[16] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[16].Value = nUsuario;
                    aParam[17] = new SqlParameter("@nNaturaleza", SqlDbType.Int, 4);
                    aParam[17].Value = nNaturaleza;
                    aParam[18] = new SqlParameter("@nModeloContratacion", SqlDbType.TinyInt, 1);
                    aParam[18].Value = nModeloContratacion;
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_OF", aParam);
                }
                else
                {
                    aParam[16] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[16].Value = nUsuario;
                    aParam[17] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
                    aParam[17].Value = bMostrarBitacoricos;
                    aParam[18] = new SqlParameter("@nNaturaleza", SqlDbType.Int, 4);
                    aParam[18].Value = nNaturaleza;
                    aParam[19] = new SqlParameter("@nModeloContratacion", SqlDbType.TinyInt, 1);
                    aParam[19].Value = nModeloContratacion;
                    aParam[20] = new SqlParameter("@nAnnoPIG", SqlDbType.Int, 4);
                    aParam[20].Value = nAnnoPIG;

                    if (sModulo.ToLower() == "pst_bit")
                        return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_BITACORA", aParam);
                    else
                    {
                        if (bNoVerPIG)
                            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_DESGLOSE", aParam);
                        else
                            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOTEC", aParam);
                    }
                }
            }
        }

        public static SqlDataReader ObtenerProyectosByNumPE(string sModulo, int numPE, int nUsuario, bool bMostrarBitacoricos, bool bNoVerPIG, bool bSoloFacturables)
        {
            SqlParameter[] aParam = null;
            bool bEsAdminProduccion = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();

            if (bEsAdminProduccion)
                aParam = new SqlParameter[18];
            else
                if (sModulo.ToLower() == "pge")
                    aParam = new SqlParameter[12];
                else
                    aParam = new SqlParameter[13];

            aParam[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            //aParam[0].Value = null;
            aParam[1] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            //aParam[1].Value = null;
            aParam[2] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            //aParam[2].Value = null;
            aParam[3] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            //aParam[3].Value = null;
            aParam[4] = new SqlParameter("@idResponsable", SqlDbType.Int, 4);
            //aParam[4].Value = null;
            aParam[5] = new SqlParameter("@numPE", SqlDbType.Int, 4);
            aParam[5].Value = numPE;
            aParam[6] = new SqlParameter("@sDesPE", SqlDbType.VarChar, 70);
            //aParam[6].Value = null;
            aParam[7] = new SqlParameter("@sTipoBusqueda", SqlDbType.Char, 1);
            //aParam[7].Value = null;
            aParam[8] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            //aParam[8].Value = null;
            aParam[9] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            //aParam[9].Value = null;
            aParam[10] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            //aParam[10].Value = null;

            if (bEsAdminProduccion)
            {//Si es administrador no merece la pena investigar que figuras tiene
                aParam[11] = new SqlParameter("@nCNP", SqlDbType.Int, 4);
                aParam[12] = new SqlParameter("@nCSN1P", SqlDbType.Int, 4);
                aParam[13] = new SqlParameter("@nCSN2P", SqlDbType.Int, 4);
                aParam[14] = new SqlParameter("@nCSN3P", SqlDbType.Int, 4);
                aParam[15] = new SqlParameter("@nCSN4P", SqlDbType.Int, 4);
                aParam[16] = new SqlParameter("@bMostrarJ", SqlDbType.Bit, 1);
                aParam[17] = new SqlParameter("@bSoloFacturables", SqlDbType.Bit, 1);
                aParam[17].Value = bSoloFacturables;
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_ADMIN", aParam);
            }
            else
            {
                if (sModulo.ToLower() == "pge")
                {
                    aParam[11] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[11].Value = nUsuario;
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOECO", aParam);
                }
                else
                {
                    aParam[11] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[11].Value = nUsuario;
                    aParam[12] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
                    aParam[12].Value = bMostrarBitacoricos;
                    if (bNoVerPIG)
                        return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_DESGLOSE", aParam);
                    else
                        return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOTEC", aParam);
                }
            }
        }
        public static SqlDataReader ObtenerProyectosByNumPE(string sModulo, int numPE, int nUsuario, bool bMostrarBitacoricos, bool bNoVerPIG, string sCualidad, bool bSoloFacturables)
        {
            SqlParameter[] aParam = null;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                aParam = new SqlParameter[18];
            else
                if (sModulo.ToLower() == "pge")
                    aParam = new SqlParameter[12];
                else
                    aParam = new SqlParameter[13];

            aParam[0] = new SqlParameter("@idNodo", SqlDbType.Int, 4);
            //aParam[0].Value = null;
            aParam[1] = new SqlParameter("@sEstado", SqlDbType.Char, 1);
            //aParam[1].Value = null;
            aParam[2] = new SqlParameter("@sCategoria", SqlDbType.Char, 1);
            //aParam[2].Value = null;
            aParam[3] = new SqlParameter("@idCliente", SqlDbType.Int, 4);
            //aParam[3].Value = null;
            aParam[4] = new SqlParameter("@idResponsable", SqlDbType.Int, 4);
            //aParam[4].Value = null;
            aParam[5] = new SqlParameter("@numPE", SqlDbType.Int, 4);
            aParam[5].Value = numPE;
            aParam[6] = new SqlParameter("@sDesPE", SqlDbType.VarChar, 70);
            //aParam[6].Value = null;
            aParam[7] = new SqlParameter("@sTipoBusqueda", SqlDbType.Char, 1);
            //aParam[7].Value = null;
            aParam[8] = new SqlParameter("@sCualidad", SqlDbType.Char, 1);
            aParam[8].Value = sCualidad;
            aParam[9] = new SqlParameter("@nContrato", SqlDbType.Int, 4);
            //aParam[9].Value = null;
            aParam[10] = new SqlParameter("@nHorizontal", SqlDbType.Int, 4);
            //aParam[10].Value = null;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
            {//Si es administrador no merece la pena investigar que figuras tiene
                aParam[11] = new SqlParameter("@nCNP", SqlDbType.Int, 4);
                aParam[12] = new SqlParameter("@nCSN1P", SqlDbType.Int, 4);
                aParam[13] = new SqlParameter("@nCSN2P", SqlDbType.Int, 4);
                aParam[14] = new SqlParameter("@nCSN3P", SqlDbType.Int, 4);
                aParam[15] = new SqlParameter("@nCSN4P", SqlDbType.Int, 4);
                aParam[16] = new SqlParameter("@bMostrarJ", SqlDbType.Bit, 1);
                aParam[17] = new SqlParameter("@bSoloFacturables", SqlDbType.Bit, 1);
                aParam[17].Value = bSoloFacturables;
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_ADMIN", aParam);
            }
            else
            {
                if (sModulo.ToLower() == "pge")
                {
                    aParam[11] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[11].Value = nUsuario;
                    return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOECO", aParam);
                }
                else
                {
                    aParam[11] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
                    aParam[11].Value = nUsuario;
                    aParam[12] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
                    aParam[12].Value = bMostrarBitacoricos;
                    if (bNoVerPIG)
                        return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_DESGLOSE", aParam);
                    else
                        return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_MODULOTEC", aParam);
                }
            }
        }
        public static SqlDataReader ObtenerProyectosParaFacturar(int t314_idusuario, Nullable<int> t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[1].Value = t301_idproyecto;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_PARAFACTURAR", aParam);
        }

        /// <summary>
        /// Obtiene los proyectos borrables para un usuario
        /// Para que un proyecto pueda ser borrado por un usuario, deben darse las siguientes circunstancias:
        ///1ª.- Que el usuario sea responsable del proyecto, delegado o colaborador.
        ///2ª.- Que el proyecto esté en estado presupuestado o abierto.
        ///3ª.- Que si el proyecto está abierto, no tenga ningún mes cerrado, ninguna réplica, ninguna imputación en IAP, ni ningún apunte en GASVI. (Esto obliga a una doble comprobación)
        /// </summary>
        public static SqlDataReader ObtenerProyectosBorrables(int nUsuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_BORRABLES", aParam);
        }

        /// <summary>
        /// Obtiene los proyectos subnodos de un proyecto económico 
        /// </summary>
        public static SqlDataReader ObtenerProyectosSubNodo(SqlTransaction tr, int nPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPE", SqlDbType.Int, 4);
            aParam[0].Value = nPE;
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GET_PROYECTOS_SUBNODO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GET_PROYECTOS_SUBNODO", aParam);
        }

        public static SqlDataReader ObtenerProyectosProfesionales(
            //int nOpcion,
                   int t314_idusuario,
                   DateTime nDesde,
                   DateTime nHasta,
                   Nullable<int> nNivelEstructura,
                   string t301_categoria,
                   string t305_cualidad,
                   string sProyectos,
                   string sClientes,
                   string sResponsables,
                   string sNaturalezas,
                   string sHorizontal,
                   string sModeloContrato,
                   string sContrato,
                   string sIDEstructura,
                   string sSectores,
                   string sSegmentos,
                   bool bComparacionLogica,
                   bool @cOrdenacion,
                   string cTipoProfesional,
                   bool cConSinconsumos,
                   string sCNP,
                   string sCSN1P,
                   string sCSN2P,
                   string sCSN3P,
                   string sCSN4P,
                   string t422_idmoneda
               )
        {
            SqlParameter[] aParam = new SqlParameter[26];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[3].Value = nNivelEstructura;
            aParam[4] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[4].Value = t301_categoria;
            aParam[5] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[5].Value = t305_cualidad;
            aParam[6] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[6].Value = sProyectos;
            aParam[7] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[7].Value = sClientes;
            aParam[8] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[8].Value = sResponsables;
            aParam[9] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[9].Value = sNaturalezas;
            aParam[10] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[10].Value = sHorizontal;
            aParam[11] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sModeloContrato;
            aParam[12] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sContrato;
            aParam[13] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[13].Value = sIDEstructura;
            aParam[14] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSectores;
            aParam[15] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSegmentos;
            aParam[16] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[16].Value = bComparacionLogica;
            aParam[17] = new SqlParameter("@cOrdenacion", SqlDbType.Bit, 1);
            aParam[17].Value = cOrdenacion;
            aParam[18] = new SqlParameter("@cTipoProfesional", SqlDbType.Char, 10);
            aParam[18].Value = cTipoProfesional;
            aParam[19] = new SqlParameter("@cConSinconsumos", SqlDbType.Bit, 1);
            aParam[19].Value = cConSinconsumos;
            aParam[20] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCNP;
            aParam[21] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN1P;
            aParam[22] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCSN2P;
            aParam[23] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[23].Value = sCSN3P;
            aParam[24] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[24].Value = sCSN4P;
            aParam[25] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[25].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_PROFESION_PROYECTOS_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_PROFESION_PROYECTOS_USU", aParam);
        }
        public static SqlDataReader ObtenerProyectosNoCerrados(
                   int t314_idusuario,
                   int nAnoMes,
                   Nullable<int> nNivelEstructura,
                   string t301_categoria,
                   string t305_cualidad,
                   string sProyectos,
                   string sClientes,
                   string sResponsables,
                   string sNaturalezas,
                   string sHorizontal,
                   string sModeloContrato,
                   string sContrato,
                   string sIDEstructura,
                   string sSectores,
                   string sSegmentos,
                   bool bComparacionLogica,
                   string sCNP,
                   string sCSN1P,
                   string sCSN2P,
                   string sCSN3P,
                   string sCSN4P
               )
        {
            SqlParameter[] aParam = new SqlParameter[21];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nAnoMes", SqlDbType.Int, 4);
            aParam[1].Value = nAnoMes;
            aParam[2] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[2].Value = nNivelEstructura;
            aParam[3] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[3].Value = t301_categoria;
            aParam[4] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[4].Value = t305_cualidad;
            aParam[5] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[5].Value = sProyectos;
            aParam[6] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[6].Value = sClientes;
            aParam[7] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[7].Value = sResponsables;
            aParam[8] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[8].Value = sNaturalezas;
            aParam[9] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[9].Value = sHorizontal;
            aParam[10] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[10].Value = sModeloContrato;
            aParam[11] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[11].Value = sContrato;
            aParam[12] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[12].Value = sIDEstructura;
            aParam[13] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[13].Value = sSectores;
            aParam[14] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[14].Value = sSegmentos;
            aParam[15] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[15].Value = bComparacionLogica;
            aParam[16] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[16].Value = sCNP;
            aParam[17] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[17].Value = sCSN1P;
            aParam[18] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCSN2P;
            aParam[19] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN3P;
            aParam[20] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN4P;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_INF_NOCERRADOS_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_INF_NOCERRADOS_USU", aParam);
        }
        public static SqlDataReader ObtenerProduccion(
                   int nOpcion,
                   int t314_idusuario,
                   int nDesde,
                   int nHasta,
                   Nullable<int> nNivelEstructura,
                   string t301_categoria,
                   string t305_cualidad,
                   string sProyectos,
                   string sClientes,
                   string sResponsables,
                   string sNaturalezas,
                   string sHorizontal,
                   string sModeloContrato,
                   string sContrato,
                   string sIDEstructura,
                   string sSectores,
                   string sSegmentos,
                   bool bComparacionLogica,
                   string sCNP,
                   string sCSN1P,
                   string sCSN2P,
                   string sCSN3P,
                   string sCSN4P,
                   string sProduccion,
                   string t422_idmoneda
               )
        {
            SqlParameter[] aParam = new SqlParameter[25];
            aParam[0] = new SqlParameter("@nOpcion", SqlDbType.Int, 4);
            aParam[0].Value = nOpcion;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;
            aParam[2] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[2].Value = nDesde;
            aParam[3] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[3].Value = nHasta;
            aParam[4] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[4].Value = nNivelEstructura;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
            aParam[7] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[7].Value = sProyectos;
            aParam[8] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[8].Value = sClientes;
            aParam[9] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[9].Value = sResponsables;
            aParam[10] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[10].Value = sNaturalezas;
            aParam[11] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[11].Value = sHorizontal;
            aParam[12] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sModeloContrato;
            aParam[13] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[13].Value = sContrato;
            aParam[14] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[14].Value = sIDEstructura;
            aParam[15] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSectores;
            aParam[16] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[16].Value = sSegmentos;
            aParam[17] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[17].Value = bComparacionLogica;
            aParam[18] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCNP;
            aParam[19] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN1P;
            aParam[20] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN2P;
            aParam[21] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN3P;
            aParam[22] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCSN4P;
            aParam[23] = new SqlParameter("@sProduccion", SqlDbType.VarChar, 8000);
            aParam[23].Value = sProduccion;
            aParam[24] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[24].Value = t422_idmoneda;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_TOTALPRODUCIDO_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_TOTALPRODUCIDO_USU", aParam);
        }
        public static SqlDataReader ObtenerMasivoProyectos
            (
               int t314_idusuario,
               int nDesde,
               int nHasta,
               Nullable<int> nNivelEstructura,
               string t301_estado,
               string t301_categoria,
               string t305_cualidad,
               string sProyectos,
               string sClientes,
               string sResponsables,
               string sNaturalezas,
               string sHorizontal,
               string sModeloContrato,
               string sContrato,
               string sIDEstructura,
               string sSectores,
               string sSegmentos,
               bool bComparacionLogica,
               string sCNP,
               string sCSN1P,
               string sCSN2P,
               string sCSN3P,
               string sCSN4P,
               bool bMostrarBitacoricos
           )
        {
            SqlParameter[] aParam = new SqlParameter[24];

            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nNivelEstructura", SqlDbType.TinyInt, 2);
            aParam[3].Value = nNivelEstructura;
            aParam[4] = new SqlParameter("@t301_estado", SqlDbType.Char, 1);
            aParam[4].Value = t301_estado;
            aParam[5] = new SqlParameter("@t301_categoria", SqlDbType.Char, 1);
            aParam[5].Value = t301_categoria;
            aParam[6] = new SqlParameter("@t305_cualidad", SqlDbType.Char, 1);
            aParam[6].Value = t305_cualidad;
            aParam[7] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[7].Value = sProyectos;
            aParam[8] = new SqlParameter("@sClientes", SqlDbType.VarChar, 8000);
            aParam[8].Value = sClientes;
            aParam[9] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[9].Value = sResponsables;
            aParam[10] = new SqlParameter("@sNaturalezas", SqlDbType.VarChar, 8000);
            aParam[10].Value = sNaturalezas;
            aParam[11] = new SqlParameter("@sHorizontal", SqlDbType.VarChar, 8000);
            aParam[11].Value = sHorizontal;
            aParam[12] = new SqlParameter("@sModeloContrato", SqlDbType.VarChar, 8000);
            aParam[12].Value = sModeloContrato;
            aParam[13] = new SqlParameter("@sContrato", SqlDbType.VarChar, 8000);
            aParam[13].Value = sContrato;
            aParam[14] = new SqlParameter("@sIDEstructura", SqlDbType.VarChar, 8000);
            aParam[14].Value = sIDEstructura;
            aParam[15] = new SqlParameter("@sSectores", SqlDbType.VarChar, 8000);
            aParam[15].Value = sSectores;
            aParam[16] = new SqlParameter("@sSegmentos", SqlDbType.VarChar, 8000);
            aParam[16].Value = sSegmentos;
            aParam[17] = new SqlParameter("@bComparacionLogica", SqlDbType.Bit, 1);
            aParam[17].Value = bComparacionLogica;
            aParam[18] = new SqlParameter("@sCNP", SqlDbType.VarChar, 8000);
            aParam[18].Value = sCNP;
            aParam[19] = new SqlParameter("@sCSN1P", SqlDbType.VarChar, 8000);
            aParam[19].Value = sCSN1P;
            aParam[20] = new SqlParameter("@sCSN2P", SqlDbType.VarChar, 8000);
            aParam[20].Value = sCSN2P;
            aParam[21] = new SqlParameter("@sCSN3P", SqlDbType.VarChar, 8000);
            aParam[21].Value = sCSN3P;
            aParam[22] = new SqlParameter("@sCSN4P", SqlDbType.VarChar, 8000);
            aParam[22].Value = sCSN4P;
            aParam[23] = new SqlParameter("@bMostrarBitacoricos", SqlDbType.Bit, 1);
            aParam[23].Value = bMostrarBitacoricos;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSMASIVO_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSMASIVO_USU", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza el estado de un registro de la tabla T301_PROYECTO.
        /// Si el cambio es a CERRADO se actualiza el ETPR y FFPR de todas sus tareas que no estuvieran finalizadas, cerradas o anuladas
        /// con el sumatorio de los consumos IAP y la última fecha de imputación
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	26/11/2009 9:02:50
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t301_idproyecto, string t301_estado)
        {
            if (t301_estado == "C")
                DAL.PROYECTO.CierreTecnico(tr, t301_idproyecto);

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t301_estado", SqlDbType.Text, 1);
            aParam[1].Value = t301_estado;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_ESTADO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_ESTADO_U", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza la  naturaleza o el contrato de la tabla T301_PROYECTO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	19/02/2014 9:02:50
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int UpdateNatuContraCli(SqlTransaction tr, int t301_idproyecto, int t323_idnaturaleza, Nullable<int> t306_idcontrato, int t302_idcliente_proyecto)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
            aParam[1].Value = t323_idnaturaleza;
            aParam[2] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[2].Value = t306_idcontrato;
            aParam[3] = new SqlParameter("@t302_idcliente_proyecto", SqlDbType.Int, 4);
            aParam[3].Value = t302_idcliente_proyecto;

            
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_NATUCONTRACLI_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_NATUCONTRACLI_U", aParam);
        }
        /// <summary>
        /// Obtiene el estado del proyecto que se le pasa como parametro
        /// </summary>
        public static string getEstado(SqlTransaction tr, int nIdPE)
        {
            string sRes = "C";
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = nIdPE;
            object obj;
            if (tr == null)
                obj = SqlHelper.ExecuteScalar("SUP_PROYECTO_ESTADO", aParam);
            else
                obj = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTO_ESTADO", aParam);
            if (obj != null)
                sRes = obj.ToString();
            return sRes;
        }

        public static bool AccesoEnEscrituraOrdenFacturacion(SqlTransaction tr, int t314_idusuario, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ORDENFAC_ACCESOESCRITURA", aParam)) == 0) ? true : false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ORDENFAC_ACCESOESCRITURA", aParam)) == 0) ? true : false;

        }
        public static bool EsReplicableByPSN(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROYECTO_ESREPLICABLE_ByPSN", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTO_ESREPLICABLE_ByPSN", aParam)) == 0) ? false : true;

        }
        public static bool HayMesesAbiertos(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROY_MESESABIERTOS", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROY_MESESABIERTOS", aParam)) == 0) ? false : true;

        }
        /// <summary>
        /// Actualiza todas las tareas del proyecto que no estén Finalizadas, Cerradas o Anuladas
        /// ETPR = sumatorio consumos IAP
        /// FFPR = fecha último consumo IAP
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="nIdPE"></param>
        public static void CierreTecnico(SqlTransaction tr, int nIdPE)
        {
            DAL.PROYECTO.CierreTecnico(tr, nIdPE);
        }
        /// <summary>
        /// Obtiene la lista de proyectos Contratantes (C) y Replicados con getión (P) del proyecto que se pasa como parámetro
        /// Para cada uno de los proyectos obtiene la lista de meses desde el siguiente al último mes cerrado del proyecto hasta el mes de la fecha actual
        ///Se supone que no existe ningun mes abierto despues del último cerrado porque en caso contrario no se hubiera llamado a este procedimiento
        /// Para cada uno de los meses, si hay consumos IAP y no existe mes -> crear mes, traspasar IAP y cerrar mes
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="nIdPE"></param>
        public static void TraspasarIAP(SqlTransaction tr, int nIdPE, bool bRevisarMesesAbiertos)
        {
            int idAnoMesIni = 0, idAnoMesFin = Fechas.FechaAAnnomes(DateTime.Now), idPSN=-1, nSMPSN=-1;
            string sEstadoMes = "A";
            List<IB.SUPER.APP.Models.ProyectoEconomico> lstProys = new List<IB.SUPER.APP.Models.ProyectoEconomico>();
            ArrayList aMeses = new ArrayList();
            bool bHayConsumos = false;
            DataSet dsProf = null;

            if (bRevisarMesesAbiertos && PROYECTO.HayMesesAbiertos(tr, nIdPE))
            {
                throw (new Exception("Intento de cerrar un proyecto que tiene meses abiertos en instancias contratantes o replicadas con gestión."));
            }


            #region Obtengo la lista de PSNs a procesar
            SqlDataReader dr1 = SUPER.DAL.PROYECTO.GetProyectosSubnodoCualidad(tr, nIdPE, "C,P");
            while (dr1.Read())
            {
                IB.SUPER.APP.Models.ProyectoEconomico oProyectoEconomico = new IB.SUPER.APP.Models.ProyectoEconomico();
                oProyectoEconomico.t305_idproyectosubnodo = int.Parse(dr1["t305_idproyectosubnodo"].ToString());
                oProyectoEconomico.modelo_coste = dr1["t301_modelocoste"].ToString();
                oProyectoEconomico.umc_iap_nodo= int.Parse(dr1["t303_ultcierreIAP"].ToString());

                lstProys.Add(oProyectoEconomico);

            }
            dr1.Close();
            dr1.Dispose();
            #endregion
            foreach (IB.SUPER.APP.Models.ProyectoEconomico oPSN in lstProys)
            {
                idPSN = oPSN.t305_idproyectosubnodo;
                #region Obtengo los meses a procesar
                idAnoMesIni = SUPER.DAL.PROYECTOSUBNODO.GetUltimoMesCerrado(tr, idPSN);
                if (idAnoMesIni == 0)
                {//Si no existe ningún mes cerrado tomo como inicio EL SIGUIENTE AL ULTIMO MES CERRADO DEL CR DEL PROYECTO
                    idAnoMesIni = Fechas.AddAnnomes(oPSN.umc_iap_nodo, 1);
                }
                else
                {//Cojo el siguiente al último mes cerrado
                    int iAnoMesActual = Fechas.FechaAAnnomes(DateTime.Now);
                    iAnoMesActual = Fechas.AddAnnomes(iAnoMesActual, -14);
                    //A veces las replicas tienen el últimi mes cerrado hace mucho tiempo y no tiene sentido ir a buscar meses anteriores a los últimos 13 meses
                    if (idAnoMesIni < iAnoMesActual)
                        idAnoMesIni = Fechas.AddAnnomes(iAnoMesActual, 1);
                    else
                        idAnoMesIni = Fechas.AddAnnomes(idAnoMesIni, 1);
                }
                aMeses.Clear();
                for (int iAnoMes= idAnoMesIni; iAnoMes<= idAnoMesFin; iAnoMes=Fechas.AddAnnomes(iAnoMes, 1) )
                {
                    aMeses.Add(iAnoMes);
                }
                #endregion
                foreach(int iAnoMes in aMeses)
                {
                    DateTime dtIni = Fechas.AnnomesAFecha(iAnoMes);
                    DateTime dtFin = Fechas.getSigDiaUltMesCerrado(iAnoMes).AddDays(-1);

                    bHayConsumos = SUPER.Capa_Negocio.Consumo.HayImputacionesIAP(tr, idPSN, dtIni, dtFin);

                    if (bHayConsumos)
                    {
                        nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, idPSN, iAnoMes, sEstadoMes, 0, 0, false, 0, 0);
                        #region Datos Profesionales
                        dsProf = CONSPERMES.ObtenerDatosPSNaTraspasarDS(tr, idPSN, iAnoMes, "", true, false);
                        foreach (DataRow oProf in dsProf.Tables[0].Rows)
                        {
                            double nUnidades = (oPSN.modelo_coste == "J") ? double.Parse(oProf["jornadas_adaptadas"].ToString()) : double.Parse(oProf["horas_reportadas_proy"].ToString());
                            if (nUnidades != 0)
                            {
                                CONSPERMES.Insert(tr, nSMPSN,
                                                (int)oProf["t314_idusuario"],
                                                (oPSN.modelo_coste == "J") ? double.Parse(oProf["jornadas_adaptadas"].ToString()) : double.Parse(oProf["horas_reportadas_proy"].ToString()),
                                                decimal.Parse(oProf["t330_costecon"].ToString()),
                                                decimal.Parse(oProf["t330_costerep"].ToString()),
                                                (oProf["t303_idnodo"] != DBNull.Value) ? (int?)oProf["t303_idnodo"] : null,
                                                (oProf["t313_idempresa"] != DBNull.Value) ? (int?)oProf["t313_idempresa"] : null);
                            }
                        }
                        dsProf.Dispose();

                        #endregion

                        SEGMESPROYECTOSUBNODO.UpdateTraspasoIAP(tr, nSMPSN, true);
                        SEGMESPROYECTOSUBNODO.Cerrar(tr, nSMPSN);
                    }
                }
            }
        }

        #region Soporte Administrativo
        /// <summary>
        /// Se utiliza para cargar los datos de la subpestaña Soporte Administrativo
        /// del la pestaña Control de la pantalla de detalle de proyecto económico
        /// </summary>
        public static SqlDataReader getSoporteAdministrativo(SqlTransaction tr, int nIdPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = nIdPE;
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GET_SOPORTE_ADMINISTRATIVO_PE", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GET_SOPORTE_ADMINISTRATIVO_PE", aParam);
        }
        /// <summary>
        /// Actualiza los campos del soporte administrativo de la tabla T301_PROYECTO.
        /// </summary>
        /// <history>
        /// 	Creado por [Mikel]	14/12/2010
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int UpdateSoporte(SqlTransaction tr, int t301_idproyecto, byte t301_externalizable,
                                        Nullable<int> t314_idusuario_SAT, Nullable<int> t314_idusuario_SAA, Nullable<int> idCal)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@t301_externalizable", SqlDbType.Bit, 1);
            aParam[1].Value = t301_externalizable;
            aParam[2] = new SqlParameter("@t314_idusuario_SAT", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario_SAT;
            aParam[3] = new SqlParameter("@t314_idusuario_SAA", SqlDbType.Int, 4);
            aParam[3].Value = t314_idusuario_SAA;
            aParam[4] = new SqlParameter("@t066_idcal", SqlDbType.Int, 4);
            aParam[4].Value = idCal;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_SOPORTE_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_SOPORTE_U", aParam);
        }
        /// <summary>
        /// Actualiza los campos del usuario de soporte administrativo de la tabla T301_PROYECTO.
        /// </summary>
        /// <history>
        /// 	Creado por [Mikel]	13/01/2011
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int UpdateUSA(SqlTransaction tr, int t301_idproyecto, string sTipo, int nIdUser)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;
            aParam[1] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[1].Value = sTipo;
            aParam[2] = new SqlParameter("@nIdUser", SqlDbType.Int, 4);
            aParam[2].Value = nIdUser;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_USA_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_USA_U", aParam);
        }
        /// <summary>
        /// Actualiza el cualificador de la pestaña de control
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int UpdateCualificador(SqlTransaction tr, int t301_idproyecto, Nullable<int> t055_idcalifOCFA)
        {

            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto),
                ParametroSql.add("@t055_idcalifOCFA", SqlDbType.Int, 4, t055_idcalifOCFA)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_CONTROL_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_CONTROL_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de los proyectos del USA para un informe
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/01/2011 13:04:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader InformeUSA(int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_INFORME_USA_PANT", aParam);
        }
        /// <summary>
        /// Devuelve la lista de Reponsable, Delegados y Colaboradores de un proyectosubnodo
        /// </summary>
        /// <history>
        /// 	Creado por [Mikel]	16/12/2010
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader getMailResponsables(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PSN_RESPONSABLES_MAIL_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PSN_RESPONSABLES_MAIL_S", aParam);
        }
        #endregion
        #region Experiencia profesional
        public static string GetProyectosExperienciaProfesional(SqlTransaction tr, int idCliente)
        {
            StringBuilder sb = new StringBuilder();
            string sTooltip = "";
            string sTooltipExp = "";

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 900px;' cellpadding='0' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style=' width:60px;'/>");
            sb.Append("<col style=' width:240px;'/>");
            sb.Append("<col style=' width:200px;'/>");
            sb.Append("<col style=' width:200px;'/>");
            sb.Append("<col style=' width:200px;'/>");
            sb.Append("</colgroup>");
            SqlDataReader dr = SUPER.DAL.EXPPROF.GetProyectosExpProf(tr, idCliente);

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;' ");
                sb.Append(" id='" + dr["t301_idproyecto"].ToString() + "'");
                sb.Append(" idEP='" + dr["t808_idexpprof"].ToString() + "'");
                sb.Append(" cli='" + dr["t302_denominacion"].ToString() + "'");
                sb.Append(" onclick='ms(this);' ondblclick='aceptarClick(this.rowIndex)'>");
                //sb.Append("<td><nobr class='NBR W280' onmouseover='TTip(event);'>" + dr["proyecto"].ToString() + "</nobr></td>");
                
                sb.Append("<td style='text-align:right; padding-right:10px;' class='MA'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td style='text-align:left;border-left: none;' class='MA'><nobr class='NBR W230' ");
                sTooltip = "<label style=width:50px;>Cliente:</label>" + dr["t302_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN4")) sTooltip += "<br><label style=width:50px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label>" + dr["t394_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN3")) sTooltip += "<br><label style=width:50px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label>" + dr["t393_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN2")) sTooltip += "<br><label style=width:50px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label>" + dr["t392_denominacion"].ToString();
                if (Utilidades.EstructuraActiva("SN1")) sTooltip += "<br><label style=width:50px;>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label>" + dr["t391_denominacion"].ToString();
                sTooltip += "<br><label style=width:50px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString();

                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip) + "\",null,null,400)\' onMouseout=\"hideTTE()\" ");
                sb.Append(">" + dr["t301_denominacion"].ToString() + "</nobr></td>");

                sb.Append("<td><nobr class='NBR W190' onmouseover='TTip(event);'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190' onmouseover='TTip(event);'>" + dr["Responsable"].ToString() + "</nobr></td>");

                sTooltipExp = "<label style=width:80px;>AC Tecnológica:</label>" + dr["denACT"].ToString().Replace("'", " ");
                sTooltipExp += "<br><label style=width:80px;>AC Sectorial:</label>" + dr["denACS"].ToString().Replace("'", " ");
                sTooltipExp += "<br><label style=width:80px;>Descripción:</label>" + dr["t808_descripcion"].ToString().Replace("'"," ");

                sb.Append("<td><nobr class='NBR W190' ");
                sb.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltipExp) + "\",null,null,600)\' onMouseout=\"hideTTE()\" ");
                sb.Append(">" + dr["t808_denominacion"].ToString() + "</nobr></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string GetValidadores(SqlTransaction tr, int t301_idproyecto)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px; border-collapse: collapse; ' cellspacing='0' border='0'>");
            SqlDataReader dr = SUPER.DAL.PROYECTOCVT.GetValidadores(tr, t301_idproyecto);
            string sColor = "black";
            while (dr.Read())
            {
                sColor = "black";
                if ((int)dr["baja"] == 1) sColor = "gray";
                sb.Append("<tr style='noWrap:true; height:16px;' ");
                sb.Append(" id='" + dr["t001_idficepi"].ToString() + "' baja='");
                
                if ((int)dr["baja"] == 1) sb.Append("1");
                else sb.Append("0");

                sb.Append("' ondblclick='aceptarClick(this.rowIndex)'>");//onclick='ms(this)'
                sb.Append("<td><label class='texto' style='color:" + sColor + "'>" + dr["profesional"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        public static string GetValidadoresExperiencia(SqlTransaction tr, int t808_idexpprof)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 500px; border-collapse: collapse; ' cellspacing='0' border='0'>");
            SqlDataReader dr = SUPER.DAL.PROYECTOCVT.GetValidadoresExperiencia(tr, t808_idexpprof);
            string sColor = "black";
            while (dr.Read())
            {
                sColor = "black";
                if ((int)dr["baja"] == 1) sColor = "gray";
                sb.Append("<tr style='noWrap:true; height:16px;' ");
                sb.Append(" id='" + dr["t001_idficepi"].ToString() + "' baja='");

                if ((int)dr["baja"] == 1) sb.Append("1");
                else sb.Append("0");

                sb.Append("' ondblclick='aceptarClick(this.rowIndex)'>");//onclick='ms(this)'
                sb.Append("<td><label class='texto' style='color:" + sColor + "'>" + dr["profesional"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            return sb.ToString();
        }
        
        public static string Obtener(string sEstado, int iAnnoPIG, string sProyectos, string sNaturalezas)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            SqlDataReader dr = SUPER.DAL.PROYECTO.Obtener( null, sEstado, iAnnoPIG, sProyectos,  sNaturalezas );

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 450px;'>" + (char)10);
            sb.Append("<colgroup>" + (char)10);
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:340px;' />");
            sb.Append("</colgroup>" + (char)10);

            while (dr.Read())
            {
                //sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "'");
                sb.Append("<tr id='" + dr["t301_idproyecto"].ToString() + "'");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");

                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td");

                sb.Append(" style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><div class='NBR W320' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</div></td>");
                sb.Append("</tr>" + (char)10);

            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            return sb.ToString();
        }

        public static string Grabar(string sEstado, string strDatos)
        {
            string sResul = "";
            string sProy = "";
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión. " + ex.Message));
            }
            #endregion

            try
            {
                #region Proyectos para modificar el estado
                if (strDatos != "") //No se ha modificado nada 
                {
                    string[] aDatos = Regex.Split(strDatos, "///");
                    foreach (string oDatos in aDatos)
                    {
                        if (oDatos == "") continue;
                        string[] aValores = Regex.Split(oDatos, "##");
                        sProy = int.Parse(aValores[0]).ToString("#,###");
                        if (sEstado == "C")
                        {
                            int nPE = int.Parse(aValores[0]);
                            SUPER.DAL.PROYECTO.CierreTecnico(tr, nPE);
                            //Si hay consumos IAP y no existe mes -> crear mes, traspasar IAP y cerrar mes
                            PROYECTO.TraspasarIAP(tr, nPE, true);

                        }
                        SUPER.DAL.PROYECTO.Update(tr, int.Parse(aValores[0]), sEstado);
                    }
                }
                #endregion
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = Errores.mostrarError("En el proyecto económico nº " + sProy + ".", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                    throw (new Exception(sResul));
            }
            return "OK@#@";
        }
        /// <summary>
        /// Dado un PE devuelve, si existe, el código de la experiencia profesional a la que está asociado siempre que exista un profesional
        /// asociado a esa experiencia y su perfil sea una plantilla
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="iPE"></param>
        /// <returns></returns>
        public static string GetExpConPlantilla(SqlTransaction tr, int iPE)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = iPE;

            if (tr == null)
                return Convert.ToString(SqlHelper.ExecuteScalar("SUP_PROY_EXPPROFCONPLANTILLA_S", aParam));
            else
                return Convert.ToString(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROY_EXPPROFCONPLANTILLA_S", aParam));
        }

        #endregion
    }
}