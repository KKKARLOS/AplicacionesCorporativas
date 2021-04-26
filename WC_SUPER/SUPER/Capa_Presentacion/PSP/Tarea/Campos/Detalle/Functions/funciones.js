var bCrearNuevo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        if (!bCambios) {
            //setOp($I("btnNuevo"), 30);
            //setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
        }
        if (bNueva=="false")
        {
            $I("txtDenominacion").onkeyup = null;
        }
        //if (es_administrador == "" && sOrigen != 'ADM') {
        if (sOrigen != 'ADM') {
            removeByValue('cboAmbito', '0');
            CargarConcepto("1");
        }
        else
        {//Vengo del menú ADM por loque solo puedo dar de alta campos de ámbito Empresarial
            for (var i = $I("cboAmbito").length - 1; i >= 1; i--) {
                removeByValue('cboAmbito', i);
            }
            $I("cboAmbito").disabled = true;
        }

        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function removeByValue(id, value) {
    var select = document.getElementById(id);

    for (i=0;i<select.length;  i++) {
        if (select.options[i].value==value) {
            select.remove(i);
        }
    }
}

function unload(){

}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function salir() {
    bSalir = false;
    var returnValue = sRecargarCat;
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function aG(){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            bCambios = true;
            //setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));

    }else{
        switch (aResul[0]){
           case "grabar":
                sRecargarCat="S";
                bCambios = false;
                //bNueva='false';	
                //setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"), 30);
                //setOp($I("btnNuevo"), 100);
                //$I("hdnID").value = aResul[2];
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bCrearNuevo) {
                    bCrearNuevo = false;
                    //setTimeout("nuevo();", 100);
                }
                else {
                    if (bSalir)
                        salir();
                }
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDenominacion").value=="")
        {
            $I("txtDenominacion").focus();    
            mmoff('War', 'La denominación del campo es obligatoria', 320);
            //mmoff('Se debe indicar la denominación de la tarifa de datos', 400, null, null, 100, 100);            
            return false;
        }  
          
        if ($I("cboAmbito").value == "5" && $I("hdn_uidequipo").value == "") {
            mmoff('War', 'Debes indicar el equipo de profesionales', 320);
            return false;
        }
        if ($I("cboTipoDato").value == "") {
            $I("cboTipoDato").focus();
            mmoff('War', 'El tipo de dato es obligatorio', 260);
            return false;
        }
        // verificar que no se pueda duplicar con el mismo ambito y denominacion
        //var tbl = fOpener().$I("tblDatos");
        tblDatos = fOpener().$I("tblDatos");
        if (!tblDatos) return true;

        for (var i = 0; i < tblDatos.rows.length; i++) {
            if ($I("txtDenominacion").value == tblDatos.rows[i].cells[0].children[0].innerText
                &&                
                $I("cboAmbito").options[$I("cboAmbito").selectedIndex].innerText == tblDatos.rows[i].cells[1].children[0].innerText
                ) {
                mmoff("War", "No se permite que la denominación y el ambito de un campo estén repetidos.", 300);
                return false;
            }
         }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar(){
    try{
        if (getOp($I("btnGrabarSalir")) != 100) return;
        if (!comprobarDatos()) return;
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        if (bNueva == 'false') sb.Append("U##");                            //0 0=Update 1=Insert     
        else sb.Append("I##");
        
        sb.Append(dfn($I("hdnID").value) + "##");                           //1 ID     
        sb.Append(Utilidades.escape($I("txtDenominacion").value) + "##");   //2 Denominanción     
        sb.Append($I("cboTipoDato").value + "##");                          //3 Tipo de dato 
        sb.Append($I("cboAmbito").value + "##");                            //4 Ambito         
        sb.Append(dfn($I("hdn_ficepi_owner").value) + "##");                //5 ficepi_owner
        sb.Append(dfn($I("hdn_idproyectosubnodo").value) + "##");           //6 idproyectosubnodo
        sb.Append(Utilidades.escape($I("hdn_uidequipo").value) + "##");     //7 uidequipo
        //sb.Append(dfn($I("hdn_ficepi_actual").value) + "##");             //  ficepi_actual

        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");                
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de los campos", e.message);
    }
}
//function nuevo() {
//    try {
//        if (bCambios) {
//            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
//                if (answer) {
//                    bCrearNuevo = true;
//                    grabar();
//                }
//                else {
//                    setOp($I("btnGrabarSalir"), 30);
//                    NuevoContinuar();
//                }
//            });
//        }
//        else NuevoContinuar();

//    } catch (e) {
//        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
//    }
//}
//function NuevoContinuar() {
//    try {
//        $I("cboAmbito").value = "0";
//        $I("txtDenominacion").value = "";
//        $I("cboTipoDato").value = "F";
//        $I("lblAmbitoSel").innerText = "";
//        $I("hdnID").value = "0";
//        $I("hdn_ficepi_owner").value = "";
//        //$I("hdn_idproyectosubnodo").value = "";
//        $I("hdn_uidequipo").value = "";

//        bNueva = 'true';
//        bCambios = false;
//    } catch (e) {
//        mostrarErrorAplicacion("Error en NuevoContinuar", e.message);
//    }
//}

function CargarConcepto(strOpcion) {
    $I("lblConceptoEnlace").innerText = "";
    $I("lblConceptoEnlace").style.visibility = "hidden";
    $I("lblAmbitoSel").innerText = "";

    $I("hdn_ficepi_owner").value = "";
    //$I("hdn_idproyectosubnodo").value = "";
    $I("hdn_uidequipo").value = "";
    $I("lblAmbitoSel").style.visibility = "visible";

    switch (strOpcion) {
        case "0":
        //case "1":
        case "2":
        case "3":
        case "4":
            $I("lblConceptoEnlace").innerText = "";
            $I("lblConceptoEnlace").style.visibility = "visible";
            //$I("lblConceptoEnlace").style.top = "10px";
            //$I("lblConceptoEnlace").style.left = "200px";
            break;
        case "1":
            $I("lblConceptoEnlace").innerText = "";
            $I("lblConceptoEnlace").style.visibility = "visible";
            $I("lblAmbitoSel").innerText = sProfesional;
            $I("hdn_ficepi_owner").value = $I("hdn_ficepi_actual").value;
            break;
        case "5":
            $I("lblConceptoEnlace").innerText = "Equipo";
            $I("lblConceptoEnlace").style.visibility = "visible";
            if (nName != 'ie') $I("lblConceptoEnlace").style.left = "230px";

            break;
    }//Fin switch
}
function gestionAmbito() {
    try {

        switch ($I("cboAmbito").value) {
            case "0":
                $I("lblAmbitoSel").style.visibility = "hidden";
                aG();
                break;
            case "1":
                $I("lblAmbitoSel").innerText = sProfesional;
                $I("hdn_ficepi_owner").value = $I("hdn_ficepi_actual").value;
                aG();
                break;
            case "5":
                mostrarProcesando();
                //window.focus();
                var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/GP/getGP.aspx";
                modalDialog.Show(strEnlace, self, sSize(500, 470))
                    .then(function (ret) {
                        if (ret != null) {
                            var aDatos = ret.split("@#@");
                            $I("hdn_uidequipo").value = aDatos[0];
                            $I("lblAmbitoSel").innerText = aDatos[1];
                        }
                        aG();
                    });

                ocultarProcesando();
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la gestión del ámbito", e.message);
    }
}





