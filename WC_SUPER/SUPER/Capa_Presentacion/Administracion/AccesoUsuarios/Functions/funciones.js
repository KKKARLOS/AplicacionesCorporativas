function init(){
    try{
        scrollTablaProfAsig();
        //actualizarLupas("tblTitRecAsig", "tblAsignados");
        actualizarTodasLupas();
        $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function actualizarTodasLupas(){
    actualizarLupas("tblTitRecAsig", "tblAsignados");
    actualizarLupas("tblTitRec", "tblRelacion");
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabarMasivo":
                bCambios = false;
                BorrarFilasDe("tblRelacion");
                var aFila = FilasDe("tblAsignados");
                for (var i=aFila.length -1; i >= 0; i--){
                    //if (aFila[i].b=="T"){
                    if (aFila[i].getAttribute("b") == "T") {
                        $I("tblAsignados").deleteRow(i);
                    }
                }
                actualizarTodasLupas();
                ocultarProcesando();
                mmoff("Suc","Grabación correcta", 160); 
                //popupWinespopup_winLoad();
                break;
            case "grabar":
                bCambios = false;
                actualizarLupas("tblTitRecAsig", "tblAsignados");
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160); 
                //popupWinespopup_winLoad();
                break;
            case "tecnicos":
		        $I("divRelacion").children[0].innerHTML = aResul[2];
		        $I("divRelacion").scrollTop = 0;
		        scrollTablaProf();
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
		        actualizarLupas("tblTitRec", "tblRelacion");
                ocultarProcesando();
                break;
        }
    }
}
function mostrarRelacionTecnicos(){
    try{
        var sValor1, sValor2, sValor3;
        sValor1 = Utilidades.escape($I("txtApellido1").value);
        sValor2 = Utilidades.escape($I("txtApellido2").value);
        sValor3 = Utilidades.escape($I("txtNombre").value);
        if (sValor1=="" && sValor2=="" && sValor3==""){
            mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
            return;
        }
        var js_args = "tecnicos@#@";
        js_args += sValor1+"@#@"+sValor2+"@#@"+sValor3;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf(){
    try{
        if ($I("divRelacion").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divRelacion").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacion").offsetHeight/20+1, $I("tblRelacion").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            //if (!tblRelacion.rows[i].sw){
            if (!$I("tblRelacion").rows[i].getAttribute("sw")) {
                oFila = $I("tblRelacion").rows[i];
                
                //oFila.sw = "1";
                oFila.setAttribute("sw", "1");
                                
                //if (oFila.sexo=="V"){
                if (oFila.getAttribute("sexo")=="V"){
                    //switch (oFila.tipo) {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgPV.cloneNode(false), null); break;
                    }
                }else{
                    //switch (oFila.tipo){
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgPM.cloneNode(false), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divAsignados").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divAsignados").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divAsignados").offsetHeight/20+1, $I("tblAsignados").rows.length);
        
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            //if (!tblAsignados.rows[i].sw){
            if (!$I("tblAsignados").rows[i].getAttribute("sw")) {
                oFila = $I("tblAsignados").rows[i];
                //oFila.sw = "1";
                oFila.setAttribute("sw", "1");
                
                //if (oFila.sexo=="V"){
                if (oFila.getAttribute("sexo") == "V") {
                    //switch (oFila.tipo){
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgPV.cloneNode(false), null); break;
                    }
                }else{
                    //switch (oFila.tipo){
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgPM.cloneNode(false), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function insertarRecurso(oChk){
    var iFilaNueva=0, iFilaDest;
    var sNombreNuevo, sNombreAct, js_args="";
    var oFila=oChk.parentNode.parentNode;
    var bExiste=false;
    try{
        var idRecurso = oFila.id;
        //1º buscar si existe en el array de recursos 
        var aFila = FilasDe("tblAsignados");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
                iFilaDest=i;
                bExiste = true;
                break;
            }
        }
        if (oChk.checked){
            if (bExiste){
                js_args="grabar@#@" + oFila.id + "@#@T";
                $I("tblAsignados").deleteRow(iFilaDest);
            }
        }
        else{
            if (!bExiste){
                iFilaNueva=aFila.length;
                sNombreNuevo = oFila.cells[1].innerText;
                for (var i=0; i < aFila.length; i++){
                    //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                    sNombreAct=aFila[i].cells[1].innerText;
                    if (sNombreAct>sNombreNuevo){
                        iFilaNueva=i;
                        break;
                    }
                }
                var oNF = $I("tblAsignados").insertRow(iFilaNueva);

                //oNF.onclick = function() { mmse(this); };
                oNF.attachEvent('onclick', mm);
                
                oNF.style.height = "20px";
                oNF.id = oFila.id;
                //oNF.sw = 1;
                oFila.setAttribute("sw", "1");

                //oNF.title = oFila.title;                
                oNF.setAttribute("title",oFila.getAttribute("title"));
                
                iFila=oNF.rowIndex;
                oNC1 = oNF.insertCell(-1);
                oNF.cells[0].innerHTML = oFila.cells[0].innerHTML;
                oNC2 = oNF.insertCell(-1);
                oNF.cells[1].innerText = oFila.cells[1].innerText;
                
                js_args="grabar@#@" + oFila.id + "@#@F";
                
                $I("divAsignados").scrollTop = iFilaNueva * 16;
            }
       }
       if (js_args != ""){
            mostrarProcesando();
            RealizarCallBack(js_args, "");       
       }
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
    }
}
function asignarCompleto()
{
    try{
        var sw=0;
        var js_args = "grabarMasivo@#@";
        var aFila = FilasDe("tblAsignados");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].className == "FS" || aFila[i].getAttribute("class") == "FS") {
                js_args += aFila[i].id +"///";
                //aFila[i].b = "T";
                aFila[i].setAttribute("b", "T");
            }
        }
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al marcar el profesional para activarlo", e.message);
	}
}
