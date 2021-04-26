<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Evaluacion_Formularios_Estandar_Default" %>
<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />    
    <title>HOME PROGRESS</title>
    <script type="text/javascript" src="../models.js"></script>
    <script type="text/javascript">
        <%=estado%>
        <%=idvaloracion%>
        <%=puntuacion%>
        <%=origen%>
        <%=acceso%>
        <%=menu%>
        <%=IdEvaluacion%>
        var strServer = "<%=Session["strServer"]%>";    
    </script>
</head>

<body>
     <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br />
    
    <div style="position:relative">
        <img id="imgCualificacion" runat="server" style="position:absolute; width:100px; z-index:10000; right:160px; top:71px;" />
    </div>
    
     <div class="modal fade" id="modal-ConfirmCualificacion" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Confirmación de la cualificación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtCualificacion" class="cajaH5"></h5>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmar" class="btn btn-primary">Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div class="modal fade" id="modal-NOValida" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Confirmación de la cualificación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtCualificacionNoValida" class="cajaH5"></h5>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarNoValida" class="btn btn-primary">Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
   
    <div class="modal fade" id="modal-Descualificar" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Confirmación de la cualificación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtDescualificar" class="cajaH5"></h5>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarDescualificar" class="btn btn-primary">Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <a href="~/Capa_Presentacion/Evaluacion/Busqueda/Default.aspx?origen=Eva&pt=Estandar" id="btnRegresar" runat="server" style="display:none;">Regresar</a>


    <div class="container">
        <!-- Imagen estados -->
       <%-- <img id="imgEstados" runat="server"/>--%>

        <!--Evaluador -->
        <div class="row">
            <div class="col-xs-6">
                <fieldset class="fieldset">
                    <legend  class="fieldset"><span id="lgEvaluador"  runat="server" style="margin-top:3px"></span></legend>
                    <div class="col-xs-12">
                        <strong><span id="sEvaluador" runat="server"></span></strong>
                    </div>
                </fieldset>
            </div>
            <div class="col-xs-6">
                <fieldset class="fieldset" disabled>
                    <legend class="fieldset"><span>Evaluación</span></legend>
                    <b><span>Apertura</span></b>
                    <input type="text" class="W80" id="dApertura" runat="server" />
                    <b><span>Cierre</span></b>
                    <input type="text" class="W80" id="dCierre" runat="server" />
                </fieldset>
            </div>
        </div>

        <!--Evaluado-->
        <div class="row">
            <div class="col-xs-12">
                <fieldset class="fieldset evaluador">
                    <legend class="fieldset"><span id="lgEvaluado" runat="server"></span></legend>
                    <div class="row">
                        <div class="col-xs-6">
                            <strong><span id="sEvaluado" runat="server"></span></strong>
                        </div>


                         <div class="col-xs-5 col-xs-offset-1">
                              <strong><span style="vertical-align:top">C.R.</span></strong>
                            <%--<input class="W280" type="text" id="sCR" runat="server" disabled/>--%>
                             <textarea style="margin-left:3px" class="W320" type="text" id="sCR" rows="2" runat="server" disabled/>
                        </div>
                       


                    </div>
                    <!--Actividad-->
                    <div class="row">
                        <div class="col-xs-12">
                        </div>
                        <div class="col-xs-7">
                         <fieldset class="fieldset" style="width:480px">
                            <legend class="fieldset"><span>Actividad</span></legend>                                              
                            <label class="checkbox-inline">
                                <input type="radio" value="1" name="rdbActividad" id="rdbTecnica" runat="server">
                                Técnica
                            </label>
                            <label class="checkbox-inline">
                                <input type="radio" value="2" name="rdbActividad" id="rdbGestion" runat="server">
                                Gestión
                            </label>
                            <label class="checkbox-inline">
                                <input type="radio" value="3" name="rdbActividad" id="rdbComercial" runat="server">
                                Comercial
                            </label>
                            <label class="checkbox-inline">
                                <input type="radio" value="4" name="rdbActividad" id="rdbDirectiva" runat="server">
                                Directiva
                            </label>
                        </fieldset>
                        </div>


                       
                         <div class="col-xs-5 MT25">
                            <strong><span style="vertical-align:top">Rol</span></strong>
                            <%--<input class="W280" type="text" id="sRol"  runat="server" disabled/>--%>
                             <textarea style="margin-left:8px" class="W320" type="text" id="sRol" rows="2" runat="server" disabled/>

                        </div>

                    </div>                      
                </fieldset>
            </div>
        </div>

        <!-- Objeto de evaluación-->
        <div class="row">
            <div class="col-xs-12">
                <fieldset class="fieldset evaluador">
                    <legend class="fieldset"><span>Objeto de la evaluación</span></legend>
                    <div class="row">
                        <div class="col-xs-5">
                            <input id="rdbEvalDesempeno" value="2" runat="server" type="radio" name="evaluacion"/>Evaluación global del desempeño<br /><br />
                            <input id="rdbEvalProyecto" value="1" runat="server" type="radio" name="evaluacion"/>Evaluación de un proyecto
                        </div>



                         <div class="col-xs-7">
                            <fieldset class="fieldset">
                                <legend class="fieldset"><span>Período de evaluación</span></legend>
                                <div class="col-xs-6">
                                    <select id="selMesIni" runat="server">
                                        <option></option>
                                    </select>
                                    <select id="selAnoIni" runat="server">
                                        <option></option>
                                    </select>
                                </div>
                                <div class="col-xs-6">
                                    <select id="selMesFin" runat="server">
                                        <option></option>
                                    </select>
                                    <select id="selAnoFin" runat="server">
                                        <option></option>
                                    </select>
                                </div>

                            </fieldset>
                        </div>

                        <!-- Div Proyecto (sólo aparece si Evaluación de un proyecto está activo) -->
                        <div class="col-xs-offset-5">
                            <div id="divProyecto" class="row" runat="server">
                                <div class="col-xs-2">
                                    <strong><button id="lblProyecto" class="btn btn-link">Proyecto</button></strong>
                                </div>
                                <div class="col-xs-10">
                                    <input type="text" placeholder="Selecciona el proyecto" class="form-control" id="sProyecto" runat="server" disabled/>
                                </div>
                            </div>


                    </div>
                    
                       
                        </div>
                    
                </fieldset>
            </div>
        </div>

        <br />
        <div class="row">
            <div class="col-xs-12">
                <span>1. Aspectos más importantes a reconocer del trabajo realizado en el período que se valora (es imprescindible identificar las situaciones concretas de trabajo en que esos aspectos fueron puestos de manifiesto)</span>
            </div>
            <div class="col-xs-12">
                <textarea id="sAreconocer" runat="server" class="evaluador"></textarea>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-12">
                <span>2. Aspectos más importantes que deben ser mejorados (es imprescindible identificar las situaciones concretas de trabajo en que esos aspectos fueron puestos de manifiesto)</span>
            </div>
            <div class="col-xs-12">
                <textarea id="sAmejorar" runat="server" class="evaluador"></textarea>
            </div>
        </div>
    </div>
    <br />

    <!-- PUNTO 3-->

    <!-- HABILIDADES-->
    <div class="container">
        <span>3. Evalúa de modo general el desempeño del empleado en las siguientes habilidades y valores corporativos.</span>
        <div class="panel panel-default clearfix evaluador">
            <div class="panel-heading">
                <h2 class="panel-title">Habilidades</h2>
            </div>
            <div>
                <div class="col-xs-4"></div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">A mejorar</span>
                    <span class="visible-xs">S</span>
                </div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">Suficiente</span>
                    <span class="visible-xs">M</span>
                </div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">Bueno</span>
                    <span class="visible-xs">L</span>
                </div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">Alto</span>
                    <span class="visible-xs">XL</span>
                </div>
            </div>

            <div id="divHabilidades">
                <!--Liderazgo / Gestión de equipo-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            A) Gestión de relación de clientes                                          
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="habilidades" id="rdbGestion1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="habilidades" id="rdbGestion2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="habilidades" id="rdbGestion3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="habilidades" id="rdbGestion4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Liderazgo / Gestión de equipo-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            B) Liderazgo / Gestión de equipo
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="habilidades2" id="rdbLiderazgo1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="habilidades2" id="rdbLiderazgo2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="habilidades2" id="rdbLiderazgo3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="habilidades2" id="rdbLiderazgo4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Planificación / Organización-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            C) Planificación / Organización
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="habilidades3" id="rdbOrganizacion1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="habilidades3" id="rdbOrganizacion2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="habilidades3" id="rdbOrganizacion3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="habilidades3" id="rdbOrganizacion4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Expertise / Técnico-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            D) Expertise técnico
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="habilidades4" id="rdbTecnico1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="habilidades4" id="rdbTecnico2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="habilidades4" id="rdbTecnico3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="habilidades4" id="rdbTecnico4" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- VALORES CORPORATIVOS-->
    <div class="container">
        <div class="panel panel-default clearfix evaluador">
            <div class="panel-heading">
                <h2 class="panel-title">Valores Corporativos</h2>
            </div>
            <div>
                <div class="col-xs-4"></div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">A mejorar</span>
                    <span class="visible-xs">S</span>
                </div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">Suficiente</span>
                    <span class="visible-xs">M</span>
                </div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">Bueno</span>
                    <span class="visible-xs">L</span>
                </div>
                <div class="col-xs-2 text-center header">
                    <span class="hidden-xs">Alto</span>
                    <span class="visible-xs">XL</span>
                </div>
            </div>
            <div id="divValores">
                <!--Cooperación-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            A) Cooperación
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="valores" id="rdbCooperacion1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="valores" id="rdbCooperacion2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="valores" id="rdbCooperacion3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="valores" id="rdbCooperacion4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Iniciativa-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            B) Iniciativa
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="valores2" id="rdbIniciativa1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="valores2" id="rdbIniciativa2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="valores2" id="rdbIniciativa3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="valores2" id="rdbIniciativa4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Perseverancia-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            C) Perseverancia
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="valores3" id="rdbPerseverancia1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="valores3" id="rdbPerseverancia2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="valores3" id="rdbPerseverancia3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="valores3" id="rdbPerseverancia4" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- PUNTO 4-->
    <div class="container">
        <span>4. Intereses de carrera. (Seleccionar una opción)</span>
        <div class="panel panel-default">
            <div class="panel panel-body evaluador">
                <div class="row">
                    <span class="checkbox-inline PL10">A) Seguir la línea actual. Refuerzo del rol actual.</span>
                    <label class="checkbox-inline">
                        <input type="radio" class="MT-5" value="1" name="rdbEvolucion" id="rdbEvolucion1" runat="server">
                    </label>
                </div>
                <div class="row">
                    <span class="checkbox-inline PL10">B) Evolucionar hacia función</span>
                    <label class="checkbox-inline PL5">
                        <input type="radio" value="2" name="rdbEvolucion" id="rdbEvolucion2" runat="server">
                        Comercial
                    </label>
                    <label class="checkbox-inline PL5">
                        <input type="radio" value="3" name="rdbEvolucion" id="rdbEvolucion3" runat="server">
                        Técnica
                    </label>
                    <label class="checkbox-inline PL5">
                        <input type="radio" value="4" name="rdbEvolucion" id="rdbEvolucion4" runat="server">
                        Gestión / Admón / RRHH
                    </label>
                    <label class="checkbox-inline PL5">
                        <input type="radio" value="5" name="rdbEvolucion" id="rdbEvolucion5" runat="server">
                        Directiva
                    </label>
                </div>
                <div class="row">
                    <span class="checkbox-inline PL10">C) Interesado en trayectoria internacional</span>
                    <label class="checkbox-inline">
                        <input type="radio" class="MT-5"  value="7" name="rdbEvolucion" id="rdbEvolucion7" runat="server">
                    </label>
                </div>
                <div class="row">
                    <span class="checkbox-inline PL10">D) Otros intereses profesionales (cambio de proyecto, geografía, tecnología, etc)</span>
                    <label class="checkbox-inline">
                        <input type="radio" class="MT-5"  value="6" name="rdbEvolucion" id="rdbEvolucion6" runat="server">
                    </label>
                </div>
            </div>
        </div>
    </div>

    <!-- 5. Prioridades de formación-->
    <div class="container fk-div">
        <div class="row">
            <div class="col-xs-12">
                <span>5. Prioridades de formación</span>
            </div>
        </div>
        <span>La detección de necesidades de formación técnica se realizará a través de la dirección de cada unidad (plan de formación). Las acciones de competencias
        y habilidades se publicarán en En Forma, cada profesional previo acuerdo con su responsable se apuntará a las que respondan a sus intereses / necesidades.
        </span>
        <br />
        <span>Anota en esta sección las prioridades de formación sobre las que trabajará el profesional (cursos, autoaprendizaje, etc.) os servirá de contraste en la siguiente evaluación.
        </span>
        <textarea id="sFormacion" runat="server" class="evaluador"></textarea>
    </div>
    <br />
    <!-- 6. Campo de empleado-->
    <div class="container">
        <span>6. Campo de empleado. Autoevaluación con datos y anotaciones concretas</span>
        <br />
        <textarea id="sAutoevaluacion" runat="server" class="evaluado"></textarea>
    </div>
    <hr /><br />
    
    <div class="container">
        <div class="pull-left">
            <%--<span id="txtEvaluador" runat="server"></span>--%>
            
        </div>

        <div class="pull-right">
            <%--<span id="txtEvaluado" runat="server"></span>--%>
        </div>
    </div>
    
    <br /><br />

    <div class="container">
        <div id="selloEvaluado" runat="server" class="box sello"></div>        
        <div id="selloEvaluador" runat="server" class="box sello"></div>        
    </div>

    <div class="container">
        <div class="row">
            <div class="pull-left">                
                 <span runat="server" id="spanEvaluador" class="checkbox-inline ML60"></span>
            </div>


             <img id="imgEstados" runat="server"/>

            <div class="pull-right MR13">
               <span runat="server" id="spanEvaluado" class="checkbox-inline MR100"></span>
            </div>
        </div>
    </div>
    <hr />

    <div id="pieFormulario" class="container">
        <div class="row">
            <b><span class="ML50">Nota: Esta documentación es confidencial. No dejar a la vista.</span></b>
            <div class="pull-right" id="divBotones" runat="server">
                <b><button runat="server" type="button" id="sinfirma" style="display:none" class="btn btn-primary">Guardar sin firmar</button></b>
                <b><button runat="server" type="button" id="firmado" style="display:none;margin-right:7px" class="btn btn-primary">Firmar y finalizar</button></b>
                <b><button runat="server" type="button" id="Imprimir" style="display:inline-block; margin-right:7px" class="btn btn-primary">Imprimir</button></b>
                <b><button runat="server" type="button" id="cancelar"  style="display:none;margin-right:7px" class="btn btn-default">Cancelar</button></b>
                <b><button runat="server" type="button" id="Cvalida" style="display:none;margin-right:7px" class="btn btn-primary">Cualificar como válida</button></b>
                <b><button runat="server" type="button" id="CnoValida" style="display:none;margin-right:7px" class="btn btn-primary">Cualificar como no válida</button></b>
                <b><button runat="server" type="button" id="Descualificar" style="display:none;margin-right:7px"  class="btn btn-primary">Descualificar</button></b>                
                <b><button runat="server" type="button" id="regresar" style="display:none;" class="btn btn-primary">Regresar</button></b>
            </div>

        </div>
    </div>

    <!--MODAL PROYECTOS-->    
    <div class="modal fade" id="modal-proyectos">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de proyecto</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <p>
                            Relación de proyectos en los que el evaluado ha colaborado en el periodo indicado
                        </p>
                        <br />
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisProyectos">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button class="btn btn-primary">Seleccionar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL PROYECTOS-->
    
    <!-- MODAL EVALUADOR - Guardar y firmar -->
    
    <div class="modal fade" id="modal-evaluador">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Firmar y finalizar</h4>
                </div>
                <div class="modal-body">
                <p>La firma de la evaluación implica la grabación de la infomación con tu firma, y una comunicación al evaluado para que aporte su autoevaluación y firma. Por tanto, la evaluación ya no estará accesible para ti en modo edición. ¿Estás conforme?</p>
                </div>
                <div class="modal-footer">
                    <b><button id="evaluadormn" class="btn btn-default pull-right" style="margin-left:10px" data-dismiss="modal">No</button></b>
                    <b><button id="evaluadorms" class="btn btn-primary pull-right">Sí</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->-

    <!--FIN MODAL EVALUADOR - Guardar y firmar -->

    <!-- MODAL EVALUADO - Guardar sin firmar -->
    
    <div class="modal fade" id="modal-evaluado-sinfirma">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Guardar sin firmar</h4>
                </div>
                <div class="modal-body">
                <p>Estás guardando la evaluación sin firmarla. A los 15 días de la fecha de firma de la evaluación por parte de tu evaluador, la evaluación se cierra de forma definitiva y ya no tendrás acceso a ella en modo edición. ¿Quieres continuar con la grabación, o regresar a la edición de la evaluación?</p>
                </div>
                <div class="modal-footer">
                    <b><button id="evaluadomr" class="btn btn-default pull-right" style="margin-left:7px" data-dismiss="modal">Regresar</button></b>
                    <b><button id="evaluadomc" class="btn btn-primary pull-right">Continuar</button></b>
                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->-

    <!--FIN MODAL EVALUADO - Guardar sin firmar -->

    <!-- MODAL EVALUADO - Guardar y firmar -->
    
    <div class="modal fade" id="modal-evaluado-confirma">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Firmar y finalizar</h4>
                </div>
                <div class="modal-body">
                <p>La firma de la evaluación implica la grabación de la infomación con tu firma, y una comunicación al evaluador avisando del cierre de la evaluación. Por tanto, ya no podrás modificarla. ¿Quieres seguir adelante?</p>
                </div>
                <div class="modal-footer">
                    <b><button id="evaluadomn" class="btn btn-default pull-right" style="margin-left:10px" data-dismiss="modal">No</button></b>
                    <b><button id="evaluadoms" class="btn btn-primary pull-right" >Sí</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->-

    <!--FIN MODAL EVALUADO - Guardar y firmar -->


     <div class="modal fade" id="modal-datosmodificados">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Datos modificados</h4>
                </div>
                <div class="modal-body">
                <p>Has introducido cambios en el formulario que, si no se graban, no saldrán impresos. ¿Quieres grabarlos?</p>
                </div>
                <div class="modal-footer">                    
                    <b><button id="btnCancelar" style="margin-left:10px" class="btn btn-default pull-right" data-dismiss="modal">No</button></b>
                    <b><button id="btnconfirm" class="btn btn-primary pull-right">Sí</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>




</body>
</html>

<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<!-- Aquí iría el js de la página-->
<%--<script src="/js/plugins/datepicker/bootstrap-datepicker.js"></script>
<script src="/js/plugins/datepicker/locales/bootstrap-datepicker.es.js"></script>--%>