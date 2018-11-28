<%@ Page Title="Customer Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginCustomer.aspx.cs" Inherits="CarHireWebApp.Account.LoginCustomer" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3><%: Title %>.</h3>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>Use a customer account to log in.</h4>
                    <hr />
                      <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="userNameTxt" CssClass="col-md-2 control-label">User Name</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="userNameTxt" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="userNameTxt"
                                CssClass="text-danger" ErrorMessage="The user name is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="passwordTxt" CssClass="col-md-2 control-label">Password</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="passwordTxt" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="passwordTxt" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="rememberMeChk" />
                                <asp:Label runat="server" AssociatedControlID="rememberMeChk">Remember me?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="CustomerLogInBtn" Text="Log in" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
                <p>
                    <asp:HyperLink runat="server" ID="forgotPasswordHyperlink" NavigateUrl="~/Account/Forgot" ViewStateMode="Disabled">Forgot password?</asp:HyperLink>
                </p>
                <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" NavigateUrl="~/RegisterCustomer" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                </p>
            </section>
        </div>
    </div>
</asp:Content>
