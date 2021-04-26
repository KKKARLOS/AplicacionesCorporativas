var indiceFila = 0, indiceFila2=0;
var bGrabable=true;
var sPerfilEmpleado;
//Vbles para acceder al detalle de una línea cuando la pantalla está pdte de grabar
var bDetalle=false, bDetalleHito=false;
var iFDet=-1,iFHit=-1;
var iFila2 = -1;
var bSalir = false;
function init(){
    try{
        if (bRes1024) 
            setResolucion1024();
        activarBtnPT();
        empadrarLineas("ORIGINAL");
    //	Si la plantilla es Empresarial solo será modificable si el usuario conectado tiene perfil de Administrador
    //	Si la plantilla es Departamental solo será modificable si el usuario conectado tiene perfil de Oficina Técnica o superior
    //	Si la plantilla es Personal siempre es modificable (se supone que un usuario solo ve las plantillas personales que son suyas)
        sModificable=$I("txtModificable").value;
        sPerfilEmpleado=$I("txtPerfil").value;
        
        if (sModificable=="T"){bGrabable=true;}
        else {bGrabable=false;}
        //Control de estado de los botones de los items de la plantilla
        var aFilaBotones = $I("btnPT").getElementsByTagName("TR");
        if (bGrabable){//
            for (i=0;i<10;i++){
                if (i != 5)//botón de PT
                    aFilaBotones[0].cells[0].children[i].style.visibility = "visible"; 
            }
            aFilaBotones[0].cells[1].children[0].style.visibility = "visible"; 
        }
        else{
            for (i=0;i<10;i++){
                aFilaBotones[0].cells[0].children[i].style.visibility = "hidden"; 
            }
            aFilaBotones[0].cells[1].children[0].style.visibility = "hidden";
        }
        //control de estado de los botones de los hitos de la plantilla
        var aFilaBotones2 = $I("Table1").getElementsByTagName("TR");
        if (bGrabable){//
            for (i=0;i<4;i++){
                aFilaBotones2[0].cells[0].children[i].style.visibility = "visible"; 
            }
            aFilaBotones2[0].cells[1].children[0].style.visibility = "visible"; 
        }
        else{
            for (i=0;i<4;i++){
                aFilaBotones2[0].cells[0].children[i].style.visibility = "hidden"; 
            }
            aFilaBotones2[0].cells[1].children[0].style.visibility = "hidden";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al iniciar la página", e.message);
    }
}
function activarBtnPT(){
	var bPlantPT=false;
	try{
	    //if (document.getElementById("ctl00_CPHC_lblTipo").innerHTML=="Plantilla Proy. Técnico") bPlantPT=true;
	    if ($I("lblTipo").value=="Proyecto técnico") bPlantPT=true;
	    if (!bGrabable)return false;
	    
	    var aFilaBotones2 = $I("btnPT").getElementsByTagName("TR");
	    if (bPlantPT){
	        //Desplazamos 20pixels el texto de la caecera de la 1ª columna
	        var aFC = $I("tblDatos3").getElementsByTagName("TR");
            aFC[0].cells[1].children[0].style.marginLeft = "20px"; 
            //Hago invisible el botón de PT
	        aFilaBotones2[0].cells[0].children[5].style.visibility = "hidden";
	        //Si estamos en una plantilla de P.T. y el código de plantilla es cero (estamos insertando una nueva plantilla)
	        //ponemos, sino existe, una linea de PT inventada e invisible 
	        insertarPtInvisible("tblDatos");
	    }
	    else {
	        aFilaBotones2[0].className = "";
	    }
	}
	catch(e){
		mostrarErrorAplicacion("Error al activar el botón de Proyectos Técnicos", e.message);
	}
}
function modificarItem(idFila){
	var sEstado, sTipo, sIcono, iFilaAct=-1;
	try{
        var aFila = $I("tblDatos").getElementsByTagName("TR");
        for (i=0;i<aFila.length;i++){
            if (aFila[i].id==idFila){
                iFilaAct=i;
                break;
            }
        }
        if (iFilaAct!=-1){
            sEstado = aFila[iFilaAct].getAttribute("est");
            if (sEstado=="N") {
                aFila[iFilaAct].setAttribute("est","U");
                sTipo = aFila[iFilaAct].getAttribute("tipo");
                sIcono=fgGetIcono3(sTipo,"U"); 
                aFila[iFilaAct].cells[0].children[0].src=sIcono;   
            }
	        activarGrabar();
	    }
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción", e.message);
	}
}
function modificarNombreTarea(e){
	var sEstado, sTipo, sIcono;
	try{
	    if (!e) e = event;
	    var oElement = e.srcElement ? e.srcElement : e.target;

	    switch (e.keyCode) {
	        case 13: //Si estamos en la última línea, Abrimos una linea del mismo tipo
                break;
            case 16://shift
                break;
            case 17://ctrl
                break;
            case 37://flecha izda
                //desplazarTarea("I");
                break;
            case 38://flecha arriba
//                subirTarea();
                break; 
            case 39://flecha dcha
                //desplazarTarea("D");
                break; 
            case 40://flecha abajo
//                bajarTarea()
                break; 
            default:
	            activarGrabar();
	            aFila = $I("tblDatos").getElementsByTagName("TR");
	            sEstado = aFila[iFila].getAttribute("est");
	            if (sEstado=="N") {
	                aFila[iFila].setAttribute("est", "U");
	                sTipo = aFila[iFila].getAttribute("tipo");
                    sIcono=fgGetIcono3(sTipo,"U"); 
                    aFila[iFila].cells[0].children[0].src=sIcono;   
                }
	            
	    }//switch
	    activarGrabar();
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción", e.message);
	}
}
function accionLinea(e) {
	var sEstado, sTipo, sIcono,sTitulo,sDesc, sAccion;
	var iPosHitoPE=-1;
	try{
	    if (!e) e = event;
	    var oElement = e.srcElement ? e.srcElement : e.target;

	    switch (e.keyCode) {
	        case 13: //Si estamos en la última línea, Abrimos una linea del mismo tipo
        	    recalcularFilaSeleccionada();
        	    aFila = $I("tblDatos").getElementsByTagName("TR");
	            sAccion=getRadioButtonSelectedValue("rdbAccion",false);
	            sTipo = getTipo(aFila[iFila].getAttribute("tipo"));
        	    if (sAccion=="A"){
                    iPosHitoPE=flPosHitoPE();
                    if ((sTipo!="H") &&(iFila==aFila.length-1)){
                        nuevaTarea("tblDatos",sTipo);
                    }
                    else if ((iPosHitoPE>0) &&(iFila==aFila.length-2)){//penultima fila y la ultima es hito de PE
                        nuevaTarea("tblDatos",sTipo);
                    }
                }
                else if (sAccion=="I"){
                    if (sTipo!="H"){
                        if (iFila==aFila.length-1){
                            nuevaTarea("tblDatos",sTipo);
                        }
                        else{
                            iFila+=1;
                            insertarTarea("tblDatos",sTipo);
                        }
                    }
                }
                break;
            case 38://flecha arriba
                aFila = $I("tblDatos").getElementsByTagName("TR");
                recalcularFilaSeleccionada();
                if (iFila>0){
                    iFila-=1;
                    ms(aFila[iFila]);                 
                    aFila[iFila].cells[0].children[1].focus();
                }
                break; 
            case 40://flecha abajo
                aFila = $I("tblDatos").getElementsByTagName("TR");
                recalcularFilaSeleccionada();
                if (iFila<aFila.length-1){                    
                    iFila+=1;
                    ms(aFila[iFila]);                    
                    aFila[iFila].cells[0].children[1].focus();
                }
                break; 
	    }//switch
	}
	catch(e){
		mostrarErrorAplicacion("Error al actuar sobre la línea", e.message);
	}
}
function recalcularFilaSeleccionada(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        if (iFila<0){
            var aFilas1 = FilasDe("tblDatos");
            for (var i=0;i<aFilas1.length;i++){
                if (aFilas1[i].className == "FS" && aFilas1[i].style.display != "none"){
                    iFilaSeleccionada=i;
                    iNumFilasSeleccionadas++;
                }
            }
            if (iNumFilasSeleccionadas==1){
                iFila=iFilaSeleccionada;
            }
        }
    }
    catch(e){
        iFila=-1;
	    mostrarErrorAplicacion("Error al recalcular fila selecionada", e.message);
    }
}
function flPosHitoPE(){
    //Comprueba si la última línea es un hito de Proyecto Economico y en su caso devuelve el nº de fila
    //En caso contrario devuelve -1
    var aF, bSeguir=true, iUltimaFila=-1, sMargen, sTipoLinea, sEstado;
    try{aF = $I("tblDatos").getElementsByTagName("TR");}
    catch(e){bSeguir=false;}
    try{
        if (!bSeguir)return;
	    for (i=aF.length-1;i>=0;i--){
	        sTipoLinea = getTipo(aF[i].getAttribute("tipo"));
	        sEstado = aF[i].getAttribute("est");
	        sMargen = aF[i].cells[0].children[0].style.marginLeft;
	        if (sMargen=="") sMargen="0px";
	        if (sEstado!="D"){
                if (sTipoLinea=="H"){
                    if (sMargen=="0px"){
                        iUltimaFila=i;
                    }
                }
                i=0;
            }
        } 
        return iUltimaFila;
	}
	catch(e){
	    iFila = -1;
		mostrarErrorAplicacion("Error al comprobar si la última línea es un hito de Proyecto económico", e.message);
	}
}
function Tarea(objTabla,sTipo){
	/* En función del botón clickado y de la opción elegida se realiza una acción sobre el desglose de tareas
	*/
	var sAccion;
	try{
	    if (document.getElementById("ctl00_CPHC_lblTipo").innerHTML=="Proyecto técnico") {
	        if (sTipo=="P"){
	            mmoff("Inf", "En una plantilla de Proyecto técnico no se pueden\nmanejar líneas de Proyecto técnico",380);
	            return;
	        }
	    }
	    sAccion=getRadioButtonSelectedValue("rdbAccion",false);
	    switch(sAccion){
	        case "A":
	            nuevaTarea(objTabla,sTipo);
	            break;
	        case "I":
	            insertarTarea(objTabla,sTipo);
	            break;
	        case "M":
	            modificarTarea(objTabla,sTipo);
	            break;
	        default:
	            mmoff("Inf", "Acción '" + sAccion + "' no contemplada", 320);
	    }
	    activarGrabar();
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al tratar línea", e.message);
	}
}
function subirFilasMarcadas(){
//Recorre las filas marcadas y las va subiendo una a una
	var sTipoProyecto;
	var iFilaAct,iFilaOriginal;
	var bHaySubida=false;
	try{
	    iFilaOriginal=iFila;
	    sTipoProyecto=document.getElementById("ctl00_CPHC_lblTipo").innerHTML;
	    aFila = $I("tblDatos").getElementsByTagName("TR");
	    for (iFilaAct=0;iFilaAct<aFila.length;iFilaAct++){	        
            if (aFila[iFilaAct].className == "FS"){
	            //Si está marcada la primera fila NO se puede subir
	            if (sTipoProyecto=="Proyecto técnico"){
	                if (iFilaAct==1) return;
	            }
	            else {if (iFilaAct==0) return;}
                iFila=iFilaAct;
                subirTarea();
                bHaySubida=true;
            }
        }
        if (bHaySubida){
            nfo--;//Decremento la vble global que indica el nº de fila original
            iFila=iFilaOriginal-1;
            activarGrabar();
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al subir filas marcadas", e.message);
	}
}
function bajarFilasMarcadas(){
//Recorre las filas marcadas y las va bajando una a una
	var nFilas=0, iFilaAct,iFilaOriginal;
	var bHayBajada=false;
	try{
	    iFilaOriginal=iFila;
	    aFila = $I("tblDatos").getElementsByTagName("TR");
	    nFilas=aFila.length - 1;
	    for (iFilaAct=nFilas;iFilaAct>=0;iFilaAct--){	        
            if (aFila[iFilaAct].className == "FS"){
	            //Si está marcada la última fila NO se puede bajar
	            if (iFilaAct==nFilas) return;
                iFila=iFilaAct;
                bajarTarea();
                bHayBajada=true;
            }
        }
        if (bHayBajada){
            nfo++;//Incremento la vble global que indica el nº de fila original
            iFila=iFilaOriginal+1;
            activarGrabar();
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al bajar filas marcadas", e.message);
	}
}

function subirTarea(){
	try{
	    if (document.getElementById("ctl00_CPHC_lblTipo").innerHTML=="Proyecto técnico"){
	        if (iFila==1) return;
	    }
	    else {if (iFila==0) return;}
	    var iFilaAnt=fgGetAntLineaNoBorrada(iFila,true);
	    var oRow=$I("tblDatos").moveRow(iFila,iFilaAnt);
	    
	    activarGrabar();
	    //Recalculo los padres de cada linea
	    comprobarDatos();
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al subir línea", e.message);
	}
}
function bajarTarea(){
	var nFilas=0;
	try{
	    aFila = $I("tblDatos").getElementsByTagName("TR");
	    nFilas=aFila.length - 1;
	    if (iFila==nFilas) return;
	    var iFilaSig=fgGetSigLineaNoBorrada(iFila,true);
	    var oRow=$I("tblDatos").moveRow(iFila,iFilaSig);

	    activarGrabar();
	    //Recalculo los padres de cada linea
	    comprobarDatos();
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al bajar línea", e.message);
	}
}
function insertarTarea(objTabla,sTipo){
	try{
	var sDesTipo,sMargen,sIcono;
	if (iFila<0){
	    mmoff("Inf", "Para insertar una fila debes seleccionar sobre\nque fila se realizará la acción",380);
	    return;
	}
    switch (sTipo){
        case "P":
            sDesTipo="P.T.";
            sMargen="0px";
            break;
        case "F":
            sDesTipo="FASE";
            sMargen="20px";
            break;
        case "A":
            sDesTipo="ACTI.";
            sMargen=getMargen(sTipo, iFila-1);
            break;
        case "H":
            sDesTipo="HITO";
            sMargen="20px";
            break;
        default:
            sDesTipo="TAREA";
            sMargen=getMargen(sTipo, iFila-1);
            break;
    }
	oNF = $I(objTabla).insertRow(iFila);
	oNF.style.height = "20px";
	oNF.style.cursor = "pointer";
	oNF.attachEvent('onclick', mm);
	oNF.attachEvent('onkeydown', accionLinea);
	oNF.ondblclick = function (){mostrarDetalle();};
	
	iFila=oNF.rowIndex;
	//oNF.sAv="T";
	oNF.setAttribute("sOb", "F");
	oNF.setAttribute("tipo", sDesTipo);
	oNF.setAttribute("est", "I");
	oNF.setAttribute("cT", "-1");
	oNF.setAttribute("ord", "-1");
	oNF.setAttribute("p1", "0");
	oNF.setAttribute("p2", "0");
	
	oNC1 = oNF.insertCell(-1);
	//oNC1.style.width = "40px";
	oNC1.style.width = "600px";
    var objTxt2;
    //sIcono=fgGetIcono(sDesTipo, "I", sMargen);
    //objTxt2 = document.createElement(sIcono);
    //oNC1.appendChild(objTxt2);
    oNC1.appendChild(fgGetIcono(sDesTipo, "I", sMargen));

//	oNC2 = oNF.insertCell(-1);
//	oNC2.style.width = "520px";
//	oNC2.appendChild(document.createElement("<input type='text' name='txtD'"+iFila+" id='Desc"+iFila+"' class='txtM' style='width:400px; margin-left:"+sMargen+"' MaxLength='50' value='' onfocus=\"javascript:this.className='txtM';this.select()\" onblur=\"javascript:this.className='txtL'\"; onkeydown='modificarNombreTarea()'>"));
//  oNC2.children[0].focus();
	var oCtrl1 = document.createElement("input");
	oCtrl1.type = "text";
	oCtrl1.setAttribute("style","width:400px;margin-left:5px;");
	oCtrl1.className = "txtL";
	oCtrl1.maxLength = (sTipo == "T") ? "100" : "50";
	oCtrl1.onfocus = function() { this.select() };
	oCtrl1.attachEvent('onkeydown', modificarNombreTarea);

	oNC1.appendChild(oCtrl1);

	var oCtrl2 = document.createElement("input");
	oCtrl2.setAttribute("type", "checkbox");
	oCtrl2.className = "checkTabla";
	oCtrl2.checked = true;
	oCtrl2.onclick = function() { modificarItem() };
	
    oNC3 = oNF.insertCell(-1);
    oNC3.style.width = "50px";
    if (sTipo == "T") {
        oNC3.appendChild(oCtrl2);
	    //oNC3.appendChild(document.createElement("<input type='checkbox' style='width:15' class='checkTabla' checked='true' onclick='modificarItem()'>"));
    }
    else{
	    oNC3.innerText = "";
    }

    var oCtrl3 = document.createElement("input");
    oCtrl3.setAttribute("type", "checkbox");
    oCtrl3.className = "checkTabla";
    oCtrl3.checked = true;
    oCtrl3.onclick = function() { modificarItem() };
    
    oNC4 = oNF.insertCell(-1);
    oNC4.style.width = "50px";
    if (sTipo=="T"){
	    //oNC4.appendChild(document.createElement("<input type='checkbox' style='width:15' class='checkTabla' checked='true' onclick='modificarItem()'>"));
        oNC4.appendChild(oCtrl3);
    }
    else{
	    oNC4.innerText = "";
    }

    ms(oNF);
    
    comprobarDatos();
	activarGrabar();
	oNC1.children[1].focus();
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al insertar línea", e.message);
	}
}
function insertarPtInvisible(objTabla){
	try{
	var sDesTipo,sMargen,sIcono;
	//Si la tabla ya tiene elementos no inserto la linea de PT por defecto
    aFila = $I("tblDatos").getElementsByTagName("TR");
    nFilas=aFila.length;
    if (nFilas>0) return;
    
	iFila=0;
	
    sDesTipo="P.T.";
    sMargen="0px";
	oNF = $I(objTabla).insertRow(iFila);
	oNF.style.height = "20px";
	oNF.style.cursor = "pointer";
	oNF.style.display = "none";

	oNF.setAttribute("tipo", sDesTipo);
	oNF.setAttribute("est", "I");
	oNF.setAttribute("cT", "-1");
	oNF.setAttribute("ord", "0");
	oNF.setAttribute("p1", "0");
	oNF.setAttribute("p2", "0");
	
	oNC1 = oNF.insertCell(-1);
	//oNC1.style.width = "40px";
	oNC1.style.width = "600px";
    
    //var objTxt2;
    //sIcono=fgGetIcono(sDesTipo,"I", "0");
    //objTxt2 = document.createElement(sIcono);
	//oNC1.appendChild(objTxt2);
    oNC1.appendChild(fgGetIcono(sDesTipo,"I", "0"));
    
//	oNC2 = oNF.insertCell(-1);
//	oNC2.style.width = "600px";
//	oNC2.appendChild(document.createElement("<input type='text' name='txtD'"+iFila+" id='Desc"+iFila+"' class='txtM' style='width:400px; margin-left:"+sMargen+"' MaxLength='50' value='LINEA DE P.T.'>"));
//	oNC1.appendChild(document.createElement("<input type='text' class='txtL' style='width:400px;' MaxLength='50' value='LINEA DE P.T.'>"));

	var oCtrl1 = document.createElement("input");
	oCtrl1.type = "text";
	oCtrl1.style.width = "400px;";
	oCtrl1.className = "txtL";
	oCtrl1.maxLength = "50";
	oCtrl1.value = 'LINEA DE P.T.'
	oNC1.appendChild(oCtrl1);
    
    oNC2 = oNF.insertCell(-1);
    oNC2.style.width = "50px";
    oNC2.innerText = " ";
    oNC3 = oNF.insertCell(-1);
    oNC3.style.width = "50px";
    oNC3.innerText = " ";
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al insertar línea de P.T.", e.message);
	}
}
function nuevaTarea(objTabla,sTipo){
	try{
	var iPosHitoPE=-1,sDesTipo,sMargen,sIcono, bPlantPT=false;
	
	iPosHitoPE=flPosHitoPE();
	if (iPosHitoPE>=0){
	    iFila=iPosHitoPE;
	    oNF = $I(objTabla).insertRow(iFila);
	}
	else oNF = $I(objTabla).insertRow(-1);
    iFila=oNF.rowIndex;
    if ($I("lblTipo").value=="Proyecto técnico") bPlantPT=true;
    switch (sTipo){
        case "P":
            sDesTipo="P.T.";
            sMargen="0px";
            break;
        case "F":
            sDesTipo="FASE";
            sMargen="20px";
            break;
        case "A":
            sDesTipo="ACTI.";
            sMargen=getMargen(sTipo, iFila-1);
            break;
        case "H":
            sDesTipo="HITO";
//            if (bPlantPT)sMargen="20px";
//            else sMargen="0px";
            sMargen=getMargen(sTipo, iFila-1);
            break;
        default:
            sDesTipo="TAREA";
            sMargen=getMargen(sTipo, iFila-1);
            break;
    }
    oNF.style.height = "20px";
	oNF.style.cursor = "pointer";
	oNF.attachEvent('onclick', mm);
	oNF.attachEvent('onkeydown', accionLinea);	
	oNF.ondblclick = function (){mostrarDetalle();};

	oNF.setAttribute("sOb", "F");
	oNF.setAttribute("tipo", sDesTipo);
	oNF.setAttribute("est", "I");
	oNF.setAttribute("cT", "-1");
	oNF.setAttribute("ord", "-1");
	oNF.setAttribute("p1", "0");
	oNF.setAttribute("p2", "0");


	oNC1 = oNF.insertCell(-1);
//	oNC1.style.width = "40px";
	oNC1.style.width = "600px";
    //var objTxt2;
	//sIcono=fgGetIcono(sDesTipo,"I", sMargen);
	//objTxt2 = document.createElement(sIcono);
	//oNC1.appendChild(objTxt2);
	oNC1.appendChild(fgGetIcono(sDesTipo, "I", sMargen));
	
//	oNC2 = oNF.insertCell(-1);
//	oNC2.style.width = "520px";
//	oNC2.appendChild(document.createElement("<input type='text' name='txtD'"+iFila+" id='Desc'"+iFila+" class='txtTabla' style='width:400px; margin-left:"+sMargen+"' MaxLength='50' value='' onfocus=\"javascript:this.className='txtM';this.select()\" onblur=\"javascript:this.className='txtL'\" onkeydown='modificarNombreTarea()'>"));
//  oNC2.children[0].focus();
   
//    oNC1.appendChild(document.createElement("<input type='text' class='txtL' style='width:400px;margin-left:5px;' MaxLength='50' value='' onfocus='this.select()' onkeydown='modificarNombreTarea()'>"));

    var oCtrl1 = document.createElement("input");
    oCtrl1.type = "text";
    oCtrl1.setAttribute("style","width:400px;margin-left:5px;");
    oCtrl1.className = "txtL";
    oCtrl1.maxLength = "50";

    oCtrl1.onfocus = function() { this.select() };
    oCtrl1.attachEvent('onkeydown', modificarNombreTarea);

    oNC1.appendChild(oCtrl1);

    var oCtrl2 = document.createElement("input");
    oCtrl2.setAttribute("type", "checkbox");
    oCtrl2.className = "checkTabla";
    oCtrl2.checked = true;
    oCtrl2.onclick = function() { modificarItem() };

    oNC3 = oNF.insertCell(-1);
    oNC3.style.width = "50px";
    if (sTipo == "T") {
        oNC3.appendChild(oCtrl2);
	    //oNC3.appendChild(document.createElement("<input type='checkbox' style='width:15' class='check' checked='true' onclick='modificarItem()'>"));
    }
    else{
	    oNC3.innerText = " ";
    }
    
    var oCtrl3 = document.createElement("input");
    oCtrl3.setAttribute("type", "checkbox");
    oCtrl3.className = "checkTabla";
    oCtrl3.checked = true;
    oCtrl3.onclick = function() { modificarItem() };
    
    oNC4 = oNF.insertCell(-1);
    oNC4.style.width = "50px";
    if (sTipo == "T") {
        oNC4.appendChild(oCtrl3);
	    //oNC4.appendChild(document.createElement("<input type='checkbox' style='width:15' class='check' checked='true' onclick='modificarItem()'>"));
    }
    else{
	    oNC4.innerText = " ";
    }

    ms(oNF);   
	activarGrabar();
	comprobarDatos();
	oNC1.children[1].focus();
	}
	catch(e){
	    iFila = -1;
		mostrarErrorAplicacion("Error al añadir línea", e.message);
	}
}

function modificarTarea(objTabla,sTipo){
	var sEstado="",sDesTipo,sIcono, bPlantPT=false,sFacturable=" ";
	try{
	    if (iFila<0){
	        mmoff("Inf", "Para modificar una fila debe seleccionar sobre\nque fila se realizará la acción",380);
	        return;
	    }
	    if ($I("lblTipo").value=="Proyecto técnico") bPlantPT=true;
	    aFila = $I("tblDatos").getElementsByTagName("TR");
	    //Solo permitimos modificar tipo de linea para lineas nuevas
	    //sEstado = aFila[iFila].cells[2].children[0].value;
	    sEstado = aFila[iFila].getAttribute("est");
	    if (sEstado=="I"){
            switch (sTipo){
                case "P":
                    sDesTipo="P.T.";
                    sMargen="0px";
                    break;
                case "F":
                    sDesTipo="FASE";
                    sMargen="20px";
                    break;
                case "A":
                    sDesTipo="ACTI.";
                    sMargen="20px";
                    break;
                case "T":
                    sDesTipo="TAREA";
                    sMargen="20px";
                    sFacturable="<input type='checkbox' style='width:15' name='chk" + iFila + "' id='chk" + iFila + "' class='checkTabla' checked='true'>";
                    break;
                case "H":
                    sDesTipo="HITO";
                    if (bPlantPT)sMargen="20px";
                    else sMargen="0px";
                    break;
                default:
                    sDesTipo="TAREA";
                    sMargen="20px";
                    break;
            }
            aFila[iFila].setAttribute("tipo", sDesTipo);
	        sIcono=fgGetIcono2(sDesTipo);
            aFila[iFila].cells[0].children[0].src=sIcono;
	        aFila[iFila].cells[0].children[0].style.marginLeft = sMargen;
	        aFila[iFila].cells[1].innerHTML=sFacturable;
	        activarGrabar();
	        comprobarDatos();
	    }
	    else{
	        mmoff("Inf", "Solo se puede cambiar el tipo de la línea para líneas nuevas\nPara líneas ya existentes deberás borrar e insertar.",400);
	    }
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar tipo de línea", e.message);
	}
}
/*
function fOver(objFila){
	TTip(objFila);
}
function fOut(objFila){
}
function TTip(objFila){
	try{
		event.srcElement.title = event.srcElement.innerText;
	}
	catch(e){};
}
*/
function desplazarFilasMarcadas(sTipo){
//Recorre las filas marcadas y las va desplazando una a una ala izda o dcha segun el parametro
	var iFilaAct;
	try{
	    aFila = $I("tblDatos").getElementsByTagName("TR");
	    for (iFilaAct=0;iFilaAct<aFila.length;iFilaAct++){	        
            if (aFila[iFilaAct].className == "FS"){
                iFila=iFilaAct;
                desplazarTarea(sTipo);
            }
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al desplazar filas marcadas", e.message);
	}
}
function desplazarTarea(sTipo){
/*De las fila seleccionadas establece el margen izquierdo para la descripción 
  sTipo indica si el desplazamiento es hacia la derecha (D) o hacia la izquierda (I)
  P.T. -> margen=0px
  FASE -> margen=20px
  ACTI. -> margen=40px o 20 px
  TAREA -> margen=60px o 40 px o 20px
  HITO -> margen=80px o 60px o 40 px o 20px o 0px(solo si PE)
*/    
    try{
        var sAux="",sEstado,sTipoTarea,sIcono,bPlantPT=false;
        var nMargen=0,intPos=0;
        if (iFila==-1){
            mmoff("Inf", "Debes seleccionar una fila", 180);
            return;
        }
       	aFila = $I("tblDatos").getElementsByTagName("TR");
       	//Recojo el tipo de linea actual
       	sTipoTarea = aFila[iFila].getAttribute("tipo");
       	//Recojo el margen actual y lo transformo a numerico
       	sAux=String(aFila[iFila].cells[0].children[0].style.marginLeft);
       	if (sAux=="") sAux="0px";
       	//Recojo el estado actual
       	sEstado = aFila[iFila].getAttribute("est");
       	
       	if (sAux=="") sAux="0px";
       	intPos = sAux.indexOf("p");
       	sAux=sAux.substring(0,intPos);
       	nMargen=Number(sAux);
       	//Calculo el nuevo margen
       	if (sTipo=='D') nMargen +=20;
       	else nMargen -=20;
       	//Compruebo que el margen sea válido
       	if (nMargen > 80) {nMargen=80;return;}
       	if (nMargen < 0) {nMargen=0;return;}
        
   	    switch(sTipoTarea){
   	        case "P.T.":
   	            if (nMargen!=0) return;
   	            break;
   	        case "FASE":
   	            if (nMargen!=20) return;
   	            break;
   	        case "ACTI.":
   	            if (nMargen!=20 && nMargen!=40) return;
   	            break;
   	        case "TAREA":
   	            if (nMargen!=20 && nMargen!=40 && nMargen!=60) return;
   	            break;
            case "HITO":
                //los hitos de tarea pueden estar en cualquier nivel salvo el cero que solo puede ser en PE
                if ($I("lblTipo").value=="Proyecto técnico") bPlantPT=true;
                if (bPlantPT){
                    if (nMargen!=20 && nMargen!=40 && nMargen!=60 && nMargen!=80) return;
                }
                else{
                    if (nMargen!=0 && nMargen!=20 && nMargen!=40 && nMargen!=60 && nMargen!=80) return;
                }
                break;
   	        default:
   	            return;
   	    }
       	activarGrabar();
       	
       	sAux=nMargen + 'px';
       	//Asigno el nuevo margen a la celda
        aFila[iFila].cells[0].children[0].style.marginLeft = sAux;
        //Marco la fila para updatear
        if (sEstado != "I" && sEstado != "D") aFila[iFila].setAttribute("est", "U");
        
        comprobarDatos();
        activarGrabar();
    }
    catch(e){alert(e.message);};
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    if (strResultado==""){
        ocultarProcesando();
        return;
    }
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        if (aResul[3] != null){
            jqConfirm("", aResul[2], "", "", "war", 450).then(function (answer) {
                if (answer)
                    setTimeout("recargarPlantilla();", 50);
            });
        }
        else mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                //La función Grabar de servidor devuelve el HTML de la lista de tareas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divHitos").children[0].innerHTML = aResul[3];
                
                empadrarLineas("ORIGINAL");
                if (bDetalle) {
                    bDetalle = false;
                    setTimeout("mostrarDetalle()", 2500);
                }
                else {
                    if (bDetalleHito) {
                        bDetalleHito = false;
                        setTimeout("mostrarDetalleHito()", 2500);
                    }
                }
                iFila2 = -1;
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) AccionBotonera("regresar", "P");

                break;
                
//            case "grabarcomo":
//                //ACTUALIZO EL CODIGO DE PLANTILLA
//                $I("hdnIDPlantilla").value= aResul[2];
//                //RECARGO LOS ITEMS DE LA NUEVA PLANTILLA
//                $I("divCatalogo").children[0].innerHTML = aResul[3];
//                $I("divHitos").children[0].innerHTML = aResul[4];
//                empadrarLineas("ORIGINAL");
//                break;
            case "plantilla":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
                empadrarLineas("ORIGINAL");
                break;
            case "setResolucion":
                location.reload(true);
                break;
        }
        ocultarProcesando();
    }
}
function recargarPlantilla(){
    try{
        var js_args = "plantilla//";
        js_args += $I("hdnIDPlantilla").value +"//"; 
        if ($I("lblTipo").value=="Proyecto técnico")js_args +="T";
        else js_args +="E";
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
	}
	catch(e){
		mostrarErrorAplicacion("Error al recargar la plantilla.", e.message);
	}

}
function empadrarLineas(sTipo){
/*Asigna nº de linea padre a cada linea del desglose
    Si sTipo="ORIGINAL" cargamos las columans de padre actual y padre original
    Sino solo la de padre actual
*/
    var sAux,sEstado,sMargen,sTipo,sDesc;
    var intPos=0,nMargen=0,padre0=0,padre1=0,padre2=0,padre3=0,nTareas=0,nCol,iPadreAct,iPadreOri;
    try{
        aFila = $I("tblDatos").getElementsByTagName("TR");
        nTareas=aFila.length;
        
        for (i=0;i<nTareas;i++){
            sDesc = aFila[i].cells[0].children[1].value;
            sEstado = aFila[i].getAttribute("est");
            if (sEstado!="D"){
                sMargen=String(aFila[i].cells[0].children[0].style.marginLeft);
                if (sMargen==""){
                    sMargen="0px";
                    nMargen=0;
                }
                else{
                    intPos = sMargen.indexOf("p");
       	            sAux=sMargen.substring(0,intPos);
       	            nMargen=Number(sAux);
                }
                aFila[i].getAttribute("p1","0");
                if (sTipo == "ORIGINAL") aFila[i].setAttribute("p2", "0");
                switch (nMargen)
                {
                    case 0:
                        padre0 = i;
                        break;
                    case 20:
                        padre1 = i;
                        aFila[i].setAttribute("p1", padre0);
                        if (sTipo=="ORIGINAL") aFila[i].p2 = padre0;
                        break;
                    case 40:
                        padre2 = i;
                        aFila[i].setAttribute("p1", padre1);
                        if (sTipo == "ORIGINAL") aFila[i].setAttribute("p2", padre1);
                        break;
                    case 60:
                        padre3 = i;
                        aFila[i].setAttribute("p1", padre2);
                        if (sTipo == "ORIGINAL") aFila[i].setAttribute("p2", padre2);
                        break;
                    case 80:
                        aFila[i].setAttribute("p1", padre3);
                        if (sTipo == "ORIGINAL") aFila[i].setAttribute("p2", padre3);
                        break;
                    default:
                        aFila[i].cells[0].children[1].style.color="Red";
                }//switch (nMargen)
            }//if
        }//for            
    }
	catch(e){
		mostrarErrorAplicacion("Error al comprobar la paternidad de las lineas\n"+sDesc, e.message);
    }
}
function comprobarDatos(){
/*Recorre la tabla de desglose para comprobar si todas las líneas son correctas
    Las que no lo sean quedarán marcadas en rojo
    En primer lugar asigna a cada linea el nº de linea de su padre
*/
    var bRes=true;
    try{
        empadrarLineas("ACTUAL");
        bRes=comprobarDatos2();
        if (bRes){
            if (!comprobarDatosHitos2()){
                mmoff("War", "Todos los hitos especiales deben tener denominación.",400);
                 return false;
            }
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al comprobar la paternidad de las lineas", e.message);
		bRes=false;
    }
    return bRes;
}
function comprobarDatos2(){
/*Recorre la tabla de desglose para comprobar si todas las líneas son correctas
    Las que no lo sean quedarán marcadas en rojo
*/
    var bHayError=false,bLineaOk=true;
    var sAmbito,sTipo,sDes,sAux,sEstado,sMargen,sMargenAux;
    var intPos=0,nMargen=0,nTareas=0,nMargenAux,iFilaSig,iFilaAnt;
    try{
        if (document.getElementById("ctl00_CPHC_lblTipo").innerHTML=="Proyecto técnico") sAmbito="T";
        else sAmbito="E";
    
        aFila = $I("tblDatos").getElementsByTagName("TR");
        nTareas=aFila.length;
        
        for (i=0;i<nTareas;i++){
            bLineaOk=true;
            aFila[i].cells[0].children[1].style.color="Black";
            sDes = aFila[i].cells[0].children[1].value;
            sTipo = aFila[i].getAttribute("tipo");
            switch (sTipo){
                case "P.T.":
                    sTipo="P";
                    break;
                case "FASE":
                    sTipo="F";
                    break;
                case "ACTI.":
                    sTipo="A";
                    break;
                case "TAREA":
                    sTipo="T";
                    break;
                case "HITO":
                    sTipo="H";
                    break;
                default:
                    mmoff("Inf", "La línea " + i + "\n" + sDes + "\nno es correcta",400);
                    break;
            }
            sMargen=String(aFila[i].cells[0].children[0].style.marginLeft);
            if (sMargen==""){
                sMargen="0px";
                nMargen=0;
            }
            else{
                intPos = sMargen.indexOf("p");
   	            sAux=sMargen.substring(0,intPos);
   	            nMargen=Number(sAux);
   	        }
   	        sEstado = aFila[i].getAttribute("est");
            //empadrarLineas();
            if (sEstado!="D"){    
                switch (sTipo){
                case "P":
                    break;
                case "F":
                    if (i == 0 && sAmbito=="E") bLineaOk=false;
                    if (nMargen != 20)
                    {//Las fases deben estar identadas a 20 
                        bLineaOk=false;
                    }
                    if (bLineaOk)
                    {//Una fase nunca puede ser la última linea de un desglose
                        if (i == nTareas - 1) bLineaOk=false;
                        else
                        {//De una fase debe colgar al menos una actividad
                            iFilaSig=fgGetSigLineaNoBorrada(i,false);
                            if (iFilaSig==i){bLineaOk=false;}
                            else{
                                sMargenAux=String(aFila[iFilaSig].cells[0].children[0].style.marginLeft);
                                if (sMargenAux==""){
                                    sMargenAux="0px";
                                    nMargenAux=0;
                                }
                                else{
                                    intPos = sMargenAux.indexOf("p");
       	                            sAux=sMargenAux.substring(0,intPos);
       	                            nMargenAux=Number(sAux);
                                }
                                if ((aFila[iFilaSig].getAttribute("tipo") != "ACTI.") || (nMargenAux != nMargen + 20))
                                    bLineaOk=false;
                            }
                        }
                    }
                    if (bLineaOk)
                    {//Una fase debe colgar de un Proy. Técnico
                        iPadre=aFila[i].getAttribute("p1");
                        if (aFila[iPadre].getAttribute("tipo") != "P.T.") bLineaOk = false;
                        else{//si el padre está borrado es como si no tuviera
                            if (aFila[iPadre].getAttribute("est") == "D") bLineaOk = false;
                        }
                    }
                    break;
                case "A":
                    if (sAmbito == "E" && i == 0) bLineaOk=false;
                    if (sAmbito == "T" && i == 1 && nMargen != 20) bLineaOk=false;
                    if (nMargen != 20 && nMargen != 40)
                    {//Las actividades deben estar identadas a 20 o 40 
                        bLineaOk=false;
                    }
                    if (bLineaOk)
                    {//Una actividad nunca puede ser la última linea de un desglose
                        if (i == nTareas - 1) bLineaOk=false;
                        else
                        {//De una actividad debe colgar al menos una tarea
                            iFilaSig=fgGetSigLineaNoBorrada(i,false);
                            if (iFilaSig==i){bLineaOk=false;}
                            else{
                                sMargenAux=String(aFila[iFilaSig].cells[0].children[0].style.marginLeft);
                                if (sMargenAux==""){
                                    sMargenAux="0px";
                                    nMargenAux=0;
                                }
                                else{
                                    intPos = sMargenAux.indexOf("p");
       	                            sAux=sMargenAux.substring(0,intPos);
       	                            nMargenAux=Number(sAux);
       	                        }
       	                        if ((aFila[iFilaSig].getAttribute("tipo") != "TAREA") || (nMargenAux != nMargen + 20))
                                    bLineaOk=false;
                            }
                        }
                    }
                    if (bLineaOk)
                    {//Una actividad debe colgar de un Proy. Técnico o de una Fase
                        iPadre=aFila[i].getAttribute("p1");
                        if (aFila[iPadre].getAttribute("tipo") != "P.T." && aFila[iPadre].getAttribute("tipo") != "FASE")
                            bLineaOk=false;
                        else{//si el padre está borrado es como si no tuviera
                            if (aFila[iPadre].getAttribute("est") == "D") bLineaOk = false;
                        }
                    }
                    break;

                case "H":
                    if (i == 0) bLineaOk=false;
                    if ((sAmbito == "T") && (i==1))bLineaOk=false;
                    //Solo permito hitos de identación cero como última linea  
                    if ((i<nTareas-1) && nMargen==0) bLineaOk=false;
                    //Solo permito hitos de identación cero como última linea y en plantillas de PE
                    if (nMargen==0){
                        if (sAmbito == "T")bLineaOk=false;
                        else{
                            if (i<nTareas-1) bLineaOk=false;
                        }
                    }
                    else{
                        if (bLineaOk)
                        {//La linea anterior de un HITO solo puede ser una tarea o un hito
                            iFilaAnt=fgGetAntLineaNoBorrada(i,true);
                            if (iFilaAnt==i){bLineaOk=false;}
                            else{

                                if (aFila[iFilaAnt].getAttribute("tipo") != "TAREA" && aFila[iFilaAnt].getAttribute("tipo") != "HITO")
                                    {bLineaOk=false;
                                    } 
                                else
                                {
                                    sMargenAux=String(aFila[iFilaAnt].cells[0].children[0].style.marginLeft);
                                    if (sMargenAux==""){
                                        sMargenAux="0px";
                                        nMargenAux=0;
                                    }
                                    else{
                                        intPos = sMargenAux.indexOf("p");
                                        sAux=sMargenAux.substring(0,intPos);
                                        nMargenAux=Number(sAux);
                                    }
                                    if (aFila[iFilaAnt].getAttribute("tipo") == "HITO")
                                    {
                                        if  (nMargen >= nMargenAux)
                                        {//Si su padre es un hito La identación de su padre debe ser superior
                                            bLineaOk=false;
                                        }
                                    }
                                    else//es una tarea
                                    {
                                        if (nMargen > nMargenAux + 20)
                                        {//El hito no debe estar identado con respecto a la tarea mas de una posición
                                            bLineaOk=false;
                                        }
                                    }
                                }
                            }//else if (iFilaAnt==i)
                            //El elemento siguiente a un hito no puede tener una identación >= a la del hito
                            if (bLineaOk){
                                if (i<nTareas-1){
                                    iFilaSig=fgGetSigLineaNoBorrada(i,true);
                                    if (i!=iFilaSig){
                                        sMargenAux=String(aFila[iFilaSig].cells[0].children[0].style.marginLeft);
                                        if (sMargenAux==""){
                                            sMargenAux="0px";
                                            nMargenAux=0;
                                        }
                                        else{
                                            intPos = sMargenAux.indexOf("p");
                                            sAux=sMargenAux.substring(0,intPos);
                                            nMargenAux=Number(sAux);
                                        }
                                        if  (nMargen <= nMargenAux){
                                            bLineaOk=false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "T":
                        if (i == 0)bLineaOk=false;
                        if (i == 1 && nMargen != 20) bLineaOk=false;
                        if (nMargen != 20 &&nMargen != 40 &&nMargen != 60) bLineaOk=false;
                        if (i>1 && bLineaOk)//no es la primera fila
                        {//El padre de una tarea no puede ser una fase
                            iFilaAnt=fgGetAntLineaNoBorrada(i,false);
                            if (iFilaAnt==i){bLineaOk=false;}
                            else{
                                if (aFila[iFilaAnt].cells[0].children[0].value == "F")
                                    bLineaOk=false;
                                else
                                {
                                    sMargenAux=String(aFila[iFilaAnt].cells[0].children[0].style.marginLeft);
                                    if (sMargenAux==""){
                                        sMargenAux="0px";
                                        nMargenAux=0;
                                    }
                                    else{
                                        intPos = sMargenAux.indexOf("p");
   	                                    sAux=sMargenAux.substring(0,intPos);
   	                                    nMargenAux=Number(sAux);
   	                                }
   	                                if (((aFila[iFilaAnt].getAttribute("tipo") == "P.T.") || (aFila[iFilaAnt].getAttribute("tipo") == "ACTI.")) && (nMargenAux < nMargen - 20))
                                    {//La identación de su padre debe ser un punto superior
                                        bLineaOk=false;
                                    }
                                    else
                                    {
                                        if ((aFila[iFilaAnt].getAttribute("tipo") == "TAREA") && (nMargenAux < nMargen))
                                        {//Una tarea no puede colgar de otra tarea
                                            bLineaOk=false;
                                        }
                                    }
                                }
                            }
                        }
                        if (bLineaOk)
                        {//Una tarea debe colgar de un Proy. Técnico o de una Actividad
                            iPadre = aFila[i].getAttribute("p1");
                            if (aFila[iPadre].getAttribute("tipo") != "P.T." && aFila[iPadre].getAttribute("tipo") != "ACTI.")
                                bLineaOk=false;
                        }
                        break;
                    default:
                        
                    bLineaOk=false;
                }//switch 
            }//if
            if (!bLineaOk){
                bHayError = true;
                aFila[i].cells[0].children[1].style.color="Red";
            }
        }//for
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    if (bHayError) return false;
    else return true;
}

function grabar(){
/* Para el callback paso una cadena en la que el primer valor es el código de plantilla
   y el resto es una repetición (una por cada fila del grid) de la cadena 
   grabar@#@sEstado@#@sTipo@#@sDes@#@sEsHijo@#@sCodigo@#@sOrden@#@sMargen##";
*/    
    var js_args="";
    try{
        if (!bGrabable)return false;
        
        if (!comprobarDatos()) 
            {alert("El desglose de la plantilla no es correcto\nRevisa las líneas en rojo");
             return;
            }
        //RealizarCallBack();  //Sin argumentos
        js_args = "grabar//" + $I("hdnIDPlantilla").value + "//";
        js_args+=flGetCadenaDesglose()+ "//";
        js_args+=flGetCadenaHitosEspeciales();
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
        desActivarGrabar();
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}

function eliminarTarea(objTabla){
	var sEstado;
	try{
//	    if (iFila==-1){
//            alert("Debe seleccionar una fila");
//            return;
//        }
        aFila = $I("tblDatos").getElementsByTagName("TR");
        for (i=aFila.length - 1;i>=0;i--){
            if ((aFila[i].className == "FS")&&(aFila[i].style.display != "none")){
                //Si es una fila que ya existe en BBDD la marco para borrado, sino la elimino
                sEstado = aFila[i].getAttribute("est");
                if (sEstado=="I") $I(objTabla).deleteRow(i);
                else {
                    aFila[i].setAttribute("est", "D");
                    aFila[i].style.display = "none";
                }
            }
        }
	    comprobarDatos();
	    iFila = -1;
	    activarGrabar();
	}
	catch(e){
		mostrarErrorAplicacion("Error al eliminar línea del desglose", e.message);
	}
}  
function getMargen(sTipo,iFilaAnt){
// Lee el margen y tipo de la fila anterior    
    var bEsHijo=false;
    var intPos;
    var sTipoAnt,sMargenAnt,sMargen="20px",sAux;
    
    try{
        if (iFilaAnt>=0){
            aFila = $I("tblDatos").getElementsByTagName("TR");
            sTipoAnt = aFila[iFilaAnt].getAttribute("tipo");
            sMargenAnt=aFila[iFilaAnt].cells[0].children[0].style.marginLeft;
            if (sMargenAnt=="")sMargenAnt="0px";
            if (esHijo(sTipo,sTipoAnt)){
                //Devuelvo el margen del padre +20
                    intPos = sMargenAnt.indexOf("p");
       	            sAux=sMargenAnt.substring(0,intPos);
       	            nMargen=Number(sAux);
       	            nMargen+=20;
                    sMargen=nMargen+ "px";
            }
            else{
                if (esHermano(sTipo,sTipoAnt)){
                //Devuelvo el margen del padre 
                    sMargen=sMargenAnt;
                }
            }
        }
        return sMargen;
	}
	catch(e){
		mostrarErrorAplicacion("Error al obtener el margen de la nueva línea", e.message);
	}
}
function getTipo(sTipo){
    var sRes="";
    try{
        switch(sTipo){
            case "P","P.T.":
                sRes="P";
                break;
            case "F","FASE":
                sRes="F";
                break;
            case "A","ACTI.":
                sRes="A";
                break;
            case "T","TAREA":
                sRes="T";
                break;
            case "H","HITO":
                sRes="H";
                break;
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al obtener el tipo de la línea", e.message);
	}
	return sRes;
}
function esHijo(sTipo,sTipoAnt){
    var bEsHijo=false;
    switch(sTipo){
        case "P":break;
        case "F":break;
        case "A":
            if (sTipoAnt=="P.T." || sTipoAnt=="FASE") bEsHijo=true;
            break;
        case "T":
            if (sTipoAnt=="P.T." || sTipoAnt=="ACTI.") bEsHijo=true;
            break;
        case "H":
            if (sTipoAnt=="TAREA") bEsHijo=true;
            break;
    }
    return bEsHijo;
}
function esHermano(sTipo,sTipoAnt){
    var bEsHermano=false;
    switch(sTipo){
        case "P":break;
            if (sTipoAnt=="P" || sTipoAnt=="P.T.") bEsHermano=true;
            break;
        case "F":break;
            if (sTipoAnt=="F" || sTipoAnt=="FASE") bEsHermano=true;
            break;
        case "A":
            if (sTipoAnt=="A" || sTipoAnt=="ACTI.") bEsHermano=true;
            break;
        case "T":
            if (sTipoAnt=="T" || sTipoAnt=="TAREA") bEsHermano=true;
            break;
        case "H":
            if (sTipoAnt=="H" || sTipoAnt=="HITO") bEsHermano=true;
            break;
    }
    return bEsHermano;
}
function flGetCadenaDesglose(){
/*Recorre la tabla de desglose para obtener una cadena que se pasará como parámetro
  al procedimiento de grabación
*/
var sRes="",sTipo,sDes,sAux,sEstado,sCodigo,sMargen,sOrden,sFacturable,sAvance,sObligaEst;
try{
    aFila = $I("tblDatos").getElementsByTagName("TR");
    for (i=0;i<aFila.length;i++){
        sFacturable="F";
        sAux = aFila[i].getAttribute("tipo");
        sDes = aFila[i].cells[0].children[1].value;
        sEstado = aFila[i].getAttribute("est");
        sCodigo = aFila[i].getAttribute("cT");
        sAvance="F";
        sObligaEst="F";
        //Si el padre actual <> padre original y el estado es N (no modificado) lo paso a U (updatear)
        if (aFila[i].style.display != "none"){
            if (sEstado=="N"){
                if (aFila[i].getAttribute("p1") != aFila[i].getAttribute("p2")) sEstado = "U";
            }
        }
        sOrden = aFila[i].getAttribute("ord");
        //Recojo el margen actual para saber si es un elemento hijo
        sMargen=String(aFila[i].cells[0].children[0].style.marginLeft);
        if (sMargen=="") sMargen="0px";
        switch(sAux){
            case "P.T.":
                sTipo="P";
                sObligaEst = aFila[i].getAttribute("sOb");
                break
            case "FASE":
                sTipo="F";
                break
            case "ACTI.":
                sTipo="A";
                break
            case "TAREA":
                sTipo="T";
                if (aFila[i].cells[1].children[0].checked)sFacturable="T";
                //sAvance = aFila[i].sAv;
                if (aFila[i].cells[2].children[0].checked)sAvance="T";
                break
            case "HITO":
                sTipo="H";
                break
            default: mmoff("Inf", "Cualificador " + sAux + " no contemplado",380);
        }//switch
        sRes += sEstado+ "@#@" + sTipo + "@#@" + Utilidades.escape(sDes) + "@#@"+ sCodigo + "@#@" + sOrden +"@#@"+sMargen+"@#@"+sFacturable+
                "@#@"+sAvance+"@#@"+sObligaEst+ "##";
    }//for
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
}
}
function fgGetIcono(sTipo,sEstado, sMargen)
{//En función del tipo de linea y de sus estado devuelve una cadena con el HTML del icono a aplicar
    //var sRes;
    var oImgIco = document.createElement("img");
try{
    switch (sEstado){
        case "I":
            switch (sTipo) {
                case "P.T.":
                    //sRes = "<img src='../../../../Images/imgProyTecN.gif' border='0' title='P.T.' style='cursor: url(../../../../images/imgManoAzul2.cur);vertical-align:middle'>";
                    oImgIco.setAttribute("src", "../../../../images/imgProyTecN.gif");
                    oImgIco.setAttribute("title", "P.T.");
                    oImgIco.border = "0";
                    oImgIco.setAttribute("style","cursor: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle");
                    break;
                case "FASE":
                    //sRes = "<img src='../../../../Images/imgFaseN.gif' border='0' title='Fase' style='vertical-align:middle;margin-left:20px'>";
                    oImgIco.setAttribute("src", "../../../../images/imgFaseN.gif");
                    oImgIco.setAttribute("title", "Fase");
                    oImgIco.border = "0";
                    oImgIco.setAttribute("style","vertical-align:middle;margin-left:20px");                    
                    break;
                case "ACTI.":
                    //sRes = "<img src='../../../../Images/imgActividadN.gif' border='0' title='Actividad' style='vertical-align:middle;margin-left:" + sMargen + "'>";
                    oImgIco.setAttribute("src", "../../../../images/imgActividadN.gif");
                    oImgIco.setAttribute("title", "Actividad");
                    oImgIco.border = "0";
                    oImgIco.setAttribute("style", "vertical-align:middle;margin-left:" + sMargen); 
                    break;
                case "TAREA":
                    //sRes = "<img src='../../../../Images/imgTareaN.gif' border='0' title='Tarea' style='cursor: url(../../../../images/imgManoAzul2.cur);vertical-align:middle;margin-left:" + sMargen + "'>";
                    oImgIco.setAttribute("src", "../../../../images/imgTareaN.gif");
                    oImgIco.setAttribute("title", "Tarea");
                    oImgIco.border = "0";
                    oImgIco.setAttribute("style", "cursor: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle;margin-left:" + sMargen); 
                    break;
                case "HITO":
                    //sRes = "<img src='../../../../Images/imgHitoN.gif' border='0' title='Hito' style='cursor: url(../../../../images/imgManoAzul2.cur);vertical-align:middle;margin-left:" + sMargen + "'>";
                    oImgIco.setAttribute("src", "../../../../images/imgHitoN.gif");
                    oImgIco.setAttribute("title", "Hito");
                    oImgIco.border = "0";
                    oImgIco.setAttribute("style","cursor: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle;margin-left:" + sMargen);                    
                    break;
            }
            break;
    case "U": 
        switch (sTipo)
        {
            case "P.T.":
                //sRes="<img src='../../../../Images/imgProyTec.gif' border='0' title='P.T.' style='cursor: url(../../../../images/imgManoAzul2.cur);vertical-align:middle;margin-left:0px'>";
                oImgIco.setAttribute("src", "../../../../images/imgProyTec.gif");
                oImgIco.setAttribute("title", "P.T.");
                oImgIco.border = "0";
                oImgIco.setAttribute("style","cursor: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle;margin-left:0px");                
                break;
            case "FASE":
                //sRes="<img src='../../../../Images/imgFase.gif' border='0' title='Fase' style='vertical-align:middle;margin-left:20px'>";
                oImgIco.setAttribute("src", "../../../../images/imgFase.gif");
                oImgIco.setAttribute("title", "Fase");
                oImgIco.border = "0";
                oImgIco.setAttribute("style","vertical-align:middle;margin-left:20px");                    
                break;
            case "ACTI.":
                //sRes="<img src='../../../../Images/imgActividad.gif' border='0' title='Actividad' style='vertical-align:middle;margin-left:"+sMargen+"'>";
                oImgIco.setAttribute("src", "../../../../images/imgActividad.gif");
                oImgIco.setAttribute("title", "Actividad");
                oImgIco.border = "0";
                oImgIco.setAttribute("style", "vertical-align:middle;margin-left:" + sMargen); 
                break;
            case "TAREA":
                //sRes="<img src='../../../../Images/imgTarea.gif' border='0' title='Tarea' style='cursor: url(../../../../images/imgManoAzul2.cur);vertical-align:middle;margin-left:"+sMargen+"'>";
                oImgIco.setAttribute("src", "../../../../images/imgTarea.gif");
                oImgIco.setAttribute("title", "Tarea");
                oImgIco.border = "0";
                oImgIco.setAttribute("style", "cursor: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle;margin-left:" + sMargen); 
                break;
            case "HITO":
                //sRes="<img src='../../../../Images/imgHito.gif' border='0' title='Hito' style='cursor: url(../../../../images/imgManoAzul2.cur);vertical-align:middle;margin-left:"+sMargen+"'>";
                oImgIco.setAttribute("src", "../../../../images/imgHito.gif");
                oImgIco.setAttribute("title", "Hito");
                oImgIco.border = "0";
                oImgIco.setAttribute("style", "cursor: url(../../../../images/imgManoAzul2.cur),pointer;vertical-align:middle;margin-left:" + sMargen);                   
                
                break;
        }
        break;
    default:  
        switch (sTipo)
        {
            case "P.T.":
                //sRes="<img src='../../../../Images/imgProyTecOff.gif' border='0' title='P.T. grabado'>";
                oImgIco.setAttribute("src", "../../../../images/imgProyTecOff.gif");
                oImgIco.setAttribute("title", "P.T. grabado");
                oImgIco.border = "0";
                
                break;
            case "FASE":
                //sRes="<img src='../../../../Images/imgFaseOff.gif' border='0' title='Fase grabada'>";
                oImgIco.setAttribute("src", "../../../../images/imgFaseOff.gif");
                oImgIco.setAttribute("title", "Fase grabado");
                oImgIco.border = "0";
                
                break;
            case "ACTI.":
                //sRes="<img src='../../../../Images/imgActividadOff.gif' border='0' title='Actividad grabada'>";
                oImgIco.setAttribute("src", "../../../../images/imgActividadOff.gif");
                oImgIco.setAttribute("title", "Actividad grabado");
                oImgIco.border = "0";
                break;
            case "TAREA":
                //sRes="<img src='../../../../Images/imgTareaOff.gif' border='0' title='Tarea grabada'>";
                oImgIco.setAttribute("src", "../../../../images/imgTareaOff.gif");
                oImgIco.setAttribute("title", "Tarea grabado");
                oImgIco.border = "0";
                break;
            case "HITO":
                //sRes="<img src='../../../../Images/imgHitoOff.gif' border='0' title='Hito grabado'>";
                oImgIco.setAttribute("src", "../../../../images/imgHitoOff.gif");
                oImgIco.setAttribute("title", "Hito grabado");
                oImgIco.border = "0";
                break;
        }
        break;
    }
    return oImgIco;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener el icono para el tipo de línea", e.message);
}
}
function fgGetIcono2(sTipo)
{
var sRes;
try{
    switch (sTipo)
    {
        case "P.T.":
            sRes="../../../../Images/imgProyTec.gif";
            break;
        case "FASE":
            sRes="../../../../Images/imgFase.gif";
            break;
        case "ACTI.":
            sRes="../../../../Images/imgActividad.gif";
            break;
        case "TAREA":
            sRes="../../../../Images/imgTarea.gif";
            break;
        case "HITO":
            sRes="../../../../Images/imgHito.gif";
            break;
    }
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener el icono para el tipo de línea", e.message);
}
}
function fgGetIcono3(sTipo,sEstado)
{
var sRes;
try{
    switch (sEstado){
    case "U":
    case "I":
        switch (sTipo)
        {
            case "P.T.":
                sRes="../../../../Images/imgProyTecN.gif";
                break;
            case "FASE":
                sRes="../../../../Images/imgFaseN.gif";
                break;
            case "ACTI.":
                sRes="../../../../Images/imgActividadN.gif";
                break;
            case "TAREA":
                sRes="../../../../Images/imgTareaN.gif";
                break;
            case "HITO":
                sRes="../../../../Images/imgHitoN.gif";
                break;
        }
        break;
    default:  
        switch (sTipo)
        {
            case "P.T.":
                sRes="../../../../Images/imgProyTecOff.gif";
                break;
            case "FASE":
                sRes="../../../../Images/imgFaseOff.gif";
                break;
            case "ACTI.":
                sRes="../../../../Images/imgActividadOff.gif";
                break;
            case "TAREA":
                sRes="../../../../Images/imgTareaOff.gif";
                break;
            case "HITO":
                sRes="../../../../Images/imgHitoOff.gif";
                break;
        }
        break;
    }
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener el icono para el tipo de línea", e.message);
}
}
function fgGetSigLineaNoBorrada(iLinAct, bIncluirHitos)
{//Obtiene la siguiente linea a la actual que no este borrada
var iRes=iLinAct,j,sTipo;
try{
    aFila = $I("tblDatos").getElementsByTagName("TR");
    nTareas=aFila.length;
    
    for (j=iLinAct+1;j<nTareas;j++){
        if (aFila[j].getAttribute("est") != "D") {
            sTipo = getTipo(aFila[j].getAttribute("tipo"));
            if (bIncluirHitos){
                iRes=j;
                break;
            }
            else{
                if (sTipo!="H"){
                    iRes=j;
                    break;
                }
            }
        }
    }
    return iRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener la siguiente línea no borrada", e.message);
}
}
function fgGetAntLineaNoBorrada(iLinAct,bIncluirHitos)
{//Obtiene la siguiente linea a la actual que no este borrada
    var iRes=iLinAct,j,sTipo;
    try{
        aFila = $I("tblDatos").getElementsByTagName("TR");
        
        for (j=iLinAct-1;j>=0;j--){
            if (aFila[j].getAttribute("est") != "D") {
                sTipo = getTipo(aFila[j].getAttribute("tipo"));
                if (bIncluirHitos){
                    iRes=j;
                    break;
                }
                else{
                    if (sTipo!="H"){
                        iRes=j;
                        break;
                    }
                }
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la anterior línea no borrada", e.message);
    }
}
function calcularFilaDetalle(iFOri){
/* Dada una fila actual de una estructura sin grabar devuelve el indice que le correspondería eliminando las filas que están marcadas
   para borrar
*/
    var iFAct=-1;
    try{
        var aF = $I("tblDatos").getElementsByTagName("TR");
        for (i=0;i<=iFOri;i++){
            if (aF[i].getAttribute("est") != "D") iFAct++;
        }
        return iFAct;
	}
	catch(e){
		mostrarErrorAplicacion("Error al calcular el indice de la fila para el acceso al detalle", e.message);
    }
}
function mostrarDetalle(){
    mostrarProcesando();
    setTimeout("mDetalle()",2500);
}
function mDetalle(){
    var bRecalcular=false;
    var sEstado, sTipo, sCodigo, sPantalla="",sTamanio,sAux, sHayCambios, sAccesibilidad;
    var sTipo, sFactAnt,sFactAct, sAvanceAct,sAvanceAnt, sObligaAct,sObligaAnt;
    try{
	    if ($I("txtModificable").value=="T") sAccesibilidad="W";
	    else sAccesibilidad="R";
	    
	    if (iFDet==-1) recalcularFilaSeleccionada();
	    else{
	        iFila=iFDet;
	        iFDet=-1;
	    }
	    if (iFila==-1) recalcularFilaSeleccionada();
	    if(iFila<0){
	        ocultarProcesando();
	        return;
	    }
	    var Opciones = $I("tblDatos").getElementsByTagName("TR");
	    sTipo = Opciones[iFila].getAttribute("tipo");
        if ((sTipo!="P.T.") && (sTipo != "TAREA")){
            ocultarProcesando();
            mmoff("Inf", "El elemento no tiene detalle", 230);
            return;
        }
        sDescAnt=Opciones[iFila].cells[0].children[1].value;
        if (sDescAnt==""){
            ocultarProcesando();
            mmoff("Inf", "La denominación es obligatoria.", 230);
            return;
        }
       //Si la estructura no está grabada solicito grabacion
        if (bCambios){
            ocultarProcesando();
            jqConfirm("", "Datos modificados.<br />Para acceder al detalle es preciso grabarlos.<br><br>¿Deseas hacerlo?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    bDetalle = true; iFDet = calcularFilaDetalle(iFila);
                    grabar();
                    bCambios = false;
                    desActivarGrabar();
                }
                return;
            });
        }
        else
        {
            sFactAnt = "F";
            sAvanceAnt = "F";
            sObligaAnt = "F"
            if (sTipo == "P.T.") {
                sObligaAnt = Opciones[iFila].getAttribute("sOb");
            }
            if (sTipo == "TAREA") {
                if (Opciones[iFila].cells[1].children[0].checked) sFactAnt = "T";
                //sAvanceAnt=Opciones[iFila].sAv;
                if (Opciones[iFila].cells[2].children[0].checked) sAvanceAnt = "T";
            }
            sPantalla = strServer + "Capa_Presentacion/PSP/ItemPlant/Default.aspx?nIdTarea=";
            sCodigo = Opciones[iFila].getAttribute("cT");
            //sPantalla+=sCodigo + "&nIdPl=" + $I("hdnIDPlantilla").value + "&sTipo=" + sTipo + "&Permiso=" + sAccesibilidad;
            sPantalla += sCodigo + "&sTipo=" + sTipo + "&Permiso=" + sAccesibilidad;
            mostrarProcesando();
            modalDialog.Show(sPantalla, self, sSize(940, 550))
                .then(function (ret) {
                    if (ret != null) {
                        //Devuelve una cadena del tipo 
                        //  0          1       2         
                        //HayCambio@#@descripcion@#@facturable(T/F)@#@
                        //Recojo los valores y si hay alguna diferencia actualizo el desglose
                        //Si no es modificable se supone que no ha podido cambiar nada en la pantalla detalle
                        if (sAccesibilidad != "W") {
                            ocultarProcesando();
                            return;
                        }
                        Opciones = $I("tblDatos").getElementsByTagName("TR");
                        aNuevos = ret.split("@#@");
                        sHayCambios = aNuevos[0];
                        if (sHayCambios == "F") {//no ha habido cambios en la pantalla detalle
                            ocultarProcesando();
                            return;
                        }
                        sDescAct = aNuevos[1];
                        if (sDescAct == "") {//en este caso estamos volviendo de una pantalla con error
                            ocultarProcesando();
                            return;
                        }
                        if (sDescAnt != sDescAct) Opciones[iFila].cells[0].children[1].value = sDescAct;

                        if (sTipo == "P.T.") {
                            sObligaAct = aNuevos[4];
                            if (sObligaAnt != sObligaAct) {
                                Opciones[iFila].setAttribute("sOb", sObligaAct);
                            }
                        }
                        if (sTipo == "TAREA") {
                            //el check de facturable solo lo actualizo si vengo de una tarea
                            sFactAct = aNuevos[2];
                            sAvanceAct = aNuevos[3];
                            sObligaAct = aNuevos[4];
                            if (sFactAnt != sFactAct) {
                                //Opciones[iFila].cells[2].children[0].value=sFactAct;
                                if (sFactAct == "T") Opciones[iFila].cells[1].children[0].checked = true;
                                else Opciones[iFila].cells[1].children[0].checked = false;
                            }
                            if (sAvanceAnt != sAvanceAct) {
                                //Opciones[iFila].sAv=sFactAct;
                                if (sAvanceAct == "T") Opciones[iFila].cells[2].children[0].checked = true;
                                else Opciones[iFila].cells[2].children[0].checked = false;
                            }
                        }
                    } //if (ret != null)
                });
            window.focus();
            ocultarProcesando();
        }
    }//try
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del elemento", e.message);
    }
}
///////////////////////////////////////////
/// FUNCIONES PARA HITOS //////////////////
///////////////////////////////////////////
function subirHitosMarcados(){
//Recorre las filas marcadas y las va subiendo una a una
	var nFilas=0, iFilaAct,iFilaOriginal;
	var bHaySubida=false;
	try{
	    iFilaOriginal=iFila2;
	    var aFilaH = $I("tblDatos2").getElementsByTagName("TR");
	    nFilas=aFilaH.length;
	    for (iFilaAct=0;iFilaAct<nFilas;iFilaAct++){	        
            if (aFilaH[iFilaAct].className == "FS"){
	            //Si está marcada la primera fila NO se puede subir
	            if (iFilaAct==0) return;
                iFila2=iFilaAct;
                subirHito();
                bHaySubida=true;
            }
        }
        if (bHaySubida){
            nfo--;//Decremento la vble global que indica el nº de fila original
            iFila2=iFilaOriginal - 1;
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al subir hitos marcados", e.message);
	}
}
function subirHito(){
	var sDesc,sEstado,sH,sOrden,sIni,sTitulo,sIcono;
	try{
	    if (iFila2==-1) {
	        mmoff("Inf", "Debes seleccionar una fila", 180);
	        return;
	    }
	    if (iFila2==0) {
	        mmoff("Inf", "No se puede subir la primera fila",230);   
	        return;
	    }
	    aFila = $I("tblDatos2").getElementsByTagName("TR");
	    
	    //Guardo en variables los datos de la linea anterior
	    //Columna 1
	    sTitulo = aFila[iFila2-1].cells[0].children[0].title;
	    sIcono = aFila[iFila2-1].cells[0].children[0].src;

	    //Columna 2
	    sDesc = aFila[iFila2-1].cells[1].children[0].value;
	    sEstado = aFila[iFila2 - 1].getAttribute("est");
	    sOrden = aFila[iFila2 - 1].getAttribute("ord");
	    sH = aFila[iFila2 - 1].getAttribute("codH");
	    
	    //Copio la linea actual a la linea anterior
        aFila[iFila2-1].cells[0].children[0].title = aFila[iFila2].cells[0].children[0].title;
        aFila[iFila2-1].cells[0].children[0].src = aFila[iFila2].cells[0].children[0].src;
        aFila[iFila2-1].cells[1].children[0].value = aFila[iFila2].cells[1].children[0].value;
        aFila[iFila2 - 1].setAttribute("est", aFila[iFila2].getAttribute("est"));
        aFila[iFila2 - 1].setAttribute("ord", aFila[iFila2].getAttribute("ord"));
        aFila[iFila2 - 1].setAttribute("codH", aFila[iFila2].getAttribute("codH"));

	    //Restauro la linea actual con los valores de las variables
	    //Columna 1
	    aFila[iFila2].cells[0].children[0].src=sIcono;
	    aFila[iFila2].cells[0].children[0].title=sTitulo;
        //Columna 2
	    aFila[iFila2].cells[1].children[0].value=sDesc ;
	    aFila[iFila2].setAttribute("est", sEstado);
	    aFila[iFila2].setAttribute("ord", sOrden);
	    aFila[iFila2].setAttribute("codH", sH);
	    
	    activarGrabar();
	    aFila[iFila2-1].className = "FS";
	    if (iFila2 % 2 == 0) aFila[iFila2].className = "FA";
		else aFila[iFila2].className = "FB";	    
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al subir línea de hito especial", e.message);
	}
}
function bajarHitosMarcados(){
//Recorre las filas marcadas y las va bajando una a una
	var nFilas=0, iFilaAct, iFilaOriginal;
	var bHayBajada=false;
	try{
	    iFilaOriginal=iFila2;
	    aFila = $I("tblDatos2").getElementsByTagName("TR");
	    nFilas=aFila.length - 1;
	    for (iFilaAct=nFilas;iFilaAct>=0;iFilaAct--){	        
            if (aFila[iFilaAct].className == "FS"){
	            //Si está marcada la última fila NO se puede bajar
	            if (iFilaAct==nFilas) return;
                iFila2=iFilaAct;
                bajarHito();
                bHayBajada=true;
            }
        }
        if (bHayBajada){
            nfo++;//Incremento la vble global que indica el nº de fila original
            iFila=iFilaOriginal + 1;
        }
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al bajar hitos marcados", e.message);
	}
}
function bajarHito(){
	var sDesc,sEstado,sH,sOrden,sIcono,sIconoAux,sTitulo,sTitAux;
	var nFilas=0;
	try{
	    if (iFila2==-1) {
	        mmoff("Inf", "Debes seleccionar una fila", 180);
	        return;
	    }
	    aFila = $I("tblDatos2").getElementsByTagName("TR");
	    nFilas=aFila.length - 1;
	    if (iFila2==nFilas) {
	        mmoff("Inf", "No se puede bajar la última fila",180);   
	        return;
	    }
	    //Guardo en variables los datos de la linea siguiente
	    //Columna 1
	    sIcono = aFila[iFila2+1].cells[0].children[0].src;
	    sIconoAux = aFila[iFila2].cells[0].children[0].src;
	    sTitulo = aFila[iFila2+1].cells[0].children[0].title;
	    sTitAux = aFila[iFila2].cells[0].children[0].title;
	    //Columna 2
	    sDesc = aFila[iFila2+1].cells[1].children[0].value;
	    sEstado = aFila[iFila2 + 1].getAttribute("est");
	    sOrden = aFila[iFila2 + 1].getAttribute("ord");
	    sH = aFila[iFila2 + 1].getAttribute("codH");
	    
	    //Copio la linea actual a la linea siguiente
	    aFila[iFila2+1].cells[0].children[0].src=sIconoAux;
	    aFila[iFila2+1].cells[0].children[0].title=sTitAux;
        aFila[iFila2+1].cells[1].children[0].value=aFila[iFila2].cells[1].children[0].value;
        aFila[iFila2 + 1].setAttribute("est", aFila[iFila2].getAttribute("est"));
        aFila[iFila2 + 1].setAttribute("ord", aFila[iFila2].getAttribute("ord"));
        aFila[iFila2 + 1].setAttribute("codH", aFila[iFila2].getAttribute("codH"));

	    //Restauro la linea actual con los valores de las variables
	    //Columna 1
	    aFila[iFila2].cells[0].children[0].src=sIcono;
	    aFila[iFila2].cells[0].children[0].title=sTitulo;
	    //Columna 2
	    aFila[iFila2].cells[1].children[0].value=sDesc ;
	    aFila[iFila2].setAttribute("est",sEstado);
	    aFila[iFila2].setAttribute("ord", sOrden);
	    aFila[iFila2].setAttribute("codH", sH);
	    
	    activarGrabar();
	    aFila[iFila2+1].className = "FS";
	    if (iFila2 % 2 == 0) aFila[iFila2].className = "FA";
		else aFila[iFila2].className = "FB";	    
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al bajar línea de hito especial", e.message);
	}
}
function eliminarHito(objTabla){
	var sEstado;
	try{
        var aFila = $I("tblDatos2").getElementsByTagName("TR");
        for (i=aFila.length-1;i>=0;i--){
            if (aFila[i].className=="FS"){
                //Si es una fila que ya existe en BBDD la marco para borrado, sino la elimino
                sEstado = aFila[i].getAttribute("est");
                if (sEstado=="I") $I(objTabla).deleteRow(i);
                else {
                    aFila[i].setAttribute("est","D");
                    aFila[i].style.display = "none";
                    aFila[i].className="FB";
                }
            }
        }
	    activarGrabar();
	    iFila2 = -1;
	}
	catch(e){
		mostrarErrorAplicacion("Error al eliminar línea del desglose de hitos especiales", e.message);
	}
}  
function Hito(objTabla){
	/* En función del botón clickado y de la opción elegida se realiza una acción sobre el desglose de hitos especiales
	*/
	var sAccion;
	try{
	    if ($I("hdnIDPlantilla").value =="") 
	    {
	        mmoff("Inf", "Debes seleccionar una plantilla",220);
	        return;
	    }
	    sAccion=getRadioButtonSelectedValue("rdbAccion2",false);
	    switch(sAccion){
	        case "A":
	            nuevoHito(objTabla);
	            break;
	        case "I":
	            insertarHito(objTabla);
	            break;
	        default:
	            mmoff("Inf","Acción '"+sAccion+ "' no contemplada",320);
	    }
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al tratar línea", e.message);
	}
}
function nuevoHito(objTabla){
	try{
	oNF = $I(objTabla).insertRow(-1);
	ponerHito(objTabla);
	}
	catch(e){
	    iFila2 = -1;
		mostrarErrorAplicacion("Error al añadir línea para hitos especiales", e.message);
	}
}
function insertarHito(objTabla){
	try{
	//alert("insertarHito");
	if (iFila2<0){
	    mmoff("Inf", "Para insertar una fila debes seleccionar sobre\nque fila se realizará la acción",400);
	    return;
	}
	oNF = $I(objTabla).insertRow(iFila2);
	ponerHito(objTabla);
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al insertar línea para hitos especiales", e.message);
	}
}
function ponerHito(objTabla){
	var sIcono,iFilaAnt;
	try{
	oNF.style.cursor = "pointer";
	oNF.style.height = "20px";
    oNF.attachEvent('onclick', mm);
    oNF.attachEvent('onkeydown', accionLineaHito);

    oNF.setAttribute("est", "I");
    oNF.setAttribute("ord", "-1");
    oNF.setAttribute("codH", "-1");

    
	iFila2=oNF.rowIndex;
	oNC1 = oNF.insertCell(-1);
	oNC1.style.width = "20px";
	oNC1.ondblclick = function (){mostrarDetalleHito();};

    //var objTxt2;
	//sIcono=fgGetIcono("HITO","I", "0");
	//objTxt2 = document.createElement(sIcono);
	//oNC1.appendChild(objTxt2);
	oNC1.appendChild(fgGetIcono("HITO","I", "0"));

	var oCtrl1 = document.createElement("input");
	oCtrl1.type = "text";
	oCtrl1.style.width = "440px";
	oCtrl1.className = "txtL";
	oCtrl1.maxLength = "50";

	oCtrl1.onfocus = function() { this.select() };
	oCtrl1.attachEvent('onkeydown', modificarNombreHito);
	
	oNC2 = oNF.insertCell(-1);
	oNC2.style.width = "450px";
	oNC2.appendChild(oCtrl1);

	//oNC2.appendChild(document.createElement("<input type='text' class='txtL' style='width:440px;' MaxLength='50' value='' onfocus='this.select();' onkeydown='modificarNombreHito()' >"));
    oNC2.children[0].focus();

    //Selecciono la nueva fila y pongo el foco en el campo descripción

    ms(oNF);   
	activarGrabar();
	indiceFila2++;
	oNC2.children[0].focus();
	}
	catch(e){
	    iFila2 = -1;
		mostrarErrorAplicacion("Error al añadir línea de hito especial", e.message);
	}
}
function flGetCadenaHitosEspeciales(){
/*Recorre la tabla de desglose de hitos para obtener una cadena que se pasará como parámetro
  al procedimiento de grabación
*/
var sRes="",sDes,sEstado,sCodigo;
var sTipoGen,sOrden;
try{
    aFila = $I("tblDatos2").getElementsByTagName("TR");
    for (i=0;i<aFila.length;i++){
        sDes = aFila[i].cells[1].children[0].value;
        sEstado = aFila[i].getAttribute("est");
        sOrden = aFila[i].getAttribute("ord");
        sCodigo = aFila[i].getAttribute("codH");
        sRes+=sEstado+"##"+Utilidades.escape(sDes)+"##"+sCodigo+"##"+sOrden+"##"+"@#@";
    }//for
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener la cadena de grabación de hitos especiales", e.message);
}
}
function comprobarDatosHitos2(){
//Recorre la tabla de desglose de hitos especiales para comprobar si todas las líneas tienen descripción
    var bHayError=false;
    var sDes,sEstado;
    try{
        var aFila = $I("tblDatos2").getElementsByTagName("TR");
        var nTareas=aFila.length;
        
        for (i=0;i<nTareas;i++){
            sEstado = aFila[i].getAttribute("est");
            if (sEstado!="D"){    
                sDes = aFila[i].cells[1].children[0].value;
                if (sDes==""){
                    bHayError = true;
                    break;
                }
            }//if
        }//for
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    if (bHayError) return false;
    else return true;
}
function accionLineaHito(e){
	var sEstado, sIcono,sTitulo,sDesc, sAccion;
	try {
	    if (!e) e = event;
	    var oElement = e.srcElement ? e.srcElement : e.target;

	    switch (e.keyCode) {	
	        case 13: //Si estamos en la última línea, Abrimos una linea del mismo tipo
        	    recalcularFilaSeleccionadaHito();
        	    sAccion=getRadioButtonSelectedValue("rdbAccion2",false);
        	    aFila = $I("tblDatos2").getElementsByTagName("TR");
        	    if (sAccion=="A"){
                    nuevoHito("tblDatos2");
                }
                else if (sAccion=="I"){
                    if (iFila2==aFila.length-1){
                        nuevoHito("tblDatos2");
                    }
                    else{
                        iFila2+=1;
                        insertarHito("tblDato2");
                    }
                }
                break;
            case 38://flecha arriba
                aFila = $I("tblDatos2").getElementsByTagName("TR");
                recalcularFilaSeleccionadaHito();
                if (iFila2>0){
                    iFila2-=1;
                    ms(aFila[iFila2]);
                  
                    aFila[iFila2].cells[1].children[0].focus();
                }
                break; 
            case 40://flecha abajo
                aFila = $I("tblDatos2").getElementsByTagName("TR");
                recalcularFilaSeleccionadaHito();
                if (iFila2<aFila.length-1){                    
                    iFila2+=1;
                    ms(aFila[iFila2]);                    
                    aFila[iFila2].cells[1].children[0].focus();
                }
                break; 
	    }//switch
	}
	catch(e){
		mostrarErrorAplicacion("Error al actuar sobre la línea de hito", e.message);
	}
}
function recalcularFilaSeleccionadaHito(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        if (iFila2<0){
            //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
            var aFilas1 = FilasDe("tblDatos2");
            for (var i=0;i<aFilas1.length;i++){
                if (aFilas1[i].className == "FS" && aFilas1[i].style.display != "none"){
                    iFilaSeleccionada=i;
                    iNumFilasSeleccionadas++;
                }
            }
            if (iNumFilasSeleccionadas==1){
                iFila2=iFilaSeleccionada;
            }
        }
    }
    catch(e){
        iFila2=-1;
	    mostrarErrorAplicacion("Error al recalcular fila de hito selecionada", e.message);
    }
}
function modificarNombreHito(e){
	var sEstado, sIcono,sTitulo;
	try {
	    if (!e) e = event;
	    var oElement = e.srcElement ? e.srcElement : e.target;

	    switch (e.keyCode) {
	        case 13: //Si estamos en la última línea, Abrimos una linea del mismo tipo
                break;
            case 16://shift
                break;
            case 17://ctrl
                break;
            case 38://flecha arriba
                //subirHito();
                break; 
            case 40://flecha abajo
                //bajarHito()
                break; 
            default:
	            lineaHitoModificada();   
	    }//switch
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción del hito", e.message);
	}
}
function lineaHitoModificada(){
	try{
        //Compruebo si la linea es modificable
        var aF2 = $I("tblDatos2").getElementsByTagName("TR");
        if (iFila2 == -1) recalcularFilaSeleccionadaHito();
        activarGrabar();
        sEstado = aF2[iFila2].getAttribute("est");
        if (sEstado=="N") {
            aF2[iFila2].setAttribute("est", "U");
            sIcono=fgGetIcono3("HITO","U"); 
            //sTitulo=fgGetTitulo(sTipo);
            aF2[iFila2].cells[0].children[0].src=sIcono;   
            //aF2[iFila2].cells[0].children[0].title=sTitulo;   
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al marcar un hito especial como modificado", e.message);
	}
}
function mostrarDetalleHito(){
    var sEstado, sCodigo, sCodigoAnt, sPantalla="",sTamanio,sAux, sIdPlant, sHayCambios,sDescAnt,sDescAct;
    try{
	    sIdPlant=$I("hdnIDPlantilla").value;
	    var Opciones = $I("tblDatos2").getElementsByTagName("TR");
	    //sEstado=Opciones[iFila2].cells[1].children[1].value;
       //Compruebo si por estado del elemento permito acceso al detalle
//        if (sEstado=="I" || sEstado=="D") {
//            alert("El elemento debe estar grabado para tener acceso a su detalle");   
//            return;
//        }
	    if (iFHit==-1) recalcularFilaSeleccionadaHito();
	    else{
	        iFila2=iFHit;
	        iFHit=-1;
	    }
	    if(iFila2<0) recalcularFilaSeleccionadaHito();
	    if(iFila2<0)return;
       //Si la estructura no está grabada no permito
        if (bCambios){
            jqConfirm("", "Datos modificados.<br />Para acceder al detalle es preciso grabarlos.<br /><br />¿Deseas hacerlo?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    bDetalleHito=true;
                    iFHit=calcularFilaHitoDetalle(iFila2);
                    if (!grabar()){
                        iFHit=-1;
                        bDetalleHito=false;
                        return;
                    }
                }
                bCambios = false;
                desActivarGrabar();
                return;
            });
        }
        else
        {
            sPantalla=strServer +"Capa_Presentacion/PSP/Plantilla/HitoPlant/Default.aspx?nIdPlant="+sIdPlant+"&nIdHito=";
            sCodigoAnt=Opciones[iFila2].getAttribute("codH");
            if (sPantalla!=""){
                sPantalla += sCodigoAnt;
                mostrarProcesando();
                modalDialog.Show(sPantalla, self, sSize(940, 650))
                .then(function(ret) {
                    if (ret != null) {
                        //Devuelve una cadena del tipo hay cambio@#@tipo_hito@#@descripcion@#@codigo hito
                        //Recojo los valores y si hay alguna diferencia actualizo el desglose
                        aHitosNuevos = ret.split("@#@");
                        sHayCambios = aHitosNuevos[0];
                        if (sHayCambios == "F") {//no ha habido cambios en la pantalla detalle
                            return;
                        }

                        sDescAnt = Opciones[iFila2].cells[1].children[0].value;
                        sDescAct = aHitosNuevos[1];
                        if (sDescAnt != sDescAct) Opciones[iFila2].cells[1].children[0].value = sDescAct;

                        sCodigo = aHitosNuevos[2];
                        if (sCodigo != sCodigoAnt) {
                            Opciones[iFila2].setAttribute("codH", sCodigo);
                        }
                    }
                });
                window.focus();

                ocultarProcesando();
            }
        }
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del hito", e.message);
    }
}
function calcularFilaHitoDetalle(iFOri){
    var iFAct=-1;
    try{
        var aF = $I("tblDatos2").getElementsByTagName("TR");
        for (i=0;i<=iFOri;i++){
            if (aF[i].getAttribute("est") != "D") iFAct++;
        }
        return iFAct;
	}
	catch(e){
		mostrarErrorAplicacion("Error al calcular el indice de la fila de hitos para el acceso al detalle", e.message);
    }
}
function setResolucionPantalla(){
    try{
        mostrarProcesando();
        var js_args = "setResolucion//";
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}
function setResolucion1024(){
    try{
        $I("divCatalogo").style.height = "280px";
        $I("divHitos").style.height = "40px";       
    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}
