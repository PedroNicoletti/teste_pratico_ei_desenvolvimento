/// <reference path="../../Scripts/jquery-1.10.2.js" />
/// <reference path="../../Scripts/bootstrap.js" />

"use strict"

var TAREFA2 = TAREFA2 || {
    Carregar: () => {
        $("[id$='_btnEstranho']").on('click', () => {
            return TAREFA2.Autodestruir();
        });

        $("[id$='_btnGravar']").on('click', () => {
            return TAREFA2.Validar();
        });

        TAREFA2.AplicarMascaras();
    },
    Autodestruir: () => {
        window.alert('Este computador se autodestruirá em 20 segundos...\r\nTodos os seus códigos serão descartados e não poderão ser recuperados.');
        window.setTimeout(() => {
            window.alert('A autodestruição era brincadeira tá!')
        }, 3000);
        return false;
    },
    AplicarMascaras: () => {
        $("[id$='_txtCpf']").on('input', function () {
            var v = $(this).val().replace(/\D/g, '').substring(0, 11);
            v = v.replace(/(\d{3})(\d)/, '$1.$2');
            v = v.replace(/(\d{3})(\d)/, '$1.$2');
            v = v.replace(/(\d{3})(\d{1,2})$/, '$1-$2');
            $(this).val(v);
        });

        $("[id$='_txtTelefone']").on('input', function () {
            var v = $(this).val().replace(/\D/g, '').substring(0, 11);
            v = v.replace(/^(\d{2})(\d)/g, '($1) $2');
            v = v.replace(/(\d)(\d{4})$/, '$1-$2');
            $(this).val(v);
        });

        $("[id$='_txtDataNascimento']").on('input', function () {
            var v = $(this).val().replace(/\D/g, '').substring(0, 8);
            v = v.replace(/(\d{2})(\d)/, '$1/$2');
            v = v.replace(/(\d{2})(\d)/, '$1/$2');
            $(this).val(v);
        });
    },
    ValidarEmail: (email) => {
        var regex = /^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$/;
        return regex.test(email);
    },
    Validar: () => {
        var erros = [];

        if ($.trim($("[id$='_txtNome']").val()) === '')
            erros.push('Nome');

        if ($("[id$='_txtCpf']").val().replace(/\D/g, '').length !== 11)
            erros.push('CPF');

        if ($.trim($("[id$='_txtRg']").val()) === '')
            erros.push('RG');

        var email = $.trim($("[id$='_txtEmail']").val());
        if (email !== '' && !TAREFA2.ValidarEmail(email))
            erros.push('Email');

        if ($("[id$='_ddlSexo']").prop('selectedIndex') === 0)
            erros.push('Sexo');

        var dataNascimento = $.trim($("[id$='_txtDataNascimento']").val());
        if (dataNascimento === '' || !/^\d{2}\/\d{2}\/\d{4}$/.test(dataNascimento))
            erros.push('Data de nascimento');

        if (erros.length > 0) {
            window.alert('Verifique o(s) campo(s): ' + erros.join(', '));
            return false;
        }

        return true;
    }
}

var postBackPage = postBackPage || Sys.WebForms.PageRequestManager.getInstance();

$(document).ready(function () {
    TAREFA2.Carregar();
});

postBackPage.add_endRequest(function () {
    TAREFA2.Carregar();
});