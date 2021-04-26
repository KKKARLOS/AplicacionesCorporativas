var aValorGanado = new Array();
var oVGActivo;

function oVG(anomes, estado, t305_idproyectosubnodo,
            consumo_lb_acum_a_mes,
            consumo_iap_lb_acum_a_mes,
            consumo_ext_lb_acum_a_mes,
            consumo_oco_lb_acum_a_mes,
            consumo_ext_reconocido_lb_acum_a_mes,
            produccion_lb_acum_a_mes,
            consumo_real_acum_a_mes,
            produccion_real_acum_a_mes,
            avance_tecnico_teorico_a_mes,
            AC, AC_IAP, AC_EXT, AC_OCO,
            BAC, BAC_IAP, BAC_EXT, BAC_OCO,
            CPI, CV, EAC, EAC1, EAC2, EAC3, EACt, ETC,
            EV, EV_IAP, EV_EXT, EV_OCO,
            PV, PV_IAP, PV_EXT, PV_OCO, 
            SPI, SV, VAC, produccion_lb_total, PAC, PAC1, PAC2, PAC3, TCPI, TSPI, codigo_1, codigo_2, codigo_3, codigo_4,
            codigo_5, codigo_6, codigo_7, codigo_8, codigo_9, codigo_10, codigo_11, codigo_12, codigo_13, codigo_14,
            codigo_15, codigo_16, codigo_17, codigo_18, codigo_19, codigo_20) {
    this.anomes = anomes;
    this.estado = estado;
    this.t305_idproyectosubnodo = t305_idproyectosubnodo;
    this.consumo_lb_acum_a_mes = consumo_lb_acum_a_mes;
    this.consumo_iap_lb_acum_a_mes = consumo_iap_lb_acum_a_mes;
    this.consumo_ext_lb_acum_a_mes = consumo_ext_lb_acum_a_mes;
    this.consumo_oco_lb_acum_a_mes = consumo_oco_lb_acum_a_mes;
    this.consumo_ext_reconocido_lb_acum_a_mes = consumo_ext_reconocido_lb_acum_a_mes;
    this.produccion_lb_acum_a_mes = produccion_lb_acum_a_mes;
	this.consumo_real_acum_a_mes = consumo_real_acum_a_mes;
	this.produccion_real_acum_a_mes = produccion_real_acum_a_mes;
	this.avance_tecnico_teorico_a_mes = avance_tecnico_teorico_a_mes;
	this.AC = AC;
	this.AC_IAP = AC_IAP;
	this.AC_EXT = AC_EXT;
	this.AC_OCO = AC_OCO;
	this.BAC = BAC;
	this.BAC_IAP = BAC_IAP;
	this.BAC_EXT = BAC_EXT;
	this.BAC_OCO = BAC_OCO;
	this.CPI = CPI;
	this.CV = CV;
	this.EAC = EAC;
	this.EAC1 = EAC1;
	this.EAC2 = EAC2;
	this.EAC3 = EAC3;
	this.EACt = EACt;
	this.ETC = ETC;
	this.EV = EV;
	this.EV_IAP = EV_IAP;
	this.EV_EXT = EV_EXT;
	this.EV_OCO = EV_OCO;
	this.PV = PV;
	this.PV_IAP = PV_IAP;
	this.PV_EXT = PV_EXT;
	this.PV_OCO = PV_OCO;
	this.SPI = SPI;
	this.SV = SV;
	this.VAC = VAC;
	this.produccion_lb_total = produccion_lb_total;
	this.PAC = PAC;
	this.PAC1 = PAC1;
	this.PAC2 = PAC2;
	this.PAC3 = PAC3;
	this.TCPI = TCPI;
	this.TSPI = TSPI;
	this.codigo_1 = codigo_1;
	this.codigo_2 = codigo_2;
	this.codigo_3 = codigo_3;
	this.codigo_4 = codigo_4;
	this.codigo_5 = codigo_5;
	this.codigo_6 = codigo_6;
	this.codigo_7 = codigo_7;
	this.codigo_8 = codigo_8;
	this.codigo_9 = codigo_9;
	this.codigo_10 = codigo_10;
	this.codigo_11 = codigo_11;
	this.codigo_12 = codigo_12;
	this.codigo_13 = codigo_13;
	this.codigo_14 = codigo_14;
	this.codigo_15 = codigo_15;
	this.codigo_16 = codigo_16;
	this.codigo_17 = codigo_17;
	this.codigo_18 = codigo_18;
	this.codigo_19 = codigo_19;
	this.codigo_20 = codigo_20;
}

