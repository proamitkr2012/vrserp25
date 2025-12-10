using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSMODEL.DTO
{
    public class ORDINANCE_MAPPING_AM_DTO
    {
        [NotMapped]
        public int SrNo { get; set; }
        [NotMapped]
        public string COURSE_NAME { get; set; }
        public int ORD_RESULTTYPE_ASS_ID { get; set; }
        public int COURSE_ID { get; set; }
        public int RESULT_TYPE_ID { get; set; }
        [NotMapped]
        public String? RESULT_TYPE_NAME{ get; set; }
        [NotMapped]
        public string? ORDINANCE_NAME { get; set; }
        public int ORDINANCE_ID { get; set; }
        public string PROC_NAME { get; set; }
        public bool IsACTIVE { get; set; }
        public string COURSE_IDLIST { get; set; }

    }

    public class ORDINANCE_MAPPING_AM_DTO_DASH
    {
        public List<ORDINANCE_MAPPING_AM_DTO> DataList { get; set; }
        public PagingDTO PagingList { get; set; }

    }
    public class MANAGE_ORDINANCE_MAPPING_AM_DTO
    {
        public List<RESULT_TYPE_DTO> RESULT_TYPE_LIST { get; set; }
        public List<COURSE_FILTER_DTO> COURSE_FILTER_LIST { get; set; }
        public List<ORDINANCE_FILTER_AM_DTO> ORDINANCE_LIST { get; set; }

    }

    public class MANAGE_ORDINANCE_STUDENT_MAPPING_AM_DTO
    {
        public List<EXAM_TYPE_MASTER_DTO> EXAM_LIST { get; set; }
        public List<COURSE_FILTER_DTO> COURSE_FILTER_LIST { get; set; }
        public List<ORDINANCE_FILTER_AM_DTO> ORDINANCE_LIST { get; set; }
        public List<SESSION_MASTER_DTO> SESSION_LIST { get; set; }
        public List<HeldinDTO> HeldinList { get; set; }

    }

    public class ORDINANCE_STUDENT_DTO
    {
        public int ORDINANCE_ID { get; set; }
        public int COURSE_ID { get; set; }
        public string SESSIONNAME { get; set; }
        public string EXAMTPYENAME { get; set; }
        public string ROLL_NOS { get; set; }
        public string HELD_IN { get; set; }
        

    }

}