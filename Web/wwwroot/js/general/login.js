vueAppParams.data.submiting = false;
vueAppParams.data.restrictedAccess = false;
vueAppParams.data.table = { id: '', number: '' }
vueAppParams.data.dialogElegirMesa = false;
vueAppParams.data.tables = [];
vueAppParams.data.loadingTables = false;

//Mounted
vueAppParams.mounted = function () {
    vueAppParams.data.background = 'login-bg';
    this.loadTables();
};

//Handlers
vueAppParams.methods.loadTables = function () {

    vueAppParams.data.loadingTables = true;

    $.ajax({
        url: "/Account/GetAllTables",
        method: "GET",
        success: function (data) {
            vueApp.tables = data.content;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueAppParams.data.loadingTables = false;
        }
    }).done(() => {
        vueAppParams.data.loadingTables = false;
    });
};

vueAppParams.methods.chooseTable = function () {

    vueApp.dialogElegirMesa = true;

}


vueAppParams.methods.logInClient = function () {

    vueApp.submiting = true;
    vueApp.model.Email = 'cliente@correo.com'
    vueApp.model.Password = '12345'
    

    $.ajax({
        url: '/Account/Login',
        method: 'POST',
        data: { userVM: vueApp.model, tableId: vueApp.table.id},
        success: function (data) {

            if (data.result == AJAX_OK) {
                var orderId = data.content;
                vueApp.goToIndex(orderId);
            }
            else {
                vueApp.notification.showError(data.errorUi);
            }
            vueApp.dialogElegirMesa = false;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.submiting = false;
            vueApp.dialogElegirMesa = false;
        }
    });

}


vueAppParams.methods.goToIndex = function (id) {

    window.location = "/Home/IndexClient/" + id;
};

vueAppParams.methods.logIn = function () {
    vueAppParams.data.restrictedAccess = true;
}

vueAppParams.methods.logInEmployee = function () {
    vueApp.submiting = true;

    $.ajax({
        url: '/Account/Login',
        method: 'POST',
        data: vueApp.model,
        success: function (data) {

            if (data.result == AJAX_OK) {
                window.location = '/Home/IndexEmployee';
            }
            else {
                vueApp.notification.showError(data.errorUi);
            }
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.submiting = false;
        }
    });
};

vueAppParams.methods.goBack = function () {
    vueAppParams.data.restrictedAccess = false;
}

