using Application.Common.Service;
using Application.Services.ProductSrvs.ProductPictureSrv.Dto;
using Application.Services.ProductSrvs.ProductPictureSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;


namespace Application.Services.ProductSrvs.ProductPictureSrv
{
    public class ProductPictureService : CommonSrv<ProductPicture, ProductPictureDto>, IProductPictureService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public ProductPictureService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public ProductPictureSearchDto SearchDto(ProductPictureInputDto dto)
        {

            var model = _context.ProductPictures.Include(p => p.Picture).AsQueryable();
            if (dto.ProductId.HasValue)
            {
                model = model.Where(s => s.ProductId.Equals(dto.ProductId));
            }
            if (!string.IsNullOrEmpty(dto.Q))
            {
                model = model.Where(s => s.Label.Contains(dto.Q));
            }
            return new ProductPictureSearchDto(dto, model, mapper);

        }
        public void InsertOrUpdate(ProductPictureDto productPicture)
        {
            var item = _context.ProductPictures.FirstOrDefault(s => s.ProductId == productPicture.ProductId && s.PictureId == productPicture.PictureId);
            if (item != null)
            {
                productPicture.Label = item.Label;
                _context.ProductPictures.Update(item);
            }
            else
            {
                item = mapper.Map<ProductPicture>(productPicture);
                _context.ProductPictures.Add(item);
            }
            _context.SaveChanges();
        }

        public void InsertOrUpdate(Product product, List<ProductPictureDto> productPicturesDto)
        {
            if (product.ProductPictures != null)
            {
                _context.ProductPictures.RemoveRange(product.ProductPictures);
                _context.SaveChanges();
            }
            else
            {
                product.ProductPictures = new List<ProductPicture>();
            }
            productPicturesDto.ForEach(s => s.ProductId = product.Id);
            foreach (var item in productPicturesDto)
            {
                InsertOrUpdate(item);
            }
        }
    }
}
