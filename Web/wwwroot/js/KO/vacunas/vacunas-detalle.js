vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.itemCambio = "";
vueAppParams.data.dialog = null;
vueAppParams.data.breadcrums = [];

vueAppParams.mounted = function () {
    this.breadcrums = [
        { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
        { text: jsglobals.Vacunas, disabled: false, href: '/Vacunas/Listado' },
        { text: jsglobals.Detalle, href: '', disabled: true }

    ];
};

vueAppParams.methods.isDisabled = function () {
    return this.model.VacunaExistente;
}


vueAppParams.methods.cambioEstado = function () {

    vueAppParams.data.dialog = true;
    vueAppParams.data.itemCambio = this.model;
};

vueAppParams.methods.confirmaCambioEstado = function (itemCambio) {

    if (itemCambio.Estado) {
        itemCambio.Estado = false;
        vueAppParams.data.dialog = false;
    }
    else {
        itemCambio.Estado = true;
        vueAppParams.data.dialog = false;
    }

}
vueAppParams.methods.guardarVacuna = function () {


    var found = vueApp.model.ListaVacunas.includes(vueApp.model.Nombre);
    if (found) {
        vueApp.notification.showWarning("La vacuna ingresada ya existe. Por favor ingrese una vacuna nueva");
        return false;
    }

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    if (vueApp.model.Id == 0) {

        $.ajax({
            url: "/Vacunas/Agregar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
                setTimeout(function () { window.location = '/Vacunas/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }

    else {
        $.ajax({
            url: "/Vacunas/Actualizar",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MensajeDatosActualizadosOk);
                setTimeout(function () { window.location = '/Vacunas/Listado' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }
};

vueAppParams.methods.volver = function () {
    window.location = "/Vacunas/Listado";
};