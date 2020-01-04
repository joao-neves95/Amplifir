using System.Threading.Tasks;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
    public interface IAuditLogStore
    {
        Task<int> CreateLog( AuditLog auditLog );
    }
}
