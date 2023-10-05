<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="updateQR.aspx.cs" Inherits="RESTFulWCFService.Admin.updateQR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

    <script>
        function previewImage(input, elem, divv) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //alert(e.target.result);
                    $('#' + elem).css('display', 'inherit');
                    $('#' + elem).attr('src', '');
                    $('#' + elem).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                //var elem1 = document.getElementById(divv);
                //elem1.style.visibility = 'visible';
            }
        };
        function previewImage_paytm(input, elem1, divv) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#' + elem1).css('display', 'inherit');
                    $('#' + elem1).attr('src', '');
                    $('#' + elem1).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        };
        function Delete_image_click(ele_btn, ele_img, fileupl) {

            var elem1 = document.getElementById(ele_btn);
            var image = document.getElementById(ele_img);
            var flp = document.getElementById(fileupl);
            flp.value = '';
            $('#' + ele_img).css('display', 'none');
            //alert(image);
            elem1.style.visibility = 'hidden';
        };
    </script>
    <style>
        .delete-image {
            width: 23px;
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <div style="height: auto; width: 800px; border: 1px solid grey; padding: 0px 0px 0px 52px; box-shadow: grey 7px 4px 4px -2px">
            <label>
                Add QR Image
                              
            </label>
            <table>
                <tr>
                    <td>
                        <label for="fluUpload" style="cursor: pointer">
                            <img id="img1" src="<%=Constants__.WEB_ROOT %>/Admin/Images/img_upload.png" style="height: 62px; border: 1px solid #cb202d;" />
                        </label>
                        <asp:FileUpload ID="fluUpload" runat="server" ClientIDMode="Static" onchange="previewImage(this,'img1','Div45');" Style="visibility: hidden"></asp:FileUpload>
                    </td>
                </tr>

                <tr>
                    <td>

                        <asp:Button ID="btnUpdate" runat="server" Text="Upload QR Code" CssClass="btn btn-gradiant" OnClick="btnUpdate_Click"></asp:Button>
                        <br />
                        <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>

                    </td>
                </tr>
            </table>
            </div>
          <div style="height: auto; width: 800px; border: 1px solid grey; padding: 0px 0px 0px 52px; box-shadow: grey 7px 4px 4px -2px">
            <label>
                Add Paytm Image
                              
            </label>
            <table>
                <tr>
                    <td>
                        <label for="FileUpload1" style="cursor: pointer">
                            <img id="img2" src="<%=Constants__.WEB_ROOT %>/Admin/Images/img_upload.png" style="height: 62px; border: 1px solid #cb202d;" />
                        </label>
                        <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" onchange="previewImage_paytm(this,'img2','Div46');" Style="visibility: hidden"></asp:FileUpload>
                    </td>
                </tr>

                <tr>
                    <td>

                        <asp:Button ID="Button1" runat="server" Text="Upload Paytm Image" CssClass="btn btn-gradiant" OnClick="btnpaytm_Click"></asp:Button>
                        <br />
                        <asp:Label ID="Label2" runat="server" ForeColor="Green"></asp:Label>

                    </td>
                </tr>
            </table>
            </DIV>
    </center>
    <div id="divBackground" class="modal">
    </div>
    <div id="divImage">
        <table style="height: 100%; width: 100%">
            <tr>
                <td valign="middle" align="center">
                    <img id="imgLoader" alt="" src="images/loader.gif" />
                    <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
                </td>
            </tr>
            <tr>
                <td align="center" valign="bottom">
                    <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
                </td>
            </tr>
        </table>
    </div>
    <style type="text/css">
        /*body
{
    margin: 0;
    padding: 0;
    height: 100%;
}*/
        .modal {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }

        #divImage {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }
    </style>
    <script type="text/javascript">
        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");
            imgLoader.style.display = "block";
            img.onload = function () {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
        }
    </script>

</asp:Content>
