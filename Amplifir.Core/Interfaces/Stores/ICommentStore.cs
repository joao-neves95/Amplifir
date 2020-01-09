using System.Threading.Tasks;

namespace Amplifir.Core.Interfaces
{
    public interface ICommentStore
    {
        Task<int> CreateCommentAsync();

        Task<int> LikeCommentAsync();

        Task<int> DislikeCommentAsync();
    }
}
