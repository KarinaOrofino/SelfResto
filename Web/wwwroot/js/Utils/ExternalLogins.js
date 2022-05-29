function onGoogleSignIn(googleUser) {
    var profile = googleUser.getBasicProfile();

    var model = {
        id: profile.getId(),
        userName: profile.getName(),
        email: profile.getEmail(),
        imageUrl: profile.getImageUrl()
    };

    $.ajax({
        type: 'POST',
        url: '/Account/GoogleLogin',
        data: model,
        beforeSend: function () {
            console.log('llamando al login con google data');
        },
        success: function (data) {
            console.log('llamando al login finalizo correctamente');
        },
        error: function (data) {
            console.log('Error en el login');
        }
    }).done((res) => {

        if (res.result == 0) {
            document.location.href = "../Home/Index";
        }
        else {
            NotificationUi.CrearNotificationBoxFor($("#dynamic-notificacion"), "alert-danger", res.content.mensaje, false);
            $("#btnLogin").removeAttr("disabled", "disabled");
        }

        try {
            var auth2 = gapi.auth2.getAuthInstance();
            auth2.disconnect();
        } catch (error) {
            console.error(error);
        }
    });
}