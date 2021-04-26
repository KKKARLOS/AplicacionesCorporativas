
var oTipoTime = null;
var js_Grabar_Acuerdo = new Array();
var js_Grabar_Prof = new Array();
var js_Grabar_Proy = new Array();
var js_Grabar_CR = new Array();
var bTodos = false;
var nIndiceFilaActivo = -1;
var iFilaAcuerdo = -1;
var bRegresar = false;

function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Acuerdos";
        desActivarGrabar();
        ocultarProcesando();
        actualizarLupas("tblTitulo", "tblAcuerdos");
	    bCambios = false;	    
	    actualizarDatos(1);
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }
    else{
        switch (aResul[0]){
            case "mostrarDatos":
                vaciarTablas();
                $I("divCatProf").children[0].innerHTML = aResul[2];
                $I("divCatProy").children[0].innerHTML = aResul[3];
                $I("divCatCR").children[0].innerHTML = aResul[4];
                actualizarDatos(2);
                scrollTablaProf();
                scrollTablaProy();
                break;
            case "mostrarTodos":
                vaciarTablas();
                $I("divCatAcuerdos").children[0].innerHTML = aResul[2];
                actualizarDatos(1);
                volcarDatos(null);
                bTodos = true;
                break;

            case "grabar":
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta.", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } else {
                    var aFilaAc = FilasDe("tblAcuerdos");
                    var aFilaPf = FilasDe("tblProf");
                    var aFilaPy = FilasDe("tblProy");
                    var aFilaCR = FilasDe("tblCR");
                    var oFilaSelect = iFila;

                    var aAI = aResul[2].split("#sFin#"); //Cadena de id de los acuerdos viejos e insertados
                    var aPI = aResul[3].split("#sCad#"); //Cadena de id de los profesionales insertados
                    var aYI = aResul[4].split("#sCad#"); //Cadena de id de los proyectos insertados
                    var aCI = aResul[5].split("#sCad#"); //Cadena de id de los nodos/CR insertados

                    aPI.reverse();
                    aYI.reverse();
                    aCI.reverse();
                    var nIndiceEI = 0;

                    for (var i = js_Grabar_Acuerdo.length - 1; i >= 0; i--) {
                        for (var j = aAI.length - 1; j >= 0; j--) {
                            var aAIElem = aAI[j].split("#sCad#");
                            if (js_Grabar_Acuerdo[i].idac == aAIElem[0]) {
                                js_Grabar_Acuerdo[i].idac = aAIElem[1];
                                break;
                            }
                        }
                    }
                    for (var i = aFilaAc.length - 1; i >= 0; i--) { //Actualiza la tabla de acuerdos
                        switch (aFilaAc[i].getAttribute("bd")) {
                            case "D":
                                tblAcuerdos.deleteRow(i);
                                break;
                            case "I":
                                for (var j = aAI.length - 1; j >= 0; j--) {
                                    var aAIElem = aAI[j].split("#sCad#");
                                    if (aFilaAc[i].id == aAIElem[0]) {
                                        if (aFilaAc[i].id == iFilaAcuerdo) iFilaAcuerdo = aAIElem[1];
                                        aFilaAc[i].id = aAIElem[1];
                                        aFilaAc[i].children[1].setAttribute("style", "text-align:right");
                                        aFilaAc[i].children[1].innerText = aAIElem[1];
                                        break;
                                    }
                                }
                                for (var j = 0, nCount = js_Grabar_Prof.length; j < nCount; j++) {
                                    if (js_Grabar_Prof[j].idac == aAIElem[0]) {
                                        js_Grabar_Prof[j].idac = aAIElem[1];
                                    }
                                }
                                for (var j = 0, nCount = js_Grabar_Proy.length; j < nCount; j++) {
                                    if (js_Grabar_Proy[j].idac == aAIElem[0]) {
                                        js_Grabar_Proy[j].idac = aAIElem[1];
                                    }
                                }
                                for (var j = 0, nCount = js_Grabar_CR.length; j < nCount; j++) {
                                    if (js_Grabar_CR[j].idac == aAIElem[0]) {
                                        js_Grabar_CR[j].idac = aAIElem[1];
                                    }
                                }
                                //nIndiceEI++;
                                mfa(aFilaAc[i], "N");
                                break;
                            case "U":
                                mfa(aFilaAc[i], "N");
                                break;
                        }
                    }
                    ot('tblAcuerdos', 2, 0, '');
                    nIndiceEI = 0;
                    if (aFilaPf != null && aFilaPf.length > 0) {
                        for (var i = aFilaPf.length - 1; i >= 0; i--) { //Actualiza la tabla de profesionales
                            switch (aFilaPf[i].getAttribute("bd")) {
                                case "D":
                                    tblProf.deleteRow(i);
                                    break;
                                case "I":
                                    for (var j = 0, nCount = js_Grabar_Prof.length; j < nCount; j++) {
                                        if (js_Grabar_Prof[j].idfi == aFilaPf[i].id) {
                                            js_Grabar_Prof[j].idfi = aPI[nIndiceEI];
                                        }
                                    }
                                    aFilaPf[i].id = aPI[nIndiceEI];
                                    nIndiceEI++;
                                    mfa(aFilaPf[i], "N");
                                    break;
                            }
                        }
                        ot('tblProf', 2, 0, '');
                    }
                    nIndiceEI = 0;
                    if (aFilaPy != null && aFilaPy.length > 0) {
                        for (var i = aFilaPy.length - 1; i >= 0; i--) { //Actualiza la tabla de proyecto
                            switch (aFilaPy[i].getAttribute("bd")) {
                                case "D":
                                    tblProy.deleteRow(i);
                                    break;
                                case "I":
                                    for (var j = 0, nCount = js_Grabar_Proy.length; j < nCount; j++) {
                                        if (js_Grabar_Proy[j].idpr == aFilaPy[i].id) {
                                            js_Grabar_Proy[j].idpr = aYI[nIndiceEI];
                                        }
                                    }
                                    aFilaPy[i].id = aYI[nIndiceEI];
                                    nIndiceEI++;
                                    mfa(aFilaPy[i], "N");
                                    break;
                            }
                        }
                        ot('tblProy', 2, 0, '');
                    }
                    nIndiceEI = 0;
                    if (aFilaCR != null && aFilaCR.length > 0) {
                        for (var i = aFilaCR.length - 1; i >= 0; i--) { //Actualiza la tabla de nodos/cr
                            switch (aFilaCR[i].getAttribute("bd")) {
                                case "D":
                                    tblCR.deleteRow(i);
                                    break;
                                case "I":
                                    for (var j = 0, nCount = js_Grabar_CR.length; j < nCount; j++) {
                                        if (js_Grabar_CR[j].idcr == aFilaCR[i].id) {
                                            js_Grabar_CR[j].idcr = aCI[nIndiceEI];
                                        }
                                    }
                                    aFilaCR[i].id = aCI[nIndiceEI];
                                    nIndiceEI++;
                                    mfa(aFilaCR[i], "N");
                                    break;
                            }
                        }
                        ot('tblCR', 1, 0, '');
                    }
                    //desActivarGrabar();
                    //Inicializamos los arrays de grabar eliminado las posiciones que
                    //tienen como acción "D" y las demás posiciones las actualiza con "N"

                    if (js_Grabar_Acuerdo.length > 0) {
                        for (var j = 0, nCountLoop = js_Grabar_Acuerdo.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar_Acuerdo 
                            if (js_Grabar_Acuerdo[j].accion == "D") {
                                js_Grabar_Acuerdo.splice(j, 1);
                                nCountLoop--;
                                j--;
                            }
                            else js_Grabar_Acuerdo[j].accion = "N";
                        }
                    }
                    if (js_Grabar_Prof.length > 0) {
                        for (var j = 0, nCountLoop = js_Grabar_Prof.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar_Prof
                            if (js_Grabar_Prof[j].accion == "D") {
                                js_Grabar_Prof.splice(j, 1);
                                nCountLoop--;
                                j--;
                            }
                            else js_Grabar_Prof[j].accion = "N";
                        }
                    }
                    if (js_Grabar_Proy.length > 0) {
                        for (var j = 0, nCountLoop = js_Grabar_Proy.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar_Proy
                            if (js_Grabar_Proy[j].accion == "D") {
                                js_Grabar_Proy.splice(j, 1);
                                nCountLoop--;
                                j--;
                            }
                            else js_Grabar_Proy[j].accion = "N";
                        }
                    }
                    if (js_Grabar_CR.length > 0) {
                        for (var j = 0, nCountLoop = js_Grabar_CR.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar_CR
                            if (js_Grabar_CR[j].accion == "D") {
                                js_Grabar_CR.splice(j, 1);
                                nCountLoop--;
                                j--;
                            }
                            else js_Grabar_CR[j].accion = "N";
                        }
                    }


                    bCambios = false;
                    if ($I("tblAcuerdos").rows.length > oFilaSelect) setTimeout("hacerClick()", 50);
                    //mmoff("Grabación correcta", 200);
                }
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function hacerClick(){
    var aFilas = FilasDe("tblAcuerdos");
    var bExiste = false;
    for(var i=0, nCount=aFilas.length; i<nCount; i++){
        if(aFilas[i].id == iFilaAcuerdo){
            if (ie) aFilas[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFilas[i].dispatchEvent(clickEvent);
            }
            bExiste = true;
            break;
        }
    }
    if(!bExiste){
        vaciarTablas();
        $I("divContenedorProf").style.visibility = "hidden";
        $I("divAcuerdoInf").style.visibility = "hidden";

        $I("imgExcel2_exp").style.visibility = "hidden";
        $I("imgExcel3_exp").style.visibility = "hidden";
        $I("imgExcel4_exp").style.visibility = "hidden";
    }
}

function visualizarTablas(oFila){
    try{
        nIndiceFilaActivo = oFila.rowIndex;
        var existe = false;        
        var nCountProf = js_Grabar_Prof.length;      
        for(var i=0; i<nCountProf; i++){//Volcar datos en la tabla Profesionales
            if(js_Grabar_Prof[i].idac == oFila.id){
                existe = true
                break;
            }
        }
        if(!existe){
            var nCountProy = js_Grabar_Proy.length;
            for(var i=0; i<nCountProy; i++){//Volcar datos en la tabla proyectos
                if(js_Grabar_Proy[i].idac == oFila.id){
                    existe = true
                    break;
                }
            }
        }
        if(!existe){
            var nCountCR = js_Grabar_CR.length;
            for(var i=0; i<nCountCR; i++){//Volcar datos en la tabla CR
                if(js_Grabar_CR[i].idac == oFila.id){
                    existe = true
                    break;
                }
            }
        }

        if (oFila.getAttribute("bd") != "D"){
            $I("divContenedorProf").style.visibility = "visible";
            $I("divAcuerdoInf").style.visibility = "visible";

            $I("imgExcel2_exp").style.visibility = "visible";
            $I("imgExcel3_exp").style.visibility = "visible";
            $I("imgExcel4_exp").style.visibility = "visible"; 
            if (existe) {
                vaciarTablas();
                volcarDatos(oFila.id);
            }
            else{
                var js_args = "mostrarDatos@#@";
                js_args += oFila.id;
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
            return;
        }
        else{
            $I("divContenedorProf").style.visibility = "hidden";
            $I("divAcuerdoInf").style.visibility = "hidden";

            $I("imgExcel2_exp").style.visibility = "hidden";
            $I("imgExcel3_exp").style.visibility = "hidden";
            $I("imgExcel4_exp").style.visibility = "hidden";         }
    }
    catch(e){
        mostrarErrorAplicacion("Error al mostrar los datos de los acuerdos", e.message);
    }
}

function vaciarTablas(){
    if($I("tblProf") != null) BorrarFilasDe("tblProf");
    if($I("tblCR") != null) BorrarFilasDe("tblCR");
    if($I("tblProy") != null) BorrarFilasDe("tblProy");
} 

function volcarDatos(idac){
    if(idac == null){
        var nCountAc = js_Grabar_Acuerdo.length;
        var htmlTitulo = "";
        for(var i=0; i<nCountAc; i++){//Volcar datos en la tabla Acuerdos
            if ($I("chkTodos").checked || js_Grabar_Acuerdo[i].estado == "A" || !bTodos) {
                if (bTodos || js_Grabar_Acuerdo[i].accion == "I") {
                    var oNF = tblAcuerdos.insertRow(-1);
                    oNF.id = js_Grabar_Acuerdo[i].idac;
                    oNF.style.height = "20px";
                    oNF.setAttribute("idMoneda", js_Grabar_Acuerdo[i].moneda);
                    oNF.setAttribute("bd", js_Grabar_Acuerdo[i].accion);
                    //añadidos
                    oNF.setAttribute("descrip", js_Grabar_Acuerdo[i].descrip);
                    oNF.setAttribute("idres", js_Grabar_Acuerdo[i].idres);
                    oNF.setAttribute("nomres", js_Grabar_Acuerdo[i].nomres);
                    oNF.setAttribute("idmod", js_Grabar_Acuerdo[i].idmod);
                    oNF.setAttribute("fm", js_Grabar_Acuerdo[i].fm);

                    oNF.onclick = function() {
                        ms(this);
                        visualizarTablas(this);
                        iFilaAcuerdo = this.id;
                    };
                    oNF.ondblclick = function() {
                        modificarAcuerdo(this);
                    };

                    htmlTitulo = "<label style='width:80px;'>Denominac&oacute;n:</label>" + Utilidades.unescape(js_Grabar_Acuerdo[i].denomi) + "<br>";
                    htmlTitulo += "<label style='width:80px;'>Responsable:</label>" + js_Grabar_Acuerdo[i].nomres + "<br>";
                    htmlTitulo += "<label style='width:80px;vertical-align:top;'>Descripci&oacute;n:</label><label style='width:300px;'>" + Utilidades.unescape(js_Grabar_Acuerdo[i].descrip) + "</label>";
                    setTTE(oNF, htmlTitulo);
                    oNF.setAttribute("descrip", js_Grabar_Acuerdo[i].descrip);
                    oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                    oNF.insertCell(-1).innerText = (js_Grabar_Acuerdo[i].accion == "I") ? "" : js_Grabar_Acuerdo[i].idac;
                    oNF.cells[1].setAttribute("style", "text-align:right");
                    oNF.insertCell(-1).appendChild(oNBR180.cloneNode(true), null);
                    oNF.cells[2].children[0].innerText = Utilidades.unescape(js_Grabar_Acuerdo[i].denomi);
                    oNF.cells[2].setAttribute("style", "padding-left:10px");
                    oNF.insertCell(-1).appendChild(document.createTextNode(js_Grabar_Acuerdo[i].fi));
                    oNF.insertCell(-1).appendChild(document.createTextNode(js_Grabar_Acuerdo[i].ff));
                    mfa(oNF, js_Grabar_Acuerdo[i].accion);
                }
                else if (js_Grabar_Acuerdo[i].accion == "U" || js_Grabar_Acuerdo[i].accion == "D") {
                    var aFilasAc = FilasDe("tblAcuerdos");
                    for (var j = 0, nCountJ = aFilasAc.length; j < nCountJ; j++) {
                        if (aFilasAc[j].id == js_Grabar_Acuerdo[i].idac) {
                            aFilasAc[j].children[1].innerHTML = js_Grabar_Acuerdo[i].idac;
                            aFilasAc[j].children[2].children[0].innerHTML = Utilidades.unescape(js_Grabar_Acuerdo[i].denomi);
                            aFilasAc[j].children[3].innerHTML = js_Grabar_Acuerdo[i].fi;
                            aFilasAc[j].children[4].innerHTML = js_Grabar_Acuerdo[i].ff;
                            htmlTitulo = "<label style='width:80px;'>Denominac&oacute;n:</label>" + Utilidades.unescape(js_Grabar_Acuerdo[i].denomi) + "<br>";
                            htmlTitulo += "<label style='width:80px;'>Responsable:</label>" + js_Grabar_Acuerdo[i].nomres + "<br>";
                            htmlTitulo += "<label style='width:80px;vertical-align:top;'>Descripci&oacute;n:</label><label style='width:300px;'>" + Utilidades.unescape(js_Grabar_Acuerdo[i].descrip) + "</label>";
                            setTTE(aFilasAc[j], htmlTitulo);
                            aFilasAc[j].setAttribute("descrip", js_Grabar_Acuerdo[i].descrip);
                            aFilasAc[j].setAttribute("idres", js_Grabar_Acuerdo[i].idres);
                            aFilasAc[j].setAttribute("nomres", js_Grabar_Acuerdo[i].nomres);
                            aFilasAc[j].setAttribute("idmod",js_Grabar_Acuerdo[i].idmod);
                            aFilasAc[j].setAttribute("fm", js_Grabar_Acuerdo[i].fm);
                            aFilasAc[j].setAttribute("idMoneda", js_Grabar_Acuerdo[i].moneda);
                            mfa(aFilasAc[j], js_Grabar_Acuerdo[i].accion);
                            break;
                        }
                    }
                }
            }
        }
        ot('tblAcuerdos',2,0,'');
    }
    else{
        var nCountProf = js_Grabar_Prof.length;
        var nCountProy = js_Grabar_Proy.length;
        var nCountCR = js_Grabar_CR.length;
        for(var i=0; i<nCountProf; i++){//Volcar datos en la tabla Profesionales
            if(js_Grabar_Prof[i].idac == idac){
                oNF = $I("tblProf").insertRow(-1);
                oNF.id = js_Grabar_Prof[i].idfi;
                oNF.setAttribute("idac", js_Grabar_Prof[i].idac);
                oNF.setAttribute("tipo", js_Grabar_Prof[i].tipo);
                oNF.setAttribute("sexo", js_Grabar_Prof[i].sexo);
                var id = oNF.id;
                oNF.className = "MANO";
                oNF.style.height = "20px";
                
                oNF.setAttribute("bd", js_Grabar_Prof[i].accion);
                oNF.onclick = function (){ms(this);
                                         };
                
                oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));    	
                oNF.children[0].setAttribute("style", "padding-left:2px;");
	            oNC2 = oNF.insertCell(-1);
	            oNC2.setAttribute("style", "width:20px; padding-left:2px");
                if (js_Grabar_Prof[i].sexo == "V"){
                    switch (js_Grabar_Prof[i].tipo){
                        case "B": oNC2.appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oNC2.appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oNC2.appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (js_Grabar_Prof[i].tipo){
                        case "B": oNC2.appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oNC2.appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oNC2.appendChild(oImgTM.cloneNode(true), null); break;
                    }
                }
                oNC3 = oNF.insertCell(-1);
                oNC3.setAttribute("style", "width:363px; padding-left:5px");
                oNC3.innerText = js_Grabar_Prof[i].nombre;
                
                mfa(oNF, js_Grabar_Prof[i].accion);
            }
        }
        
        for(var i=0; i<nCountProy; i++){//Volcar datos en la tabla de proyectos
            if(js_Grabar_Proy[i].idac == idac){
                oNF = tblProy.insertRow(-1);
                oNF.id = js_Grabar_Proy[i].idpr;
                oNF.setAttribute("idac", js_Grabar_Proy[i].idac);
                oNF.setAttribute("estado", js_Grabar_Proy[i].estado);
                oNF.setAttribute("bd", js_Grabar_Proy[i].accion);
                oNF.className = "MANO";
                oNF.style.height = "20px";
                htmlTitulo = "";
                htmlTitulo += "<label style='width:80px;'>Responsable:</label>" + js_Grabar_Proy[i].responsable + "<br>";
                htmlTitulo += "<label style='width:80px;vertical-align:top;'>C.R.:</label>" + js_Grabar_Proy[i].cr + "";
                setTTE(oNF, htmlTitulo);

                oNF.onclick = function (){ms(this);
                                         };
                oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                oNF.children[0].setAttribute("style", "padding-left:2px;");
                oNC2 = oNF.insertCell(-1);
                oNC2.setAttribute("style", "width:23px; padding-left:2px");
                switch (js_Grabar_Proy[i].estado){
                    case "A": oNC2.appendChild(oImgAb.cloneNode(true), null); break;
                    case "C": oNC2.appendChild(oImgCe.cloneNode(true), null); break;
                    case "H": oNC2.appendChild(oImgHi.cloneNode(true), null); break;
                    case "P": oNC2.appendChild(oImgPr.cloneNode(true), null); break;
                }

                oNC3 = oNF.insertCell(-1);
                oNC3.setAttribute("style", "width:360px; padding-left:5px;");
                oNC3.innerText = js_Grabar_Proy[i].nombre;
                mfa(oNF, js_Grabar_Proy[i].accion);
            }
        }
        for(var i=0; i<nCountCR; i++){//Volcar datos en la tabla de CR
            if(js_Grabar_CR[i].idac == idac){
                oNF = $I("tblCR").insertRow(-1);
                oNF.id = js_Grabar_CR[i].idcr;
                var id = oNF.id;

                oNF.setAttribute("idac", js_Grabar_CR[i].idac);
                oNF.setAttribute("bd", js_Grabar_CR[i].accion);
                oNF.className = "MANO";
                oNF.style.height = "20px";
                oNF.onclick = function (){ms(this);
                                         };

                oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                oNF.children[0].setAttribute("style", "padding-left:2px");
                oNC2 = oNF.insertCell(-1);
                oNC2.setAttribute("style", "width:383px; padding-left:5px");
                oNC2.innerText = js_Grabar_CR[i].nombre;
                mfa(oNF, js_Grabar_CR[i].accion);
            }
        }
    }
    ocultarProcesando();
}

function actualizarDatos(sTipo){
    switch(sTipo){
        case 1://Acuerdos
            var aFilasAc = FilasDe("tblAcuerdos");
            var fechaActual = new Date().ToShortDateString();
            var existe = false;
            for(var i=0, nCount=aFilasAc.length; i<nCount; i++){
                existe = false;
                for(var j=0, nCount2=js_Grabar_Acuerdo.length; j<nCount2; j++){
                    if(aFilasAc[i].id == js_Grabar_Acuerdo[j].idac){
                        existe = true;
                        break;
                    }
                }
                if(!existe){
                    js_Grabar_Acuerdo[js_Grabar_Acuerdo.length]={idac:aFilasAc[i].id, accion:"N", denomi:aFilasAc[i].children[2].innerText, idres:aFilasAc[i].getAttribute("idres"), nomres:aFilasAc[i].getAttribute("nomres"), descrip:aFilasAc[i].getAttribute("descrip"), fi:aFilasAc[i].children[3].innerText, ff:aFilasAc[i].children[4].innerText, idmod:aFilasAc[i].getAttribute("idmod"), fm:aFilasAc[i].getAttribute("fm"), estado:"", moneda:aFilasAc[i].getAttribute("idMoneda")};
                    if(DiffDiasFechas(aFilasAc[i].children[4].innerText, fechaActual) <= 0) js_Grabar_Acuerdo[js_Grabar_Acuerdo.length-1].estado = "A";
                    else js_Grabar_Acuerdo[js_Grabar_Acuerdo.length-1].estado = "C";
                }
            }
            break;
        case 2://Profesionales, proyecto y/o CR
            var aFilasProf = FilasDe("tblProf");
            var aFilasProy = FilasDe("tblProy");
            var aFilasCR = FilasDe("tblCR");
            var aPadre = buscarFilaSelecPadre();
            for(var i=0, nCount=aFilasProf.length; i<nCount; i++){
                js_Grabar_Prof[js_Grabar_Prof.length]={idac:aPadre[0], idfi:aFilasProf[i].id, accion:"N", tipo:aFilasProf[i].getAttribute("tipo"), sexo:aFilasProf[i].getAttribute("sexo"), nombre:aFilasProf[i].children[2].innerText};
            }
            for(var i=0, nCount=aFilasProy.length; i<nCount; i++){
                js_Grabar_Proy[js_Grabar_Proy.length]={idac:aPadre[0], idpr:aFilasProy[i].id, accion:"N", nombre:aFilasProy[i].children[2].innerText, estado:aFilasProy[i].getAttribute("estado"), responsable:aFilasProy[i].getAttribute("resp"), cr:aFilasProy[i].getAttribute("cr")};
            }
            for(var i=0, nCount=aFilasCR.length; i<nCount; i++){
                js_Grabar_CR[js_Grabar_CR.length]={idac:aPadre[0], idcr:aFilasCR[i].id, accion:"N", nombre:aFilasCR[i].children[1].innerText};
            }
            break;
    }
    
}

function buscarFilaSelecPadre(){
    var aFilas = FilasDe("tblAcuerdos");
    var aRetorno = new Array(); 
    if (aFilas.length > 0){        
        for (x=0; x<aFilas.length; x++){
            if (aFilas[x].className == "FS"){
                aRetorno[0] = aFilas[x].id;
                //aRetorno[1] = aFilas[x].bd;
	            return aRetorno;
	        }    
        }
    }
}

function mostrarTodos(){
    if(!bTodos){
        var sb = new StringBuilder;
        sb.Append("mostrarTodos");
        RealizarCallBack(sb.ToString(), "");
    }
    else{
        if($I("tblAcuerdos") != null) BorrarFilasDe("tblAcuerdos");
        volcarDatos(null);
    }
    vaciarTablas();
    $I("divContenedorProf").style.visibility = "hidden";
    $I("divAcuerdoInf").style.visibility = "hidden";

    $I("imgExcel2_exp").style.visibility = "hidden";
    $I("imgExcel3_exp").style.visibility = "hidden";
    $I("imgExcel4_exp").style.visibility = "hidden";
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;

function scrollTablaProf(){
    try{
        if ($I("divCatProf").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divCatProf").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        if ($I("divCatProf").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatProf").offsetHeight / 20 + 1, $I("tblProf").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatProf").innerHeight / 20 + 1, $I("tblProf").rows.length);

        var nUltFila = Math.min(nFilaVisible + $I("divCatProf").offsetHeight/20+1, tblProf.rows.length);
        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblProf.rows[i].getAttribute("sw")){
                oFila = tblProf.rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("sexo") == "V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTV.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTM.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIM.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollProy = -1;
var nIDTimeProy = 0;

function scrollTablaProy(){
    try{
        if ($I("divCatProy").scrollTop != nTopScrollProy){
            nTopScrollProy = $I("divCatProy").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        if ($I("divCatProy").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatProy").offsetHeight / 20 + 1, $I("tblProy").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatProy").innerHeight / 20 + 1, $I("tblProy").rows.length);

        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblProy.rows[i].getAttribute("sw")){
                oFila = tblProy.rows[i];
                oFila.setAttribute("sw", 1);               
                switch (oFila.getAttribute("estado")){
                    case "A": oFila.cells[1].appendChild(oImgAb.cloneNode(true), null); break;
                    case "C": oFila.cells[1].appendChild(oImgCe.cloneNode(true), null); break;
                    case "H": oFila.cells[1].appendChild(oImgHi.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgPr.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}


var nNuevaFila = 30000;
function nuevoAcuerdo() {
    try {
        mostrarProcesando();
//        var url = "Detalle/Default.aspx";
//        var ret = window.showModalDialog(url, self, sSize(370, 440));
//        window.focus();
        var strEnlace = strServer + "Capa_Presentacion/Mantenimiento/Acuerdo/Detalle/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(370, 440))
            .then(function(ret) {
                if (ret != null){
                    var aDatos = ret.split("@#@");
                    var fechaActual = new Date().ToShortDateString();
                    nNuevaFila++;

                    js_Grabar_Acuerdo[js_Grabar_Acuerdo.length] = { idac: nNuevaFila, accion: "I", denomi: aDatos[1], idres: aDatos[5], nomres: aDatos[6], descrip: aDatos[4], fi: aDatos[2], ff: aDatos[3], idmod: aDatos[7], fm: fechaActual, estado: "", moneda: aDatos[8] };
                    if(DiffDiasFechas(aDatos[3], fechaActual) <= 0) js_Grabar_Acuerdo[js_Grabar_Acuerdo.length-1].estado = "A";
                    else js_Grabar_Acuerdo[js_Grabar_Acuerdo.length-1].estado = "C";

                    //if (($I("chkTodos").checked && js_Grabar_Acuerdo[js_Grabar_Acuerdo.length-1].estado == "C") || (!$I("chkTodos").checked && js_Grabar_Acuerdo[js_Grabar_Acuerdo.length-1].estado == "A")){
                    if ($I("chkTodos").checked || js_Grabar_Acuerdo[js_Grabar_Acuerdo.length-1].estado == "A"){
                        oNF = tblAcuerdos.insertRow(-1);
                        iFila = oNF.rowIndex;
                        oNF.style.height = "20px";
                        //oNF.className = "MA";
                        oNF.id = nNuevaFila;

                        oNF.setAttribute("bd", "I");
                        oNF.setAttribute("descrip", aDatos[4]);
                        oNF.setAttribute("idres", aDatos[5]);
                        oNF.setAttribute("nomres", aDatos[6]);
                        oNF.setAttribute("idmod", aDatos[7]);
                        oNF.setAttribute("fm", fechaActual);
                        oNF.setAttribute("idMoneda", aDatos[8]);

                        htmlTitulo = "<label style='width:80px;'>Denominac&oacute;n:</label>" + Utilidades.unescape(aDatos[1]) + "<br>";
                        htmlTitulo += "<label style='width:80px;'>Responsable:</label>" + aDatos[6] + "<br>";
                        htmlTitulo += "<label style='width:80px;vertical-align:top;'>Descripci&oacute;n:</label><label style='width:300px;'>" + Utilidades.unescape(aDatos[4]) + "</label>";
                        if(htmlTitulo != "" ) setTTE(oNF, htmlTitulo);
                        
                        oNF.onclick = function (){ms(this);
                                                 visualizarTablas(this);
                                                 iFilaAcuerdo = this.id;
                                                };
                        oNF.ondblclick = function (){modificarAcuerdo(this);
                                                };

                        //oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
                        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
                        oNF.insertCell(-1);
                        oNF.insertCell(-1).appendChild(oNBR180.cloneNode(true), null);
                        oNF.cells[2].setAttribute("style", "padding-left:10px");
                        oNF.cells[2].children[0].innerText = Utilidades.unescape(aDatos[1]);
                        oNF.insertCell(-1).appendChild(document.createTextNode(aDatos[2]));
                        oNF.insertCell(-1).appendChild(document.createTextNode(aDatos[3]));
                        $I("divCatAcuerdos").scrollTop = oNF.rowIndex * 20;
                        iFilaAcuerdo = iFila;
                        if (ie) oNF.click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            oNF.dispatchEvent(clickEvent);
                        }
                    }
                    activarGrabar();
                }
                ocultarProcesando();
            });

    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el nuevo acuerdo.", e.message);
    }
}

function modificarAcuerdo(oFila) {
    var fechaActual = new Date().ToShortDateString();
    var url = "Detalle/Default.aspx?iA=" + codpar(oFila.id); 
    url += "&dA=" + codpar(oFila.cells[2].innerText);
    url += "&fi=" + codpar(oFila.cells[3].innerText);
    url += "&ff=" + codpar(oFila.cells[4].innerText);
    url += "&iR=" + codpar(oFila.getAttribute("idres"));
    url += "&nR=" + codpar(oFila.getAttribute("nomres"));
    url += "&iM=" + codpar(oFila.getAttribute("idMoneda"));
    
//    var ret = window.showModalDialog(url, self, sSize(370, 440)); 
//    window.focus();
    modalDialog.Show(url, self, sSize(370, 440))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    mfa(oFila, "U");

                    htmlTitulo = "<label style='width:80px;'>Denominac&oacute;n:</label>" + Utilidades.unescape(aDatos[1]) + "<br>";
                    htmlTitulo += "<label style='width:80px;'>Responsable:</label>" + aDatos[6] + "<br>";
                    htmlTitulo += "<label style='width:80px;vertical-align:top;'>Descripci&oacute;n:</label><label style='width:300px;'>" + Utilidades.unescape(aDatos[4]) + "</label>";
                    if (htmlTitulo != "") setTTE(oFila, htmlTitulo);
                    else delTTE(oFila);

                    oFila.setAttribute("descrip", aDatos[4]);
                    oFila.setAttribute("idres", aDatos[5]);
                    oFila.setAttribute("nomres", aDatos[6]);
                    oFila.setAttribute("idmod", aDatos[7]);
                    oFila.setAttribute("fm", new Date().ToShortDateString());
                    oFila.setAttribute("idMoneda", aDatos[8]);
                    oFila.cells[1].innerText = oFila.id;
                    oFila.cells[2].children[0].innerText = Utilidades.unescape(aDatos[1]);
                    oFila.cells[3].innerText = aDatos[2];
                    oFila.cells[4].innerText = aDatos[3];
                    var nCount = js_Grabar_Acuerdo.length;
                    for (var i = 0; i < nCount; i++) {
                        if (js_Grabar_Acuerdo[i].idac == oFila.id) {
                            if (js_Grabar_Acuerdo[i].accion != "I")
                                js_Grabar_Acuerdo[i].accion = "U";
                            js_Grabar_Acuerdo[i].denomi = aDatos[1];
                            js_Grabar_Acuerdo[i].idres = aDatos[5];
                            js_Grabar_Acuerdo[i].nomres = aDatos[6];
                            js_Grabar_Acuerdo[i].descrip = aDatos[4];
                            js_Grabar_Acuerdo[i].fi = aDatos[2];
                            js_Grabar_Acuerdo[i].ff = aDatos[3];
                            js_Grabar_Acuerdo[i].idmod = aDatos[7];
                            js_Grabar_Acuerdo[i].fm = new Date().ToShortDateString();
                            js_Grabar_Acuerdo[i].moneda = aDatos[8];
                            if (DiffDiasFechas(aDatos[3], fechaActual) <= 0) js_Grabar_Acuerdo[i].estado = "A";
                            else js_Grabar_Acuerdo[i].estado = "C";
                            break;
                        }
                    }
                    activarGrabar();
                }
            });
}

function catProfesionales(){
//    var url = "../../getProfesionales/Default.aspx";
//    var ret = window.showModalDialog(url, self, sSize(430, 600));
//    window.focus();

    var strEnlace = "../../getProfesionales/Default.aspx";
    modalDialog.Show(strEnlace, self, sSize(430, 600))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    var aFilasProf = FilasDe("tblProf");
                    var existe = false;
                    for (var i = 0, nCount = aFilasProf.length; i < nCount; i++) {
                        if (aFilasProf[i].id == aDatos[0]) {
                            existe = true;
                            if (aFilasProf[i].getAttribute("bd") == "D") {
                                mfa(aFilasProf[i], "U"); //actualizar array
                                for (var j = 0, nCount2 = js_Grabar_Prof.length; j < nCount2; j++) {
                                    if (js_Grabar_Prof[j].id == aFilasProf[i].id) {
                                        js_Grabar_Prof[j].accion = "U";
                                        break;
                                    }
                                }

                            }
                            break;
                        }
                    }
                    if (!existe) {
                        oNF = tblProf.insertRow(-1);
                        oNF.id = aDatos[0];
                        var aPadre = buscarFilaSelecPadre();
                        oNF.setAttribute("idac", aPadre[0]);
                        oNF.setAttribute("tipo", aDatos[3]);
                        oNF.setAttribute("sexo", aDatos[2]);
                        oNF.className = "MA";
                        oNF.style.height = "20px";

                        oNF.setAttribute("bd", "I");
                        oNF.onclick = function() {
                            ms(this);
                        };

                        oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                        oNF.children[0].setAttribute("style", "padding-left:2px;");
                        oNC2 = oNF.insertCell(-1);
                        oNC2.setAttribute("style", "width:20px; padding-left:2px;");
                        if (aDatos[2] == "V") {
                            switch (aDatos[3]) {
                                case "B": oNC2.appendChild(oImgNV.cloneNode(true), null); break;
                                case "G": oNC2.appendChild(oImgGV.cloneNode(true), null); break;
                                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                                case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                                case "T": oNC2.appendChild(oImgTV.cloneNode(true), null); break;
                            }
                        }
                        else {
                            switch (aDatos[3]) {
                                case "B": oNC2.appendChild(oImgNM.cloneNode(true), null); break;
                                case "G": oNC2.appendChild(oImgGM.cloneNode(true), null); break;
                                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                                case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                                case "T": oNC2.appendChild(oImgTM.cloneNode(true), null); break;
                            }
                        }
                        oNC3 = oNF.insertCell(-1);
                        oNC3.setAttribute("style", "width:363px; padding-left:5px;");
                        oNC3.innerText = aDatos[1];
                        js_Grabar_Prof[js_Grabar_Prof.length] = { idac: aPadre[0], idfi: aDatos[0], accion: "I", tipo: aDatos[3], sexo: aDatos[2], nombre: aDatos[1] };
                        mfa(oNF, "I");
                        if (ie) oNF.click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            oNF.dispatchEvent(clickEvent);
                        }
                    }
                    activarGrabar();
                }
            });
}

