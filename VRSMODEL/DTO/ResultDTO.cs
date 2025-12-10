using VRSDATA.Entities;
using VRSMODEL.DTO.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VRSMODEL.DTO
{
    public class ResultDTO
    {
        public string? CourseType { get; set; }
        public string? ExamTypeName { get; set; }
        public int? CourseID { get; set; }
        public string? CourseName { get; set; }
        public string? SessionName { get; set; }

        public bool? IsPG { get; set; }

        public List<HeldinDTO> HeldinList { get; set; }

    }

    public class HeldinDTO
    {
        public string HELD_IN { get; set; }

    }



    public class ResultDTO_DASH
    {
        public string? CourseType { get; set; }
        public string? ExamTypeName { get; set; }
        public int? CourseID { get; set; }
        public string? CourseName { get; set; }
        
        public string? SessionName { get; set; }
        public bool? IsPG { get; set; }
        public List<COLLEGE_TYPE_RESULT_DTO> CourseTypeList { get; set; }
        public List<SESSION_MASTER_DTO> SESSION_MASTER_LIST { get; set; }
        public List<CourseMasterDTO_AM> CourseList { get; set; }
        public List<EXAM_TYPE_MASTER_DTO> ExamTypeList { get; set; }
        public List<HeldinDTO> HeldinList { get; set; }
        


    }


    public class COLLEGE_TYPE_RESULT_DTO
    {
        public string? CourseType { get; set; }
        public int? CtypeID { get; set; }
        

    }

}