<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Test_Ayudas_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>




<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />

</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form id="form1" runat="server">
</form>
    
    <input type="button" id="btnComercial" value="Comercial" />
    <input type="button" id="btnPromotor" value="Promotor" />
    <input type="button" id="btnLideres" value="Lideres" />
    <input type="button" id="btnClientes" value="Clientes" />
    <input type="button" id="btnAccionesPreventa" value="AccionesPreventa" />
    <input type="button" id="btnAreas" value="Areas" />
    <input type="button" id="btnSubareas" value="Subareas" />

    <div id="divAyudaComercial"></div>
    <div id="divAyudaPromotor"></div>
    <div id="divAyudaLideres"></div>
    <div id="divAyudaClientes"></div>
    <div id="divAccionesPreventa"></div>
    <div id="divAreas"></div>
    <div id="divSubareas"></div>
    <div id="ayuda1"></div>

    <script src="ayuda.js"></script>
    <script src="ayudamulti.js"></script>
    <script src="ayudasubareasficepi.js"></script>
    <script src="../../../../plugins/IB/buscaprof.js"></script>
    <script src="buscaprofmulti.js"></script>

    <script>
        $(document).ready(function () {
            

            $("#btnComercial").on("click", function () { $("#divAyudaComercial").buscaprof("show") });
            $("#divAyudaComercial").buscaprof({
                titulo: "Selección de comercial",
                modulo: "SIC",
                autoSearch: false,
                autoShow: false,
                tipoAyuda: "ComercialesPreventa",
                notFound: "No se han encontrado resultados.",
                onSeleccionar: function (data) {
                    console.log("seleccionar");
                    console.log(data.idficepi);
                    console.log(data.profesional);
                }
            });


            $("#btnPromotor").on("click", function () { $("#divAyudaPromotor").buscaprof("show") });
            $("#divAyudaPromotor").buscaprof({
                titulo: "Selección de promotor de acción preventa",
                modulo: "SIC",
                autoSearch: false,
                autoShow: false,
                tipoAyuda: "PromotoresAccionPreventa",
                notFound: "No se han encontrado resultados.",
                onSeleccionar: function (data) {
                    console.log("seleccionar");
                    console.log(data.idficepi);
                    console.log(data.profesional);
                }
            });

            $("#btnLideres").on("click", function () { $("#divAyudaLideres").buscaprofmulti("show") });
            $("#divAyudaLideres").buscaprofmulti({
                titulo: "Selección de líderes de acción preventa",
                tituloContIzda: "Profesionales",
                tituloContDcha: "Profesionales seleccionados",
                notFound: "No se han encontrado resultados.",
                modulo: "SIC",
                tipoAyuda: "LideresPreventa",
                autoSearch: true,
                autoShow: false,
                eliminarExistentes: true,
                lstSeleccionados:[
                    { t001_idficepi: 7583, profesional: "ASENJO ANDUEZA, Javier", estado: "X"},
                    { t001_idficepi: 1196, profesional: "ASENJO MORGADES, Milagros", estado: "E" },
                    { t001_idficepi: 18745, profesional: "ASPURZ ARANDIGOYEN, Manuel" },
                    { t001_idficepi: 4663, profesional: "ASUMENDI FRANCISCO, David", estado: "N" }
                ],
                onAceptar: function (data) {
                    console.log("Aceptar:");
                    data.forEach(function (item) {
                        console.log("t001_idficepi:"+ item.t001_idficepi + " profesional:" + item.profesional + " estado:" + item.estado);
                    });
                }
            });

            $("#btnClientes").on("click", function () { $("#divAyudaClientes").ayudamulti("show") });
            $("#divAyudaClientes").ayudamulti({
                titulo: "Selección de clientes",
                tituloContIzda: "Clientes",
                tituloContDcha: "Clientes seleccionados",
                notFound: "No se han encontrado resultados.",
                modulo: "SIC",
                tipoAyuda: "CuentasCRM",
                autoSearch: false,
                autoShow: false,
                eliminarExistentes: true,
                placeholderText: "Introduce el comienzo de la denominación de la cuenta",
                filtro: "",
                lstSeleccionados: [
                    { key: 1, value: "CLIENTE 1", estado: "X" },
                    { key: 2, value: "CLIENTE 2", estado: "E" },
                    { key: 3, value: "CLIENTE 3" },
                    { key: 4, value: "CLIENTE 4", estado: "N" }
                ],
                onAceptar: function (data) {
                    console.log("Aceptar:");
                    data.forEach(function (item) {
                        console.log("key:" + item.key + " value:" + item.value);
                    });
                }
            });

            $("#btnAccionesPreventa").on("click", function () { $("#divAccionesPreventa").ayudamulti("show") });
            $("#divAccionesPreventa").ayudamulti({
                titulo: "Selección de acciones preventa",
                tituloContIzda: "Acciones preventa",
                tituloContDcha: "Acciones seleccionadas",
                notFound: "No se han encontrado resultados.",
                modulo: "SIC",
                tipoAyuda: "AccionesPreventa",
                autoSearch: true,
                autoShow: false,
                eliminarExistentes: true,
                placeholderText: "Introduce el comienzo de la denominación de la acción",
                filtro: "",
                lstSeleccionados: [
                    { key: 1, value: "ACCION 1", estado: "X" },
                    { key: 2, value: "ACCION 2", estado: "E" },
                    { key: 4, value: "ACCION 3", estado: "N" }
                ],
                onAceptar: function (data) {
                    console.log("Aceptar:");
                    data.forEach(function (item) {
                        console.log("key:" + item.key + " value:" + item.value);
                    });
                }
            });

            $("#btnAreas").on("click", function () { $("#divAreas").ayudamulti("show") });
            $("#divAreas").ayudamulti({
                titulo: "Selección de áreas preventa",
                tituloContIzda: "Áreas preventa",
                tituloContDcha: "Áreas seleccionadas",
                notFound: "No se han encontrado resultados.",
                modulo: "SIC",
                tipoAyuda: "AreasPreventa",
                autoSearch: true,
                autoShow: false,
                eliminarExistentes: true,
                placeholderText: "Introduce el comienzo de la denominación del área",
                filtro: "",
                lstSeleccionados: [
                    { key: 1, value: "AREA 1", estado: "X" },
                    { key: 2, value: "AREA 2", estado: "E" },
                    { key: 4, value: "AREA 3", estado: "N" }
                ],
                onAceptar: function (data) {
                    console.log("Aceptar:");
                    data.forEach(function (item) {
                        console.log("key:" + item.key + " value:" + item.value);
                    });
                }
            });

            $("#btnSubareas").on("click", function () { $("#divSubareas").ayudasubareasficepi("show") });
            $("#divSubareas").ayudasubareasficepi({
                autoSearch: false,
                autoShow: true,
                eliminarExistentes: true,
                lstSeleccionados: [
                    { key: 1000, value: "SUBAREA 1", estado: "X" },
                    { key: 2000, value: "SUBAREA 2", estado: "E" },
                    { key: 4000, value: "SUBAREA 3", estado: "N" }
                ],
                onAceptar: function (data) {
                    console.log("Aceptar:");
                    data.forEach(function (item) {
                        console.log("key:" + item.key + " value:" + item.value);
                    });
                }
            });

            //$("#ayuda1").ayudamulti({
            //    titulo: "Ayuda de Cuentas",
            //    modulo: "SIC",
            //    autoSearch: false,
            //    filtro: "",
            //    autoShow: true,
            //    tipoAyuda: "cuentasCRM",
            //    placeholderText: "Introduce el comienzo de la denominación de la cuenta",
            //    notFound: "No se han encontrado resultados.",
            //    onSeleccionar: function (data) {
            //        console.log("seleccionar");
            //        console.log(data.key);
            //        console.log(data.value);
            //    },
            //    onCancelar: function () {
            //        console.log("cancelar");
            //    }
            //});
        });



    </script>

</body>
</html>