function catProyectos(){
//    var url = "../../getProyectosADM/Default.aspx?iA=";
//    var ret = window.showModalDialog(url, self, sSize(990, 650));
//    window.focus();

    var strEnlace = "../../getProyectosADM/Default.aspx?iA=";
    modalDialog.Show(strEnlace, self, sSize(990, 650))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    var aFilasProy = FilasDe("tblProy");
                    var existe = false;
                    for (var i = 0, nCount = aFilasProy.length; i < nCount; i++) {
                        if (aFilasProy[i].id == aDatos[0]) {
                            existe = true;
                            if (aFilasProy[i].getAttribute("bd") == "D") {
                                mfa(aFilasProy[i], "U"); //actualizar array
                                for (var j = 0, nCount2 = js_Grabar_Proy.length; j < nCount2; j++) {
                                    if (js_Grabar_Proy[j].id == aFilasProy[i].id) {
                                        js_Grabar_Proy[j].accion = "U";
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    if (!existe) {
                        oNF = tblProy.insertRow(-1);
                        oNF.id = aDatos[0];
                        var aPadre = buscarFilaSelecPadre();
                        oNF.setAttribute("idac", aPadre[0]);
                        oNF.setAttribute("estado", aDatos[2]);
                        oNF.className = "MA";
                        oNF.style.height = "20px";

                        oNF.setAttribute("bd", "I");
                        var htmlTitulo = "<label style='width:80px;'>Responsable:</label>" + aDatos[4] + "<br>";
                        htmlTitulo += "<label style='width:80px;vertical-align:top;'>C.R.:</label>" + aDatos[5] + "";
                        setTTE(oNF, htmlTitulo);

                        oNF.onclick = function() {
                            ms(this);
                        };

                        oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                        oNF.children[0].setAttribute("style", "padding-left:2px");
                        oNC2 = oNF.insertCell(-1);
                        oNC2.setAttribute("style", "padding-left:2px; width: 23px");
                        switch (aDatos[2]) {
                            case "A": oNC2.appendChild(oImgAb.cloneNode(true), null); break;
                            case "C": oNC2.appendChild(oImgCe.cloneNode(true), null); break;
                            case "H": oNC2.appendChild(oImgHi.cloneNode(true), null); break;
                            case "P": oNC2.appendChild(oImgPr.cloneNode(true), null); break;

                        }
                        oNC3 = oNF.insertCell(-1);
                        oNC3.setAttribute("style", "padding-left:5px; width: 360px");
                        oNC3.innerText = aDatos[0] + " - " + aDatos[1];
                        js_Grabar_Proy[js_Grabar_Proy.length] = { idac: aPadre[0], idpr: aDatos[0], accion: "I", estado: aDatos[2], nombre: oNC3.innerText, responsable: aDatos[4], cr: aDatos[5] };
                        mfa(oNF, "I");
                        if (ie) oNF.click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            oNF.dispatchEvent(clickEvent);
                        }
                    }
                    activarGrabar();
                }
            });
}

function catNodos(){
//    var url = "../../getNodos/Default.aspx?iA=";
//    var ret = window.showModalDialog(url, self, sSize(410, 600));
//    window.focus();

    var strEnlace = "../../getNodos/Default.aspx?iA=";
    modalDialog.Show(strEnlace, self, sSize(410, 600))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    var aFilasCR = FilasDe("tblCR");
                    var existe = false;
                    for (var i = 0, nCount = aFilasCR.length; i < nCount; i++) {
                        if (aFilasCR[i].id == aDatos[0]) {
                            existe = true;
                            if (aFilasCR[i].getAttribute("bd") == "D") {
                                mfa(aFilasCR[i], "U"); //actualizar array
                                for (var j = 0, nCount2 = js_Grabar_CR.length; j < nCount2; j++) {
                                    if (js_Grabar_CR[j].id == aFilasCR[i].id) {
                                        js_Grabar_CR[j].accion = "U";
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    if (!existe) {
                        oNF = tblCR.insertRow(-1);
                        oNF.id = aDatos[0];
                        var aPadre = buscarFilaSelecPadre();
                        oNF.setAttribute("idac", aPadre[0]);
                        oNF.className = "MA";
                        oNF.style.height = "20px";

                        oNF.setAttribute("bd", "I");
                        oNF.onclick = function() {
                            ms(this);
                        };

                        oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                        oNF.children[0].setAttribute("style", "padding-left:2px;");
                        oNC2 = oNF.insertCell(-1);
                        oNC2.setAttribute("style", "padding-left:5px; width: 383px");
                        oNC2.innerText = aDatos[1];
                        js_Grabar_CR[js_Grabar_CR.length] = { idac: aPadre[0], idcr: aDatos[0], accion: "I", nombre: aDatos[1] };
                        mfa(oNF, "I");
                        if (ie) oNF.click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            oNF.dispatchEvent(clickEvent);
                        }
                    }
                    activarGrabar();
                }
            });
}


function eliminarProf(){
    var aFilasProf = FilasDe("tblProf");
    var oFila = null;
    var iEliminar = -1;
    for(var i=0, nCount=aFilasProf.length; i<nCount; i++){
        if(aFilasProf[i].className == "FS"){
            oFila = aFilasProf[i]
            iEliminar = i;
            break;
        }
    }
    if(oFila != null){
        if(oFila.getAttribute("bd") == "I"){
            tblProf.deleteRow(iEliminar);
            for(var i=0, nCount=js_Grabar_Prof.length; i<nCount; i++){
                if(js_Grabar_Prof[i].idfi == oFila.id){
                    js_Grabar_Prof.splice(i,1);
                    nCount--;
                    i--;
                }
            }
        }
        else{
            mfa(oFila, "D");
            for(var i=0, nCount=js_Grabar_Prof.length; i<nCount; i++){
                if(js_Grabar_Prof[i].idfi == oFila.id) js_Grabar_Prof[i]={idac:oFila.getAttribute("idac"), idfi:oFila.id, accion:"D", tipo:oFila.getAttribute("tipo"), sexo:oFila.getAttribute("sexo"), nombre:oFila.innerText};
            }
        }
    }
}

function eliminarProy(){
    var aFilasProy = FilasDe("tblProy");
    var oFila = null;
    var iEliminar = -1;
    for(var i=0, nCount=aFilasProy.length; i<nCount; i++){
        if(aFilasProy[i].className == "FS"){
            oFila = aFilasProy[i]
            iEliminar = i;
            break;
        }
    }
    if(oFila != null){
        if(oFila.getAttribute("bd") == "I"){
            tblProy.deleteRow(iEliminar);
            for(var i=0, nCount=js_Grabar_Proy.length; i<nCount; i++){
                if(js_Grabar_Proy[i].idpr == oFila.id){
                    js_Grabar_Proy.splice(i,1);
                    nCount--;
                    i--;
                }
            }
        }
        else{
            mfa(oFila, "D");
            for(var i=0, nCount=js_Grabar_Proy.length; i<nCount; i++){
                if(js_Grabar_Proy[i].idpr == oFila.id) js_Grabar_Proy[i]={idac:oFila.getAttribute("idac"), idpr:oFila.id, accion:"D", estado:oFila.getAttribute("estado"), nombre:oFila.innerText, responsable:oFila.getAttribute("resp"), cr:oFila.getAttribute("cr")};
            }
        }
    }
}

function eliminarCR(){
    var aFilasCR = FilasDe("tblCR");
    var oFila = null;
    var iEliminar = -1;
    for(var i=0, nCount=aFilasCR.length; i<nCount; i++){
        if(aFilasCR[i].className == "FS"){
            oFila = aFilasCR[i]
            iEliminar = i;
            break;
        }
    }
    if(oFila != null){
        if(oFila.getAttribute("bd") == "I"){
            tblCR.deleteRow(iEliminar);
            for(var i=0, nCount=js_Grabar_CR.length; i<nCount; i++){
                if(js_Grabar_CR[i].idcr == oFila.id){
                    js_Grabar_CR.splice(i,1);
                    nCount--;
                    i--;
                }
            }
        }
        else{
            mfa(oFila, "D");
            for(var i=0, nCount=js_Grabar_CR.length; i<nCount; i++){
                if(js_Grabar_CR[i].idcr == oFila.id) js_Grabar_CR[i]={idac:oFila.getAttribute("idac"), idcr:oFila.id, accion:"D", nombre:oFila.innerText};
            }
        }
    }
}

function eliminarAcuerdo(){
    try{
        var sw = 0;
        var sw2 = 0;
        aFila = FilasDe("tblAcuerdos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw2 = 1; 
                if(js_Grabar_Prof.length > 0){
                    for(var j=0, nCountLoop = js_Grabar_Prof.length; j<nCountLoop; j++){ //Copia los elementos de js_Grabar_Prof que no hay que borrar
                        if(js_Grabar_Prof[j].idac == aFila[i].id){
                            js_Grabar_Prof.splice(j,1);
                            nCountLoop--;
                            j--;
                        }
                    }   
                }
                ///
                if(js_Grabar_Proy.length > 0){
                    for(var j=0, nCountLoop = js_Grabar_Proy.length; j<nCountLoop; j++){ //Copia los elementos de js_Grabar_Proy que no hay que borrar
                        if(js_Grabar_Proy[j].idac == aFila[i].id){
                            js_Grabar_Proy.splice(j,1);
                            nCountLoop--;
                            j--;
                        }
                    }   
                }
                ///
                if(js_Grabar_CR.length > 0){
                    for(var j=0, nCountLoop = js_Grabar_CR.length; j<nCountLoop; j++){ //Copia los elementos de js_Grabar_CR que no hay que borrar
                        if(js_Grabar_CR[j].idac == aFila[i].id){
                            js_Grabar_CR.splice(j,1);
                            nCountLoop--;
                            j--;
                        }
                    }   
                }
                vaciarTablas();                              

//                var acuerdoExiste = false;
                var nCount = js_Grabar_Acuerdo.length;
                for(var j=0; j<nCount; j++){
                    if(js_Grabar_Acuerdo[j].idac == aFila[i].id){
                        if(js_Grabar_Acuerdo[j].accion == "I"){
                            js_Grabar_Acuerdo.splice(j,1);
                            //Si es una fila nueva, se elimina
                            tblAcuerdos.deleteRow(i);
                        }
                        else js_Grabar_Acuerdo[j].accion = "D";
//                        acuerdoExiste = true;
                        break;
                    }
                }
//                if(!acuerdoExiste){
//                    var fechaActual = new Date().ToShortDateString();
//                    var fila = js_Grabar_Acuerdo.length-1;
//                    js_Grabar_Acuerdo[fila] = {idac:aFila[i].id, accion:"D", denomi:aFila[i].children[1].innerText, idres:aFila[i].idres, nomres:aFila[i].nomres, descrip:aFila[i].descrip,  fi:aFila[i].children[2].innerText, ff:aFila[i].children[3].innerText, idmod:aFila[i].idmod, fm:aFila[i].fm, estado:""};
//                    if(DiffDiasFechas(aFila[i].children[3].innerText, fechaActual) <= 0) js_Grabar_Acuerdo[fila].estado = "A";
//                    else js_Grabar_Acuerdo[fila].estado = "C";
//                }

                mfa(aFila[i],"D");
                sw = 1;
                $I("divContenedorProf").style.visibility = "hidden";
                $I("divAcuerdoInf").style.visibility = "hidden";

                $I("imgExcel2_exp").style.visibility = "hidden";
                $I("imgExcel3_exp").style.visibility = "hidden";
                $I("imgExcel4_exp").style.visibility = "hidden";
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("War", "Debe seleccionar la fila a eliminar",250);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}


//function restaurarFila2(){ //Función que se llama cuando se pincha con el botón derecho a "No Eliminar"
////    switch (event.srcElement.tagName){
////        case "TD": nIndiceInsert = event.srcElement.parentNode.rowIndex; break;
////        case "INPUT": nIndiceInsert = event.srcElement.parentNode.parentNode.rowIndex; break;
////    }

////oFilaARestaurar.parentNode.parentNode.id
//    var nCount = js_Grabar_Acuerdo.length;
////    var aFilas = FilasDe("tblAcuerdos");
////    for(var i=0; i<nCount; i++){
////        if(js_Grabar_Acuerdo[i].idac == aFilas[iFila].id){
////            js_Grabar_Acuerdo[i].accion = "U";
////            aFilas[iFila].click();
////            break;
////        }
////    }
//}




function grabarAcuerdos(){
    var nCount = js_Grabar_Acuerdo.length;
    var aFilas = FilasDe("tblAcuerdos");
    var sb = new StringBuilder;    
    var existe = false;
    for (var i = 0; i < nCount; i++) {
        existe = false;
        if (js_Grabar_Acuerdo[i].accion != "N") {
            if (js_Grabar_Acuerdo[i].accion == "D"){
                for (var j = 0, nCountAc = aFilas.length; j < nCountAc; j++) {
                    if (js_Grabar_Acuerdo[i].idac == aFilas[j].id) {
                        if (aFilas[j].getAttribute("bd") == "D") existe = true;
                        else {
                            existe = false;
                            js_Grabar_Acuerdo[i].accion = "N";
                        }
                        break;
                    }
                }
            }
            if(existe || (js_Grabar_Acuerdo[i].accion != "D" && js_Grabar_Acuerdo[i].accion != "N")){
                sb.Append(js_Grabar_Acuerdo[i].accion + "#sCad#");
                sb.Append(js_Grabar_Acuerdo[i].idac);
                if (js_Grabar_Acuerdo[i].accion != "D") {
                    sb.Append("#sCad#" + js_Grabar_Acuerdo[i].denomi + "#sCad#");
                    sb.Append(js_Grabar_Acuerdo[i].idres + "#sCad#");
                    sb.Append(js_Grabar_Acuerdo[i].descrip + "#sCad#");
                    sb.Append(js_Grabar_Acuerdo[i].fi + "#sCad#");
                    sb.Append(js_Grabar_Acuerdo[i].ff + "#sCad#");
                    sb.Append(js_Grabar_Acuerdo[i].idmod + "#sCad#");
                    sb.Append(js_Grabar_Acuerdo[i].fm + "#sCad#");
                    sb.Append(js_Grabar_Acuerdo[i].moneda);
                }
                sb.Append("#sFin#");
            }
        }
    }
    return (sb.buffer.length == 0) ? "" : sb.ToString().substring(0, sb.ToString().length - 6);
}

function grabarProf(){
    var nCount = js_Grabar_Prof.length;
    var aFilas = FilasDe("tblProf");
    var sb = new StringBuilder;
    var existe = false;
    for (var i = 0; i < nCount; i++) {
        existe = false;
        if (js_Grabar_Prof[i].accion != "N") {
            if (js_Grabar_Prof[i].accion == "D") {
                for (var j = 0, nCountAc = aFilas.length; j < nCountAc; j++) {
                    if (js_Grabar_Prof[i].idfi == aFilas[j].id) {
                        if (aFilas[j].getAttribute("bd") == "D") existe = true;
                        else {
                            existe = false;
                            js_Grabar_Prof[i].accion = "N";
                        }
                        break;
                    }
                }
            }
            if (existe || (js_Grabar_Prof[i].accion != "D" && js_Grabar_Prof[i].accion != "N")) {
                sb.Append(js_Grabar_Prof[i].accion + "#sCad#");
                sb.Append(js_Grabar_Prof[i].idac + "#sCad#");
                sb.Append(js_Grabar_Prof[i].idfi + "#sFin#");
            }
        }
    }
    return (sb.buffer.length == 0) ? "" : sb.ToString().substring(0, sb.ToString().length - 6); 
}

function grabarProy(){
    var nCount = js_Grabar_Proy.length;
    var aFilas = FilasDe("tblProy");
    var sb = new StringBuilder;
    var existe = false;
    for (var i=0; i<nCount; i++){
        if (js_Grabar_Proy[i].accion != "N") {
            if (js_Grabar_Proy[i].accion == "D") {
                for (var j = 0, nCountAc = aFilas.length; j < nCountAc; j++) {
                    if (js_Grabar_Proy[i].idpr == aFilas[j].id) {
                        if (aFilas[j].getAttribute("bd") == "D") existe = true;
                        else {
                            existe = false;
                            js_Grabar_Proy[i].accion = "N";
                        }
                        break;
                    }
                }
            }
            if (existe || (js_Grabar_Proy[i].accion != "D" && js_Grabar_Proy[i].accion != "N")) {
                sb.Append(js_Grabar_Proy[i].accion + "#sCad#");
                sb.Append(js_Grabar_Proy[i].idac + "#sCad#");
                sb.Append(js_Grabar_Proy[i].idpr + "#sFin#");
            }
        }
    }
    return (sb.buffer.length == 0) ? "" : sb.ToString().substring(0, sb.ToString().length - 6); 
}

function grabarCR(){
    var nCount = js_Grabar_CR.length;
    var aFilas = FilasDe("tblCR");
    var sb = new StringBuilder;
    var existe = false;
    for (var i = 0; i < nCount; i++) {
        existe = false;
        if (js_Grabar_CR[i].accion != "N") {
            if (js_Grabar_CR[i].accion == "D") {
                for (var j = 0, nCountAc = aFilas.length; j < nCountAc; j++) {
                    if (js_Grabar_CR[i].idcr == aFilas[j].id) {
                        if (aFilas[j].getAttribute("bd") == "D") existe = true;
                        else {
                            existe = false;
                            js_Grabar_CR[i].accion = "N";
                        }
                        break;
                    }
                }
            }
            if (existe || (js_Grabar_CR[i].accion != "D" && js_Grabar_CR[i].accion != "N")) {
                sb.Append(js_Grabar_CR[i].accion + "#sCad#");
                sb.Append(js_Grabar_CR[i].idac + "#sCad#");
                sb.Append(js_Grabar_CR[i].idcr + "#sFin#");
            }
        }
    }
    return (sb.buffer.length == 0) ? "" : sb.ToString().substring(0, sb.ToString().length - 6);
}


function grabar(){
    try{
        var js_args = "grabar";
        js_args += "@#@" + grabarAcuerdos();
        js_args += "@#@" + grabarProf();
        js_args += "@#@" + grabarProy();
        js_args += "@#@" + grabarCR();
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

function excelAcuerdo() {
    try {
        if ($I("tblAcuerdos") == null || $I("tblAcuerdos").rows.length =="0") {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        var aFilas = FilasDe("tblAcuerdos");
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
        sb.Append("	<tr style='text-align:center'>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Nº</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Acuerdo</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Desde</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Hasta</td>");
        sb.Append("	</tr>");
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td style='align:right;'>" + ((aFilas[i].getAttribute("bd") == "I") ? "-" : aFilas[i].id) + "</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].children[2].innerText + "</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].children[3].innerText + "</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].children[4].innerText + "</td>");
            sb.Append("</tr>");
        }
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append("	</tr>");
        sb.Append("</table>");
        crearExcel(sb.ToString());
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo excel con los acuerdos.", e.message);
    }
}

function excelProfesionales() {
    try {
        if ($I("tblProf") == null || $I("tblProf").rows.length =="0") {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        var aFilas = FilasDe("tblProf");
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
        sb.Append("	<tr style='text-align:center'>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Tipo</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Id Ficepi</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Profesional</td>");
        sb.Append("	</tr>");
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>");
            switch (aFilas[i].getAttribute("tipo")) {
                case "B": sb.Append("Becario"); break;
                case "G": sb.Append("Genérico"); break;
                case "E": sb.Append("Externo"); break;
                case "I": sb.Append("Interno"); break;
                case "T": sb.Append("ETT"); break;
            }
            sb.Append("</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].id + "</td>");
            sb.Append("<td>" + aFilas[i].children[2].innerText + "</td>");
            sb.Append("</tr>");
        }
        sb.Append(" <td colspan='3' style='background-color: #BCD4DF;'></td>");
        sb.Append("	</tr>");
        sb.Append("</table>");
        crearExcel(sb.ToString());
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo excel con los profesionales.", e.message);
    }
}

function excelProyectos() {
    try {
        if ($I("tblProy") == null || $I("tblProy").rows.length =="0") {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        var aFilas = FilasDe("tblProy");
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
        sb.Append("	<tr style='text-align:center'>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Estado</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Proyecto</td>");
        sb.Append("	</tr>");
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>");
            switch (aFilas[i].estado) {
                case "A": sb.Append("Abierto"); break;
                case "C": sb.Append("Cerrado"); break;
                case "P": sb.Append("Presupuestado"); break;
                case "H": sb.Append("Histórico"); break;
            }
            sb.Append("</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].children[2].innerText + "</td>");
            sb.Append("</tr>");
        }
        sb.Append(" <td colspan='2' style='background-color: #BCD4DF;'></td>");
        sb.Append("	</tr>");
        sb.Append("</table>");
        crearExcel(sb.ToString());
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo excel con los proyectos.", e.message);
    }
}

function excelNodos() {
    try {
        if ($I("tblCR") == null || $I("tblCR").rows.length =="0") {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        var aFilas = FilasDe("tblCR");
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
        sb.Append("	<tr style='text-align:center'>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Código</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Denominación</td>");
        sb.Append("	</tr>");
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td style='align:right;'>" + aFilas[i].id + "</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].children[1].innerText + "</td>");
            sb.Append("</tr>");
        }
        sb.Append(" <td colspan='2' style='background-color: #BCD4DF;'></td>");
        sb.Append("	</tr>");
        sb.Append("</table>");
        crearExcel(sb.ToString());
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo excel con los nodos.", e.message);
    }
}

