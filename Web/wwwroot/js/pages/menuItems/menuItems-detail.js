vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.itemChange = "";
vueAppParams.data.dialogChangeState = null;
vueAppParams.data.breadcrumbs = [];
vueAppParams.data.categories = [];
vueAppParams.data.loadingCategories = false;

vueAppParams.mounted = function () {
    this.breadcrumbs = [
        { text: jsglobals.Home, disabled: false, href: '/Home/Index' },
        { text: jsglobals.MenuItems, disabled: false, href: '/MenuItems/List' },
        { text: jsglobals.Detail, href: '', disabled: true }
    ];
    if (this.model.Id == 0) {
        this.model.Active = true;
    }

    this.loadCategories();
};

vueAppParams.methods.isDisabled = function () {
    return !this.model.Id == 0;
}

vueAppParams.methods.loadCategories = function () {

    vueAppParams.data.loadingCategories = true;

    $.ajax({
        url: "/MenuItems/GetAllCategories",
        method: "GET",
        success: function (data) {
            vueApp.categories = data.content;
            vueAppParams.data.loadingCategories = false;

        },
        error: defaultErrorHandler
    })
};


vueAppParams.methods.changeState = function () {

    vueAppParams.data.dialogChangeState = true;
    vueAppParams.data.itemChange = this.model;
};

vueAppParams.methods.confirmStateChange = function (itemChange) {

    if (itemChange.Active) {
        itemChange.Active = false;
        vueAppParams.data.dialogChangeState = false;
    }
    else {
        itemChange.Active = true;
        vueAppParams.data.dialogChangeState = false;
    }

};

vueAppParams.methods.saveMenuItem = function () {

    vueApp.isValid = vueApp.$refs.form.validate();
    if (!vueApp.isValid) {
        return false;
    }

    vueApp.clearErrors();
    vueApp.submiting = true;

    if (this.model.Id == 0) {

        $.ajax({
            url: "/MenuItems/Add",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MsgCreationOk);
                setTimeout(function () { window.location = '/MenuItems/List' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler
        });
    }
    else {

        $.ajax({
            url: "/MenuItems/Update",
            method: "POST",
            data: vueApp.model,
            success: function (data) {
                vueApp.notification.showSuccess(jsglobals.MsgUpdateOk);
                setTimeout(function () { window.location = '/MenuItems/List' }, TIEMPO_CIERRE_FORMULARIO);
            },
            error: defaultErrorHandler

        });
    }
};


vueAppParams.methods.goBack = function () {
    window.location = "/MenuItems/List";
};

vueAppParams.methods.UploadImage = function () {

    if (vueApp.model.CategoryId == null) {
        vueApp.notification.showWarning("Debe seleccionar una categoría");
        return
    }

    vueApp.model.categoryName = vueApp.categories.find(cat => cat.Id == vueApp.model.categoryId).name;


    var formData = new FormData();

        input = $('input[type=file]')[0];
    
        var stringName = vueApp.model.categoryName + "\\" + input.files[0].name;
        formData.append(stringName, input.files[0]);

    $.ajax({
        method: 'POST',
        cache: false,
        contentType: false,
        processData: false,
        data: formData,
        url: "/MenuItems/AddPicture",
        cache: false,
        success: function (data) {
            vueApp.model.ImageUrl = "\\images\\MenuItems\\" + stringName;
        },
        error: defaultErrorHandler
        
    });
}