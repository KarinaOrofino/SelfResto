vueAppParams.data.ordersWaiter = [];
vueAppParams.data.ordersKitchen = [];
vueAppParams.data.cronoWarning = '';
vueAppParams.data.tables = [];
vueAppParams.data.elem = [];
vueAppParams.data.dialogOrderDetails = null;
vueAppParams.data.dialogCloseCall = false;
vueAppParams.data.dialogCloseOrder = false;
vueAppParams.data.help = '';
vueAppParams.data.order = [];
vueAppParams.data.imagen = "~/images/menu.png";

vueAppParams.data.slide = 0;


    vueAppParams.data.colors = [
        'primary',
        'secondary',
        'yellow darken-2',
        'red',
        'orange',
    ];

vueAppParams.mounted = function () {
    this.getAllTables();
    this.getActiveOrdersKitchen();
    this.getActiveOrdersWaiter();
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

vueAppParams.methods.getActiveOrdersWaiter = function () {

    $.ajax({
        url: "/Home/GetActiveOrders",
        method: "GET",
        success: function (data) {
            vueAppParams.data.ordersWaiter = data.content;
        },
        error: defaultErrorHandler,
    });
};

vueAppParams.methods.getAllTables = function () {

    $.ajax({
        url: "/Home/GetAllTables",
        method: "GET",
        success: function (data) {
            vueAppParams.data.tables = data.content
        },
        error: defaultErrorHandler
    });
};



vueAppParams.methods.getActiveOrdersKitchen = function () {

    $.ajax({
        url: "/Home/GetActiveOrders",
        method: "GET",
        success: function (data) {
            vueAppParams.data.ordersKitchen = data.content.filter(o => o.orderDetails.length > 0 && !o.orderDetails.every(od => od.menuItemCategoryId >= 8));
            vueAppParams.data.ordersKitchen.forEach(o => o.cronoWarning = '');
            vueAppParams.data.ordersKitchen.forEach(o => vueApp.startTime(o));
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.changeItemStateKitchen = function (orderId, id, newState) {

    $.ajax({
        url: "/Home/ChangeItemState",
        data: { itemId: id, itemState: newState },
        method: "POST",
        success: function (data) {
            var orderIndex = vueApp.ordersKitchen.findIndex(o => o.id == orderId);
            var itemIndex = vueApp.ordersKitchen[orderIndex].orderDetails.findIndex(od => od.id == id);
            if (newState == 2) {
                vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex].orderDetailStatusId = 2;
            }
            if (newState == 3) {
                vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex].orderDetailStatusId = 3;
            }
            if (newState == 4) {
                vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex].orderDetailStatusId = 4;
            }
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.changeBackKitchen = function (orderId, id) {

    var orderIndex = vueApp.ordersKitchen.findIndex(o => o.id == orderId);
    var itemIndex = vueApp.ordersKitchen[orderIndex].orderDetails.findIndex(od => od.id == id);
    var item = vueApp.ordersKitchen[orderIndex].orderDetails[itemIndex];
    var stateBack = '';
    if (item.orderDetailStatusId == 2) {
        stateBack = 1;
    }

    if (item.orderDetailStatusId == 3) {
        stateBack = 2;
    }

    if (item.orderDetailStatusId == 4) {
        stateBack = 3;
    }

    $.ajax({
        url: "/Home/ChangeItemState",
        data: { itemId: id, itemState: stateBack },
        method: "POST",
        success: function (data) {
            if (item.orderDetailStatusId == 2) {
                item.orderDetailStatusId = 1;
            }
            if (item.orderDetailStatusId == 3) {
                item.orderDetailStatusId = 2;
            }
            if (item.orderDetailStatusId == 4) {
                item.orderDetailStatusId = 3;
            }
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.changeItemStateWaiter = function (orderId, id) {

    $.ajax({
        url: "/Home/ChangeItemState",
        data: { itemId: id, itemState: 4 },
        method: "POST",
        success: function (data) {
            var orderIndex = vueApp.ordersWaiter.findIndex(o => o.id == orderId);
            var itemIndex = vueApp.ordersWaiter[orderIndex].orderDetails.findIndex(od => od.id == id);
            vueApp.ordersWaiter[orderIndex].orderDetails[itemIndex].orderDetailStatusId = 4;
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.changeBackWaiter = function (orderId, id) {

    var orderIndex = vueApp.ordersWaiter.findIndex(o => o.id == orderId);
    var itemIndex = vueApp.ordersWaiter[orderIndex].orderDetails.findIndex(od => od.id == id);
    var item = vueApp.ordersWaiter[orderIndex].orderDetails[itemIndex];
    if (item.orderDetailStatusId == 3) {
        stateBack = 2;
    }
    if (item.orderDetailStatusId == 4 && item.menuItemCategoryId <= 7) {
        stateBack = 3;
    }
    if (item.orderDetailStatusId == 4 && item.menuItemCategoryId > 7) {
        stateBack = 1;
    }

    $.ajax({
        url: "/Home/ChangeItemState",
        data: { itemId: id, itemState: stateBack },
        method: "POST",
        success: function (data) {
            if (item.orderDetailStatusId == 3) {
                item.orderDetailStatusId = 2;
            }
            if (item.orderDetailStatusId == 4 && item.menuItemCategoryId <= 7) {
                item.orderDetailStatusId = 3;
            }
            if (item.orderDetailStatusId == 4 && item.menuItemCategoryId > 7) {
                item.orderDetailStatusId = 1;
            }
        },
        error: defaultErrorHandler
    });
};

vueAppParams.methods.seeDetail = function (id) {
    window.location = "/Orders/Detail/" + id;

};

vueAppParams.methods.seeOrderDetail = function (orderId) {

    vueApp.elem = vueApp.ordersWaiter.filter(o => o.id == orderId);
    vueAppParams.data.dialogOrderDetails = true;
};

vueAppParams.methods.closeCall = function (order) {

    vueAppParams.data.dialogCloseCall = true;
    vueAppParams.data.order = order;
};

vueAppParams.methods.confirmCloseCall = function (order) {

    order.call = false;
    vueAppParams.data.dialogCloseCall = false;
    

    $.ajax({
        url: "/Home/CloseCall",
        method: "POST",
        data: { orderId: order.id },
        success: function (data) {
            vueAppParams.data.dialogCloseCall = false;
            vueApp.notification.showSuccess(jsglobals.MsgCloseCallOk);
        },
        error: defaultErrorHandler,
    });
};

vueAppParams.methods.closeOrder = function (order) {

    vueAppParams.data.dialogCloseOrder = true;
    vueAppParams.data.order = order;
};

vueAppParams.methods.confirmCloseOrder = function (order) {

    order.payment = false;
    order.active = false;
    vueAppParams.data.dialogCloseOrder = false;

    $.ajax({
        url: "/Home/CloseOrder",
        method: "POST",
        data: { orderId: order.id},
        success: function (data) {
            vueAppParams.data.dialogCloseOrder = false;
            vueApp.notification.showSuccess(jsglobals.MsgCloseOrderOk);
            location.reload();
        },
        error: defaultErrorHandler,
    });
};







