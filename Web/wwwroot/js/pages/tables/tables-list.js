//Model
vueAppParams.data.gridData = [];
vueAppParams.data.filteredList = [];
vueAppParams.data.loadingTables = true;
vueAppParams.data.loadingExport = false;
vueAppParams.data.dialogActivate = false;
vueAppParams.data.dialogInactivate = false;
vueAppParams.data.dialog2 = null;
vueAppParams.data.tableToActivate = '';
vueAppParams.data.tableToInactivate = '';
vueAppParams.data.itemChange = "";
vueAppParams.data.breadcrumbs = [];
vueAppParams.data.search = '';
vueAppParams.data.filters = { state: '', };
vueAppParams.data.filters.state = 0;

vueAppParams.data.breadcrumbs = [
    { text: jsglobals.Home, disabled: false, href: '/Home/IndexEmployee' },
    { text: jsglobals.Tables, disabled: true, class: 'breadcrumbInactive' },
    { text: jsglobals.List, disabled: true }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};

vueAppParams.data.headers = [

    { value: 'id', align: ' d-none' },
    { text: jsglobals.Number, value: 'number', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.Name, value: 'name', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.Description, value: 'description', align: 'center', class: 'selfResto-headers' },
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
        url: "/Tables/GetAll",
        method: "GET",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.filteredList = vueApp.gridData;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.loadingTables = false;
        }
    }).done(() => {
        vueApp.loadingTables = false;
    });
};

// Metodos
vueAppParams.methods.addTable = function () {

    window.location = "/Tables/Detail";
};

vueAppParams.methods.inactivateTable = function (item) {

    vueAppParams.data.dialogInactivate = true;
    vueAppParams.data.tableToInactivate = item;
};

vueAppParams.methods.confirmInactivation = function (id) {

    vueAppParams.data.dialogInactivate = false;

    var tableIdIndex = vueApp.gridData.findIndex(table => table.id == id)
    vueApp.gridData[tableIdIndex].active = false;

    $.ajax({
        url: "/Tables/Inactivate?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgInactivationOk);
            setTimeout(function () { /*window.location = '/Tables/List'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.activateTable = function (item) {

    vueAppParams.data.dialogActivate = true;
    vueAppParams.data.tableToActivate = item;
};

vueAppParams.methods.confirmActivation = function (id) {

    vueAppParams.data.dialogActivate = false;

    var tableIdIndex = vueApp.gridData.findIndex(table => table.id == id);
    vueApp.gridData[tableIdIndex].active = true;

    $.ajax({
        url: "/Tables/Activate?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgActivationOk);
            setTimeout(function () { /*window.location = '/Tables/List'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.editTable= function (id) {

    window.location = "/Tables/Detail/" + id;
};



vueAppParams.methods.exportList = function () {
    vueApp.loadingExport = true;

    return new Promise(resolve => {

        const term = this.search.toLowerCase();

        var urlToSend = "/Tables/Export?searchField=" + term + "&state=" + vueAppParams.data.filters.state;

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
            var fileName = "TablesReport_" + fechaLocalSlash;
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




