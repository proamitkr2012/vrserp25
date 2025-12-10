using VRSMODEL;
using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace VRSREPO.Utilities
{
    public interface IMailClient
    {
        

        bool SendMail(string to, string subject, string body);
        bool SendMulpMail(string to, string subject, string body);
        

    }
}