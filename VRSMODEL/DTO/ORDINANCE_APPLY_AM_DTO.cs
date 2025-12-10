using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSMODEL.DTO
{
    public class ORDINANCE_APPLY_AM_DTO
    {
        [NotMapped]
        public int SrNo { get; set; }
        public int COURSE_ID { get; set; }
        public string SESSION_ID { get; set; }
        public bool IS_RW { get; set; }
        public string CollegeCode { get; set; }
        public string ROLL_NO { get; set; }
        public string EXAM_TYPE_ID { get; set; }
        public int RESULT_TYPE_ID { get; set; }
        
    }

    
    public class ORDINANCE_APPLY_AM_DTO_DASH
    {
        public List<EXAM_TYPE_MASTER_DTO> EXAM_TYPE_LIST { get; set; }
        public List<COURSE_FILTER_DTO> COURSE_FILTER_LIST { get; set; }
        public List<SESSION_MASTER_DTO> SESSION_MASTER_LIST { get; set; }
        public List<COLLEGES_DTO> COLLEGES_LIST { get; set; }
        public List<RESULT_TYPE_DTO> RESULT_TYPE_LIST { get; set; }


    }
    public class COLLEGES_DTO
    {
        
        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public int CollegeType { get; set; }
        public int IsRWCollege { get; set; }
        

    }
}