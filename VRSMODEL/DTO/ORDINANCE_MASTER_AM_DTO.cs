using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSMODEL.DTO
{
    public class ORDINANCE_MASTER_AM_DTO
    {
        [NotMapped]
        public int SrNo { get; set; }
        public int ORDINANCE_ID { get; set; }
        public string ORDINANCE_NAME { get; set; }
        public int RESULT_TYPE_ID { get; set; }
        [NotMapped]
        public String? RESULT_TYPE_NAME{ get; set; }
        //[NotMapped]
        //public String? COURSE_NAME { get; set; }
        //public int COURSE_ID { get; set; }
        public bool IS_RW { get; set; }
        //[NotMapped]
        //public String? SESSION_NAME { get; set; }
        //public int SESSION_ID { get; set; }
        public int SUBJECT_COUNT { get; set; }
        public int PAPER_COUNT { get; set; }
        public int COMPULSORY_PAPER_COUNT { get; set; }
        public int OPTIONAL_PAPER_COUNT { get; set; }
        
        public int GRACE { get; set; }
        public decimal RESULT_PASS_PERCENT { get; set; }
        public int THEORY_MAX { get; set; }
        public int THEORY_MIN { get; set; }
        public int PRACTICAL_MAX { get; set; }
        public int PRACTICAL_MIN { get; set; }
        public int GRAND_MAX { get; set; }
        public int GRAND_MIN { get; set; }
        public bool FINAL_YEAR_SEM { get; set; }
        public decimal I_DIV_MAX_CGPA_PERCENT { get; set; }
        public decimal I_DIV_MIN_CGPA_PERCENT { get; set; }
        public decimal II_DIV_MAX_CGPA_PERCENT { get; set; }
        public decimal II_DIV_MIN_CGPA_PERCENT { get; set; }
        public decimal III_DIV_MAX_CGPA_PERCENT { get; set; }
        public decimal III_DIV_MIN_CGPA_PERCENT { get; set; }
        public int FAIL_PAPER_FOR_BACK { get; set; }
        public decimal AGG_MARKS_PAPER_FOR_BACK { get; set; }
        //public string PROC_NAME { get; set; }
        public int SEMCREDIT { get; set; }
        public int TOTALCREDIT { get; set; }
        public int TOTAL_MAJOR_CR { get; set; }
        
    }

    public class ORDINANCE_MASTER_AM_DTO_DASH
    {
        public List<ORDINANCE_MASTER_AM_DTO> DataList { get; set; }
        public PagingDTO PagingList { get; set; }

    }
    public class MANAGE_ORDINANCE_MASTER_AM_DTO
    {
        public List<RESULT_TYPE_DTO> RESULT_TYPE_LIST { get; set; }
        public List<COURSE_FILTER_DTO> COURSE_FILTER_LIST { get; set; }
        public List<SESSION_MASTER_DTO> SESSION_MASTER_LIST { get; set; }

    }
    public class RESULT_TYPE_DTO
    {
        public int RESULT_TYPE_ID { get; set; }
        public string RESULT_TYPE { get; set; }

    }
    public class COURSE_FILTER_DTO
    {
        public int COURSE_ID { get; set; }
        public int COURSE_ID_MAIN { get; set; }
        public string COURSE_NAME { get; set; }
        public string? COURSE_NAME_PROPER { get; set; }

    }
    public class SESSION_MASTER_DTO
    {
        public int SESSION_ID { get; set; }
        public string SESSIONS { get; set; }
        public string START_YEAR { get; set; }
        public string? ENDYEAR_YEAR { get; set; }
        public bool? ISACTIVE { get; set; }

    }
}