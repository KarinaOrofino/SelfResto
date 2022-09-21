vueAppParams.data.ordersWaiter = [];
vueAppParams.data.ordersKitchen = [];
vueAppParams.data.cronoWarning = '';
vueAppParams.data.tables = [];
vueAppParams.data.elem = [];
vueAppParams.data.dialogOrderDetails = null; 

vueAppParams.data.slide = 0;


    vueAppParams.data.colors = [
        'primary',
        'secondary',
        'yellow darken-2',
        'red',
        'orange',
    ];

vueAppParams.mounted = function () {
    this.getActiveOrders();
    this.getAllTables();

};

vueAppParams.methods.CallWaiter = function () {

};

vueAppParams.methods.AskBill = function () {

};

vueAppParams.methods.goToMenu = function () {
    window.location = "/MenuItems/ListToOrder/" + vueApp.model.Id;

};


vueAppParams.methods.startTime = function () {

    vueApp.ordersKitchen.forEach(o => {

        const today = new Date();
        var date = new Date(Date.parse(o.requestedTime));
        var diffMiliseconds = today - date;

        let seconds = Math.floor(diffMiliseconds / 1000);
        let minutes = Math.floor(seconds / 60);
        let hours = Math.floor(minutes / 60);

        seconds = seconds % 60;
        minutes = minutes % 60;


        if (minutes < 9){
            o.cronoWarning = 'green';
        }
        if (minutes > 9 && minutes < 20){
            o.cronoWarning = 'orange';
        }
        if (minutes > 19){
            o.cronoWarning = 'red';
        }

        seconds = seconds.toString().padStart(2, '0');
        minutes = minutes.toString().padStart(2, '0');

        o.tiempoEnCocina = hours + ":" + minutes + ":" + seconds;
        setTimeout(vueApp.startTime, 1000);


    });
};


vueAppParams.methods.getActiveOrders = function () {

    $.ajax({
        url: "/Home/GetActiveOrders",
        method: "GET",
        success: function (data) {
            vueAppParams.data.ordersWaiter = data.content;
            vueAppParams.data.ordersKitchen = data.content.filter(o => o.orderDetails.length > 0 && !o.orderDetails.every(od => od.menuItemCategoryId >= 8));
            vueAppParams.data.ordersKitchen.forEach(o => o.cronoWarning = '');
            vueAppParams.data.ordersKitchen.forEach(o=>vueApp.startTime(o));
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.getAllTables = function () {

    $.ajax({
        url: "/Home/GetAllTables",
        method: "GET",
        success: function (data) {
            vueAppParams.data.tables = data.content;
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.changeItemState = function (orderId, id, state) {

    $.ajax({
        url: "/Home/ChangeItemState",
        data: { itemId: id, itemState: state },
        method: "POST",
        success: function (data) {
            var orderIndex = vueApp.ordersKitchen.findIndex(o => o.id == orderId);
            var itemIndex = vueApp.ordersKitchen[orderIndex].orderDetails.findIndex(od => od.id == id);
            if (state == 2) {
                vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex].stateTypeId = 2;
            }
            if (state == 3) {
                vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex].stateTypeId = 3;
            }
            if (state == 4) {
                vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex].stateTypeId = 4;
            }

        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.changeBack = function (orderId, id) {

    var orderIndex = vueApp.ordersKitchen.findIndex(o => o.id == orderId);
    var itemIndex = vueApp.ordersKitchen[orderIndex].orderDetails.findIndex(od => od.id == id);
    var item = vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex];
    var state = '';
    if (item.stateTypeId == 2) {
        state = 1;
    }

    if (item.stateTypeId == 3) {
        state = 2;
    }

    if (item.stateTypeId == 4) {
        state = 3;
    }

    $.ajax({
        url: "/Home/ChangeItemState",
        data: { itemId: id, itemState: state },
        method: "POST",
        success: function (data) {
            if (item.stateTypeId == 2) {
                item.stateTypeId = 1;
            }
            if (item.stateTypeId == 3) {
                item.stateTypeId = 2;
            }
            if (item.stateTypeId == 4) {
                item.stateTypeId = 3;
            }


        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.seeDetail = function (id) {
    window.location = "/Orders/Detail/" + id;

};

vueAppParams.methods.seeOrderDetail = function (tableId) {

    vueApp.elem = vueApp.ordersWaiter.filter(o => o.tableId == tableId);
    vueAppParams.data.dialogOrderDetails = true;
};

