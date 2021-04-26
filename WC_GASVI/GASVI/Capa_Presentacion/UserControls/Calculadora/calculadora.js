//var timer = null;
var display = getCookie("zf_display") ? getCookie("zf_display") : 0;
var dot = getCookie("zf_dot") ? getCookie("zf_dot") : 0;
var bracket = getCookie("zf_bracket") ? getCookie("zf_bracket") : 0;
var mem = getCookie("zf_mem") ? getCookie("zf_mem") : 0;
var rate = getCookie("zf_rate") ? getCookie("zf_rate") : 1;

var oIFrameCal = window.top.document.getElementById("iFrameCalculadora");
var bCalculo = false;
var bEvaluacionOK = true;

function init()
{
//	if (document.calculator.digiwatch || document.calculator.datewatch) start();
	showDisplay();
//	document.onunload = stopcalc;
	document.onkeyup = keyboard;
}

function showDisplay()
{
	document.calculator.expr.value = display;
	var lcdSymbols = (mem != 0) ? 'M' : '&nbsp;';
	document.getElementById('lcd-symbols').innerHTML = lcdSymbols;
}

function keyboard(evt)
{
    if (!evt) {
		if (window.event) evt = window.event;
		else return;
	}
	if (typeof(evt.keyCode) == 'number') {
		key = evt.keyCode; // DOM
	}
	else if (typeof(evt.which) == 'number') {
		key = evt.which; // NS4
	}
	else if (typeof(evt.charCode) == 'number') {
		key = evt.charCode; // NS 6+, Mozilla 0.9+
	}
	else return;
	
    //if (key != 13) bCalculo = false;
    
	if ((key < 106) && (key > 95)) enter(key - 96);
    if ((key < 58) && (key > 47)) enter(key - 48);
    if ((key == 8) || (key == 46) || (key == 67)) clearDisplay();
    if (key == 13) calc();        // "Enter"
    if ((key == 110) || (key == 188) || (key == 190)) enter(".");
    if ((key == 107) || (key == 187)) enter("+");
    if ((key == 109) || (key == 189)) enter("-");
    if ((key == 106) || (key == 191)) enter("*");
    if (key == 111) enter("/");   // "/" 
    if (key == 77) pop_mem();     // "M" 
    if (key == 83) push_mem();    // "S" 
    if (key == 82) set_rate();    // "R" 
    if (key == 84) to_rate();     // "T" 
    if (key == 89) from_rate();   // "Y" 
    
}

function setCookie(name, value)
{
	var curCookie = name + "=" + value + "; expires=" + "Wednesday, 09-Nov-50 23:12:40 GMT";
	document.cookie = curCookie;
}

function getCookie(name)
{
	var dc = document.cookie;
	var prefix = name + "=";
	var begin = dc.indexOf("; " + prefix);
	if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else begin += 2;
	var end = document.cookie.indexOf(";", begin);
	if (end == -1) end = dc.length;
    return unescape(dc.substring(begin + prefix.length, end));
}

function error(errorstring)
{
	alert("Error:\n" + errorstring);
}

