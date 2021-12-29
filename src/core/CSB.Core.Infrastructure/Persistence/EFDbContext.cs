using CSB.Core.Application.Infrastructure.Persistence;
using CSB.Core.Entities.Logging;
using CSB.Core.Exceptions;
using CSB.Core.Utilities.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CSB.Core.Infrastructure.Persistence
{
    public abstract class EFDbContext : DbContext, IDataContext
    {
        protected readonly IConfiguration _configuration;
        private protected readonly string _connectionStringName;
        private bool _isDisposed;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public EFDbContext(string connectionStringName, IConfiguration configuration, IServiceProvider provider)
        {
            _connectionStringName = connectionStringName;
            _configuration = configuration;
            _httpContextAccessor = (IHttpContextAccessor)provider.GetService(typeof(IHttpContextAccessor));
            _logService = (ILogService)provider.GetService(typeof(ILogService));
        }

        public IDbContextTransaction BeginTransaction()
        {
            try
            {
                return Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception exc)
            {
                ClearChangeTracker();
                throw new CoreException($"\"Transaction cannot be started.\" Exception: {exc.Message}", exc);
            }
        }

        public bool ChangeTrackingEnabled { set => ChangeTracker.AutoDetectChangesEnabled = value; }
        private void ClearChangeTracker()
        {
            if (ChangeTracker.AutoDetectChangesEnabled)
                ChangeTracker.Clear();
        }

        public override int SaveChanges()
        {
            try
            {

                List<AuditLog> logs = OnBeforeSaveChanges();
                var result = base.SaveChanges();
                OnAfterSaveChanges(logs);
                return result;
            }
            finally
            {
                ClearChangeTracker();
            }
        }

        private void OnAfterSaveChanges(List<AuditLog> logs)
        {
            _logService.LogAsync(
                        LogSettings<Logging.AuditLog>.Create(
                            "WebApiAuditLog",
                            Logging.AuditLog.Create(logs),
                            "WebApiAuditLog"));
        }

        private List<AuditLog> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var entries = this.ChangeTracker.Entries();
            var serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var logs = entries.Where(entry => entry.State != EntityState.Detached || entry.State != EntityState.Unchanged)
                        .Select(entry =>
                        {
                            Type type = entry.Entity.GetType();

                            AuditLog log = new AuditLog();
                            log.EntityName = type.Name;

                            if (entry.State == EntityState.Deleted)
                            {
                                var oldVals = entry.GetDatabaseValues().ToObject();
                                log.OldEntityValue = JsonConvert.SerializeObject(oldVals, Formatting.Indented, serializerSettings);
                                log.Type = (byte)EntityState.Deleted;
                            }
                            //New Entity
                            else if (entry.State == EntityState.Modified)
                            {
                                var oldVals = entry.GetDatabaseValues().ToObject();
                                log.OldEntityValue = JsonConvert.SerializeObject(oldVals, Formatting.Indented, serializerSettings);

                                var newVals = entry.CurrentValues.ToObject();
                                log.NewEntityValue = JsonConvert.SerializeObject(newVals, Formatting.Indented, serializerSettings);
                                log.Type = (byte)EntityState.Modified;
                            }
                            else if (entry.State == EntityState.Added)
                            {
                                var newVals = entry.CurrentValues.ToObject();
                                log.NewEntityValue = JsonConvert.SerializeObject(newVals, Formatting.Indented, serializerSettings);
                                log.Type = (byte)EntityState.Added;
                            }

                            log.LogDate = DateTime.Now;
                            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                            {

                                log.FullUrl = _httpContextAccessor.HttpContext.Request.Path;
                            }
                            return log;
                        })
                        .ToList();
            return logs;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                List<AuditLog> logs = OnBeforeSaveChanges();
                var result = await base.SaveChangesAsync();
                OnAfterSaveChanges(logs);
                return result;
            }
            finally
            {
                ClearChangeTracker();
            }
        }


        #region [ Dispose ]

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    base.Dispose();
                }
                _isDisposed = true;
            }
        }
        public new void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}