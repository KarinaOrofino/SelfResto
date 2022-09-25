//Constants JS

const UN_SEGUNDO = 1000;
const TIEMPO_CIERRE_FORMULARIO = UN_SEGUNDO * 2;
const TIEMPO_MENSAJE_ERROR = UN_SEGUNDO * 3;
const AJAX_OK = 0;
const AJAX_ERROR = -1;
const AJAX_REDIRECT = 1;
const AJAX_INVALID = 2;
const HTTP_ERROR = 500;


vueAppParams.data.orderId='';

vueAppParams.data.facebook = { imagen: "/images/facebook.png", url: "https://www.facebook.com" };
vueAppParams.data.instagram = { imagen: "/images/instagram.png", url: "https://www.instagram.com" };
vueAppParams.data.logoHeader = "/images/SelfResto.jpg";
vueAppParams.data.logoCubiertos = "/images/LogoCubiertos.png";


vueAppParams.methods.goToMenu = function () {

    if (vueApp.model.Id == 0) {
        window.location = "/MenuItems/ListToOrder/" + vueApp.model.OrderId;
    }
    else {
        window.location = "/MenuItems/ListToOrder/" + vueApp.model.Id;
    }

};

vueAppParams.methods.seeOrder = function () {

    if (vueApp.model.Id == 0) {
        window.location = "/Orders/Detail/" + vueApp.model.OrderId;
    }
    else
    {
        window.location = "/Orders/Detail/" + vueApp.model.Id;
    }
}

vueAppParams.methods.seeIndex = function () {

    if (vueApp.model.Id == 0) {
        window.location = "/Home/IndexClient/" + vueApp.model.OrderId;
    }
    else
    {
        window.location = "/Home/IndexClient/" + vueApp.model.Id;
    }
}

vueAppParams.methods.callWaiter = function () {

    if (vueApp.model.Call) {
        vueApp.notification.showWarning(jsglobals.MsgCallAlreadyRequested);
        
        return
    }

    if (vueApp.model.Id == 0) {
        var orderId = vueApp.model.OrderId;

    }
    else {
        var orderId = vueApp.model.Id;
    }

    $.ajax({
        url: "/Home/CallWaiter",
        method: "POST",
        data: { orderId: orderId },
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgCallOk);
            vueApp.model.Call = true;
        },
        error: defaultErrorHandler,
    });
}

vueAppParams.methods.requestBill = function () {

    if (vueApp.model.PaymentRequest) {
        vueApp.notification.showWarning(jsglobals.MsgBillAlreadyRequested);
        return
    }
    if (vueApp.model.Id == 0) {
        var orderId = vueApp.model.OrderId;

    }
    else {
        var orderId = vueApp.model.Id;
    }


    $.ajax({
        url: "/Home/RequestBill",
        method: "POST",
        data: { orderId: orderId },
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgRequestBillOk);
            vueApp.model.PaymentRequest = true;
        },
        error: defaultErrorHandler,
    });
}