//Model
vueAppParams.data.gridData = [];
vueAppParams.data.listadoFiltrado = [];
vueAppParams.data.listaParaExportar = [];
vueAppParams.data.loadingAplicaciones = true;
vueAppParams.data.loadingExportar = false;
vueAppParams.data.expanded = [];
vueAppParams.data.search = '';

vueAppParams.data.headers = [

    { value: 'id', align: ' d-none' },
    { text: jsglobals.Fecha, value: 'fechaString', align: '', class: 'protevac-headers' },
    { text: jsglobals.Paciente, value: 'nombrePaciente', align: '', class: 'protevac-headers upper' },
    { text: jsglobals.Medico, value: 'nombreMedico', align: '', class: 'protevac-headers' },
    { text: jsglobals.Vacuna, value: 'listaDetalles', align: ' d-none', class: 'protevac-headers' },
    { text: jsglobals.Acciones, value: 'acciones', align: 'center text-uppercase', class: 'protevac-headers' }

];

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Aplicaciones, disabled: true },
    { text: jsglobals.Listado, disabled: true }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};

vueAppParams.methods.loadGrid = function () {

    $.ajax({
        url: "/Aplicaciones/ObtenerTodas",
        method: "GET",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.listadoFiltrado = vueApp.gridData;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.loadingAplicaciones = false;
        }
    }).done(() => {
        vueApp.loadingAplicaciones = false;
    });
};

vueAppParams.methods.colapsar = function () {

    vueAppParams.data.expanded = [];

};

vueAppParams.methods.agregarAplicacion= function (event) {
    window.location = "Detalle/";
};

vueAppParams.methods.eliminarAplicacion = function (id) {

    $.ajax({
        url: "/Aplicaciones/Eliminar/?idAplicacion=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosInactivadosOk);
            setTimeout(function () { window.location.reload() });
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.exportarAplicaciones = function () {
    vueApp.loadingExportar = true;

    return new Promise(resolve => {

        const term = this.search.toLowerCase();

        var urlToSend = "/Aplicaciones/Exportar?campoBusqueda=" + term;
        var req = new XMLHttpRequest();
        req.open("GET", urlToSend);
        req.responseType = "blob";
        req.onload = function (event) {
            var blob = req.response;
            if (req.status == HTTP_ERROR) {
                vueApp.notification.showError(jsglobals.ErrorGenerico);
                return;
            }
            var fecha = new Date();
            var fechaLocal = fecha.toLocaleDateString();
            var fechaLocalSlash = fechaLocal.replaceAll("/", "-")
            var fileName = "ReporteAplicaciones_" + fechaLocalSlash;
            var link = document.createElement("a");
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;
            link.click();
            vueApp.loadingExportar = false;
        };

        req.send();

        resolve(req.status);
    });
};
