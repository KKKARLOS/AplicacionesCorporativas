function init(){
    try{
        $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function CargarDatos(){
    try{
        if ($I("txtApellido1").value==""&&$I("txtApellido2").value==""&&$I("txtNombre").value=="")
        {
            alert("Debe indicar el nombre o apellidos de la persona");
            return;
        }
   	    var js_args = "profesionales@#@"+escape($I("txtApellido1").value)+"@#@"+escape(document.all("txtApellido2").value)+"@#@"+escape($I("txtNombre").value);
        mostrarProcesando();
        RealizarCallBack(js_args,"");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al cargar los profesionales", e.message);
    }
}

function SeleccionProfesional(oFila){
    try{
        //location.href = strServer + "Default.aspx?scr="+ codpar(oFila.id);
        location.href = strServer + "Default.aspx?scr="+ codpar(oFila.id);
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
                case "profesionales":
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

//var oImgEM = document.createElement("<img src='../../../images/imgUsuEM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgIM = document.createElement("<img src='../../../images/imgUsuIM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgEV = document.createElement("<img src='../../../images/imgUsuEV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgIV = document.createElement("<img src='../../../images/imgUsuIV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

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
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollFICEPI/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                       oFila.cells[0].appendChild(oImgIV.cloneNode(), null); 
                }else{

                       oFila.cells[0].appendChild(oImgIM.cloneNode(), null);
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch(e){
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
    }
}       