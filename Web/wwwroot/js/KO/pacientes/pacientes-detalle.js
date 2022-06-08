vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.breadcrums = [];
vueAppParams.data.menuFechaNacimiento = false;
vueAppParams.data.FechaNacimientoSeleccionada = '';
vueAppParams.data.tipoSeleccionado = [];
vueAppParams.data.productoSeleccionado = [];
vueAppParams.data.standSeleccionado = []

vueAppParams.data.filtros = {
    FechaNacimientoSeleccionada: vueAppParams.methods.formatoFecha(vueAppParams.data.fechaNacimiento),
}

vueAppParams.mounted = function () {
    this.breadcrums = [
        { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
        { text: jsglobals.Pacientes, disabled: false, href: '/Pacientes/Listado' },
        { text: jsglobals.Detalle, href: '', disabled: true }

    ];

    if (this.model.Id == 0) {
        this.model.FechaNacimientoSeleccionada = '';
    }
    else {
        vueAppParams.data.filtros.FechaNacimientoSeleccionada = this.model.FechaNacimiento;
    }
};

vueAppParams.methods.guardarPaciente = function () {

    var found = vueApp.model.ListaPacientes.includes(vueApp.model.Nombre && vueApp.model.Apellido && vueApp.model.FechaNacimiento);
    if (found) {
        vueApp.notification.showWarning("El paciente ingresado ya existe. Por favor ingrese un paciente nuevo");
        return false;
        }

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    vueApp.model.FechaNacimiento = vueApp.filtros.FechaNacimientoSeleccionada;

    if (vueApp.model.Id == 0) {

        $.ajax({
            url: "/Pacientes/Agregar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
                setTimeout(function () { window.location = '/Pacientes/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }

    else {
        $.ajax({
            url: "/Pacientes/Actualizar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosActualizadosOk);
                setTimeout(function () { window.location = '/Pacientes/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }
};

vueAppParams.methods.volver = function () {
    window.history.back();
};