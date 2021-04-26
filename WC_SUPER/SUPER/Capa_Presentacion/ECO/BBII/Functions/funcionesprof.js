var js_proy = null;

function crearArrayReordenacion() {
    try {
        mostrarProcesando();
        var js_args = "setResolucion@#@";

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al crear el array para la reordenación.", e.message);
    }
}

/* ormag: Ordenar Magnitudes */
function ormag(nColumna, desc, sTipo) {
    mostrarProcesando();
    sTipo = (typeof (sTipo) == "undefined") ? 'num' : sTipo;
    setTimeout("fOrdenarMagnitudes(" + nColumna + ",'" + desc + "','" + sTipo + "')", 50);
}

function getStyleWidth(obj) {
    try {
        var nWidth = 0;
        var sWidth = obj.style.width;
        if (sWidth != "")
            nWidth = parseInt(obj.style.width.toString().substring(0, obj.style.width.toString().length - 2), 10);
        else
            nWidth = obj.clientWidth;
        return nWidth;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la anchura del objeto.", e.message);
    }
}
function fOrdenarMagnitudes(nColumna, desc, sTipo) {
    try {
        sTipo = (typeof (sTipo) == "undefined") ? 'num' : sTipo;
        if (tblBodyMovil == null) tblBodyMovil = $I("tblBodyMovil");
        if (tblBodyFijo == null) tblBodyFijo = $I("tblBodyFijo");
        if (js_proy == null) {
            js_proy = new Array();
            if (tblBodyMovil != null) {
                for (var i = 0; i < tblBodyMovil.rows.length; i++) {
                    if (tblBodyMovil.rows[i].getAttribute("esproy") == "0") continue;
                    js_proy[js_proy.length] = [];
                    js_proy[js_proy.length - 1][0] = tblBodyMovil.rows[i].getAttribute("idPSN");
                    js_proy[js_proy.length - 1][1] = tblBodyFijo.rows[i].cells[0].innerText;
                    js_proy[js_proy.length - 1][2] = tblBodyFijo.rows[i].cells[1].innerText;
                    for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
                        //js_proy[js_proy.length - 1][x + 1] = getFloat(tblBodyMovil.rows[i].cells[x].innerText);
                        js_proy[js_proy.length - 1][x + 3] = tblBodyMovil.rows[i].cells[x].innerText;
                    }
                }
            }
        }
        var nIndice = 0;
        if (js_proy.length > 1) {
            js_proy = OrdenarArrayBidimensional(js_proy, nColumna, (desc == '1') ? '1' : '0', sTipo);
            var bEncontrado = false;
            //1º Para cada proyecto "padre" del array de PSNs
            for (var iPadre = 0; iPadre < js_proy.length; iPadre++) {
                //2º Lo buscamos en la tabla "fija"
                bEncontrado = false;
                for (var iFilaFija = nIndice; iFilaFija < tblBodyFijo.rows.length; iFilaFija++) {
                    if (js_proy[iPadre][0] == tblBodyFijo.rows[iFilaFija].getAttribute("idPSN")) {
                        //3º Lo movemos al índice nuevo.
                        tblBodyFijo.moveRow(tblBodyFijo.rows[iFilaFija].rowIndex, nIndice);
                        tblBodyMovil.moveRow(tblBodyMovil.rows[iFilaFija].rowIndex, nIndice);
                        nIndice++;
                        bEncontrado = true;
                        //continue;
                    } else if (bEncontrado) break;                //if (nIndice > 0) break;

                }
            }
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ordenar el indicador.", e.message);
    }
}

