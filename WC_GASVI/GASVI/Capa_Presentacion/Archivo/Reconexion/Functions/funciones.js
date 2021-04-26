function init(){
    try{       
        $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function CargarDatos(){
    try{
        if ($I("txtApellido1").value==""&& $I("txtApellido2").value==""&& $I("txtNombre").value=="")
        {
            alert("Debe indicar el nombre o apellidos del profesional");
            return;
        }
        mostrarProcesando();
        
   	    var js_args = "CargarProf@#@"+Utilidades.escape($I("txtApellido1").value)+"@#@"+Utilidades.escape($I("txtApellido2").value)+"@#@"+Utilidades.escape($I("txtNombre").value);
        RealizarCallBack(js_args,"");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al cargar los profesionales", e.message);
    }
}

function SeleccionProfesional(oFila){
    try{
        //location.href = strServer + "Default.aspx?scr="+ codpar(oFila.id);
        location.href = strServer + "Default.aspx?scr="+ codpar(oFila.id);// +"&iu="+ codpar(oFila.idUsuario);
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el profesional", e.message);
    }
}

/* El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error" */
function RespuestaCallBack(strResultado, context){
    try{
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            mostrarErrorSQL(aResul[3], aResul[2]);
        }else{
            switch (aResul[0])
            {
                case "CargarProf":
                    $I("txtApellido1").value="";
                    $I("txtApellido2").value="";
                    $I("txtNombre").value="";
                    $I("txtApellido1").focus();
		            $I("divCatalogo").children[0].innerHTML = aResul[2];
		            $I("divCatalogo").scrollTop = 0;
		            scrollTablaProf();
		            actualizarLupas("tblTitulo", "tblDatos");
                    break;

                default:
                    ocultarProcesando();
                    alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
            }
            ocultarProcesando();
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la respuesta del callback.", e.message);
    }        
}

var nTopScrollFICEPI = -1;
var nIDTimeFICEPI = 0;
function scrollTablaProf(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollFICEPI){
            nTopScrollFICEPI = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeFICEPI);
            nIDTimeFICEPI = setTimeout("scrollTablaProf()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollFICEPI/20);
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);
     
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("sexo") =="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                    }
                }
                
                if (oFila.getAttribute("baja")=="1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch(e){
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
    }
}       