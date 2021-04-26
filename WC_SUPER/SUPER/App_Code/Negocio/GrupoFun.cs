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
//
using SUPER.Capa_Datos;
using System.Text;
/// <summary>
/// Mantenimiento de Grupo Funcionales y de los profesionales que los componen
/// </summary>
public class GrupoFun
{
    #region Atributos privados

    private int _nIdGF;
    private int _nCodUne;
    private string _sDesGF;

    #endregion
    #region Propiedades públicas

    public int nIdGF
    {
        get { return _nIdGF; }
        set { _nIdGF = value; }
    }
    public int nCodUne
    {
        get { return _nCodUne; }
        set { _nCodUne = value; }
    }
    public string sDesGF
    {
        get { return _sDesGF; }
        set { _sDesGF = value; }
    }

    #endregion

    public GrupoFun()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static SqlDataReader Catalogo(string sApellido1, string sApellido2, string sNombre, short iCodUne)
    {//Obtención del catalogo de personas susceptibles de ser integrantes
        //23/02/2016 Por petición de Yolanda se pueden buscar profesionales de cualquier CR
        SqlParameter[] aParam = new SqlParameter[5];
        //if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
        //{
            //El administrador puede seleccionar cualquier persona. Le pasamos el parametro CodUne
            //para que nos diga a cuantos grupos funcionales de esa UNE pertenece cada empleado
            aParam[0] = new SqlParameter("@sAP1", SqlDbType.VarChar, 50);
            aParam[0].Value = sApellido1;
            aParam[1] = new SqlParameter("@sAP2", SqlDbType.VarChar, 50);
            aParam[1].Value = sApellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = sNombre;
            aParam[3] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
            aParam[3].Value = iCodUne;
            aParam[4] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[4].Value = (bool)HttpContext.Current.Session["FORANEOS"];
            return SqlHelper.ExecuteSqlDataReader("SUP_GF_PROFS", aParam);
        //}
        //else
        //{//Los demás solo personas del CR
        //    //21/01/2011. Dice Victor que también deben ver los externos
        //    aParam[0] = new SqlParameter("@sAP1", SqlDbType.VarChar, 50);
        //    aParam[0].Value = sApellido1;
        //    aParam[1] = new SqlParameter("@sAP2", SqlDbType.VarChar, 50);
        //    aParam[1].Value = sApellido2;
        //    aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
        //    aParam[2].Value = sNombre;
        //    aParam[3] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
        //    aParam[3].Value = iCodUne;
        //    aParam[4] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
        //    aParam[4].Value = (bool)HttpContext.Current.Session["FORANEOS"];
        //    return SqlHelper.ExecuteSqlDataReader("SUP_GF_PROF_CRS", aParam);
        //}
    }
    public static SqlDataReader CatalogoGrupos(int ? iCodUne)
    {
        SqlParameter[] aParam = new SqlParameter[1];
        aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int);
        aParam[0].Value = iCodUne;

