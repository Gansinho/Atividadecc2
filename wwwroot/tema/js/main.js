var main = {

    fazerLogin: function () {
                      
        var dados = {
            email: $("#email").val(),
            senha: $("#senha").val()
        };

        $.ajax({
            dataType: 'script',
            url: baseUrl + 'login/registrarusuario',
            type: 'post',
            data: dados,
            success: function (res) {

                btn.classList.remove('kt-spinner', 'kt-spinner--right', 'kt-spinner--sm', 'kt-spinner--light');
                btn.disabled = false;
                displaySignInForm();
                var signInForm = $('#kt_login').find('.kt-login__signin');
                util.showErrorMsg(signInForm, 'success', 'Sua conta tá criada! Agora é so digitar seu login e senha para acessar :D');
            }
        });
    },

    nomeFuncao: function () {


    }
}