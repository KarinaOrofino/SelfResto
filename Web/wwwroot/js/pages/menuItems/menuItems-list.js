//Model
vueAppParams.data.gridData = [];
vueAppParams.data.filteredList = [];
vueAppParams.data.loadingMenuItems = true;
vueAppParams.data.loadingExport = false;
vueAppParams.data.dialogActivate = false;
vueAppParams.data.dialogInactivate = false;
vueAppParams.data.itemToActivate = '';
vueAppParams.data.itemToInactivate = '';
vueAppParams.data.seeMenuItem= false;
vueAppParams.data.itemChange = "";
vueAppParams.data.breadcrumbs = [];
vueAppParams.data.search = '';
vueAppParams.data.filters = { state: '', };
vueAppParams.data.filters.state = 0;

vueAppParams.data.breadcrumbs = [
    { text: jsglobals.Home, disabled: false, href: '/Home/IndexEmployee' },
    { text: jsglobals.MenuItems, disabled: true, class: 'breadcrumbInactive' },
    { text: jsglobals.List, disabled: true }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};

vueAppParams.data.headers = [

    { value: 'id', align: ' d-none' },
    { value: 'categoryid', align: 'd-none' },
    { text: jsglobals.VisualizationOrder, value: 'visualizationOrder', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.Name, value: 'name', align: 'center', class: 'selfResto-headers' },
    //{ text: jsglobals.Description, value: 'description', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.Price, value: 'price', align: 'right', class: 'selfResto-headers' },
    { text: jsglobals.State, value: 'active', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.Actions, value: 'actions', align: 'center', class: 'selfResto-headers' }

];

// Metodos

vueAppParams.methods.filterByState = function () {

    if (vueAppParams.data.filters.state === 0) {
        vueApp.filteredList = vueApp.gridData;
    }
    else {
        vueApp.filteredList = vueApp.gridData.filter(m => m.active == vueAppParams.data.filters.state);
    }
};

vueAppParams.methods.clean = function () {
    vueAppParams.data.search = '';
    vueAppParams.data.filters.state = 0;
    vueApp.filteredList = vueApp.gridData;
};


vueAppParams.methods.loadGrid = function () {

    $.ajax({
        url: "/MenuItems/GetAll",
        method: "GET",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.filteredList = vueApp.gridData;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.loadingMenuItems = false;
        }
    }).done(() => {
        vueApp.loadingMenuItems = false;
    });
};

// Metodos
vueAppParams.methods.addMenuItem = function () {

    window.location = "/MenuItems/Detail";
};

vueAppParams.methods.inactivateMenuItem = function (item) {

    vueAppParams.data.dialogInactivate = true;
    vueAppParams.data.itemToInactivate = item;
};

vueAppParams.methods.confirmInactivation = function (id) {

    vueAppParams.data.dialogInactivate = false;

    var menuItemIdIndex = vueApp.gridData.findIndex(mi => mi.id == id)
    vueApp.gridData[menuItemIdIndex].active = false;

    $.ajax({
        url: "/MenuItems/Inactivate?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgInactivationOk);
            setTimeout(function () { /*window.location = '/Tables/List'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.activateMenuItem = function (item) {

    vueAppParams.data.dialogActivate = true;
    vueAppParams.data.itemToActivate = item;
};

vueAppParams.methods.confirmActivation = function (id) {

    vueAppParams.data.dialogActivate = false;

    var menuItemIdIndex = vueApp.gridData.findIndex(mi => mi.id == id);
    vueApp.gridData[menuItemIdIndex].active = true;

    $.ajax({
        url: "/MenuItems/Activate?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgActivationOk);
            setTimeout(function () { /*window.location = '/Tables/List'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.editMenuItem= function (id) {

    window.location = "/MenuItems/Detail/" + id;
};

vueAppParams.methods.seeTables = function () {

    location = "/Home/IndexEmployee";
};

vueAppParams.methods.exportList = function () {
    vueApp.loadingExport = true;

    return new Promise(resolve => {

        const term = this.search.toLowerCase();

        var urlToSend = "/MenuItems/Export?searchField=" + term + "&state=" + vueAppParams.data.filters.state;

        var req = new XMLHttpRequest();
        req.open("GET", urlToSend, true);
        req.responseType = "blob";
        req.onload = function (event) {
            var blob = req.response;
            if (req.status == HTTP_ERROR) {
                vueApp.notification.showError(jsglobals.GenericError);
                vueApp.loadingExport = false;
                return;
            }
            var fecha = new Date();
            var fechaLocal = fecha.toLocaleDateString();
            var fechaLocalSlash = fechaLocal.replaceAll("/", "-")
            var fileName = "MenuItemsReport_" + fechaLocalSlash;
            var link = document.createElement("a");
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;
            link.click();
            vueApp.loadingExport = false;
        };

        req.send();

        resolve(req.status);
    });
};




