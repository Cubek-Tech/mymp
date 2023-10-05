<%@ Page Title="" Language="C#" MasterPageFile="~/massagepartener.Master" AutoEventWireup="true" CodeBehind="signin.aspx.cs" Inherits="RESTFulWCFService.signin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .sign-up-form
        {
            padding: 20px;
            background-color: #eee;
            margin: 20px 0px;
        }
    </style>
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
    <div class="container">
        <h4 class="text-center title ">Login</h4>

        <div class="row">
            <div class="col-sm-3"></div>

            <div class="col-sm-6">

                <div class="sign-up-form">
                    <br />
                    <br />
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="control-label col-sm-3">Email:</label>
                            <div class="col-sm-9">
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtEmail1" runat="server" CssClass="form-control" onblur="getPwd1(this.value);"
                                            onchange="return checkval1(this);" onkeypress="return searchKeyPress1(event);"
                                            TabIndex="1" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="signin" ForeColor="Red" ControlToValidate="txtEmail1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="signin" runat="server" ControlToValidate="txtEmail1" ForeColor="Red" SetFocusOnError="true" ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*"></asp:RegularExpressionValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-sm-3">Password:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtPassword1" TextMode="Password" runat="server" CssClass="form-control" onkeypress="return searchKeyPress1(event);"
                                    TabIndex="2" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="signin" runat="server" ForeColor="Red" ControlToValidate="txtPassword1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="chkremember" runat="server" Checked="true" />Remember me</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-5">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnlogin" CssClass="btn btn-gradiant" runat="server" ValidationGroup="signin" OnClick="btnlogin_Click" Text="Submit" OnClientClick="rememberPwd1();" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-sm-4 text-right">
                                <asp:LinkButton ID="lnkforgot" runat="server" data-toggle="modal" data-target="#forgot-modal">Forgotten password?</asp:LinkButton>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblloginerror" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnlogin" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="col-sm-3"></div>

        </div>



    </div>

         <script type="text/javascript">
             function close_login() {
                 $('#login_close_cross').click();
             }

             function searchKeyPress1(e) {

                 // look for window.event in case event isn't passed in
                 e = e || window.event;
                 if (e.keyCode == 13) {
                     document.getElementById('<%= btnlogin.ClientID%>').click();
                    return false;
                }
                return true;
            }
            function forgot_close_button() {
                $('#ctl00_txtforgotemail').val('');
                $('#ctl00_Label1').hide();
                document.getElementById('ctl00_btnforgot').disabled = '';
                document.getElementById("ctl00_txtforgotemail").disabled = '';
            }
            function checkval1(th) {

                document.getElementById('ctl00_txtEmail').value = document.getElementById('<%= txtEmail1.ClientID%>').value.trim();

              }
              function getPwd1(c_name) {
                  var i, x, y,

                  ARRcookies = document.cookie.split(";");
                  for (i = 0; i < ARRcookies.length; i++) {
                      x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                      y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                      x = x.replace(/^\s+|\s+$/g, "");
                      if (x == c_name) {
                          document.getElementById('<%= txtPassword1.ClientID%>').value = unescape(y);
                          document.getElementById('<%= chkremember.ClientID %>').checked = true;
                      }
                  }
              }
              function rememberPwd1() {

                  var emailid = document.getElementById('<%= txtEmail1.ClientID%>').value;
                  var pwd = document.getElementById('<%= txtPassword1.ClientID%>').value;
                  if (document.getElementById('<%= chkremember.ClientID %>').checked == true) {

                      var date = new Date();
                      date.setTime(date.getTime() + (180 * 24 * 60 * 60 * 1000));
                      var expires = "; expires=" + date.toGMTString();

                      document.cookie = emailid + '=' + pwd + "; expires=" + expires + ";" + ";";
                  }
                  else {
                      var d = new Date();
                      document.cookie = emailid + '=' + pwd + "; expires=" + d.toGMTString() + ";" + ";";
                  }
              }

              function getPwd1(c_name) {

                  var i, x, y,

                  ARRcookies = document.cookie.split(";");
                  for (i = 0; i < ARRcookies.length; i++) {
                      x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                      y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                      x = x.replace(/^\s+|\s+$/g, "");
                      if (x == c_name) {
                          document.getElementById('<%= txtPassword1.ClientID%>').value = unescape(y);
                          document.getElementById('<%= chkremember.ClientID %>').checked = true;
                      }
                  }
              }

              function searchKeyPress(e) {

                  // look for window.event in case event isn't passed in
                  e = e || window.event;
                  if (e.keyCode == 13) {
                      document.getElementById('<%= btnlogin.ClientID%>').click();
                return false;
            }
            return true;
        }
        </script>
</asp:Content>
