var contador = function () {
    
    var n = $("input:checked").length;

    var btnResultado = document.getElementById("btnResultado");

    if (n >= 8) {
        $("input:not(:checked)").attr('disabled', 'disabled');
        if (btnResultado !== null) {
            btnResultado.classList.remove("disabled");
        }
    }
    else {
        $("input:not(:checked)").removeAttr('disabled');
        if (btnResultado !== null) {
            btnResultado.classList.add("disabled");
        }
    }

    $("h4.contador").text(n + " de 8 filmes");
};

contador();

var submit = function () {

    var type = [];

    $("input:checked").each(function (i) {
        type[i] = $(this).val();
    });
    
    var selecionados = JSON.stringify(type);

    $.ajax({
        type: "GET",
        url: "../../Home/Resultado",
        dataType: 'json',
        data: {
            selecionados: selecionados
        },
        success: function (data) {
            $("h4.primeiro").text(data.final[0].titulo);
            $("h4.segundo").text(data.final[1].titulo);
        }
    });
};