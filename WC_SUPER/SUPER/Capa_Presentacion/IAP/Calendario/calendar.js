var MonthNames = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
var aLetraJornada = new Array("L", "M", "X", "J", "V", "S", "D");
var aLiteralJornada = new Array("Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");

var nWidth = 75;
var nHeight = 70;

var leftX;
var rightX
var topY;
var bottomY;

function Calendar() {
    var HTMLstr = "";
    HTMLstr += "<map name='Map1'>\n";
    HTMLstr += "  <area shape='rect' coords='1,1,10,10' href='javascript:nextMonth()'>\n";
    HTMLstr += "  <area shape='rect' coords='1,10,10,20' href='javascript:prevMonth()'>\n";
    HTMLstr += "</map>\n";
    HTMLstr += "<map name='Map2'>\n";
    HTMLstr += "  <area shape='rect' coords='1,1,10,10' href='javascript:nextYear()'>\n";
    HTMLstr += "  <area shape='rect' coords='1,10,10,20' href='javascript:prevYear()'>\n";
    HTMLstr += "</map>\n";

    HTMLstr += "<table id='tblCalendario' name='tblCalendario' border='0' cellspacing='0' cellpadding='0' style='background-image:url(../../../Images/imgFondoCalG.gif); background-repeat:no-repeat; padding:7px;width:650px;height:480px;table-layout:fixed;'>\n";
    HTMLstr += "<tr style='height:45px;'><td style='vertical-align:text-top;'>\n";

    HTMLstr += "<table width='630px' cellspacing='0' cellpadding='0' style='margin-top:8px;table-layout:fixed;'>\n";
    HTMLstr += "  <tr> \n";
    HTMLstr += "    <td width='230px' style='text-align:right;'><img src='../../../Images/imgFlechas2.gif' width='9px' height='20px' usemap='#Map1' border='0' hidefocus='true'>&nbsp;&nbsp;</td>\n";
    HTMLstr += "    <td width='130px' style='text-align:left;'><b><font color='#FFFFFF'><div id='main2' style='font-size:18px;'></div></font></b></td>\n";
    HTMLstr += "    <td width='50px' align='right' style='text-align:right;'><img src='../../../Images/imgFlechas2.gif' width='9px' height='20px' usemap='#Map2' border='0' hidefocus='true'>&nbsp;&nbsp;</td>\n";
    HTMLstr += "    <td width='220px' align='left' style='text-align:left;'><b><font color='#FFFFFF'><div id='main'style='font-size:18px;display:block-inline;'></div></font></b></td>\n";
    HTMLstr += "  </tr>\n";
    HTMLstr += "</table>";

    HTMLstr += "</td></tr>\n";
    HTMLstr += "<tr style='height:435px;'><td style='vertical-align:text-top;'>\n";
    HTMLstr += "<table id='tblLiterales' border=0 cols=8 width='630px' cellspacing='0' cellpadding='0' style='font-size:13px;'>\n";
    HTMLstr += "<tr height='20px'>\n";
    HTMLstr += "<td id='cldL' width='80px'>&nbsp;&nbsp;&nbsp;<b>Lunes</b></td>\n";
    HTMLstr += "<td id='cldM' width='80px'>&nbsp;&nbsp;&nbsp;<b>Martes</b></td>\n";
    HTMLstr += "<td id='cldX' width='80px'>&nbsp;<b>Miércoles</b></td>\n";
    HTMLstr += "<td id='cldJ' width='80px'>&nbsp;&nbsp;&nbsp;<b>Jueves</b></td>\n";
    HTMLstr += "<td id='cldV' width='80px'>&nbsp;&nbsp;&nbsp;<b>Viernes</b></td>\n";
    HTMLstr += "<td id='cldS' width='80px'>&nbsp;&nbsp;&nbsp;<b>Sábado</b></td>\n";
    HTMLstr += "<td id='cldD' width='80px'>&nbsp;&nbsp;<b>Domingo</b></td>\n";
    HTMLstr += "<td width='70px'></td>\n";
    HTMLstr += "</tr>\n";
    HTMLstr += "<tr>\n";
    HTMLstr += "<td colspan=8>\n";
    HTMLstr += "<div id='divCal' style='position: relative;'>";
    HTMLstr += "<div id='idSemana1' style='position: absolute; top: 20px;  left: 570px; cursor: pointer; visibility: visible;'><img id='idAcceso1' src='../../../images/icoCerradoG.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemana(0,this)' onMouseOver='ElegirCursor(this)'></div>\n";
    HTMLstr += "<div id='idSemana2' style='position: absolute; top: 90px;  left: 570px; cursor: pointer; visibility: visible;'><img id='idAcceso2' src='../../../images/icoCerradoG.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemana(1,this)' onMouseOver='ElegirCursor(this)'></div>\n";
    HTMLstr += "<div id='idSemana3' style='position: absolute; top: 160px; left: 570px; cursor: pointer; visibility: visible;'><img id='idAcceso3' src='../../../images/icoCerradoG.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemana(2,this)' onMouseOver='ElegirCursor(this)'></div>\n";
    HTMLstr += "<div id='idSemana4' style='position: absolute; top: 230px; left: 570px; cursor: pointer; visibility: visible;'><img id='idAcceso4' src='../../../images/icoCerradoG.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemana(3,this)' onMouseOver='ElegirCursor(this)'></div>\n";
    HTMLstr += "<div id='idSemana5' style='position: absolute; top: 300px; left: 570px; cursor: pointer; visibility: visible;'><img id='idAcceso5' src='../../../images/icoCerradoG.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemana(4,this)' onMouseOver='ElegirCursor(this)'></div>\n";
    HTMLstr += "<div id='idSemana6' style='position: absolute; top: 370px; left: 570px; cursor: pointer; visibility: visible;'><img id='idAcceso6' src='../../../images/icoCerradoG.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemana(5,this)' onMouseOver='ElegirCursor(this)'></div>\n";

    HTMLstr += "<div id='idPlanif1' style='position: absolute; top: 20px;  left: 605px; cursor: pointer; visibility: visible;'><img id='idAccesoPlanif1' src='../../../images/imgPlanifOFF.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemanaPlanif(0,this)'></div>\n";
    HTMLstr += "<div id='idPlanif2' style='position: absolute; top: 90px;  left: 605px; cursor: pointer; visibility: visible;'><img id='idAccesoPlanif2' src='../../../images/imgPlanifOFF.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemanaPlanif(1,this)'></div>\n";
    HTMLstr += "<div id='idPlanif3' style='position: absolute; top: 160px; left: 605px; cursor: pointer; visibility: visible;'><img id='idAccesoPlanif3' src='../../../images/imgPlanifOFF.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemanaPlanif(2,this)'></div>\n";
    HTMLstr += "<div id='idPlanif4' style='position: absolute; top: 230px; left: 605px; cursor: pointer; visibility: visible;'><img id='idAccesoPlanif4' src='../../../images/imgPlanifOFF.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemanaPlanif(3,this)'></div>\n";
    HTMLstr += "<div id='idPlanif5' style='position: absolute; top: 300px; left: 605px; cursor: pointer; visibility: visible;'><img id='idAccesoPlanif5' src='../../../images/imgPlanifOFF.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemanaPlanif(4,this)'></div>\n";
    HTMLstr += "<div id='idPlanif6' style='position: absolute; top: 370px; left: 605px; cursor: pointer; visibility: visible;'><img id='idAccesoPlanif6' src='../../../images/imgPlanifOFF.gif' style='width:32px;height:32px;box-sizing:border-box;' border='0' onClick='seleccionarSemanaPlanif(5,this)'></div>\n";

    for (var date = 1; date <= 31; date++) {
        HTMLstr += "  <div id=\"idDate" + date + "\" val=" + date + " style=\"position: absolute; visibility: hidden; height: 68px; width: 60px; padding:0px;text-align:center;\" align=center>\n";
        HTMLstr += "    <nobr style=\"font-size:35px; font-family:Jokerman, Arial; padding:0px; margin:0px; height:30px; font-weight: bold;display:inline-block;\">" + date + "</nobr><nobr style=\"position: relative; top: 3; left: 0;color: black; font-size:11px; font-family: Arial;\"><br>0 / 0</nobr>\n";
        HTMLstr += "  </div>\n";
    }

    HTMLstr += "</div>";
    HTMLstr += "</td></tr>\n";
    HTMLstr += "</table>\n";
    HTMLstr += "\n";
    HTMLstr += "</td></tr>\n";
    HTMLstr += "</table>\n";

    document.writeln(HTMLstr);
}
function ElegirCursor(objImagen) {
    var intUltBarra = $I(objImagen.id).src.lastIndexOf("/");
    var strImagen = $I(objImagen.id).src.substring((intUltBarra + 1), $I(objImagen.id).src.length);
    if (strImagen == 'icoAbiertoG.gif') {
        objImagen.style.cursor = "pointer";
        //objImagen.setAttribute("style", "cursor:pointer");
    }
    else {
        objImagen.style.cursor = "not-allowed";
        //objImagen.setAttribute("style", "cursor:not-allowed");
    }
}

