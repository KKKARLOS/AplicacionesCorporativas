function init(){
    try {

        setOp($I("imgNE"), 30);
        setOp($I("imgNMP"), 30);
        setOp($I("imgBT"), 30);
        setOp($I("imgPC"), 30);
        setOp($I("imgCO"), 30);
        setOp($I("imgAP"), 30);
        setOp($I("imgADM"), 30);
        $I("ctl00_SiteMapPath1").innerText = " > Archivo > Inicio";


//        alert(bNuevoGasvi + "\n" + bIsInRoleT + "\n" + bIsInRoleS);
        if (bIsInRoleT || bIsInRoleS || bAdministrador) {
            $I("imgNE").onclick = function () { nuevo('NE'); };

            $I("imgNE").onmouseover = function () {
                this.src = '../../images/imgNuevaSolicitudSstandardColor.png';
            };

            $I("imgNE").onmouseout = function () {
                this.src = '../../images/imgNuevaSolicitudStandad.png';
            };

            $I("imgNMP").onclick = function () { nuevo('NMP'); };

            $I("imgNMP").onmouseover = function () {
                this.src = '../../images/imgNuevaSolicitudMultipleColor.png';
            };

            $I("imgNMP").onmouseout = function () {
                this.src = '../../images/imgNuevaSolicitudMultiple.png';
            };


            $I("imgBT").onclick = function () { nuevo('BT'); };

            $I("imgBT").onmouseover = function () {
                this.src = '../../images/imgBonoTransporteColor.png';
            };

            $I("imgBT").onmouseout = function () {
                this.src = '../../images/imgBonoTransporte.png';
            };

            $I("imgPC").onclick = function () { nuevo('PC'); };

            $I("imgPC").onmouseover = function () {
                this.src = '../../images/imgPagoConcertadoColor.png';
            };

            $I("imgPC").onmouseout = function () {
                this.src = '../../images/imgPagoConcertado.png';
            };

            setOp($I("imgNE"), 100);
            setOp($I("imgNMP"), 100);
            setOp($I("imgBT"), 100);
            setOp($I("imgPC"), 100);
        }else if (bNuevoGasvi) {
            $I("imgNE").onclick = function () { nuevo('NE'); };

            $I("imgNE").onmouseover = function () {
                this.src = '../../images/imgNuevaSolicitudSstandardColor.png';
            };

            $I("imgNE").onmouseout = function () {
                this.src = '../../images/imgNuevaSolicitudStandad.png';
            };

            $I("imgNMP").onclick = function () { nuevo('NMP'); };

            $I("imgNMP").onmouseover = function () {
                this.src = '../../images/imgNuevaSolicitudMultipleColor.png';
            };

            $I("imgNMP").onmouseout = function () {
                this.src = '../../images/imgNuevaSolicitudMultiple.png';
            };

            setOp($I("imgNE"), 100);
            setOp($I("imgNMP"), 100);
            if (bBono){
                $I("imgBT").onclick = function () { nuevo('BT'); };

                $I("imgBT").onmouseover = function () {
                    this.src = '../../images/imgBonoTransporteColor.png';
                };

                $I("imgBT").onmouseout = function () {
                    this.src = '../../images/imgBonoTransporte.png';
                };

                setOp($I("imgBT"), 100);
            }
            else $I("imgBT").style.cursor = "not-allowed";
            if (bPago){
                $I("imgPC").onclick = function () { nuevo('PC'); };

                $I("imgPC").onmouseover = function () {
                    this.src = '../../images/imgPagoConcertadoColor.png';
                };

                $I("imgPC").onmouseout = function () {
                    this.src = '../../images/imgPagoConcertado.png';
                };

                setOp($I("imgPC"), 100);
            }
            else $I("imgPC").style.cursor = "not-allowed";
        }
        else{
            $I("imgNE").style.cursor = "not-allowed";
            $I("imgNMP").style.cursor = "not-allowed";
            $I("imgBT").style.cursor = "not-allowed";
            $I("imgPC").style.cursor = "not-allowed";
        }
        
        if (nNotasPendientes > 0){
            $I("imgAP").onclick = function () { location.href = "../AccionesPendientes/Default.aspx?so=in"; };

            $I("imgAP").onmouseover = function () {
                this.src = '../../images/imgAccionesPendientesColor.png';
            };

            $I("imgAP").onmouseout = function () {
                this.src = '../../images/imgAccionesPendientes.png';
            };

            //$I("imgAP").attachEvent('onclick', location.href="../AccionesPendientes/Default.aspx?so=in");
            $I("divNumAP").innerText = nNotasPendientes.ToString();
            $I("divMarcoAcciones").style.visibility = "visible";
            setOp($I("imgAP") ,100);
        }
        if (bAdministrador){
            //$I("imgADM").onclick = function() { location.href = "../Administracion/Default.aspx"; };
            $I("imgADM").attachEvent('onclick', admref);

            $I("imgADM").onmouseover = function () {
                this.src = '../../images/imgAdministracionColor.png';
            };

            $I("imgADM").onmouseout = function () {
                this.src = '../../images/imgAdministracion.png';
            };
            //$I("divMarcoADM").style.visibility = "visible";
            setOp($I("imgADM") ,100);
        }

        if (bMiAmbito) {
            $I("imgCO").onclick = function () { goMiAmbito(); };

            $I("imgCO").onmouseover = function () {
                this.src = '../../images/imgConsultasSolicitudAmbitoColor.png';
            };

            $I("imgCO").onmouseout = function () {
                this.src = '../../images/imgConsultasSolicitudAmbito.png';
            };

            setOp($I("imgCO"), 100);
        } else {
            $I("imgCO").style.cursor = "not-allowed";
        }
        //crearExcelServidor("<table><tr><td style='color:navy;font-weight:bold;'>1A</td><td>1B</td></tr><tr><td>2A</td><td>2B</td></tr></table>", "Prueba");
    } catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function admref(){
    location.href="../Administracion/Default.aspx"
}
function nuevo(sOpcion){
    try {
        var aFilas = FilasDe("tblDatos");
        if (aFilas != null) {
            for (var i = 0; i < aFilas.length; i++) {
                if (aFilas[i].getAttribute("estado") == "B" || aFilas[i].getAttribute("estado") == "O") {
                    alert("No se permite la creación de nuevas solicitudes si existe alguna en estado \"No aprobada\" o \"No aceptada\".\n\nCorríjalas y vuelva a tramitarlas, o anúlelas directamente.");
                    return;
                }
            }
        }
        
        switch(sOpcion){
            case "NE": location.href = "../NotaEstandar/Default.aspx?ni=" + codpar("0"); break;
            case "NMP": location.href = "../NotaMultiProyecto/Default.aspx?ni=" + codpar("0"); break;
            case "BT": location.href = "../BonoTransporte/Default.aspx?ni=" + codpar("0"); break;
            case "PC": location.href = "../PagoConcertado/Default.aspx?ni=" + codpar("0"); break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir a crear una nueva solicitud", e.message);
    }
}

function md(oFila){
    try{
        //alert("Referencia: "+oFila.id +"\nTipo: "+ oFila.tipo +"\nEstado: "+ oFila.estado);
        if (oFila.getAttribute("estado") == "P"){ //Nota aparcada
            switch(oFila.getAttribute("tipo")){
                case "ANE": location.href = "../NotaEstandar/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")) + "&st=" + codpar(oFila.getAttribute("tipo")); break;
                case "ANM": location.href = "../NotaMultiProyecto/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")) + "&st=" + codpar(oFila.getAttribute("tipo")); break;
            }
        }else{
            switch(oFila.getAttribute("tipo")){
                case "E": location.href = "../NotaEstandar/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")) + "&st=" + codpar(oFila.getAttribute("tipo")); break;
                case "P": location.href = "../PagoConcertado/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")); break;
                case "B": location.href = "../BonoTransporte/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")); break;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle de una solicitud", e.message);
    }
}

function goMisSolicitudes() {
    try {
        location.href = "../Consultas/MisSolicitudes/Default.aspx?so=in";
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la pantalla de \"Mis solicitudes\".", e.message);
    }
}

function goMiAmbito() {
    try {
        location.href = "../Consultas/MiAmbito/Default.aspx";
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la pantalla de \"Solicitudes de mi ámbito\".", e.message);
    }
}

