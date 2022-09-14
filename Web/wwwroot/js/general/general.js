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

vueAppParams.methods.seeOrder = function () {
    vueAppParams.data.orderId = vueApp.model.Id;
    window.location = "/Orders/Detail/" + vueAppParams.data.orderId;
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