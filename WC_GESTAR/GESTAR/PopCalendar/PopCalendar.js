var __PopCalLastControl = ""
var __PopCalTemporal = new String()
var __PopCalPageIsValid = true
var __BlankField = ""

var oCal;
/* Crear Objeto Calendario */
function coc(){
    /* Objeto calendario */
	oCal=PopCalendar.newCalendar();
	oCal.imgDir = strServer + 'PopCalendar/CSS/Classic_Images/';
	oCal.initCalendar();
}

//document.onreadystatechange = function() {
//    if (document.all) {
//        if (document.readyState == "complete") coc();
//        try {
//            if ($I("divPestRetr") != null) {
//                if (document.readyState == "complete") {
//                    oPestRetr = $I("divPestRetr");
//                    oImgPestana = $I("imgPestHorizontalAux");
//                }
//            }
//        } catch (e) { }
//    }
//    else try {coc();} catch (e) {};
//}

var bCargado = false

function Cargado() {
    if (bCargado) return
    bCargado = true
    try { coc(); } catch (e) {}
    if ($I("divPestRetr") != null) 
    {
        oPestRetr = $I("divPestRetr");
        oImgPestana = $I("imgPestHorizontalAux");
    }
}


if (document.addEventListener) { // native event  
    document.addEventListener("DOMContentLoaded", Cargado, false)
} else if (document.attachEvent) {  // IE  
    // IE, the document is inside a frame  
    document.attachEvent("onreadystatechange", function() {
        if (document.readyState === "complete") {
            Cargado()
        }
    })
}  


function __PopCalSetFocus(o)
{
	o.keyboard=true
	o.eventKey=0
	/*
	* Código modificado para saber cuando
	* se cambia el valor del input para
	* poder realizar alguna acción
	*/
	if (document.all) {
	    try { o.fireEvent("onchange"); } catch (e) { };	    
	} else {
	    var changeEvent = document.createEvent("MouseEvent");
	    changeEvent.initEvent("change", false, true);
	    try { o.dispatchEvent(changeEvent); } catch (e) { };	    
	}  	
    
	o.oValue=o.value;
}

/*
Función propia para borrar la fecha
*/
function BorrarFecha(l) 
{
	var objPopCalendar = objPopCalList[l];
	PopCalHideCalendar(l);
	
	objPopCalendar.ctlToPlaceValue.value = "";
	__PopCalSetFocus(objPopCalendar.ctlToPlaceValue);
}

/* mc: mostrar calendario */

function mc_old(oCal) {
    if ( typeof(bLectura)!="undefined" && bLectura==true)return;
    __PopCalShowCalendar(oCal)
}

function mc(e) {
    if (typeof (bLectura) != "undefined" && bLectura == true) return;
    if (!e) e = event;
    var oCal = (e.srcElement != null) ?  e.srcElement : e.target; 
    if (e.srcElement == null && e.target == null) oCal = e; 
    __PopCalShowCalendar(oCal)
}

function vF(oFecha) {//validarFecha

    var msgV = "Fecha no válida.\n\nLos formatos reconocidos son:\n\nddmmyy\nddmmyyyy\nd/m/yy\ndd/m/yy\nd/mm/yy\nd/m/yyyy\ndd/m/yyyy\nd/mm/yyyy";
    var Fecha = oFecha.value;
    if (Fecha == "") {
        var _goma = oFecha.getAttribute("goma")
        if (_goma != null && _goma == 0) {
            if (oFecha.getAttribute("oValue") != "") {//Si tenía valor y siendo obligatorio se lo quitamos
                mmoff("Inf", "Fecha obligatoria.", 180);
                //oFecha.focus(); 
            }
        }
        return;
    }
    else {
        if (Fecha.length < 10) {
            Fecha = formateafecha(oFecha.value);
            if (Fecha.length == 10)
                oFecha.value = Fecha;
        }
    }
    var Ano = new String(Fecha.substring(Fecha.lastIndexOf("/") + 1, Fecha.length))
    var Mes = new String(Fecha.substring(Fecha.indexOf("/") + 1, Fecha.lastIndexOf("/")))
    var Dia = new String(Fecha.substring(0, Fecha.indexOf("/")))
    //En BBDD los campos smalldatetime llegan hasta el 6 de junio de 2079
    if (Ano == "" || isNaN(Ano) || Ano.length < 4 || parseFloat(Ano) < 1900 || parseFloat(Ano) > 2078) {
        mmoff("Inf", msgV, 400);
        oFecha.value = "";
        setTimeout(function () { oFecha.focus(); }, 10);
        //oFecha.focus();
        return;
    }
    if (Mes == "" || isNaN(Mes) || parseFloat(Mes) < 1 || parseFloat(Mes) > 12) {
        mmoff("Inf", msgV, 400);
        oFecha.value = "";
        setTimeout(function () { oFecha.focus(); }, 10);
        //oFecha.focus();    
        return;
    }
    if (Dia == "" || isNaN(Dia) || parseInt(Dia, 10) < 1 || parseInt(Dia, 10) > 31) {
        mmoff("Inf", msgV, 400);
        oFecha.value = "";
        setTimeout(function () { oFecha.focus(); }, 10);
        //oFecha.focus();   
        return;
    }
    if (Mes == 4 || Mes == 6 || Mes == 9 || Mes == 11) {
        if (Dia > 30) {
            mmoff("Inf", msgV, 400);
            oFecha.value = "";
            setTimeout(function () { oFecha.focus(); }, 10);
            //oFecha.focus(); 
            return;
        }
    }
    //Validación especial para Febrero
    if (Mes == 2) {
        if (Dia > 29 || (Dia == 29 && ((Ano % 4 != 0) || ((Ano % 100 == 0) && (Ano % 400 != 0))))) {
            mmoff("Inf", msgV, 400);
            oFecha.value = "";
            setTimeout(function () { oFecha.focus(); }, 10);
            //oFecha.focus(); 
            return;
        }
    }
}

var miCalendario;
function mc1(e) {
    if (!e) e = event;
    var obj = (typeof e.srcElement != 'undefined') ? e.srcElement : e.target;

    if (ie) {
        // botón derecho
        if (e.button == 2) {
            miCalendario = obj;
            setTimeout("mc3()", 300);
        }
    }
    else {
        // botón derecho
        if (e.which == 3 || e.which == 2) {
            miCalendario = obj;
            setTimeout("mc3()", 300);
        }
    }
}
function mc3() {
    mc(miCalendario);
}

function __PopCalShowCalendar(_span)
{ 
	var o = document.getElementById(_span.id)
	if (!o) return

	var _PopCal = eval(o.getAttribute("Calendar"))
	var _format = o.getAttribute("Format")
	
	var _lectura = o.getAttribute("lectura")
	if (_lectura != null && _lectura==1) return //modo lectura
	
	var _goma = o.getAttribute("goma")//por defecto, que muestre.
	if (_goma != null && _goma == 0) {
	    (document.all) ? $I("ImgGomaCal").style.visibility = "hidden" : $I("ImgGomaCal").setAttribute("style","visibility:hidden");
	}
	else {
	    (document.all) ? $I("ImgGomaCal").style.visibility = "visible" : $I("ImgGomaCal").setAttribute("style", "visibility:visible");
	}
	var _from = ""
	var _to = ""

	if (o.oValue == undefined)
	    o.oValue = o.value;
	
	_PopCal.ControlAlignLeft = null
	o.eventKey=0
	o.keyboard = false
	__PopCalLastControl = o.id
	_PopCal.show(o, _format, _from, _to, "__PopCalSelectDate('" + o.id + "')")
}

function __PopCalSelectDate(_o)
{
    var o = document.getElementById(_o)
    if (!o) return;
	if (o.value!=o.oValue)
	{
        __PopCalSetFocus(o)
	}
}

var objPopCalList = []
var lPopCalList = -1
var PopCalendar = new PopCalInstance()

function PopCalInstance()
{
	this.majorVersion = 2
	this.minorVersion = 9.2
	this.lastCalendar = null
	this.newCalendar = new Function("return(getCalendarInstance())")
}

function getCalendarInstance()
{
	var oPC = new PoPCalCreateCalendarInstance()

	if (!oPC.ns4)
	{
		if (oPC.dom)
		{
			if (lPopCalList==-1)
			{
				PopCalCommonComponents(oPC);
			}
			oPC.id = ++lPopCalList
			oPC.calendarInstance = new PopCalCalendarInstance()
			oPC.initCalendar = new Function("PopCalInitCalendar(" + lPopCalList + ");")
			oPC.show = new Function("ctl", "format", "from", "to", "execute", "overwrite", "PopCalShow(ctl, format, from, to, execute, overwrite, " + lPopCalList + ");")
			oPC.formatDate = new Function("dateValue", "oldFormat", "newFormat", "return(PopCalFormatDate(dateValue, oldFormat, newFormat, " + lPopCalList + "));")
			oPC.addDays = new Function("dateValue", "format", "daysToAdd", "return(PopCalAddDays(dateValue, format, daysToAdd, " + lPopCalList + "))")
			oPC.forcedToday = new Function("dateValue", "format", "PopCalForcedToday(dateValue, format, " + lPopCalList + ")")
			oPC.getDate = new Function("dateValue", "dateFormat","return(PopCalGetDate(dateValue, dateFormat, " + lPopCalList + "))")
			oPC.scroll = new Function("PopCalScroll(" + lPopCalList + ");")
			oPC.hide = new Function("PopCalHideCalendar(" + lPopCalList + ",true)")
			objPopCalList[lPopCalList] = oPC
		}
	}
	return (oPC)
}

