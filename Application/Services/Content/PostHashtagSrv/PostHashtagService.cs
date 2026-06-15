using Application.Services.Content.HashtagSrv.Iface;
using Application.Services.Content.PostHashtagSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;
using System.Collections.Generic;

namespace Application.Services.Content.PostHashtagSrv
{
    public class PostHashtagService : IPostHashtagService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IHashtagService hashtagService;

        public PostHashtagService(IDataBaseContext _context, IMapper mapper, IHashtagService hashtagService)
        {
            this._context = _context;
            this.mapper = mapper;
            this.hashtagService = hashtagService;
        }

        public void InsertOrUpdate(Post post, string hashtag)
        {
            var item = hashtagService.GetOrAddByName(hashtag);
            post.Hashtags.Add(item);
            //item.posts.Add(post);
            _context.SaveChanges();
        }

        public void InsertOrUpdate(Post post, List<string> hashtags)
        {
            if (post.Hashtags != null)
            {
                post.Hashtags.Clear();
                _context.SaveChanges();
            }
            else
            {
                post.Hashtags = new List<Hashtag>();
            }
            foreach (var item in hashtags)
            {
                InsertOrUpdate(post, item);
            }
        }
    }
}