function OrdenarArrayBidimensional(oArray, n, desc, sTipo) {

    //this.nuevoarray = null;
    this.sort = function(column, desc, TipoOrd) {
        switch (TipoOrd) {
            case "": sortfn = this.sortCaseInsensitive; break;
            case "num":
            case "atrnum":
            case "mes":
                sortfn = this.sortNumeric; break;
            case "fec": sortfn = this.sortDate; break;
            case "por": sortfn = this.sortNumeric; break;
        }

        this.sortColumnIndex = column;
        var newRows = new Array();
        var oCell = null;
        for (j = 0; j < oArray.length; j++) {
            oCell = oArray[j]; //[this.sortColumnIndex];
            switch (TipoOrd) {
                case "": oCell["sAuxOrd"] = oArray[j][thisObject.sortColumnIndex].toString().toLowerCase().replace(/á/g, "a").replace(/é/g, "e").replace(/í/g, "i").replace(/ó/g, "o").replace(/ú/g, "u"); break;
                case "num":
                    oCell["sAuxOrd"] = oArray[j][thisObject.sortColumnIndex].toString().replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".").replace(/^\s+|\s+$/g, '');
                    if (oCell["sAuxOrd"] == "") oCell["sAuxOrd"] = "0";
                    break;
                case "por":
                    oCell["sAuxOrd"] = oArray[j][thisObject.sortColumnIndex].toString().replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".").replace(/^\s+|\s+$/g, '');
                    if (oCell["sAuxOrd"] == "") oCell["sAuxOrd"] = oCell["sAuxOrd"].substring(0, oCell["sAuxOrd"].length - 2);
                    else oCell["sAuxOrd"] = "0";
                    break;
                case "fec":
                    oCell["sAuxOrd"] = oArray[j][thisObject.sortColumnIndex].toString();
                    if (oCell["sAuxOrd"] == "") {
                        oCell["sAuxOrd"] = "0";
                    } else {
                        oCell["sAuxOrd"] = cadenaAfecha(oCell.getAttribute("sAuxOrd")).getTime();
                    }
                    break;
                case "mes":
                    oCell["sAuxOrd"] = oArray[j][thisObject.sortColumnIndex].toString();
                    if (oCell["sAuxOrd"] == "") {
                        oCell["sAuxOrd"] = "0";
                    } else {
                        oCell["sAuxOrd"] = DescLongToAnoMes(oCell["sAuxOrd"]);
                    }
                    break;
            }
            newRows[j] = oArray[j];
        }
        newRows.sort(sortfn);

        if (desc == '1') newRows.reverse();

        //        for (i = 0; i < newRows.length; i++) {
        //            this.tbody[0].appendChild(newRows[i]);
        //        }
        oArray = newRows;
    }

    this.sortCaseInsensitive = function(a, b) {
        if (a["sAuxOrd"] < b["sAuxOrd"]) return -1;
        if (a["sAuxOrd"] == b["sAuxOrd"]) return 0;
        return 1;
    }
    this.sortDate = function(a, b) {
        if (a[thisObject.sortColumnIndex]["sAuxOrd"] < b[thisObject.sortColumnIndex]["sAuxOrd"]) return -1;
        if (a[thisObject.sortColumnIndex]["sAuxOrd"] == b[thisObject.sortColumnIndex]["sAuxOrd"]) return 0;
        return 1;
    }
    this.sortNumeric = function(a, b) {
        return parseFloat(a["sAuxOrd"]) - parseFloat(b["sAuxOrd"]);
    }

    var thisObject = this;

    //if (!(this.tbody && this.tbody[0].rows && this.tbody[0].rows.length > 1)) return;
    if (!oArray || oArray.length <= 1) return;

    sort(n, desc, sTipo);
    return oArray;
}

