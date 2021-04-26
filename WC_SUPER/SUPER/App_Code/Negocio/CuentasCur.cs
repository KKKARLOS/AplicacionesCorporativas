using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SUPER.Capa_Datos;
using SUPER.DAL;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Negocio
{
    public partial class CUENTASCUR
    {
        #region Propiedades y Atributos

        private int _cu_id;
        public int cu_id
        {
            get { return _cu_id; }
            set { _cu_id = value; }
        }

        private string _cu_nombre;
        public string cu_nombre
        {
            get { return _cu_nombre; }
            set { _cu_nombre = value; }
        }

        private decimal _cu_vn;
        public decimal cu_vn
        {
            get { return _cu_vn; }
            set { _cu_vn = value; }
        }

        private bool _cu_escliente;
        public bool cu_escliente
        {
            get { return _cu_escliente; }
            set { _cu_escliente = value; }
        }
        private DateTime? _cu_fecha;
        public DateTime? cu_fecha
        {
            get { return _cu_fecha; }
            set { _cu_fecha = value; }
        }
        private int? _t484_idsegmento;
        public int? t484_idsegmento
        {
            get { return _t484_idsegmento; }
            set { _t484_idsegmento = value; }
        }
        #endregion

        #region Constructores

        public CUENTASCUR()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos
        public static CUENTASCUR Obtener(SqlTransaction tr, int cu_id)
        {
            CUENTASCUR o = new CUENTASCUR();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@cu_id", SqlDbType.Int, 4);
            aParam[0].Value = cu_id;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("CU_CTA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "CU_CTA_S", aParam);

            if (dr.Read())
            {
                if (dr["cu_id"] != DBNull.Value)
                    o.cu_id = int.Parse(dr["cu_id"].ToString());
                    //o.cu_id = (int)dr["cu_id"];
                if (dr["cu_nombre"] != DBNull.Value)
                    o.cu_nombre = (string)dr["cu_nombre"];
                if (dr["cu_vn"] != DBNull.Value)
                    o.cu_vn = (decimal)dr["cu_vn"];
                if (dr["cu_escliente"] != DBNull.Value)
                    o.cu_escliente = (bool)dr["cu_escliente"];
                if (dr["cu_fecha"] != DBNull.Value)
                    o.cu_fecha = (DateTime)dr["cu_fecha"];
                if (dr["t484_idsegmento"] != DBNull.Value)
                    o.t484_idsegmento = int.Parse(dr["t484_idsegmento"].ToString()); // (int)dr["t484_idsegmento"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de la cuenta"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }

        public static string Grabar(string strDatos)
        {
            string sResul = "", sDesc = "", sElementosInsertados = "";
            int nAux = 0;

            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            try
            {
                string[] aCuenta = Regex.Split(strDatos, "///");
                foreach (string oCuenta in aCuenta)
                {
                    if (oCuenta == "") continue;
                    string[] aValores = Regex.Split(oCuenta, "##");
                    //0. Opcion BD. "I", "U", "D"
                    //1. ID 
                    //2. Descripcion
                    //3. Volumen de negocio
                    //4. Es cliente

                    switch (aValores[0])
                    {
                        case "I":
                            nAux = SUPER.DAL.CUENTASCUR.Insert(tr, Utilidades.unescape(aValores[2]), decimal.Parse(aValores[3]), (aValores[4] == "1") ? true : false);
                            if (sElementosInsertados == "") sElementosInsertados = nAux.ToString();
                            else sElementosInsertados += "//" + nAux.ToString();
                            break;
                        case "U":
                            SUPER.DAL.CUENTASCUR.Update(tr, int.Parse(aValores[1]), Utilidades.unescape(aValores[2]), decimal.Parse(aValores[3]), (aValores[4] == "1") ? true : false);
                            break;
                        case "D":
                            SUPER.DAL.CUENTASCUR.Delete(tr, int.Parse(aValores[1]));
                            break;
                    }
                }
                Conexion.CommitTransaccion(tr);

                sResul = "OK@#@" + sElementosInsertados;
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al grabar las cuentas.", ex, false) + "@#@" + sDesc;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }

            return sResul;
        }
        public static string obtenerCuentas(string sDenominacion)
        {
            StringBuilder sb = new StringBuilder();

            SqlDataReader dr = SUPER.DAL.CUENTASCUR.Catalogo(sDenominacion);

            sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 500px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' /><col style='width:290px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>");
            sb.Append("<tbody id='tbodyDatos'>");

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["cu_id"].ToString() + "' bd='' onclick='mm(event)' style='height:20px'>");
                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td style='padding-left:5px;'><input type='text' class='txtL' style='width:270px' value=\"" + dr["cu_nombre"].ToString() + "\" maxlength='25' onKeyUp='fm(event)'></td>");
                sb.Append("<td><input type='text' class='txtNumL' style='width:95px;' value=\"" + decimal.Parse(dr["cu_vn"].ToString()).ToString("N") + "\" onKeyUp='fm(event)' onfocus='fn(this,6,2)'></td>");

                sb.Append("<td align='center'><input type='checkbox' class='check' onclick='fm(event)' ");
                if ((bool)dr["cu_escliente"]) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }

        #endregion
    }
}
