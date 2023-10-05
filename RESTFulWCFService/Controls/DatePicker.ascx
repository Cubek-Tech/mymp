<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_DatePicker" Codebehind="DatePicker.ascx.cs" %>


<script type="text/javascript">


    $(function() {

        var click_yes = document.getElementById("<%=hdnOnclick.ClientID%>").value;
        var dnow = new Date();
        var Month = document.getElementById("<%=hdnMonth.ClientID%>").value;
        var Year = document.getElementById("<%=hdnyear.ClientID%>").value;
        var Todate = document.getElementById("<%=hdnTodate.ClientID%>").value;
        var PastDate = document.getElementById("<%=hdnpastDate.ClientID%>").value;
        if (Todate == 'true' || Todate == 'True') {
            var myDate = new Date();
            //var prettyDate = (myDate.getMonth() + 1) + '/' + myDate.getDate() + '/' + myDate.getFullYear();
            var month = new Array(12);
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";
            var prettyDate = myDate.getDate() + '-' + month[myDate.getMonth()] + '-' + myDate.getFullYear();
            $("#<%=datepicker.ClientID%>").val(prettyDate.toString());
        }
        if (PastDate == 'true' || PastDate == 'True') {
            var myDate = new Date();
            var month = new Array(12);
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";
            var prettyDate = myDate.getDate() + '-' + month[myDate.getMonth()] + '-' + myDate.getFullYear();
            //myDate.getMonth() + '/' + myDate.getDate() + '/' + myDate.getFullYear();
            $("#<%=datepicker.ClientID%>").val(prettyDate.toString());

        }
        // save original function to call in our new one

        var _gotoToday = $.datepicker._gotoToday;
        // make a new _gotoToday function that does what the old one
        // did, but adds some extra feature

        $.datepicker._gotoToday = function(id) {
            _gotoToday.call(this, id);
            var target = $(id),
         inst = this._getInst(target[0]);
            //Added by Ryan Waterer on 1/30/2009 to have it return
            // the value when the person selects the "Today" button
            // onSelect: function() { }, onClose: function() {},
            this._selectDate(id, this._formatDate(inst,
         inst.selectedDay, inst.drawMonth, inst.drawYear));
        }

        if ($("#<%=hdnfrom.ClientID%>").val() == "" && $("#<%=hdnto.ClientID%>").val() == "") {
          
            /*if condition start*/
            var min = $("#<%=hdnStartdate.ClientID%>").val();
            var max = $("#<%=hdnEnddate.ClientID%>").val();
           
            if (min != "") {

                $("#<%=datepicker.ClientID%>").attr('readOnly', 'true');
                $("#<%=datepicker.ClientID%>").datepicker({
                    changeMonth: Month, changeYear: Year, showOn: "both", buttonImage: '<%=Constants__.PhysicalLocalHostPath%>jquery-ui-1.8.14.custom/images/cal.gif',
                    buttonText: 'Select Date',
                    buttonImageOnly: true,
                    showButtonPanel: true,
                    dateFormat: 'd-M-yy',
                    minDate: min,
                    maxDate: max,
                    showAnim: 'fadeIn',
//                    yearRange: '1950:2050',
                    beforeShow: function(input) {
                        setTimeout(function() {
                            var buttonPane = $(input).datepicker("widget")
                    .find(".ui-datepicker-buttonpane");
                            var btn = $('<button style="display:none;" class="ui-datepicker-current ui-state-default ui-priority-secondary ui-corner-all" type="button">Clear</button>');
                            btn.unbind("click")
                   .bind("click", function() {
                       $.datepicker._clearDate(input);
                   });
                            btn.appendTo(buttonPane);
                        }, 1)
                    }
                });

                /*if condition end*/

            } else {
          

                /*if condition start*/
                $("#<%=datepicker.ClientID%>").attr('readOnly', 'true');
                $("#<%=datepicker.ClientID%>").datepicker({
                    // showOn: 'both', 
                    //            buttonImage: 'childtime/images/calendar.gif',
                    //            buttonImageOnly: true,
                    //            buttonText: 'Show Calendar',
                    //            numberOfMonths: 3,
                    //            showButtonPanel: true,
                    //            minDate: -0, maxDate: '+7d',
                    //            beforeShowDay: function(date){ return [date.getDay() == 1,""]},
                    //      


                    //               //.datepicker({
                    changeMonth: Month, changeYear: Year, showOn: "both", buttonImage: '<%=Constants__.PhysicalLocalHostPath%>jquery-ui-1.8.14.custom/images/cal.gif',
                    buttonText: 'Select Date',
                    buttonImageOnly: true,
                    showButtonPanel: true,
                    dateFormat: 'd-M-yy',
                    minDate: new Date(),
                    maxDate: '90d',
                    showAnim: 'fadeIn',
//                    yearRange: '1950:2050',
                    beforeShow: function(input) {
                        setTimeout(function() {
                            var buttonPane = $(input).datepicker("widget")
             .find(".ui-datepicker-buttonpane"); var btn = $('<button style="display:none;" class="ui-datepicker-current ui-state-default ui-priority-secondary ui-corner-all" type="button">Clear</button>');
                            btn
             .unbind("click")
             .bind("click", function() {
                 $.datepicker._clearDate(input);
             });
                            btn.appendTo(buttonPane);
                        }, 1)
                    }
                });

                /*if condition end*/
            }


        }

        else {
            
            var getFrom = $("#<%=hdnfrom.ClientID%>").val();

            $("#<%=datepicker.ClientID%>").attr('readOnly', 'true');
            var from = "";
            var fromotherid = "";
            from = getFrom.split(';')[0];
            fromotherid = getFrom.split(';')[1].toString();
            var getTo = $("#<%=hdnto.ClientID%>").val();
            var selectedDate = $("#<%=datepicker.ClientID%>").val();
            var tmprelateddatepicker = $("#" + fromotherid.toString());
            var tmprelateddate = tmprelateddatepicker.val();
            $("#<%=datepicker.ClientID%>").datepicker({
                changeMonth: Month, changeYear: Year, showOn: "both", buttonImage: '<%=Constants__.PhysicalLocalHostPath%>jquery-ui-1.8.14.custom/images/cal.gif',
                buttonText: 'Select Date', buttonImageOnly: true, showButtonPanel: true,
                showAnim: 'fadeIn',
                yearRange: '1950:2050',
                dateFormat: 'd-M-yy',
                minDate: from == "from" ? '' : selectedDate,
                maxDate: from == "from" ? '' : '90d',
                onSelect: function(selectedDate, inst) {
                    var from = $("#<%=datepicker.ClientID%>");
                    var to = tmprelateddatepicker;

                    var option = getFrom.split(';')[0]


                    if (option == "from") {
                        from.datepicker("option", "minDate", '');
                        from.datepicker("option", "maxDate", to.val());

                        to.datepicker("option", "minDate", from.val());
                        to.datepicker("option", "maxDate", '');
                    }
                    else {

                        to.datepicker("option", "minDate", '');
                        to.datepicker("option", "maxDate", from.val());

                    }





                },
                beforeShow: function(input) {


                    setTimeout(function() {
                        var buttonPane = $(input).datepicker("widget")
             .find(".ui-datepicker-buttonpane"); var btn = $('<button style="display:none;" class="ui-datepicker-current ui-state-default ui-priority-secondary ui-corner-all" type="button">Clear</button>');
                        btn
             .unbind("click")
             .bind("click", function() {
                 $.datepicker._clearDate(input);
             });
                        btn.appendTo(buttonPane);
                    }, 1)
                }
            });
            if (getTo != "") {
                $("#<%=datepicker.ClientID%>").datepicker('setDate', getTo);


            }

        }











        $("#<%=datepicker.ClientID%>").keypress(function(event) {

            var keycode = (event.keyCode ? event.keyCode : event.which);
            if ($("#<%=datepicker.ClientID%>").val() != "") {
                if (keycode == 9 && keycode == 13) {
                    try {

                        //.datepicker({ dateFormat: 'd M yy' })
                        var dateText = $("#<%=datepicker.ClientID%>").val();
                        // alert(dateText);
                        $.datepicker.parseDate('d-M-yy', dateText);
                    } catch (e) {
                        var val = e;
                        if (val.toString().toLowerCase() == 'Missing number at position 0'.toString().toLowerCase()) {
                            alert('Please Select Date.');
                        }
                        else {
                            alert('Invalid Date');
                            $("#<%=datepicker.ClientID%>").val('');
                        }
                    };
                }
            }
        });
        if ($("#<%=datepicker.ClientID%>").val() != "") {

            $("#<%=datepicker.ClientID%>").blur(function() {

                if ($("#<%=datepicker.ClientID%>").val() != "") {
                    try {
                        var dateText1 = $("#<%=datepicker.ClientID%>").val();
                        //alert(dateText1);
                        $.datepicker.parseDate('d-M-yy', dateText1);
                    } catch (e) {

                        var val = e;
                        if (val.toString().toLowerCase() == 'Missing number at position 0'.toString().toLowerCase()) {
                            alert('Please Select Date.');
                        }
                        else {
                            alert('Invalid Date');
                            $("#<%=datepicker.ClientID%>").val('');
                        }
                    };
                }
            });
        }


    });

    
