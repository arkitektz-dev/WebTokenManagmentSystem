
const PopulateAdminTicketCard = () => {
    $.ajax({
        type: "GET",
        url: ProjectBaseUrl + "/Home/GetAllTicketCount",
        success: function (data) {

            console.log(data);
            document.getElementById("txtActiveCounter").innerText = data.message.ActiveCounter;
            document.getElementById("txtTicketIssued").innerText = data.message.TicketIssued;
            document.getElementById("txtIssuedResolved").innerText = data.message.IssuedResolved;
            document.getElementById("txtWaiting").innerText = data.message.Waiting;

        },
        error: function () {

        }
    });
}

PopulateAdminTicketCard();

