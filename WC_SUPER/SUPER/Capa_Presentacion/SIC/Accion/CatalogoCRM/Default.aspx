<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Accion_CatalogoCRM_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/cabeceraPreventa.ascx" TagPrefix="uc1" TagName="cabeceraPreventa" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Acciones preventa solicitadas para la Oportunidad, Extensión u Objetivo</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />

</head>
<body>
    <uc1:cabeceraPreventa runat="server" ID="cabeceraPreventa" />
    <form class="form-horizontal" id="frmAcciones" runat="server">

        <div class="container-fluid">
            <div class="row">
                <div>
                    <div class="ibox-content blockquote blockquote-info">

                        <div class="form-group">
                            <label class="col-sm-1 control-label" for="txtIditemorigen_cab" id="lblItemorigen_cab" runat="server">Oportunidad</label>
                            <div class="col-sm-1">
                                <input type="text" id="txtIditemorigen_cab" runat="server" readonly="readonly" class="form-control text-right" style="width: 90px;" />
                            </div>
                            <div class="col-sm-10">
                                <input type="text" id="txtDenominacion_cab" readonly="readonly" class="form-control" value="" />
                            </div>
                        </div>


                        <div id="divOportExt" class="form-group hide" runat="server">
                            <label class="col-sm-1 control-label" for="txtlblOportExt_cab" id="lblOportExt" runat="server">Oportunidad</label>
                            <div class="col-sm-1">
                                <input type="text" id="txtOportExt" runat="server" readonly="readonly" class="form-control text-right" style="width: 90px;" />
                            </div>
                            <div class="col-sm-10">
                                <input type="text" id="txtdenOportExt_cab" readonly="readonly" class="form-control" value="" />
                            </div>
                        </div>


                        <div id="div_txtCuenta_cab" class="form-group MB0" runat="server">

                            <label class="col-sm-1 control-label" for="txtCuenta_cab">Cuenta</label>
                            <div class="col-sm-11">
                                <input type="text" id="txtCuenta_cab" readonly="readonly" class="form-control" value="" />
                            </div>

                        </div>

                    </div>

                </div>

            </div>

            <div class="row">

                <div class="ibox-title">
                    <h5 id="h5Title" class="text-primary"></h5>  
                    <a runat="server" id="btnInsert" style="display:none" class="btn btn-primary btn-xs">Añadir nueva acción</a>
                                                              
                    <h5 class="pull-right"><a class="underline" href ="../../Guias/A_Catalogo_de_acciones_para_ON_Ext_Obj.pdf" target="_blank">Guía</a></h5>
                    <h5 class="pull-right"><a id="linkImputaciones" class="underline"> Imputaciones registradas</a></h5>

                </div>
                <div class="ibox-content  blockquote blockquote-info">
                    <div class="col-xs-12 table-responsive">
                        <table id="tblAcciones" class="table table-bordered table-condensed">
                            <thead class="well text-primary">
                                <tr>
                                    <th class="text-center">Ref.</th>
                                    <th class="text-center">Acción preventa solicitada</th>
                                    <th class="text-center">Unidad</th>
                                    <th class="text-center">Área</th>
                                    <th class="text-center">Subárea</th>
                                    <th class="text-center">Líder</th>
                                    <th class="text-center">Solicitante</th>
                                    <th class="text-center">Estado</th>
                                    <th class="text-center">Creación</th>
                                    <th class="text-center">Fin requerido</th>
                                    <th class="text-center">Fin./Anul.</th>                                                                                                            
                                </tr>
                            </thead>
                        </table>

                    </div>

                    <div class="clearfix"></div>
                   
                </div>

            </div>
            <br />

        </div>

    </form>

    <!-- Modal imputaciones registradas -->
    <div class="modal fade" id="modal-imputaciones">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Imputaciones registradas</h4>
                </div>
                <div class="modal-body">            
                    <div class="well">
                        <label>Jornadas:</label>
                        <span id="jornadas"></span>
                        <br />
                        <label>Euros:</label>
                        <span id="euros"></span>
                    </div>
                </div>
                <div class="modal-footer">
                    <a data-dismiss="modal" class="btn btn-primary">Cerrar</a>                    
                </div>
            </div>
        </div>
    </div>

    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../plugins/datatables/dataTables.fixedColumns.min.js"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/accounting.min.js"></script>
    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180315"></script>

</body>
</html>
