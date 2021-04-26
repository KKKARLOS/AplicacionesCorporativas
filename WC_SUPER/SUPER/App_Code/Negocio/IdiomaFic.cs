using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;
using SUPER.BLL;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de IdiomaFic
    /// </summary>
    public partial class IdiomaFic
    {
        #region Propiedades y Atributos

        private int _T013_LECTURA;
        public int T013_LECTURA
        {
            get { return _T013_LECTURA; }
            set { _T013_LECTURA = value; }
        }

        private int _T013_ESCRITURA;
        public int T013_ESCRITURA
        {
            get { return _T013_ESCRITURA; }
            set { _T013_ESCRITURA = value; }
        }

        private int _T013_ORAL;
        public int T013_ORAL
        {
            get { return _T013_ORAL; }
            set { _T013_ORAL = value; }
        }
        private int _T020_IDCODIDIOMA;
        public int T020_IDCODIDIOMA
        {
            get { return _T020_IDCODIDIOMA; }
            set { _T020_IDCODIDIOMA = value; }
        }

        private string _T020_DESCRIPCION;
        public string T020_DESCRIPCION
        {
            get { return _T020_DESCRIPCION; }
            set { _T020_DESCRIPCION = value; }
        }

        private int _T001_IDFICPEI;
        public int T001_IDFICPEI
        {
            get { return _T001_IDFICPEI; }
            set { _T001_IDFICPEI = value; }
        }

        #endregion

        #region Constructor

        public IdiomaFic()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #endregion

        #region Metodos

        public static IdiomaFic Detalle(int t001_idficepi, int t020_idcodidioma)
        {
            IdiomaFic o = new IdiomaFic();
            SqlDataReader dr = SUPER.Capa_Datos.IdiomaFic.Detalle(t001_idficepi, t020_idcodidioma);

            if (dr.Read())
            {
                if (dr["T020_DESCRIPCION"] != DBNull.Value)
                    o.T020_DESCRIPCION = (string)dr["T020_DESCRIPCION"];
                if (dr["T013_LECTURA"] != DBNull.Value)
                    o.T013_LECTURA = int.Parse(dr["T013_LECTURA"].ToString());
                if (dr["T013_ESCRITURA"] != DBNull.Value)
                    o.T013_ESCRITURA = int.Parse(dr["T013_ESCRITURA"].ToString());
                if (dr["T013_ORAL"] != DBNull.Value)
                    o.T013_ORAL = int.Parse(dr["T013_ORAL"].ToString());
                if (dr["T020_IDCODIDIOMA"] != DBNull.Value)
                    o.T020_IDCODIDIOMA = int.Parse(dr["T020_IDCODIDIOMA"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de Idioma"));
            }

            dr.Close();
            dr.Dispose();

            return o;

        }

        public static void Grabar(Nullable<int> idcodidioma, int t020_idcodidioma, int? t013_lectura, int? t013_escritura, 
                                  int? t013_oral, int t001_idficepi)
        {
            if (idcodidioma!=null)
                SUPER.Capa_Datos.IdiomaFic.Update(t001_idficepi, t020_idcodidioma, t013_lectura, t013_escritura, t013_oral, (int)idcodidioma);
            else
                SUPER.Capa_Datos.IdiomaFic.Insert(t001_idficepi, t020_idcodidioma, t013_lectura, t013_escritura, t013_oral);  
        }

        public static string Borrar(int idFicepi, string sIdiomasTitulos, int IdficepiEntrada)
        {
            string sRes = "OK@#@";
            try
            {
                #region Inicio Transacción
                SqlConnection oConn;
                SqlTransaction tr;
                try
                {
                    oConn = SUPER.Capa_Negocio.Conexion.Abrir();
                    tr = SUPER.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
                }
                catch (Exception ex)
                {
                    throw (new Exception("Error al abrir la conexion", ex));
                }

                #endregion
                try
                {
                    string[] aReg = Regex.Split(sIdiomasTitulos, "##");
                    foreach (string oReg in aReg)
                    {
                        if (oReg == "") continue;
                        string[] aElem = Regex.Split(oReg, "//");
                        if (aElem[0] == "I")
                        {
                            SUPER.Capa_Datos.IdiomaFic.Delete(tr, idFicepi, short.Parse(aElem[1]));
                        }
                        else
                        {
                            if (aElem[0] == "T")
                                SUPER.DAL.TituloIdiomaFic.Delete(tr, int.Parse(aElem[1]));
                        }
                    }
                    if (idFicepi == IdficepiEntrada)
                        SUPER.DAL.Curriculum.ActualizadoCV(tr, idFicepi);

                    SUPER.Capa_Negocio.Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    SUPER.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                    throw ex;
                }
                finally
                {
                    SUPER.Capa_Negocio.Conexion.Cerrar(oConn);
                }
            }
            catch (Exception ex)
            {
                sRes = "ERROR@#@" + ex.Message;
            }
            return sRes;
        }
        

        #endregion
    }
}