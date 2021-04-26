<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Test_JqueryUploadFile_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />
    <%--<link href="uploadfile.css" rel="stylesheet" />--%>
    <%--<link href="uploadfile.custom.css" rel="stylesheet" />--%>


</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />

    <!-- meter paddingtop al container -->
    <form id="form1" runat="server">


        <div id="divDocumentacion"></div>

    <!-- 
    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalDocumentacion">Documentación</button>

    <div class='modal fade' id='modalDocumentacion'>
        <div class='modal-dialog modal-lg'>
            <div class='modal-content'>

                <div class='modal-header btn-primary'>
                    <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>
                    <h4 class='modal-title'>Documentación adjunta</h4>
                </div>
                <div class='modal-body'>

                                <div class="card well">
                                    <div class="card-block">
                                        <h4>
                                            <span class="card-title">Filename un poco largo.pdf</span>
                                            <span class="text-muted small">(650 KB)</span>
                                            <span><a href="#"><i class="fa fa-download fa-3" aria-hidden="true"></i></a></span>
                                            <span><a href="#"><i class="fa fa-pencil fa-3" aria-hidden="true"></i></a></span>
                                            <span><a href="#"><i class="fa fa-trash fa-3" aria-hidden="true"></i></a></span>
                                        </h4>
                                        <h6 class="card-subtitle text-muted">
                                            <span>Documento funcional</span>
                                            <span>15/07/2016 15:31</span>
                                            <span>Pedro García Artetxe</span>
                                        </h6>
                                        <p class="card-text">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                                    </div>
                                </div>
                                <div class="card well">
                                    <div class="card-block">
                                        <h4 class="card-title">Filename un poco largo.pdf
                                            <span><a href="#"><i class="fa fa-download fa-3" aria-hidden="true"></i></a></span>
                                            <span><a href="#"><i class="fa fa-pencil fa-3" aria-hidden="true"></i></a></span>
                                            <span><a href="#"><i class="fa fa-trash fa-3" aria-hidden="true"></i></a></span>
                                        </h4>
                                        <h6 class="card-subtitle text-muted">
                                            <span>650 KB</span>
                                            <span>15/07/2016 15:31</span>
                                            <span>Pedro García Artetxe</span>
                                        </h6>
                                        <p class="card-text">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                                    </div>
                                </div>
                                <div class="card well">
                                    <div class="card-block">
                                        <h4 class="card-title">Filename un poco largo.pdf
                                            <span><a href="#"><i class="fa fa-download fa-3" aria-hidden="true"></i></a></span>
                                            <span><a href="#"><i class="fa fa-pencil fa-3" aria-hidden="true"></i></a></span>
                                            <span><a href="#"><i class="fa fa-trash fa-3" aria-hidden="true"></i></a></span>
                                        </h4>
                                        <h6 class="card-subtitle text-muted">
                                            <span>650 KB</span>
                                            <span>15/07/2016 15:31</span>
                                            <span>Pedro García Artetxe</span>
                                        </h6>
                                        <p class="card-text">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                                    </div>
                                </div>

                </div>

                <div class='modal-footer clear'>
                    <button type='button' class='btn btn-default' data-dismiss='modal'>Cerrar</button>
                </div>

            </div>
        </div>
    </div>


        -->

<%--    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalDocumentacionEdicionlocal">editar</button>
    <div class='modal fade' id='modalDocumentacionEdicionlocal'>
        <div class='modal-dialog'>
            <div class='modal-content'>

                <div class='modal-header btn-primary'>
                    <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>
                    <h4 class='modal-title'>Editar documento</h4>
                </div>
                <div class='modal-body'>
                    <div class="row">
                        <div class="form-group MB0">
                            <label class="col-sm-2 control-label" for="mde-documento">Documento</label>
                            <div class="col-sm-10">
                                <input type="text" id="mde-documento" class="form-control" value="" disabled="">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group MB0">
                            <label class="col-sm-2 control-label" for="mde-autor">Autor</label>
                            <div class="col-sm-10">
                                <input type="text" id="mde-autor" class="form-control" value="" disabled="">
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="form-group MB0">
                            <label class="col-sm-2 control-label" for="fileuploaderplugin">Actualizar documento</label>
                            <div class="col-sm-10">
                                <div id="fileuploaderplugin">Upload</div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="form-group MB0">
                            <label class="col-sm-2 control-label" for="mde-tipodocumento">Tipo</label>
                            <div class="col-sm-5">
                                <select id="mde-tipodocumento" class="form-control"></select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group MB0">
                            <label class="col-sm-2 control-label" for="mde-descripcion">Descripción</label>
                            <div class="col-sm-10">
                                <textarea id="mde-descripcion" class="form-control" rows="5" ></textarea>
                            </div>
                        </div>
                    </div>
                </div>

                <div class='modal-footer clear'>
                    <button type='button' class='btn btn-primary fk_mde-btnGrabar' data-dismiss='modal'>Grabar</button>
                    <button type='button' class='btn btn-default' data-dismiss='modal'>Cancelar</button>
                </div>

            </div>
        </div>
    </div>--%>


        <button type="button" id="btn1">Tarea 8 (acción 4), Edicion</button>
        <button type="button" id="btn2">Tarea 8 (acción 4), Consulta</button>
        <br />
        <button type="button" id="btn3">Accion 4, Edicion</button>
        <button type="button" id="btn4">Accion 4, Consulta</button>

        <br /><br /><br />

        <button type="button" id="btn11">Tarea 8 (acción 4), Count</button>
        <br />
        <button type="button" id="btn13">Accion 4, Count</button>
        <br />

        <br /><br /><br />

        <button type="button" id="btn21">Tarea GUID</button>
        <br />
        <button type="button" id="btn22">Accion GUID</button>
        <br />


   
    </form>
    <!--

    

        -->
    <script src="../../../../scripts/string.js"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <%--<script src="jquery.uploadfile.js"></script>--%>

    <script src="../../Documentos/modelsDocumento.js"></script>
    <script src="../../Documentos/modelsDocumentoList.js"></script>
    <script src="../../Documentos/viewDocumentos.js"></script>
    <script src="../../Documentos/appDocumentos.js"></script>

    <%--<script src="../../Documentos/Documentos.min.js"></script> --%>

    <script src="app.js"></script>

    <iframe id="fileuploader-ifrmDownloadDoc" scrolling='no' marginheight="0" marginwidth="0" frameborder="0" style="display: none;"></iframe>

    <script>

        //$('.modal').on('hidden.bs.modal', function (event) {
        //    $(this).removeClass('fv-modal-stack');
        //    $('body').data('fv_open_modals', $('body').data('fv_open_modals') - 1);
        //});


        //$('.modal').on('hidden.bs.modal', function (event) {

        //    // keep track of the number of open modals

        //    if (typeof ($('body').data('fv_open_modals')) == 'undefined') {
        //        $('body').data('fv_open_modals', 0);
        //    }


        //    // if the z-index of this modal has been set, ignore.

        //    if ($(this).hasClass('fv-modal-stack')) {
        //        return;
        //    }

        //    $(this).addClass('fv-modal-stack');

        //    $('body').data('fv_open_modals', $('body').data('fv_open_modals') + 1);

        //    $(this).css('z-index', 1040 + (10 * $('body').data('fv_open_modals')));

        //    $('.modal-backdrop').not('.fv-modal-stack')
        //            .css('z-index', 1039 + (10 * $('body').data('fv_open_modals')));


        //    $('.modal-backdrop').not('fv-modal-stack')
        //            .addClass('fv-modal-stack');

        //});



    </script>

</body>

</html>




