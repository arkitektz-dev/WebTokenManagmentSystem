
$("#onSearch").on('click', function () {
    var getDropdownValue = document.getElementById("txtRoleList").value;
    listUserTable(getDropdownValue);


});


$("#btnSearch").click(function () {
    $("#panelSearch").toggle("slow");
});






const listUserTable = (RoleID) => {

   

    $.ajax({
        type: "GET",
        url: "/User/ListUserTable",
        data: { Role: RoleID},
        success: function (data) {
            document.getElementById("PanelUserList").innerHTML = "";
            document.getElementById("PanelUserList").innerHTML = data;

            //var table = $('#example2').DataTable();
        },
        error: function () {
            alert("Error occured!!")
        }
    });

}

 
listUserTable('');