function enter(string)
{
    if("1234567890".indexOf(string)>-1 && bCalculo){
        clearDisplay();
    }
    bCalculo = false;
    
	if (document.calculator.expr.value == '0') {
		if (string == "(") bracket ++;
		if (string == ".") dot ++;
		else dot = 0;
		if ((string == ")") || (string == "e")) {
			error("Operación incorrecta");
			string = "0";
		}
		document.calculator.expr.value = string;
		if (string == "*" || string == "/" || string == ".") {
			document.calculator.expr.value = "0" + string;
		}
	}
	else {
		disp = new String(document.calculator.expr.value);
		last = disp.substring(disp.length - 1, disp.length);
		if (last == "(") {
		    if (string == '.') dot ++;
			if (string == "*" || string == "/" || string == ")" || string == "e") {
				error("Operación incorrecta");
				string = "";
			}
			if (string == "(") bracket ++;
		}
		if (last == ")") {
			if (string == '.') dot ++;
			if (string == "*" || string == "/" || string == "-" || string == "+") {
				print(string);
				string = "";
			}
			else {
				if (((string != "") && (bracket < 0)) || (string == ".")) {
					error("Operación incorrecta");
					string = "";
				}
			}
			if (string == ")") {
				bracket --;
				if (bracket < 0) {
					calc();
					string = '';
				}
			}
		}
		if (last=="e") {
			if (string == '.') {
				error("Operación incorrecta");
				string = "";
				dot = 0;
			}
			if (string == "*" || string == "e" || string == "/" || string == "(" || string == ")") {
				error("Operación incorrecta");
				dot = 0;
				string = "";
			}
		}
		if (last == ".") {
			if (string == '.') {
				error("Operación incorrecta");
				string = "";
				dot = 0;
			}
			if (string == "*" || string == "." || string == "e" || string == "/" ||
				string == "-" || string == "+" || string == "(" || string == ")") {
				error("Operación incorrecta");
				string = "";
			}
		}
		if (last == "+" || last == "-" || last == "*" || last == "/") {
			if (string == '.') {
				dot ++;
				if (dot > 1) {
					error("Operación incorrecta");
					string="";
					dot = 0;
				}
			}
			if (string == "e" || string == ")" || string == "*" || string == "/" ||
				string == "-" || string == "+") {
				error("Operación incorrecta");
				string = "";
			}
			if (string == "(") {
				bracket ++;
				print(string);
				string = "";
			}
		}
		if (last=="0" || last=="1" || last=="2" || last=="3" || last=="4" ||
			last=="5" || last=="6" || last=="7" || last=="8" || last=="9") {
			if (string == '.') {
				dot ++;
				if (dot > 1) {
					error("Operación incorrecta");
					string = "";
				}
			}
			if (string == "*" || string=="e" || string=="/" || string=="-" || string=="+" || string==")") dot=0;
			if (string == "(") {
				error("Operación incorrecta");
				string = "";
				dot = 0;
			}
			if (string == ")") {
				bracket --;
				if (bracket < 0) {
					calc();
					dot = 0;
					string = "";
				}
			}
		}
		print(string);
	}
}

function print(string)
{
	document.calculator.expr.value = document.calculator.expr.value + string;
}

function evaluate()
{
	try {
	    bEvaluacionOK = true;
		var val = eval(document.calculator.expr.value);
	}
	catch(e) {
		error("Operación incorrecta");
		val = document.calculator.expr.value;
		bEvaluacionOK = false;
	}
	return val;
}

function calc()
{
	document.calculator.expr.value = evaluate();
	bracket = 0;
	dot = 0;
	bCalculo = true;
}

function clearDisplay()
{
	document.calculator.expr.value = "0";
	bracket = 0;
	dot = 0;
}

function set_rate()
{
	rate = evaluate();
	document.calculator.expr.value = rate;
	setCookie("zf_rate", rate);
}

function from_rate()
{
	document.calculator.expr.value = rate * evaluate();
}

function to_rate()
{
	document.calculator.expr.value = evaluate() / rate;
}

function push_mem()
{
	document.calculator.expr.value = evaluate();
	mem = evaluate();
	if (bEvaluacionOK){
	    setCookie("zf_mem", mem);
	    document.getElementById('lcd-symbols').innerHTML = 'M';
	}
}

function clear_mem()
{
	mem = '0';
	setCookie("zf_mem", mem);
	document.getElementById('lcd-symbols').innerHTML = '&nbsp;';
}

function pop_mem()
{
	if (mem != 0) {
		enter(mem)
	}
}

function plus_mem()
{
	document.calculator.expr.value = evaluate();
	mem = evaluate() + mem;
	if (bEvaluacionOK){
	    setCookie("zf_mem", mem);
	    document.getElementById('lcd-symbols').innerHTML = 'M';
	}
}

function minus_mem()
{
	document.calculator.expr.value = evaluate();
	mem = mem - evaluate();
	if (bEvaluacionOK){
	    setCookie("zf_mem", mem);
	    document.getElementById('lcd-symbols').innerHTML = 'M';
	}
}

