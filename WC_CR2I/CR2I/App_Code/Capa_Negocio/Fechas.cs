using System;
using System.Text.RegularExpressions;
using RJS.Web.WebControl;

namespace CR2I.Capa_Negocio
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
			}

			return diff;
		}  


		public static RJS.Web.WebControl.PopCalendar InsertarCalendario(string strTxtDestino)
		{
			RJS.Web.WebControl.PopCalendar PopCal = new PopCalendar();

			PopCal.ID		= "ID"+ strTxtDestino;
            //PopCal.Control = "ctl00$CPHC$" + strTxtDestino;
            PopCal.Control = strTxtDestino;
            PopCal.Separator = "/";
			PopCal.StartAt	= PopCalendar.StartAtEnum.Monday;
			PopCal.ShowToday = false;
			PopCal.Language = PopCalendar.LanguageEnum.Spanish;
			//PopCal.

			return PopCal;
		}
	}
}
