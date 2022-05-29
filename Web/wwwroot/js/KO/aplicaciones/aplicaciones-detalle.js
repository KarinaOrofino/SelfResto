// Modelo
vueAppParams.data.submiting = false;
vueAppParams.data.isValid = false;
vueAppParams.data.isDisabled = false;
vueAppParams.data.breadcrums = [];
vueAppParams.data.itemCambio = "";
vueAppParams.data.dialog = null;
vueAppParams.data.dialogConfirmarGuardado = null;
vueAppParams.data.menuDesde = false;
vueAppParams.data.listaCalidad = [];
vueAppParams.data.listaGrado = [];
vueAppParams.data.listaGrupo = [];
vueAppParams.data.listaCestas = [];
vueAppParams.data.listaChatarra = [];
vueAppParams.data.sumaPeso = [];
vueAppParams.data.cestasInit = true;
vueAppParams.data.volverDoble = false;
vueAppParams.data.sortableList = [];
vueAppParams.data.ordenSortable = [];

const today = new Date();
const yyyy = today.getFullYear();
let mm = today.getMonth() + 1; // Months start at 0!
let dd = today.getDate();

if (dd < 10) dd = '0' + dd;
if (mm < 10) mm = '0' + mm;
vueAppParams.data.fechaActual = yyyy + '-' + mm + '-' + dd;

// Elementos para editar en capas
vueAppParams.data.editChatarra = 0;
vueAppParams.data.editPeso = 0;
vueAppParams.data.editOrden = 0;


vueAppParams.mounted = async function () {
    //Breadcrums
    this.breadcrums = [
        { text: jsglobals.Recetas, disabled: false, href: '/Recetas/Listado' },
        { text: this.model.IdReceta> 0 ? jsglobals.Editar : jsglobals.Nuevo, href: '/Recetas/Detalle', disabled: true }
    ];
    this.isDisabled = !this.model.Activo;
    await this.cargarListaCalidad();
    await this.cargarListaChatarra();
    await this.obtenerCestas();
};

vueAppParams.data.headers = [
    {
        text: jsglobals.Cesta, value: 'Carga', align: ' d-none', sortable: false,
        class: 'protevac-headers'
    },
    {
        text: jsglobals.Orden, value: 'Orden', align: 'center', sortable: false,
        class: 'protevac-headers'
    },
    {
        text: jsglobals.Chatarra, value: 'Chatarra', align: 'center', sortable: false,
        class: 'protevac-headers'
    },
    {
        text: jsglobals.Peso, value: 'Peso', align: 'center', sortable: false,
        class: 'protevac-headers'
    },
    {
        text: jsglobals.Acciones, value: 'actions', sortable: false, align: 'center',
        class: 'protevac-headers'
    }
];


vueAppParams.methods.cargarListaCalidad = async function () {
	await $.ajax({
		url: "/Recetas/ObtenerListaCalidad",
		method: "GET",
		success: function (data) {
			vueApp.listaCalidad = data.content;
			vueApp.listaGrupo = [...new Map(vueApp.listaCalidad.map(item => [item["codigoGrupo"], new Object({ "nombre": item.nombre, "codigoGrupo": item.codigoGrupo })])).values()];
			vueApp.listaGrado = vueApp.listaCalidad.map(item => new Object({ "calidad": item.calidad, "codigoCalidad": item.codigoCalidad }));
		},
		error: defaultErrorHandler
	})
};

vueAppParams.methods.cargarListaChatarra = async function () {
    await $.ajax({
        url: "/Recetas/ObtenerListaChatarra",
        method: "GET",
        success: function (data) {
            vueApp.listaChatarra = data.content;
            vueApp.listaChatarra.push(new Object({ codigo: 0, nombre: "Sin asignar" }));
        },
        error: defaultErrorHandler
    })
}

vueAppParams.methods.actualizarCalidad = function () {
    vueApp.listaGrado = vueApp.listaCalidad.filter(item => item.codigoGrupo == this.model.GrupoGrado);
}

vueAppParams.methods.cambioReserva = function () {
    if (this.model.Reserva) {
        this.model.Grado = 0;
        this.model.GrupoGrado = 0;
        this.model.Cestas = 1;
    }
}