function PoPCalCreateCalendarInstance()
{
	this.id = 0
	this.startAt = 1 // 0 - sunday, 1 - monday
	this.showWeekNumber = 0 // 0 - don't show, 1 - show
	this.showToday = 0 // 0 - don't show, 1 - show
	this.showWeekend = 1  // 0 - don't show, 1 - show
	this.selectWeekend = 1 // 0 - don't Select, 1 - Select
	this.defaultFormat = "dd/mm/yyyy" //Default Format
	this.fixedX = -1 // x position (-1 if to appear below control)
	this.fixedY = -1 // y position (-1 if to appear below control)
	this.fade = 0 // 0 - don't fade, .1 to 1 - fade (Only IE) 
	this.move = 0  // 0  - don't move, 1 - move
	this.saveMovePos = 0  // 0  - don't save, 1 - save
	this.centuryLimit = 50 // 1940 - 2039
	this.keepInside = 1 // 0 - don't keep inside, 1 - keep inside (Only IE)
	this.executeFade = true
	this.forceTodayTo = null
	this.forceTodayFormat = null
	this.overWriteSelectWeekend = null
	this.overWriteWeekend = null
	this.imgDir = ""
	this.CssClass = "ClassicStyle"
	this.gotoString = ""
	this.todayString = ""
	this.weekString = ""
	this.monthSelected = null
	this.yearSelected = null
	this.dateSelected = null
	this.omonthSelected = null
	this.oyearSelected = null
	this.odateSelected = null
	this.monthConstructed = null
	this.yearConstructed = null
	this.intervalID1 = null
	this.intervalID2 = null
	this.timeoutID1 = null
	this.timeoutID2 = null
	this.timeoutID3 = null
	this.ctlToPlaceValue = null
	this.ctlNow = null
	this.dateFormat = null
	this.nStartingYear = null
	this.onKeyPress = null
	this.onClick = null
	this.onSelectStart = null
	this.onContextMenu = null
	this.onmousemove = null
	this.onmouseup = null
	this.onresize = null
	this.onscroll = null
	this.ControlAlignLeft = null

	this.ie = false
	this.ieVersion = 0
	this.dom = document.getElementById
	this.ns4 = document.layers
	this.opera = navigator.userAgent.indexOf("Opera") != -1
	this.mozilla = ((navigator.userAgent.indexOf("Mozilla")!=-1)&&(navigator.userAgent.indexOf("Netscape")==-1))
	if (!this.opera)
	{
		this.ie = document.all
		var ms = navigator.appVersion.indexOf("MSIE")
		if (ms>0)
		{
			this.ieVersion = parseFloat(navigator.appVersion.substring(ms+5, ms+8))
		}
	}
	this.dateFrom = 01
	this.monthFrom = 00
	this.yearFrom = 1950

	this.dateUpTo = 31
	this.monthUpTo = 11
	this.yearUpTo = 2049

	this.oDate = null
	this.oMonth = null
	this.oYear = null

	this.countMonths = 12

	this.today = null
	this.dayNow = 0
	this.dateNow = 0
	this.monthNow = 0
	this.yearNow = 0
	
	this.defaultX = 0
	this.defaultY = 0

	this.keepMonth = false
	this.keepYear = false
	this.bShow = false

	this.PopCalTimeOut = null
	this.PopCalDragClose = false

	this.HalfYearList = 5

	this.movePopCal = false
	this.commandExecute = null
	this.calendarInstance = null
}

function PopCalCalendarInstance()
{
	this.initialized = 0
}

function PopCalCommonComponents(oPC)
{
	var sComponents = ""
	if (oPC.ie) sComponents += "<div id='CalendarLoadFilters' Style='z-index:+100000;position:absolute;top:0px;left:0px;display:none;filter=" + '"' + "alpha() blendTrans()" + '"' + "'></div>"
	if (((!oPC.ie)&&(!oPC.opera)&&(!oPC.mozilla))||(oPC.ieVersion>=5.5))
	{
//	    sComponents += "<iframe id='popupOverCalendar' name='popupOverCalendar' scrolling=no frameborder=0 style='position:absolute;left:0px;top:0px;width:0px;height:0px;z-index:+10000;display:none;filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);'></iframe>"
	    //	    sComponents += "<iframe id='popupOverYearMonth' name='popupOverCalendar' scrolling=no frameborder=0 style='position:absolute;left:0px;top:0px;width:0px;height:0px;z-index:+10000;display:none;filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0);'></iframe>"
	    sComponents += "<iframe id='popupOverCalendar' name='popupOverCalendar' scrolling=no frameborder=0 style='position:absolute;left:0px;top:0px;width:0px;height:0px;z-index:+10000;display:none;'></iframe>"
	    sComponents += "<iframe id='popupOverYearMonth' name='popupOverCalendar' scrolling=no frameborder=0 style='position:absolute;left:0px;top:0px;width:0px;height:0px;z-index:+10000;display:none;'></iframe>"
        sComponents += "<script language='Javascript'><!--";
	    sComponents += "setOp($I('popupOverCalendar'), 0)";
	    sComponents += "setOp($I('popupOverYearMonth'), 0)";
	    sComponents += "--></script>";
	}
	sComponents += "<div id='popupSuperMonth' style='z-index:+1000000;position:absolute;top:0px;left:0px;display:none;' onclick='PopCalCalendarVisible().bShow=true;'></div>"
	sComponents += "<div id='popupSuperYear' style='z-index:+1000000;position:absolute;top:0px;left:0px;display:none;' onclick='PopCalCalendarVisible().bShow=true;' onMouseWheel='PopCalWheelYear(PopCalCalendarVisible().id)'></div>"
	PopCalWriteHTML(sComponents)
}