function insertarVGEnArray(anomes, estado, t305_idproyectosubnodo,
                            consumo_lb_acum_a_mes,
                            consumo_iap_lb_acum_a_mes,
                            consumo_ext_lb_acum_a_mes, 
                            consumo_oco_lb_acum_a_mes,
                            consumo_ext_reconocido_lb_acum_a_mes,
                            produccion_lb_acum_a_mes, 
                            consumo_real_acum_a_mes,
                            produccion_real_acum_a_mes,
                            avance_tecnico_teorico_a_mes,
                            AC, AC_IAP, AC_EXT, AC_OCO,
                            BAC, BAC_IAP, BAC_EXT, BAC_OCO,
                            CPI, CV, EAC, EAC1, EAC2, EAC3, EACt, ETC,
                            EV, EV_IAP, EV_EXT, EV_OCO,
                            PV, PV_IAP, PV_EXT, PV_OCO,
                            SPI, SV, VAC, produccion_lb_total, PAC, PAC1, PAC2, PAC3, TCPI, TSPI, codigo_1, codigo_2, codigo_3, codigo_4,
                            codigo_5, codigo_6, codigo_7, codigo_8, codigo_9, codigo_10, codigo_11, codigo_12, codigo_13, codigo_14,
                            codigo_15, codigo_16, codigo_17, codigo_18, codigo_19, codigo_20) {
    try {
        oVGActivo = new oVG(anomes, estado, t305_idproyectosubnodo,
                            consumo_lb_acum_a_mes,
                            consumo_iap_lb_acum_a_mes,
                            consumo_ext_lb_acum_a_mes,
                            consumo_oco_lb_acum_a_mes,
                            consumo_ext_reconocido_lb_acum_a_mes,
                            produccion_lb_acum_a_mes,
                            consumo_real_acum_a_mes,
                            produccion_real_acum_a_mes,
                            avance_tecnico_teorico_a_mes,
                            AC, AC_IAP, AC_EXT, AC_OCO,
                            BAC, BAC_IAP, BAC_EXT, BAC_OCO,
                            CPI, CV, EAC, EAC1, EAC2, EAC3, EACt, ETC,
                            EV, EV_IAP, EV_EXT, EV_OCO,
                            PV, PV_IAP, PV_EXT, PV_OCO,
                            SPI, SV, VAC, produccion_lb_total, PAC, PAC1, PAC2, PAC3, TCPI, TSPI, codigo_1, codigo_2, codigo_3, codigo_4,
                            codigo_5, codigo_6, codigo_7, codigo_8, codigo_9, codigo_10, codigo_11, codigo_12, codigo_13, codigo_14,
                            codigo_15, codigo_16, codigo_17, codigo_18, codigo_19, codigo_20);
        aValorGanado[aValorGanado.length] = oVGActivo;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar un elemento en el array.", e.message);
    }
}

function buscarVGEnArray(nAnomes) {
    try {
        for (var nIndice = 0; nIndice < aValorGanado.length; nIndice++) {
            if (aValorGanado[nIndice].anomes == nAnomes) {
                oVGActivo = aValorGanado[nIndice];
                return aValorGanado[nIndice];
            }
        }
        return null;
    } catch (e) {
        mostrarErrorAplicacion("Error al buscar un elemento en el array.", e.message);
    }
}