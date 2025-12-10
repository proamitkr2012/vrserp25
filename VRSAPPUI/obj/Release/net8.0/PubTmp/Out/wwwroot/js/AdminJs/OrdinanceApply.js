
function toTitleCase(str) {
    if (!str) return str;
    // normalize to lowercase first so "hELLo" -> "Hello"
    str = str.toLowerCase();
    // Uppercase the first letter after start, space, hyphen or apostrophe
    return str.replace(/(^|\s|[-'’])(\S)/g, function (_, sep, ch) {
        return sep + ch.toUpperCase();
    });
}

function SubmitFormA() {

    flag = true;
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    if ($.trim($("#SESSION_ID").val()) == "") {
        $('#SESSION_ID').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#SESSION_ID').removeClass("input-validation-error");
    }
    if ($.trim($("#COURSE_ID").val()) == "") {
        $('#select2-COURSE_ID-container').closest('.select2-selection--single').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#select2-COURSE_ID-container').closest('.select2-selection--single').removeClass("input-validation-error");
    }

    if ($.trim($("#EXAM_TYPE_ID").val()) == "") {
        $('#EXAM_TYPE_ID').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#EXAM_TYPE_ID').removeClass("input-validation-error");
    }
    if ($.trim($("#RESULT_TYPE_ID").val()) == "") {
        $('#RESULT_TYPE_ID').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#RESULT_TYPE_ID').removeClass("input-validation-error");
    }

    if (flag) {

        var formData = new FormData();

        formData.append("COURSE_ID", $("#COURSE_ID").val()),
        formData.append("SESSION_ID", $("#SESSION_ID").val()),
        formData.append("IS_RW", $("#IS_RW").prop('checked')),
        formData.append("EXAM_TYPE_ID", $("#EXAM_TYPE_ID").val()),
        formData.append("CollegeCode", $("#CollegeCodeDDL").val()),
        formData.append("ROLL_NO", $("#ROLL_NO").val())
        
        formData.append("RESULT_TYPE_ID", $("#RESULT_TYPE_ID").val()),


            $.ajax({
                type: "POST",
                url: '/admin/dashboard/applyordinance',
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
                        $("#CollegeCodeDDL").val('').trigger('change');
                        $("#COURSE_ID").val('').trigger('change');
                       
                        $("#EXAM_TYPE_ID").val('');
                        $("#SESSION_ID").val('');
                        $("#RESULT_TYPE_ID").val('');
                        
                        $("#IS_RW").prop('checked', false);

                        $("#ROLL_NO").val('');

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