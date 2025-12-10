using VRSMODEL.DTO;
using VRSDATA.Entities;
using System;
using System.Collections.Generic;
using VRSMODEL;
using System.Threading.Tasks;
using VRSMODEL.DTO.Result;

namespace VRSREPO
{
    public interface IAdminMasterRepository : IRepository<AdminMaster>
    {
        //admin-login
        Task<AdminMasterDTO> AuthenticateAdmin(string userName, string password);
        bool IsAdminEmailExists(string email);
        
        Task<FormResponse> VisitCountSet(StudentMasterDTO model);

    }
}
