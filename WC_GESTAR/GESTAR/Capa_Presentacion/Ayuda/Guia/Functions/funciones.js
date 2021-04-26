function mostrarAyuda(nValor){
    try{
        var strArchivo = "";
        switch (nValor)
        {
            case 1: strArchivo = "Manualusuario21.pdf"; break;
        }
        if (strArchivo == "") return;
        window.open("PDF/"+strArchivo,"", "resizable=yes,status=no,scrollbars=no,menubar=no,top="+ eval(screen.availHeight/2-384)+",left="+ eval(screen.availwidth/2-512) +",width=1010px,height=705px");	
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el archivo de ayuda", e.message);
    }
}

