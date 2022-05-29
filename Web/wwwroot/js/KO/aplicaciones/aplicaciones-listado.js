//CONSTANTES
const TODOS = "todos";
ESTADO_TODOS = 0;

// Modelo
vueAppParams.data.recetasPorPagina = 10;
vueAppParams.data.gridData = [];
vueAppParams.data.gridHistorico = [];
vueAppParams.data.listaCalidad = [];
vueAppParams.data.dialog = null;
vueAppParams.data.itemHistorico = "";
vueAppParams.data.dialogHistorico = null;
vueAppParams.data.itemDelete = "";
vueAppParams.data.loadingRecetas = true;
vueAppParams.data.nombre = '';
vueAppParams.data.cestas = '';
vueAppParams.data.validoEn = '';
vueAppParams.data.menuEn = false;
vueAppParams.data.grupoGrado = '';
vueAppParams.data.listaGrupo = [];
vueAppParams.data.grado = '';
vueAppParams.data.listaGrado = [];
vueAppParams.data.listaGradoFiltrado = [];
vueAppParams.data.activo = "0";
vueAppParams.data.esReserva = false;
vueAppParams.data.primeraCarga = true;
vueAppParams.data.listaCompleta = [];
vueAppParams.data.pagina = 1;
vueAppParams.data.filtros = {
	nombre: vueAppParams.data.nombre,
	grupoGrado: vueAppParams.data.grupoGrado,
	grado: vueAppParams.data.grado,
	cestas: vueAppParams.data.cestas,
	validoEn: vueAppParams.data.validoEn,
	esReserva: vueAppParams.data.esReserva,
	activo: vueAppParams.data.activo
};

vueAppParams.data.breadcrums = [
	{ text: jsglobals.Recetas, disabled: false, href: '/Recetas/Listado' },
	{ text: jsglobals.Listado, disabled: true, href: '' }
];

vueAppParams.data.headers = [
	{ value: 'idReceta', align: ' d-none' },
	{
		text: jsglobals.Nombre, value: 'nombre', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.Grado, value: 'grado.nombre', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.Grupo, value: 'grupoGrado.nombre', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.Cestas, value: 'cestas', align: 'center',
		class: 'protevac-headers'
	},
	{
		text: jsglobals.ValidoDesde, value: 'validoDesde', align: 'center',
		class: 'protevac-headers'
	},
	{
		text: 'Estado', value: 'activo', align: 'center', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: 'Acciones', value: 'actions', align: 'start', sortable: false,
		class: 'protevac-headers', width: '150px'
	},

];

vueAppParams.data.headersHistorico = [
	{ value: 'idReceta', align: ' d-none' },
	{
		text: jsglobals.Nombre, value: 'nombre', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.UsuarioAlta, value: 'usuarioAlta', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.FechaAlta, value: 'fechaAlta', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.UsuarioModificacion, value: 'usuarioModificacion', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.FechaModificacion, value: 'fechaModificacion', align: 'start', sortable: true,
		class: 'protevac-headers'
	},
	{
		text: jsglobals.Cestas, value: 'cestas', align: 'center',
		class: 'protevac-headers'
	},
	{
		text: jsglobals.ValidoDesde, value: 'validoDesde', align: 'center',
		class: 'protevac-headers'
	},
	{
		text: jsglobals.Reserva, value: 'reserva', align: 'center',
		class: 'protevac-headers'
	},
	{
		text: 'Estado', value: 'activo', align: 'center', sortable: true,
		class: 'protevac-headers'
	}
];


// Mounted
vueAppParams.mounted = async function () {
	await this.obtenerParametros();
	await this.cargarListaCalidad();
	this.loadGrid();
};

// Metodos
vueAppParams.methods.onClickNuevo = function (event) {
	window.location = "Detalle/";
};

vueAppParams.methods.onClickLimpiarFiltros = function (filterName) {

	if (filterName == TODOS) {

		vueAppParams.data.filtros.nombre = '';
		vueAppParams.data.filtros.grupoGrado = '';
		vueAppParams.data.filtros.grado = '';
		vueAppParams.data.filtros.cestas = '';
		vueAppParams.data.filtros.validoEn = '';
		vueAppParams.data.filtros.esReserva = false;
		vueAppParams.data.filtros.activo = '0';
		vueApp.actualizarCalidad();
	}
	else {

		if (filterName) {
			if (filterName == 'activo') {
				vueApp.filtros[filterName] = '0';
			} else if (filterName == 'grupoGrado'){
				vueApp.filtros['grado'] = '';
				vueApp.filtros[filterName] = '';
				vueApp.actualizarCalidad();
            }
			else {
				vueApp.filtros[filterName] = '';
			}
		}
	}

	this.loadGrid();
};

