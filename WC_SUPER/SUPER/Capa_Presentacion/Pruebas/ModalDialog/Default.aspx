<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_ModalDialog_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
      <h1>showModalDialog polyfill demo</h1>
      <form action="">
	<p>
	  <input id="button2" type="button" value="Show Modal Dialog using eval">
	  <input id="button3" type="button" value="Muestra jConfirm">
	</p>
      </form>
      <script src="showModalDialog.js"></script>
      <script>
          function esperar() {
              var deferred = $.Deferred();
              setTimeout(function () { deferred.resolve(); }, 50);
              return deferred.promise();
          }

          function otroConfirm() {
              jqConfirm("", "Segundo confirm. ¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
                  if (answer)
                      alert("Has pulsado Aceptar el 2º confirm");
                  else
                      alert("Has pulsado Cancelar el 2º confirm");
              });
          }
          if (typeof document.attachEvent != 'undefined') {
//              alert("xxx");
//              document.getElementById('button2').attachEvent("click", function() {
//                  alert("llega");
//                  var ret = window.showModalDialog("demo-modal.html", self, "dialogWidth:500px;dialogHeight:200px");
//                  alert("Returned from modal: " + ret);
              //              });
              document.getElementById('button2').onclick = function () {
                  alert("llega");
                  var ret = window.showModalDialog("demo-modal.html", self, "dialogWidth:500px;dialogHeight:200px");
                  alert("Returned from modal: " + ret);
              };
              document.getElementById('button3').onclick = function () {
                  jqConfirm("", "Primer confirm. ¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
                      if (answer) {
                          //alert("Has pulsado Aceptar");
                          var promesa = esperar();
                          promesa.done(otroConfirm);
                      }
                      else
                          alert("Has pulsado Cancelar");
                  });
              };
          } else {
              document.getElementById('button2').addEventListener("click", function() {
                  var ret = window.showModalDialog("demo-modal.html", self, "dialogWidth:500px;dialogHeight:200px");
                  alert("Returned from modal: " + ret);
              });
          }
      </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

