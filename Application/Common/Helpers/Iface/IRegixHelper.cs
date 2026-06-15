using System.Threading.Tasks;

namespace Application.Common.Helpers.Iface
{
    public interface IRegixHelper
    {
        Task<bool> IsMobileAsync(string mobile);

        Task<bool> IsEmailAsync(string email);

        Task<bool> IsNaturalCodeAsync(string naturalCode);

        Task<bool> IsPostalCode(string postalCode);

        Task<bool> IsPassword(string password);

        Task<bool> IsBonusCode(string bonus);

    }
}