vueAppParams.methods.cambioReserva = function () {
	if (vueApp.filtros.Reserva) {
		vueAppParams.data.filtros.grado = 0;
		vueAppParams.data.filtros.grupoGrado = 0;
		vueAppParams.data.filtros.Cestas = 1;
	}
}

vueAppParams.methods.obtenerParametros = async function () {
	await $.ajax({
		url: "/Recetas/ObtenerParametros",
		method: "GET",
		success: function (data) {
			vueApp.recetasPorPagina = data.content;
		},
		error: defaultErrorHandler
	});
}

vueAppParams.methods.loadGrid = function () {

	$.ajax({
		url: "/Recetas/ObtenerRecetas",
		data: { nombre: vueAppParams.data.filtros.nombre, grupoGrado: parseInt(vueAppParams.data.filtros.grupoGrado) ,grado: vueAppParams.data.filtros.grado, cestas: vueAppParams.data.filtros.cestas, validoEn: vueAppParams.data.filtros.validoEn, esReserva: vueAppParams.data.filtros.esReserva, activo: vueAppParams.data.filtros.activo },
		method: "POST",
		success: function (data) {
			vueApp.gridData = data.content;
			if (vueApp.primeraCarga) {
				vueApp.listaCompleta = [...data.content];
				vueApp.primeraCarga = false;
			}
			vueApp.gridData.forEach(x => x.grado = x.grado == -1 ? new Object({ id: x.grado, nombre: "Sin Grado" }) : new Object({ id: x.grado, nombre: vueApp.listaGradoFiltrado.find(y => y.codigoCalidad == x.grado)?.calidad ?? 'GRADO NO ENCONTRADO' }));
			vueApp.gridData.forEach(x => x.grupoGrado = new Object({ id: x.grupoGrado, nombre: vueApp.listaGrupo.find(y => y.codigoGrupo == x.grupoGrado).nombre }));
			vueApp.loadingRecetas = false;
		},
		error: defaultErrorHandler
	});
};

vueAppParams.methods.cargarListaCalidad = async function () {
	await $.ajax({
		url: "/Recetas/ObtenerListaCalidad",
		method: "GET",
		success: function (data) {
			vueApp.listaCalidad = data.content;
			vueApp.listaGrupo = [...new Map(vueApp.listaCalidad.map(item => [item["codigoGrupo"], new Object({ "nombre": item.nombre, "codigoGrupo": item.codigoGrupo })])).values()];
			vueApp.listaGrado = vueApp.listaCalidad.map(item => new Object({ "calidad": item.calidad, "codigoCalidad": item.codigoCalidad }));
			vueApp.listaGrado.push(new Object({ "calidad": "Sin Grado", "codigoCalidad": 0 }));
			vueApp.listaGradoFiltrado = vueApp.listaCalidad.map(item => new Object({ "calidad": item.calidad, "codigoCalidad": item.codigoCalidad }));
			vueApp.listaGradoFiltrado.push(new Object({ "calidad": "Sin Grado", "codigoCalidad": 0 }));
		},
		error: defaultErrorHandler
	})
};

vueAppParams.methods.actualizarCalidad = function () {
	vueApp.listaGradoFiltrado = vueApp.filtros.grupoGrado == '' ? vueApp.listaGrado : vueApp.listaCalidad.filter(item => item.codigoGrupo == vueApp.filtros.grupoGrado);
}

vueAppParams.methods.onClickVerHistorico = function (item) {
	vueAppParams.data.dialogHistorico = true;
	vueAppParams.data.itemHistorico = item;
	vueAppParams.data.loadingRecetas = true;

	$.ajax({
		url: "/Recetas/ObtenerRecetas",
		data: { nombre: null, grupoGrado: parseInt(item.grupoGrado.id), grado: item.grado.id, cestas: null, validoEn: null, esReserva: null, activo: null },
		method: "POST",
		success: function (data) {
			vueApp.gridHistorico = data.content;
			vueApp.loadingRecetas = false;
		},
		error: defaultErrorHandler
	});
}

