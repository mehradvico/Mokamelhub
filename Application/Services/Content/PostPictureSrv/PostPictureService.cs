using Application.Common.Service;
using Application.Services.Content.PostPictureSrv.Dto;
using Application.Services.Content.PostPictureSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Content.PostPictureSrv
{
    public class PostPictureService : CommonSrv<PostPicture, PostPictureDto>, IPostPictureService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public PostPictureService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public PostPictureSearchDto Search(PostPictureInputDto searchDto)
        {
            var model = _context.PostPictures.Include(s => s.Picture).AsQueryable();
            if (searchDto.PostId.HasValue)
            {
                model = model.Where(s => s.PostId.Equals(searchDto.PostId));
            }
            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                model = model.Where(s => s.Label.Contains(searchDto.Q));
            }
            return new PostPictureSearchDto(searchDto, model, mapper);
        }
        public void InsertOrUpdate(PostPictureDto PostPicture)
        {
            var item = _context.PostPictures.FirstOrDefault(s => s.PostId == PostPicture.PostId && s.PictureId == PostPicture.PictureId);
            if (item != null)
            {
                PostPicture.Label = item.Label;
                _context.PostPictures.Update(item);
            }
            else
            {
                item = mapper.Map<PostPicture>(PostPicture);
                _context.PostPictures.Add(item);
            }
            _context.SaveChanges();
        }

        public void InsertOrUpdate(Post post, List<PostPictureDto> PostPicturesDto)
        {
            if (post.PostPictures != null)
            {
                _context.PostPictures.RemoveRange(post.PostPictures);
                _context.SaveChanges();
            }
            else
            {
                post.PostPictures = new List<PostPicture>();
            }
            PostPicturesDto.ForEach(s => s.PostId = post.Id);
            foreach (var item in PostPicturesDto)
            {
                InsertOrUpdate(item);
            }
        }
    }
}
