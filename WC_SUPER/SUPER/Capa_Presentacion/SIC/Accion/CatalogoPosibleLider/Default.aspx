<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Accion_CatalogoPosibleLider_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Catálogo de acciones</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />


</head>
<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form class="form-horizontal" id="frmAcciones" runat="server">

        <div class="container-fluid">

            <div class="row">

                <div class="ibox-title">
                    <h5 class="text-primary inline">Catálogo de acciones preventa pendientes de autoasignación de líder</h5>
                </div>
                <div class="ibox-content  blockquote blockquote-info">
                    <div class="table-responsive" style="overflow-y:hidden">
                        <table id="tblAcciones" class="table table-bordered table-condensed">
                            <thead class="well text-primary">
                                <tr>
                                    <th class="text-center">Ref.</th>
                                    <th class="text-center">Acción preventa</th>
                                    <th title="Oportunidad, extensión, objetivo o solicitud preventa interna para la que se ha solicitado la acción preventa" class="text-center">Origen</th>
                                    <th class="text-center">Importe</th>
                                    <th class="text-center">Cuenta</th>
                                    <th class="text-center">Solicitante</th>
                                    <th class="text-center">Org. Comercial</th>
                                    <th class="text-center">Área</th>
                                    <th class="text-center">Subárea</th>                                    
                                    <th class="text-center">Creación</th>
                                    <th class="text-center">Fin requerido</th>
                                    
                                    
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

    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../plugins/datatables/dataTables.fixedColumns.min.js"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/accounting.min.js"></script>
    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180315"></script>

</body>
</html>
