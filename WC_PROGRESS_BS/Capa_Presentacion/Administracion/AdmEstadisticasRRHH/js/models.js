//Clase para el envio de datos ajax para el filtrado del catalogo.
var doEstadisticas = function requestFilter() {

    //EVALUACIONES
    this.Desde = null;
    this.Hasta = null;
    
    //this.Profundidad = null;

    this.t001_fecantigu = null;
    this.t941_idcolectivo = null;
    this.estado = null;
    this.t930_denominacionCR = null;
    //this.t935_idcategoriaprofesional = null;

    //EVALUADORES
    this.t303_idnodo_evaluadores = null;

    //COLECTIVOS    
    this.DesdeColectivos = null;
    this.HastaColectivos = null;
    this.t001_fecantiguColectivos = null;
    this.t303_idnodo_colectivos = null;
    this.t941_idcolectivo_colectivos = null;
    this.t001_idficepi = null;
    
}
var doParametrosRPT = function requestParamsRPT() {
    this.fecDesde = null;
    this.fecHasta = null;
    this.txtSituacion = null;
    this.sexo = null;
    this.txtEvaluador = null;
    this.txtProfundizacion = null;
    this.txtColectivo = null;
    this.txtCR_Evaluaciones = null;
    this.txtCR_Evaluadores = null;
    this.txtCR_Profesionales = null;
    this.txtEstado = null;
}