function PopCalInitCalendar(l)
{
	var oPC = objPopCalList[l]
	var PopCal=oPC.calendarInstance
	if (PopCal)
	{
		if (PopCal.initialized==0)
		{
			if ((oPC.centuryLimit < 0) || (oPC.centuryLimit > 99))
			{
				oPC.centuryLimit = 40
			}
			var sCalendar = "<div id='popupSuperCalendar" + oPC.id + "' Class='" + oPC.CssClass + "' onContextMenu='return(false)' onClick='PopCalDownMonth(" + l + ");PopCalDownYear(" + l + ");objPopCalList[" + l + "].bShow=true;' style='width:235px;z-index:+100000;position:absolute;top:0px;left:0px;visibility:hidden;'><table cellspacing=0 cellpadding=0 id='popupSuperHighLight" + oPC.id + "' style='border-width:1px;border-style:solid;border-color:a0a0a0;width:100%'><tr><td Style='cursor:default'><table width='100%' Class='TitleStyle' style='table-layout:auto;'><tr><td style='text-align:center; vertical-align:top'><span id='popupSuperCaption" + oPC.id + "'></span></td><td id='popupSuperMoveCalendar" + oPC.id + "' style='width:1px;cursor:move'></td><td style='cursor:default; text-align:center; vertical-align:middle;'>";
			
			if (document.all) sCalendar += "<Span onClick='ImgCloseBoton" + oPC.id +  ".src=\""+ oPC.imgDir + "close.gif\";objPopCalList[" + l + "].PopCalTimeOut=window.setTimeout(\"window.clearTimeout(objPopCalList[" + l + "].PopCalTimeOut);objPopCalList[" + l + "].PopCalTimeOut=null;PopCalHideCalendar(" + l + ")\",100)'>";
			else sCalendar += "<Span onClick='PopCalHideCalendar(" + l + ");'>";
			
			sCalendar += "<IMG ID='ImgCloseBoton" + oPC.id +  "' SRC='"+oPC.imgDir+"close.gif' onMouseOver='if(objPopCalList["+l+"].PopCalDragClose){this.src=\""+ oPC.imgDir + "closedown.gif\"}' onMouseDown='this.src=\""+ oPC.imgDir + "closedown.gif\"' onMouseUp='this.src=\""+ oPC.imgDir + "close.gif\"' onMouseOut='this.src=\""+ oPC.imgDir + "close.gif\"' onDrag='objPopCalList["+l+"].PopCalDragClose=true;return(false)' Class='CloseButtonStyle'></Span></td></tr></table></td></tr><tr><td align='center' Style='Padding:3px'><span id='popupSuperContent" + oPC.id +  "'></span></td></tr>"

			if (oPC.showToday==1)
			{
				sCalendar += "<tr><td style='padding:4px;' Class='TodayStyle' align=center><span id='popupSuperToday" + oPC.id +  "'></span></td></tr>"
			}
			sCalendar += "</table></div>"
			PopCalWriteHTML(sCalendar)


			oPC.today = new Date()

			if (oPC.forceTodayTo!=null)
			{
				if (oPC.forceTodayFormat==null)
				{
					oPC.forceTodayFormat = oPC.defaultFormat
				}

				if (PopCalSetDMY(oPC.forceTodayTo, oPC.forceTodayFormat, l))
				{
					oPC.today = new Date(oPC.oYear, oPC.oMonth, oPC.oDate)
				}
			}

			oPC.dayNow = oPC.today.getDay()
			oPC.dateNow = oPC.today.getDate()
			oPC.monthNow = oPC.today.getMonth()
			oPC.yearNow = oPC.today.getFullYear()

			oPC.monthName = new Array(new Array("Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"))[0];

			if (oPC.startAt==0) oPC.dayName = new Array(new Array("Domingo","Lunes","Martes","Mi&eacute;rcoles","Jueves","Viernes","S&aacute;bado"))[0];
			else oPC.dayName = new Array(new Array("Lunes","Martes","Mi&eacute;rcoles","Jueves","Viernes","S&aacute;bado","Domingo"))[0];

			oPC.gotoString = new Array("Mes Actual")[0];
			oPC.weekString = new Array("#")[0];
			oPC.todayString = new Array("Hoy es " + oPC.dayName[(oPC.dayNow-oPC.startAt==-1)?6:(oPC.dayNow-oPC.startAt)]+ ", " + oPC.dateNow + " de " + oPC.monthName[oPC.monthNow] + " de " + oPC.yearNow)[0];

			oPC.monthConstructed=false
			oPC.yearConstructed=false

			if (oPC.showToday==1)
			{
				document.getElementById("popupSuperToday" + oPC.id).innerHTML = "<Span onmouseover='window.status=\""+oPC.gotoString+"\"' onmouseout='window.status=\"\"' title='"+oPC.gotoString+"' Class='TextStyle' onClick='PopCalChangeCurrentMonth("+l+");'>" + oPC.todayString + "</Span>"
			}

			var sHTML1 = "<IMG ID='ImgGomaCal' SRC='" + oPC.imgDir + "imgBorrar.gif' style='cursor:pointer;padding-right:10px;vertical-align:middle;' onClick=\"BorrarFecha(" + l + ");\" title='Borrar fecha seleccionada'>&nbsp;&nbsp;";
			sHTML1 += "<span id='popupSuperSpanLeft" + oPC.id + "' style='margin-top:0px;' Class='DropDownStyle' onDrag='return(false)' onmouseover='PopCalSwapImage(\"popupSuperChangeLeft" + oPC.id + "\",\"left2.gif\"," + l + ");this.className=\"DropDownOverStyle\";' onmouseout='clearInterval(objPopCalList[" + l + "].intervalID1);PopCalSwapImage(\"popupSuperChangeLeft" + oPC.id + "\",\"left1.gif\"," + l + ");this.className=\"DropDownOutStyle\";window.status=\"\"' onclick='PopCalDecMonth(" + l + ")' onmousedown='clearTimeout(objPopCalList[" + l + "].timeoutID1);objPopCalList[" + l + "].timeoutID1=setTimeout(\"PopCalStartDecMonth(" + l + ")\",100)' onmouseup='clearTimeout(objPopCalList[" + l + "].timeoutID1);clearInterval(objPopCalList[" + l + "].intervalID1)'>&nbsp;<IMG id='popupSuperChangeLeft" + oPC.id + "' SRC='" + oPC.imgDir + "left1.gif' border=0>&nbsp;</span>&nbsp;"
			sHTML1 += "<span id='popupSuperSpanMonth" + oPC.id + "' style='width:85px;margin-top:0px;' Class='DropDownStyle'onDrag='return(false)' onmouseover='PopCalSwapImage(\"popupSuperChangeMonth" + oPC.id + "\",\"drop2.gif\"," + l + ");this.className=\"DropDownOverStyle\";' onmouseout='PopCalSwapImage(\"popupSuperChangeMonth" + oPC.id + "\",\"drop1.gif\"," + l + ");this.className=\"DropDownOutStyle\";window.status=\"\"' onclick='objPopCalList[" + l + "].keepMonth=!PopCalIsObjectVisible(objPopCalList[" + l + "].calendarInstance.popupSuperMonth);PopCalUpMonth(" + l + ")'></span>&nbsp;"
			sHTML1 += "<span id='popupSuperSpanRight" + oPC.id + "' style='margin-top:0px;' Class='DropDownStyle' onDrag='return(false)' onmouseover='PopCalSwapImage(\"popupSuperChangeRight" + oPC.id + "\",\"right2.gif\"," + l + ");this.className=\"DropDownOverStyle\";' onmouseout='clearInterval(objPopCalList[" + l + "].intervalID1);PopCalSwapImage(\"popupSuperChangeRight" + oPC.id + "\",\"right1.gif\"," + l + ");this.className=\"DropDownOutStyle\";window.status=\"\"' onclick='PopCalIncMonth(" + l + ")' onmousedown='clearTimeout(objPopCalList[" + l + "].timeoutID1);objPopCalList[" + l + "].timeoutID1=setTimeout(\"PopCalStartIncMonth(" + l + ")\",100)' onmouseup='clearTimeout(objPopCalList[" + l + "].timeoutID1);clearInterval(objPopCalList[" + l + "].intervalID1)'>&nbsp;<IMG id='popupSuperChangeRight" + oPC.id + "' SRC='" + oPC.imgDir + "right1.gif' border=0>&nbsp;</span>&nbsp;"
			sHTML1 += "<span id='popupSuperSpanYear" + oPC.id + "' style='margin-top:0px;' Class='DropDownStyle' onDrag='return(false)' onmouseover='PopCalSwapImage(\"popupSuperChangeYear" + oPC.id + "\",\"drop2.gif\"," + l + ");this.className=\"DropDownOverStyle\";'	onmouseout='PopCalSwapImage(\"popupSuperChangeYear" + oPC.id + "\",\"drop1.gif\"," + l + ");this.className=\"DropDownOutStyle\";window.status=\"\"' onclick='objPopCalList[" + l + "].keepYear=!PopCalIsObjectVisible(objPopCalList[" + l + "].calendarInstance.popupSuperYear);PopCalUpYear(" + l + ")' onMouseWheel='PopCalWheelYear(" + l + ")'></span>&nbsp;"

			document.getElementById("popupSuperCaption" + oPC.id).innerHTML = sHTML1

			if (oPC.ie)
			{
				if (oPC.move == 1)
				{
					var superMoveCalendar = document.getElementById("popupSuperMoveCalendar" + oPC.id)
					superMoveCalendar.width="100%"
					superMoveCalendar.onmousedown=new Function("PopCalDrag("+l+")")
					superMoveCalendar.ondblclick=new Function("PopCalMoveDefault("+l+")")
					superMoveCalendar.onmouseup=new Function("PopCalDrop("+l+")")
				}
			}
//			else
//			{
//				oPC.keepInside = 0
//			}

	
			PopCal.id = oPC.id
			PopCal.startAt = oPC.startAt
			PopCal.showWeekNumber = oPC.showWeekNumber
			PopCal.showToday = oPC.showToday
			PopCal.showWeekend = oPC.showWeekend
			PopCal.selectWeekend = oPC.selectWeekend
			PopCal.language = oPC.language
			PopCal.defaultFormat = oPC.defaultFormat
			PopCal.fixedX = oPC.fixedX
			PopCal.fixedY = oPC.fixedY
			PopCal.fade = oPC.fade
			PopCal.centuryLimit = oPC.centuryLimit
			PopCal.move = oPC.move
			PopCal.saveMovePos = oPC.saveMovePos
			PopCal.keepInside = oPC.keepInside
			PopCal.popupSuperCalendar = document.getElementById("popupSuperCalendar" + oPC.id)
			PopCal.popupSuperMonth = document.getElementById("popupSuperMonth")
			PopCal.popupSuperYear = document.getElementById("popupSuperYear")
			PopCal.popupSuperYearList = []

			PopCal.popupSuperCalendar.OverSelect = document.getElementById("popupOverCalendar")
			PopCal.popupSuperMonth.OverSelect = document.getElementById("popupOverYearMonth")
			PopCal.popupSuperYear.OverSelect = document.getElementById("popupOverYearMonth")
			if (oPC.ie)
			{
				PopCal.popupSuperCalendar.style.filter="blendTrans()"
				if ((oPC.ieVersion < 5.5) || (typeof(document.getElementById("CalendarLoadFilters").filters)!="object"))
				{
					PopCal.fade = 0
				}
			}
			PopCal.initialized = 1
		}
	}
}

function PopCalCalendarVisible()
{
	for( var i = 0; i <= lPopCalList; i++ )
	{
		if ( objPopCalList[i].calendarInstance.popupSuperCalendar.style.visibility != "hidden" ) 
		{
			return (objPopCalList[i])
		}
	}
	return (null)
}

function PopCalSetFocus(ctl)
{
	try
	{
		ctl.focus()
	}
	catch(e)
	{
		//Nothing
	}
}

function PopCalWriteHTML(sHTML)
{
	if (document.body)
	{
		if (document.body.insertAdjacentHTML)
		{
			document.body.insertAdjacentHTML("afterBegin", sHTML)
		}
		else
		{
			document.write(sHTML)
		}
	}
	else
	{
		document.write(sHTML)
	}
}

function PopCalSetPosition(o, t, l, h, w)
{
	if (t != null) o.style.top = t + "px"
	if (l != null) o.style.left = l + "px"
	if (h != null) o.style.height = h + "px"
	if (w != null) o.style.width = w + "px"
	if (o.OverSelect)
	{
	    o.OverSelect.style.top = parseInt(o.style.top, 10) + "px"
	    o.OverSelect.style.left = parseInt(o.style.left, 10) + "px"
	    o.OverSelect.style.height = (document.all)?  o.offsetHeight + "px" : o.innerHeight  + "px"
	    o.OverSelect.style.width = (document.all) ? o.offsetWidth + "px" : o.width + "px"
	}
}

