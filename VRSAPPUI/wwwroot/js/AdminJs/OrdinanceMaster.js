function OnFINAL_YEAR_SEMCheck() {
    var checkBox = document.getElementById("FINAL_YEAR_SEM");
    var text = document.getElementById("FINAL_YEAR_SEM_DIV_DETAILS");
    if (checkBox.checked == true) {
        text.style.display = "block";
    } else {
        text.style.display = "none";
    }
   
}
function GRACE_DETAILS_CHECK() {
    
    var checkBox = document.getElementById("ALLOW_GRACE");
    var text = document.getElementById("GRACE_DETAILS");
    if (checkBox.checked == true) {
        text.style.display = "block";
       
    } else {
        text.style.display = "none";

        $('#EXAM_TYPE_ID').val('').trigger('change');
       
        $('#GRACE').val("0");
        $('#GRACE_IF_PASSED_AGG').prop('checked', false);
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

function SubmitFormO() {

    flag = true;
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    if ($.trim($("#ORDINANCE_NAME").val()) == "") {
        $('#ORDINANCE_NAME').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#ORDINANCE_NAME').removeClass("input-validation-error");
    }
    if ($.trim($("#RESULT_TYPE_ID").val()) == "") {
        $("#RESULT_TYPE_ID").addClass("input-validation-error");

        flag = false;
    }
    else {
        $("#RESULT_TYPE_ID").removeClass("input-validation-error");
    }
    //if ($.trim($("#SESSION_ID").val()) == "") {
    //    $('#SESSION_ID').addClass("input-validation-error");

    //    flag = false;
    //}
    //else {
    //    $('#SESSION_ID').removeClass("input-validation-error");
    //}
    //if ($.trim($("#COURSE_ID").val()) == "") {
    //    $('#select2-COURSE_ID-container').closest('.select2-selection--single').addClass("input-validation-error");

    //    flag = false;
    //}
    //else {
    //    $('#select2-COURSE_ID-container').closest('.select2-selection--single').removeClass("input-validation-error");
    //}
    //if ($.trim($("#SUBJECT_COUNT").val()) == "0" || $.trim($("#SUBJECT_COUNT").val()) == "") {
    //    $('#SUBJECT_COUNT').addClass("input-validation-error");

    //    flag = false;
    //}
    //else {
    //    $('#SUBJECT_COUNT').removeClass("input-validation-error");
    //}
    if ($.trim($("#COMPULSORY_PAPER_COUNT").val()) == "0" || $.trim($("#COMPULSORY_PAPER_COUNT").val()) == "") {
        $('#COMPULSORY_PAPER_COUNT').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#COMPULSORY_PAPER_COUNT').removeClass("input-validation-error");
    }
    if ($.trim($("#THEORY_MAX").val()) == "0" || $.trim($("#THEORY_MAX").val()) == "") {
        $('#THEORY_MAX').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#THEORY_MAX').removeClass("input-validation-error");
    }
    if ($.trim($("#THEORY_MIN").val()) == "0" || $.trim($("#THEORY_MIN").val()) == "") {
        $('#THEORY_MIN').addClass("input-validation-error");

        flag = false;
    }
    else {
        $('#THEORY_MIN').removeClass("input-validation-error");
    }


    if (flag) {

        var formData = new FormData();
        formData.append("ORDINANCE_NAME", $("#ORDINANCE_NAME").val().toUpperCase()),
            formData.append("RESULT_TYPE_ID", $("#RESULT_TYPE_ID").val()),
            //formData.append("COURSE_ID", $("#COURSE_ID").val()),
            //formData.append("SESSION_ID", $("#SESSION_ID").val()),
            formData.append("SUBJECT_COUNT", $("#SUBJECT_COUNT").val()),
            formData.append("PAPER_COUNT", $("#PAPER_COUNT").val()),
            formData.append("COMPULSORY_PAPER_COUNT", $("#COMPULSORY_PAPER_COUNT").val()),
            formData.append("OPTIONAL_PAPER_COUNT", $("#OPTIONAL_PAPER_COUNT").val()),

            formData.append("MAJOR_PAPER_COUNT", $("#MAJOR_PAPER_COUNT").val()),
            formData.append("VOCATIONAL_PAPER_COUNT", $("#VOCATIONAL_PAPER_COUNT").val()),
            formData.append("QUALIFY_PAPER_COUNT", $("#QUALIFY_PAPER_COUNT").val()),
            formData.append("RESEARCH_PAPER_COUNT", $("#RESEARCH_PAPER_COUNT").val()),
            formData.append("PRACTICAL_PAPER_COUNT", $("#PRACTICAL_PAPER_COUNT").val()),
            
            formData.append("TOTAL_MAJOR_CR", $("#TOTAL_MAJOR_CR").val()),
            formData.append("VOCATIONAL_CR", $("#VOCATIONAL_CR").val()),
            formData.append("RESEARCH_CR", $("#RESEARCH_CR").val()),

            
            
            formData.append("RESULT_PASS_PERCENT", $("#RESULT_PASS_PERCENT").val()),
            formData.append("SEMCREDIT", $("#SEMCREDIT").val()),
            formData.append("TOTALCREDIT", $("#TOTALCREDIT").val()),
            formData.append("TOTAL_MAJOR_CR", $("#TOTAL_MAJOR_CR").val()),

            formData.append("ALLOW_GRACE", $("#ALLOW_GRACE").prop('checked')),
            formData.append("GRACE", $("#GRACE").val()),
            formData.append("GRACE_IF_PASSED_AGG", $("#GRACE_IF_PASSED_AGG").prop('checked')),
            //formData.append("GRACE_APPLY_EXAM_TYPE", $("#GRACE_APPLY_EXAM_TYPE").val()),

            formData.append("THEORY_MAX", $("#THEORY_MAX").val()),
            formData.append("THEORY_MIN", $("#THEORY_MIN").val()),
            formData.append("PRACTICAL_MAX", $("#PRACTICAL_MAX").val()),
            formData.append("PRACTICAL_MIN", $("#PRACTICAL_MIN").val()),
            formData.append("GRAND_MAX", $("#GRAND_MAX").val()),
            formData.append("GRAND_MIN", $("#GRAND_MIN").val()),
            formData.append("TOTAL_MAX", $("#TOTAL_MAX").val()),
            formData.append("TOTAL_MIN", $("#TOTAL_MIN").val()),
            formData.append("FINAL_YEAR_SEM", $("#FINAL_YEAR_SEM").prop('checked')),
            formData.append("I_DIV_MAX_CGPA_PERCENT", $("#I_DIV_MAX_CGPA_PERCENT").val()),
            formData.append("I_DIV_MIN_CGPA_PERCENT", $("#I_DIV_MIN_CGPA_PERCENT").val()),
            formData.append("II_DIV_MAX_CGPA_PERCENT", $("#II_DIV_MAX_CGPA_PERCENT").val()),
            formData.append("II_DIV_MIN_CGPA_PERCENT", $("#II_DIV_MIN_CGPA_PERCENT").val()),
            formData.append("III_DIV_MAX_CGPA_PERCENT", $("#III_DIV_MAX_CGPA_PERCENT").val()),
            formData.append("III_DIV_MIN_CGPA_PERCENT", $("#III_DIV_MIN_CGPA_PERCENT").val()),


            formData.append("I_DIV_MAX_PRAC_PERCENT", $("#I_DIV_MAX_PRAC_PERCENT").val()),
            formData.append("I_DIV_MIN_PRAC_PERCENT", $("#I_DIV_MIN_PRAC_PERCENT").val()),
            formData.append("II_DIV_MAX_PRAC_PERCENT", $("#II_DIV_MAX_PRAC_PERCENT").val()),
            formData.append("II_DIV_MIN_PRAC_PERCENT", $("#II_DIV_MIN_PRAC_PERCENT").val()),
            formData.append("III_DIV_MAX_PRAC_PERCENT", $("#III_DIV_MAX_PRAC_PERCENT").val()),
            formData.append("III_DIV_MIN_PRAC_PERCENT", $("#III_DIV_MIN_PRAC_PERCENT").val()),

            formData.append("FAIL_PAPER_FOR_BACK", $("#FAIL_PAPER_FOR_BACK").val()),
            formData.append("AGG_MARKS_PAPER_FOR_BACK", $("#AGG_MARKS_PAPER_FOR_BACK").val()),
            
            formData.append("ADD_PRAC_IN_TOTAL_MARKS", $("#ADD_PRAC_IN_TOTAL_MARKS").prop('checked')),
            
            formData.append("IS_RW", $("#IS_RW").prop('checked')),
            
            formData.append("GRACE_APPLY_EXAM_TYPE", $("#EXAM_TYPE_ID").val()),
          //  alert("ss")
        
            $.ajax({
                type: "POST",
                url: '/admin/dashboard/saveordinance',
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
                        //$("#COURSE_ID").val('').trigger('change');
                        $("#ORDINANCE_NAME").val('');
                        $("#RESULT_TYPE_ID").val('');
                        //$("#SESSION_ID").val('');
                        $("#SUBJECT_COUNT").val(0);
                        $("#PAPER_COUNT").val(0);
                        $("#COMPULSORY_PAPER_COUNT").val(0);
                        $("#OPTIONAL_PAPER_COUNT").val(0); 
                        $("#MAJOR_PAPER_COUNT").val(0); 
                        $("#GRACE").val(0);
                        $("#RESULT_PASS_PERCENT").val(0);
                        $("#THEORY_MAX").val(0);
                        $("#THEORY_MIN").val(0);
                        $("#PRACTICAL_MAX").val(0);
                        $("#PRACTICAL_MIN").val(0);
                        $("#GRAND_MAX").val(0);
                        $("#GRAND_MIN").val(0);
                        $("#SEMCREDIT").val(0);
                        $("#TOTALCREDIT").val(0); 
                        $("#TOTAL_MAJOR_CR").val(0); 
                        $("#FINAL_YEAR_SEM").prop('checked', false);
                        $("#I_DIV_MAX_CGPA_PERCENT").val(0);
                        $("#I_DIV_MIN_CGPA_PERCENT").val(0);
                        $("#II_DIV_MAX_CGPA_PERCENT").val(0);
                        $("#II_DIV_MIN_CGPA_PERCENT").val(0);
                        $("#III_DIV_MAX_CGPA_PERCENT").val(0);
                        $("#III_DIV_MIN_CGPA_PERCENT").val(0);

                        $("#I_DIV_MAX_PRAC_PERCENT").val(0);
                        $("#I_DIV_MIN_PRAC_PERCENT").val(0);
                        $("#II_DIV_MAX_PRAC_PERCENT").val(0);
                        $("#II_DIV_MIN_PRAC_PERCENT").val(0);
                        $("#III_DIV_MAX_PRAC_PERCENT").val(0);
                        $("#III_DIV_MIN_PRAC_PERCENT").val(0);

                        $("#FAIL_PAPER_FOR_BACK").val(0);
                        $("#AGG_MARKS_PAPER_FOR_BACK").val(0);
                        $("#IS_RW").prop('checked', false);
                        $("#ADD_PRAC_IN_TOTAL_MARKS").prop('checked', false);
                        $("#ALLOW_GRACE").prop('checked', false);
                        
                        $('#EXAM_TYPE_ID').val('').trigger('change');
                    
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