//Title: Cozeit More plugin by Yasir Atabani
//Documentation: na
//Author: Yasir O. Atabani
//Website: http://www.cozeit.com
//Twitter: @yatabani
//
//Version 1.0.0 Apr 29th 2013 -First Release.
//Version 1.5.0 Jun 24th 2013 -Second Release.
//Version 1.5.1 Jul  8th 2013 -Third Release.
(function ($, undefined) {
    $.fn.czMore = function (options) {

        //Set defauls for the control
        var defaults = {
            max: 5,
            min: 0,
            onLoad: null,
            onAdd: null,
            onDelete: null
        };
        //Update unset options with defaults if needed
        var options = $.extend(defaults, options);
        $(this).bind("onAdd", function (event, data) {
            options.onAdd.call(event, data);
        });
        $(this).bind("onLoad", function (event, data) {
            options.onLoad.call(event, data);
        });
        $(this).bind("onDelete", function (event, data) {
            options.onDelete.call(event, data);
        });
        //Executing functionality on all selected elements
        return this.each(function () {
            var obj = $(this);
            var i = obj.children(".recordset").length;
            var divPlus = '<div id="btnPlus" />';
            var count = '<input id="czMore_txtCount" name="czMore_txtCount" type="hidden" size="5"  class="cz_MoreCounter" />';
            var internalcount = 0;
            obj.before(count);
            var recordset = obj.children("#first");
            obj.after(divPlus);
            var set = recordset.children(".recordset").children().first();
            var btnPlus = obj.siblings("#btnPlus");
            var counter = 0;
            btnPlus.css({
                'float': 'right',
                'border': '0px',
                'background-image': 'url("/Content/icons/add.png")',
                'background-position': 'center center',
                'background-repeat': 'no-repeat',
                'height': '25px',
                'width': '25px',
                'cursor': 'pointer'
            });

            if (recordset.length) {
                obj.siblings("#btnPlus").click(function () {
                    var item = recordset.clone().html();
                    item = item.replace(/\[0\]/g, "[" + i + "]");
                    item = item.replace(/\_0\_/g, "_" + i + "_");
                    //$('input[name="Branches.Index"]').attr('id').replace('[', '_');
                    //$('input[name="Branches.Index"]').attr('id').replace(']', '__');
                    //var id = $(obj).attr("id");
                    //id = id + ".Index";

                    //$(id).val(i);
                    // $('input[name="' + id + '"]').val(i);
                    //  $('input[name="Branches.Index"]').val(i);
                    //item = $(item).children().first();
                    //item = $(item).parent();
                    //$('input[id="Branches(' + i + ').Index"]').val(count);
                    internalcount++;
                    counter++;
                    obj.append(item);
                    loadMinus(obj.children().last());
                    minusClick(obj.children().last());
                    if (options.onAdd != null) {
                        obj.trigger("onAdd", i);
                    }
                    obj.siblings("#czMore_txtCount").val(i);

                    i++;

                    return false;
                });
                recordset.remove();
                for (var j = 0; j <= i; j++) {
                    loadMinus(obj.children()[j]);
                    minusClick(obj.children()[j]);

                    if (options.onAdd != null) {

                        obj.trigger("onAdd", j);
                    }
                }

                if (options.onLoad != null) {
                    obj.trigger("onLoad", i);
                }
                //obj.bind("onAdd", function (event, data) {
                //If you had passed anything in your trigger function, you can grab it using the second parameter in the callback function.
                //});
            }

            function resetNumbering() {
                $(obj).children(".recordset").each(function (index, element) {
                    var item = $(element).clone().html();

                    var itemValue = item.val();
                    item = item.replace(/\[([0-9]\d{0})\]/g, "[" + index + "]");
                    item = item.replace(/\_([0-9]\d{0})\_/g, "_" + index + "_");
                    //$('input[name="Branches.Index"]').attr('id').replace('[', '_');
                    //$('input[name="Branches.Index"]').attr('id').replace(']', '__');
                    item.val(itemValue);
                    $(element).html(item);
                    minusClick(element);
                });
            }

            function loadMinus(recordset) {
                var divMinus = '<div id="btnMinus" />';
                $(recordset).children().first().before(divMinus);
                var btnMinus = $(recordset).children("#btnMinus");
                btnMinus.css({
                    'float': 'right',
                    'border': '0px',
                    'background-image': 'url("/Content/icons/remove.png")',
                    'background-position': 'center center',
                    'background-repeat': 'no-repeat',
                    'height': '25px',
                    'width': '25px',
                    'cursor': 'pointer'
                });
            }

            function minusClick(recordset) {
                $(recordset).children("#btnMinus").click(function () {
                    var id = $(recordset).attr("data-id");
                    var sysid = $("#systemId", recordset).val();
                    var DID = $("#deletedID").val();
                    DID += "," + sysid;
                    $("#deletedID").val(DID);
                    $(recordset).remove();
                    resetNumbering();
                    
                    i = counter - 1;
                    counter--;
                    if (options.onDelete != null) {
                        if (id != null)
                            obj.trigger("onDelete", id);
                    }
                });
            }
        });
    };
})(jQuery);