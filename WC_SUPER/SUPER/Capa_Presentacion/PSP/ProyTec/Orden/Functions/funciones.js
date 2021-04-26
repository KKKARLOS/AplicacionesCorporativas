var bSaliendo = false;
var bGetPE = false;

function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        desActivarGrabar();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function aceptar(){
    var strRetorno="F";
    bSalir=false;
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function unload(){
    if (!bSaliendo) salir();
}
function salir(){
     bSalir=false;
     bSaliendo=true;
     if (bCambios && intSession > 0) {
         jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
             if (answer) {
                 bSalir = true;
                 grabar();
             }
             bCambios = false;
             salirCerrarVentana();
         });
     }
     else salirCerrarVentana();
 }
 function salirCerrarVentana() {
     var strRetorno="F";
     var returnValue = strRetorno;
     //setTimeout("window.close();", 250);//para que de tiempo a grabar y actualizar "bCambios";
     modalDialog.Close(window, returnValue);
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "pt":
		        $I("divPTs").children[0].innerHTML = aResul[2];
		        $I("divPTs").scrollTop = 0;
		        desActivarGrabar();
                ocultarProcesando();
                break;
            case "grabar":
                desActivarGrabar();
                ocultarProcesando();
                if (bSalir) 
                    setTimeout("salir();", 50);
                else {
                    if (bGetPE) {
                        bGetPE = false;
                        setTimeout("LLamarObtenerProyectos();", 50);
                    }
                }
                break;
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[3]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            $I("hndIdPE").value = aResul[3].ToString("N",9, 0);
	            $I("txtPE").value = aResul[4];

                switch (aResul[7])
                {
                    case "A": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }
    	        
    	        setTimeout("getPT()", 20);
	            //activarGrabar();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
    }
}

function getPT(){
    try{
        mostrarProcesando();
                //Pongo la lista de PTs del PE seleccionado
        var js_args = "pt@#@"+id_proyectosubnodo_actual;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}

function grabar(){
    var sOrdenAnt, sOrdenAct;
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;
        
        var aPT = FilasDe("tblPTs");
        var js_args = "grabar@#@";
        for (var i = 0; i < aPT.length; i++){
            sOrdenAnt=aPT[i].getAttribute("ordenAnt");
            sOrdenAct=aPT[i].cells[1].children[0].value;
            if (sOrdenAnt != sOrdenAct){
                js_args += aPT[i].id + "##"+ sOrdenAct + "///"; 
            }
        }
        js_args += "@#@"; 

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del proyecto técnico", e.message);
    }
}

function comprobarDatos(){
    try{
        if (($I("hndIdPE").value == "")||($I("hndIdPE").value == "0")||($I("hndIdPE").value == "-1")){
            mmoff("War", "Debes indicar proyecto económico", 230);
            return false;
        }
        //Validaciones de los ordenes.
        var iNumRtpts=0;
        if ($I("tblPTs")!=null){
            var aFila = FilasDe("tblPTs");
            for (var i=0;i<aFila.length;i++){
                if (aFila[i].cells[1].children[0].value == ""){
                    iNumRtpts++;
                    break;
                }
            }
        }
        if (iNumRtpts>0){
            mmoff("War","Todos los proyecto técnicos deben tener un nº de orden",390);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function desActivarGrabar(){
    try{
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function activarGrabar(){
    try{
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function obtenerProyectos(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetPE = true;
                    bEnviar = grabar();
                }
                else {
                    bCambios = false;
                    LLamarObtenerProyectos();
                }
            });
        } else LLamarObtenerProyectos();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los económicos-1", e.message);
    }
}
function LLamarObtenerProyectos(){
    try{  
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sSoloAbiertos=1&sNoVerPIG=1"; //Solo proyectos abiertos
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                id_proyectosubnodo_actual = aDatos[0];
	                recuperarDatosPSN();
	            }
	        });
	    window.focus();    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos económicos-2", e.message);
    }
}

function recuperarDatosPSN(){
    try{
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        js_args += id_proyectosubnodo_actual;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}