﻿vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.itemChange = "";
vueAppParams.data.dialog = null;
vueAppParams.data.breadcrumbs = [];

vueAppParams.mounted = function () {
    this.breadcrumbs = [
        { text: jsglobals.Home, disabled: false, href: '/Home/Index' },
        { text: jsglobals.Tables, disabled: false, href: '/Tables/List' },
        { text: jsglobals.Detail, href: '', disabled: true }
    ];
    if (this.model.Id == 0) {
        this.model.Active = true;
    }
};

vueAppParams.methods.isDisabled = function () {
    return !this.model.Id == 0;
}


vueAppParams.methods.changeState = function () {

    vueAppParams.data.dialog = true;
    vueAppParams.data.itemChange = this.model;
};

vueAppParams.methods.confirmStateChange = function (itemChange) {

    if (itemChange.Active) {
        itemChange.Active = false;
        vueAppParams.data.dialog = false;
    }
    else {
        itemChange.Active = true;
        vueAppParams.data.dialog = false;
    }

};

vueAppParams.methods.saveTable = function () {

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    if (this.model.Id == 0) {

        $.ajax({
            url: "/Tables/Add",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MsgCreationOk);
                setTimeout(function () { window.location = '/Tables/List' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }
    else {

        $.ajax({
            url: "/Tables/Update",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MsgUpdateOk);
                setTimeout(function () { window.location = '/Tables/List' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler

        });
    }
};


vueAppParams.methods.goBack = function () {
    window.location = "/Tables/List";
};