    if ($I("hdnTitle").value == "") $I("hdnTitle").value = this.id_dialog_body;
    else this.id_dialog_body = $I("hdnTitle").value;

    if (this.parent.document.getElementById("ui-dialog-title-" + this.id_dialog_body)!=null)
        this.parent.document.getElementById("ui-dialog-title-" + this.id_dialog_body).innerText = this.document.title;
    var opener = this.parent;

    var modalDialog = null;
    if (typeof this.parent.modalDialog !== "undefined" && this.parent.modalDialog != null)
        modalDialog = this.parent.modalDialog;
    else
        alert("Error: no se ha inicializado la clase para mostrar ventanas modales.");
