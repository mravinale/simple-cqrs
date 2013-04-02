/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="mediator.js" />
/// <reference path="jquery-1.6.4.js" />
/// <reference path="common.js" />

var DataTable = (function () {
    var public = {};

    var selectedItem;
    var dataTable;
    var element;
    var ajaxSource;
    var mediator;

    public.reload = function () {
        dataTable.fnDraw();
    };

    public.init = function (params) {
        element = params.element;
        ajaxSource = params.ajaxSource;
        mediator = params.mediator;

        initDataTable();
        initDatatableSelection();
    };

    var initDataTable = function () {
        dataTable = $(element).dataTable({
            "bProcessing": true,
            "bPaginate": true,
            "bJQueryUI": true,
            "sPaginationType": "full_numbers",
            "bServerSide": true,
            "sAjaxSource": ajaxSource
        });
    };

    var initDatatableSelection = function() {

        $(element + ' tbody tr').live('click', function() {
            var aData = dataTable.fnGetData(this); // get datarow
            if (null != aData) {

                $(dataTable.fnSettings().aoData).each(function() {
                    $(this.nTr).removeClass('selected');
                    $(this.nTr).find("td.[class*=sorting]").addClass('sorting_1');
                    $(this.nTr).find("td.[class*=sorting]").removeClass('sorting');
                });

                $(this).addClass("selected");
                $(this).find("td.[class*=sorting]").addClass('sorting');
                $(this).find("td.[class*=sorting]").removeClass('sorting_1');

                if (aData[0] != null) {
                    selectedItem = aData[0];
                    mediator.Publish("selectedItem", selectedItem);

                }
            }
        });
    };

    return public;

} ());