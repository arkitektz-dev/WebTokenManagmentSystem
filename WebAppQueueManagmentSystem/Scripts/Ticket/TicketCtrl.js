$(document).ready(function () {
    $(function () {

        //Customer click
        $("#btnCustomer").on('click', function () {
            makeRequest("1");
        });

        //Non Customer click
        $("#btnNonCustomer").on('click', function () {
            makeRequest("2");
        });

        let makeRequest = (CustomerType) => {
            $.ajax({
                type: "POST",
                url: "/Home/GetNewTicket",
                data: { CustomerType: CustomerType},
                success: function (data) {
                     
                },
                error: function () {
                    alert("Error occured!!")
                }
            });  
        }


    });
});