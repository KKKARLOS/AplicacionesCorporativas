using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;

using SUPER.Capa_Datos;
using System.Text;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for TareaRecurso
    /// </summary>
    public class TareaRecurso
    {
        #region Atributos privados

        private int _nIdTarea;
        private int _nIdRecurso;
        private double _nEte;
        private DateTime _dFfe;
        private double _nEtp;
        private DateTime _dFip;
        private DateTime _dFfp;
        private int _nIdTarifa;
        private int _nEstado;
        private string _sComentario;
        private string _sIndicaciones;
        private int _nCompletado;
        private bool _bNotifExceso;
        //Atributos complementarios
        public string _sNombreCompleto;
        private double _nPendiente;
        private double _nAcumulado;
        private DateTime _dPrimerConsumo;
        private DateTime _dUltimoConsumo;

        #endregion

        #region Propiedades públicas

        public int nIdTarea
        {
            get { return _nIdTarea; }
            set { _nIdTarea = value; }
        }
        public int nIdRecurso
        {
            get { return _nIdRecurso; }
            set { _nIdRecurso = value; }
        }
        public double nEte
        {
            get { return _nEte; }
            set { _nEte = value; }
        }
        public DateTime dFfe
        {
            get { return _dFfe; }
            set { _dFfe = value; }
        }
        public double nEtp
        {
            get { return _nEtp; }
            set { _nEtp = value; }
        }
        public DateTime dFip
        {
            get { return _dFip; }
            set { _dFip = value; }
        }
        public DateTime dFfp
        {
            get { return _dFfp; }
            set { _dFfp = value; }
        }
        public int nIdTarifa
        {
            get { return _nIdTarifa; }
            set { _nIdTarifa = value; }
        }
        public int nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }
        public string sComentario
        {
            get { return _sComentario; }
            set { _sComentario = value; }
        }
        public string sIndicaciones
        {
            get { return _sIndicaciones; }
            set { _sIndicaciones = value; }
        }
        public int nCompletado
        {
            get { return _nCompletado; }
            set { _nCompletado = value; }
        }
        public bool bNotifExceso
		{
            get { return _bNotifExceso; }
            set { _bNotifExceso = value; }
		}

        //Atributos complementarios
        public string sNombreCompleto
        {
            get { return _sNombreCompleto; }
            set { _sNombreCompleto = value; }
        }
        public double nPendiente
        {
            get { return _nPendiente; }
            set { _nPendiente = value; }
        }
        public double nAcumulado
        {
            get { return _nAcumulado; }
            set { _nAcumulado = value; }
        }
        public DateTime dPrimerConsumo
        {
            get { return _dPrimerConsumo; }
            set { _dPrimerConsumo = value; }
        }
        public DateTime dUltimoConsumo
        {
            get { return _dUltimoConsumo; }
            set { _dUltimoConsumo = value; }
        }

        #endregion

        #region Constructores

        public TareaRecurso()
        {
            //En el constructor vacío, se inicializan los atributo
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region	Métodos públicos

        /// <summary>
        /// Obtiene los datos generales de una relación tarea/recurso,
        /// correspondientes a la tabla t336_TAREAPSPRECURSO, y devuelve una
        /// instancia u objeto del tipo TareaRecurso
        /// </summary>
        public static TareaRecurso Obtener(int nIdTarea, int nIdRecruso)
        {
            TareaRecurso o = new TareaRecurso();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdRecruso;

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREARECURSOS", aParam);

            if (dr.Read())
            {
                if (dr["t332_idtarea"] != DBNull.Value)
                    o.nIdTarea = (int)dr["t332_idtarea"];
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.nIdRecurso = (int)dr["t314_idusuario"];
                if (dr["t336_ete"] != DBNull.Value)
                    o.nEte = double.Parse(dr["t336_ete"].ToString());
                if (dr["t336_ffe"] != DBNull.Value)
                    o.dFfe = (DateTime)dr["t336_ffe"];
                if (dr["t336_etp"] != DBNull.Value)
                    o.nEtp = double.Parse(dr["t336_etp"].ToString());
                if (dr["t336_fip"] != DBNull.Value)
                    o.dFip = (DateTime)dr["t336_fip"];
                if (dr["t336_ffp"] != DBNull.Value)
                    o.dFfp = (DateTime)dr["t336_ffp"];
                if (dr["t333_idperfilproy"] != DBNull.Value)
                    o.nIdTarifa = (int)dr["t333_idperfilproy"];
                if (dr["t336_estado"] != DBNull.Value)
                    o.nEstado = (int)dr["t336_estado"];
                if (dr["t336_comentario"] != DBNull.Value)
                    o.sComentario = (string)dr["t336_comentario"];
                if (dr["t336_indicaciones"] != DBNull.Value)
                    o.sIndicaciones = (string)dr["t336_indicaciones"];
                if (dr["nombreCompleto"] != DBNull.Value)
                    o.sNombreCompleto = (string)dr["nombreCompleto"];
                if (dr["Pendiente"] != DBNull.Value)
                    o.nPendiente = double.Parse(dr["Pendiente"].ToString());
                if (dr["t336_completado"] != DBNull.Value)
                    o.nCompletado = (int)dr["t336_completado"];
                if (dr["Acumulado"] != DBNull.Value)
                    o.nAcumulado = double.Parse(dr["Acumulado"].ToString());
                if (dr["PrimerConsumo"] != DBNull.Value)
                    o.dPrimerConsumo = (DateTime)dr["PrimerConsumo"];
                if (dr["UltimoConsumo"] != DBNull.Value)
                    o.dUltimoConsumo = (DateTime)dr["UltimoConsumo"];
                if (dr["t336_notif_exceso"] != DBNull.Value)
                    o.bNotifExceso = (bool)dr["t336_notif_exceso"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningún dato de la tarea / profesional"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        /// <summary>
        /// Obtiene una relación de Recursos, asociados a una Tarea
        /// </summary>
        public static SqlDataReader Catalogo(int nIdTarea, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREARECURSOCATA2", aParam);
        }

        /// <summary>
        /// 
        /// Graba los datos básicos de la relación Tarea/Recurso,
        /// correspondientes a la tabla t336_TAREAPSPRECURSO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        //public static int Insertar(SqlTransaction tr,
        //                        int nIdTarea,
        //                        int nIdRecurso,
        //                        Nullable<double> nEte,
        //                        Nullable<DateTime> dFfe,
        //                        Nullable<double> nEtp,
        //                        Nullable<DateTime> dFip,
        //                        Nullable<DateTime> dFfp,
        //                        Nullable<int> nIdTarifa,
        //                        int nEstado,
        //                        string sComentario,
        //                        string sIndicaciones,
        //                        bool bNotifExceso
        //    )
        //{
        //    SqlParameter[] aParam = new SqlParameter[12];
        //    aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
        //    aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
        //    aParam[2] = new SqlParameter("@nEte", SqlDbType.Real, 4);
        //    aParam[3] = new SqlParameter("@dFfe", SqlDbType.SmallDateTime, 4);
        //    aParam[4] = new SqlParameter("@nEtp", SqlDbType.Real, 4);
        //    aParam[5] = new SqlParameter("@dFip", SqlDbType.SmallDateTime, 4);
        //    aParam[6] = new SqlParameter("@dFfp", SqlDbType.SmallDateTime, 4);
        //    aParam[7] = new SqlParameter("@nIdTarifa", SqlDbType.Int, 4);
        //    aParam[8] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
        //    aParam[9] = new SqlParameter("@sComentario", SqlDbType.Text, 2147483647);
        //    aParam[10] = new SqlParameter("@sIndicaciones", SqlDbType.Text, 2147483647);
        //    aParam[11] = new SqlParameter("@bNotifExceso", SqlDbType.Bit, 1);

        //    if (sComentario != null) sComentario = Utilidades.unescape(sComentario);
        //    if (sIndicaciones != null) sIndicaciones = Utilidades.unescape(sIndicaciones);

        //    aParam[0].Value = nIdTarea;
        //    aParam[1].Value = nIdRecurso;
        //    aParam[2].Value = nEte;
        //    aParam[3].Value = dFfe;
        //    aParam[4].Value = nEtp;
        //    aParam[5].Value = dFip;
        //    aParam[6].Value = dFfp;
        //    aParam[7].Value = nIdTarifa;
        //    aParam[8].Value = nEstado;
        //    aParam[9].Value = sComentario;
        //    aParam[10].Value = sIndicaciones;
        //    aParam[11].Value = bNotifExceso;

        //    int nResul = 0;
        //    if (tr == null)
        //        nResul = SqlHelper.ExecuteNonQuery("SUP_TAREARECURSOI", aParam);
        //    else
        //        nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREARECURSOI", aParam);

        //    return nResul;
        //}
        /// <summary>
        /// 
        /// Graba los datos básicos de la relación Tarea/Recurso, correspondientes a la tabla t336_TAREAPSPRECURSO,
        /// dentro de la transacción que se pasa como parámetro solo si el registro no existe ya.
        /// </summary>
        public static int InsertarSNE(SqlTransaction tr,
                                int nIdTarea,
                                int nIdRecurso,
                                Nullable<double> nEte,
                                Nullable<DateTime> dFfe,
                                Nullable<double> nEtp,
                                Nullable<DateTime> dFip,
                                Nullable<DateTime> dFfp,
                                Nullable<int> nIdTarifa,
                                int nEstado,
                                string sComentario,
                                string sIndicaciones,
                                bool bNotifExceso
            )
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nEte", SqlDbType.Real, 4);
            aParam[3] = new SqlParameter("@dFfe", SqlDbType.SmallDateTime, 4);
            aParam[4] = new SqlParameter("@nEtp", SqlDbType.Real, 4);
            aParam[5] = new SqlParameter("@dFip", SqlDbType.SmallDateTime, 4);
            aParam[6] = new SqlParameter("@dFfp", SqlDbType.SmallDateTime, 4);
            aParam[7] = new SqlParameter("@nIdTarifa", SqlDbType.Int, 4);
            aParam[8] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[9] = new SqlParameter("@sComentario", SqlDbType.Text, 2147483647);
            aParam[10] = new SqlParameter("@sIndicaciones", SqlDbType.Text, 2147483647);
            aParam[11] = new SqlParameter("@bNotifExceso", SqlDbType.Bit, 1);

            if (sComentario != null) sComentario = Utilidades.unescape(sComentario);
            if (sIndicaciones != null) sIndicaciones = Utilidades.unescape(sIndicaciones);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdRecurso;
            aParam[2].Value = nEte;
            aParam[3].Value = dFfe;
            aParam[4].Value = nEtp;
            aParam[5].Value = dFip;
            aParam[6].Value = dFfp;
            aParam[7].Value = nIdTarifa;
            aParam[8].Value = nEstado;
            aParam[9].Value = sComentario;
            aParam[10].Value = sIndicaciones;
            aParam[11].Value = bNotifExceso;

            if (tr == null)
                return (int)SqlHelper.ExecuteScalar("SUP_TAREARECURSO_I", aParam);
            else
                return (int)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREARECURSO_I", aParam);
        }


        /// <summary>
        /// 
        /// Graba los datos básicos de la relación Tarea/Recurso,
        /// correspondientes a la tabla t336_TAREAPSPRECURSO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int Modificar(SqlTransaction tr,
                                int nIdTarea,
                                int nIdRecurso,
                                Nullable<double> nEte,
                                //Nullable<DateTime> dFfe,
                                Nullable<double> nEtp,
                                Nullable<DateTime> dFip,
                                Nullable<DateTime> dFfp,
                                Nullable<int> nIdTarifa,
                                int nEstado,
                                string sComentario,
                                string sIndicaciones,
                                bool bNotifExceso
                                )
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nEte", SqlDbType.Real, 4);
            //aParam[3] = new SqlParameter("@dFfe", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@nEtp", SqlDbType.Real, 4);
            aParam[4] = new SqlParameter("@dFip", SqlDbType.SmallDateTime, 4);
            aParam[5] = new SqlParameter("@dFfp", SqlDbType.SmallDateTime, 4);
            aParam[6] = new SqlParameter("@nIdTarifa", SqlDbType.Int, 4);
            aParam[7] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);
            aParam[8] = new SqlParameter("@sComentario", SqlDbType.Text, 2147483647);
            aParam[9] = new SqlParameter("@sIndicaciones", SqlDbType.Text, 2147483647);
            aParam[10] = new SqlParameter("@bNotifExceso", SqlDbType.Bit, 1);

            if (sComentario != null) sComentario = Utilidades.unescape(sComentario);
            if (nEtp == null) nEtp = -1;
            //if (dFfp == null) dFfp = DateTime.Parse("01/01/1900");
            //if (sIndicaciones != null) sIndicaciones = Utilidades.unescape(sIndicaciones);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdRecurso;
            aParam[2].Value = nEte;
            //aParam[3].Value = dFfe;
            aParam[3].Value = nEtp;
            aParam[4].Value = dFip;
            aParam[5].Value = dFfp;
            aParam[6].Value = nIdTarifa;
            aParam[7].Value = nEstado;
            aParam[8].Value = sComentario;
            aParam[9].Value = sIndicaciones;
            aParam[10].Value = bNotifExceso;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREARECURSOU", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREARECURSOU", aParam);
        }


        /// <summary>
        /// 
        /// Graba las indicaciones de la relación Tarea/Recurso,
        /// correspondientes a la tabla t336_TAREAPSPRECURSO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int ModificarIndicaciones(SqlTransaction tr,
                            int nIdTarea,
                            int nIdRecurso,
                            string sIndicaciones
                            )
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@sIndicaciones", SqlDbType.Text, 2147483647);

            //if (sIndicaciones != null) sIndicaciones = Utilidades.unescape(sIndicaciones);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdRecurso;
            aParam[2].Value = sIndicaciones;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREARECURSOUINDI", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREARECURSOUINDI", aParam);

        }

        /// <summary>
        /// 
        /// Elimina los datos básicos de la Tarea/Recurso,
        /// correspondientes a la tabla t336_TAREAPSPRECURSO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static void Eliminar(SqlTransaction tr,
                                int nIdTarea,
                                int nIdRecurso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdRecurso;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_TAREARECURSOD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREARECURSOD", aParam);

        }

        /// <summary>
        /// 
        /// Comprueba si un técnico ha imputado consumos en una tarea determinada
        ///	(para poder eliminar o no su asociación a la tarea)
        /// </summary>
        public static bool ExistenConsumos(SqlTransaction tr,
                        int nIdTarea,
                        int nIdRecurso
        )
        {
            bool bConsumos = false;
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);

            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdRecurso;

            int nResul = 0;
            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTECONSUMO", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_EXISTECONSUMO", aParam));

            if (nResul > 0) bConsumos = true;

            return bConsumos;
        }

        /// <summary>
        /// 
        /// Comprueba si un recurso está asociado a una tarea determinada
        /// </summary>
        public static bool AsociadoATarea(SqlTransaction tr, int nIdRecurso, int nIdTarea)
        {
            bool bAsociado = false;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nTarea", SqlDbType.Int, 4);

            aParam[0].Value = nIdRecurso;
            aParam[1].Value = nIdTarea;

            int nResul = 0;
            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_RECASOCTAREA", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_RECASOCTAREA", aParam));

            if (nResul > 0) bAsociado = true;

            return bAsociado;
        }
        /// <summary>
        /// 
        /// Comprueba si un recurso está asociado a un proyecto determinado
        /// </summary>
        public static bool AsociadoAProyecto(SqlTransaction tr,
                        int nProy,
                        int nIdRecurso
        )
        {
            bool bAsociado = false;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nProy", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);

            aParam[0].Value = nProy;
            aParam[1].Value = nIdRecurso;

            int nResul = 0;
            if (tr != null)
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_RECASOCPROY", aParam));
            else
                nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_RECASOCPROY", aParam));

            if (nResul > 0) bAsociado = true;

            return bAsociado;
        }

        //public static int AsociarAProyecto(SqlTransaction tr,
        //                int nCR,
        //                int nIdRecurso,
        //                int nProy,
        //                Nullable<int> nIdTarifa,
        //                DateTime dFecAltaProy,
        //                Nullable<DateTime> dFecBajaProy
        //    )
        //{
        //    double nCoste = 0;

        //    //NODO objUne = NODO.Select(null, nCR);
        //    Recurso objRec = Recurso.Obtener(nIdRecurso);

        //    if ((nCR == objRec.idNodo) || (objRec.idNodo == 0)) //Recurso propio o Recurso externo
        //    {
        //        if (PROYECTOSUBNODO.GetTipoCoste(null, nProy) == "H")
        //            nCoste = objRec.CosteHora;
        //        else
        //            nCoste = objRec.CosteJornada;
        //    }
        //    else //if (objUne.t391_idsupernodo1 == objRec.CodEmpresa) //Recurso de otro nodo de la misma empresa o Recurso de otra empresa del grupo
        //    {
        //        nCoste= 0;
        //    }

        //    //if (dFecBajaProy == null) dFecBajaProy = DateTime.Parse("01/01/1900");
        //    if (dFecBajaProy == DateTime.Parse("01/01/0001")) dFecBajaProy = null;// dFecBajaProy = DateTime.Parse("01/01/1900");
        //    int nResul = recursos_asoc_proy.Insert(tr, nIdRecurso, nProy, nCoste, false, dFecAltaProy, dFecBajaProy, nIdTarifa);

        //    return nResul;
        //}
        //public static int ReAsociarAProyecto(SqlTransaction tr, int nIdRecurso, int nProy)
        //{
        //    return recursos_asoc_proy.Update2(tr, nIdRecurso, nProy);
        //}
        /// <summary>
        /// 
        /// Duplica los datos básicos de la relación Tarea/Recurso de una tarea,
        /// correspondientes a la tabla t336_TAREAPSPRECURSO,
        /// dentro de la transacción que se pasa como parámetro.
        /// </summary>
        public static int DuplicarRecursos(SqlTransaction tr, int nIdTareaAnt, int nIdTareaAct)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdTareaAnt", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdTareaAct", SqlDbType.Int, 4);

            aParam[0].Value = nIdTareaAnt;
            aParam[1].Value = nIdTareaAct;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREARECURSO_DUP", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREARECURSO_DUP", aParam);
        }

        public static string EnviarCorreoRecurso(SqlTransaction tr, string sTipo, string strDatosTarea, string sEtp, string sIni, string sFin, 
                                                 string sObs, string sMensGen)
        {
            string sResul = "";
            ArrayList aListCorreo = new ArrayList();
            StringBuilder sbuilder = new StringBuilder();
            string sAsunto = "", sTexto = "", sTO = "";//, sCodRed;

            try
            {
                if (sTipo == "I")
                {
                    sAsunto = "Asignación de profesional a tarea.";
                    sbuilder.Append(@"<BR>SUPER le informa de su asignación a la siguiente tarea:<BR><BR>");
                }
                else
                {
                    sAsunto = "Modificación de profesional de recurso a tarea.";
                    sbuilder.Append(@"<BR>SUPER le informa de la modificación de su asignación a la siguiente tarea:<BR><BR>");
                }

                string[] aDatosTarea = Regex.Split(strDatosTarea, "##");
                //aDatosTarea[1] = hdnIdTarea
                //aDatosTarea[2] = idRecurso
                //aDatosTarea[10] = txtDesTarea
                //aDatosTarea[11] = txtNumPE
                //aDatosTarea[12] = txtPE
                //aDatosTarea[13] = txtPT
                //aDatosTarea[14] = txtFase
                //aDatosTarea[15] = txtActividad
                //aDatosTarea[16] = txtCodPST
                //aDatosTarea[17] = txtDesPST
                //aDatosTarea[18] = txtOTL
                //aDatosTarea[19] = txtIncidencia

                sbuilder.Append("<label style='width:120px'>Proyecto económico: </label>" + aDatosTarea[11] + @" - " + Utilidades.unescape(aDatosTarea[12]) + "<br>");
                sbuilder.Append("<label style='width:120px'>Proyecto Técnico: </label>" + Utilidades.unescape(aDatosTarea[13]) + "<br>");

                if (aDatosTarea[14] != "") sbuilder.Append("<label style='width:120px'>Fase: </label>" + Utilidades.unescape(aDatosTarea[14]) + "<br>");
                if (aDatosTarea[15] != "") sbuilder.Append("<label style='width:120px'>Actividad: </label>" + Utilidades.unescape(aDatosTarea[15]) + "<br>");

                sbuilder.Append("<label style='width:120px'>Tarea: </label><b>" + aDatosTarea[1] + @" - " + Utilidades.unescape(aDatosTarea[10]) + "</b><br><br>");
                sbuilder.Append("<b>Información de la tarea:</b><br><br>");

                if (aDatosTarea[16] != "")
                    sbuilder.Append("<label style='width:120px'>OTC: </label>" + Utilidades.unescape(aDatosTarea[16]) + " - " + Utilidades.unescape(aDatosTarea[17]) + "<br>");
                if (aDatosTarea[18] != "")
                    sbuilder.Append("<label style='width:120px'>OTL: </label>" + Utilidades.unescape(aDatosTarea[18]) + "<br>");
                if (aDatosTarea[19] != "")
                    sbuilder.Append("<label style='width:120px'>Incidencia/Petición: </label>" + Utilidades.unescape(aDatosTarea[19]) + "<br>");
                if ((sEtp == null)||(sEtp == "")) sEtp = "0";
                sbuilder.Append("<label style='width:120px'>Esfuerzo: </label>" + sEtp + " horas<br>");
                if (sIni=="")
                    sbuilder.Append("<label style='width:120px'>F/Inicio: </label>&nbsp;<br>");
                else
                    sbuilder.Append("<label style='width:120px'>F/Inicio: </label>" + sIni.Substring(0, 10) + "<br>");
                if (sFin=="")
                    sbuilder.Append("<label style='width:120px'>F/Fin: </label>&nbsp;<br>");
                else
                    sbuilder.Append("<label style='width:120px'>F/Fin: </label>" + sFin.Substring(0, 10) + "<br>");
                sbuilder.Append("<label style='width:120px'>Indicaciones generales: </label>" + Utilidades.unescape(sMensGen) + "<br>");
                sbuilder.Append("<label style='width:120px'>Indicaciones particulares: </label>" + Utilidades.unescape(sObs) + "<br>");

                //sTO = Utilidades.unescape(aDatosTarea[2]);
                //sCodRed = Recurso.CodigoRed(int.Parse(sTO));
                //sTO = sCodRed.Replace(";", @"/");
                //if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
                //{
                //    sTO = HttpContext.Current.Session["IDRED_ENTRADA"].ToString();
                //    sAsunto += " (" + sCodRed + ")";
                //}
                sTO = SUPER.Capa_Negocio.Recurso.GetDireccionMail(int.Parse(aDatosTarea[2]));
                sTexto = sbuilder.ToString();

                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);

                Correo.EnviarCorreos(aListCorreo);

                //Si el correo se ha enviado correctamente, para que no lo vuelva a enviar en el proceso nocturno
                //lo borro de la tabla de correos. En la tabla de correos se ha metido a través del trigger en la T336_TAREAPSPUSUARIO
                //Tampoco podemos quitar ese trigger ya que se pueden asignar profesionales a tareas desde otros procesos y/o triggers
                
                //Lo comento porque parece que da error de interbloqueo
                //SUPER.Capa_Datos.USUARIO.BorrarCorreo(tr, int.Parse(aDatosTarea[2]), 1, true, int.Parse(aDatosTarea[1]));

                sResul = "OK@#@";
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de asignación de recurso a tarea. strDatosTarea=" + strDatosTarea, ex);
            }
            return sResul;
        }
        /// <summary>
        /// Obtiene una relación de códigos de red de los Recursos asociados a una Tarea que están activos
        /// </summary>
        public static SqlDataReader MailActivos(int nIdTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[0].Value = nIdTarea;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREARECURSO_MAIL", aParam);
        }

        public static void ActualizarEstimacion(SqlTransaction tr, int t314_idusuario, int t332_idtarea, Nullable<DateTime> t336_ffe, double t336_ete, string t336_comentario, bool t336_completado)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@t336_ffe", SqlDbType.DateTime, 8);
            aParam[3] = new SqlParameter("@t336_ete", SqlDbType.Float, 8);
            aParam[4] = new SqlParameter("@t336_comentario", SqlDbType.Text, 2147483647);
            aParam[5] = new SqlParameter("@t336_completado", SqlDbType.Bit, 1);

            aParam[0].Value = t314_idusuario;
            aParam[1].Value = t332_idtarea;
            aParam[2].Value = t336_ffe;
            aParam[3].Value = t336_ete;
            aParam[4].Value = t336_comentario;
            aParam[5].Value = t336_completado;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESTIMACIONIAPU", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESTIMACIONIAPU", aParam);
        }
        public static void FinalizarLaborEnTarea(SqlTransaction tr, int t314_idusuario, int t332_idtarea, bool t336_completado)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[1].Value = t332_idtarea;
            aParam[2] = new SqlParameter("@t336_completado", SqlDbType.Bit, 1);
            aParam[2].Value = t336_completado;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FINALIZARIAPU", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FINALIZARIAPU", aParam);
        }
        
        public static int DeleteDeTareasSinConsumos(SqlTransaction tr, int t314_idusuario, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAUSUARIO_SINCONSUMOS_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAUSUARIO_SINCONSUMOS_D", aParam);
        }

        /// <summary>
        /// Si el proyecto economico permite asignación de recursos desde la parte técnica,
        ///     Graba los datos básicos de la relación Tarea/Recurso, correspondientes a la tabla t336_TAREAPSPRECURSO,
        ///     dentro de la transacción que se pasa como parámetro solo si el registro no existe ya.
        ///     Y asocia el recurso al proyecto económico
        /// Si el proyecto economico NO permite asignación de recursos desde la parte técnica,
        ///     Si el usuario ya está asignado al proyecto economico
        ///         Graba los datos básicos de la relación Tarea/Recurso, correspondientes a la tabla t336_TAREAPSPRECURSO,
        ///         dentro de la transacción que se pasa como parámetro solo si el registro no existe ya
        /// Devuelve true si se ha insertado el recurso a la tarea
        /// </summary>
        public static bool InsertarTEC(SqlTransaction tr,int nIdTarea, int nIdRecurso,Nullable<double> nEte,Nullable<DateTime> dFfe,
                                Nullable<double> nEtp,Nullable<DateTime> dFip,Nullable<DateTime> dFfp,Nullable<int> nIdTarifa,
                                int nEstado,string sComentario,string sIndicaciones, bool bNotifExceso,
                                bool bAdmiteRecursosPST, int IdPsn, int IdNodo, int iUltCierreEco)
        {
            int iRes = 0;
            bool bRes = false;
            if (bAdmiteRecursosPST)
            {
                iRes = InsertarSNE(tr, nIdTarea, nIdRecurso, nEte, dFfe, nEtp, dFip, dFfp, nIdTarifa, nEstado, sComentario, sIndicaciones, bNotifExceso);
                if (iRes != 0) bRes = true;
                //La asignación a recurso se hace ahora por trigger desde la T336_TAREAPSPUSUARIO
                //if (!TareaRecurso.AsociadoAProyecto(tr, IdPsn, nIdRecurso))
                //{//lA FECHA DE alta en el proyecto será la siguiente al último mes cerrado del nodo
                //    DateTime dtFechaAlta = Fechas.AnnomesAFecha(Fechas.AddAnnomes(iUltCierreEco, 1));
                //    TareaRecurso.AsociarAProyecto(tr, IdNodo, nIdRecurso, IdPsn, null, dtFechaAlta, null);
                //}
                //else
                //    TareaRecurso.ReAsociarAProyecto(tr, nIdRecurso, IdPsn);
            }
            else//El PE no admite recursos desde PST
            {
                if (TareaRecurso.AsociadoAProyecto(tr, IdPsn, nIdRecurso))
                {
                    iRes = InsertarSNE(tr, nIdTarea, nIdRecurso, nEte, dFfe, nEtp, dFip, dFfp, nIdTarifa, nEstado, sComentario, sIndicaciones, bNotifExceso);
                    if (iRes != 0) bRes = true;
                    //TareaRecurso.ReAsociarAProyecto(tr, nIdRecurso, IdPsn);
                }
            }

            return bRes;
        }

        /// <summary>
        /// Actualiza los datos de esfuerzo de la asignación de un recurso a una tarea
        /// </summary>
        public static int UpdateEsfuerzo(SqlTransaction tr,
                                int nIdTarea,
                                int nIdRecurso,
                                Nullable<double> nEte,
                                Nullable<double> nEtp,
                                Nullable<DateTime> dFip,
                                Nullable<DateTime> dFfp,
                                Nullable<int> nIdTarifa,
                                int nEstado
                                )
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@nIdTarea", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
            aParam[2] = new SqlParameter("@nEte", SqlDbType.Real, 4);
            //aParam[3] = new SqlParameter("@dFfe", SqlDbType.SmallDateTime, 4);
            aParam[3] = new SqlParameter("@nEtp", SqlDbType.Real, 4);
            aParam[4] = new SqlParameter("@dFip", SqlDbType.SmallDateTime, 4);
            aParam[5] = new SqlParameter("@dFfp", SqlDbType.SmallDateTime, 4);
            aParam[6] = new SqlParameter("@nIdTarifa", SqlDbType.Int, 4);
            aParam[7] = new SqlParameter("@nEstado", SqlDbType.Bit, 1);

            if (nEtp == null) nEtp = -1;

            aParam[0].Value = nIdTarea;
            aParam[1].Value = nIdRecurso;
            aParam[2].Value = nEte;
            aParam[3].Value = nEtp;
            aParam[4].Value = dFip;
            aParam[5].Value = dFfp;
            aParam[6].Value = nIdTarifa;
            aParam[7].Value = nEstado;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREARECURSO_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREARECURSO_U2", aParam);
        }
        #endregion
        //public static int AsignarRecursoTarea(SqlTransaction tr, int t314_idusuario, int t332_idtarea)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
        //    aParam[0].Value = t314_idusuario;
        //    aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
        //    aParam[1].Value = t332_idtarea;

        //    if (tr == null)
        //        return (int)SqlHelper.ExecuteScalar("SUP_WS_RECURSO_TAREA_I", aParam);
        //    else
        //        return (int)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_WS_RECURSO_TAREA_I", aParam);
        //}
    }
}