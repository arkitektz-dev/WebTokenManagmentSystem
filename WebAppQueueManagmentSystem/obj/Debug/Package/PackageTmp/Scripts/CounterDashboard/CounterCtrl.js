let con = $.connection.ticketHub;
let currentToken = "";
let doContinue = false;
let isNextButtonRequest = false;
let isSoundPlaying = false;

//document.getElementById("listSidebar").getElementsByTagName("li").length
$.connection.hub.start().done(function () {
    //console.log("Connection Established");
    IdleWarning("On");
});

con.client.getNewTicket = function (TokenDetail) {
    console.log(TokenDetail)
    AddNewTicket(TokenDetail);
}

con.client.getRemovedTicketNumber = function (TicketNumber) {
    console.log("YOur ticket number");
    console.log(TicketNumber);
    RemoveTicketFromList(TicketNumber);
}

con.client.getSoundPlayed = function (TokenDetail) {
    //console.log("Sound Played")
    isSoundPlaying = true;


    setTimeout(function () {
        isSoundPlaying = false;
    },6000)
}


setInterval(function () {
  
    OpenPendingTicket();
    EnableNextTicketButton();
    if (document.getElementById("listSidebar").getElementsByTagName("li") != undefined) {

        var countRow = document.getElementById("listSidebar").getElementsByTagName("li").length;
        document.getElementById("TicketQueue").innerText = countRow;
    }
}, 10000);

let CallAgain = () => {

    if (isSoundPlaying == true) {
        alert("Already running");
        return;
    }

    let txtTicketNumber = document.getElementById("txtTicketNo").innerText.replace(/Ticket Number /i, '');
    let txtCounterNumber = document.getElementById("CounterNumber").value;


    $.ajax({
        type: "GET",
        url: ProjectBaseUrl+ "/Home/CallAgain",
        data: { TokenNumber: txtTicketNumber, Counter: txtCounterNumber },
        cors: true,
        contentType: 'application/json',
        secure: true,
        headers: {
            'Access-Control-Allow-Origin': '*',
        },
        success: function (data) {

            //console.log("Called");

        },
        error: function () {
           
        }
    });
}

let EnableNextTicketButton = () => {
    var list = document.getElementById("listSidebar").getElementsByTagName("li")[0];

    if (list != "") {
        document.getElementById("btnPickNextTicket").style.display = "block";
    } else {
        document.getElementById("btnPickNextTicket").style.display = "none";

    }

}

let btnSkipThis = () => {

    let txtTicketNumber = document.getElementById("txtTicketNo").innerText.replace(/Ticket Number /i, '');

    //get User Id 
    const params = new URLSearchParams(window.location.search)
    var userID = params.get('UserId')
    //console.log(userID);
 
    $.ajax({
        type: "GET",
        url: ProjectBaseUrl + "/Home/ChangeTokenStatus",
        data: { TokenNumber: txtTicketNumber, Status: 6 },
        cors: true,
        contentType: 'application/json',
        secure: true,
        headers: {
            'Access-Control-Allow-Origin': '*',
        },
        success: function (data) {
            //console.log(data.message);
            var result = data.tokenDetail;

            if (data.tokenDetail != null && data.tokenDetail != undefined) {

                stoptime = true;
                IdleWarning("On");
                currentToken = "";

            } else {
                //console.log("Error")
            }

        },
        error: function () {
            console.log("An Error occured")
        }
    });
}


let OpenPendingTicket = () => {

    const params = new URLSearchParams(window.location.search)
    var userID = params.get('UserId')
    //console.log(userID);

    $.ajax({
        type: "GET",
        url: ProjectBaseUrl + "/Home/GetPendingCounter",
        data: { UserId: userID }, 
        contentType: 'application/json', 
        cors: true,
        secure: true,
        headers: {
            'Access-Control-Allow-Origin': '*',
        },
        success: function (data) {
            console.log("This is from this" + " " + data.tokenDetail.CustomTokenNumber);

            if (data.tokenDetail.CustomTokenNumber != undefined) {

                var TokenNumber = data.tokenDetail.CustomTokenNumber;
                debugger;
                if (TokenNumber != null || TokenNumber != "") {

                    IdleWarning("Off");
                    stoptime = false;
                    if (isRun == false) {
                        timerCycle();
                    }
                    document.getElementById('txtTicketNo').innerText = "Ticket Number " + data.tokenDetail.CustomTokenNumber;

                    currentToken = TokenNumber;

                    //console.log(currentToken);

                } 

            }
            
        },
        error: function (err) { 
            console.log("An Error occured")
        }
    });
}

let RemoveTicketFromList = (TicketNumber) => {
    var ticketNumber = document.getElementById(`TicketNumber${TicketNumber}`);
   
        ticketNumber.remove();
 
}

let IdleWarning = (Switch) => {

    if (Switch == "On") {
        document.getElementById("idleWarning").style.display = "block";
        document.getElementById("TicketWorkSpaceArea").style.display = "none";
    } else {
        document.getElementById("idleWarning").style.display = "none";
        document.getElementById("TicketWorkSpaceArea").style.display = "block";
    }


}

