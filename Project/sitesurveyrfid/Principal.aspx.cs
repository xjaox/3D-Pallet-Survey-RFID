using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.IO;
using System.Web.Services;
using System.Text;
using System.Web.Script.Serialization;

namespace SiteSurveyRFID
{
    public partial class Principal : System.Web.UI.Page
    {
        public static int totalLinesY = 0;
        public static int totalLinesZ = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            ReadTag();

            totalLinesY = 0;
            totalLinesZ = 0;
        }

        public static string boxHtmlMenor(int idBox, int sizeBox, int sizePallet, int nextPositionX, int nextPositionY, float nextPositionZ, int sizeRotage)
        {
            StringBuilder text = new StringBuilder();

            try
            {
                text.Append("<div id=\"" + idBox + "\" class=\"objectBox3D01\" style=\"-webkit-transform: translateX(" + nextPositionX + "px) translateY(" + nextPositionY + "px) translateZ(" + nextPositionZ + "px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/01.png\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(0px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px; \">");
                text.Append("<img src=\"img/02.png\" style=\"-webkit-transform: translateX(-" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/03.png\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(" + sizeBox + "px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/04.png\" style=\"-webkit-transform: translateX(" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/05.png\" style=\"-webkit-transform: translateX(0px) translateY(-" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/06.png\" style=\"-webkit-transform: translateX(0px) translateY(" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("</div>");
            }
            catch (Exception)
            {

            }

            return text.ToString();
        }

        public static string boxHtml(int idBox, int sizeBox, int sizePallet, int nextPositionX, int nextPositionY, float nextPositionZ, int sizeRotage)
        {
            StringBuilder text = new StringBuilder();

            try
            {
                string img01 = "data:image/png;base64,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";
                string img02 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAD2klEQVRIDQXBsREk2HVFMdzfvbJWVcyMuTAfxShD9Dj9joD9z7/+2dt8/vqY573Ze/Zm+3qf5228Z5vk3//3v6B+Pp//8vff/wA27z326Ni40+8PFwNyvhveM2PYbM/MxttszzY2FQBmALI9ew/DbMOcaWE4a77bvMG8zYyxsU3Fwrw9b2NDxJXG28f2MBv2gLE367nfDwxfpTtt+NhY7M0eM9vABqPUqSSLPYwNm0nGsIHt4VS+e897z9uzsc17z/Ywe/PG3mzA96+/dAc+7+O9GYjNsD0b9axjk6djy1fpju8IQnTWdLRni1j577//YZv7HReFcYc/9h77cBkyZEKSrw24WBptGPdsx0u/dOftYYruD8VNe96bfuninRn3436S7hB4ymA0GBg2Bo3NNgbJcSm2eQOIh+C0k4iJUL7bY0wAFENYLMM2kwslbBgbxJ6hsY0GjAyIrzAEQ3Tq2WIP1FmElZMJKGFwp7E9hQLGTP1YvhzhPYZiAAwbG5U3wpoGIQAMKKSOTlCU4RlQ0SE8UCcRM29jszdEGbbZZpv3xkGM7bEZCmC+YhsCDNFsWXA6fJ4FGC4V8B5wMYTQqQOT5MoXctbYU6no5/asY2wfxIbZnRzGZmLj4ZKjJJAQ2PK98gEog7L3LBQeHVGzTTCETkbZsGd7tqmfHE3Q6c53BVrW5GNF8YboZDreZwpOF+IeL3sfHdu5C/BHB3RHgaefOkIhSR0F6uSs9OdHp5LkkC79/piTQzgZ0BkM8gxDSepMoFKHRwTj7qc7LprKQO6OqKPsUkcDE/fz7nBJxEah1CncDxnq1Lk7in4MIxB3wKayyElqiq9ync8+Qnf2aM8L90efr13yH+EuJOFrv59gHPYZHk2dYUhswrfORmWjopmx2UaxeF+TOXUYYo/3bDB7ALONO4Ik7nyVNqTmbUKd3dx73qjo6HSpw2wRHcY23cCGEjgC6nzDSsc+KVySPjMpttS5Pz8VY8JUOBrLNr1HCTP2JJUrXx0eo077eCOprPFGVK5TWeM9NmtstkkG91OpDHXuApXvFc4aR/h1Pp+nX06ec5suf/78x9uTWec1t1lzG5sfYBt3rhAd5n58oVJsjwhdvOM4894TNDmDezLeM3Oyss2GxsiAHiV8GZIB2MYeb5jtw5457z0GabP3sT0JY9gEWMMRDeX3++Nbs43IMJVkN0YyVBIxA1AhQwaMGUOTo6n5/fK9DmxP5TbD/bJPiOOazF2Ubfa437z37D3BHXts7HRJBAd+ly9h6uijIjwKnpn7/Vy5O4ONno0uhBjbQyrA2NQh4f8BNyemMSnze+IAAAAASUVORK5CYII=";
                string img03 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAEX0lEQVRIDQXBW84dZxUFwFq727HBMhKJQOEmMSPmwnyYGy95CCICKbFs/tPfXlTlH3//WydxvbvEmInMyERym2tMItelu+a+ZUYy9jx6HjIiTGQiuVQFdnUPZyUUcCeYESFIJCMiYRLJCDIDJiExc9ldBUVMhhAByViroQ0prUliAjGJCCEhibaqCjMmg5EEVYAZM4ORjCQiJJKYuUhpJTFa3dUuSEhJIkMSScAEQ0K7tAKYICQISWQQEjIkkUHXnRkzYzISkpgZySAyMSETElMMIw4m0YTEFEOQIBcORWOu0cVwa3WXOxSK0pVGl2YkldKuwERS2xUjpa0oc3ENWyltUCnQHrcE2JJqaIKwI1mmeqq7JqPKRvdhV61uTEaLlo5Ay3nA7gFVoxUIDQQECYGGRBICtVa3WiQmISUICkVRVQmU1p0MIQpAS1CkpIIkogoFEigZUcIkCCFoIq0mlKhbERSC0tWOpGRAu1JAS6G0QBfYqsig0S4tiGA1dbMUMwQtASBISGgriaAIGhLaEmKAFkuXrgLaRYwAbemiGNCuKiViEkJmQFsKMYkkktACQUIiAyUEt5IEBQSlkVQKq4trpEBUocvSa6SwJNpKF1TZpaTVcEOtNGS01ZYem5EuIblQhACsqiQokBG0R7sopSKqgHtbF0Ar0MqMFC2GLqVCBwWBLqVCl0QS5qbLHkoTinPcaUFTadQlLS0TlK6KLnOFVlttaVFdckVaRXcBRxctqrskbj0q0kFJFe1KQ6JdEmn0OczQ0tUWjPAcrkEIWkBRWgI1gqBV1a4oaKtdDAWk2mO7ugVCIWwX1ZaWli4AoGYXW1VKQotWu1rsQQUt3eoeVXZpQVsp3dKSaAFdVVoat9Z2XbkU3ZWhGVPso9ctW/VS0VZbrOaSrXqJ2JAJEzralRZQElp3uxLaSggmJGOuSCJBSm4JdnUXQ0sqc0lCIkECIuwqtKqoW6sJUL58fdk+rnljIhnXPT7+6r1MtNVd3YKEdmnoMhGjKsICWlrQ1l2kFfX5y5t//vCT33/3G5DEfV/+9dPP/vT9t/7w/W+93h57lhAUctEF2WqQ6EBJAHQridGlKOc5fvftJx/ev/N61n1fnq2//PFbz3Ok7Fnb1V0FQwEZRNFWz6Pn0XN0jz2P7mGPe1usRa3dx8+/PH7+/D/d48vb8eGbix7nvDyvN0Iamcq1JqHRRIRAxWCl1VZVSxs3tKWltLy7b+/fPd5/cztbtrYooEW12NiJcaG0EpKQIqqa0EiXcBNURRMaX1/Hl7djvSleZ2WGROaiJUUkl2QIYCCaUBLaoNoFuqYNhnLl8u///OLz56/Orq9f35xz/PDjf82MdlFVGkpVW92l1VbRlgCZABkVFfd2we748OH21z9/ByYxMwSJTx9/7fWq3equJIRZJDKjsMUyg2pLqVLaJXFTxHZdvXz6+EGQiZnLzJgrzrOe1+PsASOSgLa6ZUrJXIh2KcCoIoL/AzHXmaNVqLPMAAAAAElFTkSuQmCC";
                string img04 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAD5UlEQVRIDQXBwREk2FVFwby/aliJCAWO4Qv+4CIbsZuud5S5//2f/+5tPn99zPPe7D17s329z/M23jOzz/P///o/v/tx2eYf//lf3vsw3nvs0bFxp98fLgbkfDe8Z8aw2Z6ZjbfZnm1slDDT2GYg27P3MMw2zJkWhrPmbfMG8zYzxsY2lSTsTagYxMZ79mZ7mI29x8bYm/ceHTE8pTt1YGOxzR7bbANvw1EEbM/bbGNs2GzYGDY227ORfPee9563Z2Ob957tYfbmjb3Z5u5c522Uz+fjvQFiM2zPRj3r2OTp2PJVuuM7ghCdNR3t2SJ2+ev7F/KTt1EYd/hj77EPlyFDJiT52oCLpdGGcc92vPTL785fn6//+Mc/Fb/f35T7+2/2vDf90sU7M+7H/STdIfCUwWgwMGwMGpttjCRnDc82bwDxEJx2EjERynd7jAmAYgiLZdhmcqEkGzYbxJ6hsY0GjAyIrzAEQ3Tq2WIP1FmElZMJKGFwp7E9hQLGTP1YvhzhPYZiAAwbG5U3wpoGIQAMKKSOTlCU4RlQ0SE8UCcRM29jszdEGbbZZpv3xkGM7bEZCmC+YhsCDNFsWXA6fJ4FGC4V8B5wMYTQqQOT5MoXctbYU6no5/asY2wfxIbZnRzGZmLj4ZKjJJAQ2PK98gEog7L3LBQeHVGzTTCETkbZsGd7tqmfHE3Q6c53BVrW5GNF8YboZDreZwpOF+IeL3sfHdu5C/BHB3RHgaefOkIhSR0F6uSs9OdHp5LkkC79/piTQzgZ0BkM8gxDSepMoFKHRwTj7qc7LprKQO6OqKPsUkcDE/fz7nBJxEah1CncDxnq1Lk7in4MIxB3wKayyElqiq9ync8+Qnf2aM8L90efr13yt3AXkvC1308wDvsMj6bOMCQ24Vtno7JR0czYbKNYvK/JnDoMscd7Npg9gNnGHUESd75KG1LzNqHObu49b1R0dLrUYbaIDmObbmBDCRwBdb5hpWOfFC5Jn5kUW+rcn5+KMWEqHI1lm96jhBl7ksqVrw6PUad9vJFU1ngjKteprPEemzU22ySD+6lUhjp3gcr3CmeNI/w6n8/TLyfPuU2XP3/+9vZk1nnNbdbcxuYH2MadK0SHuR9fqBTbI0IX7zjOvPcETc7gnoz3zJysbLOhMTKgRwlfhmQAtrHHG2b7sGfOe49B2ux9bE/CGDYB1nBEQ/n9/vjWbCMyTCXZjZEMlUTMAFTIkAFjxtDkaGp+v3yvA9tTuc1wv+wT4rgmcxdlmz3uN+89e09wxx4bO10SwYHf5UuYOvqoCI+CZ+Z+P1fuzmCjZ6MLIcb2kAowNnVI+De2Z6FCQbff7gAAAABJRU5ErkJggg==";
                string img05 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAADrElEQVRIDQXB0a0DUFZFMe+b8IWQKJFmaIfqEIKZl7Ow91//+R8tNrq8z8ecOgpYFnAy6e8QG/cnVFzq/IrL78L8fn9+v/wu4TtjKfbG0DCNmTk5zMCzD90xfD5eE3pnZdH9fDrDff9FTYDvwNOOPdts0+8MG/a1fopJ4n0sCNMOLGBvLirtsdGsk/kWlvc+bPYG1qNsA5n3KAamN4pghmCx2caeNouNa7bnebM9NjbAzNgYxsAAYwgbYxswtsdmexgAm23gu8/HPMbgPRsuXgbFB54118/2eMfGnaKxjSDBmxmwyYHv3sc2NsM+j4vPM9mein4MN2ve+/hdOPZ4szs2imJHwzTAG+WFIAxhE2ouCAgbmwrYBCWBCtgTckgoMo9hvMlU3I9CJnUK4eBR9oC9R+go4A6ACIoyfPv92fvIMzT6/fT7Y7ONOwTqQMUY7g74xRelYoACRZ2O7/3zH27z3rONjo7CGIpOUoE69igbEJTuVER3uhiVLt3Pd4viYkNInW0qFWWiwESnEhh3MpXup06/n0Qjipzv2xjbgD3r9J5EszFU9h7SsQEVkuPCDGRv3LRpeRf7+P73//w3MGbAUKCywdTPTFCIUIwKDEkXG4WpEOb7v//4P0OAbWDDHZswDAkwQyBDMYRJBQAyoPPdZhgUUIaG2FDaCAIBlAQOUFTCNkolge/nPUCYmUAGAAKMAMVgGCKGpCAgMgS+//av/05YmPeZxd2fic02OhcDCX4/xiJJFMXm/n70wxOCkvl+3kfj855tvFnp9xCboc531AkzbSpDQlwqNu+L3zQKuJjn+zNQKC+UfmcbjrKHpoOZczcERAGijpvi7mwfoTt3//Ttfrb5YcPGxZ3DxtAPTkXHpsucjPsJStCxqXN3bEh3mO+VQbHpUWcFKjZQGSouLaL7abg/21PRqenSxf5orjPzvd95w4a4qVMZoA7UmVSEUaeos6PFndCd7uTYsyjqfPWnHsPGHqUOkIJA6He2cclRxGGlUiHnuCjn9MvJ944t4okHqQSFo/AIUadClKBTuVA27neEpU5R+XY/bYbb7EK6A6AD/MmsiERHMbpTZ6ZyTSWsn5pEfO9i2WaXPRSXFrImQ/jh6ZLoEEcFKol+rqMJoSK+57ymuM7rsSiwaCEVqAM6RUGUCnQHijoZJdT53j9/esPo+NDvDMQeoQNDIkQdw+Xuh8kR4ooS4Ir4XtmPLcrtR7QRgwCjYK4sKotrmIoIlYrYqLkL+X/31SODjL/IjgAAAABJRU5ErkJggg==";
                string img06 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAADx0lEQVRIDQXBgYEDSBEEMfXaxEcu5EOi/HkKaf/9z797m8+/PuZ5b/aevdm+3ud5m30+uvO+X3vP9tzvT78/9sx4szfbRzK40/34nY0A3w3vmTFstmdm4222Z9h74G1s3vu4O4Ewb48xA9tzTqPGorxt3mDeZsbY2KaSBO95e3i2IQG8572HZ3u2mbHZ5r0Pi7LNU7pTBzYW2+yxzTbwhsdGHWWAN4wNY5s9jI09ttlD57v3vPe8PRvbvPdsD7M3b+yNzQuPZ354mzY2LzyGDfvgR2je5+nw+Crd8R1BiM6ajvZsWdQZvNlynXkWlYn34fO4LGrIAurnawMulkYbxj3b8dIv3Xl7EjfdH3dyunl7CkXPoPj9gbsfSJ4yGA0Gho1BY7ONQc7pUti8jcUwBCEk2SDKd3uMCYBiCItl2GYSBGwQeybG2zDG0GaljZh8hSEYolPPFnugzgIUQRTQAZfMHpo6CsxwWr4c4T2GYgAMGxuVbYYwNDYqxjygcHR0AtRhngEVHcIDdRIx8zbG3gMVwbzNNtsoYNjY7EGM4Su2IcAQzZYFp8PnWcAk6Dj6PAuOTWUdSNwRK40v5Kyxp1LRz+1Zx9g+CGMAJ9mGgD1D/dQhIjMJ8L3yASiDsvcsFB4dkdFDYNARGR2bbbwvHfcj2gi/n+8KtKzJx4riDdHJdLzPKJWKQjr2mZXQHeCnQyHdsfnqJ7MeYgl11tjUsVnT34/3KDoVeMbfj8/DGAoQohjkGYaS1JlApQ6PAEv9XKcLGMG4Dqkoig4AkHeHSyI2CqVO4X7IUHTpfhJ3FKgsuig2BegkiuarXOezj9CdPdrzwv3p87VL/pGpVDjtY5f8Y+bG3nijp84KEBvlW2ejslHRzNhso1i8rw13usOjWPY+trHZsIEZd4KSkK/ShtS8Taizm3vPGxUdpTtdYKOORsebeZIZBygKVL5hpWOfFC5Jn5kUW+p0537HGIJ96MAuDZsexAagyzZfHR6jTvt4I6ms8aZ4+P3OdRbeM48w7CFB8ftxAdKdYuN7hbPGEX6dz+fpl5Pn2PzM3z//Y6zZyz7nbTRtZgwyD2elkhQ1X6gU2yNCF+84zrz3DKCQws29eT6IsrGNhUnaaNYxvgzJAGxjjzfM9mEP2ftQLMz2sT0GeDBtxEYNqQM6r4ZHZJhK4hCJuEISjUgq3VEqoWLA3oA9mcz3OrA9ldsM98s+IY42be7SnW2Md9jsPcGF4z2kIhJRx+ZLmDr6qAiPgmfmOveX3/3AM9tApYsXsffB1BHAkzDD/wFb3oZ8seBGHwAAAABJRU5ErkJggg==";

                text.Append("<div id=\"" + idBox + "\" class=\"objectBox3D01\" style=\"-webkit-transform: translateX(" + nextPositionX + "px) translateY(" + nextPositionY + "px) translateZ(" + nextPositionZ + "px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"" + img01 + "\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(0px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px; \">");
                text.Append("<img src=\"" + img02 + "\" style=\"-webkit-transform: translateX(-" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"" + img03 + "\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(" + sizeBox + "px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"" + img04 + "\" style=\"-webkit-transform: translateX(" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"" + img05 + "\" style=\"-webkit-transform: translateX(0px) translateY(-" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"" + img06 + "\" style=\"-webkit-transform: translateX(0px) translateY(" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("</div>");
            }
            catch (Exception)
            {

            }

            return text.ToString();
        }

        public static string boxHtmlFirst(int idBox, int sizeBox, int sizePallet, int nextPositionX, int nextPositionY, float nextPositionZ, int sizeRotage)
        {
            StringBuilder text = new StringBuilder();

            try
            {
                text.Append("<div id=\"" + idBox + "\" class=\"objectBox3D01\" style=\"-webkit-transform: translateX(" + nextPositionX + "px) translateY(" + nextPositionY + "px) translateZ(" + nextPositionZ + "px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/01.png\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(0px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px; \">");
                text.Append("<img src=\"img/02.png\" style=\"-webkit-transform: translateX(-" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/03.png\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(" + sizeBox + "px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/04.png\" style=\"-webkit-transform: translateX(" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/05.png\" style=\"-webkit-transform: translateX(0px) translateY(-" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");
                text.Append("<img src=\"img/06.png\" style=\"-webkit-transform: translateX(0px) translateY(" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\">");

                text.Append("<div class=\"di01\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(0px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\"></div>");
                text.Append("<div class=\"di02\" style=\"-webkit-transform: translateX(-" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\"></div>");
                text.Append("<div class=\"di03\" style=\"-webkit-transform: translateX(0px) translateY(0px) translateZ(" + sizeBox + "px) rotateX(0deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\"></div>");
                text.Append("<div class=\"di04\" style=\"-webkit-transform: translateX(" + sizeRotage + "px) translateY(0px) translateZ(" + sizeRotage + "px) rotateX(0deg) rotateY(90deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\"></div>");
                text.Append("<div class=\"di05\" style=\"-webkit-transform: translateX(0px) translateY(-" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px;\"></div>");
                text.Append("<div class=\"di06\" style=\"-webkit-transform: translateX(0px) translateY(" + sizeRotage + "px) translateZ(" + sizeRotage + "px) rotateX(90deg) rotateY(0deg) rotateZ(0deg); width:" + sizeBox + "px; height:" + sizeBox + "px; \"></div>");
                text.Append("</div>");
            }
            catch (Exception)
            {

            }

            return text.ToString();
        }

        [WebMethod]
        public static string ReadTag()
        {
            string ret = "";

            try
            {
                string directory = @"C:\Users\Administrador\Dropbox\SITE-SURVEY-3D\SiteSurveyRFID-V2\SiteSurveyRFID\xml\tags.xml";

                XDocument xdoc = XDocument.Load(directory);

                if (xdoc != null)
                {
                    foreach (var x in xdoc.Elements("caixas").Elements("tag"))
                    {
                        ret += x.Value + ";";
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return ret;
        }

        [WebMethod]
        public static object CreateBox(string box)
        {
            var json = new JavaScriptSerializer();
            string result = "";

            objCreateBox listBox = fastJSON.JSON.Instance.ToObject<objCreateBox>(box);

            objReturnBox returnBox = new objReturnBox();
            returnBox.idBox = listBox.idBox;
            returnBox.translateX = listBox.translateX;
            returnBox.translateY = listBox.translateY;
            returnBox.translateZ = listBox.translateZ;

            int marge = 5;

            int idBox = Convert.ToInt32(listBox.idBox);
            int sizeBox = Convert.ToInt32(listBox.sizeBox);
            int sizePallet = Convert.ToInt32(listBox.sizePallet);
            float sizeCenterPallet = sizePallet / 2;

            int firstPositionX = Convert.ToInt32(listBox.translateX);
            int firstPositionY = Convert.ToInt32(listBox.translateY);
            int firstPositionZ = Convert.ToInt32(listBox.translateZ);

            int nextPositionX = idBox.Equals(1) ? firstPositionX : firstPositionX + sizeBox + marge;
            int nextPositionY = firstPositionY;//.Equals(270) ? firstPositionY : firstPositionY - (sizeBox + marge);
            float nextPositionZ = firstPositionZ.Equals(sizeCenterPallet) ? firstPositionZ : firstPositionZ + marge;

            //Se for primeira caixa calcula apenas marge e tamanho caixa
            int calcMarge = idBox.Equals(1) ? 0 : marge;
            int totalLines = sizeBox > (sizePallet / 2) ? 1 : (sizePallet / (sizeBox + marge));

            try
            {
                if (sizeBox < sizePallet)
                {
                    int sizeRotage = sizeBox / 2;

                    if ((nextPositionX + sizeBox + calcMarge) < sizePallet)
                    {
                        if (totalLinesZ <= totalLines)
                        {
                            if (totalLinesY <= totalLines)
                            {
                                //Melhor caso
                                returnBox.box = boxHtml(idBox, sizeBox, sizePallet, nextPositionX, nextPositionY, nextPositionZ, sizeRotage);

                                returnBox.idBox = "" + (idBox + 1) + "";
                                returnBox.translateX = "" + nextPositionX + "";
                            }
                            else
                            {
                                //Ultrapassa altura do pallet
                            }
                        }
                        else
                        {
                            //inicia nova fileira e nivel
                            nextPositionX = 0;
                            nextPositionY -= sizeBox + marge;

                            if (totalLinesY < totalLines)
                            {
                                //Cria Caixa
                                returnBox.box = boxHtml(idBox, sizeBox, sizePallet, nextPositionX, nextPositionY, nextPositionZ, sizeRotage);

                                returnBox.idBox = "" + (idBox + 1) + "";
                                returnBox.translateX = "" + nextPositionX + "";
                                returnBox.translateY = "" + nextPositionY + "";

                                totalLinesY++;
                                totalLinesZ = 0;
                            }
                            else
                            {
                                //Ultrapassa altura do pallet
                            }
                        }
                    }
                    else
                    {
                        //inicia nova fileira
                        nextPositionX = 0;

                        if (totalLinesZ < totalLines - 1)
                        {
                            if (totalLinesY <= totalLines)
                            {
                                //nova posição
                                nextPositionZ += sizeBox + marge;

                                returnBox.box = boxHtml(idBox, sizeBox, sizePallet, nextPositionX, nextPositionY, nextPositionZ, sizeRotage);

                                returnBox.idBox = "" + (idBox + 1) + "";
                                returnBox.translateX = "" + nextPositionX + "";
                                returnBox.translateZ = "" + nextPositionZ + "";

                                totalLinesZ++;
                            }
                            else
                            {
                                //Ultrapassa altura do pallet
                            }
                        }
                        else
                        {
                            //inicia nova fileira e nivel
                            nextPositionX = 0;
                            nextPositionY -= sizeBox + marge;
                            nextPositionZ = 0 - sizeCenterPallet;

                            if (totalLinesY < totalLines - 1)
                            {
                                //Cria Caixa
                                returnBox.box = boxHtml(idBox, sizeBox, sizePallet, nextPositionX, nextPositionY, nextPositionZ, sizeRotage);

                                returnBox.idBox = "" + (idBox + 1) + "";
                                returnBox.translateX = "" + nextPositionX + "";
                                returnBox.translateY = "" + nextPositionY + "";
                                returnBox.translateZ = "" + nextPositionZ + "";

                                totalLinesY++;
                                totalLinesZ = 0;
                            }
                            else
                            {
                                //Ultrapassa altura do pallet
                            }
                        }
                    }

                    result = json.Serialize(returnBox);
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        [WebMethod]
        public static string ZerarXml()
        {
            xóml();
            return "ok";
        }

        public static void xóml()
        {
            string directory = @"C:\Users\Administrador\Dropbox\SITE-SURVEY-3D\SiteSurveyRFID-V2\SiteSurveyRFID\xml\tags.xml";

            XDocument xdoc = XDocument.Load(directory);

            if (xdoc != null)
            {
                foreach (var x in xdoc.Elements("caixas").Elements("tag"))
                {
                    x.Value = "";
                }
            }

            xdoc.Save(@"C:\Users\Administrador\Dropbox\SITE-SURVEY-3D\SiteSurveyRFID-V2\SiteSurveyRFID\xml\tags.xml");
        }
    }



    public class objReturnBox
    {
        public string idBox { get; set; }
        public string translateX { get; set; }
        public string translateY { get; set; }
        public string translateZ { get; set; }
        public string box { get; set; }
    }

    public class objCreateBox
    {
        public string idBox { get; set; }
        public string sizeBox { get; set; }
        public string sizePallet { get; set; }
        public string translateX { get; set; }
        public string translateY { get; set; }
        public string translateZ { get; set; }
    }
}