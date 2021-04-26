var indexFila = 0;

//function marcarUnaFila(idFila, fila) {
//    //alert("idFila=" + idFila + "  fila=" + fila);
//    indexFila = fila;
//    if (document.all) {
//        Opciones(fila).className= "FS";
//        for (i = 0; i < Opciones.length; i++) {
//            if (Opciones(i).id != idFila) {
//                if (i % 2 == 0) {
//                    Opciones(i).className="FA";
//                }
//                else {
//                    Opciones(i).className="FB";
//                }
//            }
//        }
//    }
//    else {
//        Opciones(fila).setAttribute("class", "FS");
//        for (i = 0; i < Opciones.length; i++) {
//            //if ((Opciones(i).id != idFila) && (Opciones(fila).getAttribute("class") == "FS")) {
//            if (Opciones(i).id != idFila) {
//                if (i % 2 == 0) {
//                    Opciones(i).setAttribute("class", "FA");
//                }
//                else {
//                    Opciones(i).setAttribute("class", "FB");
//                }
//            }
//        }
//    }
//}
function marcarUnaFila(objTabla, idFila, fila) {
    //alert(objTabla);
    //alert(idFila);
    //alert(fila);
    var indexFila = fila;
    var j = 0;
    
    if (document.all) {
        Opciones(fila).className = "FS";
        for (i = 0; i < Opciones.length; i++) {
            if (Opciones(i).id != idFila) {
                if (i % 2 == 0) {
                    Opciones(i).className = "FA";
                }
                else {
                    Opciones(i).className = "FB";
                }
            }
        }
    }
    else {
        var aFila = $I(objTabla).getElementsByTagName("TR");

        for (i = 0; i < aFila.length; i++) {
            if (aFila[i].id == idFila) {
                if (aFila[i].getAttribute("class") == "FS") {
                    if (i % 2 == 0)
                        aFila[i].setAttribute("class", "FA");
                    else
                        aFila[i].setAttribute("class", "FB");
                }
                else {
                    aFila[i].setAttribute("class", "FS");
                }
            }
            else {
                if (i % 2 == 0)
                    aFila[i].setAttribute("class", "FA");
                else
                    aFila[i].setAttribute("class", "FB");
            }
        }
    }
}

function aceptar() {
    strOpciones = "";

    if (indexFila != 0) {//oFila.getAttribute("cualidad")
        //strOpciones = tblOpciones.rows(indexFila).id + "//" + tblOpciones.rows(indexFila).children[0].textContent + "//" + tblOpciones.rows(indexFila).idFicepi;
        if (document.all) {
            strOpciones = tblOpciones.rows[indexFila].id + "//" +
	                  tblOpciones.rows[indexFila].children[0].innerText + "//" +
	                  tblOpciones.rows[indexFila].getAttribute("idFicepi");
        }
        else {
            strOpciones = tblOpciones.rows[indexFila].id + "//" +
	                  tblOpciones.rows[indexFila].children[0].textContent + "//" +
	                  tblOpciones.rows[indexFila].getAttribute("idFicepi");
        }
    } else {
        for (i = 0; i < tblOpciones.rows.length; i++) {
            //if (tblOpciones.rows(i).className == "FS") {
            if (tblOpciones.rows[i].getAttribute("class") == "FS") {
                //strOpciones += tblOpciones.rows(i).id + "//" + tblOpciones.rows(i).children[0].textContent + "//" + tblOpciones.rows(i).idFicepi + "@@";
                if (document.all) {
                    strOpciones += tblOpciones.rows[i].id + "//" +
			                   tblOpciones.rows[i].children[0].innerText + "//" +
			                   tblOpciones.rows[i].getAttribute("idFicepi") + "@@";
                }
                else {
                    strOpciones += tblOpciones.rows[i].id + "//" +
			                   tblOpciones.rows[i].children[0].textContent + "//" +
			                   tblOpciones.rows[i].getAttribute("idFicepi") + "@@";
                }
            }
        }
        strOpciones = strOpciones.substring(0, strOpciones.length - 2);
        if (strOpciones == "") {
            alert("Seleccione algún <%=strColumna%>");
            return;
        }
    }
    var returnValue = strOpciones;
    modalDialog.Close(window, returnValue);    
}

function aceptarClick(indexFila) {
    //strOpciones = tblOpciones.rows(indexFila).id + "//" + tblOpciones.rows(indexFila).children[0].textContent + "//" + tblOpciones.rows(indexFila).idFicepi;
    if (document.all) {
        strOpciones = tblOpciones.rows[indexFila].id + "//" +
                  tblOpciones.rows[indexFila].children[0].innerText + "//" +
                  tblOpciones.rows[indexFila].getAttribute("idFicepi");
    }
    else {
        strOpciones = tblOpciones.rows[indexFila].id + "//" +
                  tblOpciones.rows[indexFila].children[0].textContent + "//" +
                  tblOpciones.rows[indexFila].getAttribute("idFicepi");
    }
    var returnValue = strOpciones;
    modalDialog.Close(window, returnValue);      
}

function cerrarVentana() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);  
}
