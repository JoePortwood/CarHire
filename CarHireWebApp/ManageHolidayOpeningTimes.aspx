<%@ Page Title="ManageHolidayOpeningTimes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageHolidayOpeningTimes.aspx.cs" Inherits="CarHireWebApp.ManageHolidayOpeningTimes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css">
  <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
  <link rel="stylesheet" href="/resources/demos/style.css">
    <script>
        $(function () {
            $("[id$=HolidayStartAdd]").datepicker({
                dateFormat: "dd/mm/yy",
                minDate: 0,
                beforeShowDay: existingHolidays
            });
        });

        function existingHolidays(date) {
            var JSONHolidayDates = '<%=new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(holidayDates)%>';
            var holidayDatesArray = JSON.parse(JSONHolidayDates);
            var m = date.getMonth(), d = date.getDate(), y = date.getFullYear();

            for (i = 0; i < holidayDatesArray.length; i++) {

                //Check dates on calendar against selected holiday dates and disable if holiday is already entered
                if ($.inArray(d + '-' + (m + 1) + '-' + y, holidayDatesArray) != -1) {
                    return [false];
                }
            }
            return [true];
        }

        function clearText() {
            if ($("#MainContent_ClosedAdd").is(':checked')) {
                $("#MainContent_AltCloseTimeAdd").val("");
                $("#MainContent_AltOpenTimeAdd").val("");
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
    <asp:Table runat="server" ID="holidayOpeningTimesTbl" Visible="false" CssClass="table table-striped table-bordered"></asp:Table>
</asp:Content>