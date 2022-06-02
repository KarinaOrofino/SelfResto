//Model
vueAppParams.data.gridData = [];
vueAppParams.data.listadoFiltrado = [];
vueAppParams.data.listaParaExportar = [];
vueAppParams.data.loadingVacunas= true;
vueAppParams.data.loadingExportar = false;
vueAppParams.data.dialogActivar = false;
vueAppParams.data.dialogInactivar = false;
vueAppParams.data.vacunaAActivar = '';
vueAppParams.data.vacunaAInactivar = '';

vueAppParams.data.search = '';

vueAppParams.data.filtros = {

    estado: '',

};

vueAppParams.data.filtros.estado = 0;

vueAppParams.data.headers = [

    { value: 'id', align: ' d-none' },
    { text: jsglobals.Nombre, value: 'nombre', align: 'center', class: 'protevac-headers upper' },
    { text: jsglobals.Estado, value: 'estado', align: 'center text-uppercase', class: 'protevac-headers' },
    { text: jsglobals.Acciones, value: 'acciones', align: 'center text-uppercase', class: 'protevac-headers' }

];

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Vacunas, disabled: false, href: '/Vacunas/Listado' },
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
        url: "/Vacunas/ObtenerTodas",
        method: "GET",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.listadoFiltrado = vueApp.gridData;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.loadingVacunas = false;
        }
    }).done(() => {
        vueApp.loadingVacunas = false;
    });
};

// Metodos
vueAppParams.methods.agregarVacuna = function (event) {
    window.location = "Detalle/";
};

vueAppParams.methods.inactivarVacuna = function (item) {

    vueAppParams.data.dialogInactivar = true;
    vueAppParams.data.vacunaAInactivar = item;
};

vueAppParams.methods.confirmaInactivar = function (id) {

    vueAppParams.data.dialogInactivar = false;

    var indiceVacunaAInactivar = vueApp.gridData.findIndex(vac => vac.id == id)
    vueApp.gridData[indiceVacunaAInactivar].estado = false;

    $.ajax({
        url: "/Vacunas/Inactivar?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosInactivadosOk);
            setTimeout(function () {  });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.activarVacuna = function (item) {

    vueAppParams.data.dialogActivar = true;
    vueAppParams.data.vacunaAActivar = item;
};

vueAppParams.methods.confirmaActivar = function (id) {

    vueAppParams.data.dialogActivar = false;

    var indiceVacunaAActivar = vueApp.gridData.findIndex(med => med.id == id)
    vueApp.gridData[indiceVacunaAActivar].estado = true;

    $.ajax({
        url: "/Vacunas/Activar?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosActivadosOk);
            setTimeout(function () { });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.editarVacuna = function (id) {

    window.location = "/Vacunas/Detalle/?id=" + id;
};


vueAppParams.methods.exportarListaVacunas = function () {
    vueApp.loadingExportar = true;



    return new Promise(resolve => {

        const term = this.search.toLowerCase();
        //const isMatched = str => str.toLowerCase().includes(term);
        //vueApp.listaParaExportar = listadoFiltrado.filter(vac => isMatched(vac.nombre));
        //var paramets = JSON.stringify(vueApp.listaParaExportar)

        var urlToSend = "/Vacunas/Exportar?nombre=" + term + "&estado=" + vueAppParams.data.filtros.estado;
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
            var fileName = "ReporteVacunas_" + fechaLocalSlash;
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
