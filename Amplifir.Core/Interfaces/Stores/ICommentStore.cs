using System.Collections.Generic;
using System.Threading.Tasks;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
    public interface ICommentStore
    {
        Task<int> CreateCommentAsync( Comment newComment );

        Task<List<Comment>> GetCommentsByShoutIdAsync( int shoutId, int lastId = 0, int limit = 10 );
    }
}
