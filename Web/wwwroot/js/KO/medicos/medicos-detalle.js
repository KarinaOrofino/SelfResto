vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.itemCambio = "";
vueAppParams.data.dialog = null;
vueAppParams.data.breadcrums = [];

vueAppParams.mounted = function () {
    this.breadcrums = [
        { text: jsglobals.Inicio, disabled: false, href: '/Home/Index' },
        { text: jsglobals.Medicos, disabled: false, href: '/Medicos/Listado' },
        { text: jsglobals.Detalle, href: '', disabled: true }

    ];
};

vueAppParams.methods.isDisabled = function (){
    return this.model.MedicoExistente;
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

    if (vueApp.model.Matricula == 0) {

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

    else
    {
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

vueAppParams.methods.volver = function () {
    window.location = "/Medicos/Listado";
};