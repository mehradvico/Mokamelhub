using Application.Common.Dto.Result;
using Application.Services.Content.PostProductSrv.Dto;
using Application.Services.Content.PostProductSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Content.PostProductSrv
{
    public class PostProductService : IPostProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper _mapper;

        public PostProductService(IDataBaseContext _context, IMapper mapper)
        {
            this._context = _context;
            _mapper = mapper;
        }
        public async Task<BaseResultDto<PostProductDto>> GetPostProductsAsync(long postId)
        {
            var item = await _context.Posts.Include(s => s.Products.Where(s => s.Deleted == false && s.Active)).FirstOrDefaultAsync(s => s.Id == postId);
            if (item != null)
                return new BaseResultDto<PostProductDto>(true, _mapper.Map<PostProductDto>(item));
            return new BaseResultDto<PostProductDto>(false, _mapper.Map<PostProductDto>(item));
        }
        public void InsertOrUpdate(Post post, long productId)
        {
            var item = _context.Products.AsTracking().FirstOrDefault(s => s.Id == productId);
            if (item != null)
            {
                post.Products.Add(item);
                _context.SaveChanges();
            }
        }

        public BaseResultDto InsertOrUpdate(PostProductDto postProduct)
        {
            try
            {
                var post = _context.Posts.AsTracking().Include(s => s.Products).FirstOrDefault(s => s.Id == postProduct.Id);
                if (post.Products != null)
                {
                    post.Products.Clear();
                    _context.SaveChanges();
                }
                else
                {
                    post.Products = new List<Product>();
                }
                foreach (var item in postProduct.Products)
                {
                    InsertOrUpdate(post, item.Id);
                }
                return new BaseResultDto(true);
            }
            catch
            {
                return new BaseResultDto(false, Resource.Notification.Unsuccess);
            }


        }
    }
}
