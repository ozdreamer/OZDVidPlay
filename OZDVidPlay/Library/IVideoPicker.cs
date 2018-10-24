using System.Threading.Tasks;

namespace OZDVidPlay
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}