function gp(oCelda) {
    try {
        mostrarProcesando();

        var sDimensiones = "";
        var sMagnitudes = "";
        opener.js_Magnitudes.length = 0;
        var sb = new StringBuilder;
        sb.Append("getProfundizacion@#@");
        sb.Append(opener.$I("cboVista").value + "@#@");
        sb.Append(opener.$I("cboCategoria").value + "@#@");
        sb.Append(opener.$I("cboCualidad").value + "@#@");
        sb.Append(opener.js_subnodos.join(",") + "@#@"); //ids estructura ambito
        sb.Append(opener.getDatosTabla(2) + "@#@"); //Responsable
        if (opener.$I("cboVista").value == "1" || (opener.$I("cboVista").value == "3" && opener.$I("chkSectorCG").checked))
            sb.Append(opener.getDatosTabla(6) + "@#@"); //Sector de gestión
        else
            sb.Append("@#@"); //Sector de gestión
        if (opener.$I("cboVista").value == "1" || (opener.$I("cboVista").value == "3" && opener.$I("lblSegmentoCG").checked))
            sb.Append(opener.getDatosTabla(7) + "@#@"); //Segmento de gestión
        else
            sb.Append("@#@"); //Segmento de gestión
        //sb.Append(getDatosTabla(6) + "@#@"); //Sector
        //sb.Append(getDatosTabla(7) + "@#@"); //Segmento
        sb.Append(opener.getDatosTabla(3) + "@#@"); //Naturaleza
        sb.Append(opener.getDatosTabla(8) + "@#@"); //Clientes
        sb.Append(opener.getDatosTabla(4) + "@#@"); //ModeloCon
        sb.Append(opener.getDatosTabla(9) + "@#@"); //Contrato
        sb.Append(opener.getDatosTabla(16) + "@#@"); //ProyectoSubnodos
        sb.Append(opener.getDatosTabla(32) + "@#@"); //Comercial
        sb.Append(opener.getDatosTabla(38) + "@#@"); //Soporte Administrativo

        switch (opener.$I("cboVista").value) {
            case "1":   //Análisis del ámbito económico
                if (oCelda.getAttribute("mes") != null && oCelda.getAttribute("mes").indexOf("-") == -1) {
                    sb.Append(oCelda.getAttribute("mes") + "{sepparam}");
                    sb.Append(oCelda.getAttribute("mes") + "{sepparam}");
                    profUnMes = 1;
                } else {
                    sb.Append(opener.$I("hdnDesde").value + "{sepparam}");
                    sb.Append(opener.$I("hdnHasta").value + "{sepparam}");
                }
                if (opener.$I("hdnDesde").value == opener.$I("hdnHasta").value)
                    profUnMes = 1;

                var aInputs = opener.$I("tblDimensiones_AE").getElementsByTagName("input");

                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                            || !aInputs[i].hasAttribute("dimension")
                            || aInputs[i].parentNode.parentNode.style.display == "none"
                            ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                    //Aquí habrá que mirar si la magnitud expandible está explotada o no, para que se
                    //muestre como debe en la profundización, y permita luego el "juego" en local.
                }
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);

                aInputs = opener.$I("tblMagnitudes_AE").getElementsByTagName("input");
                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked) continue;
                    sMagnitudes += aInputs[i].getAttribute("formula") + ",";
                    opener.js_Magnitudes[opener.js_Magnitudes.length] = aInputs[i].getAttribute("formula");
                }
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);

                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");

                sb.Append(((opener.$I("chkSN4_AE").checked) ? oCelda.parentNode.getAttribute("idSN4") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN3_AE").checked) ? oCelda.parentNode.getAttribute("idSN3") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN2_AE").checked) ? oCelda.parentNode.getAttribute("idSN2") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN1_AE").checked) ? oCelda.parentNode.getAttribute("idSN1") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkNodo_AE").checked) ? oCelda.parentNode.getAttribute("idnodo") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkCliente_AE").checked) ? oCelda.parentNode.getAttribute("idCliente") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkResponsable_AE").checked) ? oCelda.parentNode.getAttribute("idResponsable") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkComercial_AE").checked) ? ((oCelda.parentNode.getAttribute("idComercial") == "") ? "-1" : oCelda.parentNode.getAttribute("idComercial")) : "") + "{sepparam}");
                sb.Append(((opener.$I("chkContrato_AE").checked) ? ((oCelda.parentNode.getAttribute("idContrato") == "") ? "-1" : oCelda.parentNode.getAttribute("idContrato")) : "") + "{sepparam}");
                //sb.Append(((opener.$I("chkProyecto_AE").checked) ? oCelda.parentNode.getAttribute("idProyecto") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkProyecto_AE").checked) ? oCelda.parentNode.getAttribute("idPSN") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkModelocon_AE").checked) ? ((oCelda.parentNode.getAttribute("idModelocon") == "") ? "0" : oCelda.parentNode.getAttribute("idModelocon")) : "") + "{sepparam}");
                sb.Append(((opener.$I("chkNaturaleza_AE").checked) ? oCelda.parentNode.getAttribute("idNaturaleza") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSector_AE").checked) ? oCelda.parentNode.getAttribute("idSector") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSegmento_AE").checked) ? oCelda.parentNode.getAttribute("idSegmento") : "") + "{sepparam}");
                break;
            case "2":   //Análisis del ámbito financiero
                sb.Append(opener.$I("hdnMesValor").value + "{sepparam}");

                var aInputs = opener.$I("tblDimensiones_AE").getElementsByTagName("input");

                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                            || !aInputs[i].hasAttribute("dimension")
                            || aInputs[i].parentNode.parentNode.style.display == "none"
                            ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                }
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);

                aInputs = opener.$I("tblMagnitudes_DF").getElementsByTagName("input");
                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked) continue;
                    sMagnitudes += aInputs[i].getAttribute("formula") + ",";
                    opener.js_Magnitudes[opener.js_Magnitudes.length] = aInputs[i].getAttribute("formula");
                }
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);

                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");

                sb.Append(((opener.$I("chkSN4_AE").checked) ? oCelda.parentNode.getAttribute("idSN4") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN3_AE").checked) ? oCelda.parentNode.getAttribute("idSN3") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN2_AE").checked) ? oCelda.parentNode.getAttribute("idSN2") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN1_AE").checked) ? oCelda.parentNode.getAttribute("idSN1") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkNodo_AE").checked) ? oCelda.parentNode.getAttribute("idnodo") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkCliente_AE").checked) ? oCelda.parentNode.getAttribute("idCliente") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkResponsable_AE").checked) ? oCelda.parentNode.getAttribute("idResponsable") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkComercial_AE").checked) ? ((oCelda.parentNode.getAttribute("idComercial") == "") ? "-1" : oCelda.parentNode.getAttribute("idComercial")) : "") + "{sepparam}");
                sb.Append(((opener.$I("chkContrato_AE").checked) ? ((oCelda.parentNode.getAttribute("idContrato") == "") ? "-1" : oCelda.parentNode.getAttribute("idContrato")) : "") + "{sepparam}");
                //sb.Append(((opener.$I("chkProyecto_AE").checked) ? oCelda.parentNode.getAttribute("idProyecto") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkProyecto_AE").checked) ? oCelda.parentNode.getAttribute("idPSN") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkModelocon_AE").checked) ? ((oCelda.parentNode.getAttribute("idModelocon") == "") ? "0" : oCelda.parentNode.getAttribute("idModelocon")) : "") + "{sepparam}");
                sb.Append(((opener.$I("chkNaturaleza_AE").checked) ? oCelda.parentNode.getAttribute("idNaturaleza") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSector_AE").checked) ? oCelda.parentNode.getAttribute("idSector") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSegmento_AE").checked) ? oCelda.parentNode.getAttribute("idSegmento") : ""));

                break;
            case "3":   //Vencimiento de facturas
                sb.Append(opener.getDatosTabla(17) + "{sepparam}"); //ClientesFact
                if (opener.$I("chkSectorCF").checked)
                    sb.Append(opener.getDatosTabla(6) + "{sepparam}"); //Sector de facturación
                else
                    sb.Append("{sepparam}"); //Sector de gestión
                if (opener.$I("chkSegmentoCF").checked)
                    sb.Append(opener.getDatosTabla(7) + "{sepparam}"); //Segmento de facturación
                else
                    sb.Append("{sepparam}"); //Segmento de gestión
                //sb.Append(getDatosTabla(6) + "{sepparam}"); //SectorFact
                //sb.Append(getDatosTabla(7) + "{sepparam}"); //SegmentoFact
                sb.Append(opener.getDatosTabla(22) + "{sepparam}"); //Sociedad - Empresa Facturación

                var aInputs = opener.$I("tblDimensiones_AE").getElementsByTagName("input");

                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                            || !aInputs[i].hasAttribute("dimension")
                            || aInputs[i].parentNode.parentNode.style.display == "none"
                            ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                }
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);

                aInputs = opener.$I("tblMagnitudes_VF").getElementsByTagName("input");
                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked) continue;
                    sMagnitudes += aInputs[i].getAttribute("formula") + ",";
                    opener.js_Magnitudes[opener.js_Magnitudes.length] = aInputs[i].getAttribute("formula");
                }
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);

                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");

                sb.Append(((opener.$I("chkSN4_AE").checked) ? oCelda.parentNode.getAttribute("idSN4") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN3_AE").checked) ? oCelda.parentNode.getAttribute("idSN3") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN2_AE").checked) ? oCelda.parentNode.getAttribute("idSN2") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSN1_AE").checked) ? oCelda.parentNode.getAttribute("idSN1") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkNodo_AE").checked) ? oCelda.parentNode.getAttribute("idnodo") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkCliente_AE").checked) ? oCelda.parentNode.getAttribute("idCliente") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkResponsable_AE").checked) ? oCelda.parentNode.getAttribute("idResponsable") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkComercial_AE").checked) ? ((oCelda.parentNode.getAttribute("idComercial") == "") ? "-1" : oCelda.parentNode.getAttribute("idComercial")) : "") + "{sepparam}");
                sb.Append(((opener.$I("chkContrato_AE").checked) ? ((oCelda.parentNode.getAttribute("idContrato") == "") ? "-1" : oCelda.parentNode.getAttribute("idContrato")) : "") + "{sepparam}");
                //sb.Append(((opener.$I("chkProyecto_AE").checked) ? oCelda.parentNode.getAttribute("idProyecto") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkProyecto_AE").checked) ? oCelda.parentNode.getAttribute("idPSN") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkModelocon_AE").checked) ? ((oCelda.parentNode.getAttribute("idModelocon") == "") ? "0" : oCelda.parentNode.getAttribute("idModelocon")) : "") + "{sepparam}");
                sb.Append(((opener.$I("chkNaturaleza_AE").checked) ? oCelda.parentNode.getAttribute("idNaturaleza") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSector_AE").checked) ? oCelda.parentNode.getAttribute("idSector") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSegmento_AE").checked) ? oCelda.parentNode.getAttribute("idSegmento") : "") + "{sepparam}");

                sb.Append(((opener.$I("chkClienteFact_AE").checked) ? oCelda.parentNode.getAttribute("idClienteFact") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSectorFact_AE").checked) ? oCelda.parentNode.getAttribute("idSectorFact") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkSegmentoFact_AE").checked) ? oCelda.parentNode.getAttribute("idSegmentoFact") : "") + "{sepparam}");
                sb.Append(((opener.$I("chkEmpresaFact_AE").checked) ? oCelda.parentNode.getAttribute("idEmpresaFact") : ""));
                break;
        }

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a profundizar.", e.message);
    }
}

