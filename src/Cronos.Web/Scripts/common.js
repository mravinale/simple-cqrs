/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="jquery-1.6.4.js" />
/// <reference path="jquery-ui-1.8.19.js" />

(function ($) {

    $.showInfo = function (text, callback) {

        $('<div></div>').html(text)
            .dialog({
                modal: true,
                title: 'Info',
                buttons: {
                    'Ok': function () {
                        $(this).dialog('close');
                        $(this).remove();
                        if (callback != null) {
                            callback();
                        }
                    }
                }
            });

    };
    $.showError = function (text, callback) {

        $('<div></div>').html(text)
            .dialog({
                modal: true,
                title: 'Error',
                buttons: {
                    'Ok': function () {
                        $(this).dialog('close');
                        $(this).remove();
                        if (callback != null) {
                            callback();
                        }
                    }
                }
            });
    };
    $.showConfirm = function (text, positiveCallback, negativeCallback) {

        $('<div></div>').html(text)
            .dialog({
                modal: true,
                title: 'Confirmation',
                buttons: {
                    'Cancel': function () {
                        $(this).dialog('close');
                        $(this).remove();
                        if (negativeCallback != null) {
                            negativeCallback();
                        }
                    },
                    'Ok': function () {
                        $(this).dialog('close');
                        $(this).remove();
                        if (positiveCallback != null) {
                            positiveCallback();
                        }

                    }
                }
            });
    };



})(jQuery);