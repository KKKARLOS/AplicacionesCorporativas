using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Collections.Generic;

namespace SUPER.Capa_Negocio
{
	public partial class CAMBIOESTRUCTURAPSN
	{
		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T467_CAMBIOESTRUCTURAPSN.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	16/03/2009 8:54:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insertar(SqlTransaction tr, int t305_idproyectosubnodo , Nullable<int> t303_idnodo_destino , Nullable<bool> t467_procesado)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo_destino;
			aParam[2] = new SqlParameter("@t467_procesado", SqlDbType.Bit, 1);
			aParam[2].Value = t467_procesado;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAPSN_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T467_CAMBIOESTRUCTURAPSN.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	16/03/2009 8:54:07
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Modificar(SqlTransaction tr, int t305_idproyectosubnodo, Nullable<int> t303_idnodo_destino, Nullable<bool> t467_procesado, string t467_excepcion)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo_destino;
			aParam[2] = new SqlParameter("@t467_procesado", SqlDbType.Bit, 1);
			aParam[2].Value = t467_procesado;
            aParam[3] = new SqlParameter("@t467_excepcion", SqlDbType.Text, 2147483647);
            aParam[3].Value = t467_excepcion;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAPSN_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_U", aParam);
		}

        public static int DeleteAll(SqlTransaction tr)
		{
			SqlParameter[] aParam = new SqlParameter[0];

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAPSN_DEL", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_DEL", aParam);
		}

        public static SqlDataReader CatalogoDestino()
		{
			SqlParameter[] aParam = new SqlParameter[0];

			return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURAPSN_CAT", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.- Cambio de estructura de proyectos
        /// Comprobar si existen réplicas del proyecto a trasladar en el nodo destino
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Paso00(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_destino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURA_PROYECTO_00", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_00", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.1.- Instancia contratante se mueve a destino sin instancia replicada
        /// •	T305_PROYECTOSUBNODO
        /// t304_idsubnodo = Si sólo hay un subnodo para ND, se pone ese. En caso contrario, se pone el de maniobra. Si éste no existe, se crea.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso01(SqlTransaction tr, int t305_idproyectosubnodo, int t304_idsubnodo, Nullable<int> t314_idusuario_responsable)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t304_idsubnodo;
            aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario_responsable;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_01", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_01", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.1.- Instancia contratante se mueve a destino sin instancia replicada
        /// •	Usuarios pertenecientes al nodo NO. 
        /// o	T330_USUARIOPROYECTOSUBNODO
        /// t330_costerep = t314_costejornada o t314_costehora en función de t301_modelocoste
        /// t330_costecon = fcalculacostecon(t330_costerep)
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso02(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_origen)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_02", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_02", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.1.- Instancia contratante se mueve a destino sin instancia replicada
        /// •	Usuarios pertenecientes al nodo NO
        /// o	T378_CONSPERMES
        /// Para meses cerrados:
        /// t378_costeunitariorep = t378_costeunitariocon
        /// t378_costeunitariocon = fcalculacostecon(t378_costeunitariorep)
        /// 
        /// Para meses abiertos:
        /// t378_costeunitariorep = t330_costerep
        /// t378_costeunitariocon = t330_costecon
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso03(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_origen)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_origen;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_03", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_03", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.1.- Instancia contratante se mueve a destino sin instancia replicada
        /// •	Usuarios pertenecientes al nodo ND. 
        /// o	T330_USUARIOPROYECTOSUBNODO
        /// t330_costecon = t314_costejornada o t314_costehora en función de t301_modelocoste 
        /// t330_costerep = 0
        /// 
        /// Para meses abiertos:
        /// t378_costeunitariorep = t330_costerep
        /// t378_costeunitariocon = t330_costecon
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso04(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_destino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_04", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_04", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.1.- Instancia contratante se mueve a destino sin instancia replicada
        /// •	Usuarios pertenecientes al nodo ND. 
        /// o	T378_CONSPERMES
        /// Para meses cerrados:
        /// t378_costeunitariocon = 378_costeunitariorep
        /// t378_costeunitariorep  = 0
        /// 
        /// Para meses abiertos:
        /// t378_costeunitariocon = t330_costecon
        /// t378_costeunitariorep = 0
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso05(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_destino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_05", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_05", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.1.- Instancia contratante se mueve a destino sin instancia replicada
        /// •	Usuarios pertenecientes a NX, un nodo distinto de NO y ND  
        /// o	T330_USUARIOPROYECTOSUBNODO
        /// t330_costecon = fcalculacostecon(t330_costerep, NX)
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso06(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_origen, int t303_idnodo_destino)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[2] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_origen;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_06", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_06", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.1.- Instancia contratante se mueve a destino sin instancia replicada
        /// •	Usuarios pertenecientes a NX, un nodo distinto de NO y ND  
        /// o	T378_CONSPERMES
        /// Para meses cerrados:
        /// t378_costeunitariocon = fcalculacostecon(t378_costeunitariorep, NX)
        /// 
        /// Para meses abiertos:
        /// t378_costeunitariocon = t330_costecon
        /// t378_costeunitariorep = t330_costerep
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso07(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_origen, int t303_idnodo_destino)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[2] = new SqlParameter("@t303_idnodo_origen", SqlDbType.Int, 4);
            aParam[2].Value = t303_idnodo_origen;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_07", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_07", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.2.- Instancia contratante se mueve a destino con instancia replicada sin gestión
        /// Cada tupla de T376_DATOECO perteneciente al grupo de consumos de la instancia replicada sin gestión, se traspasa a la instancia contratante.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static DataSet Paso08(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTRUCTURA_PROYECTO_08", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_08", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.2.- Instancia contratante se mueve a destino con instancia replicada sin gestión
        /// Cada tupla de T376_DATOECO perteneciente al grupo de consumos de la instancia replicada sin gestión, se traspasa a la instancia contratante.
        /// Para ello, si es preciso crear tuplas en T325_SEGMESPROYECTOSUBNODO, se crean.
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso09(SqlTransaction tr, int t376_iddatoeco, int t325_idsegmesproy, decimal t376_importe_mb)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t376_iddatoeco", SqlDbType.Int, 4);
            aParam[0].Value = t376_iddatoeco;
            aParam[1] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[1].Value = t325_idsegmesproy;
            aParam[2] = new SqlParameter("@t376_importe_mb", SqlDbType.Money, 8);
            aParam[2].Value = t376_importe_mb;
            

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_09", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_09", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.2.- Instancia contratante se mueve a destino con instancia replicada sin gestión
        /// Se borran las tuplas de T376_DATOECO perteneciente al grupo de consumos en la instancia contratante que tuvieran como destino ND (la instancia replicada sin gestión).
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso10(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_destino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t303_idnodo_destino", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodo_destino;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_10", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_10", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.3.- Instancia contratante se mueve a destino con instancia replicada con gestión
        /// •	T331_PT
        /// t305_idproyectosubnodo = el de la contratante.
        /// t331_orden = t331_orden + max(t331_orden de la contratante)
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static void Paso11(SqlTransaction tr, int t305_idproyectosubnodo_contratante, int t305_idproyectosubnodo_replica)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo_contratante", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo_contratante;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo_replica", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo_replica;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_11", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_11", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.3.- Instancia contratante se mueve a destino con instancia replicada con gestión
        /// •	T330_USUARIOPROYECTOSUBNODO
        /// t305_idproyectosubnodo = el de la contratante.
        /// t330_costecon = t314_costejornada o t314_costehora en función de t301_modelocoste
        /// t330_costerep = 0
        /// No se pueden pasar directamente los usuarios de la replicada a la contratante poer el trigger insteadof de la T330
        /// Asi que guardamos la lista de los que son y lo asignaremos a la contratante después de borrar la réplica
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static List<USUARIOPROYECTOSUBNODO> Paso12(SqlTransaction tr, int t305_idproyectosubnodo_contratante, int t305_idproyectosubnodo_replica)
        {
            List<USUARIOPROYECTOSUBNODO> lstUsuarios= new List<USUARIOPROYECTOSUBNODO>();
            SqlDataReader dr = SUPER.Capa_Datos.USUARIOPROYECTOSUBNODO.GetUsuariosCambioEstructura(tr, t305_idproyectosubnodo_contratante, t305_idproyectosubnodo_replica);
            while(dr.Read())
            {
                USUARIOPROYECTOSUBNODO oUser = new USUARIOPROYECTOSUBNODO();
                oUser.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                oUser.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                oUser.t330_costecon = decimal.Parse(dr["t330_costecon"].ToString());
                oUser.t330_costerep = decimal.Parse(dr["t330_costerep"].ToString());
                oUser.t330_deriva = (bool)dr["t330_deriva"];
                oUser.t330_falta = DateTime.Parse(dr["t330_falta"].ToString());
                if (dr["t330_fbaja"].ToString() != "")
                {
                    oUser.t330_fbaja = DateTime.Parse(dr["t330_fbaja"].ToString());
                }
                if (dr["t333_idperfilproy"].ToString() != "")
                {
                    oUser.t333_idperfilproy = int.Parse(dr["t333_idperfilproy"].ToString());
                }
                lstUsuarios.Add(oUser);
            }
            dr.Close();

            return lstUsuarios;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 3.3.- Instancia contratante se mueve a destino con instancia replicada con gestión
        /// o	T378_CONSPERMES
        /// Para meses cerrados:
        /// t378_costeunitariocon = 378_costeunitariorep
        /// t378_costeunitariorep  = 0
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static DataSet Paso13(SqlTransaction tr, int t305_idproyectosubnodo_replicado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo_replicado", SqlDbType.Int, 4);
            aParam[0].Value = @t305_idproyectosubnodo_replicado;

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CAMBIOESTR_PROY_CONSREPLGEST", aParam);//SUP_CAMBIOESTRUCTURA_PROYECTO_13
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CAMBIOESTR_PROY_CONSREPLGEST", aParam);//SUP_CAMBIOESTRUCTURA_PROYECTO_13
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Recojo los distintos meses
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Paso14(SqlTransaction tr, int t305_idproyectosubnodo_replicado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo_replicado", SqlDbType.Int, 4);
            aParam[0].Value = @t305_idproyectosubnodo_replicado;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTR_PROY_MESESREPLGEST", aParam);//SUP_CAMBIOESTRUCTURA_PROYECTO_14
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMBIOESTR_PROY_MESESREPLGEST", aParam);//SUP_CAMBIOESTRUCTURA_PROYECTO_14
        }

        public static void Paso15(SqlTransaction tr, int t314_idusuario, int t325_idsegmesproy_new, //int t325_idsegmesproy_old, 
                                  decimal t378_costeunitariocon, decimal t378_costeunitariorep, 
                                  float t378_unidades, Nullable<int> t303_idnodo_usuariomes, Nullable<int> t313_idempresa_nodomes)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            //aParam[0] = new SqlParameter("@t325_idsegmesproy_old", SqlDbType.Int, 4);
            //aParam[0].Value = t325_idsegmesproy_old;
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t325_idsegmesproy_new", SqlDbType.Int, 4);
            aParam[1].Value = t325_idsegmesproy_new;
            aParam[2] = new SqlParameter("@t378_costeunitariocon", SqlDbType.Money, 8);
            aParam[2].Value = t378_costeunitariocon;
            aParam[3] = new SqlParameter("@t378_costeunitariorep", SqlDbType.Money, 8);
            aParam[3].Value = t378_costeunitariorep;
            aParam[4] = new SqlParameter("@t378_unidades", SqlDbType.Float, 8);
            aParam[4].Value = t378_unidades;
            aParam[5] = new SqlParameter("@t303_idnodo_usuariomes", SqlDbType.Int, 4);
            aParam[5].Value = t303_idnodo_usuariomes;
            aParam[6] = new SqlParameter("@t313_idempresa_nodomes", SqlDbType.Int, 4);
            aParam[6].Value = t313_idempresa_nodomes;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_15", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_15", aParam);
        }
        /// <summary>
        /// Inserta la lista de usuarios (que viene de la réplica) en la instancia contratante
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t305_idproyectosubnodo_contratante"></param>
        /// <param name="lUsusariosReplica"></param>
        public static void Paso16(SqlTransaction tr, int t305_idproyectosubnodo_contratante, List<USUARIOPROYECTOSUBNODO> lUsusariosReplica)
        {
            foreach(USUARIOPROYECTOSUBNODO oUser in lUsusariosReplica)
            {
                SUPER.Capa_Negocio.USUARIOPROYECTOSUBNODO.Insert(tr,
                                                                t305_idproyectosubnodo_contratante,
                                                                oUser.t314_idusuario,
                                                                oUser.t330_costecon,
                                                                oUser.t330_costerep,
                                                                oUser.t330_deriva,
                                                                oUser.t330_falta,
                                                                oUser.t330_fbaja,
                                                                oUser.t333_idperfilproy);
            }
        }
        /// <summary>
        /// Se modifica el PSN de las notas GASVI de la replicada cambiándolo por el de la contratnate
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t305_idproyectosubnodo_contratante"></param>
        /// <param name="t305_idproyectosubnodo_replica"></param>
        public static void Paso17(SqlTransaction tr, int t305_idproyectosubnodo_contratante, int t305_idproyectosubnodo_replica)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo_contratante", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo_contratante;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo_replica", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo_replica;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURA_PROYECTO_17", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURA_PROYECTO_17", aParam);
        }

        public static void CorregirSubcontratacion(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CORREGIRSUBCONTRATACION", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CORREGIRSUBCONTRATACION", aParam);
        }
        public static void EliminarAESotrosNodos(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ELIMINARAESOTROSNODOS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ELIMINARAESOTROSNODOS", aParam);
        }

        public static void CambiarEstructuraAProyecto(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_origen, int t303_idnodo_destino, bool bMantenerResponsables)
        {
            string sTipoReplicaDestino = "";
            int idPSNReplicaDestino = 0;

            SqlDataReader dr = CAMBIOESTRUCTURAPSN.Paso00(tr, t305_idproyectosubnodo, t303_idnodo_destino);
            if (dr.Read())
            {
                idPSNReplicaDestino = (int)dr["t305_idproyectosubnodo"];
                sTipoReplicaDestino = dr["t305_cualidad"].ToString();
            }
            dr.Close();
            dr.Dispose();

            switch (sTipoReplicaDestino)
            {
                case ""://CASO A. 3.1.- Instancia contratante se mueve a destino sin instancia replicada
                    CAMBIOESTRUCTURAPSN.CambioEstructuraCasoA(tr, t305_idproyectosubnodo, t303_idnodo_origen, t303_idnodo_destino, bMantenerResponsables);
                    break;
                case "J"://CASO B. 3.2.- Instancia contratante se mueve a destino con instancia replicada sin gestión
                    CAMBIOESTRUCTURAPSN.CambioEstructuraCasoB(tr, t305_idproyectosubnodo, t303_idnodo_destino, idPSNReplicaDestino);
                    CAMBIOESTRUCTURAPSN.CambioEstructuraCasoA(tr, t305_idproyectosubnodo, t303_idnodo_origen, t303_idnodo_destino, bMantenerResponsables);
                    break;
                case "P"://CASO C. 3.3.- Instancia contratante se mueve a destino con instancia replicada con gestión
                    CAMBIOESTRUCTURAPSN.CambioEstructuraCasoC(tr, t305_idproyectosubnodo, t303_idnodo_destino, idPSNReplicaDestino);
                    CAMBIOESTRUCTURAPSN.CambioEstructuraCasoA(tr, t305_idproyectosubnodo, t303_idnodo_origen, t303_idnodo_destino, bMantenerResponsables);
                    break;
            }
        }

        public static void CambioEstructuraCasoA(SqlTransaction tr, int t305_idproyectosubnodo, int t303_idnodo_origen, int t303_idnodo_destino, bool bMantenerResponsables)
        {
            #region Obtención de subnodo para crear el proyectosubnodo
            int nCount = 0;
            int nCountManiobraTipo2 = 0;
            int idSubNodoAuxDestino = 0;
            int idSubNodoAuxManiobra = 0;
            int idSubNodoGrabar = 0;

            int nCountSubnodosNoManiobra = 0;
            int nResponsableSubNodo = 0;
            string sDenominacionNodo = "";

            DataSet dsSubNodos = SUBNODO.CatalogoActivos(tr, t303_idnodo_destino, true);
            foreach (DataRow oSN in dsSubNodos.Tables[0].Rows)
            {
                if ((byte)oSN["t304_maniobra"] == 1)
                {
                    nCount++;
                    idSubNodoAuxManiobra = (int)oSN["t304_idsubnodo"];
                    nResponsableSubNodo = (int)oSN["t314_idusuario_responsable"];
                    sDenominacionNodo = oSN["t303_denominacion"].ToString();
                }
                else if ((byte)oSN["t304_maniobra"] == 0)
                {
                    idSubNodoAuxDestino = (int)oSN["t304_idsubnodo"];
                    nCountSubnodosNoManiobra++;
                    nResponsableSubNodo = (int)oSN["t314_idusuario_responsable"];
                    sDenominacionNodo = oSN["t303_denominacion"].ToString();
                }
                else nCountManiobraTipo2++;
            }
            if (nCountSubnodosNoManiobra == 1) //si solo hay un subnodo en el nodo, que la réplica se haga a ese subnodo.
            {
                idSubNodoGrabar = idSubNodoAuxDestino;
            }
            else
            {
                if (nCount == 0)
                {
                    NODO oNodo = NODO.Select(tr, t303_idnodo_destino);
                    //crear subnodo maniobra
                    idSubNodoGrabar = SUBNODO.Insert(tr, "Proyectos a reasignar", t303_idnodo_destino, 0, true, 1, oNodo.t314_idusuario_responsable,null);//
                    nResponsableSubNodo = oNodo.t314_idusuario_responsable;
                }
                else
                {
                    if (nCount > 1)
                    {
                        dsSubNodos.Dispose();
                        throw (new Exception("El número de subnodos de maniobra es " + nCount.ToString() + " en el nodo " + sDenominacionNodo + ". Por favor avise al administrador."));
                    }

                    if (dsSubNodos.Tables[0].Rows.Count - 1 - nCountManiobraTipo2 > 1 || dsSubNodos.Tables[0].Rows.Count - 1 - nCountManiobraTipo2 == 0)
                    {
                        idSubNodoGrabar = idSubNodoAuxManiobra;
                    }
                    else
                    {
                        idSubNodoGrabar = idSubNodoAuxDestino;
                    }
                }
            }
            dsSubNodos.Dispose();
            #endregion

            CAMBIOESTRUCTURAPSN.Paso01(tr, t305_idproyectosubnodo, idSubNodoGrabar, (bMantenerResponsables)? null:(int?)nResponsableSubNodo);
            CAMBIOESTRUCTURAPSN.Paso02(tr, t305_idproyectosubnodo, t303_idnodo_origen);
            CAMBIOESTRUCTURAPSN.Paso03(tr, t305_idproyectosubnodo, t303_idnodo_origen);
            CAMBIOESTRUCTURAPSN.Paso04(tr, t305_idproyectosubnodo, t303_idnodo_destino);
            CAMBIOESTRUCTURAPSN.Paso05(tr, t305_idproyectosubnodo, t303_idnodo_destino);
            CAMBIOESTRUCTURAPSN.Paso06(tr, t305_idproyectosubnodo, t303_idnodo_origen, t303_idnodo_destino);
            CAMBIOESTRUCTURAPSN.Paso07(tr, t305_idproyectosubnodo, t303_idnodo_origen, t303_idnodo_destino);
        }
        public static void CambioEstructuraCasoB(SqlTransaction tr, int t305_idproyectosubnodo_contratante, int t303_idnodo_destino, int t305_idproyectosubnodo_replicaSG)
        {
            int nSMPSN_contratante = 0;
            string sEstadoMes = "";
            DataSet dsDatoEco = CAMBIOESTRUCTURAPSN.Paso08(tr, t305_idproyectosubnodo_replicaSG);
            foreach (DataRow oDE in dsDatoEco.Tables[0].Rows)
            {
                //2ºComprobar si existe mes para el nodo destino
                nSMPSN_contratante = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, t305_idproyectosubnodo_contratante, (int)oDE["t325_anomes"]);
                if (nSMPSN_contratante == 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, t305_idproyectosubnodo_contratante, (int)oDE["t325_anomes"]);
                    nSMPSN_contratante = SEGMESPROYECTOSUBNODO.Insert(tr, t305_idproyectosubnodo_contratante, (int)oDE["t325_anomes"], sEstadoMes, 0, 0, false, 0, 0);
                }
                CAMBIOESTRUCTURAPSN.Paso09(tr, (int)oDE["t376_iddatoeco"], nSMPSN_contratante, (decimal)oDE["t376_importe_mb"]);
                //CAMBIOESTRUCTURAPSN.Paso10(tr, t305_idproyectosubnodo_contratante, t303_idnodo_destino);
            }
            dsDatoEco.Dispose();
            CAMBIOESTRUCTURAPSN.Paso10(tr, t305_idproyectosubnodo_contratante, t303_idnodo_destino);
            PROYECTOSUBNODO.Delete(tr, t305_idproyectosubnodo_replicaSG);
        }
        public static void CambioEstructuraCasoC(SqlTransaction tr, int t305_idproyectosubnodo_contratante, int t303_idnodo_destino, int t305_idproyectosubnodo_replicaCG)
        {
            int nSMPSN_contratante = 0;
            string sEstadoMes = "";
            // Aplica la moneda del contratante al presupuesto de la estructura técnica. Aplica el PSN del contratante a los proyectos técnicos 
            CAMBIOESTRUCTURAPSN.Paso11(tr, t305_idproyectosubnodo_contratante, t305_idproyectosubnodo_replicaCG);
            //Coge los consumos de la replica
            DataSet dsDatoEco = CAMBIOESTRUCTURAPSN.Paso08(tr, t305_idproyectosubnodo_replicaCG);
            foreach (DataRow oDE in dsDatoEco.Tables[0].Rows)
            {
                //2ºComprobar si existe mes para el nodo destino
                nSMPSN_contratante = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, t305_idproyectosubnodo_contratante, (int)oDE["t325_anomes"]);
                if (nSMPSN_contratante == 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, t305_idproyectosubnodo_contratante, (int)oDE["t325_anomes"]);
                    nSMPSN_contratante = SEGMESPROYECTOSUBNODO.Insert(tr, t305_idproyectosubnodo_contratante, (int)oDE["t325_anomes"], sEstadoMes, 0, 0, false, 0, 0);
                }
                //Actualiza el importe por si ha habido cambio de moneda
                CAMBIOESTRUCTURAPSN.Paso09(tr, (int)oDE["t376_iddatoeco"], nSMPSN_contratante, (decimal)oDE["t376_importe_mb"]);
                //CAMBIOESTRUCTURAPSN.Paso10(tr, t305_idproyectosubnodo_contratante, t303_idnodo_destino);
            }
            dsDatoEco.Dispose();
            //Borra los consumos de la contratante en el CR de destino
            CAMBIOESTRUCTURAPSN.Paso10(tr, t305_idproyectosubnodo_contratante, t303_idnodo_destino);
            //Reasigno las notas GASVI
            CAMBIOESTRUCTURAPSN.Paso17(tr, t305_idproyectosubnodo_contratante, t305_idproyectosubnodo_replicaCG);

            //Guardamos en una lista los usuarios asignados a la replica con gestion
            List<USUARIOPROYECTOSUBNODO> lUsusariosReplica=CAMBIOESTRUCTURAPSN.Paso12(tr, t305_idproyectosubnodo_contratante, t305_idproyectosubnodo_replicaCG);
            //Guardo en un dataset los consumos de la replicada
            DataSet dsCPMCE = CAMBIOESTRUCTURAPSN.Paso13(tr, t305_idproyectosubnodo_replicaCG);
            //Borro rl proyecto replicado
            PROYECTOSUBNODO.Delete(tr, t305_idproyectosubnodo_replicaCG);
            //Recojo los usuarios del proyecto replicado con gestión que he almacenado previamente y los inserto en la contratante
            //Es necesario hacerlo así porque mientras exista la instancia replicada no permite insertar usuarios en la contratante
            CAMBIOESTRUCTURAPSN.Paso16(tr, t305_idproyectosubnodo_contratante, lUsusariosReplica);
            //Una vez que tengo asignados los usuarios a la contratante ya puedo pasar los consumos
            int? t303_idnodo_usuariomes = null;
            int? t313_idempresa_nodomes = null;
            foreach (DataRow oCPM in dsCPMCE.Tables[0].Rows)
            {
                nSMPSN_contratante = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, t305_idproyectosubnodo_contratante, (int)oCPM["t325_anomes"]);
                if (nSMPSN_contratante == 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, t305_idproyectosubnodo_contratante, (int)oCPM["t325_anomes"]);
                    nSMPSN_contratante = SEGMESPROYECTOSUBNODO.Insert(tr, t305_idproyectosubnodo_contratante, (int)oCPM["t325_anomes"], sEstadoMes, 0, 0, false, 0, 0);
                }
                decimal dAux = decimal.Parse(oCPM["t378_costeunitariorep_mb"].ToString());
                if (oCPM["t303_idnodo_usuariomes"].ToString() != "")
                    t303_idnodo_usuariomes = (int)oCPM["t303_idnodo_usuariomes"];
                else
                    t303_idnodo_usuariomes = null;
                if (oCPM["t313_idempresa_nodomes"].ToString() != "")
                    t313_idempresa_nodomes = (int)oCPM["t313_idempresa_nodomes"];
                else
                    t313_idempresa_nodomes = null;

                //CAMBIOESTRUCTURAPSN.Paso15(tr, (int)oCPM["t325_idsegmesproy"], (int)oCPM["t314_idusuario"], nSMPSN_contratante, dAux, dAux,
                //                            float.Parse(oCPM["t378_unidades"].ToString()), (int)oCPM["t303_idnodo_usuariomes"], (int)oCPM["t313_idempresa_nodomes"]);
                CAMBIOESTRUCTURAPSN.Paso15(tr, (int)oCPM["t314_idusuario"], nSMPSN_contratante, dAux, dAux,
                                            float.Parse(oCPM["t378_unidades"].ToString()), t303_idnodo_usuariomes, t313_idempresa_nodomes);
            }
            dsCPMCE.Dispose();
        }

        public static bool bHayAparcadas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CAMBIOESTRUCTURAPSN_COUNT", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_COUNT", aParam)) == 0) ? false : true;
        }

		#endregion
	}
}
