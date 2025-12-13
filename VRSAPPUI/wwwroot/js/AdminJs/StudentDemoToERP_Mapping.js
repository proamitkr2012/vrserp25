
function toTitleCase(str) {
    if (!str) return str;
    // normalize to lowercase first so "hELLo" -> "Hello"
    str = str.toLowerCase();
    // Uppercase the first letter after start, space, hyphen or apostrophe
    return str.replace(/(^|\s|[-'’])(\S)/g, function (_, sep, ch) {
        return sep + ch.toUpperCase();
    });
}

function SubmitFormDE() {

    flag = true;
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    if ($.trim($("#COURSE_ID").val()) == "") {
        $('#select2-COURSE_ID-container').closest('.select2-selection--single').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#select2-COURSE_ID-container').closest('.select2-selection--single').removeClass("input-validation-error");
    }
    
    //if ($.trim($("#EXAMTPYENAME").val()) == "") {
    //    $("#EXAMTPYENAME").addClass("input-validation-error");

    //    flag = false;
    //}
    //else {
    //    $("#EXAMTPYENAME").removeClass("input-validation-error");
    //}
    
    if ($.trim($("#SESSIONNAME").val()) == "") {
        $('#SESSIONNAME').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#SESSIONNAME').removeClass("input-validation-error");
    } if ($.trim($("#HELD_IN").val()) == "") {
        $('#HELD_IN').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#HELD_IN').removeClass("input-validation-error");
    }
    

    if (flag) {

        var formData = new FormData();
     
            formData.append("COURSE_ID", $("#COURSE_ID").val()),
            formData.append("SESSIONNAME", $("#SESSIONNAME").val()),
            formData.append("EXAMTPYENAME", $("#EXAMTPYENAME").val()),
            formData.append("ROLL_NOS", $("#ROLL_NOS").val()),
            formData.append("HELD_IN", $("#HELD_IN").val()),
          //  alert("ss")
        
            $.ajax({
                type: "POST",
                url: '/admin/dashboard/mappingdemotoerp',
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
                       
                        $("#COURSE_ID").val('').trigger('change');
                        $("#SESSIONNAME").val('');
                        $("#EXAMTPYENAME").val('');
                        $("#HELD_IN").val('');
                        
                        $("#ROLL_NOS").val('');
                    
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


function SubmitFormSearch() {

    flag = true;
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    if ($.trim($("#COURSE_ID").val()) == "") {
        $('#select2-COURSE_ID-container').closest('.select2-selection--single').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#select2-COURSE_ID-container').closest('.select2-selection--single').removeClass("input-validation-error");
    }
    
    if ($.trim($("#EXAMTPYENAME").val()) == "") {
        $("#EXAMTPYENAME").addClass("input-validation-error");

        flag = false;
    }
    else {
        $("#EXAMTPYENAME").removeClass("input-validation-error");
    }

    if ($.trim($("#SESSIONNAME").val()) == "") {
        $('#SESSIONNAME').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#SESSIONNAME').removeClass("input-validation-error");
    }
    if ($.trim($("#HELD_IN").val()) == "") {
        $('#HELD_IN').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#HELD_IN').removeClass("input-validation-error");
    }

    if (flag) {

        var formData = new FormData();
            formData.append("ORDINANCE_ID", $("#ORDINANCE_ID").val()),
            formData.append("COURSE_ID", $("#COURSE_ID").val()),
            formData.append("SESSIONNAME", $("#SESSIONNAME").val()),
            formData.append("EXAMTPYENAME", $("#EXAMTPYENAME").val()),
                formData.append("ROLL_NOS", $("#ROLL_NOS").val()),
                formData.append("HELD_IN", $("#HELD_IN").val()),
            $.ajax({
                type: "POST",
                url: '/admin/dashboard/searchordinancestudent',
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                beforeSend: function () {
                    // setting a timeout
                    $('#btnssdn').show();
                    $('#btnss').hide();

                },
                success: function (d) {

                    if (d.ResponseCode == 1) {
                        //alert(JSON.stringify(d))
                        toastr.success(d.ResponseMessage)
                        
                    } else {
                        toastr.error(d.ResponseMessage)
                    }


                },
                error: function (result) {
                    alert('Service call failed: ' + result.status + ' Type :' + result.statusText);
                },
                complete: function () {
                    setTimeout(function () {
                        $('#btnssdn').hide();
                        $('#btnss').show();

                    }, 0);
                },
            });

    };
}