vueAppParams.methods.onClickInactivar = function (item) {

	vueAppParams.data.dialog = true;
	vueAppParams.data.itemDelete = item;
};

vueAppParams.methods.onClickEliminar = function (item) {

	vueAppParams.data.dialog = true;
	vueAppParams.data.itemDelete = item;
};

vueAppParams.methods.onClickConfirmaBorrar = function (id) {
	let recetaABorrar = vueApp.gridData.find(y => y.idReceta == id);
	if (recetaABorrar.activo && (recetaABorrar.grado.id == 0 || recetaABorrar.grado.id == -1 ) && vueApp.listaCompleta.filter(x => x.grupoGrado.id == recetaABorrar.grupoGrado.id && x.activo == true).length < 2) {
		vueApp.notification.showError(jsglobals.MensajeSinOtroGrupoActivo);
		return false;
    }
	if (recetaABorrar.activo && vueApp.listaCompleta.filter(x => x.grado.id == recetaABorrar.grado.id && x.activo == true).length < 2) {
		vueApp.notification.showError(jsglobals.MensajeSinOtroGradoActivo);
		return false;
	}

	vueApp.clearErrors();
	vueApp.submiting = true;
	vueAppParams.data.dialog = false;

	$.ajax({
		url: "/Recetas/Inactivar",
		data: { "id" : id },
		method: "POST",
		success: function (data) {
			vueApp.notification.showSuccess(jsglobals.MensajeDatosEliminadosOk);
			setTimeout(function () { window.location = '/Recetas/Listado' });
		},
		error: defaultErrorHandler
	});

};

vueAppParams.methods.onClickConfirmaEliminar = function (id) {

	vueApp.clearErrors();
	vueApp.submiting = true;
	vueAppParams.data.dialog = false;

	$.ajax({
		url: "/Recetas/Eliminar",
		data: { "id": id },
		method: "POST",
		success: function (data) {
			vueApp.notification.showSuccess(jsglobals.MensajeDatosEliminadosOk);
			setTimeout(function () { window.location = '/Recetas/Listado' });
		},
		error: defaultErrorHandler
	});

};

vueAppParams.methods.onClickEditar = function (id) {
	window.location = "/Recetas/Detalle/" + id;
};

vueAppParams.methods.editarPantallaClick = function (event, { item }) {
	vueApp.onClickEditar(item.idReceta);
}

vueAppParams.methods.onClickExportar = function () {
	var filtros = "?nombre=" + vueAppParams.data.filtros.nombre + "&grupoGrado=" + parseInt(vueAppParams.data.filtros.grupoGrado)
		+ "&grado=" + vueAppParams.data.filtros.grado + "&cestas=" + vueAppParams.data.filtros.cestas
		+ "&validoEn=" + vueAppParams.data.filtros.validoEn + "&esReserva=" + vueAppParams.data.filtros.esReserva + "&activo=" + vueAppParams.data.filtros.activo;


	return new Promise(resolve => {

		var urlToSend = "/Recetas/Exportar" + filtros;
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
			var fileName = "Recetas_" + fechaLocalSlash;
			var link = document.createElement("a");
			link.href = window.URL.createObjectURL(blob);
			link.download = fileName;
			link.click();

		};

		req.send()

		resolve(req.status);
	});
};

vueAppParams.methods.onClickExportarHistorial = function () {
	var filtros = "?grupoGrado=" + parseInt(vueApp.itemHistorico.grupoGrado.id)
		+ "&grado=" + parseInt(vueApp.itemHistorico.grado.id);


	return new Promise(resolve => {

		var urlToSend = "/Recetas/ExportarHistorial" + filtros;
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
			var fileName = "Recetas_Historico_" + vueApp.itemHistorico.grupoGrado.nombre + "_" + vueApp.itemHistorico.grado.nombre + "_" + fechaLocalSlash;
			var link = document.createElement("a");
			link.href = window.URL.createObjectURL(blob);
			link.download = fileName;
			link.click();

		};

		req.send()

		resolve(req.status);
	});
};

vueAppParams.methods.inactivoOscuro = function (item) {
	return item.activo ? '' : 'grey lighten-2  black--text';
}