using System;

namespace VRSREPO
{
    public interface IUnitOfWork : IDisposable
    {
        AdminMasterRepository IAdminMaster { get; }
        int SaveChanges();
    }
}