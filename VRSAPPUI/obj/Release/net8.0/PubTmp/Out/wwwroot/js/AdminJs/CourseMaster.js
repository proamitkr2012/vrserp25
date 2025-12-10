function Getchangeincourse() {
    var selectedValue = $("#COURSE_ID_MAIN option:selected").text()
    $("#COURSE_NAME").val(selectedValue + "SEMESTER-I");
    
    $("#COURSE_NAME_PROPER").val(toTitleCase(selectedValue + "SEMESTER-I"));
   
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

function SubmitFormC() {

    flag = true;
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    if ($.trim($("#COURSE_ID_MAIN").val()) == "") {
        $('#select2-COURSE_ID_MAIN-container').closest('.select2-selection--single').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#select2-COURSE_ID_MAIN-container').closest('.select2-selection--single').removeClass("input-validation-error");
    }
    if ($.trim($("#COURSE_NAME").val()) == "") {
        $("#COURSE_NAME").addClass("input-validation-error");

        flag = false;
    }
    else {
        $("#COURSE_NAME").removeClass("input-validation-error");
    }
    if ($.trim($("#selectsemester").val()) == "") {
        $('#selectsemester').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#selectsemester').removeClass("input-validation-error");
    }
    if ($.trim($("#selectyear").val()) == "") {
        $('#selectyear').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#selectyear').removeClass("input-validation-error");
    }
    if ($.trim($("#selectcoursemode").val()) == "") {
        $('#selectcoursemode').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#selectcoursemode').removeClass("input-validation-error");
    }
    if ($.trim($("#selectcoursetype").val()) == "") {
        $('#selectcoursetype').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#selectcoursetype').removeClass("input-validation-error");
    }



    if (flag) {

        var formData = new FormData();
        formData.append("COURSE_ID_MAIN", $("#COURSE_ID_MAIN").val()),
            formData.append("COURSE_NAME", $("#COURSE_NAME").val().toUpperCase()),
            formData.append("COURSE_NAME_PROPER", $("#COURSE_NAME_PROPER").val()),
            formData.append("SEM_NO", $("#selectsemester").val()),
            formData.append("YEAR_NO", $("#selectyear").val()),
            formData.append("COURSE_MODE", $("#selectcoursemode").val()),
            formData.append("COURSE_TYPE", $("#selectcoursetype").val()),
            formData.append("IS_NEP", $("#IS_NEP").prop('checked')),
            formData.append("IS_ACTIVE", $("#IS_ACTIVE").prop('checked')),
          //  formData.append("IS_GROUP", $("#IS_GROUP").prop('checked'))


        $.ajax({
            type: "POST",
            url: '/admin/dashboard/savecourse',
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
                    $("#COURSE_ID_MAIN").val('').trigger('change');
                    $("#COURSE_NAME").val("");
                    $("#COURSE_NAME_PROPER").val("");
                    $("#selectsemester").val("");
                    $("#selectyear").val("");
                    $("#selectcoursemode").val("");
                    $("#selectcoursetype").val("");
                    $("#IS_NEP").prop('checked', false);
                    $("#IS_ACTIVE").prop('checked', false);
                    // $("#IS_GROUP").prop('checked', false);

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