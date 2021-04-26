using System;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Fechas.
	/// </summary>
	public class Fechas
	{
		public Fechas()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}

		public static System.DateTime crearDateTime(string strFecha)
		{
			System.DateTime dt;
			if (strFecha != "")
			{
				string[] aFec = Regex.Split(strFecha, "/");
				dt = new DateTime(int.Parse(aFec[2]),int.Parse(aFec[1]),int.Parse(aFec[0]));
			}
			else
			{
				dt = new DateTime(1900,1,1);
			}
			return dt;
		}

		public static System.DateTime crearDateTime(string strFecha, string strHora)
		{
			System.DateTime dt;
			if (strFecha != "")
			{
				string[] aFec = Regex.Split(strFecha, "/");
				string[] aHor = Regex.Split(strHora, ":");
				dt = new DateTime(int.Parse(aFec[2]),int.Parse(aFec[1]),int.Parse(aFec[0]),int.Parse(aHor[0]),int.Parse(aHor[1]),0);
			}
			else
			{
				dt = new DateTime(1900,1,1);
			}
			return dt;
		}

		public static string calendarioOutlook(DateTime dFecha, bool bUnico)
		{
			string strFecha = "";
			//DateTime dAux = dFecha.AddHours(-1);
			DateTime dAux = dFecha.ToUniversalTime();  //Para evitar problemas con los cambios de horario.

			strFecha = dAux.Year.ToString();
			if (dAux.Month < 10) strFecha += "0";
			strFecha += dAux.Month.ToString();
			if (dAux.Day < 10) strFecha += "0";
			strFecha += dAux.Day.ToString();
			strFecha += "T";
			if (dAux.Hour < 10) strFecha += "0";
			strFecha += dAux.Hour.ToString();
			if (dAux.Minute < 10) strFecha += "0";
			strFecha += dAux.Minute.ToString();
			if (dAux.Second < 10) strFecha += "0";
			strFecha += dAux.Second.ToString();
			strFecha += "Z";

			if (bUnico)
			{
				strFecha += dAux.Millisecond.ToString();
			}

			return strFecha;
		}

		public static int DateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate) 
		{ 
			int diff=0; 
			System.TimeSpan TS = new System.TimeSpan(endDate.Ticks-startDate.Ticks); 

			switch (howtocompare.ToLower()) 
			{ 
				case "year": 
					diff = Convert.ToInt32(TS.TotalDays/365); 
					break; 
				case "month": 
					diff = Convert.ToInt32((TS.TotalDays/365)*12); 
					break;
                case "week":
                    diff = Convert.ToInt32((TS.TotalDays / 365) * 52);
                    break;
                case "day":
					diff = Convert.ToInt32(TS.TotalDays); 
					break; 
				case "hour": 
					diff = Convert.ToInt32(TS.TotalHours); 
					break; 
				case "minute": 
					diff = Convert.ToInt32(TS.TotalMinutes); 
					break; 
				case "second": 
					diff = Convert.ToInt32(TS.TotalSeconds); 
					break;
                case "mm":
                    diff = Convert.ToInt32(TS.TotalMilliseconds);
                    break;
            }

			return diff;
		}  

        public static DateTime getSigDiaUltMesCerrado(int iAnoMesCerrado)
        {
            return Fechas.AnnomesAFecha(iAnoMesCerrado).AddMonths(1);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// AnnomesAFecha: zfsf  fasdfa sdfasdf asdf asdf
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static DateTime AnnomesAFecha(int nAnnoMes)
        {
            if (Fechas.ValidarAnnomes(nAnnoMes))
                return DateTime.Parse("01/" + nAnnoMes.ToString().Substring(4, 2) + "/" + nAnnoMes.ToString().Substring(0, 4));
            else
                return DateTime.Parse("01/01/1900");
        }
        public static int FechaAAnnomes(DateTime dFecha)
        {
            return (dFecha.Year * 100 + dFecha.Month);
        }
        public static bool ValidarAnnomes(int nAnnoMes)
        {
            if (nAnnoMes.ToString().Length != 6)
                throw (new Exception("La longitud del AnnoMes no es de seis dígitos"));
            if (nAnnoMes % 100 < 1 || nAnnoMes % 100 > 12)
                throw (new Exception("El mes no es coherente. Menor de 1 o mayor de 12."));
            if (nAnnoMes / 100 < 1900 || nAnnoMes / 100 > 2078)
                throw (new Exception("El año no es coherente. Menor de 1900 o mayor de 2078."));

            return true;
        }
        public static int AddAnnomes(int nAnnoMes, int nMeses)
        {
            return FechaAAnnomes(AnnomesAFecha(nAnnoMes).AddMonths(nMeses));
        }
        public static DateTime primerDiaMes(DateTime oFecha){
            return oFecha.AddDays(oFecha.Day * -1 + 1);
        }
        public static string AnnomesAFechaDescCorta(int nAnnoMes)
        {
            string[] aMeses = new string[12] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };

            if (Fechas.ValidarAnnomes(nAnnoMes))
                return aMeses[AnnomesAFecha(nAnnoMes).Month - 1] + " " + AnnomesAFecha(nAnnoMes).Year.ToString();
            else
                return "";
        }
        public static string AnnomesAMesDescLarga(int nAnnoMes)
        {
            string[] aMeses = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            if (Fechas.ValidarAnnomes(nAnnoMes))
                return aMeses[AnnomesAFecha(nAnnoMes).Month - 1];
            else
                return "";
        }
        public static string AnnomesAFechaDescLarga(int nAnnoMes)
        {
            string[] aMeses = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            if (Fechas.ValidarAnnomes(nAnnoMes))
                return aMeses[AnnomesAFecha(nAnnoMes).Month - 1] + " " + AnnomesAFecha(nAnnoMes).Year.ToString();
            else
                return "";
        }
        public static string diaSemana(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "Lunes";
                case DayOfWeek.Tuesday:
                    return "Martes";
                case DayOfWeek.Wednesday:
                    return "Miércoles";
                case DayOfWeek.Thursday:
                    return "Jueves";
                case DayOfWeek.Friday:
                    return "Viernes";
                case DayOfWeek.Saturday:
                    return "Sábado";
                case DayOfWeek.Sunday:
                    return "Domingo";
                default:
                    return "";
            }
        }

        public static string FechaACadenaLarga(DateTime dFecha)
        {
            return dFecha.ToShortDateString() + " " + ((dFecha.Hour.ToString().Length == 1) ? "0" : "") + dFecha.Hour.ToString() + ":" + ((dFecha.Minute.ToString().Length == 1) ? "0" : "") + dFecha.Minute.ToString();
        }
        public static string HoraACadenaLarga(DateTime dFecha)
        {
            return ((dFecha.Hour.ToString().Length == 1) ? "0" : "") + dFecha.Hour.ToString() + ":" + ((dFecha.Minute.ToString().Length == 1) ? "0" : "") + dFecha.Minute.ToString();
        }

        public static string SinHora(string sFecha)
        {
            string sRes = "", sAux = sFecha.Trim();

            if (sAux != "")
            {
                if (sAux.Length == 10)
                    sRes = sAux;
                else
                {
                    if (sAux.Length > 10)
                        sRes = sAux.Substring(0, 10);
                }
            }
            return sRes;
        }

    }
}
