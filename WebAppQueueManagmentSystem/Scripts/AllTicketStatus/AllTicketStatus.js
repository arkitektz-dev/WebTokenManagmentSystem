

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
        LoadingOn();
        CallTable(date, cmbTokenStatus, cmbCustomerType);
        LoadingOff();
 

    });

    const LoadingOn = () => {
         document.getElementById("LoaderEnabled").style.display = "block";
    }
    const LoadingOff = () => {
         document.getElementById("LoaderEnabled").style.display = "none";
    }


    function CallTable(TicketDate, TicketStatus, CustomerType) {
             $.ajax({
                 type: "GET",
                 url: ProjectBaseUrl + `/Home/GetTicketList?TicketDate=${TicketDate}&TicketStatus=${TicketStatus}&CustomerType=${CustomerType}`,  
                 cors: true,
                 contentType: 'application/json',
                 secure: true,
                 headers: {
                     'Access-Control-Allow-Origin': '*',
                 },
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
        CallTable();
    },10000)
 