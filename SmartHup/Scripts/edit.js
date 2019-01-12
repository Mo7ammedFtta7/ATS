//if ($('input.isSavable').prop('checked')) {
//}

window.onload = function () {
    if ($(".isSavable").is(':checked')) {
        //$("#isSavablee").val(true);
        $("#dbColumnName").prop("disabled", false);
    };

    if ($("#isPaymentInfo").is(':checked')) {
        //$("#isSavablee").val(true);
        $("#paymentInfoOrder").prop("disabled", false);
    } else {
        $("#paymentInfoOrder").prop("disabled", true);

    }
    if ($("#isParent").is(':checked')) {
        //$("#isSavablee").val(true);
        $("#parentId").prop("disabled", true);
    } else {
        $("#parentId").prop("disabled", false);

    }

    $('#isSavable').change(function () {
        if (this.checked) {
            $("#dbColumnName").prop("disabled", false);

        } else {
            $("#dbColumnName").prop("disabled", true);

        }
    });

    $('#isPaymentInfo').change(function () {
        if (this.checked) {
            $("#paymentInfoOrder").prop("disabled", false);

        } else {
            $("#paymentInfoOrder").prop("disabled", true);

        }
    });
    $('#isParent').change(function () {
        if (this.checked) {
            $("#parentId").prop("disabled", true);

        } else {
            $("#parentId").prop("disabled", false);

        }
    });
}

//function isSavablefunc(checkboxElem) {
//    if (checkboxElem.checked) {
//        $("#isSavablee").val(true);
//        $("#dbColumnName").prop("disabled", false);
//    }
//    else if (checkboxElem.checked == false) {
//        $("#isSavablee").val(false);

//        $("#dbColumnName").prop("disabled", true);
//    }

//}
//function isPaymentInfofunc(checkboxElem) {
//    if (checkboxElem.checked) {
//        $("#isPaymentInfoo").val(true);

//        $("#paymentInfoOrder").prop("disabled", false);
//    }
//    else if (checkboxElem.checked == false) {
//        $("#isPaymentInfoo").val(false);
//        $("#paymentInfoOrder").prop("disabled", true);
//    }

//}

//function precheck(checElem) {
//    if (checElem.checked) {

//        $("#precheckServiceId").prop("disabled", false);
//    }
//    else if (checElem.checked == false) {
//        $("#precheckServiceId").prop("disabled", true);
//    }
//}
//function isParentfunc(checElem) {
//    if (checElem.checked) {
//        $("#parentId").prop("disabled", true);
//    }
//    else if (checElem.checked == false) {
//        $("#parentId").prop("disabled", false);
//    }
//}
//$(".isParent").click(function () {
//    alert("asasa")
//    //if ($("#isParent").is(":checked")) {
//    //    $("#parentId").prop("disabled", true);
//    //}
//    //else if ($("#isParent").is(":not(:checked)")) {
//    //    $("#parentId").prop("disabled", false);
//    //}
//});

//$("#precheckRquired").click(function () {
//    if ($("#precheckRquired").is(":checked")) {
//        $("#precheckServiceId").prop("disabled", false);
//    }
//    else if ($("#precheckRquired_Value").is(":not(:checked)")) {
//        $("#precheckServiceId").prop("disabled", true);
//    }
//});
