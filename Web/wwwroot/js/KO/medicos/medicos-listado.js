//Model
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
//vueAppParams.data.breadcrums = [];

//vueAppParams.mounted = function () {
//    this.breadcrums = [
//        { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
//        { text: jsglobals.Medicos, disabled: false, href: '/Medicos/Listado' },
//        { text: jsglobals.Detalle, href: '', disabled: true }

//    ];

//};

vueAppParams.methods.isDisabled = function () {
    //return this.model.MedicoExistente;
}


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

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Medicos, disabled: true},
    { text: jsglobals.Listado, disabled: true}
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
vueAppParams.methods.agregarMedico = function () {

    var matricula = null

    $.ajax({
        url: "/Medicos/VerMedico/?matricula=" + matricula,
        method: "GET",
        success: function (data) {
            vueApp.model = data.content;
            vueApp.notification.showSuccess(jsglobals.MensajeDatosActivadosOk);
            setTimeout(function () { /*window.location = '/Medicos/Listado'*/ });
            vueApp.verMedico = true;
        },
        error: defaultErrorHandler
    });
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

    $.ajax({
        url: "/Medicos/VerMedico/?matricula=" + matricula,
        method: "GET",
        success: function (data) {
            vueApp.model = data.content;
            vueApp.notification.showSuccess(jsglobals.MensajeDatosActivadosOk);
            setTimeout(function () { /*window.location = '/Medicos/Listado'*/ });
            vueApp.verMedico = true;
        },
        error: defaultErrorHandler
    });

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








vueAppParams.methods.cambioEstado = function () {

    vueAppParams.data.dialog = true;
    vueAppParams.data.itemCambio = this.model;
};

vueAppParams.methods.confirmaCambioEstado = function (itemCambio) {

    if (itemCambio.Estado) {
        itemCambio.Estado = false;
        vueAppParams.data.dialog2 = false;
    }
    else {
        itemCambio.Estado = true;
        vueAppParams.data.dialog2 = false;
    }

}
vueAppParams.methods.guardarMedico = function () {


    var found = vueApp.model.ListaMatriculasMedicos.includes(vueApp.model.Matricula);
    if (found) {
        vueApp.notification.showWarning("La matrícula ingresada ya existe. Por favor ingrese una matrícula nueva");
        return false;
    }

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    if (!vueApp.model.MedicoExistente) {

        $.ajax({
            url: "/Medicos/Agregar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
                setTimeout(function () { window.location = '/Medicos/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }

    else {
        $.ajax({
            url: "/Medicos/Actualizar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosActualizadosOk);
                setTimeout(function () { window.location = '/Medicos/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }
};

//vueAppParams.methods.volver = function () {
//    window.location = "/Medicos/Listado";
//};