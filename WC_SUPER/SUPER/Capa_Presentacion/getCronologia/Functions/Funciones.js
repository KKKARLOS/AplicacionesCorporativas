function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
