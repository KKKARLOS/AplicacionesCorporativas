using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for TareaPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class TareaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("74c4b36e-d46b-4cc1-913b-a4554fc7a8fb");
        private bool disposed = false;

        #endregion

        #region Constructor

        public TareaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public TareaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        internal List<Models.TareaPreventa> Catalogo(Models.TareaPreventa oTareaPreventaFilter)
        {
            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.Catalogo(oTareaPreventaFilter);

        }

        /// <summary>
        /// Grabar la tarea. 
        /// </summary>
        /// <param name="oTareaPreventa"></param>
        /// <returns></returns>
        public int grabarTarea(Models.TareaPreventa oTareaPreventa, List<Models.ParticipanteTareaPreventa> listaParticipantes, string modoPantalla, string estadoParticipacion, Models.PerfilesEdicion oPerfilesEdicion)
        {
            Guid methodOwnerID = new Guid("5691cbfc-453f-465f-ad89-153b1e278a81");
            int t001_idficepi_ultmodificador  = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {                                                
                
                DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);

                DataTable dtParticipantes = new DataTable();
                dtParticipantes.Columns.Add(new DataColumn("col_1", typeof(int)));
                dtParticipantes.Columns.Add(new DataColumn("col_2", typeof(char)));
                

                //Recorremos la lista
                foreach (Models.ParticipanteTareaPreventa oParticipante in listaParticipantes)
                {
                    DataRow row = dtParticipantes.NewRow();
                    row["col_1"] = oParticipante.t001_idficepi_participante;
                    row["col_2"] = oParticipante.ta214_estado;

                    dtParticipantes.Rows.Add(row);
                }

                int idTareaPreventa = 0;
                

                //Casuísticas de perfiles edición
                if (modoPantalla == "A")//modo alta
                {
                    if (oPerfilesEdicion.soyFiguraSubarea || oPerfilesEdicion.soyAdministrador || oPerfilesEdicion.soyLider)
                    {
                        oTareaPreventa.t001_idficepi_promotor = t001_idficepi_ultmodificador;
                        idTareaPreventa = cTareaPreventa.Insert(oTareaPreventa, dtParticipantes);
                    }
                }
                else //Modo edición
                {
                    if (estadoParticipacion == "A")
                    {
                        //Update del participante (modelo reducido)                       
                        idTareaPreventa = cTareaPreventa.UpdateParticipante(oTareaPreventa);
                    }
                    if (oPerfilesEdicion.soyFiguraSubarea || oPerfilesEdicion.soyAdministrador || oPerfilesEdicion.soyLider)
                    {
                        //Hacer update del modelo completo
                        idTareaPreventa = cTareaPreventa.Update(oTareaPreventa, dtParticipantes, t001_idficepi_ultmodificador);
                    }
                }
                
                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTareaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }


        public int Insert(Models.TareaPreventa oTareaPreventa, List<Models.ParticipanteTareaPreventa> listaParticipantes)
        {
            Guid methodOwnerID = new Guid("3280e0c3-8064-42a1-a55b-8f73dfac701c");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);

                //int idTareaPreventa = cTareaPreventa.Insert(oTareaPreventa, listaParticipantes);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                //return idTareaPreventa;
                return 0;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        //public int UpdateEstadoTarea(int ta207_idtarea, string ta207_estado)
        //{
        //    Guid methodOwnerID = new Guid("d44b8d8a-5459-46b0-a730-f72e23bbc44a");

        //    OpenDbConn();

        //    if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

        //    try
        //    {
        //        DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);

        //        int result = cTareaPreventa.UpdateEstadoTarea(ta207_idtarea, ta207_estado);

        //        //Finalizar transacción 
        //        if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        //rollback
        //        if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

        //        throw ex;
        //    }
        //}

        internal int Delete(Int32 ta207_idtareapreventa)
        {
            Guid methodOwnerID = new Guid("c46355de-bcdc-4024-a9b0-1cc71dc62bee");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);

                int result = cTareaPreventa.Delete(ta207_idtareapreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal Models.TareaPreventa Select(Int32 ta207_idtareapreventa)
        {
            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.Select(ta207_idtareapreventa);
        }


        public List<Models.TareaPreventaCatalogoParticipante> misTareasComoParticipante()
        {

            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);

            Hashtable aListas = cTareaPreventa.misTareasComoParticipante((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);
            List<Models.TareaPreventaCatalogoParticipante> lstDatos = (List<Models.TareaPreventaCatalogoParticipante>)aListas["listaDatos"];
            List<Models.TareaPreventaCatalogoParticipante> lstParticipantes = (List<Models.TareaPreventaCatalogoParticipante>)aListas["listaParticipantes"];

            foreach (Models.TareaPreventaCatalogoParticipante item in lstDatos)
            {
                List<string> lstParticipantesTarea = (from p in lstParticipantes
                                                      where p.ta207_idtareapreventa_participante == item.ta207_idtareapreventa
                                                      select p.participantes).ToList<String>();

                item.participantes = string.Join("|", lstParticipantesTarea.ToArray());

            }

            return lstDatos;
        }

        public List<Models.TareaPreventaCatalogoParticipante> CatalogoParticipante()
        {

            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.CatalogoParticipante(null, null, null, null, null, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], null, "A");
        }


        public List<Models.TareaPreventaCatalogoParticipante> CatalogoPorAccion(int ta204_idaccionpreventa)
        {

            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);

            Hashtable aListas = cTareaPreventa.CatalogoPorAccion(ta204_idaccionpreventa, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

            List<Models.TareaPreventaCatalogoParticipante> lstDatos = (List<Models.TareaPreventaCatalogoParticipante>)aListas["listaDatos"];
            List<Models.TareaPreventaCatalogoParticipante> lstParticipantes = (List<Models.TareaPreventaCatalogoParticipante>)aListas["listaParticipantes"];

            foreach (Models.TareaPreventaCatalogoParticipante item in lstDatos)
            {
                List<string> lstParticipantesTarea = (from p in lstParticipantes
                                                      where p.ta207_idtareapreventa_participante == item.ta207_idtareapreventa
                                                      select p.participantes).ToList<String>();
                
                item.participantes = string.Join("|",lstParticipantesTarea.ToArray());
                
                
                //for (int i = 0; i < lstParticipantes.Count; i++)
                //{
                //    if (item.ta207_idtareapreventa == lstParticipantes[i].ta207_idtareapreventa_participante)
                //    {                        
                //        item.participantes += lstParticipantes[i].participantes + "|";
                //    }
                //}
                          
            }

            return lstDatos;
        }

        public Models.TareaPreventaDetalleParticipante DetalleTarea(int ta207_idtareapreventa, int t001_idficepi_conectado)
        {

            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.DetalleTarea(ta207_idtareapreventa, t001_idficepi_conectado);

        }


        public List<Models.TareaPreventa> lstDenominacionesTarea()
        {

            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.lstDenominacionesTarea();

        }

        /// <summary>
        /// Devuelve el estado de la tarea teniendo en cuenta el estado de la acción y solicitud
        /// </summary>
        public string EstadoTarea(int ta207_idtareapreventa)
        {
            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.EstadoTarea(ta207_idtareapreventa);
        
        }

        public Models.TareaPreventaDetalleParticipante estadoparticipacion(int t001_idficepi_participante, int ta207_idtareapreventa)
        {

            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.estadoparticipacion(t001_idficepi_participante, ta207_idtareapreventa);

        }


        public bool EsParticipante(int t001_idficepi_participante, int ta207_idtareapreventa) {

            Models.TareaPreventaDetalleParticipante o = this.estadoparticipacion(t001_idficepi_participante, ta207_idtareapreventa);

            if (o.ta214_estado == null || o.ta214_estado.Trim().Length == 0) return false;

            return true;
        }

        public List<Models.ParticipanteTareaPreventa> ObtenerParticipantes(int ta207_idtareapreventa)
        {

            OpenDbConn();

            DAL.TareaPreventa cTareaPreventa = new DAL.TareaPreventa(cDblib);
            return cTareaPreventa.ObtenerParticipantes(ta207_idtareapreventa);

        }

        [Serializable]
        class Figuras
        {
            public bool soyLider { get; set; }
            public bool soyAdministrador { get; set; }
            public bool soyFiguraSubarea { get; set; }
            public bool soyFiguraArea { get; set; }
            public string estadoParticipacion { get; set; }
            public string modoPantalla { get; set; }
        }

        //Obtiene el literal del estado pasado por parámetro
        public static string GetLiteralEstadoTarea(string estado)
        {
            switch (estado)
            {
                case "A": return "Abierta";
                case "F": return "Finalizada";
                case "FS": return "Anulada";
                case "FA": return "Anulada";
                case "X": return "Anulada";
                case "XS": return "Anulada";
                case "XA": return "Anulada";
                default: return "";
            }
        }
        

        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);
        }
        private void AttachDbConn(sqldblib.SqlServerSP extcDblib)
        {
            cDblib = extcDblib;
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) if (cDblib != null && cDblib.OwnerID.Equals(classOwnerID)) cDblib.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~TareaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
