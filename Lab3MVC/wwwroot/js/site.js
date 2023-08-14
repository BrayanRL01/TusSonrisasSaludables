// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {



});

function Add() {

    var student = {
        name: $('#name').val(),
        email: $('#email').val(),
        password: $('#password').val()

    };

    $.ajax({
        url: "/Home/Insert",//Vaya al controlador llamado home
        data: JSON.stringify(student), //converte la variable estudiante en tipo json
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //aca recibo el resultado del backend (datos,objetos,mensajes)
        },
        error: function (errorMessage) {
            alert("Error");
            alert(errorMessage.responseText);
        }
    });
}


