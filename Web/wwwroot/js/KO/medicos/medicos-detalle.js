vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.itemCambio = "";
vueAppParams.data.dialog = null;

vueAppParams.data.breadcrums = [
    { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
    { text: jsglobals.Medicos, disabled: false, href: '/Medicos/Listado' },
    { text: jsglobals.Detalle, disabled: true, href: '' }
];

vueAppParams.mounted = function () {

};

vueAppParams.methods.isDisabled = function () {
    if (this.model.matricula == null) {
        return true
    }
};

vueAppParams.methods.cambioEstado = function () {

    vueAppParams.data.dialog = true;
    vueAppParams.data.itemCambio = this.model;
};

vueAppParams.methods.confirmaCambioEstado = function (itemCambio) {

    if (itemCambio.estado) {
        itemCambio.estado = false;
        vueAppParams.data.dialog = false;
    }
    else {
        itemCambio.estado = true;
        vueAppParams.data.dialog = false;
    }

}
vueAppParams.methods.guardar = function () {

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    $.ajax({
        url: "/Medicos/Guardar",
        method: "POST",
        data: vueApp.model,
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
            setTimeout(function () { window.location = '/Medicos/Listado' }, TIEMPO_CIERRE_FORMULARIO);
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.onClickVolver = function () {
    window.location = "/Medicos/Listado";
};