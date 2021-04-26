function init(){
    try{
        if ($I("hdnErrores").value !=""){
            $I("imgcol1").style.visibility = "hidden";
            $I("imgcol2").style.visibility = "hidden";
            $I("imgcol3").style.visibility = "hidden";
            $I("imgcol4").style.visibility = "hidden";
            $I("imgcol5").style.visibility = "hidden";
            $I("btnSalir").style.visibility = "hidden";
        }
        if (!mostrarErrores()){
            ocultarProcesando();
            cerrarVentana();
            return;
        }
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function getResponsableLineas()
{
    try{
	    //var strEnlace = "../LineasResponsable/Default.aspx?ID="+ oFila.id + "&bNueva=false&sLectura=true&sOrigen=2";          
	    var strEnlace = "../LineasResponsable/Default.aspx";
	    //var ret = window.showModalDialog(strEnlace, self, sSize(700, 560));
	    modalDialog.Show(strEnlace, self, sSize(700, 560))
	        .then(function(ret) {
	            if (ret != null) {
	                //window.returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].cells[2].innerText + "@#@" + tblDatos.rows[indexFila].cells[3].innerText;

	                var aDatos = ret.split("@#@");
	                //var aId = aDatos[0].split("/");
	                //$I("hdnIDResponsable").value = aDatos[0];
	                $I("txtResponsable").value = aDatos[2];

	                var aDatos = ret.split("@#@");

	                $I("txtNumlinea").value = aDatos[0];
	                $I("txtNumExt").value = aDatos[1];
	                $I("hdnIDBeneficiario").value = aDatos[4];
	                $I("txtBeneficiario").value = aDatos[3];
	                getConsumos();
	            }    	        
	        });  
	}catch(e){
		mostrarErrorAplicacion("Error en la función getResponsable", e.message);
    }
}
function getConsumos()
{
    try{
        //        if (!ControlSinCriterios())
        //        {
        //            $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='mano' style='width:934px;'></table>"; 
        //            $I("lblNumLineas").innerText = ""; 
        //            return;
        //        }

        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("buscar@#@");

        sb.Append(Utilidades.escape($I("txtNumlinea").value) + "@#@");
        sb.Append(Utilidades.escape($I("cboFechaFra").value) + "@#@");
        sb.Append(Utilidades.escape($I("hdnIDBeneficiario").value) + "@#@");

        RealizarCallBack(sb.ToString(), "");

        //$I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='mano' style='width:934px;'></table>";
        //$I("lblNumLineas").innerText = "";        
 	}catch(e){
		mostrarErrorAplicacion("Error en la función getFactura", e.message);
    }   
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").className = "fondoPapel";
                $I("divCatalogo").innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                //window.focus();
                break;
   
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}
function getMesValor() {
    try {
        mostrarProcesando();
        //var ret = window.showModalDialog("../MailConsumos/default.aspx", self, "dialogwidth:270px; dialogheight:215px; center:yes; status:NO; help:NO;");
        modalDialog.Show("../MailConsumos/default.aspx", self, sSize(420, 250))
	        .then(function(ret) {
            ocultarProcesando();
	        });  
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el nodo destino.", e.message);
    }
}
