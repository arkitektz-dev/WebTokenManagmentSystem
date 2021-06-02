let con = $.connection.ticketHub;

con.client.getNewTicket = function (tokenNumber) {
    console.log("New ticket number is " + tokenNumber)

    //Add New ticket in 


}


$.connection.hub.start().done(function () {
    console.log("New ticket");
});
  
