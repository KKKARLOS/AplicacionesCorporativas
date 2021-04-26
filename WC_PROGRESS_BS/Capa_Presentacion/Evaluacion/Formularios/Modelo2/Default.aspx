<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Evaluacion_Formularios_TAU_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />    
    <title>Evaluación del desempeño</title>
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



    <div class="container">
        <!-- Imagen estados -->
        <%--<img id="imgEstados" runat="server" />--%>


        <!--Evaluador -->
        <div class="row">
            <div class="col-xs-6">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span id="lgEvaluador"  runat="server"></span></legend>
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
                    <legend class="fieldset"><span id="lgEvaluado"  runat="server"></span></legend>
                    <div class="row">
                        <div class="col-xs-4">
                            <strong><span id="sEvaluado" runat="server"></span></strong>
                        </div>
                        <div class="col-xs-6 col-xs-offset-2">
                            <strong><span style="vertical-align:top">Rol</span></strong>
                            <%--<input  type="text" id="sRol" runat="server" disabled />--%>
                            <textarea rows="2"  type="text" id="sRol" runat="server" disabled />
                        </div>                      
                    </div>
                    <br />
                    <div class="row">
                          <div class="col-xs-7 col-xs-offset-6">
                            <strong><span style="vertical-align:top">C.R.</span></strong>
                            <%--<input  type="text" id="sCR" runat="server" disabled />--%>
                            <textarea rows="2"  type="text" id="sCR" runat="server" disabled />

                          </div>
                    </div>

                </fieldset>
            </div>
        </div>



      <br />

        <!-- PUNTO 1-->

        <!-- DESEMPEÑO PROFESIONAL-->

        <span>1. Analiza a continuacion los siguientes aspectos del desempeño profesional.</span><br />
        <div class="panel panel-default clearfix evaluador">
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
                <!--Atención al cliente interno/externo -->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            A) Atención al cliente interno/externo                                          
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="desemProfesional" id="desemProfesional1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="desemProfesional" id="desemProfesional2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="desemProfesional" id="desemProfesional3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="desemProfesional" id="desemProfesional4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Plazo de respuesta-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            B) Plazo de respuesta
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="plazoResp" id="plazoResp1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="plazoResp" id="plazoResp2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="plazoResp" id="plazoResp3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="plazoResp" id="plazoResp4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Calidad de respuesta-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            C) Calidad de respuesta
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="calidadResp" id="calidadResp1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="calidadResp" id="calidadResp2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="calidadResp" id="calidadResp3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="calidadResp" id="calidadResp4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Respuesta ante situaciones difíciles-->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            D) Respuesta ante situaciones difíciles
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="dificilesResp" id="dificilesResp1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="dificilesResp" id="dificilesResp2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="dificilesResp" id="dificilesResp3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="dificilesResp" id="dificilesResp4" runat="server" />
                        </div>
                    </div>
                </div>

                <!-- Valoración de indicadores de su actividad -->
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4">
                            E) Valoración de indicadores de su actividad
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="1" name="valoraActividad" id="valoraActividad1" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="2" name="valoraActividad" id="valoraActividad2" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="3" name="valoraActividad" id="valoraActividad3" runat="server" />
                        </div>
                        <div class="col-xs-2 text-center">
                            <input type="radio" value="4" name="valoraActividad" id="valoraActividad4" runat="server" />
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <!-- PUNTO 2 -->
        <!-- ASPECTOS A RESALTAR -->

        <div class="row">
            <div class="col-xs-12">
                <span>2. Si lo consideras oportuno identifica aspectos a resaltar, bien por tratarse de desempeño destacado o bien porque deben ser mejorados.</span>
            </div>
            <div class="col-xs-12">
                <textarea id="sAmejorar" runat="server" class="evaluador"></textarea>
            </div>
        </div>
        <br />

        
    <!-- PUNTO 3-->
    <!-- HABILIDADES Y VALORES CORPORATIVOS-->      
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

        <div id="divHabilidades2">
            <!--Orientación al cliente-->
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-4">
                        A) Orientación al cliente
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="1" name="orientaCliente" id="rdbOrientaCliente1" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="2" name="orientaCliente" id="rdbOrientaCliente2" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="3" name="orientaCliente" id="rdbOrientaCliente3" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="4" name="orientaCliente" id="rdbOrientaCliente4" runat="server" />
                    </div>
                </div>
            </div>

            <!-- Orientación a resultados-->
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-4">
                        B) Orientación a resultados
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="1" name="orientaResul" id="rdbOrientaResul1" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="2" name="orientaResul" id="rdbOrientaResul2" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="3" name="orientaResul" id="rdbOrientaResul3" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="4" name="orientaResul" id="rdbOrientaResul4" runat="server" />
                    </div>
                </div>
            </div>

            <!-- Comunicación-->
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-4">
                        C) Comunicación
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="1" name="comunicacion" id="rdbComunicacion1" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="2" name="comunicacion" id="rdbComunicacion2" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="3" name="comunicacion" id="rdbComunicacion3" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="4" name="comunicacion" id="rdbComunicacion4" runat="server" />
                    </div>
                </div>
            </div>

            <!-- Compromiso-->
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-4">
                        D) Compromiso
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="1" name="compromiso" id="rdbCompromiso1" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="2" name="compromiso" id="rdbCompromiso2" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="3" name="compromiso" id="rdbCompromiso3" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="4" name="compromiso" id="rdbCompromiso4" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
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

        <div id="divValoresCorporativos">
            <!--Cooperación-->
            <div class="panel-body">
                <div class="row">
                    <div class="col-xs-4">
                        A) Cooperación
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="1" name="cooperacion" id="rdbCooperacion1" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="2" name="cooperacion" id="rdbCooperacion2" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="3" name="cooperacion" id="rdbCooperacion3" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="4" name="cooperacion" id="rdbCooperacion4" runat="server" />
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
                        <input type="radio" value="1" name="iniciativa" id="rdbIniciativa1" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="2" name="iniciativa" id="rdbIniciativa2" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="3" name="iniciativa" id="rdbIniciativa3" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="4" name="iniciativa" id="rdbIniciativa4" runat="server" />
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
                        <input type="radio" value="1" name="perseverancia" id="rdbPerseverancia1" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="2" name="perseverancia" id="rdbPerseverancia2" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="3" name="perseverancia" id="rdbPerseverancia3" runat="server" />
                    </div>
                    <div class="col-xs-2 text-center">
                        <input type="radio" value="4" name="perseverancia" id="rdbPerseverancia4" runat="server" />
                    </div>
                </div>
            </div>           
        </div>
    </div>

    <!-- PUNTO 4-->    
    <span>4. Intereses de carrera. (Seleccionar una opción)</span>
    <div class="panel panel-default">
        <div class="panel panel-body evaluador" id="divIntereses">
            <div class="row">
                <span class="checkbox-inline PL10">A) Seguir la línea actual</span>
                <label class="checkbox-inline">
                    <input type="radio" class="MT-5" value="1" name="intereses" id="rdbIntereses1" runat="server">
                </label>
            </div>
            <div class="row">
                <span class="checkbox-inline PL10">B) Refuerzo del rol actual</span>   
                <label>
                    <input type="radio" class="MT-5" value="2" name="intereses" id="rdbIntereses2" runat="server">
                </label>
            </div>
            <div class="row">
                <div class="col-xs-5">
                    <span id="spanOtrosIntereses" class="checkbox-inline PL10">C) Otros intereses profesionales (especificar)</span>
                    <label class="checkbox-inline">
                        <input type="radio" class="MT-5" value="3" name="intereses" id="rdbIntereses3" runat="server">
                    </label>
                </div>

                <div id="divOtrosIntereses" runat="server" class="col-xs-7">
                    <input id="otrosIntereses" runat="server" placeholder="Indica los posibles intereses del evaluado" type="text" />
                </div>
            </div>
        </div>
    </div>
    
    <!--PUNTO 5 -->
    <span>5. Necesidades de Formación en su puesto actual (señalar sólo en caso de que sea necesario, especificando contenidos) </span>
    <br /><br />
    
    <div id="divNecesidadFormacion">
    <div class="row evaluador">
        <div class="col-xs-5">
            <span>Ofimática</span>
        </div>
        <div class="col-xs-1">
            <input type="checkbox" id="chkOfimatica" runat="server"/>
        </div>

        <div class="col-xs-6">
            <input type="text" id="lblOfimatica" runat="server"/>
        </div>
    </div>

    <div class="row evaluador">
        <div class="col-xs-5">
            <span>Nuevas tecnologías (Hardware, Software...)</span>
        </div>
        <div class="col-xs-1">
            <input type="checkbox" id="chkTecnologias" runat="server"/>
        </div>
        <div class="col-xs-6">
            <input type="text" id="lblTecnologias" runat="server"/>
        </div>
    </div>

    <div class="row evaluador">
        <div class="col-xs-5">
            <span>Atención al cliente</span>
        </div>
        <div class="col-xs-1">
            <input type="checkbox" id="chkCliente" runat="server"/>
        </div>
        <div class="col-xs-6">
            <input type="text" id="lblCliente" runat="server"/>
        </div>
    </div>

    <div class="row evaluador">
        <div class="col-xs-5">
            <span>Comunicación efectiva / presentaciones</span>
        </div>
        <div class="col-xs-1">
            <input type="checkbox" id="chkComunicacion" runat="server"/>
        </div>
        <div class="col-xs-6">
            <input type="text" id="lblComunicacion" runat="server"/>
        </div>
    </div>

    <div class="row evaluador">
        <div class="col-xs-5">
            <span>Venta de ideas</span>
        </div>
        <div class="col-xs-1">
            <input type="checkbox" id="chkIdeas" runat="server"/>
        </div>
        <div class="col-xs-6">
            <input type="text" id="lblIdeas" runat="server"/>
        </div>
    </div>

    <div class="row evaluador">
        <div class="col-xs-5">
            <span>Conocimientos específicos</span>
        </div>
        <div class="col-xs-1">
            <input type="checkbox" id="chkConocimientos" runat="server"/>
        </div>
        <div class="col-xs-6">
            <input type="text" id="lblConocimientos" runat="server"/>
        </div>
    </div>
    </div>

    <br />

    <span>Las propuestas que se incluyen en esta sección se tendrán en cuenta para confeccionar el próximo plan de formación "Aquí fecha (ejemplo 10/11)"</span>

    </div>
    <br />

    <!-- PUNTO 6 -->
    <div class="container">
        <span>6. Campo de empleado. Autoevaluación con datos y anotaciones concretas.</span><br />
        <textarea id="sAutoevaluacion" runat="server" class="evaluado"></textarea>
    </div>


    <hr /><br />

     <!-- Firma de evaluador, de evaluado y pie de página -->
     <div class="container">
        <div class="pull-left">
            <%--<span id="txtEvaluador" runat="server"></span>            --%>
        </div>

         <img id="imgEstados" runat="server" />

        <div class="pull-right">
            <%--<span id="txtEvaluado" runat="server"></span>--%>
        </div>
    </div>
    
    <br /><br />

    <div class="container">
        <div id="selloEvaluador" runat="server" class="box sello"></div>
        <div id="selloEvaluado" runat="server" class="box sello"></div>        
    </div>

    <div class="container">
        <div class="row">
            <div class="pull-left">
                
                <span id="spanEvaluador" runat="server" class="checkbox-inline ML60"></span>
            </div>

            <div class="pull-right MR13">
                <span id="spanEvaluado" runat="server" class="checkbox-inline MR100"></span>
            </div>
        </div>
    </div>
    <hr />

    <%--<div id="pieFormulario" class="container">
        <div class="row">
            <b><span class="ML50">Nota: Esta documentación es confidencial. No dejar a la vista.</span></b>
            <div class="pull-right" id="divBotones" runat="server">
                <b><button type="button" id="sinfirma" class="btn btn-primary">Guardar sin firmar</button></b>
                <b><button type="button" id="firmado" class="btn btn-primary">Firmar y finalizar</button></b>
            </div>
        </div>
    </div>--%>


    <div id="pieFormulario" class="container">
        <div class="row">
            <b><span class="ML50">Nota: Esta documentación es confidencial. No dejar a la vista.</span></b>
            <div class="pull-right" id="divBotones" runat="server">
                <b><button runat="server" type="button" id="sinfirma" style="display:none" class="btn btn-primary">Guardar sin firmar</button></b>
                <b><button runat="server" type="button" id="firmado" style="display:none" class="btn btn-primary">Firmar y finalizar</button></b>
                <b><button runat="server" type="button" id="Imprimir" style="display:inline-block;" class="btn btn-primary">Imprimir</button></b>
                <b><button runat="server" type="button" id="cancelar"  style="display:none" class="btn btn-default">Cancelar</button></b>
                <b><button runat="server" type="button" id="Cvalida" style="display:none" class="btn btn-primary">Cualificar como válida</button></b>
                <b><button runat="server" type="button" id="CnoValida" style="display:none" class="btn btn-primary">Cualificar como no válida</button></b>
                <b><button runat="server" type="button" id="Descualificar" style="display:none"  class="btn btn-primary">Descualificar</button></b>
                
                <b><button runat="server" type="button" id="regresar" style="display:none" class="btn btn-primary">Regresar</button></b>
            </div>
        </div>
    </div>
   
    <!-- MODAL EVALUADOR - Guardar y firmar -->
    
    <div class="modal fade" id="modal-evaluador">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Firmar y finalizar</h4>
                </div>
                <div class="modal-body">
                <p>La firma de la evaluación implica la grabación de la infomación con tu firma, y una comunicación al evaluado para que aporte su autoevaluación y firma. Por tanto, ya no podrás modificarla. ¿Quieres seguir adelante?</p>
                </div>
                <div class="modal-footer">
                    <b><button style="margin-left:10px;" id="evaluadormn" class="btn btn-default pull-right" data-dismiss="modal">No</button></b>
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
                <p>La firma de la evaluación implica la grabación de la infomación con tu firma, y una comunicación al evaluador avisando del cierre de la evaluación. Por tanto, la evaluación ya no estará accesible para ti en modo edición. ¿Estás conforme?</p>
                </div>
                <div class="modal-footer">
                    <b><button id="evaluadomn" class="btn btn-default pull-right" style="margin-left:7px" data-dismiss="modal">No</button></b>
                    <b><button id="evaluadoms" class="btn btn-primary pull-right">Sí</button></b>
                    
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
 