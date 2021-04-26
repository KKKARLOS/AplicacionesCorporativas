using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

using SUPER.Capa_Datos;
//using SUPER.Capa_Negocio;
//Para usar el StringBuilder
using System.Text;
//Para usar el RegEx
using System.Text.RegularExpressions;
using SUPER.DAL;

/// <summary>
/// PLANTILLAS DE CURVIT
/// </summary>

namespace SUPER.BLL
{
    public partial class PLANTILLACVT
    {
        #region Propiedades y Atributos

        private int _t819_idplantillacvt;
        public int t819_idplantillacvt
        {
            get { return _t819_idplantillacvt; }
            set { _t819_idplantillacvt = value; }
        }

        private string _t819_denominacion;
        public string t819_denominacion
        {
            get { return _t819_denominacion; }
            set { _t819_denominacion = value; }
        }

        private string _t819_funcion;
        public string t819_funcion
        {
            get { return _t819_funcion; }
            set { _t819_funcion = value; }
        }

        private string _t819_observa;
        public string t819_observa
        {
            get { return _t819_observa; }
            set { _t819_observa = value; }
        }

        private int _t808_idexpprof;
        public int t808_idexpprof
        {
            get { return _t808_idexpprof; }
            set { _t808_idexpprof = value; }
        }
        private int _t035_idcodperfil;
        public int t035_idcodperfil
        {
            get { return _t035_idcodperfil; }
            set { _t035_idcodperfil = value; }
        }
        private string _t812_finicio;
        public string t812_finicio
        {
            get { return _t812_finicio; }
            set { _t812_finicio = value; }
        }

        private string _t812_ffin;
        public string t812_ffin
        {
            get { return _t812_ffin; }
            set { _t812_ffin = value; }
        }
        private int _t812_idexpprofficepi;
        public int t812_idexpprofficepi
        {
            get { return _t812_idexpprofficepi; }
            set { _t812_idexpprofficepi = value; }
        }

        private short _t020_idcodidioma;
        public short t020_idcodidioma
        {
            get { return _t020_idcodidioma; }
            set { _t020_idcodidioma = value; }
        }

        #endregion

        #region Constructor

        public PLANTILLACVT()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }
        public PLANTILLACVT(int t819_idplantillacvt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);

            SqlDataReader dr = SUPER.DAL.PLANTILLACVT.Select(t819_idplantillacvt);
            if (dr.Read())
            {
                if (dr["t819_idplantillacvt"] != DBNull.Value)
                    this.t819_idplantillacvt = int.Parse(dr["t819_idplantillacvt"].ToString());
                if (dr["t819_denominacion"] != DBNull.Value)
                    this.t819_denominacion = (string)dr["t819_denominacion"];
                if (dr["t819_funcion"] != DBNull.Value)
                    this.t819_funcion = (string)dr["t819_funcion"];
                if (dr["t819_observa"] != DBNull.Value)
                    this.t819_observa = (string)dr["t819_observa"];
                if (dr["t808_idexpprof"] != DBNull.Value)
                    this.t808_idexpprof = int.Parse(dr["t808_idexpprof"].ToString());
                if (dr["t035_idcodperfil"] != DBNull.Value)
                    this.t035_idcodperfil = int.Parse(dr["t035_idcodperfil"].ToString());
                if (dr["t020_idcodidioma"] != DBNull.Value)
                    this.t020_idcodidioma = short.Parse(dr["t020_idcodidioma"].ToString());

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PLANTILLACVT"));
            }
            dr.Close();
            dr.Dispose();
        }

        #endregion

        public static string GetPlantillas(int t808_idexpprof)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SUPER.DAL.PLANTILLACVT.GetPlantillas(t808_idexpprof);

            sb.Append("<table id='tblDatos' class='texto MA' style='width:800px; table-layout:fixed; border-collapse:collapse;' cellspacing='0' border='0'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:350px;' />");
            sb.Append("<col style='width:225px;' />");
            sb.Append("<col style='width:225px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t819_idplantillacvt"].ToString() + "' onclick='ms(this)' ondblclick='md(this.rowIndex)' style='height:16px;'>");
                sb.Append("<td><nobr class='NBR W340' onmouseover='TTip(event);'>" + dr["t819_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W220' onmouseover='TTip(event);'>" + dr["t035_descripcion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W220' onmouseover='TTip(event);'>" + dr["t819_funcion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }

        public static int Grabar(SqlTransaction tr, int idEP, int idPlant, string sDen, string sFun, string sObs, int? idPerfil, short t020_idcodidioma)
        {
            if (idPlant == -1)
                idPlant = SUPER.DAL.PLANTILLACVT.Insert(tr, sDen, sFun, sObs, idEP, idPerfil, t020_idcodidioma);
            else
                SUPER.DAL.PLANTILLACVT.Update(tr, idPlant, sDen, sFun, sObs, idEP, idPerfil, t020_idcodidioma);
            
            return idPlant;
        }

        public static string Borrar(string strDatos)
        {
            string sResul = "OK@#@";
            string[] aElem = Regex.Split(strDatos, "##");
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            #region abrir conexión y transacción
            try
            {
                oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion
            try
            {
                for (int i = 0; i < aElem.Length; i++)
                {
                    if (aElem[i] != "")
                    {
                        SUPER.DAL.PLANTILLACVT.Delete(tr, int.Parse(aElem[i]));
                    }
                }
                SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al borrar la plantilla.", ex);
            }
            finally
            {
                SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return sResul;
        }

        public static PLANTILLACVT Detalle(int t819_idplantillacvt, int t001_idficepi)
        {
            PLANTILLACVT pl = new PLANTILLACVT();
            SqlDataReader dr = SUPER.DAL.PLANTILLACVT.Detalle(t819_idplantillacvt,t001_idficepi);
            
            if (dr.Read())
            {
                if (dr["T819_IDPLANTILLACVT"] != DBNull.Value)
                    pl.t819_idplantillacvt = int.Parse(dr["T819_IDPLANTILLACVT"].ToString());
                if (dr["T812_FINICIO"]!=DBNull.Value)
                    pl.t812_finicio = dr["T812_FINICIO"].ToString();
                if (dr["T812_FFIN"] != DBNull.Value)
                    pl.t812_ffin = dr["T812_FFIN"].ToString();
                if (dr["T819_FUNCION"] != DBNull.Value)
                    pl.t819_funcion = (string)dr["T819_FUNCION"];
                if (dr["T819_OBSERVA"] != DBNull.Value)
                    pl.t819_observa = (string)dr["T819_OBSERVA"];
                if (dr["T035_IDCODPERFIL"] != DBNull.Value)
                    pl.t035_idcodperfil = int.Parse(dr["T035_IDCODPERFIL"].ToString());
                if (dr["T812_IDEXPPROFFICEPI"] != DBNull.Value)
                    pl.t812_idexpprofficepi = int.Parse(dr["T812_IDEXPPROFFICEPI"].ToString());
                if (dr["t020_idcodidioma"] != DBNull.Value)
                    pl.t020_idcodidioma = short.Parse(dr["t020_idcodidioma"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de PLANTILLACVT"));
            }
            return pl;
        }

    }
}