vueAppParams.methods.onClickAgregarCapa = function (n) {
    var capaActual = vueApp.listaCestas[n].length == 0 ? 0 : Math.max.apply(Math, vueApp.listaCestas[n].map(x => x.orden));
    var capa = {"carga": n+1, "orden": capaActual+1, "chatarra": 0, "peso": 0}
    vueApp.listaCestas[n].push(capa);
}

vueAppParams.methods.obtenerCestas = async function () {
    if (this.model.IdReceta > 0 && vueAppParams.data.cestasInit) {
        await $.ajax({
            url: "/Recetas/ObtenerCestas/" + this.model.IdReceta,
            method: "GET",
            success: await function (data) {
                data.content.sort((a, b) => (a[0].carga > b[0].carga) ? 1 : ((b[0].carga > a[0].carga) ? -1 : 0));
                data.content.forEach(x => x.sort((a, b) => (a.orden > b.orden) ? 1 : ((b.orden > a.orden) ? -1 : 0)));
                vueApp.listaCestas = data.content
                vueApp.listaCestas.forEach(x => vueApp.sumaPeso.push({ "cesta": x[0].carga - 1, "peso": vueApp.listaCestas[x[0].carga - 1].map(y => y.peso).reduce((a, b) => a + b) }))
                vueApp.cestasInit = false;
            },
            error: defaultErrorHandler
        });
    } else {
        if (vueAppParams.data.listaCestas.length == 0) {

            for (let i = 0; i < this.model.Cestas; i++) {
                vueAppParams.data.listaCestas.push([]);
                vueAppParams.data.sumaPeso.push({ "cesta": i, "peso": 0 });
            }
        } else if (this.model.Cestas > vueAppParams.data.listaCestas.length) {
            for (let i = vueApp.listaCestas.length; i < this.model.Cestas; i++) {
                vueAppParams.data.listaCestas.push([]);
                vueAppParams.data.sumaPeso.push({ "cesta": i, "peso": 0 });
            }
        }
    }
}

vueAppParams.methods.sumarPesos = function (n) {

    vueAppParams.data.sumaPeso[n].peso = vueApp.listaCestas[n].map(x => x.peso).reduce((a, b) => a + b);
}

vueAppParams.methods.onClickGuardar = function () {

    vueAppParams.data.dialogConfirmarGuardado = false;

    vueApp.isValid = vueApp.$refs.form.validate();

    if (vueAppParams.data.listaCestas.some(x => x.length == 0)) {
        vueApp.notification.showError(jsglobals.MensajeRecetaSinCapas);
        return false;
    }
    if (vueAppParams.data.listaCestas.some(c => c.some(p => p.peso <= 0))) {
        vueApp.notification.showError(jsglobals.MensajeCestasSinPeso);
        return false;
    }

    if (!vueApp.isValid) {
        return false;
    }

    $.ajax({
        url: "/Recetas/GuardarReceta",
        data: { modelo: this.model, listaCestas: vueApp.listaCestas },
        method: "POST",
        success: function (data) {
            vueApp.notification.showSuccess(jsglobals.MensajeDatosGuardadosOk);
            setTimeout(function () { window.location = '/Recetas/Listado' }, TIEMPO_CIERRE_FORMULARIO);
        },
        error: defaultErrorHandler
    })
}

vueAppParams.methods.onClickDuplicar = function () {
    this.model.IdReceta = 0;
    vueApp.listaCestas.forEach(x => x.forEach(y => y.cesta = 0));
    this.model.Activo = true;
    this.model.Usado = false;
    this.model.ValidoDesde = "";
    this.model.Hora = "";
    this.model.GrupoGrado = undefined;
    this.model.Grado = undefined;
    this.model.Nombre = "";
    vueApp.volverDoble = true;
    window.history.pushState("Nuevo", "Title", "./");
}

vueAppParams.methods.onChangeCambioEstado = function () {

    vueAppParams.data.dialog = true;
    vueAppParams.data.itemCambio = this.model;
};

