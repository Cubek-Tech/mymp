<%@ Page Title="" Language="C#" MasterPageFile="~/SearchPartener.Master" AutoEventWireup="true" CodeBehind="EditDetails.aspx.cs" Inherits="RESTFulWCFService.MassagePartener.User.EditDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Edit Profile | My Massage Partner</title>
    <meta name="Description" content="Edit your profile | My Massage Partner">
    <meta name="Keywords" content="body massage nearby | erotic massage near me | happy ending massage near me | need massage | Full Body Massage | Female to Male Massage | Full body Massage | Sensual Massage">

    <style>
        input[type="file"] {
            visibility: hidden;
        }

        .clas {
            display: none;
        }

        .lblother_images {
            position: absolute;
        }

        .font-weight {
            font-weight: 100;
            height: 30px;
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
        }

        .font-weight1 {
            font-weight: 100;
            height: 30px;
            padding: 5px 10px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
            margin-left: -8px;
            position: absolute;
        }
    </style>
    <script type="text/javascript">
        function previewImage(input, elem, divv) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    /* alert(e.target.result);*/
                    $('#' + elem).css('display', 'inherit');
                    $('#' + elem).attr('src', '');
                    $('#' + elem).attr('src', e.target.result);

                }

                reader.readAsDataURL(input.files[0]);
                var elem1 = document.getElementById(divv);
                elem1.style.visibility = 'visible';
            }
        };

        function Delete_image_click(ele_btn, ele_img, fileupl, hdnfield, hdnStatus) {

            var elem1 = document.getElementById(ele_btn);
            var image = document.getElementById(ele_img);
            var flp = document.getElementById(fileupl);
            var hdnStatus = document.getElementById(hdnStatus);

            flp.value = '';
            hdnStatus.value = 'D';

            $('#' + ele_img).css('display', 'none');
            //alert(image);
            elem1.style.visibility = 'hidden';
        };
        function CheckFieldLength1(mc) {
            var fn = document.getElementById('<%= txtdescription.ClientID%>');
            if (fn != null) {
                var len = fn.value.length;
                if (len >= mc) {
                    fn.value = fn.value.substring(0, mc);
                    len = mc;
                }
                //  fn.value = fn.value.substring(0, maxLength);
                document.getElementById('<%= remaining.ClientID%>').innerHTML = mc - len;
            }
        }

        function CheckFieldLength2(mc) {
            var fn = document.getElementById('<%=txtexpe_quali.ClientID%>');
            if (fn != null) {
                var len = fn.value.length;
                if (len >= mc) {
                    fn.value = fn.value.substring(0, mc);
                    len = mc;
                }
                document.getElementById('<%=Label1.ClientID%>').innerHTML = mc - len;
            }
        }

        function CheckFieldLength3(mc) {
            var fn = document.getElementById('<%=txtmassage_expe.ClientID%>');
            if (fn != null) {
                var len = fn.value.length;
                if (len >= mc) {
                    fn.value = fn.value.substring(0, mc);
                    len = mc;
                }
                document.getElementById('<%= Label2.ClientID%>').innerHTML = mc - len;
            }
        }

        function saveimage() {
            document.getElementById('<%= lnkotherimages.ClientID %>').click();
        }
        function SearchEmployees(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div class="overlay">
            </div>
            <div class="UpdateProgress_style">
                <img src="<%#Constants__.WEB_ROOT%>/Images/ajax-loader1.gif" alt="ajax" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="overlay">
    </div>
    <asp:Button runat="server" CausesValidation="false" CssClass="clas"
        ID="lnkotherimages" Text="Upload" OnClick="lnkotherimages_Click" />
    <section class="wrapper">
        <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div class="container wrapper-content ">
            <div class="main-content ">
                <div class="edit-profile-section">
                    <div class="row ">
                        <div class="col-sm-12">
                            <h2 class="title text-center">Edit Or Update Your Profile</h2>
                        </div>
                        <div class="col-sm-8">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Name:</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="txtname" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Email:</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="txtEmailID" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailID" ValidationGroup="reg" ForeColor="Red" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Password:</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPassword1" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="txtPassword1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Your Gender:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="M" Selected>Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Date Of Birth:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:DropDownList ID="ddlmonth" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Month"></asp:ListItem>
                                            <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlday" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlyear" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlday" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlmonth" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlyear" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Contact no.:</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtcontact" runat="server" CssClass="form-control" oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*?)\..*/g, '$1');"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Massage Types Can Serve.:</label>
                                    <div class="col-sm-8">
                                        <div style="height: 120px; overflow: auto; border: 1px solid lightgrey; color: gray; padding-left: 8px; background-color: white">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="90%" CssClass="font-weight1" onkeyup="SearchEmployees(this,'#ddlmassagetypes');"
                                                placeholder="Write Massage Type">
                                            </asp:TextBox>
                                            
                                            <br />
                                            <br /> 
                                            <asp:CheckBoxList ID="ddlmassagetypes" runat="server" CssClass="font-weight" ClientIDMode="Static"></asp:CheckBoxList>
                                           
                                            <script>
                                                function ValidateCheckBoxList(sender, args) {
                                                    
                                                    var checkBoxList = document.getElementById("<%=ddlmassagetypes.ClientID %>");
                                                    var checkboxes = checkBoxList.getElementsByTagName("input");
                                                    var isValid = false;
                                                    for (var i = 0; i < checkboxes.length; i++) {
                                                        if (checkboxes[i].checked) {
                                                            isValid = true;
                                                            break;
                                                        }
                                                    }
                                                    args.IsValid = isValid;
                                                }
                                            </script>
                                                
                                        </div>
                                    </div>
                                </div>
                               
                                  <div class="row">
                                   <div class="col-sm-6" style="margin-left: 243px;margin-top: -7px;">
                                        <asp:Label ID="lbllstCondition" runat="server"></asp:Label>
                                        <asp:CustomValidator ID="CustomValidator4" ErrorMessage="Please select any one Massage Type!!" ValidationGroup="reg" ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" SetFocusOnError="true"/>
                                      <%-- <asp:CustomValidator ID="CustomValidator3" runat="server" CssClass="ErrorMsg" ErrorMessage="please select any massage type" SetFocusOnError="true" ForeColor="Red"
                                                    ValidationGroup="reg" ClientValidationFunction="ddlmassagetypes"></asp:CustomValidator></div>--%>
                                   <div class="col-sm-4">
                                  </div>
                                 <%--</div>--%>   
                               </div>
                                      </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Desired Partner's Gender:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:DropDownList ID="ddlpartgen" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Male" Value="M" Selected></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                            <asp:ListItem Text="Any" Value="O"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Desired Partner Age:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:DropDownList ID="ddlpartage" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Country:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlcountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlcountry" SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="reg"></asp:RequiredFieldValidator>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">State:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">City:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlcity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcity_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Area:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlcity" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Zip Pin code:</label>
                                    <div class="col-sm-8 form-inline">
                                        <asp:TextBox ID="txtzip" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Upload Your Pictures:</label>
                                    <div class="col-sm-8 form-inline">
                                        <a href="#" data-toggle="modal" data-target="#upload-images" tooltip="Select Images">
                                            <img src="<%=Constants__.WEB_ROOT_CDN %>/image/upload_img.png" height="50" tooltip="Select Images" /></a>
                                        <asp:Label ID="lblerroruploadimages" runat="server" Visible="false" CssClass="lblother_images"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" style="display: none">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="fuotherimages" onchange="ShowUploadBtn_others();" AllowMultiple="true"
                                                CssClass="search_text__main_new_browse" TabIndex="17" runat="server" />
                                            <br>
                                            <br>
                                            <p style="color: Red; font-size: 11px;">
                                                Choose max. 8 Images to Upload at Once
                                            </p>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lnkotherimages" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div style="margin-top: 10px; display: table">
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Sorry, you can upload only jpeg,gif,png format."
                                                    ValidationExpression="^.+\.((jpg)|(JPG)|(gif)|(GIF)|(jpeg)|(JPEG)|(png)|(PNG))$"
                                                    ValidationGroup="upImg" ControlToValidate="fuotherimages" Display="Dynamic" CssClass="ErrorMsg">
                                                </asp:RegularExpressionValidator>
                                                <%--<asp:Button runat="server" CausesValidation="false" CssClass="hideItem btnupload"
                                                     OnClick="lnkotherimages_Click" Text="Upload" />--%>
                                                <span id="uploadotherimagesmsg" class="ErrorMsg"></span>
                                                <%-- <asp:Label ID="lblotherimages" runat="server" Text="" ForeColor="#FF0066" Font-Size="10px"
                                                                        CssClass="hideItem_rec_fixed"> </asp:Label>--%>
                                                <asp:ImageButton Visible="true" Style="margin-left: 6px; float: right; margin-top: -7px; height: 15px; width: 15px; padding: 0px; object-fit: contain;"
                                                    ID="ImgBtn_otherimg" runat="server" ImageUrl="/image/cross.PNG" ToolTip="Delete all other images"
                                                    OnClick="Delete_other_image_click2" CausesValidation="false" OnClientClick="confirm('Are you sure about all images deletion?');" />
                                                <asp:Label ID="Lblerrorotherimage" CssClass="ErrorMsg sucess-msg" runat="server" ></asp:Label>
                                                <div id="Div7" class="ErrorMsg ">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <%--  <asp:ImageButton Visible="true" Style="margin-left: 6px; float: right; margin-top: -7px; height: 15px; width: 15px; padding: 0px; object-fit: contain;"
                                                                        ID="ImgBtn_otherimg" runat="server" ImageUrl="/image/cross.PNG" ToolTip="Delete all other images"
                                                                         CausesValidation="false" OnClientClick="confirm('Are you sure about all images deletion?');" />
                                                                    <asp:Label ID="Lblerrorotherimage" CssClass="ErrorMsg" runat="server"></asp:Label>
                                                                    <div id="Div4" class="ErrorMsg">
                                                                    </div>--%>
                                    </div>
                                    <%--   </div>
                                </div>--%>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Please describe yourself in a few words:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" MaxLength="100" onkeydown="CheckFieldLength1(100)" TextMode="MultiLine" Height="91"></asp:TextBox>
                                            <div class="ErrorMsg">
                                                <asp:Label ID="remaining" runat="server" Text="100"></asp:Label>
                                                &nbsp; characters remaining.
                                                    <br>
                                                <asp:CustomValidator ID="CustomValidator6" runat="server" CssClass="ErrorMsg" ErrorMessage="5 numeric digit not allow."
                                                    ValidationGroup="reg" ClientValidationFunction="CheckTextString23"></asp:CustomValidator>
                                            </div>
                                            <script type="text/javascript">


                                                function CheckTextString23(source, args) {

                                                    var arraystr = ($("#<%=  txtdescription.ClientID %>").val()).split("");
                                                    var count = 0;

                                                    for (var i = 0; i <= arraystr.length; i++) {
                                                        if ($.isNumeric(arraystr[i])) { count = count + 1; }
                                                        // else
                                                        // { count = 0; }
                                                        if (count > 4) {

                                                            args.IsValid = false;
                                                        }
                                                    }
                                                }
                                            </script>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Please detail any kind of experience and qualifications whether formal or not :</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtexpe_quali" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="300" Height="91" onkeydown="CheckFieldLength2(300)"></asp:TextBox>
                                            <div class="ErrorMsg">
                                                <asp:Label ID="Label1" runat="server" Text="300"></asp:Label>
                                                &nbsp; characters remaining.
                                                    <br>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" CssClass="ErrorMsg" ErrorMessage="5 numeric digit not allow."
                                                    ValidationGroup="reg" ClientValidationFunction="CheckTextString232"></asp:CustomValidator>
                                                <script type="text/javascript">


                                                    function CheckTextString232(source, args) {

                                                        var arraystr = ($("#<%=  txtexpe_quali.ClientID %>").val()).split("");
                                                        var count = 0;

                                                        for (var i = 0; i <= arraystr.length; i++) {
                                                            if ($.isNumeric(arraystr[i])) { count = count + 1; }
                                                            // else
                                                            // { count = 0; }
                                                            if (count > 4) {

                                                                args.IsValid = false;
                                                            }
                                                        }
                                                    }
                                                </script>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">Please detail Massage experience:</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtmassage_expe" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="500" Height="91" onkeydown="CheckFieldLength3(500)"></asp:TextBox>
                                            <div class="ErrorMsg">
                                                <asp:Label ID="Label2" runat="server" Text="500"></asp:Label>
                                                &nbsp; characters remaining.
                                                    <br>
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" CssClass="ErrorMsg" ErrorMessage="5 numeric digit not allow."
                                                    ValidationGroup="reg" ClientValidationFunction="CheckTextString233"></asp:CustomValidator>
                                                <script type="text/javascript">


                                                    function CheckTextString233(source, args) {

                                                        var arraystr = ($("#<%=  txtmassage_expe.ClientID %>").val()).split("");
                                                        var count = 0;

                                                        for (var i = 0; i <= arraystr.length; i++) {
                                                            if ($.isNumeric(arraystr[i])) { count = count + 1; }
                                                            // else
                                                            // { count = 0; }
                                                            if (count > 4) {

                                                                args.IsValid = false;
                                                            }
                                                        }
                                                    }
                                                </script>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-4">I am a:</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlIam" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="T">Massage Therapist / Professional</asp:ListItem>
                                                <asp:ListItem Value="M">Manual Massager / Enthusiast</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="reg" ForeColor="Red" ControlToValidate="ddlIam" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-offset-4 col-sm-8">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnsave" runat="server" CssClass="btn btn-gradiant" Text="Update" OnClick="btnsave_Click" ValidationGroup="reg"/>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="form-group form-group-sm">
                                        <div class="col-sm-offset-3 col-sm-9">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </section>
    <div class="modal fade" id="upload-images" role="dialog">
        <div class="modal-dialog ">
            <!-- Modal content-->
            <div class="modal-content ">
                <div class="modal-header model-header-orange">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center ">Upload Images</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-3 col-xs-6">
                            <div class="upload-imagebox">
                                <span style="position: absolute">
                                    <asp:Image ID="img1" runat="server" CssClass="pop_images" ClientIDMode="Static" /></span>
                                <center>
                                    <asp:FileUpload ID="flupload1" runat="server" ClientIDMode="Static" onchange="previewImage(this,'img1','Div45');"></asp:FileUpload>
                                    <span style="position: absolute">
                                        <div id="Div45" style="visibility: hidden" class="delete-image">
                                            <img src="<%=Constants__.WEB_ROOT_CDN %>/image/cross.PNG" class="delete-image" style="margin-left: 59px; position: absolute; margin-top: -32px; cursor: pointer" tooltip="Delete" onclick="Delete_image_click('Div45','img1','flupload1','hdn_Img_other','hdn_delete');" />
                                        </div>
                                    </span>
                                    <label for="flupload1">
                                        <img src="<%=Constants__.WEB_ROOT_CDN %>/image/upload-image.png" class="upload-btn" />
                                    </label>
                                </center>
                                <asp:HiddenField ID="hdn_Img_other" runat="server" Value="0" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdn_delete" runat="server" Value="0" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="col-sm-3 col-xs-6">
                            <div class="upload-imagebox">
                                <span style="position: absolute">
                                    <asp:Image ID="img2" runat="server" CssClass="pop_images" ClientIDMode="Static"  /></span>
                                <center>
                                    <asp:FileUpload ID="flupload2" runat="server" ClientIDMode="Static" onchange="previewImage(this,'img2','Div1');"></asp:FileUpload>
                                    <span style="position: absolute">
                                        <div id="Div1" style="visibility: hidden" class="delete-image">
                                            <img src="<%=Constants__.WEB_ROOT_CDN %>/image/cross.PNG" class="delete-image" style="margin-left: 59px; position: absolute; margin-top: -32px; cursor: pointer" tooltip="Delete" onclick="Delete_image_click('Div1','img2','flupload2','hdn_Img_other1','hdn_delete1');" />
                                        </div>
                                    </span>
                                    <label for="flupload2">
                                        <img src="<%=Constants__.WEB_ROOT_CDN %>/image/upload-image.png" class="upload-btn" />
                                    </label>
                                </center>
                                <asp:HiddenField ID="hdn_Img_other1" runat="server" Value="0" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdn_delete1" runat="server" Value="1" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="col-sm-3 col-xs-6">
                            <div class="upload-imagebox">
                                <span style="position: absolute">
                                    <asp:Image ID="img3" runat="server" CssClass="pop_images" ClientIDMode="Static"  /></span>
                                <center>
                                    <asp:FileUpload ID="flupload3" runat="server" ClientIDMode="Static" onchange="previewImage(this,'img3','Div2');"></asp:FileUpload>
                                    <span style="position: absolute">
                                        <div id="Div2" style="visibility: hidden" class="delete-image">
                                            <img src="<%=Constants__.WEB_ROOT_CDN %>/image/cross.PNG" class="delete-image" style="margin-left: 59px; position: absolute; margin-top: -32px; cursor: pointer" tooltip="Delete" onclick="Delete_image_click('Div2','img3','flupload3','hdn_Img_other2','hdn_delete2');" />
                                        </div>
                                    </span>
                                    <label for="flupload3">
                                        <img src="<%=Constants__.WEB_ROOT_CDN %>/image/upload-image.png" class="upload-btn" />
                                    </label>
                                </center>
                                <asp:HiddenField ID="hdn_Img_other2" runat="server" Value="0" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdn_delete2" runat="server" Value="2" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="col-sm-3 col-xs-6">
                            <div class="upload-imagebox">
                                <span style="position: absolute">
                                    <asp:Image ID="img4" runat="server" CssClass="pop_images" ClientIDMode="Static"  /></span>
                                <center>
                                    <asp:FileUpload ID="flupload4" runat="server" ClientIDMode="Static" onchange="previewImage(this,'img4','Div3');"></asp:FileUpload>
                                    <span style="position: absolute">
                                        <div id="Div3" style="visibility: hidden" class="delete-image">
                                            <img src="<%=Constants__.WEB_ROOT_CDN %>/image/cross.PNG" class="delete-image" style="margin-left: 59px; position: absolute; margin-top: -32px; cursor: pointer" tooltip="Delete" onclick="Delete_image_click('Div3','img4','flupload4','hdn_Img_other3','hdn_delete3');" />
                                        </div>
                                    </span>
                                    <label for="flupload4">
                                        <img src="<%=Constants__.WEB_ROOT_CDN %>/image/upload-image.png" class="upload-btn" />
                                    </label>
                                </center>
                                <asp:HiddenField ID="hdn_Img_other3" runat="server" Value="0" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdn_delete3" runat="server" Value="3" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="col-sm-3 col-xs-6">
                            <div class="upload-imagebox">
                                <span style="position: absolute">
                                    <asp:Image ID="img5" runat="server" CssClass="pop_images" ClientIDMode="Static"  /></span>
                                <center>
                                    <asp:FileUpload ID="flupload7" runat="server" ClientIDMode="Static" onchange="previewImage(this,'img5','Div4');"></asp:FileUpload>
                                    <span style="position: absolute">
                                        <div id="Div4" style="visibility: hidden" class="delete-image">
                                            <img src="<%=Constants__.WEB_ROOT_CDN %>/image/cross.PNG" class="delete-image" style="margin-left: 59px; position: absolute; margin-top: -32px; cursor: pointer" tooltip="Delete" onclick="Delete_image_click('Div4','img5','flupload7','hdn_Img_other4','hdn_delete4');" />
                                        </div>
                                    </span>
                                    <label for="flupload7">
                                        <img src="<%=Constants__.WEB_ROOT_CDN %>/image/upload-image.png" class="upload-btn" />
                                    </label>
                                </center>
                                <asp:HiddenField ID="hdn_Img_other4" runat="server" Value="4" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdn_delete4" runat="server" Value="4" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="col-sm-3 col-xs-6">
                            <div class="upload-imagebox">
                                <span style="position: absolute">
                                    <asp:Image ID="img6" runat="server" CssClass="pop_images" ClientIDMode="Static" Height="100" Width="118" /></span>
                                <center>
                                    <asp:FileUpload ID="flupload8" runat="server" ClientIDMode="Static" onchange="previewImage(this,'img6','Div5');"></asp:FileUpload>
                                    <span style="position: absolute">
                                        <div id="Div5" style="visibility: hidden" class="delete-image">
                                            <img src="<%=Constants__.WEB_ROOT_CDN %>/image/cross.PNG" class="delete-image" style="margin-left: 59px; position: absolute; margin-top: -32px; cursor: pointer" tooltip="Delete" onclick="Delete_image_click('Div5','img6','flupload8','hdn_Img_other5','hdn_delete5');" />
                                        </div>
                                    </span>
                                    <label for="flupload8">
                                        <img src="<%=Constants__.WEB_ROOT_CDN %>/image/upload-image.png" class="upload-btn" />
                                    </label>
                                </center>
                                <asp:HiddenField ID="hdn_Img_other5" runat="server" Value="5" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdn_delete5" runat="server" Value="5" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a onclick="saveimage();">
                        <button type="button" class="btn btn-default">Save</button>
                    </a>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>
    <script>

        //function showmap() {
        //    showAddress('noAddress');
        //}

      <%--  function showAddress(txt) {
            document.getElementById("lblMessageMap").innerHTML = "";
            if (txt == 'Address') {
                var address = (document.getElementById('<%= txtAddress.ClientID%>').value + ' ' + $('#<%= ddlArea.ClientID %> option:selected').text() + ' ' + $('#<%= ddlCity.ClientID %> option:selected').text() + ' ' + $('#<%= ddlState.ClientID %> option:selected').text() + ' ' + $('#<%= ddlCountry.ClientID %> option:selected').text()).replace('---Select---', '');
            } else {
                var address = ($('#<%= ddlArea.ClientID %> option:selected').text() + ' ' + $('#<%= ddlCity.ClientID %> option:selected').text() + ' ' + $('#<%= ddlState.ClientID %> option:selected').text() + ' ' + $('#<%= ddlCountry.ClientID %> option:selected').text()).replace('---Select---', '');
            }
            var geocoder;
            geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    var latlng = results[0].geometry.location;
                    load(latlng.lat(), latlng.lng());
        x.png

                }
            });
        }--%>

        function ShowUploadBtn_others() {
            var fileCount = document.getElementById('<%= fuotherimages.ClientID%>').files.length;
            document.getElementById('uploadotherimagesmsg').innerHTML = '';
            if (fileCount < 9) // Selected images with in 10 count
            {
                var upid1 = document.getElementById('<%= fuotherimages.ClientID%>');
                  var upvalue1 = upid1.value;
                  var extension1 = upvalue1.substring(upvalue1.lastIndexOf('.') + 1);

                  if (extension1 == 'jpg' || extension1 == 'jpeg' || extension1 == 'bmp' || extension1 == 'gif' || extension1 == 'png') {
                      document.getElementById('<%=  lnkotherimages.ClientID %>').click();

                } else {
                    document.getElementById('uploadotherimagesmsg').innerHTML = '';
                }
            } else {
                document.getElementById('uploadotherimagesmsg').innerHTML = 'Choose Maximum 8 images.';
            }
        }

        function show_delete_image_option(dv_id) {
            var elem1 = document.getElementById(dv_id);
            elem1.style.visibility = 'visible';
        }
    </script>

</asp:Content>
