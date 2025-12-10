
function toTitleCase(str) {
    if (!str) return str;
    // normalize to lowercase first so "hELLo" -> "Hello"
    str = str.toLowerCase();
    // Uppercase the first letter after start, space, hyphen or apostrophe
    return str.replace(/(^|\s|[-'’])(\S)/g, function (_, sep, ch) {
        return sep + ch.toUpperCase();
    });
}

function SubmitFormM() {

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
    if ($.trim($("#RESULT_TYPE_ID").val()) == "") {
        $("#RESULT_TYPE_ID").addClass("input-validation-error");

        flag = false;
    }
    else {
        $("#RESULT_TYPE_ID").removeClass("input-validation-error");
    }
    
    if ($.trim($("#PROC_NAME").val()) == "") {
        $('#PROC_NAME').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#PROC_NAME').removeClass("input-validation-error");
    }


    if (flag) {

        var formData = new FormData();
            formData.append("ORDINANCE_ID", $("#ORDINANCE_ID").val()),
            formData.append("RESULT_TYPE_ID", $("#RESULT_TYPE_ID").val()),            
            formData.append("COURSE_IDLIST", $("#output").text()),            
            formData.append("PROC_NAME", $("#PROC_NAME").val().toUpperCase()),
          //  alert("ss")
        
            $.ajax({
                type: "POST",
                url: '/admin/dashboard/saveordinancemapping',
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
                        $("#PROC_NAME").val('');
                        $("#output").val('');
                        $("#RESULT_TYPE_ID").val('');
                        document.querySelectorAll(".chk").forEach(cb => cb.checked = false);
                        $("#dropBtn").html("Select Course");
                        //$("#PROC_NAME").val('');
                    
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