        return SqlHelper.ExecuteSqlDataReader("SUP_GFCATA", aParam);
    }

    
    public static SqlDataReader CatalogoGrupos(int nOrden, int nAscDesc, int iCodUne)
    {
        SqlParameter[] aParam = new SqlParameter[3];
        aParam[0] = new SqlParameter("@nOrden", SqlDbType.Int);
        aParam[0].Value = nOrden;
        aParam[1] = new SqlParameter("@nAscDesc", SqlDbType.Int);
        aParam[1].Value = nAscDesc;
        aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int);
        aParam[2].Value = iCodUne;

        return SqlHelper.ExecuteSqlDataReader("SUP_GFCATA", aParam);
    }
    /// <summary>
    /// Devuelve un catálogo de grupos funcionales de un nodo en los que el usuario es responsable del GF
    /// </summary>
    /// <param name="nOrden"></param>
    /// <param name="nAscDesc"></param>
    /// <param name="iCodUne"></param>
    /// <param name="idResp"></param>
    /// <returns></returns>
    public static SqlDataReader CatalogoGruposResponsable(int nOrden, int nAscDesc, int iCodUne, int idResp)
    {
        SqlParameter[] aParam = new SqlParameter[4];
        aParam[0] = new SqlParameter("@nOrden", SqlDbType.Int);
        aParam[0].Value = nOrden;
        aParam[1] = new SqlParameter("@nAscDesc", SqlDbType.Int);
        aParam[1].Value = nAscDesc;
        aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int);
        aParam[2].Value = iCodUne;
        aParam[3] = new SqlParameter("@t314_idusuario", SqlDbType.Int);
        aParam[3].Value = idResp;

        return SqlHelper.ExecuteSqlDataReader("SUP_GF_RESP_CAT", aParam);
    }

    //public static SqlDataReader CatGrupos(int nEmpleado, int nCodUne, string sPerfil)
    //{
    //    SqlParameter[] aParam = new SqlParameter[3];
    //    aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
    //    aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
    //    aParam[2] = new SqlParameter("@sPerfil", SqlDbType.Char, 2);

    //    aParam[0].Value = nEmpleado;
    //    aParam[1].Value = nCodUne;
    //    aParam[2].Value = sPerfil;

    //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
    //    return SqlHelper.ExecuteSqlDataReader("SUP_CAT_GF", aParam);
    //}
    public static SqlDataReader CatalogoProfesionales(int iCodGrupro)
    {
        SqlParameter[] aParam = new SqlParameter[1];
        aParam[0] = new SqlParameter("@nIdGrupro", SqlDbType.Int);
        aParam[0].Value = iCodGrupro;

        return SqlHelper.ExecuteSqlDataReader("SUP_GF_PROFCATA", aParam);
    }
    /// <summary>
    /// Obtiene la lista de los profesionales pertenecientes a la lista de Grupos Funcionales que se pasa como códigos de GF separados por coma
    /// </summary>
    //public static SqlDataReader CatalogoProfesionales2(string slCodGF)
    //{
    //    SqlParameter[] aParam = new SqlParameter[1];
    //    aParam[0] = new SqlParameter("@slCodGF", SqlDbType.VarChar, 8000);
    //    aParam[0].Value = slCodGF;

    //    return SqlHelper.ExecuteSqlDataReader("SUP_GF_PROFCATA2", aParam);
    //}
    /// <summary>
    /// 
    /// Obtiene los datos generales de un Grupo Funcional determinado,
    /// correspondientes a la tabla T037_GRUPROFUNCIONAL.
    /// </summary>
    public void Obtener(int nIdGF)
    {
        SqlParameter[] aParam = new SqlParameter[1];
        aParam[0] = new SqlParameter("@nIdGF", SqlDbType.Int, 4);
        aParam[0].Value = nIdGF;
        SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_GFS", aParam);

        if (dr.Read())
        {
            this.nIdGF = nIdGF;
            this.sDesGF = dr["nombre"].ToString();
            this.nCodUne = int.Parse(dr["t303_idnodo"].ToString());
        }
        dr.Close();
        dr.Dispose();
    }
    public static SqlDataReader VisionGruposFuncionales(int nUsuario)
    {//Obtine los grupos funcionales de los que somos responsable
        SqlParameter[] aParam = new SqlParameter[1];
        aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);

        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) aParam[0].Value = null;
        else  aParam[0].Value = nUsuario;

        return SqlHelper.ExecuteSqlDataReader("SUP_GF_AMB_VISION", aParam);
    }
    public static void BorrarIntegrantes(SqlTransaction tr, int iCodGF)
    {
        int nResul;
        SqlParameter[] aParam = new SqlParameter[1];
        aParam[0] = new SqlParameter("@nIdGrupro", SqlDbType.Int, 4);
        aParam[0].Value = iCodGF;
        if (tr==null) nResul =SqlHelper.ExecuteNonQuery("SUP_GFPROF_DByT342_idgrupro", aParam);
        else nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GFPROF_DByT342_idgrupro", aParam);
    }
    public static void InsertarIntegrante(SqlTransaction tr, int iGrupro, int iNumEmpleado,int iResponsable)
    {
        object nResul;
        SqlParameter[] aParam = new SqlParameter[3];
        aParam[0] = new SqlParameter("@nIdGrupro", SqlDbType.Int, 4);
        aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
        aParam[2] = new SqlParameter("@nResponsable", SqlDbType.Bit,1);
        aParam[0].Value = iGrupro;
        aParam[1].Value = iNumEmpleado;
        aParam[2].Value = iResponsable;
        if (tr == null) nResul = SqlHelper.ExecuteScalar("SUP_GF_PROFI_SNE", aParam);
        else nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GF_PROFI_SNE", aParam);
    }
    /// <summary>
    /// Devuelve la lista de nodos en los que el usuario tiene (o podría tener) Grupos Funcionales sobre los que realizar ABM
    /// </summary>
    /// <param name="iNumEmpleado"></param>
    /// <param name="bSoloResponsableGF"></param>
    /// <returns></returns>
    public static SqlDataReader NodosVisibles(int iNumEmpleado)
    {
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
        {//Si es administrador no merece la pena investigar que figuras tiene
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_NODO_C2", aParam);
        }
        else
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = iNumEmpleado;
            return SqlHelper.ExecuteSqlDataReader("SUP_GF_NODOSVISIBLES", aParam);
        }
    }

    #region Insertar
    public static int InsertarGrupo(SqlTransaction tr, string sDesc, int t303_idnodo)
    {
        object nResul;
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
        aParam[1] = new SqlParameter("@sDesc", SqlDbType.VarChar, 50);
        aParam[0].Value = t303_idnodo;
        aParam[1].Value = sDesc;
        if (tr==null) nResul = SqlHelper.ExecuteScalar("SUP_GFI", aParam);
        else nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GFI", aParam);
        return int.Parse(nResul.ToString());
    }
    public static int InsertarProfesional(SqlTransaction tr, int iCodGrupro, int iCodRecurso, int iResponsable)
    {
        object nResul;
        SqlParameter[] aParam = new SqlParameter[3];
        aParam[0] = new SqlParameter("@nIdGrupro", SqlDbType.Int, 4);
        aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
        aParam[2] = new SqlParameter("@nResponsable", SqlDbType.Bit, 1);
        aParam[0].Value = iCodGrupro;
        aParam[1].Value = iCodRecurso;
        aParam[2].Value = iResponsable;
        if (tr == null) nResul = SqlHelper.ExecuteScalar("SUP_GF_PROFI", aParam);
        else nResul = SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GF_PROFI", aParam);
        return int.Parse(nResul.ToString());
    }
    #endregion

    #region Modificar
    public static void ModificarGrupo(SqlTransaction tr, int iCodGrupro, string sDesc, int iIdCR)
    {
        SqlParameter[] aParam = new SqlParameter[3];
        aParam[0] = new SqlParameter("@nIdGF", SqlDbType.Int, 4);
        aParam[1] = new SqlParameter("@sDesc", SqlDbType.VarChar, 50);
        aParam[2] = new SqlParameter("@nIdCR", SqlDbType.Int, 4);
        aParam[0].Value = iCodGrupro;
        aParam[1].Value = sDesc;
        aParam[2].Value = iIdCR;
        if (tr == null) SqlHelper.ExecuteScalar("SUP_GFU", aParam);
        else SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GFU", aParam);
    }
    public static void ModificarProfesional(SqlTransaction tr, int iCodGrupro, int t314_idusuario, int iResponsable)
    {
        SqlParameter[] aParam = new SqlParameter[3];
        aParam[0] = new SqlParameter("@nIdGrupro", SqlDbType.Int, 4);
        aParam[1] = new SqlParameter("@nIdRecurso", SqlDbType.Int, 4);
        aParam[2] = new SqlParameter("@nResponsable", SqlDbType.Bit, 1);
        aParam[0].Value = iCodGrupro;
        aParam[1].Value = t314_idusuario;
        aParam[2].Value = iResponsable;
        if (tr == null) SqlHelper.ExecuteScalar("SUP_GF_PROFU", aParam);
        else SqlHelper.ExecuteScalarTransaccion(tr, "SUP_GF_PROFU", aParam);
    }
    #endregion

    #region Borrar
    public static void BorrarGrupo(SqlTransaction tr, int iCodGrupro)
    {
        int nResul;
        SqlParameter[] aParam = new SqlParameter[1];
        aParam[0] = new SqlParameter("@nCodigo", SqlDbType.Int, 4);
        aParam[0].Value = iCodGrupro;
        if (tr == null) nResul = SqlHelper.ExecuteNonQuery("SUP_GFD", aParam);
        else nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GFD", aParam);
        return;
    }
    public static void BorrarProfesionalGrupo(SqlTransaction tr, int iCodGrupro, int t314_idusuario)
    {
        int nResul;
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@nIdGrupro", SqlDbType.Int, 4);
        aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
        aParam[0].Value = iCodGrupro;
        aParam[1].Value = t314_idusuario;
        if (tr == null) nResul = SqlHelper.ExecuteNonQuery("SUP_GF_PROFD", aParam);
        else nResul = SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GF_PROFD", aParam);
        return;
    }
    #endregion
}
