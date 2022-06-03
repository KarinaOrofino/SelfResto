//Model
vueAppParams.data.gridData = [];
vueAppParams.data.listadoFiltrado = [];
vueAppParams.data.loadingPacientes = true;
vueAppParams.data.loadingExportar = false;

vueAppParams.data.search = '';

vueAppParams.data.headers = [

    { text: jsglobals.Id, align: ' d-none' },
    { text: jsglobals.Nombre, value: 'nombre', align: 'center', class: 'protevac-headers' },
    { text: jsglobals.Apellido, value: 'apellido', align: 'center text-uppercase', class: 'protevac-headers' },
    { text: jsglobals.ObraSocial, value: 'obraSocial', align: 'center text-uppercase', class: 'protevac-headers' },
    { text: jsglobals.NumeroObraSocial, value: 'numeroObraSocial', align: 'center text-uppercase', class: 'protevac-headers' },
    { text: jsglobals.FechaNacimiento, value: 'fechaNacimientoString', align: 'center text-uppercase', class: 'protevac-headers' },
    { text: jsglobals.Acciones, value: 'acciones', align: 'center text-uppercase', class: 'protevac-headers' }

];

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Pacientes, disabled: true },
    { text: jsglobals.Listado, disabled: true }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};

// Metodos


vueAppParams.methods.loadGrid = function () {

    $.ajax({
        url: "/Pacientes/ObtenerTodos",
        method: "GET",
        success: function (data) {
            vueApp.gridData = data.content;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.loadingPacientes = false;
        }
    }).done(() => {
        vueApp.loadingPacientes = false;
    });
};

// Metodos
vueAppParams.methods.agregarPaciente = function (event) {
    window.location = "Detalle/";
};


vueAppParams.methods.editarPaciente = function (id) {

    window.location = "/Pacientes/Detalle/?id=" + id;
};


vueAppParams.methods.exportarLista = function () {
    vueApp.loadingExportar = true;

    return new Promise(resolve => {

        const term = this.search.toLowerCase();

        var urlToSend = "/Pacientes/Exportar?campoBusqueda=" + term;

        var req = new XMLHttpRequest();
        req.open("GET", urlToSend, true);
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
            var fileName = "ReportePacientes_" + fechaLocalSlash;
            var link = document.createElement("a");
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;
            link.click();
            vueApp.loadingExportar = false;
        };

        req.send()

        resolve(req.status);
    });
};
