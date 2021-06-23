let con = $.connection.ticketHub;
let currentToken = "";


con.client.getNewTicket = function (TokenDetail) {
    console.log(TokenDetail)
    AddNewTicket(TokenDetail)
}

con.client.getNewTicketNumber = function (TicketNumber) {
    console.log(TicketNumber);
    RefreshList();
}


con.client.getRemovedTicketNumber = function (TicketNumber) {
    console.log("YOur ticket number");
    console.log(TicketNumber);
    RemoveTicketFromList(TicketNumber);
}

setInterval(function () {
    console.log("running");
    RefreshList();
},2000)

let RemoveTicketFromList = (TicketNumber) => {
    var ticketNumber = document.getElementById(`TokenNumber${TicketNumber}`);

    if (ticketNumber != null) {
        ticketNumber.remove();
    }
}






$.connection.hub.start().done(function () {
    console.log("Connection Established");

});




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
        data: { CustomerType: CustomerType },
        success: function (data) {

            if (data.TokenDetail.PrinterFound == false) {


                document.getElementById("txtDisplayTicket").innerText = `Your ticket number is ${data.TokenDetail.token}`
                document.getElementById("txtDisplayTicket").style.display = "block";

                setTimeout(function () {
                document.getElementById("txtDisplayTicket").style.display = "none";
                    
                },5000)


            }


            console.log(data);
        },
        error: function () {
            alert("Error occured!!")
        }
    });
}


let RefreshList = () => {
  

    $.ajax({
        type: "GET",
        url: "/Home/ListCountTicket",
        success: function (content) {

            if (document.getElementById("listCurrentTicketNumber") != null) {
                document.getElementById("listCurrentTicketNumber").innerHTML = "";
                $("#listCurrentTicketNumber").append(content);
            }

        },
        error: function () {
            alert("Error occured!!")
        }
    });

}

 

let AddNewTicket = (TokenDetail) => {

    console.log(TokenDetail);

     
        var card = `
        <li style="margin-top: -29px;" id="TokenNumber${TokenDetail.token}">
          <div class="card mb-0">
          <div class="card-body">
          <h1 class="card-title bg-primary p-4"> Ticket ${TokenDetail.token}</h1>
          </div>
          </div>
     </li>  
    `;

        console.log(card);
        document.getElementById("listCurrentTicketNumber").innerHTML = "";
        console.log($('#listSidebar').append(card));
  




}