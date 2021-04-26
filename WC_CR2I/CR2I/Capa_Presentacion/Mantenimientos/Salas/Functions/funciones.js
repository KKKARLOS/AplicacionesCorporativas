function init(){
	
	try{ 
		var objFila = $I("dgCatalogo").rows[0];
//		objFila.cells[0].style.width = "440px";
//		objFila.cells[1].style.width = "40px";
//		objFila.cells[2].style.width = "20px";
		objFila.cells[0].setAttribute("style", "width:440px");
		objFila.cells[1].setAttribute("style", "width:40px");
		objFila.cells[2].setAttribute("style", "width:20px");
	}catch(e){}
}

function unload(){

}

function confirmar() 
{ 
	if (confirm("¿Desea eliminar el registro?")==true) 
		return true; 
	else 
		return false; 
}
