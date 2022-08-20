vueAppParams.data.submitting = false;

//Mounted
vueAppParams.mounted = function () {

};
//Handlers
vueAppParams.methods.closeSession = function () {
    window.location = '/Account/Login';
}
