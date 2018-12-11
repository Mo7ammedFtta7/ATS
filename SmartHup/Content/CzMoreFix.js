function czMoreResetNumbering(_czMoreId, _form) {
    
    var container = $("#" + _czMoreId, _form); var containerLength = container.length;
    var recordsets = container.find(".recordset"); var recordsetlength = recordsets.length;
    for (var i = 0; i < recordsets.length; i++) {
        var allInput = recordsets[i].getElementsByTagName("input"); var allInputLength = allInput.length;
        for (var j = 0; j < allInput.length; j++) {
            changeName(allInput, j, i);
        }
        var select = recordsets[i].getElementsByTagName("select"); var selectLength = select.length;
        changeName(select, 0, i);

    }
}


function changeName(allInput, j, i) {
    var oldID = allInput[j].getAttribute("name");
    var IdArray = oldID.split(".");
    var newID = IdArray[0] + ".CUSTOMERS[" + i + "]." + IdArray[2];
    allInput[j].setAttribute("name", newID);
}