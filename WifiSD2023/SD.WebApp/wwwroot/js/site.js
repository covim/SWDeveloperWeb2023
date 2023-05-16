// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowConfirmDialog(modalId, ctl, showDelete, rowsToShow) {

    //Dynamische befüllung des modalen dialogs

    var $row = $(ctl).parent().parent();
    var $colums = $row.find('td');

    //Bestehende Inhalte im Modal-Body löschen
    $modal = $('#' + modalId);
    var $modalBody = $modal.find('#modalBody');
    $modalBody.empty();

    //Überschriften auslesen
    //var $thRows = $('#MoviesTable').find('th');
    var $thRows = $row.parent().parent().find('th');

    for (var i = 0; i < rowsToShow.length; i++) {
        $modalBody.append($('<dt class="col-md-3">').html($thRows[rowsToShow[i]].innerText));
        $modalBody.append($('<dd class="col-md-9">').html($colums[rowsToShow[i]].innerText));
    }

    //Überschrit abhängig von Löschen oder Details
    var $headerTitle = $modal.find('#ConfirmModalTitle');
    var $deleteCotrols = $modal.find('#DeleteControls')

    if (showDelete) {
        $headerTitle.text("Sind Sie sicher, dass sie diesen Eintrag löschen möchten?");  //könnte man auch über globalize lösen (müsste dann über einen parameter übergeben werden)
        $deleteCotrols.show();
        var itemId = $(ctl).attr('data-id');
        var $modalId = $modal.find('#Id');
        $modalId.val(itemId);
    }
    else {
        $headerTitle.text("Details");
        $deleteCotrols.hide();
    }

    var options = {
        "backdrop": "static",
        "keyboard": true
    };

    var modal = new bootstrap.Modal(document.getElementById(modalId), options);
    modal.show();
}