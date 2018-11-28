<%@ Page Title="Add Opening Times" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOpeningTime.aspx.cs" Inherits="CarHireWebApp.AddOpeningTime" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register TagPrefix="Ajaxified" Assembly="Ajaxified" Namespace="Ajaxified" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function myFunction() {
            alert("gd");
            var txt = document.getElementById("1_close");
            alert(txt);
            txt.textContent = "hi";
        }

        function clearText(dayNo) {
            if ($("#MainContent_" + dayNo + "_Chk").is(':checked')) {
                $("#MainContent_" + dayNo + "_open").val("");
                $("#MainContent_" + dayNo + "_close").val("");
            }
        }

    </script>
    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="inputErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>
    <asp:Label runat="server" ID="timeSavedLbl" Font-Bold="true" ForeColor="Blue"></asp:Label>

    <div class="spacer"></div>
    <h3>Select Location</h3>
    <asp:DropDownList runat="server" ID="locationDdl" CssClass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="LocationDdl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

    <div class="spacer"></div>
    <asp:Table runat="server" ID="openingTimesTbl" Visible="false" CssClass="table table-striped table-bordered"></asp:Table>

    <div class="spacer"></div>
    <asp:Button runat="server" ID="updateTimesBtn" Visible="false" Text="Update Opening Times" type="button" class="btn btn-primary" OnClick="AddUpdateBtn_Click"></asp:Button>

</asp:Content>
