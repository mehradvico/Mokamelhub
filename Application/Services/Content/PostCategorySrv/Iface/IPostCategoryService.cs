using Entities.Entities;
using System.Collections.Generic;

namespace Application.Services.Content.PostCategorySrv.Iface
{
    public interface IPostCategoryService
    {
        void InsertOrUpdate(Post post, long categoryId);
        void InsertOrUpdate(Post post, List<long> categoryIds);
    }
}
