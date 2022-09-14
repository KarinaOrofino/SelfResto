vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.itemChange = "";
vueAppParams.data.dialog = null;
vueAppParams.data.breadcrumbs = [];
vueAppParams.data.loadingAccesses = false;
vueAppParams.data.accessTypes = [];

vueAppParams.mounted = function () {
    this.breadcrumbs = [
        { text: jsglobals.Home, disabled: false, href: '/Home/Index' },
        { text: jsglobals.Users, disabled: false, href: '/Users/List' },
        { text: jsglobals.Detail, href: '', disabled: true }
    ];
    if (this.model.Id == 0) {
        this.model.Active = true;
    }
    this.getAccessTypes();
};

vueAppParams.methods.isDisabled = function (){
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

vueAppParams.methods.getAccessTypes = function () {

    vueAppParams.data.loadingAccesses = true;

    $.ajax({
        url: "/Users/GetAccessTypes",
        method: "GET",
        success: function (data) {
            vueApp.loadingAccesses = false;
            vueApp.accessTypes = data.content;
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.saveUser = function () {

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    if (this.model.Id == 0) {

        $.ajax({
            url: "/Users/Add",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MsgCreationOk);
                setTimeout(function () { window.location = '/Users/List' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }
    else {

        $.ajax({
            url: "/Users/Update",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MsgUpdateOk);
                setTimeout(function () { window.location = '/Users/List' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler

        });
    }
};


vueAppParams.methods.goBack = function () {
    window.location = "/Users/List";
};