$(document).ready(function () {

    function buscaDatos(numeroDePagina) {
        var urlAPI = 'https://api.themoviedb.org/3/discover/movie?api_key=e8c6d35a6bd555573d4b93aff5b6743b';
        urlAPI += '&page=' + numeroDePagina;
        //console.log(urlAPI);

        $.get(urlAPI, function (respuesta, estado) {
            $('#resultados').html('');
            // COMPRUEBO EL ESTADO DE LA LLAMADA
            if (estado === 'success') {
                // SI LLEGO HASTA AQUÍ QUIERE DECIR
                // QUE EN 'RESPUESTA' TENGO LA INFO
                $('#pagina-actual').html(respuesta.page);
                $('#total-paginas').html(respuesta.total_pages);

                var relleno = '';

                $.each(respuesta.results, function (indice, elemento) {

                    var rutaPoster = 'https://image.tmdb.org/t/p/w500' + elemento.poster_path;

                    //rutaPoster += '?d=' + new Date();

                    relleno = '<div class="item-pelicula">';
                    relleno += '    <div class="contenido-pelicula">';
                    relleno += '        <img class="imagen-pelicula" src="' + rutaPoster + '" alt="" />';
                    relleno += '    </div>';
                    relleno += '    <div class="datos-pelicula">';
                    relleno += '        <div class="cabecera">';
                    relleno += '            <span>' + elemento.title + '</span>';
                    relleno += '        </div>';
                    relleno += '        <div class="votos">' + elemento.vote_average + ' <i class="fa fa-star" aria-hidden="true"></i></div>';
                    relleno += '        <div class="clear">' + elemento.overview.substring(0, 400) + '</div>';
                    relleno += '        <div class="contenido">';
                    relleno += '        </div>';
                    relleno += '    </div>';
                    relleno += '</div>';

                    $('#resultados').append(relleno);
                });

                // preparo efecto para la imagen
                //$('img').fadeIn('slow');
                $('.item-pelicula').fadeIn('slow');

            }
        });
    }

    $('.fa-arrow-circle-right').click(function () {
        var paginaActual = parseInt($('#pagina-actual').html());
        if (paginaActual > 0) {
            $('#flecha-izda').removeClass('oculto');
        }
        else {
            $('#flecha-izda').addClass('oculto');
        }
        buscaDatos(paginaActual + 1);
    });

    $('.fa-arrow-circle-left').click(function () {
        var paginaActual = parseInt($('#pagina-actual').html());
        if (paginaActual === 2) {
            $('#flecha-izda').addClass('oculto');
        }
        else {
            $('#flecha-izda').removeClass('oculto');            
        }
        buscaDatos(paginaActual - 1);
    });

    // SE EJECUTA LA PRIMERA VEZ
    buscaDatos(1);
});
