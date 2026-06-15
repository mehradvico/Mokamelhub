using Application.Common.Dto.Result;
using Application.Services.Content.PostSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.PostSrv.Dto
{
    public class PostSearchDto : BaseSearchDto<Post, PostVDto>, IPostSearchFields
    {
        public PostSearchDto(PostInputDto dto, IQueryable<Post> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.Publish = dto.Publish;
            this.Hashtags = dto.Hashtags;
            this.Active = dto.Active;
            this.AdminConfirm = dto.AdminConfirm;
            this.Available = dto.Available;
            this.NotId = dto.NotId;
            this.CategoryIds = dto.CategoryIds;
            this.CategoryLabels = dto.CategoryLabels;
            this.IsAndCategories = dto.IsAndCategories;
            this.AllAdminConfirm = dto.AllAdminConfirm;
            this.Edited = dto.Edited;
        }
        public bool? Publish { get; set; }
        public string Hashtags { get; set; }
        public bool? Active { get; set; }
        public bool? AdminConfirm { get; set; }
        public bool? AllAdminConfirm { get; set; }
        public bool? Edited { get; set; }
        public long? NotId { get; set; }
        public long[] CategoryIds { get; set; }
        public string[] CategoryLabels { get; set; }
        public bool IsAndCategories { get; set; }
    }
}
