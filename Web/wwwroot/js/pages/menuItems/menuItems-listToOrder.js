vueAppParams.data.counter = 0;
vueAppParams.data.category = '';
vueAppParams.data.menuItems = [];
vueAppParams.data.categoriesAndItems = [];
vueAppParams.data.categ = [];
vueAppParams.data.filteredCategories = [];
vueAppParams.data.table = { id: '', number: '' };
vueAppParams.data.tables = [];
vueAppParams.data.loadingCategories = false;
vueAppParams.data.loadingMenu = false;
vueAppParams.data.loadingBebidas = false;
vueAppParams.data.loadingTables = false;
vueAppParams.data.dialogBebidas = false;
vueAppParams.data.bebidasCategorias = [];
vueAppParams.data.bebidasItems = [];
vueAppParams.data.bebidasFiltradas = [];
vueAppParams.data.salsas = [];
vueAppParams.data.tab = null;
vueAppParams.data.submitting = false;

vueAppParams.data.headers = [
	{ value: 'id', align: ' d-none' },
	{ text: jsglobals.Bebida, value: 'name', sortable: true, class: 'selfResto-headers' },
	{ text: jsglobals.Price, value: 'price', align: 'center', sortable: true, class: 'selfResto-headers' },
	{ text: jsglobals.Add, value: 'quantity', align: 'start', sortable: false, class: 'selfResto-headers' },
];

vueAppParams.mounted = function () {
	this.loadItems();
	this.loadBebidas();
};

vueAppParams.methods.changeCounter = function (catId, id, num) {
	var indexCat = vueAppParams.data.categoriesAndItems.findIndex(cat => cat.id == catId);
	var indexItem = vueAppParams.data.categoriesAndItems[indexCat].menuItems.findIndex(mi => mi.id == id);
	var cantidad = vueAppParams.data.categoriesAndItems[indexCat].menuItems[indexItem].quantity
	if (cantidad != 10 && num > 0) { 
		vueAppParams.data.categoriesAndItems[indexCat].menuItems[indexItem].quantity += num
	};
	if (cantidad <= 10 && cantidad > 0 && num < 0) {
		vueAppParams.data.categoriesAndItems[indexCat].menuItems[indexItem].quantity += num
	};
};

vueAppParams.methods.changeCounterBebidas = function (id, num) {
	var indexBeb = vueAppParams.data.bebidasItems.findIndex(beb => beb.id == id);
	var cantidad = vueAppParams.data.bebidasItems[indexBeb].quantity
	if (cantidad != 10 && num > 0) {
		vueAppParams.data.bebidasItems[indexBeb].quantity += num
	};
	if (cantidad <= 10 && cantidad > 0 && num < 0) {
		vueAppParams.data.bebidasItems[indexBeb].quantity += num
	};
};

vueAppParams.methods.loadItems = function () {

	vueAppParams.data.loadingMenu = true;

	$.ajax({
		url: "/MenuItems/GetAllCatAndMenuItems",
		method: "POST",
		success: function (data) {
			vueAppParams.data.loadingMenu = false;
			vueApp.categoriesAndItems = data.content.filter(mi => mi.id != 3 && mi.id < 9);
			vueApp.salsas = data.content[2].menuItems;
			vueApp.bebidasCategorias = data.content.filter(mi => mi.id >= 9)
		},
		error: defaultErrorHandler
	})
};

vueAppParams.methods.loadBebidas= function () {

	vueAppParams.data.loadingBebidas = true;

	$.ajax({
		url: "/MenuItems/GetAll",
		method: "GET",
		success: function (data) {
			vueApp.menuItems = data.content;
			vueAppParams.data.bebidasItems = data.content.filter(mi => mi.categoryId >= 8);
			vueAppParams.data.loadingBebidas = false;

		},
		error: defaultErrorHandler
	})
};

vueAppParams.methods.verBebidas = function (cat) {

	vueAppParams.data.dialogBebidas = true;
	vueAppParams.data.bebidasFiltradas = vueAppParams.data.bebidasItems.filter(mi => mi.categoryId == cat);
};


vueAppParams.methods.addItem = function (catId, id, quantity, idSalsa) {

	if (catId == 2 && idSalsa == undefined) {
		vueApp.notification.showWarning(jsglobals.MissingSauce);
		return
    }

	$.ajax({
		url: "/Orders/AddItem/",
		data: { orderId: vueApp.model.OrderId, itemId: id, quantity: quantity, idSalsa: idSalsa },
		method: "POST",
		success: function (data) {
			vueAppParams.data.dialogBebidas = false;
			vueAppParams.data.categoriesAndItems.filter(cat => cat.id == catId)[0].menuItems.filter(mi => mi.id == id)[0].quantity = 0;
			vueAppParams.data.categoriesAndItems.filter(cat => cat.id == catId)[0].menuItems.filter(mi => mi.id == id)[0].relatedMenuItemId = undefined;
		},
		error: defaultErrorHandler,
		complete: function () {
			vueAppParams.data.dialogBebidas = false;
		}
	});
};

vueAppParams.methods.addItemBebida = function (bebidasFiltradas) {

	bebidasFiltradas.forEach(function (bebida) {

		if (bebida.quantity > 0) {

			$.ajax({
				url: "/Orders/AddItem/",
				data: { orderId: vueApp.model.OrderId, itemId: bebida.id, quantity: bebida.quantity },
				method: "POST",
				success: function (data) {
					vueAppParams.data.dialogBebidas = false;
					vueAppParams.data.bebidasFiltradas.filter(bf => bf.id == bebida.id)[0].quantity = 0;
				},
				error: defaultErrorHandler,
				complete: function () {
					vueAppParams.data.dialogBebidas = false;
				}
			});
		}
	})
};