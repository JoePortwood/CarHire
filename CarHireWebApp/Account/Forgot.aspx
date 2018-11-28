<%@ Page Title="Forgot password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="CarHireWebApp.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3><%: Title %>.</h3>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>Forgot your password?</h4>
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
                        <asp:Label runat="server" AssociatedControlID="emailTxt" CssClass="col-md-2 control-label">Email</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="emailTxt" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="emailTxt"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="Forgot" Text="Submit" CssClass="btn btn-default" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