function PopCalShow(ctl, format, from, to, execute, overwrite, l)
{
	var oPC = objPopCalList[l]
	var PopCal=oPC.calendarInstance
	PopCalendar.lastCalendar = oPC
	
	var CenturyOn = true
	if (PopCal)
	{
		if (PopCal.initialized==1)
		{
			if (document.body)
			{
				PopCalSetFocus(document.body)
			}
			
			oPC.movePopCal = false

			if (oPC.timeoutID3 != null)
			{
				clearTimeout(oPC.timeoutID3)
				oPC.timeoutID3 = null
			}
			var objPopCalVisible = PopCalCalendarVisible()
			if ( objPopCalVisible == null ) 
			{

				oPC.overWriteSelectWeekend = oPC.overWriteWeekend
				
				oPC.overWriteWeekend = null

				if (overwrite!=null)
				{
					overwrite = PopCalPad(overwrite, 2, " ", "R")
					if (("01").indexOf(overwrite.substr(0,1))!=-1)
					{
						oPC.overWriteSelectWeekend = parseInt(overwrite.substr(0,1),10)
					}
				}

				oPC.commandExecute = null

				if (execute!=null)
				{
					oPC.commandExecute = execute
				}

				if (oPC.ie)
				{
					oPC.onKeyPress = document.onkeyup
					document.onkeyup = new Function("objPopCalList["+l+"].bShow=false;PopCalSetFocus(objPopCalList["+l+"].ctlToPlaceValue);PopCalClickDocumentBody("+l+");")
					oPC.onmouseup = document.onmouseup
					document.onmouseup=new Function("objPopCalList["+l+"].movePopCal=false;if(event.button==2){objPopCalList["+l+"].bShow=false;PopCalClickDocumentBody("+l+");}")
					if (PopCal.move == 1)
					{
						oPC.onmousemove = document.onmousemove
						document.onmousemove= new Function('PopCalTrackMouse('+l+');')
					}
					oPC.onresize = window.onresize
					window.onresize = new Function('PopCalScroll('+l+');')
					if (PopCal.keepInside==1)
					{
						oPC.onscroll = window.onscroll
						window.onscroll = new Function('PopCalScroll('+l+');')
					}
				}
				else
				{
					oPC.onKeyPress = document.onkeyup
					document.captureEvents(Event.KEYUP)
					document.onkeyup = new Function("objPopCalList["+l+"].bShow=false;PopCalClickDocumentBody("+l+");PopCalSetFocus(objPopCalList["+l+"].ctlToPlaceValue);")
				}

				oPC.onClick = document.onclick
				document.onclick = new Function('PopCalClickDocumentBody('+l+');')

				if (oPC.ie)
				{
					oPC.onSelectStart = document.onselectstart
					document.onselectstart=new Function('return(false);')

					oPC.onContextMenu = document.oncontextmenu
					document.oncontextmenu=new Function('return(false);')
				}

				oPC.yearConstructed=false
				oPC.monthConstructed=false

				oPC.ctlToPlaceValue = ctl
				PopCalSetScroll("Div", l, false)
				
				oPC.dateFormat=""

				if (format!=null)
				{
					oPC.dateFormat = format.toLowerCase()
				}
				else if (PopCal.defaultFormat!=null)
				{
					oPC.dateFormat = PopCal.defaultFormat.toLowerCase()
				}

				oPC.dateFrom = 01
				oPC.monthFrom = 00
				oPC.yearFrom = 1950
				oPC.dateUpTo = 31
				oPC.monthUpTo = 11
				oPC.yearUpTo = 2049

				oPC.dateSelected = 0
				oPC.monthSelected = oPC.monthNow
				oPC.yearSelected = oPC.yearNow

				if (PopCalSetDMY(ctl.value, oPC.dateFormat, l))
				{
					oPC.dateSelected = oPC.oDate
					oPC.monthSelected = oPC.oMonth
					oPC.yearSelected = oPC.oYear
				}
				if (from!=null)
				{
					if (PopCalIsToday(from))
					{
						oPC.dateFrom = oPC.today.getDate()
						oPC.monthFrom = oPC.today.getMonth()
						oPC.yearFrom = oPC.today.getFullYear()
					}
					else if (PopCalSetDMY(from, oPC.dateFormat, l))
					{
						oPC.dateFrom = oPC.oDate
						oPC.monthFrom = oPC.oMonth
						oPC.yearFrom = oPC.oYear
					}
				}

				if (to!=null)
				{
					if (PopCalIsToday(to))
					{
						oPC.dateUpTo = oPC.today.getDate()
						oPC.monthUpTo = oPC.today.getMonth()
						oPC.yearUpTo = oPC.today.getFullYear()
					}
					else if (PopCalSetDMY(to, oPC.dateFormat, l))
					{
						oPC.dateUpTo = oPC.oDate
						oPC.monthUpTo = oPC.oMonth
						oPC.yearUpTo = oPC.oYear
					}
				}

				if (!PopCalCenturyOn(oPC.dateFormat))
				{
					if (PopCalDateFrom(l) < PopCalPad(1950 + oPC.centuryLimit, 4, "0", "L") + "0001")
					{
						oPC.dateFrom = 01
						oPC.monthFrom = 00
						oPC.yearFrom = 1950 + oPC.centuryLimit
					}

					if (PopCalDateUpTo(l) >  PopCalPad(2000 + (oPC.centuryLimit-1), 4, "0", "L" ) + "1131")
					{
						oPC.dateUpTo = 31
						oPC.monthUpTo = 11
						oPC.yearUpTo = 2000 + (oPC.centuryLimit-1)
					}
				}

				if (PopCalDateFrom(l) > PopCalDateUpTo(l))
				{
					oPC.oDate = oPC.dateFrom
					oPC.oMonth = oPC.monthFrom
					oPC.oYear = oPC.yearFrom

					oPC.dateFrom = oPC.dateUpTo
					oPC.monthFrom = oPC.monthUpTo
					oPC.yearFrom = oPC.yearUpTo

					oPC.dateUpTo = oPC.oDate
					oPC.monthUpTo = oPC.oMonth
					oPC.yearUpTo = oPC.oYear
				}

				if (PopCalDateSelect(l) < PopCalDateFrom(l))
				{
					oPC.dateSelected = 0
					oPC.monthSelected = oPC.monthFrom
					oPC.yearSelected = oPC.yearFrom
				}

				if (PopCalDateSelect(l) > PopCalDateUpTo(l))
				{
					oPC.dateSelected = 0
					oPC.monthSelected = oPC.monthUpTo
					oPC.yearSelected = oPC.yearUpTo
				}

				oPC.odateSelected = oPC.dateSelected
				oPC.omonthSelected = oPC.monthSelected
				oPC.oyearSelected = oPC.yearSelected

				PopCalMoveDefaultPos(l)

				if (oPC.ie)
				{
					if ((PopCal.move == 1) && (PopCal.saveMovePos == 1))
					{
						if (oPC.ctlToPlaceValue != null)
						{
							if (oPC.ctlToPlaceValue.CalendarTop != null)
							{
								PopCalSetPosition(PopCal.popupSuperCalendar, oPC.ctlToPlaceValue.CalendarTop)
							}
							if (oPC.ctlToPlaceValue.CalendarLeft != null)
							{
								PopCalSetPosition(PopCal.popupSuperCalendar, null, oPC.ctlToPlaceValue.CalendarLeft)
							}
						}
					}
				}

				PopCalConstructCalendar(l)
				
				PopCalFadeIn(l)

				PopCalScroll(l)

				oPC.bShow = true
				
			}
			else
			{
				objPopCalVisible.executeFade = (objPopCalVisible.ctlNow==ctl)
				oPC.executeFade = (objPopCalVisible.ctlNow==ctl)
				
				PopCalHideCalendar(objPopCalVisible.id)

				if (objPopCalVisible.ctlToPlaceValue != null)
				{
					objPopCalVisible.ctlToPlaceValue = null
				}
				
				if (oPC!=objPopCalVisible)
				{
					oPC.ctlNow = null
				}

				if (oPC.ctlNow!=ctl) 
				{
					PopCalShow(ctl, format, from, to, execute, overwrite, oPC.id)
				}
				
				oPC.executeFade = true
				objPopCalVisible.executeFade = true
			}
			oPC.ctlNow = ctl
		}
	}
}

function PopCalAddDays(dateValue, format, daysToAdd, l)
{
	var oPC = objPopCalList[l]
	if ((dateValue!=null)&&(dateValue!=""))
	{
		var sDateFormat = (format==null) ? oPC.calendarInstance.defaultFormat.toLowerCase() : format.toLowerCase()
		var incDays = (daysToAdd==null) ? 0 : daysToAdd * 86400000
		var dFecha = null
		if (PopCalIsToday(dateValue))
		{
			dFecha = new Date(oPC.today -(-incDays))				
		}
		else if (PopCalSetDMY(dateValue, sDateFormat, l))
		{
			dFecha = new Date(PopCalGetDate(dateValue, sDateFormat, l) -(-incDays))
		}
		if (dFecha!=null) return (PopCalConstructDate(dFecha.getDate(),dFecha.getMonth(),dFecha.getFullYear(),sDateFormat,l))
	}
	return ("")
}

function PopCalScroll(l)
{
	var oPC = objPopCalList[l]
	if (oPC.calendarInstance.popupSuperCalendar.OverSelect)
	{
		oPC.calendarInstance.popupSuperCalendar.OverSelect.style.visibility = 'hidden'
		oPC.calendarInstance.popupSuperCalendar.OverSelect.style.visibility = 'visible'
	}
	if (oPC.calendarInstance.popupSuperCalendar.style.visibility != "hidden")
	{
		if ((oPC.ctlToPlaceValue.CalendarTop == null) && (oPC.ctlToPlaceValue.CalendarLeft == null))
		{
			PopCalDownMonth(l)
			PopCalDownYear(l)
			PopCalMoveDefault(l)
		}
	}
}

function PopCalMoveDefaultPos(l)
{
	var oPC = objPopCalList[l]
	var PopCal = oPC.calendarInstance
	var leftpos=0
	var toppos=0
	var lDivTop = -1

	if (((PopCal.fixedX==-1)&&(PopCal.fixedY==-1)&&(oPC.ctlToPlaceValue.style.display!='none'))||(oPC.ControlAlignLeft!=null))
	{
		var aTag = null
		if (oPC.ControlAlignLeft!=null)
		{
			aTag = oPC.ControlAlignLeft
		}
		else
		{
			aTag = oPC.ctlToPlaceValue
		}
		do 
		{
			aTag = aTag.offsetParent
			leftpos += aTag.offsetLeft 
			toppos  += aTag.offsetTop
			if (aTag.tagName.toLowerCase() == "div")
			{
				if (lDivTop == -1)
				{
					lDivTop += (1 + aTag.offsetTop)
				}
				leftpos -= aTag.scrollLeft
				toppos -= aTag.scrollTop
			}
			else if (lDivTop != -1)
			{
				lDivTop += aTag.offsetTop
			}
		} 
		while(aTag.tagName.toLowerCase()!="body")
	}
	else
	{
		var aTag = document.body
	}

	if (oPC.ControlAlignLeft!=null)
	{
		leftpos += oPC.ControlAlignLeft.offsetLeft + parseInt(oPC.ControlAlignLeft.offsetWidth,10) - parseInt(PopCal.popupSuperCalendar.offsetWidth,10)
		toppos += oPC.ControlAlignLeft.offsetTop + parseInt(oPC.ControlAlignLeft.offsetHeight,10) + 7
	}
	else
	{
		leftpos = parseInt(PopCal.fixedX==-1 ? oPC.ctlToPlaceValue.offsetLeft + leftpos: PopCal.fixedX,10)
		toppos = parseInt(PopCal.fixedY==-1 ? oPC.ctlToPlaceValue.offsetTop + toppos + oPC.ctlToPlaceValue.offsetHeight + 7 : PopCal.fixedY,10)
	}

	if (PopCal.keepInside==1)
	{
		if ((leftpos + PopCal.popupSuperCalendar.offsetWidth + 10 - aTag.scrollLeft) > aTag.offsetWidth)
		{
			leftpos -= (((((leftpos + PopCal.popupSuperCalendar.offsetWidth) - aTag.offsetWidth) + 10) - aTag.scrollLeft))
		}
		if 	(leftpos < aTag.scrollLeft + 10)
		{
			leftpos = aTag.scrollLeft + 10
		}

		if (toppos < lDivTop)
		{
			toppos = lDivTop
		}
		
		if (((toppos + PopCal.popupSuperCalendar.offsetHeight + 100) - aTag.scrollTop) > aTag.offsetHeight)
		{
			toppos -= ((((toppos + PopCal.popupSuperCalendar.offsetHeight) - aTag.offsetHeight) + 140) - aTag.scrollTop)
		}
		
		if 	(toppos < aTag.scrollTop + 10)
		{
			toppos = aTag.scrollTop + 10
		}
	}
	if (leftpos<10)
	{
		 leftpos = 10
	}
	PopCalSetPosition(PopCal.popupSuperCalendar, toppos, leftpos)
}


