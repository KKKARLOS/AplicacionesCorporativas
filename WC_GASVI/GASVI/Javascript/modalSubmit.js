<!--

    if (!document.getElementById("hdnTitle"))
  	alert('Error: no se ha declarado el campo hdnTitle');
    else if ($I("hdnTitle").value == "") $I("hdnTitle").value = this.id_dialog_body;
    else this.id_dialog_body = $I("hdnTitle").value;

    this.parent.document.getElementById("ui-dialog-title-" + this.id_dialog_body).innerText = this.document.title;
    var opener = this.parent;

    var modalDialog = null;
    if (typeof this.parent.modalDialog !== "undefined" && this.parent.modalDialog != null)
        modalDialog = this.parent.modalDialog;
    else
        alert("Error: no se ha inicializado la clase para mostrar ventanas modales.");
	
	//Devuelve el opener del diálogo, sea página maestra o modalDialog
	function fOpener() {
		if (modalDialog.array_dialogos.length > 1)//Si hay más de un diálogo, el opener es otro diálogo
			return opener.$I(modalDialog.array_dialogos[modalDialog.array_dialogos.length - 2]).contentWindow;
		else//Sino el opener es la página maestra
			return opener;
	}
-->