let con = $.connection.ticketHub;
let currentToken = "";


con.client.getNewTicket = function (TokenDetail) {
    console.log(TokenDetail)

    AddNewTicket(TokenDetail);
}

con.client.getRemovedTicketNumber = function (TicketNumber) {
    console.log("YOur ticket number");
    console.log(TicketNumber);
    RemoveTicketFromList(TicketNumber);


}


let RemoveTicketFromList = (TicketNumber) => {
    var ticketNumber = document.getElementById(`TicketNumber${TicketNumber}`);
    if (ticketNumber != null) {
        ticketNumber.remove();
    }
}


$.connection.hub.start().done(function () {
    console.log("Connection Established");
    IdleWarning("On");
});

let IdleWarning = (Switch) => {

    if (Switch == "On") {
        document.getElementById("idleWarning").style.display = "block";
        document.getElementById("TicketWorkSpaceArea").style.display = "none";
    } else {
        document.getElementById("idleWarning").style.display = "none";
        document.getElementById("TicketWorkSpaceArea").style.display = "block";
    }


}

 
let SelectTicket = (TokenNumber) => {

    if (currentToken != "") {
        alert("Please fill the open ticket")
        return;
    }

    //get User Id 
    const params = new URLSearchParams(window.location.search)
    var userID = params.get('UserId')
    console.log(userID);

    $.ajax({
        type: "GET",
        url: "/Home/AssignCounterToTicket",
        data: { TokenNumber: TokenNumber, UserId: userID, StatusId: 4 },
        success: function (data) {
            console.log(data.message);
            var result = data.message;

            if (result == "Success") {

                console.log(TokenNumber);
                IdleWarning("Off");
                stoptime = false;
                timerCycle();
                document.getElementById('txtTicketNo').innerText = "Ticket Number " +  TokenNumber;
                 
                currentToken = TokenNumber;
                console.log(currentToken);

            } else {
                console.log("Error")

            }

        },
        error: function () {
            alert("Error occured!!")
        }
    });
}


let btnCloseThisTicket = () => {

    var TokenStatusId = document.getElementById("StatusId").value;
    var ServiceStatusId = document.getElementById("ServiceId").value;
    var CounterComment = document.getElementById("txtComment").value;

    //get User Id 
    const params = new URLSearchParams(window.location.search)
    var userID = params.get('UserId')
    console.log(userID);

    console.log(currentToken,TokenStatusId, ServiceStatusId, CounterComment, userID);

    $.ajax({
        type: "GET",
        url: "/Home/SubmittedTicket",
        data: { TokenNumber: currentToken, Comment: CounterComment, ServiceOptionId: ServiceStatusId, StatusId: TokenStatusId },
        success: function (data) {
            console.log(data.message);
            var result = data.message;

            if (result == "Success") {

                stoptime = true;
                IdleWarning("On"); 
                currentToken = "";

            } else {
                console.log("Error")

            }

        },
        error: function () {
            alert("Error occured!!")
        }
    });



}


let btnGetNextTicket = () => {



}


let AddNewTicket = (TokenDetail) => {

    console.log(TokenDetail);

    var card = `
      <li id="TicketNumber${TokenDetail.token}">
         <div class="card">
              <div class="card-body">
                    <h3 class="card-title">Ticket ${TokenDetail.token}</h3>
                    <p class="card-text">Ticket Issue Time: ${TokenDetail.time}</p>
                    <button class="btn btn-primary text-white btn-block" onclick="SelectTicket('${TokenDetail.token}')">Pick this</button>
               </div>
          </div>
      </li>      
    `; 

    console.log(card);

    console.log($('#listSidebar').append(card));   

   

}