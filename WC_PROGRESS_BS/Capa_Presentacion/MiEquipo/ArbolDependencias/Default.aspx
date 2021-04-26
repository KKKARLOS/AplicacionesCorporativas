<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MiEquipo_ArbolDependencias_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Árbol de dependencias</title>
    <script>
        <%=idficepi%>
        <%=nombre%>        
        <%=origen%> 
    </script>

    <style>
       

    
    </style>
</head>
<body data-codigopantalla="203">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    
    <br class="hidden-xs" />
    <br class="hidden-xs" />


    <form runat="server"></form>

     <!--MODAL POTENCIAL-->
    <div class="modal fade" id="modal-potencial">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Datos complementarios</h4>
                </div>
                <div class="modal-body">

                   <fieldset class="fieldset">
                    <legend class="fieldset">Potencial: <span id="esPotencial"></span></legend>
                                       
                    <div id="divPotencial">                      
                        
                        <div class="row">
                            <div class="col-xs-6">
                                <span>Pruebas de identificación realizadas</span>
                                
                            </div>
                            <div class="col-xs-1">
                                <textarea id="txtPruebasRealizadas" disabled="disabled"></textarea>
                            </div>
                        </div>

                        <br />

                        <div class="row">
                            <div class="col-xs-6">
                                <span>Fecha de identificación</span>
                            </div>
                            <div class="col-xs-5">
                                <input id="fecha" type="text" value="" disabled="disabled" />
                            </div>
                        </div>

                        <br />

                        <div class="row">
                            <div class="col-xs-6">
                                <span>Origen de identificación</span>
                            </div>
                            <div class="col-xs-6">
                                <textarea id="origen" rows="2" type="text" value="" disabled="disabled"></textarea>
                            </div>
                        </div>

                        <hr />

                        <!-- Archivos potencial -->
                        <div class="row">
                            <table  class="table table-striped table-hover">
                                <thead class="bck-ibermatica">
                                    <tr>
                                        <th>Archivos</th>
                                    </tr>
                                </thead>
                                <tbody id="tblDocPotencial">
                                    
                                </tbody>
                            </table>
                        </div>

                    </div>

                </fieldset>

                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="Ul1">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">                    
                    <b><button id="btnCancelarModalPotencial" class="btn btn-default">Cerrar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL DETALLE POTENCIAL-->



      <!--MODAL POTENCIAL-->
    <div class="modal fade" id="modal-YO">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Datos complementarios</h4>
                </div>
                <div class="modal-body">

                   <fieldset class="fieldset">
                    <legend class="fieldset">yo@enibermatica</legend>
                         <br />              
                    <div id="divYO">                      
                        
                        <!-- Archivos potencial -->
                        <div class="row">
                            <table  class="table table-striped table-hover">
                                <thead class="bck-ibermatica">
                                    <tr>
                                        <th>Archivo</th>
                                    </tr>
                                </thead>
                                <tbody id="tblDocYO">
                                    
                                </tbody>
                            </table>
                        </div>

                    </div>

                </fieldset>

                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="Ul2">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">                    
                    <b><button id="btnCancelarModalYO" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL DETALLE POTENCIAL-->




     <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de evaluador/a</h4>
                </div>
                <div class="modal-body">

                    <div id="txtBusqueda" class="row">
                        <div class="col-xs-3">
                             <label for="lblApellido1">Apellido 1º</label>
                                <input id="inputApellido1" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label>
                                <input id="inputApellido2" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label>
                            <input id="txtNombre" type="text" />
                        </div>
                        <div class="col-xs-1 col-xs-offset-1">
                            <button id="btnObtener" class="btn btn-primary btn-xs">Obtener</button>
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
                    <b><button id="btnSeleccionar" class="btn btn-primary">Seleccionar</button></b>
                    <b><button id="btnCancelar" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADORES-->


    <div class="container">
        <div class="row">           
            <div class="col-xs-12">

                
                <div id="divEvaluador" runat="server" class="col-xs-8 hide">
                    <button id="lblEvaluador" class="btn btn-link">Profesional</button>
                    <input id="evaluador" type="text" readonly="readonly" />
                </div>
                
            </div>
        </div>
        <div class="row">
             <div class="col-xs-12">
                 <button id="btnExportExcelALL" class="btn btn-primary btn-xs pull-right" style="margin-bottom: 5px; margin-left:10px;">Exportar a Excel datos de todo el subárbol dependiente</button>
                <button id="btnExportExcel" class="btn btn-primary btn-xs pull-right" style="margin-bottom: 5px">Exportar a Excel datos de pantalla</button>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>

    
    <!-- Tabla evaluadores -->
    <div class="container">
        <div class="row">
            <div style="position:relative" class="col-xs-12">


                <div id="divCabecera" class="bck-ibermatica">

                    <span></span>
                    <span></span>
                    <span id="headerEvaluador"></span>
                    <span  id="headerEvaluado">Evaluados/as  (<span  id="spanNumero"></span>)</span>
                    <span id="headerRol"></span>

                </div>


                <div class="table-responsive">

                    <table id="tablaEvaluador" class="table table-striped table-hover header-fixed">
                        <thead class="bck-ibermatica">
                         
                        </thead>
                        <tbody id="tblEvaluador">
                          
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

   
    <!-- Tabla  evaluados -->
    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div class="row">
                    <%--<div class="col-xs-6">
                        <b><span style="display: inline-block; margin-top: 14px">Evaluados</span></b>
                    </div>--%>
                </div>
                <div id="evaluadoresTabla" class="table-responsive">
                    <table id="tablaEvaluado" class="table table-striped header-fixed">
                       
                        <tbody id="tblEvaluado">
                         
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <br />

    <!--Leyendas Potencial -->
    <div class="container">
        <div class="row">
            <div style="margin-top:-12px" class="col-xs-12">

                <%--<div id="leyPotencial" class="hide">
                <i class='fa fa-diamond'></i>
                <span class="MR" >Es potencial</span>
                </div>

                <div id="leyPruebasIdentificacion" class="hide">
                    <i class='fa fa-pencil-square-o'></i>
                <span class="MR">Pruebas de identificación de potencial realizadas</span>
                </div>

                <div id="leyConvocadoPruebas" class="hide">
                     <i class='fa fa-pencil-square-o text-danger'></i>
                <span class="MR">Fue convocado a pruebas de potencial y no las realizó</span>
                </div>

                <div id="leyYoIb" class="hide">
                     <i class='fa fa-at'></i>
                <span>yo@enibermatica realizado</span>
                </div>--%>

                <div class="col-xs-6">
                     <div>
                    <i class='glyphicon glyphicon-arrow-up'></i>
                    <span>Ascender en el árbol de dependencias</span>
                </div>

                <div>
                    <i class='glyphicon glyphicon-arrow-down'></i>
                    <span>Descender en el árbol de dependencias</span>
                </div>

                <div id="leyPotencial">
                    <i class='fa fa-diamond'></i>
                    <span class="MR">Es potencial</span>
                </div>

               
                </div>

                 <div id="leyPruebasIdentificacion">
                    <i class='fa fa-pencil-square-o'></i>
                    <span class="MR">Pruebas de identificación de potencial</span>
                </div>
               <%-- <div class="col-xs-6">
                     <div id="leyConvocadoPruebas">
                    <i class='fa fa-pencil-square-o text-danger'></i>
                    <span class="MR">Fue convocado a pruebas de potencial y no las realizó</span>
                </div>--%>

                <div id="leyYoIb">
                    <i class='fa fa-at'></i>
                    <span>yo@enibermatica realizado</span>
                </div>

                <div>
                    <i class="fa fa-check"></i>
                    <span>Pertenece al colectivo Progress</span>
                </div>
                </div>

               




               
               
            </div>            
        </div>
        
    </div>



    <!--Leyendas Yo@enIbermática -->

    <!-- Prueba Caja leyendas -->
    <%--	<div class="col-xs-5 col-xs-offset-1">
			<div class="box">							
				<div class="icon">
					<div class="image"><i class="fa fa-thumbs-o-up"></i></div>
					<div class="info">
						<h3 class="title">Potencial</h3>
						<p>
							Iconos y leyendas Potencial
						</p>
						
					</div>
				</div>
				
			</div> 
		</div>--%>

    

    <%--<div class="container">
        <div class="row">
            <div class="col-xs-10 col-xs-offset-1">
                <label>Nota:</label>
                <span>Los profesionales sin >>ICONO<< quedan fuera del colectivo Progress (Atención a Usuarios).</span>
            </div>
        </div>
    </div>--%>


    
   
 <!-- iframe descarga de documentos -->
    <iframe id="ifrmDownloadDoc" name="ifrmDownloadDoc" scrolling='no' marginheight="0" marginwidth="0" frameborder="0" style="display: none;"></iframe>
    <iframe width="0" height="0" id="ifrmExportExcel" style="display:none">
    </iframe>

</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>

<script type="text/javascript" src="../../../js/moment.locale.min.js"></script>