using AuditService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Controllers
{
    [Route("audit")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly Services.AuditService _auditService;

        public AuditController(Services.AuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuditLog([FromBody] AuditLog auditLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Request.Headers.TryGetValue("idUsuario", out var idUsuario))
            {
                auditLog.IdUsuario = idUsuario;
            }

            if (Request.Headers.TryGetValue("nombreUsuario", out var nombreUsuario))
            {
                auditLog.NombreUsuario = nombreUsuario;
            }

            await _auditService.CreateAuditLogAsync(auditLog);
            return CreatedAtAction(nameof(GetAuditLogById), new { id = auditLog.Id }, auditLog);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogs()
        {
            var logs = await _auditService.GetAuditLogsAsync();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuditLog>> GetAuditLogById(int id)
        {
            var log = await _auditService.GetAuditLogByIdAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }
    }
}