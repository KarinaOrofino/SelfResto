//CONSTANTES
var SELECCIONE = '';
const TODOS = "todos";

//Model
vueAppParams.data.gridData = [];
vueAppParams.data.menuFecha = false;
vueAppParams.data.mostrarFecha = true;
vueAppParams.data.loadingMedicos = true;
vueAppParams.data.loadingPacientes = true;
vueAppParams.data.loadingVacunas = true;
vueAppParams.data.loadingExportar = false;
vueAppParams.data.fechaSeleccionada = vueAppParams.methods.hoy();
vueAppParams.data.medico = {};
vueAppParams.data.paciente = {};
vueAppParams.data.vacuna = {};

vueAppParams.data.filtros = {

    fechaSeleccionada: vueAppParams.methods.formatoFecha(vueAppParams.data.fechaSeleccionada),
    medico: SELECCIONE,
    vacuna: SELECCIONE,
    paciente: SELECCIONE

};
//vueAppParams.data.medico = [];
//vueAppParams.data.vacuna = [];
//vueAppParams.data.paciente = [];

vueAppParams.data.headers = [

    { text: jsglobals.Fecha, value: 'fecha', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border', width: '110px' },
    { text: jsglobals.Paciente, value: 'paciente', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.Medico, value: 'medico', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border', width: '86px' },
    { text: jsglobals.Acciones, value: 'actions', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border' },

];

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Aplicaciones, disabled: true, href: '' }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};

vueAppParams.watch = {
    fechaSeleccionada(val) {
        vueApp.filtros.fechaSeleccionada = vueAppParams.methods.formatoFecha(vueApp.fechaSeleccionada);

    }
}

vueAppParams.methods.analizarFecha = function (fecha) {
    if (!fecha) return null

    const [day, month, year] = fecha.split('/');
    return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
}


vueAppParams.methods.limpiarFiltros = function (filterName) {

    if (filterName == TODOS) {
        vueAppParams.data.fechaSeleccionada = vueAppParams.methods.hoy();
        vueAppParams.data.mostrarFecha = true;
        vueAppParams.data.filtros.medico = SELECCIONE;
        vueAppParams.data.filtros.paciente = SELECCIONE;
        vueAppParams.data.filtros.vacuna = SELECCIONE;

    }
    else {
        if (filterName == 'medico' || filterName == 'paciente' || filterName == 'vacuna') {
            vueApp.filtros[filterName] = SELECCIONE;
        }
        else if (filterName == 'fechaSeleccionada') {
            vueAppParams.data.mostrarFecha = false;
        }
    }
    this.loadGrid();
};

vueAppParams.methods.filtrar = function () {

    this.loadGrid(false);

}

vueAppParams.methods.obtenerPacientes = function () {

    vueApp.loadingPacientes = true;

    $.ajax({
        url: "/Reportes/ObtenerPacientes",
        method: "GET",
        success: function (data) {
            vueApp.pacientes = data.content;
            vueApp.loadingPacientes = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingPacientes = false; });
}

vueAppParams.methods.obtenerMedicos = function () {

    vueApp.loadingMedicos = true;
    
    $.ajax({
        url: "/Reportes/ObtenerMedicos",
        method: "GET",
        success: function (data) {
            vueApp.medicos = data.content;
            vueApp.loadingMedicos = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingMedicos = false; });
}

vueAppParams.methods.obtenerVacunas = function () {

    vueApp.loadingVacunas = true;

    $.ajax({
        url: "/Reportes/ObtenerVacunas",
        method: "GET",
        success: function (data) {
            vueApp.vacunas = data.content;
            vueApp.loadingVacunas = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingVacunas = false; });
}


vueAppParams.methods.loadGrid = function (isDefault) {

    var filters = {};
    var fecha;

    vueApp.loadingAplicaciones = true;

    if (vueAppParams.data.mostrarFecha == true) {
        fecha = vueApp.filtros.fechaSeleccionada;
    }

    filters = { idMedico: vueApp.filtros.medico, idPaciente: vueApp.filtros.paciente, idVacuna: vueApp.filtros.idVacuna, fecha: fecha }

    $.ajax({
        url: "/Reportes/ObtenerVacunaciones",
        method: "POST",
        data: filters,
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.obtenerMedicos;
            vueApp.obtenerPacientes;
            vueApp.obtenerVacunas;
        },
        error: defaultErrorHandler
    }).done(() => {
        vueApp.loadingReportes = false;
    });
};


vueAppParams.methods.exportar = function () {

    vueApp.loadingExportar = true;
    return new Promise(resolve => {
        var fecha = "";

        var urlToSend = "/Reportes/Exportar?fecha=" + fecha
            + "&idMedico=" + vueAppParams.data.medico
            + "&idPaciente=" + vueAppParams.data.paciente
            + "&idVacuna=" + vueAppParams.data.vacuna


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
            var fileName = "ReporteReporteCargaCestas_" + fechaLocalSlash;
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