function GetTokenStatusNew() {
    debugger;
    var a = new Promise(function (resolve, reject) {
        if ($("#TicketWorkSpaceArea").is(":visible") == true) {
            let txtTicketNumber = document.getElementById("txtTicketNo").innerText.replace(/Ticket Number /i, '');
            //console.log(txtTicketNumber + "this is");
            
            var settings = {
                "url": ProjectBaseUrl + "/Home/GetTicketStatus?TokenNumber=" + txtTicketNumber,
                "method": "GET",
                "cors": true,
                "contentType": "application/json",
                "secure": true,
                "timeout": 0,
                "headers": {
                    "Access-Control-Allow-Origin": "*"
                },
            };

            $.ajax(settings).done(function (response) {
                let TokenNumberStatus = response.message.TokenStatus;
                //console.log("Token number is " + TokenNumberStatus);
                resolve("TokenNumberStatus")
            });
        } else {
            resolve("1");
        }



    });

    return a
}

let SelectTicket = async (TokenNumber) => {

    debugger;

    //console.log(TokenNumber);
    //console.log(currentToken);
    //console.log("before");
    var result = await GetTokenStatusNew();

    //console.log("Toke is servering or not ", result);

    if (isNextButtonRequest == true) {
        //console.log("open ticket");
        OpenTicket(TokenNumber);
        isNextButtonRequest = false;
        document.getElementById("txtComment").value = "";
    } else {

        if (result == "2") {
            //console.log("open ticket");
            OpenTicket(TokenNumber);
            document.getElementById("txtComment").value = "";
        } else if (result == "4") {
            alert("Please close this ticket");
            document.getElementById("txtComment").value = "";
            return;
        } else if (result == "1") {
            let isScreenOpen = $("#TicketWorkSpaceArea").is(":visible");
            if (isScreenOpen == false) {
                //console.log("open ticket");
                OpenTicket(TokenNumber);
                document.getElementById("txtComment").value = "";
            }
        }

    }


}

function OpenTicket(TokenNumber) {
    debugger;
    const params = new URLSearchParams(window.location.search)
    var userID = params.get('UserId')
    //console.log(userID);

    var settings = {
        "url": ProjectBaseUrl + '/Home/AssignCounterToTicket?TokenNumber=' + TokenNumber + '&UserId=' + userID + '&StatusId=4',
        "method": "GET", 
        "cors": true,
        "contentType": "application/json",
        "secure": true,
        "timeout": 0,
        "headers": {
            "Access-Control-Allow-Origin": "*"
        },
    };

    $.ajax(settings).done(function (data) {
        //console.log(data.message);
        var result = data.message;

        if (result == "Success") {
            //console.log(result);
            //console.log(TokenNumber);
            IdleWarning("Off");
            stoptime = false;
            if (isRun == false) {
                timerCycle();
            }
            document.getElementById('txtTicketNo').innerText = "Ticket Number " + TokenNumber;

            currentToken = TokenNumber;

            //console.log(currentToken);

        } else {
            //console.log("Error")

        }

    });


}

let btnCloseThisTicket = () => {

    //var TokenStatusId = document.getElementById("StatusId").value;
    var ServiceStatusId = document.getElementById("ServiceId").value;
    var CounterComment = document.getElementById("txtComment").value;

    //get User Id 
    const params = new URLSearchParams(window.location.search)
    var userID = params.get('UserId')
    //console.log(userID);

    ////console.log(currentToken,TokenStatusId, ServiceStatusId, CounterComment, userID);

    $.ajax({
        type: "GET",
        url: ProjectBaseUrl + "/Home/SubmittedTicket",
        data: { TokenNumber: currentToken, Comment: CounterComment, ServiceOptionId: ServiceStatusId, StatusId: 2 },
        cors: true,
        contentType: 'application/json',
        secure: true,
        headers: {
            'Access-Control-Allow-Origin': '*',
        },
        success: function (data) {
            //console.log(data.message);
            var result = data.message;
            console.log(result);
            if (result.includes("Success")) {

                stoptime = true;
                IdleWarning("On");
                currentToken = "";

            } else {
                //console.log("Error")
            }

        },
        error: function () {
            console.log("An Error occured")
        }
    });



}

let btnGetNextTicket = () => {

    var list = document.getElementById("listSidebar").getElementsByTagName("li")[0].id;
    //console.log(list);
    let TicketNumberCount = "";


    if (list != undefined && list != "" && list != null) {
        isNextButtonRequest = true;
        ClearTime();
        btnCloseThisTicket();
        TicketNumberCount = list.replace(/TicketNumber/i, '');
        currentToken = TicketNumberCount.toString();
        //console.log(currentToken);
        SelectTicket(`${TicketNumberCount}`);


    } else {
        //console.log("Rfdfdfdfd");
        btnCloseThisTicket();
    }





}

let AddNewTicket = (TokenDetail) => {

    //console.log(TokenDetail);

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

    $('#listSidebar').append(card)

    //console.log(card);

    //console.log($('#listSidebar').append(card));



}