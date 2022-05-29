//Las rules deben evaluar en true o el mensaje de error
vueAppParams.data.vrules = {
	required: v => (!!v && v != "") || "El campo es requerido",
	maxLength: (v, l, txt) => (!!v && (v + "").length <= l) || "El campo " + txt + " no debe superar los " + l + " caracteres",
	number: v => parseInt(v) || "Debe ingresar un número",
	numerico: v => (v && new RegExp('^[0-9]*$').test(v)) || jsglobals.SoloNumeros,
	mayorQueCero: v => (v && v != 0) || jsglobals.CampoNoCero,
	mayorOIgualCero: v => (v>= 0) || jsglobals.CampoMayorIgualCero,
	decimalesConPunto: v => (new RegExp('^[0-9]+([.][0-9]+)?$').test(v)) || jsglobals.FormatoDecimal,
	mayorQueCeroONulo: v => (!v || v > 0) || jsglobals.CampoMayorQueCero,
	mayorOIgualQueCeroONulo: v => (!v || v >= 0) || jsglobals.CampoMayorQueCero,
	decimalesConPuntoONulo: v => (!v || new RegExp('^(-)?[0-9]+([.][0-9]+)?$').test(v)) || jsglobals.FormatoDecimal,
	aMayorQueBONulo: (v, b) => (!v || parseFloat(v) <= parseFloat(b)) || jsglobals.NoSuperarMaximo,
	aMenorQueBONulo: (v, b) => (!v || parseFloat(v) >= parseFloat(b)) || jsglobals.NoInferiorMinimo,
	aMayorQueB: (v, b) => (parseFloat(v) <= parseFloat(b)) || jsglobals.NoSuperarMaximo,
	aMenorQueB: (v, b) => (parseFloat(v) >= parseFloat(b)) || jsglobals.NoInferiorMinimo,
	dosOMas: v => (v >= 2) || jsglobals.DebeSer2OMas,
};

vueAppParams.methods.formatoDecimal = function (numero, minimumFractionDigits = 2) {
	return numero ? numero.toLocaleString('es-AR', { minimumFractionDigits: minimumFractionDigits }) : ''
};

vueAppParams.methods.formatoFecha = function (fecha) {
	return fecha ? moment(fecha).format('DD/MM/YYYY') : ''
};

vueAppParams.methods.primerDiaMes = function () {
	return new Date(new Date().getFullYear(), new Date().getMonth(), 1).toISOString().substr(0, 10);
};

vueAppParams.methods.ultimoDiaMes = function () {
	return new Date(new Date().getFullYear(), new Date().getMonth() + 1, 0).toISOString().substr(0, 10);
};

vueAppParams.methods.hoy = function () {
	return new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()).toISOString().substr(0, 10);
};