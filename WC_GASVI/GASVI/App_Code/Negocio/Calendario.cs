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
using System.Collections;
using System.Collections.Generic;
using GASVI.DAL;

namespace GASVI.BLL{
    /// <summary>
    /// Summary description for Calendario
    /// </summary>
    public class Calendario
    {
		#region Atributos privados 

		private int _nIdCal;
        private string _sDesCal;
        private string _sTipo;
        private int _nEstado;
        private int _nCodUne;
        private int _nPromotor;
        private int _nModificador;
        ArrayList _aHorasDia;

        private int _nSemLabL;
        private int _nSemLabM;
        private int _nSemLabX;
        private int _nSemLabJ;
        private int _nSemLabV;
        private int _nSemLabS;
        private int _nSemLabD;
        private string _sObs;

        private int _njlacv;
        private int _nhlacv;

		#endregion

		#region Propiedades públicas

		public int nIdCal
		{
			get { return _nIdCal; }
			set { _nIdCal = value; }
		}
        public string sDesCal
        {
            get { return _sDesCal; }
            set { _sDesCal = value; }
        }
        public string sTipo
        {
            get { return _sTipo; }
            set { _sTipo = value; }
        }
        public int nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }
        public int nCodUne
        {
            get { return _nCodUne; }
            set { _nCodUne = value; }
        }
        public int nPromotor
        {
            get { return _nPromotor; }
            set { _nPromotor = value; }
        }
        public int nModificador
        {
            get { return _nModificador; }
            set { _nModificador = value; }
        }
        public ArrayList aHorasDia
		{
			get { return _aHorasDia; }
			set { _aHorasDia = value; }
		}

        public int nSemLabL
        {
            get { return _nSemLabL; }
            set { _nSemLabL = value; }
        }
        public int nSemLabM
        {
            get { return _nSemLabM; }
            set { _nSemLabM = value; }
        }
        public int nSemLabX
        {
            get { return _nSemLabX; }
            set { _nSemLabX = value; }
        }
        public int nSemLabJ
        {
            get { return _nSemLabJ; }
            set { _nSemLabJ = value; }
        }
        public int nSemLabV
        {
            get { return _nSemLabV; }
            set { _nSemLabV = value; }
        }
        public int nSemLabS
        {
            get { return _nSemLabS; }
            set { _nSemLabS = value; }
        }
        public int nSemLabD
        {
            get { return _nSemLabD; }
            set { _nSemLabD = value; }
        }
        public string sObs
        {
            get { return _sObs; }
            set { _sObs = value; }
        }

        public int njlacv
        {
            get { return _njlacv; }
            set { _njlacv = value; }
        }
        public int nhlacv
        {
            get { return _nhlacv; }
            set { _nhlacv = value; }
        }

        #endregion

		#region Constructores