function expocMag(oImagen, nMag) {
    try {
        var opcion = "";
        if (oImagen.src.indexOf("plusWhite.png") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar

        oImagen.src = (opcion == "M") ? "../../../Images/minusWhite.png" : "../../../Images/plusWhite.png";
        /* Se vuelven a crear las variables porque si no en firefox hay problemas. */
        tblTituloMovil = $I("tblTituloMovil");
        tblBodyMovil = $I("tblBodyMovil");
        tblPieMovil = $I("tblPieMovil");

        //        for (var i = 0; i < tblTituloMovil.rows[0].cells.length; i++) {
        //            if (tblTituloMovil.rows[0].cells[i].getAttribute("formula_padre") == nMag) {
        //                tblTituloMovil.rows[0].cells[i].style.display = (opcion == "M") ? "" : "none";
        //                tblPieMovil.rows[0].cells[i].style.display = (opcion == "M") ? "" : "none";
        //            }
        //        }

        //        for (var i = 0; i < tblBodyMovil.rows.length; i++) {
        //            for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
        //                if (tblBodyMovil.rows[i].cells[x].getAttribute("formula_padre") == nMag) {
        //                    tblBodyMovil.rows[i].cells[x].style.display = (opcion == "M") ? "" : "none";
        //                }
        //            }
        //        }

        $("#tblTituloMovil tr td[formula_padre=" + nMag + "]").css({ display: (opcion == "M") ? "" : "none" });
        $("#tblBodyMovil tr td[formula_padre=" + nMag + "]").css({ display: (opcion == "M") ? "" : "none" });
        $("#tblPieMovil tr td[formula_padre=" + nMag + "]").css({ display: (opcion == "M") ? "" : "none" });

        $("#tblTituloMovil tr td").attr({ width: "80px !important" });
        $("#tblBodyMovil tr td").attr({ width: "80px !important" });
        $("#tblPieMovil tr td").attr({ width: "80px !important" });

        var nMagnitudesVisibles = 0;
        for (var i = 0; i < tblTituloMovil.rows[0].cells.length; i++) {
            if (tblTituloMovil.rows[0].cells[i].style.display == "") {
                nMagnitudesVisibles++;
            }
        }
        //tblTituloMovil.style.width = (nMagnitudesVisibles * 91) + "px";
        //tblBodyMovil.style.width = (nMagnitudesVisibles * 91) + "px";
        //tblPieMovil.style.width = (nMagnitudesVisibles * 91) + "px";
        $("#tblTituloMovil").attr({ width: (nMagnitudesVisibles * 91) + "px !important" });
        $("#tblBodyMovil").attr({ width: (nMagnitudesVisibles * 91) + "px !important" });
        $("#tblPieMovil").attr({ width: (nMagnitudesVisibles * 91) + "px !important" });

        //        //para que la tabla se ajuste a la cabecera
        //        if (nName == "ie") {
        //            if (opcion == "M" && (nMagnitudesVisibles * 91) > parseInt(tblBodyMovil.style.width.substring(0, tblBodyMovil.style.width.length - 2))) {
        //                tblBodyMovil.style.width = (nMagnitudesVisibles * 91) + "px";
        //            }else if (opcion == "O" && parseInt(tblBodyMovil.style.width.substring(0, tblBodyMovil.style.width.length - 2)) > (nMagnitudesVisibles * 91)) {
        //                tblBodyMovil.style.width = (nMagnitudesVisibles * 91) + "px";
        //            }
        //        } else {
        //            tblBodyMovil.style.width = (nMagnitudesVisibles * 91) + "px";
        //        }

        if (opcion == "O") {
            $I("divBodyMovil").scrollLeft = 0;
            $I("divTituloMovil").scrollLeft = 0;
            $I("divPieMovil").scrollLeft = 0;
        }
        $I("divBodyMovil").children[0].style.width = (nMagnitudesVisibles * 91) + "px";

        quitarFilasNoSignificativas();
        setPosicionExcel();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar u ocultar magnitudes.", e.message);
    }
}