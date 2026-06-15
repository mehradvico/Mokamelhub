using Application.Common.Enumerable;
using Application.Common.Helpers.Iface;
using Application.Services.Content.DetailSrv.Iface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public class RegixHelper : IRegixHelper
    {
        private readonly IDetailService detailService;
        public RegixHelper(IDetailService detailService)
        {
            this.detailService = detailService;
        }
        public async Task<bool> IsMobileAsync(string mobile)
        {
            return await IsValid(regixEnum: RegixEnum.MobileRegix, mobile);
        }
        public async Task<bool> IsEmailAsync(string email)
        {
            return await IsValid(regixEnum: RegixEnum.EmailRegix, email);
        }
        public async Task<bool> IsNaturalCodeAsync(string naturalCode)
        {
            return await IsValid(regixEnum: RegixEnum.NaturalCodeRegix, naturalCode);
        }
        public async Task<bool> IsPostalCode(string postalCode)
        {
            return await IsValid(regixEnum: RegixEnum.PostalCodeRegix, postalCode);
        }
        public async Task<bool> IsPassword(string password)
        {
            return await IsValid(regixEnum: RegixEnum.PasswordRegix, password);
        }
        private async Task<bool> IsValid(RegixEnum regixEnum, string input)
        {
            var regix = await detailService.GetByLabelAsync(regixEnum.ToString());
            if (regix.IsSuccess)
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(input, regix.Data.Value, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public Task<bool> IsBonusCode(string bonus)
        {
            bool isValid = bonus.Length == 7 && bonus.All(char.IsDigit);
            return Task.FromResult(isValid);
        }

    }
}
