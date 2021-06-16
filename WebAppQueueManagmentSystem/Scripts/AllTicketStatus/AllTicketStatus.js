

    $("#btnSearch").click(function () {
        $("#panelSearch").toggle("slow");
    });


    CallTable(new Date().toLocaleDateString(), 1, 1);

    $("#onSearch").click(function () {
        //var date = $('#datetimepicker4').data("DateTimePicker").viewDate ()
        var date = `${$("#datetimepicker4").find("input").val()}`;
        var cmbTokenStatus = document.getElementById("cmbTokenStatus").value;  
        var cmbCustomerType = document.getElementById("cmbCustomerType").value;   
        console.log(date, cmbTokenStatus, cmbCustomerType);

        CallTable(date, cmbTokenStatus, cmbCustomerType);

 

    });
 //DateTime TicketDate, int TicketStatus, int CustomerType
    function CallTable(TicketDate, TicketStatus, CustomerType) {
             $.ajax({
                type: "GET",
                url: `/Home/GetTicketList?TicketDate=${TicketDate}&TicketStatus=${TicketStatus}&CustomerType=${CustomerType}`,  
                success: function (data) {
                    console.log(data);
                    document.getElementById("panel1").innerHTML = data;

                    if ($("#example2").length) {

                        $(document).ready(function () {
                            $(document).ready(function () {
                                var groupColumn = 2;
                                var table = $('#example2').DataTable();

                            });
                        });
                    }
                },
                 error: function (err) {
                     console.log(err)
                }
             });
    }
    setTimeout(function () {
        //CallTable();
    },5000)
 