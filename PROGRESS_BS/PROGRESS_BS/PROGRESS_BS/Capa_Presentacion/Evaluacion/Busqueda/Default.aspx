<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Busqueda_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />

    <title>Consulta evaluaciones</title>
    <link href="../../../js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />
    

    <!--Pasar a cliente el idficepi conectado (obtención de las estadísticas en base a él)-->
    <script>
        <%=idficepi%>
        <%=nombre%>
        <%=entrada%>
        <%=anoMin%>
        <%=modeloFormularios%>
        <%=filtrosConsultas%>
        var strServer = "<%=Session["strServer"]%>";        
    </script>

    <style>
        
    .animation {
        animation:efecto .5s;
        animation-fill-mode:both
    }
    @keyframes efecto {
        from {
            transform:scale(0.7);
            opacity:0
        }
        to {
            transform: scale(1);
            opacity:1
        }
    }

    </style>
   
</head>


<body data-codigopantalla="401">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />


    <!--Filtros de búsqueda-->
    <div class="container">
        <div class="panel-group">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <a id="linkFiltros"  data-toggle="collapse" href="#divFiltros" data-target="#divFiltros"></a>
                    <span style="margin-left:6px; margin-right:6px">|</span>
                    <a href="#" id="limpiarFiltros">Limpiar filtros</a>
                </div>

                <div id="divFiltros" class="panel-body panel-collapse collapse in">

                    <table id="tblFiltros"  class="table">
                        <thead>
                            <tr>
                                <th>Profesionales</th>
                            </tr>
                        </thead>
                    </table>


                    <div class="row">
                    

                        <div class="col-xs-12">
                            <fieldset class="fieldset" style="height:105px">

                                    <legend class="fieldset">

                                            
                                        <input id="rdbEvaluado" style="height: 15px" type="radio" value="0" name="rdbFigura" runat="server" checked>
                                        <span style="margin-right: 20px;">Evaluado/a </span>

                                        <input id="rdbEvaluador" style="height: 15px" type="radio" value="1" name="rdbFigura" runat="server">
                                        <span>Evaluador/a</span>

                                    </legend>
                                 <div class="col-xs-6" style="padding-left:0">

                                    <div id="divEvaluador" style="margin-top:3px" runat="server" class="fk-ocultar">
                                        <button id="lblEvaluador" style="margin-top:-3px" class="btn btn-link">Profesional</button>
                                        <input id="evaluador" type="text" readonly="readonly" />
                                        <i id="imgEvaluador" style="display:none" class="glyphicon glyphicon-repeat"></i>
                                    </div>


                                    <div id="divNivel" style="display: none; margin-top:4px">
                                        <span style="margin-left: 14px">Profundización</span>

                                        <select id="cboProfundizacion" runat="server">
                                            <option value="1" label="Primer nivel"></option>
                                            <option value="2" label="Hasta segundo nivel"></option>
                                            <option value="3" label="Nivel ilimitado"></option>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-xs-6">
                                   
                                    <span style="display:inline-block; height: 70px; border-left: 1px solid #ccc; margin-top: -6px;">                                        
                                    </span>
                                    <div class="fkInfo" style="margin:-81px 0 0 8px"> 
                                        
                                    </div>

                                </div>
                               

                            </fieldset>
                        </div>


                    </div>

                    <table id="tblFiltros2" class="table">
                        <thead>
                            <tr>
                                <th>Evaluaciones</th>
                            </tr>
                        </thead>
                    </table>

                    <div class="row">
                        <div class="col-xs-6">
                            <fieldset class="fieldset">
                                <legend class="fieldset"><span>Período</span></legend>
                                <div class="col-xs-6 MT-10 MB-10">
                                    <select id="selMesIni" runat="server"></select>
                                    <select id="selAnoIni" runat="server"></select>
                                </div>
                                <div class="col-xs-6 MT-10 MB-10">
                                    <select id="selMesFin" runat="server"></select>
                                    <select id="selAnoFin" runat="server"></select>
                                </div>
                            </fieldset>
                        </div>


                        <div class="col-xs-5 MT-10">
                            <div class="pull-right MB-10">
                                Estado
                                <select class="W160" runat="server" id="cboSituacion">
                                    <option value="" label="Todos"></option>
                                    <option value="ABI" label="Abiertas"></option>
                                    <option value="CUR" label="En curso"></option>
                                    <option value="CER" label="Cerradas"></option>
                                    <option value="CSF" label="Cerradas sin firmar"></option>
                                    <option value="CCF" label="Cerradas firmadas"></option>

                                </select>

                            </div>

                            <br />


                            <div id="divColectivo" class="pull-right MB-10">
                                <span>Colectivo</span>
                                <select id="cboColectivo" class="W160"  runat="server">
                                    <option value="" label="Todos"></option>
                                </select>
                            </div>

                            <br />

                            <div id="divCalidad" runat="server" class="pull-right MB-10">
                                <span>Calidad</span>
                                <select id="cboCalidad" class="W160" runat="server">
                                    <option value="" label="Todas"></option>
                                    <option value="1" label="Válidas"></option>
                                    <option value="2" label="No Válidas"></option>
                                    <option value="3" label="Cualificadas"></option>
                                    <option value="4" label="Sin cualificar"></option>
                                </select>
                            </div>

                        </div>
                    </div>




                    <div class="">
                        <div class="row">
                            <div id="divCR" runat="server" class="col-xs-6 fk-ocultar MB-10">
                                <button id="lblCR" class="btn btn-link">CR</button>
                                <input id="txtCR" type="text" readonly="readonly" />
                                <i id="imgCR" style="display:none" class="glyphicon glyphicon-repeat"></i>
                            </div>

                            <div id="divRol" runat="server" class="col-xs-6 fk-ocultar MB-10">
                                <button id="lblRol" class="btn btn-link">Rol</button>
                                <input id="txtRol" type="text" readonly="readonly" />
                                <i id="imgRol" style="display:none" class="glyphicon glyphicon-repeat"></i>
                            </div>
                        </div>

                        <div class="clearfix"></div>


                        <table id="tblFiltros3" class="table" style="display:none">
                            <thead>
                                <tr>
                                    <th>
                                        
                                        <a id="linkFormularioEstandar" data-toggle="collapse" href="#divFormularioEstandar" data-target="#divFormularioEstandar"><i class="glyphicon-plus"></i>Formulario estándar</a>
                                        
                                    </th>

                                    
                                </tr>
                            </thead>
                        </table>

                       <div id="divFormularioEstandar" class="collapse">

                        <div id="" class="col-xs-12">
                            <div id="aspectos" runat="server">
                                <div class="row col-xs-6 MB-10">
                                    <h4 style="display: inline-block">Aspectos a reconocer </h4>
                                    <select id="cboEstReconocer" runat="server">
                                        <option value="" label=""></option>
                                        <option value="0" label="Sin rellenar"></option>
                                        <option value="100" label="Menos de 100 caracteres"></option>
                                    </select>
                                </div>

                                <div class="pull-right MB-10">
                                    <h4 style="display: inline-block">Aspectos a mejorar </h4>
                                    <select id="cboEstMejorar" runat="server">
                                        <option value="" label=""></option>
                                        <option value="0" label="Sin rellenar"></option>
                                        <option value="100" label="Menos de 100 caracteres"></option>
                                    </select>
                                </div>
                            </div>

                           <div class="clearfix"></div>

                            <h4>Habilidades y valores corporativos</h4>
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


                                <div id="divHabilidades" class="MB-10">
                                    <!--Liderazgo / Gestión de equipo-->
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Gestión de relación de clientes                                        
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstGesCli" id="chkEstGesCli1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="2" name="chkEstGesCli" id="chkEstGesCli2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="2" name="chkEstGesCli" id="chkEstGesCli3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="2" name="chkEstGesCli" id="chkEstGesCli4" runat="server">
                                            </div>
                                        </div>
                                    </div>


                                    <!-- Liderazgo / Gestión de equipo-->
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Liderazgo/Gestión de equipos
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstLiderazgo" id="chkEstLiderazgo1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstLiderazgo" id="chkEstLiderazgo2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstLiderazgo" id="chkEstLiderazgo3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstLiderazgo" id="chkEstLiderazgo4" runat="server">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Planificación/Organización
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPlanOrga" id="chkEstPlanOrga1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPlanOrga" id="chkEstPlanOrga2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPlanOrga" id="chkEstPlanOrga3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPlanOrga" id="chkEstPlanOrga4" runat="server">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Expertise/Técnico
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstExpTecnico" id="chkEstExpTecnico1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstExpTecnico" id="chkEstExpTecnico2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstExpTecnico" id="chkEstExpTecnico3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstExpTecnico" id="chkEstExpTecnico4" runat="server">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Cooperación
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstCooperacion" id="chkEstCooperacion1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstCooperacion" id="chkEstCooperacion2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstCooperacion" id="chkEstCooperacion3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstCooperacion" id="chkEstCooperacion4" runat="server">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Iniciativa
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstIniciativa" id="chkEstIniciativa1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstIniciativa" id="chkEstIniciativa2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstIniciativa" id="chkEstIniciativa3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstIniciativa" id="chkEstIniciativa4" runat="server">
                                            </div>
                                        </div>
                                    </div>



                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Perseverancia
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPerseverancia" id="chkEstPerseverancia1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPerseverancia" id="chkEstPerseverancia2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPerseverancia" id="chkEstPerseverancia3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkEstPerseverancia" id="chkEstPerseverancia4" runat="server">
                                            </div>
                                        </div>
                                    </div>



                                    
                                    <div id="divEstAspectos" runat="server" class="fk-ocultar">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-xs-4">
                                                    <span>Más de X aspectos </span>                                                    
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos1" runat="server">--%>
                                                    <select id="selectMejorar">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos2" runat="server">--%>
                                                    <select id="selectSuficiente">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos3" runat="server">--%>
                                                    <select id="selectBueno">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos4" runat="server">--%>
                                                    <select id="selectAlto">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                        

                                       

                                    </div>





                                </div>
                            </div>
                        </div>



                       <div class="clearfix"></div>
                        <br />


                        <div class="col-xs-12 MB-20">
                            <h4>Intereses de carrera</h4>
                        <div class="panel panel-default">
                            <div class="panel panel-body evaluador">
                                 
                                <div class="row">
                                    <span class="checkbox-inline">Seguir la línea actual. Refuerzo del rol actual</span>
                                    
                                    <input  type="checkbox"  value="1" name="chkEvolucion" id="chkEvolucion1" runat="server">
                                    
                                </div>
                                <div class="row">
                                    <span class="checkbox-inline">Evolucionar hacia función:</span>
                                        

                                    <div id="divEvolucionar">
                                        <span class="checkbox-inline">Comercial</span>   
                                        <input type="checkbox" value="2" name="rdbEvolucion" id="rdbEvolucion2" runat="server">
                                                                           
                                        <span class="checkbox-inline">Técnica</span> 
                                        <input type="checkbox" value="3" name="rdbEvolucion" id="rdbEvolucion3" runat="server">                                        
                                                                            
                                        <span class="checkbox-inline">Gestión / Admón / RRHH</span> 
                                        <input type="checkbox" value="4" name="rdbEvolucion" id="rdbEvolucion4" runat="server">                                        
                                    
                                        <span class="checkbox-inline">Directiva</span> 
                                        <input type="checkbox" value="5" name="rdbEvolucion" id="rdbEvolucion5" runat="server">                                        
                                        
                                    </div>
                                   
                                </div>
                                <div class="row">
                                    <span class="checkbox-inline">Interesado en trayectoria internacional</span>
                                    
                                        <input type="checkbox" value="7" name="rdbEvolucion" id="rdbEvolucion7" runat="server">
                                    
                                </div>
                                <div class="row">
                                    <span class="checkbox-inline">Otros intereses profesionales (cambio de proyecto, geografía, tecnología, etc.)</span>
                                   
                                        <input type="checkbox" value="6" name="rdbEvolucion" id="rdbEvolucion6" runat="server">
                                   
                                </div>
                            </div>
                        </div>
                        </div>
                        
                        <br />

                        </div>


                        <table id="tblFiltros4" class="table" style="display:none">
                            <thead>
                                <tr>
                                    <th>                                       
                                        <a id="linkFormularioCAU" data-toggle="collapse" href="#divFormularioCAU" data-target="#divFormularioCAU"><i class="glyphicon-plus"></i>Formulario CAU</a>

                                    </th>
                                </tr>
                            </thead>
                        </table>
                       
                        <div id="divFormularioCAU" class="collapse">

                        <div class="col-xs-12">
                            <div id="aspectosCAU" runat="server">
                                <h4 style="display:inline-block">Aspectos a mejorar </h4>
                                <select id="cboCAUMejorar" runat="server">
                                    <option value="" label=""></option>
                                    <option value="0" label="Sin rellenar"></option>
                                    <option value="100" label="Menos de 100 caracteres"></option>
                                </select>
                            </div>
                            <div style="height:10px"></div>
                            <div class="clearfix"></div>

                            <h4>Habilidades y valores corporativos</h4>
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

                                <div id="divHabilidades2" class="MB-10">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Orientación al cliente
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUGesCli" id="chkCAUGesCli1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUGesCli" id="chkCAUGesCli2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUGesCli" id="chkCAUGesCli3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUGesCli" id="chkCAUGesCli4" runat="server">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Orientación a resultados
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAULiderazgo" id="chkCAULiderazgo1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAULiderazgo" id="chkCAULiderazgo2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAULiderazgo" id="chkCAULiderazgo3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAULiderazgo" id="chkCAULiderazgo4" runat="server">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Comunicación
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPlanOrga" id="chkCAUPlanOrga1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPlanOrga" id="chkCAUPlanOrga2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPlanOrga" id="chkCAUPlanOrga3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPlanOrga" id="chkCAUPlanOrga4" runat="server">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Compromiso
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUExpTecnico" id="chkCAUExpTecnico1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUExpTecnico" id="chkCAUExpTecnico2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUExpTecnico" id="chkCAUExpTecnico3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUExpTecnico" id="chkCAUExpTecnico4" runat="server">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Cooperación
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUCooperacion" id="chkCAUCooperacion1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUCooperacion" id="chkCAUCooperacion2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUCooperacion" id="chkCAUCooperacion3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUCooperacion" id="chkCAUCooperacion4" runat="server">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Iniciativa
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUIniciativa" id="chkCAUIniciativa1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUIniciativa" id="chkCAUIniciativa2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUIniciativa" id="chkCAUIniciativa3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUIniciativa" id="chkCAUIniciativa4" runat="server">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-xs-4">
                                                Perseverancia
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPerseverancia" id="chkCAUPerseverancia1" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPerseverancia" id="chkCAUPerseverancia2" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPerseverancia" id="chkCAUPerseverancia3" runat="server">
                                            </div>
                                            <div class="col-xs-2 text-center">
                                                <input type="checkbox" value="1" name="chkCAUPerseverancia" id="chkCAUPerseverancia4" runat="server">
                                            </div>
                                        </div>
                                    </div>


                                  
                                      <div id="divCAUAspectos" runat="server" class="fk-ocultar">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-xs-4">
                                                    <span>Más de X aspectos </span>                                                    
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos1" runat="server">--%>
                                                    <select id="selectMejorarCAU">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos2" runat="server">--%>
                                                    <select id="selectSuficienteCAU">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos3" runat="server">--%>
                                                    <select id="selectBuenoCAU">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-2 text-center">
                                                    <%--<input type="checkbox" value="1" name="chkEstAspectos" id="chkEstAspectos4" runat="server">--%>
                                                    <select id="selectAltoCAU">
                                                        <option value="" label=""></option>
                                                        <option value="1" label="1"></option>
                                                        <option value="2" label="2"></option>
                                                        <option value="3" label="3"></option>
                                                        <option value="4" label="4"></option>
                                                        <option value="5" label="5"></option>
                                                        <option value="6" label="6"></option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                    </div>


                                </div>
                            </div>
                        </div>


                       <%--  <div class="text-left dual-list list-left col-xs-4">
                            <h4>Intereses de carrera</h4>
                            <div id="divlisCAUIntCar">
                                <ul class="list-group" runat="server" id="lisCAUIntCar">
                                    <li class="list-group-item" data-value="1">Seguir en la línea actual</li>
                                    <li class="list-group-item" data-value="2">Refuerzo del rol actual</li>
                                    <li class="list-group-item" data-value="3">Otros intereses</li>
                                </ul>
                            </div>
                        </div>--%>

                        

                        <div class="col-xs-12 MB-20 MT-20">
                            <h4>Intereses de carrera</h4>
                            <div class="panel panel-default">
                                <div class="panel panel-body evaluador">

                                    <div class="row">
                                        <span class="checkbox-inline">Seguir la línea actual</span>

                                        <input type="checkbox" value="1" name="chkEvolucionCAU" id="chkEvolucionCAU1" runat="server">
                                    </div>
                                    <div class="row">
                                        <span class="checkbox-inline">Refuerzo del rol actual</span>
                                        <input type="checkbox" value="1" name="chkEvolucionCAU" id="chkEvolucionCAU2" runat="server">
                                    </div>
                                    <div class="row">
                                        <span class="checkbox-inline">Otros intereses</span>

                                        <input type="checkbox" value="7" name="chkEvolucionCAU" id="chkEvolucionCAU3" runat="server">
                                    </div>

                                </div>
                            </div>
                        </div>

                        </div>


                        <div class="clearfix"></div>

                        <div class="col-xs-12">
                            <button type="button" class="btn btn-primary pull-right" id="btnbuscar"><i class="glyphicon glyphicon-search" style="margin-right:4px"></i>Obtener</button>
                        </div>
                        

                        <!--Link ocultar-->
                        <div class="col-md-6">
                            <a id="linkFiltros2" data-toggle="collapse" href="#divFiltros" data-target="#divFiltros">Replegar filtros de búsqueda</a>
                            <span style="margin-left:6px; margin-right:6px;">|</span>
                            <a href="#" id="limpiarFiltros2">Limpiar filtros</a>
                        </div>

                    </div>


                </div>
            </div>
        </div>


        <div>

            <div class="row">
                <div id="tblResultados" style="display:none" class="col-xs-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Consulta de evaluaciones</h3>
                        </div>
                        <div class="panel-body">
                            <table id="tblEvaluaciones" class="table header-fixed tablesorter">
                                <thead>
                                    <tr>
                                        <th>&nbsp;</th>
                                        <th><span class="margenCabecerasOrdenables">Evaluador</span></th>
                                        <th><span class="margenCabecerasOrdenables">Evaluado</span></th>
                                        <th><span class="margenCabecerasOrdenables">Estado</span></th>
                                        <th><span class="margenCabecerasOrdenables">Apertura</span></th>
                                        <th><span class="margenCabecerasOrdenables">Cierre</span></th>
                                    </tr>
                                </thead>
                                <tbody runat="server" id="tbdEval">
                                </tbody>
                            </table>
      
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="pull-left" style="display:none;" id="tablePaging"> </div>                     
                    <button type="button" class="btn btn-primary pull-right" style="display:none; margin-left: 7px" id="btnAcceder" runat="server">Acceder a la evaluación</button>
                    <button id="btnAlterarEstado" class="btn btn-primary pull-right" style="display:none">Cambiar estado</button>
                    <button id="btnEliminar" class="btn btn-primary pull-right" style="display:none; margin-right:7px;">Eliminar</button>
                </div>
            </div>
        </div>
    </div>


    <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de profesional</h4>
                </div>
                <div class="modal-body">
                    <div id="txtBusqueda" class="row">
                        <div class="col-xs-3">
                            <label for="lblApellido1">Apellido 1º</label>
                            <input id="txtApellido1" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblApellido2">Apellido 2º</label>
                            <input id="txtApellido2" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label>
                            <input id="txtNombre" type="text" />
                        </div>
                        <div class="col-xs-1 col-xs-offset-1">
                            <button id="btnObtener" style="margin-top: 22px" class="btn btn-primary btn-xs">Obtener</button>
                        </div>
                    </div>


                    <br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluadores">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnSeleccionar" class="btn btn-primary">Seleccionar</button></b>
                    <b>
                        <button id="btnCancelar" class="btn btn-default">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADORES-->



     <!--MODAL EVALUADOS-->
    <div class="modal fade" id="modal-evaluados">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de profesional</h4>
                </div>
                <div class="modal-body">
                    <div id="txtBusquedaEvaluados" class="row">
                        <div class="col-xs-3">
                            <label for="lblApellido1Evaluados">Apellido 1º</label>
                            <input id="txtApellido1Evaluados" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblApellido2Evaluados">Apellido 2º</label>
                            <input id="txtApellido2Evaluados" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label>
                            <input id="txtNombreEvaluados" type="text" />
                        </div>
                        <div class="col-xs-1 col-xs-offset-1">
                            <button id="btnObtenerEvaluados" style="margin-top: 22px" class="btn btn-primary btn-xs">Obtener</button>
                        </div>
                    </div>


                    <br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluados">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnSeleccionarEvaluados" class="btn btn-primary">Seleccionar</button></b>
                    <b>
                        <button id="btnCancelarEvaluados" class="btn btn-default">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADOS-->




    <!--MODAL CR-->
    <div class="modal fade" id="modal-CR">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de CR</h4>
                </div>

                <div class="modal-body dual-list">                  
                    <div class="">
                        <h4>Catálogo de CR's presentes en alguna evaluación</h4>
                        <div class="well">
                            <div class="row">                           
                            <div class="col-xs-11 input-group" style="margin-left:15px">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                            <ul class="list-group" runat="server" id="lisCR">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnSeleccionarCR" class="btn btn-primary">Seleccionar</button></b>
                    <b>
                        <button id="btnCancelarCR" class="btn btn-default">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL CR-->


    <!--MODAL Rol-->
    <div class="modal fade" id="modal-Rol">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de Rol</h4>
                </div>
                <div class="modal-body dual-list">

                     <div class="">
                    <h4>Catálogo de roles presentes en alguna evaluación</h4>
                    <div class="well">
                        <div class="row">                           
                            <div class="col-xs-11 input-group" style="margin-left:15px">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="lisRol">
                        </ul>
                    </div>
                </div>


                   <%-- <div id="txtBusquedaRol" class="row">
                        <div class="col-xs-9">
                            <label for="lblNombreRol">Nombre</label>
                            <input id="txtNombreRol" type="text" />
                        </div>
                        <div class="col-xs-1 col-xs-offset-1">
                            <button id="btnObtenerRol" style="margin-top: 22px" class="btn btn-primary btn-xs">Obtener</button>
                        </div>
                    </div>
                    <br />--%>
                  <%--  <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisRol">
                            </ul>
                        </div>
                    </div>--%>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnSeleccionarRol" class="btn btn-primary">Seleccionar</button></b>
                    <b>
                        <button id="btnCancelarRol" class="btn btn-default">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL Rol-->


   
    <div class="modal fade" id="modal-alterarEstado">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Cambio de estado de evaluación</h4>
                </div>
                <div class="modal-body">
                    <span>Estado actual</span>
                    <h4 style="display:inline-block" id="txtEstadoActual"></h4>

                    <hr />

                    <span>Nuevo estado</span>
                    <select id="selectNuevoEstado">

                    </select>
                </div>
                <div class="modal-footer">                    
                    <button id="btnGrabar" type="button" class="btn btn-primary">Grabar</button>
                    <button id="btnCancelarGrabar" type ="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>


     <div class="modal fade" id="modal-confirmaciongrabar">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Confirmación de cambio de estado</h4>
                </div>
                <div class="modal-body">
                  <span>Pulsa 'Sí' para confirmar el cambio de estado. El cambio implicará una notificación tanto al evaluador/a como al evaluado/a.</span>
                </div>
                <div class="modal-footer">                    
                    <button id="btnSi" type="button" class="btn btn-primary">Sí</button>
                    <button id="btnNo" type ="button" class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>



    <div class="modal fade" id="modal-eliminar">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Confirmación de eliminación</h4>
                </div>
                <div class="modal-body">
                  <span>¿Confirmas la eliminación de la evaluación seleccionada?</span>
                  
                </div>
                <div class="modal-footer">                    
                    <button id="btnSiEliminacion" type="button" class="btn btn-primary">Sí</button>
                    <button id="btnNoEliminacion" type ="button" class="btn btn-default" data-dismiss="modal">No</button>
                </div>
            </div>
        </div>
    </div>




</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script src="../../../js/plugins/datatables/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="js/models.js"></script>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 

<script type="text/javascript" src="../../../js/pagination.js"></script> 
<script type="text/javascript" src="../../../js/moment.locale.min.js"></script>
