var tbody, tbodyParam;
var aFila, aFilaParam;
var bNuevo = false;
var idCons, idConsNew=-1, idParamNew=-1;
var strComentario = "";
var bBuscar = false;
var bIrDetalle = false;
var sID = -1;
function init(){
    try{
        idCons="";
        tbody = document.getElementById('tbodyDatos'); 
        tbody.onmousedown = startDragIMG; 
        
        if ($I("hdnIdCons").value != ""){
            //Hemos entrado a la pantalla con una consulta seleccionada
	        var aFilas = FilasDe("tblDatos");
	        if (aFilas.length > 0){
		        for (x=0;x<aFilas.length;x++){
		            if (aFilas[x].id == $I("hdnIdCons").value) {
		                seleccionar(aFilas[x]);
		                $I("divCatalogo").scrollTop = x * 20; $I("divCatalogo").scrollTop = x * 20;
		                break;
			        }    
		        }
		    }
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var idNuevoCons;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar la consulta ya que existen\nusuarios que la tienen asignada.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                idCons="";
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                refrescarParams("*");
                eval(aResul[3]);
                tbody = document.getElementById('tbodyDatos'); 
                tbody.onmousedown = startDragIMG; 
                break;
            case "grabar":
                idConsNew=-1, idParamNew=-1;
                for (var i = aFila.length - 1; i >= 0 ; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        borrarConsDeArray(aFila[i].getAttribute("idCons"), aFila[i].id);
                        $I("tblDatos").deleteRow(i);
                    }else{
                    if (aFila[i].getAttribute("bd") == "I") {//Para los AEs insertados actualizo su id con el código creado 
                            idNuevoCons = getidCons(aFila[i].id, aResul[2]);
                            if (idNuevoCons !=""){
                                if (aFila[i].className == "FS") idCons = idNuevoCons;
                                //Actualizo en el array de valores de los criterios estadísticos el ID del AE
                                for (var j= aParams.length - 1; j>=0; j--){
                                    if (aParams[j].idCons < 0 && aParams[j].idCons == aFila[i].id){
                                        aParams[j].idCons = idNuevoCons;
                                    }
                                }
                                aFila[i].id = idNuevoCons;
                            }
                        }
                        mfa(aFila[i],"N");
                    }
                }
                //Actualizo el array que soporta los valores de los parametros
                for (var i= aParams.length - 1; i>=0; i--){
                    if (aParams[i].bd == "D"){
                        aParams.splice(i, 1);
                    }
                    else
                        aParams[i].bd ="";
                }
                //Actualizo la tabla de parametros
                var sw=0;
                for (var i=0;i<aFila.length;i++){
                    aFila[i].setAttribute("orden",i);
                    if (aFila[i].className == "FS"){
                        //modoControles(aFila[i], false);
                        refrescarParams(aFila[i].id);
                        sw=1;
                        break;
                    }
                    //aFila[i].className = "";
                }
                if (sw==0) refrescarParams("*");

                nFilaDesde = -1;
                nFilaHasta = -1;
                mmoff("Suc", "Grabación correcta", 160);
                desActivarGrabar();
                
                if (bBuscar){
                    bBuscar = false;
                    setTimeout("buscar();", 20);
                }
                if (bIrDetalle) {
                    bIrDetalle = false;
                    setTimeout("setProfesionales("+sID+");", 20);
                }
                
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function EliminarConsulta(){
    try{
        var aFila = FilasDe("tblDatos");
        if (aFila == null) return;
        if (aFila.length == 0) return;
        var sw = 0;
        var strFilas = "";
        for(var i=aFila.length - 1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw = 1;
                if (aFila[i].getAttribute("bd") == "I") {
                    borrarConsDeArray(aFila[i].id);
                    $I("tblDatos").deleteRow(i);
                    refrescarParams("*");
                }
                else{
                    mfa(aFila[i], "D");
                    //02/09/2009 Andoni dice que no hay que hacer nada con las líneas detalle
//                    marcarBorradoParams(aFila[i].id);
//                    refrescarParams(aFila[i].id);
                }
            }
        }
        if (sw == 0){
            mmoff("Inf","Selecciona la fila a eliminar",220);
            return;
        }
        activarGrabar();
        
	}catch(e){
		mostrarErrorAplicacion("Error al intentar eliminar la consulta", e.message);
    }
}
function nuevoCons(){
    try{
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
        bNuevo = true;
        oNF = $I("tblDatos").insertRow(-1);
        var iFila=oNF.rowIndex;

        oNF.id = idConsNew--;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("orden", oNF.rowIndex);

        oNF.attachEvent('onclick', mm);
        
        oNF.onclick = function (){refrescarParams(this.id);habCom();};
        oNF.ondblclick = function (){setProfesionales(this.id)};

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));//celda 0

        var oCtrl1 = document.createElement("input");
        oCtrl1.setAttribute("type", "text");
        oCtrl1.className= "txtL";
        oCtrl1.setAttribute("style", "width:290px");
        oCtrl1.setAttribute("maxLength", "50");
        oCtrl1.onkeyup = function() { fm_mn(this) };

        oNF.insertCell(-1).appendChild(oCtrl1);//celda 1 Denominación de la consulta personalizada

        oNF.insertCell(-1);//celda 2 Prefijo del procedimiento almacenado

        var oCtrl2 = document.createElement("input");
        oCtrl2.setAttribute("type", "text");
        oCtrl2.className = "txtL";
        oCtrl2.setAttribute("style", "width:210px");
        oCtrl2.setAttribute("maxLength", "21");
        oCtrl2.onkeyup = function() { fm_mn(this) };

        oNF.insertCell(-1).appendChild(oCtrl2);//celda 3 Sufijo del procedimiento almacenado

        var oCtrl3 = document.createElement("input");
        oCtrl3.setAttribute("type", "checkbox");
        oCtrl3.className = "checkTabla";
        oCtrl3.checked = true;
        oCtrl3.setAttribute("style", "width:15px");
        oCtrl3.onclick = function() { fm_mn(this) };
        
        oNF.insertCell(-1).appendChild(oCtrl3);//celda 4 Estado de la consulta personalizada

        var oCtrl4 = document.createElement("nobr");
        oCtrl4.setAttribute("style", "height:16px");
        oCtrl4.className = "NBR W190";
        oCtrl4.attachEvent("onmouseover", TTip);
        
        oNF.insertCell(-1).appendChild(oCtrl4);//celda 5 Comentario


        //Anado el campo para la clave para acceso por Web Service (Solo si el usuario es DIS)
        if (es_DIS) {
            var oCtrl5 = document.createElement("input");
            oCtrl5.className = "txtL";
            oCtrl5.setAttribute("type", "text");
            oCtrl5.setAttribute("maxLength", "20");
            oCtrl5.onkeyup = function() { fm_mn(this) };
            oCtrl5.setAttribute("style", "width:105px");
            oNF.insertCell(-1).appendChild(oCtrl5); //celda 6 Clave para el acceso mediantre servicio web
        }
        else {
            oNF.insertCell(-1);
            //oCtrl5.setAttribute("readonly", "true");
        }

        oNF.cells[2].innerText = "SUP_USER_";
	    ms(oNF);      
	    oNF.cells[1].children[0].focus();
	    
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo registro", e.message);
    }
}
function ValidarAnnomes2(nAnnoMes)
{
    if (nAnnoMes.toString().length != 6){
        return false;
    }
    if (nAnnoMes % 100 < 1 || nAnnoMes % 100 > 12){
        return false;
    }
    if (nAnnoMes / 100 < 1900 || nAnnoMes / 100 > 2078){
        return false;
    }

    return true;
}
function buscar() {
    try {

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscar = true;
                    grabar();
                    return;
                }
                else
                {
                    bCambios = false;
                    desActivarGrabar();
                }                    
                LLamadaBuscar();
            });
        }
        else LLamadaBuscar();

    } catch (e) {
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}
function LLamadaBuscar() {
    try {

        var js_args = "buscar@#@";
        if ($I("chkActivas").checked) js_args += "0";
        else js_args += "1";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaBuscar", e.message);
    }
}
function comprobarDatos(){
    try{
        var nOrden = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].className == "FS") idCons=aFila[i].id;
            if (aFila[i].cells[1].children[0].value == ""){
                mmoff("War","Debes indicar la denominación de la consulta",320);
                return false;
            }
            if (aFila[i].cells[3].children[0].value == "") {
                mmoff("War", "Debes indicar el procedimiento almacenado de la consulta", 400);
                return false;
            }
            //if (aFila[i].cells[6].children[0].value != "") {
            if (getCelda(aFila[i], 6) != "") {
                if (aFila[i].getAttribute("bd") != "") {
                    if (aFila[i].cells[6].children[0].value.length < 10) {
                        mmoff("War", "La clave debe tener al menos 10 caracteres", 300);
                        return false;
                    }
                }                
            }

