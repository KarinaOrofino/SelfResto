var defaultErrorHandler = function (data) {

    //Para deshabilitar botones y prevenir el doble post
    vueApp.submiting = false;

    res = data.responseJSON;

    if (res.result == AJAX_ERROR) {

        vueApp.notification.showError(res.errorUi);

    } else if (res.result == AJAX_INVALID) {

        vueApp.errors = res.errors;

        if (res.errorUi) {
            vueApp.notification.showError(res.errorUi);
        }

    } else if (res.result == AJAX_REDIRECT) {

        vueApp.notification.showWarning("Sesion vencida");
        window.location = res.redirect;
    }
};

vueAppParams.methods.clearErrors = function () {
    vueApp.errors = {};
}