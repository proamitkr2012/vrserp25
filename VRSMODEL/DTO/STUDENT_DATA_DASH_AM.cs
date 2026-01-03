using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VRSMODEL.DTO
{
    public class STUDENT_DATA_DASH_AM
    {

        public int SrNo { get; set; }
        public STUDENT_DATA_AM STDDATA { get; set; }
        public List<STUDENT_DATA_MARKS_AM> STDMARKSLIST { get; set; }
        public STUDENT_DATA_RESULT_AM STDRESULTLIST { get; set; }
    }


    public class STUDENT_DATA_AM
    {
        public int SrNo { get; set; }
        public int MASTER_ID { get; set; }
        public string SOURCE_TYPE { get; set; }
        public string ENROLLMENT_NO { get; set; }
        public string AADHAAR_NO { get; set; }
        public string ABC_ACCOUNT_ID { get; set; }
        public string STUDENT_NAME { get; set; }
        public string FATHER_NAME { get; set; }
        public string MOTHER_NAME { get; set; }
        public string GENDER { get; set; }
        public string CATEGORY { get; set; }
        public string SUB_CATEGORY { get; set; }
        public DateTime? DOB { get; set; }
        public string NATIONALITY_CODE { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string MOBILE { get; set; }
        public string FOREIGN_MOBILE_NO { get; set; }
        public string EMAIL { get; set; }
        public string ALTERNATE_MOBILE { get; set; }
        public string GUARDIAN_MOBILE { get; set; }
        public string PHOTO_PATH { get; set; }
        public string SIGNATURE_PATH { get; set; }
        public bool MOBILE_VERIFIED { get; set; }
        public bool EMAIL_VERIFIED { get; set; }
        public string LOGIN_PASSWORD { get; set; }
        public string REMARKS { get; set; }
        public DateTime? CREATED_AT { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? LAST_UPDATED_AT { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public bool IS_ACTIVE { get; set; }
        public string RU_NUMBER { get; set; }
        public string LAST_APPROVED_BY { get; set; }
        public DateTime? LAST_APPROVED_AT { get; set; }
        public string LAST_EDIT_REMARKS { get; set; }
        public string APPLICATION_DOCUMENT { get; set; }
        public bool IS_DOWNLOAD_PHOTO { get; set; }
        public bool IS_VRS { get; set; }
        public string STUDENT_NAME_APS { get; set; }
        public string STUDENT_NAME_KRUTI_DEV { get; set; }
        public string STUDENT_NAME_UNICODE { get; set; }
        public string PHOTO_PATH_OLD { get; set; }

    }


    public class STUDENT_DATA_MARKS_AM
    {
        public int SrNo { get; set; }
        public int RESULT_ID { get; set; }
        public int MARKS_ID { get; set; }
        public string RESULT_ID_OLD { get; set; }
        public int? SUBJECT_SERIAL { get; set; }
        public int? PAPER_SERIAL { get; set; }
        public int? MASTER_ID { get; set; }
        public string EXTERNAL_ID { get; set; }
        public int? COURSE_ID { get; set; }
        public string EXAM_TYPE { get; set; }
        public string SESSION { get; set; }
        public int? YEAR_SEM { get; set; }
        public string COLLEGE_CODE { get; set; }
        public string ROLL_NO { get; set; }
        public string PAPER_CODE { get; set; }
        public string CATCH_CODE { get; set; }
        public string SUBJECT_NAME { get; set; }
        public string PAPER_NAME { get; set; }
        public int? THEORY_OBT { get; set; }
        public int INTERNAL_OBT { get; set; }
        public int? SESSIONAL_OBT { get; set; }
        public int? TOTAL_OBT { get; set; }
        public string PAPER_STATUS { get; set; }
        public string SUBJECT_CODE { get; set; }
        public string SUBJECT_TYPE { get; set; }
        public string SUBJECT_TYPE_SERIAL { get; set; }
        public int? CREDIT { get; set; }
        public int? EARNED_CREDIT { get; set; }
        public decimal? POINT { get; set; }
        public decimal? GRADE_POINT { get; set; }
        public string GRADE { get; set; }
        public int? THEORY_MAX { get; set; }
        public int? THEORY_MIN { get; set; }
        public string THEORY_GRACE { get; set; }
        public bool?  THEORY_PRACT_ABSENT { get; set; }
        public int? PRACTICAL_MAX { get; set; }
        public int? PRACTICAL_MIN { get; set; }
        public int? PRACTICAL_OBT { get; set; }
        public bool? PRACTICAL_ABSENT { get; set; }
        public int? INTERNAL_MAX { get; set; }
        public int? INTERNAL_MIN { get; set; }
        public bool? INTERNAL_ABSENT { get; set; }
        public int? SESSIONAL_MAX { get; set; }
        public int? SESSIONAL_MIN { get; set; }
        public bool? SESSIONAL_ABSENT { get; set; }
        public int? TOTAL_MAX { get; set; }
        public int? TOTAL_MIN { get; set; }
        public string SUBJECT_GRACE { get; set; }
        public string SUBJECT_STATUS { get; set; }
        public bool? RE_EX { get; set; }
        public int? OLD_MARKS { get; set; }
        public int? BACK_POST_ID { get; set; }
        public string BARCODE { get; set; }
        public bool? OMR_FEE_PAID { get; set; }
        public string SCAN_IMAGE_PATH { get; set; }
        public string OMR_SERIES { get; set; }
        public string ANSWER_KEY_PATH { get; set; }
        public DateTime? CREATED_AT { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? LAST_UPDATED_AT { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public bool? THEORY_PRACT_MW { get; set; }
        public bool? INTERNAL_MW { get; set; }
        public bool? SESSIONAL_MW { get; set; }
        public bool? UFM { get; set; }
        public string SOURCE_TYPE { get; set; }
        public string CREDIT_POINT { get; set; }
        public string REMARKS { get; set; }
        public bool? BACK_POSTED { get; set; }
        public bool? IS_INTERNAL_RECEIVED { get; set; }
        public bool? MW { get; set; }
        public bool? THEORY_ABSENT { get; set; }
        public bool? THEORY_MW { get; set; }
        public bool? ADD_PAPER_CREDIT { get; set; }


    }
    public class STUDENT_DATA_RESULT_AM
    {
        public int SrNo { get; set; }


    }


}