function power(y)
{
	document.calculator.expr.value = eval(Math.pow(evaluate(), y));
}

function power_x(y)
{
	document.calculator.expr.value = eval(Math.pow(y, evaluate()));
}

function power_m()
{
	document.calculator.expr.value = evaluate();
	document.calculator.expr.value = eval(Math.pow(evaluate(), eval(mem)));
}

function fac()
{
	x = evaluate();
	if ((x < 0) || ((x - Math.floor(x)) != 0)) {
        document.calculator.expr.value = 'Error'
	}
    else {
		if ((x == 0) || (x == 1)) {
			y = '1';
			document.calculator.expr.value = eval(y);
		}
		else {
			y = '1';
			for (i = 1; i <= x; i ++) {
				y = y * i;
			}
            document.calculator.expr.value = eval(y);
		}
	}
}

function sin()
{
	document.calculator.expr.value = eval(Math.sin(evaluate()));
}

function cos()
{
	document.calculator.expr.value = eval(Math.cos(evaluate()));
}

function tan()
{
	document.calculator.expr.value = eval(Math.tan(evaluate()));
}

function invsin()
{
	document.calculator.expr.value = 
		eval(Math.atan2(evaluate(), Math.sqrt(1 - Math.pow(evaluate(), 2))));
}

function invcos()
{
	document.calculator.expr.value =
		eval(Math.atan2(Math.sqrt(1 - Math.pow(evaluate(), 2)), evaluate()));
}

function invtan()
{
	x = evaluate();
	document.calculator.expr.value =
		eval(Math.atan2(x / Math.sqrt(1 + Math.pow(x, 2)), 1 / Math.sqrt(1 + Math.pow(x, 2))));
}

function asin()
{
	document.calculator.expr.value = eval(Math.asin(evaluate()));
}

function acos()
{
	document.calculator.expr.value = eval(Math.acos(evaluate()));
}

function atan()
{
	document.calculator.expr.value = eval(Math.atan(evaluate()));
}

function ln()
{
	document.calculator.expr.value = eval(Math.log(evaluate()));
}

function log()
{
	document.calculator.expr.value = eval(Math.log(evaluate()) / Math.LN10);
}

function log()
{
	document.calculator.expr.value = eval(Math.log(evaluate()) / Math.LN10);
}

function inv()
{
	calc();
	document.calculator.expr.value = '1/' + document.calculator.expr.value;
	document.calculator.expr.value = evaluate();
}

function neg()
{
	calc();
	document.calculator.expr.value = -evaluate();
}

//function stopcalc()
//{
//	setCookie("zf_display", document.calculator.expr.value);
//	clearTimeout(timer);
//}

//function start()
//{
//	var time = new Date();
//	var date = time.getDate();
//	var month = time.getMonth() + 1;
//	var year = time.getYear() % 100;
//	var hours = time.getHours();
//	var minutes = time.getMinutes();
//	var seconds = time.getSeconds();
//	year += (year < 38) ? 2000 : 1900;
//	year = year - ((year > 100) ? ((Math.floor(year/100))*100) : 0);
//	year = ((year < 10) ? "0" : "") + year;
//	month = ((month < 10) ? "0" : "") + month;
//	date = ((date < 10) ? "0" : "") + date;
//	hours = ((hours < 10) ? "0" : "") + hours;
//	minutes = ((minutes < 10) ? "0" : "") + minutes;
//	seconds = ((seconds < 10) ? "0" : "") + seconds;
//	var clock = hours + ":" + minutes + ":" + seconds;
//	var calendar = year + "-" + month + "-" + date;
//	if (document.calculator.digiwatch) document.calculator.digiwatch.value = clock;
//	if (document.calculator.datewatch) document.calculator.datewatch.value = calendar;
//	var timer = setTimeout("start()", 1000);
//}

function addLoadEvent(func)
{
	var oldonload = window.onload;
	if (typeof window.onload != 'function') {
		window.onload = func;
	}
	else {
		window.onload = function() {
			oldonload();
			func();
		}
	}
}

addLoadEvent(init); // Attach the setup function without interfering with other scripts

