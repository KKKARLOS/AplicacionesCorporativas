/***********************************************
* Cool DHTML tooltip script II- © Dynamic Drive DHTML code library (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit Dynamic Drive at http://www.dynamicdrive.com/ for full source code
***********************************************/

var offsetfromcursorX = 10 //Customize x offset of tooltip
var offsetfromcursorY = 10 //Customize y offset of tooltip

var offsetdivfrompointerX = 10 //Customize x offset of tooltip DIV relative to pointer image
var offsetdivfrompointerY = 14 //Customize y offset of tooltip DIV relative to pointer image. Tip: Set it to (height_of_pointer_image-1).

var nPos = location.href.indexOf("Capa_Presentacion");
var strUrlImg = location.href.substring(0, nPos) + "Images";
document.write('<img id="dhtmlpointer" src="' + strUrlImg + '/arrow2.gif">') //write out pointer image
document.write('<img id="dhtmlpointerR" src="' + strUrlImg + '/arrow3.gif">') //write out pointer image
document.write('<div id="dhtmltooltip"><div id="dhtmltooltiphead"></div><div id="dhtmltooltipbody"></div></div>') //write out tooltip DIV


var ie = document.all
var ns6 = document.getElementById && !document.all
var enabletip = false
//if (ie||ns6)

//var tipobj = document.all ? document.all["dhtmltooltip"] : document.getElementById ? document.getElementById("dhtmltooltip") : ""
//var tipobjhead = document.all ? document.all["dhtmltooltiphead"] : document.getElementById ? document.getElementById("dhtmltooltiphead") : ""
//var tipobjbody = document.all ? document.all["dhtmltooltipbody"] : document.getElementById ? document.getElementById("dhtmltooltipbody") : ""

//var pointerobj = document.all ? document.all["dhtmlpointer"] : document.getElementById ? document.getElementById("dhtmlpointer") : ""

var tipobj = ($I("dhtmltooltip") != null) ? $I("dhtmltooltip") : ""
var tipobjhead = ($I("dhtmltooltiphead") != null) ? $I("dhtmltooltiphead") : ""
var tipobjbody = ($I("dhtmltooltipbody") != null) ? $I("dhtmltooltipbody") : ""

var pointerobj = ($I("dhtmlpointer") != null) ? $I("dhtmlpointer") : ""
var pointerobjR = ($I("dhtmlpointerR") != null) ? $I("dhtmlpointerR") : ""

function ietruebody() {
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
}

/* Original */
//function ddrivetip(thetext, thewidth, thecolor) {
//    if (ns6 || ie) {
//        if (typeof thewidth != "undefined") tipobj.style.width = thewidth + "px"
//        if (typeof thecolor != "undefined" && thecolor != "") tipobj.style.backgroundColor = thecolor
//        tipobj.innerHTML = thetext
//        enabletip = true
//        return false
//    }
//}

function showTTE(sContenido, sTitulo, sImagen, thewidth) {
    if (ns6 || ie) {
        //if (typeof thewidth == "undefined") tipobj.style.width = "auto": tipobj.style.width = thewidth + "px"
        //if (typeof thecolor != "undefined" && thecolor != "") tipobj.style.backgroundColor = thecolor
        var sTituloTTE = (sTitulo != null) ? sTitulo : "Información";
        var sImagenTTE = (sImagen != null) ? sImagen : "info.gif";

        tipobj.style.width = (typeof thewidth == "undefined") ? "auto" : (thewidth + "px")
        tipobjhead.style.width = (typeof thewidth == "undefined") ? "auto" : tipobj.style.pixelWidth
        tipobjbody.style.width = (typeof thewidth == "undefined") ? "auto" : ((tipobj.style.pixelWidth - 6 < 0) ? 0 : tipobj.style.pixelWidth - 6)

        //tipobj.innerHTML = thetext
        tipobjhead.innerHTML = "<img src='" + strServer + "images/" + sImagenTTE + "' style='vertical-align:middle; margin-right:5px;' /><label style='vertical-align:middle;'>" + sTituloTTE + "</label>";
        //tipobjbody.innerHTML = Utilidades.unescape(sContenido)
        var regSL = /\n/g;
        tipobjbody.innerHTML = Utilidades.unescape(sContenido).replace(regSL, "<br>");
        enabletip = true
        return false
    }
}

function positiontip(e) {
    if (enabletip) {
        var nondefaultpos = false
        var curX = (ns6) ? e.pageX : event.clientX + ietruebody().scrollLeft;
        var curY = (ns6) ? e.pageY : event.clientY + ietruebody().scrollTop;
        //Find out how close the mouse is to the corner of the window
        var winwidth = ie && !window.opera ? ietruebody().clientWidth : window.innerWidth - 20
        var winheight = ie && !window.opera ? ietruebody().clientHeight : window.innerHeight - 20

        var rightedge = ie && !window.opera ? winwidth - event.clientX - offsetfromcursorX : winwidth - e.clientX - offsetfromcursorX
        var bottomedge = ie && !window.opera ? winheight - event.clientY - offsetfromcursorY : winheight - e.clientY - offsetfromcursorY

        var leftedge = (offsetfromcursorX < 0) ? offsetfromcursorX * (-1) : -1000

        //if the horizontal distance isn't enough to accomodate the width of the context menu
        if (rightedge < tipobj.offsetWidth) {
            //move the horizontal position of the menu to the left by it's width
            tipobj.style.left = curX - tipobj.offsetWidth + "px"
            nondefaultpos = true
            //pointerobjR.style.left = curX + tipobj.offsetWidth - offsetdivfrompointerX + "px"
            pointerobjR.style.left = curX - offsetdivfrompointerX - 20 + "px"
            pointerobjR.style.top = curY + offsetfromcursorY + "px"
        }
        else if (curX < leftedge) {
            tipobj.style.left = "5px"
        } else {
            //position the horizontal position of the menu where the mouse is positioned
            tipobj.style.left = curX + offsetfromcursorX - offsetdivfrompointerX + "px"
            pointerobj.style.left = curX + offsetfromcursorX + "px"
        }

        //same concept with the vertical position
        if (bottomedge < tipobj.offsetHeight) {
            tipobj.style.top = curY - tipobj.offsetHeight - offsetfromcursorY + "px"
            nondefaultpos = true
        }
        else {
            tipobj.style.top = curY + offsetfromcursorY + offsetdivfrompointerY + "px"
            pointerobj.style.top = curY + offsetfromcursorY + "px"
        }

        tipobjhead.style.width = (typeof thewidth == "undefined") ? "auto" : tipobj.style.pixelWidth
        tipobjbody.style.width = (typeof thewidth == "undefined") ? "auto" : ((tipobj.style.pixelWidth - 6 < 0) ? 0 : tipobj.style.pixelWidth - 6)
        tipobj.style.display = "block"
        
        if (!nondefaultpos) {
            pointerobj.style.display = "block"
            pointerobjR.style.display = "none"
        } else {
            pointerobj.style.display = "none"
            pointerobjR.style.display = "block"
        }
    }
}

function hideTTE() {
    if (ns6 || ie) {
        enabletip = false
        tipobj.style.display = "none"
        pointerobj.style.display = "none"
        pointerobjR.style.display = "none"
        tipobj.style.left = "-1000px"
        tipobj.style.backgroundColor = ''
        tipobj.style.width = ''
    }
}

document.onmousemove = positiontip
