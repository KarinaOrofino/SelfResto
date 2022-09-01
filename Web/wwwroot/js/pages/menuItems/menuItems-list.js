vueAppParams.data.counter = 0;
vueAppParams.data.category = '';
vueAppParams.data.menuItems = [];
vueAppParams.data.categoriesAndItems = [];
vueAppParams.data.categ = [];
vueAppParams.data.filteredCategories = [];
vueAppParams.data.loadingCategories = false;
vueAppParams.data.loadingMenu = false;
vueAppParams.data.loadingBebidas = false;
vueAppParams.data.dialogBebidas = false;
vueAppParams.data.bebidasCategorias = [];
vueAppParams.data.bebidasItems = [];
vueAppParams.data.bebidasFiltradas = [];
vueAppParams.data.salsas = [];
vueAppParams.data.tab = null;

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

vueAppParams.methods.addItem = function (id) {

	$.ajax({
		url: "/Order/AddItem",
		data: { itemId: id },
		method: "POST",
		success: function (data) {
			vueAppParams.data.dialogBebidas = false;
		},
		error: defaultErrorHandler,
		complete: function () {
			vueAppParams.data.dialogBebidas = false;
		}
	});
};