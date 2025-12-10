using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VRSMODEL.DTO;
using VRSMODEL.DTO.Result;

namespace RModel.DTO
{
    public class StudentList_Admin
    {
        public string? CourseType { get; set; }
        public string? ExamTypeName { get; set; }
        public int? CourseID { get; set; }
        public string? CourseName { get; set; }
        public string? SessionName { get; set; }
        public string? RollN { get; set; }
        public int? IsLiveStatus { get; set; }
        

        public bool? IsPG { get; set; }
        public List<CourseMasterDTO_AM> CourseTypeList { get; set; }
        public List<CourseMasterDTO_AM> CourseList { get; set; }
        public List<SessionDTO> SessionList { get; set; }
        public List<EXAM_TYPE_MASTER_DTO> ExamTypeList { get; set; }
        public List<StudentMasterDTO> StudentList { get; set; }
        public List<ResultViewedDTO> ResultViewed { get; set; }

    }
}