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
                        $('<option></option>').val(res.PTYPE).html(res.PAPER_TYPE_NAME + "(" + res.PTYPE + ")"));
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

function toTitleCase(str) {
    if (!str) return str;
    // normalize to lowercase first so "hELLo" -> "Hello"
    str = str.toLowerCase();
    // Uppercase the first letter after start, space, hyphen or apostrophe
    return str.replace(/(^|\s|[-'’])(\S)/g, function (_, sep, ch) {
        return sep + ch.toUpperCase();
    });
}

function SubmitFormD() {

    flag = true;
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

   
    if ($.trim($("#ORDINANCE_ID").val()) == "") {
        $('#select2-ORDINANCE_ID-container').closest('.select2-selection--single').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#select2-ORDINANCE_ID-container').closest('.select2-selection--single').removeClass("input-validation-error");
    }
    
    if ($.trim($("#selectpaper_type").val()) == "") {
        $('#selectpaper_type').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#selectpaper_type').removeClass("input-validation-error");
    }
    if ($.trim($("#PAPER_TYPE_CAT").val()) == "") {
        $('#PAPER_TYPE_CAT').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#PAPER_TYPE_CAT').removeClass("input-validation-error");
    }
    //if ($.trim($("#THEORY_MIN").val()) == "") {
    //    $('#THEORY_MIN').addClass("input-validation-error");

    //    flag = false;
    //}
    //else {
    //    $('#THEORY_MIN').removeClass("input-validation-error");
    //}


    if (flag) {

        var formData = new FormData();
        
        formData.append("ORDINANCE_ID", $("#ORDINANCE_ID").val()),
            formData.append("PAPER_TYPE_CAT", $("#PAPER_TYPE_CAT").val()),
            formData.append("PAPER_CREDIT", $("#PAPER_CREDIT").val()),
            formData.append("THEORY_MAX", $("#THEORY_MAX").val()),
            formData.append("THEORY_MIN", $("#THEORY_MIN").val()),
            formData.append("THEORY_PASS_PERCENT", $("#THEORY_PASS_PERCENT").val()),
            formData.append("INTERNAL_MAX", $("#INTERNAL_MAX").val()),
            formData.append("INTERNAL_MIN", $("#INTERNAL_MIN").val()),
            formData.append("INTERNAL_PASS_PERCENT", $("#INTERNAL_PASS_PERCENT").val()),
            formData.append("SESSIONAL_MAX", $("#SESSIONAL_MAX").val()),
            formData.append("SESSIONAL_MIN", $("#SESSIONAL_MIN").val()),
            formData.append("SESSIONAL_PASS_PERCENT", $("#SESSIONAL_PASS_PERCENT").val()),
            formData.append("PAPER_TOTAL_MAX", $("#PAPER_TOTAL_MAX").val()),
            formData.append("PAPER_TOTAL_MIN", $("#PAPER_TOTAL_MIN").val()),
            formData.append("PAPER_TOTAL_PASS_PERCENT", $("#PAPER_TOTAL_PASS_PERCENT").val())
            formData.append("THEORY_CHECK", $("#THEORY_CHECK").prop('checked')),
            formData.append("INTERNAL_CHECK", $("#INTERNAL_CHECK").prop('checked')),
            formData.append("SESSIONAL_CHECK", $("#SESSIONAL_CHECK").prop('checked')),  
                formData.append("PAPER_TOTAL_CHECK", $("#PAPER_TOTAL_CHECK").prop('checked')),  
        
            $.ajax({
                type: "POST",
                url: '/admin/dashboard/saveordinancedetails',
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
                        $("#ORDINANCE_ID").val('').trigger('change');
                      
                        $("#PAPER_TYPE_CAT").val(0);
                        $("#PAPER_CREDIT").val(0);
                        $("#THEORY_MAX").val(0);
                        $("#THEORY_MIN").val(0);
                        $("#THEORY_PASS_PERCENT").val(0);
                        $("#INTERNAL_MAX").val(0);
                        $("#INTERNAL_MIN").val(0);
                        $("#INTERNAL_PASS_PERCENT").val(0); 
                        $("#SESSIONAL_MAX").val(0); 
                        $("#SESSIONAL_MIN").val(0); 
                        $("#SESSIONAL_PASS_PERCENT").val(0); 
                        $("#PAPER_TOTAL_MAX").val(0); 
                        $("#PAPER_TOTAL_MIN").val(0); 
                        $("#PAPER_TOTAL_PASS_PERCENT").val(0); 
                        
                        $("#THEORY_CHECK").prop('checked', false);
                        $("#INTERNAL_CHECK").prop('checked', false);
                        $("#SESSIONAL_CHECK").prop('checked', false);
                        $("#PAPER_TOTAL_CHECK").prop('checked', false);
                    
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