</script>

<!--.mask("99/99/9999");-->
<div class="datepicker">
    <p>
        <asp:TextBox ID="datepicker" runat="server" EnableViewState="true" CssClass="hajanDatePicker" 
            Width="79px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvValue" ControlToValidate="datepicker" CssClass="ErrorMsg"
            runat="server" Display="Dynamic" EnableClientScript="true"></asp:RequiredFieldValidator>
        <asp:HiddenField ID="hdnTodate" runat="server" Value="" />
        <asp:HiddenField ID="hdnMonth" runat="server" Value="" />
        <asp:HiddenField ID="hdnyear" runat="server" Value="" />
        <asp:HiddenField ID="hdnMinDate" runat="server" Value="" />
        <asp:HiddenField ID="hdnMaxDate" runat="server" Value="" />
        <asp:HiddenField ID="hdnOtherControlClientID" runat="server" Value="" />
        <asp:HiddenField ID="hdnfrom" runat="server" Value="" />
        <asp:HiddenField ID="hdnto" runat="server" Value="" />
        <asp:HiddenField ID="hdnpastDate" runat="server" Value="" />
        <asp:HiddenField ID="hdnStartdate" runat="server" Value="" />
        <asp:HiddenField ID="hdnEnddate" runat="server" Value="" />
        <asp:HiddenField ID="hdnOnclick" runat="server" Value="" />
    </p>
</div>
