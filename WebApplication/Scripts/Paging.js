var testsModule = (function () {
    var _fillingDivs = function (data) {
        $(".one_test").show();

        for (var j = 0; j < data.PagingInfo.ItemsPerPage; j++) {
            if (data.Tests[j] == undefined) {
                $(".one_test-" + j).hide();
                continue;
            }
            $(".one_test__name-" + j).text(data.Tests[j].Name);
            $(".one_test__name-" + j).attr('href', '/Test/Preview/' + data.Tests[j].Id);
            $(".one_test__description-" + j).text(data.Tests[j].Description);
            $(".one_test__image-" + j).attr('src', data.Tests[j].Image);
            $(".one_test__img__ligthbox-" + j).attr('href', data.Tests[j].Image);
            $(".edit-" + j).attr('href', '/Test/Edit/' + data.Tests[j].Id);
            $(".details-" + j).attr('href', '/Test/Details/' + data.Tests[j].Id);
            $(".delete-" + j).attr('href', '/Test/Delete/' + data.Tests[j].Id);
        }
    };

    var _reloadPager = function (data) {
        //$(".pager").
        // ???
    }

    var _returnTests = function (data) {
        _fillingDivs(data);
        _reloadPager(data);
    }

    return {
        fillingDivs: _fillingDivs,
        returnTests: _returnTests
    }
}());

/*var testsModule = (function () {
    var _addingPages = function (totalPages) {
        $.ajax({
            url: '/Test/GetPages?totalPages=' + totalPages,
            type: 'GET',
            cache: false,
            success: function (data) {
                $(".pager_container").html(data);
            }
        });
    };

    var _fillingDivs = function (data) {
        $(".one_test").show();

        for (var j = 0; j < data.PagingInfo.ItemsPerPage; j++) {
            if (data.Tests[j] == undefined) {
                $(".one_test-" + j).hide();
                continue;
            }
            $(".one_test__name-" + j).text(data.Tests[j].Name);
            $(".one_test__name-" + j).attr('href', '/Test/Preview/' + data.Tests[j].Id);
            $(".one_test__description-" + j).text(data.Tests[j].Description);
            $(".one_test__image-" + j).attr('src', data.Tests[j].Image);
            $(".edit-" + j).attr('href', '/Test/Edit/' + data.Tests[j].Id);
            $(".details-" + j).attr('href', '/Test/Details/' + data.Tests[j].Id);
            $(".delete-" + j).attr('href', '/Test/Delete/' + data.Tests[j].Id);
        }
    };

    var _returnTests = function (data) {
        _fillingDivs(data);
        _addingPages(data.PagingInfo.TotalPages);
    }

    var _pagingReturnTests = function (data) {
        //console.log(data);
        _fillingDivs(data);
       // _addingPages(data.PagingInfo.TotalPages);
    }

    return {
        addingPages: _addingPages,
        fillingDivs: _fillingDivs,
        returnTests: _returnTests,
        pagingReturnTests: _pagingReturnTests
    }
}());

$(function () {
    $.ajax({
        url: '/Test/Search?page=1',
        type: 'GET',
        cache: false,
        success: function (data) {
            testsModule.fillingDivs(data);
        }
    });
}());*/

//$(document).on("click", ".pager__number", function () {
//    var inputVal = $(".search_by_keyword__inp").val();
//    $.ajax({
//        url: '/Test/Search?keyWord=' + inputVal + '&page=' + $(this).attr("id"),
//        type: 'GET',
//        cache: false,
//        success: function (data) {
//            testsModule.fillingDivs(data);
//        }
//    });
//});
