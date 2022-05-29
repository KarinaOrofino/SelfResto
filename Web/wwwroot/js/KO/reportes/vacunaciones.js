//CONSTANTES
var SELECCIONE = '';
const TODOS = "todos";

//Model
vueAppParams.data.gridData = [];
vueAppParams.data.menuDesde = false;
vueAppParams.data.menuHasta = false;
vueAppParams.data.mostrarFecha = true;
vueAppParams.data.loadingColadaInicial = true;
vueAppParams.data.loadingColadaFinal = true;
vueAppParams.data.loadingEstado = true;
vueAppParams.data.loadingCesta = true;
vueAppParams.data.loadingVia = true;
vueAppParams.data.loadingReportes = true;
vueAppParams.data.loadingExportar = false;
vueAppParams.data.coladaInicial = [];
vueAppParams.data.coladaFinal = [];
vueAppParams.data.estado = [{ id: '', nombre: '' }]; 
vueAppParams.data.cesta = [];
vueAppParams.data.via = [{ id: '', nombre: '' }]; 
vueAppParams.data.fechaDesdeSeleccionada = vueAppParams.methods.primerDiaMes();
vueAppParams.data.fechaHastaSeleccionada = vueAppParams.methods.hoy();

vueAppParams.data.filtros = {

    fechaDesdeSeleccionada: vueAppParams.methods.formatoFecha(vueAppParams.data.fechaDesdeSeleccionada),
    fechaHastaSeleccionada: vueAppParams.methods.formatoFecha(vueAppParams.data.fechaHastaSeleccionada),
    coladaInicialSeleccionado: SELECCIONE,
    coladaFinalSeleccionado: SELECCIONE,
    estadoSeleccionado: SELECCIONE,
    cestaSeleccionado: SELECCIONE,
    viaSeleccionado: SELECCIONE
};
vueAppParams.data.estadoSeleccionado = [];
vueAppParams.data.coladaInicialSeleccionado = [];
vueAppParams.data.coladaFinalSeleccionado = [];
vueAppParams.data.cestaSeleccionado = [];
vueAppParams.data.viaSeleccionado = [];

vueAppParams.data.parentHeaders = [
    { text: "", colspan: 3, rowspan: 2, class: 'text-center protevac-headers-border'},
    { text: jsglobals.Recetas, colspan: 3, rowspan: 2, class: 'text-center protevac-headers-border' },
    { text: jsglobals.Real, colspan: 4, rowspan: 1, class: 'text-center protevac-headers-border' },
    { text: "", colspan: 5, rowspan: 1, class: 'text-center protevac-headers' },
];

