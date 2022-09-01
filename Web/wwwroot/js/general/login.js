vueAppParams.data.submiting = false;
vueAppParams.data.login = '';

//Mounted
vueAppParams.mounted = function () {
    vueAppParams.data.background = 'login-bg';
    vueAppParams.data.login = true;
};
//Handlers
vueAppParams.methods.logIn = function () {
    vueApp.submiting = true;
    
        $.ajax({
            url: '/Account/Login',
            method: 'POST',
            data: vueApp.model,
            success: function (data) {

                if (data.result == AJAX_OK) {
                    window.location = '/Home/Index';
                    vueAppParams.data.login = false;
                }
                else {
                    vueApp.notification.showError(data.content.message);
                }
            },
            error: defaultErrorHandler,
            complete: function () {
                vueApp.submiting = false;
                vueAppParams.data.login = false;
            }
        });
}
