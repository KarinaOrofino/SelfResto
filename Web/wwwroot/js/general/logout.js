vueAppParams.data.submiting = false;

//Mounted
vueAppParams.mounted = function () {

};
//Handlers
vueAppParams.methods.closeSession = function () {
    vueApp.submiting = true;
    $.ajax({
        url: '/Account/LogOut',
        method: 'GET',
        success: function (data) {
            if (data.result == AJAX_OK) {
                window.location = '/MenuItems/List';
            }
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.submiting = false;
        }
    });

}