function PopCalMoveDefault(l)
{
	var oPC = objPopCalList[l]
	var PopCal = oPC.calendarInstance
	
	PopCalMoveDefaultPos(l)

	if (PopCal.saveMovePos == 1)
	{
		if (oPC.ctlToPlaceValue != null)
		{
			oPC.ctlToPlaceValue.CalendarLeft = null
			oPC.ctlToPlaceValue.CalendarTop = null
		}
	}

	oPC.bShow = false
}

function PopCalFormatDate(dateValue, oldFormat, newFormat, l)
{
	var oPC = objPopCalList[l]
	var PopCal = oPC.calendarInstance
	var newValue = ""
	if (PopCal)
	{
		var formatOld = PopCal.defaultFormat
		if (oldFormat!=null) 
		{
			formatOld = oldFormat
		}
		var formatNew = PopCal.defaultFormat 

		if (newFormat!=null) 
		{
			formatNew = newFormat
		}

		if (PopCalIsToday(dateValue))
		{
			newValue = PopCalConstructDate(oPC.today.getDate(),oPC.today.getMonth(),oPC.today.getFullYear(),formatNew,l)				
		}
		else if (PopCalSetDMY(dateValue, formatOld, l))
		{
			var xDate = new Date(oPC.oYear, oPC.oMonth, oPC.oDate)
			if ((xDate.getDate()==oPC.oDate)&&(xDate.getMonth()==oPC.oMonth)&&(xDate.getFullYear()==oPC.oYear))
			{
				newValue = PopCalConstructDate(oPC.oDate,oPC.oMonth,oPC.oYear,formatNew,l)
			}
		}
	}
	return newValue
}

function PopCalForcedToday(dateValue, format, l)
{
	var oPC = objPopCalList[l]
	if (oPC.calendarInstance)
	{
		oPC.forceTodayTo = dateValue
		oPC.forceTodayFormat = format
	}
}

function PopCalSetScroll(elmID, l, restore)
{
	var oPC = objPopCalList[l]
	if( oPC.ie)
	{
		if (oPC.ctlToPlaceValue != null)
		{
			if (PopCalIsObjectVisible(oPC.ctlToPlaceValue))
			{
				var objParent = oPC.ctlToPlaceValue.offsetParent
				while((objParent)&&(objParent.tagName.toLowerCase()!="body"))
				{
					if (objParent.tagName.toLowerCase() == elmID.toLowerCase())
					{
						if (restore)
						{
							objParent.onscroll = null
							if (objParent.savedScroll!=null) objParent.onscroll = objParent.savedScroll
							objParent.savedScroll = null
						}
						else
						{
							objParent.savedScroll = objParent.onscroll
							objParent.onscroll = new Function("PopCalScroll("+l+");")
						}
					}
					objParent = objParent.offsetParent
				} 
			}
		}
	}
}

function PopCalSwapImage(srcImg, destImg, l)
{
	var oPC = objPopCalList[l]
	document.getElementById(srcImg).src = oPC.imgDir + destImg
}

function PopCalHideCalendar(l, HideNow)	
{
	var oPC = objPopCalList[l]
	if (!oPC) 
	{
		oPC=null
		return(false)
	}
	var PopCal = oPC.calendarInstance
	if (PopCal.popupSuperCalendar.style.visibility != "hidden")
	{
		PopCalSetScroll("Div", l, true)
		if (oPC.ie)
		{
			document.onkeyup = oPC.onKeyPress
		}
		else
		{
			document.releaseEvents(Event.KEYUP)
			document.onkeyup = oPC.onKeyPress
		}

		document.onclick = oPC.onClick
		
		if (oPC.ie)
		{
			document.onselectstart = oPC.onSelectStart
			document.oncontextmenu = oPC.onContextMenu
		}

		if (oPC.ie)
		{
			if (PopCal.move == 1)
			{
				document.onmousemove = oPC.onmousemove
			}
			document.onmouseup = oPC.onmouseup
			window.onresize = oPC.onresize
			if (PopCal.keepInside==1)
			{
				window.onscroll = oPC.onscroll
			}
		}
			
		oPC.onKeyPress = null
		oPC.onClick = null
		oPC.onSelectStart = null
		oPC.onContextMenu = null
		oPC.onmousemove = null
		oPC.onmouseup = null
		oPC.onresize = null
		oPC.onscroll = null

		if (PopCal.popupSuperMonth != null)
		{
			PopCal.popupSuperMonth.style.display="none"
			if (PopCal.popupSuperMonth.OverSelect)
			{
				PopCal.popupSuperMonth.OverSelect.style.display="none"
				PopCal.popupSuperMonth.OverSelect.style.height = 1 + "px"
				PopCal.popupSuperMonth.OverSelect.style.width = 1 + "px"
			}
		}
		if (PopCal.popupSuperYear != null)
		{
			PopCal.popupSuperYear.style.display="none"
			if (PopCal.popupSuperYear.OverSelect)
			{
				PopCal.popupSuperYear.OverSelect.style.display="none"
				PopCal.popupSuperYear.OverSelect.style.height = 1 + "px"
				PopCal.popupSuperYear.OverSelect.style.width = 1 + "px"
			}
		}
		try {
		    (document.all) ? $I("ImgGomaCal").style.visibility = "hidden" : $I("ImgGomaCal").setAttribute("style", "visibility:hidden");
		}catch (e) {};
		
		PopCalFadeOut(l, HideNow)

		if (!oPC) 
		{
			oPC=null
			return(false)
		}
	}
}

function PopCalFadeIn(l) {
    var oPC = objPopCalList[l]
    var PopCal = oPC.calendarInstance

    var objCal = PopCal.popupSuperCalendar
    var objOver = PopCal.popupSuperCalendar.OverSelect
    if (!oPC.ie) {
        objCal.style.display = "none"
        objCal.style.visibility = "visible"
        objCal.style.display = "inline-block"
    }
    else if ((PopCal.fade > 0) && (oPC.executeFade)) {
        objCal.filters.blendTrans.Stop()

        if (PopCal.fade > 1) PopCal.fade = 1

        objCal.style.filter = "blendTrans(duration=" + PopCal.fade + ")"

        if ((objCal.style.visibility != "visible") && (objCal.filters.blendTrans.status != 2)) {
            objCal.filters.blendTrans.Apply()
            objCal.style.visibility = "visible"
            objCal.filters.blendTrans.Play()
        }
        else {
            objCal.style.visibility = "visible"
        }
    }
    else {
        objCal.style.visibility = "visible"
    }
    if (objOver) objOver.style.display = 'inline-block'
}

function PopCalFadeOut(l, HideNow) {
    var oPC = objPopCalList[l]

    var PopCal = oPC.calendarInstance
    var objCal = PopCal.popupSuperCalendar

    if ((oPC.ie) && (PopCal.fade > 0) && (oPC.executeFade) && (!HideNow)) {

        objCal.filters.blendTrans.Stop()

        if (PopCal.fade > 1) PopCal.fade = 1

        objCal.style.filter = "blendTrans(duration=" + PopCal.fade + ")"

        if ((objCal.style.visibility != "hidden") && (objCal.filters.blendTrans.status != 2)) {
            objCal.filters.blendTrans.Apply()
            objCal.style.visibility = "hidden"
            objCal.filters.blendTrans.Play()
            oPC.timeoutID3 = setTimeout("PopCalMoveTo(0, 0, " + l + ")", (PopCal.fade + .05) * 1000)
        }
        else {
            objCal.style.visibility = "hidden"
            PopCalMoveTo(0, 0, l)
        }       
    }
    else {
        objCal.style.visibility = "hidden"
        PopCalMoveTo(0, 0, l)
    }
}
function PopCalMoveTo(x, y, l) {
    if (!objPopCalList) return (true)
    var oPC = objPopCalList[l]
    if (!oPC) return (true)
    if (PopCalCalendarVisible() == null) {
        var PopCal = oPC.calendarInstance
        var objCal = PopCal.popupSuperCalendar
        var objOver = PopCal.popupSuperCalendar.OverSelect

        objCal.style.left = x + "px"
        objCal.style.top = y + "px"
        if (objOver) {
            objOver.style.left = x + "px"
            objOver.style.top = y + "px"
            objOver.style.display = "none"
        }
    }
    if (oPC.timeoutID3 != null) {
        clearTimeout(oPC.timeoutID3)
        oPC.timeoutID3 = null
    }
}
function PopCalIsObjectVisible(obj)
{
	var bVisible = ((obj.style.display != 'none')&&(obj.style.visibility!='hidden'))
	var objParent = obj.offsetParent
	while((objParent)&&(objParent.tagName.toLowerCase()!="body")&&(bVisible))
	{
		bVisible = ((objParent.style.display != 'none')&&(objParent.style.visibility!='hidden'))
		objParent = objParent.offsetParent
	}
	return (bVisible)
}

