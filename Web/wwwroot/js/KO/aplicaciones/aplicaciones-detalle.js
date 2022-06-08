vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.breadcrums = [];
vueAppParams.data.menuFecha = false;
vueAppParams.data.mostrarFecha = true;
vueAppParams.data.loadingMedicos = true;
vueAppParams.data.loadingPacientes = true;
vueAppParams.data.loadingVacunas = true;
vueAppParams.data.fecha = vueAppParams.methods.hoy();
vueAppParams.data.medicos = [];
vueAppParams.data.vacunas = [];
vueAppParams.data.pacientes = [];
vueAppParams.data.vacunasSeleccionadas = [];

vueAppParams.mounted = function () {
    this.breadcrums = [
        { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
        { text: jsglobals.Aplicaciones, disabled: false, href: '/Aplicaciones/Listado' },
        { text: jsglobals.Detalle, href: '', disabled: true }
    ];
    
    this.obtenerMedicos();
    this.obtenerPacientes();
    this.obtenerVacunas();
};

//vueAppParams.watch = {
//    fecha(val) {
//        vueApp.fecha = vueAppParams.methods.formatoFecha(vueApp.fecha);
//    }
//}


vueAppParams.methods.obtenerPacientes = function () {

    vueAppParams.data.loadingPacientes = true;

    $.ajax({
        url: "/Pacientes/ObtenerTodos",
        method: "GET",
        success: function (data) {
            vueApp.pacientes = data.content;
            vueApp.loadingPacientes = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingPacientes = false; });
}

vueAppParams.methods.obtenerMedicos = function () {

    vueAppParams.data.loadingMedicos = true;

    $.ajax({
        url: "/Medicos/ObtenerTodos",
        method: "GET",
        success: function (data) {
            vueApp.medicos = data.content;
            vueApp.loadingMedicos = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingMedicos = false; });
}

vueAppParams.methods.obtenerVacunas = function () {

    vueAppParams.data.loadingVacunas = true;

    $.ajax({
        url: "/Vacunas/ObtenerTodas",
        method: "GET",
        success: function (data) {
            vueApp.vacunas = data.content;
            vueApp.loadingVacunas = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingVacunas = false; });
}

vueAppParams.methods.guardarAplicacion = function () {


    var found = vueApp.model.ListaPacientes.some(pac => pac.Nombre == vueApp.model.NombrePaciente.toLocaleUpperCase());
    //agregar fecha
    if (found) {
        vueApp.notification.showWarning("Paciente ya ingresado en esa fecha");
        return false;
    }

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    vueApp.model.ListaIdsVacunas = vueApp.vacunasSeleccionadas;
    vueApp.model.Fecha = vueApp.fecha;

    if (vueApp.model.Id == 0) {

        $.ajax({
            url: "/Aplicaciones/Agregar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
                setTimeout(function () { window.location = '/Aplicaciones/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }

    else {
        $.ajax({
            url: "/Aplicaciones/Actualizar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosActualizadosOk);
                setTimeout(function () { window.location = '/Aplicaciones/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }
};

vueAppParams.methods.nuevoPaciente= function () {
    window.location = "/Pacientes/Detalle";
};

vueAppParams.methods.nuevoMedico = function () {
    window.location = "/Medicos/Detalle";
};

vueAppParams.methods.nuevaVacuna = function () {
    window.location = "/Vacunas/Detalle";
};

vueAppParams.methods.volver = function () {
    window.history.back();
};