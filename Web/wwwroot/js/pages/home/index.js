
vueAppParams.mounted = function () {
	
};

vueAppParams.methods.CallWaiter = function () {

}

vueAppParams.methods.AskBill = function () {

}

vueAppParams.methods.goToMenu = function (id) {
	vueAppParams.data.orderId = vueApp.model.Id;
	window.location = "/MenuItems/ListToOrder/" + vueAppParams.data.orderId;

}




