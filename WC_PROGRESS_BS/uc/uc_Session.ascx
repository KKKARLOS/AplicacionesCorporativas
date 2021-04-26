<%@ Control Language="c#" Inherits="uc_Session" CodeFile="uc_Session.ascx.cs" %>
 
<script src="<% =Session["strServer"].ToString() %>js/jquery.min.js"></script>  

<style type=text/css>
    #popupWin_Session {
        position:fixed;        
        left:25%;
        z-index:100000;
    }
</style>
	
<script type="text/javascript">

    var strServer = "<%=Session["strServer"].ToString() %>";

    function MostrarMensajeSession() {               
        $("#popupWin_Session").css("display", "block");
    }
    function CerrarMensajeSession() {        
        $("#popupWin_Session").css("display", "none");
    }
  
</script>


<script>
    $(document).ready(function () {
        $(".btn.btn-primary").on("click", function () {
            ReiniciarSession();
            CerrarMensajeSession();
        })

        $(".btn.btn-default").on("click", function () {            
            CerrarMensajeSession();
        })
    })
</script>

<div id="popupWin_Session" style="display:none">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header  btn-primary">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">Caducidad de sesión</h4>
			</div>
			<div class="modal-body">
				La sesión de PROGRESS va a caducar en breve.<br /><br />
                        ¿Deseas reiniciar el tiempo de la sesión?<br />
                        Si pulsas "No" podrías perder los datos pendientes de grabar.<br /><br />
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-primary">Sí</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">No</button>				
			</div>
		</div>
	</div>
</div>