using System.Collections.Generic;
using System.Threading.Tasks;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
    public interface ICommentStore
    {
        Task<int> CreateCommentAsync();

        Task<int> CreateCommentReactionAsync();

        Task<List<Comment>> GetCommentsByShoutIdAsync( int shoutId, int lastId = 0, int limit = 10 );

        Task<int> DeleteCommentReactionAsync();
    }
}
