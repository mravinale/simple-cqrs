/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="jquery-1.6.4.js" />
/// <reference path="common.js" />
/// <reference path="dataTables.extension.js" />
/// <reference path="mediator.js" />
/// <reference path="jquery-ui-1.8.19.js" />

$(function () {
    var mediator = new Mediator();

    DataTable.init({
        element: "#example",
        ajaxSource: "Albums/Browse",
        mediator: mediator
    });
    
    Albums.init({mediator : mediator});
});

var Albums = (function ($) {
    var public = {};
    var selectedItem;
    var mediator;

    public.init = function (param) {
        mediator = param.mediator;

        initSignalR();
        initEvents();
        initLayoutResources();
    };

    var initLayoutResources = function () {
        $(".insert-link").button();
        $(".link").button();
        $(".link").hide();

        $("#dialog").dialog({ autoOpen: false, modal: true });

        $(".edit-link").createForm({ baseUrl: "Albums/Edit/" });
        $(".details-link").createForm({ baseUrl: "Albums/Details/" });
        $(".insert-link").createForm({ baseUrl: "Albums/Insert/" });
        
        $(".delete-link").deleteAction({ baseUrl: "Albums/Delete/" });
    };

    var initEvents = function () {
        mediator.Subscribe("selectedItem", function (data) {
            selectedItem = data;
            $(".link").show();
        });
    };

    var initSignalR = function () {
        //Create SignalR object to get communicate with server
        var hub = $.connection.AlbumsHub;

        hub.Redraw = function () {
            DataTable.reload();
        };
        // Start the connection
        $.connection.hub.start();
    };

    $.fn.createForm = function (param) {
        return this.each(function () {
            $(this).click(function () {
                $.ajax({
                    url: param.baseUrl + selectedItem,
                    success: function (data) {
                        $("#dialog").html(data);
                        $("#dialog").dialog('open');
                    },
                    error: function () { $.showError("Sorry,error"); }
                });
            });
        });
    };

    $.fn.deleteAction = function (param) {
        return this.each(function () {
            $(this).click(function (event) {
                event.preventDefault();
                if (selectedItem == null) return false;

                $.showConfirm("Are you sure you want to delete this Album?", function () {
                    $.post(param.baseUrl + selectedItem)
                    .success(function () { $.showInfo("Deleted Succesfully!"); })
                    .error(function () { $.showError("Sorry, there was an error saving"); });
                });
                return false;
            });
        });
    };
    
    return public;
} (jQuery));


