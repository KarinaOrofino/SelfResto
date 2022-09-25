vueAppParams.data.categories = [];
vueAppParams.data.filteredCategories = [];
vueAppParams.data.panel = [];
vueAppParams.data.panelItems = 0;

vueAppParams.methods.expandAll = function () {
	this.panel = [...Array(this.panelItems).keys()].map((k, i) => i)
};

vueAppParams.methods.collapseAll = function () {
	this.panel = [];
};


vueAppParams.mounted = function () {
	this.getCategories();
};

vueAppParams.methods.getCategories = function () {


	$.ajax({
		url: "/MenuItems/GetAllCategories",
		method: "GET",
		success: function (data) {
			vueApp.categories = data.content;
			vueApp.filteredCategories = vueApp.filter(vueApp.categories, vueApp.model.OrderDetails);
			vueAppParams.data.panelItems = vueApp.filteredCategories.length;
			vueApp.expandAll();
		},
		error: defaultErrorHandler
	})
};

vueAppParams.methods.filter = function (arr1, arr2) {
	let res = [];
	res = arr1.filter(elem1 => {
		return arr2.find(elem2 => {
			return elem2.MenuItemCategoryId === elem1.id;
		});
	});
	return res;
};

//vueAppParams.methods.goToMenu = function () {

//		window.location = "/MenuItems/ListToOrder/" + vueApp.model.Id;

//};