//            if (aFila[i].getAttribute("orden") != nOrden){
//                if (aFila[i].getAttribute("bd") != "I") aFila[i].setAttribute("bd","U");
//                aFila[i].setAttribute("orden",nOrden);
//            }
//            nOrden++;
        }
        //Control de parametros de la consulta
        var nOrden=0;
        var sNomCons="";
        var idConsant="";
        for (var nIndice=0; nIndice < aParams.length; nIndice++){
            if (aParams[nIndice].idCons != idConsant){
                idConsant = aParams[nIndice].idCons;
                nOrden=0;
            }
            if (aParams[nIndice].bd != "D"){
                if (aParams[nIndice].texto == ""){
                    for (var i=0; i<aFila.length; i++){
                        if (aFila[i].id == aParams[nIndice].idCons){

                            ms(aFila[i]);      
                           
                            $I("divCatalogo").scrollTop = i * 20;
                            sNomCons = aFila[i].cells[3].children[0].value;
                            for (var x = 0; x < $I("tblDatosParam").rows.length; x++) {
                                if (aParams[nIndice].idParam == $I("tblDatosParam").rows[x].id) {
                                    ms($I("tblDatosParam").rows[x]); 
                                   
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    mmoff("War", "Debes indicar el nombre del parámetro de la consulta SUP_USER_" + sNomCons,450);
                    return false;
                }
                if (aParams[nIndice].nombre == ""){
                    for (var i=0; i<aFila.length; i++){
                        if (aFila[i].id == aParams[nIndice].idCons) {
                            ms(aFila[i]);

                            $I("divCatalogo").scrollTop = i * 20;
                            sNomCons = aFila[i].cells[3].children[0].value;
                            for (var x = 0; x < $I("tblDatosParam").rows.length; x++) {
                                if (aParams[nIndice].idParam == $I("tblDatosParam").rows[x].id) {

                                    ms($I("tblDatosParam").rows[x]); 
                                 
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    mmoff("War", "Debes indicar la denominación del parámetro de la consulta SUP_USER_" + sNomCons,450);
                    return false;
                }
                
                var pattern = /[,.]/; 

                if (aParams[nIndice].tipo == "I" && aParams[nIndice].defecto!=""){
                    if  ((dfn(Utilidades.unescape(aParams[nIndice].defecto))=="0"&&unescape(aParams[nIndice].defecto)!="0")||(dfn(Utilidades.unescape(aParams[nIndice].defecto).replace(/[.]/g, ",")).search(pattern)>0))
                    {
                        for (var i=0; i<aFila.length; i++){
                            if (aFila[i].id == aParams[nIndice].idCons) {

                                ms(aFila[i]);

                                $I("divCatalogo").scrollTop = i * 20;
                                sNomCons = aFila[i].cells[3].children[0].value;
                                for (var x = 0; x < $I("tblDatosParam").rows.length; x++) {
                                    if (aParams[nIndice].idParam == $I("tblDatosParam").rows[x].id) {
                                        ms($I("tblDatosParam").rows[x]);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        mmoff("War", "Si el tipo de dato es \"Entero\", debes indicar un valor por defecto entero en la consulta SUP_USER_" + sNomCons,450);
                        return false;
                    }
                }  
                  
                if (aParams[nIndice].tipo == "M" && aParams[nIndice].defecto!=""){
                    if  (dfn(Utilidades.unescape(aParams[nIndice].defecto))=="0"&&unescape(aParams[nIndice].defecto)!="0")
                    {
                        for (var i=0; i<aFila.length; i++){
                            if (aFila[i].id == aParams[nIndice].idCons) {
                                ms(aFila[i]);

                                $I("divCatalogo").scrollTop = i * 20;
                                sNomCons = aFila[i].cells[3].children[0].value;
                                for (var x = 0; x < $I("tblDatosParam").rows.length; x++) {
                                    if (aParams[nIndice].idParam == $I("tblDatosParam").rows[x].id) {
                                        ms($I("tblDatosParam").rows[x]);  
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        mmoff("War", "Si el tipo de dato es \"Money\", debes indicar un valor money en la consulta SUP_USER_" + sNomCons,450);
                        return false;
                    }
                }  
                var patron = new RegExp(/\d{8}/); 

                if (aParams[nIndice].tipo == "D" && aParams[nIndice].defecto!=""){                   
                    if  ((!patron.test(Utilidades.unescape(aParams[nIndice].defecto)))||(aParams[nIndice].defecto.length!=8))
                    {
                        for (var i=0; i<aFila.length; i++){
                            if (aFila[i].id == aParams[nIndice].idCons) {
                                ms(aFila[i]);
 
                                $I("divCatalogo").scrollTop = i * 20;
                                sNomCons = aFila[i].cells[3].children[0].value;
                                for (var x = 0; x < $I("tblDatosParam").rows.length; x++) {
                                    if (aParams[nIndice].idParam == $I("tblDatosParam").rows[x].id) {
                                        ms($I("tblDatosParam").rows[x]);  
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if  (aParams[nIndice].defecto.length==8)
                            mmoff("War", "Si el tipo de dato es \"Date\", debes indicar un valor de 8 digitos en el formato AAAAMMDD en la consulta SUP_USER_" + sNomCons,450);
                        else
                            mmoff("War", "La longitud del campo debe ser de 8", 240);
                        return false;
                    }
                }                           
                patron = new RegExp(/\d{6}/); 

                if (aParams[nIndice].tipo == "A" && aParams[nIndice].defecto!=""){                   
                    if  ((!patron.test(Utilidades.unescape(aParams[nIndice].defecto)))||(aParams[nIndice].defecto.length!=6)||(!ValidarAnnomes(aParams[nIndice].defecto)))
                    {
                        for (var i=0; i<aFila.length; i++){
                            if (aFila[i].id == aParams[nIndice].idCons){
                                ms(aFila[i]);
                                $I("divCatalogo").scrollTop = i * 20;
                                sNomCons = aFila[i].cells[3].children[0].value;
                                for (var x = 0; x < $I("tblDatosParam").rows.length; x++) {
                                    if (aParams[nIndice].idParam == $I("tblDatosParam").rows[x].id) {
                                        ms($I("tblDatosParam").rows[x]);   
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if  (aParams[nIndice].defecto.length==6){
                            if (ValidarAnnomes(aParams[nIndice].defecto)) mmoff("War", "Si el tipo de dato es \"AñoMes\", debes indicar un valor de 6 digitos en el formato AAAAMM en la consulta SUP_USER_" + sNomCons, 450);
                        }else{
                            mmoff("War", "La longitud del campo debe ser de 6", 260);
                        }
                        return false;
                    }
                }                           
                if ((aParams[nIndice].tipo == "B") && (Utilidades.unescape(aParams[nIndice].defecto)!="0"&&unescape(aParams[nIndice].defecto)!="1") && (aParams[nIndice].defecto!="")){                   
                    {
                        for (var i=0; i<aFila.length; i++){
                            if (aFila[i].id == aParams[nIndice].idCons){
                                ms(aFila[i]);
                                $I("divCatalogo").scrollTop = i * 20;
                                sNomCons = aFila[i].cells[3].children[0].value;
                                for (var x = 0; x < $I("tblDatosParam").rows.length; x++) {
                                    if (aParams[nIndice].idParam == $I("tblDatosParam").rows[x].id) {
                                        ms($I("tblDatosParam").rows[x]);  
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        mmoff("War", "Si el tipo de dato es \"Boolean\", debes indicar los valores (0,1) en la consulta SUP_USER_" + sNomCons,420);
                        return false;
                    }
                }                           

                                         
            }
            nOrden++;               
        }

	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function grabar(){
    try{
        aFila = FilasDe("tblDatos");
        
        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var nEstado = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd")  +"##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); 
                //Denominación de la Consulta
                sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) +"##"); 
                sb.Append("SUP_USER_" + Utilidades.escape(aFila[i].cells[3].children[0].value) +"##"); 
                
                nEstado = 0;
                if (aFila[i].cells[4].children[0].checked) nEstado = 1; 
                sb.Append(nEstado +"##"); 
                sb.Append(Utilidades.escape(aFila[i].cells[5].innerText) +"##"); 
                //sb.Append((aFila[i].cells[6].children[0].checked)? "1///": "0///");
                sb.Append(Utilidades.escape(getCelda(aFila[i], 6)) + "///"); 
            }
        }
        //Control de parametros//
        sb.Append("@#@");
        for (var nIndice=0; nIndice < aParams.length; nIndice++){
            if (aParams[nIndice].bd != ""){
                sb.Append(aParams[nIndice].bd +"##");
                sb.Append(aParams[nIndice].idCons +"##");
                sb.Append(aParams[nIndice].idParam +"##");
                sb.Append(aParams[nIndice].tipo +"##");
                sb.Append(aParams[nIndice].comentario +"##");
                sb.Append(aParams[nIndice].defecto +"##");
                sb.Append(aParams[nIndice].visible +"##");
                sb.Append(aParams[nIndice].orden +"##");
                sb.Append(aParams[nIndice].texto +"##");
                sb.Append(aParams[nIndice].nombre +"##");
                sb.Append(aParams[nIndice].opcional +"///");
            }
        }
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

///////////////////////////////////////////////////////////////
// FUNCIONES PARA LA TABLA DE PARAMETROS                     //
///////////////////////////////////////////////////////////////
function nuevoParam() {
    try {
        if (idCons == "") {        
            mmoff("Inf","Para insertar parámetros, debe seleccionar la consulta",330);
            return;
        }
        oNF = $I("tblDatosParam").insertRow(-1);
        var iFila = oNF.rowIndex;

        oNF.id = idParamNew;
        oNF.setAttribute("idCons", idCons);
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:22px");
        oNF.setAttribute("orden", oNF.rowIndex);

        oNF.attachEvent('onclick', mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        
        var oImgMoveRow = document.createElement("img");
        oImgMoveRow.setAttribute("src", "../../../../images/imgMoveRow.gif");
        oImgMoveRow.setAttribute("style", "cursor:row-resize;");
        oImgMoveRow.setAttribute("title", "Pinchar y arrastrar para ordenar");
        oImgMoveRow.ondragstart = function() { return false; };

        oNC1 = oNF.insertCell(-1);
        oNC1.setAttribute("style", "text-align:center;");
        oNC1.appendChild(oImgMoveRow);

        var oCtrl1 = document.createElement("input");
        oCtrl1.setAttribute("type", "text");
        oCtrl1.className = "txtL";
        oCtrl1.setAttribute("style", "width:120px");
        oCtrl1.setAttribute("maxLength", "25");

        oCtrl1.onfocus = function() { this.className = 'txtM'; this.select(); };
        oCtrl1.onblur = function() { this.className = 'txtL'; setDenominacion(this); };
        oCtrl1.onkeyup = function() { actualizarDatos('U', 'texto', this) };

        oNC2 = oNF.insertCell(-1);
        oNC2.setAttribute("style", "padding-left:3px;");
        oNC2.appendChild(oCtrl1);

        var oCtrl2 = document.createElement("input");
        oCtrl2.setAttribute("type", "text");
        oCtrl2.className = "txtL";
        oCtrl2.setAttribute("style", "width:120px");
        oCtrl2.setAttribute("maxLength", "25");

        oCtrl2.onfocus = function() { this.className = 'txtM'; this.select(); };
        oCtrl2.onblur = function() { this.className = 'txtL'; };
        oCtrl2.onkeyup = function() { actualizarDatos('U', 'nombre', this) };

        oNC3 = oNF.insertCell(-1);
        oNC3.setAttribute("style", "padding-left:3px;");
        oNC3.appendChild(oCtrl2);

        //Tipo
        oNC3 = oNF.insertCell(-1);
        oNC3.setAttribute("style", "width:70px");
        //oNC3.style.borderRight="solid 1px #569BBD";
        //oNC3.style.paddingRight="1px";
        var oCtrlTipo = $I("cboTipoParam").cloneNode(true);
        //oCtrlTipo.disabled=true;            
        oNC3.appendChild(oCtrlTipo);

        //Comentario
        var oCtrl3 = document.createElement("input");
        oCtrl3.setAttribute("type", "text");
        oCtrl3.className = "txtL";
        oCtrl3.setAttribute("style", "width:230px");
        oCtrl3.setAttribute("maxLength", "100");

        oCtrl3.onfocus = function() { this.className = 'txtM'; this.select(); };
        oCtrl3.onblur = function() { this.className = 'txtL'; };
        oCtrl3.onkeyup = function() { actualizarDatos('U', 'comentario', this) };

        oNF.insertCell(-1).appendChild(oCtrl3);

        //Valor defecto

        var oCtrl4 = document.createElement("input");
        oCtrl4.setAttribute("type", "text");
        oCtrl4.className = "txtL";
        oCtrl4.setAttribute("style", "width:170px");
        oCtrl4.setAttribute("maxLength", "8000");

        oCtrl4.onfocus = function() { this.className = 'txtM'; this.select(); };
        oCtrl4.onblur = function() { this.className = 'txtL'; };
        oCtrl4.onkeyup = function() { actualizarDatos('U', 'defecto', this) };

        oNF.insertCell(-1).appendChild(oCtrl4);

        //Visible
        oNC6 = oNF.insertCell(-1);
        oNC6.align = "left";
        oNC6.setAttribute("style", "width:95px");
        //oNC6.style.borderRight="solid 1px #569BBD";
        var oCtrlVisible = $I("cboVisible").cloneNode(true);
        oNC6.appendChild(oCtrlVisible);

        var oCtrl5 = document.createElement("input");
        oCtrl5.setAttribute("type", "checkbox");
        oCtrl5.className = "checkTabla";

        oCtrl5.onclick = function() { actualizarDatos('U', 'opcional', this); };

//        oNC4 = oNF.insertCell(-1);
//        oNC4.setAttribute("style", "text-align:left;background-color:red;");
//        oNC4.appendChild(oCtrl5);
        
        oNF.insertCell(-1).appendChild(oCtrl5);

        ms(oNF);

        oNF.cells[2].children[0].focus();
        insertarParamEnArray("I", idCons, idParamNew--, "", "", "I", "", "", "M", iFila);
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al crear un nuevo parámetro", e.message);
    }
}
function eliminarParam(){
    try{
        var sw = 0;
        aFilaParam = FilasDe("tblDatosParam");
        if (aFilaParam == null) return;
        if (aFilaParam.length == 0) return;
        for (var i=aFilaParam.length-1; i>=0; i--){
            if (aFilaParam[i].className == "FS"){
                sw = 1;
                if (aFilaParam[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    borrarParamDeArray(aFilaParam[i].getAttribute("idCons"), aFilaParam[i].id);
                    $I("tblDatosParam").deleteRow(i);
                }    
                else{
                    //aFilaParam[i].bd = "D";
                    mfa(aFilaParam[i], "D");
                    oParamActivo = buscarParamEnArray(aFilaParam[i].getAttribute("idCons"), aFilaParam[i].id);
                    if (oParamActivo.bd != "I") 
                         oParamActivo.bd="D";
                    else
                        borrarParamDeArray(aFilaParam[i].getAttribute("idCons"), aFilaParam[i].id);
                }
            }
        }
        if (sw == 0) mmoff("Inf", "Selecciona la fila a eliminar", 220);
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el parámetro", e.message);
    }
}
function actualizarDatos(accion, clave, obj){
    try{
        var oFila = obj.parentNode.parentNode;
        //if (oFila.bd != "I") oFila.bd = "U";
        fm_mn(oFila);
        activarGrabar();
        oParamActivo = buscarParamEnArray(oFila.getAttribute("idCons"), oFila.id);
        oParamActualizar(accion, clave, obj)
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los datos", e.message);
    }
}
function refrescarParams(id){
    try{
        if (id == "*"){
            $I("divValores").children[0].innerHTML = "<table id='tblDatosParam' border='1'></table>";
            return;
        }
        idCons=id;
        var sb = new StringBuilder;
        var aParamOrd = new Array();
        var sImagen="imgFN.gif";
        sb.Append("<table id='tblDatosParam' class='texto MANO' style='WIDTH: 905px;' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:10px;' />");
        sb.Append("<col style='width:25px;' />");
        sb.Append("<col style='width:125px;' />");
        sb.Append("<col style='width:125px;' />");
        sb.Append("<col style='width:75px' />");
        sb.Append("<col style='width:240px;' />");
        sb.Append("<col style='width:180px' />");
        sb.Append("<col style='width:95px' />");
        sb.Append("<col style='width:30px' />");
        sb.Append("</colgroup>");
        
        sb.Append("<tbody id='tbodyDatosParam'>");
        for (var nIndice=0; nIndice < aParams.length; nIndice++){
            if (aParams[nIndice].idCons==id){
                var sb2 = new StringBuilder;
                sb2.Append("<tr idCons='"+id+"' id='" + aParams[nIndice].idParam + "' style='height:22px' bd='"+aParams[nIndice].bd+"' orden='" + aParams[nIndice].orden + "' onclick='mm(event)' >");
                switch(aParams[nIndice].bd){
                    case "I":
                        sImagen="imgFI.gif";
                        break;
                    case "U":
                        sImagen="imgFU.gif";
                        break;
                    case "D":
                        sImagen="imgFD.gif";
                        break;
                    default:
                        sImagen="imgFN.gif";
                        break;
                }
                sb2.Append("<td><img src='../../../../images/"+sImagen+"'></td>");
                sb2.Append("<td style='text-align:center;'><img src='../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                if (aParams[nIndice].bd=="I")
                    sb2.Append("<td style='padding-left:3px;'><input type='text' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL';setDenominacion(this);\" style='width:120px' value='" + Utilidades.unescape(aParams[nIndice].texto) + "' onKeyUp=\"actualizarDatos('U','texto',this);\" MaxLength='25'></td>");
                else
                    sb2.Append("<td style='padding-left:3px;'><input type='text' readonly class='txtV' style='width:120px' value='" + Utilidades.unescape(aParams[nIndice].texto) + "' MaxLength='25'></td>");

                sb2.Append("<td style='padding-left:3px;'><input type='text' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:120px' value='" + Utilidades.unescape(aParams[nIndice].nombre) + "' onKeyUp=\"actualizarDatos('U','nombre',this);\" MaxLength='25'></td>");
                
                //Tipo
                //sb2.Append("<td style='border-right: solid 1px #569BBD;'><select class=combo style='width:70px' onchange=\"actualizarDatos('U','tipo',this);\">");
                sb2.Append("<td><select class=combo style='width:70px' onchange=\"actualizarDatos('U','tipo',this);\">");
                sb2.Append("<option value='I' ");
                if (aParams[nIndice].tipo == "I") sb2.Append(" selected ");
                sb2.Append(">Entero</option>");
                sb2.Append("<option value='V'");
                if (aParams[nIndice].tipo == "V") sb2.Append(" selected ");
                sb2.Append(">Varchar</option>");
                sb2.Append("<option value='M'");
                if (aParams[nIndice].tipo == "M") sb2.Append(" selected ");
                sb2.Append(">Money</option>");
                sb2.Append("<option value='D'");
                if (aParams[nIndice].tipo == "D") sb2.Append(" selected ");
                sb2.Append(">Date</option>");
                sb2.Append("<option value='B'");
                if (aParams[nIndice].tipo == "B") sb2.Append(" selected ");
                sb2.Append(">Boolean</option>");
                sb2.Append("<option value='A'");
                if (aParams[nIndice].tipo == "A") sb2.Append(" selected ");
                sb2.Append(">Añomes</option>");
                sb2.Append("</select></td>");
                //Comentario
                sb2.Append("<td><input type='text' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:230px' value='" + Utilidades.unescape(aParams[nIndice].comentario) + "' onKeyUp=\"actualizarDatos('U','comentario',this);\" MaxLength='100'></td>");
                //Valor defecto
                sb2.Append("<td><input type='text' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:170px' value='" + Utilidades.unescape(aParams[nIndice].defecto) + "' onKeyUp=\"actualizarDatos('U','defecto',this);\" MaxLength='8000'></td>");
                //Visible
                sb2.Append("<td><select class=combo style='width:85px' onchange=\"actualizarDatos('U','visible',this);\">");
                sb2.Append("<option value='N' ");
                if (aParams[nIndice].visible == "N") sb2.Append(" selected ");
                sb2.Append(">No visible</option>");
                sb2.Append("<option value='V'");
                if (aParams[nIndice].visible == "V") sb2.Append(" selected ");
                sb2.Append(">Visible</option>");
                sb2.Append("<option value='M'");
                if (aParams[nIndice].visible == "M") sb2.Append(" selected ");
                sb2.Append(">Modificable</option>");
                sb2.Append("</select></td>");
                sb2.Append("<td><input type='checkbox' class='check' onclick=\"actualizarDatos('U','opcional',this);\" ");
                if (aParams[nIndice].opcional == "1") sb2.Append(" checked ");
                sb2.Append("></td>");
                sb2.Append("</tr>");
                //Puede que haya registros con el mismo orden por lo que si no lo tratamos se machacarían valores en el array
                var iOrd=aParams[nIndice].orden;
                for (var i=iOrd; i < 32000; i++){
                    if (aParamOrd[i]==null){
                        aParamOrd[i]=sb2.ToString();
                        break;
                    }
                }
            }
        }
        sb.Append(aParamOrd.join(''));
        sb.Append("</tbody>");
        sb.Append("</table>");

        $I("divValores").children[0].innerHTML = sb.ToString();
        tbodyParam = document.getElementById('tbodyDatosParam'); 
        tbodyParam.onmousedown = startDragIMGParam; 
	}catch(e){
		mostrarErrorAplicacion("Error al refrescar los parámetros", e.message);
    }
}
function mfa(oFila, sAccion){ 
    try{
        if (bLectura) return; 
        switch(sAccion){ //Para los casos en los que se quiere indicar la acción en tablas que no tienen las imágenes que indican el estado
            case "I": oFila.setAttribute("bd","I"); break;
            case "U": if (oFila.getAttribute("bd") != "I") { oFila.setAttribute("bd","U"); } break;
            case "D": oFila.setAttribute("bd", "D"); break;
            case "N": oFila.setAttribute("bd", "");  break;
        }
        
        if (oFila.cells[0].children[0] == null) return;
        if (oFila.cells[0].children[0].src == null) return;
        if (oFila.cells[0].children[0].src.indexOf("imgFI.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFU.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFD.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFN.gif")==-1) return;
        switch(sAccion){
            case "I":
                oFila.cells[0].children[0].src = strServer +"images/imgFI.gif";
                activarGrabar();
                break;
            case "U":
                if (oFila.getAttribute("bd") != "I") {
                    oFila.cells[0].children[0].src = strServer +"images/imgFU.gif";
                }
                activarGrabar();
                break;
            case "D":
                oFila.cells[0].children[0].src = strServer +"images/imgFD.gif";
                activarGrabar();
                break;
            case "N":
                oFila.cells[0].children[0].src = strServer +"images/imgFN.gif";
                break;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al indicar actualización de la fila", e.message);
    }
}
function getidCons(idOld, sCadena){
    try{
        var sElem, sRes="";
        var aLista = sCadena.split("@@");
        for (var i=0;i<aLista.length;i++){
            sElem = aLista[i];
            if (sElem != ""){
                var aCons = sElem.split("##");
                if (aCons[0] == idOld){
                    sRes = aCons[1];
                    break;
                }
            }
        }
        return sRes;
    }catch(e){
	    mostrarErrorAplicacion("Error al buscar el código de la consulta insertada", e.message);
    }
}
function setProfesionales(nIdCons){
    try {
        sID = nIdCons;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bIrDetalle = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    desActivarGrabar();
                    LlamarAsign(nIdCons);
                }
            });
        }
        else
            LlamarAsign(nIdCons);
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar la asignación de profesionales", e.message);
    }
}
function LlamarAsign(nIdCons)
{
    try
    {
        if (nIdCons >= 0) {
            location.href = "../Asignacion/Default.aspx?nIdCons=" + nIdCons;
        } else {
            mmoff("War", "Debes grabar los datos de la consulta para asociar profesionales a la misma", 500, 3000);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en LlamarAsign", e.message);
    }
}
function mostrarComentario(){
    try{
        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].className == "FS") {
                //alert(tblDatos.rows[i].cells[5].innerText);
                strComentario = $I("tblDatos").rows[i].cells[5].children[0].innerText;

                mostrarProcesando();
        	    //window.focus();
                modalDialog.Show(strServer + "Capa_Presentacion/Administracion/ConsultasPer/Catalogo/Comentario.aspx", self, sSize(450, 250))
                    .then(function(ret) {
        	            if (ret != null && ret != strComentario) {
        	                $I("tblDatos").rows[i].cells[5].children[0].innerText = ret;
        	                $I("tblDatos").rows[i].cells[5].children[0].title = "";
        	                mfa($I("tblDatos").rows[i], "U");
        	            }
        	        });
        	    
        	    ocultarProcesando();
        	    
                break;
            }
        }
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar la descripción de la consulta.", e.message);
    }
}

function habCom(){
    AccionBotonera("comentariobot", "H");
}

function setDenominacion(oNombre){
    try {
        var oElem = getNextElementSibling(oNombre.parentNode);
        if (oElem.children[0].value == "") {
            oElem.children[0].value = oNombre.value;
            actualizarDatos('U', 'nombre', oElem.children[0]);
            fm_mn(oNombre.parentNode.parentNode);
        }
    }
    catch(e){
		mostrarErrorAplicacion("Error al establecer la denominación del parámetro.", e.message);
    }
}
