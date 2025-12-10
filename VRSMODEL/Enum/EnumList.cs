using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VRSMODEL.Enum
{
    public enum ENUM_APP_SEX 
    {
        CERTIFICATE ,
        DIPLOMA ,
        PG ,
        PHD,
        UG
    }

    public enum ENUM_APP_STATUS: int
    {
        Registration = 1,
        Appliedprogram = 2,
        DocumentUploaded = 3,
        Verifiedapplication = 4,
        Feepaid=5,
        Downloadapplication=6,
        
        CounsellingDocVerified=7,
        Counselling = 8,
        CounsellingFeePAID =9,
        CounsellingLetter=10,
    }
}
