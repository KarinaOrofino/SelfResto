//Modelo
vueAppParams.data.Aplicaciones = [];

// Mounted
vueAppParams.mounted = function () {	
	//this.obtenerAplicaciones();	
};

vueAppParams.methods.obtenerAplicaciones = function () {

    $.ajax({
        url: "/Aplicaciones/ObtenerTodos",
        method: "POST",
        success: function (data) {            
            vueApp.Aplicaciones = data.content;            
        },
        error: defaultErrorHandler
    })
};