function PopCalConstructDate(d,m,y,format,l)
{
	var oPC = objPopCalList[l]
	var sTmp = format
	sTmp = sTmp.replace ("dd","<e>")
	sTmp = sTmp.replace ("d","<d>")
	sTmp = sTmp.replace ("<e>",PopCalPad(d, 2, "0", "L"))
	sTmp = sTmp.replace ("<d>",d)
	sTmp = sTmp.replace ("mmmm","<l>")
	sTmp = sTmp.replace ("mmm","<s>")
	sTmp = sTmp.replace ("mm","<n>")
	sTmp = sTmp.replace ("m","<m>")
	sTmp = sTmp.replace ("yyyy",PopCalPad(y, 4, "0", "L"))
	sTmp = sTmp.replace ("yy",PopCalPad(y, 4, "0", "L").substr(2))
	sTmp = sTmp.replace ("<m>",m+1)
	sTmp = sTmp.replace ("<n>",PopCalPad(m+1, 2, "0", "L"))
	sTmp = sTmp.replace ("<s>",PopCalSpcChr(oPC.monthName[m]).substr(0,3))
	sTmp = sTmp.replace ("<l>",PopCalSpcChr(oPC.monthName[m]))
	return sTmp
}

function PopCalCloseCalendar(l) 
{
	var oPC = objPopCalList[l]
	PopCalHideCalendar(l)
	if (!oPC) 
	{
		oPC=null
		return(false)
	}
	oPC.ctlToPlaceValue.value = PopCalConstructDate(oPC.dateSelected,oPC.monthSelected,oPC.yearSelected,oPC.dateFormat,l)
	if (oPC.commandExecute!=null)
	{
		eval(oPC.commandExecute)
	}
	else
	{
		PopCalSetFocus(oPC.ctlToPlaceValue)
	}
}

function PopCalClickDocumentBody(l) 
{ 		
	var oPC = objPopCalList[l]
	if (oPC.ie)
	{
		if (event.keyCode==82)
		{
			if (oPC.ctlToPlaceValue != null)
			{
				if ((oPC.ctlToPlaceValue.CalendarLeft != null)&&(oPC.ctlToPlaceValue.CalendarTop != null))
				{
					PopCalMoveDefault(l)
					PopCalDrop(l)
					if (document.body)
					{
						PopCalSetFocus(document.body)
					}
				}
			}
		}
	}
	document.getElementById("popupSuperHighLight" + oPC.id).style.borderColor = "#a0a0a0"
	oPC.PopCalDragClose = false
	if (!oPC.bShow)
	{
		PopCalHideCalendar(l)
	}
	if (!oPC)
	{
		oPC=null
		return(false)
	}
	oPC.bShow = false
}


/*** Month Pulldown	***/
function PopCalStartDecMonth(l)
{
	var oPC = objPopCalList[l]
	oPC.intervalID1=setInterval("PopCalDecMonth("+l+")",80)
}

function PopCalStartIncMonth(l)
{
	var oPC = objPopCalList[l]
	oPC.intervalID1=setInterval("PopCalIncMonth("+l+")",80)
}

function PopCalIncMonth(l) 
{
	var oPC = objPopCalList[l]
	oPC.monthSelected++
	if (oPC.monthSelected>11) {
		oPC.monthSelected=0
		oPC.yearSelected++
	}

	if ((oPC.yearSelected > oPC.yearUpTo) || (oPC.yearSelected == oPC.yearUpTo && oPC.monthSelected > oPC.monthUpTo))
	{
		PopCalDecMonth(l)
	}
	else
	{
		PopCalConstructCalendar(l)
	}
}

function PopCalDecMonth(l)
{
	var oPC = objPopCalList[l]
	oPC.monthSelected--
	if (oPC.monthSelected<0)
	{
		oPC.monthSelected=11
		oPC.yearSelected--
	}

	if ((oPC.yearSelected < oPC.yearFrom) || (oPC.yearSelected == oPC.yearFrom && oPC.monthSelected < oPC.monthFrom))
	{
		PopCalIncMonth(l)
	}
	else
	{
		PopCalConstructCalendar(l)
	}
}

function PopCalConstructMonth(l)
{
	var oPC = objPopCalList[l]
	PopCalDownYear(l)
	if (!oPC.monthConstructed)
	{
		var beginMonth = 0
		var endMonth = 11

		oPC.countMonths = 0

		if (oPC.yearSelected == oPC.yearFrom)
		{
			beginMonth = oPC.monthFrom
		}

		if (oPC.yearSelected == oPC.yearUpTo)
		{
			endMonth = oPC.monthUpTo
		}

		var sHTML = ""
		for (var i=beginMonth; i<=endMonth; i++)
		{
			oPC.countMonths++
			var sName = oPC.monthName[i]
			if (i==oPC.monthSelected){
				sName =	"<B>" +	sName +	"</B>"
			}
			sHTML += "<tr><td id='popupSuperMonth" + i + "' Class='OptionStyle' onmouseover='objPopCalList["+l+"].bShow=true;this.className=\"OptionOverStyle\"' onmouseout='objPopCalList["+l+"].bShow=false;this.className=\"OptionOutStyle\"' style='cursor:default' onclick='objPopCalList["+l+"].bShow=false;objPopCalList["+l+"].monthConstructed=false;objPopCalList["+l+"].monthSelected=" + i + ";PopCalConstructCalendar("+l+");PopCalDownMonth("+l+");event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
		}

		var PopCal = oPC.calendarInstance
		PopCal.popupSuperMonth.className = oPC.CssClass
		PopCal.popupSuperMonth.innerHTML = "<table width='100%' Style='border-width:1;border-style:solid;border-color:#a0a0a0;' Class='ListStyle' cellspacing=0 onmouseover='clearTimeout(objPopCalList["+l+"].timeoutID1)' onmouseout='clearTimeout(objPopCalList["+l+"].timeoutID1);event.cancelBubble=true'>" + sHTML + "</table>"

		oPC.monthConstructed=true
	}
}

function PopCalUpMonth(l)
{
	var oPC = objPopCalList[l]
	if ((oPC.yearSelected == oPC.yearFrom) || (oPC.yearSelected == oPC.yearUpTo))
	{
		oPC.monthConstructed=false
	}
	else if (oPC.countMonths != 12)
	{
		oPC.monthConstructed=false
	}
	
	PopCalConstructMonth(l)

	var PopCal = oPC.calendarInstance
	PopCal.popupSuperMonth.style.display = "inline-block"
	if (PopCal.popupSuperMonth.OverSelect) PopCal.popupSuperMonth.OverSelect.style.display = "inline-block"

	var lTop = parseInt(PopCal.popupSuperCalendar.style.top,10) +  + parseInt(document.getElementById("popupSuperSpanMonth" + oPC.id).offsetTop,10) + parseInt(document.getElementById("popupSuperSpanMonth" + oPC.id).offsetHeight,10) + 6
	var lLeft = parseInt(PopCal.popupSuperCalendar.style.left,10) + parseInt(document.getElementById("popupSuperSpanMonth" + oPC.id).offsetLeft,10)
	lLeft += (oPC.ie)?4:5
	PopCalSetPosition(PopCal.popupSuperMonth, lTop, lLeft, null, parseInt(document.getElementById("popupSuperSpanMonth" + oPC.id).offsetWidth,10))
}

function PopCalDownMonth(l)
{
	var oPC = objPopCalList[l]
	if (oPC.calendarInstance.popupSuperMonth.style.display != "none")
	{
		if (!oPC.keepMonth)
		{
			oPC.calendarInstance.popupSuperMonth.style.display = "none"
			if (oPC.calendarInstance.popupSuperMonth.OverSelect)
			{
				oPC.calendarInstance.popupSuperMonth.OverSelect.style.display = "none"
				oPC.calendarInstance.popupSuperMonth.OverSelect.style.height = 1
				oPC.calendarInstance.popupSuperMonth.OverSelect.style.width = 1
			}
		}
	}
	oPC.keepMonth = false
}

/*** Year Pulldown ***/
function PopCalWheelYear(l)
{
	var oPC = objPopCalList[l]
	if (PopCalIsObjectVisible(oPC.calendarInstance.popupSuperYear))
	{
		if (event.wheelDelta >= 120)
		{
			for	(var i=0; i<3; i++)
			{
				PopCalDecYear(l)
			}
		}
		else if (event.wheelDelta <= -120)
		{
			for	(var i=0; i<3; i++)
			{
				PopCalIncYear(l)
			}
		}
	}
}


function PopCalIncYear(l)
{
	var oPC = objPopCalList[l]
	if ((oPC.nStartingYear+(oPC.HalfYearList*2+1)) <= oPC.yearUpTo)
	{
		var PopCal=oPC.calendarInstance
		for	(var i=0; i<(oPC.HalfYearList*2+1); i++){
			var newYear = (i+oPC.nStartingYear)+1
			var txtYear
			if (newYear==oPC.yearSelected)
			{ 
				txtYear = "&nbsp;<B>" + newYear + "</B>&nbsp;" 
			}
			else
			{
				txtYear = "&nbsp;" + newYear + "&nbsp;" 
			}
			PopCal.popupSuperYearList[i].innerHTML = txtYear
		}
		oPC.nStartingYear ++
	}
	oPC.bShow=true
}

function PopCalDecYear(l)
{
	var oPC = objPopCalList[l]
	if (oPC.nStartingYear-1 >= oPC.yearFrom)
	{
		var PopCal=oPC.calendarInstance
		for (var i=0; i<(oPC.HalfYearList*2+1); i++)
		{
			var newYear	= (i+oPC.nStartingYear)-1
			var txtYear

			if (newYear==oPC.yearSelected)
			{
				txtYear = "&nbsp;<B>"+ newYear + "</B>&nbsp;"
			}
			else
			{
				txtYear = "&nbsp;" + newYear + "&nbsp;" 
			}
			PopCal.popupSuperYearList[i].innerHTML = txtYear
		}
		oPC.nStartingYear --
	}
	oPC.bShow=true
}

