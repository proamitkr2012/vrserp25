function Getchangeinpapertype() {
    var selectedValue = $("#selectpaper_type option:selected").val();
    var formData = new FormData();
    formData.append("PAPER_MASTER_TYPE", selectedValue)
    $("#PAPER_TYPE_CAT").html(""); 
    $("#PAPER_TYPE_CAT").append(
        $('<option></option>').val("").html("--select--"));

    if (selectedValue != "") {
        $.ajax({
            type: "POST",
            url: '/admin/dashboard/GetPaperTypeCat',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                $("#PAPER_TYPE_CAT").html(""); // clear before appending new list
                //$("#ddlBatchNo").append(
                //    $('<option></option>').val("").html("--select--"));

                // alert(response)
                $.each(response, function (i, res) {
                   // alert(res.PTYPE)
                    $("#PAPER_TYPE_CAT").append(
                        $('<option></option>').val(res.PTYPE).html(res.PAPER_TYPE_NAME + "(" + res.PTYPE +")"));
                });

                //if (from != 'byclick') {

                //    if ($("#hdnBatchNo").val() != "") {
                //        $("#ddlBatchNo").val($("#hdnBatchNo").val());
                //        //var b = $('#ddlBatchNo option:selected').val();
                //        //$("#lblBatchNo").val(b);

                //    }
                //}
            }
        });
    }
}

function SubmitFormP() {

    flag = true;
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    if ($.trim($("#PAPER_CODE_PK").val()) == "") {
        $("#PAPER_CODE_PK").addClass("input-validation-error");
        flag = false;
    }
    else {
        $("#PAPER_CODE_PK").removeClass("input-validation-error");
    }
    if ($.trim($("#selectpaper_type").val()) == "") {
        $("#selectpaper_type").addClass("input-validation-error");

        flag = false;
    }
    else {
        $("#selectpaper_type").removeClass("input-validation-error");
    }
    if ($.trim($("#PAPER_NAME").val()) == "") {
        $('#PAPER_NAME').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#PAPER_NAME').removeClass("input-validation-error");
    }
   

    if (flag) {

        var formData = new FormData();
                formData.append("PAPER_CODE_PK", $("#PAPER_CODE_PK").val().toUpperCase()),
                formData.append("SUBJECT_NAME", $("#SUBJECT_NAME").val()),
                formData.append("PAPER_NAME", $("#PAPER_NAME").val()),
                    formData.append("PAPER_TYPE", $("#selectpaper_type").val()),
                formData.append("PAPER_TYPE_CAT", $("#PAPER_TYPE_CAT").val()),
                formData.append("CREDIT", $("#CREDIT").val()),
                    formData.append("IS_QUALIFIYING", $("#IS_QUALIFIYING").prop('checked')?1:0),
                formData.append("THEORY_MAX", $("#THEORY_MAX").val()),
                formData.append("THEORY_MIN", $("#THEORY_MIN").val()),
                formData.append("INTERNAL_MAX", $("#INTERNAL_MAX").val()),
                formData.append("INTERNAL_MIN", $("#INTERNAL_MIN").val()),
                formData.append("SESSIONAL_MAX", $("#SESSIONAL_MAX").val()),
                formData.append("SESSIONAL_MIN", $("#SESSIONAL_MIN").val()),
                formData.append("TOTAL_MAX", $("#TOTAL_MAX").val()),
                formData.append("TOTAL_MIN", $("#TOTAL_MIN").val()),
                formData.append("SUBJECT_PAPER_CODE", $("#SUBJECT_PAPER_CODE").val()),
                formData.append("PAPER_SERIAL_NO", $("#PAPER_SERIAL_NO").val()),



        $.ajax({
            type: "POST",
            url: '/admin/dashboard/savepaper',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            beforeSend: function () {
                // setting a timeout
                $('#btnwaitc').show();
                $('#btnc').hide();

            },
            success: function (d) {
                if (d.ResponseCode == 1) {
                    //alert(JSON.stringify(d))
                    toastr.success(d.ResponseMessage)
                   
                    $("#PAPER_CODE_PK").val("");
                    $("#SUBJECT_NAME").val("");
                    $("#PAPER_NAME").val("");
                    $("#selectpaper_type").val("");
                    $("#PAPER_TYPE_CAT").val("");
                    $("#CREDIT").val("");
                 
                    $("#IS_QUALIFIYING").prop('checked', false);
                    $("#THEORY_MAX").val(0);
                    $("#THEORY_MIN").val(0);
                    $("#INTERNAL_MAX").val(0);
                    $("#INTERNAL_MIN").val(0);
                    $("#SESSIONAL_MAX").val(0);
                    $("#SESSIONAL_MIN").val(0);
                    $("#TOTAL_MAX").val(0);
                    $("#TOTAL_MIN").val(0);
                    $("#SUBJECT_PAPER_CODE").val("");
                    $("#PAPER_SERIAL_NO").val("");
                    

                } else {
                    toastr.error(d.ResponseMessage)
                }

            },
            error: function (result) {
                alert('Service call failed: ' + result.status + ' Type :' + result.statusText);
            },
            complete: function () {
                setTimeout(function () {
                    $('#btnwaitc').hide();
                    $('#btnc').show();

                }, 0);
            },
        });

    };
}