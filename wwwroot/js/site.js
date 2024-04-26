// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const input = document.getElementById("input");


        function llenar(valor){
            input.value += valor;
            console.log(input.value);
        }

        function remover(){
            input.value = input.value.slice(0, -1);
        }

        function Limpiar(){
            input.value = "";
        }