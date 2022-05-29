//CONSTANTES
const TODOS = 'todos';

// Modelo
vueAppParams.data.gridData = [];
vueAppParams.data.dialog = null;
vueAppParams.data.itemDelete = "";
vueAppParams.data.loadingFuncionalidades = true;
vueAppParams.data.descripcion = '';
vueAppParams.data.activo = 'true';
vueAppParams.data.filtros = {
	descripcion: vueAppParams.data.descripcion,
	activo: vueAppParams.data.activo
};

vueAppParams.data.breadcrums = [
	{ text: jsglobals.Funcionalidades, disabled: false, href: '/Funcionalidad/Listado' },
	{ text: jsglobals.Listado, disabled: true, href: '' }
];

vueAppParams.data.headers = [
	{ value: 'id', align: ' d-none' },
	{ text: jsglobals.Funcionalidad, value: 'descripcion', sortable: true, class: 'protevac-headers' },
	{ text: jsglobals.Estado, value: 'activo', align: 'center', sortable: true, class: 'protevac-headers'},
	{ text: jsglobals.Acciones, value: 'actions', align: 'start', sortable: false, class: 'protevac-headers'},

];

// Mounted
vueAppParams.mounted = function () {
	this.loadGrid();
};


// Metodos
vueAppParams.methods.onClickNueva = function (event) {
	window.location = "Detalle/";
};

vueAppParams.methods.onClickLimpiarFiltros = function (filterName) {

	if (filterName == TODOS) {

		vueAppParams.data.filtros.descripcion = ''
		vueAppParams.data.filtros.activo = 'true';
	}

	else {

		if (filterName) {
			if (filterName == 'activo') {
				vueApp.filtros[filterName] = '0';
			}
			else {
				vueApp.filtros[filterName] = '';

			}
		}
	}

	this.loadGrid();
};

vueAppParams.methods.loadGrid = function () {

	$.ajax({
		url: "/Funcionalidad/ObtenerFuncionalidades",
		data: {
			descripcion: vueAppParams.data.filtros.descripcion,
			activo: vueAppParams.data.filtros.activo
		},
		method: "POST",
		success: function (data) {
			vueApp.gridData = data.content;
			vueApp.loadingFuncionalidades = false;

		},
		error: defaultErrorHandler
	})
};

vueAppParams.methods.onClickInactivar = function (item) {

	vueAppParams.data.dialog = true;
	vueAppParams.data.itemDelete = item;
};

vueAppParams.methods.onClickConfirmaBorrar = function (id) {

	vueApp.clearErrors();
	vueApp.submiting = true;
	vueAppParams.data.dialog = false;

	$.ajax({
		url: "/Funcionalidad/Inactivar?funcionalidadId=" + id,
		method: "GET",
		success: function (data) {
			vueApp.notification.showSuccess(jsglobals.MensajeDatosEliminadosOk);
			setTimeout(function () { window.location = '/Funcionalidad/Listado' });
		},
		error: defaultErrorHandler
	});

};

vueAppParams.methods.onClickEditar = function (id) {

	window.location = "/Funcionalidad/Detalle/" + id;
};

vueAppParams.methods.onClickExportar = function () {

	return new Promise(resolve => {

		var urlToSend = "/Funcionalidad/Exportar?descripcion=" + vueApp.filtros.descripcion + "&activo=" +vueApp.filtros.activo;
		var req = new XMLHttpRequest();
		req.open("GET", urlToSend, true);
		req.responseType = "blob";
		req.onload = function (event) {
			var blob = req.response;
			if (req.status == HTTP_ERROR) {
				vueApp.notification.showError(jsglobals.ErrorGenerico);
				return;
			}
				var fecha = new Date();
				var fechaLocal = fecha.toLocaleDateString();
				var fechaLocalSlash = fechaLocal.replaceAll("/", "-")
				var fileName = "Funcionalidades_" + fechaLocalSlash;
				var link = document.createElement("a");
				link.href = window.URL.createObjectURL(blob);
				link.download = fileName;
				link.click();
			
		};

		req.send()

		resolve(req.status);
	});
};
