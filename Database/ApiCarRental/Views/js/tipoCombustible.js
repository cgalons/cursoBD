$(document).ready(function () {

    function GetTipoCombustible() {
        var urlAPI = 'http://localhost:63387/api/tipoCombustible';

        $.get(urlAPI, function (respuesta, estado) {

            debugger;
            console.log(respuesta);
            $('#resultados').html('');
            // COMPRUEBO EL ESTADO DE LA LLAMADA
            if (estado === 'success') {
                // SI LLEGO HASTA AQUÍ QUIERE DECIR

                var relleno = '';
                debugger;
                $.each(respuesta.dataTipoCombustible, function (indice, elemento) {

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

    $('#btnAddTipoCombustible').click(function () {
        debugger;
        var nuevoTP = $('#txtTPDenominacion').val();
        var urlAPI = 'http://localhost:63387/api/tipoCombustible';

        var dataNuevoTP = {
            id: 0,
            denominacion: nuevoTP
        };
        debugger;

        $.ajax({
            url: urlAPI,
            type: "POST",
            dataType: 'json',
            data: dataNuevoTP,
            success: function (respuesta) {
                debugger;
                console.log(respuesta);
                GetTipoCombustible();
            },
            error: function (respuesta) {
                console.log(respuesta);
            }
        });
    });

    GetTipoCombustible();

});