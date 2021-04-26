<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MiEquipo_DesgloseRol_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Desglose de Rol</title>

    <script>
        <%=idficepi%>
        <%=nombre%>
        <%=sexo%>
        <%=origen%>
    </script>
  
</head>
<body data-codigopantalla="302">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>    
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <form runat="server"></form>
     <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de evaluador/a</h4>
                </div>
                <div class="modal-body">
                    
                    <div id="txtBusqueda" class="">
                        <div class="pull-left">
                             <label for="lblApellido1">Apellido 1º</label><br />
                                <input id="inputApellido1" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label><br />
                                <input id="inputApellido2" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label><br />
                            <input id="txtNombre" type="text" />
                        </div>
                        <div class="pull-right">
                            <button id="btnObtener" class="btn btn-primary btn-xs hide">Obtener</button>
                        </div>
                    </div>
                       

                    <br /><br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluadores">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnSeleccionar" class="btn btn-primary" style="display:none">Seleccionar</button></b>
                    <b><button id="btnCancelar" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADORES-->



     <!--MODAL EVALUADORES MI EQUIPO-->
    <div class="modal fade" id="modal-evaluadoresMiequipo">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de evaluador/a</h4>
                </div>
                <div class="modal-body">
                                      
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluadoresMiequipo">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnSeleccionarMiequipo" class="btn btn-primary">Seleccionar</button></b>
                    <b><button id="btnCancelarMiequipo" class="btn btn-default">Cancelar</button></b>                    
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
                <fieldset class="fieldset">
                    <legend class="fieldset">Filtros</legend>
                   

                    <!--Contenido filtros-->
                    <div id="filtros">                      
                        <div class="row">                                                        
                             <div id="divEvaluador" runat="server" class="col-xs-7">
                                <%--<button id="lblEvaluador" class="btn btn-link quitarLabel">Evaluador</button>--%>
                                 <span id="lblEvaluador">Evaluador</span>
                                <input id="evaluador" type="text" readonly="readonly"/>
                            </div>

                            <div id="divProfundizacion" class="col-xs-5">
                                <span>Nivel de dependencia</span>
                                <select id="cboProfundizacion" runat="server" onchange="cargarDesgloseRol()">
                                    <option selected="selected" value="1">Primer nivel</option>
                                    <option value="2">Hasta segundo nivel</option>
                                    <option value="3">Todos los niveles</option>                                    
                                </select>    
                            </div>                                                      
                        </div>

                    </div>

                </fieldset>
            </div>
        </div>
    </div>


    <div class="container MT5">
        <div class="row">
            <div class="col-xs-10 col-xs-offset-1" style="margin-bottom:5px;">
                <img id="imgPlegarTodo" alt="Expandir a primer nivel" src="../../../imagenes/nivel1.png" />
                <img id="imgExpandirTodo" alt="Expandir todo" src="../../../imagenes/nivel2.png" />
                <button id="btnExportExcel" class="btn btn-primary btn-xs pull-right">Exportar a Excel</button>
            </div>            
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div id="divHeaderTabla" class="col-xs-10 header col-xs-offset-1">
                <span id="txtHeaderNumero" >Rol  (Nº de profesionales   <span id="spanSumaProfesionales"></span>)</span>
                <span id="txtHeaderProfesionales"  class="hide">Profesional</span>
            </div>
        </div>



        <div class="row">
            <div class="col-xs-10 col-xs-offset-1 bordeLista">

                <!-- Árbol -->
                <div class="tree">
                    <ul id="ulDesglose" style="padding-left: 0">
                    </ul>
                </div>
                <!-- FIN Árbol -->

            </div>
             
        </div>
    </div>

    <iframe width="0" height="0" id="ifrmExportExcel" style="display:none">

    </iframe>

</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>

 
