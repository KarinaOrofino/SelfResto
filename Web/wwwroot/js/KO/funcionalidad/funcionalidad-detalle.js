vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.breadcrums = [];
vueAppParams.data.roles = [];
vueAppParams.data.TiposAcceso = [];
vueAppParams.data.dialog = null;
vueAppParams.data.dialog2 = null;
vueAppParams.data.itemDelete = "";
vueAppParams.data.itemCambio = "";
vueAppParams.data.gridData = [];
vueAppParams.data.loadingRolesxFuncionalidad = true;
vueAppParams.data.seleccion = {
    tipoAcceso: vueAppParams.data.TiposAcceso,
    rol: vueAppParams.data.roles,
};

vueAppParams.mounted = function () {
    //Breadcrums
    this.breadcrums = [
        {text: jsglobals.Funcionalidad, disabled: false, href: '/Funcionalidad/Listado'},
        { text: this.model.id > 0 ? jsglobals.Editar : jsglobals.Nueva, href: '/Funcionalidad/Detalle', disabled: true }
    ];
    
    this.loadGrid();
    this.obtenerRoles();
    this.obtenerTiposAcceso();
};

vueAppParams.data.headers = [
    { text: jsglobals.Rol, value: 'nombreRol' },
    { text: jsglobals.TipoAcceso, value: 'nombreTipoAcceso', align: 'center' },
    { text: jsglobals.Acciones, value: 'actions', align: 'center', sortable: false, class: 'protevac-headers' },
];

// Handlers
vueAppParams.methods.isDisabled = function () {
    if (this.model.id == null) {
        return true
    }
};

vueAppParams.methods.onChangeCambioEstado = function () {

    vueAppParams.data.dialog = true;
    vueAppParams.data.itemCambio = this.model;
};

vueAppParams.methods.onClickNoConfirma = function (itemCambio) {

    if (itemCambio.activo) {
        itemCambio.activo = false;
        vueAppParams.data.dialog = false;
    }
    else {
        itemCambio.activo = true;
        vueAppParams.data.dialog = false;
    }

};

vueAppParams.methods.obtenerRoles = function () {

    $.ajax({
        url: "/Funcionalidad/ObtenerRoles",
        method: "POST",
        success: function (data) {
            vueApp.roles = data;
        },
        error: defaultErrorHandler
    })
};

vueAppParams.methods.obtenerTiposAcceso = function () {

    $.ajax({
        url: "/Funcionalidad/ObtenerTiposAcceso",
        method: "POST",
        success: function (data) {
            vueApp.TiposAcceso = data;
        },
        error: defaultErrorHandler
    })
};

vueAppParams.methods.loadGrid = function () {

    $.ajax({
        url: "/Funcionalidad/ObtenerRolesxFuncionalidad?idFuncionalidad=" + vueAppParams.data.model.id,
        method: "POST",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.loadingRolesxFuncionalidad = false;

        },
        error: defaultErrorHandler
    })
};

vueAppParams.methods.onClickAgregarRol = function () {


    var nombreRol = vueAppParams.data.seleccion.rol;
    if (nombreRol.length == 0)
        {
        vueApp.notification.showWarning(jsglobals.MensajeRolVacio);
        }
    else
    {
        var nombreTipoAcceso = vueAppParams.data.seleccion.tipoAcceso;
        if (nombreTipoAcceso.length == 0) {
            vueApp.notification.showWarning(jsglobals.MensajeTipoAccesoVacio);
        }
        else {
            if (vueApp.gridData.findIndex(s => s.nombreRol == vueAppParams.data.seleccion.rol) >= 0) {
                vueApp.notification.showWarning(jsglobals.MensajeRolExistente);
            }
            else {
                vueApp.gridData.push({
                    idTipoAcceso: vueAppParams.data.TiposAcceso[vueAppParams.data.TiposAcceso.findIndex(a => a.descripcion == vueAppParams.data.seleccion.tipoAcceso)].idTipoAcceso,
                    nombreTipoAcceso: nombreTipoAcceso,
                    idRol: vueAppParams.data.roles[vueAppParams.data.roles.findIndex(a => a.descripcion == vueAppParams.data.seleccion.rol)].idRol,
                    nombreRol: nombreRol
                });

                if (this.model.id == null) {
                    vueApp.model.listaRolesxFuncionalidad.push(
                        {
                            idFuncionalidad: 0,
                            idTipoAcceso: vueAppParams.data.TiposAcceso[vueAppParams.data.TiposAcceso.findIndex(a => a.descripcion == vueAppParams.data.seleccion.tipoAcceso)].idTipoAcceso,
                            idRol: vueAppParams.data.roles[vueAppParams.data.roles.findIndex(a => a.descripcion == vueAppParams.data.seleccion.rol)].id
                        });
                }
                else {
                    vueApp.model.listaRolesxFuncionalidad.push({
                        idFuncionalidad: vueApp.model.id,
                        idTipoAcceso: vueAppParams.data.TiposAcceso[vueAppParams.data.TiposAcceso.findIndex(a => a.descripcion == vueAppParams.data.seleccion.tipoAcceso)].idTipoAcceso,
                        idRol: vueAppParams.data.roles[vueAppParams.data.roles.findIndex(a => a.descripcion == vueAppParams.data.seleccion.rol)].id
                    });
                }

                vueAppParams.data.seleccion.tipoAcceso = [];
                vueAppParams.data.seleccion.rol = [];
            }

        }
     }
};

vueAppParams.methods.onClickEliminarRol = function (item) {

    vueAppParams.data.dialog2 = true;
    vueAppParams.data.itemDelete = vueApp.gridData.indexOf(item);
};

vueAppParams.methods.onClickConfirmaBorrar = function (item) {

    vueApp.clearErrors();
    vueAppParams.data.dialog2 = false;
    var index = vueApp.model.listaRolesxFuncionalidad.findIndex(s => s.id == vueApp.gridData[item].id && s.idTipoAcceso == vueApp.gridData[item].idTipoAcceso && s.idRol == vueApp.gridData[item].idRol)
    vueApp.model.listaRolesxFuncionalidad.splice(index,1);
    vueApp.gridData.splice(item, 1);
};

vueAppParams.methods.onClickGuardar = function () {

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    $.ajax({
        url: "/Funcionalidad/Guardar",
        method: "POST",
        data: vueApp.model,
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
            setTimeout(function () { window.location = '/Funcionalidad/Listado' }, TIEMPO_CIERRE_FORMULARIO);
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.onClickVolver = function () {
    window.location = "/Funcionalidad/Listado";
};