using AuditService.Data;
using AuditService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditService.Services
{
    public class AuditService
    {
        private readonly AuditDbContext _context;

        public AuditService(AuditDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuditLog>> GetAuditLogsAsync()
        {
            return await _context.AuditLogs.ToListAsync();
        }

        public async Task<AuditLog> GetAuditLogByIdAsync(int id)
        {
            return await _context.AuditLogs.FindAsync(id);
        }

        public async Task CreateAuditLogAsync(AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}