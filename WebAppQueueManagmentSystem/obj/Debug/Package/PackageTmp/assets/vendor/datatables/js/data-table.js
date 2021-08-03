jQuery(document).ready(function($) {
    'use strict';

    if ($("table.first").length) {

        $(document).ready(function() {
            $('table.first').DataTable();
        });
    }

    /* Calender jQuery **/

    if ($("table.second").length) {

        $(document).ready(function() {
            var table = $('table.second').DataTable({
                lengthChange: false,
                buttons: ['copy', 'excel', 'pdf', 'print', 'colvis']
            });

            table.buttons().container()
                .appendTo('#example_wrapper .col-md-6:eq(0)');
        });
    }


    if ($("#example2").length) {

        $(document).ready(function() {
            $(document).ready(function() {
                var groupColumn = 2;
                var table = $('#example2').DataTable();
                 
            });
        });
    }

    if ($("#example3").length) {

        $('#example3').DataTable({
            select: {
                style: 'multi'
            }
        });

    }
    if ($("#example4").length) {

        $(document).ready(function() {
            var table = $('#example4').DataTable({
                fixedHeader: true
            });
        });
    }

});