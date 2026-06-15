using Application.Common.Dto.SpotPlayer;

namespace Application.Common.Interface
{
    public interface ISpotPlayer
    {
        SpotPlayerResponseDto GetToken(string course, string name, string payload, string texts, bool test = false);
    }
}
