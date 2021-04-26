using System;
using System.Web;
using System.Collections.Generic;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;
using SUPER.Capa_Negocio;
/// <summary>
/// Summary description for FestivosCals
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class FestivosCals : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("831600f7-7ee8-4f74-b5ba-c1897104bc54");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public FestivosCals()
			: base()
        {
			//OpenDbConn();
        }
		
		public FestivosCals(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        //internal List<Models.FestivosCals> Catalogo(int nIdCal, DateTime dUMC)
        public string Catalogo(int nIdCal, DateTime dUMC)
        {
            OpenDbConn();

            DAL.FestivosCals cFestivosCals = new DAL.FestivosCals(cDblib);
            List<Models.FestivosCals> oLista = cFestivosCals.Catalogo(nIdCal, dUMC);
            
            string sRes = "";
            int i = 0;

            foreach (Models.FestivosCals oFila in oLista)
            {
                sRes += "aFestivos[" + i + "] = '" + oFila.t067_dia.ToShortDateString() + "';\n";
                i++;
            }
            return sRes;
        }

        //internal List<Models.FestivosCals> Catalogo(int nIdCal, DateTime dUMC)
        public List<Models.FestivosCals> CatalogoFestivos(int nIdCal, DateTime dUMC)
        {
            OpenDbConn();

            DAL.FestivosCals cFestivosCals = new DAL.FestivosCals(cDblib);
            List<Models.FestivosCals> oLista = cFestivosCals.Catalogo(nIdCal, dUMC);

            
            return oLista;
        }

        public List<Models.FestivosCals> CatalogoFestivosRango(int nIdCal, DateTime fechaIni, DateTime fechaFin)
        {
            OpenDbConn();

            DAL.FestivosCals cFestivosCals = new DAL.FestivosCals(cDblib);
            List<Models.FestivosCals> oLista = cFestivosCals.CatalogoRango(nIdCal, fechaIni, fechaFin);


            return oLista;
        }


        //internal List<Models.FestivosCals> Catalogo(int nIdCal, DateTime dUMC)
        public List<String> CatalogoFestivosString(int nIdCal, DateTime dUMC)
        {
            OpenDbConn();

            DAL.FestivosCals cFestivosCals = new DAL.FestivosCals(cDblib);
            List<String> lFestivosCals = new List<String>();
            List<Models.FestivosCals> oLista = cFestivosCals.Catalogo(nIdCal, dUMC);
            
            foreach (Models.FestivosCals oFila in oLista)
            {
                lFestivosCals.Add(oFila.t067_dia.ToShortDateString());
            }
            return lFestivosCals;
        }

        //Comento la condición Session["reconectar_iap"].ToString() == "" porque sino, cuando un usuario despues de imputar en IAP en nombre
        //de otro, quiere entrar de nuevo con su usuario no cargaba correctamente Session["perfil_iap"]
        public void obtenerOpcionReconexion()
        {
            
            if (Utilidades.EsAdminProduccion()
                //|| HttpContext.Current.User.IsInRole("S")
                )//SECRETARIA    --  PENDIENTE DE DETERMINAR QUÉ HARÁN LAS SECRETARIAS
            {
                HttpContext.Current.Session["reconectar_iap"] = "1";
                HttpContext.Current.Session["perfil_iap"] = "A";
            }
            else if (HttpContext.Current.User.IsInRole("RG") && HttpContext.Current.User.IsInRole("SN"))//Session["reconectar_iap"].ToString() == "" && 
            {
                HttpContext.Current.Session["reconectar_iap"] = "1";
                HttpContext.Current.Session["perfil_iap"] = "GS";
            }
            else if (HttpContext.Current.User.IsInRole("RG"))//Session["reconectar_iap"].ToString() == "" && 
            {
                HttpContext.Current.Session["reconectar_iap"] = "1";
                HttpContext.Current.Session["perfil_iap"] = "RG";
            }
            else if (HttpContext.Current.User.IsInRole("SN"))
            {
                HttpContext.Current.Session["reconectar_iap"] = "1";
                HttpContext.Current.Session["perfil_iap"] = "SN";
            }
            else
            {
                if (HttpContext.Current.Session["IDRED"].ToString() != "")
                {
                    //Contemplar que la persona pueda tener dos usuario con los que imputar
                    //Ej: externo que pasa a interno
                    if (Recurso.ObtenerCountUsuarios(HttpContext.Current.Session["IDRED"].ToString()) > 1)
                    {
                        HttpContext.Current.Session["reconectar_iap"] = "1";
                        HttpContext.Current.Session["perfil_iap"] = "P";  //Personal
                    }
                }
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
        ~FestivosCals()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
