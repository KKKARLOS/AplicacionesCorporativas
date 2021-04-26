/***********************************************
Función: DiffDiasFechas
Inputs: Fecha1 --> en formato dd/mm/aaaa "02/05/2006";
        Fecha2 --> en formato dd/mm/aaaa "17/09/2006";
Output: Integer con el número de días de diferencia.
************************************************/
var aF1_js;
var aF2_js;
function DiffDiasFechas(Fecha1, Fecha2){
    try{
        aF1_js	= (Fecha1 == "") ? "01/01/1900".split("/"):Fecha1.split("/");
        aF2_js	= (Fecha2 == "") ? "01/01/1900".split("/"):Fecha2.split("/");
        //return ((new Date(aF2_js[2],eval(aF2_js[1]-1),aF2_js[0])).getTime() - (new Date(aF1_js[2],eval(aF1_js[1]-1),aF1_js[0])).getTime()) / 86400000;
		var fDias = ((new Date(aF2_js[2],eval(aF2_js[1]-1),aF2_js[0])).getTime() - (new Date(aF1_js[2],eval(aF1_js[1]-1),aF1_js[0])).getTime()) / 86400000;
		if (fDias % 1 > 0.5)
			return parseInt(fDias, 10) + 1;
		else 
        	return parseInt(fDias, 10);
	}catch(e){
		mostrarErrorAplicacion("Error al calcular la diferencia de días entre las fechas '"+ Fecha1 +"' y '"+ Fecha2 +"'", e.message);
	}
}

Date.prototype.add = function (sInterval, iNum){
    var dTemp = this;
    if (!sInterval || iNum == 0) return dTemp;
    switch (sInterval.toLowerCase()){
        case "ms": dTemp.setMilliseconds(dTemp.getMilliseconds() + iNum); break;
        case "s":  dTemp.setSeconds(dTemp.getSeconds() + iNum); break;
        case "mi": dTemp.setMinutes(dTemp.getMinutes() + iNum); break;
        case "h":  dTemp.setHours(dTemp.getHours() + iNum); break;
        case "d":  dTemp.setDate(dTemp.getDate() + iNum); break;
        case "mo": dTemp.setMonth(dTemp.getMonth() + iNum); break;
        case "y": dTemp.setFullYear(dTemp.getFullYear() + iNum); break;
    }
    return dTemp;
}

Date.prototype.DayOfWeek = function(){
    try{
        var now = this.getDay();
        if (now == 0) now = 6;
        else now--;
        var names = new Array("L", "M", "X", "J", "V", "S", "D");
        return(names[now]);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el día de la semana", e.message);
	}
}

