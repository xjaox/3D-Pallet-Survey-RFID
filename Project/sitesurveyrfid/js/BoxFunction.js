
/*
Software of Site Survey - Reading tags on boxes with RFID.

Authors: João Neto (code and design)
Geyzon Lemos (code and design)
*/

var atualiza = false;

function Enable(tag) {
    $(function () {
        debugger;
        var id = ("#" + tag);
        $(id).addClass("Enable");
    });
}

function Desable(tag) {
    $(function () {
        debugger;
        var id = ("#" + tag);
        $(id).removeClass("Enable");
    });
}

function Reset() {
    $(function () {
        debugger;
        $(".Enable").each(function (i) {
            $(this).removeClass("Enable");
        });
    });
}

function ZerarXml() {
    $.ajax({
        type: 'post',
        url: 'Principal.aspx/ZerarXml',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //Caso sucesso faz a transição visual
        success: function (jsonResult) {
        },
        //Caso ocorra erro mande um alerta na tela
        error: function () { alert("t"); }
    });
}

function Readtag() {
    //Chamada ajax via JSON do metodo para vincular o Usuario
    if (atualiza == true) {
        $.ajax({
            type: 'post',
            url: 'Principal.aspx/ReadTag',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //Caso sucesso faz a transição visual
            success: function (jsonResult) {
                var res = jsonResult.d;
                var tags = res.split(";");
                var listBox = $(document).find(".objectBox3D01");

                for (i = 0; i < listBox.length; i++) {

                    var cont = 0;
                    var active = false;

                    while (active != true && cont < tags.length) {
                        if (tags[cont].split("|")[0] == listBox[i].id) {
                            active = true;
                        }
                        cont++;
                    }

                    if (active) {

                        Enable(listBox[i].id);

                        var cor = tags[cont - 1].split("|")[1];
                        AddColor(listBox[i].id, cor);

                        if (cor == 1) {
                            $("#lblAltaIntensidade").html(new Number($("#lblAltaIntensidade").html()) + 1);
                        }

                        if (cor == 0) {
                            $("#lblMediaIntensidade").html(new Number($("#lblMediaIntensidade").html()) + 1);
                        }

                        if (cor == -1) {
                            $("#lblBaixaIntensidade").html(new Number($("#lblBaixaIntensidade").html()) + 1);
                        }

                    } else {
                        Desable(listBox[i].id);
                        RemoveColor(listBox[i].id);

                        $("#lblTagNaoLida").html(new Number($("#lblTagNaoLida").html()) + 1);
                    }

                    startSetInterval = true;
                }
            },
            //Caso ocorra erro mande um alerta na tela
            error: function () { alert("Erro de Leitura!"); }

        });
    }
}

var startSetInterval = false;
var objCreateBox = new Object();

objCreateBox.idBox = '1';
objCreateBox.sizePallet = '550';
objCreateBox.translateX = '0';

objCreateBox.sizeBox = "130";

//400 - tamanho Cena
var sizePalletY = (395 - objCreateBox.sizeBox).toString();
var sizePalletZ = "-" + (objCreateBox.sizePallet / 2).toString();

objCreateBox.translateY = sizePalletY;
objCreateBox.translateZ = sizePalletZ;


function CreateBox() {

    objCreateBox.sizeBox = $("#txtDimensao").val();
    if (objCreateBox.idBox == "1") {
        sizePalletY = (395 - objCreateBox.sizeBox).toString();
        objCreateBox.translateY = sizePalletY;
    }

    //Conversão dos parametros para JSON
    var parameters = JSON.stringify(objCreateBox);

    $.ajax({
        type: 'post',
        url: 'Principal.aspx/CreateBox',
        contentType: 'application/json; charset=utf-8',
        data: "{box:'" + parameters + "'}",
        dataType: "json",
        //Caso sucesso faz a transição visual
        success: function (jsonResult) {

            var res = $.parseJSON(jsonResult.d);

            $("#boxs").append(res.box);

            objCreateBox.idBox = res.idBox;
            objCreateBox.translateX = res.translateX;
            objCreateBox.translateY = res.translateY;
            objCreateBox.translateZ = res.translateZ;

            $("#btAdcionarCaixa").prop('disabled', false);
            //document.getElementById("btAdcionarCaixa").disabled = false;
        },
        //Caso ocorra erro mande um alerta na tela
        error: function () {
            alert("Erro ao Criar Caixa!");
            //document.getElementById("btAdcionarCaixa").disabled = false;
            $("#btAdcionarCaixa").prop('disabled', false);
        }
    });
}

function AddColor(tag, valor) {
    $(function () {
        var id = ("#" + tag + " img");
        var cor = "img/imgP.png";
        switch (valor) {
            case 0: 
            {
                cor = "imgV.png";
                $(id).attr('src', 'img/' + cor);
            };
                break;
            case 1: 
            {
                cor = "imgL.png";
                $(id).attr('src', 'img/' + cor);
            };
                break;
            case 2:
            {
                cor = "imgG.png";
                $(id).attr('src', 'img/' + cor);
            };
                break;
        }
    });
}

function RemoveColor(tag) {
    $(function () {
        var id = ("#" + tag + " div");
        $(id).removeClass("bgColorBrack").removeClass("bgColorRed").removeClass("bgColorOrange").removeClass("bgColorGreen");
    });
}

$(function () {
    $("#btn").click(function () {
        Readtag();
    });
});

$(function () {
    $("#btAdcionarCaixa").click(function () {
        var button = document.getElementById("btAdcionarCaixa").disabled;

        if (button == false) {
            $("#btAdcionarCaixa").prop('disabled', true);
            CreateBox();
        } else {
            $("#btAdcionarCaixa").prop('disabled', false);
        }
    });
});

$(function () {
    $("#btLimparPallet").click(function () {
        location.reload();
    });
});

$(function () {
    $("#btMenos").click(function () {
        var a = new Number($("#txtDimensao").val());

        var b = a - 10;

        if (b > 10) {
            $("#txtDimensao").val(b);
        }
    });
});

$(function () {
    $("#btMais").click(function () {
        var a = new Number($("#txtDimensao").val());

        var b = a + 10;

        if (b < 520) {
            $("#txtDimensao").val(b);
        }
    });
});

$(function () {
    $("#btLerPallet").click(function () {
        atualiza = true;
        Readtag();
    });
});

var lado = 'left';

$(function () {
    $('#funcoesTopo').click(function () {
        var tamanho = screen.width - 268;
        if (lado == 'left') {
            $('#funcoes').animate({
                'margin-right': '20px',
                'left': '+=' + tamanho
            }, 300);
            lado = 'right';
        } else {
            $('#funcoes').animate({
                'margin': '10px',
                'left': '-=' + tamanho
            }, 300);
            lado = 'left';
        }
    });
});

//$(function () {

//    setInterval(function () {
//        if (startSetInterval == true) {

//            for (var i = 0; i < objCreateBox.idBox; i++ ) {
//                var randId = Math.floor((Math.random() * (objCreateBox.idBox - 1)) + 1);
//                var randCor = Math.floor(Math.random() * 3);

//                AddColor(randId, randCor);
//            }
//        }
//    }, 3000);
//});        
    