function llevar()
{
    try{
        var oDestino = window.top.oDestinoCalculadora;
        if (typeof(oDestino) != "undefined" && oDestino != ""){
            calc();
            window.top.document.getElementById(window.top.oDestinoCalculadora).value = document.calculator.expr.value.replace(".",",").ToString("N");
            window.top.document.getElementById(window.top.oDestinoCalculadora).focus();
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al llevar el valor de la calculadora", e.message);
    }
}
function show_calc()
{
    try{
        window.top.document.getElementById("iFrameCalculadora").style.visibility = "visible";
    }catch(e){
	    mostrarErrorAplicacion("Error al mostrar la calculadora", e.message);
    }
}
function hide_calc()
{
    try{
        window.top.document.getElementById("iFrameCalculadora").style.visibility = "hidden";
        window.top.oDestinoCalculadora = "";
        clear_mem();
    }catch(e){
	    mostrarErrorAplicacion("Error al ocultar la calculadora", e.message);
    }
}
function clearDisplay()
{
	document.calculator.expr.value = "0";
	bracket = 0;
	dot = 0;
}
function teclaC()
{
    try{
		disp = new String(document.calculator.expr.value);
		last = disp.substring(disp.length - 1, disp.length);
        if (last == "*" || last == "/" || last == "-" || last == "+"){
            document.calculator.expr.value = disp.substring(0, disp.length - 1);
        }else{
            var sw = 0;
            for (var i=disp.length; i>=0; i--){
                var sAux = disp.charAt(i);
                if (sAux == "*" || sAux == "/" || sAux == "-" || sAux == "+"){
                    sw = 1;
                    document.calculator.expr.value = disp.substring(0, i+1);
                    break;
                }
            }
            if (sw == 0)
                clearDisplay();
        }

    }catch(e){
	    mostrarErrorAplicacion("Error al pulsar la tecla C", e.message);
    }
}

	
	var popupCalculadoramousemoveBack,popupCalculadoramouseupBack;
	var popupCalculadoraofsX,popupCalculadoraofsY;
	var popupCalculadoratmrHide=-1;

	function popupCalculadora_DragDrop(e)
	{
		popupCalculadoramousemoveBack=document.body.onmousemove;
		popupCalculadoramouseupBack=document.body.onmouseup;
		popupCalculadoraofsX=event.clientX;
		popupCalculadoraofsY=event.clientY;

		document.body.onmousemove=popupCalculadoraespopup_DragDropMove;
		document.body.onmouseup=popupCalculadoraespopup_DragDropStop;
		if (popupCalculadoratmrHide!=-1) clearInterval(popupCalculadoratmrHide);
	}

	function popupCalculadoraespopup_DragDropMove(e)
	{
		//el=window.top.document.getElementById("iFrameCalculadora");
		//el=oIFrameCal;

		if (e==null&&event!=null)
		{
            if (typeof(window.dialogArguments)=="undefined"){ //Página maestra
		        oIFrameCal.style.left = oIFrameCal.offsetLeft + event.clientX - popupCalculadoraofsX;
                oIFrameCal.style.top = Math.max(122, oIFrameCal.offsetTop + event.clientY - popupCalculadoraofsY);
			    //oIFrameCal.style.top = oIFrameCal.offsetTop + event.clientY - popupCalculadoraofsY;
            }else{ //Página modal
			    oIFrameCal.style.left = oIFrameCal.offsetLeft + event.clientX - popupCalculadoraofsX;
			    oIFrameCal.style.top = oIFrameCal.offsetTop + event.clientY - popupCalculadoraofsY;
            }
			event.cancelBubble=true;
		}
		else
		{
			el.style.left=(e.pageX*1-popupCalculadoraofsX)+'px';
			el.style.top=(e.pageY*1-popupCalculadoraofsY)+'px';
			e.cancelBubble=true;
		}

		if ((event.button&1)==0) popupCalculadoraespopup_DragDropStop();
	}

	function popupCalculadoraespopup_DragDropStop()
	{
		document.body.onmousemove=popupCalculadoramousemoveBack;
		document.body.onmouseup=popupCalculadoramouseupBack;
	}	