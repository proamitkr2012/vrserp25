using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VRSMODEL.DTO
{
    public class COURSE_MASTER_AM_DTO
    {
        
        public int SrNo { get; set; }
        public int COURSE_ID { get; set; }
        public int COURSE_ID_MAIN { get; set; }
        public string COURSE_NAME { get; set; }
        public string? COURSE_NAME_PROPER { get; set; }
        public int SEM_NO { get; set; }
        public int? YEAR_NO { get; set; }
        public string? AGENCY_COURSE_ID { get; set; }
        public string? AGENCY_COURSE_NAME { get; set; }
        public int? AGENCY_SEM_NO { get; set; }
        public string COURSE_TYPE { get; set; }
        public string? COURSE_MODE { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public string? MARKSHEET_FORMAT_CODE { get; set; }
        public bool? IS_GROUP { get; set; }
        public bool? IS_NEP { get; set; }
        public bool? IS_ALLOWINPRACTICAL { get; set; }
        public string? SEMESTER_NAME { get; set; }

    }


    public class COURSE_MASTER_AM_DTO_DASH
    {
        public List<COURSE_MASTER_AM_DTO> CourseList { get; set; }
        public PagingDTO PagingList { get; set; }

    }


    public class PagingDTO
    {
        public int RecordCount { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageCurrent { get; set; } = 1;

    }

    public class MANAGE_COURSE_MASTER_AM_DTO
    {
        public List<COURSE_MASTER_MAIN_DTO> COURSE_MASTER_MAIN_LIST { get; set; }
        public List<COURSE_TYPE_MASTER_DTO> COURSE_TYPE_MASTER_LIST { get; set; }
        public List<EXAM_TYPE_MASTER_DTO> EXAM_TYPE_MASTER_LIST { get; set; }
        public List<COURSE_MODE_DTO> COURSE_MODE_LIST { get; set; }

        
    }

    public class COURSE_MASTER_MAIN_DTO
    {
        public int COURSE_ID_MAIN { get; set; }
        public string COURSE_NAME_MAIN { get; set; }
        public string COURSE_NAME_MAIN_PROPER { get; set; }
        public bool IS_ACTIVE { get; set; }

    }
    public class COURSE_TYPE_MASTER_DTO
    {
        public string COURSE_TYPE { get; set; }
        public string COURSE_TYPE_NAME { get; set; }
        public bool IS_VISIBLE { get; set; }

    }
    public class EXAM_TYPE_MASTER_DTO
    {
        public int EX_TYPE_ID { get; set; }
        public string EXAM_TYPE { get; set; }
        public int EXAM_TYPE_SERIAL { get; set; }
        public bool IS_ACTIVE { get; set; }

    }
    public class COURSE_MODE_DTO
    {
        public string COURSE_MODE { get; set; }

    }

}