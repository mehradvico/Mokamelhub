using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Accounting.UserSrv.Iface;
using Application.Services.ProductSrvs.ProductLikeSrv.Dto;
using Application.Services.ProductSrvs.ProductLikeSrv.Iface;
using Application.Services.ProductSrvs.ProductSrv.Iface;
using Application.Services.Setting.MessageSenderSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductLikeSrv
{
    public class ProductLikeService : CommonSrv<ProductLike, ProductLikeDto>, IProductLikeService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IMessageSenderService messageService;
        private readonly IUserService userService;
        private readonly IProductService productService;
        public ProductLikeService(IDataBaseContext _context, IMapper mapper, IMessageSenderService messageService, IUserService userService, IProductService productService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this.messageService = messageService;
            this.userService = userService;
            this.productService = productService;
        }
        public override async Task<BaseResultDto<ProductLikeDto>> InsertAsyncDto(ProductLikeDto dto)
        {
            var item = await _context.ProductLikes.FirstOrDefaultAsync(s => s.ProductId == dto.ProductId && s.UserId == dto.UserId);
            if (item == null)
            {
                var user = await userService.FindAsyncDto(dto.UserId);
                var product = await productService.FindAsyncDto(dto.ProductId);
                await messageService.SendMessageAsync(messageType: Common.Enumerable.Message.MessageTypeEnum.UserLikeProduct, mobileReceptor: user.Data.Mobile, emailReceptor: user.Data.Email, token1: user.Data.FirstName, token2: product.Data.Name);

                return await base.InsertAsyncDto(dto);
            }
            return new BaseResultDto<ProductLikeDto>(isSuccess: true, data: null);
        }
        public override BaseResultDto DeleteDto(ProductLikeDto dto)
        {
            var item = _context.ProductLikes.FirstOrDefault(s => s.ProductId == dto.ProductId && s.UserId == dto.UserId);
            if (item != null)
            {
                _context.ProductLikes.Remove(item);
                _context.SaveChanges();
                return new BaseResultDto(isSuccess: false, val: Resource.Notification.Success);
            }
            return new BaseResultDto(isSuccess: false, val: Resource.Notification.NothingFound);
        }
        public ProductLikeSearchDto SearchDto(ProductLikeInputDto dto)
        {
            var model = _context.ProductLikes.Include(s => s.Product).ThenInclude(s => s.Category).Include(s => s.Product).ThenInclude(s => s.Picture).Where(s => s.UserId.Equals(dto.UserId)).Select(s => s.Product).AsQueryable();
            return new ProductLikeSearchDto(dto, model, mapper);
        }
    }
}
