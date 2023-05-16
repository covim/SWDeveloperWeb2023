$(document).ready(function () {

    //javascript selector
    //var moviesTable = document.querySelector('#MoviesTable');
    //var del2 = $(moviesTable).find('a[data-name=Delete]');

    //JQuery (sehr comfortabel) Methoden (diese Bekommen aus konvention immer ein $ vor den namen)
    var $movieTable = $('#MoviesTable');
    var $delteHyperLinks = $movieTable.find('a[data-name=Delete]');
    var $detailHyperLinks = $movieTable.find('a[data-name=Details]');

    $delteHyperLinks.on('click', function () {

        var rowsToShow = [0, 1, 2, 3, 4, 5];
        ShowConfirmDialog("ConfirmModal", this, true, rowsToShow);
        return false;
    })


    $detailHyperLinks.on('click', function () {

        var rowsToShow = [0, 1, 2, 3, 4, 5];
        ShowConfirmDialog("ConfirmModal", this, false, rowsToShow);
        return false;
    })
})