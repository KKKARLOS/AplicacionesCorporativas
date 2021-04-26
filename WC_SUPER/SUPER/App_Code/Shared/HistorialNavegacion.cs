using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace IB.SUPER.Shared
{

    public class HistorialNavegacion
    {
        public HistorialNavegacion()
        {
        }

        /// <summary>
        /// Inserta una nueva url en el historial de navegación
        /// </summary>
        /// <param name="strUrl">url a insertal en el historial</param>
        /// <param name="secuencial">navegación secuencial. No permite que exista dos veces la misma url en el buffer.</param>
        public static void Insertar(string strUrl, bool secuencial)
        {
            Stack stkHistorial = (Stack)HttpContext.Current.Session["Historial"];

            if (stkHistorial == null)
            {
                stkHistorial = new Stack();
                stkHistorial.Push(strUrl);

                HttpContext.Current.Session["Historial"] = stkHistorial;
            }

            if (stkHistorial.Count == 0)
            {
                stkHistorial.Push(strUrl);
            }
            else
            {
                if (secuencial)
                {
                    if (!stkHistorial.Contains(strUrl))
                    {
                        stkHistorial.Push(strUrl);
                    }
                }
                else
                {
                    if (strUrl != stkHistorial.Peek().ToString())
                    {
                        stkHistorial.Push(strUrl);
                    }
                }
            }


        }

        /// <summary>
        /// Devuelve la url anterior a la pantalla actual (navegación atrás)
        /// </summary>
        /// <param name="urlActual">Indica la url actual. Si es "", se devuelve la que toca.
        /// Si tiene valor (se usa con nevagación secuencial), se busca en el buffer para devolver la anterior y se eliminan las invalidas (por si el usuario ha navegado con las flechas atras-alante del navegador).</param>
        /// <returns></returns>
        public static string Leer(string urlActual)
        {

            //Debug.WriteLine("urlActual=" + urlActual);

            Stack stkHistorial = (Stack)HttpContext.Current.Session["Historial"];

            if (stkHistorial == null || stkHistorial.Count == 0)
            {
                return "";
            }

            if (urlActual.Trim().Length == 0)
            {
                //Se elimina la última opción, ya que esta es la página en la que
                //se encuentra el usuario, que ha registrado en el Page_Load.
                if (stkHistorial.Count > 1)
                {
                    stkHistorial.Pop();
                }
            }
            else {
                while (stkHistorial.Count > 1)
                {
                    //Debug.WriteLine("stkHistorial.Peek()=" + stkHistorial.Peek().ToString());
                    if (urlActual.StartsWith(stkHistorial.Peek().ToString()))
                    {
                        //Debug.WriteLine("StartsWith=true");
                        stkHistorial.Pop();
                        break;
                    }
                    else
                    {
                        //Debug.WriteLine("StartsWith=false");
                        stkHistorial.Pop();
                    }
                }
            }

            //Debug.WriteLine("return " + stkHistorial.Peek().ToString());
            return stkHistorial.Pop().ToString();

        }

        /// <summary>
        /// Reemplaza la última url que haya entrado en el buffer por la que se pasa como parámetro
        /// </summary>
        /// <param name="strUrl">url que ocupará la primera posición</param>
        public static void Reemplazar(string strUrl)
        {
            Stack stkHistorial = (Stack)HttpContext.Current.Session["Historial"];

            if (stkHistorial == null) return ;

            if (stkHistorial.Count > 0) stkHistorial.Pop();

            stkHistorial.Push(strUrl);
                

        }

        /// <summary>
        /// Vacía el historial de navegación
        /// </summary>
        public static void Resetear()
        {

            HttpContext.Current.Session["Historial"] = new Stack();
        }



    }
}