function setCurrentMonth() {
    if (intMesInicio != 0) {
        setYearMonth(intAnnoInicio, intMesInicio);
    } else {
        date = new Date();
        currentyear = date.getFullYear()
        if (currentyear < 1000)
            currentyear += 1900
        setYearMonth(currentyear, date.getMonth() + 1);
    }
}

function setMonth(nMonth) {
    setYearMonth(nCurrentYear, nMonth);
}

function setYearMonth(nYear, nMonth) {
    nCurrentYear = nYear;
    nCurrentMonth = nMonth;
    //var cross_obj = document.getElementById("main");
    //var cross_obj2 = document.getElementById("main2");
    if (document.all) {
        document.getElementById("main").innerText = nCurrentYear;
        document.getElementById("main2").innerText = MonthNames[nCurrentMonth - 1];
    }
    else {
        document.getElementById("main").textContent = nCurrentYear;
        document.getElementById("main2").textContent = MonthNames[nCurrentMonth - 1];

    }
    var date = new Date(nCurrentYear, nCurrentMonth - 1, 1);
    var nWeek = 1;
    var nDate;
    var i = 0;
    var sw = 0;
    while (date.getMonth() == nCurrentMonth - 1) {
        nDate = date.getDate();
        //alert(nDate);
        nLastDate = nDate;

        var posDay = date.getDay() - 1;
        if (posDay == -1) posDay = 6;
        var posLeft = posDay * (nWidth + 5) + 5;
        var posTop = (nWeek - 1) * nHeight + 10;
        var cross_obj3 = document.getElementById("idDate" + nDate).style;

        cross_obj3.left = posLeft;
        cross_obj3.top = posTop;
        //      document.getElementById("idDate" + nDate).setAttribute("style", "left:" + posLeft);
        //      document.getElementById("idDate" + nDate).setAttribute("style", "top:" + posTop);
        document.getElementById("idDate" + nDate).setAttribute("style", "position: absolute;left:" + posLeft + "px;top:" + posTop + "px; visibility: visible; height: 68px; width: 60px;");

        //if (date.getDay() == 0 || date.getDay() == 6){
        var nDia = date.getDay();
        if (nDia == 0) nDia = 6;
        else nDia--;

        //        if (aSemLab[nDia] == 0){
        //        	cross_obj2.children[0].className  = "diaFestivo";
        //        }else{
        //        	cross_obj2.children[0].className  = "diaFuturo";   //azul de días sin datos.
        //		}
        cross_obj3.visibility = "visible";

        date = new Date(nCurrentYear, date.getMonth(), date.getDate() + 1);
        //alert("aSemanas["+ eval(nWeek-1) +"]["+ i +"] = "+ nDate);
        aSemanas[nWeek - 1][i] = nDate;
        i++;

        if (posDay == 6) {
            nWeek++;
            i = 0;
        }
    }
    for (++nDate; nDate <= 31; nDate++) {
        cross_obj3 = document.getElementById("idDate" + nDate).style;
        cross_obj3.visibility = "hidden";
        //cross_obj3.setAttribute("style", "visibility:hidden");
        document.getElementById("idDate" + nDate).setAttribute("style", "visibility: hidden;");

    }
    formateardiasAux();
    //ocultarProcesando();
}

