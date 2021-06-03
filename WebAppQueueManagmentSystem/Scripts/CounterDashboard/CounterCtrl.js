let con = $.connection.ticketHub;
let currentToken = "";

con.client.getNewTicket = function (TokenDetail) {
    console.log(TokenDetail)

    AddNewTicket(TokenDetail);
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

                IdleWarning("Off");
                timerCycle();
                document.getElementById('txtTicketNo').innerText = "Ticket Number " +  TokenNumber;
                document.getElementById(`TicketNumber${TokenNumber}`).remove();
                currentToken = TokenNumber;

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

    console.log(TokenStatusId, ServiceStatusId, CounterComment);
}


let btnGetNextTicket = () => {



}


let AddNewTicket = (TokenDetail) => {

    console.log(TokenDetail);

    var card = `
      <li class="TicketNumber${TokenDetail.token}">
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