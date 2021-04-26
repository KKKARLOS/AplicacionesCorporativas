var myTD = null;
var oF1 = null, oF2 = null, oF3 = null, oF4 = null, oF5 = null, oF6 = null, oF7 = null;
var oF8 = null, oF9 = null, oF10 = null, oF11 = null, oF12 = null;
function init() {
    try {
        //CrearTabla1();
        myTD = oTablaDinamica2;
        //myTD.acumulado = 1;
        //setIndicadores();
        setExcelImg("imgExcel", "divCatalogo");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function Obtener() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("obtener@#@");

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica 2", e.message);
    }
}

function setIndicadoresAux() {
    try {
        mostrarProcesando();
        setTimeout("setIndicadores()", 20);
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica 2", e.message);
    }
}

function setIndicadores() {
    try {
//        myTD.delAgrupacion();
//        myTD.delVisualizacion();
//        myTD.delDato();
        myTD.evolucionmensual = parseInt(getRadioButtonSelectedValue("rdbEvolucion", false), 10);
        
        oF1 = new Date();
        myTD.delArrays();
        oF2 = new Date();
        
        var aInputs = $I("tblDimensiones").getElementsByTagName("input");
        for (var i = 0; i < aInputs.length; i++) {
            if (!aInputs[i].checked) continue;
            if (aInputs[i].getAttribute("tipo") == "agregado") {
                myTD.addAgrupacion(aInputs[i].getAttribute("codigo"));
                myTD.addVisualizacion(aInputs[i].getAttribute("valor"));
            } else {
                myTD.addDato(aInputs[i].getAttribute("codigo"));
            }
        }
        oF3 = new Date();
        CrearTabla2();

        //        var oF2 = new Date();
        //alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");
        //       alert((oF2.getTime() - oF1.getTime()) + " ms.");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica 2", e.message);
    }
}
function CrearTabla2() {
    try {
        mostrarProcesando();

        myTD.datos = strDatos;
        oF4 = new Date();
        myTD.crearResultado();
        oF5 = new Date();
        myTD.mostrarResultado();
        oF6 = new Date();

        //        var oF2 = new Date();
        var sMsg = "";
        sMsg = "Borrado de arrays: " + (oF2.getTime() - oF1.getTime()) + " ms.";
        sMsg += "\nCreación de dimensiones: " + (oF3.getTime() - oF2.getTime()) + " ms.";
        sMsg += "\nAsignación de datos: " + (oF4.getTime() - oF3.getTime()) + " ms.";
        sMsg += "\nCreación de resultado: " + (oF5.getTime() - oF4.getTime()) + " ms.";
        sMsg += "\nMostrado de resultado: " + (oF6.getTime() - oF5.getTime()) + " ms.";
        sMsg += "\nTotal: " + (oF6.getTime() - oF1.getTime()) + " ms.";
        alert(sMsg);
        //alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");
        //       alert((oF2.getTime() - oF1.getTime()) + " ms.");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica 2", e.message);
    }
}


