<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="SiteSurveyRFID.Principal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Seal Sistemas</title>

    <link href="css/styles.css" rel="stylesheet" type="text/css" />
    <script src="js/jsmove.js" type="text/javascript"></script>
    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/BoxFunction.js" type="text/javascript"></script>
</head>
<body style="display: none; transition: 2s" id="canvas3D" class="loaded">
    <form id="form1" runat="server">
    <section class="slides layout-regular template-default" style="-webkit-perspective: 1000px" lang="fr">

    <article class="current">
    
        <div id="funcoes">
        <div id="funcoesTopo"></div>
            Tag <input type="text" id="txtTag" style="width:99%" disabled="disabled" />
            <br />
            Tamanho da Caixa
            <br />
            <div class="buttons" id="btMenos" style="background:#BE0000; color:#FFF;" />-</div>
            <input type="text" id="txtDimensao" value="130" style="width:78px; text-align:center;" readonly="readonly" />
            <div class="buttons" id="btMais" style="background:#489600; color:#FFF;"/>+</div>
            <br />
            <div class="buttons" id="btAdcionarCaixa" style="background:#E8E9ED; width:100%; border: solid 1px #9D9CA4; color:#000;" />Adicionar Caixa</div>
            <div class="buttons" id="btLerPallet" style="background:#E8E9ED; width:100%; border: solid 1px #9D9CA4; color:#000;"/>Ler Pallet</div>
            <div class="buttons" id="btLimparPallet" style="background:#E8E9ED; width:100%; border: solid 1px #9D9CA4; color:#000;"/>Limpar Pallet</div>
        </div>
    
        <div class="scene3d end" id="scene">

            <div class="objectScene3D end" runat="server" id="objScene" onclick="move(&#39;objScene&#39;); move(&#39;scene&#39;)">

                <div id="boxs"></div>

                <div class="objectPallet3D">
                    <div class="objectFaceBox3D" id="paT01">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT01L01">
                        <img style="position:absolute; left:0; top:0px; " src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT01L02">
                    <img style="position:absolute; left:0; top:0px; display:inline;" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT01E01">
                    <img style="position:absolute; left:0; top:0px; display:block;" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT01E02">
                    <img style="position:absolute; left:0; top:0px; display:block;" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB01">
                        <img style="position:absolute; left:0; top:0px; display:block;" src="img/paT01.png" />
                    </div>

                    <div class="objectFaceBox3D" id="paT02">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT02L01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT02L02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT02E01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT02E02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB02">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>

                    <div class="objectFaceBox3D" id="paT03">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT03L01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT03L02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT03E01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT03E02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB03">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>

                    <div class="objectFaceBox3D" id="paT04">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT04L01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT04L02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT04E01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT04E02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB04">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>

                    <div class="objectFaceBox3D" id="paT05">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT05L01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT05L02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT05E01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT05E02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB05">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>

                    <div class="objectFaceBox3D" id="paT06">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT06L01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT06L02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT06E01">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT06E02">
                    <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB06">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>

                    <div class="objectFaceBox3D" id="paT07">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT07L01">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT07L02">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT07E01">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT07E02">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB07">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>

                    <div class="objectFaceBox3D" id="paT08">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>
                    <div class="objectFaceBox3D" id="paT08L01">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT08L02">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paL01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT08E01">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paT08E02">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paE01.png">
                    </div>
                    <div class="objectFaceBox3D" id="paB08">
                        <img style="position:absolute; left:0; top:0px; z-index:-1" src="img/paT01.png" />
                    </div>

            </div>
        </div>        
       
    </article>  

<!-- ------------------------------------------------------------------------------- -->

    </section>
    </form>
</body>
</html>
