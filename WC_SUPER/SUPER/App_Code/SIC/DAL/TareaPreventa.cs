using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.SIC.Models;
using System.Collections;

/// <summary>
/// Summary description for TareaPreventa
/// </summary>

namespace IB.SUPER.SIC.DAL
{

    internal class TareaPreventa
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            ta207_idtareapreventa = 1,
            ta204_idaccionpreventa = 2,
            t001_idficepi_promotor = 3,
            ta207_descripcion = 4,
            ta207_fechaprevista = 5,
            ta207_estado = 6,
            ta207_motivoanulacion = 7,
            t001_idficepi_participante = 8,
            ta214_estado = 9,
            ta199_idunidadpreventa = 10,
            ta200_idareapreventa  = 11,
            ta201_idsubareapreventa = 12,            
            t001_idficepi_lider = 13,
            ta207_denominacion = 14,
            ta207_observaciones = 15,
            guidprovisional = 16,
            ta207_comentario = 17,
            t001_idficepi = 18,
            datatable = 19,
            ta205_idtipoaccionpreventa = 20,
            t001_idficepi_conectado= 21,
            t001_idficepi_ultmodificador = 22,
            ta219_idtipotareapreventa = 23                
        }

        internal TareaPreventa(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas
        /// <summary>
        /// Inserta un registro en la tabla TA207_TAREAPREVENTA
        /// </summary>
        /// <param name="oTareaPreventa"></param>
        /// <returns></returns>
        internal int Insert(Models.TareaPreventa oTareaPreventa, DataTable listaParticipantes)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[9] {
                    Param(enumDBFields.ta204_idaccionpreventa, oTareaPreventa.ta204_idaccionpreventa),
                    Param(enumDBFields.t001_idficepi_promotor, oTareaPreventa.t001_idficepi_promotor),
                    Param(enumDBFields.ta207_descripcion, oTareaPreventa.ta207_descripcion),
                    Param(enumDBFields.ta219_idtipotareapreventa, oTareaPreventa.ta219_idtipotareapreventa),                    
                    Param(enumDBFields.ta207_denominacion, oTareaPreventa.ta207_denominacion),
                    Param(enumDBFields.ta207_observaciones, oTareaPreventa.ta207_observaciones),
                    Param(enumDBFields.ta207_fechaprevista, oTareaPreventa.ta207_fechaprevista),										
                    Param(enumDBFields.guidprovisional, oTareaPreventa.uidDocumento),
                    Param(enumDBFields.datatable, listaParticipantes)                    
                };

                return (int)cDblib.ExecuteScalar("SIC_TAREAPREVENTA_I", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene un TareaPreventa a partir del id
        /// </summary>
        internal Models.TareaPreventa Select(Int32 ta207_idtareapreventa)
        {
            Models.TareaPreventa oTareaPreventa = null;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa)
				};

                dr = cDblib.DataReader("SUPER.SIC_TareaPreventa_SEL", dbparams);
                if (dr.Read())
                {
                    oTareaPreventa = new Models.TareaPreventa();
                    oTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oTareaPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    oTareaPreventa.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    oTareaPreventa.ta207_descripcion = Convert.ToString(dr["ta207_descripcion"]);
                    oTareaPreventa.ta207_fechaprevista = Convert.ToDateTime(dr["ta207_fechaprevista"]);
                    oTareaPreventa.ta207_estado = Convert.ToString(dr["ta207_estado"]);
                    if (!Convert.IsDBNull(dr["ta207_motivoanulacion"]))
                        oTareaPreventa.ta207_motivoanulacion = Convert.ToString(dr["ta207_motivoanulacion"]);

                }
                return oTareaPreventa;

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

        /// <summary>
        /// Actualiza un TareaPreventa a partir del id
        /// </summary>
        internal int Update(Models.TareaPreventa oTarea, DataTable listaParticipantes, Int32 t001_idficepi_ultmodificador)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[10] {
					Param(enumDBFields.ta207_idtareapreventa, oTarea.ta207_idtareapreventa),															
                    Param(enumDBFields.ta207_estado, oTarea.ta207_estado),					
                    Param(enumDBFields.ta207_denominacion, oTarea.ta207_denominacion),
                    Param(enumDBFields.ta219_idtipotareapreventa, oTarea.ta219_idtipotareapreventa),
                    Param(enumDBFields.ta207_fechaprevista, oTarea.ta207_fechaprevista),					
                    Param(enumDBFields.ta207_descripcion, oTarea.ta207_descripcion),					
                    Param(enumDBFields.ta207_observaciones, oTarea.ta207_observaciones),
                    Param(enumDBFields.ta207_motivoanulacion, oTarea.ta207_motivoanulacion),
                    Param(enumDBFields.datatable, listaParticipantes),
                    Param(enumDBFields.t001_idficepi_ultmodificador, t001_idficepi_ultmodificador)

				};
                int r = (int)cDblib.ExecuteScalar("SIC_TAREAPREVENTA_U", dbparams);

                if (r == 0) throw new Exception("No se ha actualizado la tarea");
                return r;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //internal int UpdateEstadoTarea(int ta207_idtarea, string ta207_estado)
        //{
        //    try
        //    {
        //        SqlParameter[] dbparams = new SqlParameter[2] {
        //            Param(enumDBFields.ta207_idtareapreventa, ta207_idtarea),															
        //            Param(enumDBFields.ta207_estado, ta207_estado),					
        //        };

        //        return (int)cDblib.Execute("SIC_TAREAPREVENTA_ESTADO_U", dbparams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        internal int UpdateParticipante(Models.TareaPreventa oTareaPreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta207_idtareapreventa, oTareaPreventa.ta207_idtareapreventa),
                    Param(enumDBFields.ta207_comentario, oTareaPreventa.ta207_comentario)
                    
                };

                return (int)cDblib.ExecuteScalar("SIC_TAREAPREVENTA_PARTICIPANTE_U", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina un TareaPreventa a partir del id
        /// </summary>
        internal int Delete(Int32 ta207_idtareapreventa)
        {
            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {
					Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa)
				};

                return (int)cDblib.Execute("SUPER.SIC_TareaPreventa_DEL", dbparams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene todos los TareaPreventa
        /// </summary>
        internal List<Models.TareaPreventa> Catalogo(Models.TareaPreventa oTareaPreventaFilter)
        {
            Models.TareaPreventa oTareaPreventa = null;
            List<Models.TareaPreventa> lst = new List<Models.TareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[6] {
					Param(enumDBFields.ta204_idaccionpreventa, oTareaPreventaFilter.ta204_idaccionpreventa),
					Param(enumDBFields.t001_idficepi_promotor, oTareaPreventaFilter.t001_idficepi_promotor),
					Param(enumDBFields.ta207_descripcion, oTareaPreventaFilter.ta207_descripcion),
					Param(enumDBFields.ta207_fechaprevista, oTareaPreventaFilter.ta207_fechaprevista),
					Param(enumDBFields.ta207_estado, oTareaPreventaFilter.ta207_estado),
					Param(enumDBFields.ta207_motivoanulacion, oTareaPreventaFilter.ta207_motivoanulacion)
				};

                dr = cDblib.DataReader("SUPER.SIC_TareaPreventa_CAT", dbparams);
                while (dr.Read())
                {
                    oTareaPreventa = new Models.TareaPreventa();
                    oTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oTareaPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    oTareaPreventa.t001_idficepi_promotor = Convert.ToInt32(dr["t001_idficepi_promotor"]);
                    oTareaPreventa.ta207_descripcion = Convert.ToString(dr["ta207_descripcion"]);
                    oTareaPreventa.ta207_fechaprevista = Convert.ToDateTime(dr["ta207_fechaprevista"]);
                    oTareaPreventa.ta207_estado = Convert.ToString(dr["ta207_estado"]);
                    if (!Convert.IsDBNull(dr["ta207_motivoanulacion"]))
                        oTareaPreventa.ta207_motivoanulacion = Convert.ToString(dr["ta207_motivoanulacion"]);

                    lst.Add(oTareaPreventa);

                }
                return lst;

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

        public Hashtable misTareasComoParticipante(int t001_idficepi)
        {
            Models.TareaPreventaCatalogoParticipante oTareaPreventa = null;
            List<Models.TareaPreventaCatalogoParticipante> lst = new List<Models.TareaPreventaCatalogoParticipante>();
            List<Models.TareaPreventaCatalogoParticipante> lst2 = new List<Models.TareaPreventaCatalogoParticipante>();
            Hashtable htListas = new Hashtable();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {                                  
					Param(enumDBFields.t001_idficepi, t001_idficepi)                    
				};

                dr = cDblib.DataReader("SIC_MISTAREASPARTICIPANTE_CAT", dbparams);
                while (dr.Read())
                {
                    oTareaPreventa = new Models.TareaPreventaCatalogoParticipante();
                    oTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oTareaPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);                    
                    oTareaPreventa.ta207_denominacion = Convert.ToString(dr["ta207_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta207_fechacreacion"]))  oTareaPreventa.ta207_fechacreacion = Convert.ToDateTime(dr["ta207_fechacreacion"]);
                    if (!Convert.IsDBNull(dr["ta207_fechafinprevista"]))  oTareaPreventa.ta207_fechafinprevista = Convert.ToDateTime(dr["ta207_fechafinprevista"]);
                    oTareaPreventa.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);                    
                    //oTareaPreventa.ta207_estado = Convert.ToString(dr["ta207_estado"]);
                    if (!Convert.IsDBNull(dr["ta201_denominacion"])) oTareaPreventa.ta201_denominacion = Convert.ToString(dr["ta201_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta200_denominacion"])) oTareaPreventa.ta200_denominacion = Convert.ToString(dr["ta200_denominacion"]);
                    if (!Convert.IsDBNull(dr["ta199_denominacion"])) oTareaPreventa.ta199_denominacion = Convert.ToString(dr["ta199_denominacion"]);
                    oTareaPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    oTareaPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    oTareaPreventa.den_item = Convert.ToString(dr["den_item"]);
                    oTareaPreventa.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    oTareaPreventa.den_cuenta = Convert.ToString(dr["den_cuenta"]);
                    oTareaPreventa.ta208_negrita = Convert.ToBoolean(dr["ta208_negrita"]);

                    oTareaPreventa.lider = Convert.ToString(dr["lider"]);
                    oTareaPreventa.solicitante = Convert.ToString(dr["solicitante"]);
                    oTareaPreventa.den_cuenta = Convert.ToString(dr["den_cuenta"]);

                    lst.Add(oTareaPreventa);
                }

                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        oTareaPreventa = new Models.TareaPreventaCatalogoParticipante();
                        oTareaPreventa.ta207_idtareapreventa_participante = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                        oTareaPreventa.participantes = Convert.ToString(dr["participante"]);
                        lst2.Add(oTareaPreventa);
                    }
                }

                htListas.Add("listaDatos", lst);
                htListas.Add("listaParticipantes", lst2);

                return htListas;

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

        
        public List<Models.TareaPreventaCatalogoParticipante> CatalogoParticipante(Nullable<int> ta199_idunidadpreventa, Nullable<int> ta200_idareapreventa, Nullable<int> ta201_idsubareapreventa, Nullable<int> ta204_idaccionpreventa, Nullable<int> ta205_idtipoaccionpreventa, int t001_idficepi, Nullable<int> t001_idficepi_lider, string ta214_estado)
        {
            Models.TareaPreventaCatalogoParticipante oTareaPreventa = null;
            List<Models.TareaPreventaCatalogoParticipante> lst = new List<Models.TareaPreventaCatalogoParticipante>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[8] {
                    Param(enumDBFields.ta199_idunidadpreventa, ta199_idunidadpreventa ),
                    Param(enumDBFields.ta200_idareapreventa, ta200_idareapreventa  ),
                    Param(enumDBFields.ta201_idsubareapreventa, ta201_idsubareapreventa   ),
                    Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa    ),  
                    Param(enumDBFields.ta205_idtipoaccionpreventa, ta205_idtipoaccionpreventa),                      
					Param(enumDBFields.t001_idficepi_participante, t001_idficepi),
                    Param(enumDBFields.t001_idficepi_lider , t001_idficepi_lider ),                    
                    Param(enumDBFields.ta214_estado, ta214_estado),
				};

                dr = cDblib.DataReader("SIC_TAREASPARTICIPANTE_CAT", dbparams);
                while (dr.Read())
                {
                    oTareaPreventa = new Models.TareaPreventaCatalogoParticipante();
                    oTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    oTareaPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);
                    oTareaPreventa.ta207_denominacion = Convert.ToString(dr["ta207_denominacion"]);
                    oTareaPreventa.ta207_fechacreacion = Convert.ToDateTime(dr["ta207_fechacreacion"]);
                    oTareaPreventa.ta207_fechafinprevista = Convert.ToDateTime(dr["ta207_fechafinprevista"]);
                    oTareaPreventa.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    oTareaPreventa.lider = Convert.ToString(dr["lider"]);
                    oTareaPreventa.ta207_estado = Convert.ToString(dr["ta207_estado"]);
                    oTareaPreventa.ta206_iditemorigen = Convert.ToInt32(dr["ta206_iditemorigen"]);
                    oTareaPreventa.ta206_itemorigen = Convert.ToString(dr["ta206_itemorigen"]);
                    oTareaPreventa.den_item = Convert.ToString(dr["den_item"]);
                    oTareaPreventa.ta206_denominacion = Convert.ToString(dr["ta206_denominacion"]);
                    oTareaPreventa.den_cuenta = Convert.ToString(dr["den_cuenta"]);

                    lst.Add(oTareaPreventa);
                }

                return lst;

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

        public Hashtable CatalogoPorAccion(int ta204_idaccionpreventa, int t001_idficepi)
        {
            Models.TareaPreventaCatalogoParticipante oTareaPreventa = null;
            List<Models.TareaPreventaCatalogoParticipante> lst = new List<Models.TareaPreventaCatalogoParticipante>();
            List<Models.TareaPreventaCatalogoParticipante> lst2 = new List<Models.TareaPreventaCatalogoParticipante>();
            
            Hashtable htListas = new Hashtable();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {
                    Param(enumDBFields.ta204_idaccionpreventa, ta204_idaccionpreventa),              
                    Param(enumDBFields.t001_idficepi, t001_idficepi)                    
				};

                dr = cDblib.DataReader("SIC_TAREASDEUNAACCIONPREVENTA_CAT", dbparams);

                
                while (dr.Read())
                {
                    oTareaPreventa = new Models.TareaPreventaCatalogoParticipante();
                    oTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);                    
                    oTareaPreventa.ta207_denominacion = Convert.ToString(dr["ta207_denominacion"]);
                    oTareaPreventa.ta207_fechafinprevista = Convert.ToDateTime(dr["ta207_fechafinprevista"]);
                    if (!Convert.IsDBNull(dr["ta207_fechafinreal"]))
                        oTareaPreventa.ta207_fechafinreal = Convert.ToDateTime(dr["ta207_fechafinreal"]);
                    if (!Convert.IsDBNull(dr["ta207_fechacreacion"]))
                        oTareaPreventa.ta207_fechacreacion = Convert.ToDateTime(dr["ta207_fechacreacion"]);
                    oTareaPreventa.ta207_estado = Convert.ToString(dr["ta207_estado"]);
                    if (!Convert.IsDBNull(dr["ta208_negrita"]))
                        oTareaPreventa.ta208_negrita = Convert.ToBoolean(dr["ta208_negrita"]);
                    oTareaPreventa.accesoadetalle = Convert.ToBoolean(dr["accesoadetalle"]);
                    lst.Add(oTareaPreventa);
                    
                }

                if (dr.NextResult()) {
                    while (dr.Read()) 
                    {
                        oTareaPreventa = new Models.TareaPreventaCatalogoParticipante();
                        oTareaPreventa.ta207_idtareapreventa_participante = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                        oTareaPreventa.participantes = Convert.ToString(dr["participante"]);
                        lst2.Add(oTareaPreventa);

                        
                    }                    
                }
                htListas.Add("listaDatos", lst);
                htListas.Add("listaParticipantes", lst2);

                return htListas;

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


        public Models.TareaPreventaDetalleParticipante DetalleTarea(int ta207_idtareapreventa, int t001_idficepi_conectado)
        {
            Models.TareaPreventaDetalleParticipante oTareaPreventa = new TareaPreventaDetalleParticipante(); ;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {					
                    Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa),
                    Param(enumDBFields.t001_idficepi_conectado, t001_idficepi_conectado)
				};

                dr = cDblib.DataReader("SIC_DETALLETAREA_S", dbparams);

                if (dr.Read())
                {
                    oTareaPreventa = new Models.TareaPreventaDetalleParticipante();
                    oTareaPreventa.ta207_idtareapreventa = Convert.ToInt32(dr["ta207_idtareapreventa"]);
                    if (!Convert.IsDBNull(dr["ta219_idtipotareapreventa"]))
                        oTareaPreventa.ta219_idtipotareapreventa = Convert.ToInt32(dr["ta219_idtipotareapreventa"]);

                    oTareaPreventa.ta219_denominacion = Convert.ToString(dr["ta219_denominacion"]);
                    oTareaPreventa.ta204_idaccionpreventa = Convert.ToInt32(dr["ta204_idaccionpreventa"]);                    
                    oTareaPreventa.ta207_descripcion = Convert.ToString(dr["ta207_descripcion"]);
                    oTareaPreventa.ta207_denominacion = Convert.ToString(dr["ta207_denominacion"]);
                    oTareaPreventa.ta207_estado = Convert.ToString(dr["ta207_estado"]);
                    oTareaPreventa.ta207_fechafinprevista = Convert.ToDateTime(dr["ta207_fechafinprevista"]);
                    if (!Convert.IsDBNull(dr["ta207_fechafinreal"]))
                        oTareaPreventa.ta207_fechafinreal = Convert.ToDateTime(dr["ta207_fechafinreal"]);
                    
                    oTareaPreventa.ta207_fechacreacion = Convert.ToDateTime(dr["ta207_fechacreacion"]);
                    oTareaPreventa.ta205_denominacion = Convert.ToString(dr["ta205_denominacion"]);
                    oTareaPreventa.ta207_comentarios = Convert.ToString(dr["ta207_comentarios"]);
                    oTareaPreventa.ta207_observaciones = Convert.ToString(dr["ta207_observaciones"]);
                    if (!Convert.IsDBNull(dr["t001_idficepi_lider"]))
                        oTareaPreventa.t001_idficepi_lider = Convert.ToInt32(dr["t001_idficepi_lider"]);
                    if (!Convert.IsDBNull(dr["lider"]))                        
                        oTareaPreventa.lider = Convert.ToString(dr["lider"]);
                    oTareaPreventa.ta201_idsubareapreventa = Convert.ToInt32(dr["ta201_idsubareapreventa"]);

                    if (!Convert.IsDBNull(dr["ta207_motivoanulacion"]))                        
                        oTareaPreventa.ta207_motivoanulacion = Convert.ToString(dr["ta207_motivoanulacion"]);
                }

                return oTareaPreventa;

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

        public string EstadoTarea(int ta207_idtareapreventa)
        {
            IDataReader dr = null;
            string ta207_estado = "X";

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
                    Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa)
				};

                dr = cDblib.DataReader("SIC_ESTADOTAREA_S", dbparams);

                if (dr.Read())
                {
                    ta207_estado = Convert.ToString(dr["ta207_estado"]);
                }

                return ta207_estado;

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


        public Models.TareaPreventaDetalleParticipante estadoparticipacion(int t001_idficepi_participante, int ta207_idtareapreventa)
        {
            Models.TareaPreventaDetalleParticipante oTareaPreventa = new TareaPreventaDetalleParticipante(); ;
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[2] {					
                    Param(enumDBFields.t001_idficepi_participante, t001_idficepi_participante),
                    Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa),
				};

                dr = cDblib.DataReader("SIC_ESTADOPARTICIPACION_S", dbparams);

                if (dr.Read())
                {
                    oTareaPreventa = new Models.TareaPreventaDetalleParticipante();
                    oTareaPreventa.ta214_estado = dr["ta214_estado"].ToString();
                   
                }

                return oTareaPreventa;

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


        


       public List<Models.ParticipanteTareaPreventa> ObtenerParticipantes(int ta207_idtareapreventa)
        {
            Models.ParticipanteTareaPreventa oParticipanteTareaPreventa = null;
            List<Models.ParticipanteTareaPreventa> lst = new List<Models.ParticipanteTareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[1] {					
                    Param(enumDBFields.ta207_idtareapreventa, ta207_idtareapreventa)
				};

                dr = cDblib.DataReader("SIC_TAREAPARTICIPANTES_C", dbparams);
                while (dr.Read())
                {
                    oParticipanteTareaPreventa = new Models.ParticipanteTareaPreventa();
                    oParticipanteTareaPreventa.ta214_estado = Convert.ToString(dr["ta214_estado"]);
                    oParticipanteTareaPreventa.t001_idficepi_participante = Convert.ToInt32(dr["t001_idficepi_participante"]);
                    oParticipanteTareaPreventa.participante = Convert.ToString(dr["participante"]);

                    lst.Add(oParticipanteTareaPreventa);
                }

                return lst;

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


        public List<Models.TareaPreventa> lstDenominacionesTarea()
        {
            Models.TareaPreventa oDenominacion = null;
            List<Models.TareaPreventa> lst = new List<Models.TareaPreventa>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[0] {
                    
                };

                dr = cDblib.DataReader("SIC_TIPOTAREAPREVENTA_C", dbparams);
                while (dr.Read())
                {
                    oDenominacion = new Models.TareaPreventa();
                    oDenominacion.ta219_idtipotareapreventa = Convert.ToInt32(dr["ta219_idtipotareapreventa"]);
                    oDenominacion.ta219_denominacion = Convert.ToString(dr["ta219_denominacion"]);
                    
                    lst.Add(oDenominacion);
                }

                return lst;

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
                case enumDBFields.ta207_idtareapreventa:
                    paramName = "@ta207_idtareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta204_idaccionpreventa:
                    paramName = "@ta204_idaccionpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.t001_idficepi_promotor:
                    paramName = "@t001_idficepi_promotor";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;
                case enumDBFields.ta207_descripcion:
                    paramName = "@ta207_descripcion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 750;
                    break;

                case enumDBFields.ta207_denominacion:
                    paramName = "@ta207_denominacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 50;
                    break;

                case enumDBFields.ta207_observaciones:
                    paramName = "@ta207_observaciones ";
                    paramType = SqlDbType.VarChar;
                    paramSize = 750;
                    break;

                case enumDBFields.ta207_fechaprevista:
                    paramName = "@ta207_fechafinprevista";
                    paramType = SqlDbType.Date;
                    paramSize = 10;
                    break;
                case enumDBFields.ta207_estado:
                    paramName = "@ta207_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;
                case enumDBFields.ta207_motivoanulacion:
                    paramName = "@ta207_motivoanulacion";
                    paramType = SqlDbType.VarChar;
                    paramSize = 250;
                    break;
                case enumDBFields.t001_idficepi_participante:
                    paramName = "@t001_idficepi_participante";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.ta214_estado:
                    paramName = "@ta214_estado";
                    paramType = SqlDbType.Char;
                    paramSize = 1;
                    break;

                case enumDBFields.ta199_idunidadpreventa:
                    paramName = "@ta199_idunidadpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.ta200_idareapreventa:
                    paramName = "@ta200_idareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.ta201_idsubareapreventa:
                    paramName = "@ta201_idsubareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.ta205_idtipoaccionpreventa:
                    paramName = "@ta205_idtipoaccionpreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;


                case enumDBFields.t001_idficepi_lider:
                    paramName = "@t001_idficepi_lider";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.guidprovisional:
                    paramName = "@guidprovisional";
                    paramType = SqlDbType.UniqueIdentifier;
                    paramSize = 20;
                    break;

                case enumDBFields.ta207_comentario:
                    paramName = "@ta207_comentario";
                    paramType = SqlDbType.VarChar;
                    paramSize = 750;
                    break;

                case enumDBFields.datatable:
                    paramName = "@TABLAPARTICIPANTES";
                    paramType = SqlDbType.Structured;
                    paramSize = 100;
                    break;
                
                case enumDBFields.t001_idficepi_conectado:
                    paramName = "@t001_idficepi_conectado";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi:
                    paramName = "@t001_idficepi";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.t001_idficepi_ultmodificador:
                    paramName = "@t001_idficepi_ultmodificador";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
                    break;

                case enumDBFields.ta219_idtipotareapreventa:
                    paramName = "@ta219_idtipotareapreventa";
                    paramType = SqlDbType.Int;
                    paramSize = 4;
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
