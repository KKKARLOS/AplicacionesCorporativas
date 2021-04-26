var bProcesoNoCorrecto = false;
var bMostrar = false;

function init(){
    try{
        ToolTipBotonera("procesar","Procesar réplica");

        if (origen=="proynocerrados")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; Cierre mensual &gt; Proyectos no cerrados &gt; Cierre Mensual &gt; Réplica ";
        else if (origen=="carrusel")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; Seguimiento &gt; Detalle económico (Carrusel) &gt; Cierre Mensual &gt; Réplica ";
        else if (origen=="menucierre")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; Cierre Mensual &gt; Réplica ";

        var tblDatos = $I("tblDatos");
        
        if (tblDatos==null || tblDatos.rows.length == 0){
            $I("imgSemaforo").src = "../../../Images/imgSemaforoA.gif";
            $I("divMsgR").style.display = "none";
            $I("divMsgA").style.display = "block";
            $I("divMsgA2").style.display = "none";
            $I("divMsgV").style.display = "none";
            $I("divMsg").style.display = "none";
        }else{
        if (tblDatos.rows.length == 1) ms(tblDatos.rows[0]); 
            var sw=0;
            for (var i=0; i<tblDatos.rows.length; i++){
                if (tblDatos.rows[i].cells[2].children[0].src.indexOf("imgRepNO.gif") > -1){
                    sw = 1;
                    break;
                }
            }
            if (sw == 1){
                $I("imgSemaforo").src = "../../../Images/imgSemaforoR.gif";
                $I("divMsgR").style.display = "block";
                $I("divMsgA").style.display = "none";
                $I("divMsgA2").style.display = "none";
                $I("divMsgV").style.display = "none";
                $I("divMsg").style.display = "none";
                AccionBotonera("procesar", "D");
            }else{
                $I("imgSemaforo").src = "../../../Images/imgSemaforoA.gif";
                $I("divMsgR").style.display = "none";
                $I("divMsgA").style.display = "none";
                $I("divMsgA2").style.display = "block";
                $I("divMsgV").style.display = "none";
                $I("divMsg").style.display = "none";
                AccionBotonera("procesar", "H");
            }
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        if (aResul[3] != "0"){
            if (aResul[3] == "1205" && nIntentosProcesoDeadLock < nLimiteIntentosProcesoDeadLock){
                nIntentosProcesoDeadLock++;
                bMostrar = true;
                mmoff("War", "Existen varios procesos ejecutándose simultáneamente. Disculpa la espera.", 500, 5000);
                setTimeout("procesar()", nSetTimeoutProcesoDeadLock);
            }else mostrarErrorSQL(aResul[3], aResul[2]);
        }else mostrarErrorSQL(aResul[3], aResul[2]);
        
		if (aResul[0] == "setReplica") AccionBotonera("procesar", "H");
    }else{
        switch (aResul[0]){
            case "setReplica":
                //alert(aResul[2]);
                bMostrar = false;
                ocultarProcesando();
                var tblDatos = $I("tblDatos");
                ms(tblDatos.rows[iFila]);
                $I("divCatalogo").scrollTop = (iFila) * 20;
                nIntentosProcesoDeadLock = 0;
                tblDatos.rows[iFila].setAttribute("procesado", "1");
                tblDatos.rows[iFila].cells[2].children[0].src = "../../../images/imgReplicado.gif";
                if (iFila == tblDatos.rows.length - 1) {
                    tblDatos.rows[iFila].className = "";
                    AccionBotonera("procesar", "D");
                    $I("imgSemaforo").src = "../../../Images/imgSemaforo.gif";
                    $I("divMsgR").style.display = "none";
                    $I("divMsgA").style.display = "none";
                    $I("divMsgA2").style.display = "none";
                    $I("divMsgV").style.display = "none";
                    $I("divMsg").style.display = "block";
                    //alert(opcion);
                    if (opcion == "cerrarlista") {
                        location.href = "../Cierre/Default.aspx?origen=" + origen + "&opcion=" + opcion + "&lp=" + lstProy;
                    }
                    else if (opcion == "cerrarmes") {
                            location.href = "../Cierre/Default.aspx?nProy=" + tblDatos.rows[0].id + "&nPSN=" + tblDatos.rows[0].getAttribute("nPSN") + "&sAnomes=" + sAnomes + "&origen=" + origen + "&opcion=" + opcion;
                        //return; //El return detiene el location.href en Chrome.
                    } else if (opcion == "menucierresat" || opcion == "menucierresatsaa") {
                        location.href = "../Cierre/Default.aspx?origen=" + origen + "&opcion=" + opcion;
                        //return; //El return detiene el location.href en Chrome.
                    }
                    for (var i = $I("tblNodos").rows.length - 1; i >= 0; i--) $I("tblNodos").deleteRow(i);
                }
                else {
                    bMostrar = true;
                    setTimeout("procesar()", 200);
                }
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (!bMostrar) ocultarProcesando();
    }
}

var oFilaATratar;
function getNodos(oFila){
    try{
        //alert(oFila.id);
        mostrarProcesando();
        oFilaATratar = oFila;
        setTimeout("getNodos2()", 20);

	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar los nodos", e.message);
    }
}

var oImgP = document.createElement("img");
oImgP.setAttribute("src", "../../../images/imgReplicaGestionada.gif");
oImgP.setAttribute("style", "width:16px; height:16px; border: 0px;");

var oImgJ = document.createElement("img");
oImgJ.setAttribute("src", "../../../images/imgReplicaNoGestionada.gif");
oImgJ.setAttribute("style", "width:16px; height:16px; border: 0px;");

var oImgReq = document.createElement("img");
oImgReq.setAttribute("src", "../../../images/imgRequerido.gif");
oImgReq.setAttribute("style", "border: 0px;");
oImgReq.className = "ICO";

var oImgOpc = document.createElement("img");
oImgOpc.setAttribute("src", "../../../images/imgOpcional.gif");
oImgOpc.setAttribute("style", "border: 0px;");
oImgOpc.className = "ICO";

var oImgGoma = document.createElement("img");
oImgGoma.setAttribute("src", "../../../images/imgBorrar.gif");
oImgGoma.setAttribute("style", "visibility:hidden; border: 0px;");
oImgGoma.title = 'Borra el responsable seleccionado';
oImgGoma.className = "ICO MANO";

function getNodos2(){
    try{
        //alert(oFila.id);
        oFila = oFilaATratar;
        var tblNodos = $I("tblNodos");
        
        for (var i=tblNodos.rows.length-1;i>=0;i--) tblNodos.deleteRow(i);
        var oCtrl2;
        for (var i=0; i < aNodoReplica.length; i++){
            if (aNodoReplica[i].idProyecto == oFila.id){
                //oNF --> objeto nueva fila
                oNF = tblNodos.insertRow(-1);
                oNF.setAttribute("idNodo", aNodoReplica[i].idNodo);
                oNF.setAttribute("idProyecto", aNodoReplica[i].idProyecto);
                oNF.setAttribute("idGestor", aNodoReplica[i].idGestor);
                oNF.setAttribute("tiporeplica", aNodoReplica[i].tiporeplica);
                oNF.setAttribute("propuestafirme", aNodoReplica[i].propuestafirme);
                oNF.style.height = "22px";
                oNF.onclick = function(){setNodoActivo(tblDatos.rows[iFila].id, this.getAttribute("idNodo"));};

                oNC1 = oNF.insertCell(-1);
	            oNC1.innerText = aNodoReplica[i].desNodo;
	            
                oNC2 = oNF.insertCell(-1);
                oNC2.align = "center";
                if (aNodoReplica[i].tiporeplica == "P")
                    oCtrl2 = oImgP.cloneNode(true);// document.createElement("<img border='0' src='../../../Images/imgReplicaGestionada.gif' width='16px' height='16px' />");
                else
                    oCtrl2 = oImgJ.cloneNode(true);// document.createElement("<img border='0' src='../../../Images/imgReplicaNoGestionada.gif' width='16px' height='16px' />");
                if (aNodoReplica[i].propuestafirme == 0){
                    if ($I("imgSemaforo").src.indexOf("imgSemaforo.gif") == -1) oCtrl2.className = "MA";
                    oCtrl2.ondblclick = function(){setTipoGestion(this.parentNode.parentNode);};
                }
	            oNC2.appendChild(oCtrl2);

                oNC3 = oNF.insertCell(-1);
                oNC3.ondblclick = function(){getGestor(this.parentNode);};
                if (aNodoReplica[i].idGestor == ""){// oNC3.className = "REQ MA";
                    oNC3.className = "MA";
                    if (aNodoReplica[i].tiporeplica == "P")
                        oNC3.appendChild(oImgReq.cloneNode(true), null);
                    else
                        oNC3.appendChild(oImgOpc.cloneNode(true), null);
                }else
    	            oNC3.innerText = aNodoReplica[i].nombreGestor;

    	        oNC4 = oNF.insertCell(-1);
    	        var oGom = oImgGoma.cloneNode(true);
    	        oGom.onclick = function() { borrarResponsable(this); };
    	        oNC4.appendChild(oGom);                
    	        if (aNodoReplica[i].idGestor != "") oNC4.children[0].style.visibility = "visible";
            }
        }
        if (!bMostrar) ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la necesidad de replicar el proyecto", e.message);
    }
}


function borrarResponsable(oGoma){
    try{
        oNodoActivo.idGestor = "";
        oGoma.parentNode.parentNode.setAttribute("idGestor","");
        oGoma.parentNode.parentNode.cells[2].innerText = "";
        oGoma.parentNode.parentNode.cells[2].className = "MA";
        
        if (oNodoActivo.tiporeplica == "P")
            oGoma.parentNode.parentNode.cells[2].appendChild(oImgReq.cloneNode(true), null);
        else
            oGoma.parentNode.parentNode.cells[2].appendChild(oImgOpc.cloneNode(true), null);
        oGoma.style.visibility = "hidden";
        
        comprobarSemaforoFila();
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el tipo de réplica", e.message);
    }
}

function setTipoGestion(oFila){
    try{
        if ($I("imgSemaforo").src.indexOf("imgSemaforo.gif") > -1) return;
        if (oFila.cells[1].children[0].src.indexOf("imgReplicaGestionada.gif") > -1){
            oFila.cells[1].children[0].src = "../../../images/imgReplicaNoGestionada.gif";
            oNodoActivo.tiporeplica = "J";
            oFila.setAttribute("tiporeplica","J");
            if (oNodoActivo.idGestor == ""){
                oFila.cells[2].children[0].src = "../../../images/imgOpcional.gif";
            }
        }else{
            oFila.cells[1].children[0].src = "../../../images/imgReplicaGestionada.gif";
            oNodoActivo.tiporeplica = "P";
            oFila.setAttribute("tiporeplica", "P");
            if (oNodoActivo.idGestor == ""){
                oFila.cells[2].children[0].src = "../../../images/imgRequerido.gif";
            }
        }
        comprobarSemaforoFila();
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el tipo de réplica", e.message);
    }
}

function getGestor(oFila){
    try{
        if ($I("imgSemaforo").src.indexOf("imgSemaforo.gif") > -1) return;
        mostrarProcesando();

        //var ret = window.showModalDialog("../getResponsable.aspx?tiporesp=crp&idNodo=" + oFila.getAttribute("idNodo") + "&sNodo=" + Utilidades.escape(oFila.cells[0].innerText) + "&tiporeplica=" + oFila.getAttribute("tiporeplica"), self, sSize(550, 540));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=crp&idNodo=" + oFila.getAttribute("idNodo") + "&sNodo=" + Utilidades.escape(oFila.cells[0].innerText) + "&tiporeplica=" + oFila.getAttribute("tiporeplica"), self, sSize(550, 540))
	        .then(function(ret) {
	            if (ret != null){
	                //alert(ret); 
		            var aDatos = ret.split("@#@");
		            oFila.setAttribute("idGestor", aDatos[0]);
		            if (oFila.cells[2].children.length>0)
    		            oFila.cells[2].children[0].removeNode();
                    oFila.cells[2].className = "MA";
		            oFila.cells[2].innerText = aDatos[1];
		            oNodoActivo.idGestor = aDatos[0];
		            oNodoActivo.nombreGestor = aDatos[1];
		            oFila.cells[3].children[0].style.visibility = "visible";
		            comprobarSemaforoFila();
	            }
                ocultarProcesando();
	        });             
	}catch(e){
		mostrarErrorAplicacion("Error al asignar el gestor", e.message);
    }
}

function comprobarSemaforoFila(){
    try{
        var swP = 0;
        var swG = 0;
        var tblNodos = $I("tblNodos");
        var tblDatos = $I("tblDatos");
        
        for (var i=0; i<tblNodos.rows.length; i++){
            if (tblNodos.rows[i].getAttribute("idGestor") == ""){
                swG = 1;
                if (tblNodos.rows[i].getAttribute("tiporeplica") == "P") {
                    swP = 1;
                    break;
                }
            }
        }
        
        if (swP == 1){
            
            tblDatos.rows[iFila].cells[2].children[0].src = "../../../Images/imgRepNO.gif";
            
//            $I("imgSemaforo").src = "../../../Images/imgSemaforoR.gif";
//            $I("divMsgR").style.display = "block";
//            $I("divMsgA").style.display = "none";
//            $I("divMsgA2").style.display = "none";
//            $I("divMsgV").style.display = "none";
//            $I("divMsg").style.display = "none";
//            ocultarProcesando();
//            return;
        }else{
            if (swG == 1) tblDatos.rows[iFila].cells[2].children[0].src = "../../../Images/imgRepPrec.gif";
            else tblDatos.rows[iFila].cells[2].children[0].src = "../../../Images/imgRepOK.gif";
        }


        var swRojo = 0;
        var swAmarillo = 0;
        for (var i=0; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].cells[2].children[0].src.indexOf("imgRepNO.gif") > -1){
                swRojo = 1;
                //break;
            }
            if (tblDatos.rows[i].cells[2].children[0].src.indexOf("imgRepPrec.gif") > -1){
                swAmarillo = 1;
                //break;
            }
        }

        if (swRojo == 1){
            $I("imgSemaforo").src = "../../../Images/imgSemaforoR.gif";
            $I("divMsgR").style.display = "block";
            $I("divMsgA").style.display = "none";
            $I("divMsgA2").style.display = "none";
            $I("divMsgV").style.display = "none";
            $I("divMsg").style.display = "none";
            AccionBotonera("procesar", "D");
        }else if (swAmarillo == 1){
            $I("imgSemaforo").src = "../../../Images/imgSemaforoA.gif";
            $I("divMsgR").style.display = "none";
            $I("divMsgA").style.display = "none";
            $I("divMsgA2").style.display = "block";
            $I("divMsgV").style.display = "none";
            $I("divMsg").style.display = "none";
            AccionBotonera("procesar", "H");
        }else{
            $I("imgSemaforo").src = "../../../Images/imgSemaforoV.gif";
            $I("divMsgR").style.display = "none";
            $I("divMsgA").style.display = "none";
            $I("divMsgA2").style.display = "none";
            $I("divMsgV").style.display = "block";
            $I("divMsg").style.display = "none";
            AccionBotonera("procesar", "H");
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los semáforos.", e.message);
    }
}


function procesar(){
    try{
        mostrarProcesando();
        bMostrar = true;
        var js_args = "setReplica@#@";
        var sw = 0;
        if (iFila < 0) iFila = 0;

        var tblDatos = $I("tblDatos");
                
        for (var i=iFila;i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].getAttribute("procesado") == ""){
                iFila = i;
                ms(tblDatos.rows[i]);
                js_args += tblDatos.rows[i].id +"@#@";
                js_args += tblDatos.rows[i].getAttribute("nPSN") + "@#@";
                js_args += tblDatos.rows[i].cells[1].innerText +"@#@";
                sw = 1;
                for (var nIndice=0; nIndice < aNodoReplica.length; nIndice++){
                    if (aNodoReplica[nIndice].idProyecto == tblDatos.rows[i].id){
                        js_args += aNodoReplica[nIndice].idNodo +"##";
                        js_args += aNodoReplica[nIndice].tiporeplica +"##";
                        js_args += aNodoReplica[nIndice].idGestor +"///";
                    }
                }
                break;
            }
        }

        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "No hay proyectos a replicar.", 250);
            return;
        }
        //alert(js_args);return;
        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos a procesar.", e.message);
    }
}
