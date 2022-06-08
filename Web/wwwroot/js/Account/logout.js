vueAppParams.data.submiting = false;

//Mounted
vueAppParams.mounted = function () {

};
//Handlers
vueAppParams.methods.onClickCerrarSesion = function () {
    vueApp.submiting = true;
    $.ajax({
        url: '/Account/LogOut',
        method: 'GET',
        success: function (data) {
            if (data.result == AJAX_OK) {
                window.location = '/Aplicaciones/Listado';
            }
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.submiting = false;
        }
    });

}
