// JScript File
var aPSN = new Array();

function objPSN(idPSN, idProyecto, denominacion, cliente, nodo, traspasoIAP, estado, categoria, cualidad, tiene_consumos, annomes, estadomes, modelocoste){
	this.idPSN	        = idPSN;
	this.idProyecto	    = idProyecto;
	this.denominacion   = denominacion;
	this.cliente		= cliente;
	this.nodo		    = nodo;
	this.traspasoIAP	= traspasoIAP;
	this.estado		    = estado;
	this.categoria		= categoria;
	this.cualidad		= cualidad;
	this.tiene_consumos	= tiene_consumos;
	this.annomes		= annomes;
	this.estadomes      = estadomes;
	this.modelocoste	= modelocoste;
}

function insertarPSNEnArray(idPSN, idProyecto, denominacion, cliente, nodo, traspasoIAP, estado, categoria, cualidad, tiene_consumos, annomes, estadomes, modelocoste){
    try{
        var oPSN = new objPSN(idPSN, idProyecto, denominacion, cliente, nodo, traspasoIAP, estado, categoria, cualidad, tiene_consumos, annomes, estadomes, modelocoste);
        aPSN[aPSN.length]= oPSN;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un proyectosubnodo en el array.", e.message);
    }
}

function mostrarDatosPSN(idPSN){
    try{
        for (var nIndice=0; nIndice < aPSN.length; nIndice++){
            if (aPSN[nIndice].idPSN == idPSN){
                var objPSN = aPSN[nIndice];
                var strMsg = "idPSN: "+ objPSN.idPSN +"\nidProyecto: "+ objPSN.idProyecto +"\ndenominacion: "+ objPSN.denominacion +"\ncliente: "+ objPSN.cliente +"\n";
                strMsg += "nodo: "+ objPSN.nodo +"\ntraspasoIAP: "+ objPSN.traspasoIAP +"\nestado: "+ objPSN.estado +"\ncategoria: "+ objPSN.categoria +"\ncualidad: "+ objPSN.cualidad +"\n";
                strMsg += "tiene_consumos: "+ objPSN.tiene_consumos +"\nannomes: "+ objPSN.annomes +"\nestadomes: "+ objPSN.estadomes  +"\nmodelocoste: "+ objPSN.modelocoste +"\n";
                mmoff("Inf", strMsg, 400);
                return;
             }
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos de un profesional del array.", e.message);
    }
}
