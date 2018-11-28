<%@ Page Title="View Location" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewLocations.aspx.cs" Inherits="CarHireWebApp.View_Locations" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script type="text/javascript"
      src="https://maps.googleapis.com/maps/api/js?key=YOURKEY">
    </script>--%>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&signed_in=true&libraries=places"></script>
    <script type="text/javascript">
        var JSONLocations = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(locations)%>';
        var JSONOpeningTimes = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(openingTimes)%>';
        var JSONHolidayOpeningTimes = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(holidayOpeningTimes)%>';
    </script>
    <script src="Scripts/mapandlocations.js"></script>

    <asp:Label runat="server" ID="generalErrorLbl" Font-Bold="true" ForeColor="Red"></asp:Label>

    <div class="spacer"></div>
    <input class="form-control" type="text" id="addressInputTxt" placeholder="Enter your address for nearest dealer" size="500" />

    <div id="map-canvas"></div>

    <asp:Table runat="server" BorderWidth="0px" Visible="false" ID="LocationsTbl">
    </asp:Table>
</asp:Content>
