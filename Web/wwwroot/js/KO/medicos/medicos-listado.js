//Model
vueAppParams.data.gridData = [];
vueAppParams.data.listadoFiltrado = [];
vueAppParams.data.loadingMedicos= true;
vueAppParams.data.loadingExportar = false;
vueAppParams.data.dialogActivar = false;
vueAppParams.data.dialogInactivar = false;
vueAppParams.data.medicoAActivar = '';
vueAppParams.data.medicoAInactivar = '';

vueAppParams.data.search = '';
vueAppParams.data.filtros = {

    matricula: '',
    nombre: '',
    apellido: '',
    estado: '',

};
vueAppParams.data.filtros.estado = 0;

vueAppParams.data.headers = [

    { text: jsglobals.Matricula, value: 'matricula', align: 'center', class: 'protevac-headers' },
    { text: jsglobals.Nombre, value: 'nombre', align: 'center', class: 'protevac-headers'},
    { text: jsglobals.Apellido, value: 'apellido', align: 'center text-uppercase', class: 'protevac-headers' },
    { text: jsglobals.Estado, value: 'estado', align: 'center text-uppercase', class: 'protevac-headers' },
    { text: jsglobals.Acciones, value: 'acciones', align: 'center text-uppercase', class: 'protevac-headers' }

];

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Medicos, disabled: false, href: '/Medicos/Listado' },
    { text: jsglobals.Listado, disabled: true, href: '' }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};

// Metodos

vueAppParams.methods.filtrarPorEstado = function () {

    if (vueAppParams.data.filtros.estado === 0) {
        vueApp.listadoFiltrado = vueApp.gridData;
    }
    else { 
        vueApp.listadoFiltrado = vueApp.gridData.filter(m => m.estado == vueAppParams.data.filtros.estado);
    }
};


vueAppParams.methods.loadGrid = function () {

    $.ajax({
        url: "/Medicos/ObtenerTodos",
        method: "GET",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.listadoFiltrado = vueApp.gridData;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.loadingMedicos = false;
        }
    }).done(() => {
        vueApp.loadingMedicos = false;
    });
};

// Metodos
vueAppParams.methods.onClickNuevo = function (event) {
    window.location = "Detalle/";
};

vueAppParams.methods.onClickInactivar = function (item) {

    vueAppParams.data.dialogInactivar = true;
    vueAppParams.data.medicoAInactivar = item;
};

vueAppParams.methods.onClickConfirmaInactivar = function (matricula) {

    vueAppParams.data.dialogInactivar = false;

    var indiceMedicoAInactivar = vueApp.gridData.findIndex(med => med.matricula == matricula)
    vueApp.gridData[indiceMedicoAInactivar].estado = false;

    $.ajax({
        url: "/Medicos/Inactivar?matricula=" + matricula,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosInactivadosOk);
            setTimeout(function () { /*window.location = '/Medicos/Listado'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.onClickActivar = function (item) {

    vueAppParams.data.dialogActivar = true;
    vueAppParams.data.medicoAActivar = item;
};

vueAppParams.methods.onClickConfirmaActivar = function (matricula) {

    vueAppParams.data.dialogActivar = false;

    var indiceMedicoAActivar = vueApp.gridData.findIndex(med => med.matricula == matricula)
    vueApp.gridData[indiceMedicoAActivar].estado = true;

    $.ajax({
        url: "/Medicos/Activar?matricula=" + matricula,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosActivadosOk);
            setTimeout(function () { /*window.location = '/Medicos/Listado'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.onClickEditar = function (id) {

    window.location = "/Funcionalidad/Detalle/" + id;
};


vueAppParams.methods.onClickExportar = function () {
    vueApp.loadingExportar = true;

    var filters = "?matricula=" + vueApp.filtros.matricula
        + "&nombre=" + vueApp.filtros.nombre
        + "&apellido=" + vueApp.filtros.apellido
        + "&estado=" + vueApp.filtros.estado;

    return new Promise(resolve => {
        var urlToSend = "/Medicos/Exportar" + filters;

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
            var fileName = "ReporteMedicos_" + fechaLocalSlash;
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