vueAppParams.methods.onClickVolver = function () {
    if (vueApp.volverDoble) {
        window.history.go(-2);
    } else {
        window.history.back();
    }
};

vueAppParams.methods.onClickNoConfirma = function () {
    vueAppParams.data.dialog = false;
    this.model.Activo = !this.model.Activo
};

vueAppParams.methods.ConfirmarGuardado = function () {
    vueApp.isValid = vueApp.$refs.form.validate();

    if (vueAppParams.data.listaCestas.length > this.model.Cestas) {
        vueAppParams.data.listaCestas.splice(this.model.Cestas);
    }

    if (!vueApp.isValid) return false;

    if (vueAppParams.data.listaCestas.some(x => x.length == 0)) {
        vueApp.notification.showError(jsglobals.MensajeRecetaSinCapas);
        return false;
    }

    if (vueAppParams.data.listaCestas.some(c => c.some(p => p.peso <= 0))) {
        vueApp.notification.showError(jsglobals.MensajeCestasSinPeso);
        return false;
    }

    if (vueAppParams.data.listaCestas.some(c => c.some(ch => ch.chatarra == 0))){
        vueApp.notification.showError(jsglobals.MensajeChatarraSinAsignar)
        return false;
    }

    vueAppParams.data.dialogConfirmarGuardado = true;
}

vueAppParams.methods.onClickCancelarGuardado = function () {
    vueAppParams.data.dialogConfirmarGuardado = false;
};

vueAppParams.methods.guardarChatarra = function (item, n) {
    var aCambiar = vueAppParams.data.listaCestas[n].findIndex(c => c.orden == item.orden);
    vueAppParams.data.listaCestas[n][aCambiar].chatarra = vueApp.editChatarra;
};

vueAppParams.methods.guardarPeso = function (item, n) {
    var aCambiar = vueAppParams.data.listaCestas[n].findIndex(c => c.orden == item.orden);
    vueAppParams.data.listaCestas[n][aCambiar].peso = vueApp.editPeso;
    var aSumar = vueAppParams.data.sumaPeso.findIndex(p => p.cesta == n);
    vueApp.sumarPesos(aSumar);
};

vueAppParams.methods.guardarOrden = async function (item, n) {
    var aCambiar = vueAppParams.data.listaCestas[n].findIndex(c => c.orden == item.orden);
    vueAppParams.data.listaCestas[n][aCambiar].orden = await vueApp.editOrden;
    vueApp.listaCestas[n].sort((a, b) => (a.orden > b.orden) ? 1 : ((b.orden > a.orden) ? -1 : 0));
    for (let a = 0; a < vueApp.listaCestas[n].length; a++) {
        vueAppParams.data.listaCestas[n][a].orden = a + 1;
    }
    console.log(this);
};

vueAppParams.methods.onClickBorrarCapa = function (item, n) {
    let cargaGuardada = item.orden;
    vueAppParams.data.listaCestas[n].splice(item.orden-1, 1);
    vueApp.listaCestas[n].forEach(x => x.orden = x.orden >= cargaGuardada ? cargaGuardada++ : x.orden);
}

vueAppParams.methods.cerrarChatarra = function (n, orden) {
    var chatarra = "checkChatarra"+n
    this.$refs[chatarra][orden-1].save();
}

vueAppParams.methods.cestasOrdenables = function (index) {
    let cesta = "cesta" + index;
    console.log('cesta' + cesta);
    console.log(document.getElementById(cesta))
    vueApp.ordenSortable.push(document.getElementById(cesta).innerHTML);
    const _self = this;

    vueApp.sortableList.push(Sortable.create(document.getElementById(cesta), {
        dataIdAttr: "id",
        onEnd: function (event) {
            const rowSelected = _self.listaCestas[index].splice(event.oldIndex, 1)[0];
            _self.listaCestas[index].splice(event.newIndex, 0, rowSelected);
            for (let a = 0; a < _self.listaCestas[index].length; a++) {
                _self.listaCestas[index][a].orden = a + 1;
            }
            vueApp.sortableList[index].sort(vueApp.sortableList[index].toArray().sort(), true);
        }
    }));
}