function nextMonth() {
    for (i = 1; i < 7; i++) {
        $I("idAcceso" + i).src = "../../../images/icoCerradoG.gif";
        $I("idAccesoPlanif" + i).src = "../../../images/imgPlanifOFF.gif";
        aSemanas[i - 1].length = 0;
    }
    nCurrentMonth++;
    if (nCurrentMonth > 12) {
        nCurrentMonth -= 12;
        nextYear();
    }

    try { mostrarProcesando(); } catch (e) { }
    setTimeout("setYearMonth(" + nCurrentYear + "," + nCurrentMonth + ")", 20);
}


function prevMonth() {
    for (i = 1; i < 7; i++) {
        $I("idAcceso" + i).src = "../../../images/icoCerradoG.gif";
        $I("idAccesoPlanif" + i).src = "../../../images/imgPlanifOFF.gif";
        aSemanas[i - 1].length = 0;
    }
    nCurrentMonth--;
    if (nCurrentMonth < 1) {
        nCurrentMonth += 12;
        prevYear();
    }
    try { mostrarProcesando(); } catch (e) { }
    setTimeout("setYearMonth(" + nCurrentYear + "," + nCurrentMonth + ")", 20);
}

function prevYear() {
    for (i = 1; i < 7; i++) {
        $I("idAcceso" + i).src = "../../../images/icoCerradoG.gif";
        $I("idAccesoPlanif" + i).src = "../../../images/imgPlanifOFF.gif";
        aSemanas[i - 1].length = 0;
    }
    nCurrentYear--;
    try { mostrarProcesando(); } catch (e) { }
    setTimeout("setYearMonth(" + nCurrentYear + "," + nCurrentMonth + ")", 20);
}

function nextYear() {
    for (i = 1; i < 7; i++) {
        $I("idAcceso" + i).src = "../../../images/icoCerradoG.gif";
        $I("idAccesoPlanif" + i).src = "../../../images/imgPlanifOFF.gif";
        aSemanas[i - 1].length = 0;
    }
    nCurrentYear++;
    try { mostrarProcesando(); } catch (e) { }
    setTimeout("setYearMonth(" + nCurrentYear + "," + nCurrentMonth + ")", 20);
}

