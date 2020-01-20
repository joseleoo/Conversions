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

$('.popover-dismiss').popover({
    trigger: 'focus'
});

function copy() {
    /* Get the text field */
    var copyText = document.getElementById("inputUrl");

    /* Select the text field */
    copyText.select();
    copyText.setSelectionRange(0, 99999); /*For mobile devices*/

    /* Copy the text inside the text field */
    document.execCommand("copy");
    $('#copied').show();
    $('#copied').fadeOut(2000);
}

