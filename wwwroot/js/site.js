// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const input = document.getElementById("input");
        let texto = "";


        function llenar(valor){
            console.log(valor);
            texto += valor;
            input.value = texto;
        }

        function remover(){
            texto = texto.slice(0, -1);
            input.value = texto;
        }

        function Limpiar(){
            texto = "";
            input.value = texto;
        }