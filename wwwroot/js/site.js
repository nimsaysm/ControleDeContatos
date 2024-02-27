﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//iniciar o uso do plugin DataTable de busca e paginação de tabelas
$(document).ready(function () {
    getDataTable('#table-contatos');
    getDataTable('#table-usuarios');

    $('#btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');
        
        //requisição via JS (ajax) na Controller para solicitar todos os contatos e mostrar na tela
        $.ajax({
            //método Get na Controller Usuario no método de Listar Usuario através do Id 
            type: 'GET',
            url: '/Usuario/ListarContatosPorUsuarioId/' + usuarioId, 
            success: function(result){
                $('#listaContatosUsuario').html(result);              
                //parte de uma View -> guarda uma parte do HTML
                $('#modalContatosUsuario').modal('toggle');
                getDataTable('#table-contatos-usuario');
          }});         
    });

    $('#btn-modal').click(function () {
        $('#modalContatosUsuario').modal('toggle');
    })
});

function getDataTable(idTabela) {
    $(idTabela).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
        "sEmptyTable": "Nenhum registro encontrado na tabela",
        "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
        "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
        "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
        "sInfoPostFix": "",
        "sInfoThousands": ".",
        "sLengthMenu": "Mostrar _MENU_ registros por pagina",
        "sLoadingRecords": "Carregando...",
        "sProcessing": "Processando...",
        "sZeroRecords": "Nenhum registro encontrado",
        "sSearch": "Pesquisar",
        "oPaginate": {
            "sNext": "Proximo",
            "sPrevious": "Anterior",
            "sFirst": "Primeiro",
            "sLast": "Ultimo"
        },
        "oAria": {
            "sSortAscending": ": Ordenar colunas de forma ascendente",
            "sSortDescending": ": Ordenar colunas de forma descendente"
        }
        }
    });
}


//quando clicar no close-alert
$('.close-alert').click(function () {
    $('.alert').hide('hide');
});