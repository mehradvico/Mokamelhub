using Entities.Entities;
using System.Collections.Generic;

namespace Application.Services.Content.PostHashtagSrv.Iface
{
    public interface IPostHashtagService
    {
        void InsertOrUpdate(Post post, string hashtag);
        void InsertOrUpdate(Post post, List<string> hashtags);
    }
}
