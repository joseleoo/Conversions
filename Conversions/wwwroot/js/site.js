// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    //E 10 segundos (10000 milisegundos) se ejecutará la función refrescar
    setTimeout(refrescar, 10000);
});
function refrescar() {
    //Actualiza la página
    location.reload();
}