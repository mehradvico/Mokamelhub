using Application.Services.CategorySrv.Iface;
using Application.Services.Content.PostCategorySrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Content.PostCategorySrv
{
    public class PostCategoryService : IPostCategoryService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        public PostCategoryService(IDataBaseContext _context, IMapper mapper, ICategoryService categoryService)
        {
            this._context = _context;
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        public void InsertOrUpdate(Post post, long categoryId)
        {
            var item = _context.Categories.AsTracking().FirstOrDefault(s => s.Id == categoryId);
            if (item != null)
            {
                post.Categories.Add(item);
                _context.SaveChanges();
            }
        }

        public void InsertOrUpdate(Post post, List<long> categoryIds)
        {
            if (post.Categories != null)
            {
                post.Categories.Clear();
                _context.SaveChanges();
            }
            else
            {
                post.Categories = new List<Category>();
            }
            foreach (var item in categoryIds)
            {
                InsertOrUpdate(post, item);
            }
        }
    }
}
