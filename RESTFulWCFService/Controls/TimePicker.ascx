<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_TimePicker" Codebehind="TimePicker.ascx.cs" %>




<div class="datepicker">
<asp:TextBox ID="datepicker1" runat="server" CssClass="hajanDatePicker"></asp:TextBox>
<asp:TextBox ID="datepicker2" runat="server" CssClass="hajanDatePicker"></asp:TextBox>
<asp:HiddenField ID="hdnStartDate" runat="server" />

</div>
<script>


  
    var startDate = new Date(document.getElementById("<%=hdnStartDate.ClientID%>").value);  //(2012, 6, 12);
    var endDate = new Date(document.getElementById("<%=hdnStartDate.ClientID%>").value);
    endDate.setDate(endDate.getDate() + 92);

    var minDate = new Date(startDate);
    var maxDate = new Date(endDate);


    $(document).ready(function() {

        $("#<%=datepicker1.ClientID%>").datepicker({ showOn: "both", buttonImage: '<%=Constants__.PhysicalLocalHostPath%>jquery-ui-1.8.14.custom/images/cal.gif',
            buttonText: 'Select Date', buttonImageOnly: true, showButtonPanel: true,
            showAnim: 'fadeIn',
            yearRange: '1950:2050',
            dateFormat: 'd-M-yy',
            stepMonths: 3,
            numberOfMonths: 3,
            minDate: minDate,
            maxDate: maxDate,
            onSelect: function() {

                var fecha = $(this).datepicker('getDate');
                var nextend = fecha;
                nextend.setDate(nextend.getDate() + 92);


                $("#<%=datepicker2.ClientID%>").datepicker({

                    showOn: "both", buttonImage: '<%=Constants__.PhysicalLocalHostPath%>jquery-ui-1.8.14.custom/images/cal.gif',
                    buttonText: 'Select Date', buttonImageOnly: true, showButtonPanel: true,
                    showAnim: 'fadeIn',
                    yearRange: '1950:2050',
                    dateFormat: 'd-M-yy',
                    stepMonths: 3,
                    numberOfMonths: 3
                });
                $("#<%=datepicker2.ClientID%>").datepicker('option', { minDate: new Date(($("#<%=datepicker1.ClientID%>").datepicker('getDate'))), maxDate: new Date(nextend) });
                $("#<%=datepicker2.ClientID%>").datepicker('setDate', nextend);
            }

        });
        $("#<%=datepicker1.ClientID%>").datepicker('setDate', startDate);

        $("#<%=datepicker1.ClientID%>").attr('readOnly', 'true');
        $("#<%=datepicker2.ClientID%>").attr('readOnly', 'true');
    }); 
     
</script>