        public Calendario()
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
            this.aHorasDia = new ArrayList();
        }

        public Calendario(int nIdCal)
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
            this.nIdCal = nIdCal;
            this.aHorasDia = new ArrayList();
        }


        public Calendario(int nIdCal, string sDesCal, int nEstado, int nCodUne, string sDesUne)
        {
            this.nIdCal = nIdCal;
            this.sDesCal = sDesCal;
            this.nEstado = nEstado;
            this.nCodUne = nCodUne;
            this.aHorasDia = new ArrayList();
        }

        public Calendario(int nIdCal,
            string sDesCal,
            string sTipo,
            int nEstado,
            int nCodUne,
            int nSemLabL,
            int nSemLabM,
            int nSemLabX,
            int nSemLabJ,
            int nSemLabV,
            int nSemLabS,
            int nSemLabD,
            string sObs,
            int njlacv,
            int nhlacv)
        {
            this.nIdCal =  nIdCal;
            this.sDesCal =  sDesCal;
            this.sTipo =  sTipo;
            this.nEstado =  nEstado;
            this.nCodUne =  nCodUne;
            this.nSemLabL =  nSemLabL;
            this.nSemLabM =  nSemLabM;
            this.nSemLabX =  nSemLabX;
            this.nSemLabJ =  nSemLabJ;
            this.nSemLabV =  nSemLabV;
            this.nSemLabS =  nSemLabS;
            this.nSemLabD =  nSemLabD;
            this.sObs = sObs;
            this.njlacv = njlacv;
            this.nhlacv = nhlacv;
            this.aHorasDia = new ArrayList();
        }

        #endregion

        #region	Métodos públicos

        /// <summary>
        /// 
        /// Obtiene los datos generales de un calendario determinado,
        /// correspondientes a la tabla t066_CALENDARIO.
        /// </summary>
        public void Obtener()
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[0].Value = this.nIdCal;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_CALENDARIOS", aParam);

            if (dr.Read())
            {
                this.nIdCal = int.Parse(dr["t066_idcal"].ToString());
                this.sDesCal = dr["t066_descal"].ToString();
                this.sTipo = dr["t066_tipocal"].ToString();
                this.nEstado = int.Parse(dr["t066_estado"].ToString());
                this.nCodUne = int.Parse(dr["t303_idnodo"].ToString());
                this.nSemLabL = int.Parse(dr["t066_SemLabL"].ToString());
                this.nSemLabM = int.Parse(dr["t066_SemLabM"].ToString());
                this.nSemLabX = int.Parse(dr["t066_SemLabX"].ToString());
                this.nSemLabJ = int.Parse(dr["t066_SemLabJ"].ToString());
                this.nSemLabV = int.Parse(dr["t066_SemLabV"].ToString());
                this.nSemLabS = int.Parse(dr["t066_SemLabS"].ToString());
                this.nSemLabD = int.Parse(dr["t066_SemLabD"].ToString());
                this.sObs = dr["t066_obs"].ToString();
                this.njlacv = int.Parse(dr["t066_njlacv"].ToString());
                this.nhlacv = int.Parse(dr["t066_nhlacv"].ToString());
            }
            dr.Close();
            dr.Dispose();
        }

        /// <summary>
        /// 
        /// Obtiene los datos generales de un calendario determinado,
        /// correspondientes a la tabla t066_CALENDARIO, y devuelve una
        /// instancia u objeto del tipo Calendario
        /// </summary>
        public static Calendario Obtener(int nIdCal)
        {
            Calendario objCal = null;

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[0].Value = nIdCal;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_CALENDARIOS", aParam);

            if (dr.Read())
            {
                objCal = new Calendario(int.Parse(dr["t066_idcal"].ToString()),
                        dr["t066_descal"].ToString(),
                        dr["t066_tipocal"].ToString(),
                        int.Parse(dr["t066_estado"].ToString()),
                        int.Parse(dr["t303_idnodo"].ToString()),
                        int.Parse(dr["t066_SemLabL"].ToString()),
                        int.Parse(dr["t066_SemLabM"].ToString()),
                        int.Parse(dr["t066_SemLabX"].ToString()),
                        int.Parse(dr["t066_SemLabJ"].ToString()),
                        int.Parse(dr["t066_SemLabV"].ToString()),
                        int.Parse(dr["t066_SemLabS"].ToString()),
                        int.Parse(dr["t066_SemLabD"].ToString()),
                        dr["t066_obs"].ToString(),
                        int.Parse(dr["t066_njlacv"].ToString()),
                        int.Parse(dr["t066_nhlacv"].ToString()));
                    
                dr.Close();
                dr.Dispose();
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningún dato del calendario"));
            }

            return objCal;
        }

        public static SqlDataReader ObtenerCatalogoDR(int nOrden, int nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 2);
            aParam[1] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 2);

            aParam[0].Value = nOrden;
            aParam[1].Value = nAscDesc;

            return SqlHelper.ExecuteSqlDataReader("SUP_CALENDARIOSCATA", aParam);
        }

        /// <summary>
        /// 
        /// Obtiene los datos del desglose de horas de un calendario determinado,
        /// correspondientes a la tabla t067_DESGLOSECAL.
        /// </summary>
        public void ObtenerHoras(int nAnno)
        {
            DiaCal objDiaCal;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nAnno", SqlDbType.Int, 4);

            aParam[0].Value = nIdCal;
            aParam[1].Value = nAnno;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_DESGLOSECALS", aParam);

            if (dr.HasRows){
                while (dr.Read())
                {
                    objDiaCal = new DiaCal(int.Parse(dr["t066_idcal"].ToString()), (DateTime)dr["t067_dia"], double.Parse(dr["t067_horas"].ToString()), int.Parse(dr["t067_festivo"].ToString()));
                    this.aHorasDia.Add(objDiaCal);
                }
            }else{
                DateTime objDate = new DateTime(nAnno, 1, 1);
                while (objDate.Year == nAnno)
                {
                    objDiaCal = new DiaCal(this.nIdCal, objDate, 0, 0);
                    this.aHorasDia.Add(objDiaCal);
                    objDate = objDate.AddDays(1);
                }
            }
            dr.Close();
            dr.Dispose();
        }

        /// <summary>
        /// 
        /// Obtiene los datos del desglose de horas de un calendario determinado,
        /// para un rango de fechas concreto,
        /// correspondientes a la tabla t067_DESGLOSECAL.
        /// </summary>
        public void ObtenerHorasRango(DateTime dDesde, DateTime dHasta)
        {
            DiaCal objDiaCal;

            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[3] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);

            aParam[0].Value = this.nIdCal;
            aParam[1].Value = null;
            aParam[2].Value = dDesde;
            aParam[3].Value = dHasta;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_DESGLOSECALS", aParam);

            while (dr.Read())
            {
                objDiaCal = new DiaCal(int.Parse(dr["t066_idcal"].ToString()), (DateTime)dr["t067_dia"], double.Parse(dr["t067_horas"].ToString()), int.Parse(dr["t067_festivo"].ToString()));
                this.aHorasDia.Add(objDiaCal);
            }
            dr.Close();
            dr.Dispose();
        }

        /// <summary>
        /// 
        /// Graba los datos básicos del Calendario,
        /// correspondientes a la tabla t066_CALENDARIO.
        /// </summary>
        public static int Insertar(string sDesCal, int nCodUne, int nEstado, string sTipo, int nPromotor, string sObs, int njlacv, int nhlacv)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@sDesCal", SqlDbType.VarChar, 50);
            aParam[1] = new SqlParameter("@nPromotor", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[4] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@sObs", SqlDbType.Text, 2147483647);
            aParam[6] = new SqlParameter("@njlacv", SqlDbType.Int, 4);
            aParam[7] = new SqlParameter("@nhlacv", SqlDbType.Int, 4);

            aParam[0].Value = sDesCal;
            aParam[1].Value = nPromotor;
            aParam[2].Value = nCodUne;
            aParam[3].Value = sTipo;
            aParam[4].Value = nEstado;
            aParam[5].Value = sObs;
            aParam[6].Value = njlacv;
            aParam[7].Value = nhlacv;

            object nResul = SqlHelper.ExecuteScalar("SUP_CALENDARIOI", aParam);

            return int.Parse(nResul.ToString());
        }

        /// <summary>
        /// 
        /// Graba los datos básicos del Calendario,
        /// correspondientes a la tabla t066_CALENDARIO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int Insertar(SqlTransaction tr, string sDesCal, int nCodUne, int nEstado, string sTipo, int nPromotor, string sObs, int njlacv, int nhlacv)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@sDesCal", SqlDbType.VarChar, 50);
            aParam[1] = new SqlParameter("@nPromotor", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[4] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@sObs", SqlDbType.Text, 2147483647);
            aParam[6] = new SqlParameter("@njlacv", SqlDbType.Int, 4);
            aParam[7] = new SqlParameter("@nhlacv", SqlDbType.Int, 4);

            aParam[0].Value = sDesCal;
            aParam[1].Value = nPromotor;
            aParam[2].Value = nCodUne;
            aParam[3].Value = sTipo;
            aParam[4].Value = nEstado;
            aParam[5].Value = sObs;
            aParam[6].Value = njlacv;
            aParam[7].Value = nhlacv;

            object nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CALENDARIOI", aParam);

            return int.Parse(nResul.ToString());
        }

        /// <summary>
        /// 
        /// Obtiene los días festivos y no laborables de un calendario determinado,
        /// a partir de la fecha de último cierre,
        /// en la tabla t067_DESGLOSECAL.
        /// </summary>
        public static SqlDataReader ObtenerFestivos(int nIdCal, DateTime dUMC)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[0].Value = nIdCal;
            aParam[1] = new SqlParameter("@dUMC", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dUMC;

            return SqlHelper.ExecuteSqlDataReader("SUP_FESTIVOSCALS", aParam);
        }

        /// <summary>
        /// 
        /// Graba los datos correspondientes al desglose de un Calendario,
        /// en la tabla t067_DESGLOSECAL,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public void InsertarHoras(SqlTransaction tr, int nAnno)
        {
            if (aHorasDia.Count > 0)
                DiaCal.Eliminar(tr, ((DiaCal)aHorasDia[0]).nIdCal, nAnno);
                //((DiaCal)aHorasDia[0]).Eliminar(tr, nAnno);


            foreach (DiaCal oDia in aHorasDia)
            {
                //oDia.nIdCal = this.nIdCal;
                //oDia.Insertar(tr);
                DiaCal.Insertar(tr, oDia.nIdCal, oDia.dFecha, oDia.nHoras, oDia.nFestivo);
            }
        }

        /// <summary>
        /// 
        /// Inserta en la tabla t067_DESGLOSECAL el desglose horario para un nuevo
        /// calendario, tomando como referencia el ID de un calendario
        /// que se pasa como parámetro.
        /// </summary>
        public static void InsertarHorasComo(int nIdCalOriginal, int nIdCal)
        {
            //int nResul = SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
            //    "SUP_CALENDARIOCOMO", nIdCalOriginal, this.nIdCal);
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdCalOriginal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);

            aParam[0].Value = nIdCalOriginal;
            aParam[1].Value = nIdCal;

            SqlHelper.ExecuteNonQuery("SUP_CALENDARIOCOMO", aParam);
        }

        /// <summary>
        /// 
        /// Modifica los datos básicos del Calendario,
        /// correspondientes a la tabla t066_CALENDARIO.
        /// </summary>
        public static int Modificar(int nIDCal, string sDesCal, int nCodUne, int nEstado, string sTipo, int nModificador, string sObs, int njlacv, int nhlacv)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesCal", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nModificador", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[6] = new SqlParameter("@sObs", SqlDbType.Text, 2147483647);
            aParam[7] = new SqlParameter("@njlacv", SqlDbType.Int, 4);
            aParam[8] = new SqlParameter("@nhlacv", SqlDbType.Int, 4);

            aParam[0].Value = nIDCal;
            aParam[1].Value = sDesCal;
            aParam[2].Value = nCodUne;
            aParam[3].Value = nModificador;
            aParam[4].Value = nEstado;
            aParam[5].Value = sTipo;
            if (sObs == "**!") aParam[6].Value = null;
            else aParam[6].Value = sObs;
            aParam[7].Value = njlacv;
            aParam[8].Value = nhlacv;

            return SqlHelper.ExecuteNonQuery("SUP_CALENDARIOU", aParam);
        }

        /// <summary>
        /// 
        /// Modifica los datos básicos del Calendario,
        /// correspondientes a la tabla t066_CALENDARIO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int Modificar(SqlTransaction tr, int nIDCal, string sDesCal, int nCodUne, int nEstado, string sTipo, int nModificador, string sObs, int njlacv, int nhlacv)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesCal", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nModificador", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[6] = new SqlParameter("@sObs", SqlDbType.Text, 2147483647);
            aParam[7] = new SqlParameter("@njlacv", SqlDbType.Int, 4);
            aParam[8] = new SqlParameter("@nhlacv", SqlDbType.Int, 4);

            aParam[0].Value = nIDCal;
            aParam[1].Value = sDesCal;
            aParam[2].Value = nCodUne;
            aParam[3].Value = nModificador;
            aParam[4].Value = nEstado;
            aParam[5].Value = sTipo;
            if (sObs == "**!") aParam[6].Value = null;
            else aParam[6].Value = sObs;
            aParam[7].Value = njlacv;
            aParam[8].Value = nhlacv;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CALENDARIOU", aParam);
        }

        /// <summary>
        /// 
        /// Modifica los datos básicos del Calendario,
        /// correspondientes a la tabla t066_CALENDARIO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public void Modificar(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@sDesCal", SqlDbType.VarChar, 50);
            aParam[2] = new SqlParameter("@nCodUne", SqlDbType.Int, 4);
            aParam[3] = new SqlParameter("@nModificador", SqlDbType.Int, 4);
            aParam[4] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[5] = new SqlParameter("@sTipo", SqlDbType.Char, 1);
            aParam[6] = new SqlParameter("@nSemLabL", SqlDbType.Int, 4);
            aParam[7] = new SqlParameter("@nSemLabM", SqlDbType.Int, 4);
            aParam[8] = new SqlParameter("@nSemLabX", SqlDbType.Int, 4);
            aParam[9] = new SqlParameter("@nSemLabJ", SqlDbType.Int, 4);
            aParam[10] = new SqlParameter("@nSemLabV", SqlDbType.Int, 4);
            aParam[11] = new SqlParameter("@nSemLabS", SqlDbType.Int, 4);
            aParam[12] = new SqlParameter("@nSemLabD", SqlDbType.Int, 4);
            //aParam[13] = new SqlParameter("@sObs", SqlDbType.Text, 2147483647);

            aParam[0].Value = this.nIdCal;
            aParam[1].Value = this.sDesCal;
            aParam[2].Value = this.nCodUne;
            aParam[3].Value = this.nModificador;
            aParam[4].Value = this.nEstado;
            aParam[5].Value = this.sTipo;
            aParam[6].Value = this.nSemLabL;
            aParam[7].Value = this.nSemLabM;
            aParam[8].Value = this.nSemLabX;
            aParam[9].Value = this.nSemLabJ;
            aParam[10].Value = this.nSemLabV;
            aParam[11].Value = this.nSemLabS;
            aParam[12].Value = this.nSemLabD;
            //aParam[13].Value = this.sObs;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CALENDARIOU2", aParam);
        }

        /// <summary>
        /// 
        /// Elimina los datos básicos del Calendario,
        /// correspondientes a la tabla t066_CALENDARIO.
        /// Además con la delete en cascada que tiene, también
        /// se borran los datos relacionados en t067_DESGLOSECAL.
        /// </summary>
        public void Eliminar()
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[0].Value = this.nIdCal;

            SqlHelper.ExecuteNonQuery("SUP_CALENDARIOD", aParam);
        }

        public static void Eliminar(SqlTransaction tr, int nIDCalendario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[0].Value = nIDCalendario;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CALENDARIOD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CALENDARIOD", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla V_SUP_Calendario.
        /// </summary>
        /// <history>
        /// 	Creado por [dotofean]	22/11/2006 17:18:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t066_idcal, string t066_descal, Nullable<bool> t066_estado, string t066_tipocal, Nullable<int> t303_idnodo, string t303_denominacion, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t066_idcal", SqlDbType.Int, 4);
            aParam[0].Value = t066_idcal;
            aParam[1] = new SqlParameter("@t066_descal", SqlDbType.Text, 50);
            aParam[1].Value = t066_descal;
            aParam[2] = new SqlParameter("@t066_estado", SqlDbType.Bit, 1);
            aParam[2].Value = t066_estado;
            aParam[3] = new SqlParameter("@t066_tipocal", SqlDbType.Text, 1);
            aParam[3].Value = t066_tipocal;
            aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[4].Value = t303_idnodo;
            aParam[5] = new SqlParameter("@t303_denominacion", SqlDbType.Text, 40);
            aParam[5].Value = t303_denominacion;

            aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[6].Value = nOrden;
            aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[7].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_V_SUP_Calendario_C", aParam);
        }
        public static SqlDataReader CatalogoUsu(Nullable<int> nUsuario, Nullable<int> t066_idcal, string t066_descal, Nullable<bool> t066_estado, string t066_tipocal, string t303_denominacion, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@t066_idcal", SqlDbType.Int, 4);
            aParam[1].Value = t066_idcal;
            aParam[2] = new SqlParameter("@t066_descal", SqlDbType.Text, 50);
            aParam[2].Value = t066_descal;
            aParam[3] = new SqlParameter("@t066_estado", SqlDbType.Bit, 1);
            aParam[3].Value = t066_estado;
            aParam[4] = new SqlParameter("@t066_tipocal", SqlDbType.Text, 1);
            aParam[4].Value = t066_tipocal;

            aParam[5] = new SqlParameter("@t303_denominacion", SqlDbType.Text, 40);
            aParam[5].Value = t303_denominacion;

            aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[6].Value = nOrden;
            aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[7].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_Calendario_C", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los calendarios empresariales, más los del CR que se pasa por parámetro, 
        /// más los personales del IDFicepi que se pasa por parámetro.
        /// </summary>
        /// <history>
        /// 	Creado por [dotofean]	22/11/2006 17:18:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Asignacion(int t303_idnodo, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t001_idficepi;

            return SqlHelper.ExecuteSqlDataReader("SUP_CALENDARIO_ASIG", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Asigna un calendario al profesional asociado a un usuario SUPER
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	10/01/2011
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int setCalendarioProfesional(SqlTransaction tr, int t314_idusuario, int t066_idcal)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t066_idcal", SqlDbType.Int, 4);
            aParam[1].Value = t066_idcal;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROF_CAL_U", aParam);
        }
        #endregion
    }


    /// <summary>
    /// Summary description for DiaCal
    /// </summary>
    public class DiaCal{

		#region Atributos privados 

		private int _nIdCal;
        private DateTime _dFecha;
		private double _nHoras;
        private int _nFestivo;

		#endregion

		#region Propiedades públicas

		public int nIdCal
		{
			get { return _nIdCal; }
			set { _nIdCal = value; }
		}
        public DateTime dFecha
        {
            get { return _dFecha; }
            set { _dFecha = value; }
        }
        public double nHoras
        {
            get { return _nHoras; }
            set { _nHoras = value; }
        }
        public int nFestivo
        {
            get { return _nFestivo; }
            set { _nFestivo = value; }
        }

		#endregion

		#region Constructores

        public DiaCal(){

        }

        public DiaCal(int nIdCal, DateTime dFecha, double nHoras, int nFestivo){
            this.nIdCal = nIdCal;
            this.dFecha = dFecha;
            this.nHoras = nHoras;
            this.nFestivo = nFestivo;
        }

		#endregion

        #region	Métodos públicos

        /// <summary>
        /// Inserta los datos de detalle de un día, para un calendario determinado.
        /// </summary>
        public static void Insertar(SqlTransaction tr, int nIdCal, DateTime dFecha, double nHoras, int nFestivo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@dFecha", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@nHoras", SqlDbType.Real, 4);
            aParam[3] = new SqlParameter("@nFestivo", SqlDbType.Bit, 1);

            aParam[0].Value = nIdCal;
            aParam[1].Value = dFecha;
            aParam[2].Value = nHoras;
            aParam[3].Value = nFestivo;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DESGLOSECALI", aParam);
            
        }

        /// <summary>
        /// Borra todos los datos de detalle del calendario y año indicados.
        /// </summary>
        public static void Eliminar(SqlTransaction tr, int nIdCal, int nAnno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nAnno", SqlDbType.Int, 4);

            aParam[0].Value = nIdCal;
            aParam[1].Value = nAnno;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DESGLOSECALD", aParam);
        }

        /// <summary>
        /// Borra todos los datos de detalle del calendario y año indicados
        /// </summary>
        public static void Eliminar(int nIdCal, int nAnno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdCal", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nAnno", SqlDbType.Int, 4);

            aParam[0].Value = nIdCal;
            aParam[1].Value = nAnno;

            SqlHelper.ExecuteNonQuery("SUP_DESGLOSECALD", aParam);
        }

        /// <summary>
        /// Borra todos los datos de detalle del año indicado para todos los calendarios.
        /// </summary>
        public static void EliminarAnno(int nAnno, int idCal)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@idCal", SqlDbType.Int, 4);

            aParam[0].Value = nAnno;
            aParam[1].Value = @idCal;
            
            SqlHelper.ExecuteNonQuery("SUP_DESGLOSEANNOD", aParam);
        }

        #endregion

    }
}
