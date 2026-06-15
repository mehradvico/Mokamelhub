using Application.Common.Service;
using Application.Services.Content.PostFileSrv.Dto;
using Application.Services.Content.PostFileSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Content.PostFileSrv
{
    public class PostFileService : CommonSrv<PostFile, PostFileDto>, IPostFileService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public PostFileService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public PostFileSearchDto Search(PostFileInputDto searchDto)
        {
            var model = _context.PostFiles.Include(s => s.File).AsQueryable();
            if (searchDto.PostId.HasValue)
            {
                model = model.Where(s => s.PostId.Equals(searchDto.PostId));
            }
            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                model = model.Where(s => s.Label.Contains(searchDto.Q));
            }
            return new PostFileSearchDto(searchDto, model, mapper);
        }
        public void InsertOrUpdate(PostFileDto postFile)
        {
            var item = _context.PostFiles.FirstOrDefault(s => s.PostId == postFile.PostId && s.FileId == postFile.FileId);
            if (item != null)
            {
                postFile.Label = item.Label;
                _context.PostFiles.Update(item);
            }
            else
            {
                item = mapper.Map<PostFile>(postFile);
                _context.PostFiles.Add(item);
            }
            _context.SaveChanges();
        }

        public void InsertOrUpdate(Post post, List<PostFileDto> postFilesDto)
        {
            if (post.PostFiles != null)
            {
                _context.PostFiles.RemoveRange(post.PostFiles);
                _context.SaveChanges();
            }
            else
            {
                post.PostFiles = new List<PostFile>();
            }
            postFilesDto.ForEach(s => s.PostId = post.Id);
            foreach (var item in postFilesDto)
            {
                InsertOrUpdate(item);
            }
        }
    }
}
