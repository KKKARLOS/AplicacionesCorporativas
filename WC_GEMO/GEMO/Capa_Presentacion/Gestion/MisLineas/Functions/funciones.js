
function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
        scrollTablaLineas(); 
        setExcelImg("imgExcel", "divCatalogo");       
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function Detalle(oFila)
{
    try{    
	    var strEnlace = "../Inventario/Detalle/Default.aspx?ID="+ oFila.id + "&bNueva=false&sLectura=true&sOrigen=1";          
	    //var ret = window.showModalDialog(strEnlace, self, "dialogwidth:1000px; dialogheight:500px; center:yes; status:NO; help:NO;");
	    modalDialog.Show(strEnlace, self, sSize(1000, 500))
	        .then(function(ret) {
	            if (ret != null) buscar();
	        });         
        
          
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}

var nTopScrollLineas = -1;
var nIDTimeLineas = 0;

function scrollTablaLineas(){
    try{   
        if ($I("divCatalogo").scrollTop != nTopScrollLineas){
            nTopScrollLineas = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeLineas);
            nIDTimeLineas = setTimeout("scrollTablaLineas()", 50);
            return;
        }
        clearTimeout(nIDTimeLineas);
        var tblDatos = $I("tblDatos");        
        var nFilaVisible = Math.floor(nTopScrollLineas/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);

                switch (oFila.getAttribute("estado"))
                {
                    case "X": 
                    case "A":
                    case "Y":                   
                            oFila.cells[0].appendChild(oImgEst1.cloneNode(), null); break; 
                    case "B": oFila.cells[0].appendChild(oImgEst5.cloneNode(), null); break;
                    case "I": oFila.cells[0].appendChild(oImgEst2.cloneNode(), null);break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de líneas.", e.message);
    }
}
function excel(){
    try{
        aFila = FilasDe("tblDatos");
        var tblDatos = $I("tblDatos");
        
        if (tblDatos==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 390);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
		sb.Append("	<tr align='center'>");
		sb.Append("        <td style='background-color: #BCD4DF;'>Estado</td>");
		sb.Append("        <td style='background-color: #BCD4DF;'>Cod.País</td>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Línea</td>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Extensión </td>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Responsable</td>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Beneficiario / Departamento</td>");
		sb.Append("	</tr>");
        for (var i=0; i<tblDatos.rows.length; i++){
            sb.Append("<tr>");
	        for (var x=0; x<tblDatos.rows[i].cells.length; x++){
	            if (x==0)
	            {
	                switch (tblDatos.rows[i].getAttribute("estado"))
                    {
                        case ("X"):
                        {
                            sb.Append("<td style='align:right;'>Preactiva</td>");                                                   
                            break;
                        }
                        case ("A"):
                            sb.Append("<td style='align:right;'>Activa</td>");
                            break; 
                        case ("B"):
                        {
                            sb.Append("<td style='align:right;'>Bloqueada</td>");                                                           
                            break;                            
                        }
                        case ("Y"):
                        {
                            sb.Append("<td style='align:right;'>Preinactiva</td>");                                                           
                            break;
                        }
                        case ("I"):
                        {
                            sb.Append("<td style='align:right;'>Inactiva</td>"); 
                            break;
                        }
                    } 	            
	            } 
	            if (x > 0){
	                 sb.Append("<td style='align:right;'>" + tblDatos.rows[i].cells[x].innerText +"</td>");
	            }
           }
            sb.Append("</tr>");
        }	
        sb.Append("        <td style='background-color: #BCD4DF; padding-left:3px;'>Nº de líneas: "+ $I("lblNumLineas").innerText +"</td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
		sb.Append("	</tr>");
	    sb.Append("</table>");
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel con los líneas de alta", e.message);
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
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("lblNumLineas").innerText = aResul[3];
                scrollTablaLineas();
                actualizarLupas("tblTitulo", "tblDatos");

                window.focus();
                break;        
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}
function Estados(iOp)
{ 
    if  (($I("chkAbierto").checked==false) && ($I("chkBloqueado").checked==false) && ($I("chkInactivo").checked==false))
    {
        if (iOp==1) $I("chkInactivo").checked=true;
        else if (iOp==2) $I("chkAbierto").checked=true;
        else if (iOp==3) $I("chkAbierto").checked=true;
        return false;         
    }
}
function Asignacion(iOp)
{ 
    if (($I("chkResponsable").checked==false) && ($I("chkBeneficiario").checked==false))
    {
        if (iOp==1) $I("chkBeneficiario").checked=true;
        else if (iOp==2) $I("chkResponsable").checked=true;    
        return false;
    }   
    else  return true;
}
function buscar(){
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
        
        var sEstados="";
        sEstados += ($I("chkAbierto").checked)? ",X,A,Y":"";
        sEstados += ($I("chkBloqueado").checked)? ",B":"";
        sEstados += ($I("chkInactivo").checked)? ",I":"";
        
        if (sEstados.length>0) sEstados=sEstados.substring(1,sEstados.length);       
        
        sb.Append(Utilidades.escape(sEstados) + "@#@");
        
        sb.Append(($I("chkResponsable").checked)? "1@#@":"@#@");
        sb.Append(($I("chkBeneficiario").checked)? "1@#@":"@#@");

        RealizarCallBack(sb.ToString(), ""); 
        
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='mano' style='width:934px;'></table>"; 
        $I("lblNumLineas").innerText = "";            
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos.", e.message);
    }
}
