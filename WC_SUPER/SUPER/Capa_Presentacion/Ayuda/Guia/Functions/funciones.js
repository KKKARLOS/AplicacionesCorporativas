function mostrarAyuda(nValor){
    try{
        var strArchivo = "";
        switch (nValor)
        {
            case 1: strArchivo = "GrupoFuncional.pdf"; break;
            case 2: strArchivo = "OTL.pdf"; break;
            case 3: strArchivo = "Calendarios.pdf"; break;
            case 4: strArchivo = "OficinaTecnica.pdf"; break;
            case 5: strArchivo = "PlantillasPST.pdf"; break;
            case 6: strArchivo = "TarifasPerfil.pdf"; break;
            case 7: strArchivo = "OrigenesdeTareas.pdf"; break;
            case 8: strArchivo = "OTC.pdf"; break;
            case 9: strArchivo = "AtributosEstadisticos.pdf"; break;
            case 10: strArchivo = "AvanceTecnico.pdf"; break;
            case 11: strArchivo = "CodigosExternos.pdf"; break;
            case 12: strArchivo = "EstructuraTecnica.pdf"; break;
            case 13: strArchivo = "DetallePT.pdf"; break;
            case 14: strArchivo = "DetalleFase.pdf"; break;
            case 15: strArchivo = "DetalleActividad.pdf"; break;
            case 16: strArchivo = "DetalleTarea.pdf"; break;
            case 17: strArchivo = "DetalleHito.pdf"; break;
            case 18: strArchivo = "Bitacora.pdf"; break;
            case 19: strArchivo = "Ocupacion.pdf"; break;
        }
        if (strArchivo == "") return;
        window.open("PDF/"+strArchivo,"", "resizable=yes,status=no,scrollbars=no,menubar=no,top="+ eval(screen.avalHeight/2-384)+",left="+ eval(screen.availWidth/2-512) +",width=1010px,height=705px");	
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el archivo de ayuda", e.message);
    }
}