function PopCalSelectYear(nYear, l)
{
	var oPC = objPopCalList[l]
	oPC.yearSelected=parseInt(nYear+oPC.nStartingYear,10)
	if ((oPC.yearSelected == oPC.yearFrom) && (oPC.monthSelected < oPC.monthFrom))
	{
		oPC.monthSelected = oPC.monthFrom
	}
	else if ((oPC.yearSelected == oPC.yearUpTo) && (oPC.monthSelected > oPC.monthUpTo))
	{
		oPC.monthSelected = oPC.monthUpTo
	}
	oPC.yearConstructed=false
	PopCalConstructCalendar(l)
	PopCalDownYear(l)
}

function PopCalConstructYear(l)
{
	var oPC = objPopCalList[l]
	PopCalDownMonth(l)

	var sHTML = ""
	var longList = true
	if (!oPC.yearConstructed)
	{
		var beginYear = oPC.yearSelected-oPC.HalfYearList
		var endYear = oPC.yearSelected+oPC.HalfYearList

		if ((oPC.yearUpTo - oPC.yearFrom + 1) <= (oPC.HalfYearList * 2 + 1))
		{
			beginYear = oPC.yearFrom
			endYear = oPC.yearUpTo
			longList = false
		}
		else if (beginYear < oPC.yearFrom)
		{
			beginYear = oPC.yearFrom
			endYear = beginYear + oPC.HalfYearList * 2
		}
		else if (endYear > oPC.yearUpTo)
		{
			endYear = oPC.yearUpTo
			beginYear = endYear - (oPC.HalfYearList * 2)
		}

		oPC.nStartingYear = beginYear

		if (longList)
		{
			sHTML += "<tr><td align='center' Class='OptionStyle' onmouseover='objPopCalList["+l+"].bShow=true;this.className=\"OptionOverStyle\"' onmouseout='objPopCalList["+l+"].bShow=false;clearInterval(objPopCalList["+l+"].intervalID1);this.className=\"OptionOutStyle\"' style='cursor:default;border-bottom:1px #a0a0a0 solid' onmousedown='clearInterval(objPopCalList["+l+"].intervalID1);objPopCalList["+l+"].intervalID1=setInterval(\"PopCalDecYear("+l+")\",10)' onmouseup='clearInterval(objPopCalList["+l+"].intervalID1)'><IMG id='popupSuperUpYear' onDrag='return(false)' SRC='"+oPC.imgDir+"up.gif' BORDER=0></td></tr>"
		}

		var j =	0
		for (var i=(beginYear); i<=(endYear); i++)
		{
			var sName =	i
			if (i==oPC.yearSelected)
			{
				sName = "<B>" + sName + "</B>"
			}

			sHTML += "<tr><td id='popupSuperYear" + j + "' Class='OptionStyle' align='center' onmouseover='objPopCalList["+l+"].bShow=true;this.className=\"OptionOverStyle\"' onmouseout='objPopCalList["+l+"].bShow=false;this.className=\"OptionOutStyle\"' style='cursor:default' onclick='objPopCalList["+l+"].bShow=false;PopCalSelectYear("+j+","+l+");event.cancelBubble=true'>&nbsp;" + sName + "&nbsp;</td></tr>"
			j ++
		}

		if (longList)
		{
			sHTML += "<tr><td align='center' Class='OptionStyle' onmouseover='objPopCalList["+l+"].bShow=true;this.className=\"OptionOverStyle\"' onmouseout='objPopCalList["+l+"].bShow=false;clearInterval(objPopCalList["+l+"].intervalID2);this.className=\"OptionOutStyle\"' style='cursor:default;border-top:1px #a0a0a0 solid' onmousedown='clearInterval(objPopCalList["+l+"].intervalID2);objPopCalList["+l+"].intervalID2=setInterval(\"PopCalIncYear("+l+")\",10)' onmouseup='clearInterval(objPopCalList["+l+"].intervalID2)'><IMG id='popupSuperDownYear' onDrag='return(false)' SRC='"+oPC.imgDir+"down.gif' BORDER=0></td></tr>"
		}

		var PopCal=oPC.calendarInstance
		PopCal.popupSuperYear.className = oPC.CssClass
		PopCal.popupSuperYear.innerHTML	= "<table width='100%' Style='border-width:1;border-style:solid;border-color:#a0a0a0;' Class='ListStyle' onmouseover='clearTimeout(objPopCalList["+l+"].timeoutID2)' onmouseout='clearTimeout(objPopCalList["+l+"].timeoutID2);' cellspacing=0>"	+ sHTML	+ "</table>"

		PopCal.popupSuperYearList = []
		for (var i=0; i<j; i++)
		{
			PopCal.popupSuperYearList[i] = document.getElementById("popupSuperYear" + i)
		}

		oPC.yearConstructed = true
	}
}
function PopCalDownYear(l) 
{
	var oPC = objPopCalList[l]
	if (oPC.calendarInstance.popupSuperYear.style.display != "none")
	{
		if (!oPC.keepYear)
		{
			clearInterval(oPC.intervalID1)
			clearTimeout(oPC.timeoutID1)
			clearInterval(oPC.intervalID2)
			clearTimeout(oPC.timeoutID2)
			PopCalYearDown = true
			oPC.calendarInstance.popupSuperYear.style.display = "none"
			if (oPC.calendarInstance.popupSuperYear.OverSelect)
			{
				oPC.calendarInstance.popupSuperYear.OverSelect.style.display = "none"
				oPC.calendarInstance.popupSuperYear.OverSelect.style.height = 1
				oPC.calendarInstance.popupSuperYear.OverSelect.style.width = 1
			}
		}
	}
	oPC.keepYear = false
}

function PopCalUpYear(l)
{
	var oPC = objPopCalList[l]
	var PopCal = oPC.calendarInstance
	PopCalConstructYear(l)
	PopCal.popupSuperYear.style.display = "inline-block"
	if (PopCal.popupSuperYear.OverSelect) PopCal.popupSuperYear.OverSelect.style.display = "inline-block"
	var lTop = parseInt(PopCal.popupSuperCalendar.style.top,10) + parseInt(document.getElementById("popupSuperSpanYear" + oPC.id).offsetTop,10) + parseInt(document.getElementById("popupSuperSpanYear" + oPC.id).offsetHeight,10) + 6
	var lLeft = parseInt(PopCal.popupSuperCalendar.style.left,10) + parseInt(document.getElementById("popupSuperSpanYear" + oPC.id).offsetLeft,10)
	lLeft += (oPC.ie)? 4:5

	PopCalSetPosition(PopCal.popupSuperYear, lTop, lLeft, null, parseInt(document.getElementById("popupSuperSpanYear" + oPC.id).offsetWidth,10))
}

function PopCalConstructCalendar(l)
{
	var oPC = objPopCalList[l]
	var PopCal=oPC.calendarInstance
	var aNumDays = Array (31,0,31,30,31,30,31,31,30,31,30,31)

	var startDate = new Date(oPC.yearSelected,oPC.monthSelected,1)
	var endDate
	var numDaysInMonth
	var notSelect
	var selectWeekends = PopCal.selectWeekend
	
	if (oPC.overWriteSelectWeekend!=null)
	{
		selectWeekends = oPC.overWriteSelectWeekend
	}

	if (oPC.monthSelected==1)
	{
		endDate = new Date(oPC.yearSelected,2,1)
		
		endDate = new Date(endDate - (86400000))

		numDaysInMonth = endDate.getDate()
	}
	else
	{
		numDaysInMonth = aNumDays[oPC.monthSelected]
	}

	var datePointer	= 0
	dayPointer = startDate.getDay() - PopCal.startAt
	
	if (dayPointer<0)
	{
		dayPointer = 6
	}

	var sHTML = "<table border=0 cellpadding=0 cellspacing=0 Class='BodyStyle'><tr>"

	if (PopCal.showWeekNumber==1)
	{
		sHTML += "<td Class='HeaderStyle' align='center'>" + oPC.weekString + "</td>"
	}

	for (var i=0; i<7; i++)
	{
		sHTML += "<td Class='HeaderStyle' align='right'>"+ PopCalChrSpc(PopCalSpcChr(oPC.dayName[i]).substr(0,3)).replace('Mir','Mi&eacute;').replace('Sba','S&aacute;b')+"</td>"
	}
	sHTML +="</tr><tr>"
	
	if (PopCal.showWeekNumber==1)
	{
		sHTML += "<td align=right Style='cursor:default'>" + PopCalWeekNbr(startDate, l) + "&nbsp;</td>"
	}

	for (var i=1; i<=dayPointer;i++ )
	{
		sHTML += "<td Style='cursor:default'>&nbsp;</td>"
	}

	for (var datePointer=1; datePointer<=numDaysInMonth; datePointer++ )
	{
		dayPointer++
		sHTML += "<td align=right class='DateStyle'>"
		var sStyle=""
		
		
		var _date = new Date(oPC.yearSelected, oPC.monthSelected, datePointer)
		
		if ((datePointer==oPC.odateSelected) && (oPC.monthSelected==oPC.omonthSelected) && (oPC.yearSelected==oPC.oyearSelected))
		{ 
			sStyle+=" SelectedDateStyle"
		}

		notSelect = false
		var _IsDate = false
		var sHint = ""

		var regexp= /\"/g
		sHint=sHint.replace(regexp,"&quot;")

		if (oPC.yearSelected == oPC.yearFrom && oPC.monthSelected == oPC.monthFrom)
		{
			if (datePointer < oPC.dateFrom)
			{
				notSelect = true
			}
		}

		if (oPC.yearSelected == oPC.yearUpTo && oPC.monthSelected == oPC.monthUpTo)
		{
			if (datePointer > oPC.dateUpTo)
			{
				notSelect = true
			}
		}
		
		if ((selectWeekends!=1) && (!notSelect))
		{
			if ((dayPointer % 7 == (PopCal.startAt * -1)+1) || (dayPointer % 7 == (PopCal.startAt * -1)+7) || (dayPointer % 7 == (PopCal.startAt * -1)))
			{
				notSelect = true
			}
		}


		if ((datePointer==oPC.dateNow)&&(oPC.monthSelected==oPC.monthNow)&&(oPC.yearSelected==oPC.yearNow))
		{
			sStyle += " CurrentDateStyle"
		}

		if (((dayPointer % 7 == (PopCal.startAt * -1)+1) || (dayPointer % 7 == (PopCal.startAt * -1)+7) || (dayPointer % 7 == (PopCal.startAt * -1))) && (PopCal.showWeekend==1))
		{
			sStyle += " WeekendStyle"
		}

		if (notSelect)
		{
			sStyle += " DisableDateStyle"
			sHTML += "<Span title=\"" + sHint + "\" Class='" + sStyle + "'>&nbsp;" + datePointer + "&nbsp;</Span>"
		}
		else
		{
			var dateMessage = "onmouseover='this.className+=\" DayOverStyle\";' onmouseout='this.className=this.getAttribute(\"CSS\");' "
			sHTML += "<span "+dateMessage+" title=\"" + sHint + "\" Class='"+sStyle+"' CSS='"+sStyle+"' onClick='objPopCalList["+l+"].dateSelected=" + datePointer + ";PopCalCloseCalendar("+l+");'>&nbsp;" + datePointer + "&nbsp;</span>"
		}
		sHTML += "</td>"
		
		if ((dayPointer+PopCal.startAt) % 7 == PopCal.startAt) { 
			sHTML += "</tr><tr>" 
			if ((PopCal.showWeekNumber==1)&&(datePointer<numDaysInMonth))
			{
				sHTML += "<td align=right Style='cursor:default'>" + (PopCalWeekNbr(new Date(oPC.yearSelected,oPC.monthSelected,datePointer+1), l)) + "&nbsp;</td>"
			}
		}
		

	}
	
	while ((dayPointer+PopCal.startAt) % 7!=PopCal.startAt)
	{
		sHTML += "<td Style='cursor:default'>&nbsp;</td>"
		++dayPointer
	}

	var intLen = oPC.monthName[oPC.monthSelected].length
	//alert(intLen)
	intLen = 10 -intLen
	//alert(intLen)
	var sEspacio = ""
	for (i=0;i<intLen;i++) sEspacio += "&nbsp;&nbsp;"
	document.getElementById("popupSuperContent" + oPC.id).innerHTML = sHTML
	document.getElementById("popupSuperSpanMonth" + oPC.id).innerHTML = "&nbsp;" + oPC.monthName[oPC.monthSelected] + sEspacio +"<IMG id='popupSuperChangeMonth" + oPC.id +  "' onDrag='return(false)' SRC='"+oPC.imgDir+"drop1.gif' WIDTH='12' HEIGHT='10' BORDER=0>"
	document.getElementById("popupSuperSpanYear" + oPC.id).innerHTML = "&nbsp;" + oPC.yearSelected + "&nbsp;<IMG id='popupSuperChangeYear" + oPC.id +  "' onDrag='return(false)' SRC='"+oPC.imgDir+"drop1.gif' WIDTH='12' HEIGHT='10' BORDER=0>"
}

