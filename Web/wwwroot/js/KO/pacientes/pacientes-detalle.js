//CONSTANTES

// Modelo
vueAppParams.data.interval = {};
vueAppParams.data.value = 0;
vueAppParams.data.dialogActivacion = false;
vueAppParams.data.dialogTomarControl = false;
vueAppParams.data.dialogCierreCesta = false;
vueAppParams.data.cesta = "";
vueAppParams.data.colada = '';
vueAppParams.data.coladas = [{ id: '', nombre: '' }];
vueAppParams.data.receta = [{ idReceta: '', nombreReceta: '' }];
vueAppParams.data.cestas = [];
vueAppParams.data.viaActual = 0;
vueAppParams.data.cestaActiva = [];
vueAppParams.data.loadingCestas = false;
vueAppParams.data.comenzandoCarga = false;
vueAppParams.data.cambiandoControl = false;
vueAppParams.data.loadingCerrarCesta = false;
vueAppParams.data.CapaSinCargar = false;
vueAppParams.data.evento = '';
vueAppParams.data.expand = false;

/*Variables de peso de balanza*/
vueAppParams.data.pesoBalanza = [];
vueAppParams.data.totalBalanza = 0;
vueAppParams.data.balanzaActiva = 0;
vueAppParams.data.show = false;
vueAppParams.data.editBalanza = 0;
vueAppParams.data.subiendoBalanza = false;
vueAppParams.data.curvasActivas = 0;
vueAppParams.data.rectasActivas = 0;
/**/

vueAppParams.mounted = function () {

	this.obtenerCapasTodasLasCestas();

};

vueAppParams.data.breadcrums = [

	{ text: jsglobals.CargaCestas, disabled: false, href: '/CargaCestas/Inicio' },
	{ text: jsglobals.CargaActual, disabled: true, href: '' }
];

vueAppParams.methods.obtenerCapasTodasLasCestas = function () {

	vueAppParams.data.loadingCestas = true;

	$.ajax({
		url: '/CargaCestas/ObtenerCapasTodasLasCestas',
		data: {idReceta: vueAppParams.data.model.IdReceta, idColada : vueAppParams.data.model.Id},
		method: 'POST',
		success: function (data) {
			vueApp.cestas = data.content;
			vueApp.colorearCestasYCapas();
			vueAppParams.data.cestaActiva = vueAppParams.data.cestas.filter(c => c.activo && c.cestaPropia);
			vueAppParams.data.viaActual = vueAppParams.data.cestaActiva.length > 0 ? vueAppParams.data.cestaActiva[0].idVia : 0;
			vueAppParams.data.loadingCestas = false;
		},
		error: defaultErrorHandler
	});
};

vueAppParams.methods.colorearCestasYCapas = function () {

	for (i = 0; i < vueApp.cestas.length; i++) {

		if (vueApp.cestas[i].activo) {
			vueApp.cestas[i].color = 'teal';
		}
		else if (!vueApp.cestas[i].activo && vueApp.cestas[i].fechaHoraCierre == null) {
			vueApp.cestas[i].color = 'teal grey';
		}
		else {
			vueApp.cestas[i].color = 'teal lighten-4';
		}

		for (j = 0; j < vueApp.cestas[i].listaDetalles.length; j++) {
			var capa = vueApp.cestas[i].listaDetalles[j];
			//Pendiente
			if (vueApp.cestas[i].activo && !capa.activo && capa.peso == 0) {
				capa.color = 'white';
				capa.icon = 'scheduled';
			}
			//Cargando
			else if (capa.activo) {
				capa.color = 'green lighten-1';
				capa.icon = '';
				capa.cargandoCesta = true;
				this.progress = Math.round((100 * capa.peso) / capa.pesoReceta);
				this.value = this.progress;
			}
			//Cargada
			else if (!capa.activo && capa.peso != 0) {
				capa.color = 'blue lighten-4';
				capa.icon = 'check';
			}
			//Sin Cargar, Cesta Cerrada
			else if (vueApp.cestas[i].fechaHoraCierre != null && capa.peso == 0) {
				capa.color = 'blue-grey lighten-5';
			}
		}

		/*lista reducida con numero de cesta y listaPesos, para activar manualmente la balanza*/
		vueAppParams.data.pesoBalanza = vueAppParams.data.cestas.map(x => new Object({
			'cesta': x.numero,
			'listaPesos': x.listaDetalles.map(y => new Object({ 'orden': y.orden, 'peso': y.peso, 'pesoInicial': y.pesoInicial })),
			'activo': false
		}));
		vueApp.curvasActivas = vueApp.cestas[i].idVia == 2 && vueApp.cestas[i].activo ? vueApp.curvasActivas + 1 : vueApp.curvasActivas;
		vueApp.rectasActivas = vueApp.cestas[i].idVia == 1 && vueApp.cestas[i].activo ? vueApp.rectasActivas + 1 : vueApp.rectasActivas;
			/**/
	}
}


vueAppParams.methods.abrirCesta = function (cesta) {

	vueAppParams.data.dialogActivacion = true;
	vueAppParams.data.cesta = cesta;
};

vueAppParams.methods.confirmaAbrirCesta = function (id) {

	$.ajax({
		url: "/CargaCestas/AbrirCesta",
		data: {idCarga : id},
		method: "POST",
		success: function (data) {
			vueApp.comenzandoCarga = false;
			vueAppParams.data.dialogActivacion = false;
			vueApp.notification.showSuccess(jsglobals.MensajeComienzoCargaOk);
			setTimeout(function () { window.location = "/CargaCestas/Detalle?id=" + id});
		},
		error: defaultErrorHandler,
		complete: function () {
				vueApp.comenzandoCarga = false;
				vueAppParams.data.dialogActivacion = false;
		}
	});
};

