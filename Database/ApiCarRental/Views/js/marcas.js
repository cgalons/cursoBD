$(document).ready(function () {

    function GetMarcas() {
        var urlAPI = 'http://localhost:63387/api/marcas';

        $.get(urlAPI, function (respuesta, estado) {

            debugger;
            console.log(respuesta);
            $('#resultados').html('');
            // COMPRUEBO EL ESTADO DE LA LLAMADA
            if (estado === 'success') {
                // SI LLEGO HASTA AQUÍ QUIERE DECIR

                var relleno = '';

                relleno += '<table>';
                relleno += '<tr>';

                relleno += '      <td>Id.</td>'
                relleno += '      <td>Denominacion</td>'
                relleno += '      <td>Acciones</td>'
                relleno += '</tr>';

                $.each(respuesta.dataMarca, function (indice, elemento) {

                    relleno += '    <tr>';
                    relleno += '       <td>' + elemento.id + '</td>';
                    relleno += '       <td>' + elemento.denominacion + '</td>';
                    relleno += '      <td><button data-id=" ' + elemento.id + '" id ="btnEliminar">X</button>'
                    relleno += '      <button id="btnEditar">Editar</button></td>'
                    relleno += '   <tr>';
                   
                });

                debugger;
                //$.each(respuesta.dataMarca, function (indice, elemento) {

                //    relleno = '<ul>';
                //    relleno += '    <li>';
                //    relleno += elemento.denominacion;
                //    relleno += '    </li>';
                //    relleno += '</ul>';
                relleno += '</table>';
                $('#resultados').append(relleno);
                
            }
        });
    }

    $('#resultados').on('click', '#btnEliminar', function () {
        alert('Eliminando!!!!!!');
    });
   

    $('#btnAddMarca').click(function () {
        debugger;
        var nuevaMarca = $('#txtMarcaDenominacion').val();
        var urlAPI = 'http://localhost:63387/api/marcas';

        var dataNuevaMarca = {
            id: 0,
            denominacion: nuevaMarca
        };
        debugger;

        $.ajax({
            url: urlAPI,
            type: "POST",
            dataType: 'json',
            data: dataNuevaMarca,
            success: function (respuesta) {
                debugger;
                console.log(respuesta);
                GetMarcas();
            },
            error: function (respuesta) {
                console.log(respuesta);
            }
        });
    });

    GetMarcas();

    $.ajax({
        url: urlAPI + '/5', // OTRA FORMA DE LOCALHOST
        type: "PUT",
        dataType: 'json',
        data: dataNuevaMarca,
        success: function (respuesta) {
            GetMarcas();
        },
        error: function (respuesta) {
            console.log(respuesta);
        }
    });

    $('#resultados').on('click', '#btnEliminar', function () {
        var idMarca = $(this).attr('data-id');
        var urlAPI = 'http://localhost:52673/api/marcas';
        $.ajax({
            url: urlAPI + '/' + idMarca,
            type: "DELETE",
            success: function (respuesta) {
                GetMarcas();
            },
            error: function (respuesta) {
                console.log(respuesta);
            }
        });
    });
}) 