let con1 = $.connection.newAssignCounter;



con1.client.getNewTicketNumber = function (TokenNumber) {
   
    RefreshList();

}


$.connection.hub.start().done(function () {
    console.log("Connection Established");

});


