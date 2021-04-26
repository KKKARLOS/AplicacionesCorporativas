var aFila;
//JQuery
var options, acTit;
$(function () {
    options = {
        serviceUrl: "../../../UserControls/AutocompleteData.ashx",
        width: 306,
        minChars: 3
    };

    acTit = $("#ctl00_CPHC_txtResponsable").autocomplete(options);
    acTit.setOptions({ params: { opcion: 'responsables' } });
    //acTit.setOptions({ params: { opcion: 'responsables', t001_idficepi: $I("ctl00_CPHC_hdnIDFicepiResp").value } });
});
function init(){
    try{
        ToolTipBotonera("eliminar","Elimina de base de datos toda la información relativa al calendario seleccionado");
        ToolTipBotonera("HorarioOff","Borra los datos horarios de los calendarios seleccionados, correspondientes al año indicado");
        aFila = FilasDe("tblDatos");

        acTit.onSelect = function () { onSelect(acTit); };
        acTit.onValueChange = function () { onValueChange(acTit); };

        swm($I("txtResponsable"));
        setExcelImg("imgExcel", "divCatalogo");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

// Inicio Autocompleted
function onValueChange(object) {
    clearInterval(object.onChangeInterval);
    object.currentValue = object.el.val();
    var q = object.getQuery(object.currentValue);
    if (q === '') setPantalla();
    object.selectedIndex = -1;
    if (object.ignoreValueChange) {
        object.ignoreValueChange = false;
        return;
    }
    if (q === '' || q.length < object.options.minChars || q.indexOf('%') != -1) {
        object.hide();
    } else {
        object.getSuggestions(q);
    }    
}

function onSelect(object) {
    var me, fn, s, d;
    me = object;
    i = me.selectedIndex;
    fn = me.options.onSelect;
    s = me.suggestions[i];
    d = me.data[i];
    me.el.val(me.getValue(s));
    if ($.isFunction(fn)) { fn(s, d, me.el); }
   
    setPantalla();
    
    //getDatosTitulo();
}

// Fin Autocompleted

function mostrarDetalle(nIDCal){
    try{
        location.href = "../Detalle/Default.aspx?nCalendario="+ nIDCal;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del calendario", e.message);
    }
}
function mostrarHorario(){
    try{
        if (iFila == -1) {
            mmoff("War", "Debes seleccionar algún calendario para acceder a su horario.", 430); 
            return;
        }
        if (nfs > 1)return;
        var nIDCal = $I("tblDatos").rows[iFila].id;
        location.href = "../Horario/Default.aspx?nCalendario="+ nIDCal;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el horario", e.message);
    }
}
function nuevoCalendario(){
    try{
        location.href = "../Detalle/Default.aspx?nCalendario=0";
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo calendario", e.message);
    }
}
function eliminarCalendario(){
    try{
        var aFila = FilasDe("tblDatos");
        if (aFila.length == 0) return;
        var sw = 0;
        var strFilas = "";
        for(var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS") 
            {
                sw = 1;
                strFilas += aFila[i].id + "\\" + Utilidades.escape(aFila[i].cells[0].innerText) + "##";
            }
        }
        if (sw == 0) {
            mmoff("War", "Selecciona la fila a eliminar.", 210); 
            return;
        }else strFilas = strFilas.substring(0, strFilas.length-2);
        
        var js_args = "eliminar@#@";
        js_args += strFilas; //IDs de calendarios a eliminar

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el calendario", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos > 0) {
		    mostrarError("No se puede eliminar el calendario '" + aResul[3] + "',\n ya que existen profesionales que lo tienen asignado.\n\nSi deseas que un calendario no se pueda asignar a más profesionales debes desactivarlo.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "eliminar":
                aFila = FilasDe("tblDatos");
                 for(var i=aFila.length-1;i>=0;i--)
                     if (aFila[i].className == "FS") $I("tblDatos").deleteRow(i);
                //popupWinespopup_winLoad();
                mmoff("Suc","Grabación correcta", 160); 
                break;
            case "borrar":
                //popupWinespopup_winLoad();
                mmoff("Suc", "Grabación correcta", 160); 
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                break;
            case "setPantalla":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                break;                             
        }
        ocultarProcesando();
    }
}

function borrar(nAnno, sCal){
    try{
        var js_args = "borrar@#@"+nAnno+"@#@"+sCal;
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los horarios del año "+ nAnno, e.message);
    }
}

function borrarAux()
{
    try{
        if (nfs == 0){
            mmoff("War", "Debes seleccionar algún calendario.", 280); 
            return;
        }
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/Calendario/Catalogo/AnnoCal.aspx", self, sSize(280, 110))
            .then(function(ret) {
                if (ret != null) {
                    jqConfirm("", "¿Deseas borrar todos los horarios correspondientes al año " + ret + " de los calendarios seleccionados?", "", "", "war",400).then(function (answer) {
                        if (answer) {
                            var sCal = "";
                            var aFila = FilasDe("tblDatos");
                            for (var i = 0; i < aFila.length; i++) {
                                if (aFila[i].className == "FS") {
                                    sCal += aFila[i].id + "##";
                                }
                            }
                            sCal = sCal.substring(0, sCal.length - 2);
                            borrar(ret, sCal);
                        }
                    });
                }
            });
        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los horarios del año "+ nAnno, e.message);
    }
}

function ordenarTabla(nOrden,nAscDesc){
	buscar(nOrden,nAscDesc);
}

function buscar(nOrden,nAscDesc){
    try{
        var js_args = "buscar@#@";
        js_args += nOrden +"@#@";
        js_args += nAscDesc +"@#@";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordernar el catálogo", e.message);
    }
}
function setPantalla() {
    try {

        if (acTit.data[acTit.selectedIndex] != undefined) $I("hdnIDFicepiResp").value = acTit.data[acTit.selectedIndex];
        else $I("hdnIDFicepiResp").value = "";

        var js_args = "setPantalla@#@";
        js_args += ($I("chkActivos").checked) ? "1@#@" : "0@#@";
        js_args += ($I("hdnIDFicepiResp").value) + "@#@";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al ordernar el catálogo", e.message);
    }
}

function excel() {
    try {
        var tblBodyMovil = $I("tblDatos");
        if (tblBodyMovil == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center>");
        sb.Append("        <td style='width=:150px; background-color: #BCD4DF;'>Calendario</TD>");
        sb.Append("        <td style='width=:150px; background-color: #BCD4DF;'>C.R.</TD>");
        sb.Append("        <td style='width=:70px; background-color: #BCD4DF;'>Tipo</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Activo</TD>");

        sb.Append("        <td style='background-color: #BCD4DF;'>Responsable</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Nº de jornadas lab.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Nª de horas lab.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Observaciones</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Provincia</TD>");
        sb.Append("	</TR>");

        for (var i = 0; i < tblBodyMovil.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < tblBodyMovil.rows[i].cells.length-1; x++) {
               sb.Append(tblBodyMovil.rows[i].cells[x].outerHTML);               
            }
       
            sb.Append("<td>");
            (tblBodyMovil.rows[i].getAttribute("estado") == "True") ? sb.Append("Si") : sb.Append( "No");
            sb.Append("</td>");
       
            sb.Append("<td>");
            sb.Append(tblBodyMovil.rows[i].getAttribute("responsable"));
            sb.Append("</td><td>");
            sb.Append(tblBodyMovil.rows[i].getAttribute("njorlabcal"));
            sb.Append("</td><td>");
            sb.Append(tblBodyMovil.rows[i].getAttribute("nhlacv"));
            sb.Append("</td><td>");
            sb.Append(tblBodyMovil.rows[i].getAttribute("observaciones"));
            sb.Append("</td><td>");
            sb.Append(tblBodyMovil.rows[i].getAttribute("provincia"));
            sb.Append("</td>");            
            sb.Append("</tr>");
        }
       
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
