using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using IB.SUPER.Shared;
using BLL = IB.SUPER.SIC.BLL;
using Models = IB.SUPER.SIC.Models;
using System.Configuration;

namespace IB.SUPER.Services.SIC
{

    [WebService(Namespace = "http://SUPER.ibermatica.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class Profesionales : System.Web.Services.WebService
    {

        public Profesionales()
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.ProfesionalSimple> LideresSubarea(int ta201_idsubareapreventa)
        {

            BLL.AyudaProfesionales cAP = null;
            try
            {
                cAP = new BLL.AyudaProfesionales();

                List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.LideresSubarea(ta201_idsubareapreventa);

                cAP.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAP.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de líderes", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de líderes"));
            }

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.ProfesionalSimple> GeneralFicepi(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {


            BLL.AyudaProfesionales cAP = null;
            try
            {
                cAP = new BLL.AyudaProfesionales();

                List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.FicepiGeneral(t001_nombre, t001_apellido1, t001_apellido2);

                cAP.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAP.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de profesionales", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de profesionales"));
            }


        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.ProfesionalSimple> ComercialesPreventa (string t001_nombre, string t001_apellido1, string t001_apellido2)
        {


            BLL.AyudaProfesionales cAP = null;
            try
            {
                cAP = new BLL.AyudaProfesionales();

                List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.Comerciales(t001_nombre, t001_apellido1, t001_apellido2);

                cAP.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAP.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de profesionales", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de profesionales"));
            }


        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.ProfesionalSimple> PromotoresAccionPreventa(string t001_nombre, string t001_apellido1, string t001_apellido2)
        {


            BLL.AyudaProfesionales cAP = null;
            try
            {
                cAP = new BLL.AyudaProfesionales();

                List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.PromotoresAccion(t001_nombre, t001_apellido1, t001_apellido2);

                cAP.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAP.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de profesionales", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de profesionales"));
            }


        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.ProfesionalSimple> LideresPreventa(string proc, string t001_nombre, string t001_apellido1, string t001_apellido2)
        {

            BLL.AyudaProfesionales cAP = null;
            try
            {
                cAP = new BLL.AyudaProfesionales();

                List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.Lideres(proc, t001_nombre, t001_apellido1, t001_apellido2);

                cAP.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAP.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de líderes", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de líderes"));
            }

        } 

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.ProfesionalSimple> LideresPreventaAmbitoVision(string t001_nombre, string t001_apellido1, string t001_apellido2, bool admin)
        {

            BLL.AyudaProfesionales cAP = null;
            try
            {
                cAP = new BLL.AyudaProfesionales();

                List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.LideresAmbitoVision(t001_nombre, t001_apellido1, t001_apellido2, admin);

                cAP.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAP.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de líderes", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de líderes"));
            }

        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<IB.SUPER.SIC.Models.ProfesionalSimple> LideresPreventaConAmbitoVision(string proc, string t001_nombre, string t001_apellido1, string t001_apellido2, bool admin)
        {

            BLL.AyudaProfesionales cAP = null;
            try
            {
                cAP = new BLL.AyudaProfesionales();

                List<IB.SUPER.SIC.Models.ProfesionalSimple> lst = cAP.LideresPreventaConAmbitoVision(proc, t001_nombre, t001_apellido1, t001_apellido2, admin);

                cAP.Dispose();

                return lst;
            }
            catch (Exception ex)
            {
                cAP.Dispose();

                LogError.LogearError("Ocurrió un error obteniendo la lista de líderes", ex);
                throw new Exception(System.Uri.EscapeDataString("Ocurrió un error obteniendo la lista de líderes"));
            }

        }


        public class profesionalEntity
        {

            public string nombre { get; set; }
            public string apellido1 { get; set; }
            public string apellido2 { get; set; }
            public int idficepi { get; set; }

        }
    }
}