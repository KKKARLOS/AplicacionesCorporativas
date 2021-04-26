using System;
using System.Collections.Generic;
using System.Data;

using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoTecnicoIAP
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoTecnicoIAP : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("bc601644-b43a-4633-ba76-ce48fd87b71b");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoTecnicoIAP()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoTecnicoIAP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.CONSUMOIAP_PROYECTOS> Catalogo(int t314_idusuario, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

            DAL.CONSUMOIAP_PROYECTOS cCONSUMOIAP_PROYECTOS = new DAL.CONSUMOIAP_PROYECTOS(cDblib);
            return  cCONSUMOIAP_PROYECTOS.Catalogo(t314_idusuario, dDesde, dHasta);
        }
        public List<Models.ConsumoTecnicoIAP> Catalogo(int t314_idusuario, Nullable<int> t305_idproyectosubnodo, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

            DAL.ConsumoTecnicoIAP cConsumoTecnicoIAP = new DAL.ConsumoTecnicoIAP(cDblib);
            return cConsumoTecnicoIAP.Catalogo(t314_idusuario, t305_idproyectosubnodo, dDesde, dHasta);

        }
        public DataSet ExportarExcel(int t314_idusuario, Nullable<int> t305_idproyectosubnodo, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

            DAL.ConsumoTecnicoIAP cConsumoTecnicoIAP = new DAL.ConsumoTecnicoIAP(cDblib);
            List<Models.ConsumoTecnicoIAP> lConsumoTecnicoIAP = cConsumoTecnicoIAP.Catalogo(t314_idusuario, t305_idproyectosubnodo, dDesde, dHasta);
            DataSet ds = new DataSet();
            DataTable dtbody = lConsumoTecnicoIAP.CopyGenericToDataTable<Models.ConsumoTecnicoIAP>();

            ds.Tables.Add(dtbody);
            return ds;
        }
        public DataSet ExportarExcel2(int t314_idusuario, Nullable<int> t305_idproyectosubnodo, DateTime dDesde, DateTime dHasta)
        {
            //OpenDbConn();

            //DAL.ConsumoTecnicoIAP cConsumoTecnicoIAP = new DAL.ConsumoTecnicoIAP(cDblib);
            //List<Models.ConsumoTecnicoIAP> lConsumoTecnicoIAP = cConsumoTecnicoIAP.ExportarExcel(t314_idusuario, t305_idproyectosubnodo, dDesde, dHasta);

            DataSet ds = new DataSet();
            //Datatable de cabecera
            DataTable dtHeader = new DataTable("HEAD-Excel");
            DataColumn dtc = new DataColumn("clave");
            dtHeader.Columns.Add(dtc);
            dtc = new DataColumn("valor");
            dtHeader.Columns.Add(dtc);

            DataTable dtbody = new DataTable("BODY-Excel");
            dtbody.Clear();

            dtbody.Columns.Add("Tipo", typeof(String));
            dtbody.Columns.Add("Estructura técnica / F.consumo / Comentarios", typeof(String));
            dtbody.Columns.Add("Horas", typeof(Double));
            dtbody.Columns.Add("Jornadas", typeof(Double));

            DataRow oRow = dtbody.NewRow();
            oRow[0] = "PE";
            oRow[1] = "25769 - SUPER (Mantenimiento correctivo)";            
            oRow[2] = "74,5";
            oRow[3] = "9,245";
            dtbody.Rows.Add(oRow);

            oRow = dtbody.NewRow();
            oRow[0] = "PT";
            oRow[1] = "IB-SUPER (Mantenimiento correctivo)";
            oRow[2] = "74,5";
            oRow[3] = "9,245";
            dtbody.Rows.Add(oRow);

            oRow = dtbody.NewRow();
            oRow[0] = "T";
            oRow[1] = "349974 - Correcciones baja entidad (comentario obligatorio)";
            oRow[2] = "74,5";
            oRow[3] = "9,245";
            dtbody.Rows.Add(oRow);

            oRow = dtbody.NewRow();
            oRow[0] = " ";
            oRow[1] = "07/01/2016 Tratamiento a nodos no representativos con subnodos con empresa. Situación de cambio el no a representativo";
            oRow[2] = "8,50";
            oRow[3] = "1";
            dtbody.Rows.Add(oRow);

            ds.Tables.Add(dtHeader);
            ds.Tables.Add(dtbody);
            return ds;
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
        ~ConsumoTecnicoIAP()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
