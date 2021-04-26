using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class INCENTIVOSPRODUCTIVIDAD
    {
        #region Metodos

        public static string ObtenerIncentivos()
        {
            StringBuilder sb = new StringBuilder();

            #region Cabecera tabla HTML
            sb.Append("<table id='tblDatos' style='width:970px;'>");
			sb.Append("    <colgroup>");
            sb.Append("    <col style='width:40px;' />");
            sb.Append("    <col style='width:70px;' />");
            sb.Append("    <col style='width:280px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("    <col style='width:50px;' />");
            sb.Append("    <col style='width:220px;' />");
            sb.Append("    <col style='width:80px;' />");
            sb.Append("    <col style='width:80px;' />");
            sb.Append("    <col style='width:100px;' />");
            sb.Append("</colgroup>");
            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.INCENTIVOSPRODUCTIVIDAD.ObtenerIncentivos();
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t726_idincentivo"].ToString() + "' ");
                sb.Append("idiberper='" + dr["t726_idiberper"].ToString() + "' ");
                sb.Append("idficepi='" + dr["t001_idficepi"].ToString() + "' ");
                sb.Append("idusuario='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("idnodo_usuario='" + dr["t303_idnodo_usuario"].ToString() + "' ");
                sb.Append("importe='" + dr["t726_importe"].ToString() + "' ");
                sb.Append("importeSS='" + dr["t726_importeSS"].ToString() + "' ");
                sb.Append("idproyecto='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("anomes='" + dr["anomesimputacion"].ToString() + "' ");
                sb.Append("style='height:20px;' ");
                if (dr["t314_idusuario"].ToString() == "" && dr["t001_idficepi"].ToString() != "")
                    sb.Append("onclick='ms(this);'");
                sb.Append(">");

                if (dr["t314_idusuario"].ToString() != "")
                    sb.Append("<td style='text-align:center;'><input type='checkbox' class='check' checked></td>");
                else
                    sb.Append("<td><input style='text-align:center;' type='checkbox' class='check' disabled></td>");

                if (dr["t001_idficepi"].ToString() != "")
                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)dr["t726_idiberper"]).ToString("#,###") + "</td>");
                else
                    sb.Append("<td style='color:red;text-align:right; padding-right:5px;'>" + ((int)dr["t726_idiberper"]).ToString("#,###") + "</td>");

                sb.Append("<td><nobr class='NBR W270'>" + dr["Profesional"].ToString() + "</nobr></td>");

                if (dr["t314_idusuario"].ToString() == "")// && dr["t001_idficepi"].ToString() != "")
                    sb.Append("<td style='text-align:right; padding-right:5px;' class='MA' ondblclick='getProfesional(this, event)'>" + ((dr["t314_idusuario"] == DBNull.Value) ? "" : ((int)dr["t314_idusuario"]).ToString("#,###")) + "</td>");
                else
                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((dr["t314_idusuario"] == DBNull.Value) ? "" : ((int)dr["t314_idusuario"]).ToString("#,###")) + "</td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                sb.Append("<td><div class='NBR W220' onmouseover='TTip(event)'>" + dr["t301_denominacion"].ToString() + "</div></td>");
                sb.Append("<td style='text-align:right; padding-right:3px;'>" + decimal.Parse(dr["t726_importe"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right; padding-right:3px;'>" + decimal.Parse(dr["t726_importeSS"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='padding-left:5px;'>" + Fechas.AnnomesAFechaDescLarga((int)dr["anomesimputacion"]) + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return sb.ToString();
        }

        public static string ObtenerUsuarios(int t001_idficepi)
        {
            StringBuilder sb = new StringBuilder();
            int nIdFicepi = 0;
            string sProfesional = "";

            #region Cabecera tabla HTML
            sb.Append("<table id='tblDatos' class='texto MA' border='0' style='width:800px; table-layout:fixed; border-collapse:collapse;'>");
            sb.Append("<colgroup>");
			sb.Append("	<col style='width:60px;' />");
			sb.Append("	<col style='width:220px;' />");
			sb.Append("	<col style='width:220px;' />");
			sb.Append("	<col style='width:60px;' />");
            sb.Append("	<col style='width:60px;' />");
            sb.Append("</colgroup>");
            #endregion

            SqlDataReader dr = SUPER.Capa_Datos.INCENTIVOSPRODUCTIVIDAD.ObtenerUsuarios(t001_idficepi);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    nIdFicepi = (int)dr["t001_idficepi"];
                    sProfesional = dr["Profesional"].ToString();
                }
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' ");
                sb.Append("idnodo='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("profesional=\"" + Utilidades.escape(dr["Profesional"].ToString()) + "\" ");
                sb.Append("style='height:20px;' ");
                sb.Append("onclick='ms(this);' ondblclick='aceptarClick(this.rowIndex);'");
                sb.Append(">");

                sb.Append("<td style='text-align:right; padding-right:10px;'>" + ((int)dr["t314_idusuario"]).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W200'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W200'>" + dr["t313_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + ((DateTime)dr["t314_falta"]).ToShortDateString() + "</td>");
                sb.Append("<td>" + ((dr["t314_fbaja"] == DBNull.Value) ? "" : ((DateTime)dr["t314_fbaja"]).ToShortDateString()) + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            sb.Append("@#@"+ nIdFicepi.ToString());
            sb.Append("@#@" + sProfesional);

            return sb.ToString();
        }


        public static string Procesar(string sRegistros, string strDatos)
        {
            string sResul = "", sEstadoMes = "";
            int nPSN_C = 0, nPSN_J = 0, nPSN_P = 0, nSegMesProy = 0;
            int nIDEmpresaNodoProyecto_C = 0;
            int nNodoUsuario = 0, nEmpresaNodoUsuario = 0, nNodo_C = 0, nNodo_J = 0, nNodo_P = 0;
            decimal nTipoCambio_C = 0,nTipoCambio_J = 0,nTipoCambio_P = 0;
            
            SqlConnection oConn = null;
            SqlTransaction tr = null;
            bool bErrorControlado = false;

            string[] aIncentivos = Regex.Split(strDatos, "#reg#");

            #region Abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                throw (new Exception("Error al abrir la conexión."));
            }
            #endregion

            try
            {

                //inserto los registros seleccionados en la T726_INCENTIVOSPRODUCTIVIDAD
                Capa_Datos.INCENTIVOSPRODUCTIVIDAD.Insertar(tr, sRegistros);

                //Trato los registros seleccionados
                int i = 0;
                foreach (string oIncentivo in aIncentivos)
                {
                    i++;
                    if (oIncentivo == "") continue;
                    string[] aDatos = Regex.Split(oIncentivo, "#sep#");

                    #region Datos Incentivo
                    //aDatos[0] = id incentivo //0
                    //aDatos[1] = idusuario //1
                    //aDatos[2] = idnodo_usuario //2
                    //aDatos[3] = importe //3
                    //aDatos[4] = idproyecto //4
                    //aDatos[5] = anomes //5
                    //aDatos[6] = Profesional //6
                    //aDatos[7] = Denominación proyecto //7
                    //aDatos[8] = importe SS //8
                    #endregion

                    #region inicializar variables
                    nPSN_C = 0;
                    nPSN_J = 0;
                    nPSN_P = 0;
                    nSegMesProy = 0;
                    nIDEmpresaNodoProyecto_C = 0;
                    nNodoUsuario = 0;
                    nEmpresaNodoUsuario = 0;
                    nNodo_C = 0;
                    nNodo_J = 0;
                    nNodo_P = 0;
                    nTipoCambio_C = 0;
                    nTipoCambio_J = 0;
                    nTipoCambio_P = 0;
                    #endregion

                    DataSet ds = Capa_Datos.INCENTIVOSPRODUCTIVIDAD.ObtenerInstanciasProyecto(tr, int.Parse(aDatos[4]), int.Parse(aDatos[1]), int.Parse(aDatos[5]));
                    foreach (DataRow oFila in ds.Tables[0].Rows)
                    {
                        switch (oFila["t305_cualidad"].ToString())
                        {
                            case "C":
                                nPSN_C = (int)oFila["t305_idproyectosubnodo"];
                                nNodo_C = (int)oFila["t303_idnodo"];
                                nIDEmpresaNodoProyecto_C = (int)oFila["t313_idempresa_nodo"];
                                nTipoCambio_C = decimal.Parse(oFila["t699_tipocambio"].ToString());
                                break;
                            case "J": 
                                nPSN_J = (int)oFila["t305_idproyectosubnodo"]; 
                                nNodo_J = (int)oFila["t303_idnodo"];
                                nTipoCambio_J = decimal.Parse(oFila["t699_tipocambio"].ToString());
                                break;
                            case "P": 
                                nPSN_P = (int)oFila["t305_idproyectosubnodo"]; 
                                nNodo_P = (int)oFila["t303_idnodo"];
                                nTipoCambio_P = decimal.Parse(oFila["t699_tipocambio"].ToString());
                                break;
                        }
                        if (nNodoUsuario == 0)
                            nNodoUsuario = (int)oFila["t303_idnodo_usuario"];
                        if (nEmpresaNodoUsuario == 0)
                            nEmpresaNodoUsuario = (int)oFila["t313_idempresa_nodousuario"];
                    }

                    if (nPSN_C == 0)
                    {
                        sResul = "Instancia contratante del proyecto económico no existente";
                        bErrorControlado = true;
                        throw (new Exception(sResul));
                    }

                    if (nPSN_P != 0) //Réplica con gestión
                    {
                        nSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, nPSN_P, int.Parse(aDatos[5]));
                        if (nSegMesProy == 0)
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, nPSN_P, int.Parse(aDatos[5]));
                            nSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, nPSN_P, int.Parse(aDatos[5]), sEstadoMes, 0, 0, false, 0, 0);
                        }
                        DATOECO.Insert(tr, nSegMesProy, Constantes.nIdClaseProductividad, Utilidades.unescape(aDatos[6]), decimal.Parse(aDatos[3]) * nTipoCambio_P, null, null, 4);
                        DATOECO.Insert(tr, nSegMesProy, Constantes.nIdClaseProductividadSS, Utilidades.unescape(aDatos[6]), decimal.Parse(aDatos[8]) * nTipoCambio_P, null, null, 4);
                    }
                    else if (nNodo_C == nNodoUsuario) //El usuario pertenece a la Contratante
                    {
                        nSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, nPSN_C, int.Parse(aDatos[5]));
                        if (nSegMesProy == 0)
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, nPSN_C, int.Parse(aDatos[5]));
                            nSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, nPSN_C, int.Parse(aDatos[5]), sEstadoMes, 0, 0, false, 0, 0);
                        }
                        DATOECO.Insert(tr, nSegMesProy, Constantes.nIdClaseProductividad, Utilidades.unescape(aDatos[6]), decimal.Parse(aDatos[3]) * nTipoCambio_C, null, null, 4);
                        DATOECO.Insert(tr, nSegMesProy, Constantes.nIdClaseProductividadSS, Utilidades.unescape(aDatos[6]), decimal.Parse(aDatos[8]) * nTipoCambio_C, null, null, 4);
                    }
                    else //Réplica sin gestión
                    {
                        //Para la réplica sin gestión
                        if (nNodo_J == 0) //no existe la réplica sin gestión, por lo que hay que crearla.
                        {
                            #region Creación de la instancia de proyecto
                            int nCountManiobraTipo1 = 0, idNodoAuxManiobra = 0, nCountSubnodosNoManiobra = 0, idNodoAuxDestino = 0, idSubNodoGrabar = 0;
                            int nResponsablePSN = 0;
                            DataSet dsSubnodos = PROYECTOSUBNODO.ObtenerSubnodosParaReplicar(tr, nNodoUsuario);
                            foreach (DataRow oFila in dsSubnodos.Tables[0].Rows)
                            {
                                if ((byte)oFila["t304_maniobra"] == 1)
                                {
                                    nCountManiobraTipo1++;
                                    idNodoAuxManiobra = (int)oFila["t304_idsubnodo"];
                                }
                                else
                                {
                                    nCountSubnodosNoManiobra++;
                                    idNodoAuxDestino = (int)oFila["t304_idsubnodo"];
                                }
                            }

                            if (nCountSubnodosNoManiobra == 1) //si solo hay un subnodo en el nodo, que la réplica se haga a ese subnodo.
                            {
                                idSubNodoGrabar = idNodoAuxDestino;
                            }
                            else
                            {
                                if (nCountManiobraTipo1 == 0)
                                {
                                    NODO oNodo2 = NODO.SelectEnTransaccion(tr, nNodoUsuario);
                                    nResponsablePSN = oNodo2.t314_idusuario_responsable;
                                    //crear subnodo maniobra
                                    idSubNodoGrabar = SUBNODO.Insert(tr, "Proyectos a reasignar", nNodoUsuario, 0, true, 1, oNodo2.t314_idusuario_responsable,null);
                                }
                                else
                                {
                                    if (nCountManiobraTipo1 > 1)
                                    {
                                        ds.Dispose();
                                        throw (new Exception("El número de subnodos de maniobra es " + nCountManiobraTipo1.ToString() + " en el nodo " + nNodoUsuario.ToString() + ". Por favor avise al administrador."));
                                    }

                                    if (ds.Tables[0].Rows.Count - 1 > 1 || ds.Tables[0].Rows.Count - 1 == 0)
                                    {
                                        idSubNodoGrabar = idNodoAuxManiobra;
                                    }
                                    else
                                    {
                                        idSubNodoGrabar = idNodoAuxDestino;
                                    }
                                }
                            }
                            ds.Dispose();

                            if (nResponsablePSN == 0)
                            {
                                NODO oNodo3 = NODO.SelectEnTransaccion(tr, nNodoUsuario);
                                nResponsablePSN = oNodo3.t314_idusuario_responsable;
                            }
                            if (nTipoCambio_J == 0)
                            {
                                //hay que obtener el tipo de cambio de la moneda por defecto del nodo en el mes del incentivo.
                                nTipoCambio_J = NODO.getTipocambioMonedaNodoMes(tr, nNodoUsuario, int.Parse(aDatos[5]));
                            }
                            nPSN_J = PROYECTOSUBNODO.Insert(tr, int.Parse(aDatos[4]), idSubNodoGrabar, false, "J",
                                                        false, nResponsablePSN, Utilidades.unescape(aDatos[7]), "X", "X",
                                                        false, false, false, false, false, "", "",
                                                        "", null, null, null, null, null, null, false, 0);
                            #endregion
                        }

                        nSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, nPSN_J, int.Parse(aDatos[5]));
                        if (nSegMesProy == 0)
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, nPSN_J, int.Parse(aDatos[5]));
                            nSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, nPSN_J, int.Parse(aDatos[5]), sEstadoMes, 0, 0, false, 0, 0);
                        }

                        DATOECO.Insert(tr, nSegMesProy, Constantes.nIdClaseProductividad, Utilidades.unescape(aDatos[6]), decimal.Parse(aDatos[3]) * nTipoCambio_J, null, null, 4);
                        DATOECO.Insert(tr, nSegMesProy, Constantes.nIdClaseProductividadSS, Utilidades.unescape(aDatos[6]), decimal.Parse(aDatos[8]) * nTipoCambio_J, null, null, 4);


                        //Para la contratante (se añade el nodo destino)
                        nSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, nPSN_C, int.Parse(aDatos[5]));
                        if (nSegMesProy == 0)
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, nPSN_C, int.Parse(aDatos[5]));
                            nSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, nPSN_C, int.Parse(aDatos[5]), sEstadoMes, 0, 0, false, 0, 0);
                        }
                        DATOECO.Insert(tr, nSegMesProy, (nIDEmpresaNodoProyecto_C == nEmpresaNodoUsuario) ? 4 : 6, Utilidades.unescape(aDatos[6]), (decimal.Parse(aDatos[3]) + decimal.Parse(aDatos[8])) * nTipoCambio_C, nNodoUsuario, null, 4);
                    }

                    Capa_Datos.INCENTIVOSPRODUCTIVIDAD.Registrar(tr, int.Parse(aDatos[0]));
                }

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                if (bErrorControlado) sResul = ex.Message;
                else sResul = Errores.mostrarError("Error al procesar.", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
                if (sResul != "")
                {
                    if (bErrorControlado) sResul = "ErrorControlado##EC##" + sResul;
                    throw (new Exception(sResul));
                }
            }

            return "";
        }
        #endregion
    }
}
