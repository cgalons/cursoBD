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
                debugger;
                $.each(respuesta.dataMarca, function (indice, elemento) {

                    relleno = '<ul>';
                    relleno += '    <li>';
                    relleno += elemento.denominacion;
                    relleno += '    </li>';
                    relleno += '</ul>';

                    $('#resultados').append(relleno);
                });
            }
        });
    }

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

});