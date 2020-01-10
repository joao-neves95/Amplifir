using System.Threading.Tasks;

namespace Amplifir.Core.Interfaces
{
    public interface IBadWordsService
    {
        Task<string> CleanAsync( string content );
    }
}
