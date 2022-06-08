﻿//Model
vueAppParams.data.gridData = [];
vueAppParams.data.listadoFiltrado = [];
vueAppParams.data.loadingMedicos= true;
vueAppParams.data.loadingExportar = false;
vueAppParams.data.dialogActivar = false;
vueAppParams.data.dialogInactivar = false;
vueAppParams.data.medicoAActivar = '';
vueAppParams.data.medicoAInactivar = '';
vueAppParams.data.verMedico = false;
vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.itemCambio = "";
vueAppParams.data.dialog2 = null;
vueAppParams.data.breadcrums = [];

vueAppParams.methods.isDisabled = function () {
    //return this.model.MedicoExistente;
};

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Medicos, disabled: true },
    { text: jsglobals.Listado, disabled: true }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};


vueAppParams.data.search = '';

vueAppParams.data.filtros = {

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
vueAppParams.methods.agregarMedico = function () {

    window.location = "/Medicos/Detalle";
};

vueAppParams.methods.inactivarMedico = function (item) {

    vueAppParams.data.dialogInactivar = true;
    vueAppParams.data.medicoAInactivar = item;
};

vueAppParams.methods.confirmaInactivar = function (matricula) {

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

vueAppParams.methods.activarMedico = function (item) {

    vueAppParams.data.dialogActivar = true;
    vueAppParams.data.medicoAActivar = item;
};

vueAppParams.methods.confirmaActivar = function (matricula) {

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

vueAppParams.methods.editarMedico = function (matricula) {

    window.location = "/Medicos/Detalle/?matricula=" + matricula;
  };



vueAppParams.methods.exportarLista = function () {
    vueApp.loadingExportar = true;

    return new Promise(resolve => {

        const term = this.search.toLowerCase();

        var urlToSend = "/Medicos/Exportar?campoBusqueda=" + term + "&estado=" + vueAppParams.data.filtros.estado;

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