vueAppParams.data.headers = [

    { text: jsglobals.Fecha, value: 'fecha', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border', width: '110px' },
    { text: jsglobals.Colada, value: 'colada', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.Cesta, value: 'cesta', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border', width: '86px' },
    { text: jsglobals.Capa, value: 'recetaCapa', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.Material, value: 'recetaMaterial', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border', width: '180px' },
    { text: jsglobals.Peso, value: 'recetaPeso', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.Capa, value: 'realCapa', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.Material, value: 'realMaterial', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border', width: '180px' },
    { text: jsglobals.Peso, value: 'realPeso', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.HoraCarga, value: 'realHoraCarga', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border', width: '170px'},
    { text: jsglobals.HoraFin, value: 'horaFin', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border', width: '170px'},
    { text: jsglobals.Comentarios, value: 'comentario', align: 'center', sortable: false, class: 'protevac-headers protevac-headers-border', width: '140px'},
    { text: jsglobals.Estado, value: 'estado', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.Por, value: 'por', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border' },
    { text: jsglobals.Via, value: 'via', align: 'center text-uppercase', sortable: false, class: 'protevac-headers protevac-headers-border' }

];

vueAppParams.data.breadcrums = [
    { text: jsglobals.Reportes, disabled: false, href: '/Reportes/CargaDeCestas' },
    { text: jsglobals.CargaCestasDeChatarra, disabled: true, href: '' }
];

// Mounted
vueAppParams.mounted = function () {
    this.loadGrid(true);
    this.obtenerVia();
    this.obtenerEstado();
    
};
vueAppParams.watch = {   
    fechaDesdeSeleccionada (val) {
        vueApp.filtros.fechaDesdeSeleccionada = vueAppParams.methods.formatoFecha(vueApp.fechaDesdeSeleccionada);
    },
    fechaHastaSeleccionada(val) {
        vueApp.filtros.fechaHastaSeleccionada = vueAppParams.methods.formatoFecha(vueApp.fechaHastaSeleccionada);
        
    }
}

vueAppParams.methods.analizarFecha = function (fecha) {
    if (!fecha ) return null

    const [day, month, year] = fecha.split('/');
  return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
}


vueAppParams.methods.onClickLimpiarFiltros = function (filterName) {
    
    if (filterName == TODOS) {
        vueAppParams.data.fechaDesdeSeleccionada = vueAppParams.methods.primerDiaMes();
        vueAppParams.data.fechaHastaSeleccionada = vueAppParams.methods.hoy();
        vueAppParams.data.filtros.fechaDesdeSeleccionada = vueAppParams.methods.formatoFecha(vueAppParams.methods.primerDiaMes());
        vueAppParams.data.filtros.fechaHastaSeleccionada = vueAppParams.methods.formatoFecha(vueAppParams.methods.hoy());
        vueAppParams.data.mostrarFecha = true;
        vueAppParams.data.filtros.viaSeleccionado = [];
        vueAppParams.data.filtros.estadoSeleccionado = [];
        vueAppParams.data.filtros.coladaInicialSeleccionado = [];
        vueAppParams.data.filtros.coladaFinalSeleccionado = [];
        vueAppParams.data.filtros.cestaSeleccionado = [];
        vueApp.coladaInicial = [];
        vueApp.coladaFinal = [];
        vueApp.cesta = [];
    }

    else {
        if (filterName == 'viaSeleccionado' || filterName == 'estadoSeleccionado' || filterName == 'coladaFinalSeleccionado' || filterName == 'cestaSeleccionado') {
            vueApp.filtros[filterName] = [];
        } else if (filterName == 'fechaDesdeSeleccionada' || filterName == 'fechaHastaSeleccionada') {
            vueAppParams.data.mostrarFecha = false;
            vueAppParams.data.filtros.viaSeleccionado = [];
            vueAppParams.data.filtros.estadoSeleccionado = [];
            vueAppParams.data.filtros.coladaInicialSeleccionado = [];
            vueAppParams.data.filtros.coladaFinalSeleccionado = [];
            vueAppParams.data.filtros.cestaSeleccionado = [];
            vueApp.coladaInicial = [];
            vueApp.coladaFinal = [];
            vueApp.cesta = [];
        } else if (filterName == 'coladaInicialSeleccionado' ) {
            vueAppParams.data.filtros.coladaInicialSeleccionado = [];
            vueAppParams.data.filtros.coladaFinalSeleccionado = [];
            vueApp.coladaFinal = [];
        }
    }
    if (vueAppParams.data.mostrarFecha != true && vueApp.coladaInicial.length == 0) {
        vueApp.loadingColadaInicial = true;
        vueApp.obtenerColadaInicialSinFecha();

    }
    this.loadGrid();
};

vueAppParams.methods.onClickFiltrar = function () {

    this.loadGrid(false);

}

vueAppParams.methods.obtenerColadaInicialSinFecha = function () {
    $.ajax({
        url: "/Reportes/obtenerColadaSinFecha",
        method: "GET",
        success: function (data) {
            vueApp.coladaInicial = data.content;
            vueApp.loadingColadaInicial = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingColadaInicial = false; });
}

vueAppParams.methods.obtenerColadaInicial = function () {

    var cantidadColadas = vueApp.gridData.length;
    for (i = 0; i < cantidadColadas; i++) {
        if (!vueApp.gridData[i].colada.includes('BACKUP')) {
            vueApp.coladaInicial.push({
                //id: vueApp.gridData[i].id,
                nombre: vueApp.gridData[i].colada
            });
        }
    }
    vueApp.coladaInicial.sort((a, b) => (a.nombre > b.nombre) ? 1 : ((b.nombre > a.nombre) ? -1 : 0));
    vueApp.loadingColadaInicial = false;
}

vueAppParams.methods.isDisabled = function () {
    if (vueAppParams.data.mostrarFecha != true) {
        return true
    } 
};

vueAppParams.methods.onClickObtenerColadaFinal = function (value, ci) {
    if (vueApp.filtros.coladaFinalSeleccionado != null || vueApp.filtros.coladaFinalSeleccionado != '') {
        vueApp.coladaFinal = [];
    }
    var colaFinal = 0;
    var cantidadColadas = ci.length;
    for (i = 0; i < cantidadColadas; i++) {
        colaFinal = parseInt(ci[i].nombre);
        if (colaFinal >= parseInt(value)) {
        vueApp.coladaFinal.push({            
            //id: vueApp.gridData[i].id,
            nombre: ci[i].nombre
        });
    }
    }
    vueApp.coladaFinal.sort((a, b) => (a.nombre > b.nombre) ? 1 : ((b.nombre > a.nombre) ? -1 : 0));
    vueApp.loadingColadaFinal = false;
}

vueAppParams.methods.obtenerCesta = function () {
    var cantCesta = vueApp.gridData.length;
    for (i = 0; i < cantCesta; i++) {
        vueApp.cesta.push({
            nombre: vueApp.gridData[i].cesta
        });
    }
    vueApp.cesta.sort((a, b) => (a.nombre > b.nombre) ? 1 : ((b.nombre > a.nombre) ? -1 : 0));
    vueApp.loadingCesta = false;
};

vueAppParams.methods.obtenerVia = function () {

    $.ajax({
        url: "/Reportes/ObtenerVia",
        method: "GET",
        success: function (data) {
            vueApp.via = data.content;
            vueApp.loadingVia = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingVia = false; });

};

vueAppParams.methods.obtenerEstado = function () {
    $.ajax({
        url: "/Reportes/ObtenerEstado",
        method: "GET",
        success: function (data) {
            vueApp.estado = data.content;
            vueApp.loadingEstado = false;
        },
        error: defaultErrorHandler
    }).done(() => { vueApp.loadingEstado = false; });
}


vueAppParams.methods.loadGrid = function (isDefault) {

    var filters = {};
    var fechaDesde;
    var fechaHasta = "";

    if (isDefault) {
       
        filters = { idEstado: null, idVia: null, idColadaInicial: null, idColadaFinal: null, cesta: null, fechaDesde: vueAppParams.data.fechaDesdeSeleccionada, fechaHasta: vueAppParams.data.fechaHastaSeleccionada }
    }
    else {
        vueApp.loadingReportes = true;
               
        if (vueAppParams.data.mostrarFecha == true) {           
            fechaDesde = vueApp.filtros.fechaDesdeSeleccionada;
            fechaHasta = vueApp.filtros.fechaHastaSeleccionada;
        }        
     
        filters = { idEstado: parseInt(vueApp.filtros.estadoSeleccionado), idVia: vueApp.filtros.viaSeleccionado, idColadaInicial: vueApp.filtros.coladaInicialSeleccionado, idColadaFinal: vueApp.filtros.coladaFinalSeleccionado, cesta: vueApp.filtros.cestaSeleccionado, fechaDesde: fechaDesde, fechaHasta: fechaHasta }
    }

    $.ajax({
        url: "/Reportes/ObtenerReporteCargaCestas",
        method: "POST",
        data: filters,
        success: function (data) {
            vueApp.gridData = data.content;
            if (isDefault) {
                vueApp.gridDataDefault = data.content;
            }
           
            if (vueAppParams.data.mostrarFecha == true) {
                vueApp.obtenerColadaInicial(); 
            }
          
            vueApp.loadingReportes = false;
            vueApp.obtenerCesta();           
        },
        error: defaultErrorHandler
    }).done(() => {
        vueApp.loadingReportes = false;
    });
};


vueAppParams.methods.onClickExportar = function () {

    vueApp.loadingExportar = true;
    return new Promise(resolve => {
        var fechaDesde = "";
        var fechaHasta = "";
        if (vueAppParams.data.mostrarFecha == true) {
            fechaDesde = vueApp.filtros.fechaDesdeSeleccionada;
            fechaHasta = vueApp.filtros.fechaHastaSeleccionada;
        }

        var urlToSend = "/Reportes/ExportarReporteCargaCestas?fechaDesde=" + fechaDesde
            + "&fechaHasta=" + fechaHasta
            + "&idVia=" + vueApp.filtros.viaSeleccionado
            + "&idEstado=" + parseInt(vueApp.filtros.estadoSeleccionado)
            + "&idColadaInicial=" + vueApp.filtros.coladaInicialSeleccionado
            + "&idColadaFinal=" + vueApp.filtros.coladaFinalSeleccionado
            + "&cesta=" + vueApp.filtros.cestaSeleccionado;


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
            var fileName = "ReporteReporteCargaCestas_" + fechaLocalSlash;
            var link = document.createElement("a");
            link.href = window.URL.createObjectURL(blob);
            link.download = fileName;
            link.click();
            vueApp.loadingExportar = false;
        };

        req.send()

        resolve(req.status);
    });
};
