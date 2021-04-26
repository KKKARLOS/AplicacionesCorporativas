
function obtener(){
  try{  
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("obtener@#@");
        sb.Append($I("txtApellido1").value + "@#@");
        sb.Append($I("txtApellido2").value + "@#@");
        sb.Append($I("txtNombre").value + "@#@");
        sb.Append($I("txtPromotor").getAttribute("idF") + "@#@");
        sb.Append((($I("chkBloqueados").checked)? "1": "0") + "@#@");
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos.", e.message);
    }
}
function limpiar() {
    $I("divCatalogo").children[0].innerHTML = "";
}

function RespuestaCallBack(strResultado, context){
try{
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "obtener":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                break;
            default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ").", 410);;
        }
    }
        ocultarProcesando();
 }
 catch(e)
 {
    mostrarErrorAplicacion("Error al obtener los datos.", e.message);
 }
}

function abrirPromotor () {

    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getProfesional.aspx";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(450, 530))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtPromotor").value = aDatos[1];
                    $I("txtPromotor").setAttribute("idF", aDatos[2]);
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el promotor.", e.message);
    }
}

function borrarPromotor(){
    $I("txtPromotor").value ="";
    $I("txtPromotor").setAttribute("idF", ""); 
}

var oImgOK = document.createElement("img");
oImgOK.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgOk.gif");
oImgOK.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila;
        if ($I("divCatalogo").offsetHeight != 'undefined')
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblCatalogo").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblCatalogo").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if ($I("tblCatalogo").rows[i].getAttribute("sw")=="0") {
                oFila = $I("tblCatalogo").rows[i];
                oFila.setAttribute("sw", 1);
                //oFila.setAttribute("class","MANO");

                oFila.onclick = function() { ms(this); };
                oFila.ondblclick = function() { abrirDetalle(this.id); };


                if (oFila.getAttribute("sexo") == "V") 
                    oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); 
                else 
                    oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); 
                    
                if (oFila.cells[6].getAttribute("bloqueado") == "1")
                    oFila.cells[6].appendChild(oImgOK.cloneNode(true), null); 
                
                
                var oNOBR = document.createElement("nobr");
                oNOBR.className = 'NBR W280';
                oNOBR.appendChild(document.createTextNode(oFila.cells[1].innerText));
                oFila.cells[1].innerHTML = oNOBR.outerHTML;
                
                var oNOBR2 = document.createElement("nobr");
                oNOBR2.className = 'NBR W280';
                oNOBR2.appendChild(document.createTextNode(oFila.cells[2].innerText));
                oFila.cells[2].innerHTML = oNOBR2.outerHTML;
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}


function abrirDetalle (idFicepi) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Foraneos/Consultas/Detalle/Default.aspx?idF=" + codpar(idFicepi);
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(900, 430))
            .then(function(ret) {
                if (ret != null) {
                    //obtener();
                    var aResul = ret.split("##");
                    var aFila = FilasDe("tblCatalogo");
                    for (var i = 0; i < aFila.length; i++) {
                        if (aFila[i].getAttribute("id") == idFicepi) {
                            aFila[i].setAttribute("sw", "0");
                            aFila[i].cells[1].children[0].innerText = aResul[0];
                            if (aResul[1] == "S") {
                                aFila[i].setAttribute("bloqueado", "1");
                                //aFila[i].cells[4].children[0].checked = true;
                                if (aFila[i].cells[6].innerHTML == "") {
                                    aFila[i].cells[6].appendChild(oImgOK.cloneNode(true), null);
                                }
                            }
                            else {
                                aFila[i].setAttribute("bloqueado", "0");
                                //aFila[i].cells[4].children[0].checked = false;
                                if (aFila[i].cells[6].innerHTML != "") {
                                    aFila[i].cells[6].removeChild(aFila[i].cells[6].children[0]);
                                }
                            }
                            aFila[i].setAttribute("sexo", aResul[2]);
                            if (aResul[2] == "M")
                                aFila[i].cells[0].children[0].setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuFM.gif");
                            else
                                aFila[i].cells[0].children[0].setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuFV.gif");
                            break;
                        }
                    }
                    //scrollTabla();
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al abrir el detalle del profesional.", e.message);
    }
}

//function abrirDetalle (idFicepi) {
//    try {
//        mostrarProcesando();
//        var strEnlace = "../Detalle/Default.aspx?idF="+ codpar(idFicepi);
//        window.open(strEnlace);
//        ocultarProcesando();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al abrir el detalle del profesional.", e.message);
//    }
//}


function excel() {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila == null) {
            ocultarProcesando();
            mmoff("War","No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:auto'>Profesional</TD>");
        sb.Append("        <td style='width:auto'>Promotor</TD>");
        sb.Append("        <td style='width:auto'>Fecha alta</TD>");
        sb.Append("        <td style='width:auto'>Fecha última conexión</TD>");
        sb.Append("        <td style='width:auto'>Número de proyectos de alta</TD>");
        sb.Append("        <td style='width:auto'>Bloqueado</TD>");
        sb.Append("	</TR>");
        for (var i = 0; i < aFila.length; i++) {
           sb.Append("<tr>");
           sb.Append("<td>" + aFila[i].cells[1].innerText + "</td>");
           sb.Append("<td>" + aFila[i].cells[2].innerText + "</td>");
           sb.Append("<td>" + aFila[i].cells[3].innerText + "</td>");
           sb.Append("<td>" + aFila[i].cells[4].innerText + "</td>");
           sb.Append("<td>" + aFila[i].cells[5].innerText + "</td>");
           sb.Append("<td>" + ((aFila[i].cells[6].getAttribute("bloqueado") == "1") ? "Sí" : "No") + "</td>");
           sb.Append("</tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        //var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}