/*
//ejemplos
var d = new Date();
var d2 = d.add("d", 3); //+3days
var d3 = d.add("h", -3); //-3hours
alert(d2);
alert(d3);
*/
Date.prototype.ToShortDateString = function()
{
    try{
        //var dTemp = this;
        var sDia = this.getDate().toString();
        var sMes = (this.getMonth()+1).toString();
        var sAnno = this.getFullYear().toString();
        if (sDia.length==1) sDia = "0"+ sDia;
        if (sMes.length==1) sMes = "0"+ sMes;
    
        return sDia+"/"+sMes+"/"+sAnno;
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a ToShortDateString() ", e.message);
		return false
	}
}
Date.prototype.ToAnomes = function()
{
    try{
        var nMes = this.getMonth()+1;
        var nAnno = this.getFullYear();
    
        return nAnno*100+nMes;
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a anomes", e.message);
		return false
	}
}
Date.prototype.ToLongDateString = function()
{
    try{
        var nDiaSemana = this.getDay();
        if (nDiaSemana == 0) nDiaSemana = 6;
        else nDiaSemana--;
        var dias = new Array("Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
        var aMeses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
        
        return(dias[nDiaSemana]+ ", "+ this.getDate()+ " de "+ aMeses[this.getMonth()]+ " de "+ this.getFullYear().toString());
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a ToLongDateString() ", e.message);
		return false
	}
}

var aMes = new Array("Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic");
function AnoMesToMesAno(nAnoMes){
    try{
        if (nAnoMes == ""){
            alert("Dato de año/mes no válido");
            return;
        }
        return nAnoMes.toString().substring(4,6)+"-"+nAnoMes.toString().substring(0,4);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el annomes a mes-año", e.message);
    }
}

function AnoMesToMesAnoDesc(nAnoMes){
    try{
        if (nAnoMes == ""){
            alert("Dato de año/mes no válido");
            return;
        }
        return aMes[parseInt(nAnoMes.toString().substring(4,6), 10)-1]+" "+nAnoMes.toString().substring(0,4);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el annomes a mes-año desc", e.message);
    }
}

var aMeses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
function AnoMesToMesAnoDescLong(nAnoMes){
    try{
        if (nAnoMes == ""){
            alert("Dato de año/mes no válido");
            return;
        }
        return aMeses[parseInt(nAnoMes.toString().substring(4,6), 10)-1]+" "+nAnoMes.toString().substring(0,4);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el annomes a mes-año desc", e.message);
    }
}

function DescToAnoMes(sDesc){
    try{
        switch (sDesc.substring(0, 3)){
            case "Ene": return sDesc.substring(sDesc.length-4, sDesc.length) + "01"; break;
            case "Feb": return sDesc.substring(sDesc.length-4, sDesc.length) + "02"; break;
            case "Mar": return sDesc.substring(sDesc.length-4, sDesc.length) + "03"; break;
            case "Abr": return sDesc.substring(sDesc.length-4, sDesc.length) + "04"; break;
            case "May": return sDesc.substring(sDesc.length-4, sDesc.length) + "05"; break;
            case "Jun": return sDesc.substring(sDesc.length-4, sDesc.length) + "06"; break;
            case "Jul": return sDesc.substring(sDesc.length-4, sDesc.length) + "07"; break;
            case "Ago": return sDesc.substring(sDesc.length-4, sDesc.length) + "08"; break;
            case "Sep": return sDesc.substring(sDesc.length-4, sDesc.length) + "09"; break;
            case "Oct": return sDesc.substring(sDesc.length-4, sDesc.length) + "10"; break;
            case "Nov": return sDesc.substring(sDesc.length-4, sDesc.length) + "11"; break;
            case "Dic": return sDesc.substring(sDesc.length-4, sDesc.length) + "12"; break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el mes-año desc a annomes.", e.message);
    }
}

function DescLongToAnoMes(sDesc){
    try{
        var iPos = sDesc.indexOf(" ");
        switch (sDesc.substring(0, iPos)){
            case "Enero": return sDesc.substring(sDesc.length-4, sDesc.length) + "01"; break;
            case "Febrero": return sDesc.substring(sDesc.length-4, sDesc.length) + "02"; break;
            case "Marzo": return sDesc.substring(sDesc.length-4, sDesc.length) + "03"; break;
            case "Abril": return sDesc.substring(sDesc.length-4, sDesc.length) + "04"; break;
            case "Mayo": return sDesc.substring(sDesc.length-4, sDesc.length) + "05"; break;
            case "Junio": return sDesc.substring(sDesc.length-4, sDesc.length) + "06"; break;
            case "Julio": return sDesc.substring(sDesc.length-4, sDesc.length) + "07"; break;
            case "Agosto": return sDesc.substring(sDesc.length-4, sDesc.length) + "08"; break;
            case "Septiembre": return sDesc.substring(sDesc.length-4, sDesc.length) + "09"; break;
            case "Octubre": return sDesc.substring(sDesc.length-4, sDesc.length) + "10"; break;
            case "Noviembre": return sDesc.substring(sDesc.length-4, sDesc.length) + "11"; break;
            case "Diciembre": return sDesc.substring(sDesc.length-4, sDesc.length) + "12"; break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el mes-año desc a annomes.", e.message);
    }
}

function fechaAcadena(oFecha){
    try{
	    var strDia = oFecha.getDate().toString();
	    if (strDia.length == 1) strDia = "0" + strDia;
	    var strMes = eval(oFecha.getMonth()+1).toString();
	    if (strMes.length == 1) strMes = "0" + strMes;
	    var strAnno= oFecha.getYear().toString();
	    if (strAnno.length == 2) strAnno = "20" + strAnno;
                
        return strDia+"/"+strMes+"/"+strAnno;
	}catch(e){
		mostrarErrorAplicacion("Error al convertir una fecha a cadena", e.message);
	}
}

function cadenaAfecha(sCadena){
    try{
        var aFecha = sCadena.split("/");
        return new Date(aFecha[2],eval(aFecha[1]-1),aFecha[0]);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir una cadena a fecha", e.message);
	}
}
/***********************************************
Función: fechasCongruentes
Inputs: Fecha1 --> en formato dd/mm/aaaa "02/05/2006";
        Fecha2 --> en formato dd/mm/aaaa "17/09/2006";
Output: Devuelve false si solo hay Fecha2
        o Fecha1 > Fecha2
************************************************/
function fechasCongruentes(Fecha1, Fecha2){
    try{
        if (Fecha1 == "" && Fecha2 != "") return false;
        if (Fecha1 != "" && Fecha2 != ""){
            var nDias = DiffDiasFechas(Fecha1, Fecha2);
            if (nDias < 0) return false;
        }            
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar la congruencia entre las fechas '"+ Fecha1 +"' y '"+ Fecha2 +"'", e.message);
	}
}

function AnnomesAFecha(nAnnoMes)
{
    if (ValidarAnnomes(nAnnoMes))
        return new Date(nAnnoMes.toString().substring(0, 4), eval(nAnnoMes.toString().substring(4, 6)-1) ,1);
    else
        return new Date(1900,0,1);
}

function FechaAAnnomes(dFecha)
{
    return (dFecha.getFullYear() * 100 + dFecha.getMonth() +1);
}

function ValidarAnnomes(nAnnoMes)
{
    if (nAnnoMes.toString().length != 6){
        mostrarErrorAplicacion("La longitud del AnnoMes no es de seis dígitos", "");
        return false;
    }
    if (nAnnoMes % 100 < 1 || nAnnoMes % 100 > 12){
        mostrarErrorAplicacion("El mes no es coherente. Menor de 1 o mayor de 12.", "");
        return false;
    }
    if (nAnnoMes / 100 < 1900 || nAnnoMes / 100 > 2078){
        mostrarErrorAplicacion("El año no es coherente. Menor de 1900 o mayor de 2078.", "");
        return false;
    }

    return true;
}

function AddAnnomes(nAnnoMes, nMeses)
{
    return FechaAAnnomes(AnnomesAFecha(nAnnoMes).add("mo", nMeses));
}
