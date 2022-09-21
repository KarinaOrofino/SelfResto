vueAppParams.data.notification = {
    show: false,
    type: 'sucess',
    message: '',
    timeout: 3000,
    icon: 'mdi-alert',
    iconColor: 'white'
};

vueAppParams.data.notification.showSuccess = function (message) {
    vueApp.notification.show = true;
    vueApp.notification.message = message;
    vueApp.notification.type = 'success';
    vueApp.notification.icon = "mdi-check";    
};

vueAppParams.data.notification.showWarning = function (message) {
    vueApp.notification.show = true;
    vueApp.notification.message = message;
    vueApp.notification.type = 'warning';
    vueApp.notification.icon = "mdi-alert";    
};

vueAppParams.data.notification.showError = function (message) {
    vueApp.notification.show = true;
    vueApp.notification.message = message;
    vueApp.notification.type = 'error';
    vueApp.notification.icon = "mdi-alert-octagram";    
};
