using Application.Services.Dto;

namespace Application.Common.Interface
{
    public interface ICurrentUserHelper
    {
        public CurrentUserDto CurrentUser { get; }
    }
}
