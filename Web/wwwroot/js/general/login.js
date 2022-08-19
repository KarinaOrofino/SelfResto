vueAppParams.data.submiting = false;

//Mounted
vueAppParams.mounted = function () {
    vueAppParams.data.background = 'login-bg';
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
                    window.location = '/MenuItems/List';
                }
                else {
                    vueApp.notification.showError(data.content.messages[0]);
                }
            },
            error: defaultErrorHandler,
            complete: function () {
                vueApp.submiting = false;
            }
        });
}
