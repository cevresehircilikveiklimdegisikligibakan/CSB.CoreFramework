using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace CSB.Core.Application.Infrastructure.Persistence
{
    public interface IDataContext : IDisposable
    {
        IDbContextTransaction BeginTransaction();
        bool ChangeTrackingEnabled { set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}