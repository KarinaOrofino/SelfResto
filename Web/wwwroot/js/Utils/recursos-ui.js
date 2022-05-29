var jsglobals = null;
var Recursos = null;

// -- definicion de objeto
function RecursosUi() {    
    this.url = '/Home/JSGlobales';
    this.urlObtenerVersion = '/Home/ObtenerVersion';
    this.KEYDB = 'globales';
    this.KEYVERSION = 'VERSION';    
}

$(document).ready(function () {

    try {

        Recursos = new RecursosUi();
        var myself = this;

        $.ajax({
            url: Recursos.urlObtenerVersion,
            error: function () { myself.LoadingGlobalsDefault(); },
            async: false,
            cache: false,
            type: "GET",
            success: function (data) {

                try {
                    Recursos.LoadingGlobalsDefault();

                    //Si el numero de version se modifico, se recargan los Recursos
                    if (data.content != localStorage.getItem(Recursos.KEYVERSION) ||
                        (typeof jsglobals == 'undefined') ||
                        localStorage.getItem(Recursos.KEYVERSION) == null ||
                        localStorage.getItem(Recursos.KEYDB) == null) {
                        Recursos.CargarGlobales();
                        localStorage.setItem(Recursos.KEYVERSION, data.content);
                    } else {
                        //Sino, se utilizan los actuales
                        Recursos.RecuperarGlobalesLocales();
                    }

                    vueApp.jsglobals = jsglobals;
                    
                } catch (e) {
                    // Se cargan los defaults
                    console.log(e);
                    Recursos.LoadingGlobalsDefault();
                }
            }

        });
    }
    catch (ex) {
        alert("Ha ocurrido un error inesperado cargando datos globales del sistema. Elimine el cache del explorador"
            + " y reintente nuevamente. Si el problema persiste; contáctese con su Administrador.");
    };
});

RecursosUi.prototype.RecuperarGlobalesLocales = function () {

    var myself = this;
    var auxiliar = null;

    if (localStorage) {
        // LocalStorage is supported!
        try {
            auxiliar = JSON.parse(localStorage.getItem(myself.KEYDB));
        } catch (e) {
            console.log(e);
        }

        if (auxiliar == null || auxiliar.length == 0) {
            return false;
        } else {
            jsglobals = auxiliar;
            return true;
        }

    } else {
        // No support. Use a fallback such as browser cookies or store on the server.
        return false;
    }
}

RecursosUi.prototype.CargarGlobales = function () {

    var myself = this;

    $.ajax({
        url: myself.url,
        type: "GET",
        error: function () { myself.LoadingGlobalsDefault(); },
        async: false,
        cache: true,
        success: function (data) {
            if (data.result == 0) {
                try {

                    jsonResources = {};
                    for (var i = 0; i < data.content.length; i++) {
                        jsonResources[data.content[i].key] = data.content[i].value;
                    }

                    jsglobals = jsonResources;                    

                    if (localStorage) {
                        localStorage.setItem(myself.KEYDB, JSON.stringify(jsonResources));
                    }

                } catch (e) {
                    console.log(e);
                }
            } else {
                console.log("Error cargando globales :" + data.content.mensaje);
            }
        }
    });
}

RecursosUi.prototype.LimpiarLocalStorage = function () {

    /*Se limpian todas las claves de los resources, actualmente se encuentran 
    'globales' para la aplicacion y 'globalesAngular' para las pantallas que 
    utilicen angular*/

    if (localStorage)
        localStorage.clear();

};

RecursosUi.prototype.LoadingGlobalsDefault = function () {
    var myself = this;
    // para manejar los acentos se deben utilizar los codigos unicode...
    // h t t p: //0xcc.net/jsescape/
    // h t t p: //www.zonaw.com/caracteres-unicode-para-javascript/
    // cargo los defaults
    jsglobals = {
        "ErrorGenerico": "Ha ocurrido un error en el proceso. Por favor, comuníquese con el area de sistemas."
    };
}