using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSMODEL.DTO
{
    public class PAPER_MASTER_AM_DTO
    {
        [NotMapped]
        public int SrNo { get; set; }
        public string PAPER_CODE_PK { get; set; }
        public string? SUBJECT_NAME { get; set; }
        public string? PAPER_NAME { get; set; }
        public string? PAPER_TYPE { get; set; }
        public string? PAPER_TYPE_CAT { get; set; }
        public int? CREDIT { get; set; }
        public int IS_QUALIFIYING { get; set; }
        public int? THEORY_MAX { get; set; }
        public int? THEORY_MIN { get; set; }
        public int? INTERNAL_MAX { get; set; }
        public int? INTERNAL_MIN { get; set; }
        public int? SESSIONAL_MAX { get; set; }
        public int? SESSIONAL_MIN { get; set; }
        public int? TOTAL_MAX { get; set; }
        public int? TOTAL_MIN { get; set; }
        public string? SUBJECT_PAPER_CODE { get; set; }
        public int ISRW { get; set; }
        public int SESSIONID { get; set; }
        public int PAPER_SERIAL_NO { get; set; }

    }

    public class PAPER_MASTER_AM_DTO_DASH
    {
        public List<PAPER_MASTER_AM_DTO> PaperList { get; set; }
        public PagingDTO PagingList { get; set; }

    }
    public class MANAGE_PAPER_MASTER_AM_DTO
    {
        public List<PAPER_TYPE_AM_DTO> PAPER_TYPE_LIST { get; set; }
        public PAPER_MASTER_AM_DTO PAPER_MASTER_AM { get; set; }
        public  List<PAPER_TYPE_CATEGORY_AM_DTO> PAPERcatlist { get; set; }

}
    public class PAPER_TYPE_AM_DTO
    {
        public string PAPER_TYPE_NAME { get; set; }
        public string PTYPE { get; set; }

    }
    public class PAPER_TYPE_CATEGORY_AM_DTO
    {
        public int ID { get; set; }
        public string PAPER_TYPE_NAME { get; set; }
        public string PTYPE { get; set; }
        public string IS_VISIBLE { get; set; }
        public string PAPER_MASTER_TYPE { get; set; }

    }
}