vueAppParams.methods.tomarControl = function (cesta) {

		vueAppParams.data.dialogTomarControl = true;
		vueAppParams.data.cesta = cesta;
};

vueAppParams.methods.confirmaTomarControl = function (id) {

	$.ajax({
		url: "/CargaCestas/TomarControl",
		data: { idCarga: id },
		method: "POST",
		success: function (data) {
			vueApp.cambiandoControl = false;
			vueAppParams.data.dialogTomarControl = false;
			setTimeout(function () { window.location = "/CargaCestas/Detalle?id=" + id });
		},
		error: defaultErrorHandler,
		complete: function () {
			vueApp.cambiandoControl = false;
			vueAppParams.data.dialogTomarControl = false;
		}
	});
};

vueAppParams.methods.cerrarCesta = function (cesta) {

	for (i = 0; i < cesta.listaDetalles.length; i++) {
		if (cesta.listaDetalles[i].peso == 0) {
			vueAppParams.data.CapaSinCargar = true;
		}
	}
	vueAppParams.data.dialogCierreCesta = true;
	vueAppParams.data.cesta = cesta;
};

vueAppParams.methods.confirmaCierreCesta = function (cesta) {

	vueApp.loadingCerrarCesta = true;

	$.ajax({
		url: "/CargaCestas/CerrarCesta",
		data: { cesta: cesta },
		method: "POST",
		success: function (data) {
			vueAppParams.data.dialogCierreCesta = false;
			vueApp.notification.showSuccess(jsglobals.MensajeCierreCestaOk);
			vueApp.loadingCerrarCesta = false;
			setTimeout(function () { window.location = "/CargaCestas/CargaCestas?id=" + vueAppParams.data.model.Id });
		},
		error: defaultErrorHandler,
		complete: function () {
			vueApp.loadingCerrarCesta = false;
			vueAppParams.data.dialogCierreCesta = false;
			vueAppParams.data.CapaSinCargar = true;
		}
	});

};

vueAppParams.methods.verDetalleCesta = function (id) {

	window.location = "/CargaCestas/Detalle/" + id;
};

vueAppParams.methods.volver = function () {

	window.location = "/CargaCestas/Inicio/";
};

/*selector para mover balanza*/
vueAppParams.methods.activarBalanza = function (cesta) {
	vueAppParams.data.show = true;
	vueAppParams.data.balanzaActiva = cesta;
	vueApp.cestas[cesta - 1].activo = true;
	vueApp.pesoBalanza[cesta - 1].activo = true;
	vueApp.totalBalanza = vueApp.pesoBalanza[cesta - 1].listaPesos.reduce((a, b) => a + b.peso, 0);
	vueApp.cestas[cesta - 1].listaDetalles.forEach(x => x.fechaHoraInicioPeso = new Date().toJSON());

	$.ajax({
		url: "/CargaCestas/ControlarBalanza",
		data: { idCarga: vueApp.cestas[cesta - 1].id },
		method: "POST",
		success: function (data) {
			vueApp.cestas[cesta - 1].usuarioApertura = data.content;
			if (vueApp.cestas[cesta - 1].idVia == 1) {
				vueApp.rectasActivas++;
			} else {
				vueApp.curvasActivas++;
            }
		},
		error: defaultErrorHandler,
		complete: function () {
			vueApp.cambiandoControl = false;
			vueAppParams.data.dialogTomarControl = false;
		}
	});
}

vueAppParams.methods.desactivarBalanza = function (cesta) {
	vueApp.subiendoBalanza = true;

	// guardar cambios llamando al desactivar balanza
	$.ajax({
		url: '/CargaCestas/ModificarBalanza',
		data: { cesta: vueApp.cestas[cesta - 1] },
		method: 'POST',
		success: function (data) {
			vueAppParams.data.balanzaActiva = 0;
			vueApp.pesoBalanza[cesta - 1].activo = false;
			vueAppParams.data.pesoBalanza = vueAppParams.data.cestas.map(x => new Object({
				'cesta': x.numero,
				'listaPesos': x.listaDetalles.map(y => new Object({ 'orden': y.orden, 'peso': y.peso, 'pesoInicial': y.pesoInicial })),
				'activo': false
			}));
			vueApp.subiendoBalanza = false;
			vueApp.editBalanza = 0;
			vueApp.cestas[cesta - 1].activo = false;
			if (vueApp.cestas[cesta - 1].idVia == 1) {
				vueApp.rectasActivas--;
			} else {
				vueApp.curvasActivas--;
			}
		}
	});
}

vueAppParams.methods.modificarBalanza = function (cesta) {
	let ordenMasAlto = vueApp.cestas[cesta - 1].listaDetalles.reduce((a, b) => a.orden > b.orden ? a : b);

	vueApp.totalBalanza -= vueApp.editBalanza;

	ordenMasAlto.peso -= vueApp.editBalanza;

	ordenMasAlto.fechaHoraUltimoPeso = new Date().toJSON();

	while (ordenMasAlto.peso <= 0) {

		let pesoNegativo = ordenMasAlto.peso * -1;
		ordenMasAlto.peso = 0;

		if (ordenMasAlto.orden == 1) {
			vueApp.totalBalanza = 0;
			ordenMasAlto.peso = 0;
			break;
		}
		else {
			ordenMasAlto = vueApp.cestas[cesta - 1].listaDetalles.find(x => x.orden == ordenMasAlto.orden - 1);
			ordenMasAlto.fechaHoraUltimoPeso = new Date().toJSON();
			ordenMasAlto.peso -= pesoNegativo;
		}
    }

	
}
	/**/

