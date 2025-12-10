using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VRSMODEL.DTO
{
    public class ORDINANCE_DETAILS_AM_DTO
    {
        [NotMapped]
        public int SrNo { get; set; }
        public int ORDINANCE_ID { get; set; }
        [NotMapped]
        public string ORDINANCE_NAME { get; set; }
        public string PAPER_TYPE_CAT { get; set; }
       public int PAPER_CREDIT { get; set; } 
       public int THEORY_MAX { get; set; }
       public int THEORY_MIN { get; set; }
       public decimal THEORY_PASS_PERCENT { get; set; }
       public int INTERNAL_MAX { get; set; }
       public int INTERNAL_MIN { get; set; }
       public decimal INTERNAL_PASS_PERCENT { get; set; }
       public int SESSIONAL_MAX { get; set; }
       public int SESSIONAL_MIN { get; set; }
       public decimal SESSIONAL_PASS_PERCENT { get; set; }
       public int PAPER_TOTAL_MAX { get; set; }
       public int PAPER_TOTAL_MIN { get; set; }
       public decimal PAPER_TOTAL_PASS_PERCENT { get; set; }
       public bool IS_DELETED { get; set; }
       public DateTime CREATEON { get; set; }
        public DateTime UPDATEON { get; set; }
        public bool THEORY_CHECK { get; set; }
        public bool INTERNAL_CHECK { get; set; }
        public bool SESSIONAL_CHECK { get; set; }
        public bool PAPER_TOTAL_CHECK { get; set; }
        
    }

    public class ORDINANCE_DETAILS_AM_DTO_DASH
    {
        public List<ORDINANCE_DETAILS_AM_DTO> DataList { get; set; }
        public PagingDTO PagingList { get; set; }

    }
    public class MANAGE_ORDINANCE_DETAILS_AM_DTO
    {
        public List<ORDINANCE_FILTER_AM_DTO> ORDINANCE_LIST { get; set; }
        public List<PAPER_TYPE_AM_DTO> PAPER_MASTER_LIST { get; set; }

    }
    public class ORDINANCE_FILTER_AM_DTO
    {
      
        public int ORDINANCE_ID { get; set; }
        public string ORDINANCE_NAME { get; set; }
    }
    }