// Modelo
vueAppParams.data.colada = '';
vueAppParams.data.coladas = [{ id: '', nombre: '' }];
vueAppParams.data.receta = [{ idReceta: '', nombreReceta: '' }];
vueAppParams.data.Paso = 1;
vueAppParams.data.cestas = [];
vueAppParams.data.loadingCestas = false;
vueAppParams.data.loadingColada = false;
vueAppParams.data.cestaElegida = [];

vueAppParams.data.breadcrums = [

	{ text: jsglobals.CargaCestas, disabled: false, href: '/CargaCestas/Inicio' },
	{ text: jsglobals.Inicio, disabled: true, href: '' }
];

vueAppParams.mounted = function () {

	this.obtenerColadasEntrantes();
}

vueAppParams.methods.obtenerColadasEntrantes = function () {

	$.ajax({
		url: '/CargaCestas/ObtenerColadasEntrantes',
		success: function (data) {
			vueApp.coladas = data.content;
		},
		error: defaultErrorHandler
	});
};

vueAppParams.methods.obtenerColadaYReceta = function (item) {

	vueApp.loadingColada = true;

	$.ajax({
		url: '/CargaCestas/ObtenerColadaYCargas',
		method: 'POST',
		data: { coladaVM: item },
		success: function (data) {
			vueApp.colada = data.content;
			vueApp.Paso = 2;
			vueApp.loadingColada = false;
		},
		error: defaultErrorHandler,
		complete: function () {
			vueApp.loadingColada = false;
		}
	});
};


vueAppParams.methods.obtenerVia = function (item) {

	vueAppParams.data.cestaElegida = item;
	vueAppParams.data.Paso = 3;
}

vueAppParams.methods.volverAPasoAnterior = function () {

	vueAppParams.data.Paso = 1;
	if (vueAppParams.data.Paso == 1) {
		vueAppParams.data.colada = '';
		vueAppParams.data.cestas = [];
	}
};

vueAppParams.methods.comenzarCestas = function () {

	vueAppParams.data.Paso = 4;

	vueAppParams.data.loadingCestas = true;
	$.ajax({
		url: '/CargaCestas/ComenzarCestas',
		data: { cesta: vueAppParams.data.cestaElegida, nombreColada: vueAppParams.data.colada.nombre },
		method: 'POST',
		success: function (data) {
			if (vueAppParams.data.cestaElegida.fechaHoraInicio == null) {
				vueAppParams.methods.abrirCesta(vueAppParams.data.cestaElegida.id);
			}
			else {
				window.location = "/CargaCestas/Detalle/" + vueAppParams.data.cestaElegida.id
			}
		},
		error: defaultErrorHandler,
		complete: function () {
			vueAppParams.data.loadingCestas = false;
		}
	});
};

vueAppParams.methods.abrirCesta = function (id) {

	$.ajax({
		url: "/CargaCestas/AbrirCesta",
		data: { idCarga: id },
		method: "POST",
		success: function (data) {
			vueApp.notification.showSuccess(jsglobals.MensajeComienzoCargaOk);
			setTimeout(function () { window.location = "/CargaCestas/Detalle/" + id });
		},
		error: defaultErrorHandler,
		complete: function () {
			vueAppParams.data.loadingCestas = false;
		}
	});
}