function PopCalDateNow(l)
{
	var oPC = objPopCalList[l]
	return PopCalPad(oPC.yearNow, 4, "0", "L") + PopCalPad(oPC.monthNow, 2, "0", "L") + PopCalPad(oPC.dateNow, 2, "0", "L")
}

function PopCalDateSelect(l)
{
	var oPC = objPopCalList[l]
	return PopCalPad(oPC.yearSelected, 4, "0", "L") + PopCalPad(oPC.monthSelected, 2, "0", "L") + PopCalPad(oPC.dateSelected, 2, "0", "L")
}

function PopCalDateFrom(l)
{
	var oPC = objPopCalList[l]
	return PopCalPad(oPC.yearFrom, 4, "0", "L") + PopCalPad(oPC.monthFrom, 2, "0", "L") + PopCalPad(oPC.dateFrom, 2, "0", "L")
}

function PopCalDateUpTo(l)
{
	var oPC = objPopCalList[l]
	return PopCalPad(oPC.yearUpTo, 4, "0", "L") + PopCalPad(oPC.monthUpTo, 2, "0", "L") + PopCalPad(oPC.dateUpTo, 2, "0", "L")
}

function PopCalCenturyOn(dateFormat)
{
	var formatChar =  " "

	dateFormat = dateFormat.toLowerCase()
	
	var aFormat = dateFormat.split(formatChar)
	if (aFormat.length<3)
	{
		formatChar = "/"
		aFormat = dateFormat.split(formatChar)
		if (aFormat.length<3)
		{
			formatChar = "."
			aFormat = dateFormat.split(formatChar)
			if (aFormat.length<3)
			{
				formatChar = "-"
				aFormat = dateFormat.split(formatChar)
				if (aFormat.length<3)
				{
					// invalid date	format
					formatChar = ""
				}
			}
		}
	}

	if ( formatChar != "" )
	{
		for (var i=0;i<3;i++)
		{
			if (aFormat[i]=="yyyy")
			{
				return true
			}
		}
	}
	return false
}

function PopCalSetDMY(dateValue, dateFormat, l)
{
	var oPC = objPopCalList[l]
	var PopCal=oPC.calendarInstance
	oPC.oDate = null
	oPC.oMonth = null
	oPC.oYear = null

	var formatChar =  " "

	dateFormat = dateFormat.toLowerCase()
	
	var aFormat = dateFormat.split(formatChar)
	if (aFormat.length<3)
	{
		formatChar = "/"
		aFormat = dateFormat.split(formatChar)
		if (aFormat.length<3)
		{
			formatChar = "."
			aFormat = dateFormat.split(formatChar)
			if (aFormat.length<3)
			{
				formatChar = "-"
				aFormat = dateFormat.split(formatChar)
				if (aFormat.length<3)
				{
					// invalid date	format
					formatChar = ""
				}
			}
		}
	}

	var tokensChanged = 0

	if ( formatChar != "" )
	{
		// use user's date
		var aData = dateValue.split(formatChar)

		for (var i=0;i<3;i++)
		{
			if ((aFormat[i]=="d") || (aFormat[i]=="dd"))
			{
				oPC.oDate = parseInt(aData[i],10)
				tokensChanged ++
			}
			else if ((aFormat[i]=="m") || (aFormat[i]=="mm"))
			{
				if (((parseInt(aData[i],10) - 1)>=0) && ((parseInt(aData[i],10) - 1)<=11))
				{
					oPC.oMonth = parseInt(aData[i],10) - 1
					tokensChanged ++
				}
			}
			else if ((aFormat[i]=="yy") || (aFormat[i]=="yyyy"))
			{
				oPC.oYear = parseInt(aData[i],10)
				if (oPC.oYear<=99)
				{
					tokensChanged ++
					if (oPC.oYear < 100)
					{
						if (oPC.oYear < PopCal.centuryLimit)
						{
							oPC.oYear += 100
						}
						oPC.oYear += 1950
					}
				}
				else if (oPC.oYear<=9999)
				{
					tokensChanged ++
				}
			}
		}
	}
	return ((tokensChanged==3)&&!isNaN(oPC.oDate)&&!isNaN(oPC.oMonth)&&!isNaN(oPC.oYear))
}

function PopCalGetDate(dateValue, dateFormat, l)
{
	var oPC = objPopCalList[l]
	if (PopCalIsToday(dateValue))
	{
		return (new Date(oPC.today.getFullYear(),oPC.today.getMonth(),oPC.today.getDate()))
	}	
	else if (PopCalFormatDate(dateValue, dateFormat, 'yyyy-mmm-dd', l) != '')
	{
		return (new Date(oPC.oYear, oPC.oMonth, oPC.oDate))
	}
	return null
}

function PopCalChangeCurrentMonth(l)
{
	var oPC = objPopCalList[l]
	if ((PopCalDateFrom(l).substr(0,6) <= PopCalDateNow(l).substr(0,6)) && (PopCalDateNow(l).substr(0,6) <= PopCalDateUpTo(l).substr(0,6)))
	{

		oPC.monthSelected=oPC.monthNow
		oPC.yearSelected=oPC.yearNow
		oPC.yearConstructed=false
		oPC.monthConstructed=false
		PopCalConstructCalendar(l)
	}
}

function PopCalPad(s, l, c, X)
{
	var x = X
	var r = s.toString()

	if (r.length >= l) return (r.substr(0, l))
	if (c==null) c = ' '

	do
	{
		if (X=='C')
		{
			if (x=='L') x = 'R'
			else x = 'L'
		}
	
		if (x=='L') r = c + r
		else if (x=='R') r = r + c
		
	} while (r.length < l)

	return (r)
}


function PopCalSpcChr(_Word)
{
	var sTmp = _Word
	sTmp = sTmp.replace("&aacute;",unescape("%E1"))
	sTmp = sTmp.replace("&auml;",unescape("%E4"))
	sTmp = sTmp.replace("&eacute;",unescape("%E9"))
	sTmp = sTmp.replace("&ucirc;",unescape("%FB"))
	sTmp = sTmp.replace("&ccedil;",unescape("%E7"))
	return sTmp
}

function PopCalChrSpc(_Word)
{
	var sTmp = _Word
	if (unescape("%E1")!='') sTmp = sTmp.replace(unescape("%E1"),"&aacute;")
	if (unescape("%E4")!='') sTmp = sTmp.replace(unescape("%E4"),"&auml;")
	if (unescape("%E9")!='') sTmp = sTmp.replace(unescape("%E9"),"&eacute;")
	if (unescape("%FB")!='') sTmp = sTmp.replace(unescape("%FB"),"&ucirc;")
	if (unescape("%E7")!='') sTmp = sTmp.replace(unescape("%E7"),"&ccedil;")
	return sTmp
}

function PopCalIsToday(_hoy)
{
	return ((',now,today,hoy,heute,oggi,hoje,').indexOf(',' + _hoy.toLowerCase() + ',') != -1)
}


