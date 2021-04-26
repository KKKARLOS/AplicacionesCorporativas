<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Participante_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />
    <title>Mis tareas preventa en curso</title>

    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />
</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />

   


    <!-- meter paddingtop al container -->
    <form id="form1" runat="server"></form>
    <form class="form-horizontal">
        <section>
            <div class="container-fluid">            
                <div class="row">
                    <div class="ibox-title">
                        <h5 class="text-primary">Mis tareas preventa en curso</h5>
                    </div>
                    <div class="ibox-content  blockquote blockquote-info">
                        <div class="table-responsive" style="overflow-y:hidden">
                            <table id="tblTareas" class="table table-bordered table-condensed">
                                <thead class="well text-primary cabeceraTabla">
                                    <tr>
                                        <th class="text-center">Ref.</th>
                                        <th class="text-center">Tarea</th>       
                                        <th class="text-center">Participantes</th>       
                                        <th class="text-center">Acción preventa</th>                                        
                                        <th title="Oportunidad, extensión, objetivo o solicitud preventa interna para la que se ha solicitado la acción preventa" class="text-center">Origen</th>       
                                        <th class="text-center">Cuenta</th>  
                                        <th class="text-center">Solicitante de la acción</th>                                               
                                        <th class="text-center">Área</th>                                        
                                        <th class="text-center">Subárea</th>                                        
                                        <th class="text-center">Líder</th>       
                                        <th class="text-center">Creación</th>                                                                          
                                        <th class="text-center">Fin previsto</th>                                                                                                                        
                                        <%--<th class="text-center">Unidad</th>--%>
                                    </tr>
                                </thead>

                            </table>

                        </div>

                        <div class="clearfix"></div>
                    </div>

                </div>
            </div>


        </section>
    </form>

    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../plugins/datatables/dataTables.fixedColumns.min.js"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/accounting.min.js"></script>
    <script src="js/view.js"></script>
    <script src="js/app.js"></script>

    </body>

    </html>


