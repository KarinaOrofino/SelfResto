//Model
vueAppParams.data.gridData = [];
vueAppParams.data.filteredList = [];
vueAppParams.data.loadingUsers= true;
vueAppParams.data.loadingExport = false;
vueAppParams.data.dialogActivate = false;
vueAppParams.data.dialogInactivate = false;
vueAppParams.data.dialog2 = null;
vueAppParams.data.userToActivate = '';
vueAppParams.data.userToInactivate = '';
vueAppParams.data.itemChange = "";
vueAppParams.data.breadcrumbs = [];
vueAppParams.data.search = '';
vueAppParams.data.filters = { state: '', };
vueAppParams.data.filters.state = 0;

vueAppParams.data.breadcrumbs = [
    { text: jsglobals.Home, disabled: false, href: '/Home/IndexEmployee'},
    { text: jsglobals.Users, disabled: true, class:'breadcrumbInactive' },
    { text: jsglobals.List, disabled: true }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
};

vueAppParams.data.headers = [

    { value: 'id', align: ' d-none' },
    { text: jsglobals.Name, value: 'name', align: 'center', class: 'selfResto-headers'},
    { text: jsglobals.Surname, value: 'surname', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.Email, value: 'email', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.AccessType, value: 'accessTypeName', align: 'center', class: 'selfResto-headers' },
    { text: jsglobals.State, value: 'active', align: 'center', class: 'selfResto-headers'},
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
        url: "/Users/GetAll",
        method: "GET",
        success: function (data) {
            vueApp.gridData = data.content;
            vueApp.filteredList = vueApp.gridData;
        },
        error: defaultErrorHandler,
        complete: function () {
            vueApp.loadingUsers = false;
        }
    }).done(() => {
        vueApp.loadingUsers = false;
    });
};

// Metodos
vueAppParams.methods.addUser = function () {

    window.location = "/Users/Detail";
};

vueAppParams.methods.inactivateUser = function (item) {

    vueAppParams.data.dialogInactivate = true;
    vueAppParams.data.userToInactivate = item;
};

vueAppParams.methods.confirmInactivation = function (id) {

    vueAppParams.data.dialogInactivate = false;

    var userIdIndex = vueApp.gridData.findIndex(user => user.id == id)
    vueApp.gridData[userIdIndex].active = false;

    $.ajax({
        url: "/Users/Inactivate?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgInactivationOk);
            setTimeout(function () { /*window.location = '/Users/List'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.activateUser = function (item) {

    vueAppParams.data.dialogActivate = true;
    vueAppParams.data.userToActivate = item;
};

vueAppParams.methods.confirmActivation = function (id) {

    vueAppParams.data.dialogActivate = false;

    var userIdIndex = vueApp.gridData.findIndex(user => user.id == id);
    vueApp.gridData[userIdIndex].active = true;

    $.ajax({
        url: "/Users/Activate?id=" + id,
        method: "GET",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MsgActivationOk);
            setTimeout(function () { /*window.location = '/Medicos/Listado'*/ });
        },
        error: defaultErrorHandler
    });

};

vueAppParams.methods.editUser = function (id) {

    window.location = "/Users/Detail/" + id;
  };



vueAppParams.methods.exportList = function () {
    vueApp.loadingExport = true;

    return new Promise(resolve => {

        const term = this.search.toLowerCase();

        var urlToSend = "/Users/Export?searchField=" + term + "&state=" + vueAppParams.data.filters.state;

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
            var fileName = "UsersReport_" + fechaLocalSlash;
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




