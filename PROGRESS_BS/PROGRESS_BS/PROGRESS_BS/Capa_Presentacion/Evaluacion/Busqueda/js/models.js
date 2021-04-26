//Clase para el envio de datos ajax para el filtrado del catalogo.
var doConsultas = function requestFilter() {
    this.Origen = null;
    this.idevaluador = null;
    this.codestado = null;
    this.Idficepi_usuario = null;
    this.t934_idmodeloformulario = null;    
    this.desde                 = null;
    this.hasta                 = null;
    this.t001_idficepi = null;
    this.t001_idficepi_evaluador = null;
    //this.figura                 = 0;
    this.profundidad            = null;
    this.estado                 = "";
    this.t930_denominacionCR    = "";
    this.t930_denominacionROL	= "";
    this.t941_idcolectivo       = null;
    this.t930_puntuacion        = null;
    this.lestt930_gescli        = [];
    this.lestt930_liderazgo     = [];
    this.lestt930_planorga      = [];
    this.lestt930_exptecnico    = [];
    this.lestt930_cooperacion   = [];
    this.lestt930_iniciativa    = [];
    this.lestt930_perseverancia = [];
    this.estaspectos = null;
    this.Estmejorar = null;


    this.SelectMejorar = null;
    this.SelectSuficiente = null;
    this.SelectBueno = null;
    this.SelectAlto = null;

    this.SelectMejorarCAU = null;
    this.SelectSuficienteCAU = null;
    this.SelectBuenoCAU = null;
    this.SelectAltoCAU = null;

    this.lestaspectos           = [];
    this.lestt930_interesescar  = [];
    this.estreconocer           = null;
    this.estmejorar             = null;
    this.lcaut930_gescli        = [];
    this.lcaut930_liderazgo     = [];
    this.lcaut930_planorga      = [];
    this.lcaut930_exptecnico    = [];
    this.lcaut930_cooperacion   = [];
    this.lcaut930_iniciativa    = [];
    this.lcaut930_perseverancia = [];
    this.cauaspectos            = 0;
    this.lcauaspectos           = [];
    this.lcaut930_interesescar  = [];
    this.caumejorar = null;

    this.txtNombre = null;

}