var oTablaDinamica2 = {
    //acumulado: 0,
    evolucionmensual: 0,
    datos: {},
    //dimensiones: new Array(),
    cols_agrupacion: new Array(),
    cols_visualizacion: new Array(),
    cols_datos: new Array(),
    tabla_resultado: new Array(),
    meses: new Array(),

    addAgrupacion: function(agrupacion) {
        this.cols_agrupacion[this.cols_agrupacion.length] = agrupacion;
    },
    addVisualizacion: function(visualizacion) {
        this.cols_visualizacion[this.cols_visualizacion.length] = visualizacion;
    },
    addDato: function(dato) {
        this.cols_datos[this.cols_datos.length] = dato;
    },
    /*delAgrupacion: function() {
    this.cols_agrupacion.length = 0;
    },
    delVisualizacion: function() {
    this.cols_visualizacion.length = 0;
    },
    delDato: function() {
    this.cols_datos.length = 0;
    },*/
    delArrays: function() {
        this.cols_agrupacion.length = 0;
        this.cols_visualizacion.length = 0;
        this.cols_datos.length = 0;
    },


    crearResultado: function() {
        if (this.datos == null) {
            ocultarProcesando();
            mmoff("War", "No hay datos que mostrar.", 200);
            return;
        }
        this.tabla_resultado.length = 0;
        this.meses.length = 0;

        if (this.evolucionmensual == 1) {
            this.anomes_min = this.datos["anomes_min"];
            this.anomes_max = this.datos["anomes_max"];

            var anomes_min_aux = this.anomes_min;
            var anomes_max_aux = this.anomes_max;
            while (anomes_min_aux <= anomes_max_aux) {
                this.meses[this.meses.length] = anomes_min_aux;
                anomes_min_aux = (anomes_min_aux % 100 == 12) ? (parseInt(anomes_min_aux / 100, 10) + 1) * 100 + 1 : anomes_min_aux + 1;
            }
        }

        for (var i = 0, nLoop = this.datos["datos"].length; i < nLoop; i++) {
            var oDato = this.datos["datos"][i];
            var sw = 0;
            for (var n = 0; n < this.tabla_resultado.length; n++) {
                var sw_agrupacion = 0;
                for (var x = 0; x < this.cols_agrupacion.length; x++) {
                    if (oDato[this.cols_agrupacion[x]] == this.tabla_resultado[n][this.cols_agrupacion[x]]) {
                        sw_agrupacion++;
                    }
                }
                if (sw_agrupacion == this.cols_agrupacion.length) {
                    for (var x = 0; x < this.cols_datos.length; x++) {
                        if (this.evolucionmensual == 0) {
                            this.tabla_resultado[n][this.cols_datos[x]] += oDato[this.cols_datos[x]];
                        } else {
                            for (var z = 0; z < this.meses.length; z++) {
                                this.tabla_resultado[n][this.cols_datos[x] + "_" + this.meses[z]] += (this.meses[z] == oDato.anomes) ? oDato[this.cols_datos[x]] : 0;
                            }
                        }
                    }
                    sw = 1;
                    break;
                }
            }
            if (sw == 1) continue;

            //            this.tabla_resultado[this.tabla_resultado.length] = {
            //                idproyectosubnodo: oDato.idproyectosubnodo,
            //                anomes: oDato.anomes,
            //                idnodo: oDato.idnodo,
            //                desnodo: oDato.desnodo,
            //                idproyecto: oDato.idproyecto,
            //                desproyecto: oDato.desproyecto,
            //                idcliente: oDato.idcliente,
            //                descliente: oDato.descliente,
            //                idresponsableproyecto: oDato.idresponsableproyecto,
            //                desresponsableproyecto: oDato.desresponsableproyecto,
            //                cualidad: oDato.cualidad,
            //                idnaturaleza: oDato.idnaturaleza,
            //                desnaturaleza: oDato.desnaturaleza,
            //                Ingresos_Netos: oDato.Ingresos_Netos,
            //                Margen: oDato.Margen,
            //                Obra_en_curso: oDato.Obra_en_curso,
            //                Saldo_de_Clientes: oDato.Saldo_de_Clientes,
            //                Total_Cobros: oDato.Total_Cobros,
            //                Total_Gastos: oDato.Total_Gastos,
            //                Total_Ingresos: oDato.Total_Ingresos,
            //                Volumen_de_Negocio: oDato.Volumen_de_Negocio,
            //                Otros_consumos: oDato.Otros_consumos,
            //                Consumo_recursos: oDato.Consumo_recursos
            //            }
            if (this.evolucionmensual == 0) {
                this.tabla_resultado[this.tabla_resultado.length] = {
                    idproyectosubnodo: oDato.idproyectosubnodo,
                    anomes: oDato.anomes,
                    idnodo: oDato.idnodo,
                    desnodo: oDato.desnodo,
                    idproyecto: oDato.idproyecto,
                    desproyecto: oDato.desproyecto,
                    idcliente: oDato.idcliente,
                    descliente: oDato.descliente,
                    idresponsableproyecto: oDato.idresponsableproyecto,
                    desresponsableproyecto: oDato.desresponsableproyecto,
                    cualidad: oDato.cualidad,
                    idnaturaleza: oDato.idnaturaleza,
                    desnaturaleza: oDato.desnaturaleza,
                    Ingresos_Netos: oDato.Ingresos_Netos,
                    Margen: oDato.Margen,
                    Obra_en_curso: oDato.Obra_en_curso,
                    Saldo_de_Clientes: oDato.Saldo_de_Clientes,
                    Total_Cobros: oDato.Total_Cobros,
                    Total_Gastos: oDato.Total_Gastos,
                    Total_Ingresos: oDato.Total_Ingresos,
                    Volumen_de_Negocio: oDato.Volumen_de_Negocio,
                    Otros_consumos: oDato.Otros_consumos,
                    Consumo_recursos: oDato.Consumo_recursos
                }
            } else {
                this.tabla_resultado[this.tabla_resultado.length] = {
                    idproyectosubnodo: oDato.idproyectosubnodo,
                    anomes: oDato.anomes,
                    idnodo: oDato.idnodo,
                    desnodo: oDato.desnodo,
                    idproyecto: oDato.idproyecto,
                    desproyecto: oDato.desproyecto,
                    idcliente: oDato.idcliente,
                    descliente: oDato.descliente,
                    idresponsableproyecto: oDato.idresponsableproyecto,
                    desresponsableproyecto: oDato.desresponsableproyecto,
                    cualidad: oDato.cualidad,
                    idnaturaleza: oDato.idnaturaleza,
                    desnaturaleza: oDato.desnaturaleza
                }

                for (var x = 0; x < this.cols_datos.length; x++) { //Para cada dato a mostrar, creo los meses
                    for (var z = 0; z < this.meses.length; z++) {
                        this.tabla_resultado[this.tabla_resultado.length - 1][this.cols_datos[x] + "_" + this.meses[z]] = (this.meses[z] == oDato.anomes) ? oDato[this.cols_datos[x]] : 0;
                    }
                }
            }
        }
    },
    mostrarResultado: function() {
        if (this.datos == null) {
            return;
        }
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' style='width:auto' cellpadding='0' cellspacing='0' border='0'></table>";
        var tblDatos = $I("tblDatos");
        if (tblDatos != null) {
            var oNF = null;
            var oNC = null;
            oNF = tblDatos.insertRow(-1);
            oNF.className = "TBLINI";
            oNF.style.textAlign = "center";
            oNF.style.height = "20px";
            oNF.style.border = "1px solid #FFFFFF";
            oNF.style.borderCollapse = "collapse";
            oNF.style.borderSpacing = "0px";
            oNF.style.backgroundRepeat = "repeat-x";

            for (var x = 0; x < this.cols_visualizacion.length; x++) {
                oNF.insertCell(-1).innerText = this.cols_visualizacion[x];
            }
            for (var x = 0; x < this.cols_datos.length; x++) {
                //oNF.insertCell(-1).innerText = this.cols_datos[x];
                if (this.evolucionmensual == 0) {
                    oNF.insertCell(-1).innerText = this.cols_datos[x];
                } else {
                    for (var z = 0; z < this.meses.length; z++) {
                        oNF.insertCell(-1).innerText = this.cols_datos[x] + "_" + this.meses[z];
                    }
               }
            }

            for (var i = 0; i < this.tabla_resultado.length; i++) {
                oNF = tblDatos.insertRow(-1);

                for (var x = 0; x < this.cols_visualizacion.length; x++) {
                    oNF.insertCell(-1).innerText = Utilidades.unescape(this.tabla_resultado[i][this.cols_visualizacion[x]]);
                }

                for (var x = 0; x < this.cols_datos.length; x++) {
                    if (this.evolucionmensual == 0) {
                        oNC = oNF.insertCell(-1);
                        oNC.style.textAlign = "right";
                        oNC.innerText = this.tabla_resultado[i][this.cols_datos[x]].ToString("N");
                    } else {
                        for (var z = 0; z < this.meses.length; z++) {
                            oNC = oNF.insertCell(-1);
                            oNC.style.textAlign = "right";
                            oNC.innerText = this.tabla_resultado[i][this.cols_datos[x] + "_" + this.meses[z]].ToString("N");
                        }
                    }
                }
            }
            $I("divCatalogo").children[0].style.width = tblDatos.scrollWidth + "px";
        }
        $I("divCatalogo").scrollTop = 0;
        $I("divCatalogo").scrollLeft = 0;
    }
};


function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "obtener":
                bOcultarProcesando = false;
                strDatos = eval("(" + aResul[2] + ")");
                setTimeout("setIndicadores();", 20);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function excel() {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            mmoff("No hay información en pantalla para exportar.", 300);
            return;
        }

        var aFila = FilasDe("tblDatos");
        if (aFila.length == 0) {
            ocultarProcesando();
            mmoff("No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        var tblDatos = $I("tblDatos");
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
        for (var x = 0, nLoop = $I("tblDatos").rows[0].cells.length; x < nLoop; x++) {
            sb.Append("<td style='width:auto;'>" + tblDatos.rows[0].cells[x].innerText + "</td>");
        }
        sb.Append("</tr>");

        for (var i = 1; i < tblDatos.rows.length; i++)
            sb.Append(tblDatos